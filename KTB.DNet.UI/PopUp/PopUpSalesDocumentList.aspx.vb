Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Lib
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class PopUpSalesDocumentList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgDocumentHistory As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim sHelper As New SessionHelper
    Dim criterias As CriteriaComposite
#End Region

#Region "Custom Method"
    Private Sub BindToGrid()
        Dim LetterID As Integer = CInt(viewstate("ID"))
        CreateCriteria()

        Dim arlHistory As ArrayList = New LetterHistoryFacade(User).Retrieve(criterias)
        If arlHistory.Count > 0 Then
            dtgDocumentHistory.DataSource = arlHistory
        Else
            dtgDocumentHistory.DataSource = New ArrayList
        End If

        dtgDocumentHistory.DataBind()
    End Sub

    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(LetterHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(LetterHistory), "Letter.ID", MatchType.Exact, CInt(viewstate("ID"))))
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            Dim strID As String = Request.QueryString("id")
            If Not strID = "" Then
                viewstate.Add("ID", strID)
                BindToGrid()
            End If
        End If
    End Sub

    Private Sub dtgDocumentHistory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDocumentHistory.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblno As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblno.Text = e.Item.ItemIndex + 1
        End If
    End Sub
End Class
