Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Public Class frmForgetPasswordConfirmation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblConfirmationMessage As System.Web.UI.WebControls.Label
    Protected WithEvents LinkButton1 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ImgPC As System.Web.UI.WebControls.Image
    Protected WithEvents lblKonfirmasiHeader As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim msg2Login As String = Page.Request.QueryString("msg2Login")
            Dim message As String = Page.Request.QueryString("msg")
            Dim msg2Auth As String = Page.Request.QueryString("msg2Auth")

            If msg2Login <> String.Empty Then
                lblConfirmationMessage.Text = msg2Login
                lblKonfirmasiHeader.Text = "Konfirmasi Kode Aktivasi"
            ElseIf message <> String.Empty Then
                lblConfirmationMessage.Text = message
                lblKonfirmasiHeader.Text = "Konfirmasi Reset Password"
            ElseIf msg2Auth <> String.Empty Then
                lblConfirmationMessage.Text = msg2Auth
                lblKonfirmasiHeader.Text = "Konfirmasi Login Gagal"
            End If
            ImgPC.ImageUrl = "WebResources\GetPCImage.aspx"
        End If
    End Sub

     
    Private Sub LinkButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Session.RemoveAll()

        Response.Redirect("Login.aspx")
    End Sub
End Class
