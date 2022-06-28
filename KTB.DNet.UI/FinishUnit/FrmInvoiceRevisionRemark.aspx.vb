Public Class FrmInvoiceRevisionRemark
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'If Not IsPostBack Then
        lblCreatorName.Text = User.Identity.Name.Substring(6)
        lblDate.Text = Format(DateTime.Now, "dd MMM yyyy")
        Dim str As String = Request.QueryString("text")
        Dim tempchr As Char() = str.ToCharArray
        If str <> String.Empty Then
            For i As Integer = 0 To str.Length - 1

                If (tempchr(i) = "@") AndAlso (tempchr(i + 1) = "*") Then
                    tempchr(i) = Chr(13)
                ElseIf (tempchr(i) = "*") Then
                    tempchr(i) = Chr(10)
                ElseIf (tempchr(i) = "|") Then
                    tempchr(i) = Chr(60)
                End If
            Next
            txtComment.Text = tempchr
        End If

        lblTitle.Text = "Invoice Revision Remark"
        lblKomentar.Text = "Detail Remark"
        lblCreatedBy.Text = "Remark Oleh :"
        lblCreatedDate.Text = "Pada Tanggal :"

    End Sub

End Class