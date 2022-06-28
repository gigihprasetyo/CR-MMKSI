#Region "Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security
'cr spk
Imports KTB.DNet.BusinessFacade.Profile
'end
#End Region


Public Class FrmCustomerRequest
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtRefNoPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTipePengajuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtRefKodePelanggan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGedung As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlamat As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKelurahan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKecamatan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodePos As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTelepon As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlPropinsi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKota As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblDiajukanOleh As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPerorangan As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPerusahaan As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlTambahan As System.Web.UI.WebControls.Panel
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblDiprosesOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglProses As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodePelanggan As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCetak As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lnkbtnDeleteAttachment As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNama2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents lblGedung As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents lblKelurahan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecamatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblPropinsi As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents lblTelepon As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefNoPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipePengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefKodePlgn As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodePos As System.Web.UI.WebControls.Label
    Protected WithEvents lblCetak As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lbtnDownload As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnBatalValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents PnlBUMN As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlLainnya As System.Web.UI.WebControls.Panel
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatalProses As System.Web.UI.WebControls.Button
    Protected WithEvents btnBlock As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatalBlock As System.Web.UI.WebControls.Button
    Protected WithEvents btnSelesai As System.Web.UI.WebControls.Button
    Protected WithEvents lbtnRefKode As System.Web.UI.WebControls.Label
    Protected WithEvents btnBaru As System.Web.UI.WebControls.Button
    Protected WithEvents lblCetakTitle As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPreArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblRefNomorPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Protected WithEvents lnkReloadPlg As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkReloadReff As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblNama2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipePerusahaan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipePerusahaan As System.Web.UI.WebControls.Label
    Protected WithEvents hdnMCPConfirmation As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnVerifyMCP As System.Web.UI.HtmlControls.HtmlInputHidden
    'CR_SPK_SALESMAN_CUSTOMER_ENHANCE juni 2021
    Protected WithEvents ddlTipePerorangan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlIdentityAsing As DropDownList
    Protected WithEvents TxtFlag As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCountryCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchCountryName As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnSearchCountryName As System.Web.UI.WebControls.LinkButton
    Protected WithEvents TglLahir As System.Web.UI.WebControls.TextBox
    Protected WithEvents TglLahirCW As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlIdentity As DropDownList
    Protected WithEvents txtNoHp As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTipePerorangan As System.Web.UI.WebControls.Label
    Protected WithEvents lblIdentity As System.Web.UI.WebControls.Label
    Protected WithEvents lblIdentityaAsing As System.Web.UI.WebControls.Label
    '
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        If Request.QueryString("ID") = String.Empty Then
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerorangan)
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerusahaan)
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlTambahan)
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlBUMN)
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlLainnya)
        Else
            Dim obj_CustomerRequest As CustomerRequest = New CustomerRequestFacade(User).Retrieve(CInt(Request.QueryString("ID")))
            RenderProfilePanel(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerorangan)
            RenderProfilePanel(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerusahaan)
            RenderProfilePanel(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlTambahan)
            RenderProfilePanel(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlBUMN)
            RenderProfilePanel(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlLainnya)
        End If
        'CR SPK
        txtCountryCode.Attributes.Add("readonly", "readonly")
        txtCountryCode.Text = "62"
        'txtCountryName.Attributes.Add("readonly", "readonly")
        lblSearchCountryName.Visible = True
        lblSearchCountryName.Attributes("onclick") = "ShowPopUpSPKMasterCountryCode();"
    End Sub
#End Region

#Region " Private Variables"
    Private objCustomerRequest As CustomerRequest

    Dim sessHelp As KTB.DNet.Utility.SessionHelper = New KTB.DNet.Utility.SessionHelper
    Private objLoginDealer As Dealer
    Private objUserInfo As UserInfo
    Private mode As enumMode.Mode

#End Region

#Region "Cek Privilege"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerViewDetail_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=KONSUMEN - Pendaftaran ")
        End If
    End Sub

    Dim bCekBtnSavePriv As Boolean = SecurityProvider.Authorize(Context.User, SR.CustomerCreateRegistration_Privilege)
    Dim bCekBtnValidatePriv As Boolean = SecurityProvider.Authorize(Context.User, SR.CustomerValidateStatus_Privilege)
    Dim bCekBtnBtlValPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.CustomerStatusCancelValidation_Privilege)

#End Region

    Private Sub RenderProfilePanel(ByVal objCustomerRequest As CustomerRequest, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel)
        Dim objRenderPanel As RenderingProfile

        If Not objCustomerRequest Is Nothing Then
            Dim status As String = New EnumStatusCustomerRequest().RetrieveName(objCustomerRequest.Status)
            If status = "Baru" Or status = "Validasi" Then
                objRenderPanel = New RenderingProfile(False)
            Else
                objRenderPanel = New RenderingProfile(True)
            End If
            objRenderPanel.GeneratePanel(objCustomerRequest.ID, objPanel, objGroup, profileType, User)
        Else
            objRenderPanel = New RenderingProfile(False)
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User)
        End If


    End Sub

