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
Imports KTB.DNet.BusinessFacade

#End Region

Namespace KTB.DNet.Parser

    Public Class SparePartPaymentParser
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
                    '    aDatas.Add(_oDOH)
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
            'Dim objSparePartFlow As New V_SparePartFlow
            'Dim objSparePartFlowFac As New V_SparePartFlowFacade(user)

            Dim objSparePartDO As New SparePartDO
            Dim objSparePartDOFac As New SparePartDOFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length <> 3 Then
                writeError("Invalid Header Format")
            Else
                '0 DO Number
                If cols(1).Trim = String.Empty Then
                    writeError("SO Number can't be empty")
                Else
                    Try
                        Dim SONumber As String = cols(1).Trim

                        Dim objSparePartDOFacade As SparePartDOFacade = New SparePartDOFacade(user)
                        Dim strsql As String = ""
                        strsql += "select ID from Sparepartdo"
                        strsql += " where id in"
                        strsql += " ("
                        strsql += " select dodet.sparepartdoid "
                        strsql += " from sparepartdodetail dodet"
                        strsql += " , SparePartPOEstimate est"
                        strsql += " where 1 = 1"
                        strsql += " and dodet.SparePartPOEstimateID = est.ID"
                        strsql += " and est.SONumber = '" & SONumber & "'"
                        strsql += " and dodet.RowStatus = 0"
                        strsql += " and est.RowStatus = 0"
                        strsql += ")"

                        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartDO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartDO), "ID", MatchType.InSet, "(" & strsql & ")"))
                        Dim arlDO As ArrayList = objSparePartDOFacade.Retrieve(criterias)

                        If arlDO.Count > 0 Then
                            For Each item As SparePartDO In arlDO
                                objSparePartDO = item
                                objSparePartDO.PaymentDate = MyBase.GetDateLong(cols(2).Trim)
                                'objSparePartDOFacade.Update(item)
                            Next
                        Else
                            writeError("There is no DO with SO Number : " & cols(1).Trim & Environment.NewLine)
                        End If

                        'Dim objSOFacade As SparePartPOEstimateFacade = New SparePartPOEstimateFacade(user)
                        'Dim objSO As SparePartPOEstimate = objSOFacade.Retrieve(SONumber)
                        'If Not IsNothing(objSO) AndAlso objSO.ID > 0 Then
                        '    Dim objDODetailFacade As SparePartDODetailFacade = New SparePartDODetailFacade(user)

                        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartDODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartDODetail), "SparePartPOEstimate.SONumber", MatchType.Exact, SONumber))
                        '    Dim arlDODetail As ArrayList = objDODetailFacade.Retrieve(criterias)
                        '    If arlDODetail.Count > 0 Then
                        '        Dim objSparePartDODetail As SparePartDODetail = CType(arlDODetail(0), SparePartDODetail)
                        '        objSparePartDO = objSparePartDODetail.SparePartDO
                        '        objSparePartDO.PaymentDate = MyBase.GetDateLong(cols(2).Trim)
                        '    Else
                        '        writeError("There is no SO with number : " & cols(1).Trim & " on DO")
                        '    End If
                        'Else
                        '    writeError("There is no SO with number : " & cols(1).Trim)
                        'End If

                        'objSparePartFlow = objSparePartFlowFac.RetrieveBySONUmber(SONumber)
                        'If Not IsNothing(objSparePartFlow) AndAlso objSparePartFlow.SOID > 0 Then
                        '    objSparePartDO = objSparePartDOFac.Retrieve(objSparePartFlow.DOID)
                        '    If Not IsNothing(objSparePartDO) Then
                        '        objSparePartDO.PaymentDate = MyBase.GetDateLong(cols(2).Trim)
                        '    End If
                        'Else
                        '    writeError("There is no SO with number : " & cols(1).Trim)
                        'End If
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
