#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Utility

#End Region
Public Class PopUpEventDocument
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgEvent As System.Web.UI.WebControls.DataGrid

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
        If Not IsPostBack() Then
            ViewState("CurrentSortColumn") = "EventNo"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            BindData(0)
            If dtgEvent.Items.Count > 0 Then
                btnChoose.Disabled = False
            Else
                btnChoose.Disabled = True
            End If
        End If
    End Sub

    Private Sub BindData(ByVal indexpage As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(EventMaster), "EventNo", Sort.SortDirection.ASC))
        Try
            dtgEvent.DataSource = New EventMasterFacade(User).Retrieve(criterias, sortColl)
        Catch ex As Exception

        End Try
        dtgEvent.DataBind()
        
    End Sub

    Private Sub dtgEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEvent.PageIndexChanged
        dtgEvent.CurrentPageIndex = e.NewPageIndex
        BindData(dtgEvent.CurrentPageIndex)
        
    End Sub

    Private Sub dtgEvent_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEvent.SortCommand
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

        dtgEvent.SelectedIndex = -1
        dtgEvent.CurrentPageIndex = 0
        bindGridSorting(dtgEvent.CurrentPageIndex)
    End Sub

    Private Sub dtgEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEvent.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As EventMaster = CType(e.Item.DataItem, EventMaster)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                Dim lblPeriod As Label = CType(e.Item.FindControl("lblPeriod"), Label)
                lblPeriod.Text = CType(RowValue.StartMonth - 1, enumMonth.Month).ToString.Replace("_", " ") & " " & RowValue.Period & " s.d " & CType(RowValue.EndMonth - 1, enumMonth.Month).ToString.Replace("_", " ") & " " & RowValue.Period

                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(EventMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            dtgEvent.DataSource = New EventMasterFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgEvent.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dtgEvent.VirtualItemCount = totalRow
            dtgEvent.DataBind()
        End If

    End Sub
End Class
