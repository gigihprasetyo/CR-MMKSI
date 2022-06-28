
#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.Pk
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class PopUpRedemptionDetail2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblVehicle As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents dgSPDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblCeiling As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodCeiling As System.Web.UI.WebControls.Label
    Protected WithEvents lblCeilingTOP As System.Web.UI.WebControls.Label
    Protected WithEvents lblCeilingCOD As System.Web.UI.WebControls.Label
    Protected WithEvents lblCeilingRTGS As System.Web.UI.WebControls.Label
    Protected WithEvents lblPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalQty As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPrice As System.Web.UI.WebControls.Label
    Protected WithEvents btnCalculate As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotJK As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalJK As System.Web.UI.WebControls.Label
    Protected WithEvents txtCeilingInfo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblInfoLock As System.Web.UI.WebControls.Label
    Protected WithEvents btnSet As System.Web.UI.WebControls.Button
    Protected WithEvents lblStatusLock As System.Web.UI.WebControls.Label
    Protected WithEvents imgSet As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents imgStatus As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents txtsDay As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtsTotRow As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtsTotCol As System.Web.UI.WebControls.TextBox

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
    Private ses_arlRedemptionDetail As String = "PopUpRedemptionDetail.arlRedemptionDetail"
    Private vst_DealerCode As String = "PopUpRedemptionDetail.DealerCode"
    Private vst_RowIndex As String = "RowIndex"
    Private vst_DayIndex As String = "DayIndex"
    Private vst_ID As String = "vst_ID"
    Private vst_Locked As String = "vst_Locked"
    Private objRH As RedemptionHeader

    Private _sessCategoryCode As String = "FrmEntryRedemptionPlan._sessCategoryCode"
    Private _sessSubCategoryCode As String = "FrmEntryRedemptionPlan._sessSubCategoryCode"
#End Region

