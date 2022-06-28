#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports System.IO
#End Region

Public Class frmEquipmentOrderList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnConfirm As System.Web.UI.WebControls.Button
    Protected WithEvents btnTidakSetuju As System.Web.UI.WebControls.Button
    Protected WithEvents btnSetuju As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEqSalesHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblNoP3B As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorP3B As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblkodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenis As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenis As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents TROperator As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents btnBatalRilis As System.Web.UI.WebControls.Button
    Protected WithEvents icTanggalP3BAwal As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents IcTanggalP3BAkhir As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents lblTotalHarga As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents btnTransferUlang As System.Web.UI.WebControls.Button
    Protected WithEvents chkTanggalP3B As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkTanggalValidasi As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents icTglValidFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents IcTglValidTo As KTB.DNet.WebCC.IntiCalendar

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
    Private objEquipmentSalesHeader As EquipmentSalesHeader
    Private ArlEquipmentSalesHeader As ArrayList
    Private sessionHelper As New SessionHelper
    Private TotalHarga As Double
    Private objDealer As Dealer
#End Region

#Region "CustomMethod"
    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(txtKodeDealer.Text)
        objSSPO.Add(txtNomorP3B.Text)
        objSSPO.Add(lboxStatus.SelectedIndex)
        objSSPO.Add(ddlJenis.SelectedIndex)
        objSSPO.Add(chkTanggalP3B.Checked)
        objSSPO.Add(icTanggalP3BAwal.Value)
        objSSPO.Add(IcTanggalP3BAkhir.Value)
        objSSPO.Add(chkTanggalValidasi.Checked)
        objSSPO.Add(icTglValidFrom.Value)
        objSSPO.Add(IcTglValidTo.Value)
        objSSPO.Add(dtgEqSalesHeader.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessionHelper.SetSession("SESSIONEQORDERLIST", objSSPO)
    End Sub

    Private Sub GetSessionCriteria()
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONEQORDERLIST")
        If Not objSSPO Is Nothing Then
            txtKodeDealer.Text = objSSPO.Item(0)
            txtNomorP3B.Text = objSSPO.Item(1)
            lboxStatus.SelectedIndex = objSSPO.Item(2)
            ddlJenis.SelectedIndex = objSSPO.Item(3)
            chkTanggalP3B.Checked = objSSPO.Item(4)
            icTanggalP3BAwal.Value = objSSPO.Item(5)
            IcTanggalP3BAkhir.Value = objSSPO.Item(6)
            chkTanggalValidasi.Checked = objSSPO.Item(7)
            icTglValidFrom.Value = objSSPO.Item(8)
            IcTglValidTo.Value = objSSPO.Item(9)
            dtgEqSalesHeader.CurrentPageIndex = objSSPO.Item(10)
            ViewState("CurrentSortColumn") = objSSPO.Item(11)
            ViewState("CurrentSortDirect") = objSSPO.Item(12)
        End If
    End Sub
    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub RecordStatusChangeHistory(ByVal item As EquipmentSalesHeader, ByVal oldStatus As Integer)
        Dim objStatusChangeHistoryFacade As StatusChangeHistoryFacade

        'For Each item As EquipmentSalesHeader In arrListPE
        'If item.Status = EquipmentStatus.EquipmentStatusEnum.Setuju Or item.Status = EquipmentStatus.EquipmentStatusEnum.Tidak_Setuju Then
        '    If item.Validate2By <> String.Empty Then
        '        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), item.RegPONumber, EquipmentStatus.EquipmentStatusEnum.Rilis2, item.Status)
        '    Else
        '        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), item.RegPONumber, EquipmentStatus.EquipmentStatusEnum.Rilis1, item.Status)
        '    End If
        'Else
        If Not item Is Nothing Then
            objStatusChangeHistoryFacade = New StatusChangeHistoryFacade(User)
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), item.RegPONumber, oldStatus, item.Status)
        End If
        'End If
        'Next
    End Sub

    Private Sub BindToDropDownList()
        Dim ListitemBlank As ListItem
        ListitemBlank = New ListItem("Silahkan Pilih", -1)

        '--Bind To listBox
        lboxStatus.DataSource = EquipmentStatus.RetrieveEquipmentStatus()
        lboxStatus.DataTextField = "NameStatus"
        lboxStatus.DataValueField = "ValStatus"
        lboxStatus.DataBind()
        lboxStatus.SelectedIndex = -1

        '--DropDownList Jenis
        Me.ddlJenis.DataSource = EquipmentKind.RetrieveEquipmentKind()
        Me.ddlJenis.DataTextField = "NameStatus"
        Me.ddlJenis.DataValueField = "valStatus"
        Me.ddlJenis.DataBind()
        Me.ddlJenis.Items.Insert(0, ListitemBlank)

        '--DropDownList Status
        ddlStatus.DataSource = EquipmentStatus.RetrieveEquipmentProses()
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, ListitemBlank)

        If Not SecurityProvider.Authorize(Context.User, SR.P3BResponseReject_Privilege) Then
            ddlStatus.Items.RemoveAt(9)
            ddlStatus.Items.RemoveAt(8)
        End If
        'If Not SecurityProvider.Authorize(Context.User, SR.P3BListDisApprove_Privilege) Then
        '    ddlStatus.Items.RemoveAt(9)
        'End If
        'If Not SecurityProvider.Authorize(Context.User, SR.P3BListApprove_Privilege) Then
        '    ddlStatus.Items.RemoveAt(8)
        'End If
        If Not SecurityProvider.Authorize(Context.User, SR.P3BResponseRelease1_Privilege) AndAlso Not SecurityProvider.Authorize(Context.User, SR.P3BResponseRelease2_Privilege) Then
            ddlStatus.Items.RemoveAt(7)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.P3BResponseRelease2_Privilege) Then
            ddlStatus.Items.RemoveAt(6)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.P3BResponseRelease1_Privilege) Then
            ddlStatus.Items.RemoveAt(5)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.P3BResponseConfirmation_Privilege) Then
            ddlStatus.Items.RemoveAt(4)
            ddlStatus.Items.RemoveAt(3)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.P3BListValidate_Privilege) Then
            ddlStatus.Items.RemoveAt(2)
            ddlStatus.Items.RemoveAt(1)
        End If



    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "RegPONumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        TROperator.Visible = False
        icTanggalP3BAwal.Value = DateTime.Now.AddMonths(-1)
        icTglValidFrom.Value = DateTime.Now.AddMonths(-1)
    End Sub

    Private Sub BindGrid()
        BindDataToGrid(dtgEqSalesHeader.CurrentPageIndex)
    End Sub

    Private Sub BindDataToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '--Get Criterias From TextBox
        If txtNomorP3B.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "PONumber", MatchType.Exact, txtNomorP3B.Text))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        '--Get Criterias From DropDownList
        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
        End If
        If ddlJenis.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "Kind", MatchType.Exact, ddlJenis.SelectedValue))
        End If

        '--Get Criterias From Calendar
        If chkTanggalP3B.Checked OrElse chkTanggalValidasi.Checked Then
            If chkTanggalP3B.Checked Then
                If CType(icTanggalP3BAwal.Value, Date) <= CType(IcTanggalP3BAkhir.Value, Date) Then
                    Dim TanggalAwal As New DateTime(CInt(icTanggalP3BAwal.Value.Year), CInt(icTanggalP3BAwal.Value.Month), CInt(icTanggalP3BAwal.Value.Day), 0, 0, 0)
                    Dim TanggalAkhir As New DateTime(CInt(IcTanggalP3BAkhir.Value.Year), CInt(IcTanggalP3BAkhir.Value.Month), CInt(IcTanggalP3BAkhir.Value.Day), 23, 59, 59)
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "CreatedTime", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "CreatedTime", MatchType.LesserOrEqual, Format(TanggalAkhir, "yyyy-MM-dd HH:mm:ss")))
                End If
            End If
            If chkTanggalValidasi.Checked Then
                If CType(icTglValidFrom.Value, Date) <= CType(IcTglValidTo.Value, Date) Then
                    Dim TglAwal As New DateTime(CInt(icTglValidFrom.Value.Year), CInt(icTglValidFrom.Value.Month), CInt(icTglValidFrom.Value.Day), 0, 0, 0)
                    Dim TglAkhir As New DateTime(CInt(IcTglValidTo.Value.Year), CInt(IcTglValidTo.Value.Month), CInt(IcTglValidTo.Value.Day), 23, 59, 59)
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "ValidateDate", MatchType.GreaterOrEqual, Format(TglAwal, "yyyy-MM-dd HH:mm:ss")))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "ValidateDate", MatchType.LesserOrEqual, Format(TglAkhir, "yyyy-MM-dd HH:mm:ss")))
                End If
            End If

            ArlEquipmentSalesHeader = New EquipmentSalesHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgEqSalesHeader.PageSize, _
                   total, CType(ViewState("CurrentSortColumn"), String), _
                   CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgEqSalesHeader.DataSource = ArlEquipmentSalesHeader
            dtgEqSalesHeader.VirtualItemCount = total
            dtgEqSalesHeader.DataBind()
            lblTotalHargaValue.Text = "Rp " & FormatNumber(TotalHarga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            TROperator.Visible = True
        Else
            MessageBox.Show("Harus Memilih Tanggal")
        End If
    End Sub

    'Private Function PopulateEquipmentValidate() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkItem As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New EquipmentStatus
    '    Dim objEquipmentSalesFacade As New EquipmentSalesHeaderFacade(User)

    '    For Each oDataGridItem In dtgEqSalesHeader.Items
    '        chkItem = oDataGridItem.FindControl("chkItem")
    '        If chkItem.Checked Then
    '            Dim _eq As New KTB.Dnet.Domain.EquipmentSalesHeader
    '            _eq.ID = oDataGridItem.Cells(0).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _eq.Status = oDataGridItem.Cells(15).Text 'CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _eq.Status = EquipmentStatus.EquipmentStatusEnum.Baru Then 'enumStatusPK.Status.Baru Then
    '                _eq = objEquipmentSalesFacade.Retrieve(_eq.ID)
    '                _eq.Status = status.EquipmentStatusEnum.Validasi
    '                oExArgs.Add(_eq)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulateEquipmentConfirm() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkItem As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New EquipmentStatus
    '    Dim objEquipmentSalesFacade As New EquipmentSalesHeaderFacade(User)

    '    For Each oDataGridItem In dtgEqSalesHeader.Items
    '        chkItem = oDataGridItem.FindControl("chkItem")
    '        If chkItem.Checked Then
    '            Dim _eq As New KTB.Dnet.Domain.EquipmentSalesHeader
    '            _eq.ID = oDataGridItem.Cells(0).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _eq.Status = oDataGridItem.Cells(15).Text 'CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _eq.Status = EquipmentStatus.EquipmentStatusEnum.Validasi Then  'enumStatusPK.Status.Baru Then
    '                _eq = objEquipmentSalesFacade.Retrieve(_eq.ID)
    '                _eq.Status = status.EquipmentStatusEnum.Konfirmasi
    '                oExArgs.Add(_eq)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulateEquipmentAgree() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkItem As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New EquipmentStatus
    '    Dim objEquipmentSalesFacade As New EquipmentSalesHeaderFacade(User)

    '    For Each oDataGridItem In dtgEqSalesHeader.Items
    '        chkItem = oDataGridItem.FindControl("chkItem")
    '        If chkItem.Checked Then
    '            Dim _eq As New KTB.Dnet.Domain.EquipmentSalesHeader
    '            _eq.ID = oDataGridItem.Cells(0).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _eq.Status = oDataGridItem.Cells(15).Text 'CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _eq.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 OrElse _eq.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then    'enumStatusPK.Status.Baru Then
    '                _eq = objEquipmentSalesFacade.Retrieve(_eq.ID)
    '                _eq.Status = status.EquipmentStatusEnum.Setuju
    '                oExArgs.Add(_eq)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulateEquipmentDisAgree() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkItem As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New EquipmentStatus
    '    Dim objEquipmentSalesFacade As New EquipmentSalesHeaderFacade(User)

    '    For Each oDataGridItem In dtgEqSalesHeader.Items
    '        chkItem = oDataGridItem.FindControl("chkItem")
    '        If chkItem.Checked Then
    '            Dim _eq As New KTB.Dnet.Domain.EquipmentSalesHeader
    '            _eq.ID = oDataGridItem.Cells(0).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _eq.Status = oDataGridItem.Cells(15).Text 'CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _eq.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 OrElse _eq.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then    'enumStatusPK.Status.Baru Then
    '                _eq = objEquipmentSalesFacade.Retrieve(_eq.ID)
    '                _eq.Status = status.EquipmentStatusEnum.Tidak_Setuju
    '                oExArgs.Add(_eq)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ActivateUserPrivilege()
            BindToDropDownList()
            InitiatePage()
            GetInformation()
            GetSessionCriteria()

            BindGrid()
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        '------Comment By Sony------
        btnSetuju.Visible = False
        btnTidakSetuju.Visible = False
        btnValidasi.Visible = False
        '---------------------------
    End Sub
    '--Check User Privilege---
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.P3BListView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar P3B")
        End If
        btnSetuju.Visible = SecurityProvider.Authorize(Context.User, SR.P3BListApprove_Privilege)
        btnTidakSetuju.Visible = SecurityProvider.Authorize(Context.User, SR.P3BListDisApprove_Privilege)
        btnValidasi.Visible = SecurityProvider.Authorize(Context.User, SR.P3BListValidate_Privilege)
        btnTransferUlang.Visible = SecurityProvider.Authorize(Context.User, SR.P3BResponseConfirmation_Privilege)
    End Sub

    Private Sub GetInformation()
        If Request.QueryString("TglP3BAwal") <> String.Empty Then
            txtNomorP3B.Text = Request.QueryString("NoP3B")
            icTanggalP3BAwal.Value = Request.QueryString("TglP3BAwal")
            IcTanggalP3BAkhir.Value = Request.QueryString("TglP3BAkhir")
            txtKodeDealer.Text = Request.QueryString("Dealer")
            BindToListBox(Request.QueryString("Status"))
            ddlJenis.SelectedValue = Request.QueryString("Jenis")
            BindGrid()
        End If

    End Sub

    Private Sub BindToListBox(ByVal stringStatus As String)
        Dim str As String() = stringStatus.Split(",")
        For i As Integer = 0 To str.Length - 1
            For Each item As ListItem In lboxStatus.Items
                If item.Value.ToString = str(i) Then
                    item.Selected = True
                End If
            Next
        Next
    End Sub

    Sub dtgEqSalesHeader_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgEqSalesHeader.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private ViewPengajuanPrivilege As Boolean
    Private ViewResponsePrivilege As Boolean
    Sub dtgEqSalesHeader_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgEqSalesHeader.CurrentPageIndex * dtgEqSalesHeader.PageSize)
            If e.Item.ItemIndex = 0 Then
                ViewPengajuanPrivilege = SecurityProvider.Authorize(Context.User, SR.PengajuanP3BView_Privilege)
                ViewResponsePrivilege = SecurityProvider.Authorize(Context.User, SR.P3BResponseView_Privilege)
            End If
            If Not (ArlEquipmentSalesHeader Is Nothing) Then
                objEquipmentSalesHeader = ArlEquipmentSalesHeader(e.Item.ItemIndex)
                Dim DealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                DealerCode.Text = objEquipmentSalesHeader.Dealer.DealerCode
                If DealerCode.Text <> String.Empty Then
                    DealerCode.ToolTip = objEquipmentSalesHeader.Dealer.SearchTerm1
                End If
                e.Item.Cells(3).Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
                e.Item.Cells(6).Text = CType(objEquipmentSalesHeader.Kind, EquipmentKind.EquipmentKindEnum).ToString
                e.Item.Cells(11).Text = FormatNumber((CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)).ToString, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                TotalHarga += CType(e.Item.Cells(11).Text, Double)
                If objEquipmentSalesHeader.EstimateDeliveryDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                    e.Item.Cells(13).Text = Format(objEquipmentSalesHeader.EstimateDeliveryDate, "dd/MM/yyyy")
                Else
                    e.Item.Cells(13).Text = String.Empty
                End If
                If objEquipmentSalesHeader.ValidateDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                    e.Item.Cells(9).Text = Format(objEquipmentSalesHeader.ValidateDate, "dd/MM/yyyy")
                Else
                    e.Item.Cells(9).Text = String.Empty
                End If
                If objEquipmentSalesHeader.Total <> 0 Then
                    e.Item.Cells(12).Text = FormatNumber((objEquipmentSalesHeader.TotalPembayaran * 100) / (objEquipmentSalesHeader.Total * 11 / 10), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Else
                    e.Item.Cells(12).Text = 0
                End If
                If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 Then
                    e.Item.Cells(15).Text = "*"
                End If

                Dim linkbutton1 As linkbutton = e.Item.FindControl("lbnView")
                If (objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Baru) AndAlso (objEquipmentSalesHeader.Dealer.ID = CType(sessionHelper.GetSession("DEALER"), Dealer).ID) Then
                    linkbutton1.Text = "<img src=""../images/Edit.gif"" border=""0"" alt=""Ubah"">"
                End If
                linkbutton1.Visible = ViewPengajuanPrivilege

                Dim linkbutton As linkbutton = e.Item.FindControl("lbnEdit")
                'If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Baru Or objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Setuju Or objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Tidak_Setuju Then
                If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Baru OrElse objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Validasi Then
                    linkbutton.Visible = False
                Else
                    linkbutton.Visible = ViewResponsePrivilege
                End If

                Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
                lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistorySV.aspx?DocType=" & LookUp.DocumentType.Pembelian_Equipment & "&DocNumber=" & objEquipmentSalesHeader.RegPONumber, "", 400, 400, "DealerSelection")

                If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Validasi Then
                    Dim daylimit As Integer = KTB.DNet.Lib.WebConfig.GetValue("IdleP3B")
                    Dim _date As DateTime = objEquipmentSalesHeader.ValidateDate.AddDays(daylimit)
                    Dim _dateTime As DateTime = New DateTime(CInt(_date.Year), CInt(_date.Month), CInt(_date.Day))
                    'Dim _date As TimeSpan = objEquipmentSalesHeader.ValidateDate.Subtract(DateTime.Now.Date)
                    'Dim day As TimeSpan = DateTime.Now.Date.Subtract(objEquipmentSalesHeader.ValidateDate)
                    'Dim Time As TimeSpan = _date.Subtract(DateTime.Now.Date)
                    If _dateTime < DateTime.Now.Date Then
                        e.Item.BackColor = Color.Red
                    End If
                ElseIf objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 Or objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then
                    For Each item As EquipmentSalesDetail In objEquipmentSalesHeader.EquipmentSalesDetails
                        If (CType(item.Price, Double) - CType(item.EstimatePrice, Double)) / CType(item.EstimatePrice, Double) >= 20 / 100 Then
                            e.Item.BackColor = Color.Yellow
                        End If
                    Next
                End If

            End If
        End If
    End Sub

    Private Sub dtgEqSalesHeader_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEqSalesHeader.ItemCommand
        If e.CommandName = "Edit" Then
            SetSessionCriteria()
            Response.Redirect("../Service/FrmConfirmEquipmentOrder.aspx?Id=" & e.Item.Cells(0).Text & "&NoP3B=" & txtNomorP3B.Text & "&TglP3BAwal=" & icTanggalP3BAwal.Value & "&TglP3BAkhir=" & IcTanggalP3BAkhir.Value & "&Dealer=" & txtKodeDealer.Text & "&Status=" & GetSelectedItem(lboxStatus) & "&Jenis=" & ddlJenis.SelectedValue)
        ElseIf e.CommandName = "View" Then
            SetSessionCriteria()
            Response.Redirect("../Service/FrmCreateEquipmentOrder.aspx?Id=" & e.Item.Cells(0).Text & "&NoP3B=" & txtNomorP3B.Text & "&TglP3BAwal=" & icTanggalP3BAwal.Value & "&TglP3BAkhir=" & IcTanggalP3BAkhir.Value & "&Dealer=" & txtKodeDealer.Text & "&Status=" & GetSelectedItem(lboxStatus) & "&Jenis=" & ddlJenis.SelectedValue)
        ElseIf e.CommandName = "Payment" Then
            SetSessionCriteria()
            sessionHelper.SetSession("PrevPage", Request.Url.ToString())
            Response.Redirect("../PopUp/PopUpPaymentStatus.aspx?PENumber=" & e.Item.Cells(4).Text & "&type=1")
        End If
    End Sub

    Private Sub dtgEqSalesHeader_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEqSalesHeader.SortCommand
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

        dtgEqSalesHeader.SelectedIndex = -1
        dtgEqSalesHeader.CurrentPageIndex = 0
        BindDataToGrid(dtgEqSalesHeader.CurrentPageIndex)
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgEqSalesHeader.CurrentPageIndex = 0
        BindGrid()
    End Sub

    'Private Sub btnValidasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidasi.Click
    '    Dim listEQ As ArrayList = PopulateEquipmentValidate()

    '    If listEQ.Count = 0 Then
    '        'to do
    '        MessageBox.Show(SR.DataNotFoundByStatus("Equipment", "Baru"))
    '    Else
    '        Dim objEquipmentSalesFacade As New EquipmentSalesHeaderFacade(User)
    '        objEquipmentSalesFacade.UpdateEquipmentStatus(listEQ)
    '        RecordStatusChangeHistory(listEQ, EquipmentStatus.EquipmentStatusEnum.Baru)

    '        BindGrid()
    '    End If
    'End Sub

    'Private Sub btnSetuju_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetuju.Click
    '    Dim listEQ As ArrayList = PopulateEquipmentAgree()

    '    If listEQ.Count = 0 Then
    '        'to do
    '        MessageBox.Show(SR.DataNotFoundByStatus("Equipment", "Rilis1 atau Rilis2"))
    '    Else
    '        Dim objEquipmentSalesFacade As New EquipmentSalesHeaderFacade(User)
    '        objEquipmentSalesFacade.UpdateEquipmentStatus(listEQ)
    '        RecordStatusChangeHistory(listEQ, EquipmentStatus.EquipmentStatusEnum.Rilis1)
    '        BindGrid()
    '    End If
    'End Sub

    'Private Sub btnTidakSetuju_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTidakSetuju.Click
    '    Dim listEQ As ArrayList = PopulateEquipmentDisAgree()

    '    If listEQ.Count = 0 Then
    '        'to do
    '        MessageBox.Show(SR.DataNotFoundByStatus("Equipment", "Rilis1 atau Rilis2"))
    '    Else
    '        Dim objEquipmentSalesFacade As New EquipmentSalesHeaderFacade(User)
    '        objEquipmentSalesFacade.UpdateEquipmentStatus(listEQ)
    '        RecordStatusChangeHistory(listEQ, EquipmentStatus.EquipmentStatusEnum.Rilis1)
    '        BindGrid()
    '    End If
    'End Sub

    Private Sub btnProses_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProses.Click
        If Me.ddlStatus.SelectedIndex <> -1 Then
            Dim al As ArrayList
            al = PopulateEquipment(Me.ddlStatus.SelectedValue)
            If al.Count > 0 Then
                Dim i As Integer = New EquipmentSalesHeaderFacade(User).UpdateTransaction(al)
                BindGrid()
            Else
                MessageBox.Show("Tidak ada Data yang dapat diproses " & Me.ddlStatus.SelectedItem.Text)
            End If
        End If
    End Sub

    Private Function PopulateEquipment(ByVal type As Integer) As ArrayList
        Dim item As DataGridItem
        Dim collEq As ArrayList = New ArrayList
        Dim Eq As EquipmentSalesHeader
        Dim _status As EquipmentStatus = New EquipmentStatus
        Dim EQFacade As EquipmentSalesHeaderFacade

        For Each item In Me.dtgEqSalesHeader.Items
            If CType(item.FindControl("chkItem"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                EQFacade = New EquipmentSalesHeaderFacade(User)
                Eq = EQFacade.Retrieve(id)
                Select Case (type)
                    Case 1 'diValidasi
                        If Eq.Status = EquipmentStatus.EquipmentStatusEnum.Baru Then
                            Eq.Status = _status.EquipmentStatusEnum.Validasi
                            Eq.ValidateDate = DateTime.Now.Date
                            collEq.Add(Eq)
                            RecordStatusChangeHistory(Eq, EquipmentStatus.EquipmentStatusEnum.Baru)
                        End If
                    Case 2 'Batal Validasi
                        If Eq.Status = EquipmentStatus.EquipmentStatusEnum.Validasi Then
                            Eq.Status = _status.EquipmentStatusEnum.Baru
                            collEq.Add(Eq)
                            RecordStatusChangeHistory(Eq, EquipmentStatus.EquipmentStatusEnum.Validasi)
                        End If
                    Case 3 'Konfirmasi
                        If Eq.Status = EquipmentStatus.EquipmentStatusEnum.Validasi Then
                            Eq.Status = _status.EquipmentStatusEnum.Konfirmasi
                            For Each equipmentSalesDetailItem As EquipmentSalesDetail In Eq.EquipmentSalesDetails
                                equipmentSalesDetailItem.EstimatePrice = equipmentSalesDetailItem.PriceFromEquipmentMaster
                            Next
                            collEq.Add(Eq)
                            RecordStatusChangeHistory(Eq, EquipmentStatus.EquipmentStatusEnum.Validasi)
                        End If

                    Case 4 'Batal Konfirmasi
                        If Eq.Status = EquipmentStatus.EquipmentStatusEnum.Konfirmasi Then
                            Eq.Status = _status.EquipmentStatusEnum.Validasi
                            collEq.Add(Eq)
                            RecordStatusChangeHistory(Eq, EquipmentStatus.EquipmentStatusEnum.Konfirmasi)
                        End If

                    Case 5 'Rilis1
                        If Eq.Status = EquipmentStatus.EquipmentStatusEnum.Konfirmasi Then
                            Eq.Status = _status.EquipmentStatusEnum.Rilis1
                            For Each equipmentSalesDetailItem As EquipmentSalesDetail In Eq.EquipmentSalesDetails
                                equipmentSalesDetailItem.Price = equipmentSalesDetailItem.PriceFromEquipmentMaster
                            Next
                            collEq.Add(Eq)
                            RecordStatusChangeHistory(Eq, EquipmentStatus.EquipmentStatusEnum.Konfirmasi)
                        End If

                    Case 6 'Rilis2
                        If Eq.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 Then
                            Eq = EQFacade.Retrieve(id)
                            Eq.Status = _status.EquipmentStatusEnum.Rilis2
                            collEq.Add(Eq)
                            RecordStatusChangeHistory(Eq, EquipmentStatus.EquipmentStatusEnum.Rilis1)
                        End If

                    Case 7 'Batal Rilis
                        If Eq.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 Then
                            Eq.Status = _status.EquipmentStatusEnum.Konfirmasi
                            collEq.Add(Eq)
                            RecordStatusChangeHistory(Eq, EquipmentStatus.EquipmentStatusEnum.Rilis1)
                        ElseIf Eq.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then
                            If SecurityProvider.Authorize(Context.User, SR.P3BResponseRelease2_Privilege) Then
                                Eq.Status = _status.EquipmentStatusEnum.Rilis1
                                collEq.Add(Eq)
                                RecordStatusChangeHistory(Eq, EquipmentStatus.EquipmentStatusEnum.Rilis2)
                            Else
                                MessageBox.Show("Maaf, P3B Dalam status Rilis2 ")
                            End If
                        End If
                        'Case 8 'Setuju
                        '    If item.Cells(2).Text.ToUpper = EquipmentStatus.EquipmentStatusEnum.Rilis2.ToString.ToUpper Then
                        '        Eq = EQFacade.Retrieve(id)
                        '        Eq.Status = _status.EquipmentStatusEnum.Setuju
                        '        collEq.Add(Eq)
                        '        RecordStatusChangeHistory(collEq, EquipmentStatus.EquipmentStatusEnum.Rilis2)
                        '    End If

                        'Case 9 'Tidak Setuju
                        '    If item.Cells(2).Text.ToUpper = EquipmentStatus.EquipmentStatusEnum.Rilis2.ToString.ToUpper Then
                        '        Eq = EQFacade.Retrieve(id)
                        '        Eq.Status = _status.EquipmentStatusEnum.Tidak_Setuju
                        '        collEq.Add(Eq)
                        '        RecordStatusChangeHistory(collEq, EquipmentStatus.EquipmentStatusEnum.Rilis2)
                        '    End If

                    Case 8 'Tolak
                        If Eq.Status = EquipmentStatus.EquipmentStatusEnum.Validasi Then
                            Eq.Status = _status.EquipmentStatusEnum.Ditolak
                            collEq.Add(Eq)
                            RecordStatusChangeHistory(Eq, EquipmentStatus.EquipmentStatusEnum.Validasi)
                        End If

                    Case 9 'Batal Tolak
                        If Eq.Status = EquipmentStatus.EquipmentStatusEnum.Ditolak Then
                            Eq.Status = _status.EquipmentStatusEnum.Validasi
                            collEq.Add(Eq)
                            RecordStatusChangeHistory(Eq, EquipmentStatus.EquipmentStatusEnum.Ditolak)
                        End If

                End Select
            End If
        Next
        Return collEq
    End Function

    Private Sub btnTransferUlang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferUlang.Click
        ReDownload()
    End Sub

    Private Sub ReDownload()
        Dim item As DataGridItem
        Dim collEq As ArrayList = New ArrayList
        Dim Eq As EquipmentSalesHeader
        Dim EQFacade As EquipmentSalesHeaderFacade = New EquipmentSalesHeaderFacade(User)
        For Each item In Me.dtgEqSalesHeader.Items
            If CType(item.FindControl("chkItem"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                Eq = EQFacade.Retrieve(id)
                If Not Eq Is Nothing Then
                    If Eq.ID > 0 Then
                        If Eq.IsKTBView = 1 Then 'Udah pernah didownload
                            collEq.Add(Eq)
                        End If
                    End If
                End If
            End If
        Next
        'Process
        If collEq.Count > 0 Then
            CreateTextFile(collEq)
        Else
            MessageBox.Show("Tidak ada data untuk Transfer Ulang")
        End If
    End Sub

    Private Sub CreateTextFile(ByVal coll As ArrayList)
        Dim _fileHelper As New FileHelper
        Dim fileInfo As FileInfo
        'Dim arrListEquipmentSalesHeader As New ArrayList
        'arrListEquipmentSalesHeader.Add(objEquipmentSalesHeader)
        Try
            fileInfo = _fileHelper.TransferPEtoSAP(coll)
            MessageBox.Show(SR.UploadSucces(fileInfo.Name))
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail(fileInfo.Name))
        End Try
    End Sub

#End Region


End Class