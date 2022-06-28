#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.Parser

    Public Class DealerOperationAreaBussinessParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _arrDlrOprAreaBussiness As ArrayList
        Private _DlrOprAreaBussiness As DealerOperationAreaBussiness
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

                _arrDlrOprAreaBussiness = New ArrayList()
                _DlrOprAreaBussiness = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_DlrOprAreaBussiness) Then
                                _arrDlrOprAreaBussiness.Add(_DlrOprAreaBussiness)
                                _DlrOprAreaBussiness = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _DlrOprAreaBussiness = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "LeasingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DlrOprAreaBussiness = Nothing
                    End Try
                Next

                If Not IsNothing(_DlrOprAreaBussiness) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _DlrOprAreaBussiness.ErrorMessage = errorMessage.ToString()
                    _arrDlrOprAreaBussiness.Add(_DlrOprAreaBussiness)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try


            Return _arrDlrOprAreaBussiness
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As DealerStallEquipmentFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objStallEquipment As DealerStallEquipment In _arrDlrOprAreaBussiness
                Try

                    If Not IsNothing(objStallEquipment.ErrorMessage) AndAlso objStallEquipment.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objStallEquipment.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New DealerStallEquipmentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.Insert(objStallEquipment)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objStallEquipment.StallEquipment & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrDlrOprAreaBussiness.Count.ToString(), "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "LeasingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LeasingParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As DealerOperationAreaBussiness
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objStallEquipment As New DealerStallEquipment
            Dim func As New DealerStallEquipmentFacade(user)


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

                    objStallEquipment.DealerID = Me.GetDealerID(Code)
                Catch ex As Exception
                    writeError("Code error: " & ex.Message)
                End Try


                Try '2 Name
                    Dim Name As String = cols(2).Trim

                    If Name = String.Empty Then
                        writeError("Name can't be empty")
                    End If

                    Select Case Name.ToLower
                        Case "stallsmall"
                            objStallEquipment.StallEquipment = 1

                        Case "stallmedium"
                            objStallEquipment.StallEquipment = 2

                        Case "etc"
                            objStallEquipment.StallEquipment = 3

                    End Select

                Catch ex As Exception
                    writeError("Name error: " & ex.Message)
                End Try


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then

                    objStallEquipment.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objStallEquipment.LastUpdateBy = "WS"
                    'objWitholdTax.Status = 1
                End If
            End If

            Return Nothing
        End Function



        Private Function GetDealerID(ByVal dealerCode As String) As Integer
            Dim result As Integer = 0

            Try
                Dim objDealer As New Dealer

                objDealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(dealerCode)
                result = objDealer.ID
            Catch ex As Exception

            End Try
            Return IIf(result = 0, 0, result)

        End Function
#End Region

    End Class
End Namespace
