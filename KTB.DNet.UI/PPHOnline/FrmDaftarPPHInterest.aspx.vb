#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Globalization
Imports System.Linq
Imports System.Collections.Generic

#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.PO

Imports OfficeOpenXml
#End Region



Public Class FrmDaftarPPHInterest
    Inherits System.Web.UI.Page

    Private enumStatusPPHInterest As New Dictionary(Of Integer, String)
    Private oDealer As Dealer
    Private ListPriv As Boolean
    Private InputPriv As Boolean
    Private sessionName As String

#Region "Event Handler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = Session("DEALER")
        InitPriv()
        If Not IsPostBack Then
            Dim pageID As String = Guid.NewGuid().ToString()
            sessionName = pageID.Substring(pageID.Length = 10)
            ViewState("sessionName") = sessionName
            setControl()
            bindDdlStatus()
            bindDataGrid(0)
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Session("SEARCHCRITERIA" + ViewState("sessionName")) = Nothing
        If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB AndAlso txtDealerCode.Text = String.Empty Then
            MessageBox.Show("Silahkan Masukan Kode Dealer")
            Return
        End If

        Dim crits As CriteriaComposite

        crits = getSearchCriteria()
        Session("SEARCHCRITERIA" + ViewState("sessionName")) = crits
       

        dgListSOInterest.SelectedIndex = -1
        dgListSOInterest.CurrentPageIndex = 0
        bindDataGrid(0)
    End Sub

    Protected Sub dgListSOInterest_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListSOInterest.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim items As ArrayList = dgListSOInterest.DataSource
            Dim obj As VW_SalesOrderInterest = CType(items(e.Item.ItemIndex), VW_SalesOrderInterest)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblNoPO As Label = CType(e.Item.FindControl("lblNoPO"), Label)
            Dim lblSO As Label = CType(e.Item.FindControl("lblSO"), Label)
            Dim lblBilling As Label = CType(e.Item.FindControl("lblBilling"), Label)
            Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
            Dim lblPercentage As Label = CType(e.Item.FindControl("lblPercentage"), Label)
            Dim lblDPP As Label = CType(e.Item.FindControl("lblDPP"), Label)
            Dim lblPPH As Label = CType(e.Item.FindControl("lblPPH"), Label)
            Dim lblAfterPPH As Label = CType(e.Item.FindControl("lblAfterPPH"), Label)
            Dim lblBillingDate As Label = CType(e.Item.FindControl("lblBillingDate"), Label)
            Dim lblLastStatus As Label = CType(e.Item.FindControl("lblLastStatus"), Label)
            Dim lblLastSubmission As Label = CType(e.Item.FindControl("lblLastSubmission"), Label)
            Dim lblHistory As Label = CType(e.Item.FindControl("lblHistory"), Label)
            Dim lblDocNumber As Label = CType(e.Item.FindControl("lblDocNumber"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgListSOInterest.CurrentPageIndex * dgListSOInterest.PageSize)
            lblDealerCode.Text = obj.Dealer.DealerCode
            If Not IsNothing(obj.SalesOrder) Then
                lblSO.Text = obj.SalesOrder.SONumber
            End If
            lblNoPO.Text = obj.PONumber
            lblBilling.Text = obj.BillingNumber
            lblType.Text = obj.TrType
            Dim p As Decimal = 0
            If obj.PPHAmount <> 0 And obj.DPPAmount <> 0 Then
                p = obj.PPHAmount / obj.DPPAmount
            End If
            lblPercentage.Text = String.Format("{0:P2}", p)
            lblDPP.Text = obj.DPPAmount.ToString("#,###.00")
            lblPPH.Text = obj.PPHAmount.ToString("#,###.00")
            lblAfterPPH.Text = (obj.DPPAmount - obj.PPHAmount).ToString("#,###.00")
            lblBillingDate.Text = obj.BillingDate.ToString("dd/MM/yyyy")
            lblLastStatus.Text = enumStatusPPHInterest(obj.Status)
            lblLastSubmission.Text = obj.NoReg
            lblHistory.Text = "history"
            lblDocNumber.Text = obj.DocNumber
        End If
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub

    Protected Sub dgListSOInterest_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgListSOInterest.SelectedIndexChanged

    End Sub

    Protected Sub dgListSOInterest_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgListSOInterest.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgListSOInterest.SelectedIndex = -1
        dgListSOInterest.CurrentPageIndex = 0
        bindGridSorting(dgListSOInterest.CurrentPageIndex)
    End Sub

    Protected Sub dgListSOInterest_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgListSOInterest.PageIndexChanged
        dgListSOInterest.SelectedIndex = -1
        dgListSOInterest.CurrentPageIndex = e.NewPageIndex
        bindDataGrid(dgListSOInterest.CurrentPageIndex)
    End Sub

#End Region

    Private Sub InitPriv()
        ListPriv = SecurityProvider.Authorize(Context.User, SR.SOPPhInterest_List_Privilage)
        InputPriv = SecurityProvider.Authorize(Context.User, SR.SOPPhInterest_input_Privilage)
    
        If Not InputPriv OrElse oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            btnGenerateBuktiPotong.Visible = False
        End If

        If Not ListPriv Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Status DO - Daftar SO Interest")
        End If
    End Sub

    Private Sub setControl()
        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            btnGenerateBuktiPotong.Visible = False
        Else
            txtDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Attributes.Add("readonly", "readonly")
        End If
        Dim TT As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
        icPeriodStart.Value = TT.AddMonths(-1)
        icPeriodEnd.Value = TT.AddDays(-1)
    End Sub

    Private Sub bindDdlStatus()
        Dim crit As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "enumStatusPPHInterest"))
        Dim arrStatus As ArrayList = New StandardCodeFacade(User).Retrieve(crit)

        ddlStatus.Items.Add(New ListItem("Semua", -2))
        For Each s As StandardCode In arrStatus
            ddlStatus.Items.Add(New ListItem(s.ValueDesc, s.ValueId))
            enumStatusPPHInterest(s.ValueId) = s.ValueDesc
        Next
        Session("ENUMSTATUS" + ViewState("sessionName")) = enumStatusPPHInterest
    End Sub

    Private Sub bindDataGrid(ByVal pgIndex As Integer)
        Dim crits As CriteriaComposite
        crits = Session("SEARCHCRITERIA" + ViewState("sessionName"))
        If IsNothing(crits) Then
            crits = getSearchCriteria()
            Session("SEARCHCRITERIA" + ViewState("sessionName")) = crits
        Else
            crits = Session("SEARCHCRITERIA" + ViewState("sessionName"))
        End If
        Dim totRow As Integer = 0
        Dim arrData As ArrayList = New VW_SalesOrderInterestFacade(User).RetrieveActiveList(crits, pgIndex + 1, dgListSOInterest.PageSize, totRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgListSOInterest.VirtualItemCount = totRow

        Session("ARRDATA" + ViewState("sessionName")) = arrData
        enumStatusPPHInterest = CType(Session("ENUMSTATUS" + ViewState("sessionName")), Dictionary(Of Integer, String))
        dgListSOInterest.DataSource = arrData
        dgListSOInterest.DataBind()
    End Sub

    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            enumStatusPPHInterest = CType(Session("ENUMSTATUS" + ViewState("sessionName")), Dictionary(Of Integer, String))
            dgListSOInterest.DataSource = New VW_SalesOrderInterestFacade(User).RetrieveActiveList(CType(Session("SEARCHCRITERIA" + ViewState("sessionName")), CriteriaComposite), indexPage + 1, dgListSOInterest.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgListSOInterest.VirtualItemCount = totalRow
            dgListSOInterest.DataBind()
        End If

    End Sub

    Private Function getSearchCriteria() As CriteriaComposite
        oDealer = Session("DEALER")
        Dim crit As New CriteriaComposite(New Criteria(GetType(VW_SalesOrderInterest), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "BillingDate", MatchType.GreaterOrEqual, icPeriodStart.Value))
        crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "BillingDate", MatchType.LesserOrEqual, icPeriodEnd.Value))
        If Not txtDealerCode.Text.Trim = String.Empty Then
            crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Trim.Replace(";", "','").Trim() & "')"))
        Else
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(oDealer, User)))
            End If
        End If


        If Not ddlStatus.SelectedValue = -2 Then
            crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If Not txtSONumber.Text.Trim = String.Empty Then
            crit.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "SONumber", MatchType.Exact, txtSONumber.Text.Trim))
        End If

        Return crit
    End Function

    Private Sub SetDownload()

        Dim arrData As New ArrayList

        If dgListSOInterest.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        arrData = getSelectedData()

        If arrData.Count > 0 Then
            Dim strFileName As String = "Daftar PPH Interest"
            CreateExcel(strFileName, arrData)
        Else
            MessageBox.Show("Tidak ada data yang dipilih")
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        enumStatusPPHInterest = CType(Session("ENUMSTATUS" + ViewState("sessionName")), Dictionary(Of Integer, String))
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Dealer Code"
            ws.Cells("C3").Value = "No PO"
            ws.Cells("D3").Value = "SO"
            ws.Cells("E3").Value = "Billing No"
            ws.Cells("F3").Value = "Tipe"
            ws.Cells("G3").Value = "%"
            ws.Cells("H3").Value = "DPP"
            ws.Cells("I3").Value = "PPH"
            ws.Cells("J3").Value = "Nilai Setelah PPH"
            ws.Cells("K3").Value = "Status Terakhir"
            ws.Cells("L3").Value = "Nomor Pengajuan terakhir"
            ws.Cells("M3").Value = "Tanggal Billing"
            ws.Cells("N3").Value = "Doc Number"

            ws.Cells("A3:Q3").Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            ws.Cells("A3:Q3").Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen)
            ws.Cells("A3:Q3").AutoFitColumns()

            Dim rowStart As Integer = 4

            For i As Integer = 0 To Data.Count - 1

                Dim itemDetail As VW_SalesOrderInterest = Data(i)



                ws.Cells(String.Format("A{0}", rowStart)).Value = rowStart - 3
                If Not IsNothing(itemDetail.Dealer) Then
                    ws.Cells(String.Format("B{0}", rowStart)).Value = itemDetail.Dealer.DealerCode
                End If
                ws.Cells(String.Format("C{0}", rowStart)).Value = itemDetail.PONumber
                ws.Cells(String.Format("D{0}", rowStart)).Value = itemDetail.SONumber
                ws.Cells(String.Format("E{0}", rowStart)).Value = itemDetail.BillingNumber
                ws.Cells(String.Format("F{0}", rowStart)).Value = itemDetail.TrType
                ws.Cells(String.Format("G{0}", rowStart)).Value = "15%"
                ws.Cells(String.Format("H{0}", rowStart)).Value = itemDetail.DPPAmount
                ws.Cells(String.Format("H{0}", rowStart)).Style.Numberformat.Format = "#,##0"

                ws.Cells(String.Format("I{0}", rowStart)).Value = itemDetail.PPHAmount
                ws.Cells(String.Format("I{0}", rowStart)).Style.Numberformat.Format = "#,##0"
                ws.Cells(String.Format("J{0}", rowStart)).Value = itemDetail.DPPAmount - itemDetail.PPHAmount
                ws.Cells(String.Format("J{0}", rowStart)).Style.Numberformat.Format = "#,##0"
                ws.Cells(String.Format("K{0}", rowStart)).Value = enumStatusPPHInterest(itemDetail.Status)
                If Not IsNothing(itemDetail.InterestPPHHeader) Then
                    ws.Cells(String.Format("L{0}", rowStart)).Value = itemDetail.InterestPPHHeader.NoReg
                End If
                ws.Cells(String.Format("M{0}", rowStart)).Value = itemDetail.BillingDate
                ws.Cells(String.Format("M{0}", rowStart)).Style.Numberformat.Format = "dd/MM/yyyy"
                ws.Cells(String.Format("N{0}", rowStart)).Value = itemDetail.DocNumber
                'ws.Cells(String.Format("L{0}", rowStart)).Value = "History"
                rowStart += 1
            Next

            CreateExcelFile(pck, FileName & "_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xlsx")
        End Using

    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        'Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=""{0}""; size={1}; creation-date={2}; modification-date={2}; read-date={2}", fileName, fileBytes.Length, DateTime.Now.ToString("R")))
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        Response.ContentType = "application/vnd.ms-excel"
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub

    Protected Sub btnGenerateBuktiPotong_Click(sender As Object, e As EventArgs) Handles btnGenerateBuktiPotong.Click
        Dim arrToGenerate As ArrayList = getSelectedData()
        Dim statusAllowedToGenerate As Integer() = {-1, 2}
        For Each dt As VW_SalesOrderInterest In arrToGenerate
            If Not statusAllowedToGenerate.Contains(dt.Status) Then
                MessageBox.Show("Hanya data dengan status baru dan tolak yang dapat dilakukan pengajuan")
                Return
            End If
        Next
        If arrToGenerate.Count = 0 Then
            MessageBox.Show("Tidak ada data yang dipilih")
            Return
        End If

        Session("ARRSOINTERESTTOGENERATE" + ViewState("sessionName")) = arrToGenerate
        Server.Transfer("../PPHOnline/FrmInputBuktiPotong.aspx?mode=generate&sessionID=" + ViewState("sessionName"))
    End Sub

    Private Function getSelectedData() As ArrayList
        Dim arrData As ArrayList = Session("ARRDATA" + ViewState("sessionName"))
        Dim resData As New ArrayList()
        Dim countChecked As Integer = 0
        For Each row As DataGridItem In dgListSOInterest.Items

            Dim cbSelected As CheckBox = row.FindControl("chkAdd")

            If cbSelected.Checked = True Then
                Dim objToAdd As VW_SalesOrderInterest = CType(arrData(countChecked), VW_SalesOrderInterest)
                resData.Add(objToAdd)
            End If
            countChecked = countChecked + 1
        Next

        Return resData
    End Function

End Class