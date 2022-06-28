Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmPemotonganAlokasiBabit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNoPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchNoPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents pnlButton As System.Web.UI.WebControls.Panel
    Protected WithEvents lblAlokasiBabit As System.Web.UI.WebControls.Label
    Protected WithEvents lblTerpakai As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisa As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnRilis As System.Web.UI.WebControls.Button
    Protected WithEvents pnlAlokasibabit As System.Web.UI.WebControls.Panel
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents pnlSearch As System.Web.UI.WebControls.Panel
    Protected WithEvents icTglPersetujuanFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglPersetujuanTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkTanggalPengajuan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkTanggalPersetujuan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPersetujuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatusPersetujuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnUbah As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipeBabit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCancel As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnPrint As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Dim sessHelper As New SessionHelper
#End Region

#Region "Custom Methods"
    Private Sub BindTipeBabit()
        ddlTipeBabit.Items.Clear()
        ddlTipeBabit.Items.Add(New ListItem("Silahkan Pilih", ""))
        ddlTipeBabit.Items.Add(New ListItem("Regular", EnumBabit.BabitType.Regular))
        ddlTipeBabit.Items.Add(New ListItem("Khusus", EnumBabit.BabitType.Khusus))
        ddlTipeBabit.SelectedIndex = 0
    End Sub
    Private Sub BindStatusPersetujuan()
        ddlStatusPersetujuan.Items.Clear()
        ddlStatusPersetujuan.Items.Add(New ListItem("Semua", "-1"))
        ddlStatusPersetujuan.Items.Add(New ListItem("Belum Disetujui", "1"))
        ddlStatusPersetujuan.Items.Add(New ListItem("Sudah Disetujui", "2"))
        ddlStatusPersetujuan.SelectedIndex = 0
    End Sub
    Sub FillBabitAllocation(ByVal BabitAllocation As Integer, ByVal PenggunaanBabit As Integer)
        lblAlokasiBabit.Text = BabitAllocation.ToString("#,##0")
        lblTerpakai.Text = PenggunaanBabit.ToString("#,##0")
        lblSisa.Text = (BabitAllocation - PenggunaanBabit).ToString("#,##0")
    End Sub

    Sub BindGrid(ByVal index As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "Status", MatchType.Exact, CType(EnumBabit.StatusBabitProposal.Proses, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitKhususAmount", MatchType.Exact, 0))

        If (txtNoPengajuan.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "NoPengajuan", MatchType.InSet, "('" & txtNoPengajuan.Text.Trim.Replace(";", "','") & "')"))
        End If

        If (txtKodeDealer.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        If chkTanggalPengajuan.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "CreatedTime", MatchType.GreaterOrEqual, icDateFrom.Value))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "CreatedTime", MatchType.LesserOrEqual, icDateUntil.Value.AddDays(1)))
        End If

        If txtNoPersetujuan.Text.Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "NoPersetujuan", MatchType.InSet, "('" & txtNoPersetujuan.Text.Trim.Replace(";", "','") & "')"))
        End If

        If ddlTipeBabit.SelectedIndex <> 0 Then
            If ddlTipeBabit.SelectedValue = EnumBabit.BabitType.Regular Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.AllocationType", MatchType.InSet, "(" & CType(EnumBabit.BabitAllocationType.Alokasi_Reguler, Short).ToString & "," & CType(EnumBabit.BabitAllocationType.Alokasi_Tambahan, Short).ToString & ")"))
            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.AllocationType", MatchType.Exact, CType(EnumBabit.BabitAllocationType.Babit_Khusus, Short)))
            End If
        End If

        If chkTanggalPersetujuan.Checked Then
            Dim dtStart As DateTime = New DateTime(icTglPersetujuanFrom.Value.Year, icTglPersetujuanFrom.Value.Month, icTglPersetujuanFrom.Value.Day, 0, 0, 0)
            Dim dtEnd As DateTime = New DateTime(icTglPersetujuanTo.Value.Year, icTglPersetujuanTo.Value.Month, icTglPersetujuanTo.Value.Day, 0, 0, 0)
            dtEnd = dtEnd.AddDays(1)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "LastUpdateTime", MatchType.GreaterOrEqual, dtStart))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "LastUpdateTime", MatchType.Lesser, dtEnd))
        End If

        If ddlStatusPersetujuan.SelectedIndex <> 0 Then
            If ddlStatusPersetujuan.SelectedValue = "1" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "NoPersetujuan", MatchType.InSet, "(null, '')"))
            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "NoPersetujuan", MatchType.No, ""))
            End If
        End If


        Dim arl As ArrayList = New ArrayList
        arl = New BabitSalesComm.BabitProposalFacade(User).RetrieveByCriteria(criterias, index + 1, dtgList.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

        If (arl.Count > 0) Then
            Dim totalAlokasi As Decimal = 0
            Dim totalPengunaan As Decimal = 0
            Dim tmpArl As ArrayList = CommonFunction.SortArraylist(arl, GetType(BabitProposal), "Dealer.ID", Sort.SortDirection.ASC)
            Dim tmpDealerId As Integer = 0
            For Each obj As BabitProposal In tmpArl
                If (tmpDealerId <> obj.Dealer.ID) Then
                    tmpDealerId = obj.Dealer.ID
                    totalAlokasi += obj.BabitAllocation.DanaBabit
                End If
                totalPengunaan += obj.KTBApprovalAmount
            Next
            FillBabitAllocation(totalAlokasi, totalPengunaan)

            'Dim arl2 As ArrayList = New ArrayList
            'Dim n As Integer = arl.Count - 1
            'For i As Integer = 0 To n
            '    arl2.Add(CType(arl(i), BabitProposal))
            '    Dim objTmp As BabitProposal = New BabitProposal
            '    'diakalin id disimpan ke status, krn ini data dummy untuk grid nested
            '    objTmp.ID = 0
            '    objTmp.Status = CType(arl(i), BabitProposal).ID
            '    arl2.Add(objTmp)
            'Next
            'sessHelper.SetSession("ListTmp", arl2)

            dtgList.DataSource = arl
            dtgList.VirtualItemCount = totalRow
            dtgList.DataBind()
        Else
            pnlButton.Visible = False
            dtgList.DataSource = New ArrayList
            dtgList.DataBind()
            lblAlokasiBabit.Text = "0"
            lblTerpakai.Text = "0"
            lblSisa.Text = "0"
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Sub UpdateData(ByVal Process As String)
        Dim i As Integer = 0
        Dim Amount As Decimal = 0
        Dim oBabitProposal As BabitProposal
        Dim isChecked As Boolean = False
        Dim arl As New ArrayList
        For Each item As DataGridItem In dtgList.Items
            Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim intiCalTglEvi As KTB.DNet.WebCC.IntiCalendar = CType(item.FindControl("intiCalTglEvi"), KTB.DNet.WebCC.IntiCalendar)
            Dim txtPersetujuanKTB As TextBox = CType(item.FindControl("txtPersetujuanKTB"), TextBox)
            Dim lblJumlahPengajuan As Label = CType(item.FindControl("lblJumlahPengajuan"), Label)

            If (chkItemChecked.Checked) Then
                isChecked = True
                oBabitProposal = New BabitProposal
                oBabitProposal = New BabitSalesComm.BabitProposalFacade(User).Retrieve(Convert.ToInt32(dtgList.DataKeys().Item(i)))

                If (intiCalTglEvi.Value > Now.Date) Then
                    MessageBox.Show("Tanggal terima evidence harus lebih kecil atau sama dengan hari ini")
                    Exit Sub
                End If

                If (oBabitProposal.ActivityType <> CInt(EnumBabit.BabitProposalType.Iklan)) Then
                    If (txtPersetujuanKTB.Text <> String.Empty) Then
                        If CDec(txtPersetujuanKTB.Text) < 0 Then
                            MessageBox.Show("Nilai persetujuan tidakl valid.")
                            Exit Sub
                        ElseIf (Convert.ToDecimal(lblJumlahPengajuan.Text) < Convert.ToDecimal(txtPersetujuanKTB.Text)) Then
                            MessageBox.Show("Persetujuan MMKSI tidak boleh lebih besar dari jumlah pengajuan")
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Persetujuan MMKSI harus diisi")
                        Exit Sub
                    End If
                    Dim jumlahPengajuan As Decimal = Convert.ToDecimal(lblJumlahPengajuan.Text)
                    If (oBabitProposal.BabitAllocation.TipeAlokasi = EnumBabit.BabitAllocationType.Alokasi_Reguler Or oBabitProposal.BabitAllocation.TipeAlokasi = EnumBabit.BabitAllocationType.Alokasi_Tambahan) Then
                        If CDec(txtPersetujuanKTB.Text) > (jumlahPengajuan * 0.5) Then
                            MessageBox.Show(String.Format("{0} - Nilai persetujuan maksimal 50% dari biaya yang diajukan untuk alokasi reguler dan alokasi tambahan, {1}", oBabitProposal.NoPengajuan, (jumlahPengajuan * 0.5)))
                            Exit Sub
                        End If
                    End If
                    Amount += Convert.ToDecimal(txtPersetujuanKTB.Text)
                    If (Amount > Convert.ToDecimal(oBabitProposal.BabitAllocation.SisaBabit)) Then
                        MessageBox.Show("Dana yg disetujui melebihi jumlah sisa alokasi babit " & "(" & oBabitProposal.BabitAllocation.SisaBabit & ")")
                        Exit Sub
                    End If
                    oBabitProposal.TglTerimaEvidance = intiCalTglEvi.Value
                    oBabitProposal.KTBApprovalAmount = txtPersetujuanKTB.Text
                    If (Process = "Rilis") Then
                        If (oBabitProposal.Status <> CType(EnumBabit.StatusBabitProposal.Disetujui, Short)) Then
                            oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Disetujui, Short)
                            arl.Add(oBabitProposal)
                        End If
                    Else
                        arl.Add(oBabitProposal)
                    End If
                Else
                    oBabitProposal.TglTerimaEvidance = intiCalTglEvi.Value
                    If (Process = "Rilis") Then
                        If (oBabitProposal.Status <> CType(EnumBabit.StatusBabitProposal.Disetujui, Short)) Then
                            oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Disetujui, Short)
                            arl.Add(oBabitProposal)
                        End If
                    Else
                        arl.Add(oBabitProposal)
                    End If
                End If
            End If
            i = i + 1
        Next

        If (arl.Count > 0) Then
            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(arl) = 1) Then
                If Not IsNothing(Request.QueryString("src")) Then
                    If (arl.Count = 1) Then
                        Dim obj As BabitProposal = CType(arl(0), BabitProposal)
                        Dim objTmp As BabitProposal = New BabitSalesComm.BabitProposalFacade(User).Retrieve(obj.ID)
                        txtNoPersetujuan.Text = objTmp.NoPersetujuan
                    End If
                End If
                MessageBox.Show(SR.UpdateSucces)
                BindGrid(dtgList.CurrentPageIndex)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            If (Process = "Rilis") Then
                If (isChecked) Then
                    MessageBox.Show("Babit proposal yang dipilih telah disetujui")
                Else
                    MessageBox.Show("Babit proposal belum dipilih")
                End If
            Else
                MessageBox.Show("Babit proposal belum dipilih")
            End If
        End If
    End Sub

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(icDateFrom.Value)
        arrLastState.Add(icDateUntil.Value)
        arrLastState.Add(txtNoPengajuan.Text)
        arrLastState.Add(dtgList.CurrentPageIndex)
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(ddlTipeBabit.SelectedIndex)
        arrLastState.Add(ddlStatusPersetujuan.SelectedIndex)
        arrLastState.Add(txtNoPersetujuan.Text)
        Session("BABITALLOCATIONPTGSESSIONLASTSTATE") = arrLastState
    End Sub

    Private Sub GetSessionCriteria()
        If Not Session("BABITALLOCATIONPTGSESSIONLASTSTATE") Is Nothing Then
            Dim arrLastState As ArrayList = Session("BABITALLOCATIONPTGSESSIONLASTSTATE")
            If Not arrLastState Is Nothing Then
                icDateFrom.Value = arrLastState.Item(0)
                icDateUntil.Value = arrLastState.Item(1)
                txtNoPengajuan.Text = arrLastState.Item(2)
                dtgList.CurrentPageIndex = arrLastState.Item(3)
                txtKodeDealer.Text = arrLastState.Item(4)
                ddlTipeBabit.SelectedValue = arrLastState.Item(5)
                ddlStatusPersetujuan.SelectedIndex = arrLastState.Item(6)
                txtNoPersetujuan.Text = arrLastState.Item(7)
                Session("BABITALLOCATIONPTGSESSIONLASTSTATE") = Nothing
                BindGrid(dtgList.CurrentPageIndex)
            End If
        End If
    End Sub

    Private Function CekCriteria()
        If chkTanggalPengajuan.Checked Then
            If (icDateFrom.Value > icDateUntil.Value) Then
                MessageBox.Show("Tanggal pengajuan awal harus lebih kecil atau sama dengan Tanggal pengajuan akhir")
                Return False
            End If
        End If

        If chkTanggalPersetujuan.Checked Then
            If icTglPersetujuanFrom.Value.Subtract(icTglPersetujuanTo.Value).TotalDays > 0 Then
                MessageBox.Show("Tanggal Persetujuan awal harus lebih kecil atau sama dengan Tanggal Persetujuan akhir")
                Return False
            End If
        End If
        Return True
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.BabitApprovalView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Persetujuan Babit")
        End If
    End Sub

    Private Function blnCekApprovalEdit() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.BabitApprovalEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function blnCekApprovalPrint() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.BabitApprovalPrint_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If (Not IsPostBack) Then

            lblSearchNoPengajuan.Attributes("onclick") = "ShowPPNoPengajuan();"
            lblKodeDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            ViewState("currSortColumn") = "NoPengajuan"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindTipeBabit()
            BindStatusPersetujuan()
            GetSessionCriteria()

            If Request.QueryString("src") Is Nothing Or Request.QueryString("src") = String.Empty Then
                btnCancel.Visible = False
            Else
                btnCancel.Visible = True
                txtKodeDealer.Enabled = False
                txtNoPengajuan.Enabled = False
                ddlTipeBabit.Enabled = False
                txtNoPersetujuan.Enabled = False
                ddlStatusPersetujuan.Enabled = False
                chkTanggalPengajuan.Enabled = False
                chkTanggalPersetujuan.Enabled = False
                icDateFrom.Enabled = False
                icDateUntil.Enabled = False
                icTglPersetujuanFrom.Enabled = False
                icTglPersetujuanTo.Enabled = False
                btnSearch.Enabled = False
            End If
            Dim objDealer As Dealer = Session.Item("DEALER")
            btnUbah.Visible = False
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnUbah.Visible = True
            End If

            ' add security
            If Not blnCekApprovalEdit() Then
                btnUbah.Enabled = False
                btnSave.Enabled = False
                btnRilis.Enabled = False
                dtgList.Columns(0).Visible = False ' checkbox list
            End If

            If Not blnCekApprovalPrint() Then
                btnPrint.Visible = False
                dtgList.Columns(0).Visible = False ' checkbox list
            End If

        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If CekCriteria() Then
            BindGrid(dtgList.CurrentPageIndex)
            pnlButton.Visible = False
            'dtgList.Columns(0).Visible = False
            'dtgList.Columns(9).Visible = False
        End If
    End Sub

    Private Sub btnUbah_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbah.Click
        If CekCriteria() Then
            BindGrid(0)
            If dtgList.Items.Count > 0 Then
                pnlButton.Visible = True
            End If
            dtgList.Columns(0).Visible = True
            dtgList.Columns(9).Visible = True
        End If
    End Sub

    Private Sub dtgList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgList.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim oBabitProposal As BabitProposal = CType(e.Item.DataItem, BabitProposal)
            If (IsNothing(oBabitProposal)) Then Return

            'If (oBabitProposal.ID > 0) Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(1).Controls.Add(lNum)
            Dim lblActivityType As Label = CType(e.Item.FindControl("lblActivityType"), Label)
            Dim lblJumlahPengajuan As Label = CType(e.Item.FindControl("lblJumlahPengajuan"), Label)
            Dim intiCalTglEvi As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("intiCalTglEvi"), KTB.DNet.WebCC.IntiCalendar)
            Dim lblCalender As Label = e.Item.FindControl("lblCalender")
            Dim txtPersetujuanKTB As TextBox = e.Item.FindControl("txtPersetujuanKTB")
            Dim lblPersetujuanKTB As Label = e.Item.FindControl("lblPersetujuanKTB")

            If (oBabitProposal.Status = EnumBabit.StatusBabitProposal.Disetujui) Then
                txtPersetujuanKTB.Enabled = False
            End If

            If (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
                Dim lnkDetail As LinkButton = CType(e.Item.FindControl("lnkDetail"), LinkButton)
                lnkDetail.Attributes.Add("onclick", String.Format("javascript:showPopUp('../PopUp/PopUpBpIklan.aspx?id={0}&dummy={1}','',500,760,'');", oBabitProposal.ID, DateTime.Now.ToString("ddMMyyyyhhmmss")))
                lnkDetail.Text = "Detil"
                txtPersetujuanKTB.Enabled = False
            End If

            If (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Even, Short)) Then
                lblActivityType.Text = "Event" 'EnumBabit.BabitProposalType.Even.ToString()
                Dim EventCost As Double = 0
                If (Not IsNothing(oBabitProposal.BPEvent)) Then
                    If (Not IsNothing(oBabitProposal.BPEvent.EventActivitys)) Then
                        For Each item As EventActivity In oBabitProposal.BPEvent.EventActivitys
                            EventCost += item.Comsumption + item.Entertainment + item.Equipment + CDbl(item.Place) + item.Others
                        Next
                    End If
                End If
                lblJumlahPengajuan.Text = EventCost.ToString("#,##0")
                EventCost = 0
            ElseIf (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
                lblActivityType.Text = EnumBabit.BabitProposalType.Iklan.ToString()
                Dim arlIklan As New ArrayList
                arlIklan = New BabitSalesComm.BabitProposalFacade(User).RetrieveIklanByBabitProposalID(oBabitProposal.ID)
                Dim IklanCost As Integer = 0
                For Each item As BPIklan In arlIklan
                    IklanCost += item.Expense
                Next
                lblJumlahPengajuan.Text = IklanCost.ToString("#,##0")
                IklanCost = 0
            ElseIf (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
                lblActivityType.Text = EnumBabit.BabitProposalType.Pameran.ToString()
                Dim totalPameranCost As Integer = 0
                If (Not IsNothing(oBabitProposal.BPPameran)) Then
                    If (Not IsNothing(oBabitProposal.BPPameran.PameranDisplays)) Then
                        For Each item As PameranDisplay In oBabitProposal.BPPameran.PameranDisplays
                            totalPameranCost += item.Others
                        Next
                    End If
                    totalPameranCost += oBabitProposal.BPPameran.Expense
                    lblJumlahPengajuan.Text = totalPameranCost.ToString("#,##0")
                End If
            End If

            If (oBabitProposal.TglTerimaEvidance <> "1/1/1753") Then
                intiCalTglEvi.Value = oBabitProposal.TglTerimaEvidance
                lblCalender.Text = oBabitProposal.TglTerimaEvidance.ToString("dd-MM-yyyy")
            End If

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                Dim objProposal As BabitProposal = CType(e.Item.DataItem, BabitProposal)

                If CType(objProposal.Status, EnumBabit.StatusBabitProposal) = EnumBabit.StatusBabitProposal.Disetujui Then
                    lbtnDelete.Visible = False
                End If
            End If
            'Else
            'Dim tCell As TableCell = New TableCell
            'Dim gridIklan As BPIklanGrid = CType(LoadControl("BPIklanGrid.ascx"), BPIklanGrid)
            'gridIklan.ID = String.Format("BPIklanGrid{0}", oBabitProposal.Status)
            ''diakalin krn id nya disimpan di status sbg flag kalo item ini adalah item untuk nested grid
            'gridIklan.BabitProposalID = oBabitProposal.Status
            'tCell.Controls.Add(gridIklan)

            ''Dim gView As DataGrid = CType(sender, DataGrid)
            'Dim colSpan As Integer = e.Item.Cells.Count - 1

            'e.Item.Cells.Clear()
            'tCell.Attributes("ColSpan") = colSpan.ToString()
            ''e.Item.Cells.Add(New TableCell)
            ''e.Item.Cells.Add(New TableCell)
            'e.Item.Cells.Add(tCell)
            'End If

        End If
    End Sub

    Private Sub dtgList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgList.PageIndexChanged
        dtgList.CurrentPageIndex = e.NewPageIndex
        BindGrid(dtgList.CurrentPageIndex)
    End Sub

    Private Sub dtgList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgList.SortCommand
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
        dtgList.CurrentPageIndex = 0
        BindGrid(dtgList.CurrentPageIndex)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        UpdateData("Save")
    End Sub

    Private Sub btnRilis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis.Click
        UpdateData("Rilis")
    End Sub

    Private Sub dtgList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgList.ItemCommand
        Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
        Dim facade As New BabitSalesComm.BabitProposalFacade(User)
        Dim objProposal As BabitProposal = facade.Retrieve(CInt(lblID.Text))
        Select Case e.CommandName
            Case "Link"
                SetSessionCriteria()
                Response.Redirect("../Babit/FrmPengajuanBabit.aspx?id=" & e.CommandArgument & "&Mode=View&Src=Pemotongan")
            Case "Delete"
                If CType(objProposal.Status, EnumBabit.StatusBabitProposal) = EnumBabit.StatusBabitProposal.Disetujui Then
                    MessageBox.Show("Data yang sudah dirilis tidak bisa dihapus.")
                    Return
                End If
                If (facade.Delete(objProposal) <> -1) Then
                    MessageBox.Show("Data berhasil dihapus")
                    BindGrid(0)
                Else
                    MessageBox.Show("Data tidak berhasil dihapus")
                End If
                'Case "UpdateBpIklan"
                '    Dim gridIklan As BPIklanGrid = CType(e.Item.FindControl("BPIklanGrid.ascx"), BPIklanGrid)
                '    gridIklan.UpdateBpIklan(objProposal.ID)
            Case "detail"
                BindGrid(dtgList.CurrentPageIndex)
        End Select
    End Sub

    Private Sub chkTanggalPengajuan_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTanggalPengajuan.CheckedChanged
        icDateFrom.Enabled = chkTanggalPengajuan.Checked
        icDateUntil.Enabled = chkTanggalPengajuan.Checked
    End Sub

    Private Sub chkTanggalPersetujuan_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTanggalPersetujuan.CheckedChanged
        icTglPersetujuanFrom.Enabled = chkTanggalPersetujuan.Checked
        icTglPersetujuanTo.Enabled = chkTanggalPersetujuan.Checked
    End Sub

    'Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Request.QueryString("src") = "ListProposalBabit" Then
    '        Response.Redirect("../BABIT/FrmListPengajuanBabit.aspx", True)
    '    End If
    'End Sub

#End Region

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim i As Integer = 0
    '    For Each item As DataGridItem In dtgList.Items
    '        Dim objProposal As BabitProposal = New BabitSalesComm.BabitProposalFacade(User).Retrieve(Convert.ToInt32(dtgList.DataKeys().Item(i)))
    '        Dim gridIklan As BPIklanGrid = CType(item.FindControl(String.Format("BPIklanGrid{0}", objProposal.ID)), BPIklanGrid)
    '        gridIklan.UpdateBpIklan(objProposal.ID)
    '        i += 1
    '    Next

    'End Sub

    'Private Sub dtgList_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgList.ItemCreated
    '    'If (e.Item.ItemType <> ListItemType.Item) Then Return

    '    'Dim arlTmp As ArrayList = CType(sessHelper.GetSession("ListTmp"), ArrayList)

    '    'If (e.Item.ItemIndex = 0) Then Return

    '    'Dim oBabitProposal As BabitProposal = CType(arlTmp(e.Item.ItemIndex - 1), BabitProposal)
    '    'If (IsNothing(oBabitProposal)) Then Return
    '    'If (oBabitProposal.ID <> 0) Then Return

    '    'Dim tCell As TableCell = New TableCell
    '    'Dim gridIklan As BPIklanGrid = CType(LoadControl("BPIklanGrid.ascx"), BPIklanGrid)
    '    'gridIklan.ID = String.Format("BPIklanGrid{0}", oBabitProposal.ID)
    '    ''diakalin krn id nya disimpan di status sbg flag kalo item ini adalah item untuk nested grid
    '    'gridIklan.BabitProposalID = oBabitProposal.Status
    '    'tCell.Controls.Add(gridIklan)

    '    ''Dim gView As DataGrid = CType(sender, DataGrid)
    '    'Dim colSpan As Integer = e.Item.Cells.Count - 1

    '    'e.Item.Cells.Clear()
    '    'tCell.Attributes("ColSpan") = colSpan.ToString()
    '    ''e.Item.Cells.Add(New TableCell)
    '    ''e.Item.Cells.Add(New TableCell)
    '    'e.Item.Cells.Add(tCell)

    '    'Dim gRow As DataGridItem = New DataGridItem(-1, -1, ListItemType.Item)

    '    'gRow.Cells.Add(New TableCell)
    '    'gRow.Cells.Add(New TableCell)
    '    'gRow.Cells.Add(tCell)
    '    'Dim tbl As Table = CType(e.Item.Parent, Table)

    '    'tbl.Controls.AddAt(gView.Controls(0).Controls.Count, gRow)

    'End Sub

End Class
