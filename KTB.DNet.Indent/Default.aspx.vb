Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Response.Redirect("~/Page/Login.aspx")
        End If
    End Sub
End Class