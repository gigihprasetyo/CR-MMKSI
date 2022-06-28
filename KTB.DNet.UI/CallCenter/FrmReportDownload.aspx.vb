#Region "DNet Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.BusinessFacade.FinishUnit
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

Public Class FrmReportDownload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents btnShow As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents ddlServiceCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlServiceType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlVehicle As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgReports As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents valDealer As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents hdnValSave As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents icPeriodFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPeriodTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTo As System.Web.UI.WebControls.Label
    Protected WithEvents lblsd As System.Web.UI.WebControls.Label
    'Protected WithEvents fraDownload As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents fraDownload As System.Web.UI.HtmlControls.HtmlIframe

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

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Authorization()
        lblSearchDealer.Attributes("onclick") = "ShowDealerSelection();"

        If Not IsPostBack Then
            Initialization()
            Dim objDealer As Dealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                'txtDealerCode.ReadOnly = True
                txtDealerCode.Attributes.Add("readonly", "readonly")
            End If
        Else
            If Request.Form("hdnValSave") = "1" Then
                If Not _sessHelper.GetSession("REPORTDEALER") Is Nothing Then
                    Dim arlReportDealer As ArrayList = CType(_sessHelper.GetSession("REPORTDEALER"), ArrayList)
                    UpdateReport(arlReportDealer)
                    hdnValSave.Value = "-1"
                End If
            End If
            GetDealer()
            If ddlServiceType.Items.Count > 0 AndAlso CInt(ddlServiceType.SelectedItem.Value) > 0 Then
                ShowReport()
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If IsValidParameter() Then
            If Not IsReportExist() Then
                SaveRequestReport()
                ShowReport()
            End If
        End If
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        'fraDownload.Attributes("src", "")
        If ddlServiceType.Items.Count > 0 Then
            ShowReport(0)
        End If
    End Sub

    Private Sub dgReports_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReports.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objReport As CcReportDealer = e.Item.DataItem
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblPeriod As Label = CType(e.Item.FindControl("lblPeriod"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblRequestor As Label = CType(e.Item.FindControl("lblRequestor"), Label)
            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)

            lblNo.Text = (dgReports.CurrentPageIndex * dgReports.PageSize + e.Item.ItemIndex + 1).ToString()
            lblPeriod.Text = objReport.PeriodFrom.ToString("dd/MM/yyyy") & " - " & objReport.PeriodTo.ToString("dd/MM/yyyy")
            lblStatus.Text = EnumCcReportStatus.GetStringValue(objReport.ReportStatus)
            If objReport.ReportStatus = EnumCcReportStatus.ReportStatus.Done Or objReport.ReportStatus = EnumCcReportStatus.ReportStatus.Downloaded Then
                lbtnDownload.Visible = True
            Else
                lbtnDownload.Visible = False
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
            Case "Download"
                Dim objReport As CcReportDealer = New CcReportDealerFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                If Not objReport Is Nothing Then
                    DownloadFile(objReport)
                End If
        End Select

    End Sub

    Private Sub dgReports_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgReports.PageIndexChanged
        dgReports.CurrentPageIndex = e.NewPageIndex
        BindDataReport(dgReports.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom Method"

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Customer Satisfaction")
        End If
        'btnSave.Enabled = SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_sales_privilege) Or SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_aftersales_privilege)
    End Sub


    Private Sub Initialization()
        GetDealer()
        GetServiceCategoryList()
        GetServiceTypeList()
        GetVehicleKind()
        ViewState("CurrentSortColumn") = "PeriodFrom"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

    End Sub

    Private Sub GetDealer()
        If txtDealerCode.Text.Trim <> "" Then
            txtDealerCode.Text = txtDealerCode.Text.Trim
        End If
    End Sub

    Private Sub GetServiceCategoryList()
        ddlServiceCategory.Items.Clear()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcReportMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(CcReportMaster), "ID", Sort.SortDirection.ASC))
        critCol.opAnd(New Criteria(GetType(CcReportMaster), "RptType", MatchType.Exact, "1"))
        Dim objReportList As ArrayList = New CcReportMasterFacade(User).Retrieve(critCol, sortCol)


        Dim li As ListItem
        For Each objReport As CcReportMaster In objReportList
            li = New ListItem(objReport.RptDesc, objReport.ID.ToString)
            ddlServiceCategory.Items.Add(li)
        Next

        li = New ListItem("Silahkan pilih", "0")
        ddlServiceCategory.Items.Insert(0, li)
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
                If SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_sales_privilege) Then
                    ddlServiceType.Items.Add(li)
                End If
            ElseIf oneCategory.Code = "ASS" Then
                If SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_aftersales_privilege) Then
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
    Private Sub ShowReport(Optional ByVal IDX As Integer = -1)
        If IDX >= 0 Then
            dgReports.CurrentPageIndex = IDX
        End If

        'dgReports.CurrentPageIndex = 0
        BindDataReport(dgReports.CurrentPageIndex)
    End Sub

    Private Sub UpdateReport(ByVal arlReportDealer As ArrayList)
        Dim iReturn As Integer = 0
        Dim objReportDealerFacade As New CcReportDealerFacade(User)
        
        Try
            For Each objReportDealer As CcReportDealer In arlReportDealer
                objReportDealer.ReportStatus = EnumCcReportStatus.ReportStatus.InProgress
                objReportDealer.PdfFileName = String.Empty
                iReturn = objReportDealerFacade.Update(objReportDealer)
            Next
            MessageBox.Show(SR.SaveSuccess)
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try

    End Sub

    Private Sub SaveRequestReport()
        Dim objReportDealer As New CcReportDealer
        Dim arrOfSpllitedRow() As String
        If txtDealerCode.Text.Trim <> "" Then
            arrOfSpllitedRow = txtDealerCode.Text.Trim.Split(";")
        End If
        Dim objDealer As Dealer
        Dim objReportMaster As CcReportMaster
        Dim objCustCategory As CcCustomerCategory
        Dim objVehicle As CcVehicleCategory

        Try
            For Each strDealer As String In arrOfSpllitedRow
                'Try
                objDealer = New DealerFacade(User).Retrieve(strDealer)
                objReportMaster = New CcReportMasterFacade(User).Retrieve(CType(ddlServiceCategory.SelectedValue, Integer))
                objCustCategory = New CcCustomerCategoryFacade(User).Retrieve(CType(ddlServiceType.SelectedValue, Integer))
                objVehicle = New CcVehicleCategoryFacade(User).Retrieve(CType(ddlVehicle.SelectedValue, Integer))

                objReportDealer.Dealer = objDealer
                objReportDealer.CcReportMaster = objReportMaster
                objReportDealer.CcCustomerCategory = objCustCategory
                objReportDealer.CcVehicleCategory = objVehicle
                objReportDealer.PeriodFrom = icPeriodFrom.Value
                objReportDealer.PeriodTo = icPeriodTo.Value
                objReportDealer.ReportStatus = EnumCcReportStatus.ReportStatus.InProgress
                objReportDealer.PdfFileName = String.Empty
                objReportDealer.RowStatus = CType(DBRowStatus.Active, Integer)

                Dim iResult As Integer = New CcReportDealerFacade(User).Insert(objReportDealer)
            Next
            MessageBox.Show(SR.SaveSuccess)
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try

    End Sub

    Private Sub BindDataReport(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arlReportDealer As New ArrayList
        dgReports.DataSource = arlReportDealer
        dgReports.DataBind()
        If DateDiff(DateInterval.Month, icPeriodFrom.Value, icPeriodTo.Value) > 6 Then
            MessageBox.Show("Range periode report tidak boleh lebih dari 6 bulan")
            Exit Sub
        End If
        If icPeriodFrom.Value > icPeriodTo.Value Then
            MessageBox.Show("Periode awal tidak boleh lebih besar dari periode akhir")
            Exit Sub
        End If

        If (idxPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(CcReportDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim objDealer As New Dealer
            If (txtDealerCode.Text.Trim() <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcReportDealer), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Trim().Replace(";", "','") & "')"))
            Else
                MessageBox.Show("Tentukan dealer terlebih dahulu")
                Exit Sub
            End If

            criterias.opAnd(New Criteria(GetType(CcReportDealer), "PeriodFrom", MatchType.GreaterOrEqual, icPeriodFrom.Value))
            criterias.opAnd(New Criteria(GetType(CcReportDealer), "PeriodTo", MatchType.LesserOrEqual, icPeriodTo.Value))

            If ddlServiceCategory.SelectedValue <> 0 Then
                criterias.opAnd(New Criteria(GetType(CcReportDealer), "CcReportMaster.ID", MatchType.Exact, CType(ddlServiceCategory.SelectedValue, Integer)))
            End If

            If ddlServiceType.SelectedValue <> 0 Then
                criterias.opAnd(New Criteria(GetType(CcReportDealer), "CcCustomerCategory.ID", MatchType.Exact, CType(ddlServiceType.SelectedValue, Integer)))
            End If

            If ddlVehicle.SelectedValue <> 0 Then
                criterias.opAnd(New Criteria(GetType(CcReportDealer), "CcVehicleCategory.ID", MatchType.Exact, CType(ddlVehicle.SelectedValue, Integer)))
            End If

            arlReportDealer = New CcReportDealerFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgReports.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dgReports.DataSource = arlReportDealer
            dgReports.VirtualItemCount = totalRow
            dgReports.DataBind()
        End If
    End Sub

    Private Sub DownloadFile(ByVal objReport As CcReportDealer)

        Dim path As path
        Dim fullpath As String = KTB.DNet.Lib.WebConfig.GetValue("CC_REPORT")

        fullpath = fullpath & objReport.PdfFileName
        fullpath = fullpath.Trim()
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
            Dim Exist = New FileInfo(fullpath).Exists
            If Exist Then
                If objReport.ReportStatus <> EnumCcReportStatus.ReportStatus.Downloaded Then
                    'update CcReportDealer, changed status to downloaded
                    objReport.ReportStatus = EnumCcReportStatus.ReportStatus.Downloaded
                    Dim iReturn As Integer = New CcReportDealerFacade(User).Update(objReport)
                End If

                'insert CcReportDealerLog
                Dim objLog As New CcReportDealerLog
                objLog.CcReportDealerID = objReport.ID
                Dim iRtn As Integer = New CcReportDealerLogFacade(User).Insert(objLog)

                'BindDataReport(dgReports.CurrentPageIndex)

                'fraDownload.Attributes.Add("src", "../PopUp/PopUpDownloadCcReport.aspx?file=" + fullpath)

                Response.Redirect("../PopUp/PopUpDownloadCcReport.aspx?file=" & fullpath)

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
                'Response.WriteFile(fullpath)
                'Response.End()

                'fullpath = fullpath.Replace("\", "\\")
                'Dim strScript As String = "<script>DownloadCcReport('" & fullpath & "');</script>"
                'Page.RegisterStartupScript("DownloadCcReports", strScript)
            Else
                MessageBox.Show("File tidak tersedia")
            End If
            imp.StopImpersonate()
            imp = Nothing
        Else
            MessageBox.Show("File tidak dapat diakses")
        End If
    End Sub

    Private Function IsValidParameter() As Boolean
        If DateDiff(DateInterval.Month, icPeriodFrom.Value, icPeriodTo.Value) > 6 Then
            MessageBox.Show("Range periode report tidak boleh lebih dari 6 bulan")
            Return False
        End If
        If icPeriodFrom.Value > icPeriodTo.Value Then
            MessageBox.Show("Periode awal tidak boleh lebih besar dari periode akhir")
            Return False
        End If

        If txtDealerCode.Text = "" Then
            MessageBox.Show("Tentukan kode dealer")
            Return False
        End If
        If ddlServiceCategory.SelectedIndex = 0 Then
            MessageBox.Show("Pilih Report")
            Return False
        End If
        If ddlServiceType.Items.Count > 0 Then
            If ddlServiceType.SelectedValue = "0" Then
                MessageBox.Show("Pilih Jenis Pelayanan")
                Return False
            End If
        End If
        If ddlVehicle.SelectedIndex = 0 Then
            MessageBox.Show("Pilih Jenis Kendaraan")
            Return False
        End If
        Return True
    End Function

    Private Function IsReportExist() As Boolean
        Try
            Dim criterias As New CriteriaComposite(New Criteria(GetType(CcReportDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(CcReportDealer), "Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim objDealer As New Dealer
            If (txtDealerCode.Text.Trim() <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcReportDealer), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Trim().Replace(";", "','") & "')"))
            End If
            criterias.opAnd(New Criteria(GetType(CcReportDealer), "PeriodFrom", MatchType.Exact, icPeriodFrom.Value))

            criterias.opAnd(New Criteria(GetType(CcReportDealer), "PeriodTo", MatchType.Exact, icPeriodTo.Value))

            If ddlServiceCategory.SelectedValue <> 0 Then
                criterias.opAnd(New Criteria(GetType(CcReportDealer), "CcReportMaster.ID", MatchType.Exact, CType(ddlServiceCategory.SelectedValue, Integer)))
            End If

            If ddlServiceType.SelectedValue <> 0 Then
                criterias.opAnd(New Criteria(GetType(CcReportDealer), "CcCustomerCategory.ID", MatchType.Exact, CType(ddlServiceType.SelectedValue, Integer)))
            End If

            If ddlVehicle.SelectedValue <> 0 Then
                criterias.opAnd(New Criteria(GetType(CcReportDealer), "CcVehicleCategory.ID", MatchType.Exact, CType(ddlVehicle.SelectedValue, Integer)))
            End If

            Dim arlReportDealer As ArrayList = New CcReportDealerFacade(User).RetrieveByCriteria(criterias)
            Dim oRpt As New CcReportDealer
            If arlReportDealer.Count > 0 Then
                _sessHelper.SetSession("REPORTDEALER", arlReportDealer)
                'Dim strDealer1 As String
                'Dim strDealer2 As String
                Dim msg As String = ""
                'Dim msg1 As String = ""
                'Dim msg2 As String = ""

                For Each _rpt As CcReportDealer In arlReportDealer
                    msg = msg & "," & _rpt.Dealer.DealerCode
                    'If _rpt.PdfFileName <> "" Then
                    '    strDealer1 = strDealer1 & "," & _rpt.Dealer.DealerCode
                    'Else
                    '    strDealer2 = strDealer2 & "," & _rpt.Dealer.DealerCode
                    'End If
                Next
                'If strDealer1 <> "" Then
                '    msg1 = "Report sudah di generate untuk dealer " & strDealer1 & "."
                'End If
                'If strDealer2 <> "" Then
                '    msg2 = "Permintaan laporan tersebut sudah ada untuk dealer " & strDealer2 & "."
                'End If
                'If msg2 <> "" Then
                '    If msg1 <> "" Then
                '        MessageBox.Confirm(msg2 & msg1 & " Apakah mau generate ulang?", "hdnValSave")
                '    Else
                '        MessageBox.Show("Permintaan untuk laporan tersebut sudah ada")
                '    End If
                'End If

                If msg <> "" Then
                    MessageBox.Confirm("Permintaan laporan tersebut sudah ada untuk dealer " & msg & "." & " Apakah mau generate ulang?", "hdnValSave")
                End If
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

End Class
