Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search

Public Class PopUpPOConfirmationList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            InitialPage()
        End If

    End Sub

    Private Sub InitialPage()
        ViewState("currSortColumn") = "PoNumber"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        Hidden1.Value = Request.QueryString("Index")
        BindDgSparePart(0)
    End Sub

    Private Sub BindSparePart()
        dtgPO.DataSource = New SparePartForecastHeaderFacade(User).RetrieveActiveList()
        dtgPO.DataBind()
    End Sub

    Private Sub dtgPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPO.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            'e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub

    Private Sub BindDgSparePart(ByVal pageIndeks As Integer)
        Dim totalRow As Integer = 0

        If cfPO.ColumnName = "ALL" Then
            dtgPO.DataSource = New SparePartForecastHeaderFacade(User).RetrieveActiveList(pageIndeks, dtgPO.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        Else
            dtgPO.DataSource = New SparePartForecastHeaderFacade(User).RetrieveWithOneCriteria(pageIndeks, dtgPO.PageSize, totalRow, cfPO.ColumnName, cfPO.OperatorName, cfPO.KeyWord, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), "q")
        End If

        dtgPO.VirtualItemCount = totalRow
        dtgPO.DataBind()
    End Sub

    Private Sub dtgPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPO.PageIndexChanged
        dtgPO.CurrentPageIndex = e.NewPageIndex
        BindDgSparePart(e.NewPageIndex)
    End Sub

    Private Sub cfPO_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfPO.Filter
        dtgPO.CurrentPageIndex = 0
        BindDgSparePart(0)
    End Sub

    Private Sub dtgPO_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPO.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        BindDgSparePart(dtgPO.CurrentPageIndex)
    End Sub

End Class