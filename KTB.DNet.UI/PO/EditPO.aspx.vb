#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.Transfer
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.MDP
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
#End Region

Public Class EditPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
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
    Protected WithEvents btnKirim2 As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents icPermintaanKirim As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalBiayaKirim As System.Web.UI.WebControls.Label
    Protected WithEvents btnHitung As System.Web.UI.WebControls.Button
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents lblNoPO As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm1 As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblJatuhTempo As System.Web.UI.WebControls.Label
    Protected WithEvents lblF1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblF2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblF3 As System.Web.UI.WebControls.Label
    Protected WithEvents chkFreePPh As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkFactoring As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblFactoringColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactoring As System.Web.UI.WebControls.Label
    Protected WithEvents rdoByKTB As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rdoByDealer As System.Web.UI.WebControls.RadioButton
    Protected WithEvents hidPODestinationID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents txtPODestinationCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchPODestination As System.Web.UI.WebControls.Label

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
    Private sessionHelper As New SessionHelper
    Private SubTotalharga As Double
    Private SubTotalPPh As Double
    Private SubTotalInterest As Double
    Private SubTotalDeposit As Double
    Private SubTotalSisa As Double
    Private SubTotalOrder As Double
    Private SubTotalBiayaKirimPPN As Double
    Private arrOrder As New ArrayList
    Private nTOP As Integer
    Private nMonth As Integer
    Private objSPL As SPL
    Dim objDealer As Dealer

    Private objNHFac As NationalHolidayFacade = New NationalHolidayFacade(User)
    Private objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
    Private _sessIsTransfer As String = "FrmEditPO._sessIsTransfer"
#End Region

