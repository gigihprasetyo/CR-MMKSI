#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade
Imports System.IO
Imports System.Linq
Imports System.Collections.Generic
Imports System.Security.Principal
#End Region

Public Class PesananKendaraanBiasa
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisPesanan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitanAtauImport As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTahunPerakitanAtauImport As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTanggalPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblRencanaPenebusan As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlRencanaPenebusan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNomorPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorPesanan As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgPesananKendaraan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblKotaValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalPesananValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLagend As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPK As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPKValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnit As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnitValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaUnit As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaUnitPD As System.Web.UI.WebControls.Label
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button

    Protected WithEvents lblKodeDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents lblLagend2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblspaNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblspaDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents btnBaru As System.Web.UI.WebControls.Button
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label

    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents HideField As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtUrlToBack As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkGuarantee As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblGuarantee As System.Web.UI.WebControls.Label
    Protected WithEvents lblColonGuarantee As System.Web.UI.WebControls.Label

    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents lblDealerBranch As Label
    Protected WithEvents lblPopUpDealerBranch As Label
    Protected WithEvents txtBranchName As TextBox

    Protected WithEvents lbl2KodeCabang As Label
    Protected WithEvents spanPopUpDB As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents trDealerBranch As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents trBranchHilang As Global.System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents cbxIsConfirmation As System.Web.UI.WebControls.CheckBox

    Protected WithEvents UploadFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents lblEvidencePath As Label
    Protected WithEvents lblFileName As LinkButton
    Protected WithEvents lblUploadDok As Label
    Protected WithEvents lbltitik2Upload As Label
    Protected WithEvents lnkbtnFileName As LinkButton
    Protected WithEvents lbtnDeleteFile As LinkButton

    Protected WithEvents tdDealerBranch As Global.System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents tdTitik2Branch As Global.System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents tdBranchName As Global.System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents txtKonfirmasi As System.Web.UI.WebControls.TextBox

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
    Private objPkHeader As KTB.DNet.Domain.PKHeader
    Private objPKDetail As KTB.DNet.Domain.PKDetail
    Private Mode As enumMode.Mode
    Private PKNumber As String
    Private DealerCode As String
    Private objDealer As Dealer
    Private _sessionHelper As New SessionHelper

    Private FU As String = "UploadFile"
    Private FU_NAME As String = "FU_FileName"
    Private pkHelper As New PKHelper(New System.Security.Principal.GenericPrincipal(New GenericIdentity("SYSTEM"), Nothing))

#End Region

