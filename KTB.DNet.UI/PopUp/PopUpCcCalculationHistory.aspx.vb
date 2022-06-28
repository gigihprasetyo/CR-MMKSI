#Region "DNet Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Imports OfficeOpenXml.Style
Imports OfficeOpenXml

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq

#End Region

Public Class PopUpCcCalculationHistory
    Inherits System.Web.UI.Page

    Dim gridColNo As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ViewState("CurrentSortColumn") = "CcPeriod.YearMonth"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            BindDropDown()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub BindDropDown()
        ddlPeriode.Items.Clear()
        Dim listYearMonth As New List(Of String)
        Dim formulaId As Integer = CInt(Page.Request.QueryString("formula"))
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "RowStatus", MatchType.Exact, CShort(DBRowStatus.Active)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "CcCSPerformanceMaster.ID", MatchType.Exact, formulaId))

        Dim arlResult As ArrayList = New CcCSPerformanceCalculationHistoryFacade(User).Retrieve(criterias)
        If arlResult.Count > 0 Then
            For Each history As CcCSPerformanceCalculationHistory In arlResult
                If Not listYearMonth.Contains(history.CcPeriod.YearMonth) Then
                    listYearMonth.Add(history.CcPeriod.YearMonth)
                End If
            Next
        End If
        listYearMonth.Sort()

        For Each yearMonth As String In listYearMonth
            ddlPeriode.Items.Add(New ListItem(yearMonth, yearMonth))
        Next

        ddlPeriode.Items.Insert(0, New ListItem("SEMUA", "-1"))

    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        gridColNo = dtgHistory.PageSize * indexPage
        Dim formulaId As Integer = CInt(Page.Request.QueryString("formula"))

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "RowStatus", MatchType.Exact, CShort(DBRowStatus.Active)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "CcCSPerformanceMaster.ID", MatchType.Exact, formulaId))

        If ddlPeriode.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "CcPeriod.YearMonth", MatchType.Exact, ddlPeriode.SelectedValue))
        End If

        Dim arlResult As ArrayList = New CcCSPerformanceCalculationHistoryFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgHistory.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If arlResult.Count > 0 Then
            dtgHistory.DataSource = arlResult
            dtgHistory.VirtualItemCount = totalRow
            dtgHistory.CurrentPageIndex = indexPage
            dtgHistory.DataBind()
        End If


    End Sub

    Private Sub dtgHistory_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgHistory.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            gridColNo += 1
            Dim dat As CcCSPerformanceCalculationHistory = CType(e.Item.DataItem, CcCSPerformanceCalculationHistory)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblFormula As Label = CType(e.Item.FindControl("lblFormula"), Label)
            Dim lblPeriod As Label = CType(e.Item.FindControl("lblPeriod"), Label)
            Dim lblCluster As Label = CType(e.Item.FindControl("lblCluster"), Label)
            Dim lblRequest As Label = CType(e.Item.FindControl("lblRequest"), Label)
            Dim lblProcess As Label = CType(e.Item.FindControl("lblProcess"), Label)

            lblNo.Text = gridColNo
            lblFormula.Text = dat.CcCSPerformanceMaster.Description
            lblPeriod.Text = dat.CcPeriod.YearMonth
            lblCluster.Text = dat.CcCSPerformanceCluster.ClusterName
            lblRequest.Text = dat.RequestedDate

            If dat.ProcessedDate.Year = 1753 Then
                lblProcess.Text = String.Empty
            Else
                lblProcess.Text = dat.ProcessedDate
            End If

        End If
    End Sub

    Private Sub dtgHistory_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgHistory.PageIndexChanged
        Try
            'dgList.CurrentPageIndex = e.NewPageIndex
            BindDataGrid(e.NewPageIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtgHistory_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgHistory.SortCommand
        Try
            If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
                Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                    Case Sort.SortDirection.ASC
                        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                    Case Sort.SortDirection.DESC
                        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
                End Select
            Else
                ViewState("CurrentSortColumn") = e.SortExpression
                ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End If

            dtgHistory.CurrentPageIndex = 0
            BindDataGrid(dtgHistory.CurrentPageIndex)
        Catch ex As Exception

        End Try
    End Sub
End Class