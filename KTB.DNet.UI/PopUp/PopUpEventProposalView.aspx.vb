#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class PopUpEventProposalView
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlSalesmanArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbPeriode As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlNamaKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSend As System.Web.UI.WebControls.Button
    Protected WithEvents tblCategoryModelType As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents calDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblArea As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblActivityType As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents dtgGuest As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgFoodAndBeverage As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgEntertainment As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgDecoration As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgCar As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgDoorPize As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgOthers As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTotalAllCost As System.Web.UI.WebControls.Label
    Protected WithEvents trGridTamu As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents lblActivityName As System.Web.UI.WebControls.Label
    Protected WithEvents lblActivityDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblActivityPlace As System.Web.UI.WebControls.Label
    Protected WithEvents lblInvitationNumber As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Class Declaration"
    <Serializable()> Private Class SortCommand
        Private _sortColumn As String
        Private _sortDirect As Sort.SortDirection
        Public Sub New()
            _sortColumn = "ID"
            _sortDirect = Sort.SortDirection.ASC
        End Sub
        Public Sub New(ByVal SortColumn As String, ByVal SortDirect As Sort.SortDirection)
            _sortColumn = SortColumn
            _sortDirect = SortDirect
        End Sub
        Public Property SortColumn() As String
            Get
                Return _sortColumn
            End Get
            Set(ByVal Value As String)
                _sortColumn = Value
            End Set
        End Property
        Public Property SortDirect() As Sort.SortDirection
            Get
                Return _sortDirect
            End Get
            Set(ByVal Value As Sort.SortDirection)
                _sortDirect = Value
            End Set
        End Property
    End Class
#End Region

#Region "Constants"

#End Region

#Region "Private Variables"
    Private TotalUnitCost As Decimal = 0
    Private TotalSubCost As Decimal = 0
    Private TotalCost As Decimal = 0
#End Region

#Region "Custom Method"
#Region "Sort"
    Private Property SortMakanan() As SortCommand
        Get
            If IsNothing(ViewState("SortMakanan")) Then
                ViewState("SortMakanan") = New SortCommand
            End If
            Return ViewState("SortMakanan")
        End Get
        Set(ByVal Value As SortCommand)
            ViewState("SortMakanan") = Value
        End Set
    End Property
    Private Property SortEntertainment() As SortCommand
        Get
            If IsNothing(ViewState("SortEntertainment")) Then
                ViewState("SortEntertainment") = New SortCommand
            End If
            Return ViewState("SortEntertainment")
        End Get
        Set(ByVal Value As SortCommand)
            ViewState("SortEntertainment") = Value
        End Set
    End Property
    Private Property SortDecoration() As SortCommand
        Get
            If IsNothing(ViewState("SortDecoration")) Then
                ViewState("SortDecoration") = New SortCommand
            End If
            Return ViewState("SortDecoration")
        End Get
        Set(ByVal Value As SortCommand)
            ViewState("SortDecoration") = Value
        End Set
    End Property
    Private Property SortDisplayCar() As SortCommand
        Get
            If IsNothing(ViewState("SortDisplayCar")) Then
                ViewState("SortDisplayCar") = New SortCommand
            End If
            Return ViewState("SortDisplayCar")
        End Get
        Set(ByVal Value As SortCommand)
            ViewState("SortDisplayCar") = Value
        End Set
    End Property
    Private Property SortDoorPrize() As SortCommand
        Get
            If IsNothing(ViewState("SortDoorPrize")) Then
                ViewState("SortDoorPrize") = New SortCommand
            End If
            Return ViewState("SortDoorPrize")
        End Get
        Set(ByVal Value As SortCommand)
            ViewState("SortDoorPrize") = Value
        End Set
    End Property
    Private Property SortOther() As SortCommand
        Get
            If IsNothing(ViewState("SortOther")) Then
                ViewState("SortOther") = New SortCommand
            End If
            Return ViewState("SortOther")
        End Get
        Set(ByVal Value As SortCommand)
            ViewState("SortOther") = Value
        End Set
    End Property
    Private Property SortGuest() As SortCommand
        Get
            If IsNothing(ViewState("SortGuest")) Then
                ViewState("SortGuest") = New SortCommand
            End If
            Return ViewState("SortGuest")
        End Get
        Set(ByVal Value As SortCommand)
            ViewState("SortGuest") = Value
        End Set
    End Property
