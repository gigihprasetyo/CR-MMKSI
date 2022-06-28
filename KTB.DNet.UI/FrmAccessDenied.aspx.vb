Public Class FrmAccessDenied
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents labelCallAdmin As HtmlTableCell
    Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label

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
        'Put user code to initialize the page here
        If Not IsPostBack Then

            Dim isEncode As Boolean = False

            If Not IsNothing(Request.QueryString("isEncode")) AndAlso Request.QueryString("isEncode").ToString() = "1" Then
                isEncode = True
            End If
            If Not IsNothing(Request.QueryString("mess")) Then
                Dim strMessage As String = CType(Request.QueryString("mess"), String)
                Dim strDescription As String

                If Not IsNothing(Request.QueryString("messDescription")) Then
                    strDescription = CType(Request.QueryString("messDescription"), String)
                End If


                If isEncode Then
                    strMessage = Server.UrlDecode(strMessage)
                    strDescription = Server.UrlDecode(strDescription)
                End If
                If (String.IsNullOrEmpty(strMessage) = False) Then
                    strMessage = strMessage.ToUpper
                End If

                If (String.IsNullOrEmpty(strDescription) = False) Then
                    strDescription = strDescription.ToUpper
                End If

                lblInfo.Text = "MAAF : " & strMessage.ToUpper
                If (String.IsNullOrEmpty(strDescription) = False) Then
                    strDescription = strDescription.Replace("|", "<br />")

                    lblDescription.Text = strDescription
                End If

                labelCallAdmin.Visible = False
            ElseIf Not IsNothing(Request.QueryString("modulName")) Then
                Dim strModuleName As String = CType(Request.QueryString("modulName"), String)
                If isEncode Then
                    strModuleName = Server.UrlDecode(strModuleName)
                End If

                lblInfo.Text = "MAAF : ANDA TIDAK PUNYA AKSES PADA MODUL " & strModuleName.ToUpper
            Else
                lblInfo.Text = "MAAF : ANDA TIDAK PUNYA AKSES PADA MODUL INI"
            End If

        End If
    End Sub

End Class
