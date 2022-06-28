#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class FrmCeiling
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesOrg As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents txtReportDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalCredit As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents icReqDelivery As Intimedia.WebCC.IntiCalendar
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIsSpanned As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSaveMaxTOPDate As System.Web.UI.WebControls.Button

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
    Private sHelper As New SessionHelper
    Private sb As System.Text.StringBuilder
    Private objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)

#End Region

#Region "Custom Methods"

    Private Sub BindDTG(ByVal PgIdx As Integer)

        'Clear Session first
        sHelper.SetSession("FrmCeiling.IsAutoBind", False)
        sHelper.SetSession("FrmCeiling.CreditAccount", "")
        sHelper.SetSession("FrmCeiling.ReqDeliveryDate", Now)
        sHelper.SetSession("FrmCeiling.PageIndex", 0)


        Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
        Dim crtSCM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_CreditMaster), "RowStatus", CType(DBRowStatus.Active, Short)))
        Dim arlRsl As ArrayList
        Dim srtSCM As SortCollection = New SortCollection
        Dim Total As Decimal

        If viewstate.Item("SQLOrderField") = "" Then
            srtSCM.Add(New Sort(GetType(sp_CreditMaster), "CreditAccount", Sort.SortDirection.ASC))
            srtSCM.Add(New Sort(GetType(sp_CreditMaster), "PaymentType", Sort.SortDirection.ASC))
        Else
            srtSCM.Add(New Sort(GetType(sp_CreditMaster), viewstate.Item("SQLOrderField"), viewstate.Item("SQLOrderDirection")))
        End If


        If txtCreditAccount.Text.Trim <> "" Then
            'crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "CreditAccount", txtCreditAccount.Text))
            crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "CreditAccount", MatchType.InSet, "('" & txtCreditAccount.Text.Trim().Replace(";", "','") & "')"))
        End If
        arlRsl = objSCMFac.RetrieveFromSP(CDate(txtReportDate.Text), icReqDelivery.Value, crtSCM, srtSCM, True)
        Me.sHelper.SetSession("FrmCeiling.arlSCM", arlRsl)
        dtgMain.DataSource = arlRsl
        dtgMain.DataBind()
        RegisterStartupScript("OpenWindow", "<script>Spanning();</script>")
        If txtCreditAccount.Text.Trim <> "" Then
            For Each oSCM As sp_CreditMaster In arlRsl
                Total = Total + oSCM.Plafon
            Next
            lblTotalCredit.Text = "Rp. " & FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

        Exit Sub

        Dim objCMFac As CreditMasterFacade = New CreditMasterFacade(User)
        Dim crtCM As CriteriaComposite
        Dim arlCM As ArrayList = New ArrayList
        Dim TotRow As Integer

        crtCM = New CriteriaComposite(New Criteria(GetType(CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtCreditAccount.Text.Trim <> "" Then
            'crtCM.opAnd(New Criteria(GetType(CreditMaster), "CreditAccount", MatchType.Exact, txtCreditAccount.Text))
            crtCM.opAnd(New Criteria(GetType(CreditMaster), "CreditAccount", MatchType.InSet, "('" & txtCreditAccount.Text.Trim().Replace(";", "','") & "')"))
        End If
        arlCM = objCMFac.RetrieveActiveList(crtCM, PgIdx + 1, dtgMain.PageSize, TotRow, viewstate.Item("SQLOrderField"), viewstate.Item("SQLOrderDirection"))

        txtIsSpanned.Text = viewstate.Item("IsSpanning")
        dtgMain.CurrentPageIndex = PgIdx
        dtgMain.VirtualItemCount = TotRow
        dtgMain.DataSource = arlCM
        dtgMain.DataBind()
        RegisterStartupScript("OpenWindow", "<script>Spanning();</script>")

    End Sub

    Private Sub Initialization()
        Dim objDealer As Dealer

        'viewstate.Add("SQLOrderField", "CreditAccount")
        'viewstate.Add("SQLOrderDirection", Sort.SortDirection.ASC)
        viewstate.Add("SQLOrderField", "")
        viewstate.Add("SQLOrderDirection", Sort.SortDirection.ASC)
        viewstate.Add("IsCalcAccelaratedGyro", True)

        viewstate.Add("IsSpanning", "1")
        txtReportDate.Text = Format(Now, "dd MMM yyyy")

        btnSaveMaxTOPDate.Visible = True
        objDealer = sHelper.GetSession("DEALER")
        If objDealer.DealerCode.Trim.ToUpper <> "KTB" Then
            txtCreditAccount.Text = objDealer.CreditAccount
            txtDealerName.Text = objDealer.DealerName
            txtCreditAccount.Enabled = False
            lblSearchDealer.Visible = False
            dtgMain.Columns(10).Visible = False
            viewstate.Item("IsCalcAccelaratedGyro") = False
            btnSaveMaxTOPDate.Visible = False
        End If
    End Sub

    Private Function GetLiquefiedPO(ByVal CreditAccount As String, ByVal PaymentType As Short) As Decimal
        Dim arlD As New ArrayList
        Dim objDFac As DealerFacade = New DealerFacade(User)
        Dim crtD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim Total As Decimal = 0

        crtD.opAnd(New Criteria(GetType(Dealer), "CreditAccount", MatchType.Exact, CreditAccount))
        arlD = objDFac.Retrieve(crtD)
        For Each objD As Dealer In arlD
            Total += GetDealerPO(objD, PaymentType, CType(txtReportDate.Text, Date), icReqDelivery.Value)
        Next
        Return Total
    End Function

    Private Function GetDealerPO(ByVal objDealer As Dealer, ByVal PaymentType As Short, ByVal ReportDate As Date, ByVal ReqDeliveryDate As Date) As Decimal
        Dim TotalLiquefied As Decimal = 0
        Dim TotalAcceleratedGyro As Decimal = 0
        Dim arlResult As ArrayList = New ArrayList
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim crtPOH As CriteriaComposite
        Dim arlPOH As ArrayList = New ArrayList
        Dim EffectiveDate As Date
        Dim tmpDate As Date
        Dim nTOPDays As Integer
        Dim DateStart As Date = DateSerial(Now.Year, Now.Month - 11, Now.Day)

        'To do : Limit Evaluated PO to a specific PO Date
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(DateStart, "yyyy/MM/dd")))

        arlPOH = objPOHFac.Retrieve(crtPOH)

        For Each objPOH As POHeader In arlPOH
            'Total TotalLiquefied 
            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100331
            'If IsHavingGyro(objPOH) Then
            '    'EffectiveDate = CType(objPOH.DailyPayments(0), DailyPayment).EffectiveDate
            '    EffectiveDate = ReqDeliveryDate ' assuming all dp is included in this procedure (it will be checked below)
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
            EffectiveDate = IIf(objPOH.IsHavingGyro, ReqDeliveryDate, objPOH.EffectiveDate)
            'End	:Optimize EffectiveDate calculation;By:DoniN;20100331
            If EffectiveDate >= ReportDate And EffectiveDate <= ReqDeliveryDate Then
                If objPOH.Status = 8 Then
                    If EffectiveDate >= ReportDate.AddDays(1) Then
                        Dim IsHavingGyro As Boolean = False
                        Dim TempAmount As Decimal = 0

                        Dim crtTotal As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crtTotal.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, objPOH.ID))
                        crtTotal.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
                        crtTotal.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.Exact, 0))
                        crtTotal.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.Exact, 0))
                        crtTotal.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, 0))
                        'crtTotal.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(" & CType(EnumPaymentStatus.PaymentStatus.Validasi, String) & "," & CType(EnumPaymentStatus.PaymentStatus.Selesai, String) & ")"))
                        crtTotal.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, String)))
                        crtTotal.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, DateSerial(Now.Year, Now.Month, Now.Day + 1)))
                        crtTotal.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, ReqDeliveryDate))
                        Dim aggregates As New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
                        'TotalLiquefied = TotalLiquefied + objDPFac.GetAggregateResult(aggregates, crtTotal)

                        TempAmount = objDPFac.GetAggregateResult(aggregates, crtTotal)
                        If TempAmount = 0 Then TempAmount = objPOH.TotalPODetail()

                        TotalLiquefied += TempAmount
                        'If objPOH.DailyPayments.Count = 0 Then
                        '    TotalLiquefied += objPOH.TotalPODetail()
                        'Else

                        '    'For Each objDP As DailyPayment In objPOH.DailyPayments
                        '    '    If (objDP.PaymentPurpose.ID = 3 Or objDP.PaymentPurpose.ID = 6) And objDP.IsReversed = 0 And objDP.IsCleared = 0 And objDP.EffectiveDate >= DateSerial(Now.Year, Now.Month, Now.Day + 1) And objDP.EffectiveDate <= ReqDeliveryDate Then
                        '    '        TotalLiquefied = TotalLiquefied + objDP.Amount
                        '    '        'sb.Append(objPOH.PONumber & vbTab & objPOH.Status & vbTab & objDP.Amount & vbTab & EffectiveDate.ToString("dd/MM/YYYY") & vbTab & "DPID=" & objDP.PaymentPurpose.ID & Chr(13))
                        '    '    End If
                        '    'Next
                        'End If
                    End If
                Else
                    TotalLiquefied += objPOH.TotalPODetail()
                End If
            End If
            'End Total TotalLiquefied
        Next
        Return TotalLiquefied
    End Function


    Private Function AddWorkingDay(ByVal StateDate As Date, ByVal nAdded As Integer, Optional ByVal IsBackWard As Boolean = False) As Date
        Dim objNHFac As NationalHolidayFacade = New NationalHolidayFacade(User)
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
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
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

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Ceiling_master_lihat_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Sales Credit Control - Ceiling Master")
        End If
        btnSaveMaxTOPDate.Visible = SecurityProvider.Authorize(Context.User, SR.ceiling_master_tgl_validasi_simpan_privilege)
        Dim Idx As Integer = CommonFunction.GetColumnIndexOfDTG(Me.dtgMain, "Tanggal Validitas")
        If Idx >= 0 Then
            Me.dtgMain.Columns(Idx).Visible = SecurityProvider.Authorize(Context.User, SR.ceiling_master_tgl_validasi_lihat_privilege)
        End If
    End Sub

