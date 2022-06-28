Public Class PopUpImage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                If Not IsNothing(Request.QueryString("type")) Then

                    photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & Request.QueryString("url").ToString() & "&type=" & Request.QueryString("type").ToString()
                Else
                    photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & Request.QueryString("url").ToString() & "&type=SPKCustomer"
                End If

            Catch ex As Exception
                photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=notfound.png&type=SPKCustomer"
            End Try
        End If
    End Sub

End Class