#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Transfer
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade
Imports System.Linq
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.Configuration
#End Region



Public Class CreatePODraft
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
    Private sessionHelper As New SessionHelper
    Dim objDealer As Dealer
    Dim PermintaanKirim As Date
    Private objContractHeader As ContractHeader
    Private objNHFac As NationalHolidayFacade = New NationalHolidayFacade(User)
    Private nTOP As Integer
    Private nMonth As Integer
    Private objSPL As SPL
    Private arrOrder As New ArrayList
    Private objContractDetail As ContractDetail
    Private SubTotalItem As Integer
    Private SubTotalharga As Double
    Private SubTotalPPh As Double
    Private SubTotalInterest As Double
    Private subTotalDeposit As Double
    Private subTotalBiayaKirimPPN As Double
    Dim objPODraftHeader As PODraftHeader
    Dim objPODraftDetail As PODraftDetail
    Dim ProductionYear As Integer
    Dim pops As Boolean = False
    Dim warning As String = ""
    Dim Mode As String = String.Empty

    'Private objPODraftHeaderValidation As PODraftHeader
    'Private objPODraftDetailValidation As PODraftDetail
    Private _sessSPL As String = "FrmCreatePO._sessSPL"
    Private _sessIsTransfer As String = "FrmCreatePO._sessIsTransfer"
#End Region

