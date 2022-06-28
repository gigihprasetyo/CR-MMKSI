Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class PopUpHistoryLogisticFeeReturn
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgHistoryLogisticFeeReturn As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRegBuktiPotong As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoBuktiPotong As System.Web.UI.WebControls.Label

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
        If Not IsPostBack Then
            ViewState("currentSortColumn") = "CreatedTime"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            BindHeader()
        End If
    End Sub

    Private Sub BindHeader()
        Dim objPFRH As New LogisticPPHHeader
        objPFRH = New LogisticPPHHeaderFacade(User).Retrieve(CInt(Request.QueryString("Id")))
        If Not IsNothing(objPFRH) Then
            lblDealer.Text = objPFRH.Dealer.DealerCode & " / " & objPFRH.Dealer.DealerName
            lblNoBuktiPotong.Text = objPFRH.BuktiPotongNumber
            lblNoRegBuktiPotong.Text = objPFRH.NoReg
            BindDataGrid(0)
        End If
    End Sub

    Public Sub BindDataGrid(ByVal IndexPage As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticFeeReturnHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(LogisticFeeReturnHistory), "LogisticPPHHeader.ID", MatchType.Exact, CInt(Request.QueryString("Id"))))
        _arrList = New LogisticFeeReturnHistoryFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgHistoryLogisticFeeReturn.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgHistoryLogisticFeeReturn.DataSource = _arrList
        dgHistoryLogisticFeeReturn.VirtualItemCount = totalRow
        dgHistoryLogisticFeeReturn.DataBind()
    End Sub


    Private Sub dgHistoryLogisticFeeReturn_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgHistoryLogisticFeeReturn.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgHistoryLogisticFeeReturn.CurrentPageIndex * dgHistoryLogisticFeeReturn.PageSize)

                Dim objHistory As LogisticFeeReturnHistory = CType(e.Item.DataItem, LogisticFeeReturnHistory)

                Dim lblOldStatus As Label = CType(e.Item.FindControl("lblOldStatus"), Label)
                lblOldStatus.Text = CType(objHistory.OldStatus, EnumPengembalianLogisticPPHStatus.PengembalianPPhStatus).ToString

                Dim lblNewStatus As Label = CType(e.Item.FindControl("lblNewStatus"), Label)
                lblNewStatus.Text = CType(objHistory.NewStatus, EnumPengembalianLogisticPPHStatus.PengembalianPPhStatus).ToString

            End If
        End If

    End Sub

    Private Sub dgHistoryLogisticFeeReturn_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgHistoryLogisticFeeReturn.PageIndexChanged
        dgHistoryLogisticFeeReturn.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgHistoryLogisticFeeReturn.CurrentPageIndex)
    End Sub

    Private Sub dgHistoryLogisticFeeReturn_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgHistoryLogisticFeeReturn.SortCommand
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

        dgHistoryLogisticFeeReturn.SelectedIndex = -1
        dgHistoryLogisticFeeReturn.CurrentPageIndex = 0
        BindDataGrid(dgHistoryLogisticFeeReturn.CurrentPageIndex)
    End Sub
End Class
