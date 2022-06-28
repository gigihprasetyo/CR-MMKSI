Public Class FrmComment
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblKomentar As System.Web.UI.WebControls.Label
    Protected WithEvents txtComment As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack() Then
            Dim str As String = Request.QueryString("text")
            Dim tempchr As Char() = str.ToCharArray
            If str <> String.Empty Then
                For i As Integer = 0 To str.Length - 1

                    If (tempchr(i) = "@") AndAlso (tempchr(i + 1) = "*") Then
                        tempchr(i) = Chr(13)
                    ElseIf (tempchr(i) = "*") Then
                        'AndAlso tempchr(i - 1) = Chr(13) 
                        tempchr(i) = Chr(10)
                    ElseIf (tempchr(i) = "|") Then
                        tempchr(i) = Chr(60)
                    End If
                Next
                txtComment.Text = tempchr
            End If
        End If
    End Sub

End Class
