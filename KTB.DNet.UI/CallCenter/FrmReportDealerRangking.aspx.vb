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

Public Class FrmReportDealerRangking
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents fraDownload As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents fraDownload As System.Web.UI.HtmlControls.HtmlIframe
    Protected WithEvents ddlPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlDealer As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlVehicle As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlServiceType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlReport As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnShow As System.Web.UI.WebControls.Button
    Protected WithEvents dgReports As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlMonth2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnRefreshGrid As System.Web.UI.WebControls.Button
    Protected WithEvents ddlYear2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatusDealer As System.Web.UI.WebControls.DropDownList

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
        End If
    End Sub

    Private Sub btnRefreshGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshGrid.Click
        BindDataReport(dgReports.CurrentPageIndex)
    End Sub

    Private Sub ddlPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriod.SelectedIndexChanged
        GetMonthList(CType(ddlPeriod.SelectedValue, Integer))
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        If ddlYear.SelectedIndex <> 0 And ddlMonth.SelectedIndex <> 0 And ddlYear2.SelectedIndex <> 0 And ddlMonth2.SelectedIndex <> 0 Then
            Dim dtStart As DateTime = New DateTime(CType(ddlYear.SelectedValue, Integer), CType(ddlMonth.SelectedValue, Integer), 1)
            Dim dtEnd As DateTime = New DateTime(CType(ddlYear2.SelectedValue, Integer), CType(ddlMonth2.SelectedValue, Integer), 1)
            If dtEnd < dtStart Then
                Dim arlReportDealer As New ArrayList
                dgReports.DataSource = arlReportDealer
                dgReports.DataBind()
                MessageBox.Show("Periode Bulan / Tahun akhir tidak boleh lebih kecil dari bulan / tahun awal")
            Else
                ShowReport()
            End If
        Else
            ShowReport()
        End If
    End Sub

    Private Sub dgReports_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReports.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objReport As DRReportRanking = e.Item.DataItem
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblDealerGroup As Label = CType(e.Item.FindControl("lblDealerGroup"), Label)
            Dim lblPeriodType As Label = CType(e.Item.FindControl("lblPeriodType"), Label)
            Dim lblPeriod As Label = CType(e.Item.FindControl("lblPeriod"), Label)
            Dim lblDownload As Label = CType(e.Item.FindControl("lblDownload"), Label)
            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            Dim lblShowDownload As Label = e.Item.FindControl("lblShowDownload")
            Dim lblStatusDealer As Label = e.Item.FindControl("lblStatusDealer")

            Dim lblDownloadXLS As Label = CType(e.Item.FindControl("lblDownloadXLS"), Label)
            Dim lbtnDownloadXLS As LinkButton = CType(e.Item.FindControl("lbtnDownloadXLS"), LinkButton)
            Dim lblShowDownloadXLS As Label = e.Item.FindControl("lblShowDownloadXLS")


            lblNo.Text = (dgReports.CurrentPageIndex * dgReports.PageSize + e.Item.ItemIndex + 1).ToString()

            If IsNothing(objReport.DealerGroup) Then
                lblDealerGroup.Text = "NASIONAL"
            Else
                lblDealerGroup.Text = objReport.DealerGroup.GroupName
            End If

            If objReport.PeriodType = 0 Then
                lblPeriodType.Text = "Bulanan"
                lblPeriod.Text = CType(CInt(Format(objReport.Period, "MM")), LookUp.EnumBulan).ToString() & " - " & Format(objReport.Period, "yyyy").ToString
            Else
                lblPeriodType.Text = "Triwulan"
                Select Case CInt(Format(objReport.Period, "MM"))
                    Case 1
                        lblPeriod.Text = "Jan - Mar " & Format(objReport.Period, "yyyy").ToString
                    Case 4
                        lblPeriod.Text = "Apr - Jun " & Format(objReport.Period, "yyyy").ToString
                    Case 7
                        lblPeriod.Text = "Jul - Sep " & Format(objReport.Period, "yyyy").ToString
                    Case 10
                        lblPeriod.Text = "Okt - Des " & Format(objReport.Period, "yyyy").ToString
                End Select
            End If
            lblStatusDealer.Text = EnumDealerType.GetStringValue(objReport.DealerType)
            If objReport.DownloadStatus = 0 Then
                lblDownload.Text = "Belum Download"
            Else
                lblDownload.Text = "Sudah Download"
            End If
            Dim btnUpdateAfter As Button = e.Item.FindControl("btnUpdateAfter")
            lblShowDownload.Attributes.Add("OnClick", "UpdateAfterDownload(" & btnUpdateAfter.ClientID & ",'" & objReport.PdfFileName & "')")

            Dim btnUpdateAfterXLS As Button = e.Item.FindControl("btnUpdateAfter")
            lblShowDownloadXLS.Attributes.Add("OnClick", "UpdateAfterDownload(" & btnUpdateAfter.ClientID & ",'" & objReport.XlsFileName & "')")
            If objReport.ReportStatus = 0 Then
                lbtnDownload.Visible = True
                lbtnDownloadXLS.Visible = True
            Else
                lbtnDownload.Visible = False
                lbtnDownloadXLS.Visible = True
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
            Case "UpdateAfterDownload"
                Dim objReport As DRReportRanking = New DRReportRankingFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                If Not objReport Is Nothing Then
                    'DownloadFile(objReport)
                    objReport.DownloadStatus = 1 'Sudah download
                    Dim vReturn As Integer = New DRReportRankingFacade(User).Update(objReport)
                    BindDataReport(dgReports.CurrentPageIndex)
                End If
        End Select

    End Sub

    Private Sub dgReports_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgReports.PageIndexChanged
        dgReports.CurrentPageIndex = e.NewPageIndex
        BindDataReport(dgReports.CurrentPageIndex)
    End Sub


