Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search

Public Class PopUpPartConfirmationList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            InitialPage()
        End If
    End Sub

    Private Sub InitialPage()
        ViewState("currSortColumn") = "SparePartMaster.PartName"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        Hidden1.Value = Request.QueryString("Index")
        BindDgSparePart(0)
    End Sub

    Private Sub BindSparePart()
        dtgSparePart.DataSource = New SparePartForecastMasterFacade(User).RetrieveActiveList()
        dtgSparePart.DataBind()
    End Sub

    Private Sub dtgSparePart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSparePart.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            'e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub

    Private Sub BindDgSparePart(ByVal pageIndeks As Integer)
        Dim totalRow As Integer = 0

        If cfSparePart.ColumnName = "ALL" Then
            dtgSparePart.DataSource = New SparePartForecastMasterFacade(User).RetrieveActiveList(pageIndeks, dtgSparePart.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        Else
            dtgSparePart.DataSource = New SparePartForecastMasterFacade(User).RetrieveWithOneCriteria(pageIndeks, dtgSparePart.PageSize, totalRow, cfSparePart.ColumnName, cfSparePart.OperatorName, cfSparePart.KeyWord, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), "q")
        End If

        dtgSparePart.VirtualItemCount = totalRow
        dtgSparePart.DataBind()
    End Sub

    Private Sub dtgSparePart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSparePart.PageIndexChanged
        dtgSparePart.CurrentPageIndex = e.NewPageIndex
        BindDgSparePart(e.NewPageIndex)
    End Sub

    Private Sub cfSparePart_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfSparePart.Filter
        dtgSparePart.CurrentPageIndex = 0
        BindDgSparePart(0)
    End Sub

    Private Sub dtgSparePart_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSparePart.SortCommand
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

        BindDgSparePart(dtgSparePart.CurrentPageIndex)
    End Sub
End Class