Public Class FrmInvalidMenuControl
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("reason")) Then
                lblInfo.Text = "MAAF : " & Server.UrlDecode(Request.QueryString("reason").ToString())
            Else
                lblInfo.Text = "MAAF : ANDA TIDAK PUNYA AKSES PADA MODUL INI"
            End If
        End If
    End Sub

End Class