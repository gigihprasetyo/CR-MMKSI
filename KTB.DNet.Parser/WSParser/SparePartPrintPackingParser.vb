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

    Public Class SparePartPrintPackingParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aDOHs As ArrayList
        Private _oDOH As SparePartDO
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

                _aDOHs = New ArrayList()
                _oDOH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oDOH) Then
                                _aDOHs.Add(_oDOH)
                                _oDOH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oDOH = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartDOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDOParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oDOH = Nothing
                    End Try
                Next

                If Not IsNothing(_oDOH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oDOH.ErrorMessage = errorMessage.ToString()
                    _aDOHs.Add(_oDOH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Dim aDatas As New ArrayList
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For i As Integer = 0 To _aDOHs.Count - 1
                _oDOH = CType(_aDOHs(i), SparePartDO)
                If Not IsNothing(_oDOH.ErrorMessage) AndAlso _oDOH.ErrorMessage.Trim() <> String.Empty Then
                    If nError = 0 Then
                        SysLogParameter.LogErrorToSyslog("Start : Log Error of SparePartDOParser", "ws-worker", "SparePartDOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDOParser, BlockName)
                    End If
                    sMsg = sMsg & _oDOH.ErrorMessage.Trim() & ";"
                    nError += 1
                    SysLogParameter.LogErrorToSyslog(_oDOH.ErrorMessage, "ws-worker", "SparePartDOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDOParser, BlockName)
                    'Else
                    'aDatas.Add(_oDOH)
                End If
                aDatas.Add(_oDOH)
            Next
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _aDOHs.Count.ToString() & " Data", "ws-worker", "SparePartDOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDOParser, BlockName)
                SysLogParameter.LogErrorToSyslog("End : Log Error of SparePartDOParser", "ws-worker", "SparePartDOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDOParser, BlockName)

                Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _aDOHs.Count.ToString() & " Data. Message : " & sMsg)
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                'Throw e
            End If
            _aDOHs = New ArrayList()
            _aDOHs = aDatas

            Return _aDOHs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As SparePartDOFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objSparePartDO As SparePartDO In _aDOHs
                Try

                    If Not IsNothing(objSparePartDO.ErrorMessage) AndAlso objSparePartDO.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objSparePartDO.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New SparePartDOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.Update(objSparePartDO)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "SparePartDOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objSparePartDO.DONumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aDOHs.Count.ToString(), "ws-worker", "SparePartDOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartDOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As SparePartDO
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objSparePartDO As New SparePartDO
            Dim objSparePartDOFac As New SparePartDOFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length <> 3 Then
                writeError("Invalid Header Format")
            Else
                '0 DO Number
                If cols(1).Trim = String.Empty Then
                    writeError("DO Number can't be empty")
                Else
                    Try
                        Dim DONumber As String = cols(1).Trim
                        objSparePartDO = objSparePartDOFac.Retrieve(DONumber)
                        If Not IsNothing(objSparePartDO) AndAlso objSparePartDO.ID > 0 Then
                            If cols(2).Trim <> String.Empty Then
                                objSparePartDO.ReadyForDeliveryDate = MyBase.GetDateLong(cols(2).Trim)
                            End If
                        Else
                            writeError("There is no DO with number : " & cols(1).Trim)
                        End If
                    Catch ex As Exception
                        writeError("Delete DO error: " & ex.Message)
                    End Try
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objSparePartDO) Then objSparePartDO = New SparePartDO()
                    objSparePartDO.ErrorMessage = errorMessage.ToString()
                Else
                    objSparePartDO.LastUpdateBy = "SAP"
                End If
            End If

            Return objSparePartDO
        End Function

#End Region

    End Class
End Namespace
