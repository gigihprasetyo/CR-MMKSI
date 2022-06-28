Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit
Public Class PopUpHistoryKuitansiPencairanDepositA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgHistoryKuitansi As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

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
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim NoSurat As String = Request.QueryString("NoSurat")
        criterias.opAnd(New Criteria(GetType(DepositAStatusHistory), "DocNumber", MatchType.Exact, NoSurat))
        criterias.opAnd(New Criteria(GetType(DepositAStatusHistory), "DocType", MatchType.Exact, DocTypeKuitansi))
        _arrList = New DepositAStatusHistoryFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgHistoryKuitansi.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgHistoryKuitansi.DataSource = _arrList
        dgHistoryKuitansi.VirtualItemCount = totalRow
        dgHistoryKuitansi.DataBind()
    End Sub

    Private Sub dgHistoryPencairan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgHistoryKuitansi.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgHistoryKuitansi.CurrentPageIndex * dgHistoryKuitansi.PageSize)

                Dim objHistoryPengajuan As DepositAStatusHistory = CType(e.Item.DataItem, DepositAStatusHistory)


                Dim lblOldStatus As Label = CType(e.Item.FindControl("lblOldStatus"), Label)
                Dim selectedStatusOLD As EnumDepositA.StatusKuitansi = CType([Enum].Parse(GetType(EnumDepositA.StatusKuitansi), objHistoryPengajuan.OldStatus), EnumDepositA.StatusKuitansi)
                Dim SelectedStatusOLDName As String = selectedStatusOLD.GetName(GetType(EnumDepositA.StatusKuitansi), objHistoryPengajuan.OldStatus)
                lblOldStatus.Text = SelectedStatusOLDName
                Dim lblNewStatus As Label = CType(e.Item.FindControl("lblNewStatus"), Label)
                Dim selectedStatusNew As EnumDepositA.StatusKuitansi = CType([Enum].Parse(GetType(EnumDepositA.StatusKuitansi), objHistoryPengajuan.NewStatus), EnumDepositA.StatusKuitansi)
                Dim SelectedStatusNewName As String = selectedStatusOLD.GetName(GetType(EnumDepositA.StatusKuitansi), objHistoryPengajuan.NewStatus)
                lblNewStatus.Text = SelectedStatusNewName
                If objHistoryPengajuan.NewStatus = 0 Then
                    lblOldStatus.Text = String.Empty
                End If
            End If
        End If

    End Sub

    Private Sub dgHistoryPencairan_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgHistoryKuitansi.PageIndexChanged
        dgHistoryKuitansi.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgHistoryKuitansi.CurrentPageIndex)
    End Sub

    Private Sub dgHistoryPencairan_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgHistoryKuitansi.SortCommand
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

    'Private Enum StatusPencairanALL
    '    Baru = 0
    '    Validasi = 1
    '    Konfirmasi = 10
    '    Printed = 11
    '    Selesai = 12
    '    Hapus = 13
    '    CancelJV = 15
    'End Enum

End Class
