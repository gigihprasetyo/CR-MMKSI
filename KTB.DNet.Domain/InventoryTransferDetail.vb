
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : InventoryTransferDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 25/03/2018 - 21:47:40
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("InventoryTransferDetail")> _
    Public Class InventoryTransferDetail
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _owner As String = String.Empty
        Private _baseQuantity As Double
        Private _consumptionTaxIn As String = String.Empty
        Private _consumptionTaxOut As String = String.Empty
        Private _fromBatchNo As String = String.Empty
        Private _fromDealer As String = String.Empty
        Private _fromConfiguration As String = String.Empty
        Private _fromExteriorColor As String = String.Empty
        Private _fromInteriorColor As String = String.Empty
        Private _fromLocation As String = String.Empty
        Private _fromSerialNo As String = String.Empty
        Private _fromSite As String = String.Empty
        Private _fromStyle As String = String.Empty
        Private _fromWarehouse As String = String.Empty
        Private _inventoryTransferNo As String = String.Empty
        Private _inventoryUnit As String = String.Empty
        Private _productDescription As String = String.Empty
        Private _product As String = String.Empty
        Private _quantity As Double
        Private _remarks As String = String.Empty
        Private _servicePartsandMaterial As String = String.Empty
        Private _sourceData As String = String.Empty
        Private _stockNumber As String = String.Empty
        Private _stockNumberNV As String = String.Empty
        Private _stockNumberLookupName As String = String.Empty
        Private _stockNumberLookupType As Integer
        Private _toBatchNo As String = String.Empty
        Private _toDealer As String = String.Empty
        Private _toConfiguration As String = String.Empty
        Private _toExteriorColor As String = String.Empty
        Private _toInteriorColor As String = String.Empty
        Private _toLocation As String = String.Empty
        Private _toSerialNo As String = String.Empty
        Private _toSite As String = String.Empty
        Private _toStyle As String = String.Empty
        Private _toWarehouse As String = String.Empty
        Private _transactionUnit As String = String.Empty
        Private _vIN As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _inventoryTransfer As InventoryTransfer



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("Owner", "'{0}'")> _
        Public Property Owner As String
            Get
                Return _owner
            End Get
            Set(ByVal value As String)
                _owner = value
            End Set
        End Property


        <ColumnInfo("BaseQuantity", "#,##0")> _
        Public Property BaseQuantity As Double
            Get
                Return _baseQuantity
            End Get
            Set(ByVal value As Double)
                _baseQuantity = value
            End Set
        End Property


        <ColumnInfo("ConsumptionTaxIn", "'{0}'")> _
        Public Property ConsumptionTaxIn As String
            Get
                Return _consumptionTaxIn
            End Get
            Set(ByVal value As String)
                _consumptionTaxIn = value
            End Set
        End Property


        <ColumnInfo("ConsumptionTaxOut", "'{0}'")> _
        Public Property ConsumptionTaxOut As String
            Get
                Return _consumptionTaxOut
            End Get
            Set(ByVal value As String)
                _consumptionTaxOut = value
            End Set
        End Property


        <ColumnInfo("FromBatchNo", "'{0}'")> _
        Public Property FromBatchNo As String
            Get
                Return _fromBatchNo
            End Get
            Set(ByVal value As String)
                _fromBatchNo = value
            End Set
        End Property


        <ColumnInfo("FromDealer", "'{0}'")> _
        Public Property FromDealer As String
            Get
                Return _fromDealer
            End Get
            Set(ByVal value As String)
                _fromDealer = value
            End Set
        End Property


        <ColumnInfo("FromConfiguration", "'{0}'")> _
        Public Property FromConfiguration As String
            Get
                Return _fromConfiguration
            End Get
            Set(ByVal value As String)
                _fromConfiguration = value
            End Set
        End Property


        <ColumnInfo("FromExteriorColor", "'{0}'")> _
        Public Property FromExteriorColor As String
            Get
                Return _fromExteriorColor
            End Get
            Set(ByVal value As String)
                _fromExteriorColor = value
            End Set
        End Property


        <ColumnInfo("FromInteriorColor", "'{0}'")> _
        Public Property FromInteriorColor As String
            Get
                Return _fromInteriorColor
            End Get
            Set(ByVal value As String)
                _fromInteriorColor = value
            End Set
        End Property


        <ColumnInfo("FromLocation", "'{0}'")> _
        Public Property FromLocation As String
            Get
                Return _fromLocation
            End Get
            Set(ByVal value As String)
                _fromLocation = value
            End Set
        End Property


        <ColumnInfo("FromSerialNo", "'{0}'")> _
        Public Property FromSerialNo As String
            Get
                Return _fromSerialNo
            End Get
            Set(ByVal value As String)
                _fromSerialNo = value
            End Set
        End Property


        <ColumnInfo("FromSite", "'{0}'")> _
        Public Property FromSite As String
            Get
                Return _fromSite
            End Get
            Set(ByVal value As String)
                _fromSite = value
            End Set
        End Property


        <ColumnInfo("FromStyle", "'{0}'")> _
        Public Property FromStyle As String
            Get
                Return _fromStyle
            End Get
            Set(ByVal value As String)
                _fromStyle = value
            End Set
        End Property


        <ColumnInfo("FromWarehouse", "'{0}'")> _
        Public Property FromWarehouse As String
            Get
                Return _fromWarehouse
            End Get
            Set(ByVal value As String)
                _fromWarehouse = value
            End Set
        End Property


        <ColumnInfo("InventoryTransferNo", "'{0}'")> _
        Public Property InventoryTransferNo As String
            Get
                Return _inventoryTransferNo
            End Get
            Set(ByVal value As String)
                _inventoryTransferNo = value
            End Set
        End Property


        <ColumnInfo("InventoryUnit", "'{0}'")> _
        Public Property InventoryUnit As String
            Get
                Return _inventoryUnit
            End Get
            Set(ByVal value As String)
                _inventoryUnit = value
            End Set
        End Property


        <ColumnInfo("ProductDescription", "'{0}'")> _
        Public Property ProductDescription As String
            Get
                Return _productDescription
            End Get
            Set(ByVal value As String)
                _productDescription = value
            End Set
        End Property


        <ColumnInfo("Product", "'{0}'")> _
        Public Property Product As String
            Get
                Return _product
            End Get
            Set(ByVal value As String)
                _product = value
            End Set
        End Property


        <ColumnInfo("Quantity", "#,##0")> _
        Public Property Quantity As Double
            Get
                Return _quantity
            End Get
            Set(ByVal value As Double)
                _quantity = value
            End Set
        End Property


        <ColumnInfo("Remarks", "'{0}'")> _
        Public Property Remarks As String
            Get
                Return _remarks
            End Get
            Set(ByVal value As String)
                _remarks = value
            End Set
        End Property


        <ColumnInfo("ServicePartsandMaterial", "'{0}'")> _
        Public Property ServicePartsandMaterial As String
            Get
                Return _servicePartsandMaterial
            End Get
            Set(ByVal value As String)
                _servicePartsandMaterial = value
            End Set
        End Property


        <ColumnInfo("SourceData", "'{0}'")> _
        Public Property SourceData As String
            Get
                Return _sourceData
            End Get
            Set(ByVal value As String)
                _sourceData = value
            End Set
        End Property


        <ColumnInfo("StockNumber", "'{0}'")> _
        Public Property StockNumber As String
            Get
                Return _stockNumber
            End Get
            Set(ByVal value As String)
                _stockNumber = value
            End Set
        End Property


        <ColumnInfo("StockNumberNV", "'{0}'")> _
        Public Property StockNumberNV As String
            Get
                Return _stockNumberNV
            End Get
            Set(ByVal value As String)
                _stockNumberNV = value
            End Set
        End Property


        <ColumnInfo("StockNumberLookupName", "'{0}'")> _
        Public Property StockNumberLookupName As String
            Get
                Return _stockNumberLookupName
            End Get
            Set(ByVal value As String)
                _stockNumberLookupName = value
            End Set
        End Property


        <ColumnInfo("StockNumberLookupType", "{0}")> _
        Public Property StockNumberLookupType As Integer
            Get
                Return _stockNumberLookupType
            End Get
            Set(ByVal value As Integer)
                _stockNumberLookupType = value
            End Set
        End Property


        <ColumnInfo("ToBatchNo", "'{0}'")> _
        Public Property ToBatchNo As String
            Get
                Return _toBatchNo
            End Get
            Set(ByVal value As String)
                _toBatchNo = value
            End Set
        End Property


        <ColumnInfo("ToDealer", "'{0}'")> _
        Public Property ToDealer As String
            Get
                Return _toDealer
            End Get
            Set(ByVal value As String)
                _toDealer = value
            End Set
        End Property


        <ColumnInfo("ToConfiguration", "'{0}'")> _
        Public Property ToConfiguration As String
            Get
                Return _toConfiguration
            End Get
            Set(ByVal value As String)
                _toConfiguration = value
            End Set
        End Property


        <ColumnInfo("ToExteriorColor", "'{0}'")> _
        Public Property ToExteriorColor As String
            Get
                Return _toExteriorColor
            End Get
            Set(ByVal value As String)
                _toExteriorColor = value
            End Set
        End Property


        <ColumnInfo("ToInteriorColor", "'{0}'")> _
        Public Property ToInteriorColor As String
            Get
                Return _toInteriorColor
            End Get
            Set(ByVal value As String)
                _toInteriorColor = value
            End Set
        End Property


        <ColumnInfo("ToLocation", "'{0}'")> _
        Public Property ToLocation As String
            Get
                Return _toLocation
            End Get
            Set(ByVal value As String)
                _toLocation = value
            End Set
        End Property


        <ColumnInfo("ToSerialNo", "'{0}'")> _
        Public Property ToSerialNo As String
            Get
                Return _toSerialNo
            End Get
            Set(ByVal value As String)
                _toSerialNo = value
            End Set
        End Property


        <ColumnInfo("ToSite", "'{0}'")> _
        Public Property ToSite As String
            Get
                Return _toSite
            End Get
            Set(ByVal value As String)
                _toSite = value
            End Set
        End Property


        <ColumnInfo("ToStyle", "'{0}'")> _
        Public Property ToStyle As String
            Get
                Return _toStyle
            End Get
            Set(ByVal value As String)
                _toStyle = value
            End Set
        End Property


        <ColumnInfo("ToWarehouse", "'{0}'")> _
        Public Property ToWarehouse As String
            Get
                Return _toWarehouse
            End Get
            Set(ByVal value As String)
                _toWarehouse = value
            End Set
        End Property


        <ColumnInfo("TransactionUnit", "'{0}'")> _
        Public Property TransactionUnit As String
            Get
                Return _transactionUnit
            End Get
            Set(ByVal value As String)
                _transactionUnit = value
            End Set
        End Property


        <ColumnInfo("VIN", "'{0}'")> _
        Public Property VIN As String
            Get
                Return _vIN
            End Get
            Set(ByVal value As String)
                _vIN = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("InventoryTransferID", "{0}"), _
        RelationInfo("InventoryTransfer", "ID", "InventoryTransferDetail", "InventoryTransferID")> _
        Public Property InventoryTransfer As InventoryTransfer
            Get
                Try
                    If Not IsNothing(Me._inventoryTransfer) AndAlso (Not Me._inventoryTransfer.IsLoaded) Then

                        Me._inventoryTransfer = CType(DoLoad(GetType(InventoryTransfer).ToString(), _inventoryTransfer.ID), InventoryTransfer)
                        Me._inventoryTransfer.MarkLoaded()

                    End If

                    Return Me._inventoryTransfer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As InventoryTransfer)

                Me._inventoryTransfer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._inventoryTransfer.MarkLoaded()
                End If
            End Set
        End Property



#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

