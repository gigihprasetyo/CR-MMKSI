#Region "Custom Namespace Imports"
Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.BusinessFacade.SparePart
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.Utility
Imports KTB.DNET.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessValidation.Helpers
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmPurchaseOrderEstimate
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents cmbOrderTye As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents cmdSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgPOEstimate As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ccPODateStart As KTB.DNET.WebCC.IntiCalendar
    Protected WithEvents ccPODateEnd As KTB.DNET.WebCC.IntiCalendar
    Protected WithEvents cmdDownload As System.Web.UI.WebControls.Button
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNomorPesanan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalTagihan As System.Web.UI.WebControls.Label
    Protected WithEvents btnGetDealer As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadFaktur As System.Web.UI.WebControls.Button
    Protected WithEvents cmbDocumentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList

    Protected WithEvents ccSODateStart As KTB.DNET.WebCC.IntiCalendar
    Protected WithEvents ccSODateEnd As KTB.DNET.WebCC.IntiCalendar
    Protected WithEvents chkPO As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkSO As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private nDealerID As Integer
    Private sessHelper As SessionHelper = New SessionHelper
    Private ArrList As ArrayList = New ArrayList
    Private totalRow As Integer = 0
    Private _isPrintAllowed As Boolean = False
    Private _isShowDetailAllowed As Boolean = False
    Private _sessData As String = "FrmPurchaseOrderEstimate.Data"
    Private _sessDataForDownload As String = "FrmPurchaseOrderEstimate.DataForDownload"
    Private _IsForDownload As Integer = -5
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
#End Region

