Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security

Public Class frmCreateEquipmentOrder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblNoP3B As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenis As System.Web.UI.WebControls.Label
    Protected WithEvents lblPermintaanKirim As System.Web.UI.WebControls.Label
    Protected WithEvents lblPenjelasan As System.Web.UI.WebControls.Label
    Protected WithEvents lblRencanaKirim As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggapanKTB As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchPenjelasan As System.Web.UI.WebControls.Label
    Protected WithEvents lblSeachTanggapanKTB As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenis As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtPenjelasan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKonfirmasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnHapus As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents lblNoReqPO As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoReqPOValue As System.Web.UI.WebControls.Label
    Protected WithEvents iCPermintaanKirim As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNoP3B As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblRencanaKirimValue As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents lblSubsidi As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubsidiValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubTotalValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPembayaranValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPayPCT As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTotalPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPN As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPNValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaPembayaranValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaPCT As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgEquipmentOrder As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents tableAmountAndGrid As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatalValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnBaru As System.Web.UI.WebControls.Button
    'Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private EquipmentSalesHeaderID As String
    Private objDealer As Dealer
    Private Mode As enumMode.Mode
    Private objEquipmentSalesHeader As EquipmentSalesHeader
    Private objEquipmentSalesDetail As EquipmentSalesDetail
    Private objEquipmentFacade As New EquipmentSalesHeaderFacade(User)
    Private sessionHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub ClearField()
        lblNoReqPOValue.Text = String.Empty
        txtNoP3B.Text = String.Empty
        iCPermintaanKirim.Value = DateTime.Now
        txtPenjelasan.Text = String.Empty
        lblStatusValue.Text = enumStatusPK.Status.Baru.ToString
        lblSubsidiValue.Text = String.Empty
        lblTotalPembayaranValue.Text = 0
        lblSisaPembayaranValue.Text = 0
        ddlJenis.SelectedIndex = 0
        lblSubTotalValue.Text = 0
        lblTotalValue.Text = 0
        lblTotalPayPCT.Text = "(0%)"
        lblSisaPCT.Text = "(0%)"
        lblPPNValue.Text = 0
    End Sub

    Private Sub BindDetailToGrid()
        objDealer = sessionHelper.GetSession("DEALER")
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        If (objEquipmentSalesHeader.Status <> CType(EquipmentStatus.EquipmentStatusEnum.Rilis1, Short) AndAlso objEquipmentSalesHeader.Status <> CType(EquipmentStatus.EquipmentStatusEnum.Baru, Short) OrElse ((Not (objEquipmentSalesHeader.Dealer Is Nothing)) AndAlso (objEquipmentSalesHeader.Dealer.ID <> objDealer.ID))) Then
            dtgEquipmentOrder.Columns(8).Visible = False
            dtgEquipmentOrder.Columns(9).Visible = False
            dtgEquipmentOrder.Columns(10).Visible = False
            dtgEquipmentOrder.ShowFooter = False
        ElseIf objEquipmentSalesHeader.Status = CType(EquipmentStatus.EquipmentStatusEnum.Rilis1, Short) Then
            dtgEquipmentOrder.Columns(9).Visible = False
            dtgEquipmentOrder.Columns(10).Visible = False
            dtgEquipmentOrder.ShowFooter = False
        Else
            dtgEquipmentOrder.Columns(8).Visible = True
            dtgEquipmentOrder.Columns(9).Visible = True
            dtgEquipmentOrder.Columns(10).Visible = True
            btnSimpan.Enabled = True
            btnHapus.Enabled = True
        End If

        dtgEquipmentOrder.DataSource = objEquipmentSalesHeader.EquipmentSalesDetails
        dtgEquipmentOrder.DataBind()
    End Sub

    Private Sub BindDataToPage()
        If IsNothing(sessionHelper.GetSession("Equipment")) Then
            objEquipmentSalesHeader = New KTB.DNet.Domain.EquipmentSalesHeader
            objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Baru
            sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
            ClearField()
        Else
            objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                BindHeaderToForm()
            End If
        End If
        BindDetailToGrid()
    End Sub

    Private Sub SetMode()
        If EquipmentSalesHeaderID = String.Empty Then
            btnKembali.Visible = False
            Mode = enumMode.Mode.NewItemMode
            SetButtonNewMode()
            ViewState("Mode") = Mode
            sessionHelper.RemoveSession("Equipment")
            objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
            'If objDealer Is Nothing Then
            '    Response.Redirect("../SessionExpired.htm")
            'End If
        Else
            GetInformation()
            btnKembali.Visible = True
            LoadEquipmentSales()
            Mode = enumMode.Mode.EditMode
            SetButtonEditMode()
            ViewState("Mode") = Mode
        End If
    End Sub

    Private Sub LoadEquipmentSales()
        objEquipmentSalesHeader = New EquipmentSalesHeaderFacade(User).Retrieve(CInt(EquipmentSalesHeaderID))
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
        objDealer = objEquipmentSalesHeader.Dealer
    End Sub

    Private Sub SetButtonNewMode()
        btnValidasi.Enabled = False
        btnBatalValidasi.Enabled = False
        ddlJenis.Enabled = True
        FillddlJenis()
    End Sub

    Private Sub FillddlJenis()
        ddlJenis.DataSource = EquipmentKind.RetrieveEquipmentKind()
        ddlJenis.DataTextField = "NameStatus"
        ddlJenis.DataValueField = "ValStatus"
        ddlJenis.DataBind()
        If Not SecurityProvider.Authorize(Context.User, SR.PengajuanP3BPerbaikan_Privilege) Then
            ddlJenis.Items.RemoveAt(2)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.PengajuanP3BBarangBaru_Privilege) Then
            ddlJenis.Items.RemoveAt(1)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.PengajuanP3BBarang_Privilege) Then
            ddlJenis.Items.RemoveAt(0)
        End If

        If ddlJenis.Items.Count = 0 Then
            tableAmountAndGrid.Visible = False
            btnSimpan.Enabled = False
            btnBaru.Enabled = False
        End If
    End Sub

    Private Sub SetButtonEditMode()
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        ddlJenis.Enabled = False
        objDealer = sessionHelper.GetSession("DEALER")
        'If objDealer Is Nothing Then
        '    Response.Redirect("../SessionExpired.htm")
        'End If
        If (objEquipmentSalesHeader.Status <> EquipmentStatus.EquipmentStatusEnum.Baru) OrElse ((Not (objEquipmentSalesHeader.Dealer Is Nothing)) AndAlso (objEquipmentSalesHeader.Dealer.ID <> objDealer.ID)) Then
            btnSimpan.Enabled = False
            btnBaru.Enabled = False
            btnHapus.Enabled = False
            dtgEquipmentOrder.ShowFooter = False
            lblSearchPenjelasan.Visible = False
            lblSeachTanggapanKTB.Visible = False
            txtNoP3B.ReadOnly = True
            iCPermintaanKirim.Enabled = False
            If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Validasi Then
                btnValidasi.Enabled = False
                btnBatalValidasi.Enabled = True
            Else
                btnValidasi.Enabled = False
                btnBatalValidasi.Enabled = False
            End If
        End If
        If (objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Baru) Then
            lblSearchTotalPembayaran.Visible = True
            If objEquipmentSalesHeader.ID <> 0 Then
                btnValidasi.Enabled = True
                btnBatalValidasi.Enabled = False
                btnBaru.Enabled = True
            End If
            'If (objEquipmentSalesHeader.Status <> EquipmentStatus.EquipmentStatusEnum.Validasi) Then
            '    lblSeachTanggapanKTB.Visible = True
            'End If
        End If
    End Sub

    Private Sub RetrieveMaster()
        lblDealerCode.Text = objDealer.DealerCode
        lblSearchTerm1.Text = objDealer.SearchTerm1

    End Sub

    Private Sub BindHeaderToForm()
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        lblDealerCode.Text = objEquipmentSalesHeader.Dealer.DealerCode
        lblSearchTerm1.Text = objEquipmentSalesHeader.Dealer.SearchTerm1
        lblNoReqPOValue.Text = objEquipmentSalesHeader.RegPONumber
        lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
        iCPermintaanKirim.Value = objEquipmentSalesHeader.ReqDeliveryDate
        If objEquipmentSalesHeader.EstimateDeliveryDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
            lblRencanaKirimValue.Text = objEquipmentSalesHeader.EstimateDeliveryDate
        Else
            lblRencanaKirimValue.Text = String.Empty
        End If
        'ddlJenis.SelectedValue = objEquipmentSalesHeader.Kind
        Dim listItem As New ListItem(CType(objEquipmentSalesHeader.Kind, EquipmentKind.EquipmentKindEnum).ToString, objEquipmentSalesHeader.Kind)
        ddlJenis.Items.Add(listItem)
        txtPenjelasan.Text = objEquipmentSalesHeader.DeteilRequirement
        txtKonfirmasi.Text = objEquipmentSalesHeader.ResponseDetail
        txtNoP3B.Text = objEquipmentSalesHeader.PONumber
        lblSubsidiValue.Text = FormatNumber(objEquipmentSalesHeader.Subsidi, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblSubTotalValue.Text = FormatNumber(objEquipmentSalesHeader.Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblPPNValue.Text = FormatNumber(10 * CType(objEquipmentSalesHeader.Total, Double) / 100, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalValue.Text = FormatNumber((CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)).ToString, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalPembayaranValue.Text = FormatNumber(objEquipmentSalesHeader.TotalPembayaran, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 OrElse objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then
            lblSisaPembayaran.Text = "Sisa Pembayaran"
            lblSisaPembayaranValue.Text = FormatNumber(CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100) - CType(objEquipmentSalesHeader.TotalPembayaran, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Else
            lblSisaPembayaranValue.Text = FormatNumber((CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)) / 2, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
        If objEquipmentSalesHeader.Total <> 0 Then
            lblTotalPayPCT.Text = "(" & FormatNumber(CType(objEquipmentSalesHeader.TotalPembayaran, Double) * 100 / (CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
            If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 OrElse objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then
                lblSisaPCT.Text = "(" & FormatNumber((CType(lblTotalValue.Text, Double) - CType(objEquipmentSalesHeader.TotalPembayaran, Double)) * 100 / (CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
            Else
                lblSisaPCT.Text = "(50%)"
            End If
        Else
            lblTotalPayPCT.Text = "(0%)"
            If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 OrElse objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then
                lblSisaPCT.Text = "(0%)"
            Else
                lblSisaPCT.Text = "(50%)"
            End If
        End If


    End Sub
#End Region

#Region "Event Hendlers"

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("../Service/frmEquipmentOrderList.aspx?NoP3B=" & ViewState("NoP3B") & "&TglP3BAwal=" & ViewState("TglP3BAwal") & "&TglP3BAkhir=" & ViewState("TglP3BAkhir") & "&Dealer=" & ViewState("Dealer") & "&Status=" & ViewState("Status") & "&Jenis=" & ViewState("Jenis"))
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            'ViewState("Count") = "0"
            ActivateUserPrivilege()
            EquipmentSalesHeaderID = Request.QueryString("id")
            SetMode()
            RetrieveMaster()
            BindDataToPage()
            ddlJenis_SelectedIndexChanged(Me, EventArgs.Empty)
        End If
        'Hidden1.Value = CInt(ViewState("Count")) + 1
        'ViewState("Count") = Hidden1.Value
        'CheckStatus() '--Add by Sony
        lblSearchPenjelasan.Attributes("onclick") = "ShowPPPenjelasan();"
        lblSeachTanggapanKTB.Attributes("onclick") = "ShowPPKonfirmasi();"
        lblSearchTotalPembayaran.Attributes("onclick") = "ShowPPPembayaran();"
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PengajuanP3BView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Pengajuan P3B")
        End If
        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanP3BSave_Privilege)
        btnHapus.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanP3BDelete_Privilege)
        btnValidasi.Visible = SecurityProvider.Authorize(Context.User, SR.P3BListValidate_Privilege)
        btnBatalValidasi.Visible = SecurityProvider.Authorize(Context.User, SR.P3BListValidate_Privilege)
        btnBaru.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanP3BBarangBaru_Privilege)
    End Sub

    Private Sub CheckStatus()

        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Baru And objEquipmentSalesHeader.ID <> 0 Then
            btnValidasi.Enabled = True
        Else
            btnValidasi.Enabled = False
        End If

        If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Validasi Then
            btnBatalValidasi.Enabled = True
            btnSimpan.Enabled = False
            btnBaru.Enabled = False
            btnHapus.Enabled = False
        Else
            btnBatalValidasi.Enabled = False
        End If

    End Sub
    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        If Not (objEquipmentSalesHeader Is Nothing) Then
            If (objEquipmentSalesHeader.RegPONumber <> String.Empty) Then
                Dim objEquipmentSalesHeaderFacade As New EquipmentSalesHeaderFacade(User)
                objEquipmentSalesHeaderFacade.Delete(objEquipmentSalesHeader)
            End If
            sessionHelper.RemoveSession("Equipment")
            BindDataToPage()
            Mode = enumMode.Mode.NewItemMode
            SetButtonNewMode()
            ViewState("Mode") = Mode
        End If
    End Sub

    Private Function ValidateItem(ByVal kodeEquipment As String, ByVal Unit As String) As Boolean
        If (kodeEquipment = String.Empty Or Unit = String.Empty) Then
            lblError.Text = "Error : KodeEquipment dan Jumlah Tidak boleh Kosong"
            Return False
        Else
            Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Status", MatchType.Exact, CType(EquipmentMasterStatus.EquipmentMasterStatusEnum.Aktive, Short)))
            criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "EquipmentNumber", MatchType.Exact, kodeEquipment))

            criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Kind", MatchType.Exact, ddlJenis.SelectedValue))
            Dim ArrEquipmentMaster As ArrayList = New EquipmentMasterFacade(User).Retrieve(criterias1)
            If (ArrEquipmentMaster.Count = 0) Then
                lblError.Text = "Error : Kode Equipment dan Jenis tidak Cocok"
                Return False
            End If
        End If
        Return True
    End Function

    Private Function ValidateDuplication(ByVal kodeEquipment As String, ByVal Mode As String, ByVal Rowindex As Integer) As Boolean
        If (Mode = "Add") Then
            For Each item As EquipmentSalesDetail In objEquipmentSalesHeader.EquipmentSalesDetails
                If (item.EquipmentMaster.EquipmentNumber = kodeEquipment) Then
                    lblError.Text = "Error : Duplikasi Kode Equipment"
                    Return False
                End If
            Next
        Else
            Dim i As Integer = 0
            For Each item As EquipmentSalesDetail In objEquipmentSalesHeader.EquipmentSalesDetails
                If (item.EquipmentMaster.EquipmentNumber = kodeEquipment) Then
                    If i <> Rowindex Then
                        lblError.Text = "Error : Duplikasi KodeTipe dan KodeWarna"
                        Return False
                    End If
                End If
                i = i + 1
            Next
        End If
        Return True
    End Function

    Private Sub GetInformation()
        ViewState("NoP3B") = Request.QueryString("NoP3B")
        ViewState("TglP3BAwal") = Request.QueryString("TglP3BAwal")
        ViewState("TglP3BAkhir") = Request.QueryString("TglP3BAkhir")
        ViewState("Dealer") = Request.QueryString("Dealer")
        ViewState("Status") = Request.QueryString("Status")
        ViewState("Jenis") = Request.QueryString("Jenis")
    End Sub

    Private Sub dtgEquipmentOrderItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblSearchEquipmentFooter As Label = CType(e.Item.FindControl("lblSearchEquipmentFooter"), Label)
        lblSearchEquipmentFooter.Attributes("onclick") = "ShowPPKodeEquipmentSelection();"
    End Sub

    Private Sub dtgEquipmentOrderItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblSearchKodeEquipmentEdit As Label = CType(e.Item.FindControl("lblSearchKodeEquipmentEdit"), Label)
        lblSearchKodeEquipmentEdit.Attributes("onclick") = "ShowPPKodeEquipmentSelection();"
    End Sub

    Sub dtgEquipmentOrder_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs)
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        If E.Item.ItemType = ListItemType.Footer Then
            dtgEquipmentOrderItemFooter(E)
        ElseIf E.Item.ItemType = ListItemType.EditItem OrElse E.Item.ItemType = ListItemType.SelectedItem Then
            dtgEquipmentOrderItemEdit(E)
        End If
        If E.Item.ItemIndex <> -1 Then
            objEquipmentSalesDetail = objEquipmentSalesHeader.EquipmentSalesDetails(E.Item.ItemIndex)
            If E.Item.ItemType = ListItemType.AlternatingItem OrElse E.Item.ItemType = ListItemType.Item Then
                E.Item.Cells(3).Text = objEquipmentSalesDetail.EquipmentMaster.Description
                E.Item.Cells(11).Text = objEquipmentSalesDetail.EquipmentMaster.ID
                If objEquipmentSalesDetail.Price <> 0 Then
                    E.Item.Cells(5).Text = FormatNumber(objEquipmentSalesDetail.Price * objEquipmentSalesDetail.Quantity, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Else
                    E.Item.Cells(5).Text = FormatNumber(objEquipmentSalesDetail.PriceFromEquipmentMaster * objEquipmentSalesDetail.Quantity, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                End If
                If objEquipmentSalesDetail.EstimatePrice <> 0 Then
                    E.Item.Cells(6).Text = FormatNumber(objEquipmentSalesDetail.EstimatePrice * objEquipmentSalesDetail.Quantity, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Else
                    E.Item.Cells(6).Text = FormatNumber(objEquipmentSalesDetail.PriceFromEquipmentMaster * objEquipmentSalesDetail.Quantity, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                End If
                E.Item.Cells(7).Text = FormatNumber(objEquipmentSalesDetail.Discount * CType(E.Item.Cells(5).Text, Double) / 100, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Dim LbtnKodeEq As LinkButton = CType(E.Item.FindControl("LbnKode"), LinkButton)
                LbtnKodeEq.Text = objEquipmentSalesDetail.EquipmentMaster.EquipmentNumber
                If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 Then
                    'Dim lbtnDelete As LinkButton = E.Item.FindControl("lbtnDelete")
                    If (CType(objEquipmentSalesDetail.Price, Double) - CType(objEquipmentSalesDetail.EstimatePrice, Double)) / CType(objEquipmentSalesDetail.EstimatePrice, Double) >= 20 / 100 Then
                        E.Item.BackColor = Color.Yellow
                    End If
                End If
            ElseIf E.Item.ItemType = ListItemType.EditItem Then
                If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 Then
                    Dim rangeValidator1 As RangeValidator = E.Item.FindControl("RangeValidator2")
                    rangeValidator1.MinimumValue = 0
                    Dim searchLabel As Label = E.Item.FindControl("lblSearchKodeEquipmentEdit")
                    searchLabel.Visible = False
                    Dim txtBox As TextBox = E.Item.FindControl("txtKodeEquipmentEdit")
                    txtBox.ReadOnly = True
                End If

            End If
        End If
    End Sub

    Sub dtgEquipmentOrder_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs)
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        Select Case (e.CommandName)
            Case "Delete"
                Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")
                Mode = ViewState("Mode")
                If (Mode = enumMode.Mode.EditMode) Then
                    Dim EquipmentSalesDetailFacade As New EquipmentSalesDetailFacade(User)
                    EquipmentSalesDetailFacade.Delete(objEquipmentSalesHeader.EquipmentSalesDetails.Item(CType(lbl1.Text, Integer) - 1))
                End If
                objEquipmentSalesHeader.EquipmentSalesDetails.Remove(objEquipmentSalesHeader.EquipmentSalesDetails.Item(CType(lbl1.Text, Integer) - 1))
                sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
                BindDataToPage()
                Mode = ViewState("Mode")
                If objEquipmentSalesHeader.EquipmentSalesDetails.Count = 0 And Mode = enumMode.Mode.NewItemMode Then
                    SetButtonNewMode()
                End If
                dtgEquipmentOrder.ShowFooter = True
                dtgEquipmentOrder.EditItemIndex = -1
            Case "Add"
                If Not Page.IsValid Then
                    Return
                End If
                Dim txt1 As TextBox = e.Item.FindControl("txtKodeEquipmentFooter")
                Dim txt2 As TextBox = e.Item.FindControl("txtJumlahFooter")
                If (ValidateItem(txt1.Text.ToUpper, txt2.Text) And ValidateDuplication(txt1.Text.ToUpper, "Add", -1)) Then
                    objEquipmentSalesDetail = New EquipmentSalesDetail
                    Dim objEquipmentMaster As EquipmentMaster = New EquipmentMasterFacade(User).Retrieve(txt1.Text)
                    objEquipmentSalesDetail.EquipmentMaster = objEquipmentMaster
                    objEquipmentSalesDetail.Quantity = txt2.Text
                    Mode = ViewState("Mode")
                    If (Mode = enumMode.Mode.EditMode) Then
                        Dim equipmentSalesDetailFacade As New EquipmentSalesDetailFacade(User)
                        objEquipmentSalesDetail.EquipmentSalesHeader = objEquipmentSalesHeader
                        equipmentSalesDetailFacade.Insert(objEquipmentSalesDetail)
                        objEquipmentSalesHeader.EquipmentSalesDetails.Clear()
                    Else
                        objEquipmentSalesHeader.EquipmentSalesDetails.Add(objEquipmentSalesDetail)
                    End If
                Else
                    Exit Sub
                End If
                sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
                BindDataToPage()
                SetButtonEditMode()
                CountTotalAndPPN(objEquipmentSalesHeader.EquipmentSalesDetails)
            Case "Kode"
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                Response.Redirect("frmEquipmentListDetail.aspx?Eq=" & e.Item.Cells(11).Text & "&Status=" & "detail")
        End Select
    End Sub

    Sub dtgEquipmentOrder_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        If e.Item.BackColor.ToString = "Color [Yellow]" Or lblStatusValue.Text.Trim.ToUpper = "BARU" Then
            dtgEquipmentOrder.EditItemIndex = CInt(e.Item.ItemIndex)
            dtgEquipmentOrder.ShowFooter = False
            BindDetailToGrid()
        Else
            MessageBox.Show("Informasi Item Tidak dapat dirubah")
        End If

    End Sub

    Sub dtgEquipmentOrder_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dtgEquipmentOrder.EditItemIndex = -1
        BindDetailToGrid()
        If lblStatusValue.Text = "Baru" Then
            dtgEquipmentOrder.ShowFooter = True
        End If
    End Sub

    Sub dtgEquipmentOrder_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        Dim txt1 As TextBox = E.Item.FindControl("txtKodeEquipmentEdit")
        Dim txt2 As TextBox = E.Item.FindControl("txtJumlahEdit")
        If (ValidateItem(txt1.Text.ToUpper, txt2.Text) And ValidateDuplication(txt1.Text.ToUpper, "Edit", E.Item.ItemIndex)) Then
            objEquipmentSalesDetail = objEquipmentSalesHeader.EquipmentSalesDetails(E.Item.ItemIndex)
            Dim objEquipmentMaster As EquipmentMaster = New EquipmentMasterFacade(User).Retrieve(txt1.Text)
            objEquipmentSalesDetail.EquipmentMaster = objEquipmentMaster
            objEquipmentSalesDetail.Quantity = txt2.Text
            sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
            dtgEquipmentOrder.EditItemIndex = -1
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                Dim PKEquipmentSalesDetailFacade As New EquipmentSalesDetailFacade(User)
                objEquipmentSalesDetail.EquipmentSalesHeader = objEquipmentSalesHeader
                PKEquipmentSalesDetailFacade.Update(objEquipmentSalesDetail)
            End If
            dtgEquipmentOrder.EditItemIndex = -1
            dtgEquipmentOrder.ShowFooter = True
            BindDetailToGrid()
            CountTotalAndPPN(objEquipmentSalesHeader.EquipmentSalesDetails)
        End If
    End Sub


    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        dtgEquipmentOrder.EditItemIndex = -1
        dtgEquipmentOrder.ShowFooter = True
        'If Not (objEquipmentSalesHeader.EquipmentSalesDetails.Count = 0) Then
        Mode = ViewState("Mode")
        BindDataToObject()
        If Mode = enumMode.Mode.NewItemMode Then
            SaveToDatabase()
            Mode = enumMode.Mode.EditMode
            ViewState("Mode") = Mode
            SetButtonEditMode()
        Else
            Dim objEquipmentSalesHeaderFacade As New EquipmentSalesHeaderFacade(User)
            objEquipmentSalesHeaderFacade.Update(objEquipmentSalesHeader)
            MessageBox.Show("Data Berhasil Disimpan")
            RefreshData(objEquipmentSalesHeader.ID)
        End If
        'Else
        'MessageBox.Show("Belum ada PK Detail")
        'End If
    End Sub

    Private Sub SaveToDatabase()
        Dim int As Integer = New EquipmentSalesHeaderFacade(User).Insert(objEquipmentSalesHeader)
        If int > 0 Then
            MessageBox.Show("Data Berhasil Disimpan")
            RefreshData(int)
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, -1, CInt(EquipmentStatus.EquipmentStatusEnum.Baru))
        End If
    End Sub

    Private Sub RefreshData(ByVal id As Integer)
        objEquipmentSalesHeader = New EquipmentSalesHeaderFacade(User).Retrieve(id)
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)

        BindHeaderToForm()
        BindDetailToGrid()
    End Sub

    Private Sub BindDataToObject()
        objEquipmentSalesHeader.Dealer = sessionHelper.GetSession("DEALER")
        If ddlJenis.Enabled = True Then
            objEquipmentSalesHeader.Kind = ddlJenis.SelectedValue
        End If
        objEquipmentSalesHeader.PONumber = txtNoP3B.Text
        objEquipmentSalesHeader.ReqDeliveryDate = iCPermintaanKirim.Value
        objEquipmentSalesHeader.ResponseDetail = txtKonfirmasi.Text
        objEquipmentSalesHeader.DeteilRequirement = txtPenjelasan.Text
        Dim objEquipmentSalesPayment As New EquipmentSalesPayment
        objEquipmentSalesPayment.KwitansiNumber = "11111"
        objEquipmentSalesPayment.Sequence = 1
        objEquipmentSalesHeader.EquipmentSalesPayments.Add(objEquipmentSalesPayment)
    End Sub

    Private Sub CountTotalAndPPN(ByVal ArrList As ArrayList)
        lblSubsidiValue.Text = FormatNumber(Calculation.CountPESubsidi(ArrList), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblSubTotalValue.Text = FormatNumber(Calculation.CountPESubTotal(ArrList), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblPPNValue.Text = FormatNumber(Calculation.CountPEPPN(ArrList), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalValue.Text = FormatNumber(Calculation.CountPETotal(ArrList), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 OrElse objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then
            lblSisaPembayaranValue.Text = FormatNumber(CType(lblTotalValue.Text, Double) - CType(objEquipmentSalesHeader.TotalPembayaran, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Else
            lblSisaPembayaranValue.Text = FormatNumber(CType(lblTotalValue.Text, Double) / 2, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
        If objEquipmentSalesHeader.Total <> 0 Then
            lblTotalPayPCT.Text = "(" & FormatNumber(CType(objEquipmentSalesHeader.TotalPembayaran, Double) * 100 / CType(lblTotalValue.Text, Double), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
            If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 OrElse objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then
                lblSisaPCT.Text = "(" & FormatNumber((CType(lblTotalValue.Text, Double) - CType(objEquipmentSalesHeader.TotalPembayaran, Double)) * 100 / CType(lblTotalValue.Text, Double), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
            Else
                lblSisaPCT.Text = "(50%)"
            End If
        Else
            lblTotalPayPCT.Text = "(0%)"
            If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 OrElse objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then
                lblSisaPCT.Text = "(0%)"
            Else
                lblSisaPCT.Text = "(50%)"
            End If
        End If


        'lblSisaPembayaranValue.Text = FormatNumber(CType(lblTotalValue.Text, Double) - CType(objEquipmentSalesHeader.TotalPembayaran, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'If objEquipmentSalesHeader.Total <> 0 Then
        '    lblTotalPayPCT.Text = "(" & FormatNumber(CType(objEquipmentSalesHeader.TotalPembayaran, Double) * 100 / CType(lblTotalValue.Text, Double), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
        '    lblSisaPCT.Text = "(" & FormatNumber((CType(lblTotalValue.Text, Double) - CType(objEquipmentSalesHeader.TotalPembayaran, Double)) * 100 / CType(lblTotalValue.Text, Double), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
        'Else
        '    lblTotalPayPCT.Text = "(0%)"
        '    lblSisaPCT.Text = "(0%)"
        'End If
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlJenis.SelectedIndexChanged
        If ddlJenis.SelectedItem.Text = "Pembelian_Baru" Then
            tableAmountAndGrid.Visible = False
        Else
            tableAmountAndGrid.Visible = True
        End If

        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        If Not objEquipmentSalesHeader Is Nothing Then
            objEquipmentSalesHeader.Kind = ddlJenis.SelectedValue
        End If
    End Sub

    Private Sub btnValidasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidasi.Click
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Baru Then
            objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Validasi
            objEquipmentSalesHeader.ValidateDate = DateTime.Now.Date
            objEquipmentFacade.Update(objEquipmentSalesHeader)
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Baru), CInt(EquipmentStatus.EquipmentStatusEnum.Validasi))
            sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
            lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
            BindHeaderToForm()
            BindDetailToGrid()
            SetButtonEditMode()
            btnValidasi.Enabled = False
            btnBatalValidasi.Enabled = True
        End If
    End Sub

    Private Sub btnBatalValidasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatalValidasi.Click
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Validasi Then
            objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Baru
            objEquipmentFacade.Update(objEquipmentSalesHeader)
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Validasi), CInt(EquipmentStatus.EquipmentStatusEnum.Baru))
            sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
            lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
            BindHeaderToForm()
            BindDetailToGrid()
            SetButtonEditMode()
            btnValidasi.Enabled = True
            btnBatalValidasi.Enabled = False
            dtgEquipmentOrder.ShowFooter = True
        End If
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        btnSimpan_Click(Nothing, Nothing)
        ClearField()
        Response.Redirect("frmCreateEquipmentOrder.aspx")
    End Sub

#End Region



End Class
