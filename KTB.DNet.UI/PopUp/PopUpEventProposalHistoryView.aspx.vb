Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility

Public Class PopUpEventProposalHistoryView
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents dtgHistoryAgreement As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgHistory As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private ReadOnly Property GetID() As Int32
        Get
            Return Request.QueryString("id")
        End Get
    End Property

    Private ReadOnly Property Mode() As String
        Get
            Return Request.QueryString("mode")
        End Get
    End Property

    Private Sub BindGridPlace()
        Dim objFacade As New EventProposalHistoryFacade(User)
        Dim crit As New CriteriaComposite(New Criteria(GetType(EventProposalHistory), "RowStatus", CType(DBRowStatus.Active, Short)))
        Dim totalRows As Integer = 0
        crit.opAnd(New Criteria(GetType(EventProposalHistory), "EventProposal", GetID))
        dtgHistory.DataSource = objFacade.RetrieveActiveList(crit, dtgHistory.CurrentPageIndex + 1, _
            dtgHistory.PageSize, totalRows, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
        dtgHistory.DataBind()
    End Sub

    Private Sub BindGridAgree()
        Dim objFacade As New EventProposalHistoryAgreementFacade(User)
        Dim crit As New CriteriaComposite(New Criteria(GetType(EventProposalHistoryAgreement), "RowStatus", _
            CType(DBRowStatus.Active, Short)))
        Dim totalRows As Integer = 0
        crit.opAnd(New Criteria(GetType(EventProposalHistoryAgreement), "EventProposal", GetID))
        dtgHistoryAgreement.DataSource = objFacade.RetrieveActiveList(crit, dtgHistoryAgreement.CurrentPageIndex + 1, _
            dtgHistoryAgreement.PageSize, totalRows, viewstate("CurrentSortColumn"), viewstate("CurrentSortDirect"))
        dtgHistoryAgreement.DataBind()
    End Sub

    Private Sub BindGrid()
        Select Case Mode
            Case "Place"
                BindGridPlace()
                dtgHistoryAgreement.Visible = False
            Case "Agree"
                BindGridAgree()
                dtgHistory.Visible = False
        End Select
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            ViewState("CurrentSortColumn") = "ID"
            BindGrid()
        End If
    End Sub

    Private Sub dtgHistory_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
        Handles dtgHistory.ItemDataBound, dtgHistoryAgreement.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dtgHistory.PageSize * dtgHistory.CurrentPageIndex)).ToString
        End If
    End Sub

    Private Sub dtgHistory_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) _
        Handles dtgHistory.SortCommand, dtgHistoryAgreement.SortCommand
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
        BindGrid()
    End Sub

    Private Sub dtgHistory_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) _
        Handles dtgHistory.PageIndexChanged, dtgHistoryAgreement.PageIndexChanged
        Dim dtg As DataGrid = source
        dtg.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub
End Class