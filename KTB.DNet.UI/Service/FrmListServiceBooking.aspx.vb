Imports System.Collections.Generic
Imports System.Linq
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessValidation
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports OfficeOpenXml
Imports GlobalExtensions
#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Reflection

#End Region
Public Class FrmListServiceBooking
    Inherits System.Web.UI.Page


    Private sessHelper As SessionHelper = New SessionHelper
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bListPrivilege As Boolean = False
    Private stdFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private vechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Private fSKindFacade As FSKindFacade = New FSKindFacade(User)
    Private pMKindFacade As PMKindFacade = New PMKindFacade(User)
    Private recallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    Private vechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
    Private chassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
    Private dealerFacade As DealerFacade = New DealerFacade(User)
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private isDealerPiloting As Boolean = False
    Dim isDealerDMS As Boolean = False

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        isDealerDMS = objDealer.IsDealerDMS
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        CheckPrivilege()
        If Not IsPostBack Then
            crit = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, "ServiceBooking.AvailableDayEdit"))
            Dim arrLst As ArrayList = New AppConfigFacade(User).Retrieve(crit)

            If arrLst.Count > 0 Then
                Dim appconf As AppConfig = CType(arrLst(0), AppConfig)
                ViewState("CutOff") = CInt(appconf.Value)
            End If
            bindDDLstatus()
            ReadData()
            dtgServiceBooking.CurrentPageIndex = 0
            BindPage(dtgServiceBooking.CurrentPageIndex)
        End If
        txtKodeDealer.Text = HiddenField1.Value
        txtNamaDealer.Text = HiddenField2.Value
    End Sub

    Protected Sub btnHidden_Click(sender As Object, e As EventArgs) Handles btnHidden.Click
        ReadData()
        dtgServiceBooking.CurrentPageIndex = 0
        BindPage(dtgServiceBooking.CurrentPageIndex)
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        ReadData()
        dtgServiceBooking.CurrentPageIndex = 0
        BindPage(dtgServiceBooking.CurrentPageIndex)
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub dtgServiceBooking_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgServiceBooking.PageIndexChanged
        '-- Change datagrid page

        dtgServiceBooking.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)

    End Sub

    Private Sub dtgServiceBooking_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgServiceBooking.ItemCommand
        If e.CommandName = "lnkDetail" Then
            'Dim nResult = New ServiceBookingFacade(User).UpdateStatus(e.Item.Cells(0).Text)
            'MessageBox.Show("Service Booking Dibatalkan.")
            'ReadData()
            'dtgServiceBooking.CurrentPageIndex = 0
            'BindPage(dtgServiceBooking.CurrentPageIndex)
            Dim msgErr As String = String.Empty
            crit = New CriteriaComposite(New Criteria(GetType(ServiceReminderFollowUp), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(ServiceReminderFollowUp), "ServiceBooking.ID", MatchType.Exact, e.Item.Cells(0).Text))
            Dim arrLstGSR As ArrayList = New ServiceReminderFollowUpFacade(User).Retrieve(crit)
            Dim objGSR As New KTB.DNet.Domain.ServiceReminderFollowUp

            Dim crits = New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(CustomerCaseResponse), "ServiceBooking.ID", MatchType.Exact, e.Item.Cells(0).Text))
            Dim arrLstCC As ArrayList = New CustomerCaseResponseFacade(User).Retrieve(crits)


            If arrLstCC.Count > 0 Then
                
                If String.IsNullorEmpty(msgErr) Then
                    RegisterStartupScript("Open", String.Format("<script>ShowCancelServiceBooking({0},'{1}');</script>", _
                            e.Item.Cells(0).Text, "CC"))
                    Return
                Else
                    MessageBox.Show(msgErr)
                End If
            End If
            If arrLstGSR.Count > 0 And arrLstCC.Count = 0 Then
                If String.IsNullorEmpty(msgErr) Then
                    objGSR = CType(arrLstGSR(0), ServiceReminderFollowUp)
                    crits = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(ServiceReminder), "ID", MatchType.Exact, objGSR.ServiceReminder.ID))
                    Dim arrLstSR As ArrayList = New ServiceReminderFacade(User).Retrieve(crits)
                    If arrLstSR.Count > 0 Then
                        Dim objSR As New KTB.DNet.Domain.ServiceReminder
                        objSR = CType(arrLstSR(0), ServiceReminder)
                        objSR.Status = 2
                        'If Not IsNothing(objSR.CaseNumber) Or (objSR.CaseNumber <> "") Then
                        '    Dim objCC As New CustomerCase
                        '    Dim CRITERIA = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        '    CRITERIA.opAnd(New Criteria(GetType(CustomerCase), "ID", MatchType.Exact, objSR.CaseNumber))
                        '    Dim arrLstCCs As ArrayList = New CustomerCaseFacade(User).Retrieve(CRITERIA)
                        '    If arrLstCCs.Count > 0 Then
                        '        objCC = CType(arrLstCCs(0), CustomerCase)
                        '    End If
                        '    Dim objCCR As New CustomerCaseResponse
                        '    objCCR.CustomerCase.ID = objCC.ID
                        '    objCCR.ServiceBooking.ID = objGSR.ServiceBooking.ID
                        '    objCCR.WorkOrderNumber = ""
                        '    objCCR.Subject = objCC.Subject
                        '    objCCR.des
                        'End If
                        Dim nResult = New ServiceReminderFacade(User).Update(objSR) 'update status servicereminder
                        If nResult <> -1 Then
                            Dim objGSRNew As New KTB.DNet.Domain.ServiceReminderFollowUp
                            objGSRNew.ServiceReminder = objGSR.ServiceReminder
                            objGSRNew.ServiceBooking = objGSR.ServiceBooking
                            objGSRNew.FollowUpStatus = 2
                            objGSRNew.FollowUpAction = "Konsumen Batal Booking Service"
                            objGSRNew.FollowUpDate = DateTime.Now
                            objGSRNew.BookingDate = objGSR.BookingDate
                            objGSRNew.RowStatus = 0
                            nResult = New ServiceReminderFollowUpFacade(User).Insert(objGSRNew) 'insert new row servicereminderfollowup
                            If nResult <> -1 Then
                                nResult = New ServiceBookingFacade(User).UpdateStatus(e.Item.Cells(0).Text)
                                If nResult <> -1 Then
                                    MessageBox.Show("Service Booking Dibatalkan.")
                                    'ReadData()
                                    'dtgServiceBooking.CurrentPageIndex = 0
                                    'BindPage(dtgServiceBooking.CurrentPageIndex)
                                End If
                                'MessageBox.Show(SR.SaveSuccess)
                            Else
                                MessageBox.Show(SR.SaveFail)
                            End If
                        Else
                            MessageBox.Show(SR.SaveFail)
                        End If
                    End If

                    'RegisterStartupScript("Open", String.Format("<script>ShowCancelServiceBooking({0},'{1}');</script>", _
                    '        e.Item.Cells(0).Text, "GSR"))
                    'Return
                Else
                    MessageBox.Show(msgErr)
                End If
            End If

            If arrLstCC.Count = 0 And arrLstGSR.Count = 0 Then
                Dim nResult = New ServiceBookingFacade(User).UpdateStatus(e.Item.Cells(0).Text)
                If nResult <> -1 Then
                    MessageBox.Show("Service Booking Dibatalkan.")

                    'dtgServiceBooking.CurrentPageIndex = 0
                    'BindPage(dtgServiceBooking.CurrentPageIndex)
                End If
            End If

            ReadData()
            dtgServiceBooking.CurrentPageIndex = 0
            BindPage(dtgServiceBooking.CurrentPageIndex)
        ElseIf e.CommandName = "lnkEdit" Then
            Response.Redirect("FrmInputServiceBooking.aspx?MenuFrom=Daftar&Mode=Edit&ID=" + e.Item.Cells(0).Text)
        ElseIf e.CommandName = "lnkPreview" Then
            Response.Redirect("FrmInputServiceBooking.aspx?MenuFrom=Daftar&Mode=View&ID=" + e.Item.Cells(0).Text)
        End If
    End Sub

    Private Sub dtgServiceBooking_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceBooking.ItemDataBound
        Dim RowValue As ServiceBooking = CType(e.Item.DataItem, ServiceBooking)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lbNo"), Label)
            Dim lnkDetail As LinkButton = CType(e.Item.FindControl("lnkDetail"), LinkButton)
            Dim lnkEdit As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)
            Dim lnkPreview As LinkButton = CType(e.Item.FindControl("lnkPreview"), LinkButton)

            lblNo.Text = (dtgServiceBooking.CurrentPageIndex * dtgServiceBooking.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            If isDealerDMS Or objDealer Is Nothing Or Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lnkEdit.Visible = False
                lnkDetail.Visible = False
            End If

            Dim Tanggal As Date = RowValue.IncomingDateStart.Date
            'Dim TglSekarang As Date = Date.Now.Date
            Dim DayConfig As Integer = 0

            If Not IsNothing(ViewState("CutOff")) Then
                DayConfig = ViewState("CutOff")
            End If

            Dim ff As Integer = (Tanggal - Date.Now.Date).TotalDays
            'Dim ff As Integer = (Tanggal - Date.Now).TotalDays


            If RowValue.Status <> "0" And Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If isDealerDMS Then
                    lnkDetail.Visible = False
                Else
                    If Not objDealer Is Nothing Then
                        If (objDealer.ID = RowValue.Dealer.ID) Then
                            If (ff >= DayConfig) Then
                                If (RowValue.Status = 2) Then
                                    lnkDetail.Visible = False
                                    lnkEdit.Visible = False
                                ElseIf (RowValue.Status = 1) Then
                                    lnkDetail.Visible = True
                                    lnkEdit.Visible = True
                                End If
                            Else
                                lnkDetail.Visible = False
                                lnkEdit.Visible = False
                            End If
                        Else
                            lnkDetail.Visible = False
                            lnkEdit.Visible = False
                        End If
                    End If
                End If
            Else
                lnkDetail.Visible = False
                lnkEdit.Visible = False
            End If

            lnkPreview.Visible = RowValue.Status <> CInt(EnumStallMaster.StatusBooking.Request).ToString
        End If
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Dim arrServiceBooking As ArrayList = CType(sessHelper.GetSession("ServiceBookingList"), ArrayList)
        'Dim aStatus As New ArrayList
        If arrServiceBooking.Count <> 0 Then
            '   DoDownload(arrStallMaster)
            SetDownload()
        End If
    End Sub

#End Region

#Region "Custom Method"

    Private Sub CheckPrivilege()
        m_bListPrivilege = SecurityProvider.Authorize(Context.User, SR.ServiceBooking_View_Privilage)
        isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingListServiceBooking))
        If Not m_bListPrivilege Or Not isDealerPiloting Then
            'If Not m_bListPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Stall - Daftar Service Booking")
        End If
    End Sub

    Private Sub ReadData()
        '-- Read all data selected
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objDealer Is Nothing Then
            If Not objDealer.DealerGroup Is Nothing Then
                criterias.opAnd(New Criteria(GetType(ServiceBooking), "Dealer.DealerGroup.DealerGroupCode", MatchType.Exact, objDealer.DealerGroup.DealerGroupCode))
            End If
        End If

        If (txtKodeDealer.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(ServiceBooking), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        End If

        If cbKedatangan.Checked Then
            If (CInt(ICKedatangan.Value.Year <> 1)) And (CInt(ICKedatanganTo.Value.Year <> 1)) Then
                Dim TanggalAwal As New Date(CInt(ICKedatangan.Value.Year), CInt(ICKedatangan.Value.Month), CInt(ICKedatangan.Value.Day), 0, 0, 0)
                'Dim tanggal As Date = TanggalAwal.AddDays(1)
                Dim tanggal As New Date(CInt(ICKedatanganTo.Value.Year), CInt(ICKedatanganTo.Value.Month), CInt(ICKedatanganTo.Value.Day), 0, 0, 0)
                '        tanggal.AddDays()
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceBooking), "IncomingDateStart", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd")))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceBooking), "IncomingDateStart", MatchType.Lesser, Format(tanggal.AddDays(1), "yyyy-MM-dd")))
            End If
        End If

        If cbPengerjaan.Checked Then
            If (CInt(ICPengerjaan.Value.Year <> 1)) And (CInt(ICPengerjaan.Value.Year <> 1)) Then
                Dim TanggalAwal As New Date(CInt(ICPengerjaan.Value.Year), CInt(ICPengerjaan.Value.Month), CInt(ICPengerjaan.Value.Day), 0, 0, 0)
                'Dim tanggal As Date = TanggalAwal.AddDays(1)
                Dim tanggal As New Date(CInt(ICPengerjaanTo.Value.Year), CInt(ICPengerjaanTo.Value.Month), CInt(ICPengerjaanTo.Value.Day), 0, 0, 0)
                '        tanggal.AddDays()
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceBooking), "WorkingTimeStart", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd")))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceBooking), "WorkingTimeStart", MatchType.Lesser, Format(tanggal.AddDays(1), "yyyy-MM-dd")))
            End If
        End If

        If (txtPlatNomor.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(ServiceBooking), "PlateNumber", MatchType.Exact, txtPlatNomor.Text.Trim()))
        End If

        If (txtNamaKonsumen.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(ServiceBooking), "CustomerName", MatchType.Exact, txtNamaKonsumen.Text.Trim()))
        End If

        If (txtNoTelp.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(ServiceBooking), "CustomerPhoneNumber", MatchType.Exact, txtNoTelp.Text.Trim()))
        End If

        If ddlStatus.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(ServiceBooking), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

        'Dim arrStallMaster As ArrayList = New StallMasterFacade(User).RetrieveByCriteria(criterias, 1, 25, 25)
        Dim arrServiceBooking As ArrayList = New ServiceBookingFacade(User).Retrieve(criterias)


        '-- Store InvoiceReqList into session for later use
        sessHelper.SetSession("ServiceBookingList", arrServiceBooking)

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrServiceBooking As ArrayList = CType(sessHelper.GetSession("ServiceBookingList"), ArrayList)
        Dim aStatus As New ArrayList
        If arrServiceBooking.Count <> 0 Then
            ' SortListControl(arrStallMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrServiceBooking, pageIndex, dtgServiceBooking.PageSize)
            dtgServiceBooking.DataSource = PagedList
            dtgServiceBooking.VirtualItemCount = arrServiceBooking.Count()
            dtgServiceBooking.DataBind()
        Else
            dtgServiceBooking.DataSource = New ArrayList
            dtgServiceBooking.VirtualItemCount = 0
            dtgServiceBooking.CurrentPageIndex = 0
            dtgServiceBooking.DataBind()
        End If
    End Sub

    Private Sub bindDDLstatus()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.Status"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        With ddlStatus
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
        End With
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Piih ", -1))
        'ddlStatus.SelectedIndex = 0
    End Sub