#End Region


#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 500
        ActivateUserPrivilege()
        If Not IsPostBack Then
            Initialization()
            If Not sHelper.GetSession("FrmCeiling.IsAutoBind") Is Nothing Then
                If CType(sHelper.GetSession("FrmCeiling.IsAutoBind"), Boolean) = True Then
                    txtCreditAccount.Text = CType(sHelper.GetSession("FrmCeiling.CreditAccount"), String)
                    Me.icReqDelivery.Value = CType(sHelper.GetSession("FrmCeiling.ReqDeliveryDate"), Date)
                    txtReportDate.Text = sHelper.GetSession("FrmCeiling.ReportDate")
                    BindDTG(CType(sHelper.GetSession("FrmCeiling.PageIndex"), Integer))
                End If
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If CDate(txtReportDate.Text) > Me.icReqDelivery.Value Then
            MessageBox.Show("Tanggal permintaan kirim tidak boleh kurang dari Tanggal Laporan")
            Exit Sub
        End If
        If txtCreditAccount.Text.Trim <> "" Then
            'If Not txtCreditAccount.Text.Trim.IsInterned(";") Then
            Dim objD As Dealer = New DealerFacade(User).Retrieve(txtCreditAccount.Text.Trim)
            If Not objD Is Nothing Then
                If objD.ID > 0 Then
                    txtDealerName.Text = objD.DealerName
                End If
            End If
            'Else
            '    txtDealerName.Text = String.Empty
            'End If
        End If
        'sb = New System.Text.StringBuilder

        BindDTG(0)


        'Response.Write(sb.ToString.Replace(Chr(13), "<br>"))


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
        BindDTG(0)
    End Sub


    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType")
            Dim lblPlafon As Label = e.Item.FindControl("lblPlafon")
            Dim lblOutStanding As Label = e.Item.FindControl("lblOutStanding")
            Dim lblAvailablePlafon As Label = e.Item.FindControl("lblAvailablePlafon")
            Dim lblInProcess As Label = e.Item.FindControl("lblInProcess")
            Dim lblRemainPlafon1 As Label = e.Item.FindControl("lblRemainPlafon1")
            Dim lblRemainPlafon As Label = e.Item.FindControl("lblRemainPlafon")
            Dim lblPOInPropose As Label = e.Item.FindControl("lblPOInPropose")
            Dim lblNewPOPlafon As Label = e.Item.FindControl("lblNewPOPlafon")
            Dim lblLiquefiedPO As Label = e.Item.FindControl("lblLiquefiedPO")
            Dim lblAcceleratedGyro As Label = e.Item.FindControl("lblAcceleratedGyro")
            Dim lblKeterangan As Label = e.Item.FindControl("lblKeterangan")
            Dim calMaxTOPDate As Intimedia.WebCC.IntiCalendar = e.Item.FindControl("calMaxTOPDate")
            Dim PaymentType As Integer

            If viewstate.Item("IsSpanning") = "1" Then
                If (e.Item.ItemIndex + 3) Mod 3 = 0 Then
                    lblNo.Text = (e.Item.ItemIndex + 3) / 3
                End If
            Else
                lblNo.Text = e.Item.ItemIndex + 1 + (dtgMain.PageSize * (dtgMain.CurrentPageIndex))
            End If
            PaymentType = CType(lblPaymentType.Text, Integer)
            If CType(lblPaymentType.Text, Integer) = enumPaymentType.PaymentType.COD Then
                Dim TotCair As Decimal = CommonFunction.GetPOCair(lblCreditAccount.Text, enumPaymentType.PaymentType.COD, Now.AddDays(1), Me.icReqDelivery.Value)
                'lblLiquefiedPO.Text = FormatNumber(GetLiquefiedPO(lblCreditAccount.Text, CType(lblPaymentType.Text, Short)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblLiquefiedPO.Text = FormatNumber(TotCair, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPaymentType.Text = "COD"
                lblPlafon.Text = FormatNumber(lblPlafon.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblOutStanding.Text = FormatNumber(lblOutStanding.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAvailablePlafon.Text = FormatNumber(lblPlafon.Text - lblOutStanding.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPOInPropose.Text = FormatNumber(lblPOInPropose.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAcceleratedGyro.Text = "-"
                lblKeterangan.Text = ""
                calMaxTOPDate.Visible = False
            ElseIf CType(lblPaymentType.Text, Integer) = enumPaymentType.PaymentType.TOP Then
                Dim TotCair As Decimal = CommonFunction.GetPOCair(lblCreditAccount.Text, enumPaymentType.PaymentType.TOP, Now.AddDays(1), Me.icReqDelivery.Value)
                lblLiquefiedPO.Text = FormatNumber(TotCair, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'lblLiquefiedPO.Text = FormatNumber(GetLiquefiedPO(lblCreditAccount.Text, CType(lblPaymentType.Text, Short)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                If CType(viewstate.Item("IsCalcAccelaratedGyro"), Boolean) Then
                    lblAcceleratedGyro.Text = FormatNumber(GetAcceleratedGyro(lblCreditAccount.Text, CType(lblPaymentType.Text, Short)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Else
                    lblAcceleratedGyro.Text = "0"
                End If
                lblPaymentType.Text = "TOP"
                lblPlafon.Text = FormatNumber(lblPlafon.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblOutStanding.Text = FormatNumber(lblOutStanding.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAvailablePlafon.Text = FormatNumber(lblPlafon.Text - lblOutStanding.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPOInPropose.Text = FormatNumber(lblPOInPropose.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblKeterangan.Text = ""
                Dim oSCM As sp_CreditMaster = CType(CType(Me.sHelper.GetSession("FrmCeiling.arlSCM"), ArrayList)(e.Item.ItemIndex), sp_CreditMaster)
                calMaxTOPDate.Value = IIf(oSCM.MaxTOPDate.Year < 1900, DateSerial(1900, 1, 1), oSCM.MaxTOPDate)
                calMaxTOPDate.Visible = True
                calMaxTOPDate.Enabled = (CType(Session("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB)

                If CType(lblPlafon.Text.Trim, Decimal) <> 0 Then
                    If oSCM.MaxTOPDate < Date.Now Then
                        lblKeterangan.Text = "Tanggal validitas < tanggal hari ini"
                    ElseIf oSCM.MaxTOPDate >= Date.Now And oSCM.MaxTOPDate < Date.Now.AddDays(42) Then
                        lblKeterangan.Text = "Tanggal validitas kurang dari 6 minggu."
                    End If
                End If
            ElseIf CType(lblPaymentType.Text, Integer) = enumPaymentType.PaymentType.RTGS Then
                lblLiquefiedPO.Text = ""  ' FormatNumber("0", 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPaymentType.Text = "RTGS"
                lblPlafon.Text = "" 'FormatNumber("0", 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblOutStanding.Text = "" ' FormatNumber("0", 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAvailablePlafon.Text = "" 'FormatNumber("0", 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

                lblPOInPropose.Text = FormatNumber(lblPOInPropose.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAcceleratedGyro.Text = "-"
                lblKeterangan.Text = ""
                calMaxTOPDate.Visible = False
            End If

            If lblPaymentType.Text = "RTGS" Then
                lblRemainPlafon.Text = "" '"0"
            Else
                If lblPaymentType.Text.Trim.ToUpper = "COD" Then
                    lblRemainPlafon.Text = FormatNumber((lblAvailablePlafon.Text - lblPOInPropose.Text) + lblLiquefiedPO.Text + 0, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Else
                    lblRemainPlafon.Text = FormatNumber((lblAvailablePlafon.Text - lblPOInPropose.Text) + lblLiquefiedPO.Text + lblAcceleratedGyro.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                End If
                'lblRemainPlafon.Text = FormatNumber(GetRemainCeiling(lblAvailablePlafon.Text, lblCreditAccount.Text, PaymentType, CDate(txtReportDate.Text), icReqDelivery.Value))
            End If
        End If
    End Sub

    Private Function GetAcceleratedGyro(ByVal CreditAccount As String, ByVal PaymentType As Short) As Decimal
        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim crtDP As CriteriaComposite
        Dim arlDP As ArrayList
        Dim TotalAcceleratedGyro As Decimal = 0
        Dim TodayDate As Date = Now
        Dim ReqDelDate As Date = icReqDelivery.Value
        Dim sqlAccGyro As String = "select dp.ID from DailyPayment dp inner join DailyPayment dp2 on dp2.ID=dp.AcceleratorID and dp2.GyroType=" & CType(EnumGyroType.GyroType.Percepatan, Integer).ToString

        'Accelerated Gyro
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "ID", MatchType.InSet, "(" & sqlAccGyro & ")"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.GreaterOrEqual, TodayDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.LesserOrEqual, ReqDelDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(" & CType(EnumPaymentStatus.PaymentStatus.Validasi, String) & "," & CType(EnumPaymentStatus.PaymentStatus.Selesai, String) & ")"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.No, 1))

        arlDP = objDPFac.Retrieve(crtDP)
        TotalAcceleratedGyro = 0
        Dim aggregates As New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
        TotalAcceleratedGyro += objDPFac.GetAggregateResult(aggregates, crtDP)

        'For Each oDP As DailyPayment In arlDP
        '    TotalAcceleratedGyro += oDP.Amount
        'Next
        Return TotalAcceleratedGyro
    End Function

#Region "RemainCeiling"

    Private Function GetRemainCeiling(ByVal AvCeiling As Decimal, ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
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
    Private Function GetReqPO(ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal

        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As New ArrayList
        Dim Sql As String = ""
        Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim Total As Decimal = 0

        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, StartDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EndDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0 and IsReversed=1 and IsCleared<>1)"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next
        Return Total
    End Function
    Private Function GetTotalPODetail(ByRef objPOH As POHeader) As Decimal
        Dim Total As Decimal = 0

        Total = objPOH.TotalPODetail()
        'For Each objPOD As PODetail In objPOH.PODetails
        '    If objPOH.Status = 0 Or objPOH.Status = 2 Then
        '        Total = Total + (objPOD.ReqQty * objPOD.Price)
        '    ElseIf objPOH.Status = 4 Or objPOH.Status = 6 Or objPOH.Status = 8 Then
        '        Total = Total + (objPOD.AllocQty * objPOD.Price)
        '    End If
        'Next
        Return Total
    End Function
    Private Function GetPOCair(ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList
        Dim crtPOH As CriteriaComposite
        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        'Dim arlDP As ArrayList
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
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))

        Dim aggregates As New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
        Total += objDPFac.GetAggregateResult(aggregates, crtDP)

        'arlDP = objDPFac.Retrieve(crtDP)
        'For Each objDP As DailyPayment In arlDP
        '    Total += objDP.Amount
        'Next

        'Not Accelerated
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, "0"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))

        aggregates = New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
        Total += objDPFac.GetAggregateResult(aggregates, crtDP)

        'arlDP = objDPFac.Retrieve(crtDP)
        'For Each objDP As DailyPayment In arlDP
        '    Total += objDP.Amount
        'Next

        'doesn't have gyro
        StartDate = AddWorkingDay(StartDate, -2, True)
        EndDate = AddWorkingDay(EndDate, -2, True)
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0 and IsReversed<>1 and IsCleared<>1)"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Dealer.CreditAccount", MatchType.Exact, CreditAccount))
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

    Private Sub dtgMain_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMain.PageIndexChanged
        BindDTG(e.NewPageIndex)
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        If e.CommandName = "Detail" Then
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType")
            Dim lblLiquefiedPO As Label = e.Item.FindControl("lblLiquefiedPO")

            'Make Session with current criteria/parameters
            sHelper.SetSession("FrmCeiling.IsAutoBind", True)
            sHelper.SetSession("FrmCeiling.CreditAccount", txtCreditAccount.Text)
            sHelper.SetSession("FrmCeiling.ReqDeliveryDate", Me.icReqDelivery.Value)
            sHelper.SetSession("FrmCeiling.ReportDate", txtReportDate.Text)
            sHelper.SetSession("FrmCeiling.PageIndex", dtgMain.CurrentPageIndex)
            Dim sLiquified As String = lblLiquefiedPO.Text
            If sLiquified.Trim = String.Empty Then sLiquified = "0"
            sHelper.SetSession("FrmCeilingDetail.LiquefiedPO", sLiquified)
            'lblLiquefiedPO

            Response.Redirect("FrmCeilingDetail.aspx?CreditAccount=" & lblCreditAccount.Text & "&PaymentType=" & (New enumPaymentType).GetEnumValue(lblPaymentType.Text) & "&ReportDate=" & txtReportDate.Text & "&ReqDeliveryDate=" & icReqDelivery.Value)
        End If
    End Sub

    Private Sub btnSaveMaxTOPDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveMaxTOPDate.Click
        Dim arlCM As ArrayList = CType(Me.sHelper.GetSession("FrmCeiling.arlSCM"), ArrayList)
        Dim oCMFac As CreditMasterFacade = New CreditMasterFacade(User)
        Dim oCM As CreditMaster
        Dim oSCM As sp_CreditMaster
        Dim nError As Integer = 0
        Dim nSaved As Integer = 0

        For Each di As DataGridItem In Me.dtgMain.Items
            Dim calMaxTOPDate As Intimedia.WebCC.IntiCalendar = di.FindControl("calMaxTOPDate")
            Dim lblCreditAccount As Label = di.FindControl("lblCreditAccount")
            Dim lblKeterangan As Label = di.FindControl("lblKeterangan")
            Dim lblPaymentType As Label = di.FindControl("lblPaymentType")
            Dim isAllow As Boolean = False

            lblKeterangan.Text = String.Empty
            If lblPaymentType.Text = "TOP" Then
                If calMaxTOPDate.Value < Date.Now Then
                    lblKeterangan.Text = "Simpan gagal, tanggal validitas < tanggal hari ini"
                    nError += 1
                ElseIf calMaxTOPDate.Value >= Date.Now And calMaxTOPDate.Value < Date.Now.AddDays(42) Then
                    lblKeterangan.Text = "Simpan berhasil, tanggal validitas kurang dari 6 minggu."
                    isAllow = True
                Else
                    isAllow = True
                End If
                If isAllow Then
                    oSCM = CType(arlCM(di.ItemIndex), sp_CreditMaster)
                    If Not IsNothing(oSCM) AndAlso oSCM.ID > 0 Then
                        oCM = oCMFac.Retrieve(oSCM.ID)
                        oCM.MaxTOPDate = calMaxTOPDate.Value

                        If oCMFac.Update(oCM) = -1 Then
                            nError += 1
                        Else
                            nSaved += 1
                        End If
                    End If
                End If
            End If

        Next
        If arlCM.Count > 0 Then
            If nSaved > 0 Then
                MessageBox.Show(SR.SaveSuccess & IIf(nError > 0, ". " & nError & " data gagal disimpan", ""))
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub

#End Region

End Class