#Region "Custom Method"


    Private Function GetCurrentPaymentMethod(ByRef objPKHeader As PKHeader, ByRef obPOHeader As POHeader) As Short


        'start : add payment scheme information (Gyro or Transfer) on 20160815
        Dim cTC As New CriteriaComposite(New Criteria(GetType(TransferControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim curPeriod As Date = DateSerial(icPermintaanKirim.Value.Year, icPermintaanKirim.Value.Month, 1)
        Dim sTC As New SortCollection()
        Dim oTCFac As New TransferControlFacade(User)
        Dim aTCs As ArrayList
        Dim oTC As TransferControl
        Dim state As Short

        cTC.opAnd(New Criteria(GetType(TransferControl), "CreditAccount", MatchType.Exact, objPKHeader.Dealer.CreditAccount))
        cTC.opAnd(New Criteria(GetType(TransferControl), "ValidFrom", MatchType.LesserOrEqual, curPeriod))
        cTC.opAnd(New Criteria(GetType(TransferControl), "ProductCategory.ID", MatchType.Exact, objPKHeader.Category.ProductCategory.ID))
        cTC.opAnd(New Criteria(GetType(TransferControl), "PaymentType", MatchType.Exact, objPOHeader.TermOfPayment.PaymentType))

        sTC.Add(New Sort(GetType(TransferControl), "ValidFrom", Sort.SortDirection.DESC))
        aTCs = oTCFac.Retrieve(cTC, sTC)
        If (aTCs.Count > 0) Then
            oTC = aTCs(0)
            'objPOHeader.IsTransfer = oTC.Status
            state = oTC.Status
        Else
            'objPOHeader.IsTransfer = TransferControl.EnumPaymentScheme.Gyro
            state = TransferControl.EnumPaymentScheme.Gyro
        End If
        'end : add payment scheme information (Gyro or Transfer) on 20160815
        Return state
    End Function


#Region "Available Ceiling"

    Private Function AddWorkingDay(ByVal StateDate As Date, ByVal nAdded As Integer, Optional ByVal IsBackWard As Boolean = False) As Date
        'Dim objNHFac As NationalHolidayFacade = New NationalHolidayFacade(User)
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
        Dim DPFacade As New DailyPaymentFacade(User)

        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Dealer.CreditAccount", MatchType.Exact, objDealer.CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(TodayDate.AddMonths(-9), "yyyy/MM/dd")))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            'Total TotalLiquefied 
            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100329
            'If IsHavingGyro(objPOH) Then
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
            'End    :Optimize EffectiveDate calculation;By:DoniN;20100329
            If EffectiveDate >= TodayDate And EffectiveDate <= ReqDelDate Then
                If objPOH.Status = 8 Then
                    If EffectiveDate >= TodayDate.AddDays(1) Then
                        If objPOH.DailyPayments.Count = 0 Then
                            TotalLiquefied += objPOH.TotalPODetail()
                        Else
                            'For Each objDP As DailyPayment In objPOH.DailyPayments
                            '    If (objDP.PaymentPurpose.ID = 3 Or objDP.PaymentPurpose.ID = 6) And objDP.IsReversed = 0 And objDP.IsCleared = 0 And objDP.EffectiveDate >= DateSerial(Now.Year, Now.Month, Now.Day + 1) And objDP.EffectiveDate <= ReqDelDate Then
                            '        TotalLiquefied = TotalLiquefied + objDP.Amount
                            '        'sb.Append(objPOH.PONumber & vbTab & objPOH.Status & vbTab & objDP.Amount & vbTab & EffectiveDate.ToString("dd/MM/YYYY") & vbTab & "DPID=" & objDP.PaymentPurpose.ID & Chr(13))
                            '    End If
                            'Next
                            Dim tmpTotal As Decimal = 0
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

                        End If
                    End If
                Else
                    TotalLiquefied += objPOH.TotalPODetail()
                End If
            End If

            'End Total TotalLiquefied
            'End Total PO 3
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
        'arlDP = objDPFac.Retrieve(crtDP)
        'For Each oDP As DailyPayment In arlDP
        '    TotalAcceleratedGyro += oDP.Amount
        'Next

        arlResult.Add(TotalLiquefied)
        arlResult.Add(TotalAcceleratedGyro)
        Return arlResult
    End Function

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

    Private Function IsHavingGyro(ByRef objPOH As POHeader) As Boolean
        Dim crtDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlDP As New ArrayList

        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, objPOH.ID))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        arlDP = objDPFac.Retrieve(crtDP)
        Return IIf(arlDP.Count > 0, True, False)
        'Dim Rsl As Boolean = True

        'If objPOH.DailyPayments.Count < 1 Then
        '    Rsl = False
        'Else
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

    Private Function GetTotalPOInThisTrans() As Decimal
        Dim POInThisTrans As Decimal = 0

        For Each di As DataGridItem In dtgDetail.Items
            Dim TextBox1 As TextBox = di.FindControl("TextBox1")
            Dim objPODFac As PODetailFacade = New PODetailFacade(User)
            Dim objPOD As PODetail = objPODFac.Retrieve(CType(di.Cells(0).Text, Integer))
            Dim objCD As ContractDetail = objPODFac.GetContractDetail(objPOD)   'objPOH.ContractHeader.ContractDetails(di.ItemIndex)

            POInThisTrans += CType(TextBox1.Text, Decimal) * objCD.Amount
        Next
        Return POInThisTrans
    End Function

    Private Function IsLesserThanAvailableCeiling(Optional ByVal IsAfterSaving As Boolean = False) As Boolean
        Dim objD As Dealer = Session("DEALER")
        Dim TotalPO As Decimal = Me.GetTotalPOInThisTrans() ' CType(viewstate.Item("SubTotalHarga"), Decimal)
        Dim oTEOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(Me.ddlTermOfPayment.SelectedValue, Integer))
        Dim IsLesser As Boolean = False
        Dim objPOH As POHeader = CType(sessionHelper.GetSession("PO"), POHeader)

        If chkFactoring.Checked Then
            Dim AvFactCeiling As Decimal = 0
            Dim oFM As FactoringMaster = New FactoringMasterFacade(User).Retrieve(Me.GetProductCategory(), objD.CreditAccount)

            If oTEOP.PaymentType = enumPaymentType.PaymentType.TOP Then
                Dim dtJatuhTempo As Date = DateAdd(DateInterval.Day, oTEOP.TermOfPaymentValue, Me.icPermintaanKirim.Value)
                If dtJatuhTempo > oFM.MaxTOPDate Then
                    MessageBox.Show("Jatuh Tempo PO Melebihi Tanggal Validitas Ceiling")
                    Return False
                End If
            End If
            IsLesser = CommonFunction.IsEnoughForFactoring(Me.GetProductCategory(), objPOH.ID, TotalPO, CType(Session("DEALER"), Dealer).CreditAccount, IsAfterSaving, AvFactCeiling)       ' IsEnoughForFactoring()
            Me.lblF1.Text = FormatNumber(AvFactCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.lblF2.Text = FormatNumber(TotalPO, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.lblF3.Text = FormatNumber(objPOH.TotalHarga(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Me.lblAvailable.Text = FormatNumber(AvFactCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Else
            Dim Ceiling As Decimal = 0
            Dim Proposed As Decimal = 0
            Dim Liquified As Decimal = 0
            Dim Outstanding As Decimal = 0
            Dim TodaysAvCeiling As Decimal = 0
            Dim TomorrowAvCeiling As Decimal = 0
            Dim AvCeiling As Decimal = 0

            'Credit Ceiling


            If oTEOP.PaymentType = CType(enumPaymentType.PaymentType.TOP, Short) Then
                If IsNothing(objPOHeader) Then
                    BindToPOHeaderObject()
                End If

                Dim paymentScheme As Short = Me.GetCurrentPaymentMethod(objPOHeader.ContractHeader.PKHeader, objPOHeader)

                If paymentScheme = TransferControl.EnumPaymentScheme.Gyro Then

                    Dim objSCM As sp_CreditMaster = GetCeilingCredit(Me.GetProductCategory(), objD.CreditAccount, oTEOP.PaymentType)
                    If objSCM Is Nothing Then
                        MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
                        Return False
                    Else
                        If objSCM.ID < 1 Then
                            MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
                            Return False
                        End If
                    End If
                    Dim dtJatuhTempo As Date = DateAdd(DateInterval.Day, oTEOP.TermOfPaymentValue, Me.icPermintaanKirim.Value)
                    If (objPOH.IsTransfer <> 1) Then
                        If dtJatuhTempo > objSCM.MaxTOPDate Then
                            MessageBox.Show("Jatuh Tempo PO Melebihi Tanggal Validitas Ceiling")
                            Return False
                        End If
                    End If


                    IsLesser = CommonFunction.IsCeilingEnoughSimulationTOP(Me.GetProductCategory(), objPOH.ID, Me.icPermintaanKirim.Value, TotalPO, objD.CreditAccount, oTEOP.PaymentType, IsAfterSaving, Ceiling, Proposed, Liquified, Outstanding, TodaysAvCeiling, TomorrowAvCeiling, AvCeiling)
                    'Me.lblF1.Text = "Today = " & FormatNumber(TodaysAvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    'Me.lblF1.Text &= " ; Tomorrow = " & FormatNumber(TomorrowAvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    If TodaysAvCeiling > TomorrowAvCeiling Then
                        Me.lblF1.Text = FormatNumber(TomorrowAvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Else
                        Me.lblF1.Text = FormatNumber(TodaysAvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    End If
                    Me.lblF2.Text = FormatNumber(TotalPO, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Me.lblF3.Text = FormatNumber(objPOH.TotalHarga(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                Else 'paymentSchema = TRANSFER
                    Dim i As Integer = objPOHeader.ID
                    Dim oTCFac As New TransferCeilingFacade(User)
                    Dim oTC As New TransferCeiling()
                    Dim IsEnough As Boolean = False
                    Dim sMsg As String = ""
                    Dim NewAvCeiling As Decimal = 0

                    IsEnough = oTCFac.IsEnoughCeiling(objPOHeader, TotalPO, oTC, NewAvCeiling, sMsg)
                    ''  Me.lblCeiling.Text = FormatNumber(NewAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

                    If Not IsEnough Then MessageBox.Show(sMsg)

                    Return IsEnough
                End If
            End If


            'Me.lblCeiling.Text = FormatNumber(Ceiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Me.lblProposed.Text = FormatNumber(Proposed, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Me.lblLiquified.Text = FormatNumber(Liquified, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Me.lblOutstanding.Text = FormatNumber(Outstanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Me.lblTodayAvCeiling.Text = FormatNumber(TodaysAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Me.lblTomorrowAvCeiling.Text = FormatNumber(TomorrowAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            'Me.lblAvailable.Text = FormatNumber(AvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
        Return IsLesser
    End Function


#End Region '"Available Ceiling"


#Region "RemainCeiling Old"

    Private Function GetRemainCeilingOld(ByVal AvCeiling As Decimal, ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
        Dim RemCeilH As Decimal = 0
        Dim RemCeilHPlus1 As Decimal = 0

        RemCeilH = AvCeiling - GetReqPO(CreditAccount, PaymentType, StartDate, EndDate) + GetPOCair(CreditAccount, PaymentType, StartDate, EndDate)
        RemCeilHPlus1 = AvCeiling - GetReqPO(CreditAccount, PaymentType, StartDate.AddDays(1), EndDate.AddDays(1)) + GetPOCair(CreditAccount, PaymentType, StartDate.AddDays(1), EndDate.AddDays(1))
        If RemCeilH < RemCeilHPlus1 Then
            Return RemCeilH
        Else
            Return RemCeilHPlus1
        End If

    End Function
    Private Function GetReqPOOld(ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal

        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As New ArrayList
        Dim Sql As String = ""
        Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim Total As Decimal = 0

        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, StartDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EndDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and dp.RejectStatus=0 and dp.IsReversed=1 and dp.IsCleared<>1 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & ")"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next
        Return Total
    End Function
    Private Function GetTotalPODetailOld(ByRef objPOH As POHeader) As Decimal
        Dim Total As Decimal = 0
        Total = objPOH.TotalPODetail
        'For Each objPOD As PODetail In objPOH.PODetails
        '    If objPOH.Status = 0 Or objPOH.Status = 2 Then
        '        Total = Total + (objPOD.ReqQty * objPOD.Price)
        '    ElseIf objPOH.Status = 4 Or objPOH.Status = 6 Or objPOH.Status = 8 Then
        '        Total = Total + (objPOD.AllocQty * objPOD.Price)
        '    End If
        'Next
        Return Total
    End Function
    Private Function GetPOCairOld(ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
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
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        arlDP = objDPFac.Retrieve(crtDP)
        For Each objDP As DailyPayment In arlDP
            Total += objDP.Amount
        Next
        'Not Accelerated
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, "0"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
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
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next

        Return Total

    End Function

#End Region


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
        '
        For i = 0 To DateDiff(DateInterval.Day, StartDate, EndDate) - 1
            If i = 0 Then
                FocusedDate = StartDate
                If FocusedDate = EndDate Then Exit For
                'TotReq = GetReqPO(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                TotReq = GetReqPO(CreditAccount, PaymentType, FocusedDate, FocusedDate)

                TotCair = 0 'GetPOCair(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                AvCeiling = AvCeiling - TotReq + TotCair 'it's covered by SAP Application
            Else
                FocusedDate = AddWorkingDay(FocusedDate, 1)
                If FocusedDate = EndDate Then Exit For
                'TotReq = GetReqPO(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                'TotCair = GetPOCair(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                TotReq = GetReqPO(CreditAccount, PaymentType, FocusedDate, FocusedDate)
                TotCair = GetPOCair(CreditAccount, PaymentType, FocusedDate, FocusedDate)
                AvCeiling = AvCeiling - TotReq + TotCair
            End If
        Next
        'End    :Get AvCeiling from Looping ' starting from ReportDate to ReqDelDate

        StartDate = EndDate
        Dim TotalA As Decimal = GetReqPO(CreditAccount, PaymentType, StartDate, EndDate)
        Dim TotalB As Decimal = GetPOCair(CreditAccount, PaymentType, StartDate, EndDate)
        'lblAvCeilingFirst.Text = AvCeiling
        'lblA.Text = TotalA
        'lblC.Text = TotalB
        RemCeilH = AvCeiling - TotalA + TotalB
        'TotalA = GetReqPO(CreditAccount, PaymentType, AddWorkingDay(startdate,1) StartDate.AddDays(1), EndDate.AddDays(1))
        TotalA = GetReqPO(CreditAccount, PaymentType, AddWorkingDay(StartDate, 1), AddWorkingDay(StartDate, 1))
        TotalB = GetPOCair(CreditAccount, PaymentType, AddWorkingDay(StartDate, 1), AddWorkingDay(StartDate, 1))
        'lblB.Text = TotalA
        'lblD.Text = TotalB
        'RemCeilHPlus1 = AvCeiling - TotalA + TotalB
        RemCeilHPlus1 = RemCeilH - TotalA + TotalB
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
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and dp.RejectStatus=0 and dp.IsReversed=1 and dp.IsCleared<>1 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short).ToString & ")"
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
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(" & CType(EnumPaymentStatus.PaymentStatus.Selesai, String) & ")"))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.No, 1))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        arlDP = objDPFac.Retrieve(crtDP)
        For Each objDP As DailyPayment In arlDP
            Total += objDP.Amount
        Next
        'Not Accelerated
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, "0"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(" & CType(EnumPaymentStatus.PaymentStatus.Selesai, String) & ")"))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.No, 1))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        arlDP = objDPFac.Retrieve(crtDP)
        For Each objDP As DailyPayment In arlDP
            Total += objDP.Amount
        Next

        'doesn't have gyro
        StartDate = AddWorkingDay(StartDate, -2, True)
        EndDate = AddWorkingDay(EndDate, -2, True)
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0 and IsReversed<>1 and IsCleared<>1 and Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & ")"
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

    Private Function ValidasiWaktuPengajuan() As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, icPermintaanKirim.Value.Day))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, icPermintaanKirim.Value.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, icPermintaanKirim.Value.Year))
        Dim arlNationalHoliday As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criterias)
        If (arlNationalHoliday.Count = 0) Then
            'start  :check, buat hari libur, dan tanggal permintaan kirim adalah hari pertama masuk setelah libur
            If CommonFunction.AddNWorkingDay(DateSerial(Now.AddDays(-1).Year, Now.AddDays(-1).Month, Now.AddDays(-1).Day), 1, False).ToString("yyyyMMdd") <> Now.ToString("yyyyMMdd") Then 'it means holiday
                If CommonFunction.AddNWorkingDay(DateSerial(Now.AddDays(-1).Year, Now.AddDays(-1).Month, Now.AddDays(-1).Day), 1, False).ToString("yyyyMMdd") = Me.icPermintaanKirim.Value.ToString("yyyyMMdd") Then
                    MessageBox.Show("Tanggal permintaan kirim " & Me.icPermintaanKirim.Value.ToString("dd MMM yyyy") & " tidak bisa dibuat hari ini. \nPengajuan maksimal pada tanggal " & CommonFunction.AddNWorkingDay(Now, 1, True).ToString("dd MMM yyyy"))
                    Return False
                End If
            End If
            'end    :check, buat hari libur, dan tanggal permintaan kirim adalah hari pertama masuk setelah libur
            objPOHeader = sessionHelper.GetSession("PO")
            If ddlOrderType.SelectedValue = LookUp.EnumJenisOrder.Harian Then
                If (icPermintaanKirim.Value > DateTime.Now) AndAlso (icPermintaanKirim.Value.Month = objPOHeader.ReqAllocationMonth) Then
                    If Not (icPermintaanKirim.Value.Date = DateTime.Now.Date) Then
                        Dim nextDate As Date = New NationalHolidayFacade(User).RetrieveNextDay(DateTime.Now.AddDays(1))
                        If (icPermintaanKirim.Value.Date = nextDate.Date) Then
                            Dim Batas As String() = KTB.DNet.Lib.WebConfig.GetValue("BatasPOHarian").ToString.Split(":")
                            Dim Waktu As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CInt(Batas(0)), CInt(Batas(1)), 0)
                            If Not (DateTime.Now < Waktu) Then
                                'MessageBox.Show("Batas Waktu pembuatan PO untuk pengiriman Besok sudah lewat")
                                MessageBox.Show(SR.InvalidCreateDate("PO"))
                                Return False
                            End If
                        End If
                    Else
                        'MessageBox.Show(SR.InvalidSendDate)

                        'Start  :RemainModule : set POValid if RemarkStatus is in (GantiKe*)
                        If objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeCOD Or objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeRTGS Or objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeTOP Then
                        Else
                            MessageBox.Show(SR.InvalidSendDate)
                        End If
                        'End    :RemainModule : set POValid if RemarkStatus is in (GantiKe*)
                    End If
                Else
                    'MessageBox.Show(SR.InvalidSendDate)
                    'Return False

                    'Start  :RemainModule : set POValid if RemarkStatus is in (GantiKe*)
                    If objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeCOD Or objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeRTGS Or objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeTOP Then
                    Else
                        'reamrks by anh 20160630 req by yurike
                        'Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetValue("PODateAllowed") 'yyyyMMdd - 20141031
                        Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetString(SR.PODateAllowed) 'yyyyMMdd - 20141031
                        If Not (Date.Now.ToString("yyyyMMdd") = strPODateAllowed) Then
                            '  If Not (Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 30).ToString("yyyyMMdd")) Then
                            MessageBox.Show(SR.InvalidSendDate)
                            Return False
                        End If

                        'reamrks by anh 20160630 req by yurike
                    End If
                    'End    :RemainModule : set POValid if RemarkStatus is in (GantiKe*)
                End If
            Else
                If Not (IsValidPOTambahan()) Then
                    MessageBox.Show(SR.InvalidCreateDate("PO Tambahan"))
                    Return False
                Else
                    Dim nextDatePO As Date = New NationalHolidayFacade(User).RetrieveNextDay(DateTime.Now.AddDays(1))
                    Dim startDatePO As Date = New Date(nextDatePO.Year, nextDatePO.Month, nextDatePO.Day, 0, 0, 0)
                    Dim endDatePO As Date = New Date(nextDatePO.Year, nextDatePO.Month, nextDatePO.Day, 23, 59, 59)
                    'reamrks by anh 20160630 req by yurike
                    If Not ((icPermintaanKirim.Value.Date >= startDatePO) And (icPermintaanKirim.Value.Date <= endDatePO)) Then
                        'Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetValue("PODateAllowed") 'yyyyMMdd - 20141031
                        Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetString(SR.PODateAllowed)
                        If Not (Date.Now.ToString("yyyyMMdd") = strPODateAllowed) Then
                            '  If Not (Date.Now.ToString("yyyyMMdd") = New Date(2014, 6, 30).ToString("yyyyMMdd")) Then

                            MessageBox.Show(SR.InvalidSendDate)
                            Return False
                        End If

                    End If
                    'end reamrks by anh 20160630 req by yurike

                    'If Not (icPermintaanKirim.Value.Date = DateTime.Now.Date) Then
                    '    MessageBox.Show(SR.InvalidSendDate)
                    '    Return False
                    'End If
                End If
            End If
        Else
            MessageBox.Show(SR.InvalidSendDate & " (" & arlNationalHoliday(0).Description & ")")
            Return False
        End If

        'start   : OC carry over dengan pricing-date baru (contoh : oc carryover 2 Jan tapi pakai pricingdate 15 jan, karena lebih murah, permintaan dealer
        Me.GetPO()
        Dim objContractHeader As ContractHeader = objPOHeader.ContractHeader
        Dim PricingDate As Date = New Date(objContractHeader.PricePeriodYear, objContractHeader.PricePeriodMonth, objContractHeader.PricePeriodDay)
        Dim DeliveryDate As Date = Me.icPermintaanKirim.Value
        If DeliveryDate < PricingDate Then
            MessageBox.Show("Tanggal permintaan kirim tidak boleh sebelum tanggal berlakunya harga kendaraan " & PricingDate.ToString("dd MM yyyy") & ". Untuk lebih lanjut hubungi MMKSI – Whole Sales Dept.")
            Return False
        End If
        'end     : OC carry over dengan pricing-date baru (contoh : oc carryover 2 Jan tapi pakai pricingdate 15 jan, karena lebih murah, permintaan dealer

        Return True
    End Function

    Private Function POIsValid(ByRef isValidateSPL As Boolean) As Boolean
        sessionHelper.SetSession(_sessIsTransfer, 0)
        BindToPOHeaderObject()
        'replaced with ValidasiWaktuPengajuan   

        If Not Me.ValidasiWaktuPengajuan() Then Return False 'double validation;by:dna;for:yurike;on:20110626

        objPOHeader = sessionHelper.GetSession("PO")
        Dim objPKHead As PKHeader = New PKHeaderFacade(User).Retrieve(objPOHeader.ContractHeader.PKNumber)
        'objSPL = New SPLFacade(User).Retrieve(objPKHead.SPLNumber.ToString())
        Dim objTOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
        Dim MaxTOPValue As Integer = 0

        'If objPOHeader.IsFactoring = 1 Then
        '    MaxTOPValue = DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth)
        '    If objTOP.TermOfPaymentValue > MaxTOPValue Then
        '        MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
        '        Return False
        '    End If

        '    'Return True
        'End If

        If objPOHeader.IsFactoring = 1 OrElse (objPKHead Is Nothing OrElse objPKHead.MaxTopIndicator = -1) Then
            MaxTOPValue = POHeaderFacade.GetMinTOPDaysByVehicleType(objPOHeader, objPOHeader.PODetails, (objPOHeader.IsFactoring = 1))
            If MaxTOPValue = 0 Then
                MaxTOPValue = DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth)
            End If
            If objTOP.TermOfPaymentValue > MaxTOPValue Then
                MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
                Return False
            End If
        Else
            If objPKHead.MaxTopIndicator = 0 Then
                MaxTOPValue = objPKHead.MaxTOPDate.Subtract(icPermintaanKirim.Value).Days
            ElseIf objPKHead.MaxTopIndicator = 1 Then
                MaxTOPValue = objPKHead.MaxTopDay
            End If
        End If

        If objPOHeader.IsFactoring <> 1 Then
            Dim IsTOPBySPLOk As Boolean = False
            If objTOP.PaymentType = CType(enumPaymentType.PaymentType.TOP, Integer) Then
                'start : Installment & SPL Validation on 20160815
                If Not IsNothing(objSPL) Then
                    IsTOPBySPLOk = True
                    objPOHeader.SPL = objSPL 'TODO
                    If objSPL.NumOfInstallment > 1 AndAlso objTOP.TermOfPaymentValue <> objSPL.MaxTOPDay Then
                        MessageBox.Show("Cara Pembayaran harus sama dengan di Maks hari TOP di SPL yaitu : " & objSPL.MaxTOPDay.ToString())
                        Return False
                    End If

                    If objSPL.NumOfInstallment <= 1 AndAlso Not IsNothing(objPKHead) AndAlso objPKHead.MaxTopDay > 0 AndAlso objTOP.TermOfPaymentValue > 0 AndAlso objTOP.TermOfPaymentValue > objPKHead.MaxTopDay Then
                        IsTOPBySPLOk = True
                        MessageBox.Show("Maximum TOP " & objPKHead.MaxTopDay.ToString() & " Hari")
                        Return False
                    End If
                End If

                If IsTOPBySPLOk = False Then
                    If objTOP.TermOfPaymentValue > MaxTOPValue Then
                        MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
                        Return False
                    ElseIf objTOP.TermOfPaymentValue < MaxTOPValue Then
                        If Not (objPKHead Is Nothing OrElse objPKHead.MaxTopIndicator = -1) Then
                            If ViewState("ShowMessage") = "OK" Then
                                Return True
                            Else
                                ViewState("ShowMessage") = "OK"
                                MessageBox.Show("Maximum TOP yang bisa anda gunakan " & MaxTOPValue & " Hari")
                                Return False
                            End If
                        End If
                    End If
                Else
                    MaxTOPValue = POHeaderFacade.GetMinTOPDaysByVehicleType(objPOHeader, objPOHeader.PODetails, (objPOHeader.IsFactoring = 1))
                    If MaxTOPValue = 0 Then
                        MaxTOPValue = DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth)
                    End If
                    If objTOP.TermOfPaymentValue > (MaxTOPValue) Then
                        MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
                        Return False
                    End If
                End If
                'end : Installment & SPL Validation on 20160815 
            End If

            If Not String.IsNullOrEmpty(objPKHead.SPLNumber) Then
                If objPKHead.MaxTopDay > 0 Then
                    isValidateSPL = True
                End If
            End If
            ''start : add payment scheme information (Gyro or Transfer) on 20160815
            'Dim cTC As New CriteriaComposite(New Criteria(GetType(TransferControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'Dim curPeriod As Date = DateSerial(icPermintaanKirim.Value.Year, icPermintaanKirim.Value.Month, 1)
            'Dim sTC As New SortCollection()
            'Dim oTCFac As New TransferControlFacade(User)
            'Dim aTCs As ArrayList
            'Dim oTC As TransferControl

            'cTC.opAnd(New Criteria(GetType(TransferControl), "CreditAccount", MatchType.Exact, objPOHeader.Dealer.CreditAccount))
            'cTC.opAnd(New Criteria(GetType(TransferControl), "ValidFrom", MatchType.LesserOrEqual, curPeriod))
            'cTC.opAnd(New Criteria(GetType(TransferControl), "ProductCategory.ID", MatchType.LesserOrEqual, objPOHeader.ContractHeader.Category.ProductCategory.ID))
            'cTC.opAnd(New Criteria(GetType(TransferControl), "PaymentType", MatchType.LesserOrEqual, objPOHeader.TermOfPayment.PaymentType))

            'sTC.Add(New Sort(GetType(TransferControl), "ValidFrom", Sort.SortDirection.DESC))
            'aTCs = oTCFac.Retrieve(cTC, sTC)
            'If (aTCs.Count > 0) Then
            '    oTC = aTCs(0)
            '    objPOHeader.IsTransfer = oTC.Status
            'Else
            '    objPOHeader.IsTransfer = TransferControl.EnumPaymentScheme.Gyro
            'End If
            ''end : add payment scheme information (Gyro or Transfer) on 20160815
            'sessionHelper.SetSession(_sessIsTransfer, objPOHeader.IsTransfer)
        Else
            objPOHeader.IsTransfer = 0
            sessionHelper.SetSession(_sessIsTransfer, objPOHeader.IsTransfer)
        End If

        Return True
    End Function

    ''' <summary>
    ''' New Function
    ''' </summary>
    ''' <param name="v_objPOHeader"></param>
    ''' <remarks></remarks>
    Private Sub IsTransferChecking(ByRef v_objPOHeader As POHeader)
        If v_objPOHeader.IsFactoring <> 1 Then

            'start : add payment scheme information (Gyro or Transfer) on 20160815
            Dim cTC As New CriteriaComposite(New Criteria(GetType(TransferControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim curPeriod As Date = DateSerial(icPermintaanKirim.Value.Year, icPermintaanKirim.Value.Month, 1)
            Dim sTC As New SortCollection()
            Dim oTCFac As New TransferControlFacade(User)
            Dim aTCs As ArrayList
            Dim oTC As TransferControl

            cTC.opAnd(New Criteria(GetType(TransferControl), "CreditAccount", MatchType.Exact, v_objPOHeader.Dealer.CreditAccount))
            cTC.opAnd(New Criteria(GetType(TransferControl), "ValidFrom", MatchType.LesserOrEqual, curPeriod))
            cTC.opAnd(New Criteria(GetType(TransferControl), "ProductCategory.ID", MatchType.LesserOrEqual, v_objPOHeader.ContractHeader.Category.ProductCategory.ID))
            cTC.opAnd(New Criteria(GetType(TransferControl), "PaymentType", MatchType.LesserOrEqual, v_objPOHeader.TermOfPayment.PaymentType))

            sTC.Add(New Sort(GetType(TransferControl), "ValidFrom", Sort.SortDirection.DESC))
            aTCs = oTCFac.Retrieve(cTC, sTC)
            If (aTCs.Count > 0) Then
                oTC = aTCs(0)
                v_objPOHeader.IsTransfer = oTC.Status
            Else
                v_objPOHeader.IsTransfer = TransferControl.EnumPaymentScheme.Gyro
            End If
            'end : add payment scheme information (Gyro or Transfer) on 20160815
            sessionHelper.SetSession(_sessIsTransfer, v_objPOHeader.IsTransfer)
        Else
            v_objPOHeader.IsTransfer = 0
            sessionHelper.SetSession(_sessIsTransfer, v_objPOHeader.IsTransfer)
        End If
    End Sub

    Private Function IsValidPOTambahan() As Boolean
        Dim BatasAwal As String() = KTB.DNet.Lib.WebConfig.GetValue("AwalBatasPOTambahan").ToString.Split(":")
        Dim BatasAkhir As String() = KTB.DNet.Lib.WebConfig.GetValue("AkhirBatasPOTambahan").ToString.Split(":")
        Dim WaktuAwal As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CInt(BatasAwal(0)), CInt(BatasAwal(1)), 0)
        Dim WaktuAkhir As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CInt(BatasAkhir(0)), CInt(BatasAkhir(1)), 0)
        If Not ((DateTime.Now >= WaktuAwal) And (DateTime.Now <= WaktuAkhir)) Then
            Return False
        End If
        Return True
    End Function


    Private Sub GetPO()
        Dim POid As String = Request.QueryString("id")
        objPOHeader = New POHeaderFacade(User).Retrieve(CInt(POid))
        sessionHelper.SetSession("PO", objPOHeader)
    End Sub


    Private Function GetProductCategory() As ProductCategory
        If Me.sessionHelper.GetSession("PO") Is Nothing Then
            Me.GetPO()
        End If
        Dim oPOH As POHeader = CType(Me.sessionHelper.GetSession("PO"), POHeader)
        Dim oPC As ProductCategory = oPOH.ContractHeader.Category.ProductCategory

        Return oPC
    End Function

    Private Sub BindHeaderToForm()
        '--Bind From Label
        lblDealerCode.Text = objPOHeader.ContractHeader.Dealer.DealerCode
        lblSearchTerm1.Text = objPOHeader.ContractHeader.Dealer.SearchTerm1
        lblName.Text = objPOHeader.ContractHeader.Dealer.DealerName
        lblContractNumber.Text = objPOHeader.ContractHeader.ContractNumber
        lblCity.Text = objPOHeader.ContractHeader.Dealer.City.CityName
        'lblCreated.Text = objPOHeader.ContractHeader.CreatedBy
        lblOrderType.Text = CType(objPOHeader.ContractHeader.ContractType, enumOrderType.OrderType).ToString
        lblSalesOrg.Text = objPOHeader.ContractHeader.Category.CategoryCode
        lblProductYear.Text = objPOHeader.ContractHeader.ProductionYear
        lblProjectName.Text = objPOHeader.ContractHeader.ProjectName

        '-- Commented by Agus P.
        lblNoPO.Text = objPOHeader.PONumber

        '--Bind From TextBox
        txtDealerPONumber.Text = objPOHeader.DealerPONumber
        If lblTotal.Text = "" Then
            lblTotal.Text = FormatNumber(CType(objPOHeader.TotalHarga, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
        If lblTotalBiayaKirim.Text = "" Then
            lblTotalBiayaKirim.Text = FormatNumber(CType(objPOHeader.TotalHargaLC, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
        '--Bind From DropDownList
        Me.ddlTermOfPayment.DataSource = New TermOfPaymentFacade(User).RetrieveActiveList()
        Me.ddlTermOfPayment.DataValueField = "ID"
        Me.ddlTermOfPayment.DataTextField = "Description"
        Me.ddlTermOfPayment.DataBind()
        Me.ddlTermOfPayment.SelectedValue = objPOHeader.TermOfPayment.ID

        For Each item As ListItem In LookUp.ArrayJenisPO
            ddlOrderType.Items.Add(item)
        Next
        Me.ddlOrderType.SelectedValue = objPOHeader.POType

        '--Bind From Calendar 
        icPermintaanKirim.Value = objPOHeader.ReqAllocationDateTime

        lblJatuhTempo.Text = Format(objPOHeader.ReqAllocationDateTime.AddDays(objPOHeader.TermOfPayment.TermOfPaymentValue), "dd/MM/yyyy")

        'Start  :RemainModule-DailyPO:Free PPH DoniN
        chkFreePPh.Checked = IIf(objPOHeader.FreePPh22Indicator = 0, True, False)
        If objPOHeader.Status = enumStatusPO.Status.Baru Then
            SetFreePPh() 'Get Latest FreePPh Status
            objPOHeader.FreePPh22Indicator = IIf(chkFreePPh.Checked, 0, 1)
        End If
        'End    :RemainModule-DailyPO:Free PPH DoniN


        'Start  :Factoring;by:dna;on:20101004;for:yurike;remark:set control
        Me.chkFactoring.Checked = IIf(objPOHeader.IsFactoring = 1, True, False)
        Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
        Dim oDealer As Dealer = CType(Session("DEALER"), Dealer)
        If CommonFunction.GetTransControlStatus(oDealer, EnumDealerTransType.DealerTransKind.FactoringMMC) AndAlso oFMFac.IsAllowedToProposePO(objPOHeader.ContractHeader.Category.ProductCategory, oDealer.CreditAccount) Then
            Me.chkFactoring.Enabled = True
        Else
            Me.chkFactoring.Enabled = False
        End If
        'End    :Factoring;by:dna;on:20101004;for:yurike;remark:set control

        'Add PODestination By WDI for Isye 20161208
        If Not IsNothing(objPOHeader.PODestination) Then
            hidPODestinationID.Value = objPOHeader.PODestination.ID
            If hidPODestinationID.Value = "-1" OrElse hidPODestinationID.Value = "1" Then
                txtPODestinationCode.Text = ""
                rdoByDealer.Checked = True
            Else
                txtPODestinationCode.Text = objPOHeader.PODestination.Code & "/ " & objPOHeader.PODestination.Nama
                rdoByKTB.Checked = True
            End If
        End If
    End Sub


    Private Sub SetFreePPh()
        Dim objD As Dealer = CType(Session("DEALER"), Dealer)
        Dim CreatePODate As Date = DateSerial(Now.Year, Now.Month, Now.Day)
        Dim objFPFac As FreePPhFacade = New FreePPhFacade(User)
        Dim crtFP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreePPh), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlFP As ArrayList
        Dim objCH As ContractHeader

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

        'objCH = objPOHeader.ContractHeader
        'objCH.FreePPh22Indicator = IIf(chkFreePPh.Checked, 0, 1)
        'objCHFac.Update(objCH)
        'crtPO.opAnd(New Criteria(GetType(POHeader), "ContractHeader.ID", MatchType.Exact, objCH.ID))
        'crtPO.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.Exact, CType(enumStatusPO.Status.Baru, Short).ToString))
        'arlPO = objPOFac.Retrieve(crtPO)
        'For Each objPO As POHeader In arlPO
        '    objPO.FreePPh22Indicator = objCH.FreePPh22Indicator
        '    objPOFac.Update(objPO)
        'Next
    End Sub


    Private Sub BindDetailToGrid()
        SubTotalDeposit = 0
        SubTotalharga = 0
        SubTotalPPh = 0
        SubTotalInterest = 0
        SubTotalBiayaKirimPPN = 0

        Dim objTOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
        nTOP = objTOP.TermOfPaymentValue
        nMonth = DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth)
        Dim objPKHead As PKHeader = New PKHeaderFacade(User).Retrieve(objPOHeader.ContractHeader.PKNumber)

        objSPL = New SPLFacade(User).Retrieve(objPKHead.SPLNumber.ToString())
        dtgDetail.DataSource = objPOHeader.PODetails
        dtgDetail.DataBind()
        If SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege) Then
            dtgDetail.Columns(6).Visible = False
        Else
            dtgDetail.Columns(6).Visible = True
        End If
        For Each item As DataGridItem In dtgDetail.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim lblFreeDays As Label = CType(item.FindControl("lblFreeDays"), Label)
                Dim lblMaxTOPDays As Label = CType(item.FindControl("lblMaxTOPDays"), Label)
                Dim txtQtyOrderBox As TextBox = item.FindControl("TextBox1")
                If txtQtyOrderBox.Text.Trim = "" Then txtQtyOrderBox.Text = 0

                Dim objPODFac As PODetailFacade = New PODetailFacade(User)
                Dim objPOD As PODetail = objPODFac.Retrieve(CType(item.Cells(0).Text, Integer))

                If ViewState("Hitung") Then

                    Dim getFreeDays As Integer = 0
                    Dim getMaxTopDays As Integer = 0
                    HitungSetFreeDays(objPOHeader, getMaxTopDays, getFreeDays, objPOD, txtQtyOrderBox.Text)

                    lblFreeDays.Text = getFreeDays
                    lblMaxTOPDays.Text = getMaxTopDays
                End If
            End If
        Next
        sums = 0
    End Sub

    Private Function BindToPOHeaderObject() As Decimal
        objPOHeader = sessionHelper.GetSession("PO")
        Dim orgTotalPengajuan As Decimal = objPOHeader.TotalHarga
        objPOHeader.DealerPONumber = txtDealerPONumber.Text
        objPOHeader.ReqAllocationDate = icPermintaanKirim.Value.Day
        objPOHeader.ReqAllocationMonth = icPermintaanKirim.Value.Month
        objPOHeader.ReqAllocationYear = icPermintaanKirim.Value.Year
        objPOHeader.ReqAllocationDateTime = icPermintaanKirim.Value.Date
        objPOHeader.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
        'Start  :RemainModule-DailyPO:Free PPH DoniN
        Me.SetFreePPh() 'get the latest FreePPh Status
        objPOHeader.FreePPh22Indicator = IIf(chkFreePPh.Checked = True, 0, 1) '  objPOHeader.ContractHeader.FreePPh22Indicator
        'Start  :RemainModule-DailyPO:Free PPH DoniN
        'Start  :Optimize EffectiveDate calculation;By:DoniN;20100329
        objPOHeader.EffectiveDate = New POHeaderFacade(User).GetPOEffectiveDate(objPOHeader.ReqAllocationDateTime, objPOHeader.TermOfPayment.PaymentType, objPOHeader.TermOfPayment.TermOfPaymentValue)
        'End    :Optimize EffectiveDate calculation;By:DoniN;20100329
        objPOHeader.IsFactoring = IIf(chkFactoring.Checked, 1, 0)

        If Not IsNothing(sessionHelper.GetSession(_sessIsTransfer)) Then
            objPOHeader.IsTransfer = CType(sessionHelper.GetSession(_sessIsTransfer), Short)
        End If

        'Add PODestination By WDI for Isye 20161208
        If rdoByKTB.Checked AndAlso (hidPODestinationID.Value <> "" AndAlso hidPODestinationID.Value <> "1" AndAlso hidPODestinationID.Value <> "-1" AndAlso txtPODestinationCode.Text.Trim() <> "") Then
            objPOHeader.PODestination = New PODestinationFacade(User).Retrieve(CType(hidPODestinationID.Value, Integer))
        Else
            objPOHeader.PODestination = New PODestinationFacade(User).Retrieve(1)
        End If

        BindToPODetailObject()
        Return orgTotalPengajuan
    End Function

    Private Sub BindToPODetailObject()
        For Each dtgitem As DataGridItem In dtgDetail.Items
            Dim txtBox As TextBox = dtgitem.FindControl("TextBox1")
            Dim lblFreeDays As Label = dtgitem.FindControl("lblFreeDays")
            Dim lblMaxTOPDays As Label = dtgitem.FindControl("lblMaxTOPDays")

            If Not ((txtBox.Text = String.Empty)) Then
                objPODetail = objPOHeader.PODetails(dtgitem.ItemIndex)
                objPODetail.ReqQty = txtBox.Text


                If objPODetail.ReqQty < 1 Then
                    objPODetail.Interest = 0
                Else
                    If objPOHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
                        objPODetail.Interest = CType(dtgitem.Cells(9).Text, Double) / objPODetail.ReqQty
                    Else
                        objPODetail.Interest = 0
                    End If
                End If

                ''Append Code Date 2014-08-29
                '' CR Factory DIscount
                '' By Ali Akbar
                If objPOHeader.IsFactoring Then
                    Dim objPrice As Price
                    objPrice = New PriceFacade(User).RetrieveByCriteria(objPODetail.ContractDetail)

                    nTOP = objPOHeader.TermOfPayment.TermOfPaymentValue
                    nMonth = DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth)
                    objPODetail.AmountRewardDepA = Calculation.CountRewardAmountDepositA(objPrice, nTOP, nMonth)
                    objPODetail.DiscountReward = objPrice.DiscountReward
                    objPODetail.AmountReward = Calculation.CountRewardAmount(objPODetail.ContractDetail, objPrice, nTOP, nMonth)
                    objPODetail.Price = Calculation.CountRewardsVehiclePrice(objPODetail.ContractDetail, objPrice, nTOP, nMonth)
                    objPODetail.PPh22 = Calculation.CountRewardPPh22(objPODetail.ContractDetail, objPrice, nTOP, nMonth)
                Else
                    objPODetail.DiscountReward = 0
                    objPODetail.AmountReward = 0
                    objPODetail.AmountRewardDepA = 0
                    objPODetail.Price = objPODetail.ContractDetail.Amount
                    objPODetail.PPh22 = objPODetail.ContractDetail.PPh22
                End If

                If CType(dtgitem.Cells(10).Text, Double) > 0 AndAlso CType(txtBox.Text, Double) > 0 Then
                    objPODetail.LogisticCost = CType(dtgitem.Cells(10).Text, Double) / CType(txtBox.Text, Double)
                Else
                    objPODetail.LogisticCost = 0
                End If
                ''End Append
                objPODetail.FreeDays = CType(lblFreeDays.Text, Integer)
                objPODetail.MaxTOPDay = CType(lblMaxTOPDays.Text, Integer)
            End If
        Next
    End Sub

    Private Sub ImplementCeiling()
        Dim objPOH As POHeader = CType(sessionHelper.GetSession("PO"), POHeader)
        Dim objTOPFac As TermOfPaymentFacade = New TermOfPaymentFacade(User)
        Dim PaymentTypeOfRemark As Integer
        Dim arlLI As New ArrayList

        If objPOH.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeCOD Or objPOH.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeRTGS Or objPOH.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeTOP Then
            'LockControls
            Me.icPermintaanKirim.Enabled = False
            For Each di As DataGridItem In dtgDetail.Items
                Dim TextBox1 As TextBox = di.FindControl("TextBox1")
                TextBox1.Enabled = False
            Next
            Me.txtDealerPONumber.Enabled = False
            'Filter
            If objPOH.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeCOD Then PaymentTypeOfRemark = enumPaymentType.PaymentType.COD
            If objPOH.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeTOP Then PaymentTypeOfRemark = enumPaymentType.PaymentType.TOP
            If objPOH.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeRTGS Then PaymentTypeOfRemark = enumPaymentType.PaymentType.RTGS
            For Each li As ListItem In ddlTermOfPayment.Items
                If objTOPFac.Retrieve(CType(li.Value, Integer)).PaymentType = objPOH.TermOfPayment.PaymentType Then
                    arlLI.Add(li)
                ElseIf objTOPFac.Retrieve(CType(li.Value, Integer)).PaymentType = PaymentTypeOfRemark Then
                    arlLI.Add(li)
                Else
                    'unused/hide
                End If
            Next
            ddlTermOfPayment.Items.Clear()
            For Each li As ListItem In arlLI
                ddlTermOfPayment.Items.Add(li)
            Next
        End If
    End Sub

    Private Function GetItemDeposit(ByVal oPOD As PODetail) As Double
        Dim oTEOPFac As TermOfPaymentFacade = New TermOfPaymentFacade(User)
        Dim oTEOP As TermOfPayment
        Dim TEOPID As Integer

        TEOPID = CType(ddlTermOfPayment.SelectedValue, Integer)
        oTEOP = oTEOPFac.Retrieve(TEOPID)
        If oTEOP.PaymentType = enumPaymentType.PaymentType.TOP Then
            Return oPOD.ContractDetail.GuaranteeAmount
        Else
            Return 0
        End If

        Exit Function

        If oPOD.POHeader.ContractHeader.PKHeader.JaminanID = 0 Then
            Return 0
        Else
            Dim oJFac As JaminanFacade = New JaminanFacade(User)
            Dim oJ As Jaminan
            oJ = oJFac.Retrieve(oPOD.POHeader.ContractHeader.PKHeader.JaminanID)
            For Each oJD As JaminanDetail In oJ.JaminanDetails
                'If oJD.VehicleTypeCode = oCD.VechileColor.VechileType.VechileTypeCode andalso (Me.icPermintaanKirim.Value >= oJD.Jaminan.ValidFrom  And Me.icPermintaanKirim.Value <= oJD.Jaminan.ValidTo) And (iif(ojd.Purpose =lookup.enumPurpose.Semua ,true,ojd.Purpose = oCD.ContractHeader.Purpose ) Then
                If oJD.VehicleTypeCode = oPOD.ContractDetail.VechileColor.VechileType.VechileTypeCode AndAlso (Me.icPermintaanKirim.Value >= oJD.Jaminan.ValidFrom And Me.icPermintaanKirim.Value <= oJD.Jaminan.ValidTo) And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = oPOD.POHeader.ContractHeader.Purpose) Then
                    Return oJD.Amount
                End If
            Next
        End If
        Return 0
    End Function

    Private Function IsEnableCeilingFilter() As Boolean
        If chkFactoring.Checked Then Return True
        Dim oD As Dealer = sessionHelper.GetSession("DEALER")
        Dim oTC As TransactionControl

        If Me.GetProductCategory().Code.Trim.ToUpper() = "MFTBC" Then
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

        'If IsNothing(oTC) Then
        '    Return True
        'Else
        '    If Not IsNothing(oTC) AndAlso oTC.ID > 0 AndAlso oTC.Status = 1 Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'End If
    End Function

    Private Sub SetControls()

        Dim IsImplementFactoring As Boolean = IIf(KTB.DNet.Lib.WebConfig.GetValue("ImpelementFactoring") = 1, True, False)
        If IsImplementFactoring Then
            Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(CType(Session("DEALER"), Dealer).ID, EnumDealerTransType.DealerTransKind.FactoringMMC)
            If Not (Not IsNothing(oTC) AndAlso oTC.Status = 1) Then
                IsImplementFactoring = False
            End If
        End If
        If IsImplementFactoring Then
            IsImplementFactoring = SecurityProvider.Authorize(Context.User, SR.Po_pengajuan_factoring_privilege)
        End If

        lblFactoring.Visible = IsImplementFactoring
        lblFactoringColon.Visible = IsImplementFactoring
        chkFactoring.Visible = IsImplementFactoring

        If chkFactoring.Checked AndAlso chkFactoring.Visible = False Then chkFactoring.Visible = True 'view historical data

        If dtgDetail.Items.Count > 0 Then
            Dim isAllMDP As Boolean = True
            For Each DataDetail As DataGridItem In dtgDetail.Items
                Dim chkIsMDP As CheckBox = DataDetail.FindControl("chkIsMDP")
                If chkIsMDP.Checked = False Then
                    isAllMDP = False
                End If
            Next
            If isAllMDP Then
                icPermintaanKirim.Enabled = False
            End If
        End If
    End Sub

    Private Function InvalidTransferDate(ByVal poh As POHeader) As Boolean
        If objPOHeader.IsTransfer = 1 AndAlso objPOHeader.IsFactoring = 0 AndAlso objPOHeader.TermOfPayment.PaymentType = CInt(enumPaymentType.PaymentType.TOP) Then

            Dim objD As Dealer = Session("DEALER")
            Dim productCategoryId As Integer = GetProductCategory().ID
            Dim vJthTempo As DateTime = poh.ReqAllocationDateTime.AddDays(poh.TermOfPayment.TermOfPaymentValue)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "CreditAccount", MatchType.Exact, objD.CreditAccount))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "PaymentType", MatchType.Exact, objPOHeader.TermOfPayment.PaymentType))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "ProductCategory.ID", MatchType.Exact, productCategoryId))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "ValidFrom", MatchType.LesserOrEqual, objPOHeader.ReqAllocationDateTime))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "Status", MatchType.Exact, 1))
            Dim sTC As New SortCollection()
            sTC.Add(New Sort(GetType(TransferControl), "ValidFrom", SortDirection.Descending))

            Dim otc As TransferControlFacade = New TransferControlFacade(User)
            Dim arrTC As ArrayList = New ArrayList()
            arrTC = otc.Retrieve(criterias, sTC)

            If IsNothing(arrTC) OrElse arrTC.Count = 0 Then
                Return True
            End If

            If vJthTempo > CType(arrTC(0), TransferControl).ValidityDate Then
                Return True
            End If
        End If


        Return False
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 500
        CheckUserPrivilege()
        'txtPODestinationCode.Text = ""
        If Not IsPostBack Then
            lblSearchPODestination.Attributes("onclick") = "ShowPPPODestination()"
            rdoByKTB.Attributes("onclick") = "SetPODestinationByKTB()"
            rdoByDealer.Attributes("onclick") = "SetPODestinationByDealer()"
            txtPODestinationCode.Attributes.Add("readonly", "readonly")
            ViewState("ShowMessage") = ""
            GetPO()
            For i As Integer = 0 To objPOHeader.PODetails.Count - 1
                arrOrder.Add(CType(objPOHeader.PODetails(i), PODetail).ReqQty)
            Next
            sessionHelper.SetSession("Ord", arrOrder)
            BindHeaderToForm()
            BindDetailToGrid()
            If Request.QueryString("src").ToUpper = "CREATENEW" Then
                MessageBox.Show("PO Berhasil Dibuat")
                If Not IsNothing(sessionHelper.GetSession("Warning")) Then
                    Dim wng As String = sessionHelper.GetSession("Warning")
                    If wng <> String.Empty AndAlso wng <> "" Then
                        MessageBox.Show(wng)
                    End If
                End If
            End If
            If Request.QueryString("src").ToUpper = "CREATE" Then
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.PO_Harian), objPOHeader.PONumber, -1, CInt(enumStatusPO.Status.Baru))
                MessageBox.Show("PO Berhasil Dibuat")
            End If

            ImplementCeiling()

        End If
        'Hidden1.Value = CInt(ViewState("Count")) + 1
        'ViewState("Count") = Hidden1.Value
        btnBatal.Attributes.Add("OnClick", "return confirm('Yakin PO ini akan dihapus?');")
        SetControls()
        btnKirim.Attributes.Add("onclick", "javascript:Disable()")
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PengajuanPO_Save_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Edit PO")
        End If
        btnKirim.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPOUpdate_Privilege)
        btnBatal.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPOStatusCancel_Privilege)

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Label10.Visible = isPriceVisible
        Total.Visible = isPriceVisible
        Label9.Visible = isPriceVisible
        lblTotal.Visible = isPriceVisible
        lblTotalBiayaKirim.Visible = isPriceVisible
        dtgDetail.Columns(6).Visible = isPriceVisible
        dtgDetail.Columns(7).Visible = isPriceVisible
        dtgDetail.Columns(8).Visible = isPriceVisible
        dtgDetail.Columns(9).Visible = isPriceVisible
    End Sub

    Sub dtgDetail_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dtgDetail.ItemDataBound
        If Not (objPOHeader.PODetails.Count = 0 Or e.Item.ItemIndex = -1) Then
            objPODetail = objPOHeader.PODetails(e.Item.ItemIndex)
            e.Item.Cells(2).Text = objPODetail.ContractDetail.VechileColor.MaterialNumber
            e.Item.Cells(3).Text = objPODetail.ContractDetail.VechileColor.MaterialDescription
            e.Item.Cells(4).Text = CInt(objPODetail.ContractDetail.SisaUnit) + CInt(objPODetail.ReqQty)
            Dim rangeValidator As RangeValidator = e.Item.FindControl("RangeValidator1")
            If CInt(e.Item.Cells(4).Text) > 0 Then
                rangeValidator.MaximumValue = CInt(e.Item.Cells(4).Text)
            Else
                rangeValidator.MaximumValue = 0
            End If

            e.Item.Cells(6).Text = FormatNumber(GetItemDeposit(objPODetail), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            SubTotalDeposit += CType(e.Item.Cells(6).Text, Double)
            Dim TxtBox As TextBox = e.Item.FindControl("TextBox1")
            TxtBox.Text = arrOrder(e.Item.ItemIndex)
            sums += CType(TxtBox.Text, Integer)
            e.Item.Cells(7).Text = FormatNumber(CType(TxtBox.Text, Double) * CType(objPODetail.ContractDetail.Amount, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            'Start  :RemainModule-DailyPO:FreePPh By:Doni N
            e.Item.Cells(8).Text = FormatNumber(CType(TxtBox.Text, Double) * CType(objPODetail.ContractDetail.PPh22, Double) * CInt(objPODetail.POHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'e.Item.Cells(7).Text = FormatNumber(CType(TxtBox.Text, Double) * CType(objPODetail.ContractDetail.PPh22, Double) * IIf(CInt(objPODetail.POHeader.FreePPh22Indicator) = 1, 0, 1), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Start  :RemainModule-DailyPO:FreePPh By:Doni N

            Dim arrPKDtl As ArrayList
            Dim freeIntIndicator As Integer
            Dim objPKHead As PKHeader = New PKHeaderFacade(User).Retrieve(objPODetail.POHeader.ContractHeader.PKNumber)
            freeIntIndicator = objPKHead.FreeIntIndicator
            arrPKDtl = objPKHead.PKDetails

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objPODetail.ContractDetail.VechileColor.ID))
            Dim oDealer As Dealer = Session("DEALER")
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, oDealer.ID))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
            'modified by ali
            'Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(criterias, sortColl)
            Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(objPODetail.ContractDetail)

            'Tambahan SLA
            If Not IsNothing(objPODetail.LogisticCost) Then
                e.Item.Cells(10).Text = FormatNumber(objPODetail.LogisticCost, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
            If rdoByKTB.Checked AndAlso (hidPODestinationID.Value <> "" AndAlso hidPODestinationID.Value <> "1" AndAlso hidPODestinationID.Value <> "-1" AndAlso txtPODestinationCode.Text.Trim() <> "") Then
                Dim SAPModel As String = objPODetail.ContractDetail.VechileColor.VechileType.SAPModel

                Dim podes As PODestination = New PODestinationFacade(User).Retrieve(CType(hidPODestinationID.Value, Integer))

                Dim criterialogistic As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "Status", MatchType.Exact, "A"))
                criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "RegionCode", MatchType.Exact, podes.RegionCode))
                criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "SAPModel", MatchType.Exact, SAPModel))
                criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "EffectiveDate", MatchType.LesserOrEqual, DateTime.Now))

                Dim sortColllog As SortCollection = New SortCollection
                sortColllog.Add(New Sort(GetType(LogisticPrice), "EffectiveDate", Sort.SortDirection.DESC))

                Dim logisticPrices As ArrayList = New LogisticPriceFacade(User).RetrieveByCriteria(criterialogistic, sortColllog)
                If logisticPrices.Count > 0 Then
                    Dim logisticPrice As LogisticPrice = logisticPrices(0)
                    e.Item.Cells(10).Text = FormatNumber(CType(TxtBox.Text, Double) * logisticPrice.TotalLogisticPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    objPODetail.LogisticCost = logisticPrice.TotalLogisticPrice
                Else : e.Item.Cells(10).Text = 0
                    objPODetail.LogisticCost = 0
                End If
            Else : e.Item.Cells(10).Text = 0
                objPODetail.LogisticCost = 0
            End If
            'end tambahan SLA

            Dim MaxTOPDayValue As Integer = 0
            Dim intFreedays As Integer

            Dim lblFreeDays As Label = e.Item.FindControl("lblFreeDays")
            Dim lblMaxTOPDays As Label = e.Item.FindControl("lblMaxTOPDays")
            lblFreeDays.Text = objPODetail.FreeDays
            lblMaxTOPDays.Text = objPODetail.MaxTOPDay
            objDealer = objPOHeader.Dealer

            Dim isTransControlPO As Boolean = CommonFunction.CheckTransactionControlPO(objDealer)

            If isTransControlPO Then
                intFreedays = CType(lblFreeDays.Text, Integer)
                MaxTOPDayValue = CType(lblMaxTOPDays.Text, Integer)
            Else
                For Each row As PKDetail In arrPKDtl
                    If row.VechileColor.ID = objPODetail.ContractDetail.VechileColor.ID Then
                        intFreedays = row.FreeDays
                        MaxTOPDayValue = row.MaxTOPDay
                    End If
                Next
            End If

            If objPriceArrayList.Count > 0 Then
                Dim objPrice As Price
                For Each item As Price In objPriceArrayList
                    If item.ValidFrom <= New DateTime(objPODetail.ContractDetail.ContractHeader.PricePeriodYear, objPODetail.ContractDetail.ContractHeader.PricePeriodMonth, objPODetail.ContractDetail.ContractHeader.PricePeriodDay) Then
                        objPrice = item
                        Exit For
                    End If
                Next
                Dim oTEOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))
                If oTEOP.PaymentType = enumPaymentType.PaymentType.TOP Then  'If ddlTermOfPayment.SelectedValue = enumPaymentType.PaymentType.TOP Then
                    If Me.chkFactoring.Checked Then
                        e.Item.Cells(9).Text = FormatNumber(CType(TxtBox.Text, Double) * freeIntIndicator * Calculation.CountInterest(nTOP, nMonth, objPrice.FactoringInt, objPODetail.ContractDetail.Amount - CType(e.Item.Cells(6).Text, Double), objPrice.PPh23), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                        '' CR Sirkular Rewards
                        '' by : ali Akbar
                        '' 2014-09-24
                        Dim _VehicleNettPrice As Double = 0
                        Dim _PPh22 As Double = 0
                        Dim _interest As Double = 0
                        _VehicleNettPrice = Calculation.CountRewardsVehiclePrice(objPODetail.ContractDetail, objPrice, nTOP, nMonth)
                        _PPh22 = Calculation.CountRewardPPh22(objPODetail.ContractDetail, objPrice, nTOP, nMonth)
                        _interest = Calculation.CountRewardsInterest(objPODetail.ContractDetail, objPrice, nTOP, nMonth)

                        'Harga
                        e.Item.Cells(7).Text = FormatNumber(CType(TxtBox.Text, Double) * (_VehicleNettPrice), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                        e.Item.Cells(8).Text = FormatNumber(CType(TxtBox.Text, Double) * (_PPh22) * CInt(objPOHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                        e.Item.Cells(9).Text = FormatNumber(CType(TxtBox.Text, Double) * freeIntIndicator * _interest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Else

                        'modified by FWA 20190216 --start--
                        'lblInterest.Text = FormatNumber(CType(txtBox.Text, Double) * freeIntIndicator * Calculation.CountInterest(nTOP, nMonth, objPrice.Interest, objContractDetail.Amount - ItemDeposit, objPrice.PPh23), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                        e.Item.Cells(9).Text = FormatNumber(CType(TxtBox.Text, Double) * freeIntIndicator * Calculation.CountInterest(intFreedays, nTOP, nMonth, objPrice.Interest, objPODetail.ContractDetail.Amount - CType(e.Item.Cells(6).Text, Double), objPrice.PPh23), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    End If
                Else
                    e.Item.Cells(9).Text = FormatNumber(CType(TxtBox.Text, Double) * 0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                End If
            Else
                e.Item.Cells(9).Text = FormatNumber(CType(TxtBox.Text, Double) * 0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            End If
            SubTotalInterest += CType(e.Item.Cells(9).Text, Double)
            SubTotalharga = SubTotalharga + CType(e.Item.Cells(7).Text, Double)
            SubTotalPPh = SubTotalPPh + CType(e.Item.Cells(8).Text, Double)
            If e.Item.Cells(10).Text <> "" Then
                SubTotalBiayaKirimPPN = SubTotalBiayaKirimPPN + CType(e.Item.Cells(10).Text, Double)
            End If
            'SubTotalSisa += CType(e.Item.Cells(4).Text, Double)
            'SubTotalOrder += CType(e.Item.Cells(5).Text, Double)
            lblFreeDays.Text = intFreedays
            lblMaxTOPDays.Text = MaxTOPDayValue

            Dim chkIsMDP As CheckBox = e.Item.FindControl("chkIsMDP")
            chkIsMDP.Enabled = False
            Dim PODraftCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            PODraftCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "POHeader.ID", MatchType.Exact, objPODetail.POHeader.ID))
            Dim arrPODraftHeader As ArrayList = New PODraftHeaderFacade(User).Retrieve(PODraftCriteria)

            Dim objPODraftHeader As PODraftHeader = New PODraftHeader
            If arrPODraftHeader.Count > 0 Then
                chkIsMDP.Checked = True
            Else
                chkIsMDP.Checked = False
            End If
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(3).Text = "Sub Total :"
            'e.Item.Cells(4).Text = FormatNumber(SubTotalSisa, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'e.Item.Cells(5).Text = FormatNumber(SubTotalOrder, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            e.Item.Cells(6).Text = FormatNumber(SubTotalDeposit, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(7).Text = FormatNumber(SubTotalharga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(8).Text = FormatNumber(SubTotalPPh, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(9).Text = FormatNumber(SubTotalInterest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(10).Text = FormatNumber(SubTotalBiayaKirimPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotal.Text = FormatNumber(SubTotalharga + SubTotalPPh + SubTotalInterest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotalBiayaKirim.Text = FormatNumber(SubTotalBiayaKirimPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            e.Item.Cells(3).Font.Bold = True
            e.Item.Cells(6).Font.Bold = True
            e.Item.Cells(7).Font.Bold = True
            e.Item.Cells(8).Font.Bold = True
            e.Item.Cells(9).Font.Bold = True
            e.Item.Cells(10).Font.Bold = True
        End If
    End Sub

    Private Sub btnHitung_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHitung.Click
        If Not Page.IsValid Then
            Return
        End If

        'Start  : Check to Available Ceiling
        If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling(True) Then
            MessageBox.Show("Total PO yang akan disimpan melebihi Ceiling yang tersedia")
        End If
        'End    : Check to Available Ceiling


        Dim isValidateSPL As Boolean = False
        If (POIsValid(isValidateSPL)) Then
            CountTotal()
        End If
    End Sub

    Private Function CountTotal()
        ViewState("Hitung") = True
        objPOHeader = sessionHelper.GetSession("PO")
        Dim Total As Double
        For Each item As DataGridItem In dtgDetail.Items
            Dim txtbox As TextBox = item.FindControl("TextBox1")
            arrOrder = sessionHelper.GetSession("Ord")
            arrOrder.Insert(item.ItemIndex, CInt(txtbox.Text))
            'item.Cells(6).Text = FormatNumber(CType(txtbox.Text, Double) * CType(objPOHeader.PODetails(item.ItemIndex).ContractDetail.Amount, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'item.Cells(7).Text = FormatNumber(CType(txtbox.Text, Double) * CType(objPOHeader.PODetails(item.ItemIndex).ContractDetail.PPh22, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Total = Total + ((CType(item.Cells(6).Text, Double) + CType(item.Cells(7).Text, Double)))
        Next
        sessionHelper.SetSession("Ord", arrOrder)
        sessionHelper.SetSession("PO", objPOHeader)

        If ValidatePOMaxQty() Then
            BindDetailToGrid()
            Return True
        End If

        Return False
        'lblTotal.Text = FormatNumber(Total.ToString, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    End Function

    Public Class CurrentPODetail
        Private _dealerID As Integer
        Private _modelID As Integer
        Private _qtyReq As Integer
        Private _qtyTxtBox As Integer
        Private _freeDays As Integer
        Private _maxTOPDay As Integer
        Private _seq As Integer

        Public Property DealerID() As Integer
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Integer)
                _dealerID = value
            End Set
        End Property
        Public Property ModelID() As Integer
            Get
                Return _modelID
            End Get
            Set(ByVal value As Integer)
                _modelID = value
            End Set
        End Property
        Public Property QtyReq() As Integer
            Get
                Return _qtyReq
            End Get
            Set(ByVal value As Integer)
                _qtyReq = value
            End Set
        End Property
        Public Property QtyTxtBox() As Integer
            Get
                Return _qtyTxtBox
            End Get
            Set(ByVal value As Integer)
                _qtyTxtBox = value
            End Set
        End Property
        Public Property FreeDays() As Integer
            Get
                Return _freeDays
            End Get
            Set(ByVal value As Integer)
                _freeDays = value
            End Set
        End Property
        Public Property MaxTOPDay() As Integer
            Get
                Return _maxTOPDay
            End Get
            Set(ByVal value As Integer)
                _maxTOPDay = value
            End Set
        End Property
        Public Property Sequence() As Integer
            Get
                Return _seq
            End Get
            Set(ByVal value As Integer)
                _seq = value
            End Set
        End Property
    End Class

    Private Function ValidatePOMaxQty() As Boolean
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        Dim errMsgList As StringBuilder = New StringBuilder
        Dim J% = 0

        '------------------->>> START insert Object Class from grid group by dealer id and model id
        Dim arrCurrentPODetail As ArrayList = New ArrayList
        Dim POTargetFac As New DealerPOTargetFacade(User)
        Dim intSelisih As Integer = 0
        For Each item As DataGridItem In dtgDetail.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim txtOrderBox As TextBox = item.FindControl("TextBox1")
                If txtOrderBox.Text.Trim = "" Then txtOrderBox.Text = 0

                Dim lblFreeDays As Label = item.FindControl("lblFreeDays")
                Dim lblMaxTOPDays As Label = item.FindControl("lblMaxTOPDays")

                Dim objPODFac As PODetailFacade = New PODetailFacade(User)
                Dim objPOD As PODetail = objPODFac.Retrieve(CType(item.Cells(0).Text, Integer))
                Dim strModeLID As Integer = objPOD.ContractDetail.VechileColor.VechileType.VechileModel.ID
                Dim strDealerID As Integer = objPOD.POHeader.Dealer.ID
                Dim intSeq As Integer = 0
                Dim objCurrentPODetail As New CurrentPODetail
                Dim blnIsModel As Boolean = False
                For Each obj As CurrentPODetail In arrCurrentPODetail
                    If strModeLID = obj.ModelID AndAlso strDealerID = obj.DealerID Then
                        obj.QtyReq += objPOD.ReqQty
                        obj.QtyTxtBox += CType(txtOrderBox.Text, Integer)
                        blnIsModel = True
                    End If
                Next
                If blnIsModel = False Then
                    objCurrentPODetail.DealerID = objPOD.POHeader.Dealer.ID
                    objCurrentPODetail.ModelID = strModeLID
                    objCurrentPODetail.QtyReq = objPOD.ReqQty
                    objCurrentPODetail.QtyTxtBox = txtOrderBox.Text
                    objCurrentPODetail.FreeDays = lblFreeDays.Text
                    objCurrentPODetail.MaxTOPDay = lblMaxTOPDays.Text

                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPOTarget), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
                    crit.opAnd(New Criteria(GetType(DealerPOTarget), "Dealer.ID", MatchType.Exact, objPOD.POHeader.Dealer.ID))
                    crit.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.Exact, strModeLID))
                    crit.opAnd(New Criteria(GetType(DealerPOTarget), "FreeDays", MatchType.Exact, lblFreeDays.Text))
                    crit.opAnd(New Criteria(GetType(DealerPOTarget), "MaxTOPDay", MatchType.Exact, lblMaxTOPDays.Text))
                    Dim arlPOTarget As ArrayList = POTargetFac.Retrieve(crit)
                    If Not IsNothing(arlPOTarget) AndAlso arlPOTarget.Count > 0 Then
                        Dim objPOTarget As DealerPOTarget = CType(arlPOTarget(0), DealerPOTarget)
                        If objPOTarget.IsDefault = 1 Then
                            intSeq = 9999
                        End If
                    End If
                    objCurrentPODetail.Sequence = intSeq

                    arrCurrentPODetail.Add(objCurrentPODetail)
                End If
            End If
        Next

        Dim arrCurrentPODetail2 As New List(Of CurrentPODetail)
        arrCurrentPODetail2 = (From obj As CurrentPODetail In arrCurrentPODetail
                                  Group By obj.DealerID, obj.ModelID, obj.FreeDays, obj.MaxTOPDay, obj.Sequence Into Group
                                  Select New CurrentPODetail With
                                  {.DealerID = DealerID,
                                   .ModelID = ModelID,
                                   .FreeDays = FreeDays,
                                   .MaxTOPDay = MaxTOPDay,
                                   .Sequence = Sequence,
                                   .QtyReq = Group.Sum(Function(r) r.QtyReq),
                                   .QtyTxtBox = Group.Sum(Function(r) r.QtyTxtBox)}
                               ).ToList()

        For Each objCurrentPODetail As CurrentPODetail In arrCurrentPODetail2
            Dim blnSkip As Boolean = False

            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPOTarget), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
            criteria.opAnd(New Criteria(GetType(DealerPOTarget), "Dealer.ID", MatchType.Exact, objCurrentPODetail.DealerID))
            criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.InSet, "(" & objCurrentPODetail.ModelID & ")"))
            Dim arlPOTarget As ArrayList = POTargetFac.Retrieve(criteria)

            Dim arlPOTarget2 As List(Of DealerPOTarget)
            arlPOTarget2 = (From obj As DealerPOTarget In arlPOTarget
                                Where (Format(obj.ValidFrom, "yyyyMM") = Format(Date.Now, "yyyyMM") And Format(obj.ValidTo, "yyyyMM") = Format(Date.Now, "yyyyMM"))
                              Order By obj.Dealer.ID, obj.VechileModel.ID, obj.Sequence
                              Select obj).ToList()

            Dim intSeqUpdate As Integer = 0
            Dim intSeqData As Integer = 0
            If objPOHeader.ReqAllocationDateTime <> Me.icPermintaanKirim.Value Then
                For Each objDPO As DealerPOTarget In arlPOTarget2
                    If (objDPO.ValidFrom <= icPermintaanKirim.Value And objDPO.ValidTo >= icPermintaanKirim.Value) Then
                        intSeqUpdate = objDPO.Sequence
                        Exit For
                    End If
                Next
                For Each objDPO As DealerPOTarget In arlPOTarget2
                    If (objDPO.ValidFrom <= objPOHeader.ReqAllocationDateTime And objDPO.ValidTo >= objPOHeader.ReqAllocationDateTime) Then
                        intSeqData = objDPO.Sequence
                        Exit For
                    End If
                Next
                If intSeqUpdate <> intSeqData Then
                    objCurrentPODetail.QtyReq = 0
                End If
            End If

            Dim i% = 0
            Dim txtQtyPOCurrent As Integer = 0
            Dim intCount As Integer = arlPOTarget2.Count
            Dim intModelDPO As Integer = 0
            Dim blnIsCurrentPeriod As Boolean = False
            Dim sisaBefore As Integer = 0
            Dim arrDealerPOTargetList3 As ArrayList = New ArrayList
            Dim Sisa As Decimal = 0, Terpakai As Decimal = 0
            For Each objDPO As DealerPOTarget In arlPOTarget2
                Terpakai = 0
                i += 1

                txtQtyPOCurrent = objCurrentPODetail.QtyTxtBox - objCurrentPODetail.QtyReq
                If txtQtyPOCurrent <= 0 Then txtQtyPOCurrent = 0

                'If objDPO.IsDefault = 0 Then
                Dim PDetailFac As New PODetailFacade(User)
                Dim criteriaPD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
                criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Dealer.DealerCode", MatchType.Exact, objDPO.Dealer.DealerCode))
                criteriaPD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & objDPO.VechileModel.ID & ")"))
                criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, objDPO.ValidFrom.Year & "-" & objDPO.ValidFrom.Month & "-01 00:00:00.000"))
                criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, objDPO.ValidTo.Year & "-" & objDPO.ValidTo.Month & "-" & DateTime.DaysInMonth(objDPO.ValidTo.Year, objDPO.ValidTo.Month) & " 00:00:00.000"))
                criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, "(0, 2, 4, 6, 8)"))
                criteriaPD.opAnd(New Criteria(GetType(PODetail), "FreeDays", MatchType.Exact, objDPO.FreeDays))
                Dim arlPoDetail As ArrayList = PDetailFac.Retrieve(criteriaPD)
                If Not IsNothing(arlPoDetail) AndAlso arlPoDetail.Count > 0 Then
                    For Each pDetail As PODetail In arlPoDetail
                        Select Case pDetail.POHeader.Status
                            Case 0
                                Terpakai -= pDetail.ReqQty
                            Case 2
                                If pDetail.AllocQty = 0 Then
                                    Terpakai -= pDetail.ReqQty
                                ElseIf pDetail.AllocQty > 0 Then
                                    Terpakai -= pDetail.AllocQty
                                End If
                            Case 4, 6, 8
                                Terpakai -= pDetail.AllocQty
                        End Select
                    Next
                End If

                objDPO.QtyUsed = Math.Abs(Terpakai)
                objDPO.QtySisa = (objDPO.MaxQuantity + sisaBefore) - Math.Abs(Terpakai)

                Dim dblQtySisa As Double = 0
                If Not IsNothing(objCurrentPODetail) Then
                    If CType(Format(Date.Now, "yyyyMMdd"), Integer) >= CType(Format(objDPO.ValidFrom, "yyyyMMdd"), Integer) AndAlso CType(Format(Date.Now, "yyyyMMdd"), Integer) <= CType(Format(objDPO.ValidTo, "yyyyMMdd"), Integer) Then
                        dblQtySisa = objDPO.QtySisa
                        If dblQtySisa > 0 AndAlso objCurrentPODetail.Sequence > objDPO.Sequence Then
                            txtQtyPOCurrent = objCurrentPODetail.QtyTxtBox
                        End If
                        If (objDPO.QtySisa + objCurrentPODetail.QtyReq) > 0 OrElse i = intCount Then
                            If txtQtyPOCurrent > dblQtySisa Then
                                If (objDPO.ValidFrom <= icPermintaanKirim.Value And objDPO.ValidTo >= icPermintaanKirim.Value) Then
                                    If (objDPO.QtySisa + objCurrentPODetail.QtyReq) > 0 Then
                                        If objDPO.FreeDays = objCurrentPODetail.FreeDays AndAlso objDPO.MaxTOPDay = objCurrentPODetail.MaxTOPDay Then
                                            J += 1
                                            errMsgList.Append(J & ". Sisa quantity untuk kendaraan " & objDPO.VechileModel.IndDescription & " pada periode " & objDPO.Sequence & " tidak mencukupi.\nPenambahan quantity maksimum sebanyak " & dblQtySisa & " Unit.\n\n")
                                            Exit For
                                        Else
                                            blnSkip = True
                                        End If
                                    Else
                                        blnSkip = True
                                    End If
                                Else
                                    If blnSkip And (objDPO.QtySisa + objCurrentPODetail.QtyReq) > 0 Then
                                        'If objDPO.FreeDays = objCurrentPODetail.FreeDays AndAlso objDPO.MaxTOPDay = objCurrentPODetail.MaxTOPDay Then
                                        J += 1
                                        errMsgList.Append(J & ". Sisa quantity untuk kendaraan " & objDPO.VechileModel.IndDescription & " pada periode " & objDPO.Sequence & " tidak mencukupi.\nPenambahan quantity maksimum sebanyak " & dblQtySisa & " Unit.\n\n")
                                        Exit For
                                        'End If
                                    Else
                                        J += 1
                                        errMsgList.Append(J & ". Sisa quantity untuk kendaraan " & objDPO.VechileModel.IndDescription & " pada periode " & objDPO.Sequence & " tidak mencukupi.\nPenambahan quantity maksimum sebanyak " & dblQtySisa & " Unit.\n\n")
                                        Exit For
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                If objDPO.IsDefault = 0 Then
                    If blnIsCurrentPeriod = False Then
                        If CType(Format(Date.Now, "yyyyMMdd"), Integer) >= CType(Format(objDPO.ValidFrom, "yyyyMMdd"), Integer) AndAlso CType(Format(Date.Now, "yyyyMMdd"), Integer) <= CType(Format(objDPO.ValidTo, "yyyyMMdd"), Integer) Then
                            sisaBefore = 0
                            blnIsCurrentPeriod = True
                        Else
                            sisaBefore = objDPO.QtySisa
                        End If
                    Else
                        sisaBefore = 0
                    End If
                Else
                    sisaBefore = 0
                End If
                'End If
                arrDealerPOTargetList3.Add(objDPO)
            Next
        Next

        If errMsgList.Length > 0 Then
            MessageBox.Show(errMsgList.ToString)
            Return False
        Else
            Return True
        End If
        '--------------------->>> ENDING
    End Function

    Private Function IsEnabledCreditControl(ByVal objDealer As Dealer) As Boolean
        Dim _poHeaderFacade As POHeaderFacade = New POHeaderFacade(User)
        If _poHeaderFacade.IsEnabledCreditControl(objDealer) Then
            Return True
        End If
        Return False
    End Function
    Private Function ValidPOdest(ByVal objPOHed As POHeader) As Boolean

        If Not IsNothing(objPOHed.PODestination) AndAlso objPOHed.PODestination.ID > 1 Then

            For Each pod As PODetail In objPOHed.PODetails
                If pod.LogisticCost = 0 Then
                    Return False
                End If
            Next
        End If

        Return True
    End Function

    Private Sub btnKirim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKirim.Click

    End Sub

    Private Sub btnKirim2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKirim2.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim blnIsLoop As Boolean = False
        Dim PengirimanMsg As String = String.Empty

        'Start  : Remaining Module - Check to Available Ceiling
        'If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling(True) Then
        '    MessageBox.Show("Total PO yang akan disimpan melebihi Ceiling yang tersedia")
        '    Exit Sub
        'End If
        'End    : Remaining Module - Check to Available Ceiling


        'Start  : Add By WDI 20161209
        If rdoByKTB.Checked AndAlso hidPODestinationID.Value = "" Then
            MessageBox.Show("Pengiriman oleh MMKSI, namun PO Destination belum dipilih.")
            Exit Sub
        End If
        'End    : Add By WDI 20161209

        'Firman : Add validation because even the label value is correct the negative ceiling still occured
        'donas : 20130614:temporary removed; all validation will be handled in commonfunction
        'we will open it later, when negative ceiling occurs (known by donas;yurike;ferdinan)
        'Dim oTEOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
        'If CDec(lblF1.Text.Trim) < CDec(lblTotal.Text.Trim) AndAlso oTEOP.PaymentType <> CType(enumPaymentType.PaymentType.RTGS, Short) Then
        '    MessageBox.Show("Total PO yang akan disimpan melebihi Ceiling yang tersedia.")
        '    Exit Sub
        'End If
        'end temporary removed

        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        Dim isValidateSPL As Boolean = False
        If Not CountTotal() Then Return
        If (POIsValid(isValidateSPL)) Then
            If (PODetailIsValid()) Then
LoopSave:
                BindToPOHeaderObject()

                'validasi tambahan block COD
                Dim strMsg As String = ""
                If Not POHeaderFacade.IsCODValid(objPOHeader, strMsg) Then
                    MessageBox.Show(strMsg)
                    Exit Sub
                End If

                If POFunction.ValidatePengiriman(objPOHeader.PODetails, PengirimanMsg, rdoByKTB.Checked) = False Then
                    MessageBox.Show(PengirimanMsg)
                    Exit Sub
                End If

                'validasi tambahan Max TOP Days
                If Not isValidateSPL Then
                    Dim MaxTOPDay As String = String.Empty
                    Dim isTransControlPO As Boolean = CommonFunction.CheckTransactionControlPO(objDealer)
                    Dim isValidTOP As Boolean = True
                    If isTransControlPO Then
                        isValidTOP = CommonFunction.ValidateMaxTOPDaysPO(objPOHeader, MaxTOPDay, ddlTermOfPayment.SelectedValue)
                    Else
                        isValidTOP = CommonFunction.ValidateMaxTOPDaysPK(objPOHeader, MaxTOPDay, ddlTermOfPayment.SelectedValue)
                    End If
                    If Not isValidTOP Then
                        MessageBox.Show("Maximum TOP yang anda pilih melebihi " & MaxTOPDay)
                        Exit Sub
                    End If
                End If
                'validasi tambahan block COD

                If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling(True) Then
                    MessageBox.Show("Total PO yang akan disimpan melebihi Ceiling yang tersedia")
                    Exit Sub
                End If

                Dim objPOHeaderFacade As New POHeaderFacade(User)

                IsTransferChecking(objPOHeader)

                If InvalidTransferDate(objPOHeader) Then
                    MessageBox.Show("Tanggal Jatuh Tempo melebihi Validasi Ceiling")
                    Exit Sub
                End If

                If Not ValidPOdest(objPOHeader) Then
                    MessageBox.Show("PO Tidak bisa Disimpan karena Biaya Kirim = 0")
                    Exit Sub
                End If

                'Start  : RemainModule Update Status
                If objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeCOD Or objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeRTGS Or objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeTOP Then
                    Dim IsValidChanged As Boolean = False
                    If objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeCOD And objPOHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.COD Then
                        IsValidChanged = True
                    ElseIf objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeRTGS And objPOHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then
                        IsValidChanged = True
                    ElseIf objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeTOP And objPOHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
                        IsValidChanged = True
                    End If
                    If IsValidChanged Then
                        objPOHeader.RemarkStatus = 0 'Nothing
                        objPOHeader.BlockedStatus = 0 'Nothing
                        objPOHeader.Status = 4 'Rilis= LookUp.ArrayStatusPO 'AutoEscalate From Baru To Rilis
                    End If
                End If
                'End    : RemainModule Update Status

                Dim oPOHOri As POHeader = objPOHeaderFacade.Retrieve(objPOHeader.ID)
                objPOHeaderFacade.Update(objPOHeader)

                'Remark karna sudah di execute di BindDetailToGrid()
                'SetFreeDays(objPOHeader)

                If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling(True) Then
                    Dim poF As POHeaderFacade = New POHeaderFacade(User)
                    Dim XXX = poF.Update(oPOHOri)
                    'MessageBox.Show("Update PO gagal, silahkan ulangi beberapa saat lagi")
                    MessageBox.Show("Update PO gagal, Total PO yang akan disimpan melebihi Ceiling yang tersedia")
                    Exit Sub
                End If
                If blnIsLoop = True Then
                    MessageBox.Show(SR.UpdateSucces)
                End If
                BindHeaderToForm()
                BindDetailToGrid()
                If blnIsLoop = False Then
                    blnIsLoop = True
                    GoTo LoopSave
                End If
                btnKirim.Enabled = True
                Exit Sub
            Else
                MessageBox.Show("Sisa O/C Berubah, Order Melebihi Sisa O/C")
            End If
        End If
        BindDetailToGrid()
        btnKirim.Enabled = True

        ''HACK soalnya kalo simpan 1x ga bener datanya
        'If ViewState("Save") Then
        '    ViewState.Remove("Save")
        '    Exit Sub
        'Else
        '    ViewState("Save") = True
        '    btnKirim_Click(Nothing, Nothing)
        'End If
    End Sub

    Private Function PODetailIsValid() As Boolean
        objPOHeader = sessionHelper.GetSession("PO")
        For Each item As DataGridItem In dtgDetail.Items
            Dim txtBox As TextBox = item.FindControl("TextBox1")
            If (CInt(txtBox.Text) > (CType(objPOHeader.PODetails(item.ItemIndex), PODetail).ContractDetail.SisaUnit) + CType(objPOHeader.PODetails(item.ItemIndex), PODetail).ReqQty) Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        objPOHeader = sessionHelper.GetSession("PO")
        If Not (objPOHeader Is Nothing) Then
            'Dim objPODetailFacade As New PODetailFacade(User)
            'For Each item As PODetail In objPOHeader.PODetails
            '    item.RowStatus = DBRowStatus.Deleted
            'Next
            Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                Dim isMDPCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                isMDPCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "POHeader.ID", MatchType.Exact, objPOHeader.ID))

                Dim arrPODraftHeaderStock As ArrayList = New PODraftHeaderFacade(User).Retrieve(isMDPCriteria)
                If arrPODraftHeaderStock.Count > 0 Then
                    MessageBox.Show("Proses Batal PO MDP silahkan melakukan pengajuan ke MKS")
                    Exit Sub
                End If
            End If
            objPOHeader.Status = enumStatusPO.Status.Batal
            Dim objPOHeaderFacade As New POHeaderFacade(User)
            objPOHeaderFacade.Update(objPOHeader)
            sessionHelper.RemoveSession("PO")
            If (Request.QueryString("src") = "create") Then
                Response.Redirect("../PO/OpenDailyPO.aspx")
            Else
                Response.Redirect("../PO/ConfirmDailyPO.aspx")
            End If
        End If
    End Sub

    Private Function IsCreatePOWithTOPValid() As Boolean
        Dim _connectionString As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionString")
        Dim orgPengajuan As Integer = BindToPOHeaderObject()
        Dim oldPO As POHeader = New POHeader(objPOHeader.ID)
        If oldPO.Status = "" Then
            oldPO.Status = "0"
        End If
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        Dim objTOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
        If objTOP.TermOfPaymentValue = 0 Then
            Return True
        Else
            Dim ACC As Integer = New POHeaderFacade(User).CountACC(objPOHeader.ContractHeader, objDealer, objTOP, objPOHeader, _connectionString)
            Dim totalPengajuan As Integer = objPOHeader.TotalHarga

            For Each item As PODetail In objPOHeader.PODetails
                totalPengajuan += CType(item.ReqQty, Double) * CType(item.ContractDetail.Amount, Double)
            Next
            If totalPengajuan > (ACC - oldPO.TotalHarga) Then
                Return False
            Else
                Return True
            End If
        End If
    End Function

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
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

    Private Sub SetFreeDays(poHeader As POHeader)
        'poHeader = New POHeaderFacade(User).Retrieve(687730)
        'Dim dt As Date = DateSerial(icPermintaanKirim.Value.Year, icPermintaanKirim.Value.Month, icPermintaanKirim.Value.Day)
        Dim dt As Date = icPermintaanKirim.Value
        Dim warning As String = ""
        Dim MaxTop As Integer = 0
        sessionHelper.SetSession("EditPO", sums)
        'Dim freeDays As Integer = SetFreeDays(objDealer, poHeader.PODetails, dt, dt, MaxTop, warning)
        Dim freeDays As Integer = 0
        For Each _Detail As PODetail In poHeader.PODetails
            freeDays = SetFreeDays(objDealer, poHeader.PODetails, dt, dt, MaxTop, warning, _Detail)
            '_Detail.FreeDays = CommonFunction.SetFreeDays2(objDealer, poHeader.PODetails, dt, dt, warning)
            _Detail.FreeDays = freeDays
            _Detail.MaxTOPDay = MaxTop
            Dim PDFacade As New PODetailFacade(User)
            PDFacade.Update(_Detail)
        Next
        sessionHelper.RemoveSession("EditPO")
    End Sub

    Dim sums As Integer = 0
    Private Sub HitungSetFreeDays(poHeader As POHeader, ByRef _MaxTOP As Integer, ByRef _FreeDays As Integer, poDetail As PODetail, QtyOrderBox As Integer)
        'Dim dt As Date = DateSerial(icPermintaanKirim.Value.Year, icPermintaanKirim.Value.Month, icPermintaanKirim.Value.Day)
        Dim dt As Date = icPermintaanKirim.Value
        Dim warning As String = ""
        sessionHelper.SetSession("EditPO", sums)
        objDealer = sessionHelper.GetSession("DEALER")

        Dim POTargetFac As New DealerPOTargetFacade(User)
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPOTarget), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "Dealer.ID", MatchType.Exact, objDealer.ID))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.Exact, CType(poDetail.ContractDetail.VechileColor.VechileType.VechileModel.ID, Short)))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidFrom", MatchType.GreaterOrEqual, dt.Year & "-" & dt.Month & "-01 00:00:00.000"))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidTo", MatchType.LesserOrEqual, dt.Year & "-" & dt.Month & "-" & DateTime.DaysInMonth(dt.Year, dt.Month) & " 00:00:00.000"))
        Dim arlPOTarget As ArrayList = POTargetFac.Retrieve(criteria)
        If Not IsNothing(arlPOTarget) AndAlso arlPOTarget.Count > 0 Then
            _FreeDays = SetFreeDays(objDealer, poHeader.PODetails, dt, dt, _MaxTOP, warning, poDetail, QtyOrderBox)
        End If

        sessionHelper.RemoveSession("EditPO")
    End Sub

    Public Function SetFreeDays(Dealer As Dealer, PoDetails As ArrayList, ValidFrom As Date, ValidTo As Date, ByRef VarMaxTOP As Integer, ByRef LastPeriodeRemain As String, Optional _poDetail As PODetail = Nothing, Optional QtyOrderBox As Integer = 0) As Integer
        For Each d As PODetail In PoDetails
            If Not chkFactoring.Checked Then
                Exit For
            Else
                VarMaxTOP = 0
                LastPeriodeRemain = ""
                Return 0
            End If
        Next
        'Dim POHeader As New POHeader
        Dim sessHelp As SessionHelper = New SessionHelper
        Dim recAllocDateTime As Date = Date.MinValue
        Dim POTargetFac As New DealerPOTargetFacade(User)
        Dim modelID As String = ""
        Dim detaiD As New ArrayList
        For Each podetail As PODetail In PoDetails
            If modelID.Length = 0 Then
                modelID = podetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
            Else
                modelID = modelID & "," & podetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
            End If
            'POHeader = podetail.POHeader
            recAllocDateTime = podetail.POHeader.ReqAllocationDateTime
            detaiD.Add(podetail.ID)
        Next
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPOTarget), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "Dealer.ID", MatchType.Exact, Dealer.ID))
        'criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.Exact, CType(PODetail.ContractDetail.VechileColor.VechileType.VechileModel.ID, Short)))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidFrom", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidTo", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerPOTarget), "ValidTo", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
        Dim arlPOTarget As ArrayList = POTargetFac.Retrieve(criteria, sortColl)

        'Dim arlPoDetail As ArrayList
        Dim PDetailFac As New PODetailFacade(User)
        Dim criteriaPD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Dealer.ID", MatchType.Exact, Dealer.ID))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, ("(0, 2, 4, 6, 8)")))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.IsFactoring", MatchType.Exact, 0))
        Dim arlPoDetail As ArrayList = PDetailFac.Retrieve(criteriaPD)

        Dim AllocRemain As Integer = 0
        Dim ExpiredPeriode As Boolean = False
        Dim OverQuantity As Boolean = False
        Dim CurrentQuantity As Integer = 0
        Dim arlPeriodeRemain As New ArrayList
        Dim dFDays As New Dictionary(Of Integer, Integer)
        Dim dFDaysTarget As New Dictionary(Of Integer, Integer)
        Dim _return As Integer = 0
        Dim strModelID As String = String.Empty

        For Each pDetail As PODetail In arlPoDetail
            If pDetail.ContractDetail.VechileColor.VechileType.VechileModel.ID = _poDetail.ContractDetail.VechileColor.VechileType.VechileModel.ID Then

                strModelID = _poDetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPOTarget), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
                crit.opAnd(New Criteria(GetType(DealerPOTarget), "Dealer.ID", MatchType.Exact, objDealer.ID))
                crit.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.InSet, "(" & strModelID & ")"))
                Dim arlPOTrg As ArrayList = POTargetFac.Retrieve(crit)

                Dim arlPOTrg2 As List(Of DealerPOTarget)
                arlPOTrg2 = (From obj As DealerPOTarget In arlPOTrg
                                    Where (Format(obj.ValidFrom, "yyyyMM") = Format(Date.Now, "yyyyMM") And Format(obj.ValidTo, "yyyyMM") = Format(Date.Now, "yyyyMM"))
                                  Order By obj.Dealer.ID, obj.VechileModel.ID, obj.Sequence
                                  Select obj).ToList()

                Dim intSeqUpdate As Integer = 0
                Dim intSeqData As Integer = 0
                If objPOHeader.ReqAllocationDateTime <> Me.icPermintaanKirim.Value Then
                    For Each objDPO As DealerPOTarget In arlPOTrg2
                        If (objDPO.ValidFrom <= icPermintaanKirim.Value And objDPO.ValidTo >= icPermintaanKirim.Value) Then
                            intSeqUpdate = objDPO.Sequence
                            Exit For
                        End If
                    Next
                    For Each objDPO As DealerPOTarget In arlPOTrg2
                        If (objDPO.ValidFrom <= objPOHeader.ReqAllocationDateTime And objDPO.ValidTo >= objPOHeader.ReqAllocationDateTime) Then
                            intSeqData = objDPO.Sequence
                            Exit For
                        End If
                    Next
                End If

                If Not IsNothing(sessHelp.GetSession("EditPO")) Then
                    If detaiD.Contains(pDetail.ID) Then
                        pDetail.FreeDays = 0
                        recAllocDateTime = ValidFrom
                    End If
                End If

                If Not dFDays.ContainsKey(pDetail.FreeDays) Then
                    dFDays.Add(pDetail.FreeDays, 0)
                End If

                Select Case pDetail.POHeader.Status
                    Case 0
                        dFDays(pDetail.FreeDays) += pDetail.ReqQty
                    Case 2
                        If pDetail.AllocQty = 0 Then
                            dFDays(pDetail.FreeDays) += pDetail.ReqQty
                        ElseIf pDetail.AllocQty > 0 Then
                            dFDays(pDetail.FreeDays) += pDetail.AllocQty
                        End If
                    Case 4, 6, 8
                        dFDays(pDetail.FreeDays) += pDetail.AllocQty
                End Select

                If intSeqUpdate <> intSeqData Then
                    If dFDays.ContainsKey(0) Then
                        dFDays(pDetail.FreeDays) = QtyOrderBox
                    End If
                End If

            End If
        Next

        'If Not IsNothing(sessHelp.GetSession("EditPO")) Then
        '    dFDays(0) = CType(sessHelp.GetSession("EditPO"), Integer)
        'End If

        Try
            Dim freeDays As ArrayList = POTargetFac.RetrieveDefaultFreeDays(Dealer, _poDetail.ContractDetail.VechileColor.VechileType.VechileModel.ID) '3 Row
            If freeDays.Count > 0 Then
                For Each DPT As DealerPOTarget In freeDays
                    '_return = CType(freeDays(0), DealerPOTarget).FreeDays
                    'VarMaxTOP = CType(freeDays(0), DealerPOTarget).MaxTOPDay
                    If recAllocDateTime <= DPT.ValidTo Then
                        _return = DPT.FreeDays
                        VarMaxTOP = DPT.MaxTOPDay
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
        End Try

        Dim arlPOTarget2
        If Not IsNothing(_poDetail) > 0 Then
            arlPOTarget2 = (From obj As DealerPOTarget In arlPOTarget
                                Where obj.VechileModel.ID = _poDetail.ContractDetail.VechileColor.VechileType.VechileModel.ID AndAlso Dealer.ID = obj.Dealer.ID
                                   Order By obj.Dealer.ID, obj.VechileModel.ID, obj.Sequence
                                   Select obj).ToList()
        Else
            arlPOTarget2 = arlPOTarget
        End If

        Dim carryOver As Integer = 0
        For Each dPOT As DealerPOTarget In arlPOTarget2
            If Not dFDaysTarget.ContainsKey(dPOT.FreeDays) Then
                dFDaysTarget.Add(dPOT.FreeDays, dPOT.MaxQuantity)
            End If

            If carryOver > 0 Then
                dFDaysTarget(dPOT.FreeDays) += carryOver
            End If

            If dFDays.ContainsKey(dPOT.FreeDays) Then
                'If (dFDaysTarget(dPOT.FreeDays) - dFDays(dPOT.FreeDays)) >= 0 Then
                '    dFDaysTarget(dPOT.FreeDays) -= dFDays(dPOT.FreeDays)
                '    dFDays.Remove(dPOT.FreeDays)
                'End If
                dFDaysTarget(dPOT.FreeDays) -= dFDays(dPOT.FreeDays)
                dFDays.Remove(dPOT.FreeDays)
                AllocRemain += dFDaysTarget(dPOT.FreeDays)

                If AllocRemain > 0 AndAlso _poDetail.FreeDays <> dPOT.FreeDays AndAlso _poDetail.MaxTOPDay <> dPOT.MaxTOPDay Then
                    If dFDays.ContainsKey(0) Then
                        dFDays(0) = QtyOrderBox
                    End If
                End If

                'If Date.Now > dPOT.ValidTo Then
                '    dFDaysTarget(dPOT.FreeDays) = 0
                'End If
                If LastPeriodeRemain.Length = 0 Then
                    LastPeriodeRemain = "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                Else
                    LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                End If
            End If
            carryOver = 0
            If recAllocDateTime.Date <= dPOT.ValidTo Then
                ExpiredPeriode = False
            ElseIf recAllocDateTime.Date > dPOT.ValidTo Then
                ExpiredPeriode = True
                If Date.Now.Date > dPOT.ValidTo Then
                    carryOver += dFDaysTarget(dPOT.FreeDays)
                    dFDaysTarget.Remove(dPOT.FreeDays)
                End If
            End If

            If dFDays.ContainsKey(0) Then
                If ExpiredPeriode Then
                    Continue For
                End If
                If AllocRemain >= 0 Then
                    If OverQuantity AndAlso (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
                        _return = dPOT.FreeDays
                        VarMaxTOP = dPOT.MaxTOPDay
                        dFDaysTarget(dPOT.FreeDays) -= dFDays(0)
                        dFDays.Remove(0)
                        Continue For
                    ElseIf (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
                        _return = dPOT.FreeDays
                        VarMaxTOP = dPOT.MaxTOPDay
                        dFDaysTarget(dPOT.FreeDays) -= dFDays(0)
                        dFDays.Remove(0)
                        OverQuantity = False
                    ElseIf (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) < 0 Then
                        OverQuantity = True
                        dFDaysTarget.Remove(dPOT.FreeDays)
                        Continue For
                    Else
                        Continue For
                    End If
                End If
                If LastPeriodeRemain.Length = 0 Then
                    LastPeriodeRemain = "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                Else
                    LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                End If
            End If
        Next

        Return _return
    End Function

    Public Function SetFreeDays(Dealer As Dealer, PoDetails As ArrayList, recAllocDateTime As Date, ValidFrom As Date, ValidTo As Date, ByRef VarMaxTOP As Integer, ByRef LastPeriodeRemain As String) As Integer
        For Each d As PODetail In PoDetails
            If Not chkFactoring.Checked Then
                Exit For
            Else
                VarMaxTOP = 0
                LastPeriodeRemain = ""
                Return 0
            End If
        Next
        Dim sessHelp As SessionHelper = New SessionHelper
        Dim POTargetFac As New DealerPOTargetFacade(User)
        Dim modelID As String = ""
        Dim detaiD As New ArrayList
        Dim _return As Integer = 0
        For Each podetail As PODetail In PoDetails
            If modelID.Length = 0 Then
                modelID = podetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
            Else
                modelID = modelID & "," & podetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
            End If
            recAllocDateTime = ValidFrom
            detaiD.Add(podetail.ID)
        Next

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPOTarget), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "Dealer.ID", MatchType.Exact, Dealer.ID))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidFrom", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidTo", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
        'criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.Exact, PODetail.ContractDetail.VechileColor.VechileType.VechileModel.ID))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerPOTarget), "ValidTo", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
        Dim arlPOTarget As ArrayList = POTargetFac.Retrieve(criteria, sortColl)
        Dim arlPoDetail As New ArrayList
        If Not sessHelp.GetSession("Itung") Then
            arlPoDetail = PoDetails
        Else
            Dim PDetailFac As New PODetailFacade(User)
            Dim criteriaPD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
            criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Dealer.ID", MatchType.Exact, Dealer.ID))
            criteriaPD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
            'criteriaPD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.Exact, PODetail.ContractDetail.VechileColor.VechileType.VechileModel.ID))
            criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
            criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
            criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, ("(0, 2, 4, 6, 8)")))
            criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.IsFactoring", MatchType.Exact, 0))
            arlPoDetail = PDetailFac.Retrieve(criteriaPD)
        End If

        Dim AllocRemain As Integer = 0
        Dim ExpiredPeriode As Boolean = False
        Dim OverQuantity As Boolean = False
        Dim CurrentQuantity As Integer = 0
        Dim arlPeriodeRemain As New ArrayList
        Dim dFDays As New Dictionary(Of Integer, Integer)
        Dim dFDaysTarget As New Dictionary(Of Integer, Integer)

        For Each pDetail As PODetail In arlPoDetail
            If pDetail.POHeader.IsFactoring <> 0 Then
                Continue For
            End If

            If Not IsNothing(sessHelp.GetSession("EditPO")) OrElse sessHelp.GetSession("EAlloc") Then
                If detaiD.Contains(pDetail.ID) Then
                    pDetail.FreeDays = 0
                    recAllocDateTime = ValidFrom
                    If sessHelp.GetSession("EAlloc") AndAlso sessHelp.GetSession("Itung") Then
                        For Each _d As PODetail In PoDetails
                            If pDetail.AllocQty <> _d.AllocQty AndAlso pDetail.ID = _d.ID Then
                                pDetail.AllocQty = _d.AllocQty
                            End If
                        Next
                    End If
                End If
            End If

            'If sessHelp.GetSession("EAlloc") AndAlso detaiD.Contains(pDetail.ID) Then
            '    pDetail.FreeDays = 0
            '    recAllocDateTime = ValidFrom
            'End If

            If Not dFDays.ContainsKey(pDetail.FreeDays) Then
                dFDays.Add(pDetail.FreeDays, 0)
            End If
            If sessHelp.GetSession("Itung") OrElse sessHelp.GetSession("EAlloc") Then
                Select Case pDetail.POHeader.Status
                    Case 0
                        dFDays(pDetail.FreeDays) += pDetail.ReqQty
                    Case 2
                        If pDetail.AllocQty = 0 Then
                            dFDays(pDetail.FreeDays) += pDetail.ReqQty
                        ElseIf pDetail.AllocQty > 0 Then
                            dFDays(pDetail.FreeDays) += pDetail.AllocQty
                        End If
                    Case 4, 6, 8
                        dFDays(pDetail.FreeDays) += pDetail.AllocQty
                End Select
            Else
                dFDays(pDetail.FreeDays) += pDetail.ReqQty
            End If
        Next

        If sessHelp.GetSession("Itung") AndAlso Not sessHelp.GetSession("EAlloc") Then
            If Not dFDays.ContainsKey(0) Then
                dFDays.Add(0, 0)
            End If
            For Each PoDe As PODetail In PoDetails
                dFDays(0) += PoDe.ReqQty
            Next
        End If

        If Not IsNothing(sessHelp.GetSession("EditPO")) Then
            dFDays(0) = CType(sessHelp.GetSession("EditPO"), Integer)
        End If

        Try
            Dim freeDays As ArrayList = POTargetFac.RetrieveDefaultFreeDays(Dealer, modelID)
            If freeDays.Count > 0 Then
                _return = CType(freeDays(0), DealerPOTarget).FreeDays
                VarMaxTOP = CType(freeDays(0), DealerPOTarget).MaxTOPDay
            End If
        Catch ex As Exception
        End Try

        Dim carryOver As Integer = 0
        For Each dPOT As DealerPOTarget In arlPOTarget
            If Not dFDaysTarget.ContainsKey(dPOT.FreeDays) Then
                dFDaysTarget.Add(dPOT.FreeDays, dPOT.MaxQuantity)
            End If

            If carryOver > 0 Then
                dFDaysTarget(dPOT.FreeDays) += carryOver
            End If

            If dFDays.ContainsKey(dPOT.FreeDays) Then
                'If (dFDaysTarget(dPOT.FreeDays) - dFDays(dPOT.FreeDays)) >= 0 Then
                '    dFDaysTarget(dPOT.FreeDays) -= dFDays(dPOT.FreeDays)
                '    dFDays.Remove(dPOT.FreeDays)
                'Else
                '    OverQuantity = True
                '    dFDaysTarget(dPOT.FreeDays) = 0
                '    dFDays.Remove(dPOT.FreeDays)
                'End If
                dFDaysTarget(dPOT.FreeDays) -= dFDays(dPOT.FreeDays)
                dFDays.Remove(dPOT.FreeDays)
                AllocRemain += dFDaysTarget(dPOT.FreeDays)
                'If Date.Now.Date > dPOT.ValidTo Then
                '    dFDaysTarget(dPOT.FreeDays) = 0
                'End If
                If LastPeriodeRemain.Length = 0 Then
                    LastPeriodeRemain = "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                Else
                    LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                End If
            End If
            carryOver = 0
            If recAllocDateTime.Date <= dPOT.ValidTo Then
                ExpiredPeriode = False
            ElseIf recAllocDateTime.Date > dPOT.ValidTo Then
                ExpiredPeriode = True
                If Date.Now.Date > dPOT.ValidTo Then
                    carryOver += dFDaysTarget(dPOT.FreeDays)
                    dFDaysTarget.Remove(dPOT.FreeDays)
                End If
            End If

            If dFDays.ContainsKey(0) Then
                If ExpiredPeriode Then
                    Continue For
                End If

                If AllocRemain >= 0 Then
                    If OverQuantity AndAlso (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
                        _return = dPOT.FreeDays
                        VarMaxTOP = dPOT.MaxTOPDay
                        dFDaysTarget(dPOT.FreeDays) -= dFDays(0)
                        dFDays.Remove(0)
                        Continue For
                    ElseIf (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
                        _return = dPOT.FreeDays
                        VarMaxTOP = dPOT.MaxTOPDay
                        dFDaysTarget(dPOT.FreeDays) -= dFDays(0)
                        dFDays.Remove(0)
                        OverQuantity = False
                    ElseIf (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) < 0 Then
                        OverQuantity = True
                        Continue For
                    Else
                        Continue For
                    End If
                Else
                    OverQuantity = True
                    dFDaysTarget.Remove(dPOT.FreeDays)
                    Continue For
                End If

                If LastPeriodeRemain.Length = 0 Then
                    LastPeriodeRemain = "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                Else
                    LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                End If
            End If
        Next

        Return _return
    End Function
#End Region

End Class