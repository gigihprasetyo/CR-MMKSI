#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNET.Domain
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.Parser

    Public Class DealerMasterWitholdTaxParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _arrWitholdTax As ArrayList
        Private _witholdTax As DealerMasterWITHOLDTax
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _arrWitholdTax = New ArrayList()
                _witholdTax = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_witholdTax) Then
                                _arrWitholdTax.Add(_witholdTax)
                                _witholdTax = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _witholdTax = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "LeasingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _witholdTax = Nothing
                    End Try
                Next

                If Not IsNothing(_witholdTax) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _witholdTax.ErrorMessage = errorMessage.ToString()
                    _arrWitholdTax.Add(_witholdTax)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try


            Return _arrWitholdTax
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As LeasingFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objLeasing As Leasing In _arrWitholdTax
                Try

                    If Not IsNothing(objLeasing.ErrorMessage) AndAlso objLeasing.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objLeasing.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New LeasingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.InsertFromWebSevice(objLeasing)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objLeasing.LeasingCode & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrWitholdTax.Count.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As DealerMasterWITHOLDTax
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objWitholdTax As New DealerMasterWITHOLDTax
            Dim func As New DealerMasterWITHOLDTaxFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                Dim strData As String = String.Empty


                Try '1 Code
                    Dim Code As String = cols(1).Trim

                    If Code = String.Empty Then
                        writeError("Code can't be empty")
                    End If

                    objWitholdTax.WithholdTaxCode = Code
                Catch ex As Exception
                    writeError("Code error: " & ex.Message)
                End Try


                Try '2 Name
                    Dim Name As String = cols(2).Trim & " " & cols(3).Trim

                    If Name = String.Empty Then
                        writeError("Name can't be empty")
                    End If

                    objWitholdTax.WithholdTaxtipe = Name

                Catch ex As Exception
                    writeError("Name error: " & ex.Message)
                End Try


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then

                    objWitholdTax.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objWitholdTax.LastUpdateBy = "WS"
                    'objWitholdTax.Status = 1
                End If
            End If

            Return objWitholdTax
        End Function



        Private Function GetProvinceName(ByVal ProvinceCode As String) As String
            Dim result As String = String.Empty

            Try
                Dim pp As New Province

                pp = New ProvinceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(ProvinceCode)
                result = pp.ProvinceName
            Catch ex As Exception

            End Try
            Return IIf(result = String.Empty, ProvinceCode, result)

        End Function
#End Region

    End Class
End Namespace