#Region "Custom Method"
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PKKondisiBiasa_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Pesanan Kendaraan Biasa")
        End If
        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPKBiasaSave_Privilege)
        btnBaru.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPKBiasaSave_Privilege)
        btnValidasi.Visible = SecurityProvider.Authorize(Context.User, SR.PKValidate_Privilege)
        btnDelete.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPKBiasaSave_Privilege)

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        lblTotalHargaUnit.Visible = isPriceVisible
        Label18.Visible = isPriceVisible
        lblTotalHargaUnitPD.Visible = isPriceVisible
        dtgPesananKendaraan.Columns(5).Visible = isPriceVisible
        dtgPesananKendaraan.Columns(6).Visible = isPriceVisible
        dtgPesananKendaraan.Columns(8).Visible = isPriceVisible
        dtgPesananKendaraan.Columns(9).Visible = isPriceVisible
    End Sub

    Private Sub SetDtgPesananKendaraanItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblFooterKodeModel As Label = CType(e.Item.FindControl("lblFooterKodeModel"), Label)
        lblFooterKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"
        Dim lblFooterKodeWarna As Label = CType(e.Item.FindControl("lblFooterKodeWarna"), Label)
        lblFooterKodeWarna.Attributes("onclick") = "ShowPPKodeWarnaSelection();"
    End Sub

    Private Sub SetDtgPesananKendaraanItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblEditKodeModel As Label = CType(e.Item.FindControl("lblEditKodeModel"), Label)
        lblEditKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"
        Dim lblEditKodeWarna As Label = CType(e.Item.FindControl("lblEditKodeWarna"), Label)
        lblEditKodeWarna.Attributes("onclick") = "ShowPPKodeWarnaSelection();"
    End Sub

    Private Sub SetButtonNewMode()
        btnValidasi.Enabled = False
        ddlKategori.Enabled = True
        ddlJenisPesanan.Enabled = True
        ddlTahunPerakitanAtauImport.Enabled = True
        ddlRencanaPenebusan.Enabled = True
        btnBaru.Enabled = True
        FillddlRencanaPenebusan()
    End Sub

    Private Sub FillddlRencanaPenebusan()
        objDealer = _sessionHelper.GetSession("DEALER")
        Dim arrList As ArrayList
        If ddlJenisPesanan.SelectedIndex = 0 OrElse ddlJenisPesanan.SelectedIndex = -1 Then
            arrList = LookUp.ArraylistMonth(False, 0, 6, DateTime.Now)
            If Not objDealer Is Nothing Then
                Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PKBulanan).ToString)
                If Not (objTransaction Is Nothing) Then
                    If objTransaction.Status = 0 Then
                        arrList.RemoveAt(arrList.Count - 1)
                    End If
                End If
            End If
        Else
            arrList = LookUp.ArraylistMonth(True, 0, 0, DateTime.Now)
        End If
        ddlRencanaPenebusan.DataSource = arrList
        ddlRencanaPenebusan.DataBind()
        ddlRencanaPenebusan.SelectedIndex = ddlRencanaPenebusan.Items.Count - 1
    End Sub

    Private Function IsExpired() As Boolean
        objPkHeader = _sessionHelper.GetSession("PK")
        If objPkHeader.ID <> 0 Then
            Dim PKDate As New DateTime(CInt(objPkHeader.RequestPeriodeYear), CInt(objPkHeader.RequestPeriodeMonth), 1, 0, 0, 0)
            Dim DateNow As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0)
            If (PKDate < DateNow) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub SetButtonEditMode()
        objPkHeader = _sessionHelper.GetSession("PK")
        ddlKategori.Enabled = False
        ddlJenisPesanan.Enabled = False
        ddlTahunPerakitanAtauImport.Enabled = False
        ddlRencanaPenebusan.Enabled = False
        Dim objDealerSession As Dealer = _sessionHelper.GetSession("DEALER")
        If (objPkHeader.PKStatus <> enumStatusPK.Status.Baru) OrElse (IsExpired()) OrElse ((Not (objPkHeader.Dealer Is Nothing)) AndAlso (objPkHeader.Dealer.ID <> objDealerSession.ID)) Then
            btnSimpan.Enabled = False
            btnBaru.Enabled = False
            btnDelete.Enabled = False
            btnValidasi.Enabled = False
            dtgPesananKendaraan.ShowFooter = False
            txtNomorPesanan.ReadOnly = True

        Else
            If objPkHeader.ID <> 0 Then
                btnBaru.Enabled = True
                btnValidasi.Enabled = True
            End If
        End If
    End Sub

    Private Sub BindDataToPage()
        If IsNothing(_sessionHelper.GetSession("PK")) Then
            objPkHeader = New KTB.DNet.Domain.PKHeader
            objPkHeader.PKStatus = enumStatusPK.Status.Baru
            'objDealer = Session("DEALER")
            'objPkHeader.Dealer = objDealer
            'objPkHeader.FreePPh22Indicator = objDealer.FreePPh22Indicator
            _sessionHelper.SetSession("PK", objPkHeader)

            '--CR PK 2020/02/03
            cbxIsConfirmation_CheckedChanged(Nothing, Nothing)

            ClearAllFields()
        Else
            objPkHeader = _sessionHelper.GetSession("PK")
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                BindHeaderToForm()
            End If
        End If
        BindDetailToGrid()
    End Sub

    Private Sub ClearAllFields()
        lblNomorPKValue.Text = String.Empty
        lblStatusValue.Text = enumStatusPK.Status.Baru.ToString
        ddlKategori.SelectedIndex = 0
        ddlJenisPesanan.SelectedIndex = 1
        ddlTahunPerakitanAtauImport.SelectedValue = DateTime.Now.Year.ToString
        ddlRencanaPenebusan.SelectedIndex = ddlRencanaPenebusan.Items.Count - 1
        txtNomorPesanan.Text = String.Empty
        lblTanggalPesananValue.Text = Format(DateTime.Now, "dd/MM/yyyy")
        'Start  :CR:Guarantee;By:Doni;For:Yurike;Date:20100223
        lblGuarantee.Visible = False
        lblColonGuarantee.Visible = False
        chkGuarantee.Visible = False
        chkGuarantee.Checked = False
        'End    :CR:Guarantee;By:Doni;For:Yurike;Date:20100223
        txtKonfirmasi.Text = String.Empty
    End Sub

    Private Sub BindHeaderToForm()
        objDealer = _sessionHelper.GetSession("DEALER")

        objPkHeader = _sessionHelper.GetSession("PK")
        lblKodeDealerValue.Text = objPkHeader.Dealer.DealerCode & " / " & objPkHeader.Dealer.SearchTerm1
        lblNomorPKValue.Text = objPkHeader.PKNumber
        lblStatusValue.Text = CType(objPkHeader.PKStatus, enumStatusPK.Status).ToString

        If Not IsNothing(objPkHeader.DealerBranch) Then
            txtDealerBranchCode.Text = objPkHeader.DealerBranch.DealerBranchCode
            txtBranchName.Text = objPkHeader.DealerBranch.Term1
            lblDealerBranch.Text = objPkHeader.DealerBranch.DealerBranchCode & " / " & objPkHeader.DealerBranch.Term1
            lbl2KodeCabang.Text = "Kode Cabang"
            If Not objPkHeader.PKStatus = enumStatusPK.Status.Baru OrElse CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
                trDealerBranch.Visible = False
                lbl2KodeCabang.Text = "Cabang Dealer"
                txtBranchName.Visible = False
                lblDealerBranch.Visible = True
                spanPopUpDB.Visible = False

                'trBranchHilang.Visible = False
                tdDealerBranch.Attributes.Add("style", "visibility: hidden")
                tdTitik2Branch.Attributes.Add("style", "visibility: hidden")
                tdBranchName.Attributes.Add("style", "visibility: hidden")
            End If
        End If


        If CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB OrElse Not objPkHeader.PKStatus = enumStatusPK.Status.Baru Then

            trDealerBranch.Visible = False
            lbl2KodeCabang.Text = "Cabang Dealer"
            txtBranchName.Visible = False
            lblDealerBranch.Visible = True
            spanPopUpDB.Visible = False

            'trBranchHilang.Visible = False
            tdDealerBranch.Attributes.Add("style", "visibility: hidden")
            tdTitik2Branch.Attributes.Add("style", "visibility: hidden")
            tdBranchName.Attributes.Add("style", "visibility: hidden")
        Else

        End If

        If objPkHeader.Category Is Nothing Then
            ddlKategori.SelectedValue = 0
        Else
            ddlKategori.SelectedValue = objPkHeader.Category.ID
        End If
        ddlJenisPesanan.SelectedValue = objPkHeader.OrderType
        If objPkHeader.OrderType = enumOrderType.OrderType.Tambahan Then
            Label14.Visible = False
        End If
        ddlTahunPerakitanAtauImport.SelectedValue = objPkHeader.ProductionYear
        Dim str As String = New DateTime(objPkHeader.RequestPeriodeYear, objPkHeader.RequestPeriodeMonth, objPkHeader.RequestPeriodeDay).ToString("MMM yyyy") ' Dim str As String = CType(CInt(objPkHeader.RequestPeriodeMonth) - 1, enumMonth.Month).ToString & " " & objPkHeader.RequestPeriodeYear.ToString
        Dim listItem As New ListItem(str)
        ddlRencanaPenebusan.Items.Add(listItem)
        txtNomorPesanan.Text = objPkHeader.DealerPKNumber
        lblTanggalPesananValue.Text = Format(objPkHeader.PKDate, "dd/MM/yyyy")

        If Not (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER AndAlso (objPkHeader.PKStatus = enumStatusPK.Status.Baru OrElse objPkHeader.PKStatus = enumStatusPK.Status.Batal OrElse objPkHeader.PKStatus = enumStatusPK.Status.Validasi OrElse objPkHeader.PKStatus = enumStatusPK.Status.Konfirmasi)) Then
            txtKonfirmasi.Text = objPkHeader.KTBResponse
        End If

        'Start  :CR:Guarantee;By:Doni;For:Yurike;Date:20100223
        lblGuarantee.Visible = False
        lblColonGuarantee.Visible = False
        chkGuarantee.Visible = False
        chkGuarantee.Checked = False

        Dim IsEditable As Boolean = False
        Dim IsHidden As Boolean = False

        If objPkHeader.JaminanID > 0 Then
            chkGuarantee.Checked = True
            IsHidden = False
            If objPkHeader.PKStatus = enumStatusPK.Status.Konfirmasi Then
                IsEditable = True
            Else
                IsEditable = False
            End If
        Else
            chkGuarantee.Checked = False
            IsHidden = True
            IsEditable = False
            If JaminanForPKH(objPkHeader).ID > 0 Then
                If objPkHeader.PKStatus = enumStatusPK.Status.Konfirmasi Then
                    IsHidden = False
                    IsEditable = True
                Else
                    IsHidden = False
                    IsEditable = False
                End If
            End If
        End If

        If IsHidden = False _
        AndAlso (objPkHeader.PKStatus = enumStatusPK.Status.Konfirmasi _
            Or objPkHeader.PKStatus = enumStatusPK.Status.Rilis _
            Or objPkHeader.PKStatus = enumStatusPK.Status.Setuju _
            Or objPkHeader.PKStatus = enumStatusPK.Status.Selesai) _
        Then
            If objPkHeader.PKStatus = enumStatusPK.Status.Konfirmasi AndAlso CType(Session("Dealer"), Dealer).Title <> EnumDealerTittle.DealerTittle.KTB Then
                chkGuarantee.Visible = False
                lblGuarantee.Visible = False
                lblColonGuarantee.Visible = False
            Else
                chkGuarantee.Visible = True
                lblGuarantee.Visible = True
                lblColonGuarantee.Visible = True
            End If
        Else
            chkGuarantee.Visible = False
            lblGuarantee.Visible = False
            lblColonGuarantee.Visible = False
        End If
        If IsEditable Then
            chkGuarantee.Enabled = True
        Else
            chkGuarantee.Enabled = False
        End If

        cbxIsConfirmation.Checked = If(objPkHeader.IsFormAConfirmation = 0, False, True)
        If Not IsNothing(_sessionHelper.GetSession("CallerPage")) Then
            If _sessionHelper.GetSession("CallerPage") = "ResponsePKOrder" Then
                cbxIsConfirmation.Enabled = False
            Else
                If objPkHeader.PKStatus = "0" Then
                    cbxIsConfirmation.Enabled = True
                Else
                    cbxIsConfirmation.Enabled = False
                End If
            End If
        Else
            cbxIsConfirmation.Enabled = False
        End If

        '--CR PK 2020/02/03
        lblEvidencePath.Text = objPkHeader.EvidencePath
        Dim _fileName() As String = lblEvidencePath.Text.Split("\")
        lblFileName.Text = _fileName(_fileName.Length - 1)
        lnkbtnFileName.Visible = True
        If lblFileName.Text.Trim <> "" Then
            cbxIsConfirmation.Checked = True
        End If

        Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
        Dim DestFullFilePath As String = fileInfo1.Directory.FullName

        If objPkHeader.EvidencePath.Trim <> "" Then
            Dim dataFile As String = DestFullFilePath & "\" & objPkHeader.EvidencePath

            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" OrElse
                dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".png" OrElse
                dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpeg" Then

                lnkbtnFileName.Text = "<img src=""../images/detail.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" >"
            Else
                lnkbtnFileName.Text = "<img src=""../images/detail.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);"">"
            End If
            lnkbtnFileName.Visible = True
            lnkbtnFileName.ToolTip = lblFileName.Text

            lblFileName.Visible = True
            _sessionHelper.SetSession(FU_NAME, dataFile)
            lblEvidencePath.Text = dataFile
            lblFileName.Text = Path.GetFileName(dataFile)
        End If
        cbxIsConfirmation_CheckedChanged(Nothing, Nothing)

        'If objPkHeader.JaminanID > 0 _
        'And (objPkHeader.PKStatus = enumStatusPK.Status.Konfirmasi _
        '    Or objPkHeader.PKStatus = enumStatusPK.Status.Rilis _
        '    Or objPkHeader.PKStatus = enumStatusPK.Status.Setuju _
        '    Or objPkHeader.PKStatus = enumStatusPK.Status.Selesai) Then
        '    lblGuarantee.Visible = True
        '    lblColonGuarantee.Visible = True
        '    chkGuarantee.Visible = True
        '    chkGuarantee.Checked = True
        'End If
        'End    :CR:Guarantee;By:Doni;For:Yurike;Date:20100223
    End Sub

    Private Sub BindDetailToGrid()
        objDealer = _sessionHelper.GetSession("DEALER")
        If (objPkHeader.PKStatus <> CType(enumStatusPK.Status.Baru, Short) OrElse ((Not (objPkHeader.Dealer Is Nothing)) AndAlso (objPkHeader.Dealer.ID <> objDealer.ID))) Then
            dtgPesananKendaraan.Columns(10).Visible = False
            dtgPesananKendaraan.Columns(11).Visible = False
            dtgPesananKendaraan.Columns(12).Visible = False
        End If
        dtgPesananKendaraan.DataSource = objPkHeader.PKDetails
        dtgPesananKendaraan.DataBind()
        lblTotalUnitValue.Text = FormatNumber(Calculation.CountPKUnit(objPkHeader.PKDetails), 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " Unit"
        lblTotalHargaUnitPD.Text = "Rp " & FormatNumber(Calculation.CountPKHargaTotal(objPkHeader.PKDetails), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    End Sub

    Private Sub RetrieveMaster()
        For Each item As ListItem In LookUp.ArrayJenisPesanan
            item.Selected = False
            If item.Value = 0 Then
                If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindBulanan_Privilege) Then
                    ddlJenisPesanan.Items.Add(item)
                End If
            Else
                If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindTambahan_Privilege) Then
                    ddlJenisPesanan.Items.Add(item)
                End If
            End If
        Next
        ddlJenisPesanan.ClearSelection()
        If Not objDealer Is Nothing Then
            lblKodeDealerValue.Text = objDealer.DealerCode & " / " & objDealer.SearchTerm1
            lblNamaDealerValue.Text = objDealer.DealerName
            lblKotaValue.Text = objDealer.City.CityName
            lblspaNumber.Text = objDealer.SPANumber

            If objDealer.SPADate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                lblspaDate.Text = Nothing
            Else
                lblspaDate.Text = Format(objDealer.SPADate, "dd MMMMMMMMMMMMMMM yyyy")
            End If

            If (ViewState("Mode") = enumMode.Mode.NewItemMode) Then
                Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, EnumDealerTransType.DealerTransKind.PKTambahan)
                If Not (objTransaction Is Nothing) And Not (ddlJenisPesanan.Items Is Nothing) Then
                    If objTransaction.Status = 0 Then
                        ddlJenisPesanan.Items.RemoveAt(1)
                    End If
                End If
            End If
        End If

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)

        For Each item As Category In arrayListCategory
            Dim listItem As New ListItem(item.CategoryCode, item.ID)
            listItem.Selected = False
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            End If
        Next
        ddlKategori.ClearSelection()
        'Dim str As String = ddlJenisPesanan.SelectedValue
        ddlTahunPerakitanAtauImport.DataSource = ListTahunPerakitan() 'LookUp.ArraylistYear(True, 10, 1, DateTime.Now.Year.ToString())
        ddlTahunPerakitanAtauImport.DataBind()
        lblTanggalPesananValue.Text = Format(DateTime.Now, "dd/MM/yyyy")
    End Sub
    Private Function ListTahunPerakitan()
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "Status", MatchType.Exact, 1))
        Dim arrList As ArrayList = New VechileColorIsActiveOnPKFacade(User).Retrieve(criteria)
        Dim list As List(Of VechileColorIsActiveOnPK) = arrList.Cast(Of VechileColorIsActiveOnPK)().ToList()
        Return From data In list Group data By keys = New With {Key data.ProductionYear} Into Group Select keys.ProductionYear Order By ProductionYear Descending
    End Function
    Private Function GetValidFromDocument()
        Dim Tanggal As DateTime = CType(ddlRencanaPenebusan.SelectedValue, DateTime)

        If CInt(ddlJenisPesanan.SelectedValue) = CInt(LookUp.EnumJenisPesanan.Bulanan) Then
            Tanggal = DateSerial(Tanggal.Year, Tanggal.Month, 1)
        ElseIf CInt(ddlJenisPesanan.SelectedValue) = CInt(LookUp.EnumJenisPesanan.Tambahan) Then
            'tambahan : pasti bulan ini
            Tanggal = DateSerial(Tanggal.Year, Tanggal.Month, Now.Day)
        End If
        Return Tanggal
    End Function

    Private Sub BindDataToObject()
        objPkHeader.Dealer = _sessionHelper.GetSession("DEALER")


        'If objPkHeader.Dealer Is Nothing Then
        '    Response.Redirect("../SessionExpired.htm")
        'End If
        objPkHeader.PKNumber = lblNomorPKValue.Text
        objPkHeader.PKType = "1"
        objPkHeader.Purpose = CType(LookUp.enumPurpose.Khusus, Int16)
        objPkHeader.Category = New CategoryFacade(User).Retrieve(ddlKategori.SelectedItem.ToString())
        objPkHeader.OrderType = ddlJenisPesanan.SelectedValue
        objPkHeader.ProductionYear = ddlTahunPerakitanAtauImport.SelectedValue
        Dim Tanggal As DateTime = GetValidFromDocument()
        objPkHeader.RequestPeriodeDay = Tanggal.Day
        objPkHeader.RequestPeriodeMonth = Tanggal.Month
        objPkHeader.RequestPeriodeYear = Tanggal.Year

        objPkHeader.PricingPeriodeDay = Tanggal.Day
        objPkHeader.PricingPeriodeMonth = Tanggal.Month
        objPkHeader.PricingPeriodeYear = Tanggal.Year
        If txtDealerBranchCode.Text <> "" Then
            Try
                objPkHeader.DealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text)
            Catch ex As Exception

            End Try
        End If
        If txtNomorPesanan.Text <> String.Empty Then
            Dim _DealerPKNumber As String
            _DealerPKNumber = txtNomorPesanan.Text.Replace(",", "").ToString()
            _DealerPKNumber = _DealerPKNumber.Replace(";", "").ToString()
            objPkHeader.DealerPKNumber = _DealerPKNumber
        End If
        Dim tgl() As String = lblTanggalPesananValue.Text.Split("/")
        objPkHeader.PKDate = New Date(CInt(tgl(2)), CInt(tgl(1)), CInt(tgl(0)))
        objPkHeader.HeadPKNumber = 0
        objPkHeader.Purpose = LookUp.enumPurpose.Biasa
        If objPkHeader.Dealer.FreePPh22Indicator = 0 Then
            Dim requestDateTime As New DateTime(objPkHeader.RequestPeriodeYear, objPkHeader.RequestPeriodeMonth, 1)
            If requestDateTime >= objPkHeader.Dealer.FreePPh22From AndAlso requestDateTime <= objPkHeader.Dealer.FreePPh22To Then
                objPkHeader.FreePPh22Indicator = objPkHeader.Dealer.FreePPh22Indicator
            Else
                objPkHeader.FreePPh22Indicator = 1
            End If
        Else
            objPkHeader.FreePPh22Indicator = objPkHeader.Dealer.FreePPh22Indicator
        End If
        Dim oJ As Jaminan = JaminanForPKH(objPkHeader)
        objPkHeader.JaminanID = IIf(oJ.ID > 0, oJ.ID, 0)
    End Sub

    Private Function JaminanForPKH2(ByVal oPKH As PKHeader) As Jaminan
        Dim oJFac As JaminanFacade = New JaminanFacade(User)
        Dim crtJ As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dt As Date = DateSerial(oPKH.RequestPeriodeYear, oPKH.RequestPeriodeMonth, 1)
        Dim arlJ As ArrayList = New ArrayList

        crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidFrom", MatchType.LesserOrEqual, dt))
        crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidTo", MatchType.GreaterOrEqual, dt))
        crtJ.opAnd(New Criteria(GetType(Jaminan), "Status", MatchType.Exact, 0)) 'EnumStatusSPL.RetrieveStatus
        arlJ = oJFac.Retrieve(crtJ)
        'Return IIf(arlJ.Count = 0, New Jaminan, CType(arlJ(0), Jaminan))
        For Each oJ As Jaminan In arlJ
            If (" " & oJ.DealerCode).IndexOf(oPKH.Dealer.DealerCode) > 0 And IsJDExistInPKD(oJ, oPKH) Then
                Return oJ
            End If
        Next
        Return New Jaminan

    End Function

    Private Function IsJDExistInPKD2(ByVal oJ As Jaminan, ByVal oPKH As PKHeader) As Boolean
        For Each oJD As JaminanDetail In oJ.JaminanDetailIn(oPKH.RequestPeriodeMonth, oPKH.RequestPeriodeYear)
            For Each oPKD As PKDetail In oPKH.PKDetails
                If oJD.VehicleTypeCode = oPKD.VehicleTypeCode And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = oPKH.Purpose) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

    Private Function GetDirUpload() As DirectoryInfo
        Return New DirectoryInfo(String.Format("{0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("PKFolder"), _
            KTB.DNet.Lib.WebConfig.GetValue("PKFileDirectory"), lblNomorPKValue.Text.Trim))
    End Function

    Function GetEvidencePath() As String
        Return String.Format("{0}\{1}", KTB.DNet.Lib.WebConfig.GetValue("PKFileDirectory"), Path.GetFileName(CType(_sessionHelper.GetSession(FU_NAME), String)))
    End Function

    Private Sub SaveFileToPKHeader(ByVal objPkHeader As PKHeader)
        Dim _filename As String = Path.GetFileName(CType(_sessionHelper.GetSession(FU_NAME), String))
        If Not IsNothing(_filename) Then
            If _filename.Trim().Length > 0 Then
                objPkHeader.EvidencePath = GetEvidencePath()
            End If
        End If
    End Sub

    Private Function SaveFile(ByVal _filename As String) As Boolean
        If IsNothing(_filename) OrElse _filename.ToString.Trim = "" Then Exit Function

        Dim nResult As Boolean = False
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("PKFileDirectory") & "\" & _filename      '-- Destination file
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo
        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(DestFile)

                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                Dim ext As String = System.IO.Path.GetExtension(CType(_sessionHelper.GetSession(FU_NAME), String))
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fuStream As Stream = (CType(_sessionHelper.GetSession(FU), Stream))
                Dim ibytes As Long = fuStream.Length
                Dim buffer(ibytes - 1) As Byte
                fuStream.Read(buffer, 0, ibytes)
                fuStream.Close()

                Dim fs As FileStream = New FileStream(DestFile, FileMode.Create)
                fs.Write(buffer, 0, ibytes)
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing

                nResult = True
            End If
        Catch ex As Exception
            nResult = False
            Throw ex
        End Try
        Return nResult
    End Function

    Private Sub SaveToDatabase()
        objPkHeader.FreeIntIndicator = 1
        objPkHeader.MaxTOPDate = New Date(1900, 1, 1)
        objPkHeader.MaxTopDay = 0
        objPkHeader.MaxTopIndicator = -1
        objPkHeader.IsFormAConfirmation = If(cbxIsConfirmation.Checked, 1, 0)
        SaveFileToPKHeader(objPkHeader)
        Dim int As Integer = New PKHeaderFacade(User).Insert(objPkHeader)
        If int > 0 Then
            Dim _filename As String = Path.GetFileName(CType(_sessionHelper.GetSession(FU_NAME), String))
            SaveFile(_filename)

            MessageBox.Show("Data Berhasil Disimpan")
            RefreshData(int)
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pesanan_Kendaraan), objPkHeader.PKNumber, -1, CInt(enumStatusPK.Status.Baru))
        End If
    End Sub

    Private Sub RefreshData(ByVal id As Integer)
        objPkHeader = New PKHeaderFacade(User).Retrieve(id)
        _sessionHelper.SetSession("PK", objPkHeader)
        BindHeaderToForm()
        BindDetailToGrid()
    End Sub

    Private Function ValidateItem(ByVal kodeModel As String, ByVal kodeWarna As String, ByVal Unit As String) As Boolean
        If (kodeWarna = String.Empty Or kodeModel = String.Empty Or Unit = String.Empty) Then
            lblError.Text = "Error : KodeTipe, Kode Warna, Unit (Permintaan Dealer) Tidak boleh Kosong"
            Return False
        Else
            Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Status", MatchType.No, "X"))
            criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "VechileTypeCode", MatchType.Exact, kodeModel))
            Dim ArrVehicleType As ArrayList = New VechileTypeFacade(User).Retrieve(criterias1)
            If (ArrVehicleType.Count = 0) OrElse (ArrVehicleType(0).Category.ID.ToString <> ddlKategori.SelectedValue.ToString) Then
                lblError.Text = "Error : Kode Tipe dan kategori tidak Cocok"
                Return False
            Else
                If (kodeWarna <> "ZZZZ" And kodeWarna <> "zzzz") Then
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "ColorCode", MatchType.Exact, kodeWarna.ToString))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, kodeModel.ToString))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.Status", MatchType.No, "X"))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "Status", MatchType.No, "x"))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "SpecialFlag", MatchType.No, "x"))
                    Dim ArrListVechileColor As ArrayList = New VechileColorFacade(User).Retrieve(criterias)
                    If (ArrListVechileColor Is Nothing) OrElse (ArrListVechileColor.Count = 0) Then
                        lblError.Text = "Error : Kode Warna dan Kode Tipe tidak Cocok"
                        Return False
                    End If
                End If
            End If
        End If
        Return True
    End Function

    Private Function ValidateDuplication(ByVal kodeModel As String, ByVal kodeWarna As String, ByVal Mode As String, ByVal Rowindex As Integer) As Boolean
        If (Mode = "Add") Then
            For Each item As PKDetail In objPkHeader.PKDetails
                If (item.VehicleTypeCode.ToString = kodeModel And item.VehicleColorCode.ToString = kodeWarna) Then
                    lblError.Text = "Error : Duplikasi KodeTipe dan KodeWarna"
                    Return False
                End If

            Next
        Else
            Dim i As Integer = 0
            For Each item As PKDetail In objPkHeader.PKDetails
                If (item.VehicleTypeCode.ToString = kodeModel And item.VehicleColorCode.ToString = kodeWarna) Then
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

    Private Sub SearchPKHeaderAndDetail()
        'Dim objDealer1 As Dealer = New DealerFacade(User).Retrieve(DealerCode)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKNumber", MatchType.Exact, PKNumber))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        Dim objPKHeader1 As PKHeader = New PKHeaderFacade(User).Retrieve(criterias)(0)
        _sessionHelper.SetSession("PK", objPKHeader1)
        objDealer = objPKHeader1.Dealer
    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs, ByRef shouldReturn As Boolean)
        shouldReturn = False
        Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")
        Mode = ViewState("Mode")
        If (Mode = enumMode.Mode.EditMode) Then
            If (CType(objPkHeader.PKStatus, Short) = CType(enumStatusPK.Status.Baru, Short)) Then
                If (objPkHeader.PKDetails.Count <> 1) Then
                    Dim pKDetailFacade As New PKDetailFacade(User)
                    pKDetailFacade.Delete(objPkHeader.PKDetails.Item(CType(lbl1.Text, Integer) - 1))
                Else
                    MessageBox.Show("PK Header Harus memiliki minimal 1 PK Detail")
                    shouldReturn = True : Exit Sub
                End If
            Else
                MessageBox.Show("Status PK Bukan PK Baru")
                shouldReturn = True : Exit Sub
            End If
        End If
        objPkHeader.PKDetails.Remove(objPkHeader.PKDetails.Item(CType(lbl1.Text, Integer) - 1))
        _sessionHelper.SetSession("PK", objPkHeader)
        BindDataToPage()
        Mode = ViewState("Mode")
        If objPkHeader.PKDetails.Count = 0 And Mode = enumMode.Mode.NewItemMode Then
            SetButtonNewMode()
        End If
    End Sub

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If
        'Cek Jika Bulanan Doank
        If CInt(ddlJenisPesanan.SelectedValue) = CInt(LookUp.EnumJenisPesanan.Bulanan) Then
            If (CheckDuplicate(objPkHeader)) Then
                Return
            End If
        End If
        BindDataToObject()
        Dim txt1 As TextBox = e.Item.FindControl("txtFooterKodeModel")
        Dim txt2 As TextBox = e.Item.FindControl("txtFooterKodeWarna")
        Dim txt3 As TextBox = e.Item.FindControl("txtFooterUnitPermintaanDealer")
        If (ValidateItem(txt1.Text.ToUpper, txt2.Text.ToUpper, txt3.Text) And ValidateDuplication(txt1.Text.ToUpper, txt2.Text.ToUpper, "Add", -1)) Then
            objPKDetail = New KTB.DNet.Domain.PKDetail
            objPKDetail.VehicleTypeCode = txt1.Text.ToUpper
            objPKDetail.VehicleColorCode = txt2.Text.ToUpper
            objPKDetail.VehicleColorName = New VechileColorFacade(User).Retrieve(objPKDetail.VehicleColorCode.ToString).ColorEngName
            objPKDetail.TargetQty = txt3.Text
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, CType(Session("DEALER"), Dealer).ID))
            If (objPKDetail.VehicleColorCode <> "ZZZZ") Then
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "ColorCode", MatchType.Exact, txt2.Text))
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, txt1.Text))
                objPKDetail.VechileColor = New VechileColorFacade(User).Retrieve(criterias2)(0)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objPKDetail.VechileColor.ID))
            Else
                Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "ColorCode", MatchType.Exact, "ZZZZ"))
                Dim objVehicleColor As VechileColor = New VechileColorFacade(User).Retrieve(criterias1)(0)
                objPKDetail.VechileColor = New VechileColorFacade(User).Retrieve(objVehicleColor.ID)
                objPKDetail.MaterialNumber = objPKDetail.VechileColor.MaterialNumber
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objVehicleColor.ID))
            End If
            Dim sortColl As SortCollection = New SortCollection
            'If (Not IsNothing("ValidFrom")) Then
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.ASC))
            'Else
            '    sortColl = Nothing
            'End If
            Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(criterias, sortColl)
            If (objPriceArrayList.Count <> 0) Then
                Dim objPrice As Price = Nothing
                For Each item As Price In objPriceArrayList
                    If item.ValidFrom <= Me.GetValidFromDocument() Then ' System.Convert.ToDateTime(ddlRencanaPenebusan.SelectedValue) Then
                        objPrice = item
                    End If
                Next
                If Not IsNothing(objPrice) Then
                    If objPrice.BasePrice <> 0 Then
                        objPKDetail.TargetAmount = Calculation.CountPKVehiclePrice(0, CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.BasePrice, Double), CType(objPrice.OptionPrice, Double), objPKDetail.ResponseSalesSurcharge)
                        objPKDetail.ResponseAmount = objPKDetail.TargetAmount
                        objPKDetail.TargetPPh22 = Calculation.CountPKPPh22(CType(objPrice.BasePrice, Double), 0, CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.PPh22, Double))
                        objPKDetail.ResponsePPh22 = objPKDetail.TargetPPh22
                    Else
                        MessageBox.Show("Harga Belum Ada")
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Harga Belum Ada")
                    Exit Sub
                End If
            Else
                MessageBox.Show("Harga Belum Ada")
                Exit Sub
            End If
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                Dim PkDetailFacade As New PKDetailFacade(User)
                objPKDetail.PKHeader = objPkHeader
                Dim i As Integer = PkDetailFacade.Insert(objPKDetail)

                objPKDetail.ID = i

            End If
        Else
            Exit Sub
        End If
        objPkHeader.PKDetails.Add(objPKDetail)

        BindDataToPage()
        SetButtonEditMode()
    End Sub
    Private Sub UpdateCommand(ByVal E As DataGridCommandEventArgs)
        objPkHeader = _sessionHelper.GetSession("PK")
        Dim lbl1 As Label = E.Item.FindControl("lblNo")
        Dim txt1 As TextBox = E.Item.FindControl("txtEditKodeModel")
        Dim txt2 As TextBox = E.Item.FindControl("txtEditKodeWarna")
        Dim txt3 As TextBox = E.Item.FindControl("txtEditUnitPermintaanDealer")
        If (ValidateItem(txt1.Text.ToUpper, txt2.Text.ToUpper, txt3.Text) And ValidateDuplication(txt1.Text.ToUpper, txt2.Text.ToUpper, "Edit", E.Item.ItemIndex)) Then
            objPKDetail = objPkHeader.PKDetails(E.Item.ItemIndex)
            objPKDetail.VehicleTypeCode = txt1.Text.ToUpper
            objPKDetail.VehicleColorCode = txt2.Text.ToUpper
            objPKDetail.VehicleColorName = New VechileColorFacade(User).Retrieve(objPKDetail.VehicleColorCode.ToString).ColorEngName
            objPKDetail.TargetQty = txt3.Text
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, CType(Session("DEALER"), Dealer).ID))
            If (objPKDetail.VehicleColorCode <> "ZZZZ") Then
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "ColorCode", MatchType.Exact, txt2.Text))
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, txt1.Text))
                objPKDetail.VechileColor = New VechileColorFacade(User).Retrieve(criterias2)(0)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objPKDetail.VechileColor.ID))
            Else
                Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "ColorCode", MatchType.Exact, "ZZZZ"))
                Dim objVehicleColor As VechileColor = New VechileColorFacade(User).Retrieve(criterias1)(0)
                objPKDetail.VechileColor = New VechileColorFacade(User).Retrieve(objVehicleColor.ID)
                objPKDetail.MaterialNumber = objPKDetail.VechileColor.MaterialNumber
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objVehicleColor.ID))
            End If
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.ASC))

            Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(criterias, sortColl)
            If (objPriceArrayList.Count <> 0) Then
                Dim objPrice As Price
                For Each item As Price In objPriceArrayList
                    If item.ValidFrom <= Me.GetValidFromDocument() Then ' System.Convert.ToDateTime(ddlRencanaPenebusan.SelectedValue) Then
                        objPrice = item
                    End If
                Next
                If objPrice.BasePrice <> 0 Then
                    objPKDetail.TargetAmount = Calculation.CountPKVehiclePrice(0, CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.BasePrice, Double), CType(objPrice.OptionPrice, Double), objPKDetail.ResponseSalesSurcharge)
                    objPKDetail.ResponseAmount = objPKDetail.TargetAmount
                    objPKDetail.TargetPPh22 = Calculation.CountPKPPh22(CType(objPrice.BasePrice, Double), 0, CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.PPh22, Double))
                    objPKDetail.ResponsePPh22 = objPKDetail.TargetPPh22
                Else
                    MessageBox.Show("Harga Belum Ada")
                    Exit Sub
                End If
            Else
                MessageBox.Show("Harga Belum Ada")
                Exit Sub
            End If
            _sessionHelper.SetSession("PK", objPkHeader)
            dtgPesananKendaraan.EditItemIndex = -1
            BindDetailToGrid()
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                Dim PKdetailFacade As New PKDetailFacade(User)
                objPKDetail = objPkHeader.PKDetails(E.Item.ItemIndex)
                objPKDetail.PKHeader = objPkHeader
                PKdetailFacade.Update(objPKDetail)
            End If
            dtgPesananKendaraan.ShowFooter = True
        End If
    End Sub
    Private Sub SetMode()
        If PKNumber = String.Empty And DealerCode = String.Empty Then
            Mode = enumMode.Mode.NewItemMode
            SetButtonNewMode()
            ViewState("Mode") = Mode
            _sessionHelper.RemoveSession("PK")
            objDealer = CType(_sessionHelper.GetSession("DEALER"), Dealer)
        Else
            'btnKembali.Disabled = False
            btnBack.Enabled = True
            SearchPKHeaderAndDetail()
            Mode = enumMode.Mode.EditMode
            SetButtonEditMode()
            ViewState("Mode") = Mode
        End If
    End Sub

    Private Function JaminanForPKH(ByVal oPKH As PKHeader) As Jaminan
        Dim oJFac As JaminanFacade = New JaminanFacade(User)
        Dim crtJ As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dt As Date = DateSerial(oPKH.RequestPeriodeYear, oPKH.RequestPeriodeMonth, 1)
        Dim arlJ As ArrayList = New ArrayList

        crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidFrom", MatchType.LesserOrEqual, dt))
        crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidTo", MatchType.GreaterOrEqual, dt))
        crtJ.opAnd(New Criteria(GetType(Jaminan), "Status", MatchType.Exact, 0)) 'EnumStatusSPL.RetrieveStatus
        arlJ = oJFac.Retrieve(crtJ)
        'Return IIf(arlJ.Count = 0, New Jaminan, CType(arlJ(0), Jaminan))
        For Each oJ As Jaminan In arlJ
            If (" " & oJ.DealerCode).IndexOf(oPKH.Dealer.DealerCode) > 0 And IsJDExistInPKD(oJ, oPKH) Then
                Return oJ
            End If
        Next
        Return New Jaminan

    End Function


    Private Function IsJDExistInPKD(ByVal oJ As Jaminan, ByVal oPKH As PKHeader) As Boolean
        For Each oJD As JaminanDetail In oJ.JaminanDetailIn(oPKH.RequestPeriodeMonth, oPKH.RequestPeriodeYear)
            For Each oPKD As PKDetail In oPKH.PKDetails
                If oJD.VehicleTypeCode = oPKD.VehicleTypeCode And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = oPKH.Purpose) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

