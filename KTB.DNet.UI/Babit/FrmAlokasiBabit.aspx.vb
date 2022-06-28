Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.BabitSalesComm

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class FrmAlokasiBabit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSalesmanUniform As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnSelectSalesman As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dgAlokasiBabit As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPerjanjian As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTahun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnEntryAlokasi As System.Web.UI.WebControls.Button
    Protected WithEvents ddlMonthPeriodeFrom As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblMonthPeriodeFrom As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMonthPeriodeTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblMonthPeriodeTo As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisAlokasi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblJenisAlokasi As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoPerjanjian As System.Web.UI.WebControls.Label
    Protected WithEvents btnRelease As System.Web.UI.WebControls.Button
    Protected WithEvents lnkbtnPopUpNoPerjanjian As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents hdnValNew As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnValRelease As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlTahunTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnBack As System.Web.UI.HtmlControls.HtmlInputButton

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
#End Region

#Region "PrivateCustomMethods"
    Private Sub Initialize()

        txtDealerCode.Enabled = False
        lblPopUpDealer.Visible = False

        BindJenisAlokasi()
        BindPeriode(ddlMonthPeriodeFrom)
        BindPeriode(ddlMonthPeriodeTo)
        BindTahun()
    End Sub
    Private Sub ClearData()
        txtDealerCode.Text = String.Empty
        txtNoPerjanjian.Text = String.Empty
        ddlJenisAlokasi.SelectedIndex = 0
        ddlJenisAlokasi_SelectedIndexChanged(Me, System.EventArgs.Empty)
        ddlMonthPeriodeFrom.SelectedValue = -1
        ddlMonthPeriodeTo.SelectedValue = -1
        ddlTahun.SelectedValue = -1
    End Sub

    Private Sub BindJenisAlokasi()
        ddlJenisAlokasi.Items.Clear()
        ddlJenisAlokasi.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        If blnCekListRegulerPrivilege() Then
            ddlJenisAlokasi.Items.Add(New ListItem("Alokasi Reguler", EnumBabit.BabitAllocationType.Alokasi_Reguler))
        End If
        If blnCekListTambahanPrivilege() Then
            ddlJenisAlokasi.Items.Add(New ListItem("Alokasi Tambahan", EnumBabit.BabitAllocationType.Alokasi_Tambahan))
        End If
        If blnCekListKhususPrivilege() Then
            ddlJenisAlokasi.Items.Add(New ListItem("Babit Khusus", EnumBabit.BabitAllocationType.Babit_Khusus))
        End If
        ddlJenisAlokasi.SelectedIndex = 0
        ddlJenisAlokasi_SelectedIndexChanged(Me, System.EventArgs.Empty)

        If ddlJenisAlokasi.Items.Count <= 1 Then
            btnEntryAlokasi.Enabled = False
            btnSimpan.Enabled = False
        End If
    End Sub
    Private Sub BindTahun()
        ddlTahun.Items.Clear()
        'For i As Integer = DateTime.Today.Year To DateTime.Today.Year - 80 Step -1
        '    ddlTahun.Items.Add(New ListItem(i.ToString, i))
        'Next

        ddlTahun.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlTahun.Items.Add(New ListItem((DateTime.Today.Year + 1).ToString, DateTime.Today.Year + 1))
        ddlTahun.Items.Add(New ListItem(DateTime.Today.Year.ToString, DateTime.Today.Year))

        ddlTahunTo.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlTahunTo.Items.Add(New ListItem((DateTime.Today.Year + 1).ToString, DateTime.Today.Year + 1))
        ddlTahunTo.Items.Add(New ListItem(DateTime.Today.Year.ToString, DateTime.Today.Year))

        'ddlTahun.Items.Add(New ListItem((DateTime.Today.Year - 1).ToString, DateTime.Today.Year - 1))
    End Sub
    Private Sub BindPeriode(ByRef _control As DropDownList)
        _control.Items.Clear()
        _control.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        _control.Items.Add(New ListItem("Januari", 1))
        _control.Items.Add(New ListItem("Februari", 2))
        _control.Items.Add(New ListItem("Maret", 3))
        _control.Items.Add(New ListItem("April", 4))
        _control.Items.Add(New ListItem("Mei", 5))
        _control.Items.Add(New ListItem("Juni", 6))
        _control.Items.Add(New ListItem("Juli", 7))
        _control.Items.Add(New ListItem("Agustus", 8))
        _control.Items.Add(New ListItem("September", 9))
        _control.Items.Add(New ListItem("Oktober", 10))
        _control.Items.Add(New ListItem("November", 11))
        _control.Items.Add(New ListItem("Desember", 12))
    End Sub
    Private Sub SetFormView()
        _mode = CType(ViewState("Mode"), enumMode.Mode)
        If _mode = enumMode.Mode.NewItemMode Then
            txtDealerCode.Visible = True
            lblPopUpDealer.Visible = True
            lblDealerCode.Visible = False

            ddlJenisAlokasi.Visible = True
            lblJenisAlokasi.Visible = False

            txtNoPerjanjian.Visible = True
            lnkbtnPopUpNoPerjanjian.Visible = True
            lblNoPerjanjian.Visible = False

            ddlMonthPeriodeFrom.Visible = True
            ddlMonthPeriodeTo.Visible = True
            lblMonthPeriodeFrom.Visible = False
            lblMonthPeriodeTo.Visible = False

            ddlTahun.Visible = True
            ddlTahunTo.Visible = True
        ElseIf _mode = enumMode.Mode.EditMode Then
            txtDealerCode.Visible = False
            lblPopUpDealer.Visible = False
            lblDealerCode.Visible = True

            ddlJenisAlokasi.Visible = False
            lblJenisAlokasi.Visible = True

            ddlMonthPeriodeFrom.Visible = False
            ddlMonthPeriodeTo.Visible = False
            lblMonthPeriodeFrom.Visible = True
            lblMonthPeriodeTo.Visible = True

            ddlTahun.Visible = False
            ddlTahunTo.Visible = False

            'If ddlJenisAlokasi.SelectedValue = EnumBabit.BabitAllocationType.Alokasi_Tambahan Then
            '    txtNoPerjanjian.Visible = True
            '    lnkbtnPopUpNoPerjanjian.Visible = True
            '    lblNoPerjanjian.Visible = False

            '    ddlMonthPeriodeFrom.Visible = False
            '    ddlMonthPeriodeTo.Visible = False
            '    ddlTahun.Visible = False
            '    lblMonthPeriodeFrom.Visible = True
            '    lblMonthPeriodeTo.Visible = True
            '    lblTahun.Visible = True
            'Else
            txtNoPerjanjian.Visible = False
            lnkbtnPopUpNoPerjanjian.Visible = False
            lblNoPerjanjian.Visible = True

            'ddlMonthPeriodeFrom.Visible = True
            'ddlMonthPeriodeTo.Visible = True
            'ddlTahun.Visible = True

            'lblMonthPeriodeFrom.Visible = False
            'lblMonthPeriodeTo.Visible = False
            'lblTahun.Visible = False
            'End If

        ElseIf _mode = enumMode.Mode.ViewMode Then
            txtDealerCode.Visible = False
            lblPopUpDealer.Visible = False
            lblDealerCode.Visible = True

            ddlJenisAlokasi.Visible = False
            lblJenisAlokasi.Visible = True

            txtNoPerjanjian.Visible = False
            lnkbtnPopUpNoPerjanjian.Visible = False
            lblNoPerjanjian.Visible = True

            ddlMonthPeriodeFrom.Visible = False
            ddlMonthPeriodeTo.Visible = False
            lblMonthPeriodeFrom.Visible = True
            lblMonthPeriodeTo.Visible = True

            ddlTahun.Visible = False
            ddlTahunTo.Visible = False
        End If

        Select Case _mode
            Case enumMode.Mode.NewItemMode
                btnSimpan.Visible = False
                btnEntryAlokasi.Visible = True
                btnRelease.Visible = False
                btnBack.Visible = False
            Case enumMode.Mode.EditMode
                btnSimpan.Visible = True
                btnEntryAlokasi.Visible = False
                btnRelease.Visible = True
                btnBack.Visible = True
            Case enumMode.Mode.ViewMode
                btnSimpan.Visible = False
                btnEntryAlokasi.Visible = False
                btnRelease.Visible = False
                btnBack.Visible = True
        End Select
    End Sub
    Private Sub FillForm()
        _mode = CType(ViewState("Mode"), enumMode.Mode)

        If _mode = enumMode.Mode.NewItemMode Then
            ClearData()
        ElseIf _mode = enumMode.Mode.EditMode Or _mode = enumMode.Mode.ViewMode Then
            txtDealerCode.Text = _BabitAlocation.Dealer.DealerCode
            lblDealerCode.Text = _BabitAlocation.Dealer.DealerCode

            If ddlJenisAlokasi.Items.Count - 1 > 0 Then
                ddlJenisAlokasi.SelectedValue = _BabitAlocation.Babit.AllocationType
                lblJenisAlokasi.Text = ddlJenisAlokasi.SelectedItem.Text
            Else
                ddlJenisAlokasi.SelectedValue = -1
                If (_BabitAlocation.Babit.AllocationType = CInt(EnumBabit.BabitAllocationType.Alokasi_Reguler)) Then
                    lblJenisAlokasi.Text = "Alokasi Reguler"
                ElseIf (_BabitAlocation.Babit.AllocationType = CInt(EnumBabit.BabitAllocationType.Alokasi_Tambahan)) Then
                    lblJenisAlokasi.Text = "Alokasi Tambahan"
                ElseIf (_BabitAlocation.Babit.AllocationType = CInt(EnumBabit.BabitAllocationType.Babit_Khusus)) Then
                    lblJenisAlokasi.Text = "Babit Khusus"
                End If
            End If

            txtNoPerjanjian.Text = _BabitAlocation.NoPerjanjian
            lblNoPerjanjian.Text = _BabitAlocation.NoPerjanjian

            ddlMonthPeriodeFrom.SelectedValue = _BabitAlocation.Babit.StartPeriod
            ddlMonthPeriodeTo.SelectedValue = _BabitAlocation.Babit.EndPeriod
            ddlTahun.SelectedValue = _BabitAlocation.Babit.BabitYear
            ddlTahunTo.SelectedValue = _BabitAlocation.Babit.BabitYearEnd

            lblMonthPeriodeFrom.Text = String.Format("{0} {1}", ddlMonthPeriodeFrom.SelectedItem.Text, ddlTahun.SelectedItem.Text)
            lblMonthPeriodeTo.Text = String.Format("{0} {1}", ddlMonthPeriodeTo.SelectedItem.Text, ddlTahunTo.SelectedItem.Text)

        End If
        BindGrid()
    End Sub
    Private Function RefreshCollection() As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','").Trim() & "')"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Status", MatchType.Exact, CType(EnumBabit.BabitAllocationStatus.Baru, Short)))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.StartPeriod", MatchType.LesserOrEqual, ddlMonthPeriodeFrom.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, ddlMonthPeriodeTo.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYear", MatchType.LesserOrEqual, ddlTahun.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYearEnd", MatchType.GreaterOrEqual, ddlTahunTo.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.AllocationType", MatchType.Exact, ddlJenisAlokasi.SelectedValue))


        Dim arl As New ArrayList
        arl = _BabitAlocationFacade.RetrieveListBabitAllocation(criterias)

        If arl.Count > 0 Then
            Return arl
        Else
            Return New ArrayList
        End If
    End Function

    Private Sub BindGrid()
        dgAlokasiBabit.DataSource = CType(sessHelper.GetSession("BabitAlocationList"), ArrayList)
        dgAlokasiBabit.DataBind()
    End Sub
    Private Function validateEntryAlloc() As Boolean
        If txtDealerCode.Text = String.Empty Then
            MessageBox.Show("Dealer harus di isi")
            Return False
        End If

        If ddlJenisAlokasi.SelectedValue = EnumBabit.BabitAllocationType.Alokasi_Tambahan Then
            If txtNoPerjanjian.Text = String.Empty Then
                MessageBox.Show("No Perjanjian harus di isi utk tipe alokasi tambahan.")
                Return False
            End If
        End If

        If (CInt(ddlTahun.SelectedValue) > CInt(ddlTahunTo.SelectedValue)) Then
            MessageBox.Show("Tahun periode awal dari tidak bisa lebih besar dari tahun periode akhir")
            Return False
        ElseIf (CInt(ddlTahun.SelectedValue) = CInt(ddlTahunTo.SelectedValue)) Then
            If CInt(ddlMonthPeriodeFrom.SelectedValue) >= CInt(ddlMonthPeriodeTo.SelectedValue) Then
                MessageBox.Show("Bulan periode awal tidak boleh lebih besar atau sama dengan dari periode akhir.")
                Return False
            End If
        End If

        If (CInt(ddlTahun.SelectedValue) < DateTime.Now.Year) Then
            MessageBox.Show("Tahun periode tidak boleh lebih kecil dari tahun sekarang")
            Return False
        ElseIf (CInt(ddlTahun.SelectedValue) = DateTime.Now.Year And CInt(ddlTahunTo.SelectedValue) = DateTime.Now.Year) Then
            If (CInt(ddlMonthPeriodeFrom.SelectedValue) <= DateTime.Now.Month) Then
                If (CInt(ddlMonthPeriodeTo.SelectedValue) < DateTime.Now.Month) Then
                    MessageBox.Show("Periode tidak bisa lebih kecil dari tanggal sekarang")
                    Return False
                End If
            End If
        End If

        If Not ddlJenisAlokasi.SelectedValue = EnumBabit.BabitAllocationType.Alokasi_Tambahan Then
            Dim message As String
            If IsExistDealerAllocation(ddlJenisAlokasi.SelectedValue, txtDealerCode.Text, ddlMonthPeriodeFrom.SelectedValue, ddlMonthPeriodeTo.SelectedValue, ddlTahun.SelectedValue, message) Then
                MessageBox.Show("Dealer " & message & " telah ada alokasinya yg di buat di periode ini")
                Return False
            End If
        End If

        If (txtDealerCode.Text <> "") Then
            If ddlJenisAlokasi.SelectedValue = 1 Then
                Dim arlDealer As ArrayList
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, txtDealerCode.Text.Split(";")(0)))
                arlDealer = New DealerFacade(User).Retrieve(crit)
                If arlDealer.Count = 0 Then
                    MessageBox.Show("Dealer tidak ada")
                    Return False
                End If
            Else
                For Each item As String In txtDealerCode.Text.Split(";")
                    Dim arlDealer As ArrayList
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, item))
                    arlDealer = New DealerFacade(User).Retrieve(crit)
                    If arlDealer.Count = 0 Then
                        MessageBox.Show("Dealer " & item & " tidak ada")
                        Return False
                    End If
                Next
            End If
        End If

        Return True
    End Function

    Private Function IsExistDealerAllocation(ByVal AllocationType As Integer, ByVal sListDealer As String, ByVal PeriodeDari As Integer, ByVal PeriodeSampai As Integer, ByVal PeriodeYear As Integer, ByRef message As String)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitAllocation), "Dealer.DealerCode", MatchType.InSet, "('" & sListDealer.Trim.Replace(";", "','") & "')"))
        criterias.opAnd(New Criteria(GetType(BabitAllocation), "Babit.AllocationType", MatchType.Exact, AllocationType))
        criterias.opAnd(New Criteria(GetType(BabitAllocation), "Babit.BabitYear", MatchType.LesserOrEqual, PeriodeYear))
        criterias.opAnd(New Criteria(GetType(BabitAllocation), "Babit.BabitYearEnd", MatchType.GreaterOrEqual, PeriodeYear))

        Dim tmpArr As ArrayList
        tmpArr = _BabitAlocationFacade.RetrieveListBabitAllocation(criterias)

        If tmpArr.Count > 0 Then
            Dim dtmFrom1 As DateTime = New DateTime(CInt(ddlTahun.SelectedValue), CInt(ddlMonthPeriodeFrom.SelectedValue), 1)
            Dim dtmto1 As DateTime = New DateTime(CInt(ddlTahunTo.SelectedValue), CInt(ddlMonthPeriodeTo.SelectedValue), 1)
            For Each item As BabitAllocation In tmpArr
                Dim dtmFrom2 As DateTime = New DateTime(item.Babit.BabitYear, item.Babit.StartPeriod, 1)
                Dim dtmTo2 As DateTime = New DateTime(item.Babit.BabitYearEnd, item.Babit.EndPeriod, 1)
                If (dtmFrom2 <= dtmFrom1) And (dtmTo2 >= dtmto1) Then
                    message = item.Dealer.DealerCode & "-" & item.Dealer.DealerName
                    Return True
                End If
                'If PeriodeDari >= item.Babit.StartPeriod And PeriodeDari <= item.Babit.EndPeriod OrElse _
                '   PeriodeSampai >= item.Babit.StartPeriod And PeriodeSampai <= item.Babit.EndPeriod OrElse _
                '   PeriodeDari < item.Babit.StartPeriod And PeriodeSampai > item.Babit.EndPeriod Then
                '    message = item.Dealer.DealerCode & "-" & item.Dealer.DealerName
                '    Return True
                'End If
            Next
        End If
        Return False
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationListViewDetail_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Alokasi Babit")
            End If
        Else
            If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationViewEntry_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Alokasi Babit")
            End If
        End If
        
    End Sub

    Private Function blnCekListKhususPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationCreateBabitKhususEntry_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function blnCekListRegulerPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationCreateRegularEntry_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function blnCekListTambahanPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationCreateAdditionalBabitEntry_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()

        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        lnkbtnPopUpNoPerjanjian.Attributes("onClick") = "ShowPPAlocationSelection()"

        If Not IsPostBack Then
            Initialize()
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            If Request.Params("Mode").ToString() = "New" Then
                ViewState("Mode") = enumMode.Mode.NewItemMode
                _BabitAlocation = New BabitAllocation
                sessHelper.SetSession("BabitAlocation", _BabitAlocation)
                sessHelper.SetSession("BabitAlocationList", New ArrayList)
                lblTitle.Text = "BABIT - Entry Alokasi BABIT"
                btnBack.Visible = False
            ElseIf Request.Params("Mode").ToString() = "Edit" Then
                ViewState("Mode") = enumMode.Mode.EditMode
                _BabitAlocation = _BabitAlocationFacade.RetrieveBabitAllocation(CInt(Request.Params("id")))
                sessHelper.SetSession("BabitAlocation", _BabitAlocation)
                Dim al As New ArrayList
                al.Add(_BabitAlocation)
                sessHelper.SetSession("BabitAlocationList", al)
                lblTitle.Text = "BABIT - Edit Alokasi BABIT"
                btnBack.Visible = True
            ElseIf Request.Params("Mode").ToString() = "View" Then
                ViewState("Mode") = enumMode.Mode.ViewMode
                _BabitAlocation = _BabitAlocationFacade.RetrieveBabitAllocation(CInt(Request.Params("id")))
                sessHelper.SetSession("BabitAlocation", _BabitAlocation)
                Dim al As New ArrayList
                al.Add(_BabitAlocation)
                sessHelper.SetSession("BabitAlocationList", al)
                lblTitle.Text = "BABIT - Detil Alokasi BABIT"
                dgAlokasiBabit.Columns(0).Visible = False
                btnBack.Visible = True
            End If
            FillForm()
            SetFormView()
        Else
            If (Request.Form("hdnValNew") = "1") Then
                btnSimpan_Click(Nothing, Nothing)
            End If
            If (Request.Form("hdnValRelease") = "1") Then
                btnRelease_Click(Nothing, Nothing)
            End If
            hdnValNew.Value = "-1"
            hdnValRelease.Value = "-1"
            Return
        End If
    End Sub
    Private Sub ddlJenisAlokasi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlJenisAlokasi.SelectedIndexChanged

        If ddlJenisAlokasi.SelectedIndex < 1 Then
            txtDealerCode.Enabled = False
            lblPopUpDealer.Visible = False
        Else
            txtDealerCode.Enabled = True
            lblPopUpDealer.Visible = True
        End If

        If ddlJenisAlokasi.SelectedValue = EnumBabit.BabitAllocationType.Alokasi_Tambahan Then
            txtNoPerjanjian.Visible = True
            lnkbtnPopUpNoPerjanjian.Visible = True
            lblNoPerjanjian.Visible = False

            ddlMonthPeriodeFrom.Visible = False
            ddlMonthPeriodeTo.Visible = False
            ddlTahun.Visible = False
            ddlTahunTo.Visible = False
            lblMonthPeriodeFrom.Visible = True
            lblMonthPeriodeTo.Visible = True
        Else
            txtNoPerjanjian.Visible = False
            lnkbtnPopUpNoPerjanjian.Visible = False
            lblNoPerjanjian.Visible = True

            ddlMonthPeriodeFrom.Visible = True
            ddlMonthPeriodeTo.Visible = True
            ddlTahun.Visible = True
            ddlTahunTo.Visible = True

            lblMonthPeriodeFrom.Visible = False
            lblMonthPeriodeTo.Visible = False
        End If
    End Sub
    Private Sub lnkbtnPopUpNoPerjanjian_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPopUpNoPerjanjian.Click
        Dim obj As BabitAllocation
        obj = _BabitAlocationFacade.RetrieveBabitAllocation(txtNoPerjanjian.Text)
        If Not obj Is Nothing And obj.ID > 0 Then
            ddlMonthPeriodeFrom.SelectedValue = obj.Babit.StartPeriod
            ddlMonthPeriodeTo.SelectedValue = obj.Babit.EndPeriod
            ddlTahun.SelectedValue = obj.Babit.BabitYear
            ddlTahunTo.SelectedValue = obj.Babit.BabitYearEnd

            lblMonthPeriodeFrom.Text = New DateTime(obj.Babit.BabitYear, obj.Babit.StartPeriod, 1).ToString("MMM yyyy")
            lblMonthPeriodeTo.Text = New DateTime(obj.Babit.BabitYearEnd, obj.Babit.EndPeriod, 1).ToString("MMM yyyy")
        End If
    End Sub
    Private Sub btnEntryAlokasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEntryAlokasi.Click
        If ddlJenisAlokasi.SelectedValue = -1 Then
            MessageBox.Show("Jenis Alokasi harus diisi")
            Exit Sub
        End If

        If ddlJenisAlokasi.SelectedValue <> EnumBabit.BabitAllocationType.Alokasi_Tambahan Then
            If (ddlMonthPeriodeTo.SelectedValue = -1 Or ddlMonthPeriodeFrom.SelectedValue = -1) Then
                MessageBox.Show("Periode harus diisi")
                Exit Sub
            End If
            If (ddlTahun.SelectedValue = -1 Or ddlTahunTo.SelectedValue = -1) Then
                MessageBox.Show("Tahun harus diisi")
                Exit Sub
            End If
        End If

        If validateEntryAlloc() Then
            Dim arrBabitAlloc As New ArrayList
            Dim t_BabitAlloc As BabitAllocation
            Dim t_Babit As Babit

            t_Babit = New Babit
            t_Babit.AllocationType = ddlJenisAlokasi.SelectedValue
            t_Babit.StartPeriod = ddlMonthPeriodeFrom.SelectedValue
            t_Babit.EndPeriod = ddlMonthPeriodeTo.SelectedValue
            t_Babit.BabitYear = ddlTahun.SelectedValue
            t_Babit.BabitYearEnd = ddlTahunTo.SelectedValue
            t_Babit.MarkLoaded()

            If txtDealerCode.Text.Split(";").Length > 0 Then
                If ddlJenisAlokasi.SelectedValue = 1 Then
                    txtDealerCode.Text = txtDealerCode.Text.Split(";")(0)
                End If
                For i As Integer = 0 To txtDealerCode.Text.Split(";").Length - 1
                    t_BabitAlloc = New BabitAllocation
                    t_BabitAlloc.Babit = t_Babit
                    t_BabitAlloc.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Split(";")(i))
                    t_BabitAlloc.NoPerjanjian = t_BabitAlloc.Dealer.DealerCode & ";" & t_BabitAlloc.Babit.AllocationType.ToString
                    t_BabitAlloc.ReffNoPerjanjian = txtNoPerjanjian.Text
                    arrBabitAlloc.Add(t_BabitAlloc)
                Next
            End If
            sessHelper.SetSession("BabitAlocationList", arrBabitAlloc)
            If arrBabitAlloc.Count > 0 Then
                btnSimpan.Visible = True
            End If
        Else
            sessHelper.SetSession("BabitAlocationList", New ArrayList)
            btnSimpan.Visible = False
        End If

        BindGrid()
    End Sub

    Private Sub dgAlokasiBabit_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlokasiBabit.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1
            Dim rowvalue As BabitAllocation = CType(e.Item.DataItem, BabitAllocation)
            Dim txtPC As TextBox = CType(e.Item.FindControl("txtPC"), TextBox)
            Dim txtCV As TextBox = CType(e.Item.FindControl("txtCV"), TextBox)
            Dim txtLCV As TextBox = CType(e.Item.FindControl("txtLCV"), TextBox)
            Dim lblPC As Label = CType(e.Item.FindControl("lblPC"), Label)
            Dim lblCV As Label = CType(e.Item.FindControl("lblCV"), Label)
            Dim lblLCV As Label = CType(e.Item.FindControl("lblLCV"), Label)
            Dim lblNOP As Label = CType(e.Item.FindControl("lblNOP"), Label)

            Dim lblDanaBabit As Label = CType(e.Item.FindControl("lblDanaBabit"), Label)
            txtPC.Attributes.Add("onKeyUp", "pic(this,this.value,'9999999999','N');CalculateDanaBabit('" & txtPC.ClientID & "','" & txtCV.ClientID & "','" & txtLCV.ClientID & "','" & lblDanaBabit.ClientID & "');")
            txtCV.Attributes.Add("onKeyUp", "pic(this,this.value,'9999999999','N');CalculateDanaBabit('" & txtPC.ClientID & "','" & txtCV.ClientID & "','" & txtLCV.ClientID & "','" & lblDanaBabit.ClientID & "');")
            txtLCV.Attributes.Add("onKeyUp", "pic(this,this.value,'9999999999','N');CalculateDanaBabit('" & txtPC.ClientID & "','" & txtCV.ClientID & "','" & txtLCV.ClientID & "','" & lblDanaBabit.ClientID & "');")
            If rowvalue.ID > 0 Then
                lblNOP.Text = rowvalue.NoPerjanjian
            Else
                lblNOP.Text = ""
            End If
            _mode = CType(ViewState("Mode"), enumMode.Mode)
            If _mode = enumMode.Mode.ViewMode Then
                lblCV.Visible = True
                lblPC.Visible = True
                lblLCV.Visible = True

                txtCV.Visible = False
                txtPC.Visible = False
                txtLCV.Visible = False
            Else
                lblCV.Visible = False
                lblPC.Visible = False
                lblLCV.Visible = False

                txtCV.Visible = True
                txtPC.Visible = True
                txtLCV.Visible = True
            End If

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

        dgAlokasiBabit.DataSource = CommonFunction.SortArraylist(CType(Session("BabitAlocationList"), ArrayList), _
                    GetType(BabitAllocation), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
        dgAlokasiBabit.DataBind()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If (hdnValNew.Value = "-1") Then
            MessageBox.Confirm("Yakin ingin save ?", "hdnValNew")
            Return
        End If

        If ddlJenisAlokasi.SelectedValue = "-1" Then
            MessageBox.Show("Jenis Alokasi belum dipilih.")
            Return
        End If

        If Not validateEntryAlloc() Then
            Return
        End If

        Dim result As Integer
        Dim arrAlloc As ArrayList = CType(sessHelper.GetSession("BabitAlocationList"), ArrayList)
        If arrAlloc.Count > 0 Then
            For Each item As DataGridItem In dgAlokasiBabit.Items
                Dim txtPC As TextBox = CType(item.FindControl("txtPC"), TextBox)
                Dim txtCV As TextBox = CType(item.FindControl("txtCV"), TextBox)
                Dim txtLCV As TextBox = CType(item.FindControl("txtLCV"), TextBox)

                Dim obj As BabitAllocation
                obj = CType(arrAlloc(item.ItemIndex), BabitAllocation)
                If txtPC.Text = "" Then
                    obj.PC = 0
                Else
                    If CDec(txtPC.Text) < 0 Then
                        MessageBox.Show("Nilai PC tidak boleh Minus")
                        Exit Sub
                    ElseIf CDec(txtPC.Text) > 999999999999999999 Then
                        MessageBox.Show("Nilai PC telah melebihi batas maksimal yg di perbolehkan")
                        Exit Sub
                    Else
                        obj.PC = CDec(txtPC.Text)
                    End If
                End If

                If txtCV.Text = "" Then
                    obj.CV = 0
                Else
                    If CDec(txtCV.Text) < 0 Then
                        MessageBox.Show("Nilai CV tidak boleh Minus")
                        Exit Sub
                    ElseIf CDec(txtCV.Text) > 999999999999999999 Then
                        MessageBox.Show("Nilai CV telah melebihi batas maksimal yg di perbolehkan")
                        Exit Sub
                    Else
                        obj.CV = CDec(txtCV.Text)
                    End If
                End If

                If txtLCV.Text = "" Then
                    obj.LCV = 0
                Else
                    If CDec(txtLCV.Text) < 0 Then
                        MessageBox.Show("Nilai LCV tidak boleh Minus")
                        Exit Sub
                    ElseIf CDec(txtLCV.Text) > 999999999999999999 Then
                        MessageBox.Show("Nilai LCV telah melebihi batas maksimal yg di perbolehkan")
                        Exit Sub
                    Else
                        obj.LCV = CDec(txtLCV.Text)
                    End If
                End If
            Next

            _mode = CType(ViewState("Mode"), enumMode.Mode)
            If _mode = enumMode.Mode.NewItemMode Then
                result = _BabitAlocationFacade.InsertTransactionBabitAllocation(arrAlloc)
            ElseIf _mode = enumMode.Mode.EditMode Then
                result = _BabitAlocationFacade.UpdateTransactionBabitAllocation(arrAlloc)
            End If

            If result > 0 Then
                If Request.Params("Mode").ToString() = "Edit" Then
                    _BabitAlocation = _BabitAlocationFacade.RetrieveBabitAllocation(CInt(Request.Params("id")))
                    sessHelper.SetSession("BabitAlocation", _BabitAlocation)
                    Dim al As New ArrayList
                    al.Add(_BabitAlocation)
                    sessHelper.SetSession("BabitAlocationList", al)
                    ViewState("Mode") = enumMode.Mode.EditMode
                    _BabitAlocation = CType(CType(sessHelper.GetSession("BabitAlocationList"), ArrayList)(0), BabitAllocation)
                    FillForm()
                    SetFormView()
                Else
                    sessHelper.SetSession("BabitAlocationList", RefreshCollection())
                    ViewState("Mode") = enumMode.Mode.EditMode
                    _BabitAlocation = CType(CType(sessHelper.GetSession("BabitAlocationList"), ArrayList)(0), BabitAllocation)
                    FillForm()
                    SetFormView()
                    btnBack.Visible = False
                End If

                MessageBox.Show("Data Berhasil Disimpan!")
            Else
                MessageBox.Show("Save Fail!")
            End If
        End If
    End Sub

    Private Sub btnRelease_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        If (hdnValRelease.Value = "-1") Then
            MessageBox.Confirm("Yakin ingin rilis ?", "hdnValRelease")
            Return
        End If

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
                    BindGrid()
                Else
                    MessageBox.Show("Proses Rilis gagal")
                End If
            End If

        Else
            MessageBox.Show("Tidak ada data yg di pilih.")
        End If

    End Sub

    'Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Server.Transfer("~/Babit/FrmAlokasiBabitList.aspx")
    'End Sub

#End Region

End Class

