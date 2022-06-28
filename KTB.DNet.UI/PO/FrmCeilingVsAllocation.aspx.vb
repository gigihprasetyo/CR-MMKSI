#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region


Public Class FrmCeilingVsAllocation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCetak As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchCreditAccount As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtRemainDay As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtReportDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIsShowPopup As System.Web.UI.WebControls.TextBox
    Protected WithEvents icReqDelivery As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Dim sHelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Methods"

    Private Sub Initialization()
        Dim objDealer As Dealer
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

        txtReportDate.Text = Format(Now, "dd MMM yyyy")
        txtRemainDay.Text = GetRemainWorkingDay(Now)

        viewstate.Add("SQLOrderField", "CreditAccount")
        viewstate.Add("SQLOrderDirection", Sort.SortDirection.ASC)

        BindDdlTOP()
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True, companyCode)
        ddlProductCategory.Items.RemoveAt(0)

        txtIsShowPopup.Text = "0"

        viewstate.Add("TotalCeiling", 0)
        viewstate.Add("TotalOutstanding", 0)
        viewstate.Add("TotalProposed", 0)
        viewstate.Add("TotalRemainCeiling", 0)
        viewstate.Add("TotalOCQty", 0)
        viewstate.Add("TotalOCAmount", 0)
        viewstate.Add("TotalRemainOCQty", 0)
        viewstate.Add("TotalRemainOCAmount", 0)
        viewstate.Add("TotalGyro", 0)
        viewstate.Add("TotalMaxPO", 0)
        viewstate.Add("TotalEstimation", 0)


        objDealer = sHelper.GetSession("DEALER")
        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            txtCreditAccount.Text = objDealer.CreditAccount
            txtCreditAccount.Enabled = False
            lblSearchCreditAccount.Visible = False
        End If

        'update if this page is from another page by Back Button
        If CType(sHelper.GetSession("FrmCeilingVsAllocation.IsAutoBind"), Boolean) Then
            txtCreditAccount.Text = sHelper.GetSession("FrmCeilingVsAllocation.CreditAccount")
            txtReportDate.Text = sHelper.GetSession("FrmCeilingVsAllocation.ReportDate")
            icReqDelivery.Value = sHelper.GetSession("FrmCeilingVsAllocation.ReqDeliveryDate")
            ddlPaymentType.SelectedValue = sHelper.GetSession("FrmCeilingVsAllocation.PaymentType")
            sHelper.SetSession("FrmCeilingVsAllocation.IsAutoBind", False)
            BindDTG()
        End If

    End Sub

    Private Sub BindDdlTOP()
        With ddlPaymentType
            .Items.Clear()
            .Items.Add(New ListItem("Silahkan Pilih", -1))
            .Items.Add(New ListItem("COD", enumPaymentType.PaymentType.COD))
            .Items.Add(New ListItem("TOP", enumPaymentType.PaymentType.TOP))
            'For Each li As ListItem In enumPaymentType.GetList()
            '    .Items.Add(li)
            'Next
            .SelectedValue = -1
        End With
    End Sub

    Private Function GetRemainWorkingDay(ByVal StateDate As Date) As Integer
        Dim objNHFac As NationalHolidayFacade = New NationalHolidayFacade(User)
        Dim crtNH As CriteriaComposite
        Dim arlNH As ArrayList = New ArrayList
        Dim EOM As Date
        Dim nDay As Integer

        EOM = StateDate.AddMonths(1)
        EOM = DateSerial(Year(EOM), Month(EOM), 1)
        EOM = EOM.AddDays(-1)
        nDay = DateDiff(DateInterval.Day, StateDate, EOM)
        crtNH = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayYear", MatchType.Exact, Year(StateDate)))
        crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayMonth", MatchType.Exact, Month(StateDate)))
        crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDate", MatchType.Greater, Day(StateDate)))
        arlNH = objNHFac.Retrieve(crtNH)
        nDay = nDay - arlNH.Count + 1

        Return nDay
    End Function

    Private Sub BindDTG()
        Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
        Dim crtSCM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_CreditMaster), "RowStatus", CType(DBRowStatus.Active, Short)))
        Dim arlRsl As ArrayList
        Dim srtSCM As SortCollection = New SortCollection
        Dim Total As Decimal

        txtRemainDay.Text = GetRemainWorkingDay(Now) 'CDate(txtReportDate.Text))

        srtSCM.Add(New Sort(GetType(sp_CreditMaster), viewstate.Item("SQLOrderField"), viewstate.Item("SQLOrderDirection")))
        If txtCreditAccount.Text.Trim <> "" Then
            crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "CreditAccount", txtCreditAccount.Text))
        End If
        If ddlPaymentType.SelectedValue <> -1 Then
            crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "PaymentType", MatchType.Exact, CType(ddlPaymentType.SelectedValue, Short)))
        Else
            crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "PaymentType", MatchType.InSet, "(" & CType(enumPaymentType.PaymentType.COD, Short) & "," & CType(enumPaymentType.PaymentType.TOP, Short) & ")"))
            'crtCM.opAnd(New Criteria(GetType(CreditMaster), "PaymentType", MatchType.Exact, 0))
        End If
        'srtSCM.Add(New Sort(GetType(sp_CreditMaster), "CreditAccount", Sort.SortDirection.ASC))
        'srtSCM.Add(New Sort(GetType(sp_CreditMaster), "PaymentType", Sort.SortDirection.ASC))
        ResetTotal()
        arlRsl = objSCMFac.RetrieveFromSP(Me.GetProductCategory(), CDate(txtReportDate.Text), icReqDelivery.Value, crtSCM, srtSCM)
        dtgMain.DataSource = arlRsl
        dtgMain.DataBind()
    End Sub

    Private Function GetProductCategory() As ProductCategory
        Return New ProductCategory(CType(Me.ddlProductCategory.SelectedValue, Short))
    End Function

    Private Sub ResetTotal()
        viewstate.Item("TotalCeiling") = 0
        viewstate.Item("TotalOutstanding") = 0
        viewstate.Item("TotalProposed") = 0
        viewstate.Item("TotalRemainCeiling") = 0
        viewstate.Item("TotalOCQty") = 0
        viewstate.Item("TotalOCAmount") = 0
        viewstate.Item("TotalRemainOCQty") = 0
        viewstate.Item("TotalRemainOCAmount") = 0
        viewstate.Item("TotalGyro") = 0
        viewstate.Item("TotalMaxPO") = 0
        viewstate.Item("TotalEstimation") = 0

    End Sub

    Private Sub BindDTGOld()
        Dim crtCM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objCMFac As CreditMasterFacade = New CreditMasterFacade(User)
        Dim arlCM As ArrayList = New ArrayList
        Dim srtCM As New SortCollection

        If txtCreditAccount.Text.Trim <> "" Then
            crtCM.opAnd(New Criteria(GetType(CreditMaster), "CreditAccount", MatchType.Exact, txtCreditAccount.Text.Trim))
        End If
        If ddlPaymentType.SelectedValue <> -1 Then
            crtCM.opAnd(New Criteria(GetType(CreditMaster), "PaymentType", MatchType.Exact, CType(ddlPaymentType.SelectedValue, Short)))
        Else
            crtCM.opAnd(New Criteria(GetType(CreditMaster), "PaymentType", MatchType.InSet, "(" & CType(enumPaymentType.PaymentType.COD, Short) & "," & CType(enumPaymentType.PaymentType.TOP, Short) & ")"))
            'crtCM.opAnd(New Criteria(GetType(CreditMaster), "PaymentType", MatchType.Exact, 0))
        End If
        srtCM.Add(New Sort(GetType(CreditMaster), "CreditAccount", Sort.SortDirection.ASC))
        srtCM.Add(New Sort(GetType(CreditMaster), "PaymentType", Sort.SortDirection.ASC))
        arlCM = objCMFac.Retrieve(crtCM, srtCM)

        dtgMain.DataSource = arlCM
        dtgMain.DataBind()
    End Sub

    Private Function GetProposedByCreditAccount(ByVal CreditAccount As String, ByVal PaymentType As Short) As Decimal
        Dim objDFac As DealerFacade = New DealerFacade(User)
        Dim crtD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlD As ArrayList = New ArrayList
        Dim Sql As String
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim crtPOH As CriteriaComposite
        Dim arlPOH As ArrayList = New ArrayList
        Dim ReportDate As Date = CDate(txtReportDate.Text)
        Dim ReqDevDate As Date = CDate(icReqDelivery.Value)
        Dim Total As Decimal

        crtD.opAnd(New Criteria(GetType(Dealer), "CreditAccount", MatchType.Exact, CreditAccount))
        arlD = objDFac.Retrieve(crtD)
        Total = 0
        For Each objD As Dealer In arlD

            crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, objD.ID))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"))
            'crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True)
            'Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID)"
            'crtPOH.opOr(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(ReportDate, "MM/dd/yyyy")))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(ReqDevDate, "MM/dd/yyyy")))

            arlPOH = objPOHFac.Retrieve(crtPOH)
            'For Each objPOH As POHeader In arlPOH
            '    Total = Total + GetProposed(objPOH.ID, ReportDate)
            'Next

            For Each objPOH As POHeader In arlPOH
                If IsHavingGyro(objPOH) = False Then
                    Total += objPOH.TotalPODetail()
                End If
            Next

        Next

        Return Total


    End Function
    Private Function IsHavingGyro(ByRef objPOH As POHeader) As Boolean
        Dim Rsl As Boolean = True

        If objPOH.DailyPayments.Count < 1 Then
            Rsl = False
        Else
            For Each objDP As DailyPayment In objPOH.DailyPayments
                If (objDP.PaymentPurpose.ID = 3 Or objDP.PaymentPurpose.ID = 6) And (objDP.RejectStatus = 0 And objDP.IsCleared = 0 And objDP.IsReversed = 0 AndAlso objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)) Then
                    Return True
                Else
                    Rsl = False
                End If
            Next
        End If
        Return Rsl
    End Function
    Private Function GetProposed(ByVal POID As Integer, ByVal ReportDate As Date) As Decimal   ', ByVal ReqDevDate As Date) As Decimal
        Dim crtPOH As CriteriaComposite
        Dim Sql As String
        Dim arlPOH As ArrayList = New ArrayList
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim objPOH As POHeader
        Dim Total As Decimal = 0

        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ID", MatchType.Exact, POID))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(ReportDate, "MM/dd/yyyy")))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.Exact, Format(ReportDate, "MM/dd/yyyy")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(ReportDate, "MM/dd/yyyy")))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(ReqDevDate, "MM/dd/yyyy")))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        If arlPOH.Count > 0 Then
            objPOH = arlPOH(0)
            Total += objPOH.TotalPODetail()
            'For Each objPOD As PODetail In objPOH.PODetails
            '    If objPOH.Status = 0 Or objPOH.Status = 2 Then
            '        Total = Total + (objPOD.ReqQty * objPOD.Price)
            '    ElseIf objPOH.Status = 6 Then
            '        Total = Total + (objPOD.AllocQty * objPOD.Price)
            '    End If
            'Next
        End If
        Return Total
    End Function

    Private Function ProcessedPO(ByVal CreditAccount As String, ByVal PaymentType As Short) As Decimal
        Dim objDFac As DealerFacade = New DealerFacade(User)
        Dim crtD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlD As ArrayList = New ArrayList
        Dim Sql As String
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim crtPOH As CriteriaComposite
        Dim arlPOH As ArrayList = New ArrayList
        Dim ReportDate As Date = CDate(txtReportDate.Text)
        Dim ReqDevDate As Date = CDate(txtReportDate.Text)
        Dim Total As Decimal

        crtD.opAnd(New Criteria(GetType(Dealer), "CreditAccount", MatchType.Exact, CreditAccount))
        arlD = objDFac.Retrieve(crtD)
        Total = 0
        For Each objD As Dealer In arlD

            crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, objD.ID))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(4,6,8)"), "(", True)
            Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID)"
            crtPOH.opOr(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
            'crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(ReqDevDate, "MM/dd/yyyy")))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(ReportDate, "MM/dd/yyyy")))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(ReqDevDate, "MM/dd/yyyy")))

            arlPOH = objPOHFac.Retrieve(crtPOH)
            For Each objPOH As POHeader In arlPOH
                For Each objPOD As PODetail In objPOH.PODetails
                    Total = Total + (objPOD.AllocQty * objPOD.Price)
                Next
            Next
        Next

        Return Total

    End Function

    Private Function GetContractDetailNew(ByVal CreditAccount As String, ByVal PaymentType As Short, ByVal oPC As ProductCategory) As ArrayList
        Dim arlResult As ArrayList = New ArrayList
        Dim objCHFac As ContractHeaderFacade = New ContractHeaderFacade(User)
        Dim crtCH As CriteriaComposite
        Dim arlCH As ArrayList
        Dim Sql As String
        Dim objSPLFac As SPLFacade = New SPLFacade(User)
        Dim objSPL As SPL
        Dim IsHavingSPL As Boolean
        Dim TotalQty As Decimal
        Dim TotalAmount As Decimal
        Dim RemainTotalQty As Decimal
        Dim RemainTotalAmount As Decimal
        Dim TotalGyro As Decimal
        Dim objPODFac As PODetailFacade = New PODetailFacade(User)
        Dim arlPOD As ArrayList = New ArrayList
        Dim crtPOD As CriteriaComposite
        Dim TotalTemp As Decimal
        Dim i As Integer
        Dim ReportDate As Date = CDate(txtReportDate.Text)
        Dim ReqDevDate As Date = icReqDelivery.Value
        Dim EOM As Date = CDate(txtReportDate.Text).AddMonths(1)   'End Of Month
        EOM = DateSerial(EOM.Year, EOM.Month, 1).AddDays(-1)


        TotalQty = 0
        TotalAmount = 0
        RemainTotalQty = 0
        RemainTotalAmount = 0
        TotalGyro = 0

        crtCH = New CriteriaComposite(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtCH.opAnd(New Criteria(GetType(ContractHeader), "ContractPeriodMonth", MatchType.Exact, Month(ReportDate)))
        crtCH.opAnd(New Criteria(GetType(ContractHeader), "ContractPeriodYear", MatchType.Exact, Year(ReportDate)))
        crtCH.opAnd(New Criteria(GetType(ContractHeader), "Category.ProductCategory.ID", MatchType.Exact, oPC.ID))

        Sql = "(select ID from Dealer where CreditAccount='" & CreditAccount & "')"
        crtCH.opAnd(New Criteria(GetType(ContractHeader), "Dealer.ID", MatchType.InSet, Sql))
        arlCH = objCHFac.Retrieve(crtCH)
        For Each objCH As ContractHeader In arlCH
            IsHavingSPL = True
            If objCH.SPLNumber.Trim = "" Then
                IsHavingSPL = False
            Else
                objSPL = objSPLFac.Retrieve(objCH.SPLNumber)
                If objSPL Is Nothing Then
                    IsHavingSPL = False
                Else
                    If objSPL.ID < 1 Then
                        IsHavingSPL = False
                    Else
                        For Each objSPLD As SPLDetail In objSPL.SPLDetails
                            If objSPLD.MaxTopDay = 0 Then
                                IsHavingSPL = False
                                Exit For
                            End If
                        Next
                    End If
                End If
            End If

            If (PaymentType = enumPaymentType.PaymentType.TOP And IsHavingSPL) Or (PaymentType = enumPaymentType.PaymentType.COD And Not IsHavingSPL) Then
                For Each objCD As ContractDetail In objCH.ContractDetails
                    TotalQty = TotalQty + objCD.TargetQty
                    TotalAmount = TotalAmount + (objCD.TargetQty * objCD.Amount)

                    crtPOD = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtPOD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.ID", MatchType.Exact, objCD.ID))
                    arlPOD = objPODFac.Retrieve(crtPOD)
                    TotalTemp = 0
                    For Each objPOD As PODetail In arlPOD
                        i = objPOD.POHeader.Status
                        If i = enumStatusPO.Status.Baru Or i = enumStatusPO.Status.Konfirmasi Then
                            TotalTemp = TotalTemp + objPOD.ReqQty
                        ElseIf i = enumStatusPO.Status.Rilis Or i = enumStatusPO.Status.Setuju Or i = enumStatusPO.Status.Selesai Then
                            TotalTemp = TotalTemp + objPOD.AllocQty
                        End If
                    Next
                    RemainTotalQty = RemainTotalQty + (objCD.TargetQty - TotalTemp)
                    RemainTotalAmount = RemainTotalAmount + ((objCD.TargetQty - TotalTemp) * objCD.Amount)
                Next
            End If
        Next

        'Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        'Dim crtPOH As CriteriaComposite
        'Dim arlPOH As ArrayList = New ArrayList

        'crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, ReportDate))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, ReqDevDate))
        ''crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        'arlPOH = objPOHFac.Retrieve(crtPOH)

        'TotalGyro = 0
        'For Each objPOH As POHeader In arlPOH
        '    For Each objDP As DailyPayment In objPOH.DailyPayments
        '        If objDP.RejectStatus = 0 And (objDP.EffectiveDate > CDate(txtReportDate.Text) And objDP.EffectiveDate <= EOM) Then
        '            TotalGyro = TotalGyro + objDP.Amount
        '        End If
        '    Next
        'Next

        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim crtDP As CriteriaComposite
        Dim arlDP As ArrayList
        'Not Accelerated Gyro
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, CDate(txtReportDate.Text)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, EOM))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Category.ProductCategory.ID", MatchType.Exact, oPC.ID))
        arlDP = objDPFac.Retrieve(crtDP)
        TotalGyro = 0
        For Each oDP As DailyPayment In arlDP
            TotalGyro += oDP.Amount
        Next
        'Accelerated Gyro
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.GreaterOrEqual, CDate(txtReportDate.Text)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.LesserOrEqual, EOM))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Category.ProductCategory.ID", MatchType.Exact, oPC.ID))
        arlDP = objDPFac.Retrieve(crtDP)
        'TotalGyro = 0
        For Each oDP As DailyPayment In arlDP
            TotalGyro += oDP.Amount
        Next

        arlResult.Add(TotalQty)
        arlResult.Add(TotalAmount)
        arlResult.Add(RemainTotalQty)
        arlResult.Add(RemainTotalAmount)
        arlResult.Add(TotalGyro)
        Return arlResult


    End Function

    Private Function GetContractDetail(ByVal CreditAccount As String, ByVal PaymentType As Short) As ArrayList
        Dim arlResult As ArrayList = New ArrayList
        Dim objDFac As DealerFacade = New DealerFacade(User)
        Dim crtD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlD As ArrayList = New ArrayList
        Dim Sql As String
        Dim objPKFac As PKHeaderFacade = New PKHeaderFacade(User)
        Dim crtPK As CriteriaComposite
        Dim arlPK As ArrayList = New ArrayList
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim crtPOH As CriteriaComposite
        Dim arlPOH As ArrayList = New ArrayList
        Dim ReportDate As Date = CDate(txtReportDate.Text)
        Dim ReqDevDate As Date = icReqDelivery.Value
        Dim TotalQty As Decimal
        Dim TotalAmount As Decimal
        Dim RemainTotalQty As Decimal
        Dim RemainTotalAmount As Decimal
        Dim TotalGyro As Decimal
        Dim objSPLFac As SPLFacade = New SPLFacade(User)
        Dim objSPL As SPL
        Dim IsHavingSPL As Boolean
        Dim objPODFac As PODetailFacade = New PODetailFacade(User)
        Dim arlPOD As ArrayList = New ArrayList
        Dim crtPOD As CriteriaComposite
        Dim TotalTemp As Decimal
        Dim i As Integer
        Dim EOM As Date = CDate(txtReportDate.Text).AddMonths(1)  'End Of Month
        Dim DateTemp As Date

        EOM = DateSerial(Year(EOM), Month(EOM), 1)
        EOM = EOM.AddDays(-1)

        crtD.opAnd(New Criteria(GetType(Dealer), "CreditAccount", MatchType.Exact, CreditAccount))
        arlD = objDFac.Retrieve(crtD)
        TotalQty = 0
        TotalAmount = 0
        RemainTotalQty = 0
        RemainTotalAmount = 0
        TotalGyro = 0
        For Each objD As Dealer In arlD
            crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, objD.ID))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
            'crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(4,6,8)"), "(", True)
            'Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID)"
            'crtPOH.opOr(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
            'crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(ReqDevDate, "MM/dd/yyyy")))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(ReportDate, "MM/dd/yyyy")))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(ReqDevDate, "MM/dd/yyyy")))

            arlPOH = objPOHFac.Retrieve(crtPOH)
            For Each objPOH As POHeader In arlPOH
                'Value Of OC
                objSPL = objSPLFac.Retrieve(objPOH.ContractHeader.SPLNumber)
                If objSPL Is Nothing Then
                    IsHavingSPL = False
                Else
                    If objSPL.ID < 1 Then
                        IsHavingSPL = False
                    Else
                        For Each objSPLD As SPLDetail In objSPL.SPLDetails
                            If objSPLD.MaxTopDay = 0 Then
                                IsHavingSPL = False
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (PaymentType = enumPaymentType.PaymentType.TOP And IsHavingSPL) Or (PaymentType = enumPaymentType.PaymentType.COD And Not IsHavingSPL) Then
                    DateTemp = DateSerial(Year(ReportDate), objPOH.ContractHeader.ContractPeriodMonth, 1)
                    'Only ContractHeader where ContractPeriodMonth lesser or equal with EndOfMonth(SystemTime)
                    If DateTemp <= EOM Then
                        For Each objCD As ContractDetail In objPOH.ContractHeader.ContractDetails
                            TotalQty = TotalQty + objCD.TargetQty
                            TotalAmount = TotalAmount + (objCD.TargetQty * objCD.Amount)

                            crtPOD = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crtPOD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.ID", MatchType.Exact, objCD.ID))
                            arlPOD = objPODFac.Retrieve(crtPOD)
                            TotalTemp = 0
                            For Each objPOD As PODetail In arlPOD
                                i = objPOD.POHeader.Status
                                If i = enumStatusPO.Status.Baru Or i = enumStatusPO.Status.Konfirmasi Then
                                    TotalTemp = TotalTemp + objPOD.ReqQty
                                ElseIf i = enumStatusPO.Status.Rilis Or i = enumStatusPO.Status.Setuju Or i = enumStatusPO.Status.Selesai Then
                                    TotalTemp = TotalTemp + objPOD.AllocQty
                                End If
                            Next
                            RemainTotalQty = RemainTotalQty + (objCD.TargetQty - TotalTemp)
                            RemainTotalAmount = RemainTotalAmount + ((objCD.TargetQty - TotalTemp) * objCD.Amount)
                        Next
                    End If
                End If
                For Each objDP As DailyPayment In objPOH.DailyPayments
                    If objDP.RejectStatus = 0 And (objDP.EffectiveDate > CDate(txtReportDate.Text) And objDP.EffectiveDate <= EOM And objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)) Then
                        TotalGyro = TotalGyro + objDP.Amount
                    End If
                Next
            Next
        Next

        arlResult.Add(TotalQty)
        arlResult.Add(TotalAmount)
        arlResult.Add(RemainTotalQty)
        arlResult.Add(RemainTotalAmount)
        arlResult.Add(TotalGyro)
        Return arlResult


    End Function

    Public Function IsValidReqDeliveryDate() As Boolean
        If CDate(txtReportDate.Text) > CDate(icReqDelivery.Value) Then
            MessageBox.Show("Pemintaan kirim tidak boleh kurang dari waktu sistem")
            Return False
        Else
            Return True
        End If
    End Function


    Private Sub DoDownload()

        Dim sFileName As String

        sFileName = "Ceiling Vs Alokasi" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim ListData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(ListData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(ListData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteListData(sw)
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
    Private Sub WriteListData(ByVal sw As StreamWriter)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder
        Dim sumTotalHarga As Decimal
        Dim sumTotalHargaPPN As Decimal


        itemLine.Remove(0, itemLine.Length)
        itemLine.Append("CREDIT CONTROL - Ceiling Vs Alokasi" & tab)
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append("Permintaan Kirim" & tab & Format(icReqDelivery.Value, "dd MMM yyyy"))
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append("No" & tab)
        itemLine.Append("Credit Account" & tab)
        itemLine.Append("Produk" & tab)
        itemLine.Append("Cara Pembayaran" & tab)
        itemLine.Append("Ceiling" & tab)
        itemLine.Append("Outstanding" & tab)
        itemLine.Append("PO Sedang Diajukan" & tab)
        itemLine.Append("Sisa Ceiling" & tab)
        itemLine.Append("Unit O/C" & tab)
        itemLine.Append("Nilai O/C" & tab)
        itemLine.Append("Unit Sisa O/C" & tab)
        itemLine.Append("Nilai Sisa O/C" & tab)
        itemLine.Append("Giro Cair Bulan Ini" & tab)
        itemLine.Append("Max PO" & tab)
        itemLine.Append("Estimasi Akhir Bulan" & tab)
        sw.WriteLine(itemLine.ToString())



        Dim i As Integer = 1
        'Dim myculture As CultureInfo = New CultureInfo("ID-id")
        For Each di As DataGridItem In dtgMain.Items
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(CType(di.FindControl("lblNo"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblCreditAccount"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblProduk"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblPaymentType"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblCeiling"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblOutstanding"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblProposed"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblRemainCeiling"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblOCQty"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblOCAmount"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblRemainOCQty"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lbtnRemainOCAmount"), LinkButton).Text & tab)
            itemLine.Append(CType(di.FindControl("lblGyro"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblMaxPO"), Label).Text & tab)
            itemLine.Append(CType(di.FindControl("lblEstimation"), Label).Text & tab)

            sw.WriteLine(itemLine.ToString())
        Next
        'Footer
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab)
        itemLine.Append("TOTAL" & tab)
        itemLine.Append(" " & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalCeiling"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalOutstanding"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalProposed"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalRemainCeiling"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalOCQty"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalOCAmount"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalRemainOCQty"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalRemainOCAmount"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalGyro"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalMaxPO"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
        itemLine.Append(FormatNumber(viewstate.Item("TotalEstimation"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)

        sw.WriteLine(itemLine.ToString())
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Ceiling_vs_alokasi_lihat_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Sales Credit Control - Ceiling Vs Alokasi")
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.Ceiling_vs_alokasi_download_cetak_privilege) Then
            Me.btnDownload.Visible = False
            Me.btnCetak.Visible = False
        End If

    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            Initialization()

        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If Not IsValidReqDeliveryDate() Then Exit Sub
        BindDTG()

    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType")
            Dim lblCeiling As Label = e.Item.FindControl("lblCeiling")
            Dim lblOutstanding As Label = e.Item.FindControl("lblOutstanding")
            Dim lblProposed As Label = e.Item.FindControl("lblProposed")
            Dim lblRemainCeiling As Label = e.Item.FindControl("lblRemainCeiling")
            Dim lblOCQty As Label = e.Item.FindControl("lblOCQty")
            Dim lblOCAmount As Label = e.Item.FindControl("lblOCAmount")
            Dim lblRemainOCQty As Label = e.Item.FindControl("lblRemainOCQty")
            Dim lbtnRemainOCAmount As LinkButton = e.Item.FindControl("lbtnRemainOCAmount")
            Dim lblGyro As Label = e.Item.FindControl("lblGyro")
            Dim lblMaxPO As Label = e.Item.FindControl("lblMaxPO")
            Dim lblEstimation As Label = e.Item.FindControl("lblEstimation")
            Dim PaymentType As Short
            Dim arlTemp As ArrayList = New ArrayList
            Dim TotalTemp As Decimal
            Dim txtUrlParams As TextBox = e.Item.FindControl("txtUrlParams")
            Dim UrlParams As String
            Dim objDealer As Dealer
            Dim lblProduk As Label = e.Item.FindControl("lblProduk")
            Dim oPC As ProductCategory = New ProductCategoryFacade(User).Retrieve(lblProduk.Text)

            objDealer = New DealerFacade(User).Retrieve(lblCreditAccount.Text)
            If Not objDealer Is Nothing Then
                lblCreditAccount.ToolTip = objDealer.SearchTerm1
            End If

            lblNo.Text = e.Item.ItemIndex + 1
            PaymentType = CType(lblPaymentType.Text, Short)
            lblPaymentType.Text = enumPaymentType.GetStringValue(CType(lblPaymentType.Text, Integer))
            lblCeiling.Text = FormatNumber(lblCeiling.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblOutstanding.Text = FormatNumber(lblOutstanding.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'lblProposed.Text = FormatNumber(GetProposedByCreditAccount(lblCreditAccount.Text, PaymentType), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblProposed.Text = FormatNumber(lblProposed.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'lblRemainCeiling.Text = FormatNumber((CType(lblCeiling.Text, Decimal) - CType(lblOutstanding.Text, Decimal) - ProcessedPO(lblCreditAccount.Text, PaymentType)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblRemainCeiling.Text = FormatNumber((CType(lblCeiling.Text, Decimal) - CType(lblOutstanding.Text, Decimal) - CType(lblProposed.Text, Decimal)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            'arlTemp = GetContractDetail(lblCreditAccount.Text, PaymentType)

            arlTemp = GetContractDetailNew(lblCreditAccount.Text, PaymentType, oPC)
            lblOCQty.Text = FormatNumber(CType(arlTemp(0), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblOCAmount.Text = FormatNumber(CType(arlTemp(1), Decimal), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblRemainOCQty.Text = FormatNumber(CType(arlTemp(2), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lbtnRemainOCAmount.Text = FormatNumber(CType(arlTemp(3), Decimal), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblGyro.Text = FormatNumber(CType(arlTemp(4), Decimal), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            If PaymentType = enumPaymentType.PaymentType.TOP Then
                TotalTemp = CType(lblGyro.Text, Decimal) + CType(lblRemainCeiling.Text, Decimal)
                lblMaxPO.Text = FormatNumber(TotalTemp, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            ElseIf PaymentType = enumPaymentType.PaymentType.COD Then
                TotalTemp = CType(lblRemainCeiling.Text, Decimal) * Math.Ceiling(CInt(txtRemainDay.Text) / 2)
                TotalTemp += (CDec(lblCeiling.Text) - CDec(lblRemainCeiling.Text)) * Math.Floor(CInt(txtRemainDay.Text) / 2)
                lblMaxPO.Text = FormatNumber(TotalTemp, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
            TotalTemp = CType(lblMaxPO.Text, Decimal) - CType(lbtnRemainOCAmount.Text, Decimal)
            lblEstimation.Text = FormatNumber(TotalTemp, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            'lblTitle.Text = "Max PO " & CType(Request.Item("PaymentType"), String)
            'lblPattern.Text = CType(Request.Item("sPattern"), String)
            'lblCalculation.Text = CType(Request.Item("sCalculation"), String)
            'lblResult.Text = CType(Request.Item("sResult"), String)

            If PaymentType = enumPaymentType.PaymentType.COD Then
                UrlParams = "PaymentType=COD"
                UrlParams &= "&sPattern=Sisa Ceiling x ROUNDUP(Sisa Hari Kerja/2)+(Ceiling - Sisa Ceiling) x ROUNDDOWN(Sisa Hari Kerja/2)"
                UrlParams &= "&sCalculation=(" & lblRemainCeiling.Text & " x ROUNDUP(" & txtRemainDay.Text & "/2)) + (" & lblCeiling.Text & "-" & lblRemainCeiling.Text & ") x ROUNDDOWN(" & txtRemainDay.Text & "/2)"
                UrlParams &= "&sResult=" & lblMaxPO.Text
            ElseIf PaymentType = enumPaymentType.PaymentType.TOP Then
                UrlParams = "PaymentType=TOP"
                UrlParams &= "&sPattern=Giro Cair + Sisa Ceiling"
                UrlParams &= "&sCalculation=" & lblGyro.Text & " + " & lblRemainCeiling.Text
                UrlParams &= "&sResult=" & lblMaxPO.Text
            End If
            txtUrlParams.Text = UrlParams
            'lblDetail.Attributes.Add("OnClick", "showPopUp('../PopUp/PopUpMaxPO.aspx" & UrlParams & "','',500,760);")


            viewstate.Item("TotalCeiling") += lblCeiling.Text
            viewstate.Item("TotalOutstanding") += lblOutstanding.Text
            viewstate.Item("TotalProposed") += lblProposed.Text
            viewstate.Item("TotalRemainCeiling") += lblRemainCeiling.Text
            viewstate.Item("TotalOCQty") += lblOCQty.Text
            viewstate.Item("TotalOCAmount") += lblOCAmount.Text
            viewstate.Item("TotalRemainOCQty") += lblRemainOCQty.Text
            viewstate.Item("TotalRemainOCAmount") += lbtnRemainOCAmount.Text
            viewstate.Item("TotalGyro") += lblGyro.Text
            viewstate.Item("TotalMaxPO") += lblMaxPO.Text
            viewstate.Item("TotalEstimation") += lblEstimation.Text
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim lblFCeiling As Label = e.Item.FindControl("lblFCeiling")
            Dim lblFOutstanding As Label = e.Item.FindControl("lblFOutstanding")
            Dim lblFProposed As Label = e.Item.FindControl("lblFProposed")
            Dim lblFRemainCeiling As Label = e.Item.FindControl("lblFRemainCeiling")
            Dim lblFOCQty As Label = e.Item.FindControl("lblFOCQty")
            Dim lblFOCAmount As Label = e.Item.FindControl("lblFOCAmount")
            Dim lblFRemainOCQty As Label = e.Item.FindControl("lblFRemainOCQty")
            Dim lblFRemainOCAmount As Label = e.Item.FindControl("lblFRemainOCAmount")
            Dim lblFGyro As Label = e.Item.FindControl("lblFGyro")
            Dim lblFMaxPO As Label = e.Item.FindControl("lblFMaxPO")
            Dim lblFEstimation As Label = e.Item.FindControl("lblFEstimation")

            lblFCeiling.Text = FormatNumber(viewstate.Item("TotalCeiling"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFOutstanding.Text = FormatNumber(viewstate.Item("TotalOutstanding"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFProposed.Text = FormatNumber(viewstate.Item("TotalProposed"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFRemainCeiling.Text = FormatNumber(viewstate.Item("TotalRemainCeiling"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFOCQty.Text = FormatNumber(viewstate.Item("TotalOCQty"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFOCAmount.Text = FormatNumber(viewstate.Item("TotalOCAmount"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFRemainOCQty.Text = FormatNumber(viewstate.Item("TotalRemainOCQty"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFRemainOCAmount.Text = FormatNumber(viewstate.Item("TotalRemainOCAmount"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFGyro.Text = FormatNumber(viewstate.Item("TotalGyro"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFMaxPO.Text = FormatNumber(viewstate.Item("TotalMaxPO"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFEstimation.Text = FormatNumber(viewstate.Item("TotalEstimation"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        If e.CommandName = "Detail" Then
            Dim txtUrlParams As TextBox = e.Item.FindControl("txtUrlParams")

            txtIsShowPopup.Text = "1"
            sHelper.SetSession("MaxPOUrl", txtUrlParams.Text)
            RegisterStartupScript("OpenWindow", "<script>ShowPopupMaxPO();</script>")
        ElseIf e.CommandName = "DetailRemainOC" Then
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")

            sHelper.SetSession("FrmCeilingVsAllocation.IsAutoBind", True)
            sHelper.SetSession("FrmCeilingVsAllocation.CreditAccount", txtCreditAccount.Text)
            sHelper.SetSession("FrmCeilingVsAllocation.ReqDeliveryDate", Me.icReqDelivery.Value)
            sHelper.SetSession("FrmCeilingVsAllocation.ReportDate", txtReportDate.Text)
            sHelper.SetSession("FrmCeilingVsAllocation.PaymentType", ddlPaymentType.SelectedValue)

            Response.Redirect("OutStandingMO.aspx?DealerCode=" & lblCreditAccount.Text)
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        If dtgMain.Items.Count < 1 Then
            MessageBox.Show("Data tidak ada")
            Exit Sub
        Else
            DoDownload()
        End If
    End Sub

    Private Sub dtgMain_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMain.SortCommand
        If e.SortExpression = viewstate.Item("SQLOrderField") Then
            If viewstate.Item("SQLOrderDirection") = Sort.SortDirection.ASC Then
                viewstate.Item("SQLOrderDirection") = Sort.SortDirection.DESC
            Else
                viewstate.Item("SQLOrderDirection") = Sort.SortDirection.ASC
            End If
        Else
            viewstate.Item("SQLOrderField") = e.SortExpression
            viewstate.Item("SQLOrderDirection") = Sort.SortDirection.ASC
        End If
        If viewstate.Item("SQLOrderField") = "CreditAccount" Then
            viewstate.Item("IsSpanning") = "1"
        Else
            viewstate.Item("IsSpanning") = "0"
        End If
        BindDTG()
    End Sub

    Private Sub btnCetak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCetak.Click
        sHelper.SetSession("dtgCeilingVsAllocation", Me.dtgMain)

        sHelper.SetSession("FrmCeilingVsAllocation.CreditAccount", txtCreditAccount.Text)
        sHelper.SetSession("FrmCeilingVsAllocation.ReqDeliveryDate", Me.icReqDelivery.Value)
        sHelper.SetSession("FrmCeilingVsAllocation.ReportDate", txtReportDate.Text)
        sHelper.SetSession("FrmCeilingVsAllocation.PaymentType", ddlPaymentType.SelectedValue)
        sHelper.SetSession("FrmCeilingVsAllocation.IsAutoBind", False)

        Response.Redirect("FrmCeilingVsAllocationPrint.aspx?DeliveryDate=" & Format(Me.icReqDelivery.Value, "yyyy.MM.dd") & "&PaymentType=" & ddlPaymentType.SelectedValue & "&Remain=" & txtRemainDay.Text)
    End Sub

#End Region

End Class

