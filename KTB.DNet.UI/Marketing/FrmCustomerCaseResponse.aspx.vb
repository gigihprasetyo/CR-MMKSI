Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text
Imports KTB.DNet.WebApi.Models
Imports KTB.DNet.WebApi.Models.SalesForce
Imports System.Collections.Generic
Imports System.Linq

Public Class FrmCustomerCaseResponse
    Inherits System.Web.UI.Page


#Region "Private Variable"
    Private oDealer, objDealer As Dealer
    Private oLoginUser As New UserInfo
    Private sessHelper As New SessionHelper
    Private _arrDeliveryCustomerHeader As ArrayList
    Private dt As DateTime = DateTime.Now
    Private TotalUnit As Integer
    Private Suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    Private bEditPriv As Boolean
    Private bViewPriv As Boolean
    Private bDownloadPriv As Boolean
    Private _userInfo As UserInfo

    Private AttachmentDirectory As String
    Private TargetDirectory As String
    Private TempDirectory As String
    Private Const maxFileSize As Integer = 3000 'kb
#End Region

#Region " Event Handler"
    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        AttachmentDirectory = KTB.DNet.Lib.WebConfig.GetValue("CASEEvidenceFileDirectory")
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = TargetDirectory + "\" + oDealer.ID.ToString + "-" + oLoginUser.ID.ToString + "\" + lblCaseNumber.Text + "\"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            BindStatus()
            sessHelper.SetSession("SortCol", "CreatedTime")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            If Not IsNothing(Request.QueryString("caseId")) Then
                Dim _CustomerCaseID As Integer = CInt(Request.QueryString("caseId"))
                sessHelper.SetSession("SessCustomerCaseID", _CustomerCaseID)
                If Not IsNothing(Request.QueryString("mode")) Then
                    Dim _mode As String = Request.QueryString("mode")
                    sessHelper.SetSession("Sessmode", _mode)
                    BindDataCustomerCase(_CustomerCaseID, _mode)
                Else
                    BindDataCustomerCase(_CustomerCaseID, "view")
                End If
                
            Else
                ClearForm()
            End If
            ClearTempData()
        End If
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        trChangeBook.Visible = ddlStatus.SelectedValue = EnumCustomerCaseResponse.CustomerCaseResponse.Re_Schedule
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim bolNotValidSave As Boolean = False
        If ddlStatus.SelectedIndex = 0 Then 'validasi Status blank
            lblValidddlStatus.Text = "*"
            bolNotValidSave = True
        End If
        If txtComment.Text.Trim() = "" Then 'validasi Comment blank
            lblValidtxtComment.Text = "*"
            bolNotValidSave = True
        End If

        ''Validasi File Upload kosong jika status closed
        Dim arrAttachment As ArrayList = CType(sessHelper.GetSession("ATTACMENTEVIDENCE"), ArrayList)
        If arrAttachment.Count = 0 Then
            If ddlStatus.SelectedValue.Trim() = EnumCustomerCaseResponse.CustomerCaseResponse.Closed Then 'Status Closed
                lblValidFile.Text = "*"
                bolNotValidSave = True
            End If
        End If
        If bolNotValidSave = True Then
            Return
        End If

        Dim objCustomerCase As CustomerCase = CType(sessHelper.GetSession("SessCustomerCase"), CustomerCase)

        'validasi pengisian respon untuk status close dan reschedule, khusus test drive dan service booking
        Dim responseMandatoryFlag As Boolean = CType(New AppConfigFacade(User).Retrieve("CaseValidationResponse").Value, Boolean)
        If responseMandatoryFlag Then
            If (ddlStatus.SelectedValue = 4 Or ddlStatus.SelectedValue = 5 Or ddlStatus.SelectedValue = 6) And
                (objCustomerCase.SubCategory1.ToUpper.Contains("TEST DRIVE") Or objCustomerCase.SubCategory1.ToUpper.Contains("SERVICE BOOKING")) Then
                Dim responseChecked As Boolean = False
                For i As Integer = 0 To rptCustResp.Items.Count - 1
                    Dim rb As RadioButton = CType(rptCustResp.Items(i).FindControl("rbCust"), RadioButton)
                    If rb.Checked Then
                        responseChecked = True
                        Exit For
                    End If
                Next
                For i As Integer = 0 To rptDealerResp.Items.Count - 1
                    Dim rb As RadioButton = CType(rptDealerResp.Items(i).FindControl("rbDealer"), RadioButton)
                    If rb.Checked Then
                        responseChecked = True
                        Exit For
                    End If
                Next
                If Not responseChecked Then
                    MessageBox.Show("Respon harus diisi untuk status Closed dan Reschedule")
                    Return
                End If
            End If
        End If

        'Validasi jika ada caseResponsenya yg masih ada isSend = 0
        Dim arr As New ArrayList
        Dim _criterias As New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _criterias.opAnd(New Criteria(GetType(CustomerCaseResponse), "CustomerCase.ID", MatchType.Exact, objCustomerCase.ID))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(CustomerCaseResponse), "CreatedTime", Sort.SortDirection.ASC))
        arr = New CustomerCaseResponseFacade(User).Retrieve(_criterias, sortColl)
        If arr.Count > 0 Then
            Dim objCustResp As CustomerCaseResponse = CType(arr(arr.Count - 1), CustomerCaseResponse)
            If objCustResp.IsSend = 0 Then
                MessageBox.Show("Masih ada respon case yang belum terkirim, harap klik tombol Re-Send")
                Return
            End If
        End If

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertCustomerCaseResponse()
        Else
            UpdateCustomerCaseResponse()
        End If
        btnBatal_Click(sender, e)

        If CheckLastStatusCaseIsClosed() Then
            btnBack_Click(sender, e)
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        'Dim objGSR As New ServiceReminder
        'Dim CCResponse As New CustomerCaseResponse
        'Dim CC As New CustomerCase
        '_userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        'Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)
        'Dim totday As Double = 0
        'Dim errMsg As String = String.Empty

        'If Not IsNothing(sessHelper.GetSession("modeForm")) Then
        '    If (sessHelper.GetSession("modeForm") = "GSR") Then
        '        Dim strMode As String = sessHelper.GetSession("Sessmode").ToString()
        '        If Not IsNothing(sessHelper.GetSession("caseId")) Then
        '            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            criteria.opAnd(New Criteria(GetType(CustomerCase), "ID", MatchType.Exact, sessHelper.GetSession("caseId")))
        '            Dim sortt As Sort = New Sort(GetType(CustomerCase), "ID", sortt.SortDirection.DESC)
        '            Dim sortst As SortCollection = New SortCollection
        '            sortst.Add(sortt)
        '            Dim arrLst As ArrayList = New KTB.DNet.BusinessFacade.CustomerCaseFacade(User).Retrieve(criteria, sortst)
        '            If Not IsNothing(arrLst) AndAlso arrLst.Count > 0 Then
        '                CC = CType(arrLst(0), CustomerCase)
        '                If Not IsNothing(CC.CaseNumber) Then
        '                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '                    criterias.opAnd(New Criteria(GetType(ServiceReminder), "CaseNumber", MatchType.Exact, CC.CaseNumber))
        '                    Dim sort As Sort = New Sort(GetType(ServiceReminder), "ID", sort.SortDirection.DESC)
        '                    Dim sorts As SortCollection = New SortCollection
        '                    sorts.Add(sort)
        '                    Dim arrLstcc As ArrayList = New KTB.DNet.BusinessFacade.Service.ServiceReminderFacade(User).Retrieve(criterias, sorts)
        '                    If Not IsNothing(arrLstcc) AndAlso arrLstcc.Count > 0 Then
        '                        objGSR = CType(arrLstcc(0), ServiceReminder)

        '                        If Not KTB.DNet.BusinessValidation.ServiceReminderValidation.ValidateSvcReminder(objGSR, errMsg) AndAlso Not isKTB Then
        '                            MessageBox.Show(errMsg)
        '                            Exit Sub
        '                            'Server.Transfer("~/Service" & _sessHelper.GetSession("backURL").ToString)
        '                        End If

        '                        'If (strMode = "edit") Then
        '                        '    sessHelper.SetSession("backURL", "~/Service/FrmGSDaftarService.aspx")
        '                        '    sessHelper.SetSession("SVCREMINDERID", objGSR.ID)
        '                        '    sessHelper.SetSession("SVCMODE", "EDIT")
        '                        '    If Not IsNothing(sessHelper.GetSession("SVCREMINDERID")) Then
        '                        '        Response.Redirect("~/Service/FrmGSServiceFollowUp.aspx")
        '                        '        ' Server.Transfer("~/Service/FrmGSServiceFollowUp.aspx")
        '                        '    End If

        '                        'Else
        '                        '    sessHelper.SetSession("backURL", "~/Service/FrmGSDaftarService.aspx")
        '                        '    sessHelper.SetSession("SVCREMINDERID", objGSR.ID)
        '                        '    sessHelper.SetSession("SVCMODE", "VIEW")
        '                        '    If Not IsNothing(sessHelper.GetSession("SVCREMINDERID")) Then
        '                        '        Response.Redirect("~/Service/FrmGSServiceFollowUp.aspx")
        '                        '        'Server.Transfer("~/Service/FrmGSServiceFollowup.aspx")
        '                        '    End If
        '                        'End If

        '                        If (objGSR.Status = 3) Or (objGSR.Status = 4) Or (objGSR.Status = 5) Or (objGSR.Status = 6) Then
        '                            sessHelper.SetSession("backURL", "~/Marketing/FrmCustomerCaseResponse.aspx")
        '                            sessHelper.SetSession("modeForm", "CC")
        '                            sessHelper.SetSession("mode", sessHelper.GetSession("Sessmode"))
        '                            sessHelper.SetSession("SVCREMINDERID", objGSR.ID)
        '                            sessHelper.SetSession("SVCMODE", "VIEW")
        '                            If Not IsNothing(sessHelper.GetSession("SVCREMINDERID")) Then
        '                                Response.Redirect("~/Service/FrmGSServiceFollowUp.aspx")
        '                            End If
        '                            'Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=View&CCID=" + CCResponse.ID.ToString())
        '                        Else
        '                            sessHelper.SetSession("backURL", "~/Marketing/FrmCustomerCaseResponse.aspx")
        '                            sessHelper.SetSession("modeForm", "CC")
        '                            sessHelper.SetSession("mode", sessHelper.GetSession("Sessmode"))
        '                            sessHelper.SetSession("SVCREMINDERID", objGSR.ID)
        '                            sessHelper.SetSession("SVCMODE", "EDIT")
        '                            If Not IsNothing(sessHelper.GetSession("SVCREMINDERID")) Then
        '                                Response.Redirect("~/Service/FrmGSServiceFollowUp.aspx")
        '                            End If
        '                            'Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=View&CCID=" + CCResponse.ID.ToString())
        '                        End If

        '                        'Server.Transfer(_sessHelper.GetSession("backURL").ToString)
        '                    End If
        '                End If
        '            End If
        '        End If

        '    End If
        'Else
        '    ViewState.Clear()
        '    Response.Redirect("FrmCustomerCase.aspx?isBack=1")
        'End If
        ViewState.Clear()
        Response.Redirect("FrmCustomerCase.aspx?isBack=1")
    End Sub
    Protected Sub btnGSR_Click(sender As Object, e As EventArgs) Handles btnGSR.Click
        Dim objGSR As New ServiceReminder
        Dim CCResponse As New CustomerCaseResponse
        Dim CC As New CustomerCase
        'Dim svcReminder As ServiceReminder = CType(_sessHelper.GetSession("SVCREMINDER"), ServiceReminder)
        _userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)
        Dim totday As Double = 0
        Dim errMsg As String = String.Empty

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(CustomerCase), "ID", MatchType.Exact, sessHelper.GetSession("SessCustomerCaseID")))
        Dim sortt As Sort = New Sort(GetType(CustomerCase), "ID", sortt.SortDirection.DESC)
        Dim sortst As SortCollection = New SortCollection
        sortst.Add(sortt)
        Dim arrLstcc As ArrayList = New KTB.DNet.BusinessFacade.CustomerCaseFacade(User).Retrieve(criteria, sortst)
        If Not IsNothing(arrLstcc) AndAlso arrLstcc.Count > 0 Then
            CC = CType(arrLstcc(0), CustomerCase)
        End If

        If Not IsNothing(CC.CaseNumber) Then
            Dim critt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critt.opAnd(New Criteria(GetType(ServiceReminder), "CaseNumber", MatchType.Exact, CC.CaseNumber))
            Dim arrLstSR As ArrayList = New KTB.DNet.BusinessFacade.Service.ServiceReminderFacade(User).Retrieve(critt)
            If arrLstSR.Count > 0 Then
                objGSR = CType(arrLstSR(0), ServiceReminder)
                If Not KTB.DNet.BusinessValidation.ServiceReminderValidation.ValidateSvcReminder(objGSR, errMsg) AndAlso Not isKTB Then
                    MessageBox.Show(errMsg)
                    Exit Sub
                    'Server.Transfer("~/Service" & _sessHelper.GetSession("backURL").ToString)
                End If

                If (objGSR.Status = 3) Or (objGSR.Status = 4) Or (objGSR.Status = 5) Or (objGSR.Status = 6) Then
                    'sessHelper.SetSession("backURL", "~/Marketing/FrmCustomerCaseResponse.aspx")
                    sessHelper.SetSession("modeForm", "CC")
                    sessHelper.SetSession("mode", sessHelper.GetSession("Sessmode"))
                    sessHelper.SetSession("SVCREMINDERID", objGSR.ID)
                    sessHelper.SetSession("SVCMODE", "VIEW")
                    If Not IsNothing(sessHelper.GetSession("SVCREMINDERID")) Then
                        Response.Redirect("~/Service/FrmGSServiceFollowUp.aspx")
                    End If
                    'Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=View&CCID=" + CCResponse.ID.ToString())
                Else
                    'sessHelper.SetSession("backURL", "~/Marketing/FrmCustomerCaseResponse.aspx")
                    sessHelper.SetSession("modeForm", "CC")
                    sessHelper.SetSession("mode", sessHelper.GetSession("Sessmode"))
                    sessHelper.SetSession("SVCREMINDERID", objGSR.ID)
                    sessHelper.SetSession("SVCMODE", "EDIT")
                    If Not IsNothing(sessHelper.GetSession("SVCREMINDERID")) Then
                        Response.Redirect("~/Service/FrmGSServiceFollowUp.aspx")
                    End If
                    'Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=View&CCID=" + CCResponse.ID.ToString())
                End If
            End If
        End If
    End Sub

    Protected Sub btnSB_Click(sender As Object, e As EventArgs) Handles btnSB.Click

       
        Dim objGSR As New ServiceReminder
        Dim CCResponse As New CustomerCaseResponse
        Dim CC As New CustomerCase
        'Dim arrCCResponse As ArrayList
        'Dim objSB As New ServiceBooking
        'If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
        '    objStandardCode = CType(arrDDL(0), StandardCode)
        '    strPickUpType = objStandardCode.ValueDesc
        'End If

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(CustomerCase), "ID", MatchType.Exact, sessHelper.GetSession("SessCustomerCaseID")))
        Dim sortt As Sort = New Sort(GetType(CustomerCase), "ID", sortt.SortDirection.DESC)
        Dim sortst As SortCollection = New SortCollection
        sortst.Add(sortt)
        Dim arrLstcc As ArrayList = New KTB.DNet.BusinessFacade.CustomerCaseFacade(User).Retrieve(criteria, sortst)
        If Not IsNothing(arrLstcc) AndAlso arrLstcc.Count > 0 Then
            CC = CType(arrLstcc(0), CustomerCase)
        End If

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(CustomerCaseResponse), "CustomerCase.ID", MatchType.Exact, sessHelper.GetSession("SessCustomerCaseID")))
        Dim sort As Sort = New Sort(GetType(CustomerCaseResponse), "ID", sort.SortDirection.DESC)
        Dim sorts As SortCollection = New SortCollection
        sorts.Add(sort)
        Dim arrLst As ArrayList = New KTB.DNet.BusinessFacade.CustomerCaseResponseFacade(User).Retrieve(crit, sorts)

        If Not IsNothing(CC.CaseNumber) Then
            Dim critt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critt.opAnd(New Criteria(GetType(ServiceReminder), "CaseNumber", MatchType.Exact, CC.CaseNumber))
            Dim arrLstSR As ArrayList = New KTB.DNet.BusinessFacade.Service.ServiceReminderFacade(User).Retrieve(critt)
            If arrLstSR.Count > 0 Then
                objGSR = CType(arrLstSR(0), ServiceReminder)
                If Not IsNothing(objGSR.ActualServiceDealer) Then
                    MessageBox.Show("Kendaraan sudah melakukan servis pada tanggal " & objGSR.ServiceActualDate & " .")
                    Exit Sub
                End If
            End If
        End If

        If Not oDealer Is Nothing Then
            If (oDealer.ID <> CC.Dealer.ID) Then
                'CC.Dealer.City.CityName
                'Dim oCity As KTB.DNet.Domain.City
                'Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'crits.opAnd(New Criteria(GetType(City), "ID", MatchType.Exact, CC.Dealer.City.ID))
                'Dim arrCity As ArrayList = New KTB.DNet.BusinessFacade.General.CityFacade(User).Retrieve(crits)
                'If arrCity.Count > 0 Then
                '    oCity = CType(arrCity(0), City)
                'MessageBox.Show("Input Service Booking hanya bisa dilakukan oleh Dealer " & CC.Dealer.DealerName & " " & oCity.CityName & ".")
                'MessageBox.Show("Input Service Booking hanya bisa dilakukan oleh Dealer " & CC.Dealer.DealerName & " cabang " & CC.Dealer.City.CityName & ".")
                MessageBox.Show("Input Service Booking hanya bisa dilakukan oleh Dealer " & CC.Dealer.DealerCode & ".")
                Exit Sub
                'End If
            End If
        End If


        If arrLst.Count = 0 Then

            If Not IsNothing(arrLstcc) AndAlso arrLstcc.Count > 0 Then
                CC = CType(arrLstcc(0), CustomerCase)

                CCResponse.CustomerCase = CC
                CCResponse.ServiceBooking = Nothing
                CCResponse.WorkOrderNumber = Nothing
                CCResponse.Subject = CC.Subject
                CCResponse.Response = Nothing
                CCResponse.Description = Nothing
                CCResponse.BookingDatetime = CC.BookingDatetime
                CCResponse.Status = CC.Status
                CCResponse.IsSend = 0
                CCResponse.RowStatus = 0
                CCResponse.CreatedBy = "System"
                CCResponse.CreatedTime = CC.CreatedTime

                Dim nResult As Integer = New CustomerCaseResponseFacade(User).Insert(CCResponse)
                If nResult <> -1 Then
                    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(CustomerCaseResponse), "CustomerCase.ID", MatchType.Exact, sessHelper.GetSession("SessCustomerCaseID")))
                    Dim sort1 As Sort = New Sort(GetType(CustomerCaseResponse), "ID", sort.SortDirection.DESC)
                    Dim sorts1 As SortCollection = New SortCollection
                    sorts1.Add(sort1)
                    Dim arrLstNew As ArrayList = New KTB.DNet.BusinessFacade.CustomerCaseResponseFacade(User).Retrieve(crits, sorts1)
                    If arrLstNew.Count > 0 Then
                        CCResponse = CType(arrLstNew(0), CustomerCaseResponse)
                    End If
                End If
            End If
        Else
            CCResponse = CType(arrLst(0), CustomerCaseResponse)
        End If

        'If Not IsNothing(arrLst) AndAlso arrLst.Count > 0 Then

        'End If

        

        If Not IsNothing(sessHelper.GetSession("Sessmode")) Then
            If (sessHelper.GetSession("Sessmode") = "view") Then
                Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=View&CCID=" + CCResponse.ID.ToString())
            Else
                If Not IsNothing(CCResponse.ServiceBooking) Then
                    Dim intDayOff As Integer = GetDayConfig()
                    Dim objSB As ServiceBooking = GetServiceBooking(CCResponse.ServiceBooking.ID)
                    Dim Tanggal As Date = objSB.IncomingDateStart.Date

                    Dim intSelisihTgl As Integer = (Tanggal - Date.Now.Date).TotalDays

                    If objSB.Status = 0 Then
                        Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=New&CCID=" + CCResponse.ID.ToString())
                    Else
                        If (intSelisihTgl >= intDayOff) Then
                            Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=Edit&CCID=" + CCResponse.ID.ToString())
                        Else
                            Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=New&CCID=" + CCResponse.ID.ToString())
                        End If
                    End If


                Else
                    Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=New&CCID=" + CCResponse.ID.ToString())
                End If
            End If
        End If

    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ddlStatus.Enabled = True
        txtComment.Enabled = True
        btnSave.Enabled = True
        btnSB.Enabled = True

        ddlStatus.SelectedIndex = 0
        txtComment.Text = ""
        lblValidddlStatus.Text = ""
        lblValidtxtComment.Text = ""
        lblValidFile.Text = ""

        If Not IsNothing(Request.QueryString("caseId")) Then
            Dim _CustomerCaseID As Integer = CInt(Request.QueryString("caseId"))
            sessHelper.SetSession("SessCustomerCaseID", _CustomerCaseID)
            If Not IsNothing(Request.QueryString("mode")) Then
                Dim _mode As String = Request.QueryString("mode")
                BindDataCustomerCase(_CustomerCaseID, _mode)
            Else
                BindDataCustomerCase(_CustomerCaseID, "view")
            End If
        End If

        BindDataResponse(dgCase.CurrentPageIndex)
        ClearTempData()
        sessHelper.SetSession("ATTACMENTEVIDENCE", New ArrayList)
        BindDataEvidence()
        ViewState.Add("vsProcess", "Insert")
    End Sub

