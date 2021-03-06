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
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security
#End Region


Public Class FrmCustomerDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPerorangan As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPerusahaan As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlTambahan As System.Web.UI.WebControls.Panel
    Protected WithEvents lblKodePelanggan As System.Web.UI.WebControls.Label
    Protected WithEvents lblGedung As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents lblKelurahan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecamatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblPropinsi As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents lblTelepon As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodePos As System.Web.UI.WebControls.Label
    Protected WithEvents lblCetak As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents PnlBUMN As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlLainnya As System.Web.UI.WebControls.Panel
    Protected WithEvents lblCetakTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama2 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
        Dim code As String = Request.QueryString("qxctrvvyuotrpn")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerRequest), "ID", MatchType.Exact, code))
        Dim list As ArrayList = New CustomerRequestFacade(User).Retrieve(criterias)
        Dim obj_CustomerRequest As CustomerRequest
        pnlPerorangan.Visible = False
        pnlPerusahaan.Visible = False
        pnlTambahan.Visible = False
        PnlBUMN.Visible = False
        PnlLainnya.Visible = False
        If list.Count > 0 Then
            pnlTambahan.Visible = True
            obj_CustomerRequest = list(0)
            If obj_CustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                PnlBUMN.Visible = True
            End If
            If obj_CustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya Then
                PnlLainnya.Visible = True
            End If
            If obj_CustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan Then
                pnlPerorangan.Visible = True
            End If
            If obj_CustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
                pnlPerusahaan.Visible = True
            End If
            RenderProfilePanel(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerorangan)
            RenderProfilePanel(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerusahaan)
            RenderProfilePanel(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlTambahan)
            RenderProfilePanel(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlBUMN)
            RenderProfilePanel(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlLainnya)
        End If
    End Sub

#End Region

#Region " Private Variables"
    Private objCustomer As Customer
    Dim sessHelp As KTB.DNet.Utility.SessionHelper = New KTB.DNet.Utility.SessionHelper
    Private objLoginDealer As Dealer
    Private objUserInfo As UserInfo
    Private mode As enumMode.Mode
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerListViewDetail_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=KONSUMEN - Lihat Detail")
        End If
    End Sub
#End Region

    Private Sub RenderProfilePanel(ByVal objCustomerRequest As CustomerRequest, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel)
        Dim objRenderPanel As RenderingProfile
        If Not objCustomerRequest Is Nothing Then
            objRenderPanel = New RenderingProfile(True)
            objRenderPanel.GeneratePanel(objCustomerRequest.ID, objPanel, objGroup, profileType, User)
        End If
    End Sub

#Region "Custom Method"

    Private Sub LoadFormView()
        lblKodePelanggan.Text = objCustomer.Code
        lblNama1.Text = objCustomer.Name1
        lblNama2.Text = objCustomer.Name2
        lblGedung.Text = objCustomer.Name3
        lblAlamat.Text = objCustomer.Alamat
        lblKelurahan.Text = objCustomer.Kelurahan
        lblKecamatan.Text = objCustomer.Kecamatan
        Dim objCity As City = New CityFacade(User).Retrieve(objCustomer.City.ID)
        lblPropinsi.Text = objCity.Province.ProvinceName
        If objCustomer.PreArea <> String.Empty And objCustomer.PreArea <> "blank" Then
            lblKota.Text = objCustomer.PreArea + " " + objCity.CityName
        Else
            lblKota.Text = objCity.CityName
        End If

        lblTelepon.Text = objCustomer.PhoneNo
        lblKodePos.Text = objCustomer.PostalCode
        lblEmail.Text = objCustomer.Email
        lblCetak.Text = objCustomer.PrintRegion
    End Sub

    Private Sub LoadFormView(ByVal _objCustomerReq As CustomerRequest)
        lblKodePelanggan.Text = _objCustomerReq.CustomerCode
        lblNama1.Text = _objCustomerReq.Name1
        lblNama2.Text = _objCustomerReq.Name2
        lblGedung.Text = _objCustomerReq.Name3
        lblAlamat.Text = _objCustomerReq.Alamat
        lblKelurahan.Text = _objCustomerReq.Kelurahan
        lblKecamatan.Text = _objCustomerReq.Kecamatan
        Dim objCity As City = New CityFacade(User).Retrieve(_objCustomerReq.CityID)
        lblPropinsi.Text = objCity.Province.ProvinceName
        If objCustomer.PreArea <> String.Empty And objCustomer.PreArea <> "blank" Then
            lblKota.Text = objCustomer.PreArea + " " + objCity.CityName
        Else
            lblKota.Text = objCity.CityName
        End If

        lblTelepon.Text = _objCustomerReq.PhoneNo
        lblKodePos.Text = _objCustomerReq.PostalCode
        lblEmail.Text = _objCustomerReq.Email
        lblCetak.Text = _objCustomerReq.PrintRegion


    End Sub

    Private Sub fillForm()
        mode = CType(ViewState("Mode"), enumMode.Mode)
        'Get CustomerRequest From QueryString
        Dim _custRequest As New CustomerRequest
        Dim _custReqfacade As New CustomerRequestFacade(User)
        If Not (Request.QueryString("qxctrvvyuotrpn") Is Nothing) Then
            _custRequest = _custReqfacade.Retrieve(Integer.Parse(Request.QueryString("qxctrvvyuotrpn")))
            If objCustomer.Code = "" AndAlso _custRequest.CustomerCode <> "" Then
                objCustomer = New CustomerFacade(User).Retrieve(_custRequest.CustomerCode)
            End If
            If Not _custRequest Is Nothing Then
                LoadFormView(_custRequest)
            Else
                LoadFormView()
            End If
        Else
            LoadFormView()

        End If
    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objLoginDealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        objUserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        InitiateAuthorization()
        If Not Page.IsPostBack Then
            Dim code As String = Request.QueryString("qxctrvvyuotrplgcd")
            objCustomer = New CustomerFacade(User).Retrieve(code)
            ViewState("Mode") = enumMode.Mode.ViewMode
            sessHelp.SetSession("Customer", objCustomer)
            fillForm()
        End If

    End Sub

#End Region

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmCustomerList.aspx")
    End Sub
End Class
