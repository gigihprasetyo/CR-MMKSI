Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmMasterLeasingDetail
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim _sHelper As New SessionHelper
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblGroupName As System.Web.UI.WebControls.Label
    Protected WithEvents lblProvince As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents lblPostalCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblPhoneNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents lblCP As System.Web.UI.WebControls.Label
    Protected WithEvents lblHP As System.Web.UI.WebControls.Label

    Protected WithEvents formLeasing As System.Web.UI.WebControls.Panel
    Protected WithEvents formGrid As System.Web.UI.WebControls.Panel



    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sessHelper As New SessionHelper
    Private objDomain As Leasing = New Leasing
    Private objFacade As LeasingFacade = New LeasingFacade(User)

#Region "Private Property"


#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Sales_Umum_Master_Leasing_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Master Leasing")
        End If

    End Sub

#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then
            ViewModel(Request.QueryString!id)
        End If
    End Sub





    Private Sub ViewModel(ByVal nID As Short)
        Dim objDomain As Leasing = New LeasingFacade(User).Retrieve(nID)
        'Todo session
        If Not IsNothing(objDomain) Then
            lblCode.Text = objDomain.LeasingCode
            lblName.Text = objDomain.LeasingName
            lblGroupName.Text = objDomain.LeasingGroupName
            lblProvince.Text = objDomain.Province
            lblCity.Text = objDomain.City
            lblAlamat.Text = objDomain.Alamat
            lblPostalCode.Text = objDomain.PostalCode
            lblPhoneNo.Text = objDomain.PhoneNo
            lblEmail.Text = objDomain.Email
            lblCP.Text = objDomain.ContactPerson
            lblHP.Text = objDomain.HP
        End If
    End Sub

End Class
