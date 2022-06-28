Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain

Public Class PopUpLeasingCompany
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgLeasingCompany As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnSearch As Global.System.Web.UI.WebControls.Button
    Protected WithEvents txtLeasingName As Global.System.Web.UI.WebControls.TextBox

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
            ViewState("currentSortColumn") = "LeasingCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            BindResult(0)
        End If
    End Sub
    Private Sub BindResult(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtLeasingName.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(Leasing), "LeasingName", MatchType.Partial, txtLeasingName.Text.Trim))
        End If

        Dim arrList As ArrayList = New LeasingFacade(User).RetrieveByCriteria(criterias, indexPage, dgLeasingCompany.PageSize, totalRow)
        dgLeasingCompany.DataSource = arrList
        dgLeasingCompany.VirtualItemCount = totalRow
        dgLeasingCompany.DataBind()
        If dgLeasingCompany.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgLeasingCompany.CurrentPageIndex = 0
        BindResult(0)
    End Sub

    Private Sub dgLeasingCompany_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgLeasingCompany.PageIndexChanged
        dgLeasingCompany.CurrentPageIndex = e.NewPageIndex
        BindResult(dgLeasingCompany.CurrentPageIndex)
    End Sub

    Private Sub dgLeasingCompany_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgLeasingCompany.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgLeasingCompany.SelectedIndex = -1
        dgLeasingCompany.CurrentPageIndex = 0
        BindResult(dgLeasingCompany.CurrentPageIndex)
    End Sub

    Private Sub dgLeasingCompany_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgLeasingCompany.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
    End Sub

End Class