#Region "Custom Method"

    Private Sub BindDdlPaymentType()
        Dim dlr As Dealer = CType(Session("sesDealer"), Dealer)
        Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(dlr.DealerCode)
        Dim oTopCA As TermOfPayment

        Dim listOfPayments As ArrayList

        If dlr.Title = EnumDealerTittle.DealerTittle.KTB Then
            listOfPayments = New TermOfPaymentFacade(User).RetrieveActivePaymentTypeList()
        Else
            oTopCA = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
            If Not IsNothing(oTopCA) Then
                listOfPayments = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
            End If
        End If

        ddlTermOfPayment.DataSource = listOfPayments
        ddlTermOfPayment.DataValueField = "ID"
        ddlTermOfPayment.DataTextField = "Description"
        ddlTermOfPayment.DataBind()
        ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
    End Sub

    Protected Sub cmbOrderType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ddlOrderTypeT As DropDownList = CType(sender, DropDownList)
        If cmbOrderTye.SelectedValue = "R" OrElse cmbOrderTye.SelectedValue = "I" OrElse cmbOrderTye.SelectedValue = "Z" Then
            ddlTermOfPayment.Enabled = True
            BindDdlPaymentType()
        Else
            ddlTermOfPayment.ClearSelection()
            ddlTermOfPayment.Items.Insert(0, New ListItem("", ""))
            ddlTermOfPayment.Enabled = False
        End If
    End Sub

    Private Sub WriteSPPOData(ByRef sw As StreamWriter, ByVal objSPEstimate As SparePartPOEstimate)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file
        Dim tagihan As Decimal = 0
        Dim ppn As Decimal = 0
        Dim deposit As Decimal = 0

        Try
            '-- Read SPPO header
            'Dim objSPStatus As SparePartPOStatus = CType(_sessHelper.GetSession("sesSPPOStatus"), SparePartPOStatus)
            'kode dealer; nama dealer; jenis order; nomor pesanan - tanggal; nomor penjualan ktb - tanggal; nomor faktur - tanggal; total nilai tagihan (rp)
            If Not IsNothing(objSPEstimate) Then
                itemLine.Remove(0, itemLine.Length)       '-- Empty line
                itemLine.Append("Kode Dealer:" & tab)
                itemLine.Append(objSPEstimate.SparePartPO.Dealer.DealerCode & tab & tab)  '-- Kode dealer

                tagihan = objSPEstimate.POEstimateAmount
                itemLine.Append("Nilai Tagihan (Rp):" & tab)
                itemLine.Append(Decimal.Round(tagihan, 0) & tab)   '-- Nilai Tagihan
                sw.WriteLine(itemLine.ToString())      '-- Write to file


                itemLine.Remove(0, itemLine.Length)  '-- Empty line
                itemLine.Append("Nama Dealer:" & tab)
                itemLine.Append(objSPEstimate.SparePartPO.Dealer.DealerName & " / " & objSPEstimate.SparePartPO.Dealer.SearchTerm2 & tab & tab)  '-- Nama dealer

                Dim ppnFromDbPPNMaster = CalcHelper.GetPPNMasterByTaxTypeId(objSPEstimate.SODate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                ppn = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromDbPPNMaster, dpp:=objSPEstimate.POEstimateAmount)
                itemLine.Append("PPN (Rp):" & tab)
                itemLine.Append(Decimal.Round(ppn, 0) & tab)   '-- Pajak
                sw.WriteLine(itemLine.ToString())      '-- Write to file


                itemLine.Remove(0, itemLine.Length)   '-- Empty line
                itemLine.Append("Tipe Order:" & tab)
                itemLine.Append(objSPEstimate.SparePartPO.OrderTypeDesc & tab & tab)  '-- Tipe order

                deposit = (IIf(objSPEstimate.SparePartPO.OrderType = "R", String.Format("{0:#,##0}", (objSPEstimate.POEstimateAmount * 0.03)), 0))
                itemLine.Append("Deposit C2 - Hanya Untuk RO (Rp) :" & tab)
                itemLine.Append(Decimal.Round(deposit, 2) & tab)   '-- Pajak
                sw.WriteLine(itemLine.ToString())      '-- Write to file

                itemLine.Remove(0, itemLine.Length)    '-- Empty line
                itemLine.Append("No Pesanan - Tanggal:" & tab)
                itemLine.Append(objSPEstimate.SparePartPO.PONumber & " - " & objSPEstimate.SparePartPO.PODate & tab & tab)  '-- PO number

                itemLine.Append("Total Tagihan (Rp):" & tab)
                itemLine.Append(Decimal.Round(CDec(tagihan) + CDec(ppn) + CDec(deposit), 0) & tab)    '-- Pajak
                'itemLine.Append((String.Format("{0:#,##0}", Decimal.Round(CDec(tagihan) + CDec(ppn) + CDec(deposit), 0))) & tab)    '-- Pajak
                sw.WriteLine(itemLine.ToString())      '-- Write to file

                itemLine.Remove(0, itemLine.Length)    '-- Empty line
                itemLine.Append("No Penjualan MMKSI - Tanggal:" & tab)
                itemLine.Append(objSPEstimate.SparePartPO.SparePartPOEstimate.SONumber & " - " & Format(objSPEstimate.SparePartPO.SparePartPOEstimate.SODate, "dd/MM/yyyy") & tab & tab)  '-- KTB Number
                itemLine.Append("Cara Pembayaran:" & tab)
                If Not IsNothing(objSPEstimate.SparePartPO.TermOfPayment) Then
                    itemLine.Append(objSPEstimate.SparePartPO.TermOfPayment.Description)    '-- Term of payment
                Else
                    itemLine.Append("")    '-- Term of payment
                End If
                sw.WriteLine(itemLine.ToString())      '-- Write to file

                itemLine.Remove(0, itemLine.Length)    '-- Empty line
                itemLine.Append("Jadwal Pengiriman:" & tab)
                itemLine.Append(objSPEstimate.DeliveryDate & tab)   '-- Tanggal Pengiriman
                sw.WriteLine(itemLine.ToString())      '-- Write to file

                itemLine.Remove(0, itemLine.Length)    '-- Empty line
                sw.WriteLine(itemLine.ToString())      '-- Write an empty line to file

            End If

            '-- Read SPPO detail
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartPOEstimateDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.SparePartPOEstimateDetail), "SparePartPOEstimate.ID", MatchType.Exact, objSPEstimate.ID))

            Dim arSPEstimateDetail As ArrayList = New SparePartPOEstimateDetailFacade(User).Retrieve(criterias)

            If Not IsNothing(arSPEstimateDetail) AndAlso arSPEstimateDetail.Count <> 0 Then

                '-- Write column header
                itemLine.Remove(0, itemLine.Length)  '-- Empty line

                itemLine.Append("No. Barang" & tab)  '-- Part number
                itemLine.Append("Nama Brg." & tab)   '-- Part name
                itemLine.Append("Jumlah Pesanan" & tab)      '-- Quantity
                itemLine.Append("Jumlah Pemenuhan" & tab)      '-- Quantity
                itemLine.Append("Harga Eceran (Rp)" & tab)  '-- Retail price
                itemLine.Append("Nilai Pemenuhan (Rp)" & tab)  '-- Retail price
                itemLine.Append("Nomor Pengganti" & tab)  '-- Retail price
                itemLine.Append("Diskon (%)" & tab)  '-- Retail price
                itemLine.Append("Tagihan (Rp)" & tab)  '-- Retail price
                sw.WriteLine(itemLine.ToString())      '-- Write header

                For Each sppoLine As SparePartPOEstimateDetail In arSPEstimateDetail

                    itemLine.Remove(0, itemLine.Length)  '-- Empty line

                    itemLine.Append(Chr(39) & sppoLine.PartNumber & tab)  '-- Part number
                    itemLine.Append(sppoLine.PartName & tab)              '-- Part name
                    itemLine.Append(sppoLine.OrderQty & tab)              '-- Jumlah Pesanan
                    itemLine.Append(sppoLine.AllocQty & tab)              '-- Jumlah Pemesanan
                    itemLine.Append(Decimal.Round(sppoLine.RetailPrice, 0) & tab)           '-- Harga Eceran
                    itemLine.Append(Decimal.Round(sppoLine.Amount, 0) & tab)                '-- Nilai Pemenuhan
                    itemLine.Append(sppoLine.AltPartNumber & tab)         '-- Nomor Pengganti
                    itemLine.Append(Decimal.Round(sppoLine.Discount, 0) & tab)              '-- Diskon
                    itemLine.Append(Decimal.Round(sppoLine.TotalAmount, 0) & tab)            '-- Tagihan

                    sw.WriteLine(itemLine.ToString())  '-- Write Deposit line
                Next

            End If
        Catch ex As Exception
            Dim tmp As String
            tmp = ex.Message
        End Try


    End Sub

    Private Sub download(ByVal idSPPOEstimate As Integer)
        Dim objSPEstimate As SparePartPOEstimate = New SparePartPOEstimateFacade(User).Retrieve(idSPPOEstimate)
        Dim strTime As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        Dim sFileName As String  '-- File name
        If Not IsNothing(objSPEstimate) Then
            sFileName = "Estimasi" & objSPEstimate.SparePartPO.PONumber & "-" & strTime '-- Set file name as "Status" + "PO number".xls
        Else
            sFileName = "SPPOEstimasi" & "-" & strTime   '-- Dummy file name
        End If

        '-- Temp file must be a randomly named file!
        Dim SPPOEstimasiData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNET.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SPPOEstimasiData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SPPOEstimasiData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteSPPOData(sw, objSPEstimate)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub GetDealer()
        'Dim objDealer As Dealer = New DealerFacade(User).Retrieve(nDealerID)
        If Not IsNothing(Session("DEALER")) Then
            sessHelper.SetSession("sesDealer", Session("DEALER"))
        Else
            'Response.Redirect("../SessionExpired.htm")
        End If
    End Sub

    Private Sub GetOrderType()
        cmbOrderTye.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer 'LookUp.ArraySPOrderType
            cmbOrderTye.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        cmbOrderTye.DataBind()
    End Sub

    Private Sub GetDocumentType()
        cmbDocumentType.Items.Add(New ListItem("Semua", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer 'LookUp.ArraySPOrderType
            cmbDocumentType.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        cmbDocumentType.DataBind()
    End Sub

    Private Sub RetrieveHeader()
        GetDealer()
        Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblDealerCode.Text = objDealer.DealerCode
            lblDealerName.Text = objDealer.DealerName
            lblDealerTerm.Text = objDealer.SearchTerm2
        Else
            lblDealerCode.Text = ""
            lblDealerName.Text = ""
            lblDealerTerm.Text = ""
        End If
        GetOrderType()
        GetDocumentType()
        ''cmdDownload.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpDownloadEstimate.aspx?term2=" & objDealer.SearchTerm2.ToUpper.Trim & "','',400,400,Estimate);")
    End Sub

    Private Sub RetrieveDetails(ByVal pageIndex As Integer)
        If ccPODateEnd.Value >= ccPODateStart.Value Then
            'If txtDealerCode.Text.Trim <> "" Then
            '    Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
            '    lblDealerName.Text = objDealer.DealerName
            '    lblDealerTerm.Text = objDealer.SearchTerm2
            'Else
            '    lblDealerName.Text = String.Empty
            '    lblDealerTerm.Text = String.Empty
            'End If
            FindData(pageIndex)
            If ArrList.Count > 0 Then
                dgPOEstimate.DataSource = ArrList
                dgPOEstimate.VirtualItemCount = totalRow
                dgPOEstimate.DataBind()

            Else
                dgPOEstimate.DataSource = New ArrayList
                dgPOEstimate.VirtualItemCount = 0
                dgPOEstimate.DataBind()
                lblTotalQuantity.Text = String.Empty
                lblTotalTagihan.Text = String.Empty
                If IsPostBack Then
                    MessageBox.Show(SR.DataNotFound("Data"))
                End If
            End If
        Else
            MessageBox.Show(SR.InvalidRangeDate)
        End If
    End Sub

    Private Sub FindData(ByVal pageIndex As Integer)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "SparePartPO.Dealer.ID", MatchType.Exact, objDealer.ID))
        Else
            If (Not String.IsNullOrEmpty(txtDealerCode.Text.Trim)) Then
                criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "SparePartPO.Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            End If
        End If


        If txtNomorPesanan.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "SparePartPO.PONumber", MatchType.[Partial], txtNomorPesanan.Text.Trim))
        End If

        If Not ddlTermOfPayment.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "SparePartPO.TermOfPayment.ID", MatchType.Exact, ddlTermOfPayment.SelectedValue))
        End If

        If cmbOrderTye.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "SparePartPO.OrderType", MatchType.Exact, cmbOrderTye.SelectedValue))
        If cmbDocumentType.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "DocumentType", MatchType.Exact, cmbDocumentType.SelectedValue))

        If chkPO.Checked Then
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "SparePartPO.PODate", MatchType.GreaterOrEqual, Format(ccPODateStart.Value, "yyyy/MM/dd")))
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "SparePartPO.PODate", MatchType.LesserOrEqual, Format(ccPODateEnd.Value, "yyyy/MM/dd")))
        End If

        If chkSO.Checked Then
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "SODate", MatchType.GreaterOrEqual, Format(ccSODateStart.Value, "yyyy/MM/dd")))
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "SODate", MatchType.LesserOrEqual, Format(ccSODateEnd.Value, "yyyy/MM/dd")))
        End If

        'ArrList = New SparePartPOEstimateFacade(User).Retrieve(criterias)
        'ForumList = New ForumFacade(User).RetrieveActiveList(indexPage + 1, dtgForumList.PageSize, totalRow, sessHelp.GetSession("SortCol"), sessHelp.GetSession("SortDirection"), criterias)

        If pageIndex = _IsForDownload Then 'For Downloading Purpose
            Dim aAll As ArrayList
            Dim oSorts As New SortCollection

            oSorts.Add(New Sort(GetType(SparePartPOEstimate), sessHelper.GetSession("SortCol"), sessHelper.GetSession("SortDirection")))
            aAll = New SparePartPOEstimateFacade(User).Retrieve(criterias, oSorts)
            Me.sessHelper.SetSession(Me._sessDataForDownload, aAll)
        Else
            'Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartPOEstimateDetail), "SparePartPOEstimate", MatchType.Exact, Me.ID))

            'Dim agg As Aggregate = New Aggregate(GetType(SparePartPOEstimateDetail), "((AllocQty * RetailPrice * (100 - Discount))", AggregateType.Sum)

            '_POEstimateAmount = DoLoadScalar(GetType(SparePartPOEstimateDetail).ToString(), agg, criterias)

            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimateDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim Sql As String = ""
            Sql &= " select distinct(SparePartPOEstimate.id) from SparePartPOEstimate "
            Sql &= criterias.ToString()

            criterias2.opAnd(New Criteria(GetType(SparePartPOEstimateDetail), "SparePartPOEstimate.ID", MatchType.InSet, "(" & Sql & ")"))

            Dim arrData As ArrayList = New SparePartPOEstimateDetailFacade(User).Retrieve(criterias2)
            Dim totalTagihanNewCalc As Decimal = calculateTotalTagihan(arrData)

            'Dim aggAmount As AggregateCustom = New AggregateCustom(GetType(SparePartPOEstimateDetail), "(AllocQty * RetailPrice * (100 - Discount)/100)", AggregateType.Sum)
            'Dim totalTagihan As Decimal = New SparePartPOEstimateDetailFacade(User).GetAggregateResult(aggAmount, criterias2)
            'totalTagihan = totalTagihan + totalTagihan * 0.1
            'lblTotalTagihan.Text = String.Format("{0:#,##0.00}", totalTagihan)
            lblTotalTagihan.Text = String.Format("{0:0,0.00}", totalTagihanNewCalc)


            Dim aggTotalCount As Aggregate = New Aggregate(GetType(SparePartPOEstimate), "ID", AggregateType.Count)
            Dim totalCount As Decimal = New SparePartPOEstimateFacade(User).GetAggregateResult(aggTotalCount, criterias)
            lblTotalQuantity.Text = FormatNumber(totalCount, 0, TriState.False, TriState.True, TriState.True)

            ArrList = New SparePartPOEstimateFacade(User).RetrieveActiveList(pageIndex, dgPOEstimate.PageSize, totalRow, sessHelper.GetSession("SortCol"), sessHelper.GetSession("SortDirection"), criterias)
            Me.sessHelper.SetSession(Me._sessData, ArrList)

        End If
    End Sub

    Private Function calculateTotalTagihan(ByVal arrData As ArrayList) As Decimal
        If arrData.Count > 0 Then
            Dim result As Decimal = 0
            For Each obj As SparePartPOEstimateDetail In arrData
                Dim raw As Decimal = obj.AllocQty * obj.RetailPrice * (100.0 - obj.Discount) / 100.0
                Dim ppnFromDbPPNMaster = CalcHelper.GetPPNMasterByTaxTypeId(obj.SparePartPOEstimate.SODate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                result += CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromDbPPNMaster, dpp:=raw) + raw
            Next

            Return result
        End If
        Return 0
    End Function

    Private Function CalculatePOEstimateAmount(ByVal arlPODetail As ArrayList) As Decimal
        Dim nPOAmount As Decimal = 0
        For Each objPOEstimateDetail As SparePartPOEstimateDetail In arlPODetail
            nPOAmount = nPOAmount + (objPOEstimateDetail.AllocQty * objPOEstimateDetail.RetailPrice)
        Next
        Return (nPOAmount)
    End Function


    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "Download Faktur Add " & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNET.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TraineeData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteData(sw, data)

                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If



            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder
        Dim i As Integer = 1
        Dim nDays As Integer = CType(ViewState.Item("nDays"), Integer)
        Dim oSPPOSD As SparePartPOStatusDetail

        If Not IsNothing(data) Then

            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No. PO" & tab)
            itemLine.Append("Dealer Code" & tab)
            itemLine.Append("No. Faktur" & tab)
            itemLine.Append("No. Delivery" & tab)
            itemLine.Append("Tanggal Faktur" & tab)
            itemLine.Append("Material" & tab)
            itemLine.Append("Description" & tab)
            itemLine.Append("Picking Ticket" & tab) 'Add by reza Req by Mai 11072018
            itemLine.Append("Billed Quantity" & tab)
            itemLine.Append("Net Price" & tab)
            itemLine.Append("Net Value (Amount)" & tab)
            itemLine.Append("Cara Pembayaran" & tab)

            sw.WriteLine(itemLine.ToString())

            For Each oSPPOE As SparePartPOEstimate In data
                If Not IsNothing(oSPPOE.SparePartPO) AndAlso Not IsNothing(oSPPOE.SparePartPO.SparePartPOStatus) AndAlso Not IsNothing(oSPPOE.SparePartPO.SparePartPOStatus.SparePartPOStatusDetails()) Then
                    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(SparePartPOStatus), "SONumber", MatchType.Exact, oSPPOE.SONumber))
                    crits.opAnd(New Criteria(GetType(SparePartPOStatus), "SparePartPO.ID", MatchType.Exact, oSPPOE.SparePartPO.ID))
                    Dim arrs As ArrayList = New SparePartPOStatusFacade(User).RetrieveList(crits)

                    'For Each objStatusDetail As SparePartPOStatusDetail In oSPPOE.SparePartPO.SparePartPOStatus.SparePartPOStatusDetails()
                    For Each objStatus As SparePartPOStatus In arrs
                        For Each objStatusDetail As SparePartPOStatusDetail In objStatus.SparePartPOStatusDetails
                            itemLine.Remove(0, itemLine.Length)

                            itemLine.Append(oSPPOE.SparePartPO.PONumber & tab)
                            itemLine.Append(oSPPOE.SparePartPO.Dealer.DealerCode & tab)
                            itemLine.Append(objStatus.BillingNumber & tab)
                            'oSPPOSD = CType(oSPPOE.SparePartPO.SparePartPOStatus.SparePartPOStatusDetails(0), SparePartPOStatusDetail)
                            itemLine.Append(objStatusDetail.DONumber & tab)
                            If objStatus.BillingDate.Year < 2000 Then
                                itemLine.Append("" & tab)
                            Else
                                itemLine.Append(objStatus.BillingDate.ToString("yyyy/MM/dd") & tab)
                            End If
                            itemLine.Append(objStatusDetail.SparePartMaster.PartNumber & tab)
                            itemLine.Append(objStatusDetail.SparePartMaster.PartName & tab)
                            itemLine.Append(oSPPOE.SparePartPO.PickingTicket & tab) 'Add by reza Req by Mai 11072018
                            itemLine.Append(objStatusDetail.BillingQuantity & tab)
                            itemLine.Append(GetNumericOnly(objStatusDetail.NetPrice) & tab)
                            itemLine.Append(GetNumericOnly(objStatusDetail.BillingPrice) & tab)
                            If Not IsNothing(oSPPOE.SparePartPO.TermOfPayment) Then
                                itemLine.Append(oSPPOE.SparePartPO.TermOfPayment.Description & tab)
                            Else
                                itemLine.Append("" & tab)
                            End If
                            sw.WriteLine(itemLine.ToString())
                        Next

                    Next
                End If

                'o2n
            Next
        End If
    End Sub

    Private Function GetNumericOnly(ByVal OriMoney As Decimal) As String
        Return FormatNumber(OriMoney, 0, TriState.False, TriState.UseDefault, TriState.False)
    End Function



