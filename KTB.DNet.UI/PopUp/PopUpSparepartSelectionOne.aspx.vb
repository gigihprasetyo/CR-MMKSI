Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search

Public Class PopUpSparepartSelectionOne
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"
    Private _vstIsAccessories As String = "_vstIsAccessories"
    Private _vstPQRHeaderID As String = "_vstPQRHeaderID"
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            InitialPage()
        End If
    End Sub
    Private Sub InitialPage()
        'If Not IsNothing(Request.Item("IsAccessories")) Then
        '    Me.ViewState.Add(Me._vstIsAccessories, Request.Item("IsAccessories"))
        'Else
        '    Me.ViewState.Add(Me._vstIsAccessories, "0")
        'End If
        'If Not IsNothing(Request.Item("PQRHeaderID")) Then
        '    If Request.Item("PQRHeaderID") = "" Then
        '        Me.ViewState.Add(Me._vstPQRHeaderID, "0")
        '    Else
        '        Me.ViewState.Add(Me._vstPQRHeaderID, Request.Item("PQRHeaderID"))
        '    End If
        'Else
        '    Me.ViewState.Add(Me._vstPQRHeaderID, "0")
        'End If
        ViewState("currSortColumn") = "PartName"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        Hidden1.Value = Request.QueryString("Index")
        BindDgSparePart(1)
    End Sub

    Private Sub BindSparePart()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        dtgSparePart.DataSource = New SparePartMasterFacade(User).RetrieveActiveList(companyCode)
        dtgSparePart.DataBind()
    End Sub


    Private Sub dtgSparePart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSparePart.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objPart As SparePartMaster = CType(e.Item.DataItem, SparePartMaster)
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
            'Dim lblProduct As Label = CType(e.Item.FindControl("lblProduct"), Label)
            'If Not IsNothing(objPart.ProductCategory) Then
            '    lblProduct.Text = objPart.ProductCategory.Code
            'End If

        End If
    End Sub

    Private Sub BindDgSparePart(ByVal pageIndeks As Integer)
        Dim totalRow As Integer = 0
        Dim TypeCode As String = String.Empty

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtNoSparepart.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Partial, txtNoSparepart.Text))
        End If
        If txtNamaSparepart.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartName", MatchType.Partial, txtNamaSparepart.Text))
        End If
        criterias.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))

        Dim arlSparePartMaster As ArrayList = New SparePartMasterFacade(User).RetrieveActiveList(pageIndeks + 1, dtgSparePart.PageSize, totalRow, CType(ViewState("SortColumn"), String), CType(ViewState("SortDirection"), Sort.SortDirection), criterias)
        dtgSparePart.VirtualItemCount = totalRow
        If arlSparePartMaster.Count > 0 Then
            dtgSparePart.DataSource = arlSparePartMaster
        Else
            dtgSparePart.DataSource = New ArrayList
        End If

        dtgSparePart.VirtualItemCount = totalRow

        If pageIndeks = 0 Then
            dtgSparePart.CurrentPageIndex = 0
        End If

        dtgSparePart.DataBind()

        'dtgSparePart.DataSource = New SparePartMasterFacade(User).RetrieveActiveList(pageIndeks, dtgSparePart.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), TypeCode)
        'dtgSparePart.VirtualItemCount = totalRow
        'dtgSparePart.DataBind()
    End Sub



    Private Sub dtgSparePart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSparePart.PageIndexChanged
        dtgSparePart.CurrentPageIndex = e.NewPageIndex
        BindDgSparePart(e.NewPageIndex + 1)
    End Sub

    'Private Sub cfSparePart_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfSparePart.Filter
    '    dtgSparePart.CurrentPageIndex = 0
    '    BindDgSparePart(1)

    'End Sub

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

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        dtgSparePart.CurrentPageIndex = 0
        BindDgSparePart(dtgSparePart.CurrentPageIndex)
    End Sub
End Class