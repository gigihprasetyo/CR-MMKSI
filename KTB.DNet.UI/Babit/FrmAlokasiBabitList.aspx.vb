Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.BabitSalesComm

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.UI.Helper

Public Class FrmAlokasiBabitList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisAlokasi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPerjanjian As System.Web.UI.WebControls.TextBox
    Protected WithEvents lnkbtnPopUpNoPerjanjian As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMonthPeriodeFrom As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMonthPeriodeTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTahun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgAlokasiBabit As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnRelease As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTahunTo As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _BabitAlocation As BabitAllocation
    Private _BabitAlocationFacade As New BabitFacade(User)

    Private sessHelper As New SessionHelper
    Private _mode As enumMode.Mode
    Private objDealer As Dealer
#End Region

#Region "PrivateCustomMethods"

    Private Sub checkDealer(ByVal formLoad As Boolean)

        If Not objDealer Is Nothing Then
            If formLoad = True Then
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER.ToString Then
                    txtDealerCode.Text = objDealer.DealerCode
                    txtDealerCode.Enabled = False
                    lblPopUpDealer.Visible = False
                Else
                    txtDealerCode.Enabled = True
                    lblPopUpDealer.Visible = True
                End If
            Else
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER.ToString Then
                    dgAlokasiBabit.Columns(3).Visible = False
                    dgAlokasiBabit.Columns(4).Visible = False
                Else
                    dgAlokasiBabit.Columns(3).Visible = True
                    dgAlokasiBabit.Columns(4).Visible = True
                End If
            End If

        End If
    End Sub
    Private Sub Initialize()
        BindJenisAlokasi()
        BindPeriode(ddlMonthPeriodeFrom)
        BindPeriode(ddlMonthPeriodeTo)
        BindTahun()
    End Sub
    Private Sub ClearData()
        If objDealer.Title <> EnumDealerTittle.DealerTittle.DEALER.ToString Then
            txtDealerCode.Text = String.Empty
        End If
        txtNoPerjanjian.Text = String.Empty
        ddlJenisAlokasi.SelectedIndex = 0
        'ddlMonthPeriodeFrom.SelectedValue = DateTime.Today.Month
        'ddlMonthPeriodeTo.SelectedValue = DateTime.Today.Month
        'ddlTahun.SelectedValue = DateTime.Today.Year
        ddlMonthPeriodeFrom.SelectedValue = "-1"
        ddlMonthPeriodeTo.SelectedValue = "-1"
        ddlTahun.SelectedValue = "-1"
    End Sub
    Private Sub BindJenisAlokasi()
        ddlJenisAlokasi.Items.Clear()
        ddlJenisAlokasi.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlJenisAlokasi.Items.Add(New ListItem("Alokasi Reguler", CInt(EnumBabit.BabitAllocationType.Alokasi_Reguler).ToString()))
        ddlJenisAlokasi.Items.Add(New ListItem("Alokasi Tambahan", CInt(EnumBabit.BabitAllocationType.Alokasi_Tambahan).ToString()))
        ddlJenisAlokasi.Items.Add(New ListItem("Babit Khusus", CInt(EnumBabit.BabitAllocationType.Babit_Khusus).ToString()))
    End Sub
    Private Sub BindTahun()
        ddlTahun.Items.Clear()
        ddlTahun.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahun.Items.Add(New ListItem(i.ToString, i.ToString))
        Next
        ddlTahunTo.Items.Clear()
        ddlTahunTo.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahunTo.Items.Add(New ListItem(i.ToString, i.ToString))
        Next
    End Sub
    Private Sub BindPeriode(ByRef _control As DropDownList)
        _control.Items.Clear()
        _control.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        _control.Items.Add(New ListItem("Januari", "1"))
        _control.Items.Add(New ListItem("Februari", "2"))
        _control.Items.Add(New ListItem("Maret", "3"))
        _control.Items.Add(New ListItem("April", "4"))
        _control.Items.Add(New ListItem("Mei", "5"))
        _control.Items.Add(New ListItem("Juni", "6"))
        _control.Items.Add(New ListItem("Juli", "7"))
        _control.Items.Add(New ListItem("Agustus", "8"))
        _control.Items.Add(New ListItem("September", "9"))
        _control.Items.Add(New ListItem("Oktober", "10"))
        _control.Items.Add(New ListItem("November", "11"))
        _control.Items.Add(New ListItem("Desember", "12"))
    End Sub

    Public Sub bindStatus()
        ddlStatus.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlStatus.Items.Add(New ListItem("Validasi", CInt(EnumBabit.BabitAllocationStatus.Rilis).ToString()))
        ddlStatus.Items.Add(New ListItem("Belum Validasi", CInt(EnumBabit.BabitAllocationStatus.Baru).ToString()))
    End Sub

    Private Sub CreateCriteria()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'If Not txtDealerCode.Text = String.Empty Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','").Trim() & "')"))
        'End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB.ToString Then
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            End If
        Else
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtDealerCode.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.InSet, strCrit))
            End If
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER.ToString Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Status", MatchType.Exact, CType(EnumBabit.BabitAllocationStatus.Rilis, Short)))
        End If
        If (ddlStatus.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Short)))
        End If

        If txtNoPerjanjian.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "NoPerjanjian", MatchType.Exact, txtNoPerjanjian.Text))
        End If
        If ddlMonthPeriodeFrom.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.StartPeriod", MatchType.LesserOrEqual, ddlMonthPeriodeFrom.SelectedValue))
        End If
        'If ddlMonthPeriodeTo.SelectedValue <> -1 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.LesserOrEqual, ddlMonthPeriodeTo.SelectedValue))
        'End If
        If ddlTahun.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYear", MatchType.LesserOrEqual, ddlTahun.SelectedValue))
        End If
        If ddlTahunTo.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYearEnd", MatchType.GreaterOrEqual, ddlTahunTo.SelectedValue))
        End If
        If ddlJenisAlokasi.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.AllocationType", MatchType.Exact, ddlJenisAlokasi.SelectedValue))
        End If

        If ddlMonthPeriodeTo.SelectedValue <> "-1" Then
            If (ddlMonthPeriodeFrom.SelectedValue = "-1") Or (ddlTahun.SelectedValue = "-1") Or (ddlTahunTo.SelectedValue = "-1") Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, ddlMonthPeriodeTo.SelectedValue))
            End If
        End If

        sessHelper.SetSession("CRITERIAS", criterias)
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim TotalRow As Integer = 0
        '  objDealer = Session("DEALER")

        Dim arl As New ArrayList
        Dim arlT As New ArrayList
        arl = _BabitAlocationFacade.RetrieveBabitAllocationByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), pageIndex + 1, dgAlokasiBabit.PageSize, TotalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), String))

        Dim bfacade As New BabitSalesComm.BabitProposalFacade(User)
        If (ddlMonthPeriodeFrom.SelectedValue <> "-1" And ddlTahun.SelectedValue <> "-1" And ddlMonthPeriodeTo.SelectedValue <> "-1" And ddlTahunTo.SelectedValue <> "-1") Then
            Dim dtmFrom2 As DateTime = New DateTime(CInt(ddlTahun.SelectedValue), CInt(ddlMonthPeriodeFrom.SelectedValue), 1)
            Dim dtmTo2 As DateTime = New DateTime(CInt(ddlTahunTo.SelectedValue), CInt(ddlMonthPeriodeTo.SelectedValue), 1)
            Dim arl2 As ArrayList = bfacade.FilterBabitAllocationByPeriodeList(arl, dtmFrom2, dtmTo2)
            arl = New ArrayList
            arl = arl2
            If IsNothing(arl) Then
                arl = New ArrayList
            End If
        End If

        If (IsNothing(arl) Or arl.Count <= 0) Then
            MessageBox.Show("Data tidak ditemukan")
        End If

        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB.ToString Then
            btnRelease.Visible = False
            dgAlokasiBabit.Columns(0).Visible = False
        End If

        If pageIndex >= 0 Then
            dgAlokasiBabit.DataSource = arl
            dgAlokasiBabit.VirtualItemCount = TotalRow
            dgAlokasiBabit.DataBind()
            'If arl.Count = 0 Then
            '    MessageBox.Show("Data tidak ditemukan.")
            'End If
            'Else
            '    dgAlokasiBabit.DataSource = New ArrayList
            '    dgAlokasiBabit.DataBind()
            '    dgAlokasiBabit.VirtualItemCount = 0
            'MessageBox.Show("Kode dealer tidak valid.")
        End If
    End Sub

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtDealerCode.Text)
        arrLastState.Add(txtNoPerjanjian.Text)
        arrLastState.Add(ddlJenisAlokasi.SelectedIndex)
        arrLastState.Add(ddlMonthPeriodeFrom.SelectedIndex)
        arrLastState.Add(ddlMonthPeriodeTo.SelectedIndex)
        arrLastState.Add(ddlTahun.SelectedIndex)
        arrLastState.Add(dgAlokasiBabit.CurrentPageIndex)
        arrLastState.Add(CType(ViewState("CurrentSortColumn"), String))
        arrLastState.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessHelper.SetSession("BABITALLOCATIONSESSIONLASTSTATE", arrLastState)
    End Sub

    Private Sub GetSessionCriteria()
        Dim arrLastState As ArrayList = CType(sessHelper.GetSession("BABITALLOCATIONSESSIONLASTSTATE"), ArrayList)
        If Not arrLastState Is Nothing Then
            txtDealerCode.Text = arrLastState.Item(0).ToString
            txtNoPerjanjian.Text = arrLastState.Item(1).ToString
            ddlJenisAlokasi.SelectedIndex = CInt(arrLastState.Item(2))
            ddlMonthPeriodeFrom.SelectedIndex = CInt(arrLastState.Item(3))
            ddlMonthPeriodeTo.SelectedIndex = CInt(arrLastState.Item(4))
            ddlTahun.SelectedIndex = CInt(arrLastState.Item(5))
            dgAlokasiBabit.CurrentPageIndex = CInt(arrLastState.Item(6))
            ViewState("CurrentSortColumn") = arrLastState.Item(7)
            ViewState("CurrentSortDirect") = arrLastState.Item(8)
        Else
            ViewState("CurrentSortColumn") = "NoPerjanjian"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            dgAlokasiBabit.CurrentPageIndex = 0
        End If
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Alokasi Babit")
        End If
        'If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationListView_Privilege) Then
        '        Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Alokasi Babit")
        '    End If
        'Else
        '    If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitAlocationBabit_Privilege) Then
        '        Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Alokasi Babit")
        '    End If
        'End If
    End Sub

    Private Function CekCmdRilisPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationListRelease_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private blnAlocationBabit As Boolean

    Private Function CekAlocationBabitPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitAlocationBabit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "EventHandlers"


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objDealer = CType(sessHelper.GetSession("Dealer"), Dealer)
        InitiateAuthorization()
        blnAlocationBabit = CekAlocationBabitPrivilege()

        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"

        lnkbtnPopUpNoPerjanjian.Attributes("onClick") = "ShowPPAlocationSelection()"
        If Not IsPostBack Then
            checkDealer(True)
            Initialize()
            ClearData()
            bindStatus()
            GetSessionCriteria()
            CreateCriteria()
            'BindGrid(dgAlokasiBabit.CurrentPageIndex)
            ' add security
            If Not CekCmdRilisPrivilege() Then
                btnRelease.Enabled = False
            End If
        End If
    End Sub
    Private Sub btnRelease_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        Dim i As Integer = 0
        Dim arrList As ArrayList = New ArrayList
        Dim _BabitAlloc As BabitAllocation

        For Each item As DataGridItem In dgAlokasiBabit.Items
            Dim chk As CheckBox = CType(item.FindControl("chkSelection"), CheckBox)
            If (chk.Checked) Then
                _BabitAlloc = _BabitAlocationFacade.RetrieveBabitAllocation(CInt(dgAlokasiBabit.DataKeys().Item(i)))
                arrList.Add(_BabitAlloc)
            End If
            i = i + 1
        Next

        If (arrList.Count > 0) Then
            If _BabitAlloc Is Nothing Then
                MessageBox.Show("Silakan pilih assign seragam yg akan di Rilis ")
                Return
            Else
                If (_BabitAlocationFacade.RilisBabitAllocation(arrList) > 0) Then
                    MessageBox.Show("Proses Rilis berhasil")
                    BindGrid(dgAlokasiBabit.CurrentPageIndex)
                Else
                    MessageBox.Show("Proses Rilis gagal")
                End If
            End If

        Else
            MessageBox.Show("Tidak ada data yg di pilih.")
        End If

    End Sub
    Private Sub dgAlokasiBabit_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgAlokasiBabit.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        Dim TotalRow As Integer = 0
        Dim arl As New ArrayList

        If (CType(ViewState("CurrentSortColumn"), String) = "DanaBabit") Then
            Dim arlSort As ArrayList = _BabitAlocationFacade.RetrieveBabitAllocationByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), dgAlokasiBabit.CurrentPageIndex + 1, dgAlokasiBabit.PageSize, TotalRow, "ID", CType(ViewState("CurrentSortDirect"), String))
            arl = CommonFunction.SortArraylist(arlSort, GetType(BabitAllocation), ViewState("CurrentSortColumn").ToString, CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        ElseIf (CType(ViewState("CurrentSortColumn"), String) = "PenggunaanBabit") Then
            Dim arlSort As ArrayList = _BabitAlocationFacade.RetrieveBabitAllocationByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), dgAlokasiBabit.CurrentPageIndex + 1, dgAlokasiBabit.PageSize, TotalRow, "ID", CType(ViewState("CurrentSortDirect"), String))
            arl = CommonFunction.SortArraylist(arlSort, GetType(BabitAllocation), ViewState("CurrentSortColumn").ToString, CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        ElseIf (CType(ViewState("CurrentSortColumn"), String) = "SisaBabit") Then
            Dim arlSort As ArrayList = _BabitAlocationFacade.RetrieveBabitAllocationByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), dgAlokasiBabit.CurrentPageIndex + 1, dgAlokasiBabit.PageSize, TotalRow, "ID", CType(ViewState("CurrentSortDirect"), String))
            arl = CommonFunction.SortArraylist(arlSort, GetType(BabitAllocation), ViewState("CurrentSortColumn").ToString, CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        Else
            arl = _BabitAlocationFacade.RetrieveBabitAllocationByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), dgAlokasiBabit.CurrentPageIndex + 1, dgAlokasiBabit.PageSize, TotalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), String))
        End If

        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB.ToString Then
            btnRelease.Visible = False
            dgAlokasiBabit.Columns(0).Visible = False
        End If

        If dgAlokasiBabit.CurrentPageIndex >= 0 Then
            dgAlokasiBabit.DataSource = arl
            dgAlokasiBabit.VirtualItemCount = TotalRow
            dgAlokasiBabit.DataBind()
        End If
    End Sub
    Private Sub dgAlokasiBabit_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgAlokasiBabit.PageIndexChanged
        dgAlokasiBabit.CurrentPageIndex = e.NewPageIndex
        BindGrid(dgAlokasiBabit.CurrentPageIndex)
    End Sub
    Private Sub lnkbtnPopUpNoPerjanjian_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPopUpNoPerjanjian.Click
        Dim obj As BabitAllocation
        obj = _BabitAlocationFacade.RetrieveBabitAllocation(txtNoPerjanjian.Text)
        If Not obj Is Nothing And obj.ID > 0 Then
            ddlMonthPeriodeFrom.SelectedValue = obj.Babit.StartPeriod.ToString
            ddlMonthPeriodeTo.SelectedValue = obj.Babit.EndPeriod.ToString
            ddlTahun.SelectedValue = obj.Babit.BabitYear.ToString
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgAlokasiBabit.CurrentPageIndex = 0
        If (CInt(ddlTahun.SelectedValue) <> -1 And CInt(ddlTahunTo.SelectedValue) <> -1) Then
            If (CInt(ddlTahun.SelectedValue) = CInt(ddlTahunTo.SelectedValue)) Then
                If (CInt(ddlMonthPeriodeFrom.SelectedValue) <> -1 And CInt(ddlMonthPeriodeTo.SelectedValue) <> -1) Then
                    If CInt(ddlMonthPeriodeFrom.SelectedValue) > CInt(ddlMonthPeriodeTo.SelectedValue) Then
                        MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                        Exit Sub
                    End If
                End If
            ElseIf (CInt(ddlTahun.SelectedValue) > CInt(ddlTahunTo.SelectedValue)) Then
                MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                Exit Sub
            End If
        End If
        CreateCriteria()
        BindGrid(0)
    End Sub

    Private Sub dgAlokasiBabit_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlokasiBabit.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            checkDealer(False)
            Dim rowData As BabitAllocation = CType(e.Item.DataItem, BabitAllocation)

            e.Item.Cells(1).Text = CType(e.Item.ItemIndex + 1 + (dgAlokasiBabit.CurrentPageIndex * dgAlokasiBabit.PageSize), String)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblJenisAlokasi"), Label)

            Dim lnkbtnPopUp As LinkButton = CType(e.Item.FindControl("lnkbtnPopUp"), LinkButton)
            Dim lnkbtnProposal As LinkButton = CType(e.Item.FindControl("lnkbtnProposal"), LinkButton)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)

            If rowData.BabitProposals.Count > 0 Then
                lnkbtnPopUp.Visible = True
                lnkbtnProposal.Visible = True
            Else
                lnkbtnPopUp.Visible = False
                lnkbtnProposal.Visible = False
            End If

            lnkbtnPopUp.Attributes("onClick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpBabitAllocationProposal.aspx?id=" & rowData.ID, "", 500, 760, "ViewForm")
            lnkbtnEdit.Visible = False
            lnkbtnProposal.Visible = True

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB.ToString Then
                lnkbtnEdit.Visible = True
            End If

            Select Case CType(rowData.Babit.AllocationType, EnumBabit.BabitAllocationType)
                Case EnumBabit.BabitAllocationType.Alokasi_Reguler
                    lblStatus.Text = "Alokasi Reguler"
                Case EnumBabit.BabitAllocationType.Alokasi_Tambahan
                    lblStatus.Text = "Alokasi Tambahan"
                Case EnumBabit.BabitAllocationType.Babit_Khusus
                    lblStatus.Text = "Alokasi Babit Khusus"
                    lnkbtnProposal.Visible = False
            End Select

            Dim chkSelection As CheckBox = CType(e.Item.FindControl("chkSelection"), CheckBox)
            If rowData.Status = EnumBabit.BabitAllocationStatus.Rilis Then
                lnkbtnDelete.Visible = False
                chkSelection.Visible = False
                e.Item.BackColor = System.Drawing.Color.LightBlue
            End If

            '-- add security
            If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationListEditDet_Privilege) Then
                lnkbtnEdit.Visible = False
            End If

            Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
            If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationListViewDetail_Privilege) Then
                lnkbtnView.Visible = False
            End If

            If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationListViewDanaBabit_Privilege) Then
                lnkbtnPopUp.Visible = False
            End If

            ' secure dealer case
            'If Not blnAlocationBabit Then
            '    lnkbtnProposal.Visible = False
            'End If
        End If
    End Sub
    Private Sub dgAlokasiBabit_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAlokasiBabit.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                SetSessionCriteria()
                Server.Transfer("~/Babit/FrmAlokasiBabit.aspx?Mode=View&id=" & e.CommandArgument.ToString)
            Case "Edit"
                SetSessionCriteria()
                Server.Transfer("~/Babit/FrmAlokasiBabit.aspx?Mode=Edit&id=" & e.CommandArgument.ToString)
            Case "PopUp"
                'Server.Transfer("~/PopUp/PopUpBabitAllocationProposal.aspx?id=" & e.CommandArgument)
            Case "Proposal"
                SetSessionCriteria()
                Server.Transfer("~/Babit/FrmPengajuanBabit.aspx?Mode=NewFromAlloc&Src=ListAlokasi&idAlloc=" & e.CommandArgument.ToString)
            Case "Delete"
                '  _BabitAlocationFacade.Delete(_BabitAlocationFacade.RetrieveBabitAllocation(CInt(e.CommandArgument)).Babit)

                _BabitAlocationFacade.DeleteBabitAllocation(_BabitAlocationFacade.RetrieveBabitAllocation(CInt(e.CommandArgument)))
                BindGrid(0)
        End Select
    End Sub

#End Region

End Class