#End Region

#Region "EventHandler"

    Private Sub InitiateAuthorization()

        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewSPPO_Estimate_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Estimasi Pesanan")
            End If
            '--exclude  this privilege from Asra (BA)
            'Me.cmdSearch.Visible = SecurityProvider.Authorize(context.User, SR.SearchSPPO_Estimate_Privilege)
            Me.cmdDownload.Visible = SecurityProvider.Authorize(Context.User, SR.DownLoadSPPO_Estimate_Privilege)
            _isShowDetailAllowed = SecurityProvider.Authorize(Context.User, SR.ViewSPPO_EstimateDetail_Privilege)
            If _isPrintAllowed = False And _isShowDetailAllowed = False Then
                Me.dgPOEstimate.Columns(12).Visible = False
            End If
            txtDealerCode.Visible = False
            lblSearchDealer.Visible = False
            lblDealerCode.Visible = True
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.ENHEstimatePesanaKTB_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Estimasi Pesanan")
            End If
            Me.cmdDownload.Visible = False
            txtDealerCode.Visible = True
            lblSearchDealer.Visible = True
            lblDealerCode.Visible = False
        End If


    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        'Put user code to initialize the page here
        If Not IsPostBack Then
            sessHelper.SetSession("SortCol", "CreatedTime")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)

            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            btnGetDealer.Style("display") = "none"
            RetrieveHeader()
            ddlTermOfPayment.Enabled = False
            'RetrieveDetails(dgPOEstimate.CurrentPageIndex)
            Dim org As Dealer = CType(Session("DEALER"), Dealer)
            cmdDownload.Visible = True
            If org.Title = EnumDealerTittle.DealerTittle.LEASING Then
                cmdDownload.Visible = False
            End If
        End If
        'If CType(Session("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
        '    Me.cmdDownload.Visible = False
        'Else
        '    Me.cmdDownload.Visible = True
        'End If

    End Sub

    Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        'If txtDealerCode.Text.Trim = "" And org.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    MessageBox.Show("Kode Dealer tidak boleh kosong")
        'Else
        If (txtDealerCode.Text.Trim <> "") Then
            sessHelper.SetSession("SortCol", "CreatedTime")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
        Else

            sessHelper.SetSession("SortCol", "SparePartPO.Dealer.DealerCode")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
        End If
        dgPOEstimate.CurrentPageIndex = 0
        RetrieveDetails(dgPOEstimate.CurrentPageIndex)
        'End If
    End Sub

    Private Sub dgPOEstimate_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPOEstimate.ItemDataBound
        Dim objPOEstimateHeader As SparePartPOEstimate

        If e.Item.ItemIndex > -1 Then
            objPOEstimateHeader = CType(ArrList(e.Item.ItemIndex), SparePartPOEstimate)

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgPOEstimate.PageSize * dgPOEstimate.CurrentPageIndex)
            e.Item.Cells(2).Text = objPOEstimateHeader.SparePartPO.Dealer.DealerCode
            e.Item.Cells(14).Text = objPOEstimateHeader.SparePartPO.PickingTicket 'Add by reza Req by Mai 11072018

            For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer 'jenis order
                If objPOEstimateHeader.SparePartPO.OrderType.Equals(liOrderType.Value) Then
                    e.Item.Cells(3).Text = liOrderType.Text
                    Exit For
                End If
            Next
            For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer 'tipe dokumen
                If objPOEstimateHeader.DocumentType.Equals(liOrderType.Value) Then
                    e.Item.Cells(4).Text = liOrderType.Text
                    Exit For
                End If
            Next

            Dim ppnFromDbPPNMaster = CalcHelper.GetPPNMasterByTaxTypeId(objPOEstimateHeader.SODate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
            Dim PPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromDbPPNMaster, dpp:=objPOEstimateHeader.POEstimateAmount)
            e.Item.Cells(12).Text = PPN.ToString("#,###")
            e.Item.Cells(13).Text = (PPN + objPOEstimateHeader.POEstimateAmount).ToString("#,###")
            'e.Item.Cells(2).Text = objPOEstimateHeader.SparePartPO.PONumber
            'e.Item.Cells(3).Text = Format(objPOEstimateHeader.SparePartPO.PODate, "dd/MM/yyyy")
            'e.Item.Cells(5).Text = Format(objPOEstimateHeader.SODate, "dd/MM/yyyy")
            'e.Item.Cells(6).Text = objPOEstimateHeader.DeliveryDate
            'e.Item.Cells(7).Text = String.Format("{0:#.###}", CalculatePOEstimateAmount(objPOEstimateHeader.SparePartPOEstimateDetails()))
            'e.Item.Cells(8).Text = (CDec(e.Item.Cells(7).Text) * 0.1).ToString
            'e.Item.Cells(9).Text = (CDec(e.Item.Cells(7).Text) + CDec(e.Item.Cells(8).Text)).ToString

        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            SetDGEstimateDetailItem(e)
        End If
    End Sub

    Private Sub SetDGEstimateDetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not (_isPrintAllowed = False And _isShowDetailAllowed = False) Then
            CType(e.Item.FindControl("lblDetail"), Label).Visible = _isShowDetailAllowed
            CType(e.Item.FindControl("lblPrint"), LinkButton).Visible = _isPrintAllowed
            CType(e.Item.FindControl("lblDetail"), Label).Attributes("onclick") = GeneralScript.GetPopUpEventReference("../SparePart/FrmPurchaseOrderEstimateDetail.aspx?POID=" + e.Item.Cells(0).Text + "", "", 600, 800, "Estimate")
        End If
        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.KTB Then
            CType(e.Item.FindControl("lblDetail"), Label).Visible = True
            CType(e.Item.FindControl("lblPrint"), LinkButton).Visible = False
            CType(e.Item.FindControl("lnkDownload"), LinkButton).Visible = False
            CType(e.Item.FindControl("lblDetail"), Label).Attributes("onclick") = GeneralScript.GetPopUpEventReference("../SparePart/FrmPurchaseOrderEstimateDetail.aspx?POID=" + e.Item.Cells(0).Text + "", "", 600, 800, "Estimate")
        End If
    End Sub

    Private Sub dgPOEstimate_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPOEstimate.PageIndexChanged
        dgPOEstimate.CurrentPageIndex = e.NewPageIndex
        RetrieveDetails(e.NewPageIndex + 1)
    End Sub

    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        'Dim i As Int16
        Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtDealerCode.Text.Trim().Equals("") Then
                MessageBox.Show("Kode Dealer harap di isi")
                Return
            Else
                Dim dealer As String() = txtDealerCode.Text.Trim().Split(";")
                If dealer.Length > 1 Then
                    MessageBox.Show("Pilih salah satu dealer")
                    Return
                Else
                    objDealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
                    If objDealer.ID <= 0 Then
                        MessageBox.Show("Data Tidak ditemukan")
                        Return
                    Else
                        Response.Redirect("../PopUp/PopUpDownloadEstimate.aspx?term2=" & objDealer.SearchTerm2.ToUpper.Trim & "&DealerCode=" & objDealer.DealerCode)
                    End If
                End If
            End If
        Else
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                objDealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
                If objDealer.ID <= 0 Then
                    MessageBox.Show("Data Tidak ditemukan")
                    Return
                End If

            End If

            Response.Redirect("../PopUp/PopUpDownloadEstimate.aspx?term2=" & objDealer.SearchTerm2.ToUpper.Trim & "&DealerCode=" & objDealer.DealerCode)
        End If

        'Dim fileInfo0 As FileInfo
        'Dim fileInfo1 As New FileInfo(Server.MapPath(""))
        'Dim destFilePath As String
        'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        'Dim success As Boolean = False
        'Dim newFileInfo As FileInfo
        'For i = 1 To 5
        '    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, "172.17.104.190")

        '    fileInfo0 = New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SPFileDirectory").ToString & "\SDGROUP0" & i.ToString & ".DLR")
        '    destFilePath = fileInfo1.Directory.FullName & "\" & "DataFile\SP\" & objDealer.SearchTerm2 & "\SDGROUP0" & i.ToString & ".DLR"
        '    newFileInfo = New FileInfo(destFilePath)
        '    If Not newFileInfo.Directory.Exists Then
        '        newFileInfo.Directory.Create()
        '    End If

        '    success = False

        '    newFileInfo = New FileInfo(destFilePath)

        '    If Not newFileInfo.Directory.Exists Then
        '        newFileInfo.Directory.Create()
        '    End If

        '    If (fileInfo0.Exists) Then
        '        Try
        '            success = imp.Start()
        '            If success Then
        '                fileInfo0.CopyTo(destFilePath, True)
        '                imp.StopImpersonate()
        '                imp = Nothing
        '            End If
        '            Dim _download As DonwloadFile = New DonwloadFile(url, Response)
        '            Dim th As Threading.Thread = New Threading.Thread(AddressOf _download.RedirectURL)
        '            th.Start()
        '        Catch ex As Exception
        '            MessageBox.Show("Gagal Download File.")
        '        End Try
        '    Else
        '        MessageBox.Show("SDGROUP0" & i.ToString & ".DLR:  File tidak ada")
        '    End If
        'Next
        'Page.RegisterStartupScript("test", "<script language=JavaScript> showPopUp('../PopUp/PopUpDownloadEstimate.aspx?term2=" & objDealer.SearchTerm2.ToUpper.Trim & ",'',400,400,KodeTipe); </script>")
        '  Response.Redirect("../PopUp/PopUpDownloadEstimate.aspx?term2=" & objDealer.SearchTerm2.ToUpper.Trim)
    End Sub

    Private Sub dgPOEstimate_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPOEstimate.ItemCommand
        If e.CommandName = "Download" Then
            download(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Sub btnGetDealer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDealer.Click
        If txtDealerCode.Text.Length > 0 Then
            Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
            lblDealerName.Text = ObjDealer.DealerName
            lblDealerTerm.Text = ObjDealer.SearchTerm2
        End If
    End Sub

#End Region

    Private Sub dgPOEstimate_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPOEstimate.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortCol") Then
            If sessHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortCol", e.SortExpression)
        dgPOEstimate.SelectedIndex = -1
        dgPOEstimate.CurrentPageIndex = 0
        RetrieveDetails(0)
    End Sub

    Private Sub btnDownloadFaktur_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadFaktur.Click
        FindData(Me._IsForDownload)
        DoDownload(Me.sessHelper.GetSession(Me._sessDataForDownload))
    End Sub

End Class