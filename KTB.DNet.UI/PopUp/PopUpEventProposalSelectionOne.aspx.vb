#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade

#End Region

Public Class PopUpEventProposalSelectionOne
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgEventProposalSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtEventRegNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProposalEventName As System.Web.UI.WebControls.TextBox
    Protected WithEvents icEventDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEventDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents chkTanggal As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " custom Declaration "
    Private objDealer As Dealer
#End Region

#Region " Custom Method "

    Private Sub ClearData()
        Me.txtEventRegNumber.Text = String.Empty
        Me.txtProposalEventName.text = String.Empty
        Me.icEventDateFrom.Value = Date.Now
        Me.icEventDateTo.Value = Date.Now
        chkTanggal.Checked = False
        dtgEventProposalSelection.DataSource = New ArrayList
        dtgEventProposalSelection.DataBind()
    End Sub

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            'Added by Ery for sorting
            ViewState.Add("SortColEventProposal", "EventRegNumber")
            ViewState.Add("SortDirEventProposal", Sort.SortDirection.ASC)

            objDealer = Session("DEALER")
            ClearData()
        End If
    End Sub

    Public Sub BindSearch()
        objDealer = Session("DEALER")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not txtEventRegNumber.Text.Trim = "" Then
            criterias.opAnd(New Criteria(GetType(BabitEventProposalHeader), "EventRegNumber", MatchType.[Partial], txtEventRegNumber.Text))
        End If
        If Not txtProposalEventName.Text.Trim = "" Then
            criterias.opAnd(New Criteria(GetType(BabitEventProposalHeader), "EventProposalName", MatchType.[Partial], txtProposalEventName.Text))
        End If

        If chkTanggal.Checked = True Then
            criterias.opAnd(New Criteria(GetType(BabitEventProposalHeader), "PeriodStart", MatchType.GreaterOrEqual, icEventDateFrom.Value))
            criterias.opAnd(New Criteria(GetType(BabitEventProposalHeader), "PeriodEnd", MatchType.Lesser, icEventDateTo.Value.AddDays(1)))
        End If
        criterias.opAnd(New Criteria(GetType(BabitEventProposalHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))

        Dim strSQL As String = String.Empty
        strSQL = "Select Distinct BabitEventProposalHeaderID from BabitEventReportHeader "
        strSQL += "Where RowStatus = 0 and Status <> 1"
        criterias.opAnd(New Criteria(GetType(BabitEventProposalHeader), "ID", MatchType.NotInSet, "(" & strSQL & ")"))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitEventProposalHeader), ViewState("SortColEventProposal").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("SortDirEventProposal").ToString())))

        Dim arrGrid As ArrayList = New BabitEventProposalHeaderFacade(User).RetrieveActiveList(criterias, sortColl)
        If IsNothing(arrGrid) Then arrGrid = New ArrayList
        dtgEventProposalSelection.DataSource = arrGrid
        dtgEventProposalSelection.DataBind()
    End Sub

    Private Sub dtgEventProposalSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEventProposalSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As BabitEventProposalHeader = CType(e.Item.DataItem, BabitEventProposalHeader)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()
        If dtgEventProposalSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

#End Region

    Private Sub dtgEventProposalSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEventProposalSelection.SortCommand
        If e.SortExpression = ViewState("SortColEventProposal") Then
            If ViewState("SortDirEventProposal") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirEventProposal", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirEventProposal", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColEventProposal", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgEventProposalSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEventProposalSelection.PageIndexChanged
        dtgEventProposalSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub

End Class
