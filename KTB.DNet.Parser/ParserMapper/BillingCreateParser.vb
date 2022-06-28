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
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Transfer
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Service

#End Region

Public Class BillingCreateParser
    Inherits AbstractParser

#Region "Private Variables"
    Private _Stream As StreamReader
    Private Grammar As Regex
    Private _fileName As String
    Private errorMessage As StringBuilder

    Private _aDOHs As ArrayList
    Private _oDOH As SparePartDO

    Private _monthlyDocuments As ArrayList
    Private _monthlyDocument As MonthlyDocument
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

            _monthlyDocuments = New ArrayList()
            _monthlyDocument = Nothing

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        If Not IsNothing(_monthlyDocument) Then
                            _monthlyDocuments.Add(_monthlyDocument)
                            _monthlyDocument = Nothing
                        End If

                        errorMessage = New StringBuilder()
                        _monthlyDocument = ParseHeader(line)

                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "BillingCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BillingCreateParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    _oDOH = Nothing
                End Try
            Next

            If Not IsNothing(_monthlyDocument) Then
                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _monthlyDocument.ErrorMessage = errorMessage.ToString()
                _monthlyDocuments.Add(_monthlyDocument)
            End If

        Catch ex As Exception
            Throw ex
        Finally

        End Try

        Dim aDatas As New ArrayList
        Dim nError As Integer = 0
        Dim sMsg As String = ""

        For i As Integer = 0 To _aDOHs.Count - 1
            '_oDOH = CType(_aDOHs(i), SparePartDO)
            _monthlyDocument = CType(_monthlyDocuments(i), MonthlyDocument)
            If Not IsNothing(_monthlyDocument.ErrorMessage) AndAlso _monthlyDocument.ErrorMessage.Trim() <> String.Empty Then
                If nError = 0 Then
                    SysLogParameter.LogErrorToSyslog("Start : Log Error of BillingCreateParser", "ws-worker", "BillingCreate.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BillingCreateParser, BlockName)
                End If
                sMsg = sMsg & _monthlyDocument.ErrorMessage.Trim() & ";"
                nError += 1
                SysLogParameter.LogErrorToSyslog(_monthlyDocument.ErrorMessage, "ws-worker", "BillingCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BillingCreateParser, BlockName)
                'Else
                '    aDatas.Add(_oDOH)
            End If
            aDatas.Add(_monthlyDocument)
        Next
        If nError > 0 Then
            SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _monthlyDocuments.Count.ToString() & " Data", "ws-worker", "BillingCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BillingCreateParser, BlockName)
            SysLogParameter.LogErrorToSyslog("End : Log Error of BillingCreateParser", "ws-worker", "BillingCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BillingCreateParser, BlockName)

            Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _monthlyDocuments.Count.ToString() & " Data. Message : " & sMsg)
            'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            'Throw e
        End If
        _monthlyDocuments = New ArrayList()
        _monthlyDocuments = aDatas

        Return _monthlyDocuments
    End Function

    Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
        Return Nothing
    End Function

    Protected Overrides Function DoTransaction() As Integer

        Dim doFacade As SparePartDOFacade
        Dim _monthlyDocumentFacade As MonthlyDocumentFacade
        Dim nError As Integer = 0
        Dim sMsg As String = ""

        For Each objMonthlyDocument As MonthlyDocument In _monthlyDocuments
            Try

                If Not IsNothing(objMonthlyDocument.ErrorMessage) AndAlso objMonthlyDocument.ErrorMessage <> "" Then
                    nError += 1
                    sMsg = sMsg & objMonthlyDocument.ErrorMessage.ToString() & ";"
                Else
                    _monthlyDocumentFacade = New MonthlyDocumentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    _monthlyDocumentFacade.Update(objMonthlyDocument)
                End If
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "SparePartDOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objMonthlyDocument.Dealer.DealerCode & Chr(13) & Chr(10) & objMonthlyDocument.Kind & Chr(13) & Chr(10) & objMonthlyDocument.BillingDate.ToString() & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
        Next

        If nError > 0 Then
            SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _monthlyDocuments.Count.ToString(), "ws-worker", "BillingCreateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BillingCreateParser, BlockName)
            SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "BillingCreateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BillingCreateParser, BlockName)
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

    Private Function ParseHeader(ByVal line As String) As MonthlyDocument
        Dim cols As String() = line.Split(MyBase.ColSeparator)
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
        'Dim objSparePartFlow As New V_SparePartFlow
        'Dim objSparePartFlowFac As New V_SparePartFlowFacade(user)

        Dim objSparePartDO As New SparePartDO
        Dim objSparePartDOFac As New SparePartDOFacade(user)

        Dim objMonthlyDocument As New MonthlyDocument
        Dim objMonthlyDocumentFacade As New MonthlyDocumentFacade(user)

        errorMessage = New StringBuilder()
        If cols.Length <> 7 Then
            writeError("Invalid Header Format")
        Else
            If cols(1).Trim = String.Empty Then
                writeError("Dealer Code can't be empty")
            ElseIf cols(2).Trim = String.Empty Then
                writeError("Kind can't be empty")
            ElseIf cols(3).Trim = String.Empty Then
                writeError("Billing Date Number can't be empty")
            ElseIf cols(4).Trim = String.Empty Then
                writeError("Accounting Number can't be empty")
            ElseIf cols(5).Trim = String.Empty Then
                writeError("Billing Number can't be empty")
            Else
                Try
                    Dim productCategory As String = cols(1).Trim
                    Dim dealerCode As String = cols(2).Trim
                    Dim kind As String = cols(3).Trim
                    Dim billingDate As String = cols(4).Trim
                    Dim accountingNo As String = cols(6).Trim
                    Dim billingNo As String = cols(5).Trim

                    'Dim explodedBillingDate As String() = billingDate.Split("-")
                    'billingDate = explodedBillingDate(2) & "-" & explodedBillingDate(1) & "-" & explodedBillingDate(0)

                    'Dim objSparePartDOFacade As SparePartDOFacade = New SparePartDOFacade(user)
                    'objMonthlyDocumentFacade = New MonthlyDocumentFacade(user)

                    'Dim strsql As String = ""
                    'strsql += "select * "
                    'strsql += "from MonthlyDocument "
                    ''strsql += "where RowStatus=1 "
                    'strsql += "where "
                    'strsql += "DealerID = (SELECT ID FROM Dealer WHERE DealerCode='" & dealerCode & "') "
                    'strsql += "and Kind = " & kind & " "
                    'strsql += "and PeriodeYear = year('" & billingDate & "') "
                    'strsql += "and PeriodeMonth = month('" & billingDate & "') "
                    'strsql += "and ProductCategoryID = 1 "
                    Dim strsql As String = "(SELECT ID FROM Dealer WHERE DealerCode='" & dealerCode & "') "

                    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartDO), "ID", MatchType.InSet, "(" & strsql & ")"))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.ID", MatchType.InSet, "(" & strsql & ")"))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "ProductCategory.ID", MatchType.Exact, productCategory))

                    Select Case kind
                        Case "1"
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.InSet, "(1, 6, 7)"))
                        Case "2"
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.InSet, "(2, 5)"))
                        Case "4"
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.InSet, "(4)"))
                        Case "13"
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.InSet, "(12, 13)"))
                        Case "15"
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.InSet, "(14, 15)"))
                    End Select
                    'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.Exact, kind))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeYear", MatchType.Exact, billingDate.Substring(0, 4)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeMonth", MatchType.Exact, billingDate.Substring(4, 2)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "ProductCategory.ID", MatchType.Exact, "1"))
                    'Dim arlDO As ArrayList = objSparePartDOFacade.Retrieve(criterias)
                    Dim arlMonthlyDocument As ArrayList = objMonthlyDocumentFacade.Retrieve(criterias)

                    If arlMonthlyDocument.Count > 0 Then
                        For Each item As MonthlyDocument In arlMonthlyDocument
                            objMonthlyDocument = item
                            objMonthlyDocument.AccountingNo = accountingNo.Trim()
                            objMonthlyDocument.BillingNo = billingNo.Trim()

                            Dim days As Integer = CType(billingDate.Substring(6, 2), Integer)
                            Dim month As Integer = CType(billingDate.Substring(4, 2), Integer)
                            Dim year As Integer = CType(billingDate.Substring(0, 4), Integer)
                            objMonthlyDocument.BillingDate = New DateTime(CType(billingDate.Substring(0, 4), Integer), CType(billingDate.Substring(4, 2), Integer), days)
                            objMonthlyDocumentFacade.Update(item)
                        Next
                    Else
                        writeError("There is no Monthly Document with Dealer Code : " & dealerCode & ", Kind : " & kind & ", Billing Date : " & billingDate & Environment.NewLine)
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
                    writeError("Create Billing error: " & ex.Message)
                End Try
            End If

            If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                If IsNothing(objMonthlyDocument) Then objMonthlyDocument = New MonthlyDocument()
                objMonthlyDocument.ErrorMessage = errorMessage.ToString()
            Else
                objMonthlyDocument.LastUpdateBy = "SAP"
                objMonthlyDocument.LastUpdateTime = DateTime.Now
            End If
        End If

        'Return objSparePartDO
        Return objMonthlyDocument
    End Function

#End Region

End Class