#Region "Download excel"

    Private Sub SetDownload()
        Dim arrData As New DataTable
        Dim crits As CriteriaComposite
        If dtgServiceBooking.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

    
        ' mengambil data yang dibutuhkan
        Dim arrServiceBooking As ArrayList = CType(sessHelper.GetSession("ServiceBookingList"), ArrayList)
      
        If arrServiceBooking.Count > 0 Then
            CreateExcel("ServiceBooking", arrServiceBooking)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Nomor Reservasi"
            ws.Cells("C3").Value = "Model Kendaraan"
            ws.Cells("D3").Value = "Tipe Kendaraan"
            ws.Cells("E3").Value = "Nomor Rangka"
            ws.Cells("F3").Value = "Nomor Plat Mobil"
            ws.Cells("G3").Value = "Nama Konsumen"
            ws.Cells("H3").Value = "Odometer"
            ws.Cells("I3").Value = "No Telp Konsumen"
            ws.Cells("J3").Value = "Stall"
            ws.Cells("K3").Value = "Catatan"
            ws.Cells("L3").Value = "Tipe Konsumen"
            ws.Cells("M3").Value = "Rencana Kedatangan"
            ws.Cells("N3").Value = "Rencana Pengambilan"
            ws.Cells("O3").Value = "Rencana Pengerjaan"
            ws.Cells("P3").Value = "Status"

            'Dim standardCodeStatusClaimList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusClaim").Cast(Of  _
            '                                    StandardCode).ToList()
            'Dim standardCodeStatusProsesReturList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusProsesRetur").Cast(Of  _
            'StandardCode).ToList()
            Dim strPickUpType = ""
            Dim strStatus As String = ""

            Dim idx As Integer = 0
            For i As Integer = 0 To Data.Count - 1
                Dim item As ServiceBooking = Data(i)

                ws.Cells(idx + 4, 1).Value = idx + 1
                ws.Cells(idx + 4, 2).Value = item.ServiceBookingCode

                If Not IsNothing(item.VechileModel) Then
                    ws.Cells(idx + 4, 3).Value = item.VechileModel.VechileModelCode & " - " & item.VechileModel.IndDescription
                Else
                    ws.Cells(idx + 4, 3).Value = item.VehicleTypeDescription
                End If

                If Not IsNothing(item.VechileType) Then
                    ws.Cells(idx + 4, 4).Value = item.VechileType.Description
                Else
                    ws.Cells(idx + 4, 4).Value = ""
                End If

                If Not IsNothing(item.ChassisMaster) Then
                    ws.Cells(idx + 4, 5).Value = item.ChassisMaster.ChassisNumber
                Else
                    ws.Cells(idx + 4, 5).Value = ""
                End If
                ws.Cells(idx + 4, 6).Value = item.PlateNumber
                ws.Cells(idx + 4, 7).Value = item.CustomerName
                ws.Cells(idx + 4, 8).Value = item.OdoMeter
                ws.Cells(idx + 4, 9).Value = item.CustomerPhoneNumber

                ws.Cells(idx + 4, 10).Value = item.StallMaster.StallName

                Dim tglDatang As String
                tglDatang = Format(item.IncomingDateStart, "dd/MM/yyyy hh:mm").ToString()

                Dim tglAmbil As String
                tglAmbil = Format(item.IncomingDateEnd, "dd/MM/yyyy hh:mm").ToString()

                Dim tglPengerjaan As String
                tglPengerjaan = Format(item.WorkingTimeStart, "dd/MM/yyyy hh:mm").ToString & " - " & Format(item.WorkingTimeEnd, "hh:mm").ToString


                ws.Cells(idx + 4, 11).Value = item.Notes

                Dim criteriasa As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasa.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.PickupType"))
                criteriasa.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.PickupType))
                Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criteriasa)
                Dim objStandardCode As New StandardCode
                If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
                    objStandardCode = CType(arrDDL(0), StandardCode)
                    strPickUpType = objStandardCode.ValueDesc
                End If

                ws.Cells(idx + 4, 12).Value = strPickUpType
                ws.Cells(idx + 4, 13).Value = tglDatang
                ws.Cells(idx + 4, 14).Value = tglAmbil
                ws.Cells(idx + 4, 15).Value = tglPengerjaan

                Dim criterias1 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.Status"))
                criterias1.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.Status))
                Dim arrDDL1 As ArrayList = New StandardCodeFacade(User).Retrieve(criterias1)
                Dim objStandardCode1 As New StandardCode
                If Not IsNothing(arrDDL1) AndAlso arrDDL1.Count > 0 Then
                    objStandardCode1 = CType(arrDDL1(0), StandardCode)
                    strStatus = objStandardCode1.ValueDesc
                End If

                ws.Cells(idx + 4, 16).Value = strStatus

                idx = idx + 1
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=""{0}"";", fileName))
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub
#End Region

#End Region

End Class