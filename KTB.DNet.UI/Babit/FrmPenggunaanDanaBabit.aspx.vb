Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.BabitSalesComm

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class FrmPenggunaanDanaBabit
    Inherits System.Web.UI.Page

    Private Class DetailPenggunaanDana
        'Private _Index As Integer
        Private _babitProposalID As Integer
        Private _Tanggal As DateTime
        Private _Debet As Decimal
        Private _Kredit As Decimal
        Private _Referensi As String
        Private _Keterangan As String

        'Public Property Index() As Integer
        '    Get
        '        Return _Index
        '    End Get
        '    Set(ByVal Value As Integer)
        '        _Index = Value
        '    End Set
        'End Property

        Public Property BabitProposalID() As Integer
            Get
                Return _babitProposalID
            End Get
            Set(ByVal Value As Integer)
                _babitProposalID = Value
            End Set
        End Property

        Public Property Debet() As Decimal
            Get
                Return _Debet
            End Get
            Set(ByVal Value As Decimal)
                _Debet = Value
            End Set
        End Property
        Public Property Kredit() As Decimal
            Get
                Return _Kredit
            End Get
            Set(ByVal Value As Decimal)
                _Kredit = Value
            End Set
        End Property
        Public Property Tanggal() As DateTime
            Get
                Return _Tanggal
            End Get
            Set(ByVal Value As DateTime)
                _Tanggal = Value
            End Set
        End Property
        Public Property Referensi() As String
            Get
                Return _Referensi
            End Get
            Set(ByVal Value As String)
                _Referensi = Value
            End Set
        End Property
        Public Property Keterangan() As String
            Get
                Return _Keterangan
            End Get
            Set(ByVal Value As String)
                _Keterangan = Value
            End Set
        End Property
    End Class

    Private Class PenggunaanDanaBabit
        Private _DanaAwal As Decimal
        Private _SisaDana As Decimal
        Private _Proposals As ArrayList
        Private _DanaTambahan As ArrayList


        Public Sub New()
            _DanaAwal = 0
            _Proposals = New ArrayList
            _DanaTambahan = New ArrayList
        End Sub


        Public Property Proposals() As ArrayList
            Get
                Return _Proposals
            End Get
            Set(ByVal Value As ArrayList)
                _Proposals = Value
            End Set
        End Property

        Public Property DanaTambahan() As ArrayList
            Get
                Return _DanaTambahan
            End Get
            Set(ByVal Value As ArrayList)
                _DanaTambahan = Value
            End Set
        End Property

        Public ReadOnly Property ListPenggunaanDana() As ArrayList
            Get
                Dim i As Integer = 0
                Dim previousAllovID As Integer = 0
                Dim NewArr As New ArrayList

                If _Proposals.Count > 0 Then
                    For Each item As BabitProposal In _Proposals
                        Dim NewItem As New DetailPenggunaanDana
                        'NewItem.Index = i
                        NewItem.Tanggal = item.TglTerimaEvidance
                        NewItem.Kredit = item.KTBApprovalAmount
                        NewItem.Keterangan = item.Description
                        NewItem.BabitProposalID = item.ID

                        If item.Status = EnumBabit.StatusBabitProposal.Disetujui Then
                            NewItem.Referensi = item.NoPersetujuan
                        Else
                            NewItem.Referensi = item.BabitAllocation.NoPerjanjian
                        End If

                        If item.BabitAllocation.Babit.AllocationType = EnumBabit.BabitAllocationType.Alokasi_Tambahan Then
                            If Not item.BabitAllocation.ID = previousAllovID Then
                                previousAllovID = item.BabitAllocation.ID
                                NewItem.Debet = item.BabitAllocation.DanaBabit
                                _DanaTambahan.Add(NewItem)
                            End If
                        Else
                            NewItem.Debet = 0
                            NewArr.Add(NewItem)
                        End If
                        i += 1
                    Next

                    Dim tda As Decimal = TotalDanaAwal
                    For Each item As DetailPenggunaanDana In NewArr
                        item.Debet = tda
                        'tda -= item.Debet
                    Next

                End If

                If _DanaTambahan.Count > 0 Then
                    For Each item As DetailPenggunaanDana In _DanaTambahan
                        NewArr.Add(item)
                    Next
                End If

                NewArr = KTB.DNet.Utility.CommonFunction.SortArraylist(NewArr, GetType(DetailPenggunaanDana), "Tanggal", Sort.SortDirection.ASC)

                Return NewArr
                'prepare list penggunaan 
            End Get
        End Property
        Public Property DanaAwal() As Decimal
            Get
                Return _DanaAwal
            End Get
            Set(ByVal Value As Decimal)
                _DanaAwal = Value
            End Set
        End Property
        Public ReadOnly Property SisaDana() As Decimal
            Get
                ' Calculate Sisa Dana
                Dim _SisaDana As Decimal = TotalDanaAwal - TotalDebet + TotalKredit
                Return _SisaDana
            End Get
        End Property
        Public ReadOnly Property TotalKredit() As Decimal
            Get
                ' Calculate Total Kredit
                Dim _TotalKredit As Decimal = 0
                If _Proposals.Count > 0 Then
                    Dim tmpArl As ArrayList = CommonFunction.SortArraylist(_Proposals, GetType(BabitProposal), "Dealer.ID", Sort.SortDirection.ASC)
                    Dim tmpDealerId As Integer = 0
                    'For Each item As BabitProposal In _Proposals
                    '    If (tmpDealerId <> item.Dealer.ID) Then
                    '        tmpDealerId = item.Dealer.ID
                    '        If (item.BabitAllocation.Babit.AllocationType = CInt(EnumBabit.BabitAllocationType.Alokasi_Tambahan)) Then
                    '            _TotalKredit += item.BabitAllocation.TotalDanaBabit(CInt(EnumBabit.BabitAllocationType.Alokasi_Tambahan))
                    '        End If
                    '    End If
                    'Next
                    For Each item As BabitProposal In _Proposals
                        If (item.BabitAllocation.Babit.AllocationType = CInt(EnumBabit.BabitAllocationType.Alokasi_Tambahan)) Then
                            If (tmpDealerId <> item.Dealer.ID) Then
                                tmpDealerId = item.Dealer.ID
                                _TotalKredit += item.BabitAllocation.TotalDanaBabit(CInt(EnumBabit.BabitAllocationType.Alokasi_Tambahan))
                            End If
                        End If
                    Next

                End If
                Return _TotalKredit
            End Get
        End Property

        Public ReadOnly Property TotalDanaAwal() As Decimal
            Get
                Dim _TotalDanaAwal As Decimal = 0
                If _Proposals.Count > 0 Then
                    Dim tmpArl As ArrayList = CommonFunction.SortArraylist(_Proposals, GetType(BabitProposal), "Dealer.ID", Sort.SortDirection.ASC)
                    Dim tmpDealerId As Integer = 0
                    For Each item As BabitProposal In _Proposals
                        If (tmpDealerId <> item.Dealer.ID) Then
                            tmpDealerId = item.Dealer.ID
                            _TotalDanaAwal += item.BabitAllocation.DanaBabit
                        End If
                    Next
                End If
                Return _TotalDanaAwal
            End Get
        End Property

        Public ReadOnly Property TotalDebet() As Decimal
            Get
                ' Calculate Total Debet
                Dim _TotalDebet As Decimal = 0
                'If _DanaTambahan.Count > 0 Then
                'For Each item As DetailPenggunaanDana In _DanaTambahan
                '    _TotalDebet += item.Debet
                'Next
                For Each item As BabitProposal In _Proposals
                    _TotalDebet += item.KTBApprovalAmount
                Next
                'End If

                Return _TotalDebet
            End Get
        End Property

    End Class

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMonthPeriodeFrom As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMonthPeriodeTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents dgAlokasiBabit As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlTipeBabit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlJenisAktivitas As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblDanaAwal As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaDana As System.Web.UI.WebControls.Label
    Protected WithEvents lblDebetDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblKreditDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblDebet As System.Web.UI.WebControls.Label
    Protected WithEvents lblKredit As System.Web.UI.WebControls.Label
    Protected WithEvents hfDealerName As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTahun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hdnValNew As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
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
    Private oDealer As Dealer
    Private oLoginUser As UserInfo
