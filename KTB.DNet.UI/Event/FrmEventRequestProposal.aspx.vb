#Region " Custom Namespace Imports "
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.BusinessFacade.FinishUnit
#End Region

#Region " .NET Base Class Namespace Imports "

#End Region

Public Class FrmEventRequestProposal
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblArea As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents calEventDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblEventPlace As System.Web.UI.WebControls.Label
    Protected WithEvents txtInvitationNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents dtgFoodAndBeverage As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgEntertainment As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgDecoration As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgCar As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgDoorPize As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgOthers As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlEventName As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPlace As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTotalAllCost As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblToolTip As System.Web.UI.WebControls.Label
    Protected WithEvents htipToolTip As System.Web.UI.WebControls.Panel
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidation As System.Web.UI.WebControls.Button
    Protected WithEvents dtgGuest As System.Web.UI.WebControls.DataGrid
    Protected WithEvents trGridTamu As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents btnEdit As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPlaces As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtRavine As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSubDistrict As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOwner As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDriver As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBack As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents ddlActivityType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Class Declaration "
    <Serializable()> Private Class SortCommand
        Private _ColumnToSort As String
        Private _SortDirection As Sort.SortDirection
        Public Sub New()
            _ColumnToSort = "ID"
            _SortDirection = Sort.SortDirection.ASC
        End Sub
        Public Property SortColumn() As String
            Get
                Return _ColumnToSort
            End Get
            Set(ByVal Value As String)
                _ColumnToSort = Value
            End Set
        End Property
        Public Property SortDirection() As Sort.SortDirection
            Get
                Return _SortDirection
            End Get
            Set(ByVal Value As Sort.SortDirection)
                _SortDirection = Value
            End Set
        End Property
    End Class
    Private Class ProcessGridItem
        Private _groupCode As EnumEventActivityType.EventActivityTypeGroupCode
        Public ReadOnly Property GroupCode() As EnumEventActivityType.EventActivityTypeGroupCode
            Get
                Return _groupCode
            End Get
        End Property
        Public ReadOnly Property DataGridObject() As DataGrid
            Get
                Return _gridItem.Parent.Parent
            End Get
        End Property
        Private _gridItem As DataGridItem
        Private _dataItem As V_EventProposalDetail
        Public ReadOnly Property DataItem() As V_EventProposalDetail
            Get
                If IsNothing(_dataItem) Then
                    _dataItem = _gridItem.DataItem
                End If
                Return _dataItem
            End Get
        End Property
        Private _id As TableCell
        Public ReadOnly Property ID() As TableCell
            Get
                If IsNothing(_id) Then
                    _id = _gridItem.Cells(0)
                End If
                Return _id
            End Get
        End Property
        Public Property No() As Integer
            Get
                If IsNumeric(_gridItem.Cells(1).Text) Then
                    Return _gridItem.Cells(1).Text
                Else
                    Return 0
                End If
            End Get
            Set(ByVal Value As Integer)
                _gridItem.Cells(1).Text = Value.ToString
            End Set
        End Property
        Private _jenis As TableCell
        Public ReadOnly Property Jenis() As TableCell
            Get
                If IsNothing(_jenis) Then
                    _jenis = _gridItem.Cells(2)
                End If
                Return _jenis
            End Get
        End Property
        Private _jenisEdit As DropDownList
        Public ReadOnly Property JenisEdit() As DropDownList
            Get
                If IsNothing(_jenisEdit) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "ddlFoodEventActivityTypeEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "ddlPlaceEventActivityTypeEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "ddlDecEventActivityTypeEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "ddlDoorPrizeType"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "ddlEntEventActivityTypeEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "ddlOtherType"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                            idControl = "ddlGuestType"
                    End Select
                    _jenisEdit = _gridItem.FindControl(idControl)
                End If
                Return _jenisEdit
            End Get
        End Property
        Private _item As TableCell
        Public ReadOnly Property Item() As TableCell
            Get
                If IsNothing(_item) Then
                    _item = _gridItem.Cells(3)
                End If
                Return _item
            End Get
        End Property
        Private _itemEdit As TextBox
        Public ReadOnly Property ItemEdit() As TextBox
            Get
                If IsNothing(_itemEdit) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "txtFoodItemEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "txtPlaceItemEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "txtDecEventActivityTypeEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "txtDoorPrizeItem"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "txtEntItemEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "txtOtherItem"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                            idControl = "txtGuestName"
                    End Select
                    _itemEdit = _gridItem.FindControl(idControl)
                End If
                Return _itemEdit
            End Get
        End Property
        Private _qty As TableCell
        Public ReadOnly Property Qty() As TableCell
            Get
                If IsNothing(_qty) Then
                    _qty = _gridItem.Cells(4)
                End If
                Return _qty
            End Get
        End Property
        Private _qtyEdit As TextBox
        Public ReadOnly Property QtyEdit() As TextBox
            Get
                If IsNothing(_qtyEdit) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "txtFoodQuantityEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "txtPlaceQuantity"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                            idControl = "txtCarQuantity"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "txtDecQuantity"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "txtDoorPrizeQuantity"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "txtEntQuantityEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "txtOtherQuantity"
                    End Select
                    _qtyEdit = _gridItem.FindControl(idControl)
                End If
                Return _qtyEdit
            End Get
        End Property
        Private _biayaSatuan As TableCell
        Public ReadOnly Property BiayaSatuan() As TableCell
            Get
                If IsNothing(_biayaSatuan) Then
                    _biayaSatuan = _gridItem.Cells(5)
                End If
                Return _biayaSatuan
            End Get
        End Property
        Private _biayaSatuanEdit As TextBox
        Public ReadOnly Property BiayaSatuanEdit() As TextBox
            Get
                If IsNothing(_biayaSatuanEdit) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "txtFoodUnitCostEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "txtPlaceUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "txtDecUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "txtDoorPrizeUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "txtEntUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "txtOtherUnitCost"
                    End Select
                    _biayaSatuanEdit = _gridItem.FindControl(idControl)
                End If
                Return _biayaSatuanEdit
            End Get
        End Property
        Private _biayaSatuanFooter As Label
        Public ReadOnly Property BiayaSatuanFooter() As Label
            Get
                If IsNothing(_biayaSatuanFooter) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "lblFoodUnitCostTotal"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "lblPlaceUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "lblDecUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "lblDoorPrizeUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "lblEntUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "lblOtherUnitCost"
                    End Select
                    _biayaSatuanFooter = _gridItem.FindControl(idControl)
                End If
                Return _biayaSatuanFooter
            End Get
        End Property
        Private _subTotalBiaya As TableCell
        Public ReadOnly Property SubTotalBiaya() As TableCell
            Get
                If IsNothing(_subTotalBiaya) Then
                    _subTotalBiaya = _gridItem.Cells(6)
                End If
                Return _subTotalBiaya
            End Get
        End Property
        Private _subTotalBiayaFooter As Label
        Public ReadOnly Property SubTotalBiayaFooter() As Label
            Get
                If IsNothing(_subTotalBiayaFooter) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "lblFoodTotalAllCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "lblPlaceSubTotalBiaya"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "lblDecTotalUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "lblDoorPrizeTotalUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "lblEntTotalUnitCost"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "lblOtherTotalCost"
                    End Select
                    _subTotalBiayaFooter = _gridItem.FindControl(idControl)
                End If
                Return _subTotalBiayaFooter
            End Get
        End Property
        Private _linkEdit As LinkButton
        Public ReadOnly Property LinkEdit() As LinkButton
            Get
                If IsNothing(_linkEdit) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "lnbFoodEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "lnbPlaceEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                            idControl = "lnbCardEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "lnbDecEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "lnbDoorPrizeEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "lnbEntEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "lnbOtherEdit"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                            idControl = "lnbGuestEdit"
                    End Select
                    _linkEdit = _gridItem.FindControl(idControl)
                End If
                Return _linkEdit
            End Get
        End Property
        Private _linkDelete As LinkButton
        Public ReadOnly Property LinkDelete() As LinkButton
            Get
                If IsNothing(_linkDelete) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "lnbFoodDelete"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "lnbPlaceDelete"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                            idControl = "lnbCarDelete"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "lnbDecDelete"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "lnbDoorPrizeDelete"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "lnbEntDelete"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "lnbOtherDelete"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                            idControl = "lnbGuestDelete"
                    End Select
                    _linkDelete = _gridItem.FindControl(idControl)
                End If
                Return _linkDelete
            End Get
        End Property
        Private _linkUpdate As LinkButton
        Public ReadOnly Property LinkUpdate() As LinkButton
            Get
                If IsNothing(_linkUpdate) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "lnbFoodUpdate"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "lnbPlaceUpdate"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                            idControl = "lnbCarUpdate"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "lnbDecUpdate"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "lnbDoorPrizeUpdate"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "lnbEntUpdate"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "lnbOtherUpdate"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                            idControl = "lnbGuestUpdate"
                    End Select
                    _linkUpdate = _gridItem.FindControl(idControl)
                End If
                Return _linkUpdate
            End Get
        End Property
        Private _linkAdd As LinkButton
        Public ReadOnly Property LinkAdd() As LinkButton
            Get
                If IsNothing(_linkAdd) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "lnbFoodAdd"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "lnbPlaceAdd"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                            idControl = "lnbCarInsert"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "lnbDecInsert"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "lnbDoorPrizeInsert"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "lnbEntInsert"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "lnbOtherInsert"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                            idControl = "lnbGuestInsert"
                    End Select
                    _linkAdd = _gridItem.FindControl(idControl)
                End If
                Return _linkAdd
            End Get
        End Property
        Private _linkCancel As LinkButton
        Public ReadOnly Property LinkCancel() As LinkButton
            Get
                If IsNothing(_linkCancel) Then
                    Dim idControl As String = String.Empty
                    Select Case _groupCode
                        Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                            idControl = "lnbFoodCancel"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                            idControl = "lnbPlaceCancel"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                            idControl = "lnbCarCancel"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                            idControl = "lnbDecCancel"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                            idControl = "lnbDoorPrizeCancel"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                            idControl = "lnbEntCancel"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                            idControl = "lnbOtherCancel"
                        Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                            idControl = "lnbGuestCancel"
                    End Select
                    _linkCancel = _gridItem.FindControl(idControl)
                End If
                Return _linkCancel
            End Get
        End Property
        Private _model As TableCell
        Public ReadOnly Property Model() As TableCell
            Get
                If IsNothing(_model) Then
                    _model = _gridItem.Cells(2)
                End If
                Return _model
            End Get
        End Property
        Private _modelEdit As DropDownList
        Public ReadOnly Property ModelEdit() As DropDownList
            Get
                If IsNothing(_modelEdit) Then
                    _modelEdit = _gridItem.FindControl("ddlCarModel")
                End If
                Return _modelEdit
            End Get
        End Property
        Private _varian As TableCell
        Public ReadOnly Property Varian() As TableCell
            Get
                If IsNothing(_varian) Then
                    _varian = _gridItem.Cells(3)
                End If
                Return _varian
            End Get
        End Property
        Private _varianEdit As DropDownList
        Public ReadOnly Property VariantEdit() As DropDownList
            Get
                If IsNothing(_varianEdit) Then
                    _varianEdit = _gridItem.FindControl("ddlCarVariant")
                End If
                Return _varianEdit
            End Get
        End Property
        Private _description As TableCell
        Public ReadOnly Property Description() As TableCell
            Get
                If IsNothing(_description) Then
                    _description = _gridItem.Cells(4)
                End If
                Return _description
            End Get
        End Property
        Private _descriptionEdit As TextBox
        Public ReadOnly Property DescriptionEdit() As TextBox
            Get
                If IsNothing(_descriptionEdit) Then
                    _descriptionEdit = _gridItem.FindControl("txtCarDescription")
                End If
                Return _descriptionEdit
            End Get
        End Property
        Private _Jabatan As TableCell
        Public ReadOnly Property Jabatan() As TableCell
            Get
                Return _Jabatan
            End Get
        End Property
        Private _JabatanEdit As TextBox
        Public ReadOnly Property JabatanEdit() As TextBox
            Get
                If IsNothing(_JabatanEdit) Then
                    _JabatanEdit = _gridItem.FindControl("txtGuestRank")
                End If
                Return _JabatanEdit
            End Get
        End Property
        Public Sub New(ByVal groupCode As String, ByVal gridItem As Control)
            _groupCode = [Enum].Parse(GetType(EnumEventActivityType.EventActivityTypeGroupCode), groupCode)
            _gridItem = CType(gridItem, DataGridItem)
        End Sub
        Public Sub New(ByVal groupCode As EnumEventActivityType.EventActivityTypeGroupCode, ByVal gridItem As Control)
            _groupCode = groupCode
            _gridItem = CType(gridItem, DataGridItem)
        End Sub
    End Class
