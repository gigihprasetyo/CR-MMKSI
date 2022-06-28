#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System
Imports System.Collections
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
Imports KTB.DNet.BusinessFacade.MDP
#End Region

Public Class FrmAdequacyReportDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCreditAccount As System.Web.UI.WebControls.Label
    Protected WithEvents lblCeiling As System.Web.UI.WebControls.Label
    Protected WithEvents lblReqDeliveryDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblReportDate As System.Web.UI.WebControls.Label
    Protected WithEvents dtlDetail As System.Web.UI.WebControls.DataList
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents pnlSearch As System.Web.UI.WebControls.Panel
    Protected WithEvents BtnDownloadDtl As System.Web.UI.WebControls.Button
    Protected WithEvents dtlDetails As System.Web.UI.WebControls.Repeater
    Protected WithEvents pnlDetails As System.Web.UI.WebControls.Panel
    Protected WithEvents lblProductCategory As System.Web.UI.WebControls.Label

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
    Private _sessHelper As New SessionHelper
    Private _sessData As String = "DataToDisplay"

    Private _sessProductCategory As String = "FrmAdequacyReportDetail._sessProductCategory"
#End Region

#Region "Event handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Initialization()
            BindDTL()
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmAdequacyReport.aspx")
    End Sub

    Private Sub dtlDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtlDetail.ItemDataBound
        If e.Item.ItemIndex = 0 Then ' e.Item.ItemType = ListItemType.Item Then
            Dim lblTitle As Label = e.Item.FindControl("lblTitle")
            Dim lblJumlah As Label = e.Item.FindControl("lblJumlah")
            Dim dtgDetail As DataGrid = e.Item.FindControl("dtgDetail")

            _sessHelper.SetSession("dtgDetail", dtgDetail)
            lblTitle.Text = (New enumPaymentType).PaymentType.TOP.ToString & " Outstanding"
            lblJumlah.Text = FormatNumber(lblJumlah.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            BindDtgOutstanding(dtgDetail)
        ElseIf e.Item.ItemIndex = 1 Then
            Dim oFM As FactoringMaster = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(0), FactoringMaster)
            Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
            Dim lblTitle2 As Label = e.Item.FindControl("lblTitle2")
            Dim lblJumlah2 As Label = e.Item.FindControl("lblJumlah2")
            Dim dtgDetail2 As DataGrid = e.Item.FindControl("dtgDetail2")
            Dim arlFactComponent As ArrayList = oFMFac.GetCeilingComponent(oFM.ProductCategory, oFM.CreditAccount)
            _sessHelper.SetSession("arlPOdiajukan", arlFactComponent)


            lblTitle2.Text = (New enumPaymentType).PaymentType.TOP.ToString & " Telah Diajukan"
            lblJumlah2.Text = FormatNumber(CType(arlFactComponent(3), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            _sessHelper.SetSession("TotalPOProposed", CType(arlFactComponent(3), Decimal))
            CreateFiveDays()

            dtgDetail2.Columns(3).HeaderText = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(0), Date), "dd-MMM") ' Format(CType(viewstate.Item("ReportDate"), Date), "dd-MMM")
            dtgDetail2.Columns(4).HeaderText = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(1), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(1), "dd-MMM")
            dtgDetail2.Columns(5).HeaderText = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(2), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(2), "dd-MMM")
            dtgDetail2.Columns(6).HeaderText = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(3), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(3), "dd-MMM")
            dtgDetail2.Columns(7).HeaderText = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(4), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(4), "dd-MMM")

            BindDtgProposed(dtgDetail2)


        ElseIf e.Item.ItemIndex = 3 Then

            Dim oFM As FactoringMaster = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(0), FactoringMaster)
            Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
            Dim lblTitle2 As Label = e.Item.FindControl("lblTitle2")
            Dim lblJumlah2 As Label = e.Item.FindControl("lblJumlah2")
            Dim dtgDetail2 As DataGrid = e.Item.FindControl("dtgDetail2")
            Dim arlFactComponent As ArrayList = oFMFac.GetCeilingComponent(oFM.ProductCategory, oFM.CreditAccount)
            _sessHelper.SetSession("arlPOdiajukan", arlFactComponent)


            lblTitle2.Text = (New enumPaymentType).PaymentType.TOP.ToString & " yang Akan Diajukan"
            'lblJumlah2.Text = FormatNumber(CType(arlFactComponent(3), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            _sessHelper.SetSession("TotalPOProposed", CType(arlFactComponent(3), Decimal))
            CreateFiveDays()

            dtgDetail2.Columns(3).HeaderText = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(0), Date), "dd-MMM") ' Format(CType(viewstate.Item("ReportDate"), Date), "dd-MMM")
            dtgDetail2.Columns(4).HeaderText = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(1), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(1), "dd-MMM")
            dtgDetail2.Columns(5).HeaderText = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(2), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(2), "dd-MMM")
            dtgDetail2.Columns(6).HeaderText = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(3), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(3), "dd-MMM")
            dtgDetail2.Columns(7).HeaderText = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(4), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(4), "dd-MMM")

            BindDtgDraft(dtgDetail2, lblJumlah2)
            

        End If
    End Sub

