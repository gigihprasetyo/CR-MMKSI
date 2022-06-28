Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility

Public Class PopUpPDILog
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private crit As CriteriaComposite
    Private dateFrom As DateTime
    Private dateTo As DateTime
    Private objPDILogFacade As PDILogFacade = New PDILogFacade(User)

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RefreshGrid()
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        RefreshGrid()
    End Sub

    Protected Sub dtgPDILog_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs)
        RefreshGrid(e.NewPageIndex)
    End Sub

    Protected Sub dtgPDILog_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        If e.SortExpression = ViewState.Item("SortCol") Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("SortCol") = e.SortExpression
        dtgPDILog.SelectedIndex = -1
        dtgPDILog.CurrentPageIndex = 0
        RefreshGrid(0)
    End Sub

    Protected Sub dtgPDILog_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        Dim RowValue As PDILog = CType(e.Item.DataItem, PDILog)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgPDILog.PageSize * dtgPDILog.CurrentPageIndex)
        End If
    End Sub
#End Region

#Region "Custom Method"
    Private Sub RefreshGrid(Optional indexPage As Integer = 0)
        Dim totalRow As Integer

        crit = New CriteriaComposite(New Criteria(GetType(PDILog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            crit.opAnd(New Criteria(GetType(PDILog), "PDI.Dealer.ID", MatchType.Exact, objDealer.ID))
        End If

        If txtChassisNumber.Text <> "" Then
            crit.opAnd(New Criteria(GetType(PDILog), "PDI.ChassisMaster.ChassisNumber", MatchType.StartsWith, txtChassisNumber.Text))
        End If

        If txtWONumber.Text <> "" Then
            crit.opAnd(New Criteria(GetType(PDILog), "PDI.WorkOrderNumber", MatchType.StartsWith, txtWONumber.Text))
        End If

        If CBTglPDI.Checked Then
            dateFrom = New DateTime(icTglPDIFrom.Value.Year, icTglPDIFrom.Value.Month, icTglPDIFrom.Value.Day, 0, 0, 0)
            dateTo = New DateTime(icTglPDITo.Value.Year, icTglPDITo.Value.Month, icTglPDITo.Value.Day, 23, 59, 59)

            crit.opAnd(New Criteria(GetType(PDILog), "PDI.PDIDate", MatchType.GreaterOrEqual, dateFrom))
            crit.opAnd(New Criteria(GetType(PDILog), "PDI.PDIDate", MatchType.LesserOrEqual, dateTo))
        End If

        If CBTglExpired.Checked Then
            dateFrom = New DateTime(icTglExpiredFrom.Value.Year, icTglExpiredFrom.Value.Month, icTglExpiredFrom.Value.Day, 0, 0, 0)
            dateTo = New DateTime(icTglExpiredTo.Value.Year, icTglExpiredTo.Value.Month, icTglExpiredTo.Value.Day, 23, 59, 59)

            crit.opAnd(New Criteria(GetType(PDILog), "ExpiredPDIDate", MatchType.GreaterOrEqual, dateFrom))
            crit.opAnd(New Criteria(GetType(PDILog), "ExpiredPDIDate", MatchType.LesserOrEqual, dateTo))
        End If

        Dim data As ArrayList = objPDILogFacade.RetrieveByCriteria(crit, indexPage + 1, dtgPDILog.PageSize, totalRow, ViewState.Item("SortCol"), ViewState.Item("SortDirection"))

        If data.Count = 0 Then
            dtgPDILog.CurrentPageIndex = 0
        Else
            dtgPDILog.CurrentPageIndex = indexPage
        End If

        dtgPDILog.DataSource = data
        dtgPDILog.VirtualItemCount = totalRow
        dtgPDILog.DataBind()
    End Sub
#End Region

End Class