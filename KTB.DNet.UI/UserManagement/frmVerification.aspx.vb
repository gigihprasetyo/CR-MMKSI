#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
#End Region

Public Class frmVerification
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnMainPage As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblUserName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblRegStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblActivStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblSerialNumb As System.Web.UI.WebControls.Label
    Protected WithEvents lblActiveCode As System.Web.UI.WebControls.Label
    Protected WithEvents LnkTerms As System.Web.UI.WebControls.LinkButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Fields"
    Dim sessHelp As SessionHelper = New SessionHelper
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim oUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            oUserInfo = New UserInfoFacade(User).Retrieve(oUserInfo.ID)
            If oUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER.LEASING Then
                LnkTerms.Attributes.Add("onclick", "window.open('euladsf.html','disclaimer','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=700,height=500'); return false;")
            Else
                LnkTerms.Attributes.Add("onclick", "window.open('eula2.html','disclaimer','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=700,height=500'); return false;")
            End If
            sessHelp.SetSession("LOGINUSERINFO", oUserInfo)
            lblUserName.Text = oUserInfo.UserName
            lblDealer.Text = oUserInfo.Dealer.DealerName
            lblRegStatus.Text = CType(oUserInfo.UserProfile.RegistrationStatus, EnumSE.RegistrationStatus).ToString()
            lblActivStatus.Text = CType(oUserInfo.UserProfile.ActivationStatus, EnumSE.ActivationCodeStatus).ToString()
            lblSerialNumb.Text = String.Empty
            If Not oUserInfo.UserProfile Is Nothing Then
                If Not oUserInfo.UserProfile.Bingo Is Nothing Then
                    lblSerialNumb.Text = oUserInfo.UserProfile.Bingo.SerialNumber
                End If
            End If
            lblActiveCode.Text = oUserInfo.UserProfile.ActivationCode
            If lblActiveCode.Text = String.Empty Then
                lblActiveCode.Text = oUserInfo.UserProfile.TempActivationCode
            End If
        End If
    End Sub

    Private Sub GotoMainMenu()
        Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        Dim UrlGeneral As String = "../default_general.aspx?type=0"
        Dim UrlEULA As String = "../frmUELA.aspx?type=0"
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Response.Redirect(UrlEULA)
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Response.Redirect(UrlGeneral)
            'RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlGeneral + """)</script>")
        End If
    End Sub

    Private Sub btnMainPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainPage.Click
        GotoMainMenu()
    End Sub
End Class
