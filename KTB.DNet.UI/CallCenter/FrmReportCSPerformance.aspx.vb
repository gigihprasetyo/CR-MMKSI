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


Public Class FrmReportCSPerformance
    Inherits System.Web.UI.Page


#Region "Private declaration"
    Private _sessHelper As New SessionHelper
#End Region

#Region "Custom"

    Private Sub GetServiceTypeList()
        'ddlServiceType.Items.Clear()

        'Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCustomerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim sortCol As SortCollection = New SortCollection
        'sortCol.Add(New Sort(GetType(CcCustomerCategory), "ID", Sort.SortDirection.ASC))
        'Dim objCategory As ArrayList = New CcCustomerCategoryFacade(User).Retrieve(critCol, sortCol)
        'Dim li As ListItem
        'For Each oneCategory As CcCustomerCategory In objCategory
        '    li = New ListItem(oneCategory.Description, oneCategory.ID.ToString)

        '    ddlServiceType.Items.Add(li)


        'Next
        'If ddlServiceType.Items.Count > 1 Then
        '    li = New ListItem("Silahkan pilih", "0")
        '    ddlServiceType.Items.Insert(0, li)
        'ElseIf ddlServiceType.Items.Count = 0 Then
        '    MessageBox.Show("Anda tidak mempunyai otoritas untuk melihat jenis report. Hubungi Administrator")
        'End If

    End Sub

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.CSP_Report_Privilage) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Customer Satisfaction")
        End If

    End Sub


    Private Sub DownloadReport()
        Dim Fcs As New CcCSPerformanceResultHeaderFacade(User)
        Dim Param As New List(Of SqlClient.SqlParameter)
        Dim ccPeriod As CcPeriod = New CcPeriodFacade(User).Retrieve(icPeriod.Value.ToString("yyyyMM"))

        'Get param
        ' Param.Add(New SqlClient.SqlParameter("@CustomerCategoryType", ddlServiceType.SelectedValue))
        If txtDealerCode.Text.Trim() <> "" Then
            Param.Add(New SqlClient.SqlParameter("@ListDealer", txtDealerCode.Text.Trim()))
        ElseIf txtDealerCode.Text.Trim() = "" And Me.IsDealer Then
            Param.Add(New SqlClient.SqlParameter("@ListDealer", Me.GetDealer.DealerCode))
        End If

        Param.Add(New SqlClient.SqlParameter("@ccPeriodID", ccPeriod.ID))
        Param.Add(New SqlClient.SqlParameter("@FormulaID", ddlFormula.SelectedValue))

        Dim dsData As New DataSet
        dsData = Fcs.RerieveReportCSPerformance(Param)

        If Not IsNothing(dsData) AndAlso dsData.Tables.Count > 0 AndAlso dsData.Tables(0).Rows.Count > 0 Then

            Dim pck As New ExcelPackage
            pck = GenerateREport(dsData)

            Dim Apendix As String = Guid.NewGuid().ToString()
            Dim fileDownloadName = "REportCSPerf" & DateTime.Now.ToString("yyyyMMddHHmm") + Apendix.Substring(0, 4) + ".xlsx"
            Dim contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Dim fileStream As New MemoryStream()
            'pck.SaveAs(fileStream)
            'fileStream.Position = 0

            Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & fileDownloadName



            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)


            If imp.Start() Then
                pck.SaveAs(New FileInfo(Server.MapPath("~/DataTemp/" & fileDownloadName)))
                imp.StopImpersonate()
                imp = Nothing
            Else
                'imp.StopImpersonate()
                imp = Nothing
                MessageBox.Show(SR.DownloadFail("Report"))
            End If


            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileDownloadName)

            'Dim memoryStream As New MemoryStream()
            ''Dim buf = pck.GetAsByteArray()
            ''Response.Clear()
            'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            'Response.AddHeader("content-disposition", "attachment; filename=" + fileDownloadName)
            ''Response.BinaryWrite(buf)
            'pck.SaveAs(memoryStream)
            'memoryStream.WriteTo(Response.OutputStream)
            'Response.Flush()
            'Response.End()

        Else
            MessageBox.Show(SR.DataNotFound("Report"))
        End If
    End Sub

    Private Function GenerateREport(ByVal ds As DataSet) As ExcelPackage

        Dim pck As New ExcelPackage

        Dim listData As List(Of CcPerformanceReport) = MappingDataSetToList(ds)

        Dim listCluster As List(Of String) = listData.Select(Function(x) x.ClusterName).Distinct().ToList()

        For Each cluster As String In listCluster
            Dim listPerCluster As List(Of CcPerformanceReport) = listData.Where(Function(x) x.ClusterName = cluster).ToList()
            GenerateTablePerCluster(pck, cluster, listPerCluster)
        Next



            'Dim dt As New DataTable
            'Dim dtPar As New DataTable
            'dtPar = ds.Tables(1)
            'Dim NPeriod As Integer
            'Dim NMDs As Integer
            'NPeriod = dtPar.Rows(0)("NPeriod")
            'NMDs = dtPar.Rows(0)("MDS")

            'dt = ds.Tables(0)
            'Dim DRMax As DataRow() = ds.Tables(0).Select(String.Format("TotalSampling >= {0}", (NPeriod * NMDs).ToString()), "SummaryScore DESC")
            'Dim DRMin As DataRow() = ds.Tables(0).Select(String.Format("TotalSampling < {0}", (NPeriod * NMDs).ToString()), "SummaryScore DESC")

            'If Not IsNothing(DRMax) AndAlso DRMax.Length > 0 Then
            '    GenerateReportGeneral(DRMax.CopyToDataTable(), dt, pck, "Di Atas Sampling", NMDs)
            'End If

            'If Not IsNothing(DRMin) AndAlso DRMin.Length > 0 Then
            '    GenerateReportGeneral(DRMin.CopyToDataTable(), dt, pck, "Di Bawah Sampling", NMDs)
            'End If



            Return pck

    End Function

    Private Sub GenerateReportGeneral(ByVal dtFiltered As DataTable, ByVal dt As DataTable, ByRef pck As ExcelPackage, ByVal TitleName As String, ByVal Mds As Integer)


        Dim ws = pck.Workbook.Worksheets.Add(TitleName)

        Dim idxStart As Integer = 2

        Dim idRHStart As Integer = 7
        Dim NSubParam As Integer = 0


        Dim dtParam As New DataTable
        Dim dtSubParam As New DataTable
        Dim dtDealer As New DataTable
        Dim ILastCol As Integer


        'Report Title
        ws.Cells(idxStart, 2).Value = "Dealer"
        ws.Cells(idxStart, 3).Value = ":"
        ws.Cells(idxStart, 4).Value = IIf(txtDealerCode.Text.Trim() = "", "ALL", txtDealerCode.Text.Trim())

        ws.Cells(idxStart + 1, 2).Value = "Periode"
        ws.Cells(idxStart + 1, 3).Value = ":"
        ' ws.Cells(idxStart + 1, 4).Value = icPeriodFrom.Value.ToString("MM yyyy") & " s/d " & icPeriodTo.Value.ToString("MM yyyy")

        ws.Cells(idxStart + 2, 2).Value = "Customer Category"
        ws.Cells(idxStart + 2, 3).Value = ":"
        'ws.Cells(idxStart + 2, 4).Value = ddlServiceType.SelectedItem.Text

        ws.Cells(idxStart + 3, 2).Value = "Minimum Data Sampling"
        ws.Cells(idxStart + 3, 3).Value = ":"
        ws.Cells(idxStart + 3, 4).Value = Mds



        dtParam = New DataView(dt).ToTable(True, "CFPID", "PMHeaderDesc")
        dtSubParam = New DataView(dt).ToTable(True, "CFPID", "CSPID", "PMSubDesc")
        dtDealer = New DataView(dtFiltered).ToTable(True, "DealerCode", "GroupName", "DealerName", "CityName", "AreaDesc")

        NSubParam = dtSubParam.Rows.Count + dtParam.Rows.Count

        ILastCol = 7 + NSubParam



        'Report Header
        ws.Cells(idRHStart, 1).Value = "No"
        ws.Cells(idRHStart, 1, idRHStart + 2, 1).Merge = True

        ws.Cells(idRHStart, 2).Value = "Dealer Code"
        ws.Cells(idRHStart, 2, idRHStart + 2, 2).Merge = True

        ws.Cells(idRHStart, 3).Value = "Dealer Group"
        ws.Cells(idRHStart, 3, idRHStart + 2, 3).Merge = True

        ws.Cells(idRHStart, 4).Value = "Dealer Name"
        ws.Cells(idRHStart, 4, idRHStart + 2, 4).Merge = True


        ws.Cells(idRHStart, 5).Value = "City"
        ws.Cells(idRHStart, 5, idRHStart + 2, 5).Merge = True

        ws.Cells(idRHStart, 6).Value = "Area"
        ws.Cells(idRHStart, 6, idRHStart + 2, 6).Merge = True





        'Formating CommonHeader
        For cw As Integer = 1 To (ILastCol)
            ws.Column(1).Width = 50
        Next

        'Header Summary
        ' ws.Cells(idRHStart, 7).Value = String.Format("CS PERFORMANCE SCORE ({0})", ddlServiceType.SelectedItem.Text)
        ws.Cells(idRHStart, 7, idRHStart, ILastCol).Merge = True

        'Header row2
        Dim CPar As Integer = 0
        For Each dr As DataRow In dtParam.Rows
            Dim strCFPID As String = dr("CFPID").ToString()

            Dim ND As DataRow() = dtSubParam.Select(String.Format(" CFPID = '{0}'", strCFPID))
            Dim nSubPar As Integer = 0
            If Not IsNothing(ND) Then
                nSubPar = ND.Length
            End If


            ws.Cells(idRHStart + 1, 7 + CPar).Value = dr("PMHeaderDesc").ToString()


            'Header Sub Param
            Dim idSub As Integer = 0

            For Each dr2 As DataRow In ND
                ws.Cells(idRHStart + 2, 7 + idSub + CPar).Value = dr2("PMSubDesc").ToString()
                idSub = idSub + 1
            Next
            ws.Cells(idRHStart + 1, 7 + CPar, idRHStart + 1, 7 + idSub + CPar).Merge = True
            ws.Cells(idRHStart + 2, 7 + idSub + CPar).Value = "Total"

            CPar = CPar + nSubPar + 1

        Next

        ws.Cells(idRHStart + 1, ILastCol).Value = "Total Score"
        ws.Cells(idRHStart + 1, ILastCol, idRHStart + 2, ILastCol).Merge = True




        'Fill Dealer
        Dim No As Integer = 1
        Dim idxD As Integer = 10 ' Start Dealer Data

        If Not IsNothing(dtFiltered) AndAlso dtFiltered.Rows.Count > 0 Then
            For Each drD As DataRow In dtDealer.Rows
                ws.Cells(idxD, 1).Value = No.ToString()
                ws.Cells(idxD, 2).Value = drD("DealerCode").ToString()
                ws.Cells(idxD, 3).Value = drD("GroupName").ToString()
                ws.Cells(idxD, 4).Value = drD("DealerName").ToString()

                ws.Cells(idxD, 5).Value = drD("CityName").ToString()
                ws.Cells(idxD, 6).Value = drD("AreaDesc").ToString()

                'Fill TotalSub

                CPar = 0
                For Each drP As DataRow In dtParam.Rows
                    Dim str2CFPID As String = drP("CFPID").ToString()

                    Dim ND As DataRow() = dtSubParam.Select(String.Format(" CFPID = '{0}'", str2CFPID))
                    Dim nSubPar As Integer = 0
                    If Not IsNothing(ND) Then
                        nSubPar = ND.Length
                    End If



                    'Header Sub Param
                    Dim idSub As Integer = 0
                    'Sub Param
                    For Each drS As DataRow In ND
                        ws.Cells(idxD, 7 + idSub + CPar).Value = GetValSub(dt, drD("DealerCode").ToString(), str2CFPID, drS("CSPID").ToString())
                        idSub = idSub + 1
                    Next
                    ws.Cells(idxD, 7 + idSub + CPar).Value = GetValParam(dt, drD("DealerCode").ToString(), str2CFPID)
                    ws.Cells(idxD, ILastCol).Value = GetValSummary(dt, drD("DealerCode").ToString())

                    CPar = CPar + nSubPar + 1

                Next




                No = No + 1
                idxD = idxD + 1
            Next

        Else
            idxD = idxD + 2
        End If

        idxD = idxD - 1
        'Formating Common
        For cw As Integer = 1 To (ILastCol)
            ws.Column(1).AutoFit()
        Next

        'Formating Header
        ws.Cells(1, 1, idRHStart + 2, ILastCol).Style.Font.Bold = True
        ws.Cells(idRHStart, 1, idRHStart + 2, ILastCol).Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells(idRHStart, 1, idRHStart + 2, ILastCol).Style.Fill.BackgroundColor.SetColor(Color.LightGray)
        ws.Cells(idRHStart, 1, idRHStart + 2, ILastCol).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Cells(idRHStart, 1, idRHStart + 2, ILastCol).Style.VerticalAlignment = ExcelVerticalAlignment.Center

        'Formating Score

        ws.Cells(10, 7, idxD, ILastCol).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
        ws.Cells(10, 7, idxD, ILastCol).Style.VerticalAlignment = ExcelVerticalAlignment.Center

        ws.Cells(10, 7, idxD, ILastCol).Style.Numberformat.Format = "#,##0.0000"


        'Formating Table
        Dim modelTable = ws.Cells(idRHStart, 1, idxD, ILastCol)
        modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin
        modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin
        modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin
        modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin
        modelTable.AutoFitColumns()

    End Sub

    Private Function GetValSub(ByVal dt As DataTable, ByVal StrDealer As String, ByVal StraParam As String, ByVal StrSub As String) As Decimal
        Dim res As Decimal = 0

        Try
            Dim strFilter = String.Format(" DealerCode = '{0}' AND CFPID = '{1}' AND CSPID = '{2}'", StrDealer, StraParam, StrSub)
            Dim pp = dt.Compute("SUM(SubScore)", strFilter)

            res = CType(pp, Decimal)
        Catch ex As Exception
            Dim m = ex
        End Try

        Return res
    End Function


    Private Function GetValParam(ByVal dt As DataTable, ByVal StrDealer As String, ByVal StraParam As String) As Decimal
        Dim res As Decimal = 0

        Try
            Dim strFilter = String.Format(" DealerCode = '{0}' AND CFPID = '{1}'  ", StrDealer, StraParam)
            Dim pp As DataRow() = dt.Select(strFilter)

            For Each dr As DataRow In pp
                res = CType(dr("PMScore"), Decimal)
                Exit For
            Next

        Catch ex As Exception
            Dim m = ex
        End Try

        Return res
    End Function

    Private Function GetValSummary(ByVal dt As DataTable, ByVal StrDealer As String) As Decimal
        Dim res As Decimal = 0

        Try
            Dim strFilter = String.Format(" DealerCode = '{0}'  ", StrDealer)
            Dim pp As DataRow() = dt.Select(strFilter)

            For Each dr As DataRow In pp
                res = CType(dr("SummaryScore"), Decimal)
                Exit For
            Next

        Catch ex As Exception
            Dim m = ex
        End Try

        Return res
    End Function

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Authorization()

        If Not IsPostBack Then
            If Me.IsKTB Then
                lblSearchDealer.Attributes("onclick") = "ShowDealerSelection();"
            Else
                lbtnPopupHistory.Visible = False
                lblSearchDealer.Attributes("onclick") = String.Format("ShowDealerSelectionGroup('{0}');", Me.GetDealer.DealerGroup.ID.ToString)
                btnRecalculate.Visible = False
            End If

            GetServiceTypeList()
        End If

        BindDdlFormula()
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Me.DownloadReport()
        'If ddlServiceType.SelectedIndex = 0 Then
        '    MessageBox.Show("Silahkan Pilih Jenis Customer")
        'Else
        '    Me.DownloadReport()
        'End If



    End Sub

    Private Sub BindDdlFormula()
        ddlFormula.Items.Clear()
        Dim selectedPeriod As Date = icPeriod.Value
        Dim yearMonth As String = selectedPeriod.Year & selectedPeriod.Month.ToString().PadLeft(2, "0")

        Dim selectedCcPeriod As CcPeriod = New CcPeriodFacade(User).Retrieve(yearMonth)


        If selectedCcPeriod.ID <> 0 Then
            Dim strInsert As String = "(SELECT ID FROM CcCSPerformanceMaster WHERE RowStatus = 0 AND " & selectedCcPeriod.ID & " >= CcPeriodIDFrom AND " & selectedCcPeriod.ID & " <= CcPeriodIDTo)"
            Dim criterias As New CriteriaComposite(New Criteria(GetType(CcCSPerformanceMaster), "ID", MatchType.InSet, strInsert))

            Dim arlResult As ArrayList = New CcCSPerformanceMasterFacade(User).Retrieve(criterias)

            For Each Master As CcCSPerformanceMaster In arlResult
                ddlFormula.Items.Add(New ListItem(Master.Description, Master.ID))
            Next

        Else
            MessageBox.Show("Period Tidak ditemukan")
        End If


    End Sub

    Private Function MappingDataSetToList(ds As DataSet) As List(Of CcPerformanceReport)
        Dim result As New List(Of CcPerformanceReport)
        For Each dt As DataTable In ds.Tables
            For Each row As DataRow In dt.Rows
                Dim dat As New CcPerformanceReport
                dat.DealerCode = row("DealerCode").ToString()
                dat.DealerName = row("DealerName").ToString()
                dat.SearchTerm1 = row("SearchTerm1").ToString()
                dat.GroupName = row("GroupName").ToString()
                dat.CityName = row("CityName").ToString()
                dat.Description = row("Description").ToString()
                dat.YearMonth = row("YearMonth").ToString()
                dat.Code = row("Code").ToString()
                dat.ClusterName = row("ClusterName").ToString()
                dat.ParameterName = row("ParameterName").ToString()
                dat.SubParameterName = row("SubParameterName").ToString()
                dat.TotalScore = CDec(row("TotalScore"))
                dat.SummaryScore = CDec(row("SummaryScore"))
                dat.CSP_Ranking = CShort(row("CSP_Ranking"))
                result.Add(dat)
            Next
        Next
        Return result
    End Function

    Private ReadOnly Property Bulan As Dictionary(Of Integer, String)
        Get
            Dim dicBulan As New Dictionary(Of Integer, String)
            dicBulan.Add(1, "JANUARI")
            dicBulan.Add(2, "FEBRUARI")
            dicBulan.Add(3, "MARET")
            dicBulan.Add(4, "APRIL")
            dicBulan.Add(5, "MEI")
            dicBulan.Add(6, "JUNI")
            dicBulan.Add(7, "JULI")
            dicBulan.Add(8, "AGUSTUS")
            dicBulan.Add(9, "SEPTEMBER")
            dicBulan.Add(10, "OKTOBER")
            dicBulan.Add(11, "NOVEMBER")
            dicBulan.Add(12, "DESEMBER")
            Return dicBulan
        End Get
    End Property

    Private Sub GenerateTablePerCluster(pck As ExcelPackage, cluster As String, listPerCluster As List(Of CcPerformanceReport))
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("GRADE " & cluster)
        listPerCluster = listPerCluster.OrderBy(Function(x) x.CSP_Ranking).ToList()

        Dim listDealerCode As List(Of String) = listPerCluster.Select(Function(x) x.DealerCode).Distinct().ToList()

        Dim rowCode As Integer = 4
        'Dim rowCode As Integer = 4


        Dim objMaster As CcCSPerformanceMaster = New CcCSPerformanceMasterFacade(Me.User).Retrieve(CInt(ddlFormula.SelectedValue))

        ws.Cells(1, 1).Value = "DAFTAR PESERTA " + objMaster.Description
        ws.Cells(1, 1).Style.Font.Size = 16
        ws.Cells(1, 1, 1, 4).Merge = True
        ws.Cells(1, 1).Style.Font.Bold = True

        Dim funcPeriod As New CcPeriodFacade(Me.User)
        Dim startPeriode As CcPeriod = funcPeriod.Retrieve(objMaster.CcPeriodIDFrom)
        Dim endPeriode As CcPeriod = funcPeriod.Retrieve(icPeriod.Value.ToString("yyyyMM"))

        Dim pAwal As Date = New Date(CInt(startPeriode.YearMonth.Substring(0, 4)), CInt(startPeriode.YearMonth.Substring(4, 2)), 1)
        Dim pAkhir As Date = New Date(CInt(endPeriode.YearMonth.Substring(0, 4)), CInt(endPeriode.YearMonth.Substring(4, 2)), 1)

        Dim strAwal As String = Bulan.FirstOrDefault(Function(x) x.Key = pAwal.Month).Value + " " + pAwal.Year.ToString
        Dim strAkhir As String = Bulan.FirstOrDefault(Function(x) x.Key = pAkhir.Month).Value + " " + pAkhir.Year.ToString

        ws.Cells(2, 1).Value = String.Format("PERIODE : {0} s/d {1}", strAwal, strAkhir)
        ws.Cells(2, 1).Style.Font.Size = 12
        ws.Cells(2, 1, 2, 4).Merge = True
        ws.Cells(2, 1).Style.Font.Bold = True

        ws.Cells(rowCode, 1).Value = "Rank"
        ws.Cells(rowCode, 1, rowCode + 2, 1).Merge = True
        ws.Cells(rowCode, 1, rowCode + 2, 1).Style.Border.BorderAround(ExcelBorderStyle.Medium)
        ws.Cells(rowCode, 1, rowCode + 2, 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Cells(rowCode, 1, rowCode + 2, 1).Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells(rowCode, 1, rowCode + 2, 1).Style.Fill.BackgroundColor.SetColor(Color.LightGray)

        ws.Cells(rowCode, 2).Value = "Kode Dealer"
        ws.Cells(rowCode, 2, rowCode + 2, 2).Merge = True
        ws.Cells(rowCode, 2, rowCode + 2, 2).Style.Border.BorderAround(ExcelBorderStyle.Medium)
        ws.Cells(rowCode, 2, rowCode + 2, 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Cells(rowCode, 2, rowCode + 2, 2).Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells(rowCode, 2, rowCode + 2, 2).Style.Fill.BackgroundColor.SetColor(Color.LightGray)

        ws.Cells(rowCode, 3).Value = "Nickname Dealer"
        ws.Cells(rowCode, 3, rowCode + 2, 3).Merge = True
        ws.Cells(rowCode, 3, rowCode + 2, 3).Style.Border.BorderAround(ExcelBorderStyle.Medium)
        ws.Cells(rowCode, 3, rowCode + 2, 3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Cells(rowCode, 3, rowCode + 2, 3).Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells(rowCode, 3, rowCode + 2, 3).Style.Fill.BackgroundColor.SetColor(Color.LightGray)

        ws.Cells(rowCode, 4).Value = "Group Dealer"
        ws.Cells(rowCode, 4, rowCode + 2, 4).Merge = True
        ws.Cells(rowCode, 4, rowCode + 2, 4).Style.Border.BorderAround(ExcelBorderStyle.Medium)
        ws.Cells(rowCode, 4, rowCode + 2, 4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Cells(rowCode, 4, rowCode + 2, 4).Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells(rowCode, 4, rowCode + 2, 4).Style.Fill.BackgroundColor.SetColor(Color.LightGray)

        ws.Cells(rowCode, 5).Value = "Area"
        ws.Cells(rowCode, 5, rowCode + 2, 5).Merge = True
        ws.Cells(rowCode, 5, rowCode + 2, 5).Style.Border.BorderAround(ExcelBorderStyle.Medium)
        ws.Cells(rowCode, 5, rowCode + 2, 5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Cells(rowCode, 5, rowCode + 2, 5).Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells(rowCode, 5, rowCode + 2, 5).Style.Fill.BackgroundColor.SetColor(Color.LightGray)

        Dim rowParam As Integer = rowCode + 1
        Dim rowSubParam As Integer = rowCode + 2
        Dim colStartCode As Integer = 6
        Dim colStartParam As Integer = 6
        Dim colStartSubParam As Integer = 6

        Dim rowStartData As Integer = rowCode + 2
        Dim colorIndex As Integer = 0
        Dim colorCodeIndex As Integer = 0



        Dim listCode As List(Of String) = listPerCluster.Select(Function(x) x.Code).Distinct.ToList()

        For Each code As String In listCode
            Dim listDataPerCode As List(Of CcPerformanceReport) = listPerCluster.Where(Function(x) x.Code = code).ToList()
            Dim listParameter As List(Of String) = listDataPerCode.Select(Function(x) x.ParameterName).Distinct().ToList()

            If code <> "TOTAL" Then
                ws.Cells(rowCode, colStartCode).Value = code.ToUpper()
            End If

            For Each param As String In listParameter
                Dim listDataPerParam As List(Of CcPerformanceReport) = listDataPerCode.Where(Function(x) x.ParameterName = param And x.Code = code).ToList()
                Dim listSubParameterSales As List(Of String) = listDataPerParam.Select(Function(x) x.SubParameterName).Distinct().ToList()

                ws.Cells(rowParam, colStartParam).Value = param

                For Each subparam As String In listSubParameterSales

                    If subparam = "TOTAL" And param = "TOTAL" Then
                        If listParameter.Count = 1 Then
                            ws.Cells(rowCode, colStartParam).Value = param
                            ws.Cells(rowCode, colStartParam, rowSubParam, colStartSubParam).Merge = True
                            ws.Cells(rowCode, colStartParam, rowSubParam, colStartSubParam).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                            ws.Cells(rowCode, colStartParam, rowSubParam, colStartSubParam).Style.VerticalAlignment = ExcelVerticalAlignment.Center
                            ws.Cells(rowCode, colStartParam, rowSubParam, colStartSubParam).Style.Border.BorderAround(ExcelBorderStyle.Medium)
                            ws.Cells(rowCode, colStartParam, rowSubParam, colStartSubParam).Style.Fill.PatternType = ExcelFillStyle.Solid
                            ws.Cells(rowCode, colStartParam, rowSubParam, colStartSubParam).Style.Fill.BackgroundColor.SetColor(GetListColor(colorIndex))
                        Else
                            ws.Cells(rowParam, colStartParam, rowSubParam, colStartSubParam).Merge = True
                            ws.Cells(rowParam, colStartParam, rowSubParam, colStartSubParam).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                            ws.Cells(rowParam, colStartParam, rowSubParam, colStartSubParam).Style.VerticalAlignment = ExcelVerticalAlignment.Center
                            ws.Cells(rowParam, colStartParam, rowSubParam, colStartSubParam).Style.Border.BorderAround(ExcelBorderStyle.Medium)
                            ws.Cells(rowParam, colStartParam, rowSubParam, colStartSubParam).Style.Fill.PatternType = ExcelFillStyle.Solid
                            ws.Cells(rowParam, colStartParam, rowSubParam, colStartSubParam).Style.Fill.BackgroundColor.SetColor(GetListColor(colorIndex))

                        End If
                    Else

                        If code = "TOTAL" Then
                            ws.Cells(rowCode, colStartCode, rowSubParam, colStartSubParam).Merge = True
                            ws.Cells(rowCode, colStartCode, rowSubParam, colStartSubParam).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                            ws.Cells(rowCode, colStartCode, rowSubParam, colStartSubParam).Style.VerticalAlignment = ExcelVerticalAlignment.Center
                            ws.Cells(rowCode, colStartCode, rowSubParam, colStartSubParam).Style.Border.BorderAround(ExcelBorderStyle.Medium)
                            ws.Cells(rowCode, colStartCode, rowSubParam, colStartSubParam).Style.Fill.PatternType = ExcelFillStyle.Solid
                            ws.Cells(rowCode, colStartCode, rowSubParam, colStartSubParam).Style.Fill.BackgroundColor.SetColor(GetListColor(colorIndex))
                        Else
                            ws.Cells(rowSubParam, colStartSubParam).Value = subparam
                            ws.Cells(rowSubParam, colStartSubParam).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                            ws.Cells(rowSubParam, colStartSubParam).Style.Border.BorderAround(ExcelBorderStyle.Medium)
                            ws.Cells(rowSubParam, colStartSubParam).Style.Fill.PatternType = ExcelFillStyle.Solid
                            ws.Cells(rowSubParam, colStartSubParam).Style.Fill.BackgroundColor.SetColor(GetListColor(colorIndex))
                        End If
                    End If



                    'proses insert data disini dulu

                    For i As Integer = 1 To listDealerCode.Count
                        Dim dealerCode As String = listDealerCode(i - 1)
                        Dim dataInProcess As CcPerformanceReport = listPerCluster.FirstOrDefault(Function(x) x.DealerCode = dealerCode And x.SubParameterName = subparam And x.ParameterName = param And x.Code = code)
                        ws.Cells(rowStartData + i, 1).Value = dataInProcess.CSP_Ranking
                        ws.Cells(rowStartData + i, 2).Value = dataInProcess.DealerCode
                        ws.Cells(rowStartData + i, 3).Value = dataInProcess.DealerName
                        ws.Cells(rowStartData + i, 4).Value = dataInProcess.GroupName
                        ws.Cells(rowStartData + i, 5).Value = dataInProcess.Description
                        ws.Cells(rowStartData + i, colStartSubParam).Value = dataInProcess.TotalScore
                        ws.Cells(rowStartData + i, colStartSubParam).Style.Fill.PatternType = ExcelFillStyle.Solid

                        If dataInProcess.SubParameterName = "TOTAL" Then
                            ws.Cells(rowStartData + i, colStartSubParam).Style.Fill.BackgroundColor.SetColor(GetListColor(colorIndex))
                        Else
                            ws.Cells(rowStartData + i, colStartSubParam).Style.Fill.BackgroundColor.SetColor(Color.LightGray)
                        End If


                        'ws.Cells(rowStartData + i, 1, rowStartData + 1, colStartSubParam).Style.Border.BorderAround(ExcelBorderStyle.Medium)
                    Next

                    colStartSubParam = colStartSubParam + 1

                Next

                If param <> "TOTAL" Then
                    ws.Cells(rowParam, colStartParam, rowParam, colStartSubParam - 1).Merge = True
                    ws.Cells(rowParam, colStartParam, rowParam, colStartSubParam - 1).Style.Border.BorderAround(ExcelBorderStyle.Medium)
                    ws.Cells(rowParam, colStartParam, rowParam, colStartSubParam - 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                    ws.Cells(rowParam, colStartParam, rowParam, colStartSubParam - 1).Style.Fill.PatternType = ExcelFillStyle.Solid
                    ws.Cells(rowParam, colStartParam, rowParam, colStartSubParam - 1).Style.Fill.BackgroundColor.SetColor(GetListColor(colorIndex))
                End If


                colStartParam = colStartSubParam

                colorIndex = colorIndex + 1
                If colorIndex > 9 Then
                    colorIndex = 0
                End If

            Next

            If code <> "TOTAL" Then
                ws.Cells(rowCode, colStartCode, rowCode, colStartSubParam - 1).Merge = True
                ws.Cells(rowCode, colStartCode, rowCode, colStartSubParam - 1).Style.Border.BorderAround(ExcelBorderStyle.Medium)
                ws.Cells(rowCode, colStartCode, rowCode, colStartSubParam - 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                ws.Cells(rowCode, colStartCode, rowCode, colStartSubParam - 1).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(rowCode, colStartCode, rowCode, colStartSubParam - 1).Style.Fill.BackgroundColor.SetColor(GetListColorForCode(colorCodeIndex))
                ws.Cells(rowCode, colStartCode, rowCode, colStartSubParam - 1).Style.Font.Bold = True
                colorCodeIndex = colorCodeIndex + 1
            End If

        
            colStartCode = colStartSubParam

        Next

        For i As Integer = 1 To colStartSubParam
            ws.Column(i).AutoFit()
        Next



    End Sub

    Private Function GetListColorForCode() As List(Of System.Drawing.Color)
        Dim listColor As New List(Of System.Drawing.Color)

        listColor.Add(Color.Yellow)
        listColor.Add(Color.Orange)

        listColor.Add(Color.Blue)
        listColor.Add(Color.Green)

        Return listColor
    End Function

    Private Function GetListColor() As List(Of System.Drawing.Color)
        Dim listColor As New List(Of System.Drawing.Color)

        listColor.Add(Color.LightBlue)
        listColor.Add(Color.Salmon)
        listColor.Add(Color.LightPink)
        listColor.Add(Color.LightYellow)
        listColor.Add(Color.LightGreen)
        listColor.Add(Color.LightCoral)
        listColor.Add(Color.LightCyan)
        listColor.Add(Color.LightGoldenrodYellow)
        listColor.Add(Color.LightSteelBlue)
        listColor.Add(Color.LightSkyBlue)

        Return listColor
    End Function

    Private Sub btnRecalculate_Click(sender As Object, e As EventArgs) Handles btnRecalculate.Click
        Try
            Dim listCluster As List(Of CcCSPerformanceCluster) = GetListCluster()


            If listCluster.Count > 0 Then
                Dim facade As New CcCSPerformanceCalculationHistoryFacade(User)
                For Each cluster As CcCSPerformanceCluster In listCluster
                    Dim historyData As CcCSPerformanceCalculationHistory = MappingHistoryFromUIandCluster(cluster)
                    If Not IsExistHistory(historyData) Then
                        facade.Insert(historyData)
                    End If
                Next
                MessageBox.Show("Proses recalculate berhasil diregister.")
            Else
                MessageBox.Show("Proses recalculate gagal . Tidak ada data cluster dengan formula yang dipilih.")
            End If

           

        Catch ex As Exception
            MessageBox.Show("Gagal dalam recalculate data, Harap hubungi Team D-NET : " & ex.Message)
        End Try
    End Sub

    Private Function GetListCluster() As List(Of CcCSPerformanceCluster)
        Dim result As New List(Of CcCSPerformanceCluster)

        If txtDealerCode.Text.Trim = String.Empty Then
            Dim facade As New CcCSPerformanceClusterFacade(User)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCluster), "RowStatus", MatchType.Exact, CShort(DBRowStatus.Active)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCluster), "CcCSPerformanceMasterID", MatchType.Exact, (ddlFormula.SelectedValue)))
            Dim arlResult As ArrayList = facade.Retrieve(criterias)

            If arlResult.Count > 0 Then
                result = arlResult.Cast(Of CcCSPerformanceCluster).ToList()
            End If
        Else
            Dim facade As New CcCSPerformanceClusterDealerFacade(User)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceClusterDealer), "RowStatus", MatchType.Exact, CShort(DBRowStatus.Active)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceClusterDealer), "CcCSPerformanceCluster.CcCSPerformanceMaster.ID", MatchType.Exact, (ddlFormula.SelectedValue)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceClusterDealer), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))

            Dim arlResult As ArrayList = facade.Retrieve(criterias)
            If arlResult.Count > 0 Then
                For Each clusterDealer As CcCSPerformanceClusterDealer In arlResult
                    Dim existCluster As CcCSPerformanceCluster = result.FirstOrDefault(Function(x) x.ID = clusterDealer.CcCSPerformanceCluster.ID)
                    If existCluster Is Nothing Then
                        result.Add(existCluster)
                    End If
                Next
            End If
        End If

        Return result
    End Function

    Private Function MappingHistoryFromUIandCluster(cluster As CcCSPerformanceCluster) As CcCSPerformanceCalculationHistory
        Dim result As New CcCSPerformanceCalculationHistory
        result.CcCSPerformanceCluster = cluster
        result.CcPeriod = New CcPeriodFacade(User).Retrieve(icPeriod.Value.ToString("yyyyMM"))
        result.CcCSPerformanceMaster = New CcCSPerformanceMasterFacade(User).Retrieve(CInt(ddlFormula.SelectedValue))
        result.RequestedDate = GetRequestDateByAppConfig()
        Return result
    End Function

    Private Function IsExistHistory(historyData As CcCSPerformanceCalculationHistory) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "RowStatus", MatchType.Exact, CShort(DBRowStatus.Active)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "CcCSPerformanceMaster.ID", MatchType.Exact, (historyData.CcCSPerformanceMaster.ID)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "CcPeriod.ID", MatchType.Exact, (historyData.CcPeriod.ID)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "CcCSPerformanceCluster.ID", MatchType.Exact, (historyData.CcCSPerformanceCluster.ID)))

        Dim arlResult As ArrayList = New CcCSPerformanceCalculationHistoryFacade(User).Retrieve(criterias)

        If arlResult.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub lbtnPopupHistory_Click(sender As Object, e As EventArgs) Handles lbtnPopupHistory.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "RowStatus", MatchType.Exact, CShort(DBRowStatus.Active)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceCalculationHistory), "CcCSPerformanceMaster.ID", MatchType.Exact, ddlFormula.SelectedValue))

        Dim arlResult As ArrayList = New CcCSPerformanceCalculationHistoryFacade(User).Retrieve(criterias)

        If arlResult.Count > 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "ShowHistory(" & ddlFormula.SelectedValue & ")", True)
        Else
            MessageBox.Show("Tidak ada history recalculate pada formula yang dipilih.")
        End If



    End Sub

    Private Function GetRequestDateByAppConfig() As DateTime
        Try
            Dim facade As New AppConfigFacade(User)
            Dim day As Integer = CInt(facade.Retrieve("CSPRecalculateDay").Value)
            Dim hourConfig As String = facade.Retrieve("CSPRecalculateHour").Value
            Dim hourSplit() As String = hourConfig.Split(":")
            Dim hour As Integer = CInt(hourSplit(0))
            Dim minute As Integer = CInt(hourSplit(1))

            Dim tgl As Date = Date.Now.AddDays(day)

            Return New DateTime(tgl.Year, tgl.Month, tgl.Day, hour, minute, 0)

          
        Catch ex As Exception
            Return DateTime.Now.AddDays(1)
        End Try
    End Function

End Class