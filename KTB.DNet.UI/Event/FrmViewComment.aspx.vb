Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade

Public Class FrmViewComment
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblNote As System.Web.UI.WebControls.Label
    Protected WithEvents ltrComment As System.Web.UI.WebControls.Literal

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Sub DisplayComment(ByVal id As Integer)
        'Dim oInfo As New EventInfo
        'oInfo = New [Event].EventInfoFacade(User).Retrieve(id)
        'ltrComment.Text = oInfo.ConfirmedComment
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim _id As String = Request.QueryString("id")
        Dim _page As String = Request.QueryString("page")

        Dim oInfo As New EventInfo
        oInfo = New [Event].EventInfoFacade(User).Retrieve(CInt(_id))
        If Not IsNothing(oInfo) Then
            If IsNothing(_page) Then
                ltrComment.Text = oInfo.ConfirmedComment
            Else
                ltrComment.Text = oInfo.RealComment
            End If
        Else
            ltrComment.Text = String.Empty
        End If
    End Sub

End Class
