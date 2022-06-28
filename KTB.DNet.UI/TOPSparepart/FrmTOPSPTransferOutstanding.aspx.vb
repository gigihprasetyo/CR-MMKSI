#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports OfficeOpenXml
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Linq

#End Region

Public Class FrmTOPSPTransferOutstanding
    Inherits System.Web.UI.Page

    Private oDealer As Dealer
    Private _sessHelper As New SessionHelper
    Private _sessCrit As String = "FrmTOPSPTransferOutstanding.SearchCriteria"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(Session("DEALER"), Dealer)

        If Not IsPostBack Then
            bindDDLBankCode()

            ccTransferDateStart.Value = DateAdd(DateInterval.Day, -30, Date.Now.Date)
            ccTransferDateEnd.Value = Date.Now.Date

            dgSPOutstanding.DataSource = New ArrayList
            dgSPOutstanding.DataBind()
        End If
    End Sub

    Private Sub bindDDLBankCode()
        Dim critBank As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Bank), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dSource As ArrayList = New BankFacade(User).Retrieve(critBank)
        With ddlBankCode
            .Items.Clear()
            .DataSource = dSource
            .DataValueField = "BankCode"
            .DataTextField = "BankName"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
            .SelectedIndex = 0
        End With
    End Sub


    Protected Sub dgSPOutstanding_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgSPOutstanding.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblKodeBank As Label = CType(e.Item.FindControl("lblKodeBank"), Label)
            Dim lblReffBank As Label = CType(e.Item.FindControl("lblReffBank"), Label)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblTglTransfer As Label = CType(e.Item.FindControl("lblTglTransfer"), Label)
            Dim lblJmlTransfer As Label = CType(e.Item.FindControl("lblJmlTransfer"), Label)
            Dim lblKet As Label = CType(e.Item.FindControl("lblKet"), Label)
            Dim lblNoTR As Label = CType(e.Item.FindControl("lblNoTR"), Label)
            Dim lblTransaksi As Label = CType(e.Item.FindControl("lblTransaksi"), Label)

            Dim rowValue As TOPSPTransferOutstanding = CType(e.Item.DataItem, TOPSPTransferOutstanding)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgSPOutstanding.PageSize * dgSPOutstanding.CurrentPageIndex)
            If Not IsNothing(rowValue.Bank) Then
                lblKodeBank.Text = rowValue.Bank.BankCode
            Else
                lblKodeBank.Text = ""
            End If
            lblReffBank.Text = rowValue.RefBank
            lblNoReg.Text = rowValue.RegNumber
            lblTglTransfer.Text = rowValue.TransferDate.ToString("dd/MM/yyyy")
            lblJmlTransfer.Text = rowValue.TransferAmount.ToString("N0")
            lblKet.Text = rowValue.Narrative.ToString
            lblNoTR.Text = rowValue.TRNo.ToString
            lblTransaksi.Text = EnumDNET.GetTOPSPTransferOutStandingStringValue(rowValue.IDTransaction).ToString

        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferOutstanding), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        SearchCriteria(crit)
        _sessHelper.SetSession(_sessCrit, crit)
        bindGrid(0)
    End Sub

    Private Sub bindGrid(ByVal index As Integer)
        Dim crit As CriteriaComposite = Nothing
        If Not IsNothing(_sessHelper.GetSession(_sessCrit)) Then
            crit = CType(_sessHelper.GetSession(_sessCrit), CriteriaComposite)
            Dim totalRow As Integer = 0
            Dim arlData As ArrayList = New TOPSPTransferOutstandingFacade(User).RetrieveActiveList(index, dgSPOutstanding.PageSize, totalRow, "LastUpdatedTime", Sort.SortDirection.DESC, crit)
            If arlData.Count > 0 Then
                dgSPOutstanding.DataSource = arlData
                dgSPOutstanding.VirtualItemCount = totalRow
                dgSPOutstanding.DataBind()

                Dim GrandTotalAmount As Double = 0
                For Each dtAmount As TOPSPTransferOutstanding In arlData
                    GrandTotalAmount = GrandTotalAmount + dtAmount.TransferAmount
                Next
                lblTotalAmount.Text = GrandTotalAmount.ToString("N0")
            Else
                MessageBox.Show("Data tidak ditemukan")
                dgSPOutstanding.DataSource = New ArrayList
                dgSPOutstanding.DataBind()
            End If
        Else
            MessageBox.Show("Data tidak ditemukan")
            dgSPOutstanding.DataSource = New ArrayList
            dgSPOutstanding.DataBind()
        End If
    End Sub

    Private Sub SearchCriteria(ByRef crit As CriteriaComposite)
        If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            crit.opAnd(New Criteria(GetType(TOPSPTransferOutstanding), "Dealer.DealerGroup.ID", MatchType.Exact, oDealer.DealerGroup.ID))
        End If

        If ddlBankCode.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(TOPSPTransferOutstanding), "Bank.BankCode", MatchType.Partial, ddlBankCode.SelectedValue))
        End If

        If txtNoReg.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(TOPSPTransferOutstanding), "RegNumber", MatchType.Partial, txtNoReg.Text.Trim))
        End If

        If txtNoTR.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(TOPSPTransferOutstanding), "TRNo", MatchType.Partial, txtNoTR.Text.Trim))
        End If

        If txtKeterangan.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(TOPSPTransferOutstanding), "Narrative", MatchType.Partial, txtKeterangan.Text.Trim))
        End If

        If ddlTransaksi.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(TOPSPTransferOutstanding), "IDTransaction", MatchType.Exact, ddlTransaksi.SelectedValue))
        End If

        crit.opAnd(New Criteria(GetType(TOPSPTransferOutstanding), "TransferDate", MatchType.GreaterOrEqual, Format(ccTransferDateStart.Value, "yyyy-MM-dd 00:00:00")))
        crit.opAnd(New Criteria(GetType(TOPSPTransferOutstanding), "TransferDate", MatchType.LesserOrEqual, Format(ccTransferDateEnd.Value, "yyyy-MM-dd 23:59:59")))
    End Sub

    Protected Sub dgSPOutstanding_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgSPOutstanding.PageIndexChanged
        dgSPOutstanding.CurrentPageIndex = e.NewPageIndex
        bindGrid(e.NewPageIndex + 1)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        DownloadExcel()
    End Sub

    Private Sub DownloadExcel()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgSPOutstanding.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(_sessHelper.GetSession(_sessCrit)) Then
            Dim criteria As CriteriaComposite = _sessHelper.GetSession(_sessCrit)
            arrData = New TOPSPTransferOutstandingFacade(User).Retrieve(criteria)
        End If

        If arrData.Count > 0 Then
            CreateExcel("ReportTOPCOD", arrData)
        End If
    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No" '1
            ws.Cells("B3").Value = "Kode Bank" '2
            ws.Cells("C3").Value = "Reff Bank" '3
            ws.Cells("D3").Value = "No. Registrasi" '4
            ws.Cells("E3").Value = "Tgl Transfer" '5
            ws.Cells("F3").Value = "Jml Transfer" '6
            ws.Cells("G3").Value = "Narrative/Keterangan" '7
            ws.Cells("H3").Value = "No TR" '8
            ws.Cells("I3").Value = "Transaksi" '9

            Dim idx As Integer = 0
            For i As Integer = 0 To Data.Count - 1
                Dim item As TOPSPTransferOutstanding = Data(i)
                ws.Cells(idx + 4, 1).Value = idx + 1
                ws.Cells(idx + 4, 2).Value = item.Bank.BankCode
                ws.Cells(idx + 4, 3).Value = item.RefBank
                ws.Cells(idx + 4, 4).Value = item.RegNumber
                ws.Cells(idx + 4, 5).Value = item.TransferDate
                ws.Column(5).Style.Numberformat.Format = "DD/MM/YYYY"
                ws.Cells(idx + 4, 6).Value = item.TransferAmount.ToString("N0")
                ws.Cells(idx + 4, 7).Value = item.Narrative
                ws.Cells(idx + 4, 8).Value = item.TRNo
                ws.Cells(idx + 4, 9).Value = EnumDNET.GetTOPSPTransferOutStandingStringValue(item.IDTransaction)
                idx = idx + 1
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
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
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub
End Class