#End Region

#Region " Constants "

#End Region

#Region " Private Variables "
    Private sesHelper As New SessionHelper
    Private objFacadeEventActivityType As New EventActivityTypeFacade(User)
    Private objFacadeVechileType As New VechileTypeFacade(User)
    Private objFacadeVechileModel As New VechileModelFacade(User)
#End Region

#Region " Custom Method "
#Region " Grid DataSouce "
    Private Property SetDataSourceFoodAndBeverage() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession("EventRequestProposalFoodAndBeverage")) Then
                Dim objItem As New ArrayList
                Dim objDetail As New V_EventProposalDetail
                objDetail.ID = -1
                objItem.Add(objDetail)
                sesHelper.SetSession("EventRequestProposalFoodAndBeverage", objItem)
            End If
            Return sesHelper.GetSession("EventRequestProposalFoodAndBeverage")
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession("EventRequestProposalFoodAndBeverage", Value)
        End Set
    End Property
    Private Property SetDataSourcePlaces() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession("EventRequestProposalPlaces")) Then
                Dim objItem As New ArrayList
                Dim objDetail As New V_EventProposalDetail
                objDetail.ID = -1
                objItem.Add(objDetail)
                sesHelper.SetSession("EventRequestProposalPlaces", objItem)
            End If
            Return sesHelper.GetSession("EventRequestProposalPlaces")
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession("EventRequestProposalPlaces", Value)
        End Set
    End Property
    Private Property SetDataSourceEntertainment() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession("EventRequestProposalEntertainment")) Then
                Dim objItem As New ArrayList
                Dim objDetail As New V_EventProposalDetail
                objDetail.ID = -1
                objItem.Add(objDetail)
                sesHelper.SetSession("EventRequestProposalEntertainment", objItem)
            End If
            Return sesHelper.GetSession("EventRequestProposalEntertainment")
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession("EventRequestProposalEntertainment", Value)
        End Set
    End Property
    Private Property SetDataSourceDecoration() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession("EventRequestProposalDecoration")) Then
                Dim objItem As New ArrayList
                Dim objDetail As New V_EventProposalDetail
                objDetail.ID = -1
                objItem.Add(objDetail)
                sesHelper.SetSession("EventRequestProposalDecoration", objItem)
            End If
            Return sesHelper.GetSession("EventRequestProposalDecoration")
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession("EventRequestProposalDecoration", Value)
        End Set
    End Property
    Private Property SetDataSourceDisplayCar() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession("EventRequestProposalDisplayCar")) Then
                Dim objItem As New ArrayList
                Dim objDetail As New V_EventProposalDetail
                objDetail.ID = -1
                objItem.Add(objDetail)
                sesHelper.SetSession("EventRequestProposalDisplayCar", objItem)
            End If
            Return sesHelper.GetSession("EventRequestProposalDisplayCar")
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession("EventRequestProposalDisplayCar", Value)
        End Set
    End Property
    Private Property SetDataSourceDoorPize() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession("EventRequestProposalDoorPrize")) Then
                Dim objItem As New ArrayList
                Dim objDetail As New V_EventProposalDetail
                objDetail.ID = -1
                objItem.Add(objDetail)
                sesHelper.SetSession("EventRequestProposalDoorPrize", objItem)
            End If
            Return sesHelper.GetSession("EventRequestProposalDoorPrize")
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession("EventRequestProposalDoorPrize", Value)
        End Set
    End Property
    Private Property SetDataSourceOthers() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession("EventRequestProposalOthers")) Then
                Dim objItem As New ArrayList
                Dim objDetail As New V_EventProposalDetail
                objDetail.ID = -1
                objItem.Add(objDetail)
                sesHelper.SetSession("EventRequestProposalOthers", objItem)
            End If
            Return sesHelper.GetSession("EventRequestProposalOthers")
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession("EventRequestProposalOthers", Value)
        End Set
    End Property
    Private Property SetDataSourceGuest() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession("EventRequestProposalGuest")) Then
                Dim objItem As New ArrayList
                Dim objDetail As New V_EventProposalDetail
                objDetail.ID = -1
                objItem.Add(objDetail)
                sesHelper.SetSession("EventRequestProposalGuest", objItem)
            End If
            Return sesHelper.GetSession("EventRequestProposalGuest")
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession("EventRequestProposalGuest", Value)
        End Set
    End Property
    Private Property SetDataSourceDeleted() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession("EventRequestProposalDeleted")) Then
                Dim objItem As New ArrayList
                sesHelper.SetSession("EventRequestProposalDeleted", objItem)
            End If
            Return sesHelper.GetSession("EventRequestProposalDeleted")
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession("EventRequestProposalDeleted", Value)
        End Set
    End Property
