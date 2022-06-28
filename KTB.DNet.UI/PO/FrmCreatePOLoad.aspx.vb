#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.Configuration
Imports System.Dynamic
#End Region

Public Class FrmCreatePOLoad
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSave As System.Web.UI.WebControls.Label
    Protected WithEvents LbVal As System.Web.UI.WebControls.Label
    Protected WithEvents LblInit As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents label66 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblContractNumber As System.Web.UI.WebControls.Label
    Protected WithEvents Order As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderType As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesOrg As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblProductYear As System.Web.UI.WebControls.Label
    Protected WithEvents label24 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblProjectName As System.Web.UI.WebControls.Label
    Protected WithEvents Total As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnKirim As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnHitung As System.Web.UI.WebControls.Button
    Protected WithEvents icPermintaanKirim As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents lblTotalHargaValue As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents lblA As System.Web.UI.WebControls.Label
    Protected WithEvents lblC As System.Web.UI.WebControls.Label
    Protected WithEvents lblB As System.Web.UI.WebControls.Label
    Protected WithEvents lblD As System.Web.UI.WebControls.Label
    Protected WithEvents lblAvailable As System.Web.UI.WebControls.Label
    Protected WithEvents lblAvCeilingFirst As System.Web.UI.WebControls.Label
    Protected WithEvents chkFreePPh As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkFactoring As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblFactoring As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactoringColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblCeiling As System.Web.UI.WebControls.Label
    Protected WithEvents lblProposed As System.Web.UI.WebControls.Label
    Protected WithEvents lblLiquified As System.Web.UI.WebControls.Label
    Protected WithEvents lblOutstanding As System.Web.UI.WebControls.Label
    Protected WithEvents lblTodayAvCeiling As System.Web.UI.WebControls.Label
    Protected WithEvents lblTomorrowAvCeiling As System.Web.UI.WebControls.Label
    Protected WithEvents txtID As System.Web.UI.WebControls.TextBox
    Protected WithEvents CtlTimeElapsed As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents divPage As System.Web.UI.HtmlControls.HtmlGenericControl

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
    Private objPOHeader As POHeader
    Private objPODetail As PODetail
    Private objContractHeader As ContractHeader
    Private objContractDetail As ContractDetail
    Private sessionHelper As New SessionHelper
    Private SubTotalItem As Integer
    Private SubTotalharga As Double
    Private SubTotalPPh As Double
    Private SubTotalInterest As Double
    Private subTotalDeposit As Double
    Private arrOrder As New ArrayList
    Private objDealer As Dealer
    Private nTOP As Integer
    Private nMonth As Integer
    Private objSPL As SPL
    Private sb As System.Text.StringBuilder
    Private objNHFac As NationalHolidayFacade = New NationalHolidayFacade(User)
    Private objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
    Private IsEffDateOptimized As Boolean = False '20100329
    Private Const VS_DealerCode As String = "DealerCode"
    Private Const S_Dealer As String = "SDealer"
    Private Const Temp_Dealer As String = "TempDealer"

#End Region



#Region "Testing"

    Private Sub LoadSiDealer()
        Session(Temp_Dealer) = CType(Session("Dealer"), Dealer)
        If (Not IsNothing(Request.QueryString("DealerCode"))) Then
            Dim _ObjDeal As DealerFacade = New DealerFacade(User)
            Dim _objDealer As Dealer = _ObjDeal.Retrieve(CType(Request.QueryString("DealerCode"), String))
            If (_objDealer.ID > 0) Then
                Session(S_Dealer) = _objDealer

                Session("Dealer") = _objDealer
            Else
                SendError("Silahkan MAsukan DealerCode Dengan Benar !!!!!")
            End If

            ''GEt si Kontrak
            ViewState("ContractID") = GetCOntractID()

            If (CInt(ViewState("ContractID")) = 0) Then
                SendError("COntractnya habis !!!!")
            End If

        Else
            SendError("Silahkan MAsukan DealerCode Dengan Benar !!!!")
        End If
    End Sub

    Private Sub SendError(ByVal PesanDaKesan As String)
        Dim ctx As HttpContext = HttpContext.Current
        Session("Dealer") = CType(Session("TempDealer"), Dealer)
        Response.Redirect(ctx.Request.ApplicationPath & "/ErrorCode.aspx?error=" & Server.UrlEncode(PesanDaKesan))
    End Sub

    Private Function GetCOntractID() As Integer
        GetCOntractID = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dealer As String = CType(Session("Dealer"), Dealer).DealerCode

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "Dealer.DealerCode", MatchType.Exact, dealer))
       
        Dim tanggal As DateTime = DateTime.Now.AddDays(1)
         criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractPeriodMonth", MatchType.Exact, CInt(tanggal.Month)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractPeriodYear", MatchType.Exact, CInt(tanggal.Year)))
         
        'End    : RemainModule : DailyPO -> FreePPh Doni N
        'Start  : 20131203:donas:angga:TOP Interest
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "IsCarriedOver", MatchType.No, 1))
        'End    : : 20131203:donas:angga:TOP Interest

        Dim ArrListContractHeader As ArrayList = New ContractHeaderFacade(User).Retrieve(criterias)

        For Each objContractHeader As ContractHeader In ArrListContractHeader
            If objContractHeader.TotalSisaUnit > 0 AndAlso objContractHeader.Status <> "LOCK" AndAlso objContractHeader.IsCarriedOver <> 1 Then
                GetCOntractID = objContractHeader.ID
                Return objContractHeader.ID
            End If
        Next

    End Function
#End Region

