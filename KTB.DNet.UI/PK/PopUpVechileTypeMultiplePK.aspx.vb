#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
#End Region

Public Class PopUpVechileTypeMultiplePK
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

    Private Sub ClearData()
        Me.txtVechileTypeCode.Text = String.Empty
        Me.txtDescription.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "VechileTypeCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer, Optional ByVal isSearch As Boolean = False)
        Dim totalRow As Integer = 0

        dtgVechileType.DataSource = New VechileTypeFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
            dtgVechileType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgVechileType.VirtualItemCount = totalRow
        dtgVechileType.DataBind()

        If totalRow > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            If isSearch Then
                MessageBox.Show("Data tidak ditemukan")
            End If
        End If
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtVechileTypeCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.[Partial], txtVechileTypeCode.Text))
        End If
        If txtDescription.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileType), "Description", MatchType.[Partial], txtDescription.Text))
        End If
        If txtCategory.Text.Trim <> "" AndAlso txtCategory.Text.Trim > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.ID", MatchType.Exact, txtCategory.Text.Trim))
        End If
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        If companyCode.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileType), "ProductCategory.Code", MatchType.Exact, companyCode.ToUpper.Trim))
        End If

        If Not IsNothing(Request.QueryString("IsActive")) Then
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, Request.QueryString("IsActive")))
        End If

        If txtCategory.Text.Trim <> "" And txtSubCategory.Text.Trim <> "" Then
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & txtSubCategory.Text.Trim
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        Return criterias
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()

            Dim isCategory As Boolean = False
            If Not IsNothing(Request.QueryString("Category")) Then
                txtCategory.Text = CInt(Request.QueryString("Category"))
                isCategory = True
            End If

            If Not IsNothing(Request.QueryString("SubCategory")) Then
                txtSubCategory.Text = CInt(Request.QueryString("SubCategory"))
                isCategory = True
            End If

            If isCategory Then
                BindDataGrid(0)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0, True)

    End Sub

    Private Sub dtgVechileType_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgVechileType.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=checkbox id='selectAll' name='rbSelectDealer'>")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If

    End Sub

    Private Sub dtgVechileType_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgVechileType.PageIndexChanged
        dtgVechileType.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgVechileType.CurrentPageIndex)
    End Sub

    Private Sub dtgVechileType_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgVechileType.SortCommand
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
        dtgVechileType.CurrentPageIndex = 0
        BindDataGrid(dtgVechileType.CurrentPageIndex)
    End Sub

End Class