#End Region
#Region " Sort Property "
    Private Property SortMakanan() As SortCommand
        Get
            If IsNothing(ViewState("SortMakanan")) Then
                ViewState("SortMakanan") = New SortCommand
            End If
            Return CType(ViewState("SortMakanan"), SortCommand)
        End Get
        Set(ByVal Value As SortCommand)
            ViewState("SortMakanan") = Value
        End Set
    End Property
    Private Property SortPlaces() As SortCommand
        Get
            If IsNothing(ViewState("SortPlaces")) Then
                ViewState("SortPlaces") = New SortCommand
            End If
            Return CType(ViewState("SortPlaces"), SortCommand)
        End Get
        Set(ByVal Value As SortCommand)
            ViewState("SortPlaces") = Value
        End Set
    End Property
    Private Property SortEntertainment() As SortCommand
        Get
            If IsNothing(ViewState("SortEntertainment")) Then
                ViewState("SortEntertainment") = New SortCommand
            End If
            Return CType(ViewState("SortEntertainment"), SortCommand)
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
            Return CType(ViewState("SortDecoration"), SortCommand)
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
            Return CType(ViewState("SortDisplayCar"), SortCommand)
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
            Return CType(ViewState("SortDoorPrize"), SortCommand)
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
            Return CType(ViewState("SortOther"), SortCommand)
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
            Return CType(ViewState("SortGuest"), SortCommand)
        End Get
        Set(ByVal Value As SortCommand)
            ViewState("SortGuest") = Value
        End Set
    End Property
