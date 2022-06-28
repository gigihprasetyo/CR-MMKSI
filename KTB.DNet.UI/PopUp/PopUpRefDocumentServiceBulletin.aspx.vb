Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class PopUpRefDocumentServiceBulletin
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents dgBulletin As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtPQRNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtRecallRegNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBuletinNumber As TextBox
    Protected WithEvents txtBuletinDescription As System.Web.UI.WebControls.TextBox

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'Put user code to initialize the page here
        If Not IsPostBack Then
            Initialize()
            ViewState("CurrentSortColumn") = "CreatedTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            dgBulletin.CurrentPageIndex = 0
            BindDataGrid(dgBulletin.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgBulletin.CurrentPageIndex = 0
        BindDataGrid(dgBulletin.CurrentPageIndex)

    End Sub

    Private Sub dgBulletin_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgBulletin.PageIndexChanged
        dgBulletin.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgBulletin.CurrentPageIndex)
    End Sub

    Private Sub dgBulletin_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgBulletin.SortCommand
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
        BindDataGrid(dgBulletin.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtRecallRegNo.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(RecallCategory), "RecallRegNo", MatchType.Partial, txtRecallRegNo.Text))
        End If
        If txtBuletinNumber.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(RecallCategory), "BuletinDescription", MatchType.Partial, txtBuletinNumber.Text))
        End If
        If txtBuletinDescription.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(RecallCategory), "Description", MatchType.Partial, txtBuletinDescription.Text))
        End If
        criterias.opAnd(New Criteria(GetType(RecallCategory), "ValidStartDate", MatchType.LesserOrEqual, DateTime.Now.Date))

        arrList = New RecallCategoryFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgBulletin.PageSize, totalRow, _
                        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgBulletin.DataSource = arrList
        dgBulletin.VirtualItemCount = totalRow
        dgBulletin.DataBind()
    End Sub

    Private Sub Initialize()

        ViewState("CurrentSortColumn") = "BuletinDescription"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
    End Sub

End Class