#Region "Custom"

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Cc_akses_download_report_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Customer Satisfaction")
        End If
        'btnSave.Enabled = SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_sales_privilege) Or SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_aftersales_privilege)
    End Sub

    Private Sub Initialization()
        GetReportTypeList()
        GetPeriodType()
        GetYearList()
        GetServiceTypeList()
        GetVehicleKind()
        GetDealerGroup()
        GetDealerStatus()
    End Sub

    Private Sub GetReportTypeList()
        ddlReport.Items.Clear()

        ddlReport.Items.Clear()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcReportMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCol.opAnd(New Criteria(GetType(CcReportMaster), "RptType", MatchType.Exact, 2))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(CcReportMaster), "ID", Sort.SortDirection.ASC))
        Dim objReportList As ArrayList = New CcReportMasterFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each objReport As CcReportMaster In objReportList
            li = New ListItem(objReport.RptDesc, objReport.ID.ToString)
            ddlReport.Items.Add(li)
        Next

        li = New ListItem("Silahkan pilih", "0")
        ddlReport.Items.Insert(0, li)

    End Sub

    Private Sub GetPeriodType()
        ddlPeriod.Items.Clear()
        ddlPeriod.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlPeriod.Items.Add(New ListItem("Bulanan", 0))
        ddlPeriod.Items.Add(New ListItem("Triwulan", 1))
        ddlPeriod.SelectedIndex = 0
        GetMonthList(-1)
    End Sub

    Private Sub GetMonthList(ByVal periodType As Integer)
        Dim iMonth As Integer = 0
        iMonth = CType(Format(DateTime.Now, "MM").ToString, Integer)

        If periodType <> -1 Then
            Try

                ddlMonth.Items.Clear()
                ddlMonth.Items.Insert(0, New ListItem("Silahkan Pilih", -1))

                ddlMonth2.Items.Clear()
                ddlMonth2.Items.Insert(0, New ListItem("Silahkan Pilih", -1))

                Select Case periodType
                    Case 0
                        For Each item As ListItem In LookUp.ArrayMonth()
                            item.Selected = False
                            ddlMonth.Items.Add(item)
                            ddlMonth2.Items.Add(item)
                        Next
                        'If iMonth < 12 Then
                        '    ddlMonth.SelectedValue = iMonth - 1
                        '    ddlMonth2.SelectedValue = iMonth + 1
                        'End If
                        
                    Case 1
                        ddlMonth.Items.Insert(1, New ListItem("Jan - Mar", "1"))
                        ddlMonth.Items.Insert(2, New ListItem("Apr - Jun", "4"))
                        ddlMonth.Items.Insert(3, New ListItem("Jul - Sep", "7"))
                        ddlMonth.Items.Insert(4, New ListItem("Okt - Des", "10"))
                        'If (iMonth - 3) < 0 Then
                        '    ddlMonth.SelectedValue = iMonth + 10
                        'ElseIf (iMonth - 3) = 0 Then
                        '    ddlMonth.SelectedIndex = 1
                        'Else
                        '    ddlMonth.SelectedValue = iMonth - 2
                        'End If

                        ddlMonth2.Items.Insert(1, New ListItem("Jan - Mar", "1"))
                        ddlMonth2.Items.Insert(2, New ListItem("Apr - Jun", "4"))
                        ddlMonth2.Items.Insert(3, New ListItem("Jul - Sep", "7"))
                        ddlMonth2.Items.Insert(4, New ListItem("Okt - Des", "10"))
                        'If (iMonth - 3) < 0 Then
                        '    ddlMonth2.SelectedValue = iMonth + 10
                        'ElseIf (iMonth - 3) = 0 Then
                        '    ddlMonth2.SelectedIndex = 1
                        'Else
                        '    ddlMonth2.SelectedValue = iMonth - 2
                        'End If
                End Select
                ddlMonth.SelectedIndex = 0
                ddlMonth2.SelectedIndex = 0
            Catch ex As Exception
                'MessageBox.Show("Error Binding ddlMonth, silahkan kirim error ini ke dnet admin")
            End Try
        Else
            ddlMonth.Items.Clear()
            ddlMonth.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            ddlMonth2.Items.Clear()
            ddlMonth2.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlMonth.Items.Add(item)
                ddlMonth2.Items.Add(item)
            Next
            'ddlMonth.SelectedValue = iMonth - 1
            'ddlMonth2.SelectedValue = iMonth + 1
        End If

    End Sub

    Private Sub GetYearList()
        Try
            ddlYear.Items.Clear()
            ddlYear2.Items.Clear()
            ddlYear.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            ddlYear2.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayYear(True, 10, 1, Date.Now.Year.ToString)
                item.Selected = False
                ddlYear.Items.Add(item)
                ddlYear2.Items.Add(item)
            Next
            ddlYear.ClearSelection()
            ddlYear.SelectedValue = Format(DateTime.Now, "yyyy").ToString
            ddlYear2.ClearSelection()
            ddlYear2.SelectedValue = Format(DateTime.Now, "yyyy").ToString

        Catch ex As Exception
            MessageBox.Show("Error Binding ddlYear, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    Private Sub GetServiceTypeList()
        ddlServiceType.Items.Clear()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCustomerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(CcCustomerCategory), "ID", Sort.SortDirection.ASC))
        Dim objCategory As ArrayList = New CcCustomerCategoryFacade(User).Retrieve(critCol, sortCol)
        Dim li As ListItem
        For Each oneCategory As CcCustomerCategory In objCategory
            li = New ListItem(oneCategory.Description, oneCategory.ID.ToString)
            If oneCategory.Code = "SALES" Then
                If SecurityProvider.Authorize(Context.User, SR.Cc_akses_download_report_sales_privilege) Then
                    ddlServiceType.Items.Add(li)
                End If
            ElseIf oneCategory.Code = "ASS" Then
                If SecurityProvider.Authorize(Context.User, SR.Cc_akses_download_report_aftersales_privilege) Then
                    ddlServiceType.Items.Add(li)
                End If
            End If
        Next
        If ddlServiceType.Items.Count > 1 Then
            li = New ListItem("Silahkan pilih", "0")
            ddlServiceType.Items.Insert(0, li)
        ElseIf ddlServiceType.Items.Count = 0 Then
            MessageBox.Show("Anda tidak mempunyai otoritas untuk melihat jenis report. Hubungi Administrator")
        End If

    End Sub

    Private Sub GetVehicleKind()
        ddlVehicle.Items.Clear()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcVehicleCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(CcVehicleCategory), "ID", Sort.SortDirection.ASC))
        Dim objCategory As ArrayList = New CcVehicleCategoryFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each oneCategory As CcVehicleCategory In objCategory
            li = New ListItem(oneCategory.Description, oneCategory.ID.ToString)
            ddlVehicle.Items.Add(li)
        Next

        li = New ListItem("Silahkan pilih", "0")
        ddlVehicle.Items.Insert(0, li)
    End Sub

    Private Sub GetDealerGroup()

        Dim objDealer As Dealer = _sessHelper.GetSession("DEALER")

        ddlDealer.Items.Clear()
        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCol.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerGroup), "ID", MatchType.No, 21))

        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(DealerGroup), "GroupName", Sort.SortDirection.ASC))
        Dim objDealerGroup As ArrayList = New DealerGroupFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each group As DealerGroup In objDealerGroup
            li = New ListItem(group.GroupName, group.ID.ToString)
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If objDealer.DealerGroup.ID <> 21 Then
                    If group.ID = objDealer.DealerGroup.ID Then
                        ddlDealer.Items.Add(li)
                    End If
                End If
            Else
                ddlDealer.Items.Add(li)
            End If
        Next

        li = New ListItem("Silahkan Pilih", "0")
        ddlDealer.Items.Insert(0, li)

        li = New ListItem("NASIONAL", Nothing)
        ddlDealer.Items.Insert(1, li)

    End Sub

    Private Sub GetDealerStatus()
        ddlStatusDealer.Items.Clear()
        Dim enums As New EnumDealerType
        Dim objTypeList As ArrayList = enums.RetrieveType()

        Dim li As ListItem
        For Each type As EnumDealerTypeProperty In objTypeList
            li = New ListItem(type.NameType, type.ValType)
            ddlStatusDealer.Items.Add(li)
        Next

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
            Dim objDealer As Dealer = _sessHelper.GetSession("DEALER")

            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DRReportRanking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If ddlReport.SelectedIndex <> 0 Then
                crits.opAnd(New Criteria(GetType(DRReportRanking), "CcReportMaster.ID", MatchType.Exact, CType(ddlReport.SelectedValue, Integer)))
            End If

            If ddlPeriod.SelectedIndex <> 0 Then
                crits.opAnd(New Criteria(GetType(DRReportRanking), "PeriodType", MatchType.Exact, CType(ddlPeriod.SelectedValue, Integer)))
            End If

            'added by anh 20140502 add DealerType(0-All,Main,Branch)
            'If ddlStatusDealer.SelectedIndex > 0 Then
            crits.opAnd(New Criteria(GetType(DRReportRanking), "DealerType", MatchType.Exact, CType(ddlStatusDealer.SelectedValue, Integer)))
            'End If


            Dim strSql As String = ""
            strSql = "SELECT DRReportRanking.ID FROM DRReportRanking WHERE 1=1 "
            Dim startDate As DateTime
            Dim endDate As DateTime

            If ddlYear.SelectedIndex <> 0 Then
                If ddlMonth.SelectedIndex <> 0 Then
                    startDate = New DateTime(CType(ddlYear.SelectedValue, Integer), CType(ddlMonth.SelectedValue, Integer), 1)
                Else
                    startDate = New DateTime(CType(ddlYear.SelectedValue, Integer), 1, 1)
                End If
                'strSql &= " AND YEAR(DRReportRanking.Period) >= " & CType(ddlYear.SelectedValue, Integer) & ""
            Else
                If ddlMonth.SelectedIndex <> 0 Then
                    startDate = New DateTime(2000, CType(ddlMonth.SelectedValue, Integer), 1)
                Else
                    startDate = New DateTime(2000, 1, 1)
                End If
            End If

            Dim iMonth As Integer = 0
            Select Case CType(Format(startDate, "MM"), Integer)
                Case 1, 2, 3
                    iMonth = 1
                Case 4, 5, 6
                    iMonth = 4
                Case 7, 8, 9
                    iMonth = 7
                Case 10, 11, 12
                    iMonth = 10
            End Select
            'strSql &= " AND MONTH(DRReportRanking.Period) >="
            'strSql &= " CASE DRReportRanking.PeriodType"
            'strSql &= " WHEN 0 then " & CType(Format(startDate, "MM"), Integer) & ""
            'strSql &= " WHEN 1 then " & iMonth & ""
            'strSql &= " END"
            strSql &= " AND DRReportRanking.Period >="
            strSql &= " CASE DRReportRanking.PeriodType"
            strSql &= " WHEN 0 then '" & Format(startDate, "yyyy").ToString & "-" & Format(startDate, "MM").ToString & "-1" & "'"
            strSql &= " WHEN 1 then '" & Format(startDate, "yyyy").ToString & "-" & iMonth.ToString & "-1" & "'"
            strSql &= " END"

            If ddlYear2.SelectedIndex <> 0 Then
                If ddlMonth2.SelectedIndex <> 0 Then
                    endDate = New DateTime(CType(ddlYear2.SelectedValue, Integer), CType(ddlMonth2.SelectedValue, Integer), 1)
                Else
                    endDate = New DateTime(CType(ddlYear2.SelectedValue, Integer), 12, 1)
                End If
                'strSql &= " AND YEAR(DRReportRanking.Period) <= " & CType(ddlYear2.SelectedValue, Integer) & ""
            Else
                If ddlMonth2.SelectedIndex <> 0 Then
                    endDate = New DateTime(CType(Format(Date.Now, "yyyy") + 1, Integer), CType(ddlMonth2.SelectedValue, Integer), 1)
                Else
                    endDate = New DateTime(CType(Format(Date.Now, "yyyy") + 1, Integer), 12, 1)
                End If
            End If

            Dim iMonth2 As Integer = 0
            Select Case CType(Format(endDate, "MM"), Integer)
                Case 1, 2, 3
                    iMonth2 = 3
                Case 4, 5, 6
                    iMonth2 = 6
                Case 7, 8, 9
                    iMonth2 = 9
                Case 10, 11, 12
                    iMonth2 = 12
            End Select
            'strSql &= " AND MONTH(DRReportRanking.Period) <="
            'strSql &= " CASE DRReportRanking.PeriodType"
            'strSql &= " WHEN 0 then " & CType(Format(endDate, "MM"), Integer) & ""
            'strSql &= " WHEN 1 then " & iMonth2 & ""
            'strSql &= " END"
            strSql &= " AND DRReportRanking.Period <="
            strSql &= " CASE DRReportRanking.PeriodType"
            strSql &= " WHEN 0 then '" & Format(endDate, "yyyy").ToString & "-" & Format(endDate, "MM").ToString & "-1" & "'"
            strSql &= " WHEN 1 then '" & Format(endDate, "yyyy").ToString & "-" & iMonth2.ToString & "-1" & "'"
            strSql &= " END"

            crits.opAnd(New Criteria(GetType(DRReportRanking), "ID", MatchType.InSet, "(" & strSql & ")"))

            If ddlServiceType.SelectedIndex <> 0 Then
                crits.opAnd(New Criteria(GetType(DRReportRanking), "CcCustomerCategory.ID", MatchType.Exact, CType(ddlServiceType.SelectedValue, Integer)))
            End If

            If ddlVehicle.SelectedIndex <> 0 Then
                crits.opAnd(New Criteria(GetType(DRReportRanking), "CcVehicleCategory.ID", MatchType.Exact, CType(ddlVehicle.SelectedValue, Integer)))
            End If

            If ddlDealer.SelectedIndex > 1 Then
                crits.opAnd(New Criteria(GetType(DRReportRanking), "DealerGroup.ID", MatchType.Exact, CType(ddlDealer.SelectedValue, Integer)))
            End If

            Dim strSql2 As String = ""
            strSql2 = "Select ID from DRReportRanking where DealerGroupID is NULL"
            If ddlDealer.SelectedIndex = 1 Then
                crits.opAnd(New Criteria(GetType(DRReportRanking), "ID", MatchType.InSet, "(" & strSql2 & ")"))
            End If

            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If ddlDealer.SelectedIndex = 0 Then
                    strSql2 &= " or DealerGroupID = " & objDealer.DealerGroup.ID.ToString & ""
                    crits.opAnd(New Criteria(GetType(DRReportRanking), "ID", MatchType.InSet, "(" & strSql2 & ")"))
                End If
            End If

            crits.opAnd(New Criteria(GetType(DRReportRanking), "ReportStatus", MatchType.Exact, EnumDRReportStatus.GetEnumValue("OPEN")))


            'If ddlYear.SelectedIndex <> 0 Then
            '    If ddlMonth.SelectedIndex <> 0 Then
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "Period", MatchType.GreaterOrEqual, New DateTime(CType(ddlYear.SelectedValue, Integer), CType(ddlMonth.SelectedValue, Integer), 1)))
            '    Else
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "Period", MatchType.GreaterOrEqual, New DateTime(CType(ddlYear.SelectedValue, Integer), 1, 1)))
            '    End If
            'Else
            '    If ddlMonth.SelectedIndex <> 0 Then
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "Period", MatchType.GreaterOrEqual, New DateTime(2000, CType(ddlMonth.SelectedValue, Integer), 1)))
            '    Else
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "Period", MatchType.GreaterOrEqual, New DateTime(2000, 1, 1)))
            '    End If
            'End If
            'If ddlYear2.SelectedIndex <> 0 Then
            '    If ddlMonth2.SelectedIndex <> 0 Then
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "Period", MatchType.LesserOrEqual, New DateTime(CType(ddlYear2.SelectedValue, Integer), CType(ddlMonth2.SelectedValue, Integer), 1)))
            '    Else
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "Period", MatchType.LesserOrEqual, New DateTime(CType(ddlYear2.SelectedValue, Integer), 12, 1)))
            '    End If
            'Else
            '    If ddlMonth2.SelectedIndex <> 0 Then
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "Period", MatchType.LesserOrEqual, New DateTime(CType(Format(Date.Now, "yyyy") + 1, Integer), CType(ddlMonth2.SelectedValue, Integer), 1)))
            '    Else
            '        crits.opAnd(New Criteria(GetType(DRReportRanking), "Period", MatchType.LesserOrEqual, New DateTime(CType(Format(Date.Now, "yyyy") + 1, Integer), 12, 1)))
            '    End If
            'End If

            arlReportDealer = New DRReportRankingFacade(User).RetrieveByCriteria(crits, idxPage, dgReports.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dgReports.DataSource = arlReportDealer
            dgReports.VirtualItemCount = totalRow
            dgReports.DataBind()
        End If
    End Sub


    Private Sub DownloadFile(ByVal objReport As DRReportRanking)

        Dim path As System.IO.Path
        Dim fullpath As String = KTB.DNet.Lib.WebConfig.GetValue("CC_REPORT")

        fullpath = fullpath & objReport.PdfFileName
        Dim name = path.GetFileName(fullpath)
        Dim ext = path.GetExtension(fullpath)
        Dim type As String = ""
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("CC_User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("CC_Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("CC_Server")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If imp.Start() Then
            Dim FILENAME As String = fullpath
            Dim FileCheck As FileInfo = New FileInfo(fullpath)
            If FileCheck.Exists = True Then
                'Select Case ext
                '    Case ".htm", ".html"
                '        type = "text/HTML"
                '    Case ".txt"
                '        type = "text/plain"
                '    Case ".doc", ".rtf"
                '        type = "Application/msword"
                '    Case ".csv", ".xls"
                '        type = "Application/x-msexcel"
                '    Case ".exe"
                '        MessageBox.Show("File bermasalah tidak dapat di download")
                '        Exit Sub
                '    Case Else
                '        type = "application/x-download"
                'End Select
                'Response.AppendHeader("content-disposition", "attachment; filename=" + name)
                'If type <> "" Then
                '    Response.ContentType = type
                'End If

                objReport.DownloadStatus = 1 'Sudah download
                Dim vReturn As Integer = New DRReportRankingFacade(User).Update(objReport)
                BindDataReport(dgReports.CurrentPageIndex)
                '_sessHelper.SetSession("IS_DR_REFRESH", True)
                'Page_Load(Nothing, Nothing)
                'Response.Redirect("../PopUp/PopUpDownloadCcReport.aspx?file=" & fullpath)
                'Response.WriteFile(fullpath)
                'Response.End()
                'RegisterStartupScript("Refresh", "redireccionar();")
                imp.StopImpersonate()
                imp = Nothing
            Else
                '_sessHelper.SetSession("IS_DR_REFRESH", False)
                MessageBox.Show("File tidak tersedia")
            End If
        Else
            MessageBox.Show("File tidak dapat diakses")
        End If
    End Sub

#End Region

End Class
