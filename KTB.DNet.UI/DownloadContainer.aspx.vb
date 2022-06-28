Public Class DownloadContainer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtMessage As System.Web.UI.WebControls.Label
    Protected WithEvents txtDownloadLocation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFromURL As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private _fromURL As String
    Private _strFileName As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        _strFileName = Request.QueryString("file")
        _fromURL = Request.QueryString("from")

        Me.txtDownloadLocation.Text = _strFileName
        Me.txtFromURL.Text = _fromURL

        Me.txtMessage.Text = "Download berhasil"
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(Me.txtFromURL.Text)
    End Sub
End Class
