Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports OfficeOpenXml
Imports System.Linq

Public Class FrmBabitEventReportReceiptList
    Inherits System.Web.UI.Page

    Private oDealer As Dealer
    Private sessHelper As New SessionHelper
    Private arlCheckedItemColl As ArrayList = New ArrayList
    Private SessionCriteriaEvent = "FrmBabitEventReportReceiptList.CriteriaList"
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private generateJVPriv As Boolean
    Private validasiPriv As Boolean
    Private batalValidasiPriv As Boolean
    Private konfirmasiPriv As Boolean
    Private batalKonfirmasiPriv As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Authorization()
        GetRegNumber(1)
        If Not IsPostBack Then
            PageInit()
            BindGrid(0)
        End If
        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim total As Integer = 0
        Dim crit As CriteriaComposite
        If Not IsNothing(ViewState("Back")) Then
            ReadCriteria()
            crit = SearchCriteria()
            ViewState.Remove("Back")
        Else
            crit = SearchCriteria()
        End If

        If Not IsNothing(CType(sessHelper.GetSession("FrmBabitEventReportReceiptList"), ArrayList)) Then
            sessHelper.SetSession("_PgIdxBefore", CType(sessHelper.GetSession("_PgIdxNext"), Integer))
            sessHelper.SetSession("_PgIdxNext", pageIndex)

            arlCheckedItemColl = GetCheckedItem()
            Dim currentPage As String = CType(sessHelper.GetSession("_PgIdxBefore"), String)
            sessHelper.SetSession("eventSessReceiptListsProcess" + currentPage, arlCheckedItemColl)

            Dim arlUnCheckedItemColl As ArrayList = GetUnCheckedItem()
            currentPage = CType(sessHelper.GetSession("_PgIdxBefore"), String)
            sessHelper.SetSession("eventSessReceiptListsProcess2" + currentPage, arlUnCheckedItemColl)
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitEventReportReceipt), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        Dim listSource As ArrayList = New BabitEventReportReceiptFacade(User).Retrieve(crit, sortColl)
        If listSource.Count <> 0 Then
            CommonFunction.SortListControl(listSource, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(listSource, pageIndex, dgListKuitansi.PageSize)
            sessHelper.SetSession("FrmBabitEventReportReceiptList", PagedList)
            dgListKuitansi.DataSource = PagedList
            dgListKuitansi.VirtualItemCount = listSource.Count
            dgListKuitansi.DataBind()
        Else
            dgListKuitansi.DataSource = New ArrayList
            dgListKuitansi.VirtualItemCount = 0
            dgListKuitansi.CurrentPageIndex = 0
            dgListKuitansi.DataBind()
        End If
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sessHelper.GetSession(SessionCriteriaEvent), Hashtable)
        If Not crit Is Nothing Then
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
            txtNoRegEvent.Text = CStr(crit.Item("txtNoRegBabit"))
            txtNoKuitansi.Text = CStr(crit.Item("txtNoKuitansi"))
            ddlStatus.SelectedValue = CStr(crit.Item("ddlStatus"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgListKuitansi.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Function GetRegNumber(ByVal seq As Integer) As String
        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJV), "CreatedTime", MatchType.GreaterOrEqual, Date.Today.Year.ToString))
        crit.opAnd(New Criteria(GetType(BabitEventReportJV), "CreatedTime", MatchType.Lesser, Date.Today.AddYears(1).Year.ToString))
        Dim arrl As ArrayList = New BabitEventReportJVFacade(User).Retrieve(crit)
        Dim _return As String
        If arrl.Count > 0 Then
            Dim objBH As BabitEventReportJV = CommonFunction.SortListControl(arrl, "RegNumber", Sort.SortDirection.DESC)(0)
            Dim noReg As String = objBH.RegNumber
            _return = "JVE" & Date.Today.ToString("yy") & (CInt(noReg.Substring(5, 5)) + seq).ToString("d5")
        Else
            _return = "JVE" & Date.Today.ToString("yy") & CInt(seq).ToString("d5")
        End If
        Return _return
    End Function

    Private Sub PageInit()
        BindDDLs()
        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.DESC
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Dim arrChecked As ArrayList = GetCheckedItem()
        If ddlAction.SelectedIndex = 0 Then
            MessageBox.Show("Status belum dipilih")
            Exit Sub
        End If
        If arrChecked.Count = 0 Then
            MessageBox.Show("Tidak ada data kuitansi yang di pilih")
            Exit Sub
        End If
        Dim arrProcessed As ArrayList = New ArrayList
        For Each oBabitEventReportReceipt As BabitEventReportReceipt In arrChecked
            Select Case ddlAction.SelectedValue
                Case 0 'Validasi
                    If oBabitEventReportReceipt.Status <> 0 Then
                        MessageBox.Show("Proses kuitansi hanya untuk Kuitansi status Baru")
                        Exit Sub
                    Else
                        oBabitEventReportReceipt.Status = 1
                    End If
                Case 1 'Batal Validasi
                    If oBabitEventReportReceipt.Status <> 1 Then
                        MessageBox.Show("Proses kuitansi hanya untuk Kuitansi status Validasi")
                        Exit Sub
                    Else
                        oBabitEventReportReceipt.Status = 0
                    End If
                Case 2 'Konfirmasi
                    If oBabitEventReportReceipt.Status <> 1 Then
                        MessageBox.Show("Proses kuitansi hanya untuk Kuitansi status Validasi")
                        Exit Sub
                    Else
                        oBabitEventReportReceipt.Status = 2
                    End If
                Case 3 'Batal Konfirmasi
                    If oBabitEventReportReceipt.Status <> 2 Then
                        MessageBox.Show("Proses kuitansi hanya untuk Kuitansi status Konfirmasi")
                        Exit Sub
                    Else
                        oBabitEventReportReceipt.Status = 1
                    End If
            End Select
            arrProcessed.Add(oBabitEventReportReceipt)
        Next
        Dim _result = -1
        For Each item As BabitEventReportReceipt In arrProcessed
            _result = New BabitEventReportReceiptFacade(User).Update(item)
        Next
        If _result <> -1 Then
            MessageBox.Show("Proses Update Status Sukses")
        Else
            MessageBox.Show("Proses Update Status Gagal")
        End If
        BindGrid(0)
    End Sub

    Protected Sub btnGenerateJV_Click(sender As Object, e As EventArgs) Handles btnGenerateJV.Click
        Dim arrChecked As ArrayList = GetCheckedItem()
        If arrChecked.Count = 0 Then
            MessageBox.Show("Tidak ada data kuitansi yang di pilih")
            Exit Sub
        End If
        Dim arrBRJV As ArrayList = New ArrayList()
        Dim dealerTemp As Dealer
        Dim dealerCodeTemp As String = ""
        For Each brReceipt As BabitEventReportReceipt In arrChecked
            If dealerCodeTemp <> "" Then
                If dealerCodeTemp <> brReceipt.BabitEventReportHeader.Dealer.DealerCode Then
                    MessageBox.Show("Bundling kuitansi untuk pengajuan hanya boleh untuk dealer yang sama")
                    Exit Sub
                End If
            Else
                dealerCodeTemp = brReceipt.BabitEventReportHeader.Dealer.DealerCode
            End If
            dealerTemp = brReceipt.BabitEventReportHeader.Dealer

            If brReceipt.Status <> 2 Then
                MessageBox.Show("Pengajuan JV untuk kuitansi: " & brReceipt.ReceiptNo & " statusnya belum Konfirmasi")
                Exit Sub
            End If

            If IsNothing(brReceipt.MasterAccrued) Then
                MessageBox.Show("Data accrued masih kosong untuk kuitansi: " & brReceipt.ReceiptNo & ". Silahkan masukkan nomor Accrued.")
                Exit Sub
            End If

            'arrBRJV.Add(brReceipt)
        Next

        Dim seq As Integer = 1
        Dim arrJV As New ArrayList
        Dim arrJVtoReceipt As New ArrayList
        Dim arrReceipt As New ArrayList
        For Each reportReceipt As BabitEventReportReceipt In arrChecked
            reportReceipt.Status = 3
            arrReceipt.Add(reportReceipt)

            Dim brJV As BabitEventReportJV = New BabitEventReportJV()
            brJV.Dealer = dealerTemp
            brJV.RegNumber = GetRegNumber(seq)
            arrJV.Add(brJV)
            seq += 1

            Dim JVtoReceipt As BabitEventReportJVtoReceipt = New BabitEventReportJVtoReceipt()
            JVtoReceipt.BabitEventReportJV = brJV
            JVtoReceipt.BabitEventReportReceipt = reportReceipt
            arrJVtoReceipt.Add(JVtoReceipt)
        Next

        Dim _result As Integer = 0
        If arrJVtoReceipt.Count > 0 Then
            _result = New BabitEventReportJVtoReceiptFacade(User).InsertTransaction(arrReceipt, arrJV, arrJVtoReceipt)
            If _result = 7 Then
                MessageBox.Show("Pengajuan JV berhasil dibuat")
            Else
                MessageBox.Show("Pengajuan JV gagal dibuat")
            End If
        End If

        BindGrid(0)
    End Sub

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Event_Kuitansi_Detail_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - DAFTAR KUITANSI EVENT")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Event_Kuitansi_Detail_Privilege)
            editPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Event_Kuitansi_Edit_Privilege)
            generateJVPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Event_Kuitansi_GenerateJV_Privilege)
            validasiPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Event_Kuitansi_Validasi_Privilege)
            batalValidasiPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Event_Kuitansi_Batal_Validasi_Privilege)
            konfirmasiPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Event_Kuitansi_Konfirmasi_Privilege)
            batalKonfirmasiPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Event_Kuitansi_Batal_Konfirmasi_Privilege)
        End If

        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Visible = True
            lblKodeDealer.Visible = False
            lblPopUpDealer.Visible = True
            btnGenerateJV.Visible = generateJVPriv
        Else
            txtKodeDealer.Text = oDealer.DealerCode
            lblKodeDealer.Text = oDealer.DealerCode & " / " & oDealer.DealerName

            txtKodeDealer.Visible = False
            lblKodeDealer.Visible = True
            lblPopUpDealer.Visible = False
            btnGenerateJV.Visible = False
        End If
    End Sub

    Private Function GetCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("FrmBabitEventReportReceiptList"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        If Not IsNothing(arrGrid) Then
            Dim nIndeks As Integer
            For Each dtgItem As DataGridItem In dgListKuitansi.Items
                nIndeks = dtgItem.ItemIndex
                Dim objCM As BabitEventReportReceipt = CType(arrGrid(nIndeks), BabitEventReportReceipt)
                If CType(dtgItem.Cells(1).FindControl("chkSelect"), CheckBox).Checked Then
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        Dim txtNoAcc As TextBox = CType(dtgItem.Cells(9).FindControl("txtNoAcc"), TextBox)
                        Dim lblNoAcc As Label = CType(dtgItem.Cells(9).FindControl("lblNoAcc"), Label)
                        If txtNoAcc.Text <> "" Then
                            objCM.MasterAccrued = New MasterAccruedFacade(User).Retrieve(txtNoAcc.Text)
                        ElseIf lblNoAcc.Text <> "" Then
                            objCM.MasterAccrued = New MasterAccruedFacade(User).Retrieve(lblNoAcc.Text)
                        Else
                            MessageBox.Show("Nomor Accrued belum diinputkan pada kuitansi : " & objCM.ReceiptNo)
                            Return New ArrayList
                        End If
                    End If
                    arlCheckedItem.Add(objCM)
                End If
            Next
        End If

        Return arlCheckedItem
    End Function

    Private Function GetUnCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("FrmBabitEventReportReceiptList"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        If Not IsNothing(arrGrid) Then
            Dim nIndeks As Integer
            For Each dtgItem As DataGridItem In dgListKuitansi.Items
                nIndeks = dtgItem.ItemIndex
                Dim objCM As BabitEventReportReceipt = CType(arrGrid(nIndeks), BabitEventReportReceipt)
                If Not CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                    arlCheckedItem.Add(objCM)
                End If
            Next
        End If

        Return arlCheckedItem
    End Function

    Private Sub BindDDLs()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumBabit.EventReceiptStatus"))
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
        ddlStatus.SelectedIndex = 0

        If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            ddlStatus.SelectedValue = -1
        Else
            ddlStatus.SelectedValue = 2
        End If

        ddlAction.Items.Clear()
        ddlAction.Items.Add(New ListItem("Silahkan Pilih", -1))
        With ddlAction.Items
            If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If validasiPriv Then
                    .Add(New ListItem("Validasi", "0"))
                End If
                If batalValidasiPriv Then
                    .Add(New ListItem("Batal Validasi", "1"))
                End If
            Else
                If konfirmasiPriv Then
                    .Add(New ListItem("Konfirmasi", "2"))
                End If
                If batalKonfirmasiPriv Then
                    .Add(New ListItem("Batal Konfirmasi", "3"))
                End If
            End If
        End With
    End Sub

    Private Function SearchCriteria() As CriteriaComposite
        Dim strSql As String = String.Empty
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(BabitEventReportReceipt), "BabitEventReportHeader.Status", MatchType.Exact, 8)) '---> 8 = Status Setuju

        'TODO
        If txtNoKuitansi.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventReportReceipt), "ReceiptNo", MatchType.Partial, txtNoKuitansi.Text))
        End If

        If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            crit.opAnd(New Criteria(GetType(BabitEventReportReceipt), "BabitEventReportHeader.BabitEventProposalHeader.Dealer.DealerCode", MatchType.Partial, lblKodeDealer.Text.Trim.Split("/")(0).Trim))
        Else
            If txtKodeDealer.Text <> "" Then
                crit.opAnd(New Criteria(GetType(BabitEventReportReceipt), "BabitEventReportHeader.BabitEventProposalHeader.Dealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))
            End If
        End If

        'TODO
        If txtEventName.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventReportReceipt), "BabitEventReportHeader.BabitEventProposalHeader.EventProposalName", MatchType.Partial, txtEventName.Text))
        End If

        'TODO
        If txtNoRegEvent.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventReportReceipt), "BabitEventReportHeader.BabitEventProposalHeader.EventRegNumber", MatchType.Partial, txtNoRegEvent.Text))
        End If

        'TODO
        If ddlStatus.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(BabitEventReportReceipt), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        sessHelper.SetSession("criteriadownload", crit)
        Return crit
    End Function

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGrid(0)
        StoreCriteria()
        If dgListKuitansi.Items.Count = 0 Then
            MessageBox.Show("Data list tidak ditemukan")
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("txtNoRegEvent", txtNoRegEvent.Text)
        crit.Add("txtNoKuitansi", txtNoKuitansi.Text)
        crit.Add("ddlStatus", ddlStatus.SelectedValue)

        crit.Add("PageIndex", dgListKuitansi.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sessHelper.SetSession(SessionCriteriaEvent, crit)  '-- Store in session
    End Sub

    Protected Sub dgListKuitansi_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListKuitansi.ItemDataBound
        Dim objBR As BabitEventReportReceipt = New BabitEventReportReceipt
        Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
        Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
        Dim lblRegEvent As Label = CType(e.Item.FindControl("lblRegEvent"), Label)
        Dim lblEventName As Label = CType(e.Item.FindControl("lblEventName"), Label)
        Dim lblReceipt As Label = CType(e.Item.FindControl("lblReceipt"), Label)
        Dim lblNoAcc As Label = CType(e.Item.FindControl("lblNoAcc"), Label)
        Dim lblNoJV As Label = CType(e.Item.FindControl("lblNoJV"), Label)
        Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
        Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
        Dim txtNoAcc As TextBox = CType(e.Item.FindControl("txtNoAcc"), TextBox)
        Dim hdnNoAcc As HiddenField = CType(e.Item.FindControl("hdnNoAcc"), HiddenField)
        Dim lblPopUpNoAcc As Label = CType(e.Item.FindControl("lblPopUpNoAcc"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As BabitEventReportReceipt = CType(e.Item.DataItem, BabitEventReportReceipt)
            chkSelect.Checked = False
            Dim currentPage As String = CType(sessHelper.GetSession("_PgIdxNext"), String)
            Dim arrGridDF As ArrayList = CType(sessHelper.GetSession("EventSessReceiptListsProcess" + currentPage), ArrayList)
            If IsNothing(arrGridDF) Then arrGridDF = New ArrayList
            For Each oDF As BabitEventReportReceipt In arrGridDF
                If objBR.ID = oDF.ID Then
                    chkSelect.Checked = True
                    Exit For
                End If
            Next
            Dim oBH As BabitEventProposalHeader = oData.BabitEventReportHeader.BabitEventProposalHeader
            lblNo.Text = (dgListKuitansi.PageSize * dgListKuitansi.CurrentPageIndex) + e.Item.ItemIndex + 1
            If IsNothing(oData.BabitEventReportHeader.BabitEventProposalHeader.Dealer) Then
                lblDealerCode.Text = oData.ID
                lblDealerName.Text = oData.ID
            Else
                lblDealerCode.Text = oBH.Dealer.DealerCode
                lblDealerName.Text = oBH.Dealer.DealerName
            End If

            lblRegEvent.Text = oBH.EventRegNumber
            lblEventName.Text = oBH.EventProposalName
            lblReceipt.Text = oData.ReceiptNo
            If Not IsNothing(oData.MasterAccrued) Then
                lblNoAcc.Text = oData.MasterAccrued.AccKey
                txtNoAcc.Text = oData.MasterAccrued.AccKey
            Else
                lblNoAcc.Text = ""
                txtNoAcc.Text = ""
            End If
            Dim criter As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criter.opAnd(New Criteria(GetType(BabitEventReportJVtoReceipt), "BabitEventReportReceipt.ID", MatchType.Exact, oData.ID))
            Dim oBRJVR As ArrayList = New BabitEventReportJVtoReceiptFacade(User).Retrieve(criter)
            If oBRJVR.Count > 0 Then
                lblNoJV.Text = CType(oBRJVR(0), BabitEventReportJVtoReceipt).BabitEventReportJV.RegNumber
            Else
                lblNoJV.Text = ""
            End If

            lnkbtnDetail.Visible = displayPriv
            lnkbtnEdit.Visible = False

            Select Case oData.Status
                Case 0
                    'lblStatus.Text = "Baru"
                    If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lnkbtnEdit.Visible = editPriv
                    End If
                Case 1
                    'lblStatus.Text = "Validasi"
                Case 2
                    'lblStatus.Text = "Konfirmasi"
                Case 3
                    'lblStatus.Text = "Proses JV"
                Case 4
                    'lblStatus.Text = "Selesai"
            End Select
            lblStatus.Text = CommonFunction.GetEnumDescription(oData.Status, "EnumBabit.EventReceiptStatus")

            If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                txtNoAcc.Visible = False
                lblPopUpNoAcc.Visible = False
                lblNoAcc.Visible = True
            Else
                Select Case oData.Status
                    Case 0, 1, 2
                        lblNoAcc.Visible = False
                        txtNoAcc.Visible = True
                        lblPopUpNoAcc.Visible = True
                    Case 3, 4
                        lblNoAcc.Visible = True
                        txtNoAcc.Visible = False
                        lblPopUpNoAcc.Visible = False
                    Case Else
                End Select
            End If
        End If
    End Sub

    Protected Sub dgListKuitansi_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgListKuitansi.ItemCommand
        Dim oEventReportHeader As BabitEventReportHeader = New BabitEventReportHeader
        Dim objBR As New BabitEventReportReceipt
        If e.Item.ItemType <> ListItemType.Pager AndAlso e.Item.ItemType <> ListItemType.Header Then
            objBR = New BabitEventReportReceiptFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
            oEventReportHeader = objBR.BabitEventReportHeader
        End If
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmInputBabitEventReportReceipt.aspx?From=List&Mode=Detail&BabitEventReportHeaderID=" & oEventReportHeader.ID)
            Case "Edit"
                Response.Redirect("FrmInputBabitEventReportReceipt.aspx?From=List&Mode=Edit&BabitEventReportHeaderID=" & oEventReportHeader.ID)
        End Select
    End Sub

    Private Sub dgListKuitansi_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgListKuitansi.PageIndexChanged
        dgListKuitansi.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgListKuitansi_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgListKuitansi.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        '-- Bind page-1
        dgListKuitansi.CurrentPageIndex = 0
        BindGrid(dgListKuitansi.CurrentPageIndex)
        StoreCriteria()
    End Sub
End Class