#Region "Event Handler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Mode = "CREATE"
        If Not IsNothing(Request.QueryString("mode")) Then
            Mode = Request.QueryString("mode").Trim().ToUpper()
        End If

        sessionHelper.SetSession("DraftPOMode", Mode)
        CheckUserPrivilege()

        lblSearchPODestination.Attributes("onclick") = "ShowPPPODestination()"

        If Mode = "CREATE" Then
            If Not IsNothing(Request.QueryString("dateValue")) Then
                PermintaanKirim = CType(Request.QueryString("dateValue"), Date)
            Else
                PermintaanKirim = DateTime.Now
            End If
        Else
            If Not IsNothing(sessionHelper.GetSession("PermintaanKirim")) Then
                PermintaanKirim = CType(sessionHelper.GetSession("PermintaanKirim"), Date)
            Else
                PermintaanKirim = DateTime.Now
            End If
        End If

        If Not IsPostBack Then
            BindData()

            txtPODestinationCode.Attributes.Add("readonly", "readonly")
            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            If companyCode = "MFTBC" Then
                rdoByDealer.Checked = True
                hidPODestinationID.Value = "1"
            ElseIf companyCode = "MMC" Then
                rdoByKTB.Checked = True
                hidPODestinationID.Value = "1"
            End If

            rdoByKTB.Attributes("onclick") = "SetPODestinationByKTB()"
            rdoByDealer.Attributes("onclick") = "SetPODestinationByDealer()"

            Me.txtID.Text = "0"
            ViewState.Add("SubTotalHarga", 0)
            ViewState.Add("ShowMessage", "")

            'Start  :RemainModule-DailyPO:Free PPH DoniN
            SetFreePPh()
            'End    :RemainModule-DailyPO:Free PPH DoniN
            If Not IsNothing(Request.QueryString("id")) Then
                GetPO()
                BindDataForEdit()
            End If

            btnBatal.Visible = SecurityProvider.Authorize(Context.User, SR.MDP_Daftar_PO_Draft_Batal_Privilege)
        End If

        If Mode = "VIEW" Then
            EnableControl(False)
            lblSearchPODestination.Attributes("onclick") = "ShowAlert()"
            btnKirim.Visible = False
            btnHitung.Visible = False
            btnBatal.Visible = False
        Else
            EnableControl(True)
            btnKirim.Visible = True
            btnHitung.Visible = True
            btnBatal.Visible = True
        End If

        If Mode = "EDIT" Then
            ddlContractNumber.Enabled = False
        End If

        If Mode = "CREATE" Then
            lblPageTitle.Text = "Buat PO Draft"
        ElseIf Mode = "EDIT" Then
            lblPageTitle.Text = "Edit PO Draft"
        Else
            lblPageTitle.Text = "View PO Draft"
        End If

        btnBatal.Attributes.Add("OnClick", "return confirm('Yakin Draft PO ini dibatalkan?');")
        SetControls()
    End Sub

    Protected Sub ddlContractNumber_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContractNumber.SelectedIndexChanged
        If ddlContractNumber.Items.Count > 0 Then
            objContractHeader = GetContract(CType(ddlContractNumber.SelectedItem.Value, Integer))
            For i As Integer = 0 To objContractHeader.ContractDetails.Count - 1
                arrOrder.Add(0)
            Next
            sessionHelper.SetSession("Ord", arrOrder)
            lblOrderType.Text = CType(objContractHeader.ContractType, enumOrderType.OrderType).ToString()
            lblSalesOrg.Text = objContractHeader.Category.CategoryCode
            lblTahunPerakitan.Text = objContractHeader.ProductionYear
            lblProjectName.Text = objContractHeader.ProjectName
            BindddlOrderType(objContractHeader)
            Mode = sessionHelper.GetSession("DraftPOMode")
            If Mode = "CREATE" Then
                BindDetailToGrid(objPODraftHeader)
            End If

            Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
            objDealer = CType(Session("DEALER"), Dealer)
            Dim oProductCategory As ProductCategory = CType(objContractHeader.Category.ProductCategory(), ProductCategory)
            If CommonFunction.GetTransControlStatus(objDealer, EnumDealerTransType.DealerTransKind.FactoringMMC) AndAlso oFMFac.IsAllowedToProposePO(oProductCategory, objDealer.CreditAccount) Then
                Me.chkFactoring.Enabled = True
            Else
                Me.chkFactoring.Enabled = False
            End If
        End If
    End Sub

    Private Sub BindDetailToGrid(ByVal objPODraftHeader As PODraftHeader)
        Dim objTOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
        nTOP = objTOP.TermOfPaymentValue
        nMonth = DateTime.DaysInMonth(objContractHeader.ContractPeriodYear, objContractHeader.ContractPeriodMonth)
        Dim objPKHead As PKHeader = New PKHeaderFacade(User).Retrieve(objContractHeader.PKNumber)
        objSPL = New SPLFacade(User).Retrieve(objPKHead.SPLNumber.ToString())
        arrOrder = sessionHelper.GetSession("Ord")
        Mode = sessionHelper.GetSession("DraftPOMode")
        If Mode = "CREATE" Then
            dtgDetail.DataSource = objContractHeader.ContractDetails
        Else
            If Not IsNothing(objPODraftHeader) Then
                dtgDetail.DataSource = objPODraftHeader.PODraftDetail
            Else
                dtgDetail.DataSource = objContractHeader.ContractDetails
            End If
        End If

        dtgDetail.DataBind()
        'If SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege) Then
        '    dtgDetail.Columns(6).Visible = False
        'Else
        '    dtgDetail.Columns(6).Visible = True
        'End If

    End Sub

    Protected Sub dtgDetail_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgDetail.ItemDataBound
        If Not (e.Item.ItemIndex = -1) AndAlso e.Item.ItemType <> ListItemType.Footer Then
            If Not IsNothing(sessionHelper.GetSession("SESMDPDAILYPRODYEAR")) Then
                ProductionYear = CType(sessionHelper.GetSession("SESMDPDAILYPRODYEAR"), Integer)
            Else
                ProductionYear = Now.Year
            End If

            arrOrder = sessionHelper.GetSession("Ord")
            Dim txtBox As TextBox = e.Item.FindControl("TextBox1")
            Mode = sessionHelper.GetSession("DraftPOMode")

            objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)

            If Not IsNothing(objPODraftHeader) Then
                If objPODraftHeader.PODraftDetail.Count > 0 Then
                    If objPODraftHeader.PODraftDetail.Count - 1 >= e.Item.ItemIndex Then
                        objPODraftDetail = objPODraftHeader.PODraftDetail(e.Item.ItemIndex)
                    End If
                End If
                If objDealer.Title <> EnumDealerTittle.DealerTittle.DEALER Then
                    objDealer = objPODraftHeader.Dealer
                End If
            End If

            If Mode = "CREATE" Then
                objContractDetail = objContractHeader.ContractDetails(e.Item.ItemIndex)
            Else
                objContractDetail = objPODraftDetail.ContractDetail
            End If

            e.Item.Cells(2).Text = objContractDetail.VechileColor.MaterialNumber
            e.Item.Cells(3).Text = objContractDetail.VechileColor.MaterialDescription
            If Mode = "VIEW" Then
                txtBox.Enabled = False
                'If Not IsNothing(objPODraftDetail) Then
                '    If objPODraftDetail.ContractDetail.VechileColor.MaterialNumber.ToString() = e.Item.Cells(2).Text Then
                '        txtBox.Text = objPODraftDetail.ReqQty.ToString()
                '    End If
                'End If
            ElseIf Mode = "EDIT" Then
                txtBox.Enabled = True
                'If Not IsNothing(objPODraftDetail) Then
                '    If objPODraftDetail.ContractDetail.VechileColor.MaterialNumber.ToString() = e.Item.Cells(2).Text Then
                '        txtBox.Text = objPODraftDetail.ReqQty.ToString()
                '    End If
                'End If
            Else
                txtBox.Enabled = True
                'txtBox.Text = arrOrder(e.Item.ItemIndex)
            End If
            txtBox.Text = arrOrder(e.Item.ItemIndex)

            Dim SisaContractUnit As Integer = objContractDetail.TargetQty
            Dim PengajuanStokPO As Integer = 0
            Dim PODraftDetailBaruCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            PODraftDetailBaruCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, e.Item.Cells(2).Text))
            PODraftDetailBaruCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.Status", MatchType.Exact, CType(enumStatusPO.StatusDraftPO.Baru, Integer)))

            Dim arrPODraftDetailBaru As ArrayList = New PODraftDetailFacade(User).Retrieve(PODraftDetailBaruCriteria)
            For Each PODraftDetailItem As PODraftDetail In arrPODraftDetailBaru
                If PODraftDetailItem.ContractDetail.ID = objContractDetail.ID Then
                    SisaContractUnit -= PODraftDetailItem.ReqQty
                End If

                If PermintaanKirim.Day = PODraftDetailItem.PODraftHeader.ReqAllocationDate AndAlso _
                    PermintaanKirim.Month = PODraftDetailItem.PODraftHeader.ReqAllocationMonth AndAlso _
                    PermintaanKirim.Year = PODraftDetailItem.PODraftHeader.ReqAllocationYear AndAlso _
                    objDealer.ID = PODraftDetailItem.PODraftHeader.Dealer.ID Then
                    PengajuanStokPO += PODraftDetailItem.ReqQty
                End If
            Next

            Dim PODraftDetailSubmitedCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            PODraftDetailSubmitedCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, e.Item.Cells(2).Text))
            PODraftDetailSubmitedCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.Status", MatchType.Exact, CType(enumStatusPO.StatusDraftPO.SubmitPO, Integer)))
            PODraftDetailSubmitedCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.POHeader.Status", MatchType.NotInSet, CType(enumStatusPO.Status.Batal, Integer) & "," & CType(enumStatusPO.Status.DiBlok, Integer) & "," & CType(enumStatusPO.Status.Ditolak, Integer)))

            Dim arrPODraftDetailSubmited As ArrayList = New PODraftDetailFacade(User).Retrieve(PODraftDetailSubmitedCriteria)
            For Each PODraftDetailSubmitedItem As PODraftDetail In arrPODraftDetailSubmited
                If PODraftDetailSubmitedItem.ContractDetail.ID = objContractDetail.ID Then
                    SisaContractUnit -= PODraftDetailSubmitedItem.ReqQty
                End If

                If PermintaanKirim.Day = PODraftDetailSubmitedItem.PODraftHeader.ReqAllocationDate AndAlso _
                    PermintaanKirim.Month = PODraftDetailSubmitedItem.PODraftHeader.ReqAllocationMonth AndAlso _
                    PermintaanKirim.Year = PODraftDetailSubmitedItem.PODraftHeader.ReqAllocationYear AndAlso _
                    objDealer.ID = PODraftDetailSubmitedItem.PODraftHeader.Dealer.ID Then
                    PengajuanStokPO += PODraftDetailSubmitedItem.ReqQty
                End If
            Next

            If Mode = "EDIT" Then
                'e.Item.Cells(4).Text = FormatNumber(SisaContractUnit + (CType(txtBox.Text, Integer) - objPODraftDetail.ReqQty), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Dim oPODraftDetailExisting As PODraftDetail = New PODraftDetailFacade(User).Retrieve(objPODraftDetail.ID)
                e.Item.Cells(4).Text = FormatNumber(SisaContractUnit + (oPODraftDetailExisting.ReqQty), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Else
                e.Item.Cells(4).Text = FormatNumber(SisaContractUnit, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If


            Dim rangeValidator As RangeValidator = e.Item.FindControl("RangeValidator1")
            If (CInt(SisaContractUnit) < 0) Then
                rangeValidator.MaximumValue = 0
            Else
                If Mode = "EDIT" Then
                    rangeValidator.MaximumValue = CInt(SisaContractUnit + CType(txtBox.Text, Integer))
                Else
                    rangeValidator.MaximumValue = CInt(SisaContractUnit)
                End If
            End If
            Dim lblDeposit As Label = e.Item.FindControl("lblDeposit")
            Dim ItemDeposit As Double = GetItemDeposit(objContractDetail)

            lblDeposit.Text = FormatNumber(CType(txtBox.Text, Integer) * ItemDeposit, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            'txtBox.Text = arrOrder(e.Item.ItemIndex)

            'Tambahan SLA
            Dim lblBiayaKirimPPN As Label = e.Item.FindControl("lblBiayaKirimPPN")
            If rdoByKTB.Checked AndAlso (hidPODestinationID.Value <> "" AndAlso hidPODestinationID.Value <> "1" AndAlso hidPODestinationID.Value <> "-1" AndAlso txtPODestinationCode.Text.Trim() <> "") Then
                Dim SAPModel As String = objContractDetail.VechileColor.VechileType.SAPModel

                'Dim podes As PODestination = New PODestinationFacade(User).Retrieve(CType(hidPODestinationID.Value, Integer))

                'Dim criterialogistic As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "RegionCode", MatchType.Exact, podes.RegionCode))
                'criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "SAPModel", MatchType.Exact, objContractDetail.VechileColor.VechileType.SAPModel))
                'criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "EffectiveDate", MatchType.LesserOrEqual, DateTime.Now))

                'Dim sortColllog As SortCollection = New SortCollection
                'sortColllog.Add(New Sort(GetType(LogisticPrice), "EffectiveDate", Sort.SortDirection.DESC))

                'Dim logisticPrices As ArrayList = New LogisticPriceFacade(User).RetrieveByCriteria(criterialogistic, sortColllog)
                'If logisticPrices.Count > 0 Then
                '    Dim logisticPrice As LogisticPrice = logisticPrices(0)
                '    lblBiayaKirimPPN.Text = FormatNumber(CType(txtBox.Text, Double) * logisticPrice.TotalLogisticPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'Else : lblBiayaKirimPPN.Text = 0
                'End If
                lblBiayaKirimPPN.Text = FormatNumber(CalculateLogisticCost(CType(txtBox.Text, Double), objContractDetail), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Else : lblBiayaKirimPPN.Text = 0
            End If

            Dim lblHarga As Label = e.Item.FindControl("lblHarga")
            lblHarga.Text = FormatNumber(CType(txtBox.Text, Double) * CType(objContractDetail.Amount, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Dim lblPPh22 As Label = e.Item.FindControl("lblPPh22")
            'Start  :RemainModule-DailyPO:FreePPh By:Doni N
            lblPPh22.Text = FormatNumber(CType(txtBox.Text, Double) * CType(objContractDetail.PPh22, Double) * CInt(objContractHeader.FreePPh22Indicator), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'End  :RemainModule-DailyPO:FreePPh By:Doni N
            Dim lblInterest As Label = e.Item.FindControl("lblInterest")
            Dim freeIntIndicator As Integer
            Dim objPKHeader As PKHeader = New PKHeaderFacade(User).Retrieve(objContractHeader.PKNumber)
            freeIntIndicator = objPKHeader.FreeIntIndicator
            Dim arrPKDtl As ArrayList
            arrPKDtl = objPKHeader.PKDetails

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objContractDetail.VechileColor.ID))
            Dim oDealer As Dealer = Session("DEALER")
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, oDealer.ID))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
            'Modified By ali

            Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(objContractDetail)

            Dim lblFreeDays As Label = e.Item.FindControl("lblFreeDays")
            Dim lblMaxTOPDays As Label = e.Item.FindControl("lblMaxTOPDays")

            Dim pops As Boolean = False
            Dim warning As String = ""

            Dim isTransControlPO As Boolean = CommonFunction.CheckTransactionControlPO(objDealer)
            Dim getFreeDays As Integer = 0
            Dim getMaxTopDays As Integer = 0
            'Dim intFreedays As Integer = 0

            If Not IsNothing(objPODraftHeader) Then
                If isTransControlPO Then
                    If Mode = "CREATE" Then
                        'HitungSetFreeDays(objPODraftHeader, getMaxTopDays, getFreeDays, warning, pops)
                        'lblFreeDays.Text = getFreeDays
                        'lblMaxTOPDays.Text = getMaxTopDays
                        If Not String.IsNullOrEmpty(lblFreeDays.Text) Then
                            getFreeDays = CType(lblFreeDays.Text, Integer)
                            getMaxTopDays = CType(lblMaxTOPDays.Text, Integer)
                        End If
                    Else
                        getFreeDays = objPODraftDetail.FreeDays
                        getMaxTopDays = objPODraftDetail.MaxTOPDay
                    End If
                Else
                    For Each row As PKDetail In arrPKDtl
                        If row.VechileColor.ID = objContractDetail.VechileColor.ID Then
                            getFreeDays = row.FreeDays
                            getMaxTopDays = row.MaxTOPDay
                        End If
                    Next
                    lblFreeDays.Text = getFreeDays
                    lblMaxTOPDays.Text = getMaxTopDays
                End If
            End If

            lblFreeDays.Text = getFreeDays
            lblMaxTOPDays.Text = getMaxTopDays
            If objPriceArrayList.Count > 0 Then
                Dim objPrice As Price
                For Each item As Price In objPriceArrayList
                    If item.ValidFrom <= New DateTime(objContractDetail.ContractHeader.PricePeriodYear, objContractDetail.ContractHeader.PricePeriodMonth, objContractDetail.ContractHeader.PricePeriodDay) Then
                        objPrice = item
                        Exit For
                    End If
                Next
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
                    'lblInterest.Text = FormatNumber(CType(txtBox.Text, Double) * freeIntIndicator * Calculation.CountInterest(nTOP, nMonth, objPrice.Interest, objContractDetail.Amount - ItemDeposit, objPrice.PPh23), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lblInterest.Text = FormatNumber(CType(txtBox.Text, Double) * freeIntIndicator *
                                                    Calculation.CountInterest(getFreeDays, nTOP, nMonth, objPrice.Interest,
                                                    objContractDetail.Amount - ItemDeposit, objPrice.PPh23), 0, TriState.UseDefault,
                                                    TriState.UseDefault, TriState.True)

                End If
            Else
                lblInterest.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If

            SubTotalItem += CType(txtBox.Text, Integer)
            SubTotalharga = SubTotalharga + CType(lblHarga.Text, Double)
            SubTotalPPh = SubTotalPPh + CType(lblPPh22.Text, Double)
            SubTotalInterest += CType(lblInterest.Text, Double)
            subTotalDeposit += CType(lblDeposit.Text, Double)
            subTotalBiayaKirimPPN += CType(lblBiayaKirimPPN.Text, Double)


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
                Dim lblSubTotalBiayaKirimPPN As Label = e.Item.FindControl("lblSubTotalBiayaKirimPPN")
                lblSubTotalBiayaKirimPPN.Text = FormatNumber(subTotalBiayaKirimPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblTotalBiayaKirimValue.Text = FormatNumber(subTotalBiayaKirimPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True).ToString()
            End If

            Dim MDPDailyStockCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "VechileColor.ID", MatchType.Exact, objContractDetail.VechileColor.ID))
            MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "PeriodMonth", MatchType.Exact, objContractDetail.ContractHeader.ContractPeriodMonth))
            MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "PeriodYear", MatchType.Exact, objContractDetail.ContractHeader.ContractPeriodYear))
            MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "Dealer.ID", MatchType.Exact, objContractDetail.ContractHeader.Dealer.ID))
            MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "PeriodeDate", MatchType.Exact, PermintaanKirim.Day))
            MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "ProductionYear", MatchType.Exact, ProductionYear))

            Dim arrMDPDailyStock As ArrayList = New MDPDealerDailyStockFacade(User).Retrieve(MDPDailyStockCriteria)

            Dim lblSisaQty As Label = e.Item.FindControl("lblSisaQty")

            If arrMDPDailyStock.Count > 0 Then
                For Each MDPDailyData As MDPDealerDailyStock In arrMDPDailyStock
                    If Mode = "EDIT" Then
                        lblSisaQty.Text = MDPDailyData.AllocationQuantity - PengajuanStokPO + (CType(txtBox.Text, Integer) - objPODraftDetail.ReqQty)
                    Else
                        lblSisaQty.Text = MDPDailyData.AllocationQuantity - PengajuanStokPO
                    End If
                Next
            Else
                lblSisaQty.Text = 0
            End If

            Dim MDPDealerCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MDPMasterDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            MDPDealerCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPMasterDealer), "Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim arrMDPMasterDealer As ArrayList = New MDPMasterDealerFacade(User).Retrieve(MDPDealerCriteria)
            Dim objMDPMasterDealer As MDPMasterDealer = New MDPMasterDealer
            If arrMDPMasterDealer.Count > 0 Then
                objMDPMasterDealer = CType(arrMDPMasterDealer(0), MDPMasterDealer)
            End If

            Dim chkIsMDP As CheckBox = e.Item.FindControl("chkIsMDP")
            chkIsMDP.Enabled = False
            If objMDPMasterDealer.Status = 1 Then
                Dim MDPVehicleCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MDPMasterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                MDPVehicleCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPMasterVehicle), "VehicleColor.ID", MatchType.Exact, objContractDetail.VechileColor.ID))
                Dim arrMDPMasterVehicle As ArrayList = New MDPMasterVehicleFacade(User).Retrieve(MDPVehicleCriteria)

                Dim objMDPMasterVehicle As MDPMasterVehicle = New MDPMasterVehicle
                If arrMDPMasterVehicle.Count > 0 Then
                    objMDPMasterVehicle = CType(arrMDPMasterVehicle(0), MDPMasterVehicle)
                End If
                If objMDPMasterVehicle.Status = 1 Then
                    chkIsMDP.Checked = True
                    txtBox.Enabled = True
                Else
                    chkIsMDP.Checked = False
                    txtBox.Enabled = False
                End If
            Else
                chkIsMDP.Checked = False
                txtBox.Enabled = False
            End If
        End If
    End Sub

    Protected Sub btnKirim_Click(sender As Object, e As EventArgs) Handles btnKirim.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim time1, time2, time3, time4 As DateTime
        Dim interval1, interval2, interval3 As Integer
        Mode = sessionHelper.GetSession("DraftPOMode")

        'Start  : Add By WDI 20161209
        If rdoByKTB.Checked AndAlso (hidPODestinationID.Value = "" OrElse hidPODestinationID.Value = "1" OrElse hidPODestinationID.Value = "-1" OrElse txtPODestinationCode.Text.Trim() = "") Then
            MessageBox.Show("Pengiriman oleh KTB, namun PO Destination belum dipilih.")
            Exit Sub
        End If
        'End    : Add By WDI 20161209

        RegisterClientScriptBlock("ToggleEditMode", "<script language=JavaScript>IsReadOnly(false);</script>")
        time1 = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, Date.Now.Hour, Date.Now.Minute, Date.Now.Second, Date.Now.Millisecond)

        Hitung()
        UpdateAmountToJaminan()

        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        Dim isValidateSPL As Boolean = False
        If ddlOrderType.Items.Count > 0 Then
            If Mode = "EDIT" Then
                CountTotal()
            End If
            If (POIsValid(isValidateSPL)) Then
                If (PODDraftDetailIsValid()) Then
                    'BindToPODraftHeaderObject()

                    'validasi tambahan block COD
                    Dim strMsg As String = ""
                    If Not PODraftHeaderFacade.IsCODValid(objPODraftHeader, strMsg) Then
                        MessageBox.Show(strMsg)
                        Exit Sub
                    End If
                    'validasi tambahan block COD
                    If Not IsNothing(ViewState("pops")) Then
                        If ViewState("pops") Then
                            MessageBox.Show(ViewState("warning"))
                            ViewState.Remove("warning")
                            ViewState.Remove("pops")
                            Exit Sub
                        End If
                    End If

                    If Not isValidateSPL Then
                        Dim MaxTOPDay As String = String.Empty
                        Dim isTransControlPO As Boolean = CommonFunction.CheckTransactionControlPO(objDealer)
                        Dim isValidTOP As Boolean = True
                        If isTransControlPO Then
                            isValidTOP = CommonFunction.ValidateMaxTOPDaysPO(objPODraftHeader, MaxTOPDay, ddlTermOfPayment.SelectedValue)
                        Else
                            isValidTOP = CommonFunction.ValidateMaxTOPDaysPK(objPODraftHeader, MaxTOPDay, ddlTermOfPayment.SelectedValue)
                        End If
                        If Not isValidTOP Then
                            MessageBox.Show("Maximum TOP yang anda pilih melebihi " & MaxTOPDay)
                            Exit Sub
                        End If
                    End If

                    'If (PODDraftDetailIsValid()) Then
                    If objPODraftHeader.PODraftDetail.Count <> 0 Then
                        Try

                            'This checking step is put here for minimize invalid data inserting
                            If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling() Then
                                Exit Sub
                            End If
                            'MessageBox.Show("Untuk Sementara Simpan dibypass")
                            'Exit Sub

                            If Not ValidasiWaktuPengajuan() Then Exit Sub

                            If InvalidTransferDate(objPODraftHeader) Then
                                MessageBox.Show("Tanggal Jatuh Tempo melebihi Validasi Ceiling")
                                Exit Sub
                            End If

                            If Not ValidPOdest(objPODraftHeader) Then
                                MessageBox.Show("Draft PO Tidak bisa Disimpan karena Biaya Kirim = 0")
                                Exit Sub
                            End If
                            time2 = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, Date.Now.Hour, Date.Now.Minute, Date.Now.Second, Date.Now.Millisecond)

                            Dim objPODraftHeaderFacade As New PODraftHeaderFacade(User)
                            Dim id As Integer
                            If Mode = "EDIT" Then
                                Dim oPDHOri As PODraftHeader = objPODraftHeaderFacade.Retrieve(objPODraftHeader.ID)
                                objPODraftHeaderFacade.Update(objPODraftHeader)
                                SetFreeDays(objPODraftHeader)
                                Me.txtID.Text = objPODraftHeader.ID.ToString()
                                id = objPODraftHeader.ID
                                If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling(True) Then
                                    objPODraftHeaderFacade.Update(oPDHOri)
                                    MessageBox.Show("Update PO Draft gagal, Total PO yang akan disimpan melebihi Ceiling yang tersedia")
                                    Exit Sub
                                End If
                            Else
                                objPODraftHeader.DraftPONumber = GeneratePODraftNumber()
                                id = New PODraftHeaderFacade(User).Insert(objPODraftHeader)
                                Dim objNewPODraftHeader As PODraftHeader = New PODraftHeaderFacade(User).Retrieve(id)
                                SetFreeDays(objNewPODraftHeader)
                                Me.txtID.Text = id.ToString()
                                If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling(True) Then
                                    Dim POHFac As PODraftHeaderFacade = New PODraftHeaderFacade(User)
                                    POHFac.DeleteFromDB(objPODraftHeader)
                                    MessageBox.Show("Simpan Gagal : Total PO melebihi Ceiling yang tersedia")
                                    Return
                                End If
                            End If

                            time3 = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, Date.Now.Hour, Date.Now.Minute, Date.Now.Second, Date.Now.Millisecond)
                            'Dim objNewPOHeader As PODraftHeader = New PODraftHeaderFacade(User).Retrieve(id)
                            sessionHelper.RemoveSession("Contract")

                            'Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                            'objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.PO_Harian), objNewPOHeader.PONumber, -1, CInt(enumStatusPO.Status.Baru))
                            If Mode <> "EDIT" Then
                                sessionHelper.SetSession("PrevPage", sessionHelper.GetSession("PrevPagePO"))
                            End If

                            time4 = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, Date.Now.Hour, Date.Now.Minute, Date.Now.Second, Date.Now.Millisecond)

                            If Me.CtlTimeElapsed.Value = 1 Then
                                interval1 = time2.Subtract(time1).TotalMilliseconds
                                interval2 = time3.Subtract(time2).TotalMilliseconds
                                interval3 = time4.Subtract(time3).TotalMilliseconds

                                SaveTimeElapsed(0, id, interval1, interval2, interval3)
                            End If

                            If Mode = "EDIT" Then
                                If IsNothing(sessionHelper.GetSession("PrevPage")) Then
                                    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Edit Draft PO Sukses');window.location.href='../MDP/FrmMDPDailyStock.aspx';", True)
                                Else
                                    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Edit Draft PO Sukses');window.location.href='" + sessionHelper.GetSession("PrevPage") + "';", True) 'Response.Redirect(sessionHelper.GetSession("PrevPage"))
                                End If
                            Else
                                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Pembuatan Draft PO Sukses');window.location.href='../MDP/FrmMDPDailyStock.aspx';", True)
                            End If
                        Catch ex As Exception
                            MessageBox.Show("Pembuatan Draft PO Gagal, ulangi beberapa saat lagi.")
                            Return
                        End Try
                    Else
                        MessageBox.Show("Tidak ada PO Draft Detail")
                    End If
                Else
                    MessageBox.Show("Sisa O/C Berubah, Order Melebihi Sisa O/C")
                    If Not IsNothing(sessionHelper.GetSession("Contract")) Then '-- Add by Sony
                        BindDetailToGrid(objPODraftHeader)
                    End If
                End If
            End If
        Else
            MessageBox.Show("Maaf, Anda Tidak punya Akses Membuat Draft PO")
        End If
    End Sub

    Protected Sub btnHitung_Click(sender As Object, e As EventArgs) Handles btnHitung.Click
        If Not Page.IsValid Then
            Return
        End If


        Hitung()
        UpdateAmountToJaminan()

        If IsEnableCeilingFilter() AndAlso Not IsLesserThanAvailableCeiling() Then
            Exit Sub
        End If
        If Not IsNothing(ViewState("pops")) Then
            If ViewState("pops") Then
                MessageBox.Show(ViewState("warning"))
                ViewState.Remove("warning")
                ViewState.Remove("pops")
                Exit Sub
            End If
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If IsNothing(sessionHelper.GetSession("PrevPage")) Then
            Response.Redirect("../MDP/FrmMDPDailyStock.aspx")
        Else
            Response.Redirect(sessionHelper.GetSession("PrevPage"))
        End If
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        If objPODraftHeader Is Nothing Then
            objPODraftHeader = CType(sessionHelper.GetSession("PODraftHeaderEdit"), PODraftHeader)
        End If
        If Not (objPODraftHeader Is Nothing) Then
            Mode = sessionHelper.GetSession("DraftPOMode")
            If Mode = "EDIT" Then
                objPODraftHeader.Status = CType(enumStatusPO.StatusDraftPO.Batal, Integer)
                Dim objPODraftHeaderFacade As New PODraftHeaderFacade(User)
                objPODraftHeaderFacade.Update(objPODraftHeader)
                sessionHelper.RemoveSession("PODraftHeaderEdit")
                If IsNothing(sessionHelper.GetSession("PrevPage")) Then
                    'Response.Redirect("../MDP/FrmPODraftList.aspx")
                    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Draft PO Sukses Dibatalkan');window.location.href='../MDP/FrmPODraftList.aspx';", True)
                Else
                    'Response.Redirect(sessionHelper.GetSession("PrevPage"))
                    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Draft PO Sukses Dibatalkan');window.location.href='" + sessionHelper.GetSession("PrevPage") + "';", True)
                End If
            Else
                ClearControl()
            End If
        End If
    End Sub