#End Region
    Private ReadOnly Property GetEventActivityCar() As Int32
        Get
            If IsNothing(ViewState("EventActivityCar")) Then
                Dim objFacade As New EventActivityTypeFacade(User)
                ViewState("EventActivityCar") = objFacade.RetrieveCarDefault.ID
            End If
            Return CInt(ViewState("EventActivityCar"))
        End Get
    End Property
    Public ReadOnly Property GetActivityTypeID() As Integer
        Get
            Return CInt(Request.QueryString("GroupCode"))
        End Get
    End Property
    Private Function GetActivityType(ByVal ActivityTypeID As Int32) As ActivityType
        Dim objFacade As New ActivityTypeFacade(User)
        Return objFacade.Retrieve(ActivityTypeID)
    End Function
    Private ReadOnly Property GetEventName() As ArrayList
        Get
            If (ddlActivityType.SelectedValue <> "") Then
                Dim objFacade As New EventParameterFacade(User)
                Return objFacade.RetrieveNamaKegiatan(CInt(ddlActivityType.SelectedValue))
            End If
            Return Nothing
        End Get
    End Property
    Private Sub BindEventName()
        ddlEventName.DataSource = GetEventName
        ddlEventName.DataTextField = "EventName"
        ddlEventName.DataValueField = "ID"
        ddlEventName.DataBind()
        ddlEventName.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub
    Private Sub InitDisplay()
        Dim usInfo As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        If (usInfo.TitleDealer.ToString() = EnumDealerTittle.DealerTittle.KTB.ToString()) Then
            btnValidation.Enabled = False
        Else
            btnValidation.Enabled = True
        End If
        lblDealerCode.Text = usInfo.DealerCode
        lblCity.Text = usInfo.City.CityName
        BindAcvtivityType()
        btnValidation.Attributes.Add("onclick", "return confirm('Yakin data akan divalidasi? Jika Ya Tekan Ok, Jika Tidak Tekan Cancel');")
        BindEventName()
        trGridTamu.Visible = Not IsUserDealer
    End Sub
    Private Sub BindAcvtivityType()
        Dim af As ActivityTypeFacade = New ActivityTypeFacade(User)
        Dim al As ArrayList = af.RetrieveByGroupCode(GetActivityTypeID())
        ddlActivityType.DataSource = al
        ddlActivityType.DataTextField = "ActivityName"
        ddlActivityType.DataValueField = "ID"
        ddlActivityType.DataBind()
        ddlActivityType.Items.Insert(0, New ListItem("Silahkan Pilih", ""))
    End Sub
    Private Sub BindGrid()
        BindGrid(EnumEventActivityType.EventActivityTypeGroupCode.AKO)
        BindGrid(EnumEventActivityType.EventActivityTypeGroupCode.PLC)
        BindGrid(EnumEventActivityType.EventActivityTypeGroupCode.CAR)
        BindGrid(EnumEventActivityType.EventActivityTypeGroupCode.DEK)
        BindGrid(EnumEventActivityType.EventActivityTypeGroupCode.DOR)
        BindGrid(EnumEventActivityType.EventActivityTypeGroupCode.ENT)
        BindGrid(EnumEventActivityType.EventActivityTypeGroupCode.OTH)
        BindGrid(EnumEventActivityType.EventActivityTypeGroupCode.GUE)
    End Sub
    Private Sub BindGrid(ByVal groupType As EnumEventActivityType.EventActivityTypeGroupCode)
        Select Case groupType
            Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                If dtgFoodAndBeverage.EditItemIndex = -1 Then
                    dtgFoodAndBeverage.EditItemIndex = SetDataSourceFoodAndBeverage.Count - 1
                End If
                dtgFoodAndBeverage.DataSource = SetDataSourceFoodAndBeverage
                dtgFoodAndBeverage.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                If dtgPlaces.EditItemIndex = -1 Then
                    dtgPlaces.EditItemIndex = SetDataSourcePlaces.Count - 1
                End If
                dtgPlaces.DataSource = SetDataSourcePlaces
                dtgPlaces.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                If dtgEntertainment.EditItemIndex = -1 Then
                    dtgEntertainment.EditItemIndex = SetDataSourceEntertainment.Count - 1
                End If
                dtgEntertainment.DataSource = SetDataSourceEntertainment
                dtgEntertainment.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                If dtgDecoration.EditItemIndex = -1 Then
                    dtgDecoration.EditItemIndex = SetDataSourceDecoration.Count - 1
                End If
                dtgDecoration.DataSource = SetDataSourceDecoration
                dtgDecoration.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                If dtgCar.EditItemIndex = -1 Then
                    dtgCar.EditItemIndex = SetDataSourceDisplayCar.Count - 1
                End If
                dtgCar.DataSource = SetDataSourceDisplayCar
                dtgCar.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                If dtgDoorPize.EditItemIndex = -1 Then
                    dtgDoorPize.EditItemIndex = SetDataSourceDoorPize.Count - 1
                End If
                dtgDoorPize.DataSource = SetDataSourceDoorPize
                dtgDoorPize.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                If dtgOthers.EditItemIndex = -1 Then
                    dtgOthers.EditItemIndex = SetDataSourceOthers.Count - 1
                End If
                dtgOthers.DataSource = SetDataSourceOthers
                dtgOthers.DataBind()
            Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                If dtgGuest.EditItemIndex = -1 Then
                    dtgGuest.EditItemIndex = SetDataSourceGuest.Count - 1
                End If
                dtgGuest.DataSource = SetDataSourceGuest
                dtgGuest.DataBind()
        End Select
    End Sub
    Private Function SumTotalCost(ByVal objArray As ArrayList) As Decimal
        Dim retTotal As Decimal = 0
        For Each eventDetail As V_EventProposalDetail In objArray
            retTotal = retTotal + eventDetail.TotalCost
        Next
        Return retTotal
    End Function
    Private Function ProcessDataSource(ByVal dsTarget As ArrayList, ByVal targetGrid As DataGrid, Optional ByVal itemToAdd As V_EventProposalDetail = Nothing) As ArrayList
        dsTarget = RemoveNewRow(dsTarget)
        If Not IsNothing(itemToAdd) Then
            dsTarget.Add(itemToAdd)
        End If
        targetGrid.EditItemIndex = dsTarget.Add(New V_EventProposalDetail(-1))
        Return dsTarget
    End Function
    Private Sub EmptyDataSource()
        SetDataSourceDecoration = Nothing
        SetDataSourcePlaces = Nothing
        SetDataSourceDisplayCar = Nothing
        SetDataSourceDoorPize = Nothing
        SetDataSourceEntertainment = Nothing
        SetDataSourceFoodAndBeverage = Nothing
        SetDataSourceOthers = Nothing
        SetDataSourceGuest = Nothing
        SetDataSourceDeleted = Nothing
    End Sub
    Private Function DeleteRow(ByVal dsTarget As ArrayList, ByVal id As Integer) As ArrayList
        Dim indexDelete As Int32 = -1
        For i As Integer = 0 To dsTarget.Count - 1
            If CType(dsTarget(i), V_EventProposalDetail).ID = id Then
                indexDelete = i
                Exit For
            End If
        Next
        dsTarget.RemoveAt(indexDelete)
        Return dsTarget
    End Function
    Private Function GetDataSource(ByVal groupCode As EnumEventActivityType.EventActivityTypeGroupCode) As ArrayList
        Select Case groupCode
            Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                Return SetDataSourceFoodAndBeverage
            Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                Return SetDataSourcePlaces
            Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                Return SetDataSourceDisplayCar
            Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                Return SetDataSourceDecoration
            Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                Return SetDataSourceDoorPize
            Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                Return SetDataSourceEntertainment
            Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                Return SetDataSourceOthers
            Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                Return SetDataSourceGuest
        End Select
    End Function
    Private Function GetSortCommand(ByVal groupCode As EnumEventActivityType.EventActivityTypeGroupCode) As SortCommand
        Select Case groupCode
            Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                Return SortMakanan
            Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                Return SortPlaces
            Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                Return SortDisplayCar
            Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                Return SortDecoration
            Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                Return SortDoorPrize
            Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                Return SortEntertainment
            Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                Return SortGuest
            Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                Return SortOther
        End Select
    End Function
    Private Sub SetDataSource(ByVal groupCode As EnumEventActivityType.EventActivityTypeGroupCode, ByVal dsTarget As ArrayList)
        Select Case groupCode
            Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                SetDataSourceFoodAndBeverage = dsTarget
            Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                SetDataSourcePlaces = dsTarget
            Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                SetDataSourceDisplayCar = dsTarget
            Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                SetDataSourceDecoration = dsTarget
            Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                SetDataSourceDoorPize = dsTarget
            Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                SetDataSourceEntertainment = dsTarget
            Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                SetDataSourceOthers = dsTarget
            Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                SetDataSourceGuest = dsTarget
        End Select
    End Sub
    Private Sub SetSortCommand(ByVal groupCode As EnumEventActivityType.EventActivityTypeGroupCode, ByVal sort As SortCommand)
        Select Case groupCode
            Case EnumEventActivityType.EventActivityTypeGroupCode.AKO
                SortMakanan = sort
            Case EnumEventActivityType.EventActivityTypeGroupCode.PLC
                SortPlaces = sort
            Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                SortDisplayCar = sort
            Case EnumEventActivityType.EventActivityTypeGroupCode.DEK
                SortDecoration = sort
            Case EnumEventActivityType.EventActivityTypeGroupCode.DOR
                SortDoorPrize = sort
            Case EnumEventActivityType.EventActivityTypeGroupCode.ENT
                SortEntertainment = sort
            Case EnumEventActivityType.EventActivityTypeGroupCode.GUE
                SortGuest = sort
            Case EnumEventActivityType.EventActivityTypeGroupCode.OTH
                SortOther = sort
        End Select
    End Sub
    Private ReadOnly Property IsUserDealer() As Boolean
        Get
            Dim objUserInfo As UserInfo = sesHelper.GetSession("LOGINUSERINFO")
            Return Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB
        End Get
    End Property
    Private ReadOnly Property IsPageValid() As Boolean
        Get
            Dim IsValid As Boolean = True
            If ddlEventName.SelectedValue = -1 Then
                IsValid = False
                MessageBox.Show("Silahkan pilih Nama Kegiatan")
            End If
            If GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.AKO).Count = 1 AndAlso _
                CType(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.AKO)(0), _
                V_EventProposalDetail).ID = -1 Then
                IsValid = False
                If GetActivityTypeID <> CType(EnumActivityType.ActivityType.Small_Gathering, Int32) Then
                    MessageBox.Show("Masukkan Makanan, Minuman")
                Else
                    MessageBox.Show("Masukkan Makanan, Minuman, Sewa Tempat")
                End If
            End If
            If GetActivityTypeID = CType(EnumActivityType.ActivityType.Small_Gathering, Int32) AndAlso _
                GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.PLC).Count = 1 AndAlso _
                CType(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.PLC)(0), _
                V_EventProposalDetail).ID = -1 Then
                IsValid = False
                MessageBox.Show("Masukkan Sewa Tempat")
            End If
            If GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.CAR).Count = 1 AndAlso _
                CType(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.CAR)(0), _
                V_EventProposalDetail).ID = -1 Then
                IsValid = False
                MessageBox.Show("Masukkan Display Car")
            End If
            If GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DEK).Count = 1 AndAlso _
                CType(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DEK)(0), _
                V_EventProposalDetail).ID = -1 Then
                IsValid = False
                MessageBox.Show("Masukkan Dekorasi")
            End If
            If GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DOR).Count = 1 AndAlso _
                CType(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DOR)(0), _
                V_EventProposalDetail).ID = -1 Then
                IsValid = False
                MessageBox.Show("Masukkan Door Prize")
            End If
            If GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.ENT).Count = 1 AndAlso _
                CType(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.ENT)(0), _
                V_EventProposalDetail).ID = -1 Then
                IsValid = False
                MessageBox.Show("Masukkan Entertainment")
            End If
            If GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.OTH).Count = 1 AndAlso _
                CType(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.OTH)(0), _
                V_EventProposalDetail).ID = -1 Then
                IsValid = False
                MessageBox.Show("Masukkan Lain-lain")
            End If
            If Not IsUserDealer AndAlso _
                GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.GUE).Count = 1 AndAlso _
                CType(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.GUE)(0), _
                V_EventProposalDetail).ID = -1 Then
                IsValid = False
                MessageBox.Show("Masukkan Tamu/PIC MMKSI")
            End If
            Return IsValid
        End Get
    End Property
    Private Function GetEventParameter(ByVal id As Int32) As EventParameter
        Dim objFacade As New EventParameterFacade(User)
        Return objFacade.Retrieve(id)
    End Function
    Private Function MapDtgIDToGroupCode(ByVal id As String) As EnumEventActivityType.EventActivityTypeGroupCode
        Select Case id
            Case "dtgFoodAndBeverage"
                Return EnumEventActivityType.EventActivityTypeGroupCode.AKO
            Case "dtgPlaces"
                Return EnumEventActivityType.EventActivityTypeGroupCode.PLC
            Case "dtgEntertainment"
                Return EnumEventActivityType.EventActivityTypeGroupCode.ENT
            Case "dtgDecoration"
                Return EnumEventActivityType.EventActivityTypeGroupCode.DEK
            Case "dtgCar"
                Return EnumEventActivityType.EventActivityTypeGroupCode.CAR
            Case "dtgDoorPize"
                Return EnumEventActivityType.EventActivityTypeGroupCode.DOR
            Case "dtgOthers"
                Return EnumEventActivityType.EventActivityTypeGroupCode.OTH
            Case "dtgGuest"
                Return EnumEventActivityType.EventActivityTypeGroupCode.GUE
        End Select
    End Function
    Private Function RemoveNewRow(ByVal objArray As ArrayList) As ArrayList
        If objArray.Count > 0 AndAlso CType(objArray(objArray.Count - 1), V_EventProposalDetail).ID = -1 Then
            objArray.RemoveAt(objArray.Count - 1)
        End If
        Return objArray
    End Function
    Private Sub ReadOnlyInput(ByVal IsReadOnly As Boolean)
        ddlActivityType.Enabled = Not IsReadOnly
        ddlEventName.Enabled = Not IsReadOnly
        calEventDate.Enabled = Not IsReadOnly
        txtPlace.Enabled = Not IsReadOnly
        txtInvitationNumber.Enabled = Not IsReadOnly
        txtRavine.Enabled = Not IsReadOnly
        txtSubDistrict.Enabled = Not IsReadOnly
        txtOwner.Enabled = Not IsReadOnly
        txtDriver.Enabled = Not IsReadOnly
        dtgGuest.Enabled = Not IsReadOnly
        dtgFoodAndBeverage.Enabled = Not IsReadOnly
        dtgPlaces.Enabled = Not IsReadOnly
        dtgEntertainment.Enabled = Not IsReadOnly
        dtgDecoration.Enabled = Not IsReadOnly
        dtgCar.Enabled = Not IsReadOnly
        dtgDoorPize.Enabled = Not IsReadOnly
        dtgOthers.Enabled = Not IsReadOnly
        dtgFoodAndBeverage.Columns(dtgFoodAndBeverage.Columns.Count - 1).Visible = Not IsReadOnly
        dtgFoodAndBeverage.AllowSorting = Not IsReadOnly
        dtgPlaces.Columns(dtgPlaces.Columns.Count - 1).Visible = Not IsReadOnly
        dtgPlaces.AllowSorting = Not IsReadOnly
        dtgCar.Columns(dtgCar.Columns.Count - 1).Visible = Not IsReadOnly
        dtgCar.AllowSorting = Not IsReadOnly
        dtgDecoration.Columns(dtgDecoration.Columns.Count - 1).Visible = Not IsReadOnly
        dtgDecoration.AllowSorting = Not IsReadOnly
        dtgDoorPize.Columns(dtgDoorPize.Columns.Count - 1).Visible = Not IsReadOnly
        dtgDoorPize.AllowSorting = Not IsReadOnly
        dtgEntertainment.Columns(dtgEntertainment.Columns.Count - 1).Visible = Not IsReadOnly
        dtgEntertainment.AllowSorting = Not IsReadOnly
        dtgGuest.Columns(dtgGuest.Columns.Count - 1).Visible = Not IsReadOnly
        dtgGuest.AllowSorting = Not IsReadOnly
        dtgOthers.Columns(dtgOthers.Columns.Count - 1).Visible = Not IsReadOnly
        dtgOthers.AllowSorting = Not IsReadOnly
        btnEdit.Visible = IsReadOnly
        btnValidation.Visible = IsReadOnly
        btnSave.Visible = Not IsReadOnly
        If IsReadOnly Then
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.AKO, _
                RemoveNewRow(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.AKO)))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.PLC, _
                RemoveNewRow(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.PLC)))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.CAR, _
                RemoveNewRow(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.CAR)))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DEK, _
                RemoveNewRow(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DEK)))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DOR, _
                RemoveNewRow(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DOR)))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.ENT, _
                RemoveNewRow(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.ENT)))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.GUE, _
                RemoveNewRow(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.GUE)))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.OTH, _
                RemoveNewRow(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.OTH)))
        Else
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.AKO, _
                ProcessDataSource(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.AKO), _
                dtgFoodAndBeverage))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.PLC, _
                ProcessDataSource(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.PLC), _
                dtgPlaces))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.CAR, _
                ProcessDataSource(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.CAR), _
                dtgCar))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DEK, _
                ProcessDataSource(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DEK), _
                dtgDecoration))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DOR, _
                ProcessDataSource(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.DOR), _
                dtgDoorPize))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.ENT, _
                ProcessDataSource(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.ENT), _
                dtgEntertainment))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.GUE, _
                ProcessDataSource(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.GUE), _
                dtgGuest))
            SetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.OTH, _
                ProcessDataSource(GetDataSource(EnumEventActivityType.EventActivityTypeGroupCode.OTH), _
                dtgOthers))
        End If
        BindGrid()
    End Sub
    Private Property GetID() As Int32
        Get
            If IsNothing(ViewState("ID")) Then
                If Not IsNothing(Request.QueryString("id")) Then
                    ViewState("ID") = Request.QueryString("id")
                Else
                    ViewState("ID") = -1
                End If
            End If
            Return CInt(ViewState("ID"))
        End Get
        Set(ByVal Value As Int32)
            ViewState("ID") = Value
        End Set
    End Property
    Private Sub LoadDataDetail(ByVal objColl As ArrayList)
        Dim objGues As New ArrayList, objFood As New ArrayList, objEnte As New ArrayList
        Dim objDeko As New ArrayList, objDisp As New ArrayList, objDoor As New ArrayList
        Dim objLain As New ArrayList, objPlac As New ArrayList
        For Each objItem As EventProposalDetail In objColl
            Dim objView As New V_EventProposalDetail(objItem)
            If Not IsNothing(objItem.EventActivityType) Then
                Select Case objItem.EventActivityType.EventActivityTypeGroupCode
                    Case EnumEventActivityType.EventActivityTypeGroupCode.AKO.ToString
                        objFood.Add(objView)
                    Case EnumEventActivityType.EventActivityTypeGroupCode.PLC.ToString
                        If GetActivityTypeID = _
                            CType(EnumActivityType.ActivityType.Small_Gathering, Int32) Then
                            objPlac.Add(objView)
                        Else
                            objFood.Add(objView)
                        End If
                    Case EnumEventActivityType.EventActivityTypeGroupCode.DEK.ToString
                        objDeko.Add(objView)
                    Case EnumEventActivityType.EventActivityTypeGroupCode.DOR.ToString
                        objDoor.Add(objView)
                    Case EnumEventActivityType.EventActivityTypeGroupCode.ENT.ToString
                        objEnte.Add(objView)
                    Case EnumEventActivityType.EventActivityTypeGroupCode.GUE.ToString
                        objGues.Add(objView)
                    Case EnumEventActivityType.EventActivityTypeGroupCode.OTH.ToString
                        objLain.Add(objView)
                    Case EnumEventActivityType.EventActivityTypeGroupCode.CAR.ToString
                        objDisp.Add(objView)
                End Select
            End If
        Next
        SetDataSourceFoodAndBeverage = ProcessDataSource(objFood, dtgFoodAndBeverage)
        SetDataSourcePlaces = ProcessDataSource(objPlac, dtgPlaces)
        SetDataSourceDisplayCar = ProcessDataSource(objDisp, dtgCar)
        SetDataSourceDecoration = ProcessDataSource(objDeko, dtgDecoration)
        SetDataSourceDoorPize = ProcessDataSource(objDoor, dtgDoorPize)
        SetDataSourceEntertainment = ProcessDataSource(objEnte, dtgEntertainment)
        SetDataSourceGuest = ProcessDataSource(objGues, dtgGuest)
        SetDataSourceOthers = ProcessDataSource(objLain, dtgOthers)
    End Sub
    Private Sub LoadData()
        Dim objFacade As New EventProposalFacade(User)
        Dim objItem As EventProposal = objFacade.Retrieve(GetID)
        lblDealerCode.Text = objItem.Dealer.DealerCode
        lblCity.Text = objItem.Dealer.City.CityName
        ddlActivityType.SelectedValue = objItem.ActivityType.ID.ToString()
        BindEventName()
        ddlEventName.SelectedValue = objItem.EventParameter.ID.ToString()
        ddlEventName_SelectedIndexChanged(Nothing, Nothing)
        calEventDate.Value = objItem.ActivitySchedule
        txtPlace.Text = objItem.ActivityPlace
        txtInvitationNumber.Text = objItem.InvitationNumber.ToString
        txtRavine.Text = objItem.Ravine
        txtSubDistrict.Text = objItem.SubDistrict
        txtOwner.Text = objItem.Owner.ToString
        txtDriver.Text = objItem.Driver.ToString
        LoadDataDetail(objItem.EventProposalDetails)
        If objItem.EventProposalStatus <> EnumEventProposalStatus.EventProposalStatus.Baru Then
            btnValidation.Enabled = False
            btnSave.Enabled = False
            btnEdit.Enabled = False
        End If
    End Sub
