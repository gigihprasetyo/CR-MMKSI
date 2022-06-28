#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

Public Class PODetails
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents label As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents label24 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgDetailPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents label66 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Total As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCodeValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblCityValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNameValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblContractNumberValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderTypeValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblDailyPONumberValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesOrgValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblProductYearValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblProjectNameValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTermOfPaymentValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblReqAllocValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalAmountValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalBiayaKirimValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisMOValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRegPO As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRegPOValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalPengajuanValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSPLNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblSPLNumberValue As System.Web.UI.WebControls.Label
    Protected WithEvents ibtnDownload As System.Web.UI.WebControls.ImageButton
    Protected WithEvents trSPL As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblNoSurat As System.Web.UI.WebControls.Label
    Protected WithEvents lblLine1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLine2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblJatuhTempo As System.Web.UI.WebControls.Label
    Protected WithEvents chkFreePPh As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents chkCash As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalGuarantee As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents chkFactoring As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblFactoring As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactoringColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblPengirimanBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblPengirimanBy2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPODestinationCode As System.Web.UI.WebControls.Label

    Protected WithEvents trFootNoteIsFactoring As System.Web.UI.HtmlControls.HtmlTableRow

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
    Dim objPOHeader As POHeader
    Dim objPODetail As PODetail
    Dim TotalOrder As Double = 0
    Dim TotalAlokasi As Double = 0
    Dim TotalSelisih As Double = 0
    Dim TotalHarga As Double = 0
    Dim TotalPPH As Double = 0
    Dim TotalInterest As Double = 0
    Dim TotalSubTotal As Double = 0
    Dim TotalBiayaKirim As Double = 0
    Private sessionHelper As New sessionHelper
#End Region