#Region "Custom Method"
    Private Sub BindDropDown()
        ddlCetak.Items.Clear()
        ddlCetak.Items.Add(New ListItem("YA", 0))
        ddlCetak.Items.Add(New ListItem("TIDAK", 1))
        ddlCetak.SelectedIndex = 1

        ddlTipePengajuan.Items.Clear()
        ddlTipePengajuan.DataSource = New EnumTipePengajuanCustomerRequest().RetrieveTypeDummy()
        ddlTipePengajuan.DataTextField = "NameTipe"
        ddlTipePengajuan.DataValueField = "ValTipe"
        ddlTipePengajuan.DataBind()
        ddlTipePengajuan.SelectedIndex = 0

        ddlTipe.Items.Clear()
        ddlTipe.DataSource = New EnumTipePelangganCustomerRequest().RetrieveType
        ddlTipe.DataTextField = "NameTipe"
        ddlTipe.DataValueField = "ValTipe"
        ddlTipe.DataBind()
        ddlTipePengajuan.SelectedIndex = 0
        ddlTipe_SelectedIndexChanged(Me, System.EventArgs.Empty)




        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim list As ArrayList = New ProvinceFacade(User).RetrieveActiveList(criterias, "ProvinceName", Sort.SortDirection.ASC)

        ddlPropinsi.Items.Clear()
        ddlPropinsi.DataSource = list
        ddlPropinsi.DataTextField = "ProvinceName"
        ddlPropinsi.DataValueField = "ID"
        ddlPropinsi.DataBind()
        ddlPropinsi.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlPropinsi.SelectedIndex = 0
        ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
    End Sub
    Private Sub MakeVisibleLabel(ByVal isVisible As Boolean)
        lblTipePengajuan.Visible = isVisible
        lblTipe.Visible = isVisible
        lblNama1.Visible = isVisible
        lblNama2.Visible = isVisible
        lblKodePos.Visible = isVisible
        lblKota.Visible = isVisible
        lblPropinsi.Visible = isVisible
        lblKecamatan.Visible = isVisible
        lblKelurahan.Visible = isVisible
        lblCetak.Visible = isVisible
        lblAlamat.Visible = isVisible
        lblGedung.Visible = isVisible
        lblRefNoPengajuan.Visible = isVisible
    End Sub

    Private Sub LoadFormView()
        MakeVisibleLabel(True)
        lblDealer.Text = objCustomerRequest.Dealer.DealerCode & "/" & objCustomerRequest.Dealer.SearchTerm1
        lblNoPengajuan.Text = objCustomerRequest.RequestNo
        Dim objUserRequest As New UserInfo
        objUserRequest = New UserInfoFacade(User).Retrieve(objCustomerRequest.RequestUserID)
        If Not IsNothing(objUserRequest) Then
            lblDiajukanOleh.Text = objUserRequest.Dealer.DealerCode & "-" & objUserRequest.UserName
        End If
        lblTglPengajuan.Text = objCustomerRequest.RequestDate.ToString("dd/MM/yyyy")
        lblStatusPengajuan.Text = CType(objCustomerRequest.Status, EnumStatusCustomerRequest.TipePengajuanCustomerRequest).ToString

        lblKodePelanggan.Text = objCustomerRequest.CustomerCode

        txtRefNoPengajuan.Visible = False
        lblNoPengajuan.Visible = True
        lblNoPengajuan.Text = objCustomerRequest.RequestNo
        lblRefNomorPengajuan.Visible = False

        lblRefNoPengajuan.Text = objCustomerRequest.RefRequestNo
        txtRefNoPengajuan.Visible = False

        ddlTipePengajuan.Visible = False
        Dim tipePengajuan As Integer
        If Integer.TryParse(objCustomerRequest.RequestType, tipePengajuan) Then
            lblTipePengajuan.Text = CType(tipePengajuan, EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest).ToString
        End If

        Dim objUserProcess As UserInfo = New UserInfoFacade(User).Retrieve(objCustomerRequest.ProcessUserID)
        If Not objUserProcess Is Nothing And objUserProcess.ID > 0 Then
            lblDiprosesOleh.Text = objUserProcess.Dealer.DealerCode & "-" & objUserProcess.UserName
            lblTglProses.Text = objCustomerRequest.ProcessDate.ToString("dd/MM/yyyy")
        Else
            lblDiprosesOleh.Text = ""
            lblTglProses.Text = ""
        End If
        ddlTipe.Visible = False
        lblTipe.Text = CType(objCustomerRequest.Status1, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest).ToString
        'If 1 = 2 Then
        ddlTipe.SelectedValue = objCustomerRequest.Status1
        ddlTipe_SelectedIndexChanged(Nothing, Nothing)
        ddlTipePerusahaan.Visible = False
        'cr spk
        ddlTipePerorangan.Visible = False
        ddlIdentity.Visible = False
        ddlIdentityAsing.Visible = False
        '
        'End If
        If objCustomerRequest.Status1 = CType(EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan, Short) Then
            If objCustomerRequest.TipePerusahaan >= 0 Then
                Me.lblTipePerusahaan.Text = CType(objCustomerRequest.TipePerusahaan, EnumTipePerusahaan.EnumTipePerusahaan).ToString
                lblTipePerusahaan.Visible = True
                If objCustomerRequest.TipePerusahaan < 6 Then
                    Me.lblIdentity.Text = "TDP"
                    lblIdentity.Visible = True
                ElseIf objCustomerRequest.TipePerusahaan = 6 Then
                    Me.lblIdentity.Text = "TDY"
                    lblIdentity.Visible = True
                Else
                    Me.lblIdentity.Text = "SIK"
                    lblIdentity.Visible = True
                End If
            End If
        Else
            If objCustomerRequest.TypePerorangan >= 0 Then
                Me.lblTipePerorangan.Text = CType(objCustomerRequest.TypePerorangan, EnumTipePerorangan.EnumTipePerorangan).ToString
                lblTipePerorangan.Visible = True
                If objCustomerRequest.TypeIdentitas = 0 Then
                    Me.lblIdentity.Text = "KTP"
                    lblIdentity.Visible = True
                ElseIf objCustomerRequest.TypeIdentitas = 1 Then
                    Me.lblIdentity.Text = "SIM"
                    lblIdentity.Visible = True
                ElseIf objCustomerRequest.TypeIdentitas = 2 Then
                    Me.lblIdentity.Text = "KITAS"
                    lblIdentity.Visible = True
                Else
                    Me.lblIdentity.Text = "KITAP"
                    lblIdentity.Visible = True
                End If
            End If

        End If

        lblNama1.Text = objCustomerRequest.Name1.Trim.ToUpper
        lblNama2.Text = objCustomerRequest.Name2.Trim.ToUpper
        txtNama.Text = objCustomerRequest.Name1.ToUpper()
        txtNama2.Text = objCustomerRequest.Name2.ToUpper()
        txtNama.Visible = False
        txtNama2.Visible = False

        lblGedung.Text = objCustomerRequest.Name3
        txtGedung.Visible = False

        lblAlamat.Text = objCustomerRequest.Alamat
        txtAlamat.Text = objCustomerRequest.Alamat.ToUpper()
        txtAlamat.Visible = False

        lblKelurahan.Text = objCustomerRequest.Kelurahan
        txtKelurahan.Visible = False

        lblKecamatan.Text = objCustomerRequest.Kecamatan
        txtKecamatan.Visible = False

        Dim objCity As City = New CityFacade(User).Retrieve(objCustomerRequest.CityID)

        lblPropinsi.Text = objCity.Province.ProvinceName
        ddlPropinsi.Visible = False

        ddlKota.Visible = False
        ddlPreArea.Visible = False
        If objCustomerRequest.PreArea <> String.Empty And objCustomerRequest.PreArea <> "blank" Then
            lblKota.Text = objCustomerRequest.PreArea.ToUpper + " " + objCity.CityName
        Else
            lblKota.Text = objCity.CityName
        End If

        txtTelepon.Visible = False
        lblTelepon.Text = objCustomerRequest.PhoneNo
        lblTelepon.Visible = False

        lblRefKodePlgn.Text = objCustomerRequest.ReffCode
        txtRefKodePelanggan.Visible = False
        lbtnRefKode.Visible = False

        lblKodePos.Text = objCustomerRequest.PostalCode
        txtKodePos.Visible = False

        lblEmail.Text = objCustomerRequest.Email
        lblEmail.Visible = False
        'txtEmail.Visible = False
        Dim printRegion As Integer
        If Integer.TryParse(objCustomerRequest.PrintRegion, printRegion) Then
            If printRegion = 0 Then
                lblCetak.Text = "Ya"
            Else
                lblCetak.Text = "Tidak"
            End If
        End If

        lblCetak.Visible = True
        ddlCetak.Visible = False
        lnkbtnDeleteAttachment.Visible = False
        fileUpload.Visible = False
        If objCustomerRequest.Attachment = String.Empty Then
            lnkbtnDeleteAttachment.Visible = False
            lbtnDownload.Visible = False
        Else
            lnkbtnDeleteAttachment.Visible = True
            lbtnDownload.Visible = True
        End If
        Button1.Enabled = False
        lnkReloadPlg.Visible = False
        lnkReloadReff.Visible = False

        'mod by ery
        If objCustomerRequest.Status <> EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Baru Then
            lnkbtnDeleteAttachment.Visible = False
        End If
        Dim status As String = New EnumStatusCustomerRequest().RetrieveName(objCustomerRequest.Status)
        If status = "Validasi" Then
            If objCustomerRequest.Dealer.ID = objLoginDealer.ID Then
                'btnBatalValidasi.Visible = True
                'btnBatalValidasi.Enabled = True
                btnValidasi.Visible = False
                btnValidasi.Enabled = False
                btnBatalValidasi.Visible = bCekBtnBtlValPriv
                btnBatalValidasi.Enabled = bCekBtnBtlValPriv
            End If
        Else
            If objCustomerRequest.Dealer.ID = objLoginDealer.ID Then
                btnBatalValidasi.Visible = False
                btnBatalValidasi.Enabled = False
                'btnValidasi.Visible = True
                'btnValidasi.Enabled = True

                'start dicomment by myk 2017-11-23
                'btnValidasi.Enabled = bCekBtnValidatePriv
                'btnValidasi.Visible = bCekBtnValidatePriv
                'end dicomment by myk 2017-11-23
            End If
        End If
        'CR SPK
        txtCountryCode.Text = objCustomerRequest.CountryCode
        txtNoHp.Text = objCustomerRequest.PhoneNo
        lblSearchCountryName.Visible = True
        '
    End Sub
    Private Sub LoadForm()
        MakeVisibleLabel(False)
        lblDealer.Text = objCustomerRequest.Dealer.DealerCode & "/" & objCustomerRequest.Dealer.SearchTerm1
        lblNoPengajuan.Text = objCustomerRequest.RequestNo
        Dim objUserRequest As New UserInfo
        objUserRequest = New UserInfoFacade(User).Retrieve(objCustomerRequest.RequestUserID)
        If Not IsNothing(objUserRequest) Then
            lblDiajukanOleh.Text = objUserRequest.Dealer.DealerCode & "-" & objUserRequest.UserName
        End If
        lblTglPengajuan.Text = objCustomerRequest.RequestDate.ToString("dd/MM/yyyy")
        lblStatusPengajuan.Text = CType(objCustomerRequest.Status, EnumStatusCustomerRequest.TipePengajuanCustomerRequest).ToString
        lblKodePelanggan.Text = objCustomerRequest.CustomerCode

        txtRefNoPengajuan.Text = objCustomerRequest.RefRequestNo
        If Not String.IsNullOrEmpty(objCustomerRequest.RequestType.Trim) Then
            ddlTipePengajuan.SelectedValue = objCustomerRequest.RequestType
        End If

        Dim objUserProcess As UserInfo = New UserInfoFacade(User).Retrieve(objCustomerRequest.ProcessUserID)
        If Not objUserProcess Is Nothing And objUserProcess.ID > 0 Then
            lblDiprosesOleh.Text = objUserProcess.Dealer.DealerCode & "-" & objUserProcess.UserName
            lblTglProses.Text = objCustomerRequest.ProcessDate.ToString("dd/MM/yyyy")
        Else
            lblDiprosesOleh.Text = ""
            lblTglProses.Text = ""
        End If

        ddlTipe.SelectedValue = objCustomerRequest.Status1


        ddlTipe_SelectedIndexChanged(Me, System.EventArgs.Empty)
        If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
            ddlTipePerusahaan.Visible = True
            'cr spk
            ddlTipePerorangan.Visible = False
            '
            Try
                ddlTipePerusahaan.Items.Clear()
                ddlTipePerusahaan.DataSource = New EnumTipePerusahaan().RetrieveType
                ddlTipePerusahaan.DataTextField = "NameTipe"
                ddlTipePerusahaan.DataValueField = "ValTipe"
                ddlTipePerusahaan.DataBind()

                ddlTipePerusahaan.SelectedValue = objCustomerRequest.TipePerusahaan
            Catch ex As Exception
                ddlTipePerusahaan.Visible = False
                'cr spk
                ddlTipePerorangan.Visible = True
                '
            End Try

        Else
            ddlTipePerusahaan.Visible = False
            'cr spk
            ddlTipePerorangan.Visible = True
            '
            Try
                ddlTipePerusahaan.Items.Clear()
                ddlTipePerusahaan.DataSource = New EnumTipePerorangan().RetrieveType
                ddlTipePerusahaan.DataTextField = "NameTipe"
                ddlTipePerusahaan.DataValueField = "ValTipe"
                ddlTipePerusahaan.DataBind()

                ddlTipePerusahaan.SelectedValue = objCustomerRequest.TypePerorangan
            Catch ex As Exception
                ddlTipePerusahaan.Visible = True
                'cr spk
                ddlTipePerorangan.Visible = False
                '
            End Try

        End If


        txtNama.Text = objCustomerRequest.Name1.ToUpper()
        txtNama2.Text = objCustomerRequest.Name2.ToUpper()
        txtGedung.Text = objCustomerRequest.Name3.ToUpper()
        txtAlamat.Text = objCustomerRequest.Alamat.ToUpper()
        txtKelurahan.Text = objCustomerRequest.Kelurahan.ToUpper()
        txtKecamatan.Text = objCustomerRequest.Kecamatan.ToUpper()
        Dim objCity As City = New CityFacade(User).Retrieve(objCustomerRequest.CityID)
        ddlPropinsi.SelectedValue = objCity.Province.ID
        ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
        ddlKota.SelectedValue = objCustomerRequest.CityID
        If objCustomerRequest.PreArea <> String.Empty And objCustomerRequest.PreArea <> "blank" Then
            Try
                ddlPreArea.SelectedValue = objCustomerRequest.PreArea.Substring(0, 1).ToUpper + objCustomerRequest.PreArea.Substring(1).ToUpper
            Catch ex As Exception
                ddlPreArea.SelectedIndex = 0
            End Try
        Else
            ddlPreArea.SelectedIndex = 0
        End If
        txtTelepon.Text = objCustomerRequest.PhoneNo
        txtRefKodePelanggan.Text = objCustomerRequest.ReffCode.ToUpper
        txtKodePos.Text = objCustomerRequest.PostalCode
        txtEmail.Text = objCustomerRequest.Email
        'CR SPK
        txtNoHp.Text = objCustomerRequest.PhoneNo
        '
        Dim printRegion As Integer
        If Integer.TryParse(objCustomerRequest.PrintRegion, printRegion) Then
            ddlCetak.SelectedValue = printRegion
        End If
        ddlTipePengajuan.Enabled = False
        txtRefNoPengajuan.Enabled = False
        ControlVisible(True)
        Dim status As String = New EnumStatusCustomerRequest().RetrieveName(objCustomerRequest.Status)
        If status = "Validasi" Then
            ControlEnable(False, False, True, True, False, False, False, False)
        ElseIf status = "Proses" Then
            ControlEnable(False, False, False, False, True, False, False, True)
        ElseIf status = "Selesai" Then
            ControlEnable(False, False, False, False, False, False, False, False)
        ElseIf status = "Baru" Then
            ControlEnable(True, True, False, False, False, False, False, False)
            ddlTipePengajuan.Enabled = True
            txtRefNoPengajuan.Enabled = True
        ElseIf status = "Block" Then
            ControlEnable(True, False, False, False, False, False, True, False)

        End If
        'mod by ery
        If objCustomerRequest.Status <> EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Baru Then
            lnkbtnDeleteAttachment.Visible = False
        End If
        'CR SPK
        txtCountryCode.Text = objCustomerRequest.CountryCode
        txtNoHp.Text = objCustomerRequest.PhoneNo
        lblSearchCountryName.Visible = True
        '
    End Sub
    Private Sub ControlVisible(ByVal isVisible As Boolean)
        txtNama.Visible = isVisible
        txtNama2.Visible = isVisible
        txtGedung.Visible = isVisible
        txtAlamat.Visible = isVisible
        txtKelurahan.Visible = isVisible
        txtKecamatan.Visible = isVisible
        ddlPropinsi.Visible = isVisible
        ddlKota.Visible = isVisible
        ddlPreArea.Visible = isVisible
        txtRefKodePelanggan.Visible = isVisible
        txtRefNoPengajuan.Visible = isVisible
        txtKodePos.Visible = isVisible
        ddlCetak.Visible = isVisible
        fileUpload.Visible = isVisible
        ddlTipe.Visible = isVisible
        ddlTipePengajuan.Visible = isVisible
    End Sub
    Private Sub ControlEnable(ByVal isEnable As Boolean, ByVal isValidasi As Boolean, ByVal isBtlValidasi As Boolean, _
    ByVal isProses As Boolean, ByVal isBtlProses As Boolean, ByVal isBlock As Boolean, ByVal isBtlBlock As Boolean, ByVal isSelesai As Boolean)

        If objCustomerRequest.Attachment = String.Empty Then
            lnkbtnDeleteAttachment.Visible = False
            lbtnDownload.Visible = False
        Else
            lnkbtnDeleteAttachment.Visible = isEnable
            lbtnDownload.Visible = isEnable
        End If
        txtNama.Enabled = isEnable
        txtNama2.Enabled = isEnable
        txtGedung.Enabled = isEnable
        txtAlamat.Enabled = isEnable
        txtKelurahan.Enabled = isEnable
        txtKecamatan.Enabled = isEnable
        ddlPropinsi.Enabled = isEnable
        ddlKota.Enabled = isEnable
        ddlPreArea.Enabled = isEnable
        txtTelepon.Enabled = isEnable
        txtRefKodePelanggan.Enabled = isEnable
        txtRefNoPengajuan.Enabled = isEnable
        txtKodePos.Enabled = isEnable
        ddlCetak.Enabled = isEnable
        ddlTipe.Enabled = isEnable
        ddlTipePengajuan.Enabled = isEnable
        lbtnRefKode.Visible = isEnable
        lblRefNomorPengajuan.Visible = isEnable
        lnkReloadPlg.Visible = isEnable
        lnkReloadReff.Visible = isEnable

        'start dicomment by myk 2017-11-23
        'If isValidasi Then
        '    btnValidasi.Enabled = bCekBtnValidatePriv
        '    btnValidasi.Visible = bCekBtnValidatePriv
        'Else
        '    btnValidasi.Visible = isValidasi
        '    btnValidasi.Enabled = isValidasi
        'End If
        'If isBtlValidasi Then
        '    btnBatalValidasi.Enabled = bCekBtnBtlValPriv
        '    btnBatalValidasi.Visible = bCekBtnBtlValPriv
        'Else
        '    btnBatalValidasi.Visible = isBtlValidasi
        '    btnBatalValidasi.Visible = isBtlValidasi
        'End If
        'end dicomment by myk 2017-11-23

        'btnValidasi.Visible = isValidasi
        'btnValidasi.Enabled = isValidasi
        'btnBatalValidasi.Visible = isBtlValidasi
        btnProses.Visible = False
        btnBlock.Visible = isBlock
        btnSelesai.Visible = isSelesai
        btnBatalBlock.Visible = isBtlBlock
        btnBatalProses.Visible = isBtlProses
    End Sub
    Private Sub ClearForm()
        lblDealer.Text = objLoginDealer.DealerCode & "/" & objLoginDealer.SearchTerm1
        lblDiajukanOleh.Text = objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName
        lblTglPengajuan.Text = DateTime.Today.ToString("dd/MM/yyyy")
        lblStatusPengajuan.Text = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Baru.ToString

        lblKodePelanggan.Text = ""  'Kapan di isi ???

        txtRefNoPengajuan.Text = ""
        ddlTipePengajuan.SelectedIndex = 0
        lblDiprosesOleh.Text = ""
        lblTglProses.Text = ""
        ddlTipe.SelectedIndex = 0
        txtNama.Text = ""
        txtGedung.Text = ""
        txtAlamat.Text = ""
        txtKelurahan.Text = ""
        txtKecamatan.Text = ""
        ddlPropinsi.SelectedValue = 0
        ddlKota.SelectedValue = 0
        txtTelepon.Text = ""
        txtRefKodePelanggan.Text = ""
        txtKodePos.Text = ""
        txtEmail.Text = ""
        ddlCetak.SelectedIndex = 1
        lnkbtnDeleteAttachment.Visible = False
        'CR SPK
        txtCountryCode.Text = ""
        txtNoHp.Text = ""
        ddlTipePerorangan.SelectedIndex = 0
        ddlIdentity.SelectedIndex = 0
        ddlIdentityAsing.SelectedIndex = 2
        '
    End Sub
    Private Sub fillForm()
        mode = CType(ViewState("Mode"), enumMode.Mode)
        If mode = enumMode.Mode.NewItemMode Then
            ClearForm()
        ElseIf mode = enumMode.Mode.EditMode Then
            LoadForm()
        ElseIf mode = enumMode.Mode.ViewMode Then
            LoadFormView()
        End If
    End Sub
    Private Function ValidateSave() As Boolean
        Dim objCustomer As New Customer
        Dim objCustomerReq As New CustomerRequest
        If ddlPropinsi.SelectedIndex = 0 Then
            MessageBox.Show("Silakan Pilih Propinsi !")
            Return False
        End If

        If txtNama.Text.Trim.Length > 40 Then
            MessageBox.Show("Nama 1 melebihi 40 karakter")
            Exit Function
        End If

        If txtNama2.Text.Trim.Length > 35 Then
            MessageBox.Show("Nama 2 melebihi 35 karakter")
            Exit Function
        End If

        If txtGedung.Text.Trim.Length > 40 Then
            MessageBox.Show("Gedung melebihi 40 karakter")
            Exit Function
        End If

        If txtAlamat.Text.Trim.Length > 60 Then
            MessageBox.Show("Alamat melebihi 60 karakter")
            Exit Function
        End If

        If txtKelurahan.Text.Trim.Length > 40 Then
            MessageBox.Show("Kelurahan melebihi 40 karakter")
            Exit Function
        End If

        If txtKecamatan.Text.Trim.Length > 35 Then
            MessageBox.Show("Kecamatan melebihi 35 karakter")
            Exit Function
        End If

        If ddlKota.SelectedIndex = 0 Then
            MessageBox.Show("Silakan Pilih Kota !")
            Return False
        End If

        If txtKodePos.Text <> String.Empty Then
            If Not IsNumeric(txtKodePos.Text) Then
                MessageBox.Show("Kodepos harus angka!")
                Return False
            End If
            If Not (txtKodePos.Text.Trim.Length = 5) Then
                MessageBox.Show("Kodepos 5 Angka!")
                Return False
            End If
        End If

        If txtAlamat.Text.Trim = String.Empty Then
            MessageBox.Show("Data alamat tidak boleh kosong!")
            Return False
        End If

        Dim city As KTB.DNet.Domain.City = New CityFacade(User).Retrieve(CType(ddlKota.SelectedValue, Integer))
        Dim area As String = String.Empty
        area = ddlPreArea.SelectedValue.ToUpper
        If Not IsNothing(city) Then
            Dim msg As String = ""
            Select Case Len(city.CityName)
                Case Is > 31
                    If ddlPreArea.SelectedIndex > 0 Then
                        msg = "Kosongkan (Kota / Kabupaten)"
                    End If
                Case Is = 30
                    If ddlPreArea.SelectedIndex > 0 AndAlso Len(area) > 4 Then
                        msg = "Gunakan KAB / KOTA atau kosongkan (Kota / Kabupaten)"
                    End If
                Case Is = 31
                    If ddlPreArea.SelectedIndex > 0 AndAlso Len(area) > 3 Then
                        msg = "Gunakan KAB atau kosongkan (Kota / Kabupaten)"
                    End If
                Case Is < 30
                    If ddlPreArea.SelectedIndex > 0 AndAlso Len(area) > 5 Then
                        If Len(city.CityName) > 25 Then
                            msg = "Gunakan KAB / KOTA / KODYA"
                        End If
                    End If
            End Select
            If msg <> "" Then
                MessageBox.Show(msg)
                Return False
            End If
        End If

        If ddlTipePengajuan.SelectedValue = EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest.Revisi Then
            objCustomer = New CustomerFacade(User).Retrieve(lblKodePelanggan.Text.Trim)
            If objCustomer Is Nothing Then
                MessageBox.Show("Kode pelanggan yang anda masukkan tidak ada")
                Return False
            End If
            If txtRefKodePelanggan.Text.Trim <> String.Empty Then
                Dim objCustRef As Customer = New CustomerFacade(User).Retrieve(txtRefKodePelanggan.Text.Trim)
                If objCustRef Is Nothing Then
                    MessageBox.Show("Ref Kode Pelanggan Tidak Valid")
                    Return False
                End If
            Else
                MessageBox.Show("Tipe Revisi Ref Kode Pelanggan Wajib Diisi")
                Return False
            End If
        Else
            lblKodePelanggan.Text = ""
        End If
        Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        If txtRefKodePelanggan.Text.Trim <> String.Empty Then
            objCustomer = New CustomerDealerFacade(User).RetrieveByRefCode(txtRefKodePelanggan.Text.Trim, objDealer.ID)
            If objCustomer.ID <= 0 Then
                MessageBox.Show("Ref Kode Pelanggan Tidak Valid")
                Return False
            End If
        End If
        If txtRefNoPengajuan.Text.Trim <> String.Empty Then
            objCustomerReq = New CustomerRequestFacade(User).RetrieveByRefRequestNo(txtRefNoPengajuan.Text.Trim, objDealer.ID)
            If objCustomerReq.ID <= 0 Then
                MessageBox.Show("Ref No Pengajuan Tidak Valid")
                Return False
            End If
        End If

        'Start  :CR;By:dna;For:Rina;On:20100616;Remark:add personal information
        'Perorangan
        If 1 = 2 Then 'nunggu kiamat dulu baru di-execute:  
            Dim objRenderPanel As RenderingProfile = New RenderingProfile
            Dim alGroup1 As ArrayList
            Dim sKTP As String = ""
            Dim sNPWP As String = ""
            Dim sSIUP As String = ""
            Dim sTDP As String = ""

            If EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan Then
                alGroup1 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), CType(EnumProfileType.ProfileType.CUSTOMERREQUEST, Short), User, True)
            ElseIf EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
                alGroup1 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), CType(EnumProfileType.ProfileType.CUSTOMERREQUEST, Short), User, True)
            ElseIf EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                alGroup1 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), CType(EnumProfileType.ProfileType.CUSTOMERREQUEST, Short), User, True)
            ElseIf EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya Then
                alGroup1 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), CType(EnumProfileType.ProfileType.CUSTOMERREQUEST, Short), User, True)
            End If
            For Each oCRP As CustomerRequestProfile In alGroup1
                Select Case oCRP.ProfileHeader.Code.Trim.ToUpper
                    Case "NOKTP"
                        sKTP = IIf(IsNothing(oCRP.ProfileValue), "", oCRP.ProfileValue)
                    Case "NONPWP"
                        sNPWP = IIf(IsNothing(oCRP.ProfileValue), "", oCRP.ProfileValue)
                    Case "NOSIUP"
                        sSIUP = IIf(IsNothing(oCRP.ProfileValue), "", oCRP.ProfileValue)
                    Case "NOTDP"
                        sTDP = IIf(IsNothing(oCRP.ProfileValue), "", oCRP.ProfileValue)
                End Select
            Next
            Dim nEmpty As Integer = 0
            nEmpty += IIf(sKTP.Trim = "", 1, 0)
            nEmpty += IIf(sNPWP.Trim = "", 1, 0)
            nEmpty += IIf(sSIUP.Trim = "", 1, 0)
            nEmpty += IIf(sTDP.Trim = "", 1, 0)
            If nEmpty <> 3 Then
                MessageBox.Show("No KTP, NPWP, SIUP atau TDP harus di-isi salah satu")
                Return False
            End If
        End If
        'End :CR;By:dna;For:Rina;On:20100616;Remark:add personal information

        Return True
    End Function
    Private Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

    Private Sub TransferProcess()
        Dim i As Integer = 0
        Dim arl As ArrayList = New ArrayList
        Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim CR As CustomerRequest
        Dim success As Boolean = False

        Dim IsCheck As Boolean = False
        Dim sw As StreamWriter
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "cusreq", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\" & filename  '-- Destination file
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename
        Dim tmp As Integer = 0
        Dim NoKTP As String, NoTelp As String

        CR = New CustomerRequest
        Dim CRFacade As Service.CustomerRequestFacade = New Service.CustomerRequestFacade(User)
        CR = CRFacade.Retrieve(objCustomerRequest.ID)
        If CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi Then
            IsCheck = True
            CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses
            CR.ProcessUserID = objUser.ID

            Dim ObjCity As New City
            ObjCity = New General.CityFacade(User).Retrieve(CR.CityID)
            Dim preRegion As String
            If CR.PrintRegion = "0" Then
                preRegion = "X"
            Else
                preRegion = ""
            End If

            'handle sementara untuk prearea
            If CR.PreArea.ToLower = "blank" Then
                CR.PreArea = ""
            End If

            'Untuk preArea dan kota dipisahkan dengan spasi dan bukan dengan Delimiter chr(13) (Enter)
            'Konfirmasi dari Heru
            'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(13) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & Chr(10))
            'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion )
            GetKTPAndPhone(CR, NoKTP, NoTelp) 'CR:for:Rina;by:dna:on:20110323

            'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp)
            'If Not (NoKTP.Trim = "") Then
            '    If arl.Count > 0 Then sb.Append(vbNewLine)
            '    arl.Add(CR)
            '    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP)
            '    'CR SPK
            '    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp & Chr(9) & CR.Email)
            '    'End
            'End If
            'If tmp < CountChecked() - 1 Then
            '    sb.Append(vbNewLine)
            'End If
            'cr spk
            If CR.Status1 > 0 Then
                'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp)
                If CR.TypeIdentitas < 5 Then
                    If (NoKTP.Trim <> "") Then
                        If arl.Count > 0 Then sb.Append(vbNewLine)
                        arl.Add(CR)
                        sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "TDP" & Chr(9) & NoKTP)
                    End If
                ElseIf CR.TypeIdentitas = 5 Then
                    If (NoKTP.Trim <> "") Then
                        If arl.Count > 0 Then sb.Append(vbNewLine)
                        arl.Add(CR)
                        sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "TDY" & Chr(9) & NoKTP)
                    End If
                Else
                    If (NoKTP.Trim <> "") Then
                        If arl.Count > 0 Then sb.Append(vbNewLine)
                        arl.Add(CR)
                        sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "SIK" & Chr(9) & NoKTP)
                    End If

                End If

            Else
                'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp)
                'If (NoKTP.Trim <> "") Then
                '    If arl.Count > 0 Then sb.Append(vbNewLine)
                '    arl.Add(CR)
                '    sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP)
                'End If
                If CR.TypeIdentitas = 0 Then 'KTP
                    If (NoKTP.Trim <> "") Then
                        If arl.Count > 0 Then sb.Append(vbNewLine)
                        arl.Add(CR)
                        sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP)
                    End If
                ElseIf CR.TypeIdentitas = 1 Then 'SIM
                    If (NoKTP.Trim <> "") Then
                        If arl.Count > 0 Then sb.Append(vbNewLine)
                        arl.Add(CR)
                        sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "SIM" & Chr(9) & NoKTP)
                    End If
                ElseIf CR.TypeIdentitas = 2 Then 'KITAS
                    If (NoKTP.Trim <> "") Then
                        If arl.Count > 0 Then sb.Append(vbNewLine)
                        arl.Add(CR)
                        sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KITAS" & Chr(9) & NoKTP)
                    End If
                Else
                    If (NoKTP.Trim <> "") Then 'KITAP
                        If arl.Count > 0 Then sb.Append(vbNewLine)
                        arl.Add(CR)
                        sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KITAP" & Chr(9) & NoKTP)
                    End If
                End If
            End If
            '
            tmp += 1
        End If

        If IsCheck Then
            If (sb.Length > 0) Then

                If Transfer(DestFile, sb.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder
                    'If Download(LocalDest, sb.ToString(), arl) Then         '>> Code utk download ke folder lokal
                    MessageBox.Show("Data berhasil di upload ke SAP")
                    'Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & filename)
                Else
                    MessageBox.Show("Download data gagal")
                End If
            End If
        Else
            MessageBox.Show("Daftar customer request belum dipilih atau status tidak valid")
        End If
    End Sub

    Private Sub GetKTPAndPhone(ByVal oCR As CustomerRequest, ByRef NOKTP As String, ByRef NOTELP As String)
        NOKTP = ""
        NOTELP = ""
        For Each oCRP As CustomerRequestProfile In oCR.CustomerRequestProfiles
            If oCRP.ProfileHeader.Code.Trim.ToUpper = "NOKTP" Then
                NOKTP = oCRP.ProfileValue
            ElseIf oCRP.ProfileHeader.Code.Trim.ToUpper = "NOTELP" Then
                NOTELP = oCRP.ProfileValue
            End If
        Next
    End Sub

    Private Function Transfer(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                If (New Service.CustomerRequestFacade(User).UpdateTransaction(arl) <> -1) Then
                    sw = New StreamWriter(DestFile)
                    sw.Write(Val)
                    sw.Close()
                Else
                    success = False
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function

    Private Sub CreateBatal(ByVal message As String, ByVal status As Integer)
        Dim NoKTP As String, NoTelp As String

        objCustomerRequest = CType(sessHelp.GetSession("CustomerRequest"), CustomerRequest)
        'Cek status. Jika status <> Proses & autoCustomer = Aktif
        If status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi And objCustomerRequest.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses And Convert.ToBoolean(Session("AutoCustomerStatus")) = True Then
            MessageBox.Show("Data telah ditransfer")
            Exit Sub
        Else
            If status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi Then
                CommonFunction.GetKTPAndPhone(objCustomerRequest, NoKTP, NoTelp)
                If NoKTP = String.Empty OrElse NoKTP Is Nothing Then
                    MessageBox.Show("No KTP konsumen tidak ada")
                    Exit Sub
                End If
            End If
            Dim _oldStatus = objCustomerRequest.Status
            objCustomerRequest.Status = status
            Dim nResult As Integer = New CustomerRequestFacade(User).Update(objCustomerRequest)
            If nResult <> -1 Then
                MessageBox.Show("Status sudah berubah menjadi " & message)

                '--------------------------------------------------------------------
                'by pass to transfer process to SAP if status = validasi and transaction control = aktif
                'Dev: Soni, BA: Isye, 08/06/2017 
                If status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi Then
                    If Convert.ToBoolean(Session("AutoCustomerStatus")) = True Then
                        TransferProcess()
                    End If
                End If
                '--------------------------------------------------------------------

                If ViewState("Mode") = enumMode.Mode.ViewMode Then
                    If message = "baru" Then
                        ViewState("Mode") = enumMode.Mode.EditMode
                    Else
                        ViewState("Mode") = enumMode.Mode.ViewMode
                    End If
                End If
                sessHelp.SetSession("CustomerRequest", New CustomerRequestFacade(User).Retrieve(nResult))
                objCustomerRequest = CType(sessHelp.GetSession("CustomerRequest"), CustomerRequest)
                'Insert To CustomerStatusHistory
                Dim custHistory As New CustomerStatusHistory
                custHistory.CustomerRequest = objCustomerRequest
                custHistory.OldStatus = _oldStatus
                custHistory.NewStatus = status
                custHistory.RowStatus = 0
                Dim _custHistFacade As New CustomerStatusHistoryFacade(User)
                _custHistFacade.Insert(custHistory)

            End If
            fillForm()
        End If
    End Sub

    Private Function ValidateRefCustomerCode() As Boolean
        Dim refKode As String = txtRefKodePelanggan.Text
        Dim objCustomer As Customer = New CustomerFacade(User).RetrieveCustReq(refKode.Trim)
        If objCustomer Is Nothing Then
            MessageBox.Show("Ref Kode Pelanggan tidak valid")
            Return False
        ElseIf objCustomer.RowStatus = -1 Then
            MessageBox.Show("Ref Kode Pelanggan tidak valid")
            Return False
        Else
            Return True
        End If
    End Function

    Private Function IsMCPValid(ByVal IsAfterConfirmation As Boolean) As Boolean
        hdnVerifyMCP.Value = EnumMCPStatus.MCPStatus.NonMCP
        If CType(Me.ddlTipe.SelectedValue, Integer) = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
            If IsAfterConfirmation = False Then
                If (IsGovernmentInstitution(Me.txtNama.Text) OrElse IsGovernmentInstitution(Me.txtNama2.Text)) Then
                    MessageBox.Confirm("Apakah proses pengajuan tetap dilanjutkan?", "hdnMCPConfirmation")
                    Return False
                Else
                    Return True
                End If
            Else
                If (IsGovernmentInstitution(Me.txtNama.Text) OrElse IsGovernmentInstitution(Me.txtNama2.Text)) Then
                    If Me.hdnMCPConfirmation.Value <> "1" Then 'User click No in confirmation box
                        Return False
                    Else
                        hdnVerifyMCP.Value = EnumMCPStatus.MCPStatus.NotVerifiedMCP
                        Return True 'user click ok in confirmation box
                    End If
                Else
                    Return True 'never
                End If
            End If
        Else
            Return True
        End If

    End Function
    Private Function IsGovernmentInstitution(ByVal sName As String) As Boolean
        Dim sMCPList() As String = KTB.DNet.Lib.WebConfig.GetValue("ListOfMCPName").Split(";")
        Dim i As Integer
        Dim sTemp As String = ""
        If sName.Trim() = String.Empty Then
            Return False
        End If

        For i = 0 To sMCPList.Length - 1
            sTemp = sMCPList(i).Trim
            If sTemp.IndexOf("*") >= 0 Then
                If sTemp.Split("*").Length > 2 Then

                    Dim dtValue As DataTable = New DataTable
                    Dim Wc() As String = sTemp.Split("*")

                    Dim Filter As String = String.Empty

                    dtValue.Columns.Add(New DataColumn("Value"))
                    Dim dr As DataRow = dtValue.NewRow()
                    dr(0) = sName
                    dtValue.Rows.Add(dr)

                    For F As Integer = 0 To Wc.Length - 1
                        If Wc(F) <> "" Then
                            If F = 0 Then
                                Filter = Filter & "Value LIKE '%" & Wc(F) + "%' "
                            Else
                                If Filter <> "" Then
                                    Filter = Filter & " AND Value LIKE '%" & Wc(F) & "%' "
                                Else
                                    Filter = Filter & "Value LIKE '%" & Wc(F) + "%' "
                                End If


                            End If

                        End If
                    Next
                    Dim drs As DataRow() = dtValue.Select(Filter)
                    If Not IsNothing(drs) AndAlso drs.Length > 0 Then Return True


                ElseIf sTemp.StartsWith("*") Then
                    If sName.EndsWith(sTemp.Replace("*", "")) Then Return True
                ElseIf sTemp.EndsWith("*") Then
                    If sName.StartsWith(sTemp.Replace("*", "")) Then Return True
                Else
                    If sName.StartsWith(sTemp.Substring(0, sTemp.IndexOf("*"))) _
                        AndAlso sName.EndsWith(sTemp.Substring(sTemp.IndexOf("*") + 1)) Then
                        Return True
                    End If
                End If
            Else
                If sName = sTemp Then Return True
            End If
        Next

        Return False
    End Function

    Private Sub ChangesNamaByTipeCustomer()
        mode = ViewState("Mode")
        'If mode <> enumMode.Mode.EditMode Then
        If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
            If Not txtNama.Text.Trim = "" Then
                'PT or CV or UD 
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(0) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(0) + ". "), "")
                End If
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(1) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(1) + ". "), "")
                End If
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(2) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(2) + ". "), "")
                End If
                'cr spk
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(3) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(3) + ". "), "")
                End If
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(4) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(4) + ". "), "")
                End If
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(5) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(5) + ". "), "")
                End If
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(6) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(6) + ". "), "")
                End If
                'end
            End If
            'cr spk
            If Not ddlTipePerusahaan.SelectedValue = EnumTipePerusahaan.EnumTipePerusahaan.Koperasi Then
                txtNama.Text = ddlTipePerusahaan.SelectedItem.Text.Trim + ". " + txtNama.Text.Trim.ToUpper
            End If
            If ddlTipePerusahaan.SelectedValue = EnumTipePerusahaan.EnumTipePerusahaan.Koperasi Then
                txtNama.Text = ddlTipePerusahaan.SelectedItem.Text.Trim + ". " + txtNama.Text.Trim.ToUpper
            End If
            'end
        End If
        'End If
    End Sub

