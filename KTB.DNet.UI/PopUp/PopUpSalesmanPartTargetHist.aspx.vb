Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.Salesman

Public Class PopUpSalesmanPartTargetHist
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents dgHistory As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSalesmanCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesmanName As System.Web.UI.WebControls.Label

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
        If Not IsPostBack Then
            ViewState("currentSortColumn") = "CreatedTime"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            BindHeader()
        End If
    End Sub

    Private Sub BindHeader()
        Dim objSalesmanPartTarget As New SalesmanPartTarget
        objSalesmanPartTarget = New SalesmanPartTargetFacade(User).Retrieve(CInt(Request.QueryString("Id")))
        If Not IsNothing(objSalesmanPartTarget) Then
            lblDealerCode.Text = objSalesmanPartTarget.SalesmanHeader.Dealer.DealerCode
            lblSalesmanCode.Text = objSalesmanPartTarget.SalesmanHeader.SalesmanCode
            lblSalesmanName.Text = objSalesmanPartTarget.SalesmanHeader.Name
            BindDataGrid(0)
        End If
    End Sub

    Public Sub BindDataGrid(ByVal IndexPage As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanPartTargetHist), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanPartTargetHist), "SalesmanPartTarget.ID", MatchType.Exact, CInt(Request.QueryString("Id"))))
        _arrList = New SalesmanPartTargetHistFacade(User).RetrieveByCriteria(criterias, IndexPage + 1, dgHistory.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgHistory.DataSource = _arrList
        dgHistory.VirtualItemCount = totalRow
        dgHistory.DataBind()
    End Sub

    Private Sub dgHistory_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgHistory.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgHistory.CurrentPageIndex * dgHistory.PageSize)
            End If
        End If
    End Sub

    Private Sub dgHistory_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgHistory.SortCommand
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

        dgHistory.SelectedIndex = -1
        dgHistory.CurrentPageIndex = 0
        BindDataGrid(dgHistory.CurrentPageIndex)
    End Sub

    Private Sub dgHistory_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgHistory.PageIndexChanged
        dgHistory.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgHistory.CurrentPageIndex)
    End Sub

End Class
