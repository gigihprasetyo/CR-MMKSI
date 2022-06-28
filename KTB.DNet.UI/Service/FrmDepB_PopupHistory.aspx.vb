
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service

Public Class FrmDepB_PopupHistory
    Inherits System.Web.UI.Page

    Const DocTypePengajuan = 1
    Const DocTypeKuitansi = 2

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("currentSortColumn") = "CreatedTime"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            BindDataGrid(0)
        End If
    End Sub

    Public Sub BindDataGrid(ByVal IndexPage As Integer)

        Dim statusType As String = Request.QueryString("statusType")
        Dim id As String = Request.QueryString("id")

        Dim _arrList As New ArrayList
        Dim totalRow As Integer

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DepositBStatusHistory), "StatusType", MatchType.Exact, CInt(statusType)))
        Select Case CInt(statusType)
            Case DepositBEnum.StatusType.Pencairan
                criterias.opAnd(New Criteria(GetType(DepositBStatusHistory), "DepositBPencairanHeader.ID", MatchType.Exact, CInt(id)))
            Case DepositBEnum.StatusType.DebitNote
                criterias.opAnd(New Criteria(GetType(DepositBStatusHistory), "DepositBDebitNote.ID", MatchType.Exact, CInt(id)))
            Case DepositBEnum.StatusType.Interest
                criterias.opAnd(New Criteria(GetType(DepositBStatusHistory), "DepositBInterestHeader.ID", MatchType.Exact, CInt(id)))
            Case DepositBEnum.StatusType.Kewajiban
                criterias.opAnd(New Criteria(GetType(DepositBStatusHistory), "DepositBKewajibanHeader.ID", MatchType.Exact, CInt(id)))
        End Select
        _arrList = New DepositBStatusHistoryFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgHistoryPencairan.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))

        dgHistoryPencairan.DataSource = _arrList
        dgHistoryPencairan.VirtualItemCount = totalRow
        dgHistoryPencairan.DataBind()
    End Sub

    Private Sub dgHistoryPencairan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgHistoryPencairan.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgHistoryPencairan.CurrentPageIndex * dgHistoryPencairan.PageSize)

                Dim objHistoryPengajuan As DepositBStatusHistory = CType(e.Item.DataItem, DepositBStatusHistory)

                Dim lblOldStatus As Label = CType(e.Item.FindControl("lblOldStatus"), Label)
                lblOldStatus.Text = DepositBEnum.GetStringValueStatusPencairan(objHistoryPengajuan.OldStatus)
                Dim lblNewStatus As Label = CType(e.Item.FindControl("lblNewStatus"), Label)
                lblNewStatus.Text = DepositBEnum.GetStringValueStatusPencairan(objHistoryPengajuan.NewStatus)
                If objHistoryPengajuan.NewStatus = 0 Then
                    lblOldStatus.Text = String.Empty
                End If
            End If
        End If

    End Sub

    Private Sub dgHistoryPencairan_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgHistoryPencairan.PageIndexChanged
        dgHistoryPencairan.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgHistoryPencairan.CurrentPageIndex)
    End Sub

    Private Sub dgHistoryPencairan_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgHistoryPencairan.SortCommand
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

        dgHistoryPencairan.SelectedIndex = -1
        dgHistoryPencairan.CurrentPageIndex = 0
        BindDataGrid(dgHistoryPencairan.CurrentPageIndex)
    End Sub

End Class
