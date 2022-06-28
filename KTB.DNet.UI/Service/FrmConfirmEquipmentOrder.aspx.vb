Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security


Public Class FrmConfirmEquipmentOrder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblP3B As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenis As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents lblRencanaTglPengiriman As System.Web.UI.WebControls.Label
    Protected WithEvents Label21 As System.Web.UI.WebControls.Label
    Protected WithEvents icRencanaKirim As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblPermintaanTglPengiriman As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggapanKtb As System.Web.UI.WebControls.Label
    Protected WithEvents Label22 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKonfirmasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSeachTanggapanKTB As System.Web.UI.WebControls.Label
    Protected WithEvents lblPenjelasan As System.Web.UI.WebControls.Label
    Protected WithEvents txtPenjelasan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchPenjelasan As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubtotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubTotalValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTotalPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPN As System.Web.UI.WebControls.Label
    Protected WithEvents Label24 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPNValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubsidi As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubsidiValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgEqMaster As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnTolak As System.Web.UI.WebControls.Button
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPermintaanKirimValue As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenis As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnKonfirmasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnRilis1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnRilis2 As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearch1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoP3Bvalue As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoReqPOValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoReqPO As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents cBox As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnBatalTolak As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatalKonfirmasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatalRilis As System.Web.UI.WebControls.Button
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    'Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents lblSisaPCT As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPayPCT As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPembayaranValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaPembayaranValue As System.Web.UI.WebControls.Label

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
    Private objEquipmentSalesHeader As EquipmentSalesHeader
    Private objEquipmentSalesDetail As EquipmentSalesDetail
    Private objEquipmentFacade As New EquipmentSalesHeaderFacade(User)
    Private arlSalesDetail As ArrayList
    Private sessionHelper As New SessionHelper

#End Region

#Region "Custom Method"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then

            GetInformation()
            GetId()
            BindHeaderToForm()
            BindDetailToGrid()
            SetButton()
        End If
        lblSearchPenjelasan.Attributes("onclick") = "ShowPPPenjelasan();"
        lblSeachTanggapanKTB.Attributes("onclick") = "ShowPPKonfirmasi();"
        lblSearchTotalPembayaran.Attributes("onclick") = "ShowPPPembayaran();"
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.P3BResponseView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Respon P3B")
        End If
        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.P3BResponseSave_Privilege)
        'btnKonfirmasi.Visible = SecurityProvider.Authorize(Context.User, SR.P3BResponseConfirmation_Privilege)
        'btnBatalKonfirmasi.Visible = SecurityProvider.Authorize(Context.User, SR.P3BResponseConfirmation_Privilege)
        'btnRilis1.Visible = SecurityProvider.Authorize(Context.User, SR.P3BResponseRelease1_Privilege)
        'btnRilis2.Visible = SecurityProvider.Authorize(Context.User, SR.P3BResponseRelease2_Privilege)
        'btnBatalRilis.Visible = SecurityProvider.Authorize(Context.User, SR.P3BResponseRelease1_Privilege) Or SecurityProvider.Authorize(Context.User, SR.P3BResponseRelease2_Privilege)
        'btnTolak.Visible = SecurityProvider.Authorize(Context.User, SR.P3BResponseReject_Privilege)
        'btnBatalTolak.Visible = SecurityProvider.Authorize(Context.User, SR.P3BResponseReject_Privilege)
    End Sub

    Private Sub GetInformation()
        ViewState("NoP3B") = Request.QueryString("NoP3B")
        ViewState("TglP3BAwal") = Request.QueryString("TglP3BAwal")
        ViewState("TglP3BAkhir") = Request.QueryString("TglP3BAkhir")
        ViewState("Dealer") = Request.QueryString("Dealer")
        ViewState("Status") = Request.QueryString("Status")
        ViewState("Jenis") = Request.QueryString("Jenis")
    End Sub

    Private Sub GetId()
        Dim ID As Integer = Request.QueryString("Id")
        objEquipmentSalesHeader = New EquipmentSalesHeaderFacade(User).Retrieve(CInt(ID))
        ViewState("Count") = objEquipmentSalesHeader.EquipmentSalesDetails.Count
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
    End Sub

    Private Sub BindHeaderToForm()
        lblNoReqPOValue.Text = objEquipmentSalesHeader.RegPONumber
        lblNoP3Bvalue.Text = objEquipmentSalesHeader.PONumber
        lblKodeDealerValue.Text = objEquipmentSalesHeader.Dealer.DealerCode
        lblSearch1.Text = objEquipmentSalesHeader.Dealer.SearchTerm1
        'lblSubTotalValue.Text = FormatNumber(objEquipmentSalesHeader.Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTglPermintaanKirimValue.Text = Format(objEquipmentSalesHeader.ReqDeliveryDate, "dd/MM/yyyy")
        lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
        txtPenjelasan.Text = objEquipmentSalesHeader.DeteilRequirement
        txtKonfirmasi.Text = objEquipmentSalesHeader.ResponseDetail
        lblSubsidiValue.Text = FormatNumber(objEquipmentSalesHeader.Subsidi, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblSubTotalValue.Text = FormatNumber(objEquipmentSalesHeader.Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblPPNValue.Text = FormatNumber(10 * CType(objEquipmentSalesHeader.Total, Double) / 100, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalValue.Text = FormatNumber((CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)).ToString, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalPembayaranValue.Text = FormatNumber(objEquipmentSalesHeader.TotalPembayaran, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblSisaPembayaranValue.Text = FormatNumber(CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100) - CType(objEquipmentSalesHeader.TotalPembayaran, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        If objEquipmentSalesHeader.Total <> 0 Then
            lblTotalPayPCT.Text = "(" & FormatNumber(CType(objEquipmentSalesHeader.TotalPembayaran, Double) * 100 / (CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
            lblSisaPCT.Text = "(" & FormatNumber((CType(lblTotalValue.Text, Double) - CType(objEquipmentSalesHeader.TotalPembayaran, Double)) * 100 / (CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
        Else
            lblTotalPayPCT.Text = "(0%)"
            lblSisaPCT.Text = "(0%)"
        End If
        If objEquipmentSalesHeader.EstimateDeliveryDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
            icRencanaKirim.Value = objEquipmentSalesHeader.EstimateDeliveryDate
            icRencanaKirim.Enabled = True
            cBox.Checked = True
        Else
            'icRencanaKirim.Value = objEquipmentSalesHeader.EstimateDeliveryDate
            icRencanaKirim.Enabled = False
            cBox.Checked = False
        End If

        '--DropDownList Jenis
        Me.ddlJenis.DataSource = EquipmentKind.RetrieveEquipmentKind()
        Me.ddlJenis.DataTextField = "NameStatus"
        Me.ddlJenis.DataValueField = "valStatus"
        Me.ddlJenis.DataBind()
        Dim strKind As String = objEquipmentSalesHeader.Kind
        If strKind = "" Then
            strKind = "0"
        End If
        Me.ddlJenis.SelectedValue = strKind

    End Sub

    Private Sub BindDetailToGrid()
        dtgEqMaster.DataSource = objEquipmentSalesHeader.EquipmentSalesDetails
        dtgEqMaster.DataBind()
    End Sub

    Private Sub SetButton()
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        If objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Validasi Then
            btnRilis1.Enabled = False
            btnRilis2.Enabled = False
            btnKonfirmasi.Enabled = True
            btnBatalKonfirmasi.Enabled = False
            btnTolak.Enabled = True
            btnBatalTolak.Enabled = False
            btnBatalRilis.Enabled = False
        ElseIf objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Konfirmasi Then
            btnRilis1.Enabled = True
            btnRilis2.Enabled = True
            btnKonfirmasi.Enabled = False
            btnBatalKonfirmasi.Enabled = True
            btnTolak.Enabled = False
            btnBatalTolak.Enabled = False
            btnBatalRilis.Enabled = False
        ElseIf objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Ditolak Then
            btnRilis1.Enabled = False
            btnRilis2.Enabled = False
            btnKonfirmasi.Enabled = False
            btnBatalKonfirmasi.Enabled = False
            btnTolak.Enabled = False
            btnBatalTolak.Enabled = True
            btnBatalRilis.Enabled = False
        ElseIf objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1 Then
            btnRilis1.Enabled = False
            btnRilis2.Enabled = True
            btnKonfirmasi.Enabled = False
            btnBatalKonfirmasi.Enabled = False
            btnTolak.Enabled = False
            btnBatalTolak.Enabled = False
            btnBatalRilis.Enabled = True
        ElseIf objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2 Then
            btnRilis1.Enabled = False
            btnRilis2.Enabled = False
            btnKonfirmasi.Enabled = False
            btnBatalKonfirmasi.Enabled = False
            btnTolak.Enabled = False
            btnBatalTolak.Enabled = False
            btnBatalRilis.Enabled = True
        End If
    End Sub


    Private Sub CountTotalAndPPN(ByVal ArrList As ArrayList)
        lblSubsidiValue.Text = FormatNumber(Calculation.CountPESubsidi(ArrList), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblSubTotalValue.Text = FormatNumber(Calculation.CountPESubTotal(ArrList), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblPPNValue.Text = FormatNumber(Calculation.CountPEPPN(ArrList), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalValue.Text = FormatNumber(Calculation.CountPETotal(ArrList), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblSisaPembayaranValue.Text = FormatNumber(CType(lblTotalValue.Text, Double) - CType(objEquipmentSalesHeader.TotalPembayaran, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        If objEquipmentSalesHeader.Total <> 0 Then
            lblTotalPayPCT.Text = "(" & FormatNumber(CType(objEquipmentSalesHeader.TotalPembayaran, Double) * 100 / CType(lblTotalValue.Text, Double), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
            lblSisaPCT.Text = "(" & FormatNumber((CType(lblTotalValue.Text, Double) - CType(objEquipmentSalesHeader.TotalPembayaran, Double)) * 100 / CType(lblTotalValue.Text, Double), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
        Else
            lblTotalPayPCT.Text = "(0%)"
            lblSisaPCT.Text = "(0%)"
        End If
    End Sub
#End Region

#Region "EventHandlers"
    Private Function ValidateItem(ByVal kodeEquipment As String, ByVal Jumlah As String, ByVal Subsidi As String) As Boolean
        If (kodeEquipment = String.Empty Or Jumlah = String.Empty Or Subsidi = String.Empty) Then
            lblError.Text = "Error : Field Tidak boleh Kosong"
            Return False
        Else
            Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Status", MatchType.Exact, CType(EquipmentMasterStatus.EquipmentMasterStatusEnum.Aktive, Short)))
            criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "EquipmentNumber", MatchType.Exact, kodeEquipment))
            criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Kind", MatchType.Exact, (ddlJenis.SelectedValue)))
            Dim ArrEquipmentMaster As ArrayList = New EquipmentMasterFacade(User).Retrieve(criterias1)
            If (ArrEquipmentMaster.Count = 0) Then
                lblError.Text = "Error : Kode Equipment dan Jenis tidak Cocok"
                Return False
            End If
        End If
        Return True
    End Function

    Private Function ValidateDuplication(ByVal kodeEquipment As String) As Boolean
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        For Each item As EquipmentSalesDetail In objEquipmentSalesHeader.EquipmentSalesDetails
            If (item.EquipmentMaster.EquipmentNumber = kodeEquipment) Then
                lblError.Text = "Error : Duplikasi Kode Equipment"
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub dtgEqHeaderItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblKodeEquipmentFooter As Label = CType(e.Item.Cells(2).FindControl("lblKodeEquipmentFooter"), Label)
        lblKodeEquipmentFooter.Attributes("onclick") = "ShowPPKodeEquipmentSelection();"
    End Sub

    Sub dtgEqMaster_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Footer Then
            dtgEqHeaderItemFooter(e)
        End If
        If e.Item.ItemIndex <> -1 Then
            objEquipmentSalesDetail = objEquipmentSalesHeader.EquipmentSalesDetails(e.Item.ItemIndex)
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Cells(3).Text = objEquipmentSalesDetail.EquipmentMaster.Description
                'Dim txtPrice As TextBox = e.Item.FindControl("txtHargaSatuan")
                'txtPrice.Text = FormatNumber(objEquipmentSalesDetail.Price, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'Dim txtDiscount As TextBox = e.Item.FindControl("txtSubsidi")
                'txtDiscount.Text = FormatNumber(objEquipmentSalesDetail.Discount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Dim LblJumlahHarga As Label = e.Item.FindControl("lblJumlahHarga")
                If objEquipmentSalesDetail.Price <> 0 Then
                    LblJumlahHarga.Text = FormatNumber(objEquipmentSalesDetail.Price * objEquipmentSalesDetail.Quantity, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Else
                    LblJumlahHarga.Text = FormatNumber(objEquipmentSalesDetail.EquipmentMaster.Price * objEquipmentSalesDetail.Quantity, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                End If

                e.Item.Cells(9).Text = objEquipmentSalesDetail.EquipmentMaster.ID
                Dim LbtnKodeEq As LinkButton = CType(e.Item.FindControl("LbnKode"), LinkButton)
                LbtnKodeEq.Text = objEquipmentSalesDetail.EquipmentMaster.EquipmentNumber
                Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
                If e.Item.ItemIndex >= CInt(ViewState("Count")) Then
                    lbtnDelete.Visible = True
                Else
                    lbtnDelete.Visible = False
                End If
            End If
        End If
    End Sub

    Sub dtgEqMaster_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs)
        If e.CommandName = "Add" Then
            If Not Page.IsValid Then
                Return
            End If
            objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
            Dim txtKode As TextBox = e.Item.FindControl("txtKodeEquipmentFooter")
            Dim txtJumlah As TextBox = e.Item.FindControl("txtJumlahFooter")
            'Dim txtHargaSatuan As TextBox = e.Item.FindControl("txtHargaSatuanFooter")
            Dim txtSubsidi As TextBox = e.Item.FindControl("txtSubsidiFooter")
            If (ValidateItem(txtKode.Text.ToUpper, txtJumlah.Text, txtSubsidi.Text) And ValidateDuplication(txtKode.Text.ToUpper)) Then
                objEquipmentSalesDetail = New EquipmentSalesDetail
                Dim objEquipmentMaster As EquipmentMaster = New EquipmentMasterFacade(User).Retrieve(txtKode.Text)
                objEquipmentSalesDetail.EquipmentMaster = objEquipmentMaster
                objEquipmentSalesDetail.Quantity = txtJumlah.Text
                'objEquipmentSalesDetail.Price = txtHargaSatuan.Text
                objEquipmentSalesDetail.Discount = txtSubsidi.Text
            Else
                Exit Sub
            End If
            objEquipmentSalesHeader.EquipmentSalesDetails.Add(objEquipmentSalesDetail)
            sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
            BindDetailToGrid()
            dtgEqMaster.ShowFooter = True
            CountTotalAndPPN(objEquipmentSalesHeader.EquipmentSalesDetails)
        ElseIf e.CommandName = "Delete" Then
            objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
            Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")
            objEquipmentSalesHeader.EquipmentSalesDetails.Remove(objEquipmentSalesHeader.EquipmentSalesDetails.Item(CType(lbl1.Text, Integer) - 1))
            sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
            BindDetailToGrid()
        ElseIf e.CommandName = "Kode" Then
            sessionHelper.SetSession("PrevPage", Request.Url.ToString())
            Response.Redirect("frmEquipmentListDetail.aspx?Eq=" & e.Item.Cells(9).Text & "&Status=" & "detail")
        End If
    End Sub

    '--Update Status To Tidak_Setuju
    Private Sub btnTolak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTolak.Click
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Ditolak
        objEquipmentFacade.Update(objEquipmentSalesHeader)
        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Validasi), CInt(EquipmentStatus.EquipmentStatusEnum.Ditolak))
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
        lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
        SetButton()
    End Sub

    '--Update Status To Konfirmasi
    Private Sub btnKonfirmasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKonfirmasi.Click
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Konfirmasi
        objEquipmentSalesHeader.ResponseDate = DateTime.Now
        objEquipmentSalesHeader.ResponseBy = User.Identity.Name.Substring(6)
        objEquipmentFacade.Update(objEquipmentSalesHeader)
        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Validasi), CInt(EquipmentStatus.EquipmentStatusEnum.Konfirmasi))
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
        lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
        SetButton()
    End Sub

    '--Update Status To Rilis1
    Private Sub btnRilis1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis1.Click
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1
        objEquipmentSalesHeader.Validate1Date = DateTime.Now
        objEquipmentSalesHeader.Validate1By = User.Identity.Name
        objEquipmentFacade.Update(objEquipmentSalesHeader)
        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Konfirmasi), CInt(EquipmentStatus.EquipmentStatusEnum.Rilis1))
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
        lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
        SetButton()
    End Sub

    '--Update Status To Rilis2
    Private Sub btnRilis2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis2.Click
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis2
        objEquipmentSalesHeader.Validate2Date = DateTime.Now
        objEquipmentSalesHeader.Validate2By = User.Identity.Name
        objEquipmentFacade.Update(objEquipmentSalesHeader)
        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
        If objEquipmentSalesHeader.Validate1By <> String.Empty Then
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Rilis1), CInt(EquipmentStatus.EquipmentStatusEnum.Rilis2))
        Else
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Konfirmasi), CInt(EquipmentStatus.EquipmentStatusEnum.Rilis2))
        End If

        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
        lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
        SetButton()
    End Sub

    '--Update Status To Validasi
    Private Sub btnBatalTolak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatalTolak.Click
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Validasi
        objEquipmentFacade.Update(objEquipmentSalesHeader)
        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Ditolak), CInt(EquipmentStatus.EquipmentStatusEnum.Validasi))
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
        lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
        SetButton()
    End Sub

    '--Update Status To Validasi
    Private Sub btnBatalKonfirmasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatalKonfirmasi.Click
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Validasi
        objEquipmentFacade.Update(objEquipmentSalesHeader)
        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Konfirmasi), CInt(EquipmentStatus.EquipmentStatusEnum.Validasi))
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
        lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
        SetButton()
    End Sub

    '--Update Status To Rilis1 or Konfirmasi
    Private Sub btnBatalRilis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatalRilis.Click
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        If (objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1) Then
            objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Konfirmasi
        Else
            If (objEquipmentSalesHeader.Validate1By <> Nothing) AndAlso (objEquipmentSalesHeader.Validate1By <> String.Empty) Then
                If SecurityProvider.Authorize(Context.User, SR.P3BResponseRelease2_Privilege) Then
                    objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1
                Else
                    MessageBox.Show("Maaf, P3B ini Dalam status Rilis 2")
                End If
            Else
                objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Konfirmasi
            End If
        End If
        objEquipmentFacade.Update(objEquipmentSalesHeader)
        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
        If (objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Rilis1) Then
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Rilis2), CInt(EquipmentStatus.EquipmentStatusEnum.Rilis1))
        Else
            If (objEquipmentSalesHeader.Validate2By <> Nothing) AndAlso (objEquipmentSalesHeader.Validate2By <> String.Empty) Then
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Rilis2), CInt(EquipmentStatus.EquipmentStatusEnum.Rilis1))
            Else
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pembelian_Equipment), objEquipmentSalesHeader.RegPONumber, CInt(EquipmentStatus.EquipmentStatusEnum.Rilis1), CInt(EquipmentStatus.EquipmentStatusEnum.Konfirmasi))
            End If
        End If
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
        lblStatusValue.Text = CType(objEquipmentSalesHeader.Status, EquipmentStatus.EquipmentStatusEnum).ToString
        SetButton()
    End Sub
    '--Save To DataBase
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        'If objEquipmentSalesHeader.EquipmentSalesDetails.Count > 0 Then
        objEquipmentSalesHeader.ResponseDetail = txtKonfirmasi.Text
        If icRencanaKirim.Enabled Then
            objEquipmentSalesHeader.EstimateDeliveryDate = icRencanaKirim.Value
        Else
            objEquipmentSalesHeader.EstimateDeliveryDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        End If
        BindDetail(e)
        objEquipmentFacade.UpdateTransaction(objEquipmentSalesHeader)
        MessageBox.Show("Data Berhasil Disimpan")
        'Else
        'MessageBox.Show("Tidak Ada Detail")
        'End If
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
        SetButton()
        BindHeaderToForm()
    End Sub

    Private Sub BindDetail(ByVal e As System.EventArgs)
        For Each item As DataGridItem In dtgEqMaster.Items
            'Dim TxtBoxPrice As TextBox = item.FindControl("txtHargaSatuan")
            'CType(objEquipmentSalesHeader.EquipmentSalesDetails(item.ItemIndex), EquipmentSalesDetail).Price = TxtBoxPrice.Text
            Dim TxtBoxSubsidi As TextBox = item.FindControl("txtSubsidi")
            CType(objEquipmentSalesHeader.EquipmentSalesDetails(item.ItemIndex), EquipmentSalesDetail).Discount = TxtBoxSubsidi.Text
        Next
    End Sub

    Private Sub cBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cBox.CheckedChanged
        If cBox.Checked Then
            icRencanaKirim.Enabled = True
        Else
            icRencanaKirim.Enabled = False
        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("../Service/frmEquipmentOrderList.aspx?NoP3B=" & ViewState("NoP3B") & "&TglP3BAwal=" & ViewState("TglP3BAwal") & "&TglP3BAkhir=" & ViewState("TglP3BAkhir") & "&Dealer=" & ViewState("Dealer") & "&Status=" & ViewState("Status") & "&Jenis=" & ViewState("Jenis"))
    End Sub

#End Region




End Class