#End Region

#Region "Custom Method"
    Private Sub InitiateAuthorization()

    End Sub

    Private Sub ClearForm()
        lblDealer.Text = String.Empty
        lblCustomerName.Text = String.Empty
        lblPhone.Text = String.Empty
        lblEmail.Text = String.Empty
        lblVehicleType.Text = String.Empty
        lblVariant.Text = String.Empty
        lblEngineNo.Text = String.Empty
        lblChassisNo.Text = String.Empty
        lblNoPol.Text = String.Empty
        lblOdometer.Text = String.Empty

        lblCaseNumber.Text = String.Empty
        lblCaseNumberRef.Text = String.Empty
        lblCaseDate.Text = String.Empty
        lblCategory.Text = String.Empty
        lblCategory1.Text = String.Empty
        lblCategory2.Text = String.Empty
        lblCategory3.Text = String.Empty
        lblCategory4.Text = String.Empty
        lblType.Text = String.Empty
        lblSubject.Text = String.Empty
        lblDescription.Text = String.Empty

        dgCase.VirtualItemCount = 0
        dgCase.DataSource = Nothing
        dgCase.DataBind()

        rptCustResp.DataSource = Nothing
        rptCustResp.DataBind()
        rptDealerResp.DataSource = Nothing
        rptDealerResp.DataBind()

        pnlEntry.Visible = False
        btnSave.Visible = False
        btnBatal.Visible = False
        btnSB.Visible = False
        btnGSR.Visible = False


    End Sub

    Private Sub BindStatus()
        Dim arl As ArrayList = EnumCustomerCaseResponse.RetriveCustomerPurpose(True)
        ddlStatus.Items.Clear()
        For Each item As EnumCustomerCaseResponseOp In arl
            If (Not item.ValStatus = EnumCustomerCaseResponse.CustomerCaseResponse.NewStatus) AndAlso _
                (Not item.ValStatus = EnumCustomerCaseResponse.CustomerCaseResponse.Re_Open) AndAlso _
                (Not item.ValStatus = EnumCustomerCaseResponse.CustomerCaseResponse.Escalated) Then
                ddlStatus.Items.Add(New ListItem(item.NameStatus, item.ValStatus))
            End If
        Next
    End Sub

    Private Sub BindResponse()
        Dim arrListCust As ArrayList = New StandardCodeFacade(User).RetrieveByCategory(String.Format("CustomerCase.Response.Customer.{0}", lblCategory1.Text.Replace(" ", "_")))
        Dim arrListDealer As ArrayList = New StandardCodeFacade(User).RetrieveByCategory(String.Format("CustomerCase.Response.Dealer.{0}", lblCategory1.Text.Replace(" ", "_")))

        rptCustResp.DataSource = arrListCust
        rptCustResp.DataBind()
        rptDealerResp.DataSource = arrListDealer
        rptDealerResp.DataBind()

        trResponse.Visible = arrListCust.Count > 1 Or arrListDealer.Count > 1
    End Sub

    Private Sub BindDataCustomerCase(ByVal _customerCaseID As Integer, ByVal mode As String)
        Dim arr As New ArrayList
        Dim total As Integer
        Dim objSR As KTB.DNet.Domain.ServiceReminder

        Dim objCustomerCase As CustomerCase = New CustomerCaseFacade(User).Retrieve(_customerCaseID)
        If Not IsNothing(objCustomerCase) AndAlso objCustomerCase.ID > 0 Then
            sessHelper.SetSession("SessCustomerCase", objCustomerCase)
            lblDealer.Text = objCustomerCase.Dealer.DealerCode
            lblCustomerName.Text = objCustomerCase.CustomerName
            lblPhone.Text = objCustomerCase.Phone
            lblEmail.Text = objCustomerCase.Email
            lblVehicleType.Text = objCustomerCase.CarType
            lblVariant.Text = objCustomerCase.Variants
            lblEngineNo.Text = objCustomerCase.EngineNumber
            lblChassisNo.Text = objCustomerCase.ChassisNumber
            lblNoPol.Text = objCustomerCase.PlateNumber
            lblOdometer.Text = objCustomerCase.Odometer

            lblCaseNumber.Text = objCustomerCase.CaseNumber
            lblCaseNumberRef.Text = objCustomerCase.CaseNumberReff
            lblCaseDate.Text = objCustomerCase.CaseDate
            lblCategory.Text = objCustomerCase.Category
            lblCategory1.Text = objCustomerCase.SubCategory1
            lblCategory2.Text = objCustomerCase.SubCategory2
            lblCategory3.Text = objCustomerCase.SubCategory3
            lblCategory4.Text = objCustomerCase.SubCategory4
            lblType.Text = objCustomerCase.CallerType
            lblSubject.Text = objCustomerCase.Subject
            lblDescription.Text = objCustomerCase.Description

            If Not IsNothing(objCustomerCase.CaseNumber) Then
                Dim critt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critt.opAnd(New Criteria(GetType(ServiceReminder), "CaseNumber", MatchType.Exact, objCustomerCase.CaseNumber))
                Dim arrLstSR As ArrayList = New KTB.DNet.BusinessFacade.Service.ServiceReminderFacade(User).Retrieve(critt)
                If arrLstSR.Count > 0 Then
                    'objSR = CType(arrLstSR(0), ServiceReminder)
                    btnGSR.Visible = True
                Else
                    btnGSR.Visible = False
                End If
            Else
                btnGSR.Visible = False
            End If

            Dim objCCR As CustomerCaseResponse = objCustomerCase.CustomerCaseResponses.Cast(Of CustomerCaseResponse)() _
                .Where(Function(x) x.BookingDatetime.Date.Year > 2000).OrderByDescending(Function(x) x.ID).FirstOrDefault()

            If Not objCCR Is Nothing Then
                lblTanggalX.Text = objCCR.BookingDatetime
            Else
                lblTanggalX.Text = ""
            End If

            BindResponse()
        End If

        If mode = "edit" Then
            'cek apakah CCID ini subcategory1 nya servicebooking
            Dim CstCase As New CustomerCase
            Dim arrCC As ArrayList
            If Not IsNothing(sessHelper.GetSession("SessCustomerCase")) Then
                objCustomerCase = CType(sessHelper.GetSession("SessCustomerCase"), CustomerCase)
                'arrCC = CType(sessHelper.GetSession("SessCustomerCase"), ArrayList)
                'CstCase = arrCC(0)
                If (objCustomerCase.SubCategory1.Contains("Service Booking")) Then
                    'btnSB.Visible = True
                    Dim CCID As String = sessHelper.GetSession("SessCustomerCaseID")
                    Dim intStatusCC As Integer = GetStatusCustomerCase(CCID)
                    If (intStatusCC <> 4) Then 'And intStatusCC <> 6
                        btnSB.Visible = True
                    Else
                        btnSB.Visible = False

                    End If
                Else
                    btnSB.Visible = False
                End If
            End If
            pnlEntry.Visible = True
            btnBatal.Visible = True
            btnSave.Visible = True
            'btnSB.Visible = True


        Else
            ''cek apakah CCID ini subcategory1 nya servicebooking
            'Dim CstCase As New CustomerCase
            'Dim arrCC As ArrayList
            'If Not IsNothing(sessHelper.GetSession("SessCustomerCase")) Then
            '    objCustomerCase = CType(sessHelper.GetSession("SessCustomerCase"), CustomerCase)
            '    'arrCC = CType(sessHelper.GetSession("SessCustomerCase"), ArrayList)
            '    'CstCase = arrCC(0)
            '    If (objCustomerCase.SubCategory1 = "3.3  Service Booking") Then
            '        btnSB.Visible = True
            '    Else
            '        btnSB.Visible = False
            '    End If
            'End If
            pnlEntry.Visible = False
            btnBatal.Visible = False
            btnSave.Visible = False
            btnSB.Visible = False

        End If

        BindDataResponse(dgCase.CurrentPageIndex)
        sessHelper.SetSession("ATTACMENTEVIDENCE", New ArrayList)
        BindDataEvidence()
    End Sub

    Private Sub BindDataResponse(ByVal iPage As Integer)
        Dim arr As New ArrayList
        Dim total As Integer
        Dim objCustomerCase As CustomerCase = CType(sessHelper.GetSession("SessCustomerCase"), CustomerCase)
        Dim _criterias As New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _criterias.opAnd(New Criteria(GetType(CustomerCaseResponse), "CustomerCase.ID", MatchType.Exact, objCustomerCase.ID))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(CustomerCaseResponse), sessHelper.GetSession("SortCol"), sessHelper.GetSession("SortDirection")))

        Dim arrAllData As New ArrayList
        arr = New CustomerCaseResponseFacade(User).RetrieveByCriteria(_criterias, iPage, dgCase.PageSize, total, sortColl)
        arrAllData = New CustomerCaseResponseFacade(User).Retrieve(_criterias, sortColl)

        dgCase.VirtualItemCount = total
        dgCase.DataSource = arr
        dgCase.DataBind()

        If arr.Count > 0 Then
            Dim objCustResp As CustomerCaseResponse = CType(arrAllData(0), CustomerCaseResponse)
            'comment : ddlStatus defaultnya 'Silahkan Pilih/Kosong'
            'ddlStatus.SelectedValue = CType(objCustResp.Status, String)

            If objCustResp.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Closed Then
                ddlStatus.Enabled = False
                txtComment.Enabled = False
                btnSave.Enabled = False
                btnSB.Enabled = False
                btnBatal.Enabled = False
            End If
            dgCase.SelectedIndex = -1
        End If

        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Function UpdateCustomerCaseResponse()
        Dim objCustomerCaseResponse As CustomerCaseResponse = CType(Session.Item("vsCustomerCaseResponse"), CustomerCaseResponse)
        Dim objCustomerCase As CustomerCase = CType(sessHelper.GetSession("sessCustomerCase"), CustomerCase)
        objCustomerCaseResponse.CustomerCase = objCustomerCase
        objCustomerCaseResponse.Status = ddlStatus.SelectedValue
        objCustomerCaseResponse.Description = txtComment.Text

        Dim nResult As Integer = -1
        Try
            nResult = New CustomerCaseResponseFacade(User).Update(objCustomerCaseResponse)
        Catch ex As Exception
            nResult = -1
        End Try

        If nResult <> -1 Then
            'CommitAttachment(CType(sessHelper.GetSession("ATTACMENTEVIDENCE"), ArrayList))
            BindDataResponse(dgCase.CurrentPageIndex)
            Dim sender As Object
            MessageBox.Show(SR.UpdateSucces)
        Else
            MessageBox.Show(SR.UpdateFail)
        End If
    End Function

    Private Function InsertCustomerCaseResponse()
        Dim objFacade As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)
        Dim objCustomerCase As CustomerCase = CType(sessHelper.GetSession("sessCustomerCase"), CustomerCase)
        Dim objresponse As CustomerCaseResponse = New CustomerCaseResponse
        Dim objSvcBooking As ServiceBooking = New ServiceBooking
        Dim objSVCReminder As ServiceReminder = New ServiceReminder
        Dim objSVCReminderFollowup As ServiceReminderFollowUp = New ServiceReminderFollowUp
        Dim objCCR As CustomerCaseResponse = New CustomerCaseResponse
        Dim nResult1 As Integer = 0
        Dim boolSave As Boolean = False

        objresponse.CustomerCase = objCustomerCase
        objresponse.Status = ddlStatus.SelectedValue
        objresponse.Description = txtComment.Text
        Dim strErr As String = String.Empty
        PopuplateData(objresponse, strErr)
        If Not strErr = String.Empty Then
            MessageBox.Show("Format jam salah! Format anda " & strErr & ". Contoh : 13:45")
            Return 0
        End If


        If ddlStatus.SelectedItem.Text = "Closed" Then
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(CustomerCaseResponse), "CustomerCase.ID", MatchType.Exact, objCustomerCase.ID))
            ViewState("CurrentSortColumn") = "ServiceBooking.ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(CustomerCaseResponse), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
            Dim arrCCR As ArrayList = New CustomerCaseResponseFacade(User).Retrieve(criteria, sortColl)
            If Not IsNothing(arrCCR) AndAlso arrCCR.Count > 0 Then
                objCCR = CType(arrCCR(0), CustomerCaseResponse)

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ServiceBooking), "ID", MatchType.Exact, objCCR.ServiceBooking.ID))
                Dim arrSvcBooking As ArrayList = New KTB.DNet.BusinessFacade.Service.ServiceBookingFacade(User).Retrieve(criterias)

                If Not IsNothing(arrSvcBooking) AndAlso arrSvcBooking.Count > 0 Then
                    objSvcBooking = CType(arrSvcBooking(0), ServiceBooking)
                    If objSvcBooking.Status = 1 Then
                        objSvcBooking.Status = 0
                        nResult1 = New KTB.DNet.BusinessFacade.Service.ServiceBookingFacade(User).UpdateStatus(objSvcBooking.ID)
                        If nResult1 <> -1 Then
                            boolSave = True
                            If Not IsNothing(objCustomerCase.CaseNumber) Then
                                'Dim criteriass As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                'criteriass.opAnd(New Criteria(GetType(ServiceReminder), "CaseNumber", MatchType.Exact, objCustomerCase.CaseNumber))
                                'Dim arrSR As ArrayList = New KTB.DNet.BusinessFacade.Service.ServiceReminderFacade(User).Retrieve(criteriass)
                                'If Not IsNothing(arrSR) AndAlso arrSR.Count > 0 Then
                                '    objSVCReminder = CType(arrSR(0), ServiceReminder)
                                '    objSVCReminderFollowup.ServiceReminder = objSVCReminder
                                '    objSVCReminderFollowup.ServiceBooking = Nothing
                                '    objSVCReminderFollowup.FollowUpStatus = 3
                                '    objSVCReminderFollowup.FollowUpAction = ""
                                '    objSVCReminderFollowup.FollowUpDate = DateTime.Now
                                '    objSVCReminderFollowup.BookingDate = "1753-01-01"
                                '    objSVCReminderFollowup.RowStatus = 0
                                '    nResult1 = New ServiceReminderFollowUpFacade(User).Insert(objSVCReminderFollowup) 'insert new row Followup
                                '    If nResult1 <> -1 Then
                                '        objSVCReminder.Status = 3
                                '        nResult1 = New KTB.DNet.BusinessFacade.Service.ServiceReminderFacade(User).Update(objSVCReminder) 'update status ServiceReminder
                                '    End If
                                'End If
                            End If
                        End If
                    End If
                End If

            End If
        End If


        '1. Insert to CustomerCaseResponse
        '2. Update Status to CustomerCase
        Dim nResult As Integer = 0
        Dim nResult2 As Integer = 0
        Try
            If Not (objCustomerCase Is Nothing) Then
                objCustomerCase.Status = ddlStatus.SelectedValue
            End If
            nResult = objFacade.InsertTransaction(objresponse, objCustomerCase, CType(sessHelper.GetSession("ATTACMENTEVIDENCE"), ArrayList))
        Catch ex As Exception
            nResult = -1
        End Try

        If nResult <> -1 Then
            'CommitAttachment(CType(sessHelper.GetSession("ATTACMENTEVIDENCE"), ArrayList))

            Dim sf As SalesForceInterface = New SalesForceInterface()
            Dim msg As String = String.Empty
            Dim vSFreturn As Boolean = False
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
            Else
                Dim _criterias As New CriteriaComposite(New Criteria(GetType(WsLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                _criterias.opAnd(New Criteria(GetType(WsLog), "Source", MatchType.Exact, "Internal"))
                _criterias.opAnd(New Criteria(GetType(WsLog), "Status", MatchType.Exact, "False"))
                _criterias.opAnd(New Criteria(GetType(WsLog), "Body", MatchType.[Partial], "SALESFORCEIUPDATECASE"))
                _criterias.opAnd(New Criteria(GetType(WsLog), "Body", MatchType.[Partial], objCustomerCase.SalesforceID))
                _criterias.opAnd(New Criteria(GetType(WsLog), "CreatedTime", MatchType.GreaterOrEqual, DateTime.Now.AddMinutes(-1)))

                Dim objWsLog As WsLog
                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Sort(GetType(WsLog), "CreatedTime", Sort.SortDirection.ASC))
                arr = New WsLogFacade(User).Retrieve(_criterias, sortColl)
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
                        Else
                            objResponseNew = New CustomerCaseResponseFacade(User).Retrieve(nResult)
                        End If
                        objResponseNew.IsSend = 1 'sent
                        nResult = New CustomerCaseResponseFacade(User).Update(objResponseNew)
                    End If
                End If
            End If

            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Function

    Private Sub ViewCustomerCaseResponse(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objCustomerCaseResponse As CustomerCaseResponse = New CustomerCaseResponseFacade(User).Retrieve(nID)
        sessHelper.SetSession("vsCustomerCaseResponse", objCustomerCaseResponse)
        Dim _criterias As New CriteriaComposite(New Criteria(GetType(CustomerCaseResponseEvidence), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _criterias.opAnd(New Criteria(GetType(CustomerCaseResponseEvidence), "CustomerCaseResponse.ID", MatchType.Exact, objCustomerCaseResponse.ID))
        Dim arrCustCaseResponseEvidence As ArrayList = New CustomerCaseResponseEvidenceFacade(User).Retrieve(_criterias)

        Dim arrCustCaseResponseEvidence2 As New ArrayList
        Dim EvidenceFileOnlyName As String
        For Each obj As CustomerCaseResponseEvidence In arrCustCaseResponseEvidence
            If Not IsNothing(obj.EvidenceFile) Then
                obj.Attachment = obj.EvidenceFile.Substring(obj.EvidenceFile.LastIndexOf("\") + 1)
            End If
            arrCustCaseResponseEvidence2.Add(obj)
        Next
        sessHelper.SetSession("ATTACMENTEVIDENCE", arrCustCaseResponseEvidence2)
        BindDataEvidence()

        ddlStatus.SelectedValue = CType(objCustomerCaseResponse.Status, String)
        txtComment.Text = objCustomerCaseResponse.Description

        btnSave.Enabled = EditStatus
        btnSB.Enabled = EditStatus
        btnBatal.Enabled = EditStatus
    End Sub

    Private Sub BindDataEvidence()
        dgEvidenceFile.DataSource = CType(sessHelper.GetSession("ATTACMENTEVIDENCE"), ArrayList)
        dgEvidenceFile.DataBind()
    End Sub

    Private Sub CommitAttachment(ByVal AttachmentCollection As ArrayList)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As CustomerCaseResponseEvidence In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        TargetFInfo = New FileInfo(TargetDirectory + obj.EvidenceFile)
                        TempFInfo = New FileInfo(TempDirectory + obj.EvidenceFile)

                        If TempFInfo.Exists Then
                            If Not TargetFInfo.Directory.Exists Then
                                Directory.CreateDirectory(TargetFInfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(TargetFInfo.FullName)
                        End If
                        obj.AttachmentData.SaveAs(TargetDirectory + obj.EvidenceFile)
                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ClearTempData()
        'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        'Dim success As Boolean = False

        'Try
        '    success = imp.Start()
        '    If success Then
        '        Dim dir As New DirectoryInfo(TempDirectory)
        '        dir.Delete(True)
        '    End If
        'Catch ex As Exception
        '    'Throw ex
        'End Try
    End Sub

    Private Sub RemoveCustomerCaseResponseEvidence(ByVal ObjAttachment As CustomerCaseResponseEvidence, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(TargetPath + ObjAttachment.Attachment)
                If finfo.Exists Then
                    finfo.Delete()
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PopuplateData(ByVal objCustResp As CustomerCaseResponse, ByRef strErr As String)
        If rptCustResp.Items.Count > 0 Or rptDealerResp.Items.Count > 0 Then
            Dim _criterias As CriteriaComposite
            For i As Integer = 0 To rptDealerResp.Items.Count - 1
                Dim rb As RadioButton = CType(rptDealerResp.Items(i).FindControl("rbDealer"), RadioButton)
                If rb.Checked Then
                    _criterias = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    _criterias.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, rb.Text))
                    objCustResp.Response = CShort(CType(New StandardCodeFacade(User).RetrieveByCriteria(_criterias)(0), StandardCode).ValueId)
                    Exit For
                End If
            Next

            For i As Integer = 0 To rptCustResp.Items.Count - 1
                Dim rb As RadioButton = CType(rptCustResp.Items(i).FindControl("rbCust"), RadioButton)
                If rb.Checked Then
                    _criterias = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    _criterias.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, rb.Text))
                    objCustResp.Response = CShort(CType(New StandardCodeFacade(User).RetrieveByCriteria(_criterias)(0), StandardCode).ValueId)
                    Exit For
                End If
            Next
        End If

        If ddlStatus.SelectedValue = EnumCustomerCaseResponse.CustomerCaseResponse.Re_Schedule Then
            Try
                Dim strDatetime As String = tglBooking.Value.Date.ToShortDateString & " " & txtClock.Text & ":00"
                objCustResp.BookingDatetime = Date.ParseExact(strDatetime, "dd/MM/yyyy HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Catch ex As Exception
                strErr = txtClock.Text
            End Try
        End If
    End Sub
    Private Function GetStatusCustomerCase(ByVal intIDCC As Integer) As Integer
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(CustomerCase), "ID", MatchType.Exact, intIDCC))
        Dim arrCC As ArrayList = New CustomerCaseFacade(User).Retrieve(criteria)
        Dim objCC As New CustomerCase
        Dim intStatus As Integer = 0
        If Not IsNothing(arrCC) AndAlso arrCC.Count > 0 Then
            objCC = CType(arrCC(0), CustomerCase)
            intStatus = objCC.Status
        End If
        Return intStatus
    End Function
    Private Function GetServiceBooking(ByVal intIDSB As Integer) As KTB.DNet.Domain.ServiceBooking
        Dim objSB As New ServiceBooking
        'If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
        '    objStandardCode = CType(arrDDL(0), StandardCode)
        '    strPickUpType = objStandardCode.ValueDesc
        'End If
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ServiceBooking), "ID", MatchType.Exact, intIDSB))
        Dim arrLst As ArrayList = New KTB.DNet.BusinessFacade.Service.ServiceBookingFacade(User).Retrieve(crit)

        If Not IsNothing(arrLst) AndAlso arrLst.Count > 0 Then
            objSB = CType(arrLst(0), ServiceBooking)
        End If
        Return objSB
    End Function

    Private Function GetDayConfig() As Integer
        Dim intDay As Integer = 0
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, "ServiceBooking.AvailableDayEdit"))
        Dim arrLst As ArrayList = New AppConfigFacade(User).Retrieve(crit)

        If arrLst.Count > 0 Then
            Dim appconf As AppConfig = CType(arrLst(0), AppConfig)
            intDay = CInt(appconf.Value)
        End If
        Return intDay
    End Function

