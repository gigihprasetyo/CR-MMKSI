Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.General
Public Class PopUpStockMovementDetail
    Inherits System.Web.UI.Page

    Private sessHelper As New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblChassisNo As System.Web.UI.WebControls.Label
    Protected WithEvents dgStockMovement As System.Web.UI.WebControls.DataGrid

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
            ViewState("CurrentSortColumn") = "ChassisMaster.ChassisNumber"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            BindData(0)
        End If
    End Sub
    Private Sub BindData(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim ID As Integer = Request.QueryString("ID")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StockMovement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StockMovement), "ChassisMaster.ID", MatchType.Exact, ID))
        Dim _arrStock As ArrayList = New StockMovementFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgStockMovement.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        '--Modify by Ronny
        If _arrStock.Count > 0 Then
            sessHelper.SetSession("listStockMovementDetail", _arrStock)
        Else
            sessHelper.SetSession("listStockMovementDetail", Nothing)
        End If
        '--End Modify
        dgStockMovement.DataSource = _arrStock
        dgStockMovement.VirtualItemCount = totalRow
        dgStockMovement.DataBind()
    End Sub
    
    Private Sub dgStockMovement_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgStockMovement.SortCommand
        '--Modify By Ronny
        Dim _arrStock As ArrayList
        If Not IsNothing(sessHelper.GetSession("listStockMovementDetail")) Then
            _arrStock = CType(sessHelper.GetSession("listStockMovementDetail"), ArrayList)
        Else
            Return
        End If
        '---End Modify

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

        '--Modify By Ronny
        'dgStockMovement.SelectedIndex = -1
        'dgStockMovement.CurrentPageIndex = 0
        'BindData(dgStockMovement.CurrentPageIndex)
        _arrStock = CommonFunction.PageAndSortArraylist(_arrStock, dgStockMovement.CurrentPageIndex, dgStockMovement.PageSize, GetType(StockMovement), e.SortExpression, viewstate("currentSortDirection"))
        dgStockMovement.DataSource = _arrStock
        dgStockMovement.DataBind()
        '--end Modify
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(StockMovement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            dgStockMovement.DataSource = New StockMovementFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgStockMovement.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgStockMovement.VirtualItemCount = totalRow
            dgStockMovement.DataBind()
        End If

    End Sub

    Private Sub dgStockMovement_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgStockMovement.PageIndexChanged
        dgStockMovement.CurrentPageIndex = e.NewPageIndex
        BindData(dgStockMovement.CurrentPageIndex)
    End Sub

    Private Sub dgStockMovement_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgStockMovement.ItemDataBound
        Dim RowValue As StockMovement = CType(e.Item.DataItem, StockMovement)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblStockDealer As Label = CType(e.Item.FindControl("lblStockDealer"), Label)
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(RowValue.FromDealer)
            If RowValue.FromDealer > 0 Then
                lblStockDealer.Text = objDealer.DealerCode + " - " + objDealer.SearchTerm1
            Else
                lblStockDealer.Text = ""
            End If

            Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)
            lblDescription.Text = CommonFunction.FormatSavedUser(RowValue.ProcessBy, User)


            Dim lblAllocateDealer As Label = CType(e.Item.FindControl("lblAllocateDealer"), Label)
            lblAllocateDealer.Text = RowValue.Dealer.DealerCode + " - " + RowValue.Dealer.SearchTerm1
        End If
    End Sub
End Class