#Region "Custom Methods"

    Private Sub CheckPrivilege()
        Dim oDealer As Dealer = CType(Session.Item("DEALER"), Dealer)

        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Me.btnSet.Enabled = SecurityProvider.Authorize(Context.User, SR.respon_redemption_plan_freeze_privilege)
        End If
    End Sub

    Private Sub Initialization()
        If IsNothing(Request.Item("RowIndex")) OrElse IsNothing(Request.Item("DayIndex")) Then
            Response.Write("<script>window.close();</script>")
            Exit Sub
        End If
        viewstate.Add(vst_DealerCode, Request.Item("DealerCode"))
        viewstate.Add(vst_RowIndex, Request.Item("RowIndex"))
        viewstate.Add(vst_DayIndex, Request.Item("DayIndex"))
        viewstate.Add(Me.vst_ID, Request.Item("ID"))
        sHelper.SetSession(ses_arlRedemptionDetail, New ArrayList)
        BindHeader()
        BindDtg(False)
        Dim objD As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)

        If objD.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            Me.btnSet.Visible = True
        Else
            Me.btnSet.Visible = False
        End If
        If IsStillOpen() = False Then
            Me.btnSet.Visible = False
            Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = False
        End If
        'IsAuto
        If Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = True Then
            If Not IsNothing(Request.Item("IsAuto")) Then
                Dim IsAlreadyDistributed As Boolean = False
                If Request.Item("IsAuto") = "1" Then
                    IsAlreadyDistributed = True
                End If
                If IsAlreadyDistributed Then
                    Me.btnSet.Visible = False
                    Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub BindHeader()
        objRH = GetRedemptionHeader()
        Dim objRCFac As RedemptionCeilingFacade = New RedemptionCeilingFacade(User)
        Dim arlRC As New ArrayList
        Dim crtRC As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objD As Dealer = New DealerFacade(User).Retrieve(viewstate.Item(vst_DealerCode))

        lblVehicle.Text = objRH.VechileColor.VechileType.VechileTypeCode & " - " & objRH.VechileColor.ColorCode
        lblPeriod.Text = Format(objRH.PeriodDate, "dd MMMM yyyy")
        lblPrice.Text = FormatNumber(GetVehicleAmount(), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        UpdateCeilingPosition()
    End Sub

    Private Sub UpdateCeilingPosition()
        objRH = GetRedemptionHeader()
        Dim objD As Dealer = New DealerFacade(User).Retrieve(viewstate.Item(vst_DealerCode))

        txtCeilingInfo.Text = ""
        lblCeilingCOD.Text = FormatNumber(GetRedCeiling(objD.CreditAccount, CType(enumPaymentType.PaymentType.COD, Integer), objRH.PeriodDate), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblCeilingTOP.Text = FormatNumber(GetRedCeiling(objD.CreditAccount, CType(enumPaymentType.PaymentType.TOP, Integer), objRH.PeriodDate), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblCeilingRTGS.Text = FormatNumber(GetRedCeiling(objD.CreditAccount, CType(enumPaymentType.PaymentType.RTGS, Integer), objRH.PeriodDate), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

    End Sub

    Private Function GetRedCeiling(ByVal pCreditAccount As String, ByVal pPaymentType As Integer, ByVal pDate As Date) As Decimal
        Dim oSRCFac As sp_RedemptionCeilingFacade = New sp_RedemptionCeilingFacade(User)
        Dim arlSRC As New ArrayList
        Dim oSRc As sp_RedemptionCeiling
        Dim Ceiling As Decimal
        Dim strTemp As String = ""
        Dim oSRcNext As sp_RedemptionCeiling

        arlSRC = oSRCFac.RetrieveFromSP(pCreditAccount, pPaymentType, pDate)
        If arlSRC.Count > 0 Then
            oSRc = CType(arlSRC(0), sp_RedemptionCeiling)
            Dim TotPropTillToDay As Decimal = 0
            arlSRC = oSRCFac.RetrieveFromSP(pCreditAccount, pPaymentType, pDate.AddDays(1))
            If arlSRC.Count > 0 Then
                oSRcNext = CType(arlSRC(0), sp_RedemptionCeiling)
                TotPropTillToDay = oSRcNext.TotalProposed
            End If
            Ceiling = oSRc.InitialCeiling + oSRc.TotalLiquified - TotPropTillToDay 'oSRc.TotalProposed
            strTemp = "PaymentType=" & pPaymentType & "Init =" & oSRc.InitialCeiling & ";Liquified=" & oSRc.TotalLiquified & ";Proposed=" & oSRc.TotalProposed
        Else
            Ceiling = 0
        End If
        txtCeilingInfo.Text &= "." & strTemp
        If pPaymentType = CType(enumPaymentType.PaymentType.RTGS, Integer) Then
            Ceiling = 0
        End If
        Return Ceiling
    End Function

    Private Function GetRedemptionHeader() As RedemptionHeader
        'Dim arlDataToDisplay As ArrayList = sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        'Dim oVR As v_Redemption = CType(arlDataToDisplay(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption)
        'Dim oDV As FrmEntryRedemptionPlan.DealerVehicle = CType(arlDataToDisplay(CType(viewstate.Item(vst_RowIndex), Integer)), FrmEntryRedemptionPlan.DealerVehicle)

        'Return oDV.RedemptionHeaders(CType(viewstate.Item(vst_DayIndex), Integer))

        'Dim ID As Integer = CType(viewstate.Item(Me.vst_ID), Integer)
        'Dim oRH As RedemptionHeader = New RedemptionHeaderFacade(User).Retrieve(ID)
        'If IsNothing(oRH) Then oRH = New RedemptionHeader
        'Return oRH

        Dim oRH As RedemptionHeader
        Dim arlDataToDisplay As ArrayList = sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        Dim oVR As v_Redemption = CType(arlDataToDisplay(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption)
        oRH = oVR.RedemptionHeaders(CType(Me.ViewState.Item(Me.vst_DayIndex), Integer))

        Return oRH
    End Function

    Private Function IsRespondValid(ByVal RowIndex As Integer, ByVal DayIndex As Integer) As Boolean
        Dim arlDataToDisplay As ArrayList = sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        Dim objVR As v_Redemption = CType(arlDataToDisplay(RowIndex), v_Redemption)
        Dim TotalRespond As Integer = 0
        Dim TotalStock As Integer = Me.GetRedemptionHeader().EstimationStock  ' CType(objDV.RedemptionHeaders(DayIndex), RedemptionHeader).EstimationStock
        Dim i As Integer
        Dim oVR As v_Redemption

        For i = 0 To arlDataToDisplay.Count - 1
            oVR = CType(arlDataToDisplay(i), v_Redemption)
            If oVR.VechileColorID = objVR.VechileColorID Then
                TotalRespond += GetRHByDay(oVR, DayIndex).TotalRespond(oVR.DealerID)
            End If
        Next
        'For Each oDV As FrmEntryRedemptionPlan.DealerVehicle In arlDataToDisplay
        '    If oDV.VechileColor.ID = objDV.VechileColor.ID Then
        '        TotalRespond += CType(oDV.RedemptionHeaders(DayIndex), RedemptionHeader).TotalRespond(oDV.Dealer.ID)
        '    End If
        'Next
        Return IIf(TotalRespond > TotalStock, False, True)
    End Function

    Private Function GetRHByDay(ByRef oVR As v_Redemption, ByVal DayIdx As Integer) As RedemptionHeader
        Dim ID As Integer
        Dim oRH As RedemptionHeader

        Select Case (DayIdx + 1)
            Case 1 : ID = oVR.RH1
            Case 2 : ID = oVR.RH2
            Case 3 : ID = oVR.RH3
            Case 4 : ID = oVR.RH4
            Case 5 : ID = oVR.RH5
            Case 6 : ID = oVR.RH6
            Case 7 : ID = oVR.RH7
            Case 8 : ID = oVR.RH8
            Case 9 : ID = oVR.RH9
            Case 10 : ID = oVR.RH10
            Case 11 : ID = oVR.RH11
            Case 12 : ID = oVR.RH12
            Case 13 : ID = oVR.RH13
            Case 14 : ID = oVR.RH14
            Case 15 : ID = oVR.RH15
            Case 16 : ID = oVR.RH16
            Case 17 : ID = oVR.RH17
            Case 18 : ID = oVR.RH18
            Case 19 : ID = oVR.RH19
            Case 20 : ID = oVR.RH20
            Case 21 : ID = oVR.RH21
            Case 22 : ID = oVR.RH22
            Case 23 : ID = oVR.RH23
            Case 24 : ID = oVR.RH24
            Case 25 : ID = oVR.RH25
            Case 26 : ID = oVR.RH26
            Case 27 : ID = oVR.RH27
            Case 28 : ID = oVR.RH28
            Case 29 : ID = oVR.RH29
            Case 30 : ID = oVR.RH30
            Case 31 : ID = oVR.RH31
        End Select
        oRH = New RedemptionHeaderFacade(User).Retrieve(ID)
        If IsNothing(oRH) Then oRH = New RedemptionHeader
        Return oRH
    End Function

    Private Sub UpdateRedemptionDetail()
        'Dim arlDataToDisplay As ArrayList = sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        'Dim oDV As FrmEntryRedemptionPlan.DealerVehicle = CType(arlDataToDisplay(CType(viewstate.Item(vst_RowIndex), Integer)), FrmEntryRedemptionPlan.DealerVehicle)

        'CType(oDV.RedemptionHeaders(CType(viewstate.Item(vst_DayIndex), Integer)), RedemptionHeader).RedemptionDetails = sHelper.GetSession(ses_arlRedemptionDetail)

    End Sub

    Private Sub BindDtg(Optional ByVal IsFromSession As Boolean = False)
        Dim objRDFac As RedemptionDetailFacade = New RedemptionDetailFacade(User)
        Dim arlRD As New ArrayList
        Dim objD As Dealer = New DealerFacade(User).Retrieve(viewstate.Item(vst_DealerCode))

        UpdateCeilingPosition()
        If IsFromSession Then ' AndAlso CType(sHelper.GetSession("DEALER"), Dealer).Title <> CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            arlRD = sHelper.GetSession(ses_arlRedemptionDetail)
        Else
            arlRD = GetRedemptionHeader.RedemptionDetails(objD.ID)
            sHelper.SetSession(ses_arlRedemptionDetail, arlRD)
        End If
        UpdateRedemptionDetail()
        dtgMain.DataSource = arlRD
        dtgMain.DataBind()
        btnCalculate_Click(Nothing, Nothing)
    End Sub

    Private Sub FillDtg(ByRef e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            FillRowItem(e)
        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            FillRowEdit(e)
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim objD As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)

            If objD.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                e.Item.Visible = False
            End If

            FillRowFooter(e)
        End If
    End Sub

    Private Sub FillRowItem(ByRef e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objRD As RedemptionDetail = CType(CType(sHelper.GetSession(ses_arlRedemptionDetail), ArrayList)(e.Item.ItemIndex), RedemptionDetail)
        Dim Total As Decimal = objRD.RequestQty * lblPrice.Text
        Dim Ceiling As Decimal = 0

        If e.Item.ItemIndex = 0 Then
            Me.ViewState.Add(Me.vst_Locked, (objRD.IsManualAlloc = 1))
            Me.lblInfoLock.Text = IIf(CType(Me.ViewState.Item(Me.vst_Locked), Boolean), "Buka dari perhitungan otomatis", "Kunci dari perhitungan otomatis")
            Me.imgSet.Src = "../images/" & IIf(CType(Me.ViewState.Item(Me.vst_Locked), Boolean), "lock.gif", "unlock.gif")
            Me.lblStatusLock.Text = IIf(CType(Me.ViewState.Item(Me.vst_Locked), Boolean), "TERKUNCI", "TERBUKA")
            Me.imgStatus.Src = "../images/" & IIf(CType(Me.ViewState.Item(Me.vst_Locked), Boolean), "unlock.gif", "lock.gif")

            Me.btnSet.Text = IIf(CType(Me.ViewState.Item(Me.vst_Locked), Boolean), "UnFreeze", "Freeze") ' Me.lblInfoLock.Text
        End If

        CType(e.Item.FindControl("lblTOP"), Label).Text = objRD.TermOfPayment.Description
        CType(e.Item.FindControl("lblQty"), Label).Text = objRD.RequestQty
        CType(e.Item.FindControl("lblJK"), Label).Text = objRD.RespondQty
        CType(e.Item.FindControl("lblTotal"), Label).Text = FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        If objRD.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
            Ceiling = lblCeilingTOP.Text
        ElseIf objRD.TermOfPayment.PaymentType = enumPaymentType.PaymentType.COD Then
            Ceiling = lblCeilingCOD.Text
        ElseIf objRD.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then
            Ceiling = 0 ' lblCeilingRTGS.Text
        End If
        CType(e.Item.FindControl("lblCeiling"), Label).Text = FormatNumber(Ceiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        Dim objD As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)

        If objD.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
            lbtnDelete.Visible = False
        End If
        Dim sInfo As String = String.Empty
        sInfo &= "Sequence : " & objRD.Sequence.ToString & vbCrLf
        sInfo &= "Ceiling : " & FormatNumber(objRD.Ceiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & vbCrLf
        sInfo &= "Stok : " & objRD.Stock.ToString & vbCrLf

        CType(e.Item.FindControl("lblInfo"), Label).Text = sInfo
    End Sub

    Private Function GetVehicleAmount() As Decimal
        Dim objCDFac As ContractDetailFacade = New ContractDetailFacade(User)
        Dim crtCD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlCD As New ArrayList

        crtCD.opAnd(New Criteria(GetType(ContractDetail), "VechileColor.ID", MatchType.Exact, objRH.VechileColor.ID))
        crtCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.ContractPeriodYear", MatchType.Exact, objRH.PeriodDate.Year))
        crtCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.ContractPeriodMonth", MatchType.Exact, objRH.PeriodDate.Month))
        arlCD = objCDFac.Retrieve(crtCD)
        If arlCD.Count > 0 Then
            Return CType(arlCD(0), ContractDetail).Amount
        Else
            Return 0
        End If
    End Function

    Private Sub FillRowEdit(ByRef e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objRD As RedemptionDetail = CType(CType(sHelper.GetSession(ses_arlRedemptionDetail), ArrayList)(e.Item.ItemIndex), RedemptionDetail)
        Dim Total As Decimal = objRD.RequestQty * lblPrice.Text
        Dim Ceiling As Decimal = 0

        BindDdlTOP(CType(e.Item.FindControl("ddlEditTOP"), DropDownList))
        CType(e.Item.FindControl("ddlEditTOP"), DropDownList).SelectedValue = objRD.TermOfPayment.ID
        CType(e.Item.FindControl("txtEditQty"), TextBox).Text = objRD.RequestQty
        CType(e.Item.FindControl("txtEditJK"), TextBox).Text = objRD.RespondQty
        CType(e.Item.FindControl("lblEditTotal"), Label).Text = FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        If objRD.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
            Ceiling = lblCeilingTOP.Text
        ElseIf objRD.TermOfPayment.PaymentType = enumPaymentType.PaymentType.COD Then
            Ceiling = lblCeilingCOD.Text
        ElseIf objRD.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then
            Ceiling = 0 ' lblCeilingRTGS.Text
        End If
        CType(e.Item.FindControl("lblEditCeiling"), Label).Text = FormatNumber(Ceiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        Dim objD As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        If objD.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            CType(e.Item.FindControl("txtEditQty"), TextBox).Enabled = False
            CType(e.Item.FindControl("txtEditJK"), TextBox).Enabled = True
            CType(e.Item.FindControl("ddlEditTOP"), DropDownList).Enabled = False
        Else
            CType(e.Item.FindControl("txtEditQty"), TextBox).Enabled = True
            CType(e.Item.FindControl("txtEditJK"), TextBox).Enabled = False
            CType(e.Item.FindControl("ddlEditTOP"), DropDownList).Enabled = True
        End If
    End Sub

    Private Sub FillRowFooter(ByRef e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim Ceiling As Decimal = 0

        BindDdlTOP(CType(e.Item.FindControl("ddlFooterTOP"), DropDownList))

        CType(e.Item.FindControl("txtFooterQty"), TextBox).Text = 0
        CType(e.Item.FindControl("txtFooterJK"), TextBox).Text = 0
        CType(e.Item.FindControl("lblFooterTotal"), Label).Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        If CType(e.Item.FindControl("ddlFooterTOP"), DropDownList).SelectedValue = enumPaymentType.PaymentType.TOP Then
            Ceiling = lblCeilingTOP.Text
        ElseIf CType(e.Item.FindControl("ddlFooterTOP"), DropDownList).SelectedValue = enumPaymentType.PaymentType.COD Then
            Ceiling = lblCeilingCOD.Text
        ElseIf CType(e.Item.FindControl("ddlFooterTOP"), DropDownList).SelectedValue = enumPaymentType.PaymentType.RTGS Then
            Ceiling = lblCeilingRTGS.Text
        End If
        CType(e.Item.FindControl("lblFooterCeiling"), Label).Text = FormatNumber(Ceiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Dim objD As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        If objD.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            CType(e.Item.FindControl("txtFooterQty"), TextBox).Enabled = False
            CType(e.Item.FindControl("txtFooterJK"), TextBox).Enabled = True
        Else
            CType(e.Item.FindControl("txtFooterQty"), TextBox).Enabled = True
            CType(e.Item.FindControl("txtFooterJK"), TextBox).Enabled = False
        End If
    End Sub

    Private Sub BindDdlTOP(ByRef ddl As DropDownList)
        Dim objTOPFac As TermOfPaymentFacade = New TermOfPaymentFacade(User)
        Dim crtTOP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlTOP As New ArrayList

        ddl.Items.Clear()
        arlTOP = objTOPFac.Retrieve(crtTOP)
        For Each oTOP As TermOfPayment In arlTOP
            ddl.Items.Add(New ListItem(oTOP.Description, oTOP.ID))
        Next
    End Sub

    Private Sub DTGUpdate(ByVal e As DataGridCommandEventArgs)
        Dim arlRedemptionDetail As ArrayList = CType(sHelper.GetSession(ses_arlRedemptionDetail), ArrayList)
        Dim objRD As RedemptionDetail = CType(CType(sHelper.GetSession(ses_arlRedemptionDetail), ArrayList)(e.Item.ItemIndex), RedemptionDetail)
        Dim objD As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        Dim objRDFac As RedemptionDetailFacade = New RedemptionDetailFacade(User)
        Dim objRDOld As RedemptionDetail = objRDFac.Retrieve(objRD.ID)
        Dim IsValid As Boolean = True
        Dim nOriReq As Integer
        Dim nOriRes As Integer

        objRH = Me.GetRedemptionHeader()
        If objD.Title <> CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            nOriReq = objRD.RequestQty
            objRD.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(CType(e.Item.FindControl("ddlEditTOP"), DropDownList).SelectedValue, Integer))
            objRD.RequestQty = CType(e.Item.FindControl("txtEditQty"), TextBox).Text
            objRD.RedemptionHeader = objRH
            objRD.Dealer = objD

            If Not IsRequestEnough(objRD) Then
                IsValid = False
                objRDFac.Update(objRDOld)
                MessageBox.Show("Jumlah unit rencana Dealer melebihi jumlah contract")
            End If
            If IsValid AndAlso Not IsCeilingEnough(objRD, objD) Then
                IsValid = False
                objRDFac.Update(objRDOld)
                MessageBox.Show("Ceiling tidak tersedia")
            End If
            If IsValid Then
                objRDFac.Update(objRD)
                arlRedemptionDetail(e.Item.ItemIndex) = objRD
                sHelper.SetSession(ses_arlRedemptionDetail, arlRedemptionDetail)

                objRH.RedemptionDetails = arlRedemptionDetail
                Dim arl As ArrayList = CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)
                Dim oVR As v_Redemption = CType(arl(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption)

                oVR.RedemptionHeaders(CType(Me.ViewState.Item(Me.vst_DayIndex), Integer)) = objRH
                Dim oFERP As New FrmEntryRedemptionPlan2
                oFERP.UpdateTotalQtyPublic(oVR, CType(Me.ViewState.Item(Me.vst_DayIndex), Integer), 0, objRD.RequestQty - nOriReq, 0)
                arl(CType(viewstate.Item(vst_RowIndex), Integer)) = oVR

                sHelper.SetSession("FrmEntryRedemptionPlan.arlDataToDisplay", arl)
                'CType(CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption).RedemptionHeaders(CType(Me.ViewState.Item(Me.vst_DayIndex), Integer)) = objRH
                'Dim oFERP As New FrmEntryRedemptionPlan2
                'oFERP.UpdateTotalQtyPublic(CType(CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption), CType(Me.ViewState.Item(Me.vst_DayIndex), Integer), 0, objRD.RequestQty - nOriReq, 0)

            End If
        Else
            Dim OldRespond As Integer = objRD.RespondQty

            objRD.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(CType(e.Item.FindControl("ddlEditTOP"), DropDownList).SelectedValue, Integer))
            objRD.RequestQty = CType(e.Item.FindControl("txtEditQty"), TextBox).Text
            objRD.RedemptionHeader = objRH
            objRD.RespondQty = CType(e.Item.FindControl("txtEditJK"), TextBox).Text
            objRDFac.Update(objRD)
            arlRedemptionDetail(e.Item.ItemIndex) = objRD
            sHelper.SetSession(ses_arlRedemptionDetail, arlRedemptionDetail)
            If Not IsRespondValid(CType(viewstate.Item(Me.vst_RowIndex), Integer), CType(viewstate.Item(Me.vst_DayIndex), Integer)) Then
                MessageBox.Show("Jumlah respond melebihi estimasi stok")
                objRD.RespondQty = OldRespond
                objRDFac.Update(objRD)
                arlRedemptionDetail(e.Item.ItemIndex) = objRD
                sHelper.SetSession(ses_arlRedemptionDetail, arlRedemptionDetail)
            Else
                objRH.RedemptionDetails = arlRedemptionDetail
                Dim arl As ArrayList = CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)
                Dim oVR As v_Redemption = CType(arl(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption)

                oVR.RedemptionHeaders(CType(Me.ViewState.Item(Me.vst_DayIndex), Integer)) = objRH
                Dim oFERP As New FrmEntryRedemptionPlan2
                oFERP.UpdateTotalQtyPublic(oVR, CType(Me.ViewState.Item(Me.vst_DayIndex), Integer), 0, 0, objRD.RespondQty - OldRespond)
                arl(CType(Me.ViewState.Item(Me.vst_RowIndex), Integer)) = oVR

                sHelper.SetSession("FrmEntryRedemptionPlan.arlDataToDisplay", arl)
                'CType(CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption).RedemptionHeaders(CType(Me.ViewState.Item(Me.vst_DayIndex), Integer)) = objRH
                'Dim oFERP As New FrmEntryRedemptionPlan2
                'oFERP.UpdateTotalQtyPublic(CType(CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption), CType(Me.ViewState.Item(Me.vst_DayIndex), Integer), 0, 0, objRD.RespondQty - OldRespond)
            End If
        End If

        dtgMain.EditItemIndex = -1
        GetRedemptionHeader.RedemptionDetails = New ArrayList
        BindDtg(False)
        dtgMain.ShowFooter = True
        UpdateParent()
        'RegisterStartupScript("OpenWindow", "<script>UpdateParent('" & 0 & "','" & 0 & "','" & 0 & "')</script>")
    End Sub

    Private Function IsRequestEnough(ByVal oRD As RedemptionDetail) As Boolean
        Dim aRD As New ArrayList
        Dim oRDFac As RedemptionDetailFacade = New RedemptionDetailFacade(User)
        Dim cRD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dtTemp As Date = oRD.RedemptionHeader.PeriodDate
        Dim TotalRequest As Integer = 0

        cRD.opAnd(New Criteria(GetType(RedemptionDetail), "RedemptionHeader.PeriodDate", MatchType.Greater, DateSerial(dtTemp.Year, dtTemp.Month, 1).AddDays(-1)))
        cRD.opAnd(New Criteria(GetType(RedemptionDetail), "RedemptionHeader.PeriodDate", MatchType.Lesser, DateSerial(dtTemp.Year, dtTemp.Month, 1).AddMonths(1)))
        cRD.opAnd(New Criteria(GetType(RedemptionDetail), "RedemptionHeader.VechileColor.ID", MatchType.Exact, oRD.RedemptionHeader.VechileColor.ID))
        cRD.opAnd(New Criteria(GetType(RedemptionDetail), "Dealer.ID", MatchType.Exact, oRD.Dealer.ID))
        aRD = oRDFac.Retrieve(cRD)
        For Each oRD2 As RedemptionDetail In aRD
            If oRD2.ID <> oRD.ID Then
                TotalRequest += oRD2.RequestQty
            Else
                TotalRequest += oRD.RequestQty
            End If
        Next

        Return IIf(TotalRequest <= GetTotalRequestInContract(oRD), True, False)
    End Function

    Private Function GetTotalRequestInContract(ByVal oRD As RedemptionDetail) As Integer
        Dim oCDFac As ContractDetailFacade = New ContractDetailFacade(User)
        Dim cCD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aCD As New ArrayList
        Dim Total As Integer = 0

        cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.ContractPeriodMonth", MatchType.Exact, oRD.RedemptionHeader.PeriodDate.Month))
        cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.ContractPeriodYear", MatchType.Exact, oRD.RedemptionHeader.PeriodDate.Year))
        cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.Dealer.ID", MatchType.Exact, oRD.Dealer.ID))
        cCD.opAnd(New Criteria(GetType(ContractDetail), "VechileColor.ID", MatchType.Exact, oRD.RedemptionHeader.VechileColor.ID))
        cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.CreatedTime", MatchType.LesserOrEqual, Format(CommonFunction.GetMaxContractDateOfRedemption(oRD.RedemptionHeader.PeriodDate.Month, oRD.RedemptionHeader.PeriodDate.Year), "yyyy/MM/dd HH:mm:ss")))
        aCD = oCDFac.Retrieve(cCD)
        For Each oCD As ContractDetail In aCD
            Total += oCD.TargetQty
        Next

        Return Total
    End Function

    Private Function IsCeilingEnough(ByVal oRD As RedemptionDetail, ByVal oD As Dealer) As Boolean
        Dim oSRCFac As sp_RedemptionCeilingFacade = New sp_RedemptionCeilingFacade(User)
        Dim oSRC As sp_RedemptionCeiling
        Dim arlSRC As New ArrayList

        If oRD.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then Return True
        arlSRC = oSRCFac.RetrieveFromSP(oD.CreditAccount, oRD.TermOfPayment.PaymentType, oRD.RedemptionHeader.PeriodDate)

        If arlSRC.Count <= 0 Then
            Return False
        Else
            oSRC = CType(arlSRC(0), sp_RedemptionCeiling)
            If (oSRC.InitialCeiling + oSRC.TotalLiquified - oSRC.TotalProposed) < 0 Then
                Return False
            Else
                Return True
            End If
        End If
    End Function

    Private Function ShowCeilingPosition() As Decimal
        '
    End Function

    Private Sub DTGAdd(ByVal e As DataGridCommandEventArgs)
        Dim objRD As New RedemptionDetail
        Dim objRDFac As RedemptionDetailFacade = New RedemptionDetailFacade(User)
        Dim objD As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        Dim ID As Integer = 0
        Dim IsInserted As Boolean = True

        If IsNothing(objRH) OrElse objRH.ID < 1 Then objRH = Me.GetRedemptionHeader()

        objRD.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(CType(e.Item.FindControl("ddlFooterTOP"), DropDownList).SelectedValue, Integer))
        objRD.RequestQty = CType(e.Item.FindControl("txtFooterQty"), TextBox).Text
        objRD.RedemptionHeader = objRH
        objRD.Dealer = objD
        objRD.RespondQty = CType(e.Item.FindControl("txtFooterJK"), TextBox).Text
        objRD.ID = objRDFac.Insert(objRD)
        'objRD = objRDFac.Retrieve(ID)

        If Not IsRequestEnough(objRD) Then
            objRDFac.Delete(objRD)
            MessageBox.Show("Jumlah unit rencana Dealer melebihi jumlah contract")
            IsInserted = False
        End If

        If Not objRD.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS AndAlso Not IsCeilingEnough(objRD, objD) Then
            objRDFac.Delete(objRD)
            MessageBox.Show("Ceiling tidak tersedia")
            IsInserted = False
        End If
        GetRedemptionHeader.RedemptionDetails = New ArrayList
        If IsInserted Then
            CType(CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption).RedemptionHeaders(CType(Me.ViewState.Item(Me.vst_DayIndex), Integer)) = objRH
            Dim oFERP As New FrmEntryRedemptionPlan2
            Dim arl As ArrayList = CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)
            Dim oVR As v_Redemption = CType(arl(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption)

            oFERP.UpdateTotalQtyPublic(oVR, CType(Me.ViewState.Item(Me.vst_DayIndex), Integer), 0, objRD.RequestQty, 0)
            arl(CType(viewstate.Item(vst_RowIndex), Integer)) = oVR
            sHelper.SetSession("FrmEntryRedemptionPlan.arlDataToDisplay", arl)
        End If
        BindDtg(False)
        UpdateParent()
    End Sub

    Private Sub DTGDelete(ByVal e As DataGridCommandEventArgs)
        Dim objRD As RedemptionDetail = CType(CType(sHelper.GetSession(ses_arlRedemptionDetail), ArrayList)(e.Item.ItemIndex), RedemptionDetail)
        Dim objRDFac As RedemptionDetailFacade = New RedemptionDetailFacade(User)
        Dim nOriReq As Integer, nOriRes As Integer

        nOriReq = objRD.RequestQty
        nOriRes = objRD.RespondQty
        objRDFac.Delete(objRD)
        GetRedemptionHeader.RedemptionDetails = New ArrayList

        objRH = Me.GetRedemptionHeader()
        CType(CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption).RedemptionHeaders(CType(Me.ViewState.Item(Me.vst_DayIndex), Integer)) = objRH
        Dim oFERP As New FrmEntryRedemptionPlan2
        oFERP.UpdateTotalQtyPublic(CType(CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)(CType(viewstate.Item(vst_RowIndex), Integer)), v_Redemption), CType(Me.ViewState.Item(Me.vst_DayIndex), Integer), 0, -nOriReq, -nOriRes)

        BindDtg(False)
        UpdateParent()
    End Sub

    Private Sub DTGEdit(ByVal e As DataGridCommandEventArgs)
        dtgMain.ShowFooter = False
        dtgMain.EditItemIndex = CInt(e.Item.ItemIndex)
        BindDtg(True)
    End Sub

    Private Sub DTGCancel(ByVal e As DataGridCommandEventArgs)
        dtgMain.EditItemIndex = -1
        BindDtg(True)
        dtgMain.ShowFooter = True
    End Sub

    Private Sub UpdateParent()
        'Me.ViewState.Item("AutoRefresh") = "1" '"0"
        'Me.BindDTG()


        viewstate.Add(vst_DealerCode, Request.Item("DealerCode"))
        viewstate.Add(vst_RowIndex, Request.Item("RowIndex"))
        viewstate.Add(vst_DayIndex, Request.Item("DayIndex"))
        viewstate.Add(Me.vst_ID, Request.Item("ID"))

        Dim RowIdx As Integer = Me.ViewState.Item(Me.vst_RowIndex)
        Dim DayIdx As Integer = Me.ViewState.Item(Me.vst_DayIndex)
        Dim sDay As String, sTotRow As String, sTotCol As String
        Dim i As Integer, j As Integer
        Dim aVRs As ArrayList = CType(sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)
        Dim Est As Integer, Req As Integer, Res As Integer
        Dim oVR As v_Redemption
        Dim TempReq As Integer, TempRes As Integer

        Me.GetRedComponent(CType(aVRs(RowIdx), v_Redemption), DayIdx + 1, CType(CType(aVRs(RowIdx), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader).ID, Est, Req, Res)
        If Req > 0 OrElse Res > 0 Then
            sDay = Req.ToString & "|" & Res.ToString
        End If
        'Req = CType(aVRs(RowIdx), v_Redemption).TotalRequest()
        'Res = CType(aVRs(RowIdx), v_Redemption).TotalRespond()
        Req = 0
        Res = 0
        oVR = aVRs(RowIdx)
        'For i = 5 To Me.dtgMain.Columns.Count - 2
        '    If Me.dtgMain.Columns(i).Visible Then
        '        Me.GetRedComponent(oVR, i - 4, 0, 0, TempReq, TempRes)
        '        Req += TempReq
        '        Res += TempRes
        '    End If
        'Next
        For i = 1 To 31
            Me.GetRedComponent(oVR, i, 0, 0, TempReq, TempRes)
            Req += TempReq
            Res += TempRes
        Next
        sTotRow = Req.ToString() & "|" & Res.ToString

        Req = 0
        Res = 0
        For i = 0 To aVRs.Count - 1
            oVR = aVRs(i)

            Me.GetRedComponent(oVR, DayIdx + 1, CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader).ID, 0, TempReq, TempRes)
            Req += TempReq
            Res += TempRes
        Next

        sTotCol = Req.ToString & "|" & Res.ToString
        'Me.txtsDay.Text = sDay
        'Me.txtsTotRow.Text = sTotRow
        'Me.txtsTotCol.Text = sTotCol
        RegisterStartupScript("OpenWindow", "<script>UpdateDisplay('" & sDay & "','" & sTotRow & "','" & sTotCol & "')</script>")
    End Sub

    Private Sub GetRedComponent(ByRef oVR As v_Redemption, ByVal Day As Integer, ByRef RHID As Integer, ByRef Plan As Integer, ByRef Req As Integer, ByRef Res As Integer)
        If Day < 1 OrElse Day > 31 Then Day = 0
        Select Case Day
            Case 0 : RHID = 0 : Plan = 0 : Req = 0 : Res = 0
            Case 1 : RHID = oVR.RH1 : Plan = oVR.E1 : Req = oVR.R1 : Res = oVR.A1
            Case 2 : RHID = oVR.RH2 : Plan = oVR.E2 : Req = oVR.R2 : Res = oVR.A2
            Case 3 : RHID = oVR.RH3 : Plan = oVR.E3 : Req = oVR.R3 : Res = oVR.A3
            Case 4 : RHID = oVR.RH4 : Plan = oVR.E4 : Req = oVR.R4 : Res = oVR.A4
            Case 5 : RHID = oVR.RH5 : Plan = oVR.E5 : Req = oVR.R5 : Res = oVR.A5
            Case 6 : RHID = oVR.RH6 : Plan = oVR.E6 : Req = oVR.R6 : Res = oVR.A6
            Case 7 : RHID = oVR.RH7 : Plan = oVR.E7 : Req = oVR.R7 : Res = oVR.A7
            Case 8 : RHID = oVR.RH8 : Plan = oVR.E8 : Req = oVR.R8 : Res = oVR.A8
            Case 9 : RHID = oVR.RH9 : Plan = oVR.E9 : Req = oVR.R9 : Res = oVR.A9
            Case 10 : RHID = oVR.RH10 : Plan = oVR.E10 : Req = oVR.R10 : Res = oVR.A10
            Case 11 : RHID = oVR.RH11 : Plan = oVR.E11 : Req = oVR.R11 : Res = oVR.A11
            Case 12 : RHID = oVR.RH12 : Plan = oVR.E12 : Req = oVR.R12 : Res = oVR.A12
            Case 13 : RHID = oVR.RH13 : Plan = oVR.E13 : Req = oVR.R13 : Res = oVR.A13
            Case 14 : RHID = oVR.RH14 : Plan = oVR.E14 : Req = oVR.R14 : Res = oVR.A14
            Case 15 : RHID = oVR.RH15 : Plan = oVR.E15 : Req = oVR.R15 : Res = oVR.A15
            Case 16 : RHID = oVR.RH16 : Plan = oVR.E16 : Req = oVR.R16 : Res = oVR.A16
            Case 17 : RHID = oVR.RH17 : Plan = oVR.E17 : Req = oVR.R17 : Res = oVR.A17
            Case 18 : RHID = oVR.RH18 : Plan = oVR.E18 : Req = oVR.R18 : Res = oVR.A18
            Case 19 : RHID = oVR.RH19 : Plan = oVR.E19 : Req = oVR.R19 : Res = oVR.A19
            Case 20 : RHID = oVR.RH20 : Plan = oVR.E20 : Req = oVR.R20 : Res = oVR.A20
            Case 21 : RHID = oVR.RH21 : Plan = oVR.E21 : Req = oVR.R21 : Res = oVR.A21
            Case 22 : RHID = oVR.RH22 : Plan = oVR.E22 : Req = oVR.R22 : Res = oVR.A22
            Case 23 : RHID = oVR.RH23 : Plan = oVR.E23 : Req = oVR.R23 : Res = oVR.A23
            Case 24 : RHID = oVR.RH24 : Plan = oVR.E24 : Req = oVR.R24 : Res = oVR.A24
            Case 25 : RHID = oVR.RH25 : Plan = oVR.E25 : Req = oVR.R25 : Res = oVR.A25
            Case 26 : RHID = oVR.RH26 : Plan = oVR.E26 : Req = oVR.R26 : Res = oVR.A26
            Case 27 : RHID = oVR.RH27 : Plan = oVR.E27 : Req = oVR.R27 : Res = oVR.A27
            Case 28 : RHID = oVR.RH28 : Plan = oVR.E28 : Req = oVR.R28 : Res = oVR.A28
            Case 29 : RHID = oVR.RH29 : Plan = oVR.E29 : Req = oVR.R29 : Res = oVR.A29
            Case 30 : RHID = oVR.RH30 : Plan = oVR.E30 : Req = oVR.R30 : Res = oVR.A30
            Case 31 : RHID = oVR.RH31 : Plan = oVR.E31 : Req = oVR.R31 : Res = oVR.A31

        End Select
    End Sub

    Private Function IsStillOpen() As Boolean
        Dim oRSFac As New RedemptionStatusFacade(User)
        Dim IsOpen As Boolean
        Dim CategoryCode As String = Me.sHelper.GetSession(Me._sessCategoryCode)
        Dim SubCategoryCode As String = Me.sHelper.GetSession(Me._sessSubCategoryCode)

        objRH = GetRedemptionHeader()

        IsOpen = oRSFac.IsStatusOpen(objRH.PeriodDate, CategoryCode, SubCategoryCode)

        Return IsOpen
    End Function

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            Initialization()
        End If
        Me.CheckPrivilege()
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        FillDtg(e)
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        If Not Me.IsStillOpen() Then
            MessageBox.Show("Redemption Plan Periode " & Me.lblPeriod.Text & " Sudah Closed")
            Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = False
            Exit Sub
        End If
        Select Case (e.CommandName)
            Case "Delete"
                DTGDelete(e)
            Case "Add"
                DTGAdd(e)
            Case "Edit"
                DTGEdit(e)
            Case "Update"
                DTGUpdate(e)
            Case "Cancel"
                DTGCancel(e)
        End Select
    End Sub

    Private Sub btnCalculate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalculate.Click
        Dim lblQty As Label
        Dim lblJK As Label
        Dim lblTotal As Label
        Dim SubTotal As Decimal = 0
        Dim Total As Decimal = 0
        Dim TotQty As Integer = 0
        Dim TotJK As Integer = 0

        If dtgMain.EditItemIndex >= 0 Then Exit Sub
        For Each di As DataGridItem In Me.dtgMain.Items
            lblQty = di.FindControl("lblQty")
            lblJK = di.FindControl("lblJK")
            lblTotal = di.FindControl("lblTotal")
            SubTotal = CType(lblQty.Text, Integer) * CType(Me.lblPrice.Text, Decimal)
            lblTotal.Text = FormatNumber(SubTotal, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            TotQty += CType(lblQty.Text, Integer)
            TotJK += CType(lblJK.Text, Integer)
            Total += SubTotal
        Next
        Me.lblTotalQty.Text = FormatNumber(TotQty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Me.lblTotalJK.Text = FormatNumber(TotJK, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Me.lblTotalPrice.Text = FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

    End Sub

    Private Sub btnSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSet.Click
        Dim CurState As Boolean = CType(Me.ViewState.Item(Me.vst_Locked), Boolean)
        Dim DestStatus As Short = IIf(CurState = True, 0, 1)
        Dim aRDs As ArrayList = CType(sHelper.GetSession(ses_arlRedemptionDetail), ArrayList)
        Dim i As Integer
        Dim oRD As RedemptionDetail
        Dim oRDFac As New RedemptionDetailFacade(User)

        For i = 0 To aRDs.Count - 1
            oRD = CType(aRDs(i), RedemptionDetail)

            oRD.IsManualAlloc = DestStatus
            oRDFac.Update(oRD)
        Next
        BindDtg(False)
    End Sub

#End Region
End Class
