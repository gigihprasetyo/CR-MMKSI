#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
#End Region

Public Class PopUpPartshop
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPartshop As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox

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
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub ClearData()
        Me.txtCode.Text = String.Empty
        Me.txtName.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PartShopCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
        dtgPartshop.CurrentPageIndex = 0
        BindDataGrid(dtgPartshop.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arr As New ArrayList

        Try
            arr = New PartShopFacade(User).RetrieveByCriteria(CriteriaSearch(), indexPage + 1, _
                    dtgPartshop.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgPartshop.DataSource = arr
            dtgPartshop.VirtualItemCount = totalRow
            dtgPartshop.DataBind()

        Catch ex As HttpException
            dtgPartshop.CurrentPageIndex = 0
            arr = New PartShopFacade(User).RetrieveByCriteria(CriteriaSearch(), dtgPartshop.CurrentPageIndex + 1, _
                    dtgPartshop.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgPartshop.DataSource = arr
            dtgPartshop.VirtualItemCount = totalRow
            dtgPartshop.DataBind()

        End Try

        If dtgPartshop.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Public Function CriteriaSearch() As CriteriaComposite

        Dim criterias As New CriteriaComposite(New Criteria(GetType(PartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PartShop), "Status", MatchType.Exact, CType(EnumPartShopStatus.PartShopStatus.Aktif, Short)))
        If txtCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(PartShop), "PartShopCode", MatchType.StartsWith, txtCode.Text))
        End If
        If txtName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(PartShop), "Name", MatchType.[Partial], txtName.Text))
        End If
        criterias.opAnd(New Criteria(GetType(PartShop), "PartShopCode", MatchType.IsNotNull, Nothing))
        'criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "Status", MatchType.Exact, CType(EnumJobPositionDescription.JobPosition.Aktive , Integer)))
        Return criterias
    End Function

    Private Sub dtgPartshop_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPartshop.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
        End If
    End Sub

    Private Sub dtgPartshop_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPartshop.PageIndexChanged
        dtgPartshop.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPartshop.CurrentPageIndex)
    End Sub

    Private Sub dtgPartshop_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPartshop.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        dtgPartshop.CurrentPageIndex = 0
        BindDataGrid(dtgPartshop.CurrentPageIndex)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
    End Sub
End Class
