
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : InventoryTransactionDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 26/03/2018 - 13:42:09
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
    <Serializable(), TableInfo("InventoryTransactionDetail")> _
    Public Class InventoryTransactionDetail
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
        Private _batchNo As String = String.Empty
        Private _bU As String = String.Empty
        Private _department As String = String.Empty
        Private _description As String = String.Empty
        Private _fromBU As String = String.Empty
        Private _inventoryTransactionNo As String = String.Empty
        Private _inventoryTransferDetail As String = String.Empty
        Private _inventoryUnit As String = String.Empty
        Private _location As String = String.Empty
        Private _productCrossReference As String = String.Empty
        Private _productDescription As String = String.Empty
        Private _product As String = String.Empty
        Private _quantity As Double
        Private _reasonCode As String = String.Empty
        Private _referenceNo As String = String.Empty
        Private _registerSerialNumber As String = String.Empty
        Private _runningNumber As Integer
        Private _serialNo As String = String.Empty
        Private _servicePartsAndMaterial As String = String.Empty
        Private _site As String = String.Empty
        Private _sourceData As String = String.Empty
        Private _stockNumber As String = String.Empty
        Private _stockNumberNV As String = String.Empty
        Private _totalCost As Decimal
        Private _transactionType As String = String.Empty
        Private _transactionUnit As String = String.Empty
        Private _unitCost As Decimal
        Private _vIN As String = String.Empty
        Private _warehouse As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _inventoryTransaction As InventoryTransaction



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


        <ColumnInfo("BatchNo", "'{0}'")> _
        Public Property BatchNo As String
            Get
                Return _batchNo
            End Get
            Set(ByVal value As String)
                _batchNo = value
            End Set
        End Property


        <ColumnInfo("BU", "'{0}'")> _
        Public Property BU As String
            Get
                Return _bU
            End Get
            Set(ByVal value As String)
                _bU = value
            End Set
        End Property


        <ColumnInfo("Department", "'{0}'")> _
        Public Property Department As String
            Get
                Return _department
            End Get
            Set(ByVal value As String)
                _department = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("FromBU", "'{0}'")> _
        Public Property FromBU As String
            Get
                Return _fromBU
            End Get
            Set(ByVal value As String)
                _fromBU = value
            End Set
        End Property


        <ColumnInfo("InventoryTransactionNo", "'{0}'")> _
        Public Property InventoryTransactionNo As String
            Get
                Return _inventoryTransactionNo
            End Get
            Set(ByVal value As String)
                _inventoryTransactionNo = value
            End Set
        End Property


        <ColumnInfo("InventoryTransferDetail", "'{0}'")> _
        Public Property InventoryTransferDetail As String
            Get
                Return _inventoryTransferDetail
            End Get
            Set(ByVal value As String)
                _inventoryTransferDetail = value
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


        <ColumnInfo("Location", "'{0}'")> _
        Public Property Location As String
            Get
                Return _location
            End Get
            Set(ByVal value As String)
                _location = value
            End Set
        End Property


        <ColumnInfo("ProductCrossReference", "'{0}'")> _
        Public Property ProductCrossReference As String
            Get
                Return _productCrossReference
            End Get
            Set(ByVal value As String)
                _productCrossReference = value
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


        <ColumnInfo("ReasonCode", "'{0}'")> _
        Public Property ReasonCode As String
            Get
                Return _reasonCode
            End Get
            Set(ByVal value As String)
                _reasonCode = value
            End Set
        End Property


        <ColumnInfo("ReferenceNo", "'{0}'")> _
        Public Property ReferenceNo As String
            Get
                Return _referenceNo
            End Get
            Set(ByVal value As String)
                _referenceNo = value
            End Set
        End Property


        <ColumnInfo("RegisterSerialNumber", "'{0}'")> _
        Public Property RegisterSerialNumber As String
            Get
                Return _registerSerialNumber
            End Get
            Set(ByVal value As String)
                _registerSerialNumber = value
            End Set
        End Property


        <ColumnInfo("RunningNumber", "{0}")> _
        Public Property RunningNumber As Integer
            Get
                Return _runningNumber
            End Get
            Set(ByVal value As Integer)
                _runningNumber = value
            End Set
        End Property


        <ColumnInfo("SerialNo", "'{0}'")> _
        Public Property SerialNo As String
            Get
                Return _serialNo
            End Get
            Set(ByVal value As String)
                _serialNo = value
            End Set
        End Property


        <ColumnInfo("ServicePartsAndMaterial", "'{0}'")> _
        Public Property ServicePartsAndMaterial As String
            Get
                Return _servicePartsAndMaterial
            End Get
            Set(ByVal value As String)
                _servicePartsAndMaterial = value
            End Set
        End Property


        <ColumnInfo("Site", "'{0}'")> _
        Public Property Site As String
            Get
                Return _site
            End Get
            Set(ByVal value As String)
                _site = value
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


        <ColumnInfo("TotalCost", "{0}")> _
        Public Property TotalCost As Decimal
            Get
                Return _totalCost
            End Get
            Set(ByVal value As Decimal)
                _totalCost = value
            End Set
        End Property


        <ColumnInfo("TransactionType", "'{0}'")> _
        Public Property TransactionType As String
            Get
                Return _transactionType
            End Get
            Set(ByVal value As String)
                _transactionType = value
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


        <ColumnInfo("UnitCost", "{0}")> _
        Public Property UnitCost As Decimal
            Get
                Return _unitCost
            End Get
            Set(ByVal value As Decimal)
                _unitCost = value
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


        <ColumnInfo("Warehouse", "'{0}'")> _
        Public Property Warehouse As String
            Get
                Return _warehouse
            End Get
            Set(ByVal value As String)
                _warehouse = value
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


        <ColumnInfo("InventoryTransactionID", "{0}"), _
        RelationInfo("InventoryTransaction", "ID", "InventoryTransactionDetail", "InventoryTransactionID")> _
        Public Property InventoryTransaction As InventoryTransaction
            Get
                Try
                    If Not isnothing(Me._inventoryTransaction) AndAlso (Not Me._inventoryTransaction.IsLoaded) Then

                        Me._inventoryTransaction = CType(DoLoad(GetType(InventoryTransaction).ToString(), _inventoryTransaction.ID), InventoryTransaction)
                        Me._inventoryTransaction.MarkLoaded()

                    End If

                    Return Me._inventoryTransaction

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As InventoryTransaction)

                Me._inventoryTransaction = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._inventoryTransaction.MarkLoaded()
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

