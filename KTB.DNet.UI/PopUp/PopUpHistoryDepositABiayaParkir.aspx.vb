Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class PopUpHistoryDepositABiayaParkir
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgHistoryKuitansi As System.Web.UI.WebControls.DataGrid
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
        Dim objPFRH As New ParkingFeeReturnHeader
        objPFRH = New ParkingFeeReturnHeaderFacade(User).Retrieve(CInt(Request.QueryString("Id")))
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
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ParkingFeeHistory), "ParkingFeeReturnHeader.ID", MatchType.Exact, CInt(Request.QueryString("Id"))))
        _arrList = New ParkingFeeHistoryFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgHistoryKuitansi.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgHistoryKuitansi.DataSource = _arrList
        dgHistoryKuitansi.VirtualItemCount = totalRow
        dgHistoryKuitansi.DataBind()
    End Sub


    Private Sub dgHistoryKuitansi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgHistoryKuitansi.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgHistoryKuitansi.CurrentPageIndex * dgHistoryKuitansi.PageSize)

                Dim objHistory As ParkingFeeHistory = CType(e.Item.DataItem, ParkingFeeHistory)

                Dim lblOldStatus As Label = CType(e.Item.FindControl("lblOldStatus"), Label)
                lblOldStatus.Text = CType(objHistory.OldStatus, EnumPengembalianPPhStatus.PengembalianPPhStatus).ToString

                Dim lblNewStatus As Label = CType(e.Item.FindControl("lblNewStatus"), Label)
                lblNewStatus.Text = CType(objHistory.NewStatus, EnumPengembalianPPhStatus.PengembalianPPhStatus).ToString
                
            End If
        End If

    End Sub

    Private Sub dgHistoryKuitansi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgHistoryKuitansi.PageIndexChanged
        dgHistoryKuitansi.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgHistoryKuitansi.CurrentPageIndex)
    End Sub

    Private Sub dgHistoryKuitansi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgHistoryKuitansi.SortCommand
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

        dgHistoryKuitansi.SelectedIndex = -1
        dgHistoryKuitansi.CurrentPageIndex = 0
        BindDataGrid(dgHistoryKuitansi.CurrentPageIndex)
    End Sub
End Class