#End Region

#Region "PrivateCustomMethods"
    Private Sub Initialize()
        BindTipeBabit()
        BindJenisAktivitas()
        BindPeriode(ddlMonthPeriodeFrom)
        BindPeriode(ddlMonthPeriodeTo)
        BindTahun()
    End Sub
    Private Sub ClearData()
        txtDealerCode.Text = String.Empty
        ddlTipeBabit.SelectedIndex = 0
        ddlJenisAktivitas.SelectedValue = 0
        ddlMonthPeriodeFrom.SelectedValue = DateTime.Today.Month
        ddlMonthPeriodeTo.SelectedValue = DateTime.Today.Month
    End Sub
    Private Sub BindTipeBabit()
        ddlTipeBabit.Items.Clear()
        ddlTipeBabit.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlTipeBabit.Items.Add(New ListItem("Regular", EnumBabit.BabitType.Regular))
        ddlTipeBabit.Items.Add(New ListItem("Khusus", EnumBabit.BabitType.Khusus))
    End Sub
    Private Sub BindJenisAktivitas()
        ddlJenisAktivitas.Items.Clear()
        ddlJenisAktivitas.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlJenisAktivitas.Items.Add(New ListItem("Pameran", EnumBabit.BabitProposalType.Pameran))
        ddlJenisAktivitas.Items.Add(New ListItem("Iklan", EnumBabit.BabitProposalType.Iklan))
        ddlJenisAktivitas.Items.Add(New ListItem("Event", EnumBabit.BabitProposalType.Even))
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
    Private Sub BindTahun()
        ddlTahun.Items.Clear()
        ddlTahun.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahun.Items.Add(New ListItem(i.ToString, i))
        Next
        ddlTahunTo.Items.Clear()
        ddlTahunTo.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahunTo.Items.Add(New ListItem(i.ToString, i))
        Next
    End Sub
    Private Sub BindGrid()

        'Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'If ddlTipeBabit.SelectedValue = EnumBabit.BabitType.Regular Then
        '    criterias.opAnd(New Criteria(GetType(BabitProposal), "BabitAllocation.Babit.AllocationType", MatchType.InSet, "(" & CType(EnumBabit.BabitAllocationType.Alokasi_Awal, Short).ToString & "," & CType(EnumBabit.BabitAllocationType.Alokasi_Tambahan, Short).ToString & ")"))
        'Else
        '    criterias.opAnd(New Criteria(GetType(BabitProposal), "BabitAllocation.Babit.AllocationType", MatchType.Exact, CType(EnumBabit.BabitAllocationType.Babit_Khusus, Short)))
        'End If

        'If ddlJenisAktivitas.SelectedValue > -1 Then
        '    criterias.opAnd(New Criteria(GetType(BabitProposal), "ActivityType", MatchType.Exact, ddlJenisAktivitas.SelectedValue))
        'End If

        'criterias.opAnd(New Criteria(GetType(BabitProposal), "BabitAllocation.Babit.StartPeriod", MatchType.Exact, ddlMonthPeriodeFrom.SelectedValue))
        'criterias.opAnd(New Criteria(GetType(BabitProposal), "BabitAllocation.Babit.EndPeriod", MatchType.Exact, ddlMonthPeriodeTo.SelectedValue))

        'If Not txtDealerCode.Text = String.Empty Then
        '    criterias.opAnd(New Criteria(GetType(BabitProposal), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        'End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlTipeBabit.SelectedIndex <> 0 Then
            If ddlTipeBabit.SelectedValue = EnumBabit.BabitType.Regular Then
                criterias.opAnd(New Criteria(GetType(BabitAllocation), "Babit.AllocationType", MatchType.InSet, "(" & CType(EnumBabit.BabitAllocationType.Alokasi_Reguler, Short).ToString & "," & CType(EnumBabit.BabitAllocationType.Alokasi_Tambahan, Short).ToString & ")"))
            Else
                criterias.opAnd(New Criteria(GetType(BabitAllocation), "Babit.AllocationType", MatchType.Exact, CType(EnumBabit.BabitAllocationType.Babit_Khusus, Short)))
            End If
        End If

        'If ddlJenisAktivitas.SelectedValue > -1 Then
        '    criterias.opAnd(New Criteria(GetType(BabitAllocation), "ActivityType", MatchType.Exact, ddlJenisAktivitas.SelectedValue))
        'End If

        If (ddlMonthPeriodeFrom.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(BabitAllocation), "Babit.StartPeriod", MatchType.LesserOrEqual, ddlMonthPeriodeFrom.SelectedValue))
        End If
        'If (ddlMonthPeriodeTo.SelectedValue <> "-1") Then
        '    criterias.opAnd(New Criteria(GetType(BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, ddlMonthPeriodeTo.SelectedValue))
        'End If
        If ddlTahun.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYear", MatchType.LesserOrEqual, ddlTahun.SelectedValue))
        End If
        If ddlTahunTo.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYearEnd", MatchType.GreaterOrEqual, ddlTahunTo.SelectedValue))
        End If
        If ddlMonthPeriodeTo.SelectedValue <> -1 Then
            If (ddlMonthPeriodeFrom.SelectedValue = "-1") Or (ddlTahun.SelectedValue = "-1") Or (ddlTahunTo.SelectedValue = "-1") Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, ddlMonthPeriodeTo.SelectedValue))
            End If
        End If

        'If Not txtDealerCode.Text = String.Empty Then
        '    criterias.opAnd(New Criteria(GetType(BabitAllocation), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        'End If

        If (txtDealerCode.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If

        Dim al As New ArrayList
        al = New BabitFacade(User).RetrieveBabitAllocation(criterias, "Babit.AllocationType", Sort.SortDirection.ASC)

        Dim bfacade As New BabitSalesComm.BabitProposalFacade(User)
        If (ddlMonthPeriodeFrom.SelectedValue <> "-1" And ddlTahun.SelectedValue <> "-1" And ddlMonthPeriodeTo.SelectedValue <> "-1" And ddlTahunTo.SelectedValue <> "-1") Then
            Dim dtmFrom2 As DateTime = New DateTime(CInt(ddlTahun.SelectedValue), CInt(ddlMonthPeriodeFrom.SelectedValue), 1)
            Dim dtmTo2 As DateTime = New DateTime(CInt(ddlTahunTo.SelectedValue), CInt(ddlMonthPeriodeTo.SelectedValue), 1)
            Dim arl2 As ArrayList = bfacade.FilterBabitAllocationByPeriodeList(al, dtmFrom2, dtmTo2)
            al = New ArrayList
            al = arl2
            If IsNothing(al) Then
                al = New ArrayList
            End If
        End If

        Dim _PenggunaanDanaBabit As PenggunaanDanaBabit
        _PenggunaanDanaBabit = GenerateGrid(al)
        lblDanaAwal.Text = String.Format("Rp.{0}", _PenggunaanDanaBabit.TotalDanaAwal.ToString("#,##0"))

        lblDebet.Text = String.Format("Rp.{0}", _PenggunaanDanaBabit.TotalDebet.ToString("#,##0"))
        lblKredit.Text = String.Format("Rp.{0}", _PenggunaanDanaBabit.TotalKredit.ToString("#,##0"))

        lblSisaDana.Text = String.Format("Rp.{0}", _PenggunaanDanaBabit.SisaDana.ToString("#,##0"))

        dgAlokasiBabit.DataSource = _PenggunaanDanaBabit.ListPenggunaanDana
        dgAlokasiBabit.DataBind()

        If (IsNothing(_PenggunaanDanaBabit.ListPenggunaanDana) Or _PenggunaanDanaBabit.ListPenggunaanDana.Count <= 0) Then
            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Private Function GenerateGrid(ByVal DataSource As ArrayList) As PenggunaanDanaBabit
        'Dim NewArr As New ArrayList
        Dim arrDanaTambahan As New ArrayList
        Dim obj As New PenggunaanDanaBabit

        If DataSource.Count > 0 Then
            For Each item As BabitAllocation In DataSource
                If item.Babit.AllocationType = EnumBabit.BabitAllocationType.Alokasi_Reguler Or item.Babit.AllocationType = EnumBabit.BabitAllocationType.Babit_Khusus Then
                    obj.DanaAwal += item.DanaBabit
                ElseIf item.Babit.AllocationType = EnumBabit.BabitAllocationType.Alokasi_Tambahan Then
                    Dim AllocTambahan As New DetailPenggunaanDana
                    AllocTambahan.Tanggal = item.CreatedTime
                    AllocTambahan.Debet += item.DanaBabit
                    'obj.DanaTambahan.Add(AllocTambahan)
                End If

                If item.BabitProposals.Count > 0 Then

                    If ddlJenisAktivitas.SelectedValue = -1 Then
                        For Each _proposal As BabitProposal In item.ApprovedProposals
                            obj.Proposals.Add(_proposal)
                        Next
                    Else
                        For Each _proposal As BabitProposal In item.FilterApprovedProposals(ddlJenisAktivitas.SelectedValue)
                            obj.Proposals.Add(_proposal)
                        Next
                    End If
                End If

            Next
        End If

        Return obj

    End Function
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.BabitUsageView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Penggunaan Dana Babit")
        End If
    End Sub

    'Private Function blnCekListKhususPrivilege() As Boolean
    '    If Not SecurityProvider.Authorize(context.User, SR.BabitAllocationCreateBabitKhususEntry_Privilege) Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        InitiateAuthorization()
        If Not hfDealerName.Value = String.Empty Then
            lblDealerName.Text = hfDealerName.Value
        End If
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        If Not IsPostBack Then
            If oLoginUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                txtDealerCode.Text = ""
                lblDealerCode.Text = ""
                lblDealerName.Text = ""
                lblDealerCode.Visible = False
                txtDealerCode.Visible = True
                lblPopUpDealer.Visible = True
            Else
                txtDealerCode.Text = oLoginUser.Dealer.DealerCode
                lblDealerCode.Text = oLoginUser.Dealer.DealerCode
                lblDealerName.Text = oLoginUser.Dealer.DealerName
                lblDealerCode.Visible = True
                txtDealerCode.Visible = False
                lblPopUpDealer.Visible = False
            End If

            Initialize()
        End If
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        'If txtDealerCode.Text = String.Empty Then
        '    MessageBox.Show("Silakan Isi Dealer!")
        '    Return
        'Else
        '    Dim SelectedDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
        '    If SelectedDealer.ID > 0 Then
        '        lblDealerCode.Text = SelectedDealer.DealerCode
        '        lblDealerName.Text = SelectedDealer.SearchTerm1
        '    Else
        '        MessageBox.Show("Dealer yg di masukan tidak valid.!")
        '        Return
        '    End If
        'End If
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
        BindGrid()
    End Sub

    Private Sub dgAlokasiBabit_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlokasiBabit.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1

        End If
    End Sub
#End Region

    Private Sub dgAlokasiBabit_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAlokasiBabit.ItemCommand
        If (e.CommandName = "SaveDesc") Then
            Dim obpf As BabitProposalFacade = New BabitProposalFacade(User)
            Dim obj As BabitProposal = obpf.Retrieve(CInt(e.CommandArgument))
            Dim txtKeterangan As TextBox = CType(e.Item.FindControl("txtKeterangan"), TextBox)
            obj.Description = txtKeterangan.Text
            obpf.Update(obj)
            MessageBox.Show("Keterangan sudah disimpan")
        End If

    End Sub
End Class