#End Region

#Region " Event Handler "
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            EmptyDataSource()
            InitDisplay()
            If GetID > -1 Then
                LoadData()
                ReadOnlyInput(True)
                btnBack.Visible = True
            Else
                btnBack.Visible = False
            End If
            BindGrid()
        End If
    End Sub
    Private Sub ddlEventName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEventName.SelectedIndexChanged
        If ddlEventName.SelectedIndex > 0 Then
            Dim objFacade As New EventParameterFacade(User)
            Dim objEvent As EventParameter = objFacade.Retrieve(CInt(ddlEventName.SelectedValue))
            If objEvent.EventDateStart.Year = objEvent.EventDateEnd.Year Then
                lblEventPeriod.Text = String.Format("{0:dd MMM} - {1:dd MMM yyyy}", objEvent.EventDateStart, objEvent.EventDateEnd)
            Else
                lblEventPeriod.Text = String.Format("{0:dd MMM yyyy} - {1:dd MMM yyyy}", objEvent.EventDateStart, objEvent.EventDateEnd)
            End If
            If Not IsNothing(objEvent.SalesmanArea) Then
                lblArea.Text = objEvent.SalesmanArea.AreaDesc
            End If
        End If
    End Sub
    Public Sub ddlCarModel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlCarModel As DropDownList = sender
        Dim gridItem As New ProcessGridItem(EnumEventActivityType.EventActivityTypeGroupCode.CAR, _
            ddlCarModel.Parent.Parent)
        gridItem.VariantEdit.Items.Clear()
        If ddlCarModel.SelectedIndex > 0 Then
            Dim crits As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(VechileType), "VechileModel", ddlCarModel.SelectedValue))
            Dim sorts As New SortCollection
            sorts.Add(New Sort(GetType(VechileType), "Description", Sort.SortDirection.ASC))
            Dim hsType As New Hashtable
            For Each vType As VechileType In objFacadeVechileType.RetrieveByCriteria(crits, sorts)
                If Not hsType.ContainsKey(vType.Description) Then
                    hsType.Add(vType.Description, vType)
                End If
            Next
            Dim objArrayData As New ArrayList
            For Each data As VechileType In hsType.Values
                objArrayData.Add(data)
            Next
            gridItem.VariantEdit.DataSource = objArrayData
            gridItem.VariantEdit.DataTextField = "Description"
            gridItem.VariantEdit.DataValueField = "ID"
            gridItem.VariantEdit.DataBind()
            gridItem.VariantEdit.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        Else
            gridItem.VariantEdit.Items.Insert(0, New ListItem("Pilih Model", "-1"))
        End If
    End Sub
    Private Sub dtgCar_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCar.ItemDataBound
        Dim dtg As DataGrid = sender
        Dim gridItem As New ProcessGridItem(EnumEventActivityType.EventActivityTypeGroupCode.CAR, e.Item)
        If e.Item.ItemType <> ListItemType.Footer AndAlso e.Item.ItemIndex > -1 Then
            If e.Item.ItemType = ListItemType.EditItem Then
                gridItem.ModelEdit.DataSource = objFacadeVechileModel.RetrieveActiveList
                gridItem.ModelEdit.DataTextField = "Description"
                gridItem.ModelEdit.DataValueField = "ID"
                gridItem.ModelEdit.DataBind()
                gridItem.ModelEdit.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
                gridItem.LinkUpdate.Visible = gridItem.DataItem.ID <> -1
                gridItem.LinkCancel.Visible = gridItem.DataItem.ID <> -1
                gridItem.LinkAdd.Visible = gridItem.DataItem.ID = -1
                If gridItem.DataItem.ID <> -1 Then
                    gridItem.ModelEdit.SelectedValue = gridItem.DataItem.VechileType.VechileModel.ToString
                    ddlCarModel_SelectedIndexChanged(gridItem.ModelEdit, Nothing)
                    gridItem.VariantEdit.SelectedValue = gridItem.DataItem.VechileType.ID.ToString
                Else
                    ddlCarModel_SelectedIndexChanged(gridItem.ModelEdit, Nothing)
                End If
            Else
                gridItem.LinkDelete.Attributes.Add("onclick", String.Format("return confirm('{0}');", SR.DeleteConfirmation))
            End If
            gridItem.No = (e.Item.ItemIndex + 1 + (dtg.PageSize * dtg.CurrentPageIndex)).ToString
        End If
    End Sub
    Private Sub dtgGuest_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgGuest.ItemDataBound
        Dim dtg As DataGrid = sender
        Dim gridItem As New ProcessGridItem(EnumEventActivityType.EventActivityTypeGroupCode.GUE, e.Item)
        If e.Item.ItemType <> ListItemType.Footer AndAlso e.Item.ItemIndex > -1 Then
            If e.Item.ItemType = ListItemType.EditItem Then
                gridItem.JenisEdit.DataSource = objFacadeEventActivityType.RetrieveActiveList(gridItem.GroupCode)
                gridItem.JenisEdit.DataTextField = "EventActivityTypeName"
                gridItem.JenisEdit.DataValueField = "ID"
                gridItem.JenisEdit.DataBind()
                gridItem.JenisEdit.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
                gridItem.LinkUpdate.Visible = gridItem.DataItem.ID <> -1
                gridItem.LinkCancel.Visible = gridItem.DataItem.ID <> -1
                gridItem.LinkAdd.Visible = gridItem.DataItem.ID = -1
                If gridItem.DataItem.ID <> -1 Then
                    gridItem.JenisEdit.SelectedValue = gridItem.DataItem.EventActivityTypeID.ToString
                End If
            Else
                gridItem.LinkDelete.Attributes.Add("onclick", String.Format("return confirm('{0}');", SR.DeleteConfirmation))
            End If
            gridItem.No = CType((e.Item.ItemIndex + 1 + (dtg.PageSize * dtg.CurrentPageIndex)).ToString, String)
        End If
    End Sub
    Private TotalUnitPrice As Decimal = 0
    Private SubTotalCost As Decimal = 0
    Private Sub dtg_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
        Handles dtgFoodAndBeverage.ItemDataBound, dtgEntertainment.ItemDataBound, dtgDecoration.ItemDataBound, _
            dtgDoorPize.ItemDataBound, dtgOthers.ItemDataBound, dtgPlaces.ItemDataBound
        Dim dtg As DataGrid = sender
        Dim gridItem As New ProcessGridItem(MapDtgIDToGroupCode(dtg.ID), e.Item)
        If e.Item.ItemType <> ListItemType.Footer AndAlso e.Item.ItemIndex > -1 Then
            If e.Item.ItemType = ListItemType.EditItem Then
                Dim objJeniss As ArrayList = objFacadeEventActivityType.RetrieveActiveList(gridItem.GroupCode)
                If GetActivityTypeID <> CType(EnumActivityType.ActivityType.Small_Gathering, Int32) _
                    AndAlso gridItem.GroupCode = EnumEventActivityType.EventActivityTypeGroupCode.AKO Then
                    objJeniss.AddRange(objFacadeEventActivityType.RetrieveActiveList(EnumEventActivityType.EventActivityTypeGroupCode.PLC))
                End If
                gridItem.JenisEdit.DataSource = objJeniss
                gridItem.JenisEdit.DataTextField = "EventActivityTypeName"
                gridItem.JenisEdit.DataValueField = "ID"
                gridItem.JenisEdit.DataBind()
                gridItem.JenisEdit.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
                gridItem.LinkUpdate.Visible = gridItem.DataItem.ID <> -1
                gridItem.LinkCancel.Visible = gridItem.DataItem.ID <> -1
                gridItem.LinkAdd.Visible = gridItem.DataItem.ID = -1
                If gridItem.DataItem.ID <> -1 Then
                    gridItem.JenisEdit.SelectedValue = gridItem.DataItem.EventActivityTypeID.ToString
                End If
            Else
                gridItem.LinkDelete.Attributes.Add("onclick", String.Format("return confirm('{0}');", SR.DeleteConfirmation))
            End If
            TotalUnitPrice = TotalUnitPrice + gridItem.DataItem.UnitCost
            SubTotalCost = SubTotalCost + gridItem.DataItem.TotalCost
            gridItem.No = (e.Item.ItemIndex + 1 + (dtg.PageSize * dtg.CurrentPageIndex)).ToString
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            gridItem.BiayaSatuanFooter.Text = TotalUnitPrice.ToString("#,##0")
            gridItem.SubTotalBiayaFooter.Text = SubTotalCost.ToString("#,##0")
            TotalUnitPrice = 0
            SubTotalCost = 0
        End If
    End Sub
    Public Sub lnbUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnbUpdate As LinkButton = sender
        Dim gridItem As New ProcessGridItem(lnbUpdate.CommandArgument, lnbUpdate.Parent.Parent)
        Dim objArray As ArrayList = GetDataSource(gridItem.GroupCode)
        Dim objEditItem As V_EventProposalDetail = objArray(gridItem.No - 1)
        If gridItem.GroupCode = EnumEventActivityType.EventActivityTypeGroupCode.CAR Then
            objEditItem.VechileTypeID = gridItem.VariantEdit.SelectedValue
            objEditItem.Description = gridItem.DescriptionEdit.Text
        Else
            objEditItem.EventActivityTypeID = gridItem.JenisEdit.SelectedValue
            objEditItem.Item = gridItem.ItemEdit.Text
            If gridItem.GroupCode = EnumEventActivityType.EventActivityTypeGroupCode.GUE Then
                objEditItem.Description = gridItem.JabatanEdit.Text
            Else
                objEditItem.UnitCost = gridItem.BiayaSatuanEdit.Text
            End If
        End If
        If gridItem.GroupCode <> EnumEventActivityType.EventActivityTypeGroupCode.GUE Then
            objEditItem.Quantity = gridItem.QtyEdit.Text
            objEditItem.TotalCost = objEditItem.Quantity * objEditItem.UnitCost
        End If
        objArray(gridItem.No - 1) = objEditItem
        SetDataSource(gridItem.GroupCode, ProcessDataSource(objArray, gridItem.DataGridObject))
        BindGrid(gridItem.GroupCode)
    End Sub
    Public Sub lnbDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnbDelete As LinkButton = sender
        Dim gridItem As New ProcessGridItem(lnbDelete.CommandArgument, lnbDelete.Parent.Parent)
        If gridItem.ID.Text > -1 Then
            Dim objDeleted As ArrayList = SetDataSourceDeleted
            objDeleted.Add(New V_EventProposalDetail(gridItem.ID.Text))
            SetDataSourceDeleted = objDeleted
        End If
        SetDataSource(gridItem.GroupCode, ProcessDataSource(DeleteRow(GetDataSource(gridItem.GroupCode), _
            gridItem.ID.Text), gridItem.DataGridObject))
        BindGrid(gridItem.GroupCode)
    End Sub
    Public Sub lnbInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnbAdd As LinkButton = sender
        Dim gridItem As New ProcessGridItem(lnbAdd.CommandArgument, lnbAdd.Parent.Parent)
        Select Case gridItem.GroupCode
            Case EnumEventActivityType.EventActivityTypeGroupCode.CAR
                If gridItem.ModelEdit.SelectedValue = -1 Then
                    MessageBox.Show("Silahkan pilih model kendaraan")
                    Exit Sub
                Else
                    If gridItem.VariantEdit.SelectedValue = -1 Then
                        MessageBox.Show("Silahkan pilih varian kendaraan")
                        Exit Sub
                    End If
                End If
            Case Else
                If gridItem.JenisEdit.SelectedValue = -1 Then
                    MessageBox.Show("Silahkan pilih Jenis")
                    Exit Sub
                End If
        End Select
        Dim objItem As New V_EventProposalDetail(DateTime.Now.ToString("-HHmmss"))
        If gridItem.GroupCode = EnumEventActivityType.EventActivityTypeGroupCode.CAR Then
            objItem.EventActivityTypeID = GetEventActivityCar
            objItem.Description = gridItem.DescriptionEdit.Text
            objItem.VechileTypeID = gridItem.VariantEdit.SelectedValue
        Else
            objItem.EventActivityTypeID = gridItem.JenisEdit.SelectedValue
            objItem.Item = gridItem.ItemEdit.Text
            If gridItem.GroupCode = EnumEventActivityType.EventActivityTypeGroupCode.GUE Then
                objItem.Description = gridItem.JabatanEdit.Text
            Else
                objItem.UnitCost = gridItem.BiayaSatuanEdit.Text
            End If
        End If
        If gridItem.GroupCode <> EnumEventActivityType.EventActivityTypeGroupCode.GUE Then
            objItem.Quantity = gridItem.QtyEdit.Text
            objItem.TotalCost = objItem.Quantity * objItem.UnitCost
        End If
        SetDataSource(gridItem.GroupCode, ProcessDataSource(GetDataSource(gridItem.GroupCode), _
            gridItem.DataGridObject, objItem))
        BindGrid(gridItem.GroupCode)
    End Sub
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Dim TotalCost As Decimal = 0
        TotalCost = SumTotalCost(SetDataSourceDecoration)
        TotalCost = TotalCost + SumTotalCost(SetDataSourceDoorPize)
        TotalCost = TotalCost + SumTotalCost(SetDataSourceEntertainment)
        TotalCost = TotalCost + SumTotalCost(SetDataSourceFoodAndBeverage)
        TotalCost = TotalCost + SumTotalCost(SetDataSourceOthers)
        TotalCost = TotalCost + SumTotalCost(SetDataSourcePlaces)
        lblTotalAllCost.Text = String.Format("Total Biaya : {0:#,##0}", TotalCost)
    End Sub
    Private Sub dtgCar_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCar.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.Cells(5).Attributes.Add("onmouseover", "showheadertip(this);")
            e.Item.Cells(5).Attributes.Add("onmouseout", "hideheadertip(this);")
        End If
    End Sub
    Private Sub dtg_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
        Handles dtgCar.ItemCommand, dtgDecoration.ItemCommand, dtgDoorPize.ItemCommand, dtgOthers.ItemCommand, _
        dtgEntertainment.ItemCommand, dtgFoodAndBeverage.ItemCommand, dtgGuest.ItemCommand, dtgPlaces.ItemCommand
        If e.CommandName = "Edit" OrElse e.CommandName = "Cancel" Then
            Dim dtgGrid As DataGrid = source
            Dim gridItem As New ProcessGridItem(CType(e.CommandArgument, String), e.Item)
            Select Case e.CommandName
                Case "Edit"
                    dtgGrid.EditItemIndex = e.Item.ItemIndex
                    SetDataSource(gridItem.GroupCode, RemoveNewRow(GetDataSource(gridItem.GroupCode)))
                    BindGrid(gridItem.GroupCode)
                Case "Cancel"
                    dtgGrid.EditItemIndex = -1
                    SetDataSource(gridItem.GroupCode, ProcessDataSource(GetDataSource(gridItem.GroupCode), dtgGrid))
                    BindGrid(gridItem.GroupCode)
            End Select
        End If
    End Sub
    Private Sub dtg_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) _
        Handles dtgDecoration.SortCommand, dtgCar.SortCommand, dtgDoorPize.SortCommand, _
        dtgEntertainment.SortCommand, dtgFoodAndBeverage.SortCommand, dtgGuest.SortCommand, _
        dtgOthers.SortCommand
        Dim dtg As DataGrid = source
        Dim groupCode As EnumEventActivityType.EventActivityTypeGroupCode = MapDtgIDToGroupCode(dtg.ID)
        SetDataSource(groupCode, RemoveNewRow(GetDataSource(groupCode)))
        Dim sort As SortCommand = GetSortCommand(groupCode)
        If sort.SortColumn = e.SortExpression Then
            Select Case sort.SortDirection
                Case sort.SortDirection.ASC
                    sort.SortDirection = sort.SortDirection.DESC
                Case sort.SortDirection.DESC
                    sort.SortDirection = sort.SortDirection.ASC
            End Select
        Else
            sort.SortColumn = e.SortExpression
            sort.SortDirection = sort.SortDirection.ASC
        End If
        SetSortCommand(groupCode, sort)
        SetDataSource(groupCode, ProcessDataSource(CommonFunction.SortListControl(GetDataSource(groupCode), _
            sort.SortColumn, sort.SortDirection), dtg))
        BindGrid(groupCode)
    End Sub
    Private Sub btnValidation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidation.Click
        Dim objFacade As New EventProposalFacade(User)
        Dim objItem As EventProposal = objFacade.Retrieve(GetID)
        If objItem.EventProposalStatus = EnumEventProposalStatus.EventProposalStatus.Validasi Then
            MessageBox.Show("Proposal Sudah di validasi")
        Else
            objItem.EventProposalStatus = EnumEventProposalStatus.EventProposalStatus.Validasi
            If objFacade.Update(objItem) = 1 Then
                btnValidation.Enabled = False
                btnSave.Enabled = False
                btnEdit.Enabled = False
                MessageBox.Show(SR.ValidateSucces)
            Else
                MessageBox.Show(SR.ValidateFail)
            End If
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If IsPageValid Then
            Dim objFacade As New EventProposalFacade(User)
            Dim objFacadeHistory As New EventProposalHistoryFacade(User)
            Dim objItem As New EventProposal
            Dim objHistory As New EventProposalHistory
            If GetID > -1 Then
                objItem = objFacade.Retrieve(GetID)
            Else
                objItem = New EventProposal
                objItem.EventProposalStatus = EnumEventProposalStatus.EventProposalStatus.Baru
                objItem.EventAgreementStatus = EnumEventAgreementStatus.EventAgreementStatus.Baru
            End If
            objHistory.ActivityPlaceOld = objItem.ActivityPlace
            objItem.ActivityPlace = txtPlace.Text
            objHistory.ActivityPlaceNew = objItem.ActivityPlace
            objHistory.ActivityScheduleOld = objItem.ActivitySchedule
            objItem.ActivitySchedule = calEventDate.Value
            objHistory.ActivityScheduleNew = objItem.ActivitySchedule
            objItem.ActivityType = GetActivityType(CInt(ddlActivityType.SelectedValue))
            objItem.Dealer = sesHelper.GetSession("DEALER")
            objItem.EventParameter = GetEventParameter(ddlEventName.SelectedValue)
            objItem.InvitationNumber = txtInvitationNumber.Text
            objItem.Ravine = txtRavine.Text
            objItem.SubDistrict = txtSubDistrict.Text
            objItem.Owner = txtOwner.Text
            objItem.Driver = txtDriver.Text
            If (objFacade.IsProposalExists(objItem.ID, objItem.Dealer.ID, objItem.ActivityType.ID, objItem.EventParameter.ID)) Then
                MessageBox.Show("Sudah ada proposal untuk nama kegiatan ini")
                Return
            End If
            Dim objUserInfo As UserInfo = sesHelper.GetSession("LOGINUSERINFO")
            objHistory.UpdateBy = String.Format("{0}-{1}", objUserInfo.Dealer.DealerCode, objUserInfo.UserName)
            Dim objItemDetail As New ArrayList
            objItemDetail.AddRange(SetDataSourceDecoration)
            objItemDetail.AddRange(SetDataSourcePlaces)
            objItemDetail.AddRange(SetDataSourceDisplayCar)
            objItemDetail.AddRange(SetDataSourceDoorPize)
            objItemDetail.AddRange(SetDataSourceEntertainment)
            objItemDetail.AddRange(SetDataSourceFoodAndBeverage)
            objItemDetail.AddRange(SetDataSourceOthers)
            If Not IsUserDealer Then
                objItemDetail.AddRange(SetDataSourceGuest)
            End If
            objHistory.EventProposal = objItem
            If objFacade.InsertOrUpdate(objItem, objItemDetail, SetDataSourceDeleted, objHistory) <> -1 Then
                ReadOnlyInput(True)
                btnSave.Visible = False
                GetID = objItem.ID
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        ReadOnlyInput(False)
    End Sub
    Private Sub ddlActivityType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlActivityType.SelectedIndexChanged
        BindEventName()
    End Sub
#End Region

End Class