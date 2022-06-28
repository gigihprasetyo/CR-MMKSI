Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade

Public Class PopUpBabitAlocation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStartMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlEndMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtNoPerjanjian As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgBabitAllocation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlPeriodEnd As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables Declaration"

#End Region

#Region "Custom Methods"

    Private Sub BindYear()
        ddlPeriod.Items.Clear()
        ddlPeriod.Items.Add(New ListItem("", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlPeriod.Items.Add(New ListItem(i.ToString, i))
        Next

        ddlPeriodEnd.Items.Clear()
        ddlPeriodEnd.Items.Add(New ListItem("", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlPeriodEnd.Items.Add(New ListItem(i.ToString, i))
        Next
    End Sub

    Private Sub BindMonth()
        CommonFunction.BindFromEnum("Month", ddlStartMonth, User, True, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("Month", ddlEndMonth, User, True, "NameStatus", "ValStatus")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','").Trim() & "')"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.AllocationType", MatchType.Exact, CType(EnumBabit.BabitAllocationType.Alokasi_Reguler, Short)))

        If (ddlStartMonth.SelectedValue <> "99") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.StartPeriod", MatchType.LesserOrEqual, ddlStartMonth.SelectedValue))
        End If

        'If (ddlEndMonth.SelectedValue <> "99") Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, ddlEndMonth.SelectedValue))
        'End If

        If (ddlPeriod.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYear", MatchType.LesserOrEqual, ddlPeriod.SelectedValue))
        End If

        If (ddlPeriodEnd.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYearEnd", MatchType.GreaterOrEqual, ddlPeriodEnd.SelectedValue))
        End If

        If ddlEndMonth.SelectedValue <> "99" Then
            If (ddlStartMonth.SelectedValue = "99") Or (ddlPeriod.SelectedValue = "-1") Or (ddlPeriodEnd.SelectedValue = "-1") Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, ddlEndMonth.SelectedValue))
            End If
        End If

        If (txtNoPerjanjian.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "NoPerjanjian", MatchType.Exact, txtNoPerjanjian.Text.Trim()))
        End If

        Dim arl As New ArrayList
        arl = New BabitSalesComm.BabitFacade(User).RetrieveBabitAllocationByCriteria(criterias, indexPage + 1, dtgBabitAllocation.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), String))

        Dim bfacade As New BabitSalesComm.BabitProposalFacade(User)
        If (ddlStartMonth.SelectedValue <> "99" And ddlPeriod.SelectedValue <> "-1" And ddlEndMonth.SelectedValue <> "99" And ddlPeriod.SelectedValue <> "-1") Then
            Dim dtmFrom2 As DateTime = New DateTime(CInt(ddlPeriod.SelectedValue), CInt(ddlStartMonth.SelectedValue), 1)
            Dim dtmTo2 As DateTime = New DateTime(CInt(ddlPeriodEnd.SelectedValue), CInt(ddlEndMonth.SelectedValue), 1)
            Dim arl2 As ArrayList = bfacade.FilterBabitAllocationByPeriodeList(arl, dtmFrom2, dtmTo2)
            arl = New ArrayList
            arl = arl2
            If IsNothing(arl) Then
                arl = New ArrayList
            End If
        End If

        If arl.Count > 0 Then
            dtgBabitAllocation.Visible = True
            dtgBabitAllocation.DataSource = arl
            dtgBabitAllocation.VirtualItemCount = totalRow
            dtgBabitAllocation.DataBind()
            btnChoose.Disabled = False
        Else
            dtgBabitAllocation.Visible = False
            MessageBox.Show(SR.DataNotFound("Alokasi Babit"))
            btnChoose.Disabled = True
        End If
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Not IsPostBack) Then
            txtKodeDealer.Text = Request.QueryString("DealerCode")
            BindYear()
            BindMonth()
            ViewState("currSortColumn") = "Dealer.DealerCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
    End Sub

    Private Sub dtgBabitAllocation_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBabitAllocation.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As BabitAllocation = CType(e.Item.DataItem, BabitAllocation)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                Dim lblDanaBabit As Label = CType(e.Item.FindControl("lblDanaBabit"), Label)

                e.Item.Cells(0).Controls.Add(rdbChoice)
                lblDanaBabit.Text = (RowValue.CV + RowValue.LCV + RowValue.PC).ToString("#,##0")
            End If
        End If
    End Sub

    Private Sub dtgBabitAllocation_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgBabitAllocation.PageIndexChanged
        dtgBabitAllocation.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgBabitAllocation.CurrentPageIndex)
    End Sub

    Private Sub dtgBabitAllocation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgBabitAllocation.SortCommand
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
        BindDataGrid(0)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
    End Sub

#End Region


End Class