#Region "Custom Method"

    Private Sub GetPOHeader()
        Dim POid As String = Request.QueryString("id")
        objPOHeader = New POHeaderFacade(User).Retrieve(CInt(POid))
    End Sub

    Private Sub BindHeaderToForm()
        Dim objCulture As New System.Globalization.CultureInfo("id-ID")
        lblDealerCodeValue.Text = objPOHeader.ContractHeader.Dealer.DealerCode & "/" & objPOHeader.ContractHeader.Dealer.SearchTerm1
        lblNameValue.Text = objPOHeader.ContractHeader.Dealer.DealerName
        lblContractNumberValue.Text = objPOHeader.ContractHeader.ContractNumber
        lblDailyPONumberValue.Text = objPOHeader.DealerPONumber
        lblCityValue.Text = objPOHeader.ContractHeader.Dealer.City.CityName
        lblNoRegPOValue.Text = objPOHeader.PONumber
        lblOrderTypeValue.Text = CType(objPOHeader.POType, LookUp.EnumJenisOrder).ToString
        lblSalesOrgValue.Text = objPOHeader.ContractHeader.Category.CategoryCode
        lblProductYearValue.Text = objPOHeader.ContractHeader.ProductionYear
        lblProjectNameValue.Text = objPOHeader.ContractHeader.ProjectName
        lblTermOfPaymentValue.Text = objPOHeader.TermOfPayment.Description
        lblStatusValue.Text = CType(objPOHeader.Status, enumStatusPO.Status).ToString
        lblReqAllocValue.Text = Format(objPOHeader.ReqAllocationDateTime, "dd/MM/yyyy")
        lblTanggalPengajuanValue.Text = Format(objPOHeader.CreatedTime, "dd/MM/yyyy")
        lblJenisMOValue.Text = CType(objPOHeader.ContractHeader.ContractType, enumOrderType.OrderType).ToString
        lblNoSurat.Text = objPOHeader.ContractHeader.Dealer.SPANumber & " Tanggal " & objPOHeader.ContractHeader.Dealer.SPADate.ToString("dd MMMM yyyy", objCulture)
        'lblSPADate.Text = " Tanggal " & objPOHeader.ContractHeader.Dealer.SPADate.ToString("dd MMMM yyyy", objCulture)
        'Start  :CR:Dna:Yrk:
        lblJatuhTempo.Text = Format(objPOHeader.ReqAllocationDateTime.AddDays(objPOHeader.TermOfPayment.TermOfPaymentValue), "dd/MM/yyyy")
        'End    :CR:Dna:Yrk:
        'Start  :RemainModule-DailyPO:FreePPh By:Doni N
        chkFreePPh.Checked = IIf(objPOHeader.FreePPh22Indicator = 0, True, False)
        'Start  :RemainModule-DailyPO:FreePPh By:Doni N
        If objPOHeader.Status = CType(enumStatusPO.Status.DiBlok, Integer).ToString Then
            lblTotalAmountValue.Text = "0"
            lblTotalBiayaKirimValue.Text = "0"
        Else
            lblTotalAmountValue.Text = FormatNumber(CType(objPOHeader.TotalHarga, Double) + CType(objPOHeader.TotalHargaPP, Double) + CType(objPOHeader.TotalHargaIT, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotalBiayaKirimValue.Text = FormatNumber(CType(objPOHeader.TotalHargaLC, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
        Dim objPKHead As PKHeader = New PKHeaderFacade(User).Retrieve(objPOHeader.ContractHeader.PKNumber)
        lblSPLNumberValue.Text = objPKHead.SPLNumber
        Dim objDealer As Dealer = Session("DEALER")
        lblJatuhTempo.Text = Format(objPOHeader.ReqAllocationDateTime.AddDays(objPOHeader.TermOfPayment.TermOfPaymentValue), "dd/MM/yyyy")
        If lblSPLNumberValue.Text = String.Empty OrElse objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            trSPL.Visible = True ' False
        Else
            Dim objSPL As SPL = New SPLFacade(User).Retrieve(lblSPLNumberValue.Text)
            ibtnDownload.Visible = (SecurityProvider.Authorize(Context.User, SR.ENHSalesGeneralApplikasiDownload_Previlege) AndAlso objSPL.SPLNumber <> String.Empty)
        End If
        'Start  :CR:Guarantee & CashPayment;By:Doni;For:Yurike;Date:20100223
        'Dim TotCash As Decimal = 0
        'If objPOHeader.SalesOrders.Count > 0 Then
        '    TotCash = CType(objPOHeader.SalesOrders(0), SalesOrder).CashPayment
        'End If
        'chkCash.Checked = IIf(TotCash > 0, True, False)
        chkCash.Checked = IIf(objPOHeader.TotalGuarantee() > 0, True, False)
        If Not chkCash.Checked Then
            lblInfo.Visible = False
            lblTotalGuarantee.Visible = False
        Else
            lblInfo.Visible = True
            lblTotalGuarantee.Visible = True
        End If
        Me.lblTotalGuarantee.Text = FormatNumber(objPOHeader.TotalGuarantee, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'End    :CR:Guarantee & CashPayment;By:Doni;For:Yurike;Date:20100223

        'Start  :Factoring;by:dna;on:20101004;for:yurike;remark:set control
        Me.chkFactoring.Checked = IIf(objPOHeader.IsFactoring = 1, True, False)
        Me.chkFactoring.Enabled = False
        'End    :Factoring;by:dna;on:20101004;for:yurike;remark:set control

        'Add PODestination By WDI for Isye 20170125
        If Not IsNothing(objPOHeader.PODestination) AndAlso objPOHeader.PODestination.ID > 1 Then
            lblPengirimanBy.Text = "Pengiriman oleh MMKSI"
            lblPengirimanBy2.Text = ":"
            lblPODestinationCode.Text = objPOHeader.PODestination.Code & "/ " & objPOHeader.PODestination.Nama
        Else
            lblPengirimanBy.Text = "Pengiriman oleh Dealer"
            lblPODestinationCode.Text = ""
        End If

        If Me.chkFactoring.Checked = True Then
            trFootNoteIsFactoring.Visible = True
        Else
            trFootNoteIsFactoring.Visible = False
        End If

    End Sub

    Private Sub BindDetailToGrid()
        For Each pod As PODetail In objPOHeader.PODetails

        Next

        dtgDetail.DataSource = objPOHeader.PODetails
        dtgDetail.DataBind()
    End Sub
    Private Sub SetControls()
        Dim IsImplementFactoring As Boolean = IIf(KTB.DNet.Lib.WebConfig.GetValue("ImpelementFactoring") = 1, True, False)
        If IsImplementFactoring Then
            Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(CType(Session("DEALER"), Dealer).ID, EnumDealerTransType.DealerTransKind.Factoring)
            If Not (Not IsNothing(oTC) AndAlso oTC.Status = 1) Then
                IsImplementFactoring = False
            End If
        End If
        lblFactoring.Visible = IsImplementFactoring
        lblFactoringColon.Visible = IsImplementFactoring
        chkFactoring.Visible = IsImplementFactoring
        If chkFactoring.Checked AndAlso chkFactoring.Visible = False Then chkFactoring.Visible = True
    End Sub
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Label10.Visible = isPriceVisible
        Label9.Visible = isPriceVisible
        Total.Visible = isPriceVisible
        lblTotalAmountValue.Visible = isPriceVisible
        lblTotalBiayaKirimValue.Visible = isPriceVisible
        dtgDetail.Columns(7).Visible = isPriceVisible
        dtgDetail.Columns(8).Visible = isPriceVisible
        dtgDetail.Columns(9).Visible = isPriceVisible
        dtgDetail.Columns(10).Visible = isPriceVisible

        If Not IsPostBack Then
            GetPOHeader()
            BindHeaderToForm()
            BindDetailToGrid()
            If Not SecurityProvider.Authorize(Context.User, SR.PengajuanPOView_Detail) Then
                Response.Redirect("../frmAccessDenied.aspx?modulName=Detail PO")
            End If
        End If
        'btnKembali.Attributes.Add("OnClick", "window.history.go(-1)")
        SetControls()
        Dim IsImplementFactoring As Boolean = IIf(KTB.DNet.Lib.WebConfig.GetValue("ImpelementFactoring") = 1, True, False)

        lblFactoring.Visible = IsImplementFactoring
        lblFactoringColon.Visible = IsImplementFactoring
        chkFactoring.Visible = IsImplementFactoring
    End Sub

    Sub dtgDetail_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        Dim _dealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        If Not (objPOHeader.PODetails.Count = 0 Or e.Item.ItemIndex = -1) Then
            objPODetail = objPOHeader.PODetails(e.Item.ItemIndex)
            If _dealer.Title = EnumDealerTittle.DealerTittle.DEALER And objPOHeader.Status = enumStatusPO.Status.Konfirmasi Then
                objPODetail.AllocQty = 0
                e.Item.Cells(5).Text = "0"
            End If

            e.Item.Cells(2).Text = objPODetail.ContractDetail.VechileColor.MaterialNumber
            e.Item.Cells(3).Text = objPODetail.ContractDetail.VechileColor.MaterialDescription
            e.Item.Cells(6).Text = CType(objPODetail.ReqQty, Short) - CType(objPODetail.AllocQty, Short)

            Dim arrPKDtl As ArrayList
            Dim objPKHead As PKHeader = New PKHeaderFacade(User).Retrieve(objPODetail.POHeader.ContractHeader.PKNumber)
            arrPKDtl = objPKHead.PKDetails

            Dim isTransControlPO As Boolean = CommonFunction.CheckTransactionControlPO(_dealer)
            Dim MaxTOPDayValue As Integer = 0
            Dim FreeDaysValue As Integer = 0

            If isTransControlPO Then
                MaxTOPDayValue = objPODetail.MaxTOPDay
                FreeDaysValue = objPODetail.FreeDays
            Else
                For Each row As PKDetail In arrPKDtl
                    If row.VechileColor.ID = objPODetail.ContractDetail.VechileColor.ID Then
                        MaxTOPDayValue = row.MaxTOPDay
                        FreeDaysValue = row.FreeDays
                    End If
                Next
            End If

            Dim tmpSubTotal As Double = 0
            If objPOHeader.Status = enumStatusPO.Status.Baru OrElse objPOHeader.Status = enumStatusPO.Status.Batal OrElse objPOHeader.Status = enumStatusPO.Status.Konfirmasi OrElse objPOHeader.Status = enumStatusPO.Status.Ditolak Then
                e.Item.Cells(7).Text = FormatNumber(CType(objPODetail.ReqQty, Double) * CType(objPODetail.ContractDetail.Amount, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)



                e.Item.Cells(8).Text = FormatNumber(CType(objPODetail.ReqQty, Double) * CType(objPODetail.ContractDetail.PPh22, Double) * CInt(objPODetail.POHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                '' CR Sirkular Rewards
                '' by : ali Akbar
                '' 2014-09-24
                If objPOHeader.IsFactoring = 1 Then

                    Dim objPrice As Price = New Price
                    Dim _VehiclePrice As Double = 0
                    Dim _PPh22 As Double = 0

                    'objPrice = New PriceFacade(User).RetrieveByCriteria(objPODetail.ContractDetail)
                    '_VehiclePrice = Calculation.CountRewardsVehiclePrice(objPODetail.ContractDetail, objPrice, objPOHeader.TermOfPayment.TermOfPaymentValue, DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth))
                    '_PPh22 = Calculation.CountRewardPPh22(objPODetail.ContractDetail, objPrice, objPOHeader.TermOfPayment.TermOfPaymentValue, DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth))

                    'objPrice = New PriceFacade(User).RetrieveByCriteria(objPODetail.ContractDetail)
                    _VehiclePrice = objPODetail.Price
                    _PPh22 = objPODetail.PPh22


                    e.Item.Cells(7).Text = FormatNumber(CType(objPODetail.ReqQty, Double) * (_VehiclePrice), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    e.Item.Cells(8).Text = FormatNumber(CType(objPODetail.ReqQty, Double) * (_PPh22) * CInt(objPOHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                End If

                '' END OF CR Sirkular Rewards

                'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                'e.Item.Cells(8).Text = FormatNumber(CType(objPODetail.ReqQty, Double) * CType(objPODetail.ContractDetail.PPh22, Double) * IIf(CInt(objPODetail.POHeader.FreePPh22Indicator) = 1, 0, 1), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'End    :RemainModule-DailyPO:FreePPh By:Doni N
                TotalHarga += CType(e.Item.Cells(7).Text, Double)
                TotalPPH += CType(e.Item.Cells(8).Text, Double)

                e.Item.Cells(9).Text = FormatNumber(CType(objPODetail.ReqQty, Double) * CType(objPODetail.Interest, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                TotalInterest += CType(e.Item.Cells(9).Text, Double)
                'e.Item.Cells(10).Text = FormatNumber(CType(objPODetail.ReqQty, Double) * (CType(objPODetail.ContractDetail.Amount, Double) + CType(objPODetail.Interest, Double) + (CType(objPODetail.ContractDetail.PPh22, Double) * CInt(objPODetail.POHeader.FreePPh22Indicator))), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                tmpSubTotal = CType(e.Item.Cells(7).Text, Double) + CType(e.Item.Cells(8).Text, Double) + CType(e.Item.Cells(9).Text, Double)
                e.Item.Cells(10).Text = FormatNumber(tmpSubTotal, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(11).Text = FormatNumber(CType(objPODetail.ReqQty, Double) * CType(objPODetail.LogisticCost, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                TotalBiayaKirim += CType(e.Item.Cells(11).Text, Double)
                TotalSubTotal += tmpSubTotal
            Else
                e.Item.Cells(7).Text = FormatNumber(CType(objPODetail.AllocQty, Double) * CType(objPODetail.ContractDetail.Amount, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(8).Text = FormatNumber(CType(objPODetail.AllocQty, Double) * CType(objPODetail.ContractDetail.PPh22, Double) * CInt(objPODetail.POHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                '' CR Sirkular Rewards
                '' by : ali Akbar
                '' 2014-09-24
                If objPOHeader.IsFactoring = 1 Then

                    Dim objPrice As Price = New Price
                    Dim _VehiclePrice As Double = 0
                    Dim _PPh22 As Double = 0

                    objPrice = New PriceFacade(User).RetrieveByCriteria(objPODetail.ContractDetail)
                    '_VehiclePrice = Calculation.CountRewardsVehiclePrice(objPODetail.ContractDetail, objPrice, objPOHeader.TermOfPayment.TermOfPaymentValue, DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth))
                    '_PPh22 = Calculation.CountRewardPPh22(objPODetail.ContractDetail, objPrice, objPOHeader.TermOfPayment.TermOfPaymentValue, DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth))
                    _VehiclePrice = objPODetail.Price
                    _PPh22 = objPODetail.PPh22

                    e.Item.Cells(7).Text = FormatNumber(CType(objPODetail.AllocQty, Double) * (_VehiclePrice), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    e.Item.Cells(8).Text = FormatNumber(CType(objPODetail.AllocQty, Double) * (_PPh22) * CInt(objPOHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)


                End If

                '' END OF CR Sirkular Rewards

                TotalHarga += CType(e.Item.Cells(7).Text, Double)
                'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                'e.Item.Cells(8).Text = FormatNumber(CType(objPODetail.AllocQty, Double) * CType(objPODetail.ContractDetail.PPh22, Double) * IIf(CInt(objPODetail.POHeader.FreePPh22Indicator) = 1, 0, 1), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'End    :RemainModule-DailyPO:FreePPh By:Doni N
                TotalPPH += CType(e.Item.Cells(8).Text, Double)
                e.Item.Cells(9).Text = FormatNumber(CType(objPODetail.AllocQty, Double) * CType(objPODetail.Interest, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                TotalInterest += CType(e.Item.Cells(9).Text, Double)
                'e.Item.Cells(10).Text = FormatNumber(CType(objPODetail.AllocQty, Double) * (CType(objPODetail.ContractDetail.Amount, Double) + (CType(objPODetail.ContractDetail.PPh22, Double) * CInt(objPODetail.POHeader.FreePPh22Indicator))), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                tmpSubTotal = CType(e.Item.Cells(7).Text, Double) + CType(e.Item.Cells(8).Text, Double) + CType(e.Item.Cells(9).Text, Double)
                e.Item.Cells(10).Text = FormatNumber(tmpSubTotal, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(11).Text = FormatNumber(CType(objPODetail.AllocQty, Double) * CType(objPODetail.LogisticCost, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                TotalBiayaKirim += CType(e.Item.Cells(11).Text, Double)
                TotalSubTotal += tmpSubTotal
            End If
            Me.TotalOrder += CType(e.Item.Cells(4).Text, Double)
            Me.TotalAlokasi += CType(e.Item.Cells(5).Text, Double)
            Me.TotalSelisih += CType(e.Item.Cells(6).Text, Double)

            Dim lblMaxTOPDays As Label = e.Item.FindControl("lblMaxTOPDays")
            Dim lblFreeDays As Label = e.Item.FindControl("lblFreeDays")
            lblMaxTOPDays.Text = MaxTOPDayValue
            lblFreeDays.Text = FreeDaysValue
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(3).Text = "Sub Total : "

            e.Item.Cells(4).Text = FormatNumber(TotalOrder, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(5).Text = FormatNumber(TotalAlokasi, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(6).Text = FormatNumber(TotalSelisih, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            e.Item.Cells(7).Text = FormatNumber(TotalHarga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(8).Text = FormatNumber(TotalPPH, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(9).Text = FormatNumber(TotalInterest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(10).Text = FormatNumber(TotalSubTotal, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(11).Text = FormatNumber(TotalBiayaKirim, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Private Sub ibtnDownload_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnDownload.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.Exact, lblSPLNumberValue.Text.Trim()))
        Dim arrList As ArrayList = New SPLFacade(User).Retrieve(criterias)
        If arrList.Count > 0 Then
            Dim ObjSPL As SPL = CType(arrList(0), SPL)
            Dim file As String = ObjSPL.Attachment
            Dim fInfo As New System.IO.FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & file)
            'If fInfo.Exists Then
            Try
                Response.Redirect("../Download.aspx?file=" & file)
            Catch ex As Exception
                MessageBox.Show(SR.DownloadFail(file))
            End Try
            'Else
            '    MessageBox.Show(SR.FileNotFound(fInfo.Name))
            'End If
        Else
            MessageBox.Show(SR.DataNotFound("SPL Number"))
        End If
    End Sub

#End Region


    Private Sub btnBack_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub
End Class