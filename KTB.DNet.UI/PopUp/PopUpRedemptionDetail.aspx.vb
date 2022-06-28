
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

Public Class PopUpRedemptionDetail
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
    Protected WithEvents lblStatusLock As System.Web.UI.WebControls.Label

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

    Private objRH As RedemptionHeader
#End Region

#Region "Custom Methods"

    Private Sub Initialization()
        If IsNothing(Request.Item("RowIndex")) OrElse IsNothing(Request.Item("DayIndex")) Then
            Response.Write("<script>window.close();</script>")
            Exit Sub
        End If
        viewstate.Add(vst_DealerCode, Request.Item("DealerCode"))
        viewstate.Add(vst_RowIndex, Request.Item("RowIndex"))
        viewstate.Add(vst_DayIndex, Request.Item("DayIndex"))
        sHelper.SetSession(ses_arlRedemptionDetail, New ArrayList)
        BindHeader()
        BindDtg(False)
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

        arlSRC = oSRCFac.RetrieveFromSP(pCreditAccount, pPaymentType, pDate)
        If arlSRC.Count > 0 Then
            oSRc = CType(arlSRC(0), sp_RedemptionCeiling)
            Ceiling = oSRc.InitialCeiling + oSRc.TotalLiquified - oSRc.TotalProposed
            strTemp = "PaymentType=" & pPaymentType & "Init =" & oSRc.InitialCeiling & ";Liquified=" & oSRc.TotalLiquified & ";Proposed=" & oSRc.TotalProposed
        Else
            Ceiling = 0
        End If
        txtCeilingInfo.Text &= "." & strTemp
        Return Ceiling
    End Function

    Private Function GetRedemptionHeader() As RedemptionHeader
        Dim arlDataToDisplay As ArrayList = sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        Dim oDV As FrmEntryRedemptionPlan.DealerVehicle = CType(arlDataToDisplay(CType(viewstate.Item(vst_RowIndex), Integer)), FrmEntryRedemptionPlan.DealerVehicle)

        Return oDV.RedemptionHeaders(CType(viewstate.Item(vst_DayIndex), Integer))
    End Function

    Private Function IsRespondValid(ByVal RowIndex As Integer, ByVal DayIndex As Integer) As Boolean
        Dim arlDataToDisplay As ArrayList = sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        Dim objDV As FrmEntryRedemptionPlan.DealerVehicle = CType(arlDataToDisplay(RowIndex), FrmEntryRedemptionPlan.DealerVehicle)
        Dim TotalRespond As Integer = 0
        Dim TotalStock As Integer = CType(objDV.RedemptionHeaders(DayIndex), RedemptionHeader).EstimationStock

        For Each oDV As FrmEntryRedemptionPlan.DealerVehicle In arlDataToDisplay
            If oDV.VechileColor.ID = objDV.VechileColor.ID Then
                TotalRespond += CType(oDV.RedemptionHeaders(DayIndex), RedemptionHeader).TotalRespond(oDV.Dealer.ID)
            End If
        Next
        Return IIf(TotalRespond > TotalStock, False, True)
    End Function

    Private Sub UpdateRedemptionDetail()
        Dim arlDataToDisplay As ArrayList = sHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        Dim oDV As FrmEntryRedemptionPlan.DealerVehicle = CType(arlDataToDisplay(CType(viewstate.Item(vst_RowIndex), Integer)), FrmEntryRedemptionPlan.DealerVehicle)

        CType(oDV.RedemptionHeaders(CType(viewstate.Item(vst_DayIndex), Integer)), RedemptionHeader).RedemptionDetails = sHelper.GetSession(ses_arlRedemptionDetail)

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

        If objD.Title <> CType(EnumDealerTittle.DealerTittle.KTB, String) Then
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
            End If
        End If

        dtgMain.EditItemIndex = -1
        GetRedemptionHeader.RedemptionDetails = New ArrayList
        BindDtg(False)
        dtgMain.ShowFooter = True

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
            TotalRequest += oRD2.RequestQty
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
        cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.CreatedTime", MatchType.LesserOrEqual, Format(CommonFunction.GetMaxContractDateOfRedemption(oRD.RedemptionHeader.PeriodDate.Month, oRD.RedemptionHeader.PeriodDate.Year), "yyyy/MM/dd hh:mm:ss")))
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

    Private Sub DTGAdd(ByVal e As DataGridCommandEventArgs)
        Dim objRD As New RedemptionDetail
        Dim objRDFac As RedemptionDetailFacade = New RedemptionDetailFacade(User)
        Dim objD As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        Dim ID As Integer = 0

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
        End If

        If Not objRD.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS AndAlso Not IsCeilingEnough(objRD, objD) Then
            objRDFac.Delete(objRD)
            MessageBox.Show("Ceiling tidak tersedia")
        End If
        GetRedemptionHeader.RedemptionDetails = New ArrayList
        BindDtg(False)
    End Sub

    Private Sub DTGDelete(ByVal e As DataGridCommandEventArgs)
        Dim objRD As RedemptionDetail = CType(CType(sHelper.GetSession(ses_arlRedemptionDetail), ArrayList)(e.Item.ItemIndex), RedemptionDetail)
        Dim objRDFac As RedemptionDetailFacade = New RedemptionDetailFacade(User)

        objRDFac.Delete(objRD)
        GetRedemptionHeader.RedemptionDetails = New ArrayList
        BindDtg(False)
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

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            Initialization()
        End If
        objRH = GetRedemptionHeader()

    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        FillDtg(e)
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
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

#End Region
End Class
