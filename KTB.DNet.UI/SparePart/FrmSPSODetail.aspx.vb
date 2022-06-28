#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessValidation.Helpers
#End Region

Public Class FrmSPSODetail
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"
    Private nPOID As Integer = 0
    Private objPOHead As SparePartPO
    Private objPOEstimate As SparePartPOEstimate = New SparePartPOEstimate
    Private objPOEstimateDetail As SparePartPOEstimateDetail
    Private sessHelper As SessionHelper = New SessionHelper
    Private arrList As ArrayList = New ArrayList
    Private totalRow As Integer = 0
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
#End Region

#Region "Custom Method"

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Sub retrieveHeader()
        If ViewState("FromPendingOrder") = "Yes" Then
            Dim crits As New CriteriaComposite(New Criteria(GetType(SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'crits.opAnd(New Criteria(GetType(SparePartPOEstimate), "SparePartPO.ID", MatchType.Exact, nPOID))
            crits.opAnd(New Criteria(GetType(SparePartPOEstimate), "SONumber", MatchType.Exact, nPOID))
            Dim arlList As ArrayList = New SparePartPOEstimateFacade(User).Retrieve(crits)
            If arlList.Count <> 0 Then
                objPOEstimate = arlList(0)
            Else
                objPOEstimate = Nothing
            End If
        Else
            objPOEstimate = New SparePartPOEstimateFacade(User).Retrieve(nPOID)
        End If

        If Not objPOEstimate Is Nothing Then
            lblDealerCode.Text = objPOEstimate.SparePartPO.Dealer.DealerCode
            If Not IsNothing(objPOEstimate.SparePartPO.TermOfPayment) Then
                lblCaraPembayaran.Text = objPOEstimate.SparePartPO.TermOfPayment.Description
            End If
            lblDealerName.Text = objPOEstimate.SparePartPO.Dealer.DealerName
            lblDealerTerm.Text = objPOEstimate.SparePartPO.Dealer.SearchTerm2

            Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objPOEstimate.SODate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

            lblOrderType.Text = objPOEstimate.SparePartPO.OrderTypeDesc
            lblPO.Text = objPOEstimate.SparePartPO.PONumber + " - " + objPOEstimate.SparePartPO.PODate
            lblSO.Text = objPOEstimate.SONumber + " - " + objPOEstimate.SODate
            lblSchedule.Text = Format(objPOEstimate.DeliveryDate, "dd/MM/yyyy")
            lblTotAllocAmount.Text = String.Format("{0:#,##0}", objPOEstimate.POEstimateAmount)
            'lblTotTax.Text = String.Format("{0:#,##0}", (CDec(lblTotAllocAmount.Text) * 0.1))
            lblTotTax.Text = String.Format("{0:#,##0}", CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=CDec(lblTotAllocAmount.Text)))
            lblDepositC2.Text = IIf(objPOEstimate.SparePartPO.OrderType = "R", String.Format("{0:#,##0}", (CDec(lblTotAllocAmount.Text) * 0.03)), 0)
            lblGrandAmount.Text = String.Format("{0:#,##0}", (CDec(lblTotAllocAmount.Text) + CDec(lblTotTax.Text) + CDec(lblDepositC2.Text)))
            sessHelper.SetSession("POEstimate", objPOEstimate)
        Else
            sessHelper.SetSession("POEstimate", Nothing)
        End If
    End Sub

    Private Sub retrieveDetails(ByVal pageIndex As Integer)

        If GetFromSession("POEstimate") Is Nothing Then
            arrList = New ArrayList
        Else
            objPOEstimate = CType(GetFromSession("POEstimate"), SparePartPOEstimate)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimateDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimateDetail), "SparePartPOEstimate.ID", MatchType.Exact, objPOEstimate.ID))

            Dim oSortColl As New SortCollection
            oSortColl.Add(New Sort(GetType(SparePartPOEstimateDetail), sessHelper.GetSession("SortCol"), sessHelper.GetSession("SortDirection")))

            arrList = New SparePartPOEstimateDetailFacade(User).RetrieveByCriteria(criterias, pageIndex, dgEstimateDetail.PageSize, totalRow, oSortColl)
            If arrList.Count > dgEstimateDetail.PageSize Then
                Dim arrListDownload As ArrayList = New SparePartPOEstimateDetailFacade(User).Retrieve(criterias)
                sessHelper.SetSession("FrmPurchaseOrderEstimateDetail_SPPOEstimateDetail", arrListDownload)
            Else
                sessHelper.SetSession("FrmPurchaseOrderEstimateDetail_SPPOEstimateDetail", arrList)
            End If

        End If

    End Sub

    Private Sub BindDG(ByVal pageIndex As Integer)
        retrieveDetails(pageIndex)
        If arrList.Count > 0 Then
            dgEstimateDetail.DataSource = arrList
            dgEstimateDetail.VirtualItemCount = totalRow
            dgEstimateDetail.DataBind()
        Else
            dgEstimateDetail.DataSource = arrList
            dgEstimateDetail.VirtualItemCount = 0
            dgEstimateDetail.DataBind()
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub


    Private Sub WriteSPPOEstimateData(ByRef sw As StreamWriter, ByVal objSPEstimate As SparePartPOEstimate)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        '-- Read SPPOEstimate header
        'Dim objSPStatus As SparePartPOStatus = CType(_sessHelper.GetSession("sesSPPOStatus"), SparePartPOStatus)
        'kode dealer; nama dealer; jenis order; nomor pesanan - tanggal; nomor penjualan ktb - tanggal; nomor faktur - tanggal; total nilai tagihan (rp)
        If Not IsNothing(objSPEstimate) Then
            itemLine.Remove(0, itemLine.Length)       '-- Empty line
            itemLine.Append("Kode Dealer" & tab & ": ")
            itemLine.Append(objSPEstimate.SparePartPO.Dealer.DealerCode)  '-- Kode dealer
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Nilai Tagihan (Rp)" & tab & ": ")
            itemLine.Append(lblTotAllocAmount.Text)  '-- Nilai Tagihan
            sw.WriteLine(itemLine.ToString())         '-- Write to file

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Nama Dealer" & tab & ": ")
            itemLine.Append(objSPEstimate.SparePartPO.Dealer.DealerName & " / " & objSPEstimate.SparePartPO.Dealer.SearchTerm2)  '-- Nama dealer
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("PPN (Rp)" & tab & ": ")
            itemLine.Append(lblTotTax.Text)  '-- PPN
            sw.WriteLine(itemLine.ToString())    '-- Write to file

            itemLine.Remove(0, itemLine.Length)   '-- Empty line
            itemLine.Append("Tipe Order" & tab & ": ")
            itemLine.Append(objSPEstimate.SparePartPO.OrderTypeDesc)  '-- Tipe order
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Total Tagihan (Rp)" & tab & ": ")
            itemLine.Append(lblGrandAmount.Text)  '-- Total Tagihan
            sw.WriteLine(itemLine.ToString())     '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Nomor Pesanan - Tanggal" & tab & ": ")
            itemLine.Append(lblPO.Text & tab)  '-- PO number
            sw.WriteLine(itemLine.ToString())      '-- Write to file


            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Nomor Penjualan MMKSI - Tanggal" & tab & ": ")
            itemLine.Append(lblSO.Text)  '-- total Amount
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Deposit C2 - hanya untuk RO (Rp)" & tab & ": ")
            itemLine.Append(lblDepositC2.Text)  '-- Deposit C2
            sw.WriteLine(itemLine.ToString())      '-- Write to file


            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Estimasi Tanggal Pengiriman" & tab & ": ")
            itemLine.Append(lblSchedule.Text)  '-- total Amount
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Cara Pembayaran" & tab & ": ")
            itemLine.Append(lblCaraPembayaran.Text)  '-- Cara Pembayaran
            sw.WriteLine(itemLine.ToString())      '-- Write to file

        End If

        '-- Read SPPO detail
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.ID", MatchType.Exact, objSPStatus.ID))

        Dim arrList As ArrayList = sessHelper.GetSession("FrmPurchaseOrderEstimateDetail_SPPOEstimateDetail")

        If Not IsNothing(arrList) AndAlso arrList.Count <> 0 Then

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            sw.WriteLine(itemLine.ToString())    '-- Write blank line

            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Nomor Barang" & tab)  '-- Part number
            itemLine.Append("Nama Barang" & tab)   '-- Part name
            itemLine.Append("Jumlah Pesanan" & tab)
            itemLine.Append("Jumlah Pemenuhan" & tab)
            itemLine.Append("Harga Eceran (Rp)" & tab)
            itemLine.Append("Nilai Pemenuhan (Rp)" & tab)
            itemLine.Append("Nomor Pengganti" & tab)
            itemLine.Append("Diskon (%)" & tab)
            itemLine.Append("Tagihan (Rp)" & tab)
            sw.WriteLine(itemLine.ToString())      '-- Write header


            For Each sppoLine As SparePartPOEstimateDetail In arrList

                itemLine.Remove(0, itemLine.Length)  '-- Empty line

                itemLine.Append(sppoLine.PartNumber & tab)  '-- Part number
                itemLine.Append(sppoLine.PartName & tab)    '-- Part name
                itemLine.Append(sppoLine.OrderQty & tab)                  '-- Jumlah Pesanan
                itemLine.Append(sppoLine.AllocQty & tab)                  '-- Jumlah Pemenuhan
                'itemLine.Append(FormatNumber(sppoLine.RetailPrice, 0).ToString() & tab)     '-- Harga Eceran
                itemLine.Append(FormatNumber(sppoLine.RetailPrice, 0, TriState.False, TriState.False, TriState.False).ToString() & tab)
                'itemLine.Append(FormatNumber(sppoLine.Amount, 0).ToString() & tab)          '-- Nilai Pemenuhan
                itemLine.Append(FormatNumber(sppoLine.Amount, 0, TriState.False, TriState.False, TriState.False).ToString() & tab)
                itemLine.Append(sppoLine.AltPartNumber & tab)                    '-- Nomor Pengganti
                'itemLine.Append(FormatNumber(sppoLine.Discount, 0).ToString() & tab)        '-- Diskon
                itemLine.Append(FormatNumber(sppoLine.Discount, 0, TriState.False, TriState.False, TriState.False).ToString() & tab)
                'itemLine.Append(FormatNumber(sppoLine.TotalAmount, 0).ToString() & tab)     '-- Tagihan
                itemLine.Append(FormatNumber(sppoLine.TotalAmount, 0, TriState.False, TriState.False, TriState.False).ToString() & tab)

                sw.WriteLine(itemLine.ToString())  '-- Write Deposit line
            Next

        End If

    End Sub


    Private Sub download(ByVal idSPPOEstimate As Integer)

        Dim objSPPOEstimate As SparePartPOEstimate = New SparePartPOEstimateFacade(User).Retrieve(idSPPOEstimate)

        Dim sFileName As String  '-- File name
        If Not IsNothing(objSPPOEstimate) Then
            sFileName = "SPPOEstimate" & objSPPOEstimate.SparePartPO.PONumber   '-- Set file name as "Status" + "PO number".xls
        Else
            sFileName = "SPPOEstimate"  '-- Dummy file name
        End If
        sFileName = sFileName & "_" & DateTime.Now.ToString("yyyyMMddhhmmss")
        '-- Temp file must be a randomly named file!
        Dim SPPOEstimateData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SPPOEstimateData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SPPOEstimateData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteSPPOEstimateData(sw, objSPPOEstimate)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show(ex.Message) '"Download data gagal")
        End Try
    End Sub
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            sessHelper.SetSession("SortCol", "PartNumber")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            If Request.QueryString("isFromPO") = "Yes" Then
                ViewState.Add("FromPendingOrder", "Yes")
                nPOID = CType(Request.QueryString("SOID"), Integer)
                retrieveHeader()
                BindDG(1)
            Else
                If Not IsNothing(Request.QueryString("SOID")) Then
                    nPOID = CType(Request.QueryString("SOID"), Integer)
                    retrieveHeader()
                    BindDG(1)
                End If
            End If
        End If
    End Sub

    Private Sub dgEstimateDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEstimateDetail.ItemDataBound
        If e.Item.ItemIndex > -1 Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dgEstimateDetail.PageSize * dgEstimateDetail.CurrentPageIndex)).ToString

            Dim objSparePartPOEstimateDetail As SparePartPOEstimateDetail
            objSparePartPOEstimateDetail = CType(arrList(e.Item.ItemIndex), SparePartPOEstimateDetail)
            If objSparePartPOEstimateDetail.SparePartPOEstimate.DocumentType = "N" Then
                dgEstimateDetail.Columns(6).Visible = False
                dgEstimateDetail.Columns(7).Visible = False
            End If
            'e.Item.Cells(6).Text = Math.Abs(objSparePartPOEstimateDetail.OrderQty - objSparePartPOEstimateDetail.AllocationQty)
            'e.Item.Cells(7).Text = objSparePartPOEstimateDetail.OpenQty

            'Qty-1 = Order Qty dari Inquiry = 1
            'Qty-2 = Adjustment  Qty  = nilai absolute atas selisih antara order qty dgn allocation qty  = 1 – 0 = 1
            'Qty-3 = Receive Qty = Qty-1 = 1
            'Qty-4 = Allocation Qty = qty yg dialokasikan di SO =0

            'SparePartPOEstimateDetail.OrderQty = CType(Val.Substring(69, 6).Trim(), Integer)
            'SparePartPOEstimateDetail.AllocationQty = CType(Val.Substring(76, 6).Trim(), Integer)
            'SparePartPOEstimateDetail.OpenQty = CType(Val.Substring(83, 6).Trim(), Integer)
            'SparePartPOEstimateDetail.AllocQty = CType(Val.Substring(90, 6).Trim(), Integer)

        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tempPOEstimate As String
        objPOEstimate = CType(Session("POEstimate"), SparePartPOEstimate)
        tempPOEstimate = New SparePartPOEstimateFacade(User).UpdatePOEstimateSync(objPOEstimate)
        If tempPOEstimate = String.Empty Then
            MessageBox.Show("Data Sparepart PO berhasil diupdate!")
        ElseIf tempPOEstimate Like "Transaction Error" Then
            MessageBox.Show("Transaksi Gagal!")
        Else
            MessageBox.Show("Sparepart dengan nomor " + tempPOEstimate + " tidak ada!")

        End If

        sessHelper.RemoveSession("POEstimate")
    End Sub

    Private Sub dgEstimateDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEstimateDetail.PageIndexChanged
        dgEstimateDetail.CurrentPageIndex = e.NewPageIndex
        BindDG(e.NewPageIndex + 1)
    End Sub


    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        download(CType(Request.QueryString("SOID"), Integer))
    End Sub

    Private Sub dgEstimateDetail_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgEstimateDetail.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortCol") Then
            If sessHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortCol", e.SortExpression)
        dgEstimateDetail.SelectedIndex = -1
        dgEstimateDetail.CurrentPageIndex = 0
        BindDG(dgEstimateDetail.CurrentPageIndex)
    End Sub

#End Region

End Class