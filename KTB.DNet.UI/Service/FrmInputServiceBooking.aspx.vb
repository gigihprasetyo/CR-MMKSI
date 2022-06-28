Imports System.Collections.Generic
Imports System.Linq
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessValidation
Imports System.Text
Imports Newtonsoft.Json

Public Class FrmInputServiceBooking
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bInputPrivilege As Boolean = False
    Private isDealerPiloting As Boolean = False
    Private stdFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private vechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Private fSKindFacade As FSKindFacade = New FSKindFacade(User)
    Private pMKindFacade As PMKindFacade = New PMKindFacade(User)
    Private gRKindFacade As GRKindFacade = New GRKindFacade(User)
    Private recallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    ' Private TSBAFacade As TempServiceBookingActivityFacade = New TempServiceBookingActivityFacade(User)
    Private freeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
    Private recallServiceFacade As RecallServiceFacade = New RecallServiceFacade(User)
    Private pmHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
    Private vechileColorFacade As VechileColorFacade = New VechileColorFacade(User)
    Private chassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
    Private dealerFacade As DealerFacade = New DealerFacade(User)
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private vechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
    Private srFUFacade As ServiceReminderFollowUpFacade = New ServiceReminderFollowUpFacade(User)
    Private ccResFacade As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)
    Private ccFacade As CustomerCaseFacade = New CustomerCaseFacade(User)
    Private appConfFacade As AppConfigFacade = New AppConfigFacade(User)
    Private GrandTotal As Decimal = 0
    Private GrandTotal2 As Decimal = 0
    'Private intOpen As Integer = 0
    'Private boolEdit As Boolean = False

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            Dim id As String = Request.QueryString("id")
            Dim fuId As String = Request.QueryString("FUID")
            Dim ccId As String = Request.QueryString("CCID")
            Dim ccRes As CustomerCaseResponse
            Dim srFU As ServiceReminderFollowUp

            

            ViewState("MenuFrom") = Request.QueryString("menufrom")

            ResetControl()
            ResetHistory()

            ViewState("Mode") = Request.QueryString("mode")

            'Check Mode
            If Not String.IsNullorEmpty(ViewState("Mode")) Then
                If Not String.IsNullorEmpty(ID) Then
                    LoadData(ID)
                ElseIf Not String.IsNullorEmpty(ccId) Then
                    ccRes = ccResFacade.Retrieve(CInt(ccId))
                    LoadDataCustomerResponse(ccRes)

                    If IsNothing(ccRes.ServiceBooking) Or ViewState("Mode") = "New" Then
                        Dim objCM As ChassisMaster = chassisMasterFacade.Retrieve(ccRes.CustomerCase.ChassisNumber)
                        If objCM.ID <> 0 Then
                            GetHistory(objCM)
                        End If

                        txtNoChassis.Text = objCM.ChassisNumber
                        txtNamaKonsumen.Text = ccRes.CustomerCase.CustomerName
                        txtPlatNomor.Text = ccRes.CustomerCase.PlateNumber
                        txtNoTelp.Text = ccRes.CustomerCase.Phone

                        crit = New CriteriaComposite(New Criteria(GetType(ServiceReminderFollowUp), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(ServiceReminderFollowUp), "ServiceReminder.CaseNumber", MatchType.Exact, ccRes.CustomerCase.CaseNumber))
                        crit.opAnd(New Criteria(GetType(ServiceReminderFollowUp), "FollowUpStatus", MatchType.InSet, String.Format("({0}, {1})", _
                                                CInt(EnumGlobalServiceReminder.ServiceReminderFollowUpStatus.Baru), _
                                                CInt(EnumGlobalServiceReminder.ServiceReminderFollowUpStatus.InProgress))))
                        Dim sort As Sort = New Sort(GetType(ServiceReminderFollowUp), "ID", sort.SortDirection.DESC)
                        Dim sorts As SortCollection = New SortCollection
                        sorts.Add(sort)

                        Dim arr As ArrayList = srFUFacade.Retrieve(crit, sorts)
                        If arr.Count > 0 Then
                            srFU = CType(arr(0), ServiceReminderFollowUp)
                            ViewState("FUID") = srFU.ID
                        End If
                    Else
                        LoadData(ccRes.ServiceBooking.ID)
                    End If
                ElseIf Not String.IsNullorEmpty(fuId) Then
                    srFU = srFUFacade.Retrieve(CInt(fuId))
                    ViewState("FUID") = srFU.ID

                    If IsNothing(srFU.ServiceBooking) Or ViewState("Mode") = "New" Then
                        If srFU.ServiceReminder.ChassisMaster.ID <> 0 Then
                            GetHistory(srFU.ServiceReminder.ChassisMaster)
                        End If
                        txtNoChassis.Text = srFU.ServiceReminder.ChassisNumber
                        txtNamaKonsumen.Text = srFU.ServiceReminder.CustomerName
                        txtNoTelp.Text = srFU.ServiceReminder.CustomerPhoneNumber

                        crit = New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(CustomerCaseResponse), "CustomerCase.CaseNumber", MatchType.Exact, srFU.ServiceReminder.CaseNumber))
                        crit.opAnd(New Criteria(GetType(CustomerCaseResponse), "Status", MatchType.No, CInt(EnumCustomerCaseResponse.CustomerCaseResponse.Closed)))
                        Dim sort As Sort = New Sort(GetType(CustomerCaseResponse), "ID", sort.SortDirection.DESC)
                        Dim sorts As SortCollection = New SortCollection
                        sorts.Add(sort)

                        Dim arr As ArrayList = ccResFacade.Retrieve(crit, sorts)
                        If arr.Count > 0 Then
                            ccRes = CType(arr(0), CustomerCaseResponse)
                            LoadDataCustomerResponse(ccRes)
                        End If
                    Else
                        LoadData(srFU.ServiceBooking.ID)
                    End If
                End If
            Else
                ViewState("Mode") = "New"
            End If

            
        End If
        If hdChangeVechile.Value = "" Then
            hdChangeVechile.Value = 1
        Else
            hdChangeVechile.Value = CInt(hdChangeVechile.Value) + 1
        End If
    End Sub

    Protected Sub dgSC_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs)
        dgSC.CurrentPageIndex = e.NewPageIndex
        RefreshGridSC(e.NewPageIndex)
    End Sub

    Protected Sub dgSC_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        If e.SortExpression = ViewState.Item("SCSortColumn") Then
            If ViewState.Item("SCSortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SCSortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SCSortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("SCSortColumn") = e.SortExpression
        dgSC.SelectedIndex = -1
        dgSC.CurrentPageIndex = 0
        RefreshGridSC(0)
    End Sub

    Protected Sub dgSC_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        Dim RowValue As RecallService = CType(e.Item.DataItem, RecallService)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblTglPro As Label = CType(e.Item.FindControl("lblTglPro"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgSC.PageSize * dgSC.CurrentPageIndex)
            If Not IsNothing(RowValue.CreatedTime) Then
                If RowValue.ServiceDate <= "1/1/1900" Then
                    lblTglPro.Text = ""
                Else
                    lblTglPro.Text = Format(RowValue.CreatedTime, "dd/MM/yyyy")
                End If
            End If
        End If
    End Sub

    Protected Sub dgFS_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs)
        dgFS.CurrentPageIndex = e.NewPageIndex
        RefreshGridFS(e.NewPageIndex)
    End Sub

    Protected Sub dgFS_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        If e.SortExpression = ViewState.Item("FSSortColumn") Then
            If ViewState.Item("FSSortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("FSSortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("FSSortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("FSSortColumn") = e.SortExpression
        dgFS.SelectedIndex = -1
        dgFS.CurrentPageIndex = 0
        RefreshGridFS(0)
    End Sub

    Protected Sub dgFS_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        Dim RowValue As FreeService = CType(e.Item.DataItem, FreeService)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblTglPro As Label = CType(e.Item.FindControl("lblTglPro"), Label)
            Dim lblFS As Label = CType(e.Item.FindControl("lblFS"), Label)
            Dim lblkind As Label = CType(e.Item.FindControl("lblKind"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgFS.PageSize * dgFS.CurrentPageIndex)

            If RowValue.Status = EnumFSStatus.FSStatus.Baru Then
                lblStatus.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Baru"">"
            ElseIf RowValue.Status = EnumFSStatus.FSStatus.Rilis Then
                lblStatus.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Rilis"">"
            ElseIf RowValue.Status = EnumFSStatus.FSStatus.Proses Then
                lblStatus.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Proses"">"
            ElseIf RowValue.Status = EnumFSStatus.FSStatus.Selesai Then
                lblStatus.Text = "<img src=""../images/green.gif"" border=""0"" alt=""Selesai"">"
            End If

            If Not IsNothing(RowValue.FSKind) Then
                lblkind.Text = RowValue.FSKind.KindDescription
            End If

            If Not IsNothing(RowValue.ServiceDate) Then
                If RowValue.ServiceDate <= "1/1/1900" Then
                    lblFS.Text = ""
                Else
                    lblFS.Text = Format(RowValue.ServiceDate, "dd/MM/yyyy")
                End If
            End If

            If Not IsNothing(RowValue.CreatedTime) Then
                If RowValue.ServiceDate <= "1/1/1900" Then
                    lblTglPro.Text = ""
                Else
                    lblTglPro.Text = Format(RowValue.CreatedTime, "dd/MM/yyyy")
                End If
            End If
        End If
    End Sub

    Protected Sub dgPM_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs)
        dgPM.CurrentPageIndex = e.NewPageIndex
        RefreshGridPM(e.NewPageIndex)
    End Sub

    Protected Sub dgPM_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        If e.SortExpression = ViewState.Item("PMSortColumn") Then
            If ViewState.Item("PMSortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("PMSortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("PMSortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("PMSortColumn") = e.SortExpression
        dgPM.SelectedIndex = -1
        dgPM.CurrentPageIndex = 0
        RefreshGridPM(0)
    End Sub

    Protected Sub dgPM_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        Dim RowValue As PMHeader = CType(e.Item.DataItem, PMHeader)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblTglPM As Label = CType(e.Item.FindControl("lblTglPM"), Label)
            Dim lblTglRilis As Label = CType(e.Item.FindControl("lblTglRilis"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgPM.PageSize * dgPM.CurrentPageIndex)

            If RowValue.ServiceDate <= "01/01/1900" Then
                lblTglPM.Text = ""
            Else
                lblTglPM.Text = RowValue.ServiceDate.ToString("dd/MM/yyyy")
            End If

            If RowValue.ReleaseDate <= "01/01/1900" Then
                lblTglRilis.Text = ""
            Else
                lblTglRilis.Text = RowValue.ReleaseDate.ToString("dd/MM/yyyy")
            End If
            If RowValue.PMStatus <> "" Then

                If RowValue.PMStatus = EnumPMStatus.PMStatus.Baru Then
                    lblStatus.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Baru"">"
                ElseIf RowValue.PMStatus = EnumPMStatus.PMStatus.Proses Then
                    lblStatus.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Proses"">"
                ElseIf RowValue.PMStatus = EnumPMStatus.PMStatus.Selesai Then
                    lblStatus.Text = "<img src=""../images/green.gif"" border=""0"" alt=""Selesai"">"
                End If
            Else
                lblStatus.Text = ""
            End If
        End If
    End Sub

    Protected Sub btnGetInfoChassis_Click(sender As Object, e As ImageClickEventArgs)
        crit = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ChassisMaster), "Category.ID", MatchType.NotInSet, 3))
        crit.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, txtNoChassis.Text))

        Dim lst As ArrayList = chassisMasterFacade.Retrieve(crit)
        If lst.Count > 0 Then
            Dim objChassis As ChassisMaster = CType(lst(0), ChassisMaster)
            GetHistory(objChassis)
        Else
            If ViewState("Mode") = "New" Then
                MessageBox.Show("Nomor Chassis tidak ditemukan.")
            End If
            ResetHistory()
        End If
    End Sub

    Protected Sub ddlJnsKegiatan_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlJnsService.Items.Clear()

        If Not IsNothing(sender) Then
            Dim ddl As DropDownList = sender
            Select Case ddl.SelectedValue
                Case 1
                    crit = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = fSKindFacade.Retrieve(crit)

                    With ddlJnsService.Items
                        For Each obj As FSKind In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
                        Next
                    End With
                Case 2
                    crit = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = pMKindFacade.Retrieve(crit)

                    With ddlJnsService.Items
                        For Each obj As PMKind In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
                        Next
                    End With
                Case 3
                    crit = New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = recallCategoryFacade.Retrieve(crit)

                    With ddlJnsService.Items
                        For Each obj As RecallCategory In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.RecallRegNo, obj.Description), obj.ID))
                        Next
                    End With
                Case 4
                    crit = New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = gRKindFacade.Retrieve(crit)

                    With ddlJnsService.Items
                        For Each obj As GRKind In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
                        Next
                    End With
            End Select
        End If

        ddlJnsService.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Protected Sub ddlModelKendaraan_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim lst As List(Of VechileType) = New List(Of VechileType)
        If ddlModelKendaraan.SelectedIndex <> 0 Then
            crit = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.Exact, CShort(ddlModelKendaraan.SelectedValue)))
            lst = vechileTypeFacade.Retrieve(crit).Cast(Of VechileType).ToList
        End If

        ddlVehicleTypeCode.Items.Clear()
        With ddlVehicleTypeCode.Items
            For Each obj As VechileType In lst
                .Add(New ListItem(obj.VechileCodeDesc, obj.ID))
            Next
        End With

        ddlVehicleTypeCode.Items.Insert(0, "Silahkan Pilih")
        If hdChangeVechile.Value <> "" Then
            If txtIncomingPlan.Text <> "" Or txtStallName.Text <> "" Then
                txtIncomingPlan.Text = ""
                txtStallName.Text = ""
            End If
        End If
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs)
        Dim msgErr As String = String.Empty
        Dim lstBook As List(Of ServiceBooking) = New List(Of ServiceBooking)

        If Not Validate(msgErr, lstBook) Then
            MessageBox.Show(msgErr)
            Exit Sub
        End If

        Dim confMsg As String = String.Empty
        If hdConfirm.Value = "-1" And lstBook.Count > 0 Then
            Dim svcBook As ServiceBooking = CType(lstBook(0), ServiceBooking)
            Dim svcActivity As ServiceBookingActivity = CType(svcBook.ServiceBookingActivities(0), ServiceBookingActivity)
            confMsg = _
                String.Format("Kendaraan tersebut sudah melakukan service booking untuk tanggal {0} dengan tipe Service {1}, Apakah Anda yakin tetap ingin menyimpan data transaksi ini?", _
                              svcBook.WorkingTimeStart.ToString("dd MMMM yyyy HH:mm"), stdFacade.GetByCategoryValue("ServiceBooking.ServiceType", svcActivity.ServiceTypeID.ToString).ValueDesc)
            RegisterStartupScript("Confirm", String.Format("<script>ShowConfirm('{0}', 'btnSimpan');</script>", confMsg))
            Return
        ElseIf hdConfirm.Value = "-1" And lstBook.Count = 0 Then
            confMsg = "Apakah Anda yakin ingin menyimpan transaksi ini?"
            RegisterStartupScript("Confirm", String.Format("<script>ShowConfirm('{0}', 'btnSimpan');</script>", confMsg))
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        Dim isBothSource As Boolean = Not IsNothing(ViewState("FUID")) And Not IsNothing(ViewState("CustomerCaseRespID"))
        Dim obj As ServiceBooking = New ServiceBooking
        Dim ccRes As CustomerCaseResponse
        Dim srFU As ServiceReminderFollowUp
        Dim activities As ArrayList = New ArrayList
        Dim isInsertCCResp As Boolean = False

        PopulateData(obj, activities)

        Dim result As Integer = 0
        Select Case ViewState("MenuFrom")
            Case "GSR"
                srFU = srFUFacade.Retrieve(CInt(ViewState("FUID")))
                PopulateDataFU(srFU, obj)

                If isBothSource Then
                    ccRes = ccResFacade.Retrieve(CInt(ViewState("CustomerCaseRespID")))
                    PopulateDataCCResponse(ccRes, obj)
                    isInsertCCResp = ccRes.ID = 0
                End If

                If ViewState("Mode") = "New" Then
                    If isBothSource Then
                        result = serviceBookingFacade.Insert(obj, activities, srFU, ccRes)
                        If result > 0 Then
                            UpdateDescriptionCustomerResponse(result, ccRes)
                        End If
                    Else
                        result = serviceBookingFacade.Insert(obj, activities, srFU)
                    End If
                Else
                    If isBothSource Then
                        result = serviceBookingFacade.Update(obj, activities, srFU, ccRes)
                    Else
                        result = serviceBookingFacade.Update(obj, activities, srFU)
                    End If
                End If
            Case "CustomerCase"
                ccRes = ccResFacade.Retrieve(CInt(ViewState("CustomerCaseRespID")))
                PopulateDataCCResponse(ccRes, obj)
                isInsertCCResp = ccRes.ID = 0

                If isBothSource Then
                    srFU = srFUFacade.Retrieve(CInt(ViewState("FUID")))
                    PopulateDataFU(srFU, obj)
                End If

                If ViewState("Mode") = "New" Then
                    If isBothSource Then
                        result = serviceBookingFacade.Insert(obj, activities, srFU, ccRes)
                    Else
                        result = serviceBookingFacade.Insert(obj, activities, ccRes)
                    End If

                    If result > 0 Then
                        UpdateDescriptionCustomerResponse(result, ccRes)
                    End If
                Else
                    If isBothSource Then
                        result = serviceBookingFacade.Update(obj, activities, srFU, ccRes)
                    Else
                        result = serviceBookingFacade.Update(obj, activities, ccRes)
                    End If
                End If
            Case Else
                If ViewState("Mode") = "New" Then
                    result = serviceBookingFacade.Insert(obj, activities)
                ElseIf Not IsNothing(obj.CustomerCaseResponse) Then
                    ccRes = ccResFacade.Retrieve(obj.CustomerCaseResponse.ID)
                    PopulateDataCCResponse(ccRes, obj)
                    isInsertCCResp = ccRes.ID = 0

                    result = serviceBookingFacade.Update(obj, activities, ccRes)
                Else
                    result = serviceBookingFacade.Update(obj, activities)
                End If
        End Select

        If result > 0 And isInsertCCResp Then
            SentToSalesForce(ccRes)
        End If

        If result > 0 Then
            If ViewState("StatusSB") = 2 Then
                MessageBox.Show("Simpan berhasil. Mohon lakukan konfirmasi jika data sudah final.")
            Else
                MessageBox.Show("Simpan berhasil.")
            End If
            If hdChangeVechile.Value >= 1 Then
                hdChangeVechile.Value = ""
                'Else
                '    hdChangeVechile.Value = CInt(hdChangeVechile.Value) + 1
            End If
            ViewState("Mode") = "Edit"
            LoadData(result)
        Else
            MessageBox.Show("Simpan gagal.")
        End If
    End Sub

    Protected Sub btnBaru_Click(sender As Object, e As EventArgs)
        If IsNothing(Request.QueryString("id")) And String.IsNullorEmpty(ViewState("MenuFrom")) Then
            If hdConfirm.Value = "-1" Then
                Dim confMsg As String = "Apakah Anda yakin ingin membuat service booking baru?"
                RegisterStartupScript("Confirm", String.Format("<script>ShowConfirm('{0}', 'btnBaru');</script>", confMsg))
                Return
            Else
                hdConfirm.Value = "-1"
            End If

            ResetControl()
            ResetHistory()
            SetControl(True)
        Else
            If Not String.IsNullorEmpty(ViewState("MenuFrom").ToString) Then
                Select Case ViewState("MenuFrom").ToString
                    Case "Daftar"
                        Response.Redirect("FrmListServiceBooking.aspx")
                    Case "GSR"
                        Response.Redirect("FrmGSServiceFollowUp.aspx")
                    Case "CustomerCase"
                        Response.Redirect(String.Format("~/Marketing/FrmCustomerCaseResponse.aspx?mode=edit&caseId={0}", ViewState("CustomerCaseID")))
                End Select
            End If

        End If
        
    End Sub

    Protected Sub btnPopupPlan_Click(sender As Object, e As ImageClickEventArgs)
        Dim msgErr As String = String.Empty
        Dim objSBAS As ArrayList = CType(sessHelper.GetSession("SBActivity"), ArrayList)
        If rbMitsubishi.Checked Then
            If ddlModelKendaraan.SelectedIndex = 0 Then
                msgErr = "Model kendaraan harus dipilih."
            ElseIf IsNothing(objSBAS) Then
                msgErr = "Jenis Kegiatan dan Jenis Service harus dipilih."
                'ElseIf ddlJnsKegiatan.SelectedIndex = 0 Then
                '    msgErr = "Jenis kegiatan harus dipilih."
                'ElseIf ddlJnsService.SelectedIndex = 0 Then
                '    msgErr = "Jenis service harus dipilih."
            ElseIf ddlVehicleTypeCode.SelectedIndex = 0 Then
                msgErr = "Kode Tipe Kendaraan harus dipilih."
            End If
        End If

        'ViewState("ServiceBookingActivity") = objSBAS
        'sessHelper.SetSession("OBJSBA") = objSBAS.
        'Session([ooo] = objSBAS)

        If String.IsNullorEmpty(msgErr) Then
            RegisterStartupScript("Open", String.Format("<script>ShowPPPlanSelection({0},{1},{2},{3});</script>", _
                    hdID.Value,
                    IIf(ddlModelKendaraan.SelectedIndex = 0, "0", ddlModelKendaraan.SelectedValue), _
                    IIf(ddlVehicleTypeCode.SelectedIndex = 0, "0", ddlVehicleTypeCode.SelectedValue),
                    IIf(String.IsNullorEmpty(hdCCID.Value), "0", hdCCID.Value)))
            Return
        Else
            MessageBox.Show(msgErr)
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Dim confMsg As String = String.Empty
        If hdConfirm.Value = "-1" Then
            confMsg = "Apakah anda yakin ingin membatalkan transaksi ini?"
            RegisterStartupScript("Confirm", String.Format("<script>ShowConfirm('{0}', 'btnCancel');</script>", confMsg))
            Return
        Else
            hdConfirm.Value = "-1"
        End If

        If hdCancel.Value = "-1" Then
            If Not IsNothing(ViewState("CustomerCaseRespID")) Then
                RegisterStartupScript("Open", String.Format("<script>ShowCancelServiceBooking({0},'CC');</script>", _
                        hdID.Value))
                Return
            Else
                Dim result As Integer = 0
                Dim obj As ServiceBooking = serviceBookingFacade.Retrieve(CInt(hdID.Value))
                obj.Status = CShort(EnumStallMaster.StatusBooking.Batal).ToString

                Dim srFU As ServiceReminderFollowUp
                If Not IsNothing(ViewState("FUID")) Then
                    srFU = srFUFacade.Retrieve(CInt(ViewState("FUID")))
                    srFU.ID = 0
                    srFU.FollowUpStatus = EnumGlobalServiceReminder.ServiceReminderFollowUpStatus.InProgress
                    srFU.FollowUpAction = stdFacade.GetByCategoryValue("ServiceBooking.GSR.Response", "7").ValueDesc
                    srFU.FollowUpDate = DateTime.Now

                    result = serviceBookingFacade.Update(obj, Nothing, srFU)
                Else
                    result = serviceBookingFacade.Update(obj)
                End If

                If result > 0 Then
                    MessageBox.Show("Service Booking Dibatalkan.")
                    ViewState("Mode") = "View"
                    LoadData(result)
                Else
                    MessageBox.Show("Simpan gagal.")
                End If
            End If
        Else
            hdCancel.Value = "-1"
            ViewState("Mode") = "View"
            LoadData(CInt(hdID.Value))
        End If
    End Sub

    Protected Sub rbMitsubishi_CheckedChanged(sender As Object, e As EventArgs)
        OnChangeJenisKendaraan()
    End Sub

    Protected Sub rbNonMitsubishi_CheckedChanged(sender As Object, e As EventArgs)
        OnChangeJenisKendaraan()
    End Sub

    Protected Sub txtIncomingPlan_TextChanged(sender As Object, e As EventArgs)
        If Not String.IsNullorEmpty(txtIncomingPlan.Text) And Not IsNothing(ViewState("CustomerCaseRespID")) Then
            Dim arr() As String = txtIncomingPlan.Text.Split("-")
            Dim workDateTime As DateTime = CType(String.Format("{0} {1}", arr(0).Trim, arr(1).Trim), DateTime)
            Dim reqDateTime As DateTime = Convert.ToDateTime(hdBookingTime.Value)
            trRespon.Visible = reqDateTime <> workDateTime
        Else
            trRespon.Visible = False
        End If
    End Sub