#End Region

#Region "custom method"
    Private Sub Initialization()
        If Request.Item("CreditAccount") Is Nothing Then
            Response.Redirect("FrmAdequacyReport.aspx")
            Exit Sub
        End If
        viewstate.Add("CreditAccount", Request.Item("CreditAccount"))
        viewstate.Add("ProductCategoryID", Request.Item("ProductCategoryID"))
        viewstate.Add("Ceiling", Request.Item("Ceiling"))
        viewstate.Add("ReportDate", CDate(Request.Item("ReportDate")))
        viewstate.Add("ReqDeliveryDate", CDate(Request.Item("ReqDeliveryDate")))

        lblCreditAccount.Text = viewstate.Item("CreditAccount")
        lblProductCategory.Text = Me.GetProductCategory.Code
        lblCeiling.Text = viewstate.Item("Ceiling")
        lblReqDeliveryDate.Text = Format(viewstate.Item("ReqDeliveryDate"), "dd/MM/yyyy")
        lblReportDate.Text = Format(viewstate.Item("ReportDate"), "dd/MM/yyyy")

    End Sub

    Private Function GetProductCategory() As ProductCategory
        Dim oPC As ProductCategory

        If 1 = 1 Then ' Me._sessHelper.GetSession(Me._sessProductCategory) Is Nothing Then
            Dim oPCFac As New ProductCategoryFacade(User)
            Dim PCID As Short = CType(viewstate.Item("ProductCategoryID"), Short)

            oPC = oPCFac.Retrieve(PCID)
            If IsNothing(oPC) Then oPC = New ProductCategory
            Me._sessHelper.SetSession(Me._sessProductCategory, oPC)
        Else
            oPC = CType(Me._sessHelper.GetSession(Me._sessProductCategory), ProductCategory)
        End If

        Return oPC
    End Function

    Private Sub BindDTL()

        Dim objFactoringMasterFacade As FactoringMasterFacade = New FactoringMasterFacade(User)
        Dim objFactoringMaster As New FactoringMaster
        Dim arlFactoringMaster As ArrayList = New ArrayList
        Dim arlToBind As ArrayList = New ArrayList

        Dim crtFactoringMaster As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), _
            "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtFactoringMaster.opAnd(New Criteria(GetType(FactoringMaster), "CreditAccount", viewstate.Item("CreditAccount")))
        crtFactoringMaster.opAnd(New Criteria(GetType(FactoringMaster), "ProductCategory.ID", MatchType.Exact, Me.GetProductCategory().ID))

        'arlFactoringMaster = objFactoringMasterFacade.Retrieve(crtFactoringMaster)
        'If arlFactoringMaster.Count > 0 Then
        '    objFactoringMaster = CType(arlFactoringMaster(0), FactoringMaster)
        '    lblCeiling.Text = FormatNumber(objFactoringMaster.FactoringCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        '    Me._sessHelper.SetSession(Me._sessData, arlFactoringMaster)
        'End If

        objFactoringMaster = objFactoringMasterFacade.Retrieve(Me.GetProductCategory(), viewstate.Item("CreditAccount"))
        arlToBind.Add(objFactoringMaster)
        arlToBind.Add(objFactoringMaster)