#End Region
    Private ReadOnly Property GetID() As Int32
        Get
            Return Request.QueryString("id")
        End Get
    End Property
    Private Function GetDataDetail(ByVal groupCode As EnumEventActivityType.EventActivityTypeGroupCode, ByVal sort As SortCommand) As ArrayList
        Dim objFacade As New EventProposalFacade(User)
        Return objFacade.RetrieveDetail(GetID, groupCode, sort.SortColumn, sort.SortDirect)
    End Function
    Private Sub LoadDataDetail(ByVal groupCode As EnumEventActivityType.EventActivityTypeGroupCode)
        Select Case groupCode
            Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                dtgFoodAndBeverage.DataSource = GetDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.AKO, _
                    SortMakanan)
                dtgFoodAndBeverage.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                dtgCar.DataSource = GetDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.CAR, _
                    SortDisplayCar)
                dtgCar.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                dtgDecoration.DataSource = GetDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.DEK, _
                    SortDecoration)
                dtgDecoration.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                dtgDoorPize.DataSource = GetDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.DOR, _
                    SortDoorPrize)
                dtgDoorPize.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                dtgEntertainment.DataSource = GetDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.ENT, _
                    SortEntertainment)
                dtgEntertainment.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                dtgGuest.DataSource = GetDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.GUE, _
                    SortGuest)
                dtgGuest.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                dtgOthers.DataSource = GetDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.OTH, _
                    SortOther)
                dtgOthers.DataBind()
        End Select
    End Sub
    Private Sub LoadDataDetail()
        LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.AKO)
        LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.CAR)
        LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.DEK)
        LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.DOR)
        LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.ENT)
        LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.GUE)
        LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.OTH)
    End Sub
    Private Sub LoadData()
        Dim objFacade As New EventProposalFacade(User)
        Dim objItem As EventProposal = objFacade.Retrieve(GetID)
        lblDealerCode.Text = objItem.Dealer.DealerCode
        lblCity.Text = objItem.Dealer.City.CityName
        lblActivityType.Text = objItem.EventParameter.ActivityType.ActivityName
        lblArea.Text = objItem.EventParameter.SalesmanArea.AreaDesc
        lblActivityName.Text = objItem.EventParameter.EventName
        lblEventPeriod.Text = String.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}", _
            objItem.EventParameter.EventDateStart, objItem.EventParameter.EventDateEnd)
        lblActivityDate.Text = objItem.ActivitySchedule.ToString("dd/MM/yyyy")
        lblActivityPlace.Text = objItem.ActivityPlace
        lblInvitationNumber.Text = objItem.InvitationNumber
        LoadDataDetail()
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            LoadData()
            lblTotalAllCost.Text = TotalCost.ToString("#,##0")
        End If
    End Sub
    Private Sub dtg_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) _
        Handles dtgDecoration.SortCommand, dtgCar.SortCommand, dtgDoorPize.SortCommand, _
        dtgEntertainment.SortCommand, dtgFoodAndBeverage.SortCommand, dtgGuest.SortCommand, _
        dtgOthers.SortCommand
        Dim dtg As DataGrid = source
        Dim sorts As SortCommand
        Select Case dtg.ID
            Case "dtgFoodAndBeverage"
                sorts = SortMakanan
            Case "dtgEntertainment"
                sorts = SortEntertainment
            Case "dtgDecoration"
                sorts = SortDecoration
            Case "dtgCar"
                sorts = SortDisplayCar
            Case "dtgDoorPize"
                sorts = SortDoorPrize
            Case "dtgOthers"
                sorts = SortOther
            Case "dtgGuest"
                sorts = SortGuest
        End Select
        If sorts.SortColumn = e.SortExpression Then
            Select Case sorts.SortDirect
                Case Sort.SortDirection.ASC
                    sorts.SortDirect = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    sorts.SortDirect = Sort.SortDirection.ASC
            End Select
        Else
            sorts.SortColumn = e.SortExpression
            sorts.SortDirect = Sort.SortDirection.ASC
        End If
        Select Case dtg.ID
            Case "dtgFoodAndBeverage"
                SortMakanan = sorts
                LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.AKO)
            Case "dtgEntertainment"
                SortEntertainment = sorts
                LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.ENT)
            Case "dtgDecoration"
                SortDecoration = sorts
                LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.DEK)
            Case "dtgCar"
                SortDisplayCar = sorts
                LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.CAR)
            Case "dtgDoorPize"
                SortDoorPrize = sorts
                LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.DOR)
            Case "dtgOthers"
                SortOther = sorts
                LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.OTH)
            Case "dtgGuest"
                SortGuest = sorts
                LoadDataDetail(EnumEventActivityType.EventActivityTypeGroupCode.GUE)
        End Select
    End Sub
    Private Sub dtg_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
        Handles dtgFoodAndBeverage.ItemDataBound, dtgEntertainment.ItemDataBound, dtgDecoration.ItemDataBound, _
            dtgDoorPize.ItemDataBound, dtgOthers.ItemDataBound, dtgCar.ItemDataBound, dtgGuest.ItemDataBound
        Dim dtg As DataGrid = sender
        If e.Item.ItemType <> ListItemType.Footer AndAlso e.Item.ItemType <> ListItemType.Header Then
            Dim items As EventProposalDetail = e.Item.DataItem
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtg.PageSize * dtg.CurrentPageIndex)).ToString
            TotalUnitCost = TotalUnitCost + items.UnitCost
            TotalSubCost = TotalSubCost + items.TotalCost
            TotalCost = TotalCost + TotalSubCost
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            If dtg.ID <> "dtgCar" AndAlso dtg.ID <> "dtgGuest" Then
                e.Item.Cells(5).Text = TotalUnitCost.ToString("#,##0")
                e.Item.Cells(6).Text = TotalSubCost.ToString("#,##0")
            End If
            TotalUnitCost = 0
            TotalSubCost = 0
            End If
    End Sub
#End Region

End Class