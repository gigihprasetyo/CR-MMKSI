#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
#End Region


Public Class PopUpPartsForecastMaster
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    
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
        Me.txtKodeParts.Text = String.Empty
        Me.txtNmPart.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
        dtgParts.CurrentPageIndex = 0
        BindDataGrid(dtgParts.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrl As New ArrayList
        Try
            arrl = New SparePartForecastMasterFacade(User).RetrieveByCriteria(CriteriaSearch(), indexPage + 1, _
                    dtgParts.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgParts.DataSource = arrl
            dtgParts.VirtualItemCount = totalRow
            dtgParts.DataBind()
        Catch ex As HttpException
            dtgParts.CurrentPageIndex = 0
            arrl = New SparePartForecastMasterFacade(User).RetrieveByCriteria(CriteriaSearch(), dtgParts.CurrentPageIndex + 1, _
                    dtgParts.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgParts.DataSource = arrl
            dtgParts.VirtualItemCount = totalRow
            dtgParts.DataBind()

        End Try

        If dtgParts.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        
        If txtKodeParts.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastMaster), "SparePartMaster.PartNumber", MatchType.[Partial], txtKodeParts.Text))
        End If
        If txtNmPart.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastMaster), "SparePartMaster.PartName", MatchType.[Partial], txtNmPart.Text))
        End If
        
        Return criterias
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgParts.CurrentPageIndex = 0
        BindDataGrid(dtgParts.CurrentPageIndex)
    End Sub

    Private Sub dtgParts_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgParts.ItemDataBound

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As SparePartForecastMaster = CType(e.Item.DataItem, SparePartForecastMaster)
        End If
    End Sub

    Private Sub dtgParts_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgParts.PageIndexChanged
        dtgParts.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgParts.CurrentPageIndex)
    End Sub

    Private Sub dtgParts_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgParts.SortCommand
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
        'dtgParts.CurrentPageIndex = 0
        BindDataGrid(dtgParts.CurrentPageIndex)
    End Sub

    'Add by Reza 12 Maret 2018
    'Req by Miyuki
    Private Function GetProductCategory() As Integer
        Dim _return As Integer
        Dim PCID As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ProductCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ProductCategory), "Code", MatchType.Exact, "MMC"))
        PCID = New KTB.DNet.BusinessFacade.FinishUnit.ProductCategoryFacade(User).RetrieveByCriteria(criterias, 1, 25, 1)
        For Each asd As ProductCategory In PCID
            _return = CType(asd.ID, Integer)
        Next
        Return _return
    End Function

End Class