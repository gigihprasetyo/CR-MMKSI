Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain

Public Class PopUpLeasing
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgCompetitorType As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton

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
        If Not IsPostBack() Then
            ViewState("currentSortColumn") = "Code"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            BindResult(0)
        End If
    End Sub
    Private Sub BindResult(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(CompetitorBrand), "Status", MatchType.Exact, CType(EnumStatusSPL.StatusSPL.Aktif, Short)))
        'criterias.opAnd(New Criteria(GetType(CompetitorBrand), "Code", MatchType.No, "MITSUBISHI"), "(", True)
        'criterias.opOr(New Criteria(GetType(CompetitorBrand), "Code", MatchType.No, "mitsubishi"), ")", False)
        'Dim arrList As ArrayList = New CompetitorBrandFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgCompetitorType.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))

        'Dim arrList As ArrayList = New LeasingCompanyFacade(User).RetrieveActiveList()

        Dim arrList As ArrayList = New LeasingCompanyFacade(User).RetrieveActiveList(indexPage + 1, dgCompetitorType.PageSize, totalRow)
        dgCompetitorType.DataSource = arrList
        dgCompetitorType.VirtualItemCount = totalRow
        dgCompetitorType.DataBind()
        If dgCompetitorType.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dgCompetitorType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgCompetitorType.PageIndexChanged
        dgCompetitorType.CurrentPageIndex = e.NewPageIndex
        BindResult(dgCompetitorType.CurrentPageIndex)
    End Sub

    Private Sub dgCompetitorType_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgCompetitorType.SortCommand
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

        dgCompetitorType.SelectedIndex = -1
        dgCompetitorType.CurrentPageIndex = 0
        BindResult(dgCompetitorType.CurrentPageIndex)
    End Sub

    Private Sub dgCompetitorType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCompetitorType.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

       
    End Sub
End Class
