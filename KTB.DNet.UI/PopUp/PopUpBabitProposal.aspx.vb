Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.BabitSalesComm


Public Class PopUpBabitProposal
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgBabitProposal As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtNoPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoPersetujuan As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected countChk As Integer = 0
    Private objDealer As Dealer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        objDealer = CType(Session("DEALER"), Dealer)
        If Not IsPostBack Then
            ClearData()
        End If
    End Sub

    Private Sub ClearData()
        txtNoPengajuan.Text = String.Empty
        txtNoPersetujuan.Text = String.Empty
    End Sub

    Public Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitProposal), "Dealer.Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        
        'modif for dealer criteria  by ronny
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            criterias.opAnd(New Criteria(GetType(BabitProposal), "Dealer.ID", MatchType.Exact, CInt(objDealer.ID)))
        End If
        '--end modif

        If Not txtNoPengajuan.Text = "" Then
            criterias.opAnd(New Criteria(GetType(BabitProposal), "NoPengajuan", MatchType.[Partial], txtNoPengajuan.Text.Trim()))
        End If

        If Not txtNoPersetujuan.Text = "" Then
            criterias.opAnd(New Criteria(GetType(BabitProposal), "NoPersetujuan", MatchType.[Partial], txtNoPersetujuan.Text.Trim()))
        End If
        criterias.opAnd(New Criteria(GetType(BabitProposal), "NoPersetujuan", MatchType.No, String.Empty))

        dtgBabitProposal.DataSource = New BabitProposalFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dtgBabitProposal.PageSize, totalRow, _
                CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgBabitProposal.VirtualItemCount = totalRow
        dtgBabitProposal.DataBind()
    End Sub

    Private Sub dtgBabitProposal_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBabitProposal.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objBabitPayment As BabitProposal = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgBabitProposal.CurrentPageIndex * dtgBabitProposal.PageSize)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
        If dtgBabitProposal.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgBabitProposal_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgBabitProposal.SortCommand
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
        dtgBabitProposal.SelectedIndex = -1
        dtgBabitProposal.CurrentPageIndex = 0
        BindDataGrid(dtgBabitProposal.CurrentPageIndex)
    End Sub

    Private Sub dtgBabitProposal_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgBabitProposal.PageIndexChanged
        dtgBabitProposal.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgBabitProposal.CurrentPageIndex)
    End Sub

    Private Sub dtgBabitProposal_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgBabitProposal.ItemCommand

    End Sub
End Class
