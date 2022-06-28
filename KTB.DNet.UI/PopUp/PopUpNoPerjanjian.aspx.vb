Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade

Public Class PopUpNoPerjanjian
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtNoPerjanjian As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgNoPerjanjian As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim DealerCode As String
    Dim arl As New ArrayList

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        DealerCode = Request.QueryString("DealerCode")
        If (Not IsPostBack) Then
            ViewState("currSortColumn") = "NoPerjanjian"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
    End Sub

    Private Sub dtgNoPerjanjian_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgNoPerjanjian.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As BabitAllocation = CType(e.Item.DataItem, BabitAllocation)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblPeriode As Label = CType(e.Item.FindControl("lblPeriode"), Label)
                lblPeriode.Text = MonthName(RowValue.Babit.StartPeriod, False) & " sampai " & MonthName(RowValue.Babit.EndPeriod, False) & " " & RowValue.Babit.BabitYear
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0, DealerCode)
        If dtgNoPerjanjian.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer, ByVal DealerCode As String)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.Exact, DealerCode))

        arl = New BabitSalesComm.BabitFacade(User).RetrieveBabitAllocationByCriteria(criterias, indexPage + 1, dtgNoPerjanjian.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), String))
        dtgNoPerjanjian.DataSource = arl
        dtgNoPerjanjian.VirtualItemCount = totalRow
        dtgNoPerjanjian.DataBind()
    End Sub

    Private Sub dtgNoPerjanjian_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgNoPerjanjian.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        BindDataGrid(0, DealerCode)
    End Sub
End Class
