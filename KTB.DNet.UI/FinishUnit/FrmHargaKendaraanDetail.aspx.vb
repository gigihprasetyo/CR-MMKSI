Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports OfficeOpenXml

Public Class FrmHargaKendaraanDetail
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private oDealer As Dealer
    Private objDealerVehiclePrice As DealerVehiclePrice
    Private SessionGridDataVehiclePriceDetail As String = "FrmHargaKendaraanDetail.VehiclePriceDetailList"
    Private SessionCriteria As String = "FrmHargaKendaraanDetail.Criteria"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Property SesDealerVehiclePrice() As DealerVehiclePrice
        Get
            Return CType(sesHelper.GetSession("DealerVehiclePrice"), DealerVehiclePrice)
        End Get
        Set(ByVal Value As DealerVehiclePrice)
            sesHelper.SetSession("DealerVehiclePrice", Value)
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Authorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "VechileTypeCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            If Request.QueryString("DealerVehiclePriceID").ToString <> "" Then
                SesDealerVehiclePrice() = New DealerVehiclePriceFacade(User).Retrieve(CInt(Request.QueryString("DealerVehiclePriceID")))
                objDealerVehiclePrice = SesDealerVehiclePrice()

                lblKodeDealer.Text = objDealerVehiclePrice.Dealer.DealerCode
                lblNamaDealer.Text = objDealerVehiclePrice.Dealer.DealerName
                lblCustomerClass.Text = objDealerVehiclePrice.CustomerClass
                lblCompany.Text = objDealerVehiclePrice.Name
                lblMataUang.Text = objDealerVehiclePrice.Currency
                lblTanggalBerlaku.Text = objDealerVehiclePrice.EffectiveStartDate.ToString("dd/MM/yyyy")
                lblTipeDMS.Text = objDealerVehiclePrice.CustomerTypeDMS
                lblTipeDNet.Text = EnumTipePelangganCustomerRequest.RetrieveTipePelangganCustomerRequest(objDealerVehiclePrice.CustomerTypeDNET)

                '-- Restore selection criteria
                ReadCriteria()
                ReadData()   '-- Read all data matching criteria
                BindGrid(dgVehiclePriceDetail.CurrentPageIndex)  '-- Bind page-1
            End If
            PageInit()
        End If

    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub Authorization()
        'If Not SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT NASIONAL - DAFTAR EVENT NASIONAL")
        'Else
        '    displayPriv = SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Privilege)
        '    'editPriv = SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Edit_Privilege)
        '    'deletePriv = SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Delete_Privilege)
        'End If
    End Sub

    Private Sub PageInit()

    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrVehiclePriceDetailList As ArrayList = CType(sesHelper.GetSession(SessionGridDataVehiclePriceDetail), ArrayList)
        If arrVehiclePriceDetailList.Count <> 0 Then
            CommonFunction.SortListControl(arrVehiclePriceDetailList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrVehiclePriceDetailList, pageIndex, dgVehiclePriceDetail.PageSize)
            dgVehiclePriceDetail.DataSource = PagedList
            dgVehiclePriceDetail.VirtualItemCount = arrVehiclePriceDetailList.Count()
            dgVehiclePriceDetail.DataBind()
        Else
            dgVehiclePriceDetail.DataSource = New ArrayList
            dgVehiclePriceDetail.VirtualItemCount = 0
            dgVehiclePriceDetail.CurrentPageIndex = 0
            dgVehiclePriceDetail.DataBind()
        End If
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "DealerVehiclePriceGUID", MatchType.Exact, SesDealerVehiclePrice().GUID))

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerVehiclePriceDetail), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrVehiclePriceDetailList As ArrayList = New DealerVehiclePriceDetailFacade(User).Retrieve(crit, sortColl)        

        sesHelper.SetSession(SessionGridDataVehiclePriceDetail, arrVehiclePriceDetailList)
        If arrVehiclePriceDetailList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Private Sub dgVehiclePriceDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgVehiclePriceDetail.PageIndexChanged
        '-- Change datagrid page

        ReadData()
        dgVehiclePriceDetail.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgVehiclePriceDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgVehiclePriceDetail.SortCommand
        '-- Sort datagrid rows based on a column header clicked

        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        '-- Bind page-1
        dgVehiclePriceDetail.CurrentPageIndex = 0
        ReadData()
        BindGrid(dgVehiclePriceDetail.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sesHelper.GetSession(SessionCriteria), Hashtable)
        If Not crit Is Nothing Then
            lblKodeDealer.Text = CStr(crit.Item("Dealer.DealerCode"))
            lblNamaDealer.Text = CStr(crit.Item("Dealer.DealerName"))
            lblCustomerClass.Text = CStr(crit.Item("CustomerClass"))
            lblCompany.Text = CStr(crit.Item("Name"))
            lblMataUang.Text = CStr(crit.Item("Currency"))
            lblTanggalBerlaku.Text = CStr(crit.Item("EffectiveStartDate"))
            lblTipeDMS.Text = CStr(crit.Item("CustomerTypeDMS"))
            lblTipeDNet.Text = CStr(crit.Item("CustomerTypeDNET"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgVehiclePriceDetail.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("Dealer.DealerCode", lblKodeDealer.Text)
        crit.Add("Dealer.DealerName", lblNamaDealer.Text)
        crit.Add("CustomerClass", lblCustomerClass.Text)
        crit.Add("Name", lblCompany.Text)
        crit.Add("Currency", lblMataUang.Text)
        crit.Add("EffectiveStartDate", lblTanggalBerlaku.Text)
        crit.Add("CustomerTypeDMS", lblTipeDMS.Text)
        crit.Add("CustomerTypeDNET", lblTipeDNet.Text)

        crit.Add("PageIndex", dgVehiclePriceDetail.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteria, crit) '-- Store in session
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/FinishUnit/FrmDaftarHargaKendaraan.aspx")
    End Sub

    Protected Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        If dgVehiclePriceDetail.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        ' mengambil data yang dibutuhkan
        arrData = CType(sesHelper.GetSession(SessionGridDataVehiclePriceDetail), ArrayList)
        If arrData.Count > 0 Then
            CreateExcel("DataVehiclePrice", arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        objDealerVehiclePrice = New DealerVehiclePriceFacade(User).Retrieve(CInt(Request.QueryString("DealerVehiclePriceID")))

        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = "Data Vehicle Price Detail " & objDealerVehiclePrice.EffectiveStartDate.ToString("dd/MM/yyyy")
            ws.Cells("A2").Value = objDealerVehiclePrice.CustomerClass
            ws.Cells("A3").Value = objDealerVehiclePrice.Dealer.DealerCode & " - " & objDealerVehiclePrice.Dealer.DealerName

            ws.Cells("A4").Value = "No"
            ws.Cells("B4").Value = "Kode Kendaraan"
            ws.Cells("C4").Value = "Nama Kendaraan"
            ws.Cells("D4").Value = "Warna Kendaraan"
            ws.Cells("E4").Value = "Harga Dasar"
            ws.Cells("F4").Value = "Harga Off The Road"
            ws.Cells("G4").Value = "BBN"
            ws.Cells("H4").Value = "Harga On The Road"
            ws.Cells("I4").Value = "Harga Warna Spesial"
            ws.Cells("J4").Value = "Down Payment"
            ws.Cells("K4").Value = "Pajak Konsumsi 1"
            ws.Cells("L4").Value = "Jumlah Pajak Konsumsi 1"
            ws.Cells("M4").Value = "Pajak Konsumsi 2"
            ws.Cells("N4").Value = "Jumlah Pajak Konsumsi 2"

            For i As Integer = 0 To Data.Count - 1
                Dim item As DealerVehiclePriceDetail = Data(i)
                ws.Cells(i + 5, 1).Value = i + 1
                ws.Cells(i + 5, 2).Value = item.VechileTypeCode
                ws.Cells(i + 5, 3).Value = New VechileTypeFacade(User).Retrieve(item.VechileTypeCode).Description
                ws.Cells(i + 5, 4).Value = item.VechileColorCode
                ws.Cells(i + 5, 5).Value = item.BasePrice
                ws.Cells(i + 5, 6).Value = item.OffTR
                ws.Cells(i + 5, 7).Value = item.RegistrationFee
                ws.Cells(i + 5, 8).Value = item.OTR
                ws.Cells(i + 5, 9).Value = item.SpecialColorPrice
                ws.Cells(i + 5, 10).Value = item.BookingFee
                ws.Cells(i + 5, 11).Value = item.ConsumptionTax1
                ws.Cells(i + 5, 12).Value = item.ConsumptionTaxAmount1
                ws.Cells(i + 5, 13).Value = item.ConsumptionTax2
                ws.Cells(i + 5, 14).Value = item.ConsumptionTaxAmount2
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

    Protected Sub dgVehiclePriceDetail_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgVehiclePriceDetail.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblKodeKendaraan As Label = CType(e.Item.FindControl("lblKodeKendaraan"), Label)
        Dim lblNamaKendaraan As Label = CType(e.Item.FindControl("lblNamaKendaraan"), Label)
        Dim lblWarnaKendaraan As Label = CType(e.Item.FindControl("lblWarnaKendaraan"), Label)
        Dim lblHargaDasar As Label = CType(e.Item.FindControl("lblHargaDasar"), Label)
        Dim lblOffTheRoad As Label = CType(e.Item.FindControl("lblOffTheRoad"), Label)
        Dim lblBBN As Label = CType(e.Item.FindControl("lblBBN"), Label)
        Dim lblOnTheRoad As Label = CType(e.Item.FindControl("lblOnTheRoad"), Label)
        Dim lblWarnaSpesial As Label = CType(e.Item.FindControl("lblWarnaSpesial"), Label)
        Dim lblDownPayment As Label = CType(e.Item.FindControl("lblDownPayment"), Label)
        Dim lblKonsumsi1 As Label = CType(e.Item.FindControl("lblKonsumsi1"), Label)
        Dim lblJumlahKonsumsi1 As Label = CType(e.Item.FindControl("lblJumlahKonsumsi1"), Label)
        Dim lblKonsumsi2 As Label = CType(e.Item.FindControl("lblKonsumsi2"), Label)
        Dim lblJumlahKonsumsi2 As Label = CType(e.Item.FindControl("lblJumlahKonsumsi2"), Label)



        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As DealerVehiclePriceDetail = CType(e.Item.DataItem, DealerVehiclePriceDetail)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgVehiclePriceDetail.PageSize * dgVehiclePriceDetail.CurrentPageIndex)).ToString
            lblKodeKendaraan.Text = oData.VechileTypeCode
            lblWarnaKendaraan.Text = oData.VechileColorCode
            lblHargaDasar.Text = oData.BasePrice.ToString("#,##0")
            lblOffTheRoad.Text = oData.OffTR.ToString("#,##0")
            lblBBN.Text = oData.BookingFee.ToString("#,##0")
            lblOnTheRoad.Text = oData.OTR.ToString("#,##0")
            lblWarnaSpesial.Text = oData.SpecialColorPrice.ToString("#,##0")
            lblDownPayment.Text = oData.RegistrationFee.ToString("#,##0")
            lblKonsumsi1.Text = oData.ConsumptionTax1.ToString
            lblJumlahKonsumsi1.Text = oData.ConsumptionTaxAmount1.ToString("#,##0")
            lblKonsumsi2.Text = oData.ConsumptionTax2.ToString
            lblJumlahKonsumsi2.Text = oData.ConsumptionTaxAmount2.ToString("#,##0")

            lblNamaKendaraan.Text = New VechileTypeFacade(User).Retrieve(oData.VechileTypeCode).Description

        End If
    End Sub

    
End Class