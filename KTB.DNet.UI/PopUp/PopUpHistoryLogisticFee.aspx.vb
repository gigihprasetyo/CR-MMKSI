Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class PopUpHistoryLogisticFee
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglInvoice As System.Web.UI.WebControls.Label
    Protected WithEvents lblDebitMemo As System.Web.UI.WebControls.Label
    Protected WithEvents dgHistoryLogisticFee As System.Web.UI.WebControls.DataGrid

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
        'Put user code to initialize the page herel
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("currentSortColumn") = "CreatedTime"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            BindHeader()
        End If
    End Sub

    Private Sub BindHeader()
        Dim objPF As New LogisticFee
        objPF = New LogisticFeeFacade(User).Retrieve(CInt(Request.QueryString("Id")))
        If Not IsNothing(objPF) Then
            lblDealerCode.Text = objPF.Dealer.DealerCode & " / " & objPF.Dealer.DealerName
            lblTglInvoice.Text = objPF.LogisticDN.BillingDate
            lblDebitMemo.Text = objPF.LogisticDN.DebitMemoNo
            BindDataGrid(0)
        End If
    End Sub

    Public Sub BindDataGrid(ByVal IndexPage As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticFeeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(LogisticFeeHistory), "LogisticFee.ID", MatchType.Exact, CInt(Request.QueryString("Id"))))
        _arrList = New LogisticFeeHistoryFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgHistoryLogisticFee.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgHistoryLogisticFee.DataSource = _arrList
        dgHistoryLogisticFee.VirtualItemCount = totalRow
        dgHistoryLogisticFee.DataBind()
    End Sub

    Private Sub dgHistoryLogisticFee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgHistoryLogisticFee.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgHistoryLogisticFee.CurrentPageIndex * dgHistoryLogisticFee.PageSize)

                Dim objHistory As LogisticFeeHistory = CType(e.Item.DataItem, LogisticFeeHistory)

                Dim lblOldStatus As Label = CType(e.Item.FindControl("lblOldStatus"), Label)
                lblOldStatus.Text = CType(objHistory.OldStatus, EnumLogisticFeeStatus.LogisticFeeStatus).ToString

                Dim lblNewStatus As Label = CType(e.Item.FindControl("lblNewStatus"), Label)
                lblNewStatus.Text = CType(objHistory.NewStatus, EnumLogisticFeeStatus.LogisticFeeStatus).ToString

            End If
        End If
    End Sub

    Private Sub dgHistoryLogisticFee_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgHistoryLogisticFee.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgHistoryLogisticFee.SelectedIndex = -1
        dgHistoryLogisticFee.CurrentPageIndex = 0
        BindDataGrid(dgHistoryLogisticFee.CurrentPageIndex)
    End Sub

    Private Sub dgHistoryLogisticFee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgHistoryLogisticFee.PageIndexChanged
        dgHistoryLogisticFee.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgHistoryLogisticFee.CurrentPageIndex)
    End Sub
End Class