#Region "Custom Method"

    Private Sub ClearFields()
        txtDealerPONumber.Text = String.Empty
        ddlTermOfPayment.SelectedIndex = 0
        ddlOrderType.SelectedIndex = 0
        icPermintaanKirim.Value = DateTime.Now
        lblTotalHargaValue.Text = "0"
        For Each item As DataGridItem In dtgDetail.Items
            Dim txtBox As TextBox = item.FindControl("TextBox1")
            txtBox.Text = "0"
        Next
    End Sub


    Private Function IsCreatePOWithTOPValid() As Boolean
        Dim _connectionString As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionString")
        Dim objContractHeader As ContractHeader = sessionHelper.GetSession("Contract")
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        Dim objTOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
        Dim totalPengajuan As Long = 0
        Dim ACC As Long = 0
        If objTOP.TermOfPaymentValue = 0 Then
            Return True
        Else
            BindToPOHeaderObject()
            ACC = New POHeaderFacade(User).CountACC(objContractHeader, objDealer, objTOP, objPOHeader, _connectionString)
            For Each item As PODetail In objPOHeader.PODetails
                totalPengajuan += CType(item.ReqQty, Double) * (CType(item.ContractDetail.Amount, Double))
            Next
        End If

        If totalPengajuan > ACC Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function ValidasiWaktuPengajuan() As Boolean
        objDealer = Session("DEALER")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, icPermintaanKirim.Value.Day))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, icPermintaanKirim.Value.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, icPermintaanKirim.Value.Year))
        Dim arlNationalHoliday As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criterias)
        If (arlNationalHoliday.Count = 0) Then
            'start  :check, buat hari libur, dan tanggal permintaan kirim adalah hari pertama masuk setelah libur
            If CommonFunction.AddNWorkingDay(DateSerial(Now.AddDays(-1).Year, Now.AddDays(-1).Month, Now.AddDays(-1).Day), 1, False).ToString("yyyyMMdd") <> Now.ToString("yyyyMMdd") Then 'it means holiday
                If CommonFunction.AddNWorkingDay(DateSerial(Now.AddDays(-1).Year, Now.AddDays(-1).Month, Now.AddDays(-1).Day), 1, False).ToString("yyyyMMdd") = Me.icPermintaanKirim.Value.ToString("yyyyMMdd") Then
                    'MessageBox.Show("Tanggal permintaan kirim " & Me.icPermintaanKirim.Value.ToString("dd MMM yyyy") & " tidak bisa dibuat hari ini. \nPengajuan maksimal pada tanggal " & CommonFunction.AddNWorkingDay(Now, 1, True).ToString("dd MMM yyyy"))
                    'Return False
                End If
            End If
            'end    :check, buat hari libur, dan tanggal permintaan kirim adalah hari pertama masuk setelah libur
            objContractHeader = sessionHelper.GetSession("Contract")
            If Not IsNothing(objContractHeader) Then '-- Add by Sony
                If ddlOrderType.SelectedValue = LookUp.EnumJenisOrder.Harian Then
                    Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, EnumDealerTransType.DealerTransKind.POBulanan)
                    If Not (objTransaction Is Nothing) Then
                        If objTransaction.Status = 0 Then
                            'MessageBox.Show("Maaf Anda Tidak Punya Akses membuat PO Harian")
                            'Return False
                        Else
                            If (icPermintaanKirim.Value > DateTime.Now) AndAlso (icPermintaanKirim.Value.Month = objContractHeader.ContractPeriodMonth) Then
                                If Not (icPermintaanKirim.Value.Date = DateTime.Now.Date) Then
                                    Dim nextDate As Date = New NationalHolidayFacade(User).RetrieveNextDay(DateTime.Now.AddDays(1))
                                    If (icPermintaanKirim.Value.Date = nextDate.Date) Then
                                        Dim Batas As String() = KTB.DNet.Lib.WebConfig.GetValue("BatasPOHarian").ToString.Split(":")
                                        Dim Waktu As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CInt(Batas(0)), CInt(Batas(1)), 0)
                                        If Not (DateTime.Now < Waktu) Then
                                            'MessageBox.Show("Batas Waktu pembuatan PO untuk pengiriman Besok sudah lewat")
                                            'MessageBox.Show(SR.InvalidCreateDate("PO"))
                                            'Return False
                                        End If
                                    End If
                                Else
                                    'MessageBox.Show(SR.InvalidSendDate)
                                End If
                            Else
                                'reamrks by anh 20160630 req by yurike
                                Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetValue("PODateAllowed") 'yyyyMMdd - 20141031
                                If Not (Date.Now.ToString("yyyyMMdd") = strPODateAllowed) Then
                                    'MessageBox.Show(SR.InvalidSendDate)
                                    'Return False
                                End If
                                'end reamrks by anh 20160630 req by yurike
                            End If
                        End If
                    End If
                Else
                    Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, EnumDealerTransType.DealerTransKind.POTambahan)
                    If Not (objTransaction Is Nothing) Then
                        If objTransaction.Status = 0 Then
                            'MessageBox.Show("Maaf Anda Tidak Punya Akses Membuat PO Tambahan")
                            'Return False
                        Else
                            If Not (IsValidPOTambahan()) Then
                                'MessageBox.Show(SR.InvalidCreateDate("PO Tambahan"))
                                'Return False
                            Else
                                Dim nextDatePO As Date = New NationalHolidayFacade(User).RetrieveNextDay(DateTime.Now.AddDays(1))
                                Dim startDatePO As Date = New Date(nextDatePO.Year, nextDatePO.Month, nextDatePO.Day, 0, 0, 0)
                                Dim endDatePO As Date = New Date(nextDatePO.Year, nextDatePO.Month, nextDatePO.Day, 23, 59, 59)

                                If Not ((icPermintaanKirim.Value.Date >= startDatePO) And (icPermintaanKirim.Value.Date <= endDatePO)) Then
                                    'reamrks by anh 20160630 req by yurike
                                    Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetValue("PODateAllowed") 'yyyyMMdd - 20141031
                                    If Not (Date.Now.ToString("yyyyMMdd") = strPODateAllowed) Then
                                        'MessageBox.Show(SR.InvalidSendDate)
                                        'Return False
                                    End If

                                    'end reamrks by anh 20160630 req by yurike
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Else
            'MessageBox.Show("Permintaan kirim tidak boleh di hari Libur kerja (" & arlNationalHoliday(0).Description & ")")
            'Return False
        End If
        'start   : OC carry over dengan pricing-date baru (contoh : oc carryover 2 Jan tapi pakai pricingdate 15 jan, karena lebih murah, permintaan dealer
        GetContract()
        Dim PricingDate As Date = New Date(objContractHeader.PricePeriodYear, objContractHeader.PricePeriodMonth, objContractHeader.PricePeriodDay)
        Dim DeliveryDate As Date = Me.icPermintaanKirim.Value
        If DeliveryDate < PricingDate Then
            'MessageBox.Show("Tanggal permintaan kirim tidak boleh sebelum tanggal berlakunya harga kendaraan " & PricingDate.ToString("dd MM yyyy") & ". Untuk lebih lanjut hubungi MMKSI – Whole Sales Dept.")
            'Return False
        End If
        'end     : OC carry over dengan pricing-date baru (contoh : oc carryover 2 Jan tapi pakai pricingdate 15 jan, karena lebih murah, permintaan dealer
        Return True
    End Function

    Private Function POIsValid() As Boolean
        'replaced with ValidasiWaktuPengajuan
        Dim objPKHeader As PKHeader = New PKHeaderFacade(User).Retrieve(objContractHeader.PKNumber)
        If objPKHeader.ID < 1 Then
            MessageBox.Show("Nomor PK : " & objContractHeader.PKNumber & " tidak ditemukan.")
            Return False
        End If
        objSPL = New SPLFacade(User).Retrieve(objPKHeader.SPLNumber.ToString())
        Dim objTOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
        Dim MaxTOPValue As Integer = 0


        'If objPKHeader Is Nothing OrElse objPKHeader.MaxTopIndicator = -1 Then
        '    MaxTOPValue = DateTime.DaysInMonth(objContractHeader.ContractPeriodYear, objContractHeader.ContractPeriodMonth)
        'ElseIf objPKHeader.MaxTopIndicator = 0 Then
        '    MaxTOPValue = objPKHeader.MaxTOPDate.Subtract(icPermintaanKirim.Value).Days
        'ElseIf objPKHeader.MaxTopIndicator = 1 Then
        '    MaxTOPValue = objPKHeader.MaxTopDay
        'End If
        'If objTOP.TermOfPaymentValue > MaxTOPValue Then
        '    MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
        '    Return False
        'ElseIf objTOP.TermOfPaymentValue < MaxTOPValue Then
        '    If Not (objPKHeader Is Nothing OrElse objPKHeader.MaxTopIndicator = -1) Then
        '        If ViewState("ShowMessage") = "OK" Then
        '            Return True
        '        Else
        '            ViewState("ShowMessage") = "OK"
        '            MessageBox.Show("Maximum TOP yang bisa anda gunakan " & MaxTOPValue & " Hari")
        '            Return False
        '        End If
        '    End If
        'End If

        Dim dueDate As Date = icPermintaanKirim.Value.AddDays(objTOP.TermOfPaymentValue)
        'TODO KEMUNGKINAN AKAN DI IMPLement
        'Dim criteriasDueDate As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criteriasDueDate.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, dueDate.Day))
        'criteriasDueDate.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, dueDate.Month))
        'criteriasDueDate.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, dueDate.Year))
        'Dim arlDueDateNationalHoliday As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criteriasDueDate)
        'If Not (arlDueDateNationalHoliday.Count = 0) Then
        '    MessageBox.Show("Jatuh tempo hari libur, Silahkan pilih cara pembayaran yang lain.")
        '    Return False
        'End If

        'If objPOHeader.IsFactoring = 1 Then
        '    MaxTOPValue = DateTime.DaysInMonth(objContractHeader.ContractPeriodYear, objContractHeader.ContractPeriodMonth)
        '    If objTOP.TermOfPaymentValue > (MaxTOPValue) Then
        '        MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
        '        Return False
        '    End If

        '    'Return True
        'End If
        'Request Dari Bu Rini 11/2/2008 jan 11.00
        If objPOHeader.IsFactoring = 1 OrElse objPKHeader Is Nothing OrElse objPKHeader.MaxTopIndicator = -1 Then
            MaxTOPValue = POHeaderFacade.GetMinTOPDaysByVehicleType(objPOHeader, objPOHeader.PODetails, (objPOHeader.IsFactoring = 1))
            If MaxTOPValue = 0 Then
                MaxTOPValue = DateTime.DaysInMonth(objContractHeader.ContractPeriodYear, objContractHeader.ContractPeriodMonth)
            End If
            'If objTOP.TermOfPaymentValue > (MaxTOPValue - icPermintaanKirim.Value.Day) Then
            '    MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & (MaxTOPValue - icPermintaanKirim.Value.Day) & " hari")
            '    Return False
            'End If
            If objTOP.TermOfPaymentValue > (MaxTOPValue) Then
                'MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
                'Return False
            End If

        End If

        If objPOHeader.IsFactoring <> 1 Then
            If ddlTermOfPayment.SelectedItem.Text.Trim.ToUpper.EndsWith("TOP") Then
                If objPKHeader Is Nothing OrElse objPKHeader.MaxTopIndicator = 0 Then
                    MaxTOPValue = objPKHeader.MaxTOPDate.Subtract(icPermintaanKirim.Value).Days
                    If objTOP.TermOfPaymentValue > MaxTOPValue Then
                        'MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
                        'Return False
                    Else
                        If ViewState("ShowMessage") = "OK" Then
                            Return True
                        Else
                            ViewState("ShowMessage") = "OK"
                            'MessageBox.Show("Maximum TOP yang bisa anda gunakan " & MaxTOPValue & " Hari")
                            'Return False
                        End If
                    End If
                End If

                If objPKHeader Is Nothing OrElse objPKHeader.MaxTopIndicator = 1 Then
                    MaxTOPValue = objPKHeader.MaxTopDay
                    If objTOP.TermOfPaymentValue > MaxTOPValue Then
                        'MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
                        'Return False
                    Else
                        If ViewState("ShowMessage") = "OK" Then
                            Return True
                        Else
                            ViewState("ShowMessage") = "OK"
                            'MessageBox.Show("Maximum TOP yang bisa anda gunakan " & MaxTOPValue & " Hari")
                            'Return False
                        End If
                    End If
                End If
            End If
        End If

        Return True
    End Function

    Private Function IsValidPOTambahan() As Boolean
        'session contract
        'Dim objcontrak As ContractHeader = CType(sessionHelper.GetSession("Contract"), ContractHeader)
        'If Not ((DateTime.Now.Month = objcontrak.ContractPeriodMonth) Or (DateTime.Now.Year = objcontrak.ContractPeriodYear)) Then
        '    Return False
        'End If
        Dim BatasAwal As String() = KTB.DNet.Lib.WebConfig.GetValue("AwalBatasPOTambahan").ToString.Split(":")
        Dim BatasAkhir As String() = KTB.DNet.Lib.WebConfig.GetValue("AkhirBatasPOTambahan").ToString.Split(":")
        Dim WaktuAwal As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CInt(BatasAwal(0)), CInt(BatasAwal(1)), 0)
        Dim WaktuAkhir As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CInt(BatasAkhir(0)), CInt(BatasAkhir(1)), 0)
        If Not ((DateTime.Now >= WaktuAwal) And (DateTime.Now <= WaktuAkhir)) Then
            Return False
        End If
        Return True
    End Function

    Private Sub BindToPOHeaderObject()
        objPOHeader = New POHeader
        objContractHeader = sessionHelper.GetSession("Contract")
        If Not IsNothing(objContractHeader) Then  '---Add by Sony
            objPOHeader.ContractHeader = objContractHeader
            objPOHeader.Dealer = objContractHeader.Dealer
            If txtDealerPONumber.Text <> String.Empty Then
                Dim _DealerPONumber As String
                _DealerPONumber = txtDealerPONumber.Text.Replace(",", "")
                _DealerPONumber = _DealerPONumber.Replace(";", "")
                objPOHeader.DealerPONumber = _DealerPONumber
            End If
            objPOHeader.ReqAllocationDate = icPermintaanKirim.Value.Day
            objPOHeader.ReqAllocationMonth = icPermintaanKirim.Value.Month
            objPOHeader.ReqAllocationYear = icPermintaanKirim.Value.Year
            objPOHeader.ReqAllocationDateTime = icPermintaanKirim.Value.Date
            objPOHeader.POType = ddlOrderType.SelectedValue
            objPOHeader.Status = enumStatusPO.Status.Baru
            objPOHeader.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
            'Start  :RemainModule-DailyPO:Free PPH DoniN
            objPOHeader.FreePPh22Indicator = objPOHeader.ContractHeader.FreePPh22Indicator
            Me.SetFreePPh() 'get the latest FreePPh Status
            objPOHeader.FreePPh22Indicator = IIf(chkFreePPh.Checked = True, 0, 1) '  objPOHeader.ContractHeader.FreePPh22Indicator
            'Start  :RemainModule-DailyPO:Free PPH DoniN
            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100329
            objPOHeader.EffectiveDate = New POHeaderFacade(User).GetPOEffectiveDate(objPOHeader.ReqAllocationDateTime, objPOHeader.TermOfPayment.PaymentType, objPOHeader.TermOfPayment.TermOfPaymentValue)
            'End    :Optimize EffectiveDate calculation;By:DoniN;20100329
            objPOHeader.IsFactoring = IIf(chkFactoring.Checked, 1, 0)
            BindToPODetailObject()
        End If
    End Sub

    Private Sub BindToPODetailObject()
        For Each dtgitem As DataGridItem In dtgDetail.Items
            Dim txtBox As TextBox = dtgitem.FindControl("TextBox1")
            If Not ((txtBox.Text = String.Empty) OrElse (txtBox.Text = "0")) Then
                objPODetail = GetPODetail(dtgitem.ItemIndex, txtBox.Text)
                objPODetail.ReqQty = txtBox.Text
                objPOHeader.PODetails.Add(objPODetail)
            End If
        Next
    End Sub

    Private Function GetPODetail(ByVal index As Integer, ByVal reqQty As Integer)
        objContractDetail = objContractHeader.ContractDetails(index)
        If Not IsNothing(objContractDetail) Then
            Dim poDetail As New PODetail
            poDetail.ContractDetail = objContractDetail
            poDetail.Discount = objContractDetail.Discount
            poDetail.LineItem = objContractDetail.LineItem
            poDetail.POHeader = objPOHeader
            poDetail.Price = objContractDetail.Amount




            If reqQty < 1 Then
                poDetail.Interest = 0
            Else
                If objPOHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
                    poDetail.Interest = CType(CType(dtgDetail.Items(index).FindControl("lblInterest"), Label).Text, Decimal) / reqQty
                Else
                    poDetail.Interest = 0

                End If
                ' poDetail.Interest = CType(CType(dtgDetail.Items(index).FindControl("lblInterest"), Label).Text, Decimal) / reqQty
            End If

            '' CR Sirkular Rewards
            '' by : ali Akbar
            '' 2014-09-24
            If objPOHeader.IsFactoring Then
                Dim objPrice As Price
                objPrice = New PriceFacade(User).RetrieveByCriteria(objContractDetail)

                nTOP = objPOHeader.TermOfPayment.TermOfPaymentValue
                nMonth = DateTime.DaysInMonth(objContractHeader.ContractPeriodYear, objContractHeader.ContractPeriodMonth)
                poDetail.AmountRewardDepA = Calculation.CountRewardAmountDepositA(objPrice, nTOP, nMonth)
                poDetail.DiscountReward = objPrice.DiscountReward
                poDetail.AmountReward = Calculation.CountRewardAmount(objContractDetail, objPrice, nTOP, nMonth)

                poDetail.Price = Calculation.CountRewardsVehiclePrice(objContractDetail, objPrice, nTOP, nMonth)
                poDetail.PPh22 = Calculation.CountRewardPPh22(objContractDetail, objPrice, nTOP, nMonth)

            Else
                poDetail.DiscountReward = 0
                poDetail.AmountReward = 0
                poDetail.AmountRewardDepA = 0
                poDetail.PPh22 = objContractDetail.PPh22
            End If
            '' END OF CR Sirkular Rewards


            Return poDetail
        End If
    End Function

    Private Sub GetContract()
        Dim contractId As String = CInt(ViewState("ContractID"))  'Request.QueryString("id")
        objContractHeader = New ContractHeaderFacade(User).Retrieve(CInt(contractId))
        sessionHelper.SetSession("Contract", objContractHeader)
    End Sub

    Private Function GetProductCategory() As ProductCategory
        If Me.sessionHelper.GetSession("Contract") Is Nothing Then
            Me.GetContract()
        End If
        Dim oCH As ContractHeader = CType(Me.sessionHelper.GetSession("Contract"), ContractHeader)
        Dim oPC As ProductCategory = oCH.Category.ProductCategory

        Return oPC
    End Function

    Private Sub BindHeaderToForm()
        '--Label
        lblDealerCode.Text = objContractHeader.Dealer.DealerCode & " / " & objContractHeader.Dealer.SearchTerm1
        lblName.Text = objContractHeader.Dealer.DealerName
        lblContractNumber.Text = objContractHeader.ContractNumber
        lblCity.Text = objContractHeader.Dealer.City.CityName
        'lblCreated.Text = objContractHeader.CreatedBy
        lblOrderType.Text = CType(objContractHeader.ContractType, enumOrderType.OrderType).ToString
        lblSalesOrg.Text = objContractHeader.Category.CategoryCode
        lblProductYear.Text = objContractHeader.ProductionYear
        lblProjectName.Text = objContractHeader.ProjectName

        Me.ddlTermOfPayment.DataSource = New TermOfPaymentFacade(User).RetrieveActiveList()
        Me.ddlTermOfPayment.DataValueField = "ID"
        Me.ddlTermOfPayment.DataTextField = "Description"

        Me.ddlTermOfPayment.DataBind()
        ddlTermOfPayment.ClearSelection()
        ddlTermOfPayment.SelectedValue = "1"
        '-- Bind To DropDownList Jenis Order
        For Each item As ListItem In LookUp.ArrayJenisPO
            If item.Text = "Harian" Then
                If SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Harian) OrElse SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Semua) Then
                    'ddlOrderType.Items.Add(item)
                End If
                ddlOrderType.Items.Add(item)
            ElseIf item.Text = "Tambahan" Then
                If (SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Tambahan) OrElse SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Semua)) AndAlso IsValidPOTambahan() Then
                    objContractHeader = sessionHelper.GetSession("Contract")
                    If (objContractHeader.ContractPeriodMonth = DateTime.Now.Month) AndAlso (objContractHeader.ContractPeriodYear = DateTime.Now.Year) Then
                        '  ddlOrderType.Items.Add(item)
                    End If

                End If
                ddlOrderType.Items.Add(item)
            End If
        Next
        ddlOrderType.ClearSelection()
        'Start  :Factoring;by:dna;on:20101004;for:yurike;remark:set control
        Me.chkFactoring.Checked = False
        Me.icPermintaanKirim.Value = AddWorkingDay(DateTime.Today, 5)
        Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
        Dim oDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim oProductCategory As ProductCategory = CType(objContractHeader.Category.ProductCategory(), ProductCategory)
        If CommonFunction.GetTransControlStatus(oDealer, EnumDealerTransType.DealerTransKind.Factoring) AndAlso oFMFac.IsAllowedToProposePO(oProductCategory, oDealer.CreditAccount) Then
            Me.chkFactoring.Enabled = True
        Else
            Me.chkFactoring.Enabled = False
        End If
        'End    :Factoring;by:dna;on:20101004;for:yurike;remark:set control
    End Sub

    Private Sub BindDetailToGrid()
        Dim objTOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
        nTOP = objTOP.TermOfPaymentValue
        nMonth = DateTime.DaysInMonth(objContractHeader.ContractPeriodYear, objContractHeader.ContractPeriodMonth)
        Dim objPKHead As PKHeader = New PKHeaderFacade(User).Retrieve(objContractHeader.PKNumber)
        objSPL = New SPLFacade(User).Retrieve(objPKHead.SPLNumber.ToString())
        arrOrder = sessionHelper.GetSession("Ord")
        dtgDetail.DataSource = objContractHeader.ContractDetails
        dtgDetail.DataBind()
        If SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege) Then
            dtgDetail.Columns(6).Visible = False
        Else
            dtgDetail.Columns(6).Visible = True
        End If
        dtgDetail.Columns(6).Visible = True
    End Sub

    Private Sub SetFreePPh()
        Dim objD As Dealer = CType(Session("DEALER"), Dealer)
        Dim CreatePODate As Date = DateSerial(Now.Year, Now.Month, Now.Day)
        Dim objFPFac As FreePPhFacade = New FreePPhFacade(User)
        Dim crtFP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreePPh), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlFP As ArrayList

        crtFP.opAnd(New Criteria(GetType(FreePPh), "Dealer.ID", MatchType.Exact, objD.ID))
        crtFP.opAnd(New Criteria(GetType(FreePPh), "PeriodStart", MatchType.LesserOrEqual, Format(CreatePODate, "yyyyMMdd")))
        crtFP.opAnd(New Criteria(GetType(FreePPh), "PeriodEnd", MatchType.GreaterOrEqual, Format(CreatePODate, "yyyyMMdd")))
        crtFP.opAnd(New Criteria(GetType(FreePPh), "Status", MatchType.Exact, CType(enumFreePPhStatus.FreePPhStatus.Approved, Short)))
        arlFP = objFPFac.Retrieve(crtFP)
        If arlFP.Count > 0 Then
            chkFreePPh.Checked = True
        Else
            chkFreePPh.Checked = False
        End If
        'this update process has already implemented in FrmFreePPh.UpdateCHAndPO
        ''Update Contract Header and it's POs(with status Baru)
        'Dim objCHFac As ContractHeaderFacade = New ContractHeaderFacade(User)
        'Dim objPOFac As POHeaderFacade = New POHeaderFacade(User)
        'Dim crtPO As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim arlPO As New ArrayList

        'objContractHeader.FreePPh22Indicator = IIf(chkFreePPh.Checked, 0, 1)
        'objCHFac.Update(objContractHeader)
        'crtPO.opAnd(New Criteria(GetType(POHeader), "ContractHeader.ID", MatchType.Exact, objContractHeader.ID))
        'crtPO.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.Exact, CType(enumStatusPO.Status.Baru, Short).ToString))
        'arlPO = objPOFac.Retrieve(crtPO)
        'For Each objPO As POHeader In arlPO
        '    objPO.FreePPh22Indicator = objContractHeader.FreePPh22Indicator
        '    objPOFac.Update(objPO)
        'Next
    End Sub


    Private Function GetItemDeposit(ByVal oCD As ContractDetail) As Double
        Return oCD.GuaranteeAmount
        Exit Function

        Dim i As Integer
        i = 0



        If oCD.ContractHeader.PKHeader.JaminanID = 0 Then
            Dim oJFac As JaminanFacade = New JaminanFacade(User)
            Dim crtJ As CriteriaComposite
            Dim arlJ As New ArrayList
            Dim dt As Date = DateSerial(icPermintaanKirim.Value.Year, icPermintaanKirim.Value.Month, icPermintaanKirim.Value.Day)
            'Dim oJ As Jaminan

            crtJ = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidFrom", MatchType.LesserOrEqual, dt))
            crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidTo", MatchType.GreaterOrEqual, dt))
            crtJ.opAnd(New Criteria(GetType(Jaminan), "Status", MatchType.Exact, CType(EnumStatusSPL.StatusSPL.Aktif, Short)))
            'crtJ.opAnd(New Criteria(GetType(Jaminan), "DealerCode", MatchType.Partial, lblDealerCode.Text))
            arlJ = oJFac.Retrieve(crtJ)
            If arlJ.Count > 0 Then
                For Each oJ As Jaminan In arlJ
                    If (" " & oJ.DealerCode).IndexOf(lblDealerCode.Text.Trim) > 0 Then
                        For Each oJD As JaminanDetail In oJ.JaminanDetails
                            If oJD.VehicleTypeCode = oCD.VechileColor.VechileType.VechileTypeCode AndAlso (Me.icPermintaanKirim.Value >= oJD.Jaminan.ValidFrom And Me.icPermintaanKirim.Value <= oJD.Jaminan.ValidTo) And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = oCD.ContractHeader.Purpose) Then
                                Return oJD.Amount
                            End If
                        Next
                    End If
                Next
            Else
                Return 0
            End If
        Else
            Dim oJFac As JaminanFacade = New JaminanFacade(User)
            Dim oJ As Jaminan
            oJ = oJFac.Retrieve(oCD.ContractHeader.PKHeader.JaminanID)
            For Each oJD As JaminanDetail In oJ.JaminanDetails
                If oJD.VehicleTypeCode = oCD.VechileColor.VechileType.VechileTypeCode AndAlso (Me.icPermintaanKirim.Value >= oJD.Jaminan.ValidFrom And Me.icPermintaanKirim.Value <= oJD.Jaminan.ValidTo) And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = oCD.ContractHeader.Purpose) Then
                    Return oJD.Amount
                End If
            Next
        End If
        Return 0
    End Function

    Private Sub UpdateAmountToJaminan()
        For Each di As DataGridItem In dtgDetail.Items
            Dim oCD As ContractDetail
            'sessionHelper.SetSession("Contract", objContractHeader)
            objContractHeader = CType(sessionHelper.GetSession("Contract"), ContractHeader)
            oCD = objContractHeader.ContractDetails(di.ItemIndex)
            Dim lblDeposit As Label = di.FindControl("lblDeposit")
            lblDeposit.Text = FormatNumber(GetItemDeposit(oCD), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Next
    End Sub
    Private Function GetTotalPOInUI() As Decimal
        Dim TotalPO As Decimal = 0

        For Each di As DataGridItem In Me.dtgDetail.Items
            Dim txtQty As TextBox = di.FindControl("TextBox1")
            Dim ID As Integer = CType(di.Cells(0).Text, Integer)
            Dim oCD As ContractDetail = New ContractDetailFacade(User).Retrieve(ID)
            Dim n As Integer

            If Not IsNothing(oCD) AndAlso oCD.ID > 0 Then
                Try
                    n = CType(txtQty.Text, Double)
                Catch ex As Exception
                    n = 0
                End Try
                TotalPO += n * oCD.Amount
            End If
        Next
        Return TotalPO
    End Function
#End Region



#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 300
        'Put user code to initialize the page here
        sb = New System.Text.StringBuilder


        Response.Cache.SetCacheability(HttpCacheability.NoCache)


        CheckUserPrivilege()
        If Not IsPostBack Then
          

            LbVal.Text = DateTime.Now.ToString()
            Me.txtID.Text = "0"
            ViewState.Add("SubTotalHarga", 0)
            ViewState.Add("ShowMessage", "")

            ViewState.Add("FormA", 0)
            ViewState.Add("FormB", 0)
            ViewState.Add("FormC", 0)
            ViewState.Add("FormD", 0)

            ''Add Event COntract
            'Get Avaible COntract
            LoadSiDealer()
            GetContract()
            For i As Integer = 0 To objContractHeader.ContractDetails.Count - 1
                arrOrder.Add(0)
            Next
            sessionHelper.SetSession("Ord", arrOrder)
            BindHeaderToForm()
            BindDetailToGrid()
            'Start  :RemainModule-DailyPO:Free PPH DoniN
            SetFreePPh()
            'End    :RemainModule-DailyPO:Free PPH DoniN
        End If
        'Hidden1.Value = CInt(ViewState("Count")) + 1
        'ViewState("Count") = Hidden1.Value
        btnBatal.Attributes.Add("OnClick", "return confirm('Yakin PO ini akan dihapus?');")
        '    btnKirim.Attributes.Add("OnClick", "return ConfirmCreatePO(this);")
        'btnBack.Attributes.Add("OnClick", "window.history.go(-2)")
        SetControls()

        If Not IsPostBack AndAlso IsNothing(ViewState("Save")) Then
            Try
                txtDealerPONumber.Text = Guid.NewGuid.ToString().Substring(0, 5)
                btnKirim_Click(Me, New EventArgs())

            Catch ex As Exception
                Session("Dealer") = Session(Temp_Dealer)
                SendError(ex.Message)
                '  ViewState("Save") = True

            End Try

           
        End If
    End Sub

    Private Sub SetControls()
        Dim IsImplementFactoring As Boolean = IIf(KTB.DNet.Lib.WebConfig.GetValue("ImpelementFactoring") = 1, True, False)

        If IsImplementFactoring Then
            Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(CType(Session("DEALER"), Dealer).ID, EnumDealerTransType.DealerTransKind.Factoring)

            If Not (Not IsNothing(oTC) AndAlso oTC.Status = 1) Then
                IsImplementFactoring = False
            End If
        End If
        If IsImplementFactoring Then
            IsImplementFactoring = SecurityProvider.Authorize(Context.User, SR.Po_pengajuan_factoring_privilege)
        End If
        IsImplementFactoring = True
        Me.lblFactoring.Visible = IsImplementFactoring
        Me.lblFactoringColon.Visible = IsImplementFactoring
        Me.chkFactoring.Visible = IsImplementFactoring
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PengajuanPOIconCreatePrivilege) Then
            ' Server.Transfer("../FrmAccessDenied.aspx?modulName=Detail Pengajuan PO")
        End If
        btnKirim.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPO_Save_Privilege)
        btnBatal.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPO_Save_Privilege)

        btnKirim.Visible = True
        btnBatal.Visible = True

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        isPriceVisible = True
        Label10.Visible = isPriceVisible
        Total.Visible = isPriceVisible
        Label9.Visible = isPriceVisible
        lblTotalHargaValue.Visible = isPriceVisible
        dtgDetail.Columns(6).Visible = isPriceVisible
        dtgDetail.Columns(7).Visible = isPriceVisible
        dtgDetail.Columns(8).Visible = isPriceVisible
        dtgDetail.Columns(9).Visible = isPriceVisible
    End Sub

    Sub dtgDetail_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

        If Not (e.Item.ItemIndex = -1) Then
            objContractDetail = objContractHeader.ContractDetails(e.Item.ItemIndex)
            e.Item.Cells(2).Text = objContractDetail.VechileColor.MaterialNumber
            e.Item.Cells(3).Text = objContractDetail.VechileColor.MaterialDescription
            e.Item.Cells(4).Text = FormatNumber(objContractDetail.SisaUnit, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            Dim rangeValidator As RangeValidator = e.Item.FindControl("RangeValidator1")
            Dim txtBox As TextBox = e.Item.FindControl("TextBox1")
            txtBox.Text = arrOrder(e.Item.ItemIndex)
            If (CInt(objContractDetail.SisaUnit) <= 0) Then
                rangeValidator.MaximumValue = 0
                txtBox.Text = "0"
            Else
                rangeValidator.MaximumValue = CInt(objContractDetail.SisaUnit)
                txtBox.Text = "1"
            End If
            Dim lblDeposit As Label = e.Item.FindControl("lblDeposit")
            Dim ItemDeposit As Double = GetItemDeposit(objContractDetail)
            lblDeposit.Text = FormatNumber(arrOrder(e.Item.ItemIndex) * ItemDeposit, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
          


            Dim lblHarga As Label = e.Item.FindControl("lblHarga")
            lblHarga.Text = FormatNumber(CType(txtBox.Text, Double) * CType(objContractDetail.Amount, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Dim lblPPh22 As Label = e.Item.FindControl("lblPPh22")
            'Start  :RemainModule-DailyPO:FreePPh By:Doni N
            'lblPPh22.Text = FormatNumber(CType(txtBox.Text, Double) * CType(objContractDetail.PPh22, Double) * CInt(objContractHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblPPh22.Text = FormatNumber(CType(txtBox.Text, Double) * CType(objContractDetail.PPh22, Double) * CInt(objContractHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'lblPPh22.Text = FormatNumber(CType(txtBox.Text, Double) * CType(objContractDetail.PPh22, Double) * IIf(CInt(objContractHeader.FreePPh22Indicator) = 1, 0, 1), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'End  :RemainModule-DailyPO:FreePPh By:Doni N
            Dim lblInterest As Label = e.Item.FindControl("lblInterest")
            Dim freeIntIndicator As Integer
            Dim objPKHeader As PKHeader = New PKHeaderFacade(User).Retrieve(objContractHeader.PKNumber)
            freeIntIndicator = objPKHeader.FreeIntIndicator

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objContractDetail.VechileColor.ID))
            Dim oDealer As Dealer = Session("DEALER")
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, oDealer.ID))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
            'Modified By ali
            'Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(criterias, sortColl)

            Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(objContractDetail)



            If objPriceArrayList.Count > 0 Then
                Dim objPrice As Price
                For Each item As Price In objPriceArrayList
                    If item.ValidFrom <= New DateTime(objContractDetail.ContractHeader.PricePeriodYear, objContractDetail.ContractHeader.PricePeriodMonth, objContractDetail.ContractHeader.PricePeriodDay) Then
                        objPrice = item
                        Exit For
                    End If
                Next
                'lblInterest.Text = FormatNumber(CType(txtBox.Text, Double) * freeIntIndicator * Calculation.CountInterest(nTOP, nMonth, objPrice.Interest, objContractDetail.Amount - CType(lblDeposit.Text, Double), objPrice.PPh23), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'start  :20140123:donas: add logic to prevent wrong calculation on interest field
                Dim objTOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
                nTOP = objTOP.TermOfPaymentValue
                nMonth = DateTime.DaysInMonth(objContractHeader.ContractPeriodYear, objContractHeader.ContractPeriodMonth)
                'end    :20140123:donas: add logic to prevent wrong calculation on interest field
                If chkFactoring.Checked Then
                    lblInterest.Text = FormatNumber(CType(txtBox.Text, Double) * freeIntIndicator * Calculation.CountInterest(nTOP, nMonth, objPrice.FactoringInt, objContractDetail.Amount - ItemDeposit, objPrice.PPh23), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                    '' CR Sirkular Rewards
                    '' by : ali Akbar
                    '' 2014-09-24
                    Dim _VehicleNettPrice As Double = 0
                    Dim _PPh22 As Double = 0
                    Dim _interest As Double = 0

                    _VehicleNettPrice = Calculation.CountRewardsVehiclePrice(objContractDetail, objPrice, nTOP, nMonth)
                    _PPh22 = Calculation.CountRewardPPh22(objContractDetail, objPrice, nTOP, nMonth)
                    _interest = Calculation.CountRewardsInterest(objContractDetail, objPrice, nTOP, nMonth)
                    lblHarga.Text = FormatNumber(CType(txtBox.Text, Double) * (_VehicleNettPrice), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lblPPh22.Text = FormatNumber(CType(txtBox.Text, Double) * (_PPh22) * CInt(objContractHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lblInterest.Text = FormatNumber(CType(txtBox.Text, Double) * freeIntIndicator * _interest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    '' END OF CR Sirkular Rewards
                Else
                    lblInterest.Text = FormatNumber(CType(txtBox.Text, Double) * freeIntIndicator * Calculation.CountInterest(nTOP, nMonth, objPrice.Interest, objContractDetail.Amount - ItemDeposit, objPrice.PPh23), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                End If
            Else
                lblInterest.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If

            SubTotalItem += CType(txtBox.Text, Integer)
            SubTotalharga = SubTotalharga + CType(lblHarga.Text, Double)
            SubTotalPPh = SubTotalPPh + CType(lblPPh22.Text, Double)
            SubTotalInterest += CType(lblInterest.Text, Double)
            subTotalDeposit += CType(lblDeposit.Text, Double)
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(3).Text = "Sub Total : "
            Dim lblSubTotal As Label = e.Item.FindControl("lblSubTotal")
            lblSubTotal.Text = FormatNumber(SubTotalItem, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Dim lblSubTotalHarga As Label = e.Item.FindControl("lblSubTotalHarga")
            lblSubTotalHarga.Text = FormatNumber(SubTotalharga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            ViewState.Item("SubTotalHarga") = SubTotalharga
            Dim lblSubTotalPPh22 As Label = e.Item.FindControl("lblSubTotalPPh22")
            lblSubTotalPPh22.Text = FormatNumber(SubTotalPPh, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Dim lblSubTotalInterest As Label = e.Item.FindControl("lblSubTotalInterest")
            lblSubTotalInterest.Text = FormatNumber(SubTotalInterest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Dim lblSubTotalDeposit As Label = e.Item.FindControl("lblSubTotalDeposit")
            lblSubTotalDeposit.Text = FormatNumber(subTotalDeposit, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            lblTotalHargaValue.Text = FormatNumber(SubTotalharga + SubTotalPPh + SubTotalInterest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub btnKirim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKirim.Click
        'If Not Page.IsValid Then
        '    Return
        'End If
        Dim Waktu As Object = New ExpandoObject()
        Waktu.StartSave = New ExpandoObject()
        Waktu.StartSave = DateTime.Now

        Dim time1, time2, time3, time4 As DateTime
        Dim interval1, interval2, interval3 As Integer

        RegisterClientScriptBlock("ToggleEditMode", "<script language=JavaScript>IsReadOnly(false);</script>")
        time1 = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, Date.Now.Hour, Date.Now.Minute, Date.Now.Second, Date.Now.Millisecond)
        LblInit.Text = time1.ToString()
        UpdateAmountToJaminan()
        Hitung()

        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")

        If ddlOrderType.Items.Count > 0 Then
            If (POIsValid()) Then


                BindToPOHeaderObject()
                If (PODetailIsValid()) Then
                    If objPOHeader.PODetails.Count <> 0 Then
                        Try
                            'This checking step is put here for minimize invalid data inserting
                            If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling() Then
                                Exit Sub
                            End If
                            If Not ValidasiWaktuPengajuan() Then Exit Sub
                            time2 = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, Date.Now.Hour, Date.Now.Minute, Date.Now.Second, Date.Now.Millisecond)
                            Dim id As Integer = New POHeaderFacade(User).Insert(objPOHeader)
                            time3 = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, Date.Now.Hour, Date.Now.Minute, Date.Now.Second, Date.Now.Millisecond)
                            Me.txtID.Text = id.ToString
                            If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling(True) Then
                                Dim POHFac As POHeaderFacade = New POHeaderFacade(User)
                                'POHFac.DeleteFromDB(objPOHeader)
                                'MessageBox.Show("Simpan Gagal : Total PO melebihi Ceiling yang tersedia")
                                'Return
                            End If
                            Dim objNewPOHeader As POHeader = New POHeaderFacade(User).Retrieve(id)
                            sessionHelper.RemoveSession("Contract")
                            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.PO_Harian), objNewPOHeader.PONumber, -1, CInt(enumStatusPO.Status.Baru))
                            '  sessionHelper.SetSession("PrevPage", sessionHelper.GetSession("PrevPagePO"))
                            time4 = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, Date.Now.Hour, Date.Now.Minute, Date.Now.Second, Date.Now.Millisecond)

                            If Me.CtlTimeElapsed.Value = 1 Then
                                interval1 = time2.Subtract(time1).TotalMilliseconds
                                interval2 = time3.Subtract(time2).TotalMilliseconds
                                interval3 = time4.Subtract(time3).TotalMilliseconds

                                SaveTimeElapsed(0, id, interval1, interval2, interval3)
                            End If
                            lblSave.Text = time4.ToString()

                            Waktu.EndSave = New ExpandoObject()
                            Waktu.EndSave = DateTime.Now
                            Waktu.Uri = New ExpandoObject()
                            Waktu.Uri = Request.Url.AbsoluteUri.ToString()

                            Session("POLoad") = Waktu


                            'Server.Transfer("~/PO/EditPO.aspx?id=" & id & "&src=createnew")
                            Response.Redirect("../PO/EditPOLoad.aspx?id=" & id & "&src=createnew", False)
                            Context.ApplicationInstance.CompleteRequest()
                        Catch ex As Exception
                            MessageBox.Show("Pembuatan PO Gagal, ulangi beberapa saat lagi.")
                            Return
                        End Try
                    Else
                        MessageBox.Show("Tidak ada PO Detail")
                    End If
                Else
                    MessageBox.Show("Sisa O/C Berubah, Order Melebihi Sisa O/C")
                    If Not IsNothing(sessionHelper.GetSession("Contract")) Then '-- Add by Sony
                        BindDetailToGrid()
                    End If
                End If
            End If
        Else
            MessageBox.Show("Maaf, Anda Tidak punya Akses Membuat PO")
        End If


    End Sub

    'Private Function IsCreditAccountBusy(ByVal CreditAccount As String) As Boolean
    '    Dim objCM As CreditMaster
    '    Dim objCMFac As CreditMasterFacade
    '    'dim crtCM as CriteriaComposite = new CriteriaComposite(new Criteria(gettype(


    'End Function

    Private Sub SaveTimeElapsed(ByVal mode As Short, ByVal poID As Integer, ByVal validationTimeElapsed As Integer, ByVal savingTimeElapsed As Integer, ByVal validationTimeElapsed2 As Integer)
        Try
            Dim perfLogFacade As New KTB.DNet.BusinessFacade.Helper.PerformanceLogFacade(User)

            Dim objPerfLog As New PerformanceLog
            objPerfLog.Modul = "Create PO"
            objPerfLog.ModulID = poID
            objPerfLog.Action = mode
            objPerfLog.Time1 = validationTimeElapsed
            objPerfLog.Time2 = savingTimeElapsed
            objPerfLog.Time3 = validationTimeElapsed2
            perfLogFacade.Insert(objPerfLog)
        Catch ex As Exception
            'Dim strMessage As String = ex.Message.ToString & " - " & ex.InnerException.ToString
        End Try

        'Me.CtlTimeElapsed
    End Sub

    Private Function PODetailIsValid() As Boolean
        objContractHeader = sessionHelper.GetSession("Contract")
        If Not IsNothing(objContractHeader) Then '--Add by Sony
            For Each item As DataGridItem In dtgDetail.Items
                Dim txtBox As TextBox = item.FindControl("TextBox1")
                If (CInt(txtBox.Text) > CType(objContractHeader.ContractDetails(item.ItemIndex), ContractDetail).SisaUnit) Then
                    Return False
                End If
            Next
            Return True
        End If
    End Function

    Private Function IsEnableCeilingFilter() As Boolean
        If chkFactoring.Checked Then Return True
        Dim oD As Dealer = sessionHelper.GetSession("DEALER")
        Dim oTC As TransactionControl

        If Me.GetProductCategory.Code.Trim.ToUpper() = "MFTBC" Then
            oTC = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.FilterPengajuanPO)
        Else
            oTC = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.FilterPengajuanPOMMC)
        End If
        If Not IsNothing(oTC) AndAlso oTC.ID > 0 Then
            If oTC.Status = 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If

        'If Not IsNothing(oTC) AndAlso oTC.ID > 0 AndAlso oTC.Status = 1 Then
        '    Return True
        'End If
        'Return False
    End Function

    Private Function IsEnabledCreditControl(ByVal objDealer As Dealer) As Boolean
        Dim _poHeaderFacade As POHeaderFacade = New POHeaderFacade(User)
        If _poHeaderFacade.IsEnabledCreditControl(objDealer) Then
            Return True
        End If
        Return False
    End Function


    Private Sub btnHitung_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHitung.Click
        If Not Page.IsValid Then
            Return
        End If


        UpdateAmountToJaminan()
        Hitung()

        If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling() Then
            Exit Sub
        End If
    End Sub

    Private Sub Hitung()
        BindToPOHeaderObject()
        ' Created by Ikhsan, 20081030
        ' to force the simpan button to do Hitung first before saving to DB.
        ' Requested by Yurike as Part of CR
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        If IsEnabledCreditControl(objDealer) Then
            If objDealer.LegalStatus = "" Then
                'MessageBox.Show("Dealer anda tidak memiliki TOP Transaksi.")
                'Return
            End If
            'If Not IsCreatePOWithTOPValid() Then
            '    MessageBox.Show("Credit Ceiling tidak mencukupi")
            '    Return
            'End If
        End If

        objContractHeader = sessionHelper.GetSession("Contract")
        If (POIsValid()) AndAlso ValidasiWaktuPengajuan() Then
            For Each item As DataGridItem In dtgDetail.Items
                Dim txtbox As TextBox = item.FindControl("TextBox1")
                arrOrder = sessionHelper.GetSession("Ord")
                arrOrder.Insert(item.ItemIndex, CInt(txtbox.Text))
                '    Dim lblHarga As Label = item.FindControl("lblHarga")
                '    lblHarga.Text = FormatNumber(CType(txtbox.Text, Double) * CType(objContractHeader.ContractDetails(item.ItemIndex).Amount, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                '    Dim lblPPh22 As Label = item.FindControl("lblPPh22")
                '    lblPPh22.Text = FormatNumber(CType(txtbox.Text, Double) * CType(objContractHeader.ContractDetails(item.ItemIndex).PPh22, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                '    Total = Total + ((CType(lblHarga.Text, Double) + CType(lblPPh22.Text, Double)))
                '    SubTotalharga = SubTotalharga + CType(lblHarga.Text, Double)
                '    SubTotalPPh = +CType(lblPPh22.Text, Double)
            Next
            sessionHelper.SetSession("Ord", arrOrder)
            BindDetailToGrid()
            'lblTotalHargaValue.Text = FormatNumber(Total.ToString, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'dtgDetail.DataBind()
            'Dim lblSubTotalHarga As Label = dtgDetail.FindControl("lblSubTotalHarga")
            'lblSubTotalHarga.Text = SubTotalharga
            'Dim lblSubTotalPPh22 As Label = dtgDetail.FindControl("lblSubTotalPPh22")
            'lblSubTotalPPh22.Text = SubTotalPPh

        End If
    End Sub

    Private Function IsLesserThanAvailableCeiling(Optional ByVal IsAfterSaving As Boolean = False) As Boolean
        Dim objD As Dealer = Session("DEALER")
        Dim TotalPO As Decimal = Me.GetTotalPOInUI() ' CType(viewstate.Item("SubTotalHarga"), Decimal)
        Dim oTEOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(Me.ddlTermOfPayment.SelectedValue, Integer))
        Dim IsLesser As Boolean = False

        If oTEOP.PaymentType = CType(enumPaymentType.PaymentType.RTGS, Short) Then
            IsLesser = True
        Else
            Dim Ceiling As Decimal = 0
            Dim Proposed As Decimal = 0
            Dim Liquified As Decimal = 0
            Dim Outstanding As Decimal = 0
            Dim TodaysAvCeiling As Decimal = 0
            Dim TomorrowAvCeiling As Decimal = 0
            Dim AvCeiling As Decimal = 0

            If chkFactoring.Checked Then
                Dim AvFactCeiling As Decimal = 0
                Dim oFM As FactoringMaster = New FactoringMasterFacade(User).Retrieve(Me.GetProductCategory, objD.CreditAccount)

                If oTEOP.PaymentType = enumPaymentType.PaymentType.TOP Then
                    Dim dtJatuhTempo As Date = DateAdd(DateInterval.Day, oTEOP.TermOfPaymentValue, Me.icPermintaanKirim.Value)
                    If dtJatuhTempo > oFM.MaxTOPDate Then
                        MessageBox.Show("Jatuh Tempo PO Melebihi Tanggal Validitas Ceiling")
                        Return False
                    End If
                End If
                IsLesser = CommonFunction.IsEnoughForFactoring(Me.GetProductCategory(), CType(Me.txtID.Text, Integer), TotalPO, CType(Session("DEALER"), Dealer).CreditAccount, IsAfterSaving, AvFactCeiling _
                , Ceiling, Proposed, Liquified, Outstanding)

                Me.lblCeiling.Text = FormatNumber(Ceiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblProposed.Text = FormatNumber(Proposed, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblLiquified.Text = FormatNumber(Liquified, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblOutstanding.Text = FormatNumber(Outstanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblTodayAvCeiling.Text = FormatNumber(TodaysAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'Me.lblTomorrowAvCeiling.Text = FormatNumber(TomorrowAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

                Me.lblAvailable.Text = FormatNumber(AvFactCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Else
                'Credit Ceiling
                If oTEOP.PaymentType = CType(enumPaymentType.PaymentType.TOP, Short) Then
                    Dim objSCM As sp_CreditMaster = GetCeilingCredit(Me.GetProductCategory(), objD.CreditAccount, oTEOP.PaymentType)
                    If objSCM Is Nothing Then
                        Return True '  MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
                        Return False
                    Else
                        If objSCM.ID < 1 Then
                            Return True '   MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
                            Return False
                        End If
                    End If
                    Dim dtJatuhTempo As Date = DateAdd(DateInterval.Day, oTEOP.TermOfPaymentValue, Me.icPermintaanKirim.Value)
                    If dtJatuhTempo > objSCM.MaxTOPDate Then
                        Return True '    MessageBox.Show("Jatuh Tempo PO Melebihi Tanggal Validitas Ceiling")
                        Return False
                    End If
                End If

                IsLesser = CommonFunction.IsCeilingEnough(Me.GetProductCategory(), CType(Me.txtID.Text, Integer), Me.icPermintaanKirim.Value, TotalPO, objD.CreditAccount, oTEOP.PaymentType, IsAfterSaving, Ceiling, Proposed, Liquified, Outstanding, TodaysAvCeiling, TomorrowAvCeiling, AvCeiling)
                Me.lblCeiling.Text = FormatNumber(Ceiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblProposed.Text = FormatNumber(Proposed, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblLiquified.Text = FormatNumber(Liquified, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblOutstanding.Text = FormatNumber(Outstanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblTodayAvCeiling.Text = FormatNumber(TodaysAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblTomorrowAvCeiling.Text = FormatNumber(TomorrowAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

                Me.lblAvailable.Text = FormatNumber(AvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
        End If
        If IsLesser = False Then
            Return True '    MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
        End If
        Return IsLesser
    End Function

    Private Function GetAvailableCeiling() As Decimal
        Dim objD As Dealer = Session("DEALER")
        Dim objCMFac As CreditMasterFacade = New CreditMasterFacade(User)
        Dim objCM As CreditMaster
        Dim AvCeiling As Decimal
        Dim TotalPO As Decimal = ViewState.Item("SubTotalHarga")
        Dim PaymentType As Short
        Dim objSCM As sp_CreditMaster
        Dim arlTemp As ArrayList = New ArrayList
        Dim TotalLiquefied As Decimal = 0
        Dim TotalAcceleratedGyro As Decimal = 0

        If ddlTermOfPayment.SelectedItem.Text.Trim.ToUpper = "COD" Then
            PaymentType = enumPaymentType.PaymentType.COD
        ElseIf ddlTermOfPayment.SelectedItem.Text.Trim.ToUpper = "RTGS" Then
            PaymentType = enumPaymentType.PaymentType.RTGS
            Return True
            Exit Function
        Else
            PaymentType = enumPaymentType.PaymentType.TOP
        End If

        'Credit Ceiling
        objSCM = GetCeilingCredit(Me.GetProductCategory(), objD.CreditAccount, PaymentType)
        AvCeiling = (objSCM.Plafon - objSCM.OutStanding)
        ViewState.Item("FormA") = AvCeiling
        If PaymentType = enumPaymentType.PaymentType.TOP Then
            'Proposed PO
            AvCeiling = AvCeiling - objSCM.ProposedPO
            ViewState.Item("FormB") = objSCM.ProposedPO
            'Liquefied and Accelerated Gyro
            objCM = objCMFac.Retrieve(Me.GetProductCategory, objD.CreditAccount, PaymentType)
            TotalLiquefied = 0
            TotalAcceleratedGyro = 0
            For Each objDealer As Dealer In objCM.Dealers
                arlTemp = GetDealerPO(objDealer, PaymentType)
                TotalLiquefied += arlTemp(0)
                TotalAcceleratedGyro += arlTemp(1)
            Next
            ViewState.Item("FormC") = TotalLiquefied
            ViewState.Item("FormD") = TotalAcceleratedGyro

            AvCeiling = AvCeiling + TotalLiquefied + TotalAcceleratedGyro
        ElseIf PaymentType = enumPaymentType.PaymentType.COD Then
            AvCeiling = GetRemainCeiling(AvCeiling, objD.CreditAccount, PaymentType, DateSerial(Now.Year, Now.Month, Now.Day), Me.icPermintaanKirim.Value)
        End If

        lblAvailable.Text = FormatNumber(AvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblA.Text = ViewState.Item("FormA")
        lblB.Text = ViewState.Item("FormB")
        lblC.Text = ViewState.Item("FormC")
        lblD.Text = ViewState.Item("FormD")

        'check b4 save 
        '100>110 : 90>110
        'c after save

        '100>-80+100 -> 100>-20 then delete


        If TotalPO > AvCeiling Then
            MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
            Return False
        Else
            Return True
        End If


        'objCM.AvailablePlafon = AvCeiling
        'If objCMFac.Update(objCM) = -1 Then
        '    MessageBox.Show("Gagal Update Available Ceiling ")
        'End If
    End Function

#Region "RemainCeiling"

    Private Function GetRemainCeiling(ByVal AvCeiling As Decimal, ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
        Dim RemCeilH As Decimal = 0
        Dim RemCeilHPlus1 As Decimal = 0
        Dim i As Integer
        Dim TotReq As Decimal = 0
        Dim TotCair As Decimal = 0
        Dim FocusedDate As Date

        'Start  :Get AvCeiling from Looping ' starting from ReportDate to ReqDelDate-1
        '3-5 = 2-1
        '0,1,2=3,4

        '3-3=0 -1 = 

        'Response.Write("Av Ceiling #1 = " & AvCeiling & "<br>")
        For i = 0 To DateDiff(DateInterval.Day, StartDate, EndDate) - 1
            If i = 0 Then
                FocusedDate = StartDate
                If FocusedDate = EndDate Then Exit For
                'TotReq = GetReqPO(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                TotReq = GetReqPO(CreditAccount, PaymentType, FocusedDate, FocusedDate)

                TotCair = 0 'GetPOCair(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                AvCeiling = AvCeiling - TotReq + TotCair 'it's covered by SAP Application
                'Response.Write("Date=" & FocusedDate & ":TotPO=" & TotReq & ":TotCair=" & TotCair & ":AvCeiling=" & AvCeiling & "<br>")
            Else
                FocusedDate = AddWorkingDay(FocusedDate, 1)
                If FocusedDate = EndDate Then Exit For
                'TotReq = GetReqPO(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                'TotCair = GetPOCair(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                TotReq = GetReqPO(CreditAccount, PaymentType, FocusedDate, FocusedDate)
                TotCair = GetPOCair(CreditAccount, PaymentType, FocusedDate, FocusedDate)
                AvCeiling = AvCeiling - TotReq + TotCair
                'Response.Write("Date=" & FocusedDate & ":TotPO=" & TotReq & ":TotCair=" & TotCair & ":AvCeiling=" & AvCeiling & "<br>")
            End If
        Next
        'End    :Get AvCeiling from Looping ' starting from ReportDate to ReqDelDate

        StartDate = EndDate
        Dim TotalA As Decimal = GetReqPO(CreditAccount, PaymentType, StartDate, EndDate)
        Dim TotalB As Decimal = GetPOCair(CreditAccount, PaymentType, StartDate, EndDate)
        lblAvCeilingFirst.Text = AvCeiling
        lblA.Text = TotalA
        lblC.Text = TotalB
        RemCeilH = AvCeiling - TotalA + TotalB
        'Response.Write("Date=" & EndDate & ":TotPO=" & TotalA & ":TotCair=" & TotalB & ":AvCeiling=" & RemCeilH & "<br>")

        'TotalA = GetReqPO(CreditAccount, PaymentType, AddWorkingDay(startdate,1) StartDate.AddDays(1), EndDate.AddDays(1))
        TotalA = GetReqPO(CreditAccount, PaymentType, AddWorkingDay(StartDate, 1), AddWorkingDay(StartDate, 1))
        TotalB = GetPOCair(CreditAccount, PaymentType, AddWorkingDay(StartDate, 1), AddWorkingDay(StartDate, 1))
        lblB.Text = TotalA
        lblD.Text = TotalB
        'RemCeilHPlus1 = AvCeiling - TotalA + TotalB
        RemCeilHPlus1 = RemCeilH - TotalA + TotalB
        'Response.Write("Date=" & EndDate.AddDays(1) & ":TotPO=" & TotalA & ":TotCair=" & TotalB & ":AvCeiling=" & RemCeilHPlus1 & "<br>")
        If RemCeilH < RemCeilHPlus1 Then
            Return RemCeilH
        Else
            Return RemCeilHPlus1
        End If

    End Function
    Private Function GetReqPO(ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal

        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As New ArrayList
        Dim Sql As String = ""
        Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim Total As Decimal = 0

        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, StartDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EndDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and dp.RejectStatus<>1 and dp.IsReversed<>1 and dp.IsCleared<>1 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & ")"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next
        Return Total
    End Function
    Private Function GetTotalPODetail(ByRef objPOH As POHeader) As Decimal
        Dim Total As Decimal = 0

        For Each objPOD As PODetail In objPOH.PODetails
            If objPOH.Status = 0 Or objPOH.Status = 2 Then
                Total = Total + (objPOD.ReqQty * objPOD.Price)
            ElseIf objPOH.Status = 4 Or objPOH.Status = 6 Or objPOH.Status = 8 Then
                Total = Total + (objPOD.AllocQty * objPOD.Price)
            End If
        Next
        Return Total
    End Function
    Private Function GetPOCair(ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList
        Dim crtPOH As CriteriaComposite
        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim arlDP As ArrayList
        Dim crtDP As CriteriaComposite
        Dim Sql As String = ""
        Dim Total As Decimal = 0

        'have gyro
        'Accelerated
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, "0"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(" & CType(EnumPaymentStatus.PaymentStatus.Validasi, String) & "," & CType(EnumPaymentStatus.PaymentStatus.Selesai, String) & ")"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, String)))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.No, 1))
        arlDP = objDPFac.Retrieve(crtDP)
        For Each objDP As DailyPayment In arlDP
            Total += objDP.Amount
        Next
        'Not Accelerated
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, "0"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(" & CType(EnumPaymentStatus.PaymentStatus.Validasi, String) & "," & CType(EnumPaymentStatus.PaymentStatus.Selesai, String) & ")"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, String)))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.No, 1))
        arlDP = objDPFac.Retrieve(crtDP)
        For Each objDP As DailyPayment In arlDP
            Total += objDP.Amount
        Next

        'doesn't have gyro
        StartDate = AddWorkingDay(StartDate, -2, True)
        EndDate = AddWorkingDay(EndDate, -2, True)
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and dp.RejectStatus=0 and dp.IsReversed<>1 and dp.IsCleared<>1 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & ")"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, StartDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EndDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next

        Return Total

    End Function

#End Region

    Private Function GetCeilingCredit(ByVal PC As ProductCategory, ByVal CreditAccount As String, ByVal PaymentType As Short) As sp_CreditMaster
        Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
        Dim arlSCM As ArrayList
        Dim objSCM As sp_CreditMaster
        Dim crtSCM As CriteriaComposite
        Dim ReportDate As Date = DateSerial(Now.Year, Now.Month, Now.Day)
        Dim ReqDelDate As Date = Me.icPermintaanKirim.Value

        crtSCM = New CriteriaComposite(New Criteria(GetType(sp_CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "CreditAccount", MatchType.Exact, CreditAccount))
        crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "PaymentType", MatchType.Exact, PaymentType))
        arlSCM = objSCMFac.RetrieveFromSP(PC, ReportDate, ReqDelDate, crtSCM)
        If arlSCM.Count > 0 Then
            Return CType(arlSCM(0), sp_CreditMaster)
        Else
            Return Nothing
        End If
    End Function

    'Private Function GetCeilingCreditDyn(ByVal CreditAccount As String, ByVal PaymentType As Short, ByVal pReportDate As Date, ByVal pReqDelDate As Date) As sp_CreditMaster
    '    Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
    '    Dim arlSCM As ArrayList
    '    Dim objSCM As sp_CreditMaster
    '    Dim crtSCM As CriteriaComposite
    '    Dim ReportDate As Date = pReportDate '  DateSerial(Now.Year, Now.Month, Now.Day)
    '    Dim ReqDelDate As Date = pReqDelDate ' Me.icPermintaanKirim.Value

    '    crtSCM = New CriteriaComposite(New Criteria(GetType(sp_CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "CreditAccount", MatchType.Exact, CreditAccount))
    '    crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "PaymentType", MatchType.Exact, PaymentType))
    '    arlSCM = objSCMFac.RetrieveFromSP(ReportDate, ReqDelDate, crtSCM)
    '    If arlSCM.Count > 0 Then
    '        Return CType(arlSCM(0), sp_CreditMaster)
    '    Else
    '        Return Nothing
    '    End If
    'End Function
    Private Function GetDealerPO(ByVal objDealer As Dealer, ByVal PaymentType As Short) As ArrayList
        Dim TotalLiquefied As Decimal = 0
        Dim TotalAcceleratedGyro As Decimal = 0
        Dim arlResult As ArrayList = New ArrayList
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim crtPOH As CriteriaComposite
        Dim arlPOH As ArrayList = New ArrayList
        Dim EffectiveDate As Date
        Dim tmpDate As Date
        Dim nTOPDays As Integer
        Dim TodayDate As Date = DateSerial(Now.Year, Now.Month, Now.Day)
        Dim ReqDelDate As Date = DateSerial(Me.icPermintaanKirim.Value.Year, Me.icPermintaanKirim.Value.Month, Me.icPermintaanKirim.Value.Day)
        Dim tmpTotal As Decimal = 0
        Dim DPFacade As New DailyPaymentFacade(User)

        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, objDealer.ID))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Dealer.CreditAccount", MatchType.Exact, objDealer.CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(TodayDate.AddYears(-1), "yyyy/MM/dd")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(TodayDate.AddMonths(-9), "yyyy/MM/dd")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            'Total TotalLiquefied 
            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100329
            'If IsHavingGyro(objPOH) Then
            '    'EffectiveDate = CType(objPOH.DailyPayments(0), DailyPayment).EffectiveDate
            '    EffectiveDate = ReqDelDate ' assuming all dp is included in this procedure (it will be checked below)
            'Else
            '    If objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
            '        nTOPDays = CType(objPOH.TermOfPayment.TermOfPaymentCode.Substring(1), Integer)
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, nTOPDays + 1)
            '    ElseIf objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.COD Then
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, 0 + 2)
            '    ElseIf objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, 0)
            '    End If
            'End If
            EffectiveDate = IIf(objPOH.IsHavingGyro, ReqDelDate, objPOH.EffectiveDate)
            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100329
            If EffectiveDate >= TodayDate And EffectiveDate <= ReqDelDate Then
                If objPOH.Status = 8 Then
                    If EffectiveDate >= TodayDate.AddDays(1) Then
                        If objPOH.DailyPayments.Count = 0 Then
                            tmpTotal = objPOH.TotalPODetail()
                            TotalLiquefied += tmpTotal ' objPOH.TotalPODetail()
                            'sb.Append(objPOH.PONumber & ";status=8;POD;" & tmpTotal & "<BR>")
                        Else
                            'Todo Optimize
                            'tmpTotal = 0
                            'For Each objDP As DailyPayment In objPOH.DailyPayments
                            '    If (objDP.PaymentPurpose.ID = 3 Or objDP.PaymentPurpose.ID = 6) And objDP.IsReversed = 0 And objDP.IsCleared = 0 And objDP.EffectiveDate >= DateSerial(Now.Year, Now.Month, Now.Day + 1) And objDP.EffectiveDate <= ReqDelDate Then
                            '        TotalLiquefied = TotalLiquefied + objDP.Amount
                            '        tmpTotal += objDP.Amount
                            '    End If
                            'Next

                            Dim crtTotal As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crtTotal.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, objPOH.ID))
                            crtTotal.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
                            crtTotal.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.Exact, 0))
                            crtTotal.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.Exact, 0))
                            crtTotal.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(" & CType(EnumPaymentStatus.PaymentStatus.Selesai, String) & ")"))
                            crtTotal.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, DateSerial(Now.Year, Now.Month, Now.Day + 1)))
                            crtTotal.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, ReqDelDate))
                            Dim aggregates As New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
                            tmpTotal = DPFacade.GetAggregateResult(aggregates, crtTotal)
                            TotalLiquefied = TotalLiquefied + tmpTotal

                            'sb.Append(objPOH.PONumber & ";status=8;DP;" & tmpTotal & "<BR>")
                        End If
                    End If
                Else
                    'sb.Append(objPOH.PONumber & ";status<>8;POD;" & objPOH.TotalPODetail() & "<BR>")
                    TotalLiquefied += objPOH.TotalPODetail()
                End If
            End If

        Next

        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim crtDP As CriteriaComposite
        Dim arlDP As ArrayList
        'Accelerated Gyro
        Dim sqlAccGyro As String = "select dp.ID from DailyPayment dp inner join DailyPayment dp2 on dp2.ID=dp.AcceleratorID and dp2.GyroType=" & CType(EnumGyroType.GyroType.Percepatan, Integer).ToString
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "ID", MatchType.InSet, "(" & sqlAccGyro & ")"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.GreaterOrEqual, TodayDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.LesserOrEqual, ReqDelDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(" & CType(EnumPaymentStatus.PaymentStatus.Selesai, String) & ")"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.CreditAccount", MatchType.Exact, objDealer.CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.No, 1))
        'arlDP = objDPFac.Retrieve(crtDP)
        'TotalAcceleratedGyro = 0
        'For Each oDP As DailyPayment In arlDP
        '    TotalAcceleratedGyro += oDP.Amount
        'Next
        Dim aggregatesGyro As New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
        TotalAcceleratedGyro = objDPFac.GetAggregateResult(aggregatesGyro, crtDP)

        ''Not Accelerated Gyro
        'crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.No, "1"))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, TodayDate))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, ReqDelDate))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, objDealer.CreditAccount))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        'arlDP = objDPFac.Retrieve(crtDP)
        'For Each oDP As DailyPayment In arlDP
        '    TotalAcceleratedGyro += oDP.Amount
        'Next

        arlResult.Add(TotalLiquefied)
        arlResult.Add(TotalAcceleratedGyro)
        Return arlResult
    End Function

    Private Function TotalDealerPO(ByVal objDealer As Dealer, ByVal PaymentType As Short) As Decimal
        Dim TotPO1 As Decimal = 0
        Dim TotPO2 As Decimal = 0
        Dim TotPO3 As Decimal = 0
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim crtPOH As CriteriaComposite
        Dim crtPO1 As CriteriaComposite
        Dim crtPO2 As CriteriaComposite
        Dim arlPOH As ArrayList = New ArrayList
        Dim EffectiveDate As Date
        Dim tmpDate As Date
        Dim nTOPDays As Integer


        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, objDealer.ID))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))

        'Total PO 1
        crtPO1 = crtPOH
        crtPO1.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(Now, "MM/dd/yyyy")))
        crtPO1.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(icPermintaanKirim.Value, "MM/dd/yyyy")))
        arlPOH = objPOHFac.Retrieve(crtPO1)

        TotPO1 = 0
        For Each objPOH As POHeader In arlPOH
            If IsHavingGyro(objPOH) = False Then
                TotPO1 += objPOH.TotalPODetail()
            End If
        Next
        ViewState.Item("FormB") = ViewState.Item("FormB") + TotPO1
        'End Total PO 1

        'Total PO 2 and PO 3
        crtPO2 = crtPOH
        arlPOH = objPOHFac.Retrieve(crtPO2)
        TotPO2 = 0
        TotPO3 = 0
        For Each objPOH As POHeader In arlPOH
            'Total PO 2 
            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100329
            'If IsHavingGyro(objpoh) Then
            '    EffectiveDate = CType(objpoh.DailyPayments(0), DailyPayment).EffectiveDate
            'Else
            '    If objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
            '        nTOPDays = CType(objPOH.TermOfPayment.TermOfPaymentCode.Substring(1), Integer)
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, nTOPDays + 1)
            '    ElseIf objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.COD Then
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, 0 + 2)
            '    ElseIf objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, 0)
            '    End If
            'End If
            EffectiveDate = IIf(objPOH.IsHavingGyro, CType(objPOH.DailyPayments(0), DailyPayment).EffectiveDate, objPOH.EffectiveDate)
            'End    :Optimize EffectiveDate calculation;By:DoniN;20100329
            If EffectiveDate >= Now And EffectiveDate <= icPermintaanKirim.Value Then
                TotPO2 += objPOH.TotalPODetail()
            End If
            'End Total PO 2
            'Total PO 3
            If objPOH.DailyPayments.Count > 0 Then
                For Each objDP As DailyPayment In objPOH.DailyPayments
                    If objDP.AcceleratedGyro = 1 Then
                        If (objDP.AcceleratedDate >= Format(Now, "MM/dd/yyyy") And objDP.AcceleratedDate <= Format(Me.icPermintaanKirim.Value, "MM/dd/yyyy")) Then
                            TotPO3 += objPOH.TotalPODetail()
                        End If
                    End If
                Next
            End If
            'End Total PO 3
        Next
        'End Total PO 2 and PO 3
        ViewState.Item("FormC") = ViewState.Item("FormC") + TotPO2
        ViewState.Item("FormD") = ViewState.Item("FormD") + TotPO3
        Return (TotPO2 + TotPO3) - TotPO1

    End Function

    Private Function AddWorkingDay(ByVal StateDate As Date, ByVal nAdded As Integer, Optional ByVal IsBackWard As Boolean = False) As Date
        Dim crtNH As CriteriaComposite
        Dim rslDate As Date
        Dim IsHoliday As Boolean = True
        Dim arlNH As ArrayList = New ArrayList
        Dim i As Integer = 0

        rslDate = StateDate
        For i = 1 To Math.Abs(nAdded)
            rslDate = rslDate.AddDays(IIf(IsBackWard, -1, 1))
            IsHoliday = True
            While IsHoliday = True
                crtNH = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, rslDate))
                arlNH = objNHFac.Retrieve(crtNH)

                If arlNH.Count < 1 Then
                    IsHoliday = False
                Else
                    rslDate = rslDate.AddDays(IIf(IsBackWard = False, 1, -1))
                End If
            End While
        Next
        'rslDate = StateDate.AddDays(nAdded)
        'While IsHoliday = True
        '    crtNH = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, rslDate))
        '    arlNH = objNHFac.Retrieve(crtNH)
        '    If arlNH.Count < 1 Then
        '        IsHoliday = False
        '    Else
        '        rslDate = rslDate.AddDays(IIf(IsBackWard = False, 1, -1))
        '    End If
        'End While
        Return rslDate
    End Function

    Private Function IsHavingGyro(ByRef objPOH As POHeader) As Boolean
        Dim crtDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlDP As New ArrayList

        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, objPOH.ID))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        arlDP = objDPFac.Retrieve(crtDP)
        Return IIf(arlDP.Count > 0, True, False)

        'Dim Rsl As Boolean = True

        'If objPOH.DailyPayments.Count < 1 Then
        '    Rsl = False
        'Else
        '    'Todo optimize with retrieve data, not looping
        '    For Each objDP As DailyPayment In objPOH.DailyPayments
        '        If (objDP.PaymentPurpose.ID = 3 Or objDP.PaymentPurpose.ID = 6) And (objDP.RejectStatus = 0 And objDP.IsCleared = 0 And objDP.IsReversed = 0) Then
        '            Return True
        '        Else
        '            Rsl = False
        '        End If
        '    Next
        'End If
        'Return Rsl
    End Function

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearFields()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessionHelper.GetSession("PrevPagePO") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPagePO") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPagePO").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Private Sub chkFactoring_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFactoring.CheckedChanged
        Dim aTEOP As New ArrayList
        Dim oTEOP As TermOfPayment
        Dim oTEOPFac As TermOfPaymentFacade = New TermOfPaymentFacade(User)

        If chkFactoring.Checked Then
            Dim cTEOP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cTEOP.opAnd(New Criteria(GetType(TermOfPayment), "PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.TOP, Short)))

            aTEOP = oTEOPFac.Retrieve(cTEOP)
        Else
            aTEOP = oTEOPFac.RetrieveActiveList()
        End If
        oTEOP = oTEOPFac.Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))

        Me.ddlTermOfPayment.DataSource = aTEOP
        Me.ddlTermOfPayment.DataValueField = "ID"
        Me.ddlTermOfPayment.DataTextField = "Description"
        Me.ddlTermOfPayment.DataBind()
        If oTEOP.PaymentType = enumPaymentType.PaymentType.TOP Then
            Me.ddlTermOfPayment.SelectedValue = oTEOP.ID
        End If
    End Sub

#End Region

End Class
