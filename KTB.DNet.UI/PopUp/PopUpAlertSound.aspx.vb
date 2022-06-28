Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.AlertManagement
Public Class PopUpAlertSound
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgAlertSound As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private objAlertSound As AlertSound = New AlertSound
    Private objAlertSoundFacade As AlertSoundFacade = New AlertSoundFacade(User)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("currentSortColumn") = "FileName"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            BindDataGrid(0)
        End If
    End Sub

    Public Sub BindDataGrid(ByVal IndexPage As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertSound), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AlertSound), "AlertMaster.ID", MatchType.Exact, CInt(Request.QueryString("ID"))))
        _arrList = New AlertSoundFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgAlertSound.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgAlertSound.DataSource = _arrList
        dgAlertSound.VirtualItemCount = totalRow
        dgAlertSound.DataBind()
    End Sub

    Private Sub dgAlertSound_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlertSound.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgAlertSound.CurrentPageIndex * dgAlertSound.PageSize)
            End If
        End If
    End Sub

    Private Sub dgAlertSound_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgAlertSound.PageIndexChanged
        dgAlertSound.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgAlertSound.CurrentPageIndex)
    End Sub

    Private Sub dgAlertSound_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgAlertSound.SortCommand
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

        dgAlertSound.SelectedIndex = -1
        dgAlertSound.CurrentPageIndex = 0
        BindDataGrid(dgAlertSound.CurrentPageIndex)
    End Sub
End Class