Dim oD As Dealer = CType(Me._sessHelper.GetSession("DEALER"), Dealer)
        If oD.Title = EnumDealerTittle.DealerTittle.KTB Then 'untuk memunculkan po draft
            arlToBind.Add(objFactoringMaster)
            arlToBind.Add(objFactoringMaster)
        End If

        Me._sessHelper.SetSession(Me._sessData, arlToBind)

        dtlDetail.DataSource = arlToBind
        dtlDetail.DataBind()

        RegisterStartupScript("OpenWindow", "<script>FormatDTGProposed();</script>")

    End Sub

    Private Sub BindDtgOutstanding(ByRef Dtg As DataGrid)
        Dim objDealer As New Dealer
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList = New ArrayList
        Dim crtPOH As CriteriaComposite
        Dim arlOut As ArrayList = New ArrayList
        Dim objOut As clsOutstanding
        Dim tmpDate As Date
        Dim nTOPDays As Integer
        Dim TotalPOD As Decimal
        Dim sql As String = ""

        objDealer = GetDealer(viewstate.Item("CreditAccount"))
        Dim oFM As FactoringMaster = New FactoringMasterFacade(User).Retrieve(Me.GetProductCategory(), objDealer.CreditAccount)

        If IsNothing(oFM) OrElse oFM.ID < 1 Then
            oFM = New FactoringMaster
            oFM.LastUploadedTime = DateSerial(1900, 1, 1)
        End If
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, objDealer.CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.Exact, 1))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.Exact, CType(enumStatusPO.Status.Selesai, Integer)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.PKHeader.Category.ProductCategory.ID", MatchType.Exact, Me.GetProductCategory.ID))

        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            For Each objDP As DailyPayment In objPOH.DailyPayments
                'If (objDP.PaymentPurpose.ID = 7 And objDP.IsReversed = 0 And objDP.IsCleared = 0 AndAlso objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)) AndAlso objDP.CessieTime <= oFM.LastUploadedTime Then
                If (objDP.PaymentPurpose.ID = 7 And objDP.IsReversed = 0 _
                    And (objDP.IsCleared = 0 AndAlso Not (objDP.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared)) _
                    AndAlso objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)) _
                    AndAlso objDP.CessieTime <= oFM.LastUploadedTime Then
                    objOut = New clsOutstanding

                    objOut.PONumber = objPOH.PONumber
                    objOut.DealerCode = objPOH.Dealer.DealerCode ' objDealer.DealerCode
                    objOut.SONumber = objPOH.SONumber
                    objOut.GiroNumber = objDP.SlipNumber
                    objOut.DeliveryDate = objPOH.ReqAllocationDateTime
                    objOut.BaselineDate = objDP.BaselineDate
                    objOut.EfectiveDate = objDP.EffectiveDate
                    objOut.StatusPembayaran = EnumPaymentRemarkStatus.GetStringValue(objDP.RemarkStatus)

                    TotalPOD = 0
                    TotalPOD += objPOH.TotalPODetail()
                    objOut.Amount = FormatNumber(objDP.Amount, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)  'TotalPOD
                    arlOut.Add(objOut)
                End If
            Next
        Next

        arlOut.Sort()
        Dtg.DataSource = arlOut
        Dtg.DataBind()
    End Sub

    Private Sub BindDtgProposed(ByRef Dtg As DataGrid)
        Dim arlProposed As New ArrayList
        Dim objProposed As clsProposed
        Dim EffDateStart As Date = CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        Dim i As Integer
        Dim TotalProposed(5) As Decimal
        Dim Total As Decimal
        Dim avCeiling As Decimal = 0

        TotalProposed(0) = 0
        TotalProposed(1) = 0
        TotalProposed(2) = 0
        TotalProposed(3) = 0
        TotalProposed(4) = 0

        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As New ArrayList

        Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, viewstate.Item("CreditAccount")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.Exact, 1))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.PKHeader.Category.ProductCategory.ID", MatchType.Exact, Me.GetProductCategory.ID))

        Dim sqlHavingGyro As String
        sqlHavingGyro = "select count(dp.ID) from DailyPayment dp with (nolock) inner join FactoringMaster fm on fm.CreditAccount='" & ViewState.Item("CreditAccount") & "' "
        sqlHavingGyro &= " where dp.POID = POHeader.ID And dp.Status = " & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short).ToString
        sqlHavingGyro &= " and dp.CessieID>0 and dp.CessieTime<=fm.LastUploadedTime "
        sqlHavingGyro &= " and fm.ProductCategoryID=" & Me.GetProductCategory.ID.ToString() & " "

        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, "(" & sqlHavingGyro & ")"))

        arlPOH = objPOHFac.Retrieve(crtPOH)


        For Each objPOH As POHeader In arlPOH
            'If objPOH.DailyPayments.Count < 1 Then

            objProposed = New clsProposed
            objProposed.PONumber = objPOH.PONumber '& "ProposedPO"
            objProposed.SONumber = objPOH.SONumber
            objProposed.POStatus = GetPOStatusValue(CType(objPOH.Status, Integer))
            objProposed.AmountDate1 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(0), Date), "yyyy.MM.dd"), GetTotalPODetail(objPOH), 0)
            objProposed.AmountDate2 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(1), Date), "yyyy.MM.dd"), GetTotalPODetail(objPOH), 0)
            objProposed.AmountDate3 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(2), Date), "yyyy.MM.dd"), GetTotalPODetail(objPOH), 0)
            objProposed.AmountDate4 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(3), Date), "yyyy.MM.dd"), GetTotalPODetail(objPOH), 0)
            objProposed.AmountDate5 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(4), Date), "yyyy.MM.dd"), GetTotalPODetail(objPOH), 0)
            objProposed.IsRequestedPO = True
            arlProposed.Add(objProposed)

            TotalProposed(0) = TotalProposed(0) + objProposed.AmountDate1
            TotalProposed(1) = TotalProposed(1) + objProposed.AmountDate2
            TotalProposed(2) = TotalProposed(2) + objProposed.AmountDate3
            TotalProposed(3) = TotalProposed(3) + objProposed.AmountDate4
            TotalProposed(4) = TotalProposed(4) + objProposed.AmountDate5
            'End If
        Next

        objProposed = New clsProposed

        objProposed.PONumber = "JUMLAH"
        objProposed.SONumber = ""
        objProposed.POStatus = ""
        objProposed.AmountDate1 = FormatNumber(TotalProposed(0), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate2 = FormatNumber(TotalProposed(1), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate3 = FormatNumber(TotalProposed(2), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate4 = FormatNumber(TotalProposed(3), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate5 = FormatNumber(TotalProposed(4), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        arlProposed.Add(objProposed)

        Dtg.DataSource = arlProposed
        Dtg.DataBind()

    End Sub

    Private Sub BindDtgDraft(ByRef Dtg As DataGrid, ByRef lblJumlah As Label)
        Dim arlProposed As New ArrayList
        Dim objProposed As clsProposed
        Dim EffDateStart As Date = CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        Dim i As Integer
        Dim TotalProposed(5) As Decimal
        Dim Total As Decimal
        Dim avCeiling As Decimal = 0

     

        TotalProposed(0) = 0
        TotalProposed(1) = 0
        TotalProposed(2) = 0
        TotalProposed(3) = 0
        TotalProposed(4) = 0

        Dim objPOHFac As PODraftHeaderFacade = New PODraftHeaderFacade(User)
        Dim arlPOH As New ArrayList

        Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(PODraftHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, ViewState.Item("CreditAccount")))
        crtPOH.opAnd(New Criteria(GetType(PODraftHeader), "Status", MatchType.InSet, "(0)"))
        crtPOH.opAnd(New Criteria(GetType(PODraftHeader), "IsFactoring", MatchType.Exact, 1))
        crtPOH.opAnd(New Criteria(GetType(PODraftHeader), "ContractHeader.PKHeader.Category.ProductCategory.ID", MatchType.Exact, Me.GetProductCategory.ID))

        'Dim sqlHavingGyro As String
        'sqlHavingGyro = "select count(dp.ID) from DailyPayment dp with (nolock) inner join FactoringMaster fm on fm.CreditAccount='" & ViewState.Item("CreditAccount") & "' "
        'sqlHavingGyro &= " where dp.POID = POHeader.ID And dp.Status = " & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short).ToString
        'sqlHavingGyro &= " and dp.CessieID>0 and dp.CessieTime<=fm.LastUploadedTime "
        'sqlHavingGyro &= " and fm.ProductCategoryID=" & Me.GetProductCategory.ID.ToString() & " "

        'crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, "(" & sqlHavingGyro & ")"))

        arlPOH = objPOHFac.Retrieve(crtPOH)


        For Each objPOH As PODraftHeader In arlPOH
            'If objPOH.DailyPayments.Count < 1 Then

            objProposed = New clsProposed
            objProposed.PONumber = objPOH.DraftPONumber '& "ProposedPO"
            'objProposed.SONumber = objPOH.SONumber
            objProposed.SONumber = ""
            objProposed.POStatus = ""
            objProposed.POStatus = GetPOStatusValue(CType(objPOH.Status, Integer))
            objProposed.AmountDate1 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(0), Date), "yyyy.MM.dd"), GetTotalPODraftDetail(objPOH), 0)
            objProposed.AmountDate2 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(1), Date), "yyyy.MM.dd"), GetTotalPODraftDetail(objPOH), 0)
            objProposed.AmountDate3 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(2), Date), "yyyy.MM.dd"), GetTotalPODraftDetail(objPOH), 0)
            objProposed.AmountDate4 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(3), Date), "yyyy.MM.dd"), GetTotalPODraftDetail(objPOH), 0)
            objProposed.AmountDate5 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(4), Date), "yyyy.MM.dd"), GetTotalPODraftDetail(objPOH), 0)
            objProposed.IsRequestedPO = True
            arlProposed.Add(objProposed)

            TotalProposed(0) = TotalProposed(0) + objProposed.AmountDate1
            TotalProposed(1) = TotalProposed(1) + objProposed.AmountDate2
            TotalProposed(2) = TotalProposed(2) + objProposed.AmountDate3
            TotalProposed(3) = TotalProposed(3) + objProposed.AmountDate4
            TotalProposed(4) = TotalProposed(4) + objProposed.AmountDate5
            'End If
        Next

        objProposed = New clsProposed

        objProposed.PONumber = "JUMLAH"
        objProposed.SONumber = ""
        objProposed.POStatus = ""
        objProposed.AmountDate1 = FormatNumber(TotalProposed(0), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate2 = FormatNumber(TotalProposed(1), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate3 = FormatNumber(TotalProposed(2), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate4 = FormatNumber(TotalProposed(3), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate5 = FormatNumber(TotalProposed(4), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        arlProposed.Add(objProposed)


        Dim jumlah As Decimal = TotalProposed(0) + TotalProposed(1) + TotalProposed(2) + TotalProposed(3) + TotalProposed(4)

        lblJumlah.Text = FormatNumber(jumlah, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        Dtg.Columns(0).HeaderText = "NO Draft PO"
        Dtg.Columns(1).HeaderText = ""
        Dtg.Columns(2).HeaderText = ""

        Dtg.DataSource = arlProposed
        Dtg.DataBind()

    End Sub

    Private Function GetDealer(ByVal CreditAccount As String) As Dealer
        Dim objDFac As DealerFacade = New DealerFacade(User)
        Dim objDealer As Dealer = New Dealer
        objDealer = objDFac.Retrieve(CreditAccount)
        Return objDealer
    End Function

    Private Sub CreateFiveDays()
        Dim ReportDate As Date = CType(viewstate.Item("ReportDate"), Date)
        Dim i As Integer
        Dim arlDate As New ArrayList
        ReportDate = ReportDate.AddDays(-1)
        For i = 0 To 4
            ReportDate = CommonFunction.AddNWorkingDay(ReportDate, 1)
            arlDate.Add(ReportDate)
        Next
        _sessHelper.SetSession("arlReportDate", arlDate)
    End Sub

    Private Function GetTotalPODetail(ByRef objPOH As POHeader) As Decimal
        Dim Total As Decimal = 0
        Total = objPOH.TotalPODetail()
        Return Total
    End Function

    Private Function GetTotalPODraftDetail(ByRef objPOH As PODraftHeader) As Decimal
        Dim Total As Decimal = 0
        Total = objPOH.TotalPODetail()
        Return Total
    End Function

    Private Function GetPOStatusValue(ByVal POStatus As Integer) As String
        Dim arlPOStatus As ArrayList = CType(LookUp.ArrayStatusPO, ArrayList)
        For Each li As ListItem In arlPOStatus
            If li.Value = POStatus Then Return li.Text
        Next

    End Function

    Private Class clsOutstanding
        Implements IComparable

        Private _pONumber As String
        Private _dealerCode As String
        Private _sONumber As String
        Private _giroNumber As String
        Private _deliveryDate As Date
        Private _baselineDate As Date
        Private _efectiveDate As Date
        Private _amount As Decimal
        Private _statusPembayaran As String

        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal Value As String)
                _pONumber = Value
            End Set
        End Property
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal Value As String)
                _dealerCode = Value
            End Set
        End Property
        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal Value As String)
                _sONumber = Value
            End Set
        End Property
        Public Property GiroNumber() As String
            Get
                Return _giroNumber
            End Get
            Set(ByVal Value As String)
                _giroNumber = Value
            End Set
        End Property
        Public Property DeliveryDate() As Date
            Get
                Return _deliveryDate
            End Get
            Set(ByVal Value As Date)
                _deliveryDate = Value
            End Set
        End Property
        Public Property BaselineDate() As Date
            Get
                Return _baselineDate
            End Get
            Set(ByVal Value As Date)
                _baselineDate = Value
            End Set
        End Property
        Public Property EfectiveDate() As Date
            Get
                Return _efectiveDate
            End Get
            Set(ByVal Value As Date)
                _efectiveDate = Value
            End Set
        End Property
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal Value As Decimal)
                _amount = Value
            End Set
        End Property

        Public Property StatusPembayaran() As String
            Get
                Return _statusPembayaran
            End Get
            Set(ByVal Value As String)
                _statusPembayaran = Value
            End Set
        End Property

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            If Not TypeOf obj Is clsOutstanding Then
                Throw New Exception("Object is not clsOutstanding")
            End If
            Dim Compare As clsOutstanding = CType(obj, clsOutstanding)
            Dim result As Integer = Me.EfectiveDate.CompareTo(Compare.EfectiveDate)

            If result = 0 Then
                result = Me.EfectiveDate.CompareTo(Compare.EfectiveDate)
            End If
            Return result
        End Function

    End Class

    Private Class clsProposed
        Private _pONumber As String
        Private _sONumber As String
        Private _pOStatus As String
        Private _amountDate1 As Decimal
        Private _amountDate2 As Decimal
        Private _amountDate3 As Decimal
        Private _amountDate4 As Decimal
        Private _amountDate5 As Decimal
        Private _isRequestedPO As Boolean

        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal Value As String)
                _pONumber = Value
            End Set
        End Property

        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal Value As String)
                _sONumber = Value
            End Set
        End Property

        Public Property POStatus() As String
            Get
                Return _pOStatus
            End Get
            Set(ByVal Value As String)
                _pOStatus = Value
            End Set
        End Property

        Public Property AmountDate1() As Decimal
            Get
                Return _amountDate1
            End Get
            Set(ByVal Value As Decimal)
                _amountDate1 = Value
            End Set
        End Property

        Public Property AmountDate2() As Decimal
            Get
                Return _amountDate2
            End Get
            Set(ByVal Value As Decimal)
                _amountDate2 = Value
            End Set
        End Property
        Public Property AmountDate3() As Decimal
            Get
                Return _amountDate3
            End Get
            Set(ByVal Value As Decimal)
                _amountDate3 = Value
            End Set
        End Property
        Public Property AmountDate4() As Decimal
            Get
                Return _amountDate4
            End Get
            Set(ByVal Value As Decimal)
                _amountDate4 = Value
            End Set
        End Property
        Public Property AmountDate5() As Decimal
            Get
                Return _amountDate5
            End Get
            Set(ByVal Value As Decimal)
                _amountDate5 = Value
            End Set
        End Property

        Public Property IsRequestedPO() As Boolean
            Get
                Return _isRequestedPO
            End Get
            Set(ByVal Value As Boolean)
                _isRequestedPO = Value
            End Set
        End Property
    End Class

#End Region



End Class
