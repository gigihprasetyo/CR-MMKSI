Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search

Public Class PopUpDealerInSPL
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgDaftarDealer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblNoAplikasi As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim SPLID As Integer
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack() Then
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Dim obj As SPL = New SPLFacade(User).Retrieve(Integer.Parse(Request.QueryString("SPLID")))
            lblNoAplikasi.Text = obj.SPLNumber
            BindData(0)
        End If
    End Sub
    Private Sub BindData(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        SPLID = Integer.Parse(Request.QueryString("SPLID"))


        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPLDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPLDealer), "SPL.ID", MatchType.Exact, SPLID))
        arrList = New SPLDealerFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgDaftarDealer.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgDaftarDealer.DataSource = arrList
        dtgDaftarDealer.VirtualItemCount = totalRow
        dtgDaftarDealer.DataBind()
    End Sub
    Private Sub dtgDaftarDealer_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDaftarDealer.PageIndexChanged
        dtgDaftarDealer.CurrentPageIndex = e.NewPageIndex
        BindData(dtgDaftarDealer.CurrentPageIndex)
    End Sub
    Private Sub dtgDaftarDealer_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDaftarDealer.SortCommand
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

        dtgDaftarDealer.SelectedIndex = -1
        dtgDaftarDealer.CurrentPageIndex = 0
        bindGridSorting(dtgDaftarDealer.CurrentPageIndex)
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        SPLID = Integer.Parse(Request.QueryString("SPLID"))
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SPLDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPLDealer), "SPL.ID", MatchType.Exact, SPLID))
            dtgDaftarDealer.DataSource = New SPLDealerFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgDaftarDealer.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dtgDaftarDealer.VirtualItemCount = totalRow
            dtgDaftarDealer.DataBind()
        End If

    End Sub

    Private Sub dtgDaftarDealer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDaftarDealer.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgDaftarDealer.CurrentPageIndex * dtgDaftarDealer.PageSize)
        End If
    End Sub
End Class
