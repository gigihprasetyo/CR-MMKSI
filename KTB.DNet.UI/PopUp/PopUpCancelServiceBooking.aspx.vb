Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text
Imports KTB.DNet.WebApi.Models
Imports KTB.DNet.WebApi.Models.SalesForce
Imports System.Collections.Generic
Imports System.Linq

Public Class PopUpCancelServiceBooking
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private standardCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    Private appConfFacade As AppConfigFacade = New AppConfigFacade(User)
    Private ccRFacade As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)

#Region " Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullorEmpty(Request.QueryString("id")) AndAlso CInt(Request.QueryString("id")) <> 0 Then
                sessHelper.SetSession("ID", Request.QueryString("id"))
                sessHelper.SetSession("mode", Request.QueryString("mode"))               
                BindResponse(sessHelper.GetSession("mode"))
            End If
        End If

    End Sub

    Private Function sendSalesForce() As Boolean
        Dim boolSF As Boolean = False
        Dim nResult As Integer = 0
        'send ke salesforce
        Dim objresponseFacade As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)
        Dim _criteriass = New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _criteriass.opAnd(New Criteria(GetType(CustomerCaseResponse), "ServiceBooking.ID", MatchType.Exact, sessHelper.GetSession("ID")))
        '_criteriass.opAnd(New Criteria(GetType(CustomerCaseResponse), "ValueId", MatchType.Exact, rbtnResponses.SelectedValue))
        'objCCRNew.Response = CShort(CType(New StandardCodeFacade(User).RetrieveByCriteria(_criteriass)(0), StandardCode).ValueId)
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(CustomerCaseResponse), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        '                        Dim moduleColl As ArrayList = New AssistModuleFacade(User).Retrieve(CRITERIAs, sortColl)
        Dim arrlstCCRSF As ArrayList = New CustomerCaseResponseFacade(User).Retrieve(_criteriass, sortColl)
        If arrlstCCRSF.Count > 0 Then
            Dim objresponse As New CustomerCaseResponse
            objresponse = CType(arrlstCCRSF(0), CustomerCaseResponse)
            Dim sf As SalesForceInterface = New SalesForceInterface()
            Dim msg As String = String.Empty
            Dim vSFreturn As Boolean = False
            'Dim nResult As Integer = 0
            Dim arr = New ArrayList

            vSFreturn = sf.UpdateCase(objresponse)

            If vSFreturn Then
                'Update IsSend
                Dim objResponseNew As CustomerCaseResponse = New CustomerCaseResponse()
                If objresponse.ID > 0 Then
                    objResponseNew = New CustomerCaseResponseFacade(User).Retrieve(objresponse.ID)
                End If
                objResponseNew.IsSend = 1 'sent done
                nResult = New CustomerCaseResponseFacade(User).Update(objResponseNew)
                If nResult <> -1 Then
                    boolSF = True
                    'MessageBox.Show("Proses Send SalesForce sukses")
                    'MessageBox.Show(SR.SaveSuccess)
                    'MessageBox.Show("Service Booking Dibatalkan.")
                    'RegisterStartupScript("Close", "<script>onSuccess();</script>")
                    Return boolSF
                End If
            Else
                Dim _criteriasss As New CriteriaComposite(New Criteria(GetType(WsLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                _criteriasss.opAnd(New Criteria(GetType(WsLog), "Source", MatchType.Exact, "Internal"))
                _criteriasss.opAnd(New Criteria(GetType(WsLog), "Status", MatchType.Exact, "False"))
                _criteriasss.opAnd(New Criteria(GetType(WsLog), "Body", MatchType.[Partial], "SALESFORCEIUPDATECASE"))
                _criteriasss.opAnd(New Criteria(GetType(WsLog), "Body", MatchType.[Partial], objresponse.CustomerCase.SalesforceID))
                _criteriasss.opAnd(New Criteria(GetType(WsLog), "CreatedTime", MatchType.GreaterOrEqual, DateTime.Now.AddMinutes(-1)))

                Dim objWsLog As WsLog
                Dim sortColls As SortCollection = New SortCollection
                sortColls.Add(New Sort(GetType(WsLog), "CreatedTime", Sort.SortDirection.ASC))
                arr = New WsLogFacade(User).Retrieve(_criteriasss, sortColls)
                If arr.Count > 0 Then
                    objWsLog = CType(arr(arr.Count - 1), WsLog)
                Else
                    objWsLog = New WsLog
                End If
                If arr.Count > 0 Then
                    If (objWsLog.Message.Trim() = String.Empty Or IsDBNull(objWsLog.Message)) Then
                        'Update IsSend
                        Dim objResponseNew As CustomerCaseResponse = New CustomerCaseResponse()
                        If objresponse.ID > 0 Then
                            objResponseNew = New CustomerCaseResponseFacade(User).Retrieve(objresponse.ID)
                        End If
                        objResponseNew.IsSend = 1 'sent done
                        nResult = New CustomerCaseResponseFacade(User).Update(objResponseNew)
                        If nResult <> -1 Then
                            boolSF = True
                            'nResult = New ServiceBookingFacade(User).UpdateStatus(sessHelper.GetSession("ID"))
                            'If nResult <> -1 Then
                            'MessageBox.Show(SR.SaveSuccess)
                            'MessageBox.Show("Service Booking Dibatalkan.")
                            'RegisterStartupScript("Close", "<script>onSuccess();</script>")
                            Return boolSF
                            'End If
                            'MessageBox.Show(SR.SaveSuccess)
                        End If

                    Else
                        'MessageBox.Show("Proses Send SalesForce gagal")
                        Return boolSF
                    End If
                Else
                    'MessageBox.Show("Proses Send SalesForce gagal")
                    Return boolSF
            End If
            End If
        End If
        Return boolSF
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If rbtnResponses.SelectedValue = "" Then
            MessageBox.Show("Pilih alasan cancel Service Booking.")
            Exit Sub
        End If

        Dim boolSF As Boolean = sendSalesForce()
        Dim intIDCCR As Integer
        Dim objCCR As New CustomerCaseResponse
        Dim objCC As New CustomerCase
        crit = New CriteriaComposite(New Criteria(GetType(ServiceReminderFollowUp), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ServiceReminderFollowUp), "ServiceBooking.ID", MatchType.Exact, sessHelper.GetSession("ID")))
        Dim arrLstGSR As ArrayList = New ServiceReminderFollowUpFacade(User).Retrieve(crit)
        Dim objGSR As New KTB.DNet.Domain.ServiceReminderFollowUp
        If arrLstGSR.Count > 0 Then
            objGSR = CType(arrLstGSR(0), ServiceReminderFollowUp)
            Dim crits = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(ServiceReminder), "ID", MatchType.Exact, objGSR.ServiceReminder.ID))
            Dim arrLstSR As ArrayList = New ServiceReminderFacade(User).Retrieve(crits)
            If arrLstSR.Count > 0 Then
                Dim objSR As New KTB.DNet.Domain.ServiceReminder
                objSR = CType(arrLstSR(0), ServiceReminder)
                objSR.Status = 2
                Dim nResult = New ServiceReminderFacade(User).Update(objSR) 'update status servicereminder
                If nResult <> -1 Then
                    Dim objGSRNew As New KTB.DNet.Domain.ServiceReminderFollowUp
                    objGSRNew.ServiceReminder = objGSR.ServiceReminder
                    objGSRNew.ServiceBooking = objGSR.ServiceBooking
                    objGSRNew.FollowUpStatus = 2
                    Dim criteriasa As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriasa.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.GSR.Response"))
                    criteriasa.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, 7))
                    Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criteriasa)
                    Dim objStandardCode As New StandardCode
                    If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
                        objStandardCode = CType(arrDDL(0), StandardCode)
                        objGSRNew.FollowUpAction = objStandardCode.ValueDesc
                    End If
                    objGSRNew.FollowUpDate = DateTime.Now
                    objGSRNew.BookingDate = objGSR.BookingDate
                    objGSRNew.RowStatus = 0
                    nResult = New ServiceReminderFollowUpFacade(User).Insert(objGSRNew) 'insert new row servicereminderfollowup
                    If nResult <> -1 Then
                        'nResult = New ServiceBookingFacade(User).UpdateStatus(e.Item.Cells(0).Text)
                        'If nResult <> -1 Then
                        '    MessageBox.Show("Service Booking Dibatalkan.")
                        'End If
                        'MessageBox.Show(SR.SaveSuccess)
                    Else
                        MessageBox.Show(SR.SaveFail)
                    End If
                Else
                    MessageBox.Show(SR.SaveFail)
                End If
            End If
        End If

        Dim CRITERIAs = New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CRITERIAs.opAnd(New Criteria(GetType(CustomerCaseResponse), "ServiceBooking.ID", MatchType.Exact, sessHelper.GetSession("ID")))
        Dim arrLstCCR As ArrayList = New CustomerCaseResponseFacade(User).Retrieve(CRITERIAs)
        If (arrLstCCR.Count > 0) Then
            objCCR = CType(arrLstCCR(0), CustomerCaseResponse)
            Dim CRITERIA = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            CRITERIA.opAnd(New Criteria(GetType(CustomerCase), "ID", MatchType.Exact, objCCR.CustomerCase.ID))
            Dim arrLstCCs As ArrayList = New CustomerCaseFacade(User).Retrieve(CRITERIA)
            If (arrLstCCs.Count > 0) Then
                objCC = CType(arrLstCCs(0), CustomerCase)
                objCC.Status = 6
                Dim nResult = New CustomerCaseFacade(User).Update(objCC) 'update status customercase
                If (nResult <> -1) Then
                    Dim objCCRNew As New CustomerCaseResponse
                    objCCRNew.CustomerCase = objCCR.CustomerCase
                    objCCRNew.ServiceBooking = objCCR.ServiceBooking
                    objCCRNew.WorkOrderNumber = ""
                    objCCRNew.Subject = objCCR.Subject
                    objCCRNew.Description = txtComment.Text.ToString()
                    'If rptDealerResp.Items.Count > 0 Then
                    Dim _criterias As CriteriaComposite
                    'For i As Integer = 0 To rptDealerResp.Items.Count - 1
                    'Dim rb As RadioButton = CType(rptDealerResp.Items(i).FindControl("rbDealer"), RadioButton)
                    'If rb.Checked Then
                    _criterias = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    _criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.CustomerCase.Cancel"))
                    _criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, rbtnResponses.SelectedValue))
                    objCCRNew.Response = CShort(CType(New StandardCodeFacade(User).RetrieveByCriteria(_criterias)(0), StandardCode).ValueId)
                    'Exit For
                    'End If
                    'Next
                    'End If
                    objCCRNew.BookingDatetime = objCCR.BookingDatetime
                    objCCRNew.Status = 6
                    objCCRNew.IsSend = 0
                    objCCRNew.RowStatus = 0
                    nResult = New CustomerCaseResponseFacade(User).Insert(objCCRNew) 'insert new row customercaseresponse
                    If (nResult <> -1) Then
                        nResult = New ServiceBookingFacade(User).UpdateStatus(sessHelper.GetSession("ID"))
                        If (nResult <> -1) Then
                            MessageBox.Show(SR.SaveSuccess)
                            If (boolSF) Then
                                MessageBox.Show("Proses Send SalesForce Sukses")
                            Else
                                MessageBox.Show("Proses Send SalesForce gagal")
                                'MessageBox.Show("Proses Cancel Service Booking Gagal")
                            End If
                            RegisterStartupScript("Close", "<script>onSuccess();</script>")
                            Return
                        End If
                    End If
                Else
                    MessageBox.Show(SR.SaveFail)
                End If
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub
#End Region