#End Region

#Region "Custom Method"

#Region "Bind Data"
    Private Sub BindData()
        objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        Mode = sessionHelper.GetSession("DraftPOMode")
        If Mode = "CREATE" Then
            lblDealerCode.Text = objDealer.DealerCode + "/" + objDealer.SearchTerm1
            lblName.Text = objDealer.DealerName
            lblCity.Text = objDealer.City.CityName
            lblPermintaanKirim.Text = PermintaanKirim.ToString("dd/MM/yyyy")
        End If
        BindddlTermOfPayment()
        BindddlContractNumber()
    End Sub

    Private Sub BindddlContractNumber()
        If Not IsNothing(sessionHelper.GetSession("SESMDPDAILYPRODYEAR")) Then
            ProductionYear = CType(sessionHelper.GetSession("SESMDPDAILYPRODYEAR"), Integer)
        Else
            ProductionYear = Now.Year
        End If

        ddlContractNumber.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.IsCarriedOver", MatchType.No, 1))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.ContractPeriodMonth", MatchType.Exact, CInt(PermintaanKirim.Month)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.ContractPeriodYear", MatchType.Exact, CInt(PermintaanKirim.Year)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.ProductionYear", MatchType.Exact, ProductionYear))

        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.ID", Sort.SortDirection.ASC))

        Dim arrListContractDetailAll As ArrayList = New ContractDetailFacade(User).Retrieve(criterias, sortCol)
        Dim arrListSisaUnitMorethanZero As ArrayList = New ArrayList
        For Each ContractDetailItem As ContractDetail In arrListContractDetailAll
            Dim SisaContractUnit As Integer = ContractDetailItem.TargetQty

            Dim PODraftDetailCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            PODraftDetailCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "ContractDetail.ID", MatchType.Exact, ContractDetailItem.ID))
            PODraftDetailCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.Status", MatchType.No, CType(enumStatusPO.StatusDraftPO.Batal, Integer)))

            Dim arrPODraftDetail As ArrayList = New PODraftDetailFacade(User).Retrieve(PODraftDetailCriteria)
            For Each PODraftDetailItem As PODraftDetail In arrPODraftDetail
                SisaContractUnit -= PODraftDetailItem.ReqQty
            Next

            'If ContractDetailItem.SisaUnit > 0 Then
            If SisaContractUnit > 0 Then
                If arrListSisaUnitMorethanZero.IndexOf(ContractDetailItem.ContractHeaderID) = -1 Then
                    arrListSisaUnitMorethanZero.Add(ContractDetailItem.ContractHeaderID)
                    Dim li As New ListItem
                    li.Text = ContractDetailItem.ContractHeader.ContractNumber
                    li.Value = ContractDetailItem.ContractHeader.ID
                    ddlContractNumber.Items.Add(li)
                End If
            End If
        Next

        ddlContractNumber_SelectedIndexChanged(Me, System.EventArgs.Empty)
    End Sub

    Private Sub BindddlTermOfPayment()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arrListTermOfPayment As ArrayList = New TermOfPaymentFacade(User).Retrieve(criterias)
        ddlTermOfPayment.DataSource = arrListTermOfPayment
        ddlTermOfPayment.DataValueField = "ID"
        ddlTermOfPayment.DataTextField = "Description"
        ddlTermOfPayment.DataBind()
        ddlTermOfPayment.SelectedIndex = 0
    End Sub

    Private Sub BindddlOrderType(ByVal objContractHeader As ContractHeader)
        ddlOrderType.Items.Clear()
        For Each item As ListItem In LookUp.ArrayJenisPO
            If item.Text = "Harian" Then
                If SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Harian) OrElse SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Semua) Then
                    ddlOrderType.Items.Add(item)
                End If
            ElseIf item.Text = "Tambahan" Then
                If (SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Tambahan) OrElse SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Semua)) AndAlso IsValidPOTambahan() Then
                    If (objContractHeader.ContractPeriodMonth = DateTime.Now.Month) AndAlso (objContractHeader.ContractPeriodYear = DateTime.Now.Year) Then
                        ddlOrderType.Items.Add(item)
                    End If
                End If
            End If
        Next
    End Sub

