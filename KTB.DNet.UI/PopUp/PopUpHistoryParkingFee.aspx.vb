Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class PopUpHistoryParkingFee
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents lblDebitMemo As System.Web.UI.WebControls.Label
    Protected WithEvents dgHistoryParkingFee As System.Web.UI.WebControls.DataGrid

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
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("currentSortColumn") = "CreatedTime"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            BindHeader()
        End If
    End Sub


    Private Sub BindHeader()
        Dim objPF As New ParkingFee
        objPF = New ParkingFeeFacade(User).Retrieve(CInt(Request.QueryString("Id")))
        Dim category As String = CType(Request.QueryString("Cat"), String)
        If Not IsNothing(objPF) Then
            lblDealerCode.Text = objPF.Dealer.DealerCode & " / " & objPF.Dealer.DealerName
            lblPeriod.Text = EnumParkingFeePeriod.GetStringValue(CType(objPF.Periode, Integer)) & " " & objPF.Year.ToString
            lblCategory.Text = category
            lblDebitMemo.Text = objPF.DebitMemoNumber
            BindDataGrid(0)
        End If
    End Sub

    Public Sub BindDataGrid(ByVal IndexPage As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PenaltyParkirHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PenaltyParkirHistory), "ParkingFee.ID", MatchType.Exact, CInt(Request.QueryString("Id"))))
        _arrList = New PenaltyParkirHistoryFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgHistoryParkingFee.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgHistoryParkingFee.DataSource = _arrList
        dgHistoryParkingFee.VirtualItemCount = totalRow
        dgHistoryParkingFee.DataBind()
    End Sub

    Private Sub dgHistoryParkingFee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgHistoryParkingFee.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgHistoryParkingFee.CurrentPageIndex * dgHistoryParkingFee.PageSize)

                Dim objHistory As PenaltyParkirHistory = CType(e.Item.DataItem, PenaltyParkirHistory)

                Dim lblOldStatus As Label = CType(e.Item.FindControl("lblOldStatus"), Label)
                lblOldStatus.Text = CType(objHistory.OldStatus, EnumParkingFeeStatus.ParkingFeeStatus).ToString

                Dim lblNewStatus As Label = CType(e.Item.FindControl("lblNewStatus"), Label)
                lblNewStatus.Text = CType(objHistory.NewStatus, EnumParkingFeeStatus.ParkingFeeStatus).ToString

            End If
        End If
    End Sub

    Private Sub dgHistoryParkingFee_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgHistoryParkingFee.SortCommand
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

        dgHistoryParkingFee.SelectedIndex = -1
        dgHistoryParkingFee.CurrentPageIndex = 0
        BindDataGrid(dgHistoryParkingFee.CurrentPageIndex)
    End Sub

    Private Sub dgHistoryParkingFee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgHistoryParkingFee.PageIndexChanged
        dgHistoryParkingFee.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgHistoryParkingFee.CurrentPageIndex)
    End Sub
End Class