#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If IsPostBack Then
            If Me.hdnMCPConfirmation.Value = "1" Then
                Button1_Click(Nothing, Nothing)
            End If
        End If
        objLoginDealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        objUserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        InitiateAuthorization()
        'set privilege
        Button1.Enabled = bCekBtnSavePriv
        If Not Page.IsPostBack Then

            '------------------------------------------
            'added by soni 20170524, req by isye
            'if transactionControl non Active, pass Freeze Function
            Dim AutoCustomerStatus As Boolean = False
            'only for PK Tambahan
            'If ddlJenisPesanan.SelectedItem.Value = EnumDealerTransType.DealerTransKind.PKTambahan Then
            'cek transactionControl
            Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(sessHelp.GetSession("DEALER").ID, CInt(EnumDealerTransType.DealerTransKind.AutoCustomer).ToString)
            If Not (objTransaction Is Nothing) Then
                If objTransaction.Status = 1 Then
                    AutoCustomerStatus = True
                End If
            End If
            sessHelp.SetSession("AutoCustomerStatus", AutoCustomerStatus)

            'End If
            '-------------------------------------------

            BindDropDown()
            If Request.QueryString("isSAP") <> "true" Then
                If Request.QueryString("id") <> String.Empty And Request.QueryString("mode") = "edit" Then
                    ViewState("Mode") = enumMode.Mode.EditMode
                    objCustomerRequest = New CustomerRequestFacade(User).Retrieve(CInt(Request.QueryString("ID")))
                ElseIf Request.QueryString("id") <> String.Empty And Request.QueryString("mode") = "view" Then
                    ViewState("Mode") = enumMode.Mode.ViewMode
                    objCustomerRequest = New CustomerRequestFacade(User).Retrieve(CInt(Request.QueryString("ID")))
                Else
                    ViewState("Mode") = enumMode.Mode.NewItemMode
                    objCustomerRequest = New CustomerRequest
                    btnBack.Enabled = False
                End If
                sessHelp.SetSession("CustomerRequest", objCustomerRequest)
                fillForm()
            Else
                'get data from SAPCustomer
                Dim id As Integer = CInt(Request.QueryString("SAPID"))
                Dim objSAPCust As SAPCustomer = New SAPCustomerFacade(User).Retrieve(id)
                If Not objSAPCust Is Nothing Then
                    txtNama.Text = objSAPCust.CustomerName
                    ViewState("Mode") = enumMode.Mode.NewItemMode
                    objCustomerRequest = New CustomerRequest
                    btnBack.Enabled = True
                    sessHelp.SetSession("CustomerRequest", objCustomerRequest)
                Else
                    MessageBox.Show("Data SAPCustomer tidak ditemukan")
                End If
            End If

        End If

        'cr spk
        txtCountryCode.Text = "62"
        '
        'btnValidasi.Enabled = bCekBtnValidatePriv
        'btnBatalValidasi.Enabled = bCekBtnBtlValPriv

    End Sub
    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        ddlKota.Items.Clear()
        If ddlPropinsi.SelectedIndex <> 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, ddlPropinsi.SelectedValue))
            criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

            ddlKota.DataSource = New CityFacade(User).RetrieveActiveList(criterias, "CityName", Sort.SortDirection.ASC)
            ddlKota.DataTextField = "CityName".ToUpper
            ddlKota.DataValueField = "ID"
            ddlKota.DataBind()
        End If
        ddlKota.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlKota.SelectedIndex = 0

    End Sub
    Private Sub ddlTipe_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTipe.SelectedIndexChanged
        ddlTipePerusahaan.Visible = False
        'CR_SPK_SALESMAN_CUSTOMER_ENHANCE juni 2021 By Endang
        ddlTipePerorangan.Visible = False
        ddlIdentityAsing.Visible = False
        ddlIdentity.Visible = False
        'end
        If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan Then
            pnlPerorangan.Visible = True
            pnlPerusahaan.Visible = False
            PnlBUMN.Visible = False
            PnlLainnya.Visible = False
            'CR_SPK_SALESMAN_CUSTOMER_ENHANCE juni 2021
            ddlTipePerorangan.Visible = True
            ddlIdentity.Visible = True

            ddlTipePerorangan.Items.Clear()
            ddlTipePerorangan.DataSource = New EnumTipePerorangan().RetrieveType
            ddlTipePerorangan.DataTextField = "NameTipe"
            ddlTipePerorangan.DataValueField = "ValTipe"
            ddlTipePerorangan.DataBind()
            'ddlTipePerorangan_SelectedIndexChanged(Me, System.EventArgs.Empty)
            txtCountryCode.Text = "62"
            lblSearchCountryName.Visible = False
            txtNama.Text = ""
            'end
        ElseIf ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
            pnlPerorangan.Visible = False
            pnlPerusahaan.Visible = True
            PnlBUMN.Visible = False
            PnlLainnya.Visible = False
            ddlTipePerusahaan.Visible = True
            ddlIdentityAsing.Visible = False

            ddlTipePerusahaan.Items.Clear()
            ddlTipePerusahaan.DataSource = New EnumTipePerusahaan().RetrieveType
            ddlTipePerusahaan.DataTextField = "NameTipe"
            ddlTipePerusahaan.DataValueField = "ValTipe"
            ddlTipePerusahaan.DataBind()
            txtNama.Text = ""
            ChangesNamaByTipeCustomer()
            lblSearchCountryName.Visible = True
        ElseIf ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
            pnlPerorangan.Visible = False
            pnlPerusahaan.Visible = False
            PnlBUMN.Visible = True
            PnlLainnya.Visible = False
            txtNama.Text = ""
        ElseIf ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya Then
            pnlPerorangan.Visible = False
            pnlPerusahaan.Visible = False
            PnlBUMN.Visible = False
            PnlLainnya.Visible = True
            txtNama.Text = ""
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mode = CType(ViewState("Mode"), enumMode.Mode)
        'CR SPK
        Dim Tgl As Date
        Dim TglAuto As Date
        Dim JK As Boolean = False
        Dim str As String
        Dim KTP As String
        'end CR SPK
        'CR SPK hanya kode negara 62 yg ada validasi 
        If txtCountryCode.Text = "62" Then
            'remark for phase 2
            'If txtNoHp.Text.Trim.Substring(0, 1) <> "8" Then
            '    MessageBox.Show("format input No. Handphone  : 8XXXXXXXXX")
            '    Exit Sub
            'End If
            If txtNoHp.Text.Trim.Length < 9 Then
                MessageBox.Show("No Handphone minimal 9 digit")
                Exit Sub
            End If
        End If

        'end
        'If Not IsMCPValid(IsNothing(sender)) Then Exit Sub
        If ValidateSave() Then
            If ValidateRefCustomerCode() Then

                Dim alGroup1 As ArrayList
                Dim alGroup2 As ArrayList
                Dim alGroup3 As ArrayList
                Dim alGroup4 As ArrayList
                Dim alGroup5 As ArrayList

                Dim alGroupTotal As ArrayList = New ArrayList

                Dim objRenderPanel As RenderingProfile
                'Perorangan
                If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan Then
                    objRenderPanel = New RenderingProfile
                    alGroup1 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), CType(EnumProfileType.ProfileType.CUSTOMERREQUEST, Short), User, True)
                    'CR SPK
                    For Each oCrp2 As CustomerRequestProfile In alGroup1
                        If oCrp2.ProfileHeader.Code = "NOKTP" Then
                            KTP = oCrp2.ProfileValue

                            Dim List As ArrayList
                            Dim criterias As CriteriaComposite
                            'CustomerRequestProfile
                            criterias = New CriteriaComposite(New Criteria(GetType(CustomerRequestProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(CustomerRequestProfile), "ProfileValue", MatchType.Exact, KTP))
                            List = New CustomerRequestProfileFacade(User).Retrieve(criterias)
                            If Not List Is Nothing Then
                                If List.Count > 0 Then
                                    MessageBox.Show("Identity Number Duplicate")
                                End If
                            End If
                        End If

                        If oCrp2.ProfileHeader.Code = "JK" Then
                            If oCrp2.ProfileValue = "LK" Then
                                JK = True
                            End If
                        End If
                        If oCrp2.ProfileHeader.Code = "TGLLAHIR" Then
                            TglAuto = oCrp2.ProfileValue

                            Dim dt As DateTime = Convert.ToDateTime(TglAuto)
                            Dim format As String = "ddMMyy"
                            str = dt.ToString(format)

                            TglLahirCW.Text = str


                        End If
                    Next
                    '
                    For Each oCrp As CustomerRequestProfile In alGroup1
                        If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso (IsNothing(oCrp.ProfileValue) OrElse oCrp.ProfileValue.Trim = "") Then
                            MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                            Exit Sub
                        End If
                        'If oCrp.ProfileHeader.Code = "NOKTP" Then
                        '    If oCrp.ProfileValue.Length < 5 Then
                        '        MessageBox.Show("No KTP tidak benar")
                        '        Exit Sub
                        '    End If
                        '    If oCrp.ProfileValue.Length > 40 Then
                        '        MessageBox.Show("No KTP maksimal 40 karakter")
                        '        Exit Sub
                        '    End If
                        'End If
                        If oCrp.ProfileHeader.Code = "NOKTP" Then
                            If (TxtFlag.Text = "domestik") Then
                                'KTP validation
                                If oCrp.ProfileValue.Length < 16 AndAlso ddlIdentity.SelectedValue.ToString() = "0" Then
                                    MessageBox.Show("Identity Number minimum 16 karakter")
                                    Exit Sub
                                End If
                                If oCrp.ProfileValue.Length > 16 AndAlso ddlIdentity.SelectedValue.ToString() = "0" Then
                                    MessageBox.Show("Identity Number maksimal 16 karakter")
                                    Exit Sub
                                End If
                                'KTP validation tgl lahir dan jenis kelamin
                                If JK = True Then
                                    If ddlIdentity.SelectedValue.ToString() = "0" AndAlso oCrp.ProfileValue.Substring(6, 6) <> str Then
                                        MessageBox.Show("Identity Number tidak sesuai dengan tgl lahir")
                                        'Exit Sub
                                    End If
                                End If
                                If JK = False Then
                                    Dim lahir As String = oCrp.ProfileValue.Substring(6, 2)
                                    Dim Cewe As Integer = Int64.Parse(lahir)
                                    Dim kurang40 As Integer = Cewe - 40
                                    Dim Tgllahir2digit As String = str.Substring(0, 2)
                                    Dim Tgllahir4digit As String = str.Substring(2, 4)
                                    Dim Tgllahir2digitINT As Integer = Int64.Parse(Tgllahir2digit)

                                    If ddlIdentity.SelectedValue.ToString() = "0" Then
                                        If Tgllahir2digitINT <> kurang40 Then
                                            MessageBox.Show("Identity Number tidak sesuai dengan tgl lahir")
                                            Exit Sub
                                        End If

                                    End If
                                End If

                            End If

                            'Asing
                            If (TxtFlag.Text = "asing") Then
                                'KITAS validation
                                If oCrp.ProfileValue.Length < 11 AndAlso ddlIdentityAsing.SelectedValue.ToString() = "2" Then
                                    MessageBox.Show("Identity Number minimum 11 karakter")
                                    Exit Sub
                                End If
                                If oCrp.ProfileValue.Length > 11 AndAlso ddlIdentityAsing.SelectedValue.ToString() = "2" Then
                                    MessageBox.Show("Identity Number maksimal 11 karakter")
                                    Exit Sub
                                End If
                                'KITAP validation
                                If oCrp.ProfileValue.Length < 11 AndAlso ddlIdentityAsing.SelectedValue.ToString() = "3" Then
                                    MessageBox.Show("Identity Number minimum 11 karakter")
                                    Exit Sub
                                End If
                                If oCrp.ProfileValue.Length > 11 AndAlso ddlIdentityAsing.SelectedValue.ToString() = "3" Then
                                    MessageBox.Show("Identity Number maksimal 11 karakter")
                                    Exit Sub
                                End If
                                'End validation
                            End If

                        End If
                        If oCrp.ProfileHeader.Code = "NOTELP" Then
                            If oCrp.ProfileValue.Length < 6 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                                MessageBox.Show("No Telp tidak benar")
                                Exit Sub
                            End If
                        End If
                    Next

                Else
                    alGroup1 = New ArrayList
                End If

                'Perusahaan
                If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
                    objRenderPanel = New RenderingProfile
                    alGroup2 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), CType(EnumProfileType.ProfileType.CUSTOMERREQUEST, Short), User, True)
                    'Show Dropdownlist Company Title
                    For Each oCrp As CustomerRequestProfile In alGroup2
                        If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso oCrp.ProfileValue.Trim = "" Then
                            MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                            Exit Sub
                        End If
                        If oCrp.ProfileHeader.Code = "NOKTP" Then
                            If oCrp.ProfileValue.Length < 5 Then
                                MessageBox.Show("Identity Number tidak benar")
                                Exit Sub
                            End If
                            'phase 2 req iqbal 22-03-2022
                            If oCrp.ProfileValue.Length > 50 Then
                                MessageBox.Show("Identity Number maksimal 50 karakter")
                                Exit Sub
                            End If
                            '
                        End If
                        If oCrp.ProfileHeader.Code = "NOTELP" Then
                            If oCrp.ProfileValue.Length < 6 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                                MessageBox.Show("No Telp tidak benar")
                                Exit Sub
                            End If
                        End If
                    Next
                Else
                    alGroup2 = New ArrayList
                End If

                'BUMN
                If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                    objRenderPanel = New RenderingProfile
                    alGroup4 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), CType(EnumProfileType.ProfileType.CUSTOMERREQUEST, Short), User, True)
                    For Each oCrp As CustomerRequestProfile In alGroup4
                        If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso oCrp.ProfileValue.Trim = "" Then
                            MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                            Exit Sub
                        End If
                        If oCrp.ProfileHeader.Code = "NOKTP" Then
                            If oCrp.ProfileValue.Length < 5 Then
                                MessageBox.Show("Identity Number tidak benar")
                                Exit Sub
                            End If
                            If oCrp.ProfileValue.Length > 40 Then
                                MessageBox.Show("Identity Number maksimal 40 karakter")
                                Exit Sub
                            End If
                        End If
                        If oCrp.ProfileHeader.Code = "NOTELP" Then
                            If oCrp.ProfileValue.Length < 6 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                                MessageBox.Show("No Telp tidak benar")
                                Exit Sub
                            End If
                        End If
                    Next
                Else
                    alGroup4 = New ArrayList
                End If

                'Lainnya
                If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya Then
                    objRenderPanel = New RenderingProfile
                    alGroup5 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), CType(EnumProfileType.ProfileType.CUSTOMERREQUEST, Short), User, True)
                    For Each oCrp As CustomerRequestProfile In alGroup5
                        If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso oCrp.ProfileValue.Trim = "" Then
                            MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                            Exit Sub
                        End If
                        If oCrp.ProfileHeader.Code = "NOKTP" Then
                            If oCrp.ProfileValue.Length < 5 Then
                                MessageBox.Show("Identity Number tidak benar")
                                Exit Sub
                            End If
                            If oCrp.ProfileValue.Length > 40 Then
                                MessageBox.Show("Identity Number maksimal 40 karakter")
                                Exit Sub
                            End If
                        End If
                        If oCrp.ProfileHeader.Code = "NOTELP" Then
                            If oCrp.ProfileValue.Length < 6 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                                MessageBox.Show("No Telp tidak benar")
                                Exit Sub
                            End If
                        End If
                    Next
                Else
                    alGroup5 = New ArrayList
                End If

                'Tambahan
                objRenderPanel = New RenderingProfile
                alGroup3 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), CType(EnumProfileType.ProfileType.CUSTOMERREQUEST, Short), User, True)
                For Each oCrp As CustomerRequestProfile In alGroup3
                    If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso oCrp.ProfileValue.Trim = "" Then
                        MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                        Exit Sub
                    End If
                    If oCrp.ProfileHeader.Code = "NOKTP" Then
                        If oCrp.ProfileValue.Length < 5 Then
                            MessageBox.Show("Identity Number tidak benar")
                            Exit Sub
                        End If
                        If oCrp.ProfileValue.Length > 40 Then
                            MessageBox.Show("Identity Number maksimal 40 karakter")
                            Exit Sub
                        End If
                    End If
                    If oCrp.ProfileHeader.Code = "NOTELP" Then
                        If oCrp.ProfileValue.Length < 6 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                            MessageBox.Show("No Telp tidak benar")
                            Exit Sub
                        End If
                    End If
                Next

                Dim nResult As Integer
                objCustomerRequest = CType(sessHelp.GetSession("CustomerRequest"), CustomerRequest)
                objCustomerRequest.CustomerCode = lblKodePelanggan.Text.ToUpper
                objCustomerRequest.RequestNo = lblNoPengajuan.Text.ToUpper
                objCustomerRequest.RequestDate = DateTime.Today
                objCustomerRequest.RequestType = ddlTipePengajuan.SelectedValue
                objCustomerRequest.RequestUserID = objUserInfo.ID
                objCustomerRequest.Dealer = objLoginDealer
                objCustomerRequest.RefRequestNo = txtRefNoPengajuan.Text.ToUpper
                objCustomerRequest.Status1 = ddlTipe.SelectedValue
                objCustomerRequest.Name1 = txtNama.Text.Trim.ToUpper
                objCustomerRequest.Name2 = txtNama2.Text.Trim.ToUpper
                'If Me.IsGovernmentInstitution(objCustomerRequest.Name1) OrElse Me.IsGovernmentInstitution(objCustomerRequest.Name2) Then
                '    objCustomerRequest.MCPStatus = Me.hdnVerifyMCP.Value
                'Else
                '    objCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NonMCP
                'End If
                objCustomerRequest.Name3 = txtGedung.Text.Trim.ToUpper
                objCustomerRequest.Alamat = txtAlamat.Text.Trim.ToUpper
                objCustomerRequest.Kelurahan = txtKelurahan.Text.Trim.ToUpper
                objCustomerRequest.Kecamatan = txtKecamatan.Text.Trim.ToUpper
                objCustomerRequest.CityID = ddlKota.SelectedValue
                'Remark cz CR SPK
                objCustomerRequest.PhoneNo = txtTelepon.Text.ToUpper
                objCustomerRequest.PhoneNo = txtNoHp.Text.ToUpper
                '
                objCustomerRequest.ReffCode = txtRefKodePelanggan.Text.ToUpper
                objCustomerRequest.PrintRegion = ddlCetak.SelectedValue
                objCustomerRequest.Email = txtEmail.Text.Trim.ToUpper
                objCustomerRequest.PreArea = ddlPreArea.SelectedValue.ToUpper

                If (ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan) Then
                    'If (ddlTipePerusahaan.SelectedValue <> -1) Then
                    objCustomerRequest.TipePerusahaan = ddlTipePerusahaan.SelectedValue
                    Dim _strPerusahaanType As String = CType(ddlTipePerusahaan.SelectedValue, EnumTipePerusahaan.EnumTipePerusahaan).ToString
                    'cr spk
                    If _strPerusahaanType.Trim.ToUpper <> EnumTipePerusahaan.EnumTipePerusahaan.Koperasi.ToString.Trim.ToUpper Then
                        Dim strTemp As String = ""
                        'objCustomerRequest.Name1 = _strPerusahaanType.ToUpper & "." & txtNama.Text.ToUpper
                        If objCustomerRequest.Name1.Length > 40 Then
                            strTemp = objCustomerRequest.Name1.Substring(40)
                            objCustomerRequest.Name1 = objCustomerRequest.Name1.Substring(0, 40)
                            objCustomerRequest.Name2 = strTemp & objCustomerRequest.Name2
                            If objCustomerRequest.Name2.Length > 40 Then
                                strTemp = objCustomerRequest.Name2.Substring(40)
                                objCustomerRequest.Name2 = objCustomerRequest.Name2.Substring(0, 40)
                                objCustomerRequest.Name3 = strTemp & objCustomerRequest.Name3
                                If objCustomerRequest.Name3.Length > 40 Then
                                    strTemp = objCustomerRequest.Name3.Substring(40)
                                    objCustomerRequest.Name3 = objCustomerRequest.Name3.Substring(0, 40)
                                    objCustomerRequest.Alamat = strTemp & objCustomerRequest.Alamat
                                    If objCustomerRequest.Alamat.Length > 60 Then
                                        objCustomerRequest.Alamat = objCustomerRequest.Alamat.Substring(0, 60)
                                    End If
                                End If
                            End If
                        End If
                    Else
                        'objCustomerRequest.Name1 = txtNama.Text.ToUpper
                        Dim strTemp As String = ""
                        'objCustomerRequest.Name1 = _strPerusahaanType.ToUpper & "." & txtNama.Text.ToUpper
                        If objCustomerRequest.Name1.Length > 40 Then
                            strTemp = objCustomerRequest.Name1.Substring(40)
                            objCustomerRequest.Name1 = objCustomerRequest.Name1.Substring(0, 40)
                            objCustomerRequest.Name2 = strTemp & objCustomerRequest.Name2
                            If objCustomerRequest.Name2.Length > 40 Then
                                strTemp = objCustomerRequest.Name2.Substring(40)
                                objCustomerRequest.Name2 = objCustomerRequest.Name2.Substring(0, 40)
                                objCustomerRequest.Name3 = strTemp & objCustomerRequest.Name3
                                If objCustomerRequest.Name3.Length > 40 Then
                                    strTemp = objCustomerRequest.Name3.Substring(40)
                                    objCustomerRequest.Name3 = objCustomerRequest.Name3.Substring(0, 40)
                                    objCustomerRequest.Alamat = strTemp & objCustomerRequest.Alamat
                                    If objCustomerRequest.Alamat.Length > 60 Then
                                        objCustomerRequest.Alamat = objCustomerRequest.Alamat.Substring(0, 60)
                                    End If
                                End If
                            End If
                        End If
                    End If
                    'end spk
                    'End If
                End If
                'CR SPK
                If ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perorangan Then
                    objCustomerRequest.TypePerorangan = CInt(ddlTipePerorangan.SelectedValue)
                    If ddlTipePerorangan.SelectedValue = EnumTipePerorangan.EnumTipePerorangan.Domestik Then
                        objCustomerRequest.TypeIdentitas = CInt(ddlIdentity.SelectedValue)
                    ElseIf ddlTipePerorangan.SelectedValue = EnumTipePerorangan.EnumTipePerorangan.Asing Then
                        objCustomerRequest.TypeIdentitas = CInt(ddlIdentityAsing.SelectedValue)
                    End If
                End If
                '
                'CR SPK
                objCustomerRequest.CountryCode = txtCountryCode.Text.Trim
                '

                If txtEmail.Text.Trim <> String.Empty Then
                    If EmailAddressCheck(txtEmail.Text.Trim) = False Then
                        MessageBox.Show("Alamat email tidak valid")
                        Return
                    End If
                End If
                If txtKodePos.Text.Trim = String.Empty Then
                    objCustomerRequest.PostalCode = "00000"
                Else
                    objCustomerRequest.PostalCode = txtKodePos.Text.ToUpper
                End If
                If Not fileUpload.Value = String.Empty Then
                    objCustomerRequest.Attachment = "CSR" & lblNoPengajuan.Text & Path.GetFileName(fileUpload.PostedFile.FileName)
                End If


                If mode = enumMode.Mode.NewItemMode Then
                    For Each item As CustomerRequestProfile In alGroup1
                        alGroupTotal.Add(item)
                    Next
                    For Each item As CustomerRequestProfile In alGroup2
                        alGroupTotal.Add(item)
                    Next
                    For Each item As CustomerRequestProfile In alGroup3
                        alGroupTotal.Add(item)
                    Next
                    For Each item As CustomerRequestProfile In alGroup4
                        alGroupTotal.Add(item)
                    Next
                    For Each item As CustomerRequestProfile In alGroup5
                        alGroupTotal.Add(item)
                    Next
                    If alGroupTotal.Count > 0 Then
                        nResult = New CustomerRequestFacade(User).Insert(objCustomerRequest, alGroupTotal)
                    Else
                        'MessageBox.Show(SR.SaveFail)
                        MessageBox.Show("Proses simpan gagal, Tolong capture screen isian anda dan kirimkan ke D-Net Helpdesk untuk investigasi masalah")
                    End If

                Else
                    nResult = New CustomerRequestFacade(User).Update(objCustomerRequest, alGroup1, alGroup2, alGroup3, alGroup4, alGroup5, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), New ProfileGroupFacade(User).Retrieve("cust_dbs_5"))
                End If

                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    If fileUpload.Value <> "" OrElse fileUpload.Value <> Nothing Then
                        Dim SrcFile As String = Path.GetFileName(fileUpload.PostedFile.FileName)  '-- Source file name
                        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("CustomerRequestDir") & "\CSR" & lblNoPengajuan.Text & SrcFile
                        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                        Dim success As Boolean = False
                        Dim finfo As New FileInfo(DestFile)
                        Try
                            success = imp.Start()
                            If success Then
                                If Not finfo.Directory.Exists Then
                                    Directory.CreateDirectory(finfo.DirectoryName)
                                End If
                                fileUpload.PostedFile.SaveAs(DestFile)
                                imp.StopImpersonate()
                                imp = Nothing
                            End If
                        Catch ex As Exception
                            Throw ex
                        End Try
                    End If


                    sessHelp.SetSession("CustomerRequest", New CustomerRequestFacade(User).Retrieve(nResult))
                    objCustomerRequest = CType(sessHelp.GetSession("CustomerRequest"), CustomerRequest)
                    ViewState("Mode") = enumMode.Mode.EditMode
                    fillForm()
                    'Save To CustomerStatusHistory
                    Dim _facadeHistory As New CustomerStatusHistoryFacade(User)
                    Dim _history As New CustomerStatusHistory
                    _history.CustomerRequest = objCustomerRequest
                    _history.OldStatus = 99
                    _history.NewStatus = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Baru
                    Try
                        _facadeHistory.Insert(_history)
                        MessageBox.Show("Simpan berhasil !")
                    Catch ex As Exception
                        MessageBox.Show("Simpan History Gagal Caused By : " & ex.Message)
                    End Try


                End If
            End If
        End If
    End Sub

    Private Sub lnkbtnDeleteAttachment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDeleteAttachment.Click
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                objCustomerRequest = CType(sessHelp.GetSession("CustomerRequest"), CustomerRequest)
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("CustomerRequestDir") & "\" & objCustomerRequest.Attachment

                Dim finfo As New FileInfo(DestFile)

                If finfo.Exists Then
                    finfo.Delete()

                    objCustomerRequest.Attachment = String.Empty
                    Dim nResult As Integer = New CustomerRequestFacade(User).Update(objCustomerRequest)
                    sessHelp.SetSession("CustomerRequest", New CustomerRequestFacade(User).Retrieve(nResult))
                    objCustomerRequest = CType(sessHelp.GetSession("CustomerRequest"), CustomerRequest)
                    ViewState("Mode") = enumMode.Mode.EditMode
                    fillForm()
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Request.QueryString("isSAP") <> "true" Then
            Response.Redirect("FrmCustomerRequestStatusList.aspx")
        Else
            'Response.Redirect("../SAP/FrmSAPCustomer.aspx?isBack=1")
            Response.Redirect("../FinishUnit/FrmPreCustomerEntry.aspx?isBack=1")
        End If

    End Sub
    Private Sub lbtnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDownload.Click
        Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("CustomerRequestDir") & "\" & CType(sessHelp.GetSession("CustomerRequest"), CustomerRequest).Attachment
        Response.Redirect("../Download.aspx?file=" & PathFile)
    End Sub
    Private Sub btnValidasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidasi.Click
        CreateBatal("validasi", New EnumStatusCustomerRequest().TipePengajuanCustomerRequest.Validasi)
    End Sub
    Private Sub btnBatalValidasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatalValidasi.Click
        CreateBatal("baru", New EnumStatusCustomerRequest().TipePengajuanCustomerRequest.Baru)
    End Sub
    Private Sub btnSelesai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelesai.Click
        CreateBatal("Selesai", New EnumStatusCustomerRequest().TipePengajuanCustomerRequest.Selesai)
    End Sub
    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        ViewState("Mode") = enumMode.Mode.NewItemMode
        Response.Redirect("FrmCustomerRequest.aspx")
    End Sub
    Private Sub btnBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlock.Click
        CreateBatal("block", New EnumStatusCustomerRequest().TipePengajuanCustomerRequest.Block)
    End Sub
    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        CreateBatal("proses", New EnumStatusCustomerRequest().TipePengajuanCustomerRequest.Proses)
    End Sub
    Private Sub btnBatalProses_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatalProses.Click
        CreateBatal("validasi", New EnumStatusCustomerRequest().TipePengajuanCustomerRequest.Validasi)
    End Sub
    Private Sub btnBatalBlock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatalBlock.Click
        CreateBatal("baru", New EnumStatusCustomerRequest().TipePengajuanCustomerRequest.Baru)
    End Sub

    Private Function GetCategoriForCustomer(ByVal code As String) As CustomerRequest
        Dim objCustReq As CustomerRequest
        objCustReq = New CustomerRequestFacade(User).RetrieveCodeDesc(code)
        Return objCustReq
    End Function
    Private Sub lnkReloadReff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkReloadReff.Click
        If txtRefNoPengajuan.Text.Trim <> String.Empty Then
            Dim objCustRequest As CustomerRequest = New CustomerRequestFacade(User).RetrieveByRefRequestNo(txtRefNoPengajuan.Text.Trim, CType(sessHelp.GetSession("DEALER"), Dealer).ID)
            If objCustRequest.ID <> 0 Then
                ddlTipe.SelectedValue = objCustRequest.Status1
                ddlTipe_SelectedIndexChanged(Me, System.EventArgs.Empty)
                txtNama.Text = objCustRequest.Name1.ToUpper
                txtNama2.Text = objCustRequest.Name2.ToUpper
                txtGedung.Text = objCustRequest.Name3.ToUpper
                txtAlamat.Text = objCustRequest.Alamat.ToUpper
                txtKelurahan.Text = objCustRequest.Kelurahan.ToUpper
                txtKecamatan.Text = objCustRequest.Kecamatan.ToUpper
                Dim objcity As City = New CityFacade(User).Retrieve(objCustRequest.CityID)
                ddlPropinsi.SelectedValue = objcity.Province.ID
                ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
                ddlKota.SelectedValue = objCustRequest.CityID
                txtTelepon.Text = objCustRequest.PhoneNo
                'CR SPK
                txtNoHp.Text = objCustRequest.PhoneNo
                '
                ddlCetak.SelectedValue = objCustRequest.PrintRegion
                txtEmail.Text = objCustRequest.Email
                txtKodePos.Text = objCustRequest.PostalCode
                If objCustRequest.Attachment <> String.Empty Then
                    lbtnDownload.Visible = True
                End If

                If Not objCustRequest.PreArea Is Nothing Or objCustRequest.PreArea <> "blank" Or objCustRequest.PreArea <> String.Empty Then
                    Try
                        ddlPreArea.SelectedValue = objCustRequest.PreArea.Substring(0, 1).ToUpper + objCustRequest.PreArea.Substring(1).ToUpper
                    Catch ex As Exception
                        ddlPreArea.SelectedIndex = 0
                    End Try
                Else
                    ddlPreArea.SelectedIndex = 0
                End If

            Else
                MessageBox.Show("Referensi nomor pengajuan tidak valid")
            End If
        Else
            MessageBox.Show("Referensi nomor pengajuan tidak valid")
        End If
    End Sub

    Private Sub lnkReloadPlg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkReloadPlg.Click
        If txtRefKodePelanggan.Text.Trim <> String.Empty Then
            Dim objCustomer As Customer = New CustomerDealerFacade(User).RetrieveByRefCode(txtRefKodePelanggan.Text.Trim, CType(sessHelp.GetSession("DEALER"), Dealer).ID)
            If objCustomer.ID <> 0 Then
                If GetCategoriForCustomer(txtRefKodePelanggan.Text.Trim).ID <> 0 Then
                    ddlTipe.SelectedValue = GetCategoriForCustomer(txtRefKodePelanggan.Text.Trim).Status1
                    ddlTipe_SelectedIndexChanged(Me, System.EventArgs.Empty)
                End If
                txtNama.Text = objCustomer.Name1.ToUpper
                txtNama2.Text = objCustomer.Name2.ToUpper
                txtGedung.Text = objCustomer.Name3.ToUpper
                txtAlamat.Text = objCustomer.Alamat.ToUpper
                txtKelurahan.Text = objCustomer.Kelurahan.ToUpper
                txtKecamatan.Text = objCustomer.Kecamatan.ToUpper
                ddlPropinsi.SelectedValue = objCustomer.City.Province.ID
                ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
                ddlKota.SelectedValue = objCustomer.City.ID
                If objCustomer.PreArea <> String.Empty And objCustomer.PreArea <> "blank" Then
                    Try
                        ddlPreArea.SelectedValue = objCustomer.PreArea.Substring(0, 1).ToUpper + objCustomer.PreArea.Substring(1).ToUpper
                    Catch ex As Exception
                        ddlPreArea.SelectedIndex = 0
                    End Try
                Else
                    ddlPreArea.SelectedIndex = 0
                End If
                txtTelepon.Text = objCustomer.PhoneNo
                'CR SPK
                txtNoHp.Text = objCustomer.PhoneNo
                '
                If objCustomer.PrintRegion = "X" Then
                    ddlCetak.SelectedValue = 0
                Else
                    ddlCetak.SelectedValue = 1
                End If
                txtEmail.Text = objCustomer.Email
                txtKodePos.Text = objCustomer.PostalCode
                If objCustomer.Attachment <> String.Empty Then
                    lbtnDownload.Visible = True
                End If
            Else
                MessageBox.Show("Referensi kode pelanggan tidak valid")
            End If
        Else
            MessageBox.Show("Referensi kode pelanggan tidak valid")
        End If
    End Sub
    Protected Sub ddlTipePerusahaan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipePerusahaan.SelectedIndexChanged
        ChangesNamaByTipeCustomer()
    End Sub
    'CR_SPK_SALESMAN_CUSTOMER_ENHANCE juni 2021
    Private Sub ddlTipePerorangan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipePerorangan.SelectedIndexChanged
        'ChangesNamaByTipeCustomer()


        If ddlTipePerorangan.SelectedValue = EnumTipePerorangan.EnumTipePerorangan.Domestik Then

            ddlIdentityAsing.Visible = False
            ddlIdentity.Visible = True
            TxtFlag.Text = "domestik"

            'CR SPK
            lblSearchCountryName.Visible = False
            txtCountryCode.Text = "62"
            '

        ElseIf ddlTipePerorangan.SelectedValue = EnumTipePerorangan.EnumTipePerorangan.Asing Then

            ddlIdentityAsing.Visible = True
            ddlIdentity.Visible = False
            TxtFlag.Text = "asing"
            'CR SPK
            lblSearchCountryName.Visible = True
            txtCountryCode.Text = ""
            '

        End If
    End Sub
    'END
#End Region


End Class