#End Region

    Private Function IsValidPOTambahan() As Boolean
        Dim BatasAwal As String() = KTB.DNet.Lib.WebConfig.GetValue("AwalBatasPOTambahan").ToString().Split(":")
        Dim BatasAkhir As String() = KTB.DNet.Lib.WebConfig.GetValue("AkhirBatasPOTambahan").ToString().Split(":")
        Dim WaktuAwal As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CInt(BatasAwal(0)), CInt(BatasAwal(1)), 0)
        Dim WaktuAkhir As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CInt(BatasAkhir(0)), CInt(BatasAkhir(1)), 0)
        If Not ((DateTime.Now >= WaktuAwal) And (DateTime.Now <= WaktuAkhir)) Then
            Return False
        End If
        Return True
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

        Me.lblFactoring.Visible = IsImplementFactoring
        Me.lblFactoringColon.Visible = IsImplementFactoring
        Me.chkFactoring.Visible = IsImplementFactoring

        If ddlContractNumber.Items.Count = 0 Then
            btnKirim.Enabled = False
            btnHitung.Enabled = False
        End If
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
    End Sub

    Private Function GetProductCategory() As ProductCategory
        If Me.sessionHelper.GetSession("Contract") Is Nothing Then
            GetContract(CType(ddlContractNumber.SelectedItem.Value, Integer))
        End If
        Dim oCH As ContractHeader = CType(Me.sessionHelper.GetSession("Contract"), ContractHeader)
        Dim oPC As ProductCategory = oCH.Category.ProductCategory

        Return oPC
    End Function

    Private Function GetContract(ByVal ContractHeaderID As Integer) As ContractHeader
        'Dim contractId As String = Request.QueryString("id")
        objContractHeader = New ContractHeaderFacade(User).Retrieve(ContractHeaderID)
        sessionHelper.SetSession("Contract", objContractHeader)
        Return objContractHeader
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

        Return rslDate
    End Function

    Private Function GetItemDeposit(ByVal oCD As ContractDetail) As Double
        Return oCD.GuaranteeAmount
        Exit Function

        Dim i As Integer
        i = 0

        If oCD.ContractHeader.PKHeader.JaminanID = 0 Then
            Dim oJFac As JaminanFacade = New JaminanFacade(User)
            Dim crtJ As CriteriaComposite
            Dim arlJ As New ArrayList
            Dim dt As Date = DateSerial(PermintaanKirim.Year, PermintaanKirim.Month, PermintaanKirim.Day)
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
                            If oJD.VehicleTypeCode = oCD.VechileColor.VechileType.VechileTypeCode AndAlso (PermintaanKirim.ToShortDateString() >= oJD.Jaminan.ValidFrom And PermintaanKirim.ToShortDateString() <= oJD.Jaminan.ValidTo) And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = oCD.ContractHeader.Purpose) Then
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
                If oJD.VehicleTypeCode = oCD.VechileColor.VechileType.VechileTypeCode AndAlso (PermintaanKirim.ToShortDateString() >= oJD.Jaminan.ValidFrom And PermintaanKirim.ToShortDateString() <= oJD.Jaminan.ValidTo) And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = oCD.ContractHeader.Purpose) Then
                    Return oJD.Amount
                End If
            Next
        End If
        Return 0
    End Function

    Private Function GeneratePODraftNumber() As String
        Dim NumberHead As String = String.Empty
        Dim NumberYear As String = String.Empty
        Dim NumberMonth As String = String.Empty
        Dim DraftNumber As String = "DPO000000000" 'String.Empty
        Dim RunningNumber As Integer
        Dim NewDraftNumber As String = String.Empty
        Dim NewRunningNumber As String = String.Empty

        Dim Sql As String = "(SELECT TOP 1 ID FROM PODraftHeader ORDER BY CreatedTime Desc)"
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ID", MatchType.Exact, Sql))

        Dim arrPODraftHeader As ArrayList = New PODraftHeaderFacade(User).Retrieve(criterias)

        If arrPODraftHeader.Count > 0 Then
            For Each objPODraftHeader As PODraftHeader In arrPODraftHeader
                DraftNumber = objPODraftHeader.DraftPONumber
            Next
        End If

        DraftNumber = DraftNumber.Substring(7)
        RunningNumber = CType(DraftNumber, Integer)
        RunningNumber = RunningNumber + 1
        NewRunningNumber = "00000" + RunningNumber.ToString()
        NewRunningNumber = NewRunningNumber.Substring(NewRunningNumber.Length - 5)
        NewDraftNumber = "DPO" + DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString() + NewRunningNumber
        Return NewDraftNumber

    End Function

    Private Sub UpdateAmountToJaminan()
        For Each di As DataGridItem In dtgDetail.Items
            Dim oCD As ContractDetail
            'sessionHelper.SetSession("Contract", objContractHeader)
            objContractHeader = CType(sessionHelper.GetSession("Contract"), ContractHeader)
            If Not IsNothing(objContractHeader) Then
                oCD = objContractHeader.ContractDetails(di.ItemIndex)
                Dim lblDeposit As Label = di.FindControl("lblDeposit")
                lblDeposit.Text = FormatNumber(GetItemDeposit(oCD), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
        Next
    End Sub

    Private Sub Hitung()
        Mode = sessionHelper.GetSession("DraftPOMode")
        ViewState("Hitung") = True
        'If Mode <> "EDIT" Then
        BindToPODraftHeaderObject()
        'End If

        ' Created by Ikhsan, 20081030
        ' to force the simpan button to do Hitung first before saving to DB.
        ' Requested by Yurike as Part of CR
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        If IsEnabledCreditControl(objDealer) Then
            If objDealer.LegalStatus = "" Then
                MessageBox.Show("Dealer anda tidak memiliki TOP Transaksi.")
                Return
            End If
        End If

        objContractHeader = sessionHelper.GetSession("Contract")
        Dim isValidateSPL As Boolean = False
        'If (POIsValid()) AndAlso ValidasiWaktuPengajuan() Then
        If (POIsValid(isValidateSPL)) Then
            For Each item As DataGridItem In dtgDetail.Items
                Dim txtbox As TextBox = item.FindControl("TextBox1")
                arrOrder = sessionHelper.GetSession("Ord")
                arrOrder.Insert(item.ItemIndex, CInt(txtbox.Text))

            Next
            sessionHelper.SetSession("Ord", arrOrder)

            'If Mode <> "EDIT" Then
            BindDetailToGrid(objPODraftHeader)
            'End If
            FreeDaysCreateNew()
        End If
    End Sub

    Private Sub BindToPODraftHeaderObject()
        objPODraftHeader = New PODraftHeader
        If Not IsNothing(sessionHelper.GetSession("PODraftHeaderEdit")) Then
            objPODraftHeader = CType(sessionHelper.GetSession("PODraftHeaderEdit"), PODraftHeader)
        End If
        objContractHeader = sessionHelper.GetSession("Contract")

        'objPODraftHeader.DraftPONumber = GeneratePODraftNumber()
        If Not IsNothing(objContractHeader) Then  '---Add by Sony
            objPODraftHeader.ContractHeader = objContractHeader
            objPODraftHeader.Dealer = objContractHeader.Dealer
            If txtDealerPONumber.Text <> String.Empty Then
                Dim _DealerPONumber As String
                _DealerPONumber = txtDealerPONumber.Text.Replace(",", "")
                _DealerPONumber = _DealerPONumber.Replace(";", "")
                objPODraftHeader.DealerPONumber = _DealerPONumber
            End If
            objPODraftHeader.ReqAllocationDate = PermintaanKirim.Day
            objPODraftHeader.ReqAllocationMonth = PermintaanKirim.Month
            objPODraftHeader.ReqAllocationYear = PermintaanKirim.Year
            objPODraftHeader.ReqAllocationDateTime = PermintaanKirim.Date
            objPODraftHeader.POType = ddlOrderType.SelectedValue
            objPODraftHeader.Status = enumStatusPO.Status.Baru
            objPODraftHeader.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
            'Start  :RemainModule-DailyPO:Free PPH DoniN
            objPODraftHeader.FreePPh22Indicator = objPODraftHeader.ContractHeader.FreePPh22Indicator
            Me.SetFreePPh() 'get the latest FreePPh Status
            objPODraftHeader.FreePPh22Indicator = IIf(chkFreePPh.Checked = True, 0, 1) '  objPODraftHeader.ContractHeader.FreePPh22Indicator
            'Start  :RemainModule-DailyPO:Free PPH DoniN
            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100329
            objPODraftHeader.EffectiveDate = New PODraftHeaderFacade(User).GetPOEffectiveDate(objPODraftHeader.ReqAllocationDateTime, objPODraftHeader.TermOfPayment.PaymentType, objPODraftHeader.TermOfPayment.TermOfPaymentValue)
            'End    :Optimize EffectiveDate calculation;By:DoniN;20100329
            objPODraftHeader.IsFactoring = IIf(chkFactoring.Checked, 1, 0)

            If Not IsNothing(sessionHelper.GetSession(_sessSPL)) Then
                objPODraftHeader.SPL = CType(sessionHelper.GetSession(_sessSPL), SPL)
            End If
            If Not IsNothing(sessionHelper.GetSession(_sessIsTransfer)) Then
                objPODraftHeader.IsTransfer = CType(sessionHelper.GetSession(_sessIsTransfer), Short)
            End If

            If rdoByKTB.Checked AndAlso (hidPODestinationID.Value <> "" AndAlso hidPODestinationID.Value <> "1" AndAlso hidPODestinationID.Value <> "-1" AndAlso txtPODestinationCode.Text.Trim() <> "") Then
                objPODraftHeader.PODestination = New PODestinationFacade(User).Retrieve(CType(hidPODestinationID.Value, Integer))
            Else
                objPODraftHeader.PODestination = New PODestinationFacade(User).Retrieve(1)
            End If

            BindToPODraftDetailObject()
        End If
    End Sub

    Private Sub BindToPODraftDetailObject()
        Mode = sessionHelper.GetSession("DraftPOMode")
        For Each dtgitem As DataGridItem In dtgDetail.Items
            Dim txtBox As TextBox = dtgitem.FindControl("TextBox1")

            If Not ((txtBox.Text = String.Empty) OrElse (txtBox.Text = "0")) Then
                Dim biayaKirimAndPPN As Label = dtgitem.FindControl("lblBiayaKirimPPN")
                objPODraftDetail = GetPODraftDetail(dtgitem.ItemIndex, txtBox.Text)
                objPODraftDetail.ReqQty = txtBox.Text
                If Not IsNothing(biayaKirimAndPPN.Text) Then
                    If CType(biayaKirimAndPPN.Text, Double) > 0 Then
                        objPODraftDetail.LogisticCost = CalculateLogisticCost(CType(txtBox.Text, Double), objPODraftDetail.ContractDetail) / CType(txtBox.Text, Double)
                    Else
                        objPODraftDetail.LogisticCost = 0
                    End If
                End If
                If Mode = "EDIT" Then
                    Dim isNewItem As Boolean = False
                    For Each PODraftDetailItem As PODraftDetail In objPODraftHeader.PODraftDetail
                        If PODraftDetailItem.ContractDetail.VechileColor.MaterialNumber <> dtgitem.Cells(2).Text Then
                            isNewItem = True
                        Else
                            isNewItem = False
                            PODraftDetailItem.Interest = objPODraftDetail.Interest
                            PODraftDetailItem.AmountRewardDepA = objPODraftDetail.AmountRewardDepA
                            PODraftDetailItem.DiscountReward = objPODraftDetail.DiscountReward
                            PODraftDetailItem.AmountReward = objPODraftDetail.AmountReward
                            PODraftDetailItem.Price = objPODraftDetail.Price
                            PODraftDetailItem.PPh22 = objPODraftDetail.PPh22
                            PODraftDetailItem.LogisticCost = objPODraftDetail.LogisticCost
                            PODraftDetailItem.ReqQty = CType(txtBox.Text, Integer)
                            Exit For
                        End If
                    Next
                    If isNewItem Then
                        objPODraftHeader.PODraftDetail.Add(objPODraftDetail)
                    End If
                Else
                    objPODraftHeader.PODraftDetail.Add(objPODraftDetail)
                End If
            End If
        Next

    End Sub

    Private Function GetPODraftDetail(ByVal index As Integer, ByVal reqQty As Integer)
        objContractDetail = objContractHeader.ContractDetails(index)
        If Not IsNothing(objContractDetail) Then
            Dim poDraftDetail As New PODraftDetail
            poDraftDetail.ContractDetail = objContractDetail
            poDraftDetail.Discount = objContractDetail.Discount
            poDraftDetail.LineItem = objContractDetail.LineItem
            poDraftDetail.PODraftHeader = objPODraftHeader
            poDraftDetail.Price = objContractDetail.Amount

            If reqQty < 1 Then
                poDraftDetail.Interest = 0
            Else
                If objPODraftHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
                    poDraftDetail.Interest = CType(CType(dtgDetail.Items(index).FindControl("lblInterest"), Label).Text, Decimal) / reqQty
                Else
                    poDraftDetail.Interest = 0
                End If
            End If

            '' CR Sirkular Rewards
            '' by : ali Akbar
            '' 2014-09-24
            If objPODraftHeader.IsFactoring Then
                Dim objPrice As Price
                objPrice = New PriceFacade(User).RetrieveByCriteria(objContractDetail)

                nTOP = objPODraftHeader.TermOfPayment.TermOfPaymentValue
                nMonth = DateTime.DaysInMonth(objContractHeader.ContractPeriodYear, objContractHeader.ContractPeriodMonth)
                poDraftDetail.AmountRewardDepA = Calculation.CountRewardAmountDepositA(objPrice, nTOP, nMonth)
                poDraftDetail.DiscountReward = objPrice.DiscountReward
                poDraftDetail.AmountReward = Calculation.CountRewardAmount(objContractDetail, objPrice, nTOP, nMonth)

                poDraftDetail.Price = Calculation.CountRewardsVehiclePrice(objContractDetail, objPrice, nTOP, nMonth)
                poDraftDetail.PPh22 = Calculation.CountRewardPPh22(objContractDetail, objPrice, nTOP, nMonth)

            Else
                poDraftDetail.DiscountReward = 0
                poDraftDetail.AmountReward = 0
                poDraftDetail.AmountRewardDepA = 0
                poDraftDetail.PPh22 = objContractDetail.PPh22
            End If
            '' END OF CR Sirkular Rewards

            Return poDraftDetail
        End If
    End Function

    Private Function POIsValid(ByRef isValidateSPL As Boolean) As Boolean
        sessionHelper.SetSession(_sessSPL, Nothing)
        sessionHelper.SetSession(_sessIsTransfer, 0)
        If objPODraftHeader Is Nothing Then
            objPODraftHeader = CType(sessionHelper.GetSession("PODraftHeaderEdit"), PODraftHeader)
        End If
        'replaced with ValidasiWaktuPengajuan
        Dim objPKHeader As PKHeader = New PKHeaderFacade(User).Retrieve(objContractHeader.PKNumber)
        If objPKHeader.ID < 1 Then
            MessageBox.Show("Nomor PK : " & objContractHeader.PKNumber & " tidak ditemukan.")
            Return False
        End If
        objSPL = New SPLFacade(User).Retrieve(objPKHeader.SPLNumber.ToString())
        Dim objTOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CInt(ddlTermOfPayment.SelectedValue))
        Dim MaxTOPValue As Integer = 0

        Dim dueDate As Date = PermintaanKirim.AddDays(objTOP.TermOfPaymentValue)

        'Request Dari Bu Rini 11/2/2008 jan 11.00
        If objPODraftHeader.IsFactoring = 1 OrElse objPKHeader Is Nothing OrElse objPKHeader.MaxTopIndicator = -1 Then
            MaxTOPValue = PODraftHeaderFacade.GetMinTOPDaysByVehicleType(objPODraftHeader, objPODraftHeader.PODraftDetail, (objPODraftHeader.IsFactoring = 1))
            If MaxTOPValue = 0 Then
                MaxTOPValue = DateTime.DaysInMonth(objContractHeader.ContractPeriodYear, objContractHeader.ContractPeriodMonth)
            End If

            If objTOP.TermOfPaymentValue > (MaxTOPValue) Then
                MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
                Return False
            End If

        End If

        If objPODraftHeader.IsFactoring <> 1 Then
            'start : Installment & SPL Validation on 20160815
            Dim IsTOPBySPLOk As Boolean = False

            If Not IsNothing(objSPL) Then
                IsTOPBySPLOk = True
                objPODraftHeader.SPL = objSPL
                sessionHelper.SetSession(Me._sessSPL, objSPL)
                If objSPL.NumOfInstallment > 1 AndAlso objTOP.TermOfPaymentValue <> objSPL.MaxTOPDay Then
                    MessageBox.Show("Silahkan Masukkan Cara Pembayaran " & objSPL.MaxTOPDay.ToString() & " sesuai dengan Aplikasi " & objSPL.SPLNumber)
                    Return False
                End If
            End If
            'end : Installment & SPL Validation on 20160815 

            If ddlTermOfPayment.SelectedItem.Text.Trim.ToUpper.EndsWith("TOP") Then
                If IsTOPBySPLOk = False Then
                    If objPKHeader Is Nothing OrElse objPKHeader.MaxTopIndicator = 0 Then
                        MaxTOPValue = objPKHeader.MaxTOPDate.Subtract(PermintaanKirim).Days
                        If objTOP.TermOfPaymentValue > MaxTOPValue Then
                            MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
                            Return False
                        Else
                            If ViewState("ShowMessage") = "OK" Then
                                Return True
                            Else
                                ViewState("ShowMessage") = "OK"
                                MessageBox.Show("Maximum TOP yang bisa anda gunakan " & MaxTOPValue & " Hari")
                                Return False
                            End If
                        End If
                    End If

                    If objPKHeader Is Nothing OrElse objPKHeader.MaxTopIndicator = 1 Then
                        MaxTOPValue = objPKHeader.MaxTopDay
                        If objTOP.TermOfPaymentValue > MaxTOPValue Then
                            MessageBox.Show("Maaf TOP yang Anda Pilih Tidak boleh melebihi " & MaxTOPValue & " hari")
                            Return False
                        Else
                            If ViewState("ShowMessage") = "OK" Then
                                Return True
                            Else
                                ViewState("ShowMessage") = "OK"
                                MessageBox.Show("Maximum TOP yang bisa anda gunakan " & MaxTOPValue & " Hari")
                                Return False
                            End If
                        End If
                    End If
                End If
            End If

            If Not String.IsNullOrEmpty(objPKHeader.SPLNumber) Then
                If objPKHeader.MaxTopDay > 0 Then
                    isValidateSPL = True
                End If
            End If
            'start : add payment scheme information (Gyro or Transfer) on 20160815
            objPODraftHeader.IsTransfer = Me.GetCurrentPaymentMethod(objPKHeader, objPODraftHeader)
            sessionHelper.SetSession(_sessIsTransfer, objPODraftHeader.IsTransfer)
            'end : add payment scheme information (Gyro or Transfer) on 20160815

        Else
            objPODraftHeader.IsTransfer = 0
            sessionHelper.SetSession(_sessIsTransfer, objPODraftHeader.IsTransfer)
        End If

        Return True
    End Function

    Private Function GetCurrentPaymentMethod(ByRef objPKHeader As PKHeader, ByRef obPODraftHeader As PODraftHeader) As Short


        'start : add payment scheme information (Gyro or Transfer) on 20160815
        Dim cTC As New CriteriaComposite(New Criteria(GetType(TransferControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim curPeriod As Date = DateSerial(PermintaanKirim.Year, PermintaanKirim.Month, 1)
        Dim sTC As New SortCollection()
        Dim oTCFac As New TransferControlFacade(User)
        Dim aTCs As ArrayList
        Dim oTC As TransferControl
        Dim state As Short

        cTC.opAnd(New Criteria(GetType(TransferControl), "CreditAccount", MatchType.Exact, objPKHeader.Dealer.CreditAccount))
        cTC.opAnd(New Criteria(GetType(TransferControl), "ValidFrom", MatchType.LesserOrEqual, curPeriod))
        cTC.opAnd(New Criteria(GetType(TransferControl), "ProductCategory.ID", MatchType.Exact, objPKHeader.Category.ProductCategory.ID))
        cTC.opAnd(New Criteria(GetType(TransferControl), "PaymentType", MatchType.Exact, objPODraftHeader.TermOfPayment.PaymentType))

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

    Private Function IsEnabledCreditControl(ByVal objDealer As Dealer) As Boolean
        Dim _poHeaderFacade As PODraftHeaderFacade = New PODraftHeaderFacade(User)
        If _poHeaderFacade.IsEnabledCreditControl(objDealer) Then
            Return True
        End If
        Return False
    End Function

    Private Function ValidasiWaktuPengajuan() As Boolean
        objDealer = Session("DEALER")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, PermintaanKirim.Day))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, PermintaanKirim.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, PermintaanKirim.Year))
        Dim arlNationalHoliday As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criterias)
        If (arlNationalHoliday.Count = 0) Then
            'start  :check, buat hari libur, dan tanggal permintaan kirim adalah hari pertama masuk setelah libur
            If CommonFunction.AddNWorkingDay(DateSerial(Now.AddDays(-1).Year, Now.AddDays(-1).Month, Now.AddDays(-1).Day), 1, False).ToString("yyyyMMdd") <> Now.ToString("yyyyMMdd") Then 'it means holiday
                If CommonFunction.AddNWorkingDay(DateSerial(Now.AddDays(-1).Year, Now.AddDays(-1).Month, Now.AddDays(-1).Day), 1, False).ToString("yyyyMMdd") = Me.PermintaanKirim.ToString("yyyyMMdd") Then
                    MessageBox.Show("Tanggal permintaan kirim " & Me.PermintaanKirim.ToString("dd MMM yyyy") & " tidak bisa dibuat hari ini. \nPengajuan maksimal pada tanggal " & CommonFunction.AddNWorkingDay(Now, 1, True).ToString("dd MMM yyyy"))
                    Return False
                End If
            End If
            'end    :check, buat hari libur, dan tanggal permintaan kirim adalah hari pertama masuk setelah libur
            objContractHeader = sessionHelper.GetSession("Contract")
            If Not IsNothing(objContractHeader) Then '-- Add by Sony
                If ddlOrderType.SelectedValue = LookUp.EnumJenisOrder.Harian Then
                    Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, EnumDealerTransType.DealerTransKind.POBulanan)
                    If Not (objTransaction Is Nothing) Then
                        If objTransaction.Status = 0 Then
                            MessageBox.Show("Maaf Anda Tidak Punya Akses membuat PO Harian")
                            Return False
                        Else
                            If (PermintaanKirim > DateTime.Now) AndAlso (PermintaanKirim.Month = objContractHeader.ContractPeriodMonth) Then
                                If Not (PermintaanKirim.Date = DateTime.Now.Date) Then
                                    Dim nextDate As Date = New NationalHolidayFacade(User).RetrieveNextDay(DateTime.Now.AddDays(1))
                                    If (PermintaanKirim.Date = nextDate.Date) Then
                                        Dim Batas As String() = KTB.DNet.Lib.WebConfig.GetValue("BatasPOHarian").ToString().Split(":")
                                        Dim Waktu As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CInt(Batas(0)), CInt(Batas(1)), 0)
                                        If Not (DateTime.Now < Waktu) Then
                                            'MessageBox.Show("Batas Waktu pembuatan PO untuk pengiriman Besok sudah lewat")
                                            MessageBox.Show(SR.InvalidCreateDate("PO"))
                                            Return False
                                        End If
                                    End If
                                Else
                                    MessageBox.Show(SR.InvalidSendDate)
                                End If
                            Else
                                'reamrks by anh 20160630 req by yurike
                                ' Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetValue("PODateAllowed") 'yyyyMMdd - 20141031
                                Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetString(SR.PODateAllowed)
                                If Not (Date.Now.ToString("yyyyMMdd") = strPODateAllowed) Then
                                    MessageBox.Show(SR.InvalidSendDate)
                                    Return False
                                End If
                                'end reamrks by anh 20160630 req by yurike
                            End If
                        End If
                    End If
                Else
                    Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, EnumDealerTransType.DealerTransKind.POTambahan)
                    If Not (objTransaction Is Nothing) Then
                        If objTransaction.Status = 0 Then
                            MessageBox.Show("Maaf Anda Tidak Punya Akses Membuat PO Tambahan")
                            Return False
                        Else
                            If Not (IsValidPOTambahan()) Then
                                MessageBox.Show(SR.InvalidCreateDate("PO Tambahan"))
                                Return False
                            Else
                                Dim nextDatePO As Date = New NationalHolidayFacade(User).RetrieveNextDay(DateTime.Now.AddDays(1))
                                Dim startDatePO As Date = New Date(nextDatePO.Year, nextDatePO.Month, nextDatePO.Day, 0, 0, 0)
                                Dim endDatePO As Date = New Date(nextDatePO.Year, nextDatePO.Month, nextDatePO.Day, 23, 59, 59)

                                If Not ((PermintaanKirim.Date >= startDatePO) And (PermintaanKirim.Date <= endDatePO)) Then
                                    'reamrks by anh 20160630 req by yurike
                                    ' Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetValue("PODateAllowed") 'yyyyMMdd - 20141031
                                    Dim strPODateAllowed As String = KTB.DNet.Lib.WebConfig.GetString(SR.PODateAllowed)
                                    If Not (Date.Now.ToString("yyyyMMdd") = strPODateAllowed) Then
                                        MessageBox.Show(SR.InvalidSendDate)
                                        Return False
                                    End If

                                    'end reamrks by anh 20160630 req by yurike
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Else
            MessageBox.Show("Permintaan kirim tidak boleh di hari Libur kerja (" & arlNationalHoliday(0).Description & ")")
            Return False
        End If
        'start   : OC carry over dengan pricing-date baru (contoh : oc carryover 2 Jan tapi pakai pricingdate 15 jan, karena lebih murah, permintaan dealer
        GetContract(CType(ddlContractNumber.SelectedItem.Value, Integer))
        Dim PricingDate As Date = New Date(objContractHeader.PricePeriodYear, objContractHeader.PricePeriodMonth, objContractHeader.PricePeriodDay)
        Dim DeliveryDate As Date = Me.PermintaanKirim
        If DeliveryDate < PricingDate Then
            MessageBox.Show("Tanggal permintaan kirim tidak boleh sebelum tanggal berlakunya harga kendaraan " & PricingDate.ToString("dd MM yyyy") & ". Untuk lebih lanjut hubungi MMKSI – Whole Sales Dept.")
            Return False
        End If
        'end     : OC carry over dengan pricing-date baru (contoh : oc carryover 2 Jan tapi pakai pricingdate 15 jan, karena lebih murah, permintaan dealer
        Return True
    End Function

    Private Function PODDraftDetailIsValid() As Boolean
        objContractHeader = sessionHelper.GetSession("Contract")
        If Not IsNothing(objContractHeader) Then '--Add by Sony
            For Each item As DataGridItem In dtgDetail.Items
                Dim txtBox As TextBox = item.FindControl("TextBox1")
                Dim lblSisaHariIni As Label = item.FindControl("lblSisaQty")
                Mode = sessionHelper.GetSession("DraftPOMode")
                Dim Penambahan As Integer = 0
                Dim objNewPODraftDetail As PODraftDetail
                If Mode = "EDIT" Then
                    'Dim POid As String = Request.QueryString("id")
                    'Dim objNewPODraftHeader = New PODraftHeaderFacade(User).Retrieve(CInt(POid))
                    If Not IsNothing(objPODraftHeader) Then
                        If objPODraftHeader.PODraftDetail.Count > 0 Then
                            If objPODraftHeader.PODraftDetail.Count - 1 >= item.ItemIndex Then
                                objNewPODraftDetail = objPODraftHeader.PODraftDetail(item.ItemIndex)
                                Dim objPODraftDetailExisting As PODraftDetail = New PODraftDetailFacade(User).Retrieve(objNewPODraftDetail.ID)
                                Penambahan = CInt(txtBox.Text) - objPODraftDetailExisting.ReqQty
                            End If
                        End If
                    End If
                End If

                'If (CInt(txtBox.Text) > CType(objContractHeader.ContractDetails(item.ItemIndex), ContractDetail).SisaUnit) Then
                If (CInt(txtBox.Text) > CInt(item.Cells(4).Text) OrElse Penambahan > CInt(lblSisaHariIni.Text)) Then
                    Return False
                End If
            Next
            Return True
        End If
    End Function

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

    Private Function InvalidTransferDate(ByVal poh As PODraftHeader) As Boolean
        If objPODraftHeader.IsTransfer = 1 AndAlso objPODraftHeader.IsFactoring = 0 AndAlso objPODraftHeader.TermOfPayment.PaymentType = CInt(enumPaymentType.PaymentType.TOP) Then

            Dim objD As Dealer = Session("DEALER")
            Dim productCategoryId As Integer = GetProductCategory().ID
            Dim vJthTempo As DateTime = poh.ReqAllocationDateTime.AddDays(poh.TermOfPayment.TermOfPaymentValue)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "CreditAccount", MatchType.Exact, objD.CreditAccount))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "PaymentType", MatchType.Exact, objPODraftHeader.TermOfPayment.PaymentType))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "ProductCategory.ID", MatchType.Exact, productCategoryId))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferControl), "ValidFrom", MatchType.LesserOrEqual, objPODraftHeader.ReqAllocationDateTime))
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

    Private Function ValidPOdest(ByVal objPOHed As PODraftHeader) As Boolean

        If Not IsNothing(objPOHed.PODestination) AndAlso objPOHed.PODestination.ID > 1 Then

            For Each pod As PODraftDetail In objPOHed.PODraftDetail
                If pod.LogisticCost = 0 Then
                    Return False
                End If
            Next
        End If

        Return True
    End Function

    Private Sub SaveTimeElapsed(ByVal mode As Short, ByVal poID As Integer, ByVal validationTimeElapsed As Integer, ByVal savingTimeElapsed As Integer, ByVal validationTimeElapsed2 As Integer)
        Try
            Dim perfLogFacade As New KTB.DNet.BusinessFacade.Helper.PerformanceLogFacade(User)
            Dim objPerfLog As New PerformanceLog
            objPerfLog.Modul = "Create PO Draft"
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

    Private Sub EnableControl(ByVal Mode As Boolean)
        ddlContractNumber.Enabled = Mode
        txtDealerPONumber.Enabled = Mode
        ddlOrderType.Enabled = Mode
        ddlTermOfPayment.Enabled = Mode
        txtPODestinationCode.Enabled = Mode
        rdoByKTB.Enabled = Mode
        rdoByDealer.Enabled = Mode
        lblSearchPODestination.Enabled = Mode

    End Sub

    Private Sub GetPO()
        Dim POid As String = Request.QueryString("id")
        objPODraftHeader = New PODraftHeaderFacade(User).Retrieve(CInt(POid))
        objContractHeader = GetContract(objPODraftHeader.ContractHeader.ID)
        For i As Integer = 0 To objPODraftHeader.PODraftDetail.Count - 1
            arrOrder.Add(CType(objPODraftHeader.PODraftDetail(i), PODraftDetail).ReqQty)
        Next
        sessionHelper.SetSession("Ord", arrOrder)
        sessionHelper.SetSession("PODraftHeaderEdit", objPODraftHeader)
    End Sub

    Private Sub BindDataForEdit()
        lblDealerCode.Text = objPODraftHeader.Dealer.DealerCode + "/" + objPODraftHeader.Dealer.SearchTerm1
        lblName.Text = objPODraftHeader.Dealer.DealerName
        lblCity.Text = objPODraftHeader.Dealer.City.CityName

        PermintaanKirim = CType(objPODraftHeader.ReqAllocationDateTime, Date)
        sessionHelper.SetSession("PermintaanKirim", PermintaanKirim)

        BindData()
        lblPermintaanKirim.Text = objPODraftHeader.ReqAllocationDateTime.ToString("dd/MM/yyyy")
        ddlContractNumber.SelectedValue = objPODraftHeader.ContractHeader.ID
        GetContract(objPODraftHeader.ContractHeader.ID)
        lblNomorDraftPO.Text = objPODraftHeader.DraftPONumber
        txtDealerPONumber.Text = objPODraftHeader.DealerPONumber
        ddlOrderType.SelectedValue = objPODraftHeader.POType
        ddlTermOfPayment.SelectedValue = objPODraftHeader.TermOfPayment.ID
        chkFactoring.Checked = IIf(objPODraftHeader.IsFactoring = 1, True, False)

        lblOrderType.Text = CType(objPODraftHeader.ContractHeader.ContractType, enumOrderType.OrderType).ToString()
        lblSalesOrg.Text = objPODraftHeader.ContractHeader.Category.CategoryCode
        lblTahunPerakitan.Text = objPODraftHeader.ContractHeader.ProductionYear
        lblProjectName.Text = objPODraftHeader.ContractHeader.ProjectName
        lblTotalBiayaKirimValue.Text = FormatNumber(CType(objPODraftHeader.TotalHargaLC, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        chkFreePPh.Checked = IIf(objPODraftHeader.FreePPh22Indicator = 0, True, False)
        If Not IsNothing(objPODraftHeader.PODestination) Then
            hidPODestinationID.Value = objPODraftHeader.PODestination.ID
            If hidPODestinationID.Value = "-1" OrElse hidPODestinationID.Value = "1" Then
                txtPODestinationCode.Text = ""
                rdoByDealer.Checked = True
            Else
                txtPODestinationCode.Text = objPODraftHeader.PODestination.Code & "/ " & objPODraftHeader.PODestination.Nama
                rdoByKTB.Checked = True
            End If
        End If
        arrOrder.Clear()
        For i As Integer = 0 To objPODraftHeader.PODraftDetail.Count - 1
            arrOrder.Add(CType(objPODraftHeader.PODraftDetail(i), PODraftDetail).ReqQty)
        Next
        sessionHelper.SetSession("Ord", arrOrder)
        BindDetailToGrid(objPODraftHeader)
    End Sub

    Private Sub ClearControl()
        If ddlContractNumber.Items.Count > 0 Then
            ddlContractNumber.SelectedIndex = 0
        End If
        If ddlOrderType.Items.Count > 0 Then
            ddlOrderType.SelectedIndex = 0
        End If
        If ddlTermOfPayment.Items.Count > 0 Then
            ddlTermOfPayment.SelectedIndex = 0
        End If
        If ddlOrderType.Items.Count > 0 Then
            ddlOrderType.SelectedIndex = 0
        End If
        txtDealerPONumber.Text = String.Empty
        lblSearchPODestination.Text = String.Empty
    End Sub

    Private Sub CheckUserPrivilege()
        Mode = sessionHelper.GetSession("DraftPOMode")
        If Mode = "VIEW" Then
            If Not SecurityProvider.Authorize(Context.User, SR.MDP_Daftar_PO_Draft_Display_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Buat PO Draft")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.MDPBuatPODraft_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Buat PO Draft")
            End If
        End If
        'If (Not SecurityProvider.Authorize(Context.User, SR.MDPBuatPODraft_Privilege)) AndAlso (Not SecurityProvider.Authorize(Context.User, SR.MDP_Daftar_PO_Draft_Display_Privilege)) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=Buat PO Draft")
        'End If
        btnKirim.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPO_Save_Privilege)
        btnBatal.Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPO_Save_Privilege)

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        lblTotalBiayaKirimValue.Visible = isPriceVisible
        dtgDetail.Columns(7).Visible = isPriceVisible
        dtgDetail.Columns(8).Visible = isPriceVisible
        dtgDetail.Columns(9).Visible = isPriceVisible
        dtgDetail.Columns(10).Visible = isPriceVisible
    End Sub

