Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.Utility.CommonFunction
Imports KTB.DNet.BusinessFacade.PO
Imports OfficeOpenXml
Imports System.Linq

Public Class FrmLeadTimeOfDelivery
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Private criteriadownload As String = "FrmLeadTimeOfDelivery.criteriadownload"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadData()
        End If
    End Sub

    Private Sub loadData(Optional criteria As CriteriaComposite = Nothing, Optional indexPage As Integer = 0)
        Dim criterias As CriteriaComposite
        If Not IsNothing(criteria) Then
            criterias = criteria
        Else
            criterias = New CriteriaComposite(New Criteria(GetType(PODestination), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        End If

        Dim arrNew As ArrayList = New PODestinationFacade(User).Retrieve(criterias)
        dgDeliveryOrder.CurrentPageIndex = indexPage
        dgDeliveryOrder.DataSource = arrNew
        dgDeliveryOrder.DataBind()
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        criteriaSearch()
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        criteriaSearch()
        SetDownload()
    End Sub

    Private Sub criteriaSearch()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODestination), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeDest.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(PODestination), "Code", MatchType.Partial, txtKodeDest.Text))
        End If

        If txtKodeDealer.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(PODestination), "Dealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))
        End If

        If TxtDealerDestination.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(PODestination), "DealerDestinationCode.DealerCode", MatchType.Partial, TxtDealerDestination.Text))
        End If
        sessHelper.SetSession(criteriadownload, criterias)
        loadData(criterias)
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgDeliveryOrder.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelper.GetSession(criteriadownload)) Then
            crits = CType(sessHelper.GetSession(criteriadownload), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New PODestinationFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcel("Master Data - Lead Time Pengiriman", arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Kode Destinasi"
            ws.Cells("C3").Value = "Kode Dealer"
            ws.Cells("D3").Value = "Kode Dealer Destinasi"
            ws.Cells("E3").Value = "Nama"
            ws.Cells("F3").Value = "Alamat"
            ws.Cells("G3").Value = "City"
            ws.Cells("H3").Value = "Region"
            ws.Cells("I3").Value = "Lead Time"

            For i As Integer = 0 To Data.Count - 1
                Dim oPODestination As PODestination = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = oPODestination.Code
                If IsNothing(oPODestination.Dealer) Then
                    ws.Cells(i + 4, 3).Value = ""
                Else
                    ws.Cells(i + 4, 3).Value = oPODestination.Dealer.DealerCode
                End If
                If IsNothing(oPODestination.DealerDestinationCode) Then
                    ws.Cells(i + 4, 4).Value = ""
                Else
                    ws.Cells(i + 4, 4).Value = oPODestination.DealerDestinationCode.DealerCode
                End If
                ws.Cells(i + 4, 5).Value = oPODestination.Nama
                ws.Cells(i + 4, 6).Value = oPODestination.Alamat
                If IsNothing(oPODestination.City) Then
                    ws.Cells(i + 4, 7).Value = ""
                Else
                    ws.Cells(i + 4, 7).Value = oPODestination.City.CityName
                End If
                ws.Cells(i + 4, 8).Value = oPODestination.RegionDesc
                ws.Cells(i + 4, 9).Value = oPODestination.LeadTime
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
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()
    End Sub

    Protected Sub dgDeliveryOrder_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDeliveryOrder.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim RowValue As PODestination = CType(e.Item.DataItem, PODestination)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDeliveryOrder.CurrentPageIndex * dgDeliveryOrder.PageSize)
        End If
    End Sub

    Private Sub dgDeliveryOrder_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDeliveryOrder.PageIndexChanged
        dgDeliveryOrder.SelectedIndex = -1
        dgDeliveryOrder.CurrentPageIndex = e.NewPageIndex
        loadData(sessHelper.GetSession(criteriadownload), dgDeliveryOrder.CurrentPageIndex)
    End Sub
End Class