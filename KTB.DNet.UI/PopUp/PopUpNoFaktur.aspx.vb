Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility

Public Class PopUpNoFaktur
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cfSparePart As FilterCompositeControl.CompositeFilter
    Protected WithEvents dtgSparePart As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            InitialPage()
        End If
    End Sub

    Private Sub InitialPage()
        ViewState("currSortColumn") = "BillingNumber"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        Hidden1.Value = Request.QueryString("Index")
        BindDgSparePart(DBRowStatus.Active)
    End Sub

    Private Sub dtgSparePart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSparePart.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim spPO As SparePartPO = CType(e.Item.DataItem, SparePartPO)
            'Dim poDetails As SparePartPODetail = spPO.SparePartPODetails(0)
            'Dim lblNoBarang As Label = CType(e.Item.FindControl("lblNoBarang"), Label)
            'Dim lblNamaBarang As Label = CType(e.Item.FindControl("lblNamaBarang"), Label)
            'Dim lblHargaSatuan As Label = CType(e.Item.FindControl("lblHargaSatuan"), Label)
            'lblNoBarang.Text = poDetails.SparePartMaster.PartNumber.ToString()
            'lblNamaBarang.Text = poDetails.SparePartMaster.PartName.ToString()
            'lblHargaSatuan.Text = Format(poDetails.RetailPrice, "#,##0")

            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub

    Private Sub BindDgSparePart(ByVal pageIndeks As Integer)
        Dim sessHelp As SessionHelper = New SessionHelper
        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)

        Dim totalRow As Integer = 0
        Try
            If cfSparePart.ColumnName = "ALL" Then
                If Val(Request.QueryString("claim")) = 1 Then
                    dtgSparePart.DataSource = New SparePartPOStatusFacade(User).RetrieveActiveListPerDealerClaim(objUserInfo.Dealer.ID, pageIndeks, dtgSparePart.PageSize, totalRow, CType(ViewState("currSortColumn"), String), ViewState("currSortDirection"))
                Else
                    dtgSparePart.DataSource = New SparePartPOStatusFacade(User).RetrieveActiveListPerDealer(objUserInfo.Dealer.ID, pageIndeks, dtgSparePart.PageSize, totalRow, CType(ViewState("currSortColumn"), String), ViewState("currSortDirection"))
                End If
            Else
                If Val(Request.QueryString("claim")) = 1 Then
                    dtgSparePart.DataSource = New SparePartPOStatusFacade(User).RetrieveWithOneCriteriaPerDealerClaim(objUserInfo.Dealer.ID, pageIndeks, dtgSparePart.PageSize, totalRow, cfSparePart.ColumnName, cfSparePart.OperatorName, cfSparePart.KeyWord, CType(ViewState("currSortColumn"), String), ViewState("currSortDirection"))
                Else
                    dtgSparePart.DataSource = New SparePartPOStatusFacade(User).RetrieveWithOneCriteriaPerDealer(objUserInfo.Dealer.ID, pageIndeks, dtgSparePart.PageSize, totalRow, cfSparePart.ColumnName, cfSparePart.OperatorName, cfSparePart.KeyWord, CType(ViewState("currSortColumn"), String), ViewState("currSortDirection"))
                End If
            End If

        Catch ex As Exception
            dtgSparePart.DataSource = New ArrayList
        End Try
        dtgSparePart.VirtualItemCount = totalRow
        dtgSparePart.DataBind()
    End Sub

    Private Sub dtgSparePart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSparePart.PageIndexChanged
        dtgSparePart.CurrentPageIndex = e.NewPageIndex
        BindDgSparePart(e.NewPageIndex + 1)
    End Sub

    Private Sub cfSparePart_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfSparePart.Filter
        dtgSparePart.CurrentPageIndex = 0
        BindDgSparePart(DBRowStatus.Active)
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
        BindDgSparePart(dtgSparePart.CurrentPageIndex + 1)
    End Sub
End Class
