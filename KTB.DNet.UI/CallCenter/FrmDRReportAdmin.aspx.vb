#Region "DNet Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmDRReportAdmin
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents fraDownload As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlReport As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnShow As System.Web.UI.WebControls.Button
    Protected WithEvents dgReports As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlMonth2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private declaration"
    Private _sessHelper As New SessionHelper
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Authorization()
        If Not IsPostBack Then
            Initialization()
            BindDataReport(0)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim dtStart As Date = New Date(CType(ddlYear1.SelectedValue, Integer), CType(ddlMonth.SelectedValue, Integer), 1)
        Dim dtEnd As Date = New Date(CType(ddlYear2.SelectedValue, Integer), CType(ddlMonth2.SelectedValue, Integer), 1)
        If dtEnd >= dtStart Then
            SaveData(ViewState("vsProcess"))
            BindDataReport(0)
        Else
            MessageBox.Show("periode akhir tidak boleh lebih kecil dari periode awal")
        End If

        'If Not (CType(ddlYear2.SelectedValue, Integer) < CType(ddlYear1.SelectedValue, Integer)) Then
        '    If Not (CType(ddlMonth2.SelectedValue, Integer) < CType(ddlMonth.SelectedValue, Integer)) Then
        '        SaveData(ViewState("vsProcess"))
        '        BindDataReport(0)
        '    Else
        '        MessageBox.Show("periode akhir tidak boleh lebih kecil dari periode awal")
        '    End If
        'Else
        '    MessageBox.Show("periode akhir tidak boleh lebih kecil dari periode awal")
        'End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Page_Load(sender, e)
        Initialization()
        BindDataReport(0)
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        If Not (CType(ddlYear2.SelectedValue, Integer) < CType(ddlYear1.SelectedValue, Integer)) Then
            If Not (CType(ddlMonth2.SelectedValue, Integer) < CType(ddlMonth.SelectedValue, Integer)) Then
                ShowReport()
            Else
                MessageBox.Show("periode akhir tidak boleh lebih kecil dari periode awal")
            End If
        Else
            MessageBox.Show("periode akhir tidak boleh lebih kecil dari periode awal")
        End If
    End Sub

    Private Sub dgReports_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReports.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then

            Dim objReport As DRReportRankingStatus = e.Item.DataItem
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblPeriod As Label = CType(e.Item.FindControl("lblPeriod"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lbtnOpen As LinkButton = CType(e.Item.FindControl("lbtnOpen"), LinkButton)
            Dim lbtnClosed As LinkButton = CType(e.Item.FindControl("lbtnClosed"), LinkButton)

            lblNo.Text = (dgReports.CurrentPageIndex * dgReports.PageSize + e.Item.ItemIndex + 1).ToString()
            lblStatus.Text = EnumDRReportStatus.GetStringValue(objReport.ReportStatus).ToString
            'lblPeriod.Text = CType(objReport.PeriodFrom, LookUp.EnumBulan).ToString & " " & CType(objReport.YearFrom, String).ToString & " - " & _
            '                CType(objReport.PeriodTo, LookUp.EnumBulan).ToString & " " & CType(objReport.YearTo, String).ToString
            lblPeriod.Text = CType(Format(objReport.PeriodFrom, "MM"), LookUp.EnumBulan).ToString & " " & CType(Format(objReport.PeriodFrom, "yyyy"), String) & " - " & _
                            CType(Format(objReport.PeriodTo, "MM"), LookUp.EnumBulan).ToString & " " & CType(Format(objReport.PeriodTo, "yyyy"), String)


            If objReport.ReportStatus = EnumDRReportStatus.ReportStatus.Open Then
                lbtnClosed.Visible = True
                lbtnOpen.Visible = False
            Else
                lbtnClosed.Visible = False
                lbtnOpen.Visible = True
            End If
        End If
    End Sub

    Private Sub dgReports_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgReports.SortCommand
        If CType(viewstate("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    viewstate("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    viewstate("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("CurrentSortColumn") = e.SortExpression
            viewstate("CurrentSortDirect") = Sort.SortDirection.DESC
        End If
        BindDataReport(dgReports.CurrentPageIndex)
    End Sub

    Private Sub dgReports_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgReports.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                BindReportStatus(CInt(e.Item.Cells(0).Text))
                ViewState.Add("vsProcess", "Edit")
                btnSave.Enabled = True
            Case "Open"
                OpenClosed(CInt(e.Item.Cells(0).Text), EnumDRReportStatus.ReportStatus.Open)
                BindDataReport(dgReports.CurrentPageIndex)
            Case "Closed"
                OpenClosed(CInt(e.Item.Cells(0).Text), EnumDRReportStatus.ReportStatus.Closed)
                BindDataReport(dgReports.CurrentPageIndex)
            Case "Delete"
                OpenClosed(CInt(e.Item.Cells(0).Text), EnumDRReportStatus.ReportStatus.Closed, True)
                BindDataReport(dgReports.CurrentPageIndex)
        End Select

    End Sub

    Private Sub dgReports_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgReports.PageIndexChanged
        dgReports.CurrentPageIndex = e.NewPageIndex
        BindDataReport(dgReports.CurrentPageIndex)
    End Sub

#Region "Custom"

    Private Sub Reset()
        ddlReport.SelectedIndex = 0
        ddlMonth.SelectedIndex = 0
        ddlMonth2.SelectedIndex = 0
        ddlYear1.SelectedIndex = 0
        ddlYear2.SelectedIndex = 0
        ddlReport.Enabled = True
    End Sub

    Private Sub BindReportStatus(ByVal nID As Integer)
        Dim objDRReportRankingStatus As DRReportRankingStatus = New DRReportRankingStatusFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsReportStatus", objDRReportRankingStatus)

        ddlReport.SelectedValue = objDRReportRankingStatus.CcReportMaster.ID
        ddlReport.Enabled = False
        ddlMonth.SelectedValue = CInt(Format(objDRReportRankingStatus.PeriodFrom, "MM"))
        ddlMonth2.SelectedValue = CInt(Format(objDRReportRankingStatus.PeriodTo, "MM"))
        ddlYear1.SelectedValue = CInt(Format(objDRReportRankingStatus.PeriodFrom, "yyyy"))
        ddlYear2.SelectedValue = CInt(Format(objDRReportRankingStatus.PeriodTo, "yyyy"))

    End Sub

    Private Sub OpenClosed(ByVal nID As Integer, ByVal iVal As Integer, Optional ByVal isDeleted As Boolean = False) '0: open; 1: closed
        Try
            Dim objDRReportRankingStatus As DRReportRankingStatus = New DRReportRankingStatusFacade(User).Retrieve(nID)
            If isDeleted = False Then
                objDRReportRankingStatus.ReportStatus = iVal
            Else
                objDRReportRankingStatus.RowStatus = CType(DBRowStatus.Deleted, Short)
            End If
            Dim nResult = New DRReportRankingStatusFacade(User).Update(objDRReportRankingStatus)
            If nResult <> -1 Then
                UpdateReport(objDRReportRankingStatus, iVal)
            End If
        Catch ex As Exception
            MessageBox.Show("Proses gagal")
        End Try

    End Sub

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Cc_akses_download_report_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Customer Satisfaction")
        End If
        'btnSave.Enabled = SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_sales_privilege) Or SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_aftersales_privilege)
    End Sub

    Private Sub Initialization()
        GetReportTypeList()
        GetMonthList()
        GetYearList()
        btnSave.Enabled = False
    End Sub

    Private Sub GetReportTypeList()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcReportMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCol.opAnd(New Criteria(GetType(CcReportMaster), "RptType", MatchType.Exact, 2))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(CcReportMaster), "ID", Sort.SortDirection.ASC))
        Dim objReportList As ArrayList = New CcReportMasterFacade(User).Retrieve(critCol, sortCol)

        ddlReport.Items.Clear()
        Dim li As ListItem
        For Each objReport As CcReportMaster In objReportList
            li = New ListItem(objReport.RptDesc, objReport.ID.ToString)
            ddlReport.Items.Add(li)
        Next

        li = New ListItem("Silahkan pilih", "0")
        ddlReport.Items.Insert(0, li)

    End Sub

    Private Sub GetMonthList()
        Try
            Dim iMonth As Integer = 0
            iMonth = CType(Format(DateTime.Now, "MM").ToString, Integer)

            ddlMonth.Items.Clear()
            ddlMonth2.Items.Clear()
            ddlMonth.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            ddlMonth2.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlMonth.Items.Add(item)
                ddlMonth2.Items.Add(item)
            Next
        Catch ex As Exception
            'MessageBox.Show("Error Binding ddlMonth, silahkan kirim error ini ke dnet admin")
        End Try

    End Sub

    Private Sub GetYearList()
        Try
            ddlYear1.Items.Clear()
            ddlYear1.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            ddlYear2.Items.Clear()
            ddlYear2.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayYear(True, 0, 8, Date.Now.Year.ToString)
                item.Selected = False
                ddlYear1.Items.Add(item)
                ddlYear2.Items.Add(item)
            Next
            ddlYear1.ClearSelection()
            ddlYear2.ClearSelection()
            ddlYear1.SelectedIndex = 0
            ddlYear2.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlYear, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    Private Sub ShowReport()
        dgReports.CurrentPageIndex = 0
        BindDataReport(dgReports.CurrentPageIndex)
    End Sub

    Private Sub BindDataReport(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arlReportDealer As New ArrayList
        dgReports.DataSource = arlReportDealer
        dgReports.DataBind()

        If (idxPage >= 0) Then
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DRReportRankingStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If ddlReport.SelectedIndex <> 0 Then
                crits.opAnd(New Criteria(GetType(DRReportRankingStatus), "CcReportMaster.ID", MatchType.Exact, CType(ddlReport.SelectedValue, Integer)))
            End If
            'If ddlMonth.SelectedIndex <> 0 Then
            '    If ddlMonth2.SelectedIndex <> 0 Then
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "PeriodFrom", MatchType.GreaterOrEqual, CType(ddlMonth.SelectedValue, Integer)))
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "PeriodTo", MatchType.LesserOrEqual, CType(ddlMonth2.SelectedValue, Integer)))
            '    Else
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "PeriodFrom", MatchType.Exact, CType(ddlMonth.SelectedValue, Integer)))
            '    End If
            'End If

            'If ddlYear.SelectedIndex <> 0 Then
            '    crits.opAnd(New Criteria(GetType(DRReportRanking), "Year", MatchType.Exact, CType(ddlYear.SelectedValue, Integer)))
            'End If

            arlReportDealer = New DRReportRankingStatusFacade(User).RetrieveByCriteria(crits, idxPage, dgReports.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dgReports.DataSource = arlReportDealer
            dgReports.VirtualItemCount = totalRow
            dgReports.DataBind()
        End If
    End Sub

    Private Sub SaveData(ByVal vsProses As String)
        Dim objDRReportRankingStatus As DRReportRankingStatus = New DRReportRankingStatus
        Dim objDRReportRankingStatusFacade As DRReportRankingStatusFacade = New DRReportRankingStatusFacade(User)
        Dim nResult As Integer = -1

        Try
            If vsProses = "Edit" Then
                objDRReportRankingStatus = _sessHelper.GetSession("vsReportStatus")
                If IsNothing(objDRReportRankingStatusFacade) Then
                    MessageBox.Show(SR.DataNotFound("Tidak ada data "))
                    Return
                End If
            End If

            Dim objReportMaster As CcReportMaster = New CcReportMasterFacade(User).Retrieve(CType(ddlReport.SelectedValue, Integer))
            'If objReportMaster <> objDRReportRankingStatus.CcReportMaster Then

            'End If

            Dim iStatus As Integer = 0
            If vsProses = "Edit" Then
                'untuk periode sebelumnya DRReportRanking di set ReportStatus= 1 (Closed)
                If objDRReportRankingStatus.ReportStatus = EnumDRReportStatus.ReportStatus.Closed Then
                    iStatus = EnumDRReportStatus.ReportStatus.Closed
                    UpdateReport(objDRReportRankingStatus, EnumDRReportStatus.ReportStatus.Open)
                End If
                objDRReportRankingStatus.CcReportMaster = objReportMaster
                objDRReportRankingStatus.PeriodFrom = New DateTime(CInt(ddlYear1.SelectedValue), CInt(ddlMonth.SelectedValue), 1)
                'objDRReportRankingStatus.YearFrom = ddlYear1.SelectedValue
                objDRReportRankingStatus.PeriodTo = New DateTime(CInt(ddlYear2.SelectedValue), CInt(ddlMonth2.SelectedValue), 1)
                'objDRReportRankingStatus.YearTo = ddlYear2.SelectedValue
                'If objDRReportRankingStatus.ReportStatus = EnumDRReportStatus.ReportStatus.Closed Then
                objDRReportRankingStatus.ReportStatus = EnumDRReportStatus.ReportStatus.Closed
                nResult = objDRReportRankingStatusFacade.Update(objDRReportRankingStatus)
                'End If
            End If

            If nResult <= 0 Then
                MessageBox.Show(SR.SaveFail)
            Else
                'If iStatus = EnumDRReportStatus.ReportStatus.Closed Then
                UpdateReport(objDRReportRankingStatus, EnumDRReportStatus.ReportStatus.Closed)
                'Else
                '    UpdateReport(objDRReportRankingStatus.CcReportMaster.ID, EnumDRReportStatus.ReportStatus.Open)
                'End If
                MessageBox.Show(SR.SaveSuccess)
                Reset()
                btnSave.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
    End Sub

    Private Sub UpdateReport(ByVal objDRReportRankingStatus As DRReportRankingStatus, ByVal iReportStatus As Integer)
        Dim arlReportDealer As New ArrayList
        

        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DRReportRanking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(DRReportRanking), "CcReportMaster.ID", MatchType.Exact, objDRReportRankingStatus.CcReportMaster.ID))

        Dim strSql As String = ""
        strSql = "SELECT DRReportRanking.ID FROM DRReportRanking WHERE 1=1 "

        Dim iMonth As Integer = 0
        Select Case CType(Format(objDRReportRankingStatus.PeriodFrom, "MM"), Integer)
            Case 1, 2, 3
                iMonth = 1
            Case 4, 5, 6
                iMonth = 4
            Case 7, 8, 9
                iMonth = 7
            Case 10, 11, 12
                iMonth = 10
        End Select
        strSql &= " AND DRReportRanking.Period >="
        strSql &= " CASE DRReportRanking.PeriodType"
        strSql &= " WHEN 0 then '" & Format(objDRReportRankingStatus.PeriodFrom, "yyyy").ToString & "-" & Format(objDRReportRankingStatus.PeriodFrom, "MM").ToString & "-1" & "'"
        strSql &= " WHEN 1 then '" & Format(objDRReportRankingStatus.PeriodFrom, "yyyy").ToString & "-" & iMonth.ToString & "-1" & "'"
        strSql &= " END"

        Dim iMonth2 As Integer = 0
        Select Case CType(Format(objDRReportRankingStatus.PeriodTo, "MM"), Integer)
            Case 1, 2, 3
                iMonth2 = 3
            Case 4, 5, 6
                iMonth2 = 6
            Case 7, 8, 9
                iMonth2 = 9
            Case 10, 11, 12
                iMonth2 = 12
        End Select
        strSql &= " AND DRReportRanking.Period <="
        strSql &= " CASE DRReportRanking.PeriodType"
        strSql &= " WHEN 0 then '" & Format(objDRReportRankingStatus.PeriodTo, "yyyy").ToString & "-" & Format(objDRReportRankingStatus.PeriodTo, "MM").ToString & "-1" & "'"
        strSql &= " WHEN 1 then '" & Format(objDRReportRankingStatus.PeriodTo, "yyyy").ToString & "-" & iMonth2.ToString & "-1" & "'"
        strSql &= " END"

        crits.opAnd(New Criteria(GetType(DRReportRanking), "ID", MatchType.InSet, "(" & strSql & ")"))

        arlReportDealer = New DRReportRankingFacade(User).RetrieveByCriteria(crits)

        Dim iReturn As Integer = 0
        For Each item As DRReportRanking In arlReportDealer
            item.ReportStatus = iReportStatus
            iReturn = New DRReportRankingFacade(User).Update(item)
        Next
    End Sub
#End Region


End Class