#Region "Ceiling"
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
            AvCeiling = GetRemainCeiling(AvCeiling, objD.CreditAccount, PaymentType, DateSerial(Now.Year, Now.Month, Now.Day), PermintaanKirim)
        End If

        lblAvailable.Text = FormatNumber(AvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblA.Text = ViewState.Item("FormA")
        lblB.Text = ViewState.Item("FormB")
        lblC.Text = ViewState.Item("FormC")
        lblD.Text = ViewState.Item("FormD")

        If TotalPO > AvCeiling Then
            MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
            Return False
        Else
            Return True
        End If

    End Function

    Private Function GetCeilingCredit(ByVal PC As ProductCategory, ByVal CreditAccount As String, ByVal PaymentType As Short) As sp_CreditMaster
        Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
        Dim arlSCM As ArrayList
        Dim objSCM As sp_CreditMaster
        Dim crtSCM As CriteriaComposite
        Dim ReportDate As Date = DateSerial(Now.Year, Now.Month, Now.Day)
        Dim ReqDelDate As Date = PermintaanKirim

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

    Private Function GetRemainCeiling(ByVal AvCeiling As Decimal, ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
        Dim RemCeilH As Decimal = 0
        Dim RemCeilHPlus1 As Decimal = 0
        Dim i As Integer
        Dim TotReq As Decimal = 0
        Dim TotCair As Decimal = 0
        Dim FocusedDate As Date

        For i = 0 To DateDiff(DateInterval.Day, StartDate, EndDate) - 1
            If i = 0 Then
                FocusedDate = StartDate
                If FocusedDate = EndDate Then Exit For
                TotReq = GetReqPO(CreditAccount, PaymentType, FocusedDate, FocusedDate)

                TotCair = 0 'GetPOCair(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                AvCeiling = AvCeiling - TotReq + TotCair 'it's covered by SAP Application
            Else
                FocusedDate = AddWorkingDay(FocusedDate, 1)
                If FocusedDate = EndDate Then Exit For
                TotReq = GetReqPO(CreditAccount, PaymentType, FocusedDate, FocusedDate)
                TotCair = GetPOCair(CreditAccount, PaymentType, FocusedDate, FocusedDate)
                AvCeiling = AvCeiling - TotReq + TotCair
            End If
        Next

        StartDate = EndDate
        Dim TotalA As Decimal = GetReqPO(CreditAccount, PaymentType, StartDate, EndDate)
        Dim TotalB As Decimal = GetPOCair(CreditAccount, PaymentType, StartDate, EndDate)
        lblAvCeilingFirst.Text = AvCeiling
        lblA.Text = TotalA
        lblC.Text = TotalB
        RemCeilH = AvCeiling - TotalA + TotalB

        TotalA = GetReqPO(CreditAccount, PaymentType, AddWorkingDay(StartDate, 1), AddWorkingDay(StartDate, 1))
        TotalB = GetPOCair(CreditAccount, PaymentType, AddWorkingDay(StartDate, 1), AddWorkingDay(StartDate, 1))
        lblB.Text = TotalA
        lblD.Text = TotalB
        RemCeilHPlus1 = RemCeilH - TotalA + TotalB
        If RemCeilH < RemCeilHPlus1 Then
            Return RemCeilH
        Else
            Return RemCeilHPlus1
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

    End Function

    Public Function IsLesserThanAvailableCeiling(Optional ByVal IsAfterSaving As Boolean = False) As Boolean
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
                    Dim dtJatuhTempo As Date = DateAdd(DateInterval.Day, oTEOP.TermOfPaymentValue, PermintaanKirim)
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
                Dim paymentScheme As Short = Me.GetCurrentPaymentMethod(objPODraftHeader.ContractHeader.PKHeader, objPODraftHeader)

                If paymentScheme = TransferControl.EnumPaymentScheme.Gyro Then

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
                        Dim dtJatuhTempo As Date = DateAdd(DateInterval.Day, oTEOP.TermOfPaymentValue, PermintaanKirim)
                        If dtJatuhTempo > objSCM.MaxTOPDate Then
                            MessageBox.Show("Jatuh Tempo PO Melebihi Tanggal Validitas Ceiling")
                            Return False
                        End If
                    End If

                    IsLesser = CommonFunction.IsCeilingEnoughSimulationTOP(Me.GetProductCategory(), CType(Me.txtID.Text, Integer), PermintaanKirim, TotalPO, objD.CreditAccount, oTEOP.PaymentType, IsAfterSaving, Ceiling, Proposed, Liquified, Outstanding, TodaysAvCeiling, TomorrowAvCeiling, AvCeiling)
                    Me.lblCeiling.Text = FormatNumber(Ceiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Me.lblProposed.Text = FormatNumber(Proposed, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Me.lblLiquified.Text = FormatNumber(Liquified, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Me.lblOutstanding.Text = FormatNumber(Outstanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Me.lblTodayAvCeiling.Text = FormatNumber(TodaysAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Me.lblTomorrowAvCeiling.Text = FormatNumber(TomorrowAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

                    Me.lblAvailable.Text = FormatNumber(AvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Else 'paymentSchema = TRANSFER
                    Dim i As Integer = objPODraftHeader.ID
                    Dim oTCFac As New TransferCeilingFacade(User)
                    Dim oTC As New TransferCeiling()
                    Dim IsEnough As Boolean = False
                    Dim sMsg As String = ""
                    Dim NewAvCeiling As Decimal = 0

                    IsEnough = oTCFac.IsEnoughCeiling(objPODraftHeader, TotalPO, oTC, NewAvCeiling, sMsg)
                    Me.lblCeiling.Text = FormatNumber(NewAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

                    If Not IsEnough Then MessageBox.Show(sMsg)

                    Return IsEnough

                End If
            End If
        End If
        If IsLesser = False Then
            MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
        End If
        Return IsLesser
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
        Dim ReqDelDate As Date = DateSerial(PermintaanKirim.Year, PermintaanKirim.Month, PermintaanKirim.Day)
        Dim tmpTotal As Decimal = 0
        Dim DPFacade As New DailyPaymentFacade(User)

        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Dealer.CreditAccount", MatchType.Exact, objDealer.CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(TodayDate.AddMonths(-9), "yyyy/MM/dd")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH

            EffectiveDate = IIf(objPOH.IsHavingGyro, ReqDelDate, objPOH.EffectiveDate)
            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100329
            If EffectiveDate >= TodayDate And EffectiveDate <= ReqDelDate Then
                If objPOH.Status = 8 Then
                    If EffectiveDate >= TodayDate.AddDays(1) Then
                        If objPOH.DailyPayments.Count = 0 Then
                            tmpTotal = objPOH.TotalPODetail()
                            TotalLiquefied += tmpTotal ' objPOH.TotalPODetail()
                        Else
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

        Next

        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim crtDP As CriteriaComposite
        Dim arlDP As ArrayList
        'Accelerated Gyro
        Dim sqlAccGyro As String = "select dp.ID from DailyPayment dp inner join DailyPayment dp2 on dp2.ID=dp.AcceleratorID and dp2.GyroType=" & CType(EnumGyroType.GyroType.Percepatan, Integer).ToString()
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

        Dim aggregatesGyro As New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
        TotalAcceleratedGyro = objDPFac.GetAggregateResult(aggregatesGyro, crtDP)

        arlResult.Add(TotalLiquefied)
        arlResult.Add(TotalAcceleratedGyro)
        Return arlResult
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
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, String)))
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
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, String)))
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

    Private Sub CountTotal()
        Dim Total As Double
        For Each item As DataGridItem In dtgDetail.Items
            Dim txtbox As TextBox = item.FindControl("TextBox1")
            arrOrder = sessionHelper.GetSession("Ord")
            arrOrder.Insert(item.ItemIndex, CInt(txtbox.Text))
        Next
        sessionHelper.SetSession("Ord", arrOrder)
        'sessionHelper.SetSession("PO", objPOHeader)
        BindDetailToGrid(objPODraftHeader)
    End Sub

    Private Sub SetFreeDays(poDraftHeader As PODraftHeader)
        'poHeader = New POHeaderFacade(User).Retrieve(687730)
        Dim dt As Date = PermintaanKirim
        Dim warning As String = ""
        Dim MaxTop As Integer = 0
        Mode = sessionHelper.GetSession("DraftPOMode")
        objDealer = CType(Session("DEALER"), Dealer)

        Dim pArr As ArrayList = sessionHelper.GetSession("Hitung")
        For Each pd As PODraftDetail In pArr
            For Each pdNew As PODraftDetail In objPODraftHeader.PODraftDetail
                If pd.ContractDetail.ID = pdNew.ContractDetail.ID Then
                    pdNew.FreeDays = pd.FreeDays
                    pdNew.MaxTOPDay = pd.MaxTOPDay
                    If poDraftHeader.IsFactoring <> 1 Then
                        pdNew.Interest = CalculateInterestNonFactoring(pdNew) / pdNew.ReqQty
                    Else
                        pdNew.Interest = CalCulateInterestFactoring(pdNew) / pdNew.ReqQty
                    End If
                    Dim PDFacade As New PODraftDetailFacade(User)
                    PDFacade.Update(pdNew)
                    Exit For
                End If
            Next
        Next

    End Sub

    Private Sub HitungSetFreeDays(poDraftHeader As PODraftHeader, ByRef _MaxTOP As Integer, ByRef _FreeDays As Integer, ByRef warning As String, ByRef popup As Boolean)
        Dim dt As Date = PermintaanKirim
        'Dim warning As String = ""
        objDealer = CType(Session("DEALER"), Dealer)
        Mode = sessionHelper.GetSession("DraftPOMode")
        sessionHelper.SetSession("Itung", True)
        _FreeDays = SetFreeDaysModeCreate(objDealer, poDraftHeader.PODraftDetail, dt, dt, dt, _MaxTOP, warning)

        If _FreeDays = -1 AndAlso _MaxTOP = -1 Then
            popup = True
            Exit Sub
        End If

        sessionHelper.RemoveSession("Itung")

    End Sub

    Public Function SetFreeDaysModeCreate(Dealer As Dealer, arrPODraftDetail As ArrayList, recAllocDateTime As Date, ValidFrom As Date, ValidTo As Date, ByRef VarMaxTOP As Integer, ByRef LastPeriodeRemain As String) As Integer
        Try
            If chkFactoring.Checked Then
                VarMaxTOP = 0
                LastPeriodeRemain = ""
                Return 0
            End If
        Catch
        End Try
        Mode = sessionHelper.GetSession("DraftPOMode")
        Dim sessHelp As SessionHelper = New SessionHelper
        Dim POTargetFac As New DealerPOTargetFacade(User)
        Dim modelID As String = ""
        Dim detaiD As New ArrayList
        Dim _return As Integer = 0
        For Each podetail As PODraftDetail In arrPODraftDetail
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
        Dim arlModel As ArrayList = POTargetFac.Retrieve(criteria)
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
        'criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.Exact, PODetail.ContractDetail.VechileColor.VechileType.VechileModel.ID))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerPOTarget), "ValidTo", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
        Dim arlPOTarget As ArrayList = POTargetFac.Retrieve(criteria, sortColl)

        Dim mods = From arl As DealerPOTarget In arlPOTarget
                   Select arl.VechileModel.ID Distinct

        Dim mods2 = From arl As DealerPOTarget In arlModel
        Select arl.VechileModel.ID Distinct

        Dim modsArr As New ArrayList
        For Each a As Short In mods
            modsArr.Add(a)
        Next

        Dim modsArr2 As New ArrayList
        For Each a As Short In mods2
            modsArr2.Add(a)
        Next
        Dim ada As Boolean = False
        Dim gaada As Boolean = False
        For Each st As String In modelID.Split(",")
            If modsArr2.Contains(CType(st, Short)) Then
                ada = True
            Else
                gaada = True
            End If
        Next

        If ada AndAlso gaada Then
            _return = -1
            VarMaxTOP = -1
            LastPeriodeRemain = "Model kendaraan Program TOP Khusus tidak dapat digabungkan dengan Model Kendaraan lain"
            ViewState("warning") = LastPeriodeRemain
            ViewState("pops") = True
            Return _return
        ElseIf Not ada AndAlso gaada Then
            _return = 0
            VarMaxTOP = 0
            Return _return
        End If

        Dim arlPoDetail As New ArrayList
        'If Not sessHelp.GetSession("Itung") Then
        '    arlPoDetail = objPODraftHeader.PODraftDetail
        'Else
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
        'End If

        Dim PDraftDetailFac As New PODraftDetailFacade(User)
        Dim criteriaPDraftD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODraftDetail), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
        criteriaPDraftD.opAnd(New Criteria(GetType(PODraftDetail), "PODraftHeader.Dealer.ID", MatchType.Exact, Dealer.ID))
        criteriaPDraftD.opAnd(New Criteria(GetType(PODraftDetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
        criteriaPDraftD.opAnd(New Criteria(GetType(PODraftDetail), "PODraftHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
        criteriaPDraftD.opAnd(New Criteria(GetType(PODraftDetail), "PODraftHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
        criteriaPDraftD.opAnd(New Criteria(GetType(PODraftDetail), "PODraftHeader.Status", MatchType.InSet, ("(0)")))
        criteriaPDraftD.opAnd(New Criteria(GetType(PODraftDetail), "PODraftHeader.IsFactoring", MatchType.Exact, 0))
        Dim arlPoDraftDetail As ArrayList = PDraftDetailFac.Retrieve(criteriaPDraftD)

        If arlPoDraftDetail.Count > 0 Then
            For Each oPDraftD As PODraftDetail In arlPoDraftDetail
                Dim isValidToAdd As Boolean = True
                Dim oPHeader As POHeader = New POHeader
                Dim oPDetail As PODetail = New PODetail
                If IsNothing(oPDraftD.PODraftHeader.POHeader) Then
                    oPHeader.ID = 0
                    oPHeader.Status = 0
                    oPDetail.ReqQty = oPDraftD.ReqQty
                    oPDetail.AllocQty = oPDraftD.ReqQty
                    oPDetail.FreeDays = oPDraftD.FreeDays
                    oPDetail.ID = IIf(IsNothing(oPDraftD.ID), 0, oPDraftD.ID)
                    oPHeader.IsFactoring = oPDraftD.PODraftHeader.IsFactoring
                    oPHeader.PODetails.Add(oPDetail)
                    If Mode = "EDIT" Then
                        For Each submitedPODraftDetail As PODraftDetail In arrPODraftDetail
                            If submitedPODraftDetail.ID = oPDetail.ID Then
                                isValidToAdd = False
                            End If
                        Next
                    End If
                    If isValidToAdd Then
                        arlPoDetail.Add(oPHeader.PODetails(0))
                    End If
                End If
            Next
        End If

        Dim AllocRemain As Integer = 0
        Dim ExpiredPeriode As Boolean = False
        Dim OverQuantity As Boolean = False
        Dim CurrentQuantity As Integer = 0
        Dim arlPeriodeRemain As New ArrayList
        Dim dFDays As New Dictionary(Of Integer, Integer)
        Dim dFDaysTarget As New Dictionary(Of Integer, Integer)

        For Each pDetail As PODetail In arlPoDetail
            If Not IsNothing(pDetail.POHeader) Then
                If pDetail.POHeader.IsFactoring <> 0 Then
                    Continue For
                End If
            End If

            If Not IsNothing(sessHelp.GetSession("EditPO")) OrElse sessHelp.GetSession("EAlloc") Then
                If detaiD.Contains(pDetail.ID) Then
                    pDetail.FreeDays = 0
                    recAllocDateTime = ValidFrom
                    If sessHelp.GetSession("EAlloc") AndAlso sessHelp.GetSession("Itung") Then
                        For Each _d As PODraftDetail In arrPODraftDetail
                            If pDetail.AllocQty <> _d.ReqQty AndAlso pDetail.ID = _d.ID Then
                                pDetail.AllocQty = _d.ReqQty
                            End If
                        Next
                    End If
                End If
            End If

            If Not dFDays.ContainsKey(pDetail.FreeDays) Then
                dFDays.Add(pDetail.FreeDays, 0)
            End If
            If sessHelp.GetSession("Itung") OrElse sessHelp.GetSession("EAlloc") Then
                If Not IsNothing(pDetail.POHeader) Then
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
                    'buat yg mdp
                    dFDays(pDetail.FreeDays) += pDetail.ReqQty
                End If
            Else
                dFDays(pDetail.FreeDays) += pDetail.ReqQty
            End If
        Next

        If sessHelp.GetSession("Itung") AndAlso Not sessHelp.GetSession("EAlloc") Then
            If Not dFDays.ContainsKey(0) Then
                dFDays.Add(0, 0)
            End If
            For Each PoDe As PODraftDetail In arrPODraftDetail
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
        sessHelp.RemoveSession("Warning")
        Dim carryOver As Integer = 0
        For Each dPOT As DealerPOTarget In arlPOTarget
            If Not dFDaysTarget.ContainsKey(dPOT.FreeDays) Then
                dFDaysTarget.Add(dPOT.FreeDays, dPOT.MaxQuantity)
            End If

            If carryOver > 0 Then
                dFDaysTarget(dPOT.FreeDays) += carryOver
            End If

            If dFDays.ContainsKey(dPOT.FreeDays) Then
                dFDaysTarget(dPOT.FreeDays) -= dFDays(dPOT.FreeDays)
                dFDays.Remove(dPOT.FreeDays)
                AllocRemain += dFDaysTarget(dPOT.FreeDays)
            End If
            carryOver = 0
            If recAllocDateTime <= dPOT.ValidTo Then
                ExpiredPeriode = False
            ElseIf recAllocDateTime > dPOT.ValidTo Then
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
                    If dFDaysTarget(dPOT.FreeDays) = 0 Then
                        dFDaysTarget.Remove(dPOT.FreeDays)
                        Continue For
                    ElseIf OverQuantity AndAlso (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
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
                        'Continue For

                        If LastPeriodeRemain.Length = 0 Then
                            LastPeriodeRemain = "Sisa freedays pada periode " & dPOT.Sequence & " untuk kendaraan " & dPOT.VechileModel.IndDescription & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & " Unit \n"
                        Else
                            LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays pada periode " & dPOT.Sequence & " untuk kendaraan " & dPOT.VechileModel.IndDescription & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & " Unit \n"
                        End If
                        sessHelp.SetSession("Warning", LastPeriodeRemain)
                        dFDaysTarget.Remove(dPOT.FreeDays)
                        VarMaxTOP = -1
                        Return -1
                    Else
                        Continue For
                    End If
                Else
                    OverQuantity = True
                    'Continue For

                    If LastPeriodeRemain.Length = 0 Then
                        LastPeriodeRemain = "Sisa freedays pada periode " & dPOT.Sequence & " untuk kendaraan " & dPOT.VechileModel.IndDescription & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & " Unit \n"
                    Else
                        LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays pada periode " & dPOT.Sequence & " untuk kendaraan " & dPOT.VechileModel.IndDescription & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & " Unit \n"
                    End If
                    sessHelp.SetSession("Warning", LastPeriodeRemain)
                    dFDaysTarget.Remove(dPOT.FreeDays)
                    VarMaxTOP = -1
                    Return -1
                End If
            End If
        Next

        Return _return
    End Function

    Private Function FreeDaysCreateNew()
        Dim dMod As New Dictionary(Of Integer, Integer) 'Record row / Model ID
        For Each pod As PODraftDetail In objPODraftHeader.PODraftDetail
            Dim MID As Integer = pod.ContractDetail.VechileColor.VechileType.VechileModel.ID
            If Not dMod.ContainsKey(MID) Then
                dMod.Add(MID, 1)
            Else
                dMod(MID) += 1
            End If
        Next
        For Each i As Integer In dMod.Keys
            Dim pd As New ArrayList
            For Each j As PODraftDetail In objPODraftHeader.PODraftDetail
                If j.ContractDetail.VechileColor.VechileType.VechileModel.ID = i Then
                    pd.Add(j)
                End If
            Next
            Dim getFreeDays As Integer = 0
            Dim getMaxTopDays As Integer = 0
            Dim dt As Date = DateSerial(PermintaanKirim.Year, PermintaanKirim.Month, PermintaanKirim.Day)
            sessionHelper.SetSession("Itung", True)
            getFreeDays = SetFreeDaysModeCreate(objDealer, pd, dt, dt, dt, getMaxTopDays, warning)


            If getFreeDays = -1 AndAlso getMaxTopDays = -1 Then
                ViewState("warning") = sessionHelper.GetSession("Warning")
                ViewState("pops") = True
                Exit Function
            End If

            For Each pod As PODraftDetail In pd
                pod.FreeDays = getFreeDays
                pod.MaxTOPDay = getMaxTopDays
            Next
            sessionHelper.RemoveSession("Itung")
        Next
        Dim index As Integer = 0
        Dim newArr As New ArrayList
        If Mode = "CREATE" Then
            For Each cd As ContractDetail In dtgDetail.DataSource
                For Each i As PODraftDetail In objPODraftHeader.PODraftDetail
                    Try
                        Dim lblFreeDays As Label = CType(dtgDetail.Items(index).FindControl("lblFreeDays"), Label)
                        Dim lblMaxTOPDays As Label = CType(dtgDetail.Items(index).FindControl("lblMaxTOPDays"), Label)
                        Dim lblInterest As Label = CType(dtgDetail.Items(index).FindControl("lblInterest"), Label)
                        If cd.ID = i.ContractDetail.ID Then
                            lblFreeDays.Text = i.FreeDays
                            lblMaxTOPDays.Text = i.MaxTOPDay
                            If objPODraftHeader.IsFactoring <> 1 Then
                                lblInterest.Text = FormatNumber(CalculateInterestNonFactoring(i), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                            End If
                            newArr.Add(i)
                        End If
                    Catch ex As Exception
                    End Try
                Next
                index += 1
            Next
        Else
            For Each cd As PODraftDetail In dtgDetail.DataSource
                For Each i As PODraftDetail In objPODraftHeader.PODraftDetail
                    Try
                        Dim lblFreeDays As Label = CType(dtgDetail.Items(index).FindControl("lblFreeDays"), Label)
                        Dim lblMaxTOPDays As Label = CType(dtgDetail.Items(index).FindControl("lblMaxTOPDays"), Label)
                        Dim lblInterest As Label = CType(dtgDetail.Items(index).FindControl("lblInterest"), Label)
                        If cd.ID = i.ID Then
                            lblFreeDays.Text = i.FreeDays
                            lblMaxTOPDays.Text = i.MaxTOPDay
                            If objPODraftHeader.IsFactoring <> 1 Then
                                lblInterest.Text = FormatNumber(CalculateInterestNonFactoring(i), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                            End If
                            newArr.Add(i)
                        End If
                    Catch ex As Exception
                    End Try
                Next
                index += 1
            Next
        End If

        sessionHelper.SetSession("Hitung", newArr)
    End Function

    Private Function CalculateInterestNonFactoring(ByVal objPODraftDetail As PODraftDetail)
        Dim rInterest As Decimal = 0
        Dim ItemDeposit As Double = GetItemDeposit(objPODraftDetail.ContractDetail)
        Dim oPrice As Price = GetPrice(objPODraftDetail.ContractDetail)
        rInterest = objPODraftDetail.ReqQty * objPODraftHeader.ContractHeader.PKHeader.FreeIntIndicator *
                                    Calculation.CountInterest(objPODraftDetail.FreeDays, nTOP, nMonth, oPrice.Interest,
                                    objPODraftDetail.ContractDetail.Amount - ItemDeposit, oPrice.PPh23)

        Return rInterest
    End Function

    Private Function CalCulateInterestFactoring(ByVal objPODraftDetail As PODraftDetail) As Decimal
        Dim rInterest = 0
        Dim _interest As Double = 0
        Dim oPrice As Price = GetPrice(objPODraftDetail.ContractDetail)
        _interest = Calculation.CountRewardsInterest(objContractDetail, oPrice, nTOP, nMonth)
        rInterest = objPODraftDetail.ReqQty * objPODraftHeader.ContractHeader.PKHeader.FreeIntIndicator * _interest
        Return rInterest
    End Function

    Private Function GetPrice(ByVal oContractDetail As ContractDetail) As Price
        Dim oPrice As Price = New Price
        Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(oContractDetail)
        If objPriceArrayList.Count > 0 Then
            Dim objPrice As Price
            For Each item As Price In objPriceArrayList
                If item.ValidFrom <= New DateTime(objContractDetail.ContractHeader.PricePeriodYear, objContractDetail.ContractHeader.PricePeriodMonth, objContractDetail.ContractHeader.PricePeriodDay) Then
                    objPrice = item
                    Exit For
                End If
            Next
            oPrice = objPrice
        End If
        Return oPrice
    End Function

    Private Function CalculateLogisticCost(ByVal ReqQty As Integer, ByVal oContractDetail As ContractDetail) As Decimal
        Dim podes As PODestination = New PODestinationFacade(User).Retrieve(CType(hidPODestinationID.Value, Integer))

        Dim criterialogistic As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "Status", MatchType.Exact, "A"))
        criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "RegionCode", MatchType.Exact, podes.RegionCode))
        criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "SAPModel", MatchType.Exact, oContractDetail.VechileColor.VechileType.SAPModel))
        criterialogistic.opAnd(New Criteria(GetType(LogisticPrice), "EffectiveDate", MatchType.LesserOrEqual, DateTime.Now))

        Dim sortColllog As SortCollection = New SortCollection
        sortColllog.Add(New Sort(GetType(LogisticPrice), "EffectiveDate", Sort.SortDirection.DESC))

        Dim logisticPrices As ArrayList = New LogisticPriceFacade(User).RetrieveByCriteria(criterialogistic, sortColllog)
        If logisticPrices.Count > 0 Then
            Dim logisticPrice As LogisticPrice = logisticPrices(0)
            Return (ReqQty * logisticPrice.TotalLogisticPrice)
        Else
            Return 0
        End If
    End Function

#End Region


End Class