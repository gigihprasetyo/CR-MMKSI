#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
Imports OfficeOpenXml
Imports KTB.DNet.BusinessFacade.FinishUnit

#End Region


Public Class FrmDaftarBenefitClaimDeducted
    Inherits System.Web.UI.Page

    Private objDealer As New Dealer
    Private objSessionHelper As New SessionHelper
    Private displayPriv As Boolean
    Private strFileNm As String
    Private strFileNmHeader As String

    Private Sub initPages()
        If Not SecurityProvider.Authorize(Context.User, SR.Sales_Campaign_Daftar_Reduksi_Claim_Lihat_privillage) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - DAFTAR REDUKSI CLAIM")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.Sales_Campaign_Daftar_Reduksi_Claim_Lihat_privillage)
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtDealerCode.Visible = True
            lblDealerCode.Visible = False
            lblPopUpDealer.Visible = True
        Else
            txtDealerCode.Text = objDealer.DealerCode
            lblDealerCode.Text = objDealer.DealerCode & " / " & objDealer.DealerName

            txtDealerCode.Visible = False
            lblDealerCode.Visible = True
            lblPopUpDealer.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        initPages()

        If Not IsPostBack Then
            GetDataDeducted()
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        GetDataDeducted()
    End Sub

    Private Function BindDataGrid() As List(Of V_BenefitClaimDeducted)
        Dim restData As List(Of V_BenefitClaimDeducted) = New List(Of V_BenefitClaimDeducted)

        Dim criteria As New CriteriaComposite(New Criteria(GetType(V_BenefitClaimDeducted), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtRegNumber.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(V_BenefitClaimDeducted), "RegNumberDSF", MatchType.Exact, txtRegNumber.Text.Trim))
        End If
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtDealerCode.Text.Trim <> "" Then
                Dim strKodeDealerIn As String = String.Empty
                Dim str() As String = txtDealerCode.Text.Trim().Split(";")
                For Each strCode As String In str
                    If strKodeDealerIn = String.Empty Then
                        strKodeDealerIn = "('" & strCode.Trim & "'"
                    Else
                        strKodeDealerIn += ",'" & strCode.Trim & "'"
                    End If
                Next
                strKodeDealerIn += ")"
                criteria.opAnd(New Criteria(GetType(V_BenefitClaimDeducted), "DealerCode", MatchType.InSet, strKodeDealerIn))
            End If
        Else
            criteria.opAnd(New Criteria(GetType(V_BenefitClaimDeducted), "DealerCode", MatchType.Partial, lblDealerCode.Text.Trim.Split("/")(0).Trim))
        End If

        restData = New V_BenefitClaimDeductedFacade(User).Retrieve(criteria).Cast(Of V_BenefitClaimDeducted).ToList()
        Dim dataDeducted As List(Of String) = restData.Select(Function(x) x.ID.ToString()).Distinct().ToList()
        objSessionHelper.SetSession("dataHeader", restData)

        If Not restData.Count.Equals(0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(V_BenefitClaimDeductedHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_BenefitClaimDeductedHistory), "V_BenefitClaimDeducted.ID", MatchType.InSet, dataDeducted.GenerateInSet))

            Dim dataHistory As List(Of V_BenefitClaimDeductedHistory) = New V_BenefitClaimDeductedHistoryFacade(Me.User).Retrieve(criterias).Cast(Of  _
                V_BenefitClaimDeductedHistory).ToList()
            objSessionHelper.SetSession("dataDetail", dataHistory)
        End If

        Return restData
    End Function

    Private Sub GetDataDeducted()
        Dim data As List(Of V_BenefitClaimDeducted) = BindDataGrid().ToList()
        If data.Count > 25 Then
            dtgHeader.PageSize = data.Count
        End If

        dtgHeader.DataSource = data
        dtgHeader.DataBind()
    End Sub

    Private Sub dtgHeader_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgHeader.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim data As V_BenefitClaimDeducted = CType(e.Item.DataItem, V_BenefitClaimDeducted)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblClaimRegNoDsf As Label = CType(e.Item.FindControl("lblClaimRegNoDsf"), Label)
            Dim lblClaimRegNoDealer As Label = CType(e.Item.FindControl("lblClaimRegNoDealer"), Label)
            Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
            Dim lblDeductedAmount As Label = CType(e.Item.FindControl("lblDeductedAmount"), Label)
            Dim lblRemainAmount As Label = CType(e.Item.FindControl("lblRemainAmount"), Label)

            Dim dataDetail As DataGrid = CType(e.Item.FindControl("dtgDetail"), DataGrid)

            lblNo.Text = e.Item.ItemIndex + 1 + (dtgHeader.CurrentPageIndex * dtgHeader.PageSize)
            lblDealerCode.Text = data.DealerCode
            lblDealerName.Text = data.DealerName
            lblClaimRegNoDsf.Text = data.RegNumberDSF
            lblClaimRegNoDealer.Text = data.RegNumberDealer
            lblChassisNumber.Text = data.ChassisNumber
            lblDeductedAmount.Text = data.DeductedAmount.ToString("#,##0")
            lblRemainAmount.Text = data.RemainAmount.ToString("#,##0")

            Dim dataReview As List(Of V_BenefitClaimDeductedHistory) = DataV_BenefitClaimDeductedHistory(data)

            AddHandler dataDetail.ItemDataBound, New System.Web.UI.WebControls.DataGridItemEventHandler(AddressOf dtgDetail_ItemDataBound)

            dataDetail.DataSource = dataReview
            dataDetail.DataBind()
        End If

    End Sub

    Private Function DataV_BenefitClaimDeductedHistory(ByVal Data As V_BenefitClaimDeducted) As List(Of V_BenefitClaimDeductedHistory)
        Dim dataHistory As List(Of V_BenefitClaimDeductedHistory) = CType(objSessionHelper.GetSession("dataDetail"), List(Of V_BenefitClaimDeductedHistory))
        Dim dataHistory2 As List(Of V_BenefitClaimDeductedHistory) = dataHistory.Where(Function(x) x.V_BenefitClaimDeducted.ID.Equals(Data.ID) And x.RowStatus = CType(DBRowStatus.Active, Short)).ToList()

        Return dataHistory2
    End Function

    Private Sub dtgDetail_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As V_BenefitClaimDeductedHistory = CType(e.Item.DataItem, V_BenefitClaimDeductedHistory)

            Dim lblClaimRegNoDealer As Label = CType(e.Item.FindControl("lblClaimRegNoDealer"), Label)
            Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)
            Dim lblAmountDeducted As Label = CType(e.Item.FindControl("lblAmountDeducted"), Label)
            Dim lblCreatedTime As Label = CType(e.Item.FindControl("lblCreatedTime"), Label)

            lblClaimRegNoDealer.Text = RowValue.ClaimRegNo
            lblDescription.Text = RowValue.Description
            lblAmountDeducted.Text = RowValue.AmountDeducted.ToString("#,##0")
            lblCreatedTime.Text = RowValue.TransactionDate.ToString("dd/MM/yyyy")

        End If
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrDataH As New List(Of V_BenefitClaimDeducted)
        Dim arrDataD As New List(Of V_BenefitClaimDeductedHistory)
        If dtgHeader.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        objSessionHelper.SetSession("strFileNm", "Benefit Claim Deducted")
        objSessionHelper.SetSession("strFileNmHeader", "Benefit Claim Deducted History")
        arrDataH = CType(objSessionHelper.GetSession("dataHeader"), List(Of V_BenefitClaimDeducted))
        arrDataD = CType(objSessionHelper.GetSession("dataDetail"), List(Of V_BenefitClaimDeductedHistory))

        If arrDataH.Count > 0 Then
            DoDownloadExcel(arrDataH, arrDataD)
        End If

    End Sub

    Private Sub DoDownloadExcel(ByVal dataH As List(Of V_BenefitClaimDeducted), ByVal dataD As List(Of V_BenefitClaimDeductedHistory))
        Dim pck As ExcelPackage = New ExcelPackage()
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Reduksi Claim")
        Dim ws1 As ExcelWorksheet = pck.Workbook.Worksheets.Add("Detail")

        'ws
        ws.Cells("A1").Value = "No"
        ws.Cells("B1").Value = "Kode Dealer"
        ws.Cells("C1").Value = "Nama Dealer"
        ws.Cells("D1").Value = "No Klaim Reg DSF"
        ws.Cells("E1").Value = "Nilai Reduksi Awal"
        ws.Cells("F1").Value = "Sisa"

        Dim rowStart As Integer = 2
        For Each item As V_BenefitClaimDeducted In dataH
            ws.Cells(String.Format("A{0}", rowStart)).Value = rowStart - 1
            ws.Cells(String.Format("B{0}", rowStart)).Value = item.DealerCode
            ws.Cells(String.Format("C{0}", rowStart)).Value = item.DealerName
            ws.Cells(String.Format("D{0}", rowStart)).Value = item.RegNumberDSF
            ws.Cells(String.Format("E{0}", rowStart)).Value = item.DeductedAmount
            ws.Cells(String.Format("F{0}", rowStart)).Value = item.RemainAmount

            rowStart += 1
        Next

        'ws1
        ws1.Cells("A1").Value = "No"
        ws1.Cells("B1").Value = "No Klaim Reg DSF"
        ws1.Cells("C1").Value = "No Klaim Reg Dealer"
        ws1.Cells("D1").Value = "No Benefit"
        ws1.Cells("E1").Value = "Pilihan Klaim"
        ws1.Cells("F1").Value = "Nilai Reduksi Klaim"
        ws1.Cells("G1").Value = "Tgl Transaksi"

        Dim rowStart1 As Integer = 2
        For Each item As V_BenefitClaimDeductedHistory In dataD
            ws1.Cells(String.Format("A{0}", rowStart1)).Value = rowStart1 - 1
            ws1.Cells(String.Format("B{0}", rowStart1)).Value = item.RegNumber
            ws1.Cells(String.Format("C{0}", rowStart1)).Value = item.ClaimRegNo
            ws1.Cells(String.Format("D{0}", rowStart1)).Value = item.Description
            ws1.Cells(String.Format("E{0}", rowStart1)).Value = item.AmountDeducted
            ws1.Cells(String.Format("F{0}", rowStart1)).Value = item.TransactionDate

            rowStart1 += 1
        Next


        ws.Cells("A:AZ").AutoFitColumns()
        ws1.Cells("A:AZ").AutoFitColumns()

        Dim sFileName As String
        If Not IsNothing(objSessionHelper.GetSession("strFileNm")) Then
            strFileNm = CType(objSessionHelper.GetSession("strFileNm"), String)
        End If
        If Not IsNothing(objSessionHelper.GetSession("strFileNmHeader")) Then
            strFileNmHeader = CType(objSessionHelper.GetSession("strFileNmHeader"), String)
        End If

        sFileName = strFileNm & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Response.Clear()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" + sFileName + ".xlsx")
        Response.BinaryWrite(pck.GetAsByteArray())
        Response.End()
    End Sub

End Class