#End Region

#Region "Datagrid Attachment Evidence"
    Private Sub dgEvidenceFile_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEvidenceFile.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)

            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            Dim lnkbtnEvidenceFileDelete As LinkButton = CType(e.Item.FindControl("lnkbtnEvidenceFileDelete"), LinkButton)
            If Not CType(ViewState("vsProcess"), String) = "Insert" Then
                lbtnDownload.Visible = True
                lnkbtnEvidenceFileDelete.Visible = False
            Else
                lbtnDownload.Visible = False
                lnkbtnEvidenceFileDelete.Visible = True
            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim iFileAttachmentEvidence As HtmlInputFile = CType(e.Item.Cells(1).FindControl("iFileAttachmentEvidence"), HtmlInputFile)
            Dim lnkbtnEvidenceFileAdd As LinkButton = CType(e.Item.Cells(2).FindControl("lnkbtnEvidenceFileAdd"), LinkButton)
            If Not CType(ViewState("vsProcess"), String) = "Insert" Then
                iFileAttachmentEvidence.Visible = False
                lnkbtnEvidenceFileAdd.Visible = False
            Else
                iFileAttachmentEvidence.Visible = True
                lnkbtnEvidenceFileAdd.Visible = True
            End If
        End If
    End Sub

    Private Sub UploadAttachment(ByVal ObjAttachment As CustomerCaseResponseEvidence, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo
        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.AttachmentData) Then
                    finfo = New FileInfo(TargetPath + ObjAttachment.EvidenceFile)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.EvidenceFile)
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each _custCaseResponseEvidence As CustomerCaseResponseEvidence In AttachmentCollection
                If _custCaseResponseEvidence.Attachment = FileName Then
                    bResult = True
                    Exit For
                End If
            Next
        End If
        Return bResult
    End Function

    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer = 0
        Dim bResult As Boolean = False

        If AttachmentCollection.Count > 0 Then
            For Each _PQRAttachmentBB As PQRAttachmentBB In AttachmentCollection
                If _PQRAttachmentBB.FileName = FileName AndAlso nIndeks <> i Then
                    bResult = True
                    Exit For
                End If
                i += 1
            Next
        End If
        Return bResult
    End Function

    Private Function CheckLastStatusCaseIsClosed() As Boolean
        Dim bResult As Boolean = False

        Dim arr As New ArrayList
        Dim objCustomerCase As CustomerCase = CType(sessHelper.GetSession("SessCustomerCase"), CustomerCase)
        Dim _criterias As New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _criterias.opAnd(New Criteria(GetType(CustomerCaseResponse), "CustomerCase.ID", MatchType.Exact, objCustomerCase.ID))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(CustomerCaseResponse), "CreatedTime", Sort.SortDirection.ASC))
        arr = New CustomerCaseResponseFacade(User).Retrieve(_criterias, sortColl)
        If arr.Count > 0 Then
            Dim objCustResp As CustomerCaseResponse = CType(arr(arr.Count - 1), CustomerCaseResponse)
            If objCustResp.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Closed Then
                bResult = True
            End If
        End If

        Return bResult
    End Function

#End Region

#Region "Event DataGrid"

    Private Sub dgEvidenceFile_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEvidenceFile.ItemCommand
        Dim _arrCustomerCaseResponseEvidence As ArrayList = CType(sessHelper.GetSession("ATTACMENTEVIDENCE"), ArrayList)
        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("iFileAttachmentEvidence"), HtmlInputFile)
                Dim objPostedData As HttpPostedFile
                Dim sFileName As String

                If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                    MessageBox.Show("Lampiran file masih kosong")
                    BindDataEvidence()
                    Return
                Else
                    objPostedData = FileUpload.PostedFile
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'")
                        BindDataEvidence()
                        Return
                    End If

                    'Validasi Upload file type jpg,jpeg, bmp, png, pdf dan maks size file 1 Mb
                    Dim blnNotValid1 As Boolean = False, blnNotValid2 As Boolean = False
                    If (Not (objPostedData.FileName.Trim() = String.Empty)) Then
                        Dim fileSize As Integer = objPostedData.ContentLength
                        ' 1MB -> 1000 * 1024
                        If (fileSize >= (maxFileSize * 1024) Or fileSize <= 0) Then
                            blnNotValid1 = True
                        End If
                        If ((Not Path.GetExtension(objPostedData.FileName).ToLower() = ".jpg") And
                            (Not Path.GetExtension(objPostedData.FileName).ToLower() = ".jpeg") And
                            (Not Path.GetExtension(objPostedData.FileName).ToLower() = ".bmp") And
                            (Not Path.GetExtension(objPostedData.FileName).ToLower() = ".png") And
                            (Not Path.GetExtension(objPostedData.FileName).ToLower() = ".pdf")) Then
                            blnNotValid2 = True
                        End If

                        If blnNotValid1 = True Or blnNotValid2 = True Then
                            MessageBox.Show("Maksimum upload file 3 MB dan file harus berekstensi: jpg, jpeg, bmp, png dan pdf")
                            Return
                        End If
                    End If

                    If Not FileIsExist(sFileName, _arrCustomerCaseResponseEvidence) Then
                        Dim objCustomerCaseResponseEvidence As CustomerCaseResponseEvidence = New CustomerCaseResponseEvidence

                        objCustomerCaseResponseEvidence.NewItem = True
                        objCustomerCaseResponseEvidence.AttachmentData = objPostedData
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName).Replace("_", "")
                        Dim fname As String = Path.GetFileNameWithoutExtension(objPostedData.FileName).Replace("_", "")

                        objCustomerCaseResponseEvidence.EvidenceFile = AttachmentDirectory & "\" & lblDealer.Text & "\" & lblCaseNumber.Text & "\" & fname & "_" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        objCustomerCaseResponseEvidence.Attachment = SrcFile
                        'Ready to Upload File
                        UploadAttachment(objCustomerCaseResponseEvidence, TargetDirectory)

                        _arrCustomerCaseResponseEvidence.Add(objCustomerCaseResponseEvidence)
                        sessHelper.SetSession("ATTACMENTEVIDENCE", _arrCustomerCaseResponseEvidence)
                        BindDataEvidence()
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                RemoveCustomerCaseResponseEvidence(CType(_arrCustomerCaseResponseEvidence(e.Item.ItemIndex), CustomerCaseResponseEvidence), TempDirectory)
                _arrCustomerCaseResponseEvidence.RemoveAt(e.Item.ItemIndex)
                BindDataEvidence()

            Case "Download" 'Download File
                Dim fileInfo1 As New FileInfo(TargetDirectory)
                Dim PathFile As String = fileInfo1.Directory.FullName & "\" & e.CommandArgument.Trim
                Response.Redirect("../Download.aspx?file=" & PathFile)
        End Select

    End Sub

    Private Sub dgCase_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgCase.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                btnBatal_Click(source, e)
                ViewState.Add("vsProcess", "View")
                ViewCustomerCaseResponse(e.Item.Cells(0).Text, True)
                dgCase.SelectedIndex = e.Item.ItemIndex
                ddlStatus.Enabled = False
                txtComment.Enabled = False
                btnSave.Enabled = False
                btnSB.Enabled = False
                Dim _mode As String = Request.QueryString("mode")
                If Not _mode = "edit" Then
                    pnlEntry.Visible = True
                End If
            Case "Edit"
                ViewState.Add("vsProcess", "Edit")
                ViewCustomerCaseResponse(e.Item.Cells(0).Text, True)
                dgCase.SelectedIndex = e.Item.ItemIndex
                ddlStatus.Enabled = False
            Case "Delete"
                Try
                    dgCase.SelectedIndex = e.Item.ItemIndex
                    Dim objCustomerCaseResponse As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)
                    objCustomerCaseResponse.Delete(objCustomerCaseResponse.Retrieve(CInt(e.Item.Cells(0).Text)))
                    BindDataResponse(dgCase.CurrentPageIndex)
                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail)
                End Try
            Case "View"
                Dim objCCRFacade As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)
                Dim objCCR As CustomerCaseResponse = objCCRFacade.Retrieve(CInt(e.Item.Cells(0).Text))
                Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=View&CCID=" + objCCR.ID.ToString())
            Case "ReSend"
                Dim objresponseFacade As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)
                Dim objresponse As CustomerCaseResponse = objresponseFacade.Retrieve(CInt(e.Item.Cells(0).Text))

                Dim sf As SalesForceInterface = New SalesForceInterface()
                Dim msg As String = String.Empty
                Dim vSFreturn As Boolean = False
                Dim nResult As Integer = 0
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
                        MessageBox.Show("Proses Resend sukses")
                    End If
                Else
                    Dim _criterias As New CriteriaComposite(New Criteria(GetType(WsLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    _criterias.opAnd(New Criteria(GetType(WsLog), "Source", MatchType.Exact, "Internal"))
                    _criterias.opAnd(New Criteria(GetType(WsLog), "Status", MatchType.Exact, "False"))
                    _criterias.opAnd(New Criteria(GetType(WsLog), "Body", MatchType.[Partial], "SALESFORCEIUPDATECASE"))
                    _criterias.opAnd(New Criteria(GetType(WsLog), "Body", MatchType.[Partial], objresponse.CustomerCase.SalesforceID))
                    _criterias.opAnd(New Criteria(GetType(WsLog), "CreatedTime", MatchType.GreaterOrEqual, DateTime.Now.AddMinutes(-1)))

                    Dim objWsLog As WsLog
                    Dim sortColl As SortCollection = New SortCollection
                    sortColl.Add(New Sort(GetType(WsLog), "CreatedTime", Sort.SortDirection.ASC))
                    arr = New WsLogFacade(User).Retrieve(_criterias, sortColl)
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
                        Else
                            MessageBox.Show("Proses Resend gagal")
                        End If
                    Else
                        MessageBox.Show("Proses Resend gagal")
                    End If
                End If

                BindDataResponse(dgCase.CurrentPageIndex)
        End Select
    End Sub

    Private Sub dgCase_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgCase.ItemDataBound
        If e.Item.ItemIndex >= 0 Then
            Dim RowValue As CustomerCaseResponse = CType(e.Item.DataItem, CustomerCaseResponse)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblResponCustomer As Label = CType(e.Item.FindControl("lblResponCustomer"), Label)
            Dim lblResponDealer As Label = CType(e.Item.FindControl("lblResponDealer"), Label)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lbtnDetail"), LinkButton)
            Dim lnkbtnResend As LinkButton = CType(e.Item.FindControl("lbtnResend"), LinkButton)
            Dim lblBookingDatetime As Label = CType(e.Item.FindControl("lblBookingDatetime"), Label)
            Dim lblNoServiceBooking As Label = CType(e.Item.FindControl("lblNoServiceBooking"), Label)
            Dim lblNoWorkOrder As Label = CType(e.Item.FindControl("lblNoWorkOrder"), Label)
            Dim hdnIsSend As HiddenField = CType(e.Item.FindControl("hdnIsSend"), HiddenField)
            Dim lbServiceBooking As LinkButton = CType(e.Item.FindControl("lbServiceBooking"), LinkButton)


            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dgCase.CurrentPageIndex * dgCase.PageSize)
            lblStatus.Text = EnumCustomerCaseResponse.GetStringCustomerResponse(RowValue.Status)

            If Not IsNothing(RowValue.ServiceBooking) Then
                lblNoServiceBooking.Text = RowValue.ServiceBooking.ServiceBookingCode
                lbServiceBooking.Text = RowValue.ServiceBooking.ServiceBookingCode
            Else
                lblNoServiceBooking.Text = ""
                lbServiceBooking.Text = ""
            End If

            If Not IsNothing(RowValue.WorkOrderNumber) Then
                lblNoWorkOrder.Text = RowValue.WorkOrderNumber
            Else
                lblNoWorkOrder.Text = ""
            End If

            If RowValue.BookingDatetime.Year < 1990 Then
                lblBookingDatetime.Text = ""
            Else
                lblBookingDatetime.Text = RowValue.BookingDatetime
            End If

            If RowValue.Response > 0 Then
                Dim respon As ArrayList = New StandardCodeFacade(User).RetrieveByValueIdCategory(RowValue.Response.ToString, "CustomerCase.Response")
                If respon.Count > 0 Then
                    Dim stdCd As StandardCode = CType(respon(0), StandardCode)
                    If stdCd.Category.Contains("Response.Dealer") Then
                        lblResponDealer.Text = stdCd.ValueDesc
                    Else
                        lblResponCustomer.Text = stdCd.ValueDesc
                    End If
                End If
            End If

            Dim arr = New ArrayList
            Dim _criterias As New CriteriaComposite(New Criteria(GetType(CustomerCaseResponseEvidence), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            _criterias.opAnd(New Criteria(GetType(CustomerCaseResponseEvidence), "CustomerCaseResponse.ID", MatchType.Exact, RowValue.ID))
            arr = New CustomerCaseResponseEvidenceFacade(User).Retrieve(_criterias)
            If arr.Count > 0 Then
                lnkbtnDetail.Visible = True
            Else
                lnkbtnDetail.Visible = False
            End If

            If hdnIsSend.Value.Trim = "1" Then
                lnkbtnResend.Visible = False
            Else
                lnkbtnResend.Visible = True
            End If

            Dim _mode As String = Request.QueryString("mode")
            If Not _mode = "edit" Then
                lnkbtnResend.Visible = False
            End If

            'If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            '    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "setSelected(this, '" & RowValue.ID & "');")
            'End If
        End If
    End Sub


    Private Sub dgCase_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgCase.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortCol") Then
            If sessHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortCol", e.SortExpression)
        dgCase.SelectedIndex = -1
        dgCase.CurrentPageIndex = 0
        BindDataResponse(dgCase.CurrentPageIndex)
    End Sub

    Private Sub dgCase_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgCase.PageIndexChanged
        dgCase.CurrentPageIndex = e.NewPageIndex
        BindDataResponse(e.NewPageIndex + 1)
    End Sub

    Protected Sub rptDealerResp_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim rbDealer As RadioButton = CType(e.Item.FindControl("rbDealer"), RadioButton)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            rbDealer.Attributes.Add("onclick", "SetUniqueRadioButton('response',this)")
        End If
    End Sub

    Protected Sub rptCustResp_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim rbCust As RadioButton = CType(e.Item.FindControl("rbCust"), RadioButton)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            rbCust.Attributes.Add("onclick", "SetUniqueRadioButton('response',this)")
        End If
    End Sub

#End Region

    
End Class