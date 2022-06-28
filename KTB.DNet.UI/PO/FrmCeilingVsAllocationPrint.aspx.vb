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

Public Class FrmCeilingVsAllocationPrint
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPaymentTypeTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblPaymentType As System.Web.UI.WebControls.Label
    Protected WithEvents lblRemainDay As System.Web.UI.WebControls.Label
    Protected WithEvents lblReportDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblReqDeliveryDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblPaymetTypeColon As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblCreditAccount As System.Web.UI.WebControls.Label

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

#End Region
    
#Region "Custom Methods"

    Private Sub Initialization()
        If Request.Item("DeliveryDate") Is Nothing Or Request.Item("PaymentType") Is Nothing Or Request.Item("Remain") Is Nothing Then
            Response.Redirect("FrmCeilingVsAllocation.aspx")
            Exit Sub
        End If
        If Request.Item("PaymentType") = "-1" Then
            lblPaymentTypeTitle.Visible = False
            lblPaymetTypeColon.Visible = False
            lblPaymentType.Visible = False
        Else
            lblPaymentType.Text = enumPaymentType.GetStringValue(Request.Item("PaymentType"))
        End If
        lblRemainDay.Text = Request.Item("Remain")
        lblReportDate.Text = Format(Now, "dd/MMM/yyyy")
        lblReqDeliveryDate.Text = Format(CDate(Request.Item("DeliveryDate")), "dd/MMM/yyyy")

        lblPeriode.Text = "Periode " & Format(CType(sHelper.GetSession("FrmCeilingVsAllocation.ReportDate"), Date), "MMMM")
        If CType(sHelper.GetSession("FrmCeilingVsAllocation.CreditAccount"), String).Trim <> "" Then
            Dim CreditAccount As String = CType(sHelper.GetSession("FrmCeilingVsAllocation.CreditAccount"), String)
            Dim objVCAFac As v_CreditAccountFacade = New v_CreditAccountFacade(User)
            Dim arlVCA As ArrayList
            Dim crtVCA As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_CreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            crtVCA.opAnd(New Criteria(GetType(v_CreditAccount), "CreditAccount", MatchType.Exact, CreditAccount))
            arlVCA = objVCAFac.Retrieve(crtVCA)
            If arlVCA.Count > 0 Then
                lblCreditAccount.Text = CreditAccount & " " & CType(arlVCA(0), v_CreditAccount).DealerName
                lblCreditAccount.Visible = True
            Else
                lblCreditAccount.Visible = False
            End If
        Else
            lblCreditAccount.Visible = False
        End If

        BindData()
    End Sub

    Private Sub BindData()
        Dim dtg As DataGrid
        Dim i As Integer, j As Integer
        Dim arlTemp As New ArrayList
        Dim Total(10) As Decimal
        Dim objData As clsData

        For i = 0 To 10
            Total(i) = 0
        Next
        dtg = CType(sHelper.GetSession("dtgCeilingVsAllocation"), DataGrid)
        For i = 0 To dtg.Items.Count - 1
            'arlTemp.Add(dtg.Items(i).Cells(0).Text)
            objData = New clsData

            objData.No = CType(dtg.Items(i).FindControl("lblNo"), Label).Text
            objData.CreditAccount = CType(dtg.Items(i).FindControl("lblCreditAccount"), Label).Text
            objData.ProductCategoryCode = CType(dtg.Items(i).FindControl("lblProduk"), Label).Text
            objData.PaymentType = CType(dtg.Items(i).FindControl("lblPaymentType"), Label).Text
            objData.Ceiling = CType(dtg.Items(i).FindControl("lblCeiling"), Label).Text
            objData.Outstanding = CType(dtg.Items(i).FindControl("lblOutstanding"), Label).Text
            objData.Proposed = CType(dtg.Items(i).FindControl("lblProposed"), Label).Text
            objData.RemainCeiling = CType(dtg.Items(i).FindControl("lblRemainCeiling"), Label).Text
            objData.OCQty = CType(dtg.Items(i).FindControl("lblOCQty"), Label).Text
            objData.OCAmount = CType(dtg.Items(i).FindControl("lblOCAmount"), Label).Text
            objData.RemainOCQty = CType(dtg.Items(i).FindControl("lblRemainOCQty"), Label).Text
            objData.RemainOCAmount = CType(dtg.Items(i).FindControl("lbtnRemainOCAmount"), LinkButton).Text
            objData.Gyro = CType(dtg.Items(i).FindControl("lblGyro"), Label).Text
            objData.MaxPO = CType(dtg.Items(i).FindControl("lblMaxPO"), Label).Text
            objData.Estimation = CType(dtg.Items(i).FindControl("lblEstimation"), Label).Text

            arlTemp.Add(objData)

            Total(0) += objData.Ceiling
            Total(1) += objData.Outstanding
            Total(2) += objData.Proposed
            Total(3) += objData.RemainCeiling
            Total(4) += objData.OCQty
            Total(5) += objData.OCAmount
            Total(6) += objData.RemainOCQty
            Total(7) += objData.RemainOCAmount
            Total(8) += objData.Gyro
            Total(9) += objData.MaxPO
            Total(10) += objData.Estimation
        Next
        If Not viewstate.Item("FooterTotal") Is Nothing Then viewstate.Remove("FooterTotal")
        viewstate.Add("FooterTotal", Total)
        dtgMain.DataSource = arlTemp
        dtgMain.DataBind()
        'For i = 0 To dtg.Items.Count - 1
        '    CType(dtgMain.Items(i).FindControl("lblNo"), Label).Text = CType(dtg.Items(i).FindControl("lblNo"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblCreditAccount"), Label).Text = CType(dtg.Items(i).FindControl("lblCreditAccount"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblPaymentType"), Label).Text = CType(dtg.Items(i).FindControl("lblPaymentType"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblCeiling"), Label).Text = CType(dtg.Items(i).FindControl("lblCeiling"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblOutstanding"), Label).Text = CType(dtg.Items(i).FindControl("lblOutstanding"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblProposed"), Label).Text = CType(dtg.Items(i).FindControl("lblProposed"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblRemainCeiling"), Label).Text = CType(dtg.Items(i).FindControl("lblRemainCeiling"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblOCQty"), Label).Text = CType(dtg.Items(i).FindControl("lblOCQty"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblOCAmount"), Label).Text = CType(dtg.Items(i).FindControl("lblOCAmount"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblRemainOCQty"), Label).Text = CType(dtg.Items(i).FindControl("lblRemainOCQty"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lbtnRemainOCAmount"), LinkButton).Text = CType(dtg.Items(i).FindControl("lbtnRemainOCAmount"), LinkButton).Text
        '    CType(dtgMain.Items(i).FindControl("lblGyro"), Label).Text = CType(dtg.Items(i).FindControl("lblGyro"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblMaxPO"), Label).Text = CType(dtg.Items(i).FindControl("lblMaxPO"), Label).Text
        '    CType(dtgMain.Items(i).FindControl("lblEstimation"), Label).Text = CType(dtg.Items(i).FindControl("lblEstimation"), Label).Text
        '    Total(0) += CType(dtgMain.Items(i).FindControl("lblCeiling"), Label).Text
        '    Total(1) += CType(dtgMain.Items(i).FindControl("lblOutstanding"), Label).Text
        '    Total(2) += CType(dtgMain.Items(i).FindControl("lblProposed"), Label).Text
        '    Total(3) += CType(dtgMain.Items(i).FindControl("lblRemainCeiling"), Label).Text
        '    Total(4) += CType(dtgMain.Items(i).FindControl("lblOCQty"), Label).Text
        '    Total(5) += CType(dtgMain.Items(i).FindControl("lblOCAmount"), Label).Text
        '    Total(6) += CType(dtgMain.Items(i).FindControl("lblRemainOCQty"), Label).Text
        '    Total(10) += CType(dtgMain.Items(i).FindControl("lbtnRemainOCAmount"), Label).Text
        '    Total(7) += CType(dtgMain.Items(i).FindControl("lblGyro"), Label).Text
        '    Total(8) += CType(dtgMain.Items(i).FindControl("lblMaxPO"), Label).Text
        '    Total(9) += CType(dtgMain.Items(i).FindControl("lblEstimation"), Label).Text
        'Next
    End Sub

    Private Class clsData
        Private _no As String
        Private _creditAccount As String
        Private _ProductCategoryCode As String
        Private _paymentType As String
        Private _Ceiling As String
        Private _Outstanding As String
        Private _Proposed As String
        Private _RemainCeiling As String
        Private _OCQty As String
        Private _OCAmount As String
        Private _OCRemainOCQty As String
        Private _RemainOCAmount As String
        Private _Gyro As String
        Private _MaxPO As String
        Private _Estimation As String

        Public Property No() As String
            Get
                Return _no
            End Get
            Set(ByVal Value As String)
                _no = Value
            End Set
        End Property
        Public Property CreditAccount() As String
            Get
                Return _creditAccount
            End Get
            Set(ByVal Value As String)
                _creditAccount = Value
            End Set
        End Property
        Public Property ProductCategoryCode() As String
            Get
                Return _ProductCategoryCode
            End Get
            Set(ByVal Value As String)
                _ProductCategoryCode = Value
            End Set
        End Property
        Public Property PaymentType() As String
            Get
                Return _paymentType
            End Get
            Set(ByVal Value As String)
                _paymentType = Value
            End Set
        End Property
        Public Property Ceiling() As String
            Get
                Return _Ceiling
            End Get
            Set(ByVal Value As String)
                _Ceiling = Value
            End Set
        End Property

        Public Property Outstanding() As String
            Get
                Return _Outstanding
            End Get
            Set(ByVal Value As String)
                _Outstanding = Value
            End Set
        End Property

        Public Property Proposed() As String
            Get
                Return _Proposed
            End Get
            Set(ByVal Value As String)
                _Proposed = Value
            End Set
        End Property
        Public Property RemainCeiling() As String
            Get
                Return _RemainCeiling
            End Get
            Set(ByVal Value As String)
                _RemainCeiling = Value
            End Set
        End Property
        Public Property OCQty() As String
            Get
                Return _OCQty
            End Get
            Set(ByVal Value As String)
                _OCQty = Value
            End Set
        End Property
        Public Property OCAmount() As String
            Get
                Return _OCAmount
            End Get
            Set(ByVal Value As String)
                _OCAmount = Value
            End Set
        End Property
        Public Property RemainOCQty() As String
            Get
                Return _OCRemainOCQty
            End Get
            Set(ByVal Value As String)
                _OCRemainOCQty = Value
            End Set
        End Property
        Public Property RemainOCAmount() As String
            Get
                Return _RemainOCAmount
            End Get
            Set(ByVal Value As String)
                _RemainOCAmount = Value
            End Set
        End Property
        Public Property Gyro() As String
            Get
                Return _Gyro
            End Get
            Set(ByVal Value As String)
                _Gyro = Value
            End Set
        End Property
        Public Property MaxPO() As String
            Get
                Return _MaxPO
            End Get
            Set(ByVal Value As String)
                _MaxPO = Value
            End Set
        End Property
        Public Property Estimation() As String
            Get
                Return _Estimation
            End Get
            Set(ByVal Value As String)
                _Estimation = Value
            End Set
        End Property

    End Class
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Initialization()
        End If
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            Dim Total() As Decimal
            Total = viewstate.Item("FooterTotal")

            CType(e.Item.FindControl("lblFCeiling"), Label).Text = FormatNumber(Total(0), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CType(e.Item.FindControl("lblFOutstanding"), Label).Text = FormatNumber(Total(1), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CType(e.Item.FindControl("lblFProposed"), Label).Text = FormatNumber(Total(2), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CType(e.Item.FindControl("lblFRemainCeiling"), Label).Text = FormatNumber(Total(3), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CType(e.Item.FindControl("lblFOCQty"), Label).Text = FormatNumber(Total(4), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CType(e.Item.FindControl("lblFOCAmount"), Label).Text = FormatNumber(Total(5), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CType(e.Item.FindControl("lblFRemainOCQty"), Label).Text = FormatNumber(Total(6), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CType(e.Item.FindControl("lblFRemainOCAmount"), Label).Text = FormatNumber(Total(7), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CType(e.Item.FindControl("lblFGyro"), Label).Text = FormatNumber(Total(8), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CType(e.Item.FindControl("lblFMaxPO"), Label).Text = FormatNumber(Total(9), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CType(e.Item.FindControl("lblFEstimation"), Label).Text = FormatNumber(Total(10), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        sHelper.SetSession("FrmCeilingVsAllocation.IsAutoBind", True)
        Response.Redirect("FrmCeilingVsAllocation.aspx")
    End Sub

#End Region
End Class