#Region "Custom Method"
    Private Sub BindResponse(ByVal strCategory As String)
        Dim arrList As ArrayList
        If strCategory = "CC" Then
            arrList = New StandardCodeFacade(User).RetrieveByCategory(String.Format("ServiceBooking.CustomerCase.Cancel"))
        End If

        'rptCustResp.DataSource = arrListCust
        'rptCustResp.DataBind()
        'rptDealerResp.DataSource = arrList
        'rptDealerResp.DataBind()
        For Each s As StandardCode In arrList
            If s.ValueCode <> "New" Then
                Dim ResponListItem As New ListItem(s.ValueDesc, s.ValueId)
                'ddlStatus.Items.Add(statusListItem)
                rbtnResponses.Items.Add(ResponListItem)
            End If
        Next
        'Dim responListItemFirst As New ListItem("Respon Lain", 7)
        'rbtnResponses.Items.Add(responListItemFirst)
        'rbtnResponses.Items.Add("Respon Lain")
        rbtnResponses.SelectedValue = 0

        trResponse.Visible = arrList.Count > 1
    End Sub
    'Protected Sub rptDealerResp_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    Dim rbDealer As RadioButton = CType(e.Item.FindControl("rbDealer"), RadioButton)
    '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
    '        rbDealer.Attributes.Add("onclick", "SetUniqueRadioButton('response',this)")
    '    End If
    'End Sub
#End Region


End Class