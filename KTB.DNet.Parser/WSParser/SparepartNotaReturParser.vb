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
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.Profile
#End Region

Namespace KTB.DNet.Parser
    Public Class SparepartNotaReturParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private oSparepartNotaRetur As SparepartNotaRetur
        Private arrSparepartNotaRetur As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

#Region "Protected Methods"
        Protected Overloads Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                arrSparepartNotaRetur = New ArrayList()
                oSparepartNotaRetur = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            oSparepartNotaRetur = ParseHeader(line)
                            If Not IsNothing(oSparepartNotaRetur) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then oSparepartNotaRetur.ErrorMessage = errorMessage.ToString()
                                arrSparepartNotaRetur.Add(oSparepartNotaRetur)
                                oSparepartNotaRetur = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparepartNotaReturParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparepartNotaReturParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        oSparepartNotaRetur = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return arrSparepartNotaRetur
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overloads Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facSparepartNotaRetur As New SparepartNotaReturFacade(user)
            For Each objSparepartNotaRetur As SparepartNotaRetur In arrSparepartNotaRetur
                Try
                    If Not IsNothing(objSparepartNotaRetur) Then
                        If objSparepartNotaRetur.ErrorMessage = String.Empty Then
                            If objSparepartNotaRetur.ID = 0 Then
                                If facSparepartNotaRetur.Insert(objSparepartNotaRetur) < 0 Then
                                    nError += 1
                                End If
                            Else
                                If facSparepartNotaRetur.Update(objSparepartNotaRetur) < 0 Then
                                    nError += 1
                                End If
                            End If
                        Else
                            Throw New Exception(objSparepartNotaRetur.ErrorMessage)
                            nError += 1
                        End If

                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & arrSparepartNotaRetur.Count.ToString(), "ws-worker", "SparepartNotaReturParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparepartNotaReturParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparepartNotaReturParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparepartNotaReturParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#End Region


#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As SparepartNotaRetur
            'H;DealerCode;NoDocument;TypeDocument;Month;Year;Filename
            'H;100001;004013;Nota Retur;8;2021;10001_2021.txt;


            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim _oSparepartNotaRetur As New SparepartNotaRetur

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                If Not ExistSparepartNotaReturCode(cols(1).Trim, _oSparepartNotaRetur) Then
                    '1 DealerCode
                    Try
                        Dim dlr As Dealer = New DealerFacade(user).Retrieve(cols(1).Trim)
                        _oSparepartNotaRetur.Dealer = dlr
                    Catch ex As Exception
                        Throw New Exception("Invalid Kode Dealer")
                    End Try
                End If

                '2 NoDocument
                Try
                    _oSparepartNotaRetur.NoDoc = cols(2).Trim
                Catch ex As Exception
                    Throw New Exception("Invalid No Document")
                End Try

                '3 TypeDocument
                Try
                    _oSparepartNotaRetur.TypeDoc = CInt(cols(3).Trim)
                Catch ex As Exception
                    Throw New Exception("Invalid Type Document")
                End Try

                '4 Month
                Try
                    _oSparepartNotaRetur.PeriodeMonth = CInt(cols(4).Trim)
                Catch ex As Exception
                    Throw New Exception("Invalid Periode Month")
                End Try

                '5 year
                Try
                    _oSparepartNotaRetur.PeriodeYear = CInt(cols(5).Trim)
                Catch ex As Exception
                    Throw New Exception("Invalid Periode Year")
                End Try

                '6 FileName
                Try
                    _oSparepartNotaRetur.FileNamePath = "NotaRetur\" & cols(6).Trim
                Catch ex As Exception
                    Throw New Exception("Invalid File Name")
                End Try


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    _oSparepartNotaRetur.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    _oSparepartNotaRetur.LastUpdateBy = "WS"
                End If
            End If

            Return _oSparepartNotaRetur
        End Function

        Private Function ExistSparepartNotaReturCode(ByVal kindCode As String, ByRef refSparepartNotaRetur As SparepartNotaRetur) As Boolean
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oSparepartNotaRetur As SparepartNotaRetur = New SparepartNotaReturFacade(user).Retrieve(kindCode)
            If oSparepartNotaRetur.ID > 0 Then
                refSparepartNotaRetur = oSparepartNotaRetur
                Return True
            End If
            Return False
        End Function
#End Region
    End Class

End Namespace