#End Region

#Region "Custom Method"

    Private Sub CheckPrivilege()
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.ServiceBooking_Input_Privilage)
        isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingServiceBooking))
        If (Not m_bInputPrivilege Or Not isDealerPiloting) And Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If IsNothing(Request.QueryString("mode")) Or (Not IsNothing(Request.QueryString("mode")) AndAlso Request.QueryString("mode").ToString() <> "View") Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Stall - Input Service Booking")
            End If
        End If
    End Sub

    Private Sub PopulateDataFU(ByRef srFU As ServiceReminderFollowUp, ByVal obj As ServiceBooking)
        'Dim currBookingDate As DateTime = IIf(obj.WorkingTimeStart > obj.IncomingDateStart, obj.IncomingDateStart, obj.WorkingTimeStart)
        Dim status As Integer = srFU.ServiceReminder.Status
        If status <> EnumGlobalServiceReminder.ServiceReminderStatus.CSMMKSI _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Finish _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Archive Then
            If Not IsNothing(srFU.ServiceBooking) And ViewState("Mode") <> "New" Then
                If srFU.BookingDate <> obj.WorkingTimeStart Then
                    srFU.ID = 0
                    srFU.FollowUpAction = stdFacade.GetByCategoryValue("ServiceBooking.GSR.Response", "4").ValueDesc
                    srFU.BookingDate = obj.WorkingTimeStart
                End If
            Else
                srFU.ID = 0
                srFU.FollowUpAction = stdFacade.GetByCategoryValue("ServiceBooking.GSR.Response", "3").ValueDesc
                srFU.BookingDate = obj.WorkingTimeStart
            End If

            srFU.FollowUpDate = DateTime.Now
            srFU.FollowUpStatus = EnumGlobalServiceReminder.ServiceReminderFollowUpStatus.InProgress
        End If
    End Sub

    Private Sub PopulateDataCCResponse(ByRef ccRes As CustomerCaseResponse, ByVal obj As ServiceBooking)
        'Dim currBookingDate As DateTime = IIf(obj.WorkingTimeStart > obj.IncomingDateStart, obj.IncomingDateStart, obj.WorkingTimeStart)

        If Not ccRes.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Closed Then
            If Not IsNothing(ccRes.ServiceBooking) And ViewState("Mode") <> "New" Then
                If ccRes.BookingDatetime <> obj.WorkingTimeStart Then
                    ccRes.ID = 0
                    ccRes.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Re_Schedule
                    ccRes.BookingDatetime = obj.WorkingTimeStart
                End If
            Else
                ccRes.ID = 0
                ccRes.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Inprogres
                ccRes.BookingDatetime = obj.WorkingTimeStart
            End If

            ccRes.IsSend = 0
            If ddlRespon.SelectedIndex <> 0 Then
                ccRes.Response = CInt(ddlRespon.SelectedValue)
            Else
                ccRes.Response = 0
            End If
        End If
    End Sub

    Private Sub PopulateData(ByRef obj As ServiceBooking, ByRef activities As ArrayList)
        Dim arr() As String = txtIncomingPlan.Text.Split("-")
        Dim activity As ServiceBookingActivity
        activities = CType(sessHelper.GetSession("SBActivity"), ArrayList)
        activity = CType(activities(0), ServiceBookingActivity)


        If Not String.IsNullorEmpty(hdID.Value) And hdID.Value <> "0" Then
            obj = serviceBookingFacade.Retrieve(CInt(hdID.Value))
            activities = CType(sessHelper.GetSession("SBActivity"), ArrayList)
            'SetControl()
            'obj.ServiceBookingActivities = CType(sessHelper.GetSession("SBActivity"), ArrayList)
            'activities = obj.ServiceBookingActivities
        End If
        'activity.add()
        ''With ddlJK.Items
        'For Each objs As ServiceBookingActivity In activities
        '    'objs.ServiceTypeID = activities[]
        '    'objs.KindCode = ddlJnsService.SelectedValue
        '    '.Add(New ListItem(obj.ValueDesc, obj.ValueId))
        '    objs.
        'Next()
        'End With

        '        activity = CType(sessHelper.GetSession("SBActivity"), ServiceBookingActivity)

        'If Not String.IsNullorEmpty(hdID.Value) And hdID.Value <> "0" Then
        '    obj = serviceBookingFacade.Retrieve(CInt(hdID.Value))
        '    'activities = obj.ServiceBookingActivities
        '    activities = CType(sessHelper.GetSession("SBActivity"), ArrayList)
        'End If
        'activities = CType(sessHelper.GetSession("SBActivity"), ArrayList)

        obj.ChassisMaster = chassisMasterFacade.Retrieve(txtNoChassis.Text)
        obj.ChassisNumber = txtNoChassis.Text
        obj.Notes = txtCatatan.Text

        If arr.Count = 3 Then
            obj.WorkingTimeStart = CType(String.Format("{0} {1}", arr(0).Trim, arr(1).Trim), DateTime)
            obj.WorkingTimeEnd = CType(String.Format("{0} {1}", arr(0).Trim, arr(2).Trim), DateTime)
        End If

        obj.CustomerPhoneNumber = txtNoTelp.Text
        obj.CustomerName = txtNamaKonsumen.Text
        obj.OdoMeter = IIf(String.IsNullorEmpty(txtOdoMeter.Text), 0, txtOdoMeter.Text)
        obj.PlateNumber = txtPlatNomor.Text
        obj.Dealer = dealerFacade.Retrieve(objDealer.ID)

        If Not String.IsNullorEmpty(txtIncomingStart.Text) Then
            obj.IncomingDateStart = CType(String.Format("{0} {1}", arr(0).Trim, txtIncomingStart.Text.Trim), DateTime)
        Else
            obj.IncomingDateStart = obj.WorkingTimeStart
        End If

        If Not String.IsNullorEmpty(txtIncomingStart.Text) Then
            obj.IncomingDateEnd = CType(String.Format("{0} {1}", arr(0).Trim, txtIncomingEnd.Text.Trim), DateTime)
        Else
            obj.IncomingDateEnd = obj.WorkingTimeEnd
        End If

        'If activities.Count > 0 Then
        '    For Each item As ServiceBookingActivity In activities
        '        item.ServiceTypeID = CInt(ddlJnsKegiatan.SelectedValue)
        '        item.KindCode = ddlJnsService.SelectedValue
        '    Next
        'Else
        '    activity = New ServiceBookingActivity
        '    activity.ServiceTypeID = CInt(ddlJnsKegiatan.SelectedValue)
        '    activity.KindCode = ddlJnsService.SelectedValue
        '    activities.Add(activity)
        'End If
        If ViewState("StatusSB") = 2 Then
            If ViewState("Konfirmasi") = False Then
                obj.Status = CInt(EnumStallMaster.StatusBooking.Request)
            Else
                obj.Status = CInt(EnumStallMaster.StatusBooking.Booked)
            End If
        Else
            obj.Status = CInt(EnumStallMaster.StatusBooking.Booked)
        End If
        obj.PickupType = CShort(ddlPickupType.SelectedValue)
        obj.StallMaster = stallMasterFacade.RetrieveStallCodeDealer(txtStallName.Text, objDealer.ID)
        obj.IsMitsubishi = IIf(rbMitsubishi.Checked, 1, 0)
        obj.StandardTime = CDec(hdStandardTime.Value)
        obj.StallServiceType = CDec(hdJenisService.Value)

        If ddlServiceAdvisor.SelectedIndex > 0 Then
            obj.TrTrainee = New KTB.DNet.BusinessFacade.Training.TrTraineeFacade(User).Retrieve(CInt(ddlServiceAdvisor.SelectedValue))
        End If

        If rbMitsubishi.Checked Then
            obj.VechileModel = vechileModelFacade.Retrieve(CInt(ddlModelKendaraan.SelectedValue))
            obj.VechileType = vechileTypeFacade.Retrieve(CInt(ddlVehicleTypeCode.SelectedValue))
            obj.VehicleTypeDescription = ""
        Else
            obj.VechileModel = Nothing
            obj.VechileType = Nothing
            obj.VehicleTypeDescription = txtTipeKendaraan.Text
        End If
    End Sub

    Private Function Validate(ByRef msgErr As String, ByRef lstBook As List(Of ServiceBooking)) As Boolean
        Dim objSBAS As ArrayList = CType(sessHelper.GetSession("SBActivity"), ArrayList)
        If txtPlatNomor.Text = "" Then
            msgErr = "Nomor plat harus diisi."
        ElseIf txtNamaKonsumen.Text = "" Then
            msgErr = "Nama Konsumen harus diisi."
        ElseIf txtNoTelp.Text = "" Then
            msgErr = "Nomor telp. konsumen harus diisi."
        ElseIf txtIncomingPlan.Text = "" Then
            msgErr = "Rencana pengerjaan harus diisi."
        ElseIf rbMitsubishi.Checked And ddlModelKendaraan.SelectedIndex = 0 Then
            msgErr = "Model kendaraan harus dipilih."
        ElseIf rbMitsubishi.Checked And ddlVehicleTypeCode.SelectedIndex = 0 Then
            msgErr = "Kode tipe kendaraan harus dipilih."
        ElseIf rbNonMitsubishi.Checked And String.IsNullorEmpty(txtTipeKendaraan.Text) Then
            msgErr = "Tipe Kendaraan harus diisi."
        ElseIf IsNothing(objSBAS) Then
            msgErr = "Jenis Kegiatan dan Jenis Service harus dipilih."
            'ElseIf ddlJnsKegiatan.SelectedIndex = 0 Then
            '    msgErr = "Jenis kegiatan harus dipilih."
            'ElseIf ddlJnsService.SelectedIndex = 0 Then
            '    msgErr = "Jenis service harus dipilih."
        ElseIf ddlPickupType.SelectedIndex = 0 Then
            msgErr = "Rencana kedatangan harus dipilih."
        ElseIf CInt(ddlPickupType.SelectedValue) = CInt(EnumStallMaster.PickupType.DiTinggal) And _
            (String.IsNullorEmpty(txtIncomingStart.Text) Or String.IsNullorEmpty(txtIncomingEnd.Text)) Then
            msgErr = "Waktu kedatangan dari dan sampai harus diisi."
            'ElseIf ddlJnsKegiatan.SelectedValue <> CInt(EnumStallMaster.ServiceType.FS) _
            '        And ddlJnsKegiatan.SelectedValue <> CInt(EnumStallMaster.ServiceType.PM) _
            '        And Not IsNothing(ViewState("FUID")) Then
            '    msgErr = "Jenis kegiatan yang dipilih harus FS atau PM."
            
        ElseIf Not IsNothing(ViewState("CustomerCaseRespID")) Then
            Dim arr() As String = txtIncomingPlan.Text.Split("-")
            Dim workDateTime As DateTime = CType(String.Format("{0} {1}", arr(0).Trim, arr(1).Trim), DateTime)
            Dim reqDateTime As DateTime = Convert.ToDateTime(hdBookingTime.Value)
            If (reqDateTime <> workDateTime) And ddlRespon.Visible AndAlso ddlRespon.SelectedIndex = 0 Then
                msgErr = "Dealer Respon harus dipilih."
            End If
        End If

        If Not IsNothing(ViewState("FUID")) Then
            'If (ViewState("Mode") <> "New") Then
            Dim boolMultipleService As Boolean = False
            For Each obj As ServiceBookingActivity In objSBAS
                If obj.ServiceTypeID = CInt(EnumStallMaster.ServiceType.FS) Or obj.ServiceTypeID = CInt(EnumStallMaster.ServiceType.PM) And Not IsNothing(ViewState("FUID")) Then
                    boolMultipleService = True
                    Exit For
                End If
            Next

            If (Not boolMultipleService) Then
                msgErr = "Jenis kegiatan yang dipilih harus FS atau PM."
            End If
            'End If
            'ViewState.Remove("ServiceType2")
        End If


        If String.IsNullorEmpty(msgErr) Then
            crit = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(ServiceBooking), "ChassisMaster.ChassisNumber", MatchType.Exact, txtNoChassis.Text.Trim()))
            crit.opAnd(New Criteria(GetType(ServiceBooking), "WorkingTimeStart", MatchType.GreaterOrEqual, DateTime.Now.Date))
            If ViewState("StatusSB") <> 2 Then
                crit.opAnd(New Criteria(GetType(ServiceBooking), "Status", MatchType.Exact, CInt(EnumStallMaster.StatusBooking.Booked)))
            Else
                crit.opAnd(New Criteria(GetType(ServiceBooking), "Status", MatchType.Exact, CInt(EnumStallMaster.StatusBooking.Request)))
            End If
            crit.opAnd(New Criteria(GetType(ServiceBooking), "Dealer.ID", MatchType.Exact, objDealer.ID))
            If ViewState("Mode") = "Edit" Then
                crit.opAnd(New Criteria(GetType(ServiceBooking), "ServiceBookingCode", MatchType.No, lblNoReservasi.Text))
            End If

            Dim sort As Sort = New Sort(GetType(ServiceBooking), "ID", sort.SortDirection.DESC)
            Dim sorts As SortCollection = New SortCollection
            sorts.Add(sort)

            Dim arr As ArrayList = serviceBookingFacade.Retrieve(crit, sorts)
            If IsThereServiceBooking(arr, lstBook) Then
                Dim svcBook As ServiceBooking = CType(lstBook(0), ServiceBooking)
                Dim svcActivity As ServiceBookingActivity = CType(svcBook.ServiceBookingActivities(0), ServiceBookingActivity)
                msgErr = String.Format("Kendaraan tersebut sudah melakukan Service Booking untuk tanggal {0} dengan tipe service yang sama, yaitu {1}. Sehingga transaksi tidak dapat dilanjutkan", _
                                               svcBook.WorkingTimeStart.ToString("dd MMMM yyyy HH:mm"), stdFacade.GetByCategoryValue("ServiceBooking.ServiceType", svcActivity.ServiceTypeID.ToString).ValueDesc)
            Else
                crit = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(ServiceBooking), "ChassisMaster.ChassisNumber", MatchType.Exact, txtNoChassis.Text.Trim()))
                crit.opAnd(New Criteria(GetType(ServiceBooking), "WorkingTimeStart", MatchType.GreaterOrEqual, DateTime.Now.Date))
                If ViewState("StatusSB") <> 2 Then
                    crit.opAnd(New Criteria(GetType(ServiceBooking), "Status", MatchType.Exact, CInt(EnumStallMaster.StatusBooking.Booked)))
                Else
                    crit.opAnd(New Criteria(GetType(ServiceBooking), "Status", MatchType.Exact, CInt(EnumStallMaster.StatusBooking.Request)))
                End If
                'crit.opAnd(New Criteria(GetType(ServiceBooking), "Status", MatchType.Exact, CInt(EnumStallMaster.StatusBooking.Booked)))
                crit.opAnd(New Criteria(GetType(ServiceBooking), "Dealer.ID", MatchType.No, objDealer.ID))
                If ViewState("Mode") = "Edit" Then
                    crit.opAnd(New Criteria(GetType(ServiceBooking), "ServiceBookingCode", MatchType.No, lblNoReservasi.Text))
                End If

                sort = New Sort(GetType(ServiceBooking), "ID", sort.SortDirection.DESC)
                sorts = New SortCollection
                sorts.Add(sort)

                arr = serviceBookingFacade.Retrieve(crit, sorts)
                IsThereServiceBooking(arr, lstBook)
            End If
        End If

        Return String.IsNullorEmpty(msgErr)
    End Function

    Private Sub LoadData(ByVal id As Integer)
        lblHeader.Text = "Stall - Detail Service Booking"

        Dim obj As ServiceBooking = serviceBookingFacade.Retrieve(id)
        hdID.Value = obj.ID
        hdStandardTime.Value = obj.StandardTime
        hdJenisService.Value = obj.StallServiceType
        txtNoChassis.Text = obj.ChassisNumber
        txtCatatan.Text = obj.Notes
        txtTipeKendaraan.Text = obj.VehicleTypeDescription
        txtIncomingPlan.Text = String.Format("{0} - {1} - {2}", obj.WorkingTimeStart.ToString("dd MMMM yyyy"), obj.WorkingTimeStart.ToString("HH:mm"), _
                                    obj.WorkingTimeEnd.ToString("HH:mm"))
        txtNamaKonsumen.Text = obj.CustomerName
        txtNoTelp.Text = obj.CustomerPhoneNumber
        txtPlatNomor.Text = obj.PlateNumber
        txtStallName.Text = obj.StallMaster.StallCodeDealer
        txtOdoMeter.Text = obj.OdoMeter

        ViewState("StatusSB") = obj.Status

        txtIncomingStart.Text = IIf(obj.IncomingDateStart.Year < 1900, "", obj.IncomingDateStart.ToString("HH:mm"))
        txtIncomingEnd.Text = IIf(obj.IncomingDateStart.Year < 1900, "", obj.IncomingDateEnd.ToString("HH:mm"))

        lblStatus.Text = String.Format("Status : {0}", stdFacade.GetByCategoryValue("ServiceBooking.Status", obj.Status).ValueDesc)
        lblNoReservasi.Text = obj.ServiceBookingCode
        InitDdl()


        'For Each item As ServiceBookingActivity In obj.ServiceBookingActivities
        '    If item.ServiceTypeID <> 0 Then
        '        ddlJnsKegiatan.Items.FindByValue(item.ServiceTypeID).Selected = True
        '        ddlJnsKegiatan_SelectedIndexChanged(ddlJnsKegiatan, Nothing)
        '        If Not String.IsNullorEmpty(item.KindCode) Then
        '            ddlJnsService.Items.FindByValue(item.KindCode).Selected = True
        '        End If
        '    End If
        'Next

        rbMitsubishi.Checked = obj.IsMitsubishi = 1
        rbNonMitsubishi.Checked = obj.IsMitsubishi = 0

        If rbMitsubishi.Checked Then
            If Not IsNothing(obj.ChassisMaster) Then
                GetHistory(obj.ChassisMaster)
            Else
                ResetHistory()
            End If

            ddlModelKendaraan.ClearSelection()
            ddlModelKendaraan.Items.FindByValue(obj.VechileModel.ID).Selected = True
            ddlModelKendaraan_SelectedIndexChanged(Nothing, Nothing)

            If Not IsNothing(obj.VechileType) Then
                ddlVehicleTypeCode.Items.FindByValue(obj.VechileType.ID).Selected = True
            Else
                ddlVehicleTypeCode.SelectedIndex = 0
            End If
        Else
            ResetHistory()
        End If

        ddlPickupType.Items.FindByValue(obj.PickupType).Selected = True

        SetControl(ViewState("Mode") <> "View")
        OnChangeJenisKendaraan()

        Dim ccRes As CustomerCaseResponse = obj.CustomerCaseResponse
        Dim isCCRes As Boolean = Not IsNothing(ccRes) Or Not IsNothing(Request.QueryString("CCID"))
        If isCCRes Then
            If Not IsNothing(ccRes) Then
                hdCCID.Value = ccRes.ID

                If ccRes.BookingDatetime.Year > 1900 Then
                    hdBookingTime.Value = ccRes.BookingDatetime
                ElseIf ccRes.CustomerCase.BookingDatetime.Year > 1900 Then
                    hdBookingTime.Value = ccRes.CustomerCase.BookingDatetime
                Else
                    hdBookingTime.Value = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                End If

                If ccRes.Response <> 0 And CInt(obj.Status) <> CInt(EnumStallMaster.StatusBooking.Batal) Then
                    ddlRespon.Items.FindByValue(ccRes.Response).Selected = True
                End If
                ViewState("CustomerCaseID") = ccRes.CustomerCase.ID
                ViewState("CustomerCaseRespID") = ccRes.ID
            End If
        End If

        Dim srFU As ServiceReminderFollowUp = obj.ServiceReminderFollowUp
        Dim isFU As Boolean = Not IsNothing(srFU) Or Not IsNothing(Request.QueryString("FUID"))
        If isFU Then
            If Not IsNothing(srFU) Then
                ViewState("FUID") = srFU.ID
            End If
        End If

        If Not IsNothing(obj.TrTrainee) Then
            ddlServiceAdvisor.ClearSelection()
            ddlServiceAdvisor.Items.FindByValue(obj.TrTrainee.ID).Selected = True
            'ddlServiceAdvisor_SelectedIndexChanged(Nothing, Nothing)
            ddlServiceAdvisor.SelectedValue = obj.TrTrainee.ID
        End If

        sessHelper.SetSession("SBActivity", obj.ServiceBookingActivities)
        BindDetail()
        btnCancel.Visible = ViewState("Mode") = "Edit"
        'If ViewState("StatusSB") = 2 Then
        '    sessHelper.SetSession("StatusSB", "Request")
        '    If ViewState("Mode") = "Edit" Then
        '        btnKonfirmasi.Visible = True
        '    End If
        'Else
        '    btnKonfirmasi.Visible = False
        'End If
    End Sub

    Private Sub ResetHistory()
        lblNoRangka.Text = ""
        lblModelInfo.Text = ""
        lblProgFS.Text = ""

        dgSC.DataSource = New ArrayList()
        dgSC.VirtualItemCount = 0
        dgSC.DataBind()

        dgPM.DataSource = New ArrayList()
        dgPM.VirtualItemCount = 0
        dgPM.DataBind()

        dgFS.DataSource = New ArrayList()
        dgFS.VirtualItemCount = 0
        dgFS.DataBind()

        sessHelper.RemoveSession("SBActivity")

        dgSBNew.DataSource = New ArrayList()
        dgSBNew.VirtualItemCount = 0
        dgSBNew.DataBind()

        dgCost.DataSource = New ArrayList()
        dgCost.VirtualItemCount = 0
        dgCost.DataBind()

        dgSparePart.DataSource = New ArrayList()
        dgSparePart.VirtualItemCount = 0
        dgSparePart.DataBind()

        ddlModelKendaraan.SelectedIndex = 0
        ddlServiceAdvisor.SelectedIndex = 0
    End Sub

    Private Sub ResetControl()
        ViewState("Mode") = "New"
        InitDdl()
        txtStallName.Attributes.Add("readonly", "true")
        txtIncomingPlan.Attributes.Add("readonly", "true")
        hdID.Value = "0"
        lblHeader.Text = "Stall - Input Service Booking"
        txtNoChassis.Text = ""
        txtCatatan.Text = ""
        txtIncomingPlan.Text = ""
        txtNamaKonsumen.Text = ""
        txtNoTelp.Text = ""
        txtPlatNomor.Text = ""
        txtStallName.Text = ""
        txtOdoMeter.Text = ""
        txtIncomingStart.Text = ""
        txtIncomingEnd.Text = ""
        txtTipeKendaraan.Text = ""
        lblStatus.Text = "Status : Baru"
        lblNoReservasi.Text = "[Auto Generate]"
        rbMitsubishi.Checked = True
        rbNonMitsubishi.Checked = False
        btnBaru.Text = IIf(String.IsNullorEmpty(ViewState("MenuFrom")), "Baru", "Kembali")
        btnCancel.Visible = False
        btnKonfirmasi.Visible = False
        trRespon.Visible = False

        OnChangeJenisKendaraan()
        SetControl(True)
    End Sub

    Private Sub SetControl(ByVal opt As Boolean)
        txtNoChassis.Enabled = opt
        txtCatatan.Enabled = opt
        txtNamaKonsumen.Enabled = opt
        txtNoTelp.Enabled = opt
        txtPlatNomor.Enabled = opt
        txtOdoMeter.Enabled = opt
        txtIncomingStart.Enabled = opt
        txtIncomingEnd.Enabled = opt
        txtTipeKendaraan.Enabled = opt
        ddlJnsKegiatan.Enabled = opt
        ddlJnsService.Enabled = opt
        ddlModelKendaraan.Enabled = opt
        ddlVehicleTypeCode.Enabled = opt
        ddlPickupType.Enabled = opt
        ddlRespon.Enabled = opt
        ddlServiceAdvisor.Enabled = opt
        btnSimpan.Enabled = opt
        btnGetInfoChassis.Visible = opt
        btnPopupPlan.Visible = opt
        rbMitsubishi.Enabled = opt
        rbNonMitsubishi.Enabled = opt
        dgSBNew.ShowFooter = opt
    End Sub

    Public Sub OnChangeJenisKendaraan()
        trMitsu1.Visible = rbMitsubishi.Checked
        trMitsu2.Visible = rbMitsubishi.Checked
        trNonMitsu1.Visible = rbNonMitsubishi.Checked
        btnGetInfoChassis.Visible = rbMitsubishi.Checked And ViewState("Mode") <> "View"
    End Sub

    Public Function ResultFSType() As String
        Dim fsType As String = String.Empty
        Dim fsTypeRet As String = String.Empty
        Dim dataSetResultFSType As New DataSet
        Dim dataSetResultFSTypeMSP As New DataSet
        Dim cri As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cri.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumFSKind"))

        Dim arrsc As ArrayList = New StandardCodeFacade(User).Retrieve(cri)

        dataSetResultFSTypeMSP = New FSKindOnVechileTypeFacade(User).RetrieveFromSP_MSP(txtNoChassis.Text)
        dataSetResultFSType = New FSKindOnVechileTypeFacade(User).RetrieveFromSP(txtNoChassis.Text)

        If Not IsNothing(dataSetResultFSType) AndAlso dataSetResultFSType.Tables.Count > 0 Then

            For Each dr As DataRow In dataSetResultFSType.Tables(0).Rows
                If Integer.Parse(dr.ItemArray.FirstOrDefault) < 4 Then ' lihat di standard code dengan category "EnumFSKIND"
                    If fsType = "" Then
                        fsType = CommonFunction.GetEnumDescription(Integer.Parse(dr.ItemArray.FirstOrDefault), "EnumFSKind")
                    Else
                        fsType += ", " + CommonFunction.GetEnumDescription(Integer.Parse(dr.ItemArray.FirstOrDefault), "EnumFSKind")
                    End If
                Else

                    If Not IsNothing(dataSetResultFSTypeMSP) AndAlso dataSetResultFSTypeMSP.Tables.Count > 0 Then
                        For Each drmsp As DataRow In dataSetResultFSTypeMSP.Tables(0).Rows
                            If fsType = "" Then
                                fsType = drmsp.ItemArray.FirstOrDefault
                            Else
                                fsType += ", Maintenance " + drmsp.ItemArray.FirstOrDefault
                            End If
                        Next
                    End If
                    Exit For
                End If
            Next
        Else
            If Not IsNothing(dataSetResultFSTypeMSP) AndAlso dataSetResultFSTypeMSP.Tables.Count > 0 Then
                For Each dr As DataRow In dataSetResultFSTypeMSP.Tables(0).Rows
                    If fsType = "" Then
                        fsType = dr.ItemArray.FirstOrDefault
                    Else
                        fsType += ", Maintenance " + dr.ItemArray.FirstOrDefault
                    End If
                Next
            End If
        End If

        Return fsType
    End Function

    Private Function MSPExtResult() As String
        Dim _return As String = String.Empty
        Dim arrReg As ArrayList = New MSPExRegistrationFacade(User).RetrieveArrChassisNumber(txtNoChassis.Text)
        If arrReg.Count > 0 Then
            For Each item As MSPExRegistration In arrReg
                If _return.Length > 0 Then
                    _return = _return & item.MSPExMaster.MSPExType.Description
                Else
                    _return = item.MSPExMaster.MSPExType.Description
                End If
            Next
        End If
        Return _return
    End Function

    Private Sub GetHistory(ByVal chassisMaster As ChassisMaster)
        lblNoRangka.Text = chassisMaster.ChassisNumber
        crit = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(VechileColor), "ID", MatchType.Exact, chassisMaster.VechileColor.ID))
        Dim vechileColl As ArrayList = vechileColorFacade.Retrieve(crit)
        Dim objVechileColor As VechileColor
        If vechileColl.Count > 0 Then
            objVechileColor = CType(vechileColl(0), VechileColor)
            lblModelInfo.Text = objVechileColor.MaterialNumber & " - " & objVechileColor.MaterialDescription
        End If

        Dim FSResult As String = ResultFSType()
        If FSResult.Length > 0 Then
            FSResult = FSResult & "; " & MSPExtResult()
        Else
            FSResult = MSPExtResult()
        End If
        lblProgFS.Text = FSResult

        ddlModelKendaraan.ClearSelection()
        If chassisMaster.VechileColor.VechileType.VechileModel.RowStatus = CShort(DBRowStatus.Active) Then
            ddlModelKendaraan.Items.FindByValue(chassisMaster.VechileColor.VechileType.VechileModel.ID).Selected = True
            ddlModelKendaraan_SelectedIndexChanged(Nothing, Nothing)

            ddlVehicleTypeCode.Items.FindByValue(chassisMaster.VechileColor.VechileType.ID).Selected = True
        End If



        RefreshGridSC()
        RefreshGridFS()
        RefreshGridPM()
    End Sub

    Private Sub RefreshGridSC(Optional indexPage As Integer = 0)
        crit = New CriteriaComposite(New Criteria(GetType(RecallService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(RecallService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtNoChassis.Text))

        Dim totalRow = 0
        Dim datas As ArrayList = recallServiceFacade.RetrieveActiveList(crit, indexPage + 1, dgSC.PageSize, totalRow, CType(ViewState("SCSortColumn"), String), CType(ViewState("SCSortDirection"), Sort.SortDirection))

        dgSC.DataSource = datas
        dgSC.VirtualItemCount = totalRow
        dgSC.DataBind()
    End Sub

    Private Sub RefreshGridFS(Optional indexPage As Integer = 0)
        crit = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtNoChassis.Text))

        Dim totalRow = 0
        Dim datas As ArrayList = freeServiceFacade.RetrieveActiveList(crit, indexPage + 1, dgFS.PageSize, totalRow, CType(ViewState("FSSortColumn"), String), CType(ViewState("FSSortDirection"), Sort.SortDirection))

        dgFS.DataSource = datas
        dgFS.VirtualItemCount = totalRow
        dgFS.DataBind()
    End Sub

    Private Sub RefreshGridPM(Optional indexPage As Integer = 0)
        crit = New CriteriaComposite(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, txtNoChassis.Text))

        Dim totalRow = 0
        Dim datas As ArrayList = pmHeaderFacade.RetrieveActiveList(crit, indexPage + 1, dgPM.PageSize, totalRow, CType(ViewState("PMSortColumn"), String), CType(ViewState("PMSortDirection"), Sort.SortDirection))

        dgPM.DataSource = datas
        dgPM.VirtualItemCount = totalRow
        dgPM.DataBind()
    End Sub

    Private Sub InitDdl()
        Dim results As ArrayList

        ddlModelKendaraan.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        results = vechileModelFacade.Retrieve(crit)

        With ddlModelKendaraan.Items
            For Each obj As VechileModel In results
                .Add(New ListItem(String.Format("{0} - {1}", obj.VechileModelCode, obj.IndDescription), obj.ID))
            Next
        End With

        ddlModelKendaraan.Items.Insert(0, "Silahkan Pilih")
        ddlModelKendaraan_SelectedIndexChanged(Nothing, Nothing)

        ddlJnsKegiatan.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))

        results = stdFacade.Retrieve(crit)

        With ddlJnsKegiatan.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueId))
            Next
        End With

        ddlJnsKegiatan.Items.Insert(0, "Silahkan Pilih")
        ddlJnsKegiatan_SelectedIndexChanged(Nothing, Nothing)

        ddlPickupType.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.PickupType"))

        results = stdFacade.Retrieve(crit)

        With ddlPickupType.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueId))
            Next
        End With

        ddlPickupType.Items.Insert(0, "Silahkan Pilih")

        ddlRespon.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.CustomerCase.Response"))

        results = stdFacade.Retrieve(crit)

        With ddlRespon.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueId))
            Next
        End With

        ddlRespon.Items.Insert(0, "Silahkan Pilih")

        ddlServiceAdvisor.Items.Clear()

        'crit = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crit.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPositionAreaID", MatchType.Exact, 2))
        'crit.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.Exact, 1))
        'crit.opAnd(New Criteria(GetType(TrTrainee), "JobPosition", MatchType.Exact, "SVC_SA"))
        'crit.opAnd(New Criteria(GetType(TrTrainee), "Dealer.ID", MatchType.Exact, objDealer.ID))
        'criterias = New CriteriaComposite(New Criteria(GetType(VWI_InvoiceRevision), "ChassisNumber", MatchType.Exact, _chassisMaster.ChassisNumber))
        'Dim invoiceList As List(Of VWI_InvoiceRevision) = New VWI_InvoiceRevisionFacade(User).RetrieveByCriteria(criterias).Cast(Of VWI_InvoiceRevision).ToList

        'results = New KTB.DNet.BusinessFacade.Training.TrTraineeFacade(User).Retrieve(crit)
        'view service advisor
        crit = New CriteriaComposite(New Criteria(GetType(VWI_ServiceAdvisor), "ID", MatchType.Greater, 0))
        If Not objDealer.DealerGroup Is Nothing Then
            crit.opAnd(New Criteria(GetType(VWI_ServiceAdvisor), "DealerCode", MatchType.Exact, objDealer.DealerCode))
        End If

        'crit = New CriteriaComposite(New Criteria(GetType(VWI_ServiceAdvisor), "DealerCode", MatchType.Exact, objDealer.DealerCode))

        'results = New KTB.DNet.BusinessFacade.Training.VWI_ServiceAdvisorFacade(User).Retrieve(crit)

        Dim ServiceAdvisor As List(Of VWI_ServiceAdvisor) = New KTB.DNet.BusinessFacade.Training.VWI_ServiceAdvisorFacade(User).RetrieveByCriteria(crit).Cast(Of VWI_ServiceAdvisor).ToList

        If ServiceAdvisor.Count > 0 Then
            'Dim obj As ServiceBooking = serviceBookingFacade.Retrieve(serviceBookingID)


            With ddlServiceAdvisor.Items
                For Each obj As VWI_ServiceAdvisor In ServiceAdvisor
                    .Add(New ListItem(obj.ID & " - " & obj.Name, obj.ID))
                Next
            End With

        End If


        ddlServiceAdvisor.Items.Insert(0, "Silahkan Pilih")

    End Sub

    Private Sub OnChangeJenisKegiatan(ByVal serviceType As String)
        Dim stdCode As StandardCode = stdFacade.GetByCategoryValueCode("ServiceBooking.ServiceType", serviceType)
        Dim objSBA As New ServiceBookingActivity
        Dim objSBAS As New ArrayList
        If Not IsNothing(stdCode) Then
            'objSBA.ServiceTypeID = stdCode.ValueId
            'objSBAS.Add(objSBA)
            'sessHelper.SetSession("SBActivity", objSBAS)
            'BindDetail()
            ddlJnsKegiatan.Items.FindByValue(stdCode.ValueId).Selected = True
            ddlJnsKegiatan_SelectedIndexChanged(ddlJnsKegiatan, Nothing)
            ViewState("ServiceType") = stdCode.ValueId
        End If
    End Sub

    Private Sub LoadDataCustomerResponse(ByVal ccRes As CustomerCaseResponse)
        If ccRes.BookingDatetime.Year > 1900 Then
            hdBookingTime.Value = ccRes.BookingDatetime
        ElseIf ccRes.CustomerCase.BookingDatetime.Year > 1900 Then
            hdBookingTime.Value = ccRes.CustomerCase.BookingDatetime
        Else
            hdBookingTime.Value = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        End If

        txtCatatan.Text = ccRes.CustomerCase.Description
        hdCCID.Value = ccRes.ID

        ViewState("CustomerCaseID") = ccRes.CustomerCase.ID
        ViewState("CustomerCaseRespID") = ccRes.ID
        OnChangeJenisKegiatan(ccRes.CustomerCase.ServiceType)
        'If Not IsNothing(ccRes.CustomerCase.ServiceType) Then
        '    ViewState("ServiceType") = ""
        'End If
        'ddlJK_PreRender()
    End Sub

    Private Sub UpdateDescriptionCustomerResponse(ByVal serviceBookingID As Integer, ByVal ccRes As CustomerCaseResponse)
        Dim obj As ServiceBooking = serviceBookingFacade.Retrieve(serviceBookingID)
        ccRes.Description = String.Format("Telah dilakukan service booking dengan nomer booking {0}", obj.ServiceBookingCode)
        ccResFacade.Update(ccRes)
    End Sub

    Private Sub SentToSalesForce(ByVal ccRes As CustomerCaseResponse)
        Dim sf As SalesForceInterface = New SalesForceInterface()
        Dim msg As String = String.Empty
        Dim vSFreturn As Boolean = False
        Dim arr = New ArrayList
        Dim nResult As Integer

        vSFreturn = sf.UpdateCase(ccRes)
        If vSFreturn Then
            'Update IsSend
            ccRes.IsSend = 1 'sent done
            nResult = New CustomerCaseResponseFacade(User).Update(ccRes)
        Else
            Dim _criterias As New CriteriaComposite(New Criteria(GetType(WsLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            _criterias.opAnd(New Criteria(GetType(WsLog), "Source", MatchType.Exact, "Internal"))
            _criterias.opAnd(New Criteria(GetType(WsLog), "Status", MatchType.Exact, "False"))
            _criterias.opAnd(New Criteria(GetType(WsLog), "Body", MatchType.[Partial], "SALESFORCEIUPDATECASE"))
            _criterias.opAnd(New Criteria(GetType(WsLog), "Body", MatchType.[Partial], ccRes.CustomerCase.SalesforceID))
            _criterias.opAnd(New Criteria(GetType(WsLog), "CreatedTime", MatchType.GreaterOrEqual, DateTime.Now.AddMinutes(-1)))

            Dim objWsLog As WsLog
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(WsLog), "CreatedTime", Sort.SortDirection.ASC))
            arr = New WsLogFacade(User).Retrieve(_criterias, sortColl)
            If arr.Count > 0 Then
                objWsLog = CType(arr(arr.Count - 1), WsLog)
                If (objWsLog.Message.Trim() = String.Empty Or IsDBNull(objWsLog.Message)) Then
                    'Update IsSend
                    ccRes.IsSend = 1 'sent
                    nResult = New CustomerCaseResponseFacade(User).Update(ccRes)
                End If
            End If
        End If
    End Sub

    Private Function IsThereServiceBooking(ByVal arr As ArrayList, ByRef lstBook As List(Of ServiceBooking)) As Boolean

        Dim isThere As Boolean = False
        Dim appConf As AppConfig = appConfFacade.Retrieve("ServiceBooking.AvailableDayEdit")
        Dim objSBAS As ArrayList = CType(sessHelper.GetSession("SBActivity"), ArrayList)
        For Each svcBook As ServiceBooking In arr
            For Each svcActivitiy As ServiceBookingActivity In svcBook.ServiceBookingActivities
                Dim totalDays As Integer = svcBook.WorkingTimeStart.Date.Subtract(DateTime.Now.Date).Days()
                Dim cutOff As Integer = CInt(appConf.Value)
                For Each obj As ServiceBookingActivity In objSBAS
                    'If totalDays >= cutOff And ddlJnsKegiatan.SelectedValue = svcActivitiy.ServiceTypeID Then
                    If totalDays >= cutOff And obj.ServiceTypeID = svcActivitiy.ServiceTypeID Then
                        lstBook.Add(svcBook)
                        isThere = True
                        Exit For
                    End If
                Next
            Next

            If isThere Then
                Exit For
            End If
        Next

        Return isThere
    End Function
#End Region

    Protected Sub ddlServiceAdvisor_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub BindDetail()
        dgSBNew.DataSource = CType(sessHelper.GetSession("SBActivity"), ArrayList)
        dgSBNew.DataBind()
    End Sub
    Private Function GetStdCodeDesc(ByVal Category As String, ByVal ValueID As Integer) As String
        Dim nResult As String = ""
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, Category))
        criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, ValueID))
        Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
        Dim objStandardCode As New StandardCode
        If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
            objStandardCode = CType(arrDDL(0), StandardCode)
            nResult = objStandardCode.ValueDesc
        End If
        Return nResult
    End Function
    Private Function GetKindCode(ByVal JenisKegiatan As Integer, ByVal JenisService As String) As String
        Dim nResult As String = ""
        Select Case JenisKegiatan

            Case 1
                Dim objFSKind As New FSKind
                crit = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(FSKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = fSKindFacade.Retrieve(crit)
                If results.Count > 0 Then
                    objFSKind = CType(results(0), FSKind)
                    nResult = objFSKind.KindDescription
                End If

            Case 2
                Dim objPMKind As New PMKind
                crit = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(PMKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = pMKindFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objPMKind = CType(results(0), PMKind)
                    nResult = objPMKind.KindDescription
                End If
            Case 3
                Dim objRC As New RecallCategory
                crit = New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(RecallCategory), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = recallCategoryFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objRC = CType(results(0), RecallCategory)
                    nResult = objRC.Description
                End If
            Case 4
                Dim objGRKind As New GRKind
                crit = New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(GRKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = gRKindFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objGRKind = CType(results(0), GRKind)
                    nResult = objGRKind.KindDescription
                End If
        End Select
        Return nResult
    End Function
    Private Function GetKindCodeByID(ByVal JenisKegiatan As Integer, ByVal JenisService As String) As String
        Dim nResult As String = ""
        Select Case JenisKegiatan

            Case 1
                Dim objFSKind As New FSKind
                crit = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(FSKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = fSKindFacade.Retrieve(crit)
                If results.Count > 0 Then
                    objFSKind = CType(results(0), FSKind)
                    nResult = objFSKind.KindCode
                End If

            Case 2
                Dim objPMKind As New PMKind
                crit = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(PMKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = pMKindFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objPMKind = CType(results(0), PMKind)
                    nResult = objPMKind.KindCode
                End If
            Case 3
                Dim objRC As New RecallCategory
                crit = New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(RecallCategory), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = recallCategoryFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objRC = CType(results(0), RecallCategory)
                    nResult = objRC.RecallRegNo
                End If
            Case 4
                Dim objGRKind As New GRKind
                crit = New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(GRKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = gRKindFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objGRKind = CType(results(0), GRKind)
                    nResult = objGRKind.KindCode
                End If
        End Select
        Return nResult
    End Function
    Private Function GetVechileTypeCodeByID(ByVal ID As Integer) As String
        Dim nResult As String = ""
        Dim objVechileTypeCode As New VechileType
        crit = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(VechileType), "ID", MatchType.Exact, ID))
        Dim results As ArrayList = vechileTypeFacade.Retrieve(crit)
        If results.Count > 0 Then
            objVechileTypeCode = CType(results(0), VechileType)
            nResult = objVechileTypeCode.VechileTypeCode
        End If

        Return nResult
    End Function

    Protected Sub ddlJK_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlJK As DropDownList = sender
        'If ddlJK.SelectedIndex > 0 Then
        Dim row As DataGridItem = CType(ddlJK.NamingContainer, DataGridItem)
        Dim ddlJS As DropDownList = CType(row.FindControl("ddlJS"), DropDownList)

        Dim arrList As ArrayList = CType(sessHelper.GetSession("JenisKegiatan"), ArrayList)
        If arrList.Count > 0 Then
            RebuildDDLJenisService(ddlJS, CInt(ddlJK.SelectedValue))
            ddlJS.Focus()
        End If
        'End If
    End Sub

    Protected Sub ddlJKEdt_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlJK As DropDownList = sender
        'If ddlJK.SelectedIndex > 0 Then
        Dim row As DataGridItem = CType(ddlJK.NamingContainer, DataGridItem)
        Dim ddlJS As DropDownList = CType(row.FindControl("ddlJSEdit"), DropDownList)

        Dim arrList As ArrayList = CType(sessHelper.GetSession("JenisKegiatan"), ArrayList)
        If arrList.Count > 0 Then
            RebuildDDLJenisService(ddlJS, CInt(ddlJK.SelectedValue))
            ddlJS.Focus()
        End If
        'End If
    End Sub

    Protected Sub ddlJK_PreRender(sender As Object, e As EventArgs)
        Dim results = New ArrayList
        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))
        results = stdFacade.Retrieve(crit)
        sessHelper.SetSession("JenisKegiatan", results)

        Dim ddlJK As DropDownList = sender
        If ddlJK.Items.Count = 0 Then
            ddlJK.Items.Clear()
            'If Not sessHelper.GetSession("KategoriSPK") Is Nothing Then
            With ddlJK.Items
                For Each obj As StandardCode In results
                    .Add(New ListItem(obj.ValueDesc, obj.ValueId))
                Next
            End With
            'End If
        End If
        'ddlJK.Items.Insert(0, "Silahkan Pilih")
        'ddlJK_SelectedIndexChanged(Nothing, Nothing)

        If Not IsNothing(ViewState("ServiceType")) Then
            ddlJK.Items.FindByValue(ViewState("ServiceType")).Selected = True
            ddlJK_SelectedIndexChanged(ddlJK, Nothing)
            'ViewState("ServiceType2") = ViewState("ServiceType")
            ViewState.Remove("ServiceType")
            'ViewState.
        End If

    End Sub

    Protected Sub ddlJS_PreRender(sender As Object, e As EventArgs)
        Dim ddlJS As DropDownList = sender
        Dim row As DataGridItem = CType(ddlJS.NamingContainer, DataGridItem)
        Dim ddlJK As DropDownList = CType(row.FindControl("ddlJK"), DropDownList)

        Dim arrList As ArrayList = CType(sessHelper.GetSession("JenisKegiatan"), ArrayList)
        If arrList.Count > 0 Then
            RebuildDDLJenisService(ddlJS, CInt(ddlJK.SelectedValue))
        End If
    End Sub

    Private Sub RebuildDDLJenisService(ByVal ddl As DropDownList, ByVal JKID As Integer)
        Select Case JKID
            Case 1
                crit = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim results As ArrayList = fSKindFacade.Retrieve(crit)
                ddl.Items.Clear()
                With ddl.Items
                    For Each obj As FSKind In results
                        .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
                    Next
                End With
            Case 2
                crit = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim results As ArrayList = pMKindFacade.Retrieve(crit)
                ddl.Items.Clear()
                With ddl.Items
                    For Each obj As PMKind In results
                        .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
                    Next
                End With
            Case 3
                crit = New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim results As ArrayList = recallCategoryFacade.Retrieve(crit)
                ddl.Items.Clear()
                With ddl.Items
                    For Each obj As RecallCategory In results
                        .Add(New ListItem(String.Format("{0} - {1}", obj.RecallRegNo, obj.Description), obj.ID))
                    Next
                End With
            Case 4
                crit = New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim results As ArrayList = gRKindFacade.Retrieve(crit)
                ddl.Items.Clear()
                With ddl.Items
                    For Each obj As GRKind In results
                        .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
                    Next
                End With
        End Select

        ddl.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub RebuildDDLJK(ByVal ddl As DropDownList)
        Dim results = New ArrayList
        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))
        results = stdFacade.Retrieve(crit)
        sessHelper.SetSession("JenisKegiatan", results)

        Dim ddlJK As DropDownList = ddl
        If ddlJK.Items.Count = 0 Then
            ddlJK.Items.Clear()
            'If Not sessHelper.GetSession("KategoriSPK") Is Nothing Then
            With ddlJK.Items
                For Each obj As StandardCode In results
                    .Add(New ListItem(obj.ValueDesc, obj.ValueId))
                Next
            End With
            'End If
        End If
    End Sub

    Protected Sub dgSBNew_ItemCommand(source As Object, e As DataGridCommandEventArgs)
        Dim objSBAS As ArrayList
        Dim hdID As HiddenField = CType(e.Item.FindControl("hdID"), HiddenField)
        Dim servicePattern As String = String.Format("{0},{1}", CType(EnumStallMaster.ServiceType.FS, Integer), CType(EnumStallMaster.ServiceType.PM, Integer))

        If (CType(sessHelper.GetSession("SBActivity"), ArrayList) Is Nothing) Then
            objSBAS = New ArrayList
        Else
            objSBAS = CType(sessHelper.GetSession("SBActivity"), ArrayList)
        End If

        btnSimpan.Enabled = True
        btnCancel.Enabled = True
        btnKonfirmasi.Enabled = True

        Select Case e.CommandName
            Case "Add" 'Add datagrid item 

                Dim ddlJK As DropDownList = CType(e.Item.FindControl("ddlJK"), DropDownList)
                Dim ddlJS As DropDownList = CType(e.Item.FindControl("ddlJS"), DropDownList)

                If (ddlJK.SelectedIndex = -1) Then
                    MessageBox.Show("Jenis Kegiatan harus pilih")
                    Return
                End If

                If (ddlJS.SelectedIndex = 0) Then
                    MessageBox.Show("Jenis Service harus pilih")
                    Return
                End If

                If objSBAS.Count > 0 Then
                    Dim activityList As List(Of ServiceBookingActivity) = objSBAS.Cast(Of ServiceBookingActivity).ToList
                    If activityList.Where(Function(x) servicePattern.Contains(x.ServiceTypeID)).Count > 0 And servicePattern.Contains(ddlJK.SelectedValue) Then
                        If activityList.Where(Function(x) x.ServiceTypeID = ddlJK.SelectedValue).Count > 0 Then
                            MessageBox.Show("Jenis Kegiatan Free Service atau Jenis Kegiatan Periodical Maintenance tidak boleh lebih dari 1")
                            Return
                        Else
                            MessageBox.Show("Jenis Kegiatan Free Serviced dan Jenis Kegiatan Periodical Maintenance tidak boleh dipilih secara bersamaan")
                            Return
                        End If
                    ElseIf activityList.Where(Function(x) x.ServiceTypeID = ddlJK.SelectedValue And x.KindCode = ddlJS.SelectedValue).Count > 0 Then
                        MessageBox.Show("Jenis Service " & ddlJS.SelectedItem.ToString() & " sudah ada, silahkan input Jenis Service yang lain")
                        Return
                    End If
                End If

                Dim objSBA As New ServiceBookingActivity

                'If objSBAS.Count > 0 Then
                '    objSBA.ID = 0
                'Else
                '    objSBA.ID = objSBAS.Count + 1
                'End If

                objSBA.ServiceTypeID = ddlJK.SelectedValue
                objSBA.KindCode = ddlJS.SelectedValue

                objSBAS.Add(objSBA)
                'BindCost(objSBAS)
            Case "save"
                Dim ddlJK As DropDownList = CType(e.Item.FindControl("ddlJKEdt"), DropDownList)
                Dim ddlJS As DropDownList = CType(e.Item.FindControl("ddlJSEdit"), DropDownList)

                If (ddlJK.SelectedIndex = -1) Then
                    MessageBox.Show("Jenis Kegiatan harus pilih")
                    Return
                End If

                If (ddlJS.SelectedIndex = 0) Then
                    MessageBox.Show("Jenis Service harus pilih")
                    Return
                End If

                Dim activityList As List(Of ServiceBookingActivity) = objSBAS.Cast(Of ServiceBookingActivity).ToList
                If activityList.Where(Function(x) x.ServiceTypeID = ddlJK.SelectedValue And x.ID <> hdID.Value And servicePattern.Contains(ddlJK.SelectedValue)).Count > 0 Then
                    MessageBox.Show("Jenis Kegiatan Free Service atau Periodical Maintenance tidak boleh lebih dari 1")
                    Return
                ElseIf activityList.Where(Function(x) x.ServiceTypeID = ddlJK.SelectedValue And x.KindCode = ddlJS.SelectedValue And x.ID <> hdID.Value).Count > 0 Then
                    MessageBox.Show("Jenis Service " & ddlJS.SelectedItem.Text.ToString() & " sudah ada, silahkan input Jenis Service yang lain")
                    Return
                End If

                For Each objSBA As ServiceBookingActivity In activityList
                    If objSBA.ID = CType(hdID.Value, Integer) Then
                        objSBA.ServiceTypeID = ddlJK.SelectedValue
                        objSBA.KindCode = ddlJS.SelectedValue
                        Exit For
                    End If
                Next

                dgSBNew.EditItemIndex = -1
                dgSBNew.ShowFooter = True

                'BindCost(objSBAS)
            Case "cancel"
                dgSBNew.EditItemIndex = -1
                dgSBNew.ShowFooter = True
            Case "edit"
                dgSBNew.EditItemIndex = e.Item.ItemIndex
                dgSBNew.ShowFooter = False
                btnSimpan.Enabled = False
                btnCancel.Enabled = False
                btnKonfirmasi.Enabled = False
            Case "Delete" 'Delete this datagrid item 
                objSBAS.RemoveAt(e.Item.ItemIndex)
                'BindCost(objSBAS)
        End Select

        If hdChangeVechile.Value <> "" Then
            If txtIncomingPlan.Text <> "" Or txtStallName.Text <> "" Then
                txtIncomingPlan.Text = ""
                txtStallName.Text = ""
            End If
        End If

        sessHelper.SetSession("SBActivity", objSBAS)
        BindDetail()
    End Sub

    Protected Sub dgSBNew_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        Dim RowValue As ServiceBookingActivity = CType(e.Item.DataItem, ServiceBookingActivity)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim hdID As HiddenField = CType(e.Item.FindControl("hdID"), HiddenField)

        'If e.Item.ItemType = ListItemType.Footer Then
        '    Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchJenisKegiatanFooter"), Label)
        '    lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpMultipleService.aspx", "", 400, 400, "GetSelectedChassisCode")
        'End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            'e.Item.Cells(0).Controls.Add(lNum)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
            If ViewState("Mode") = "View" Then
                lnkbtnDelete.Visible = False
                If e.Item.ItemType = ListItemType.Footer Then
                    e.Item.Visible = False
                End If
            End If
            'Dim hdDGID As HiddenField = CType(e.Item.FindControl("hdDGID"), HiddenField)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgSBNew.PageSize * dgSBNew.CurrentPageIndex)
            Dim lblJenisKegiatan As Label = CType(e.Item.FindControl("lblJenisKegiatan"), Label)
            Dim lblJenisService As Label = CType(e.Item.FindControl("lblJenisService"), Label)
            'If lblJenisService.Text <> "" Then
            lblJenisService.Text = GetKindCode(lblJenisKegiatan.Text, lblJenisService.Text)
            'End If
            lblJenisKegiatan.Text = GetStdCodeDesc("ServiceBooking.ServiceType", lblJenisKegiatan.Text)

        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim ddlJKEdt As DropDownList = CType(e.Item.FindControl("ddlJKEdt"), DropDownList)
            Dim ddlJSEdit As DropDownList = CType(e.Item.FindControl("ddlJSEdit"), DropDownList)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
            If ViewState("Mode") = "View" Then
                lnkbtnDelete.Visible = False
                If e.Item.ItemType = ListItemType.Footer Then
                    e.Item.Visible = False
                End If
            End If

            RebuildDDLJK(ddlJKEdt)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgSBNew.PageSize * dgSBNew.CurrentPageIndex)
            ddlJKEdt.Items.FindByValue(RowValue.ServiceTypeID).Selected = True
            ddlJKEdt_SelectedIndexChanged(ddlJKEdt, Nothing)
            ddlJSEdit.Items.FindByValue(RowValue.KindCode).Selected = True

            hdID.Value = RowValue.ID
        End If
    End Sub

    Protected Sub dgCost_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        Dim RowValue As VWI_ServiceCostEstimation = CType(e.Item.DataItem, VWI_ServiceCostEstimation)
        Dim lblSubtotal As Label = CType(e.Item.FindControl("lblSubtotal"), Label)
        Dim lblGrandTotal As Label = CType(e.Item.FindControl("lblGrandTotal"), Label)
        'Dim lblTglPro As Label = CType(e.Item.FindControl("lblTglPro"), Label)
        'GrandTotal = 0
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgCost.PageSize * dgCost.CurrentPageIndex)
            GrandTotal = GrandTotal + RowValue.JasaService
            'If Not IsNothing(RowValue.JenisKegiatan) Then
            '    GrandTotal = GrandTotal + RowValue.JasaService
            '    lblGrandTotal.Text = GrandTotal
            '    '    If RowValue.ServiceDate <= "1/1/1900" Then
            '    '        lblTglPro.Text = ""
            '    '    Else
            '    '        lblTglPro.Text = Format(RowValue.CreatedTime, "dd/MM/yyyy")
            '    '    End If
            'End If
            'e.
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim rowTotal As Decimal =
            '    Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"))
            '    grdTotal = grdTotal + rowTotal
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim lbl As Label = DirectCast(e.Row.FindControl("lblTotal"), Label)
            '    lbl.Text = grdTotal.ToString("N2")
            'End If

        End If
        If e.Item.ItemType = ListItemType.Footer Then
            lblGrandTotal.Text = GrandTotal
        End If
    End Sub

    Protected Sub btnCost_Click(sender As Object, e As EventArgs)
        Dim objSBA As ArrayList = CType(sessHelper.GetSession("SBActivity"), ArrayList)
        'Dim objTSBA As New TempServiceBookingActivity
        Dim VechileTypeCode As String = GetVechileTypeCodeByID(IIf(ddlVehicleTypeCode.SelectedIndex = 0, "0", ddlVehicleTypeCode.SelectedValue))
        Dim result As Integer
        'objTSBA.DealerCode = objDealer.DealerCode
        'objTSBA.VechileTypeCode = VechileTypeCode
        'objTSBA.ChassisNumber = txtNoChassis.Text
        'TSBAFacade.Delete(objTSBA)

        If (rbMitsubishi.Checked = True) Then
            If (ddlModelKendaraan.SelectedIndex = 0) Or (ddlVehicleTypeCode.SelectedIndex = 0) Then
                MessageBox.Show("Pilih Tipe Kendaraan dan Model Kendaraan terlebih dahulu.")
                Return
            End If
        End If

        If Not IsNothing(objSBA) Then
            'For Each obj As ServiceBookingActivity In objSBA
            '    objTSBA.DealerCode = objDealer.DealerCode
            '    objTSBA.VechileTypeCode = VechileTypeCode
            '    objTSBA.ChassisNumber = txtNoChassis.Text
            '    objTSBA.JenisKegiatan = obj.ServiceTypeID
            '    objTSBA.JenisService = obj.KindCode
            '    result = TSBAFacade.Insert(objTSBA)
            'Next
            'BindCost(objSBA)
            'trCost.Visible = True
            'trCost2.Visible = True
            RegisterStartupScript("Open", String.Format("<script>ShowPPCostEstimation({0},{1},'{2}');</script>", _
                    objDealer.DealerCode,
                    IIf(ddlVehicleTypeCode.SelectedIndex = 0, "0", ddlVehicleTypeCode.SelectedValue),
                    txtNoChassis.Text))
            Return
        Else
            MessageBox.Show("Pilih Jenis Kegiatan dan Jenis Service terlebih dahulu.")
        End If
    End Sub

    Private Sub BindCost(ByVal objSBA As ArrayList)
        Dim CostEstimation As List(Of VWI_ServiceCostEstimation)
        Dim objCostList = New ArrayList
        Dim objCost = New ArrayList
        Dim tbFinalObject As DataTable
        Dim DTFinal As New DataTable

        If objSBA.Count > 0 Then
            For Each obj As ServiceBookingActivity In objSBA
                Dim KindCode As String = GetKindCodeByID(obj.ServiceTypeID, obj.KindCode)
                Dim VechileTypeCode As String = GetVechileTypeCodeByID(ddlVehicleTypeCode.SelectedValue)
                crit = New CriteriaComposite(New Criteria(GetType(VWI_ServiceCostEstimation), "ID", MatchType.Greater, 0))
                crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "DealerCode", MatchType.Exact, objDealer.DealerCode))
                crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "VechileTypeCode", MatchType.Exact, VechileTypeCode))
                crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "ServiceType", MatchType.Exact, obj.ServiceTypeID))
                crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "KindCode", MatchType.Exact, KindCode))
                crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "Varian", MatchType.Exact, VechileTypeCode.Substring(0, 2)))

                CostEstimation = New KTB.DNet.BusinessFacade.Service.VWI_ServiceCostEstimationFacade(User).RetrieveByCriteria(crit).Cast(Of VWI_ServiceCostEstimation).ToList
                If CostEstimation.Count > 0 Then
                    Dim objCosts As New VWI_ServiceCostEstimation
                    For Each objs As VWI_ServiceCostEstimation In CostEstimation
                        '.Add(New ListItem(obj.ID & " - " & obj.Name, obj.ID))
                        objCosts.JenisKegiatan = objs.JenisKegiatan
                        objCosts.JenisService = objs.JenisService
                        objCosts.JasaService = objs.JasaService
                        tbFinalObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(objs.Details)
                        If Not IsNothing(tbFinalObject) Then
                            'tbFinalObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(objs.Details)
                            DTFinal.Merge(tbFinalObject)
                        End If
                        'For Each dr As DataRow In tbFinalObject.Rows
                        '    objCostList.add(dr)
                        'Next
                        objCost.add(objCosts)

                    Next
                End If
            Next
            'For Each obj As ServiceBookingActivity In objSBA
            '    Dim KindCode As String = GetKindCodeByID(obj.ServiceTypeID, obj.KindCode)
            '    Dim VechileTypeCode As String = GetVechileTypeCodeByID(ddlVehicleTypeCode.SelectedValue)
            '    crit = New CriteriaComposite(New Criteria(GetType(VWI_ServiceCostEstimation), "DealerCode", MatchType.Exact, objDealer.DealerCode))
            '    crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "ServiceType", MatchType.Exact, obj.ServiceTypeID))
            '    'crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "KindCode", MatchType.Exact, obj.KindCode))
            '    'crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "VechileTypeCode", MatchType.Exact, ddlVehicleTypeCode.SelectedValue))
            '    crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "KindCode", MatchType.Exact, KindCode))
            '    'If obj.ServiceTypeID = 1 Then
            '    crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "VechileTypeCode", MatchType.Exact, VechileTypeCode))
            '    'End If
            '    CostEstimation = New KTB.DNet.BusinessFacade.Service.VWI_ServiceCostEstimationFacade(User).RetrieveByCriteria(crit).Cast(Of VWI_ServiceCostEstimation).ToList
            '    If CostEstimation.Count > 0 Then
            '        Dim objCosts As New VWI_ServiceCostEstimation
            '        For Each objs As VWI_ServiceCostEstimation In CostEstimation
            '            '.Add(New ListItem(obj.ID & " - " & obj.Name, obj.ID))
            '            objCosts.JenisKegiatan = objs.JenisKegiatan
            '            objCosts.JenisService = objs.JenisService
            '            objCosts.JasaService = objs.JasaService
            '            tbFinalObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(objs.Details)
            '            DTFinal.Merge(tbFinalObject)
            '            'For Each dr As DataRow In tbFinalObject.Rows
            '            '    objCostList.add(dr)
            '            'Next
            '            objCost.add(objCosts)
            '        Next
            '    End If
            'Next
        End If
        GrandTotal = 0
        dgCost.DataSource = objCost
        dgCost.DataBind()
        GrandTotal2 = 0
        dgSparePart.DataSource = DTFinal
        dgSparePart.DataBind()
        ''dgCost.DataSource = CType(sessHelper.GetSession("SBActivity"), ArrayList)
        'dgCost.DataBind()
    End Sub

    Protected Sub dgSparePart_ItemCommand(source As Object, e As DataGridCommandEventArgs)

    End Sub

    Protected Sub dgSparePart_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        Dim lblSubtotal As Label = CType(e.Item.FindControl("lblSubtotal"), Label)
        Dim lblGrandTotal As Label = CType(e.Item.FindControl("lblGrandTotal"), Label)
        'Dim lblTglPro As Label = CType(e.Item.FindControl("lblTglPro"), Label)
        'GrandTotal2 = 0
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgCost.PageSize * dgCost.CurrentPageIndex)
            GrandTotal2 = GrandTotal2 + CDec(lblSubtotal.Text)
            'If Not IsNothing(RowValue.JenisKegiatan) Then
            '    GrandTotal = GrandTotal + RowValue.JasaService
            '    lblGrandTotal.Text = GrandTotal
            '    '    If RowValue.ServiceDate <= "1/1/1900" Then
            '    '        lblTglPro.Text = ""
            '    '    Else
            '    '        lblTglPro.Text = Format(RowValue.CreatedTime, "dd/MM/yyyy")
            '    '    End If
            'End If
            'e.
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim rowTotal As Decimal =
            '    Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"))
            '    grdTotal = grdTotal + rowTotal
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim lbl As Label = DirectCast(e.Row.FindControl("lblTotal"), Label)
            '    lbl.Text = grdTotal.ToString("N2")
            'End If

        End If
        If e.Item.ItemType = ListItemType.Footer Then
            lblGrandTotal.Text = GrandTotal2
        End If
    End Sub

    Protected Sub btnKonfirmasi_Click(sender As Object, e As EventArgs)
        Dim msgErr As String = String.Empty
        Dim lstBook As List(Of ServiceBooking) = New List(Of ServiceBooking)

        If Not Validate(msgErr, lstBook) Then
            MessageBox.Show(msgErr)
            Exit Sub
        End If

        Dim confMsg As String = String.Empty
        If hdConfirm.Value = "-1" And lstBook.Count > 0 Then
            Dim svcBook As ServiceBooking = CType(lstBook(0), ServiceBooking)
            Dim svcActivity As ServiceBookingActivity = CType(svcBook.ServiceBookingActivities(0), ServiceBookingActivity)
            confMsg = _
                String.Format("Kendaraan tersebut sudah melakukan service booking untuk tanggal {0} dengan tipe Service {1}, Apakah Anda yakin tetap ingin menyimpan data transaksi ini?", _
                              svcBook.WorkingTimeStart.ToString("dd MMMM yyyy HH:mm"), stdFacade.GetByCategoryValue("ServiceBooking.ServiceType", svcActivity.ServiceTypeID.ToString).ValueDesc)
            RegisterStartupScript("Confirm", String.Format("<script>ShowConfirm('{0}', 'btnKonfirmasi');</script>", confMsg))
            Return
        ElseIf hdConfirm.Value = "-1" And lstBook.Count = 0 Then
            confMsg = "Apakah Anda yakin ingin melakukan konfirmasi?"
            RegisterStartupScript("Confirm", String.Format("<script>ShowConfirm('{0}', 'btnKonfirmasi');</script>", confMsg))
            Return
        Else
            hdConfirm.Value = "-1"
        End If
        Dim obj As ServiceBooking = New ServiceBooking
        Dim activities As ArrayList = New ArrayList

        ViewState("Konfirmasi") = True
        PopulateData(obj, activities)
        Dim result As Integer = 0
        result = serviceBookingFacade.Update(obj, activities)

        If result > 0 Then
            MessageBox.Show("Konfirmasi berhasil.")
            ViewState("Mode") = "Edit"
            ViewState("Konfirmasi") = False
            LoadData(result)
        Else
            MessageBox.Show("Konfirmasi gagal.")
        End If
    End Sub

    Protected Sub ddlVehicleTypeCode_SelectedIndexChanged(sender As Object, e As EventArgs)
       If hdChangeVechile.Value <> "" Then
            If txtIncomingPlan.Text <> "" Or txtStallName.Text <> "" Then
                txtIncomingPlan.Text = ""
                txtStallName.Text = ""
            End If
        End If
    End Sub
End Class