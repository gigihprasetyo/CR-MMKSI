
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search


Public Class PopUpSparePartSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgSparePart As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cfSparePart As FilterCompositeControl.CompositeFilter
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
        ViewState("currSortColumn") = "PartName"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        Hidden1.Value = Request.QueryString("Index")
        BindDgSparePart(1)
    End Sub

    Private Sub BindSparePart()
        dtgSparePart.DataSource = New SparePartMasterFacade(User).RetrieveActiveList()
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
        Dim TypeCode As String = String.Empty

        If Not IsNothing(Request.QueryString("IPMaterialtype")) Then
            If (CType(Request.QueryString("IPMaterialtype"), Integer) = CType(EnumMaterialType.MaterialType.Parts, Integer)) Then
                TypeCode = "I"
            ElseIf (CType(Request.QueryString("IPMaterialtype"), Integer) = CType(EnumMaterialType.MaterialType.Tools, Integer)) _
            OrElse (CType(Request.QueryString("IPMaterialtype"), Integer) = CType(EnumMaterialType.MaterialType.Equipment, Integer)) Then
                TypeCode = "E"
            ElseIf (CType(Request.QueryString("IPMaterialtype"), Integer) = CType(EnumMaterialType.MaterialType.Accessories, Integer)) Then
                TypeCode = "A"
            End If
            If cfSparePart.ColumnName = "ALL" Then
                dtgSparePart.DataSource = New SparePartMasterFacade(User).RetrieveActiveList(pageIndeks, dtgSparePart.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), TypeCode)
            Else
                dtgSparePart.DataSource = New SparePartMasterFacade(User).RetrieveWithOneCriteria(pageIndeks, dtgSparePart.PageSize, totalRow, cfSparePart.ColumnName, cfSparePart.OperatorName, cfSparePart.KeyWord, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), TypeCode)
            End If
        Else
            If cfSparePart.ColumnName = "ALL" Then
                dtgSparePart.DataSource = New SparePartMasterFacade(User).RetrieveActiveListNonIndent(pageIndeks, dtgSparePart.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
            Else
                dtgSparePart.DataSource = New SparePartMasterFacade(User).RetrieveWithOneCriteriaNonIndent(pageIndeks, dtgSparePart.PageSize, totalRow, cfSparePart.ColumnName, cfSparePart.OperatorName, cfSparePart.KeyWord, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
            End If
        End If

        dtgSparePart.VirtualItemCount = totalRow
        dtgSparePart.DataBind()
    End Sub



    Private Sub dtgSparePart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSparePart.PageIndexChanged
        dtgSparePart.CurrentPageIndex = e.NewPageIndex
        BindDgSparePart(e.NewPageIndex + 1)
    End Sub

    Private Sub cfSparePart_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfSparePart.Filter
        dtgSparePart.CurrentPageIndex = 0
        BindDgSparePart(1)

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

    Private Sub dtgSparePart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgSparePart.SelectedIndexChanged

    End Sub
End Class
