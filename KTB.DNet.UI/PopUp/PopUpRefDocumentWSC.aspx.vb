Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class PopUpRefDocumentWSC
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents dgWSC As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtClaimNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private arrList As New ArrayList
    Private sesshelper As SessionHelper = New SessionHelper
    Private oDealer As Dealer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'Put user code to initialize the page here
        If Not IsPostBack Then
            Initialize()
            ViewState("CurrentSortColumn") = "CreatedTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            dgWSC.CurrentPageIndex = 0
            BindDataGrid(dgWSC.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgWSC.CurrentPageIndex = 0
        BindDataGrid(0)

    End Sub

    Private Sub dgWSC_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgWSC.PageIndexChanged
        dgWSC.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgWSC.CurrentPageIndex)
    End Sub

    Private Sub dgWSC_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgWSC.SortCommand
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
        BindDataGrid(dgWSC.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        oDealer = CType(sesshelper.GetSession("DEALER"), Dealer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(WSCHeader), "Dealer.ID", MatchType.Exact, oDealer.ID))
        If txtClaimNumber.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimNumber", MatchType.Partial, txtClaimNumber.Text))
        End If
        criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimStatus", MatchType.Exact, "DAPP"))
        arrList = New WSCHeaderFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgWSC.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgWSC.DataSource = arrList
        dgWSC.VirtualItemCount = totalRow
        dgWSC.DataBind()
    End Sub

    Private Sub Initialize()

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
    End Sub

End Class
