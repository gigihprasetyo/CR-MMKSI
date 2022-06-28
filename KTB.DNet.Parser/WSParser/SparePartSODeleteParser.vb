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
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.Parser

    Public Class SparePartSODeleteParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aSOHs As ArrayList
        Private _oSOH As SparePartPOEstimate
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

                _aSOHs = New ArrayList()
                _oSOH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oSOH) Then
                                _aSOHs.Add(_oSOH)
                                _oSOH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oSOH = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartSODeleteParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSODeleteParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oSOH = Nothing
                    End Try
                Next

                If Not IsNothing(_oSOH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oSOH.ErrorMessage = errorMessage.ToString()
                    _aSOHs.Add(_oSOH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Dim aDatas As New ArrayList
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For i As Integer = 0 To _aSOHs.Count - 1
                _oSOH = CType(_aSOHs(i), SparePartPOEstimate)
                If Not IsNothing(_oSOH.ErrorMessage) AndAlso _oSOH.ErrorMessage.Trim() <> String.Empty Then
                    If nError = 0 Then
                        SysLogParameter.LogErrorToSyslog("Start : Log Error of SparePartDODeleteParser", "ws-worker", "SparePartDODeleteParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDODeleteParser, BlockName)
                    End If
                    sMsg = sMsg & _oSOH.ErrorMessage.Trim() & ";"
                    nError += 1
                    SysLogParameter.LogErrorToSyslog(_oSOH.ErrorMessage, "ws-worker", "SparePartDODeleteParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDODeleteParser, BlockName)
                    'Else
                    'aDatas.Add(_oSOH)
                End If
                aDatas.Add(_oSOH)
            Next
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _aSOHs.Count.ToString() & " Data", "ws-worker", "SparePartDODeleteParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDODeleteParser, BlockName)
                SysLogParameter.LogErrorToSyslog("End : Log Error of SparePartPOEstimateParser", "ws-worker", "SparePartDODeleteParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDODeleteParser, BlockName)

                Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _aSOHs.Count.ToString() & " Data. Message : " & sMsg)
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                'Throw e
            End If
            _aSOHs = New ArrayList()
            _aSOHs = aDatas

            Return _aSOHs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As SparePartPOEstimateFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objSparePartPOEstimate As SparePartPOEstimate In _aSOHs
                Try

                    If Not IsNothing(objSparePartPOEstimate.ErrorMessage) AndAlso objSparePartPOEstimate.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objSparePartPOEstimate.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New SparePartPOEstimateFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.DeleteFromWebSevice(objSparePartPOEstimate)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "SparePartSODeleteParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSODeleteParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objSparePartPOEstimate.SONumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aSOHs.Count.ToString(), "ws-worker", "SparePartSODeleteParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSODeleteParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartSODeleteParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSODeleteParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As SparePartPOEstimate
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objSparePartPOEstimate As New SparePartPOEstimate
            Dim objSparePartPOEstimateFac As New SparePartPOEstimateFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length <> 2 Then
                writeError("Invalid Header Format")
            Else
                '1 SO Number
                If cols(1).Trim = String.Empty Then
                    writeError("SO Number can't be empty")
                Else
                    Try
                        Dim SONumber As String = cols(1).Trim
                        objSparePartPOEstimate = objSparePartPOEstimateFac.Retrieve(SONumber)
                        If Not IsNothing(objSparePartPOEstimate) AndAlso objSparePartPOEstimate.ID > 0 Then
                            objSparePartPOEstimate.RowStatus = CType(DBRowStatus.Deleted, Short)
                        Else
                            writeError("There is no SO with number : " & cols(1).Trim)
                        End If
                    Catch ex As Exception
                        writeError("Delete SO error: " & ex.Message)
                    End Try
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objSparePartPOEstimate) Then objSparePartPOEstimate = New SparePartPOEstimate()
                    objSparePartPOEstimate.ErrorMessage = errorMessage.ToString()
                Else
                    objSparePartPOEstimate.LastUpdateBy = "SAP"
                End If
            End If

            Return objSparePartPOEstimate
        End Function

#End Region

    End Class
End Namespace
