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
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

Namespace KTB.DNet.Parser
    Public Class MSPEXFakturPajakParser
        Inherits AbstractParser


#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objMSPExFakturPajak As MSPExFakturPajak
        Private _arrMSPExFakturPajak As ArrayList
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

                _arrMSPExFakturPajak = New ArrayList()
                objMSPExFakturPajak = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek MSPExFakturPajak
                            objMSPExFakturPajak = ParseHeader(line)
                            ' insert to array objek MSPExFakturPajak
                            If Not IsNothing(objMSPExFakturPajak) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objMSPExFakturPajak.ErrorMessage = errorMessage.ToString()
                                _arrMSPExFakturPajak.Add(objMSPExFakturPajak)
                                objMSPExFakturPajak = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MSPExFakturPajakParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExFakturPajakParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        objMSPExFakturPajak = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrMSPExFakturPajak
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facMSPExFakturPajak As New MSPExFakturPajakFacade(user)
            For Each objMSPExFakturPajak As MSPExFakturPajak In _arrMSPExFakturPajak
                Try
                    If Not IsNothing(objMSPExFakturPajak) Then
                        If objMSPExFakturPajak.ErrorMessage = String.Empty Then
                            Dim oMSPExFakturPajak As MSPExFakturPajak = facMSPExFakturPajak.RetrieveByRegNumber(objMSPExFakturPajak.MSPExRegistration.RegNumber)
                            If oMSPExFakturPajak.ID = 0 Then
                                If facMSPExFakturPajak.Insert(objMSPExFakturPajak) < 0 Then
                                    nError += 1
                                End If
                            Else
                                objMSPExFakturPajak.ID = oMSPExFakturPajak.ID
                                If facMSPExFakturPajak.Update(objMSPExFakturPajak) < 0 Then
                                    nError += 1
                                End If
                            End If
                        Else
                            Throw New Exception(objMSPExFakturPajak.ErrorMessage)
                            nError += 1
                        End If
                    Else
                        Throw New Exception(objMSPExFakturPajak.ErrorMessage)
                        nError += 1
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrMSPExFakturPajak.Count.ToString(), "ws-worker", "MSPExFakturPajakParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExFakturPajakParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MSPExFakturPajakParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExFakturPajakParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As MSPExFakturPajak
            'K;MSPEXFAKTURPAJAK_TIMESTAMP\nH;MSPRegNumber;FakturPajakNo;amount\n
            'K;MSPEXFAKTURPAJAK_20210106154405\nH;EX00000006;1312828111190;2650000\n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objMSPExFakturPajak As New MSPExFakturPajak
            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 MSPRegNumber
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("MSPRegNumber can't be empty")
                Else
                    Try
                        Dim objMSPExRegistration As MSPExRegistration = New MSPExRegistrationFacade(user).RetrieveByRegNumber(PDCode)
                        If Not IsNothing(objMSPExRegistration) AndAlso objMSPExRegistration.ID > 0 Then
                            objMSPExFakturPajak.MSPExRegistration = objMSPExRegistration
                        Else
                            Throw New Exception("Invalid MSP Ext RegNumber " & PDCode)
                        End If
                    Catch ex As Exception
                        writeError("MSPRegNumber  error: " & ex.Message)
                    End Try
                End If


                '2 FakturPajakNo
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("FakturPajakNo can't be empty")
                Else
                    objMSPExFakturPajak.FakturPajakNo = PDCode
                End If

                '3 Amount
                Try
                    objMSPExFakturPajak.Amount = MyBase.GetCurrency(cols(3))
                Catch ex As Exception
                    errorMessage.Append("Invalid Amount")
                End Try

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objMSPExFakturPajak.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objMSPExFakturPajak.LastUpdatedBy = "WS"
                End If
            End If

            Return objMSPExFakturPajak
        End Function
#End Region

    End Class
End Namespace
