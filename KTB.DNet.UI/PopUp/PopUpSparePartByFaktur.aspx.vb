Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility

Public Class PopUpSparePartByFaktur
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
        ViewState("currSortColumn") = "SparePartMaster.PartName"
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
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub

    Private Sub BindDgSparePart(ByVal pageIndeks As Integer)
        Dim total As Integer = 0
        Dim sesHelper As SessionHelper = New SessionHelper


        Dim SPPo As SparePartPOStatus
        If Not IsNothing(Request.QueryString("NoSO")) Then
            SPPo = New SparePartPOStatusFacade(User).RetrievePO(Request.QueryString("NoFaktur"), Request.QueryString("NoSO"))
        Else
            SPPo = New SparePartPOStatusFacade(User).Retrieve(Request.QueryString("NoFaktur"))

        End If



        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

        If Not SPPo Is Nothing Then
            If Not IsNothing(Request.QueryString("NoSO")) Then

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.ID", MatchType.Exact, SPPo.ID.ToString()))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartMaster.ProductCategory.Code", MatchType.Exact, companyCode.Trim.ToUpper()))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartMaster.ActiveStatus", MatchType.Exact, CInt(EnumSparePartActiveStatus.SparePartActiveStatus.Active)))


                Dim SPPoDetails As ArrayList
                If cfSparePart.ColumnName = "ALL" Then
                    SPPoDetails = New KTB.DNet.BusinessFacade.SparePart.SparePartPOStatusDetailFacade(User).RetrieveActiveList(criterias, pageIndeks, dtgSparePart.PageSize, total, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), cfSparePart.ColumnName, cfSparePart.OperatorName, cfSparePart.KeyWord))
                    SPPoDetails = New KTB.DNet.BusinessFacade.SparePart.SparePartPOStatusDetailFacade(User).RetrieveActiveList(criterias, pageIndeks, dtgSparePart.PageSize, total, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
                End If
                dtgSparePart.VirtualItemCount = total
                dtgSparePart.DataSource = SPPoDetails
                dtgSparePart.DataBind()

            Else
                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.ID", MatchType.Exact, SPPo.ID.ToString()))
                'Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.SparePartPO.Dealer.ID", MatchType.Exact, objUser.Dealer.ID.ToString()))


                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPODetail), "SparePartPO.ID", MatchType.Exact, SPPo.ID.ToString()))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPODetail), "SparePartMaster.ProductCategory.Code", MatchType.Exact, companyCode.Trim.ToUpper()))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPODetail), "SparePartMaster.ActiveStatus", MatchType.Exact, CInt(EnumSparePartActiveStatus.SparePartActiveStatus.Active)))
                
                Dim SPPoDetails As ArrayList
                If cfSparePart.ColumnName <> "ALL" Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPODetail), cfSparePart.ColumnName, cfSparePart.OperatorName, cfSparePart.KeyWord))
                End If
                SPPoDetails = New KTB.DNet.BusinessFacade.SparePart.SparePartPODetailFacade(User).RetrieveActiveList(criterias, pageIndeks, dtgSparePart.PageSize, total, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

                dtgSparePart.VirtualItemCount = total
                dtgSparePart.DataSource = SPPoDetails
                dtgSparePart.DataBind()
            End If
          
        Else
            dtgSparePart.DataSource = New ArrayList
            dtgSparePart.DataBind()
        End If

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