#End Region

#Region "EventHandler"

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        If Not Page.IsValid Then
            Return
        End If
        If btnSimpan.Enabled Then
            btnSimpan_Click(Nothing, Nothing)
        End If
        Response.Redirect("../PK/PesananKendaraanBiasa.aspx")
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If _sessionHelper.GetSession("PrevPage") Is Nothing Then
            txtUrlToBack.Text = ""
        Else
            txtUrlToBack.Text = _sessionHelper.GetSession("PrevPage")
        End If
        If txtUrlToBack.Text.Contains("ResponsePKOrder") Then

        End If

        txtKonfirmasi.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then
            UploadFile.Attributes("onchange") = "UploadFiles(this)"

            '------------------------------------------
            'added by soni 20170524, req by isye
            'if transactionControl non Active, pass Freeze Function
            Dim FreezeFuncStatus As Boolean = True
            'only for PK Tambahan
            'If ddlJenisPesanan.SelectedItem.Value = EnumDealerTransType.DealerTransKind.PKTambahan Then
            'cek transactionControl
            Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(_sessionHelper.GetSession("DEALER").ID, CInt(EnumDealerTransType.DealerTransKind.FreezePK).ToString)
            If Not (objTransaction Is Nothing) Then
                If objTransaction.Status = 0 Then
                    FreezeFuncStatus = False
                End If
            End If

            'End If
            '-------------------------------------------

            'added by anh 20140626 , req by yurike
            'remove filter for 2 days 
            If Not ((Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 26).ToString("yyyyMMdd")) Or (Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 27).ToString("yyyyMMdd"))) And FreezeFuncStatus Then
                _sessionHelper.SetSession("IsInPeriodForFreezePK", CommonFunction.IsInPeriodForFreezePK(User))
            Else
                _sessionHelper.SetSession("IsInPeriodForFreezePK", False)
            End If

            _sessionHelper.SetSession("IsInPeriodForFreezePK", FreezeFuncStatus)
            ActivateUserPrivilege()
            'ViewState("Source") = Request.QueryString("Src")
            'ViewState("Count") = 0
            'If Request.QueryString("key") <> String.Empty Then
            '    If Not KTB.DNet.UI.GlobalKey.IsKeyValid(Request.QueryString("key")) Then
            '        Response.Redirect("../frmAccessDenied.aspx?modulName=Pesanan Kendaraan Biasa")
            '    End If
            'PKNumber = DNetEncryption.SymmetricDecrypt(Request.QueryString("PKNumber"), sessionHelper.GetSession("KEY"))
            'DealerCode = DNetEncryption.SymmetricDecrypt(Request.QueryString("DealerCode"), sessionHelper.GetSession("KEY"))
            PKNumber = Request.QueryString("PKNumber")
            DealerCode = Request.QueryString("DealerCode")
            ' End If
            SetMode()
            RetrieveMaster()
            BindDataToPage()
            SetDefault()
        End If
        'Hidden1.Value = CInt(ViewState("Count")) + 1
        'ViewState("Count") = Hidden1.Value
        btnDelete.Attributes.Add("OnClick", "return confirm('Yakin PK ini akan dihapus?');")
        txtDealerBranchCode.Attributes.Add("ReadOnly", "ReadOnly")
        lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
        txtBranchName.Attributes.Add("ReadOnly", "ReadOnly")

        'btnKembali.Attributes.Add("OnClick", "window.history.go(-1)")
    End Sub

    Private Sub SetDefault()
        If ddlJenisPesanan.SelectedIndex = 1 Then
            ddlRencanaPenebusan.DataSource = Nothing
            ddlRencanaPenebusan.DataSource = LookUp.ArraylistMonth(True, 0, 0, DateTime.Now)
            ddlRencanaPenebusan.DataBind()
            Label14.Visible = False
        End If
    End Sub

    Private Sub ddlJenisPesanan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlJenisPesanan.SelectedIndexChanged
        If ddlJenisPesanan.SelectedIndex = 0 Then
            objDealer = _sessionHelper.GetSession("DEALER")
            Dim arrList As ArrayList = LookUp.ArraylistMonth(False, 0, 6, DateTime.Now)
            Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PKBulanan).ToString)
            If Not (objTransaction Is Nothing) Then
                If objTransaction.Status = 0 Then
                    arrList.RemoveAt(arrList.Count - 1)
                End If
            End If
            ddlRencanaPenebusan.DataSource = arrList
            ddlRencanaPenebusan.DataBind()
            ddlRencanaPenebusan.SelectedIndex = arrList.Count - 1
            Label14.Visible = True
        ElseIf ddlJenisPesanan.SelectedIndex = 1 Then
            ddlRencanaPenebusan.DataSource = LookUp.ArraylistMonth(True, 0, 0, DateTime.Now)
            ddlRencanaPenebusan.DataBind()
            Label14.Visible = False
        End If
    End Sub

    Sub dtgPesananKendaraan_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs)
        objPkHeader = _sessionHelper.GetSession("PK")
        Select Case (e.CommandName)
            Case "Delete"
                Dim lShouldReturn As Boolean
                DeleteCommand(e, lShouldReturn)
                If lShouldReturn Then
                    Return
                End If
            Case "Add"
                If Not Page.IsValid Then
                    Return
                End If

                Dim txtColorCode As TextBox = e.Item.FindControl("txtFooterKodeWarna")
                Dim txtVechileTypeCode As TextBox = e.Item.FindControl("txtFooterKodeModel")
                If Not pkHelper.PKDetailMatchingCCVCValidation(txtColorCode.Text, txtVechileTypeCode.Text, ddlTahunPerakitanAtauImport.SelectedValue) Then
                    MessageBox.Show(String.Format("Tipe Kendaraan {0}{1} Assy Year {2} tidak dapat dilakukan pengajuan PK",
                                                  txtVechileTypeCode.Text, txtColorCode.Text, ddlTahunPerakitanAtauImport.SelectedValue))
                    Return
                End If
                AddCommand(e)
        End Select
    End Sub

    Sub dtgPesananKendaraan_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        objPkHeader = _sessionHelper.GetSession("PK")
        dtgPesananKendaraan.ShowFooter = False
        dtgPesananKendaraan.EditItemIndex = CInt(e.Item.ItemIndex)
        BindDetailToGrid()
    End Sub

    Sub dtgPesananKendaraan_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dtgPesananKendaraan.EditItemIndex = -1
        BindDetailToGrid()
        dtgPesananKendaraan.ShowFooter = True
    End Sub

    Sub dtgPesananKendaraan_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If
        Dim txtColorCode As TextBox = E.Item.FindControl("txtEditKodeWarna")
        Dim txtVechileTypeCode As TextBox = E.Item.FindControl("txtEditKodeModel")
        If Not pkHelper.PKDetailMatchingCCVCValidation(txtColorCode.Text, txtVechileTypeCode.Text, ddlTahunPerakitanAtauImport.SelectedValue) Then
            MessageBox.Show(String.Format("Tipe Kendaraan {0}{1} Assy Year {2} tidak dapat dilakukan pengajuan PK",
                                          txtVechileTypeCode.Text, txtColorCode.Text, ddlTahunPerakitanAtauImport.SelectedValue))
            Return
        End If
        UpdateCommand(E)
        'RequiredFieldValidator1.Enabled = True
    End Sub

    Private Function CheckDuplicate(ByVal objPkHeader As PKHeader) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "OrderType", MatchType.Exact, CInt(LookUp.EnumJenisPesanan.Bulanan)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Purpose", MatchType.Exact, CInt(LookUp.enumPurpose.Biasa)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "ProductionYear", MatchType.Exact, ddlTahunPerakitanAtauImport.SelectedValue))
        Dim objDatetime As DateTime = System.Convert.ToDateTime(ddlRencanaPenebusan.SelectedValue)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeMonth", MatchType.Exact, objDatetime.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeYear", MatchType.Exact, objDatetime.Year))
        objDealer = Session("DEALER")
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(PKHeader), "ID", Sort.SortDirection.DESC))

        Dim ArrPKHeader As ArrayList = New PKHeaderFacade(User).Retrieve(criterias, sortColl)
        If ArrPKHeader.Count > 0 Then
            Dim pk As PKHeader = CType(ArrPKHeader(0), PKHeader)
            If pk.PKStatus = enumStatusPK.Status.Batal Then
                Return False
            End If
            If pk.PKStatus = enumStatusPK.Status.Ditolak Then
                Return False
            End If
            If ((pk.PKStatus <> enumStatusPK.Status.Batal) Or (pk.PKStatus <> enumStatusPK.Status.Ditolak)) AndAlso CType(ArrPKHeader(0), PKHeader).PKNumber <> objPkHeader.PKNumber Then
                MessageBox.Show("Maaf, Anda sudah memiliki PK No Reg PK " & CType(ArrPKHeader(0), PKHeader).PKNumber & " ,bila ada perubahan, silahkan direvisi.")
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function IsCVAllowedForTambahanAprilAndMay2011(ByVal CategoryCode As String, ByVal JenisPesanan As Integer, ByVal RencanaTebus As String) As Boolean
        Dim Apr2011 As String = DateSerial(2011, 4, 1).ToString("MMM yyyy") 'based on LookUp.ArraylistMonth
        Dim May2011 As String = DateSerial(2011, 5, 1).ToString("MMM yyyy")



        'Dim RencanaTebus As String = ddlRencanaPenebusan.SelectedItem.Text

        'If Me.ddlKategori.SelectedItem.Text.Trim.ToUpper = "CV" _
        'AndAlso CType(ddlJenisPesanan.SelectedValue, Integer) = CInt(LookUp.EnumJenisPesanan.Bulanan) _
        'AndAlso (RencanaTebus = Apr2011 OrElse RencanaTebus = May2011) Then
        '    MessageBox.Show("Data Tidak Bisa Disimpan. Sudah Melewati Batas Waktu Input Yang Telah Ditentukan MMKSI (tgl 21 Maret 2011)")
        '    Return False
        'End If
        '2011.04.19 ->Allow for May
        If CategoryCode.Trim.ToUpper = "CV" _
        AndAlso JenisPesanan = CInt(LookUp.EnumJenisPesanan.Bulanan) _
        AndAlso (RencanaTebus = Apr2011) Then 'AndAlso (RencanaTebus = Apr2011 OrElse RencanaTebus = May2011) Then
            MessageBox.Show("Data Tidak Bisa Disimpan. Sudah Melewati Batas Waktu Input Yang Telah Ditentukan MMKSI (tgl 21 Maret 2011)")
            Return False
        End If
        Return True
    End Function

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        ' Modified by Ikhsan, 09 Agustus 2008
        ' Requested by Yurike, to make sure that PK Tambahan is paid in the same month as PKDate.
        ' Start Modification ----------------------------------------------
        objDealer = _sessionHelper.GetSession("DEALER")
        If ddlJenisPesanan.SelectedIndex = 1 And ddlRencanaPenebusan.SelectedValue.ToString <> Format(DateTime.Now, "MMM yyyy") Then
            MessageBox.Show("Bulan Tebus PK tambahan tidak sesuai")
        Else
            'RequiredFieldValidator1.Enabled = True
            If Not Page.IsValid Then
                Return
            End If

            '--CR PK 2020/02/03
            If cbxIsConfirmation.Checked = True Then
                If lblFileName.Text.Trim = "" Then
                    MessageBox.Show("Silahkan untuk upload file dahulu")
                    Return
                End If
            End If
            '----

            If IsCVAllowedForTambahanAprilAndMay2011(Me.ddlKategori.SelectedItem.Text, CType(Me.ddlJenisPesanan.SelectedValue, Integer), Me.ddlRencanaPenebusan.SelectedItem.Text) = False Then Exit Sub

            objPkHeader = _sessionHelper.GetSession("PK")
            If (ddlJenisPesanan.Items.Count > 0 And ddlKategori.Items.Count > 0) Then
                If Not (objPkHeader.PKDetails.Count = 0) Then
                    Mode = ViewState("Mode")
                    BindDataToObject()

                    If Mode = enumMode.Mode.NewItemMode Then
                        Dim stockRatioProblem As String = ""
                        If (New TransactionControlPKFacade(User).IsTransactionPKBlocked(objPkHeader, objDealer, EnumTransactionControlPKKind.TransactionControlPKKind.INPUT_PK_TAMBAHAN, stockRatioProblem)) Then
                            If stockRatioProblem.Trim <> "" Then
                                MessageBox.Show("PK Tambahan Tidak dapat disimpan, Stock Ratio tidak memenuhi target, Hubungi MMKSI; " & stockRatioProblem)
                                Exit Sub
                            End If
                        End If
                    End If

                    If cbxIsConfirmation.Checked = False Then
                        lbtnDeleteFile_Click(Nothing, Nothing)
                    End If

                    If Mode = enumMode.Mode.NewItemMode Then
                        'Dim stockRatioProblem As String = ""
                        'If (New TransactionControlPKFacade(User).IsTransactionPKBlocked(objPkHeader, objDealer, EnumTransactionControlPKKind.TransactionControlPKKind.INPUT_PK_TAMBAHAN, stockRatioProblem)) Then
                        '    If stockRatioProblem.Trim <> "" Then
                        '        MessageBox.Show("PK Tambahan Tidak dapat disimpan, Stock Ratio tidak memenuhi target, Hubungi MMKSI; " & stockRatioProblem)
                        '        Exit Sub
                        '    End If
                        'End If

                        SaveToDatabase()
                        Mode = enumMode.Mode.EditMode
                        ViewState("Mode") = Mode
                        SetButtonEditMode()
                    Else
                        Dim objPKHeaderFacade As New PKHeaderFacade(User)
                        objPkHeader.IsFormAConfirmation = If(cbxIsConfirmation.Checked, 1, 0)

                        If (Not _sessionHelper.GetSession(FU_NAME) Is Nothing) OrElse (Not _sessionHelper.GetSession(FU) Is Nothing) Then
                            SaveFileToPKHeader(objPkHeader)
                        Else
                            objPkHeader.EvidencePath = ""
                        End If
                        If objPKHeaderFacade.Update(objPkHeader) <> -1 Then
                            If Not _sessionHelper.GetSession(FU) Is Nothing Then
                                Dim _filename As String = Path.GetFileName(CType(_sessionHelper.GetSession(FU_NAME), String))
                                SaveFile(_filename)
                            End If

                            MessageBox.Show("Data Berhasil Disimpan")
                            RefreshData(objPkHeader.ID)
                        Else
                            MessageBox.Show("Data Gagal Disimpan")
                        End If
                    End If
                Else
                    MessageBox.Show("Belum ada PK Detail")
                End If
            Else
                MessageBox.Show("Maaf, Anda Tidak punya akses untuk membuat PK")
            End If
            dtgPesananKendaraan.ShowFooter = True
        End If
        ' End Modification ------------------------------------------------        
    End Sub

    Sub dtgPesananKendaraan_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs)
        objPkHeader = _sessionHelper.GetSession("PK")
        Dim _dealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        If E.Item.ItemType = ListItemType.Footer Then
            SetDtgPesananKendaraanItemFooter(E)
        ElseIf E.Item.ItemType = ListItemType.EditItem OrElse E.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgPesananKendaraanItemEdit(E)
        End If
        If Not (objPkHeader.PKDetails.Count = 0 Or E.Item.ItemIndex = -1) Then
            objPKDetail = objPkHeader.PKDetails(E.Item.ItemIndex)
            E.Item.Cells(1).Text = objPKDetail.VechileColor.MaterialDescription.ToString
            'E.Item.Cells(4).Text = FormatNumber(CType(objPKDetail.TargetQty, Integer), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(5).Text = FormatNumber(CType(objPKDetail.TargetAmount, Long) * CType(objPKDetail.TargetQty, Long), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(6).Text = FormatNumber(CType(objPKDetail.TargetPPh22, Long) * CType(objPKDetail.TargetQty, Long) * CInt(objPkHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Start  :RemainModule-DailyPO:FreePPh By:Doni N
            'E.Item.Cells(6).Text = FormatNumber(CType(objPKDetail.TargetPPh22, Long) * CType(objPKDetail.TargetQty, Long) * IIf(CInt(objPkHeader.FreePPh22Indicator) = 1, 0, 1), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'End    :RemainModule-DailyPO:FreePPh By:Doni N
            Dim lblHarga As Label = CType(E.Item.FindControl("lblHarga"), Label)
            If ((objPkHeader.PKStatus = enumStatusPK.Status.Konfirmasi And _dealer.Title = EnumDealerTittle.DealerTittle.KTB)) OrElse (objPkHeader.PKStatus = enumStatusPK.Status.Rilis) OrElse (objPkHeader.PKStatus = enumStatusPK.Status.Setuju) OrElse (objPkHeader.PKStatus = enumStatusPK.Status.Tidak_Setuju) OrElse (objPkHeader.PKStatus = enumStatusPK.Status.DiBlok) OrElse (objPkHeader.PKStatus = enumStatusPK.Status.Selesai) Then
                E.Item.Cells(7).Text = FormatNumber(objPKDetail.ResponseQty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'E.Item.Cells(8).Text = FormatNumber(CType(objPKDetail.ResponseAmount, Long) * CType(objPKDetail.ResponseQty, Long), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                E.Item.Cells(9).Text = FormatNumber(CType(objPKDetail.ResponsePPh22, Long) * CType(objPKDetail.ResponseQty, Long) * CInt(objPkHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                'E.Item.Cells(9).Text = FormatNumber(CType(objPKDetail.ResponsePPh22, Long) * CType(objPKDetail.ResponseQty, Long) * IIf(CInt(objPkHeader.FreePPh22Indicator) = 1, 0, 1), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'End    :RemainModule-DailyPO:FreePPh By:Doni N
                lblHarga.Text = FormatNumber(CType(objPKDetail.ResponseAmount, Long) * CType(objPKDetail.ResponseQty, Long), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                If objPKDetail.ResponseDiscount > 0 OrElse objPKDetail.ResponseSalesSurcharge > 0 Then
                    Dim txtTooltips As String = String.Empty
                    If objPKDetail.ResponseDiscount > 0 Then
                        txtTooltips = txtTooltips & "Disc: " & FormatNumber(objPKDetail.ResponseDiscount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    End If
                    If objPKDetail.ResponseSalesSurcharge > 0 Then
                        txtTooltips = txtTooltips & Chr(13) & Chr(10) & "Surcharge: " & FormatNumber(objPKDetail.ResponseSalesSurcharge, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    End If
                    lblHarga.ToolTip = txtTooltips
                End If
            Else
                E.Item.Cells(7).Text = "0"
                'E.Item.Cells(8).Text = "0"
                E.Item.Cells(9).Text = "0"
                lblHarga.Text = "0"

            End If
        End If
    End Sub

    Private Sub btnValidasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidasi.Click


        If Not Page.IsValid Then
            Return
        End If
        RequiredFieldValidator1.Enabled = True
        objPkHeader = _sessionHelper.GetSession("PK")

        'added by anh 20140626 , req by yurike
        'remove filter for 2 days 
        If Not ((Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 26).ToString("yyyyMMdd")) Or (Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 27).ToString("yyyyMMdd"))) Then
            If objPkHeader.FreezeStatus(Session("IsInPeriodForFreezePK")) = enumStatusPK.EnumFreezeStatus.Freeze And Convert.ToBoolean(Session("IsInPeriodForFreezePK")) Then
                MessageBox.Show("Pengajuan PK Tambahan untuk periode bulan ini sudah ditutup")
                Return
            End If
        End If

        If Not objPkHeader.PKDetails.Count = 0 Then
            If (objPkHeader.PKStatus = enumStatusPK.Status.Baru) Then
                BindDataToObject()
                Dim objPKHeaderFacade As New PKHeaderFacade(User)
                objPkHeader.PKStatus = enumStatusPK.Status.Validasi
                If objPKHeaderFacade.Update(objPkHeader) <> -1 Then
                    MessageBox.Show("Validasi Berhasil")
                    RefreshData(objPkHeader.ID)
                    SetButtonEditMode()

                    Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                    objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pesanan_Kendaraan), objPkHeader.PKNumber, CInt(enumStatusPK.Status.Baru), CInt(enumStatusPK.Status.Validasi))
                Else
                    MessageBox.Show("Validasi Gagal")
                End If
            Else
                lblError.Text = "Error : Status PK Bukan PK Baru"
            End If
        Else
            MessageBox.Show("Belum ada PK Detail")
        End If
        dtgPesananKendaraan.ShowFooter = True
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        objPkHeader = _sessionHelper.GetSession("PK")
        If Not (objPkHeader Is Nothing) Then
            If (objPkHeader.PKNumber <> String.Empty) Then
                Dim objPKHeaderFacade As New PKHeaderFacade(User)
                objPKHeaderFacade.Delete(objPkHeader)
            End If
            _sessionHelper.RemoveSession("PK")
            BindDataToPage()
            Mode = enumMode.Mode.NewItemMode
            SetButtonNewMode()
            ViewState("Mode") = Mode
        End If
        dtgPesananKendaraan.ShowFooter = True
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If (Not UploadFile.PostedFile Is Nothing) And UploadFile.PostedFile.ContentLength > 0 Then
            If (Not UploadFile.PostedFile Is Nothing) And UploadFile.PostedFile.ContentLength > 0 Then
                Dim ext As String = System.IO.Path.GetExtension(UploadFile.PostedFile.FileName)
                If Not (ext.ToUpper() = ".JPG" _
                        OrElse ext.ToUpper() = ".JPEG" _
                        OrElse ext.ToUpper() = ".TXT" _
                        OrElse ext.ToUpper() = ".DOC" _
                        OrElse ext.ToUpper() = ".DOCX" _
                        OrElse ext.ToUpper() = ".XLS" _
                        OrElse ext.ToUpper() = ".XLSX" _
                        OrElse ext.ToUpper() = ".PNG" _
                        OrElse ext.ToUpper() = ".PDF") Then
                    MessageBox.Show("Hanya menerima file format (DOC/XLS/DOCX/XLSX/TXT/PDF/PNG/JPG/JPEG)")
                    lbtnDeleteFile_Click(Nothing, Nothing)
                    Return
                End If
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
                If UploadFile.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    lbtnDeleteFile_Click(Nothing, Nothing)
                    Return
                End If

                _sessionHelper.SetSession(FU, UploadFile.PostedFile.InputStream)
                _sessionHelper.SetSession(FU_NAME, UploadFile.PostedFile.FileName)
                lblEvidencePath.Text = UploadFile.PostedFile.FileName
                lblFileName.Text = Path.GetFileName(UploadFile.PostedFile.FileName)
                If lblFileName.Text.Trim <> "" Then
                    lbtnDeleteFile.Visible = True
                    lnkbtnFileName.Visible = True
                Else
                    lbtnDeleteFile.Visible = False
                    lnkbtnFileName.Visible = False
                End If
            Else
                MessageBox.Show("Upload file belum diisi\n")
            End If
        Else
            _sessionHelper.SetSession(FU, Nothing)
            _sessionHelper.SetSession(FU_NAME, Nothing)
        End If
    End Sub

    'Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
    '    If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
    '        Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
    '    Else
    '        Response.Redirect("../login.aspx")
    '    End If
    'End Sub


    Private Sub cbxIsConfirmation_CheckedChanged(sender As Object, e As EventArgs) Handles cbxIsConfirmation.CheckedChanged
        Dim isChecked As Boolean = cbxIsConfirmation.Checked
        Dim isEnabled As Boolean = cbxIsConfirmation.Enabled
        lblUploadDok.Visible = isChecked
        lbltitik2Upload.Visible = isChecked
        UploadFile.Visible = IIf(btnSimpan.Visible = False, False, isChecked)
        lblFileName.Visible = isChecked
        lnkbtnFileName.Visible = isChecked
        lbtnDeleteFile.Visible = isChecked

        If isChecked Then
            UploadFile.Visible = isEnabled
            If lblFileName.Text.Trim <> "" Then
                If btnSimpan.Visible AndAlso btnSimpan.Enabled Then
                    objDealer = _sessionHelper.GetSession("DEALER")
                    If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lbtnDeleteFile.Visible = False
                    Else
                        lbtnDeleteFile.Visible = True
                    End If
                Else
                    lbtnDeleteFile.Visible = False
                End If
                lnkbtnFileName.Visible = True
            Else
                lbtnDeleteFile.Visible = False
                lnkbtnFileName.Visible = False
            End If
        End If
    End Sub

    Private Sub lblFileName_Click(sender As Object, e As EventArgs) Handles lblFileName.Click
        If (ViewState("Mode") = enumMode.Mode.NewItemMode) Then Return

        Dim _fileName() As String = lblEvidencePath.Text.Split("\")
        Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("PKFileDirectory") & "\" & _fileName(_fileName.Length - 1)

        Response.Redirect("../Download.aspx?file=" & fileName)
    End Sub

    Private Sub lbtnDeleteFile_Click(sender As Object, e As EventArgs) Handles lbtnDeleteFile.Click
        Try
            _sessionHelper.RemoveSession(FU)
            _sessionHelper.RemoveSession(FU_NAME)
            lblEvidencePath.Text = ""
            lblFileName.Text = ""
            lbtnDeleteFile.Visible = False
            lnkbtnFileName.Visible = False
        Catch
        End Try
    End Sub

#End Region


End Class