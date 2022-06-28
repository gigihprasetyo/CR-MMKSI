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
#End Region


Public Class FrmCeilingDetail2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents pnlSearch As System.Web.UI.WebControls.Panel
    Protected WithEvents dtlDetails As System.Web.UI.WebControls.Repeater
    Protected WithEvents pnlDetails As System.Web.UI.WebControls.Panel
    Protected WithEvents BtnDownloadDtl As System.Web.UI.WebControls.Button
    Protected WithEvents dtlDetail As System.Web.UI.WebControls.DataList
    Protected WithEvents lblReqDeliveryDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblCeiling As System.Web.UI.WebControls.Label
    Protected WithEvents lblReportDate As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblCreditAccount As System.Web.UI.WebControls.Label
    Protected WithEvents lblOutstandingSAP As System.Web.UI.WebControls.Label
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
    Private sHelper As New SessionHelper
    Private _vstTotalProposed As String = "_vstTotalProposed"
    Private _vstTotalLiquified As String = "_vstTotalLiquified"
    Private _vstTotalOutstanding As String = "_vstTotalOutstanding"
    Private _sessData As String = "FrmCeilingDetail._sessData"
    Private _sessTotalCair As String = "FrmCeilingDetail.TotalCair"
    Private _sessProductCategory As String = "FrmCeilingDetail._sessProductCategory"
#End Region

#Region "Custom Methods"

    Private Sub Initialization()

        If Request.Item("ProductCategoryID") Is Nothing OrElse Request.Item("CreditAccount") Is Nothing OrElse Request.Item("PaymentType") Is Nothing OrElse Request.Item("ReportDate") Is Nothing OrElse Request.Item("ReqDeliveryDate") Is Nothing Then
            Response.Redirect("FrmCeiling.aspx")
            Exit Sub
        End If

        Me.lblCreditAccount.Text = Request.Item("CreditAccount")
        Me.sHelper.SetSession(Me._sessProductCategory, Request.Item("ProductCategoryID"))
        Me.lblProductCategory.Text = Me.GetProductCategory().Code
        viewstate.Add("ProductCategoryID", Request.Item("ProductCategoryID"))
        viewstate.Add("CreditAccount", Request.Item("CreditAccount"))
        viewstate.Add("PaymentType", Request.Item("PaymentType"))
        viewstate.Add("ReportDate", CDate(Request.Item("ReportDate")))
        viewstate.Add("ReqDeliveryDate", CDate(Request.Item("ReqDeliveryDate")))

        lblReqDeliveryDate.Text = Format(viewstate.Item("ReqDeliveryDate"), "dd/MM/yyyy")
        lblReportDate.Text = Format(viewstate.Item("ReportDate"), "dd/MM/yyyy")

    End Sub

    Private Function GetProductCategory() As ProductCategory
        Dim oPC As New ProductCategory

        If Not Me.sHelper.GetSession(Me._sessProductCategory) Is Nothing Then
            Dim oPCFac As New ProductCategoryFacade(User)
            oPC = oPCFac.Retrieve(CType(Me.sHelper.GetSession(Me._sessProductCategory), Short))
        End If
        Return oPC
    End Function

    Private Sub BindDTL()
        Dim aDatas As New ArrayList
        Dim CreditAccount As String = viewstate.Item("CreditAccount")
        Dim PaymentType As Short = CType(viewstate.Item("PaymentType"), Short)
        Dim oSC As sp_Ceiling
        Dim oSCFac As New sp_CeilingFacade(User)
        Dim StartDate As Date = viewstate.Item("ReportDate")
        Dim EndDate As Date = viewstate.Item("ReqDeliveryDate")
        Dim aSCs As ArrayList
        Dim oCM As CreditMaster

        aSCs = oSCFac.RetrieveFromSP(Me.GetProductCategory(), StartDate, EndDate, CreditAccount, PaymentType)
        For Each oSCTemp As sp_Ceiling In aSCs
            If oSCTemp.PaymentType = PaymentType Then
                lblCeiling.Text = FormatNumber(oSCTemp.Ceiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                oCM = New CreditMasterFacade(User).Retrieve(oSCTemp.ID)
                If Not IsNothing(oCM) AndAlso oCM.ID > 0 Then
                    If oCM.PaymentType = CType(enumPaymentType.PaymentType.RTGS, Short) Then
                        Me.lblOutstandingSAP.Text = FormatNumber(0, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Else
                        Me.lblOutstandingSAP.Text = FormatNumber(oCM.OutStanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    End If
                Else
                    Me.lblOutstandingSAP.Text = FormatNumber(0, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                End If
                oSC = oSCTemp
                Exit For
            End If
        Next

        aDatas.Add(oSC)
        aDatas.Add(oSC)
        aDatas.Add(oSC)
        aDatas.Add(oSC)
        Me.sHelper.SetSession(Me._sessData, oSC)
        Me.dtlDetail.DataSource = aDatas
        Me.dtlDetail.DataBind()
        RegisterStartupScript("OpenWindow", "<script>FormatDTGProposed();</script>")

    End Sub

    Private Sub BindDtgOutstanding(ByRef Dtg As DataGrid)
        Dim oSCPOFac As New sp_CeilingPOFacade(User)
        Dim aSCPOs As ArrayList
        Dim CreditAccount As String, PaymentType As Short, StartDate As Date, EndDate As Date, FocusedDate As Date
        Dim i As Integer
        Dim oSCPO As sp_CeilingPO
        Dim aOuts As New ArrayList
        Dim oOut As clsOutstanding


        CreditAccount = Request.Item("CreditAccount")
        PaymentType = Request.Item("PaymentType")
        StartDate = Request.Item("ReportDate")
        EndDate = Request.Item("ReportDate")
        FocusedDate = StartDate
        aSCPOs = oSCPOFac.RetrieveFromSP(Me.GetProductCategory(), CreditAccount, PaymentType, enumCeilingType.CeilingType.Outstanding, StartDate, EndDate, FocusedDate)
        For i = 0 To aSCPOs.Count - 1
            oSCPO = CType(aSCPOs(i), sp_CeilingPO)
            oOut = New clsOutstanding
            With oOut
                If oSCPO.Remark.Trim.ToUpper = "Acceleration".ToUpper Then
                    .PONumber = oSCPO.POHeader.PONumber & " *"
                Else
                    .PONumber = oSCPO.POHeader.PONumber
                End If
                .DealerCode = oSCPO.POHeader.Dealer.DealerCode
                .SONumber = oSCPO.POHeader.SONumber
                .GiroNumber = oSCPO.SlipNumbers
                .GiroStatus = oSCPO.GyroStatuss
                .DeliveryDate = oSCPO.POHeader.ReqAllocationDateTime
                .BaselineDates = oSCPO.BaselineDates
                .EfectiveDate = oSCPO.EffectiveDate
                .Amount = oSCPO.TotalDetail
            End With
            aOuts.Add(oOut)
        Next
        Dtg.DataSource = aOuts
        Dtg.DataBind()
    End Sub

    Private Sub BindDtgLiquified(ByRef Dtg As DataGrid)

        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        Dim aLiqs As New ArrayList
        Dim aTemps As ArrayList
        Dim oSCPOFac As New sp_CeilingPOFacade(User)
        'Dim FocusedDate As Date = EffDateStart
        Dim i As Integer, j As Integer
        Dim oLiq As clsProposed
        Dim TotalCair(5) As Decimal

        For i = 0 To 4
            TotalCair(i) = 0
        Next
        i = 0
        For Each FocusedDate As Date In CType(sHelper.GetSession("arlReportDate"), ArrayList)
            'aTemps = oSCPOFac.RetrieveFromSP(viewstate.Item("CreditAccount"), CType(viewstate.Item("PaymentType"), Short), enumCeilingType.CeilingType.LiquifiedPO, EffDateStart, EffDateEnd, Now)
            aTemps = oSCPOFac.RetrieveFromSP(Me.GetProductCategory(), viewstate.Item("CreditAccount"), CType(viewstate.Item("PaymentType"), Short), enumCeilingType.CeilingType.LiquifiedPO, EffDateStart, FocusedDate, FocusedDate)
            For Each oSCPO As sp_CeilingPO In aTemps
                oLiq = New clsProposed

                If oSCPO.Remark.Trim.ToUpper = "Sliding".ToUpper Then
                    oLiq.PONumber = oSCPO.POHeader.PONumber & " (Geser)"
                ElseIf oSCPO.Remark.Trim.ToUpper = "Acceleration".ToUpper Then
                    oLiq.PONumber = oSCPO.POHeader.PONumber & " *"
                Else
                    oLiq.PONumber = oSCPO.POHeader.PONumber
                End If
                oLiq.SONumber = oSCPO.POHeader.SONumber
                oLiq.POStatus = GetPOStatusValue(CType(oSCPO.POHeader.Status, Integer))
                oLiq.GiroNumber = oSCPO.SlipNumbers
                oLiq.GiroStatus = oSCPO.GyroStatuss
                If i = 0 Then oLiq.AmountDate1 = oSCPO.TotalDetail
                If i = 1 Then oLiq.AmountDate2 = oSCPO.TotalDetail
                If i = 2 Then oLiq.AmountDate3 = oSCPO.TotalDetail
                If i = 3 Then oLiq.AmountDate4 = oSCPO.TotalDetail
                If i = 4 Then oLiq.AmountDate5 = oSCPO.TotalDetail

                aLiqs.Add(oLiq)
                TotalCair(i) += oSCPO.TotalDetail
            Next
            i += 1
        Next
        oLiq = New clsProposed

        oLiq.PONumber = "JUMLAH"
        oLiq.SONumber = ""
        oLiq.POStatus = ""
        oLiq.AmountDate1 = TotalCair(0)
        oLiq.AmountDate2 = TotalCair(1)
        oLiq.AmountDate3 = TotalCair(2)
        oLiq.AmountDate4 = TotalCair(3)
        oLiq.AmountDate5 = TotalCair(4)
        sHelper.SetSession(Me._sessTotalCair, TotalCair)
        aLiqs.Add(oLiq)
        Dtg.DataSource = aLiqs
        Dtg.DataBind()

    End Sub

    Private Sub BindDtgProposed(ByRef Dtg As DataGrid)
        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        Dim aProps As New ArrayList
        Dim aTemps As ArrayList
        Dim oSCPOFac As New sp_CeilingPOFacade(User)
        'Dim FocusedDate As Date = EffDateStart
        Dim i As Integer, j As Integer
        Dim oProp As clsProposed
        Dim TotalProp(5) As Decimal

        For i = 0 To 4
            TotalProp(i) = 0
        Next
        i = 0
        For Each FocusedDate As Date In CType(sHelper.GetSession("arlReportDate"), ArrayList)
            'aTemps = oSCPOFac.RetrieveFromSP(viewstate.Item("CreditAccount"), CType(viewstate.Item("PaymentType"), Short), enumCeilingType.CeilingType.ProposedPO, EffDateStart, EffDateEnd, FocusedDate)
            aTemps = oSCPOFac.RetrieveFromSP(Me.GetProductCategory(), viewstate.Item("CreditAccount"), CType(viewstate.Item("PaymentType"), Short), enumCeilingType.CeilingType.ProposedPO, EffDateStart, FocusedDate, FocusedDate)
            For Each oSCPO As sp_CeilingPO In aTemps
                oProp = New clsProposed

                oProp.PONumber = oSCPO.POHeader.PONumber & IIf(oSCPO.Remark.Trim.ToUpper = "Sliding".ToUpper, " (Geser)", "")
                oProp.SONumber = oSCPO.POHeader.SONumber
                oProp.POStatus = GetPOStatusValue(CType(oSCPO.POHeader.Status, Integer))
                oProp.GiroNumber = oSCPO.SlipNumbers
                oProp.GiroStatus = oSCPO.GyroStatuss
                If i = 0 Then oProp.AmountDate1 = oSCPO.TotalDetail
                If i = 1 Then oProp.AmountDate2 = oSCPO.TotalDetail
                If i = 2 Then oProp.AmountDate3 = oSCPO.TotalDetail
                If i = 3 Then oProp.AmountDate4 = oSCPO.TotalDetail
                If i = 4 Then oProp.AmountDate5 = oSCPO.TotalDetail

                aProps.Add(oProp)

                TotalProp(i) += oSCPO.TotalDetail
            Next
            i += 1
        Next
        oProp = New clsProposed

        oProp.PONumber = "JUMLAH"
        oProp.SONumber = ""
        oProp.POStatus = ""
        oProp.AmountDate1 = TotalProp(0)
        oProp.AmountDate2 = TotalProp(1)
        oProp.AmountDate3 = TotalProp(2)
        oProp.AmountDate4 = TotalProp(3)
        oProp.AmountDate5 = TotalProp(4)

        aProps.Add(oProp)

        oProp = New clsProposed

        oProp.PONumber = "Balance of the Day " & (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType"))
        oProp.SONumber = ""
        oProp.POStatus = ""
        Dim lblJumlah As Label = Me.dtlDetail.Items(0).FindControl("lblJumlah")
        Dim Total As Decimal
        Dim avCeiling As Decimal = 0
        Dim TotalCair(5) As Decimal

        If CType(viewstate.Item("PaymentType"), Short) <> CType(enumPaymentType.PaymentType.RTGS, Short) Then

            TotalCair = Me.sHelper.GetSession(Me._sessTotalCair)
            avCeiling = CDec(lblCeiling.Text) - CDec(lblJumlah.Text)
            Total = CDec(lblCeiling.Text) - CDec(lblJumlah.Text) - TotalProp(0)
            oProp.AmountDate1 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            Total = Total - TotalProp(1) + TotalCair(1) 'o2n
            oProp.AmountDate2 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            Total = Total - TotalProp(2) + TotalCair(2)
            oProp.AmountDate3 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            Total = Total - TotalProp(3) + TotalCair(3)
            oProp.AmountDate4 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            Total = Total - TotalProp(4) + TotalCair(4)
            oProp.AmountDate5 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Else
            oProp.AmountDate1 = FormatNumber(0, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            oProp.AmountDate2 = FormatNumber(0, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            oProp.AmountDate3 = FormatNumber(0, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            oProp.AmountDate4 = FormatNumber(0, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            oProp.AmountDate5 = FormatNumber(0, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
        aProps.Add(oProp)

        Dtg.DataSource = aProps
        Dtg.DataBind()

    End Sub

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
        Private _giroStatus As String
        Private _deliveryDate As Date
        Private _baselineDate As Date
        Private _baselineDates As String
        Private _efectiveDate As Date
        Private _amount As Decimal

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
        Public Property GiroStatus() As String
            Get
                Return _giroStatus
            End Get
            Set(ByVal Value As String)
                _giroStatus = Value
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
        Public Property BaselineDates() As String
            Get
                Return _baselineDates
            End Get
            Set(ByVal Value As String)
                _baselineDates = Value
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
        Private _giroNumber As String
        Private _giroStatus As String
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

        Public Property GiroNumber() As String
            Get
                Return _giroNumber
            End Get
            Set(ByVal Value As String)
                _giroNumber = Value
            End Set
        End Property
        Public Property GiroStatus() As String
            Get
                Return _giroStatus
            End Get
            Set(ByVal Value As String)
                _giroStatus = Value
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

    Private Sub CreateFiveDays()
        Dim ReportDate As Date = CType(viewstate.Item("ReportDate"), Date)
        Dim i As Integer
        Dim arlDate As New ArrayList
        ReportDate = ReportDate.AddDays(-1)
        For i = 0 To 4
            ReportDate = CommonFunction.AddNWorkingDay(ReportDate, 1)
            arlDate.Add(ReportDate)
        Next
        sHelper.SetSession("arlReportDate", arlDate)
    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Initialization()
            BindDTL()
        End If
    End Sub

    Private Sub dtlDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtlDetail.ItemDataBound
        Dim oSC As sp_Ceiling = Me.sHelper.GetSession(Me._sessData)

        If e.Item.ItemIndex = 0 Then ' e.Item.ItemType = ListItemType.Item Then
            Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
            Dim lblTitle As Label = e.Item.FindControl("lblTitle")
            Dim lblJumlah As Label = e.Item.FindControl("lblJumlah")
            Dim dtgDetail As DataGrid = e.Item.FindControl("dtgDetail")

            sHelper.SetSession("dtgDetail", dtgdetail)
            lblTitle.Text = (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType")) & " Outstanding"
            lblJumlah.Text = FormatNumber(oSC.OutStanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            BindDtgOutstanding(dtgDetail)
        ElseIf e.Item.ItemIndex = 1 Then
            Dim dtgDetail2 As DataGrid = e.Item.FindControl("dtgDetail2")
            Dim lblTitle2 As Label = e.Item.FindControl("lblTitle2")
            Dim lblJumlah2 As Label = e.Item.FindControl("lblJumlah2")
            lbljumlah2.Text = sHelper.GetSession("FrmCeilingDetail.LiquefiedPO")
            lblTitle2.Text = (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType")) & " PO Cair"
            lblJumlah2.Text = FormatNumber(oSC.LiquifiedPO, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            sHelper.SetSession("FrmCeilingDetail.dtgDetail2", dtgdetail2)
        ElseIf e.Item.ItemIndex = 2 Then
            Dim lblTitle As Label = e.Item.FindControl("lblTitle")
            Dim lblJumlah As Label = e.Item.FindControl("lblJumlah")
            Dim dtgDetail As DataGrid = e.Item.FindControl("dtgDetail")
            Dim dtgDetail2 As DataGrid = CType(sHelper.GetSession("FrmCeilingDetail.dtgDetail2"), DataGrid)  ' e.Item.FindControl("dtgDetail")

            lblTitle.Text = (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType")) & " PO Cair"
            lblJumlah.Text = FormatNumber(oSC.LiquifiedPO, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CreateFiveDays()
            dtgDetail2.Columns(5).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date), "dd-MMM") ' Format(CType(viewstate.Item("ReportDate"), Date), "dd-MMM")
            dtgDetail2.Columns(6).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(1), "dd-MMM")
            dtgDetail2.Columns(7).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(2), "dd-MMM")
            dtgDetail2.Columns(8).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(3), "dd-MMM")
            dtgDetail2.Columns(9).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(4), "dd-MMM")
            'If viewstate.Item("PaymentType") = enumPaymentType.PaymentType.TOP Then
            '    BindDtgProposedNewCairTOP(dtgDetail2)
            'Else
            '    BindDtgProposedNewCair(dtgDetail2)
            'End If
            BindDtgLiquified(dtgDetail2)

            dtgdetail = dtgdetail2
            dtgdetail.DataBind()
            e.Item.Visible = False
            e.Item.Style.Add("visibility", "hidden")
        ElseIf e.Item.ItemIndex = 3 Then 'e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblTitle2 As Label = e.Item.FindControl("lblTitle2")
            Dim lblJumlah2 As Label = e.Item.FindControl("lblJumlah2")
            Dim dtgDetail2 As DataGrid = e.Item.FindControl("dtgDetail2")

            lblTitle2.Text = (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType")) & " Telah Diajukan"
            lblJumlah2.Text = FormatNumber(oSC.ProposedPO, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CreateFiveDays()
            dtgDetail2.Columns(5).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date), "dd-MMM") ' Format(CType(viewstate.Item("ReportDate"), Date), "dd-MMM")
            dtgDetail2.Columns(6).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(1), "dd-MMM")
            dtgDetail2.Columns(7).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(2), "dd-MMM")
            dtgDetail2.Columns(8).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(3), "dd-MMM")
            dtgDetail2.Columns(9).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(4), "dd-MMM")

            BindDtgProposed(dtgdetail2)
            'BindDtgProposedNew(dtgDetail2)
        End If

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmCeiling2.aspx?IsBack=1")
    End Sub

#End Region
End Class
