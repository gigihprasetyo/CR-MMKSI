#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.pk
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
#End Region

Public Class EditPOLoad
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Timer1 As System.Web.UI.Timer
    Protected WithEvents ScriptManager1 As System.Web.UI.ScriptManager
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
    Protected WithEvents icPermintaanKirim As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
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
    Private arrOrder As New ArrayList
    Private nTOP As Integer
    Private nMonth As Integer
    Private objSPL As SPL

    Private objNHFac As NationalHolidayFacade = New NationalHolidayFacade(User)
    Private objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)

#End Region

#Region "Custom Method"

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
                If dtJatuhTempo > objSCM.MaxTOPDate Then
                    MessageBox.Show("Jatuh Tempo PO Melebihi Tanggal Validitas Ceiling")
                    Return False
                End If
            End If

            IsLesser = CommonFunction.IsCeilingEnough(Me.GetProductCategory(), objPOH.ID, Me.icPermintaanKirim.Value, TotalPO, objD.CreditAccount, oTEOP.PaymentType, IsAfterSaving, Ceiling, Proposed, Liquified, Outstanding, TodaysAvCeiling, TomorrowAvCeiling, AvCeiling)
            'Me.lblF1.Text = "Today = " & FormatNumber(TodaysAvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Me.lblF1.Text &= " ; Tomorrow = " & FormatNumber(TomorrowAvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            If TodaysAvCeiling > TomorrowAvCeiling Then
                Me.lblF1.Text = FormatNumber(TomorrowAvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Else
                Me.lblF1.Text = FormatNumber(TodaysAvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
            Me.lblF2.Text = FormatNumber(TotalPO, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.lblF3.Text = FormatNumber(objPOH.TotalHarga(), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

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
                        Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetValue("PODateAllowed") 'yyyyMMdd - 20141031
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
                        Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetValue("PODateAllowed") 'yyyyMMdd - 20141031
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

    Private Function POIsValid() As Boolean
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
        Else
            If objPKHead.MaxTopIndicator = 0 Then
                MaxTOPValue = objPKHead.MaxTOPDate.Subtract(icPermintaanKirim.Value).Days
            ElseIf objPKHead.MaxTopIndicator = 1 Then
                MaxTOPValue = objPKHead.MaxTopDay
            End If
        End If

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
        Return True
    End Function

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
        lblTotal.Text = FormatNumber(CType(objPOHeader.TotalHarga, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

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
        If CommonFunction.GetTransControlStatus(oDealer, EnumDealerTransType.DealerTransKind.Factoring) AndAlso oFMFac.IsAllowedToProposePO(objPOHeader.ContractHeader.Category.ProductCategory, oDealer.CreditAccount) Then
            Me.chkFactoring.Enabled = True
        Else
            Me.chkFactoring.Enabled = False
        End If
        'End    :Factoring;by:dna;on:20101004;for:yurike;remark:set control
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
        BindToPODetailObject()
        Return orgTotalPengajuan
    End Function

    Private Sub BindToPODetailObject()
        For Each dtgitem As DataGridItem In dtgDetail.Items
            Dim txtBox As TextBox = dtgitem.FindControl("TextBox1")
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
                ''End Append

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
            Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(CType(Session("DEALER"), Dealer).ID, EnumDealerTransType.DealerTransKind.Factoring)
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

    End Sub
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 1500
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckUserPrivilege()
        If Not IsPostBack Then
            ViewState("ShowMessage") = ""
            GetPO()
            For i As Integer = 0 To objPOHeader.PODetails.Count - 1
                arrOrder.Add(CType(objPOHeader.PODetails(i), PODetail).ReqQty)
            Next
            sessionHelper.SetSession("Ord", arrOrder)
            BindHeaderToForm()
            BindDetailToGrid()
            If Request.QueryString("src").ToUpper = "CREATENEW" Then
                '    MessageBox.Show("PO Berhasil Dibuat")
            End If
            If Request.QueryString("src").ToUpper = "CREATE" Then
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.PO_Harian), objPOHeader.PONumber, -1, CInt(enumStatusPO.Status.Baru))
                '   MessageBox.Show("PO Berhasil Dibuat")
            End If

            ImplementCeiling()

        End If
        'Hidden1.Value = CInt(ViewState("Count")) + 1
        'ViewState("Count") = Hidden1.Value
        btnBatal.Attributes.Add("OnClick", "return confirm('Yakin PO ini akan dihapus?');")
        SetControls()

        If Not IsPostBack Then
            Session("Dealer") = Session("TempDealer")
            LbVal.Text = DateTime.Now.ToString()
            Dim Waktu As Object = CType(Session("POLoad"), Object)
            LblInit.Text = CType(Waktu.StartSave, DateTime).ToString()
            lblSave.Text = CType(Waktu.EndSave, DateTime).ToString()
            Timer1.Interval = 40000
            Timer1.Enabled = True
        End If


    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PengajuanPO_Save_Privilege) Then
            '  Server.Transfer("../FrmAccessDenied.aspx?modulName=Edit PO")
        End If
        btnKirim.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPOUpdate_Privilege)
        btnBatal.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPOStatusCancel_Privilege)

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Label10.Visible = isPriceVisible
        Total.Visible = isPriceVisible
        Label9.Visible = isPriceVisible
        lblTotal.Visible = isPriceVisible
        dtgDetail.Columns(6).Visible = isPriceVisible
        dtgDetail.Columns(7).Visible = isPriceVisible
        dtgDetail.Columns(8).Visible = isPriceVisible
        dtgDetail.Columns(9).Visible = isPriceVisible
    End Sub

    Sub dtgDetail_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
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
            e.Item.Cells(7).Text = FormatNumber(CType(TxtBox.Text, Double) * CType(objPODetail.ContractDetail.Amount, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            'Start  :RemainModule-DailyPO:FreePPh By:Doni N
            e.Item.Cells(8).Text = FormatNumber(CType(TxtBox.Text, Double) * CType(objPODetail.ContractDetail.PPh22, Double) * CInt(objPODetail.POHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'e.Item.Cells(7).Text = FormatNumber(CType(TxtBox.Text, Double) * CType(objPODetail.ContractDetail.PPh22, Double) * IIf(CInt(objPODetail.POHeader.FreePPh22Indicator) = 1, 0, 1), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'Start  :RemainModule-DailyPO:FreePPh By:Doni N


            Dim freeIntIndicator As Integer
            Dim objPKHead As PKHeader = New PKHeaderFacade(User).Retrieve(objPODetail.POHeader.ContractHeader.PKNumber)
            freeIntIndicator = objPKHead.FreeIntIndicator


            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objPODetail.ContractDetail.VechileColor.ID))
            Dim oDealer As Dealer = Session("DEALER")
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, oDealer.ID))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
            'modified by ali
            'Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(criterias, sortColl)
            Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(objPODetail.ContractDetail)

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
                        e.Item.Cells(8).Text = FormatNumber(CType(TxtBox.Text, Double) * (_PPh22) * CInt(objPOHeader.ContractHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                        e.Item.Cells(9).Text = FormatNumber(CType(TxtBox.Text, Double) * freeIntIndicator * _interest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                    Else
                        e.Item.Cells(9).Text = FormatNumber(CType(TxtBox.Text, Double) * freeIntIndicator * Calculation.CountInterest(nTOP, nMonth, objPrice.Interest, objPODetail.ContractDetail.Amount - CType(e.Item.Cells(6).Text, Double), objPrice.PPh23), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
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
            'SubTotalSisa += CType(e.Item.Cells(4).Text, Double)
            'SubTotalOrder += CType(e.Item.Cells(5).Text, Double)
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(3).Text = "Sub Total :"
            'e.Item.Cells(4).Text = FormatNumber(SubTotalSisa, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'e.Item.Cells(5).Text = FormatNumber(SubTotalOrder, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            e.Item.Cells(6).Text = FormatNumber(SubTotalDeposit, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(7).Text = FormatNumber(SubTotalharga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(8).Text = FormatNumber(SubTotalPPh, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(9).Text = FormatNumber(SubTotalInterest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotal.Text = FormatNumber(SubTotalharga + SubTotalPPh + SubTotalInterest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
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

        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")

        If (POIsValid()) Then
            CountTotal()
        End If
    End Sub

    Private Sub CountTotal()
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
        BindDetailToGrid()
        'lblTotal.Text = FormatNumber(Total.ToString, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    End Sub

    Private Function IsEnabledCreditControl(ByVal objDealer As Dealer) As Boolean
        Dim _poHeaderFacade As POHeaderFacade = New POHeaderFacade(User)
        If _poHeaderFacade.IsEnabledCreditControl(objDealer) Then
            Return True
        End If
        Return False
    End Function

    Private Sub btnKirim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKirim.Click
        If Not Page.IsValid Then
            Return
        End If


        'Start  : Remaining Module - Check to Available Ceiling
        If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling(True) Then
            MessageBox.Show("Total PO yang akan disimpan melebihi Ceiling yang tersedia")
            Exit Sub
        End If
        'End    : Remaining Module - Check to Available Ceiling

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

        CountTotal()
        If (POIsValid()) Then
            If (PODetailIsValid()) Then
                BindToPOHeaderObject()
                Dim objPOHeaderFacade As New POHeaderFacade(User)
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
                If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling(True) Then
                    objPOHeaderFacade.Update(oPOHOri)
                    'MessageBox.Show("Update PO gagal, silahkan ulangi beberapa saat lagi")
                    MessageBox.Show("Update PO gagal, Total PO yang akan disimpan melebihi Ceiling yang tersedia")
                    Exit Sub
                End If
                MessageBox.Show(SR.UpdateSucces)
            Else
                MessageBox.Show("Sisa O/C Berubah, Order Melebihi Sisa O/C")
                BindDetailToGrid()
            End If
        End If
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
#End Region

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


     
        Dim Waktu As Object = CType(Session("POLoad"), Object)
        Timer1.Enabled = False
        Response.Redirect(Waktu.Uri.ToString())
    End Sub
End Class