
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : SparePartPRDetailFromVendor Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 15:12:37
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
    <Serializable(), TableInfo("SparePartPRDetailFromVendor")> _
    Public Class SparePartPRDetailFromVendor
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
        Private _pRDetailNumber As String = String.Empty
        Private _pRNumber As String = String.Empty
        Private _owner As String = String.Empty
        Private _baseReceivedQuantity As Double
        Private _batchNumber As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _chassisModel As String = String.Empty
        Private _chassisNumberRegister As String = String.Empty
        Private _consumptionTax1Amount As Decimal
        Private _consumptionTax1 As String = String.Empty
        Private _consumptionTax2Amount As Decimal
        Private _consumptionTax2 As String = String.Empty
        Private _discountAmount As Decimal
        Private _engineNumber As String = String.Empty
        Private _eventData As String = String.Empty
        Private _inventoryUnit As String = String.Empty
        Private _keyNumber As String = String.Empty
        Private _landedCost As Decimal
        Private _location As String = String.Empty
        Private _productDescription As String = String.Empty
        Private _product As String = String.Empty
        Private _productVolume As Double
        Private _productWeight As Double
        Private _purchaseUnit As String = String.Empty
        Private _receivedQuantity As Double
        Private _referenceNumber As String = String.Empty
        Private _returnPRDetail As String = String.Empty
        Private _servicePartsAndMaterial As String = String.Empty
        Private _site As String = String.Empty
        Private _stockNumber As String = String.Empty
        Private _titleRegistrationFee As Decimal
        Private _totalAmount As Decimal
        Private _totalBaseAmount As Decimal
        Private _totalConsumptionTaxAmount As Decimal
        Private _totalVolume As Double
        Private _totalWeight As Double
        Private _transactionAmount As Decimal
        Private _unitCost As Decimal
        Private _warehouse As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartPRFromVendor As SparePartPRFromVendor



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


        <ColumnInfo("PRDetailNumber", "'{0}'")> _
        Public Property PRDetailNumber As String
            Get
                Return _pRDetailNumber
            End Get
            Set(ByVal value As String)
                _pRDetailNumber = value
            End Set
        End Property


        <ColumnInfo("PRNumber", "'{0}'")> _
        Public Property PRNumber As String
            Get
                Return _pRNumber
            End Get
            Set(ByVal value As String)
                _pRNumber = value
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


        <ColumnInfo("BaseReceivedQuantity", "#,##0")> _
        Public Property BaseReceivedQuantity As Double
            Get
                Return _baseReceivedQuantity
            End Get
            Set(ByVal value As Double)
                _baseReceivedQuantity = value
            End Set
        End Property


        <ColumnInfo("BatchNumber", "'{0}'")> _
        Public Property BatchNumber As String
            Get
                Return _batchNumber
            End Get
            Set(ByVal value As String)
                _batchNumber = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("ChassisModel", "'{0}'")> _
        Public Property ChassisModel As String
            Get
                Return _chassisModel
            End Get
            Set(ByVal value As String)
                _chassisModel = value
            End Set
        End Property


        <ColumnInfo("ChassisNumberRegister", "'{0}'")> _
        Public Property ChassisNumberRegister As String
            Get
                Return _chassisNumberRegister
            End Get
            Set(ByVal value As String)
                _chassisNumberRegister = value
            End Set
        End Property


        <ColumnInfo("ConsumptionTax1Amount", "{0}")> _
        Public Property ConsumptionTax1Amount As Decimal
            Get
                Return _consumptionTax1Amount
            End Get
            Set(ByVal value As Decimal)
                _consumptionTax1Amount = value
            End Set
        End Property


        <ColumnInfo("ConsumptionTax1", "'{0}'")> _
        Public Property ConsumptionTax1 As String
            Get
                Return _consumptionTax1
            End Get
            Set(ByVal value As String)
                _consumptionTax1 = value
            End Set
        End Property


        <ColumnInfo("ConsumptionTax2Amount", "{0}")> _
        Public Property ConsumptionTax2Amount As Decimal
            Get
                Return _consumptionTax2Amount
            End Get
            Set(ByVal value As Decimal)
                _consumptionTax2Amount = value
            End Set
        End Property


        <ColumnInfo("ConsumptionTax2", "'{0}'")> _
        Public Property ConsumptionTax2 As String
            Get
                Return _consumptionTax2
            End Get
            Set(ByVal value As String)
                _consumptionTax2 = value
            End Set
        End Property


        <ColumnInfo("DiscountAmount", "{0}")> _
        Public Property DiscountAmount As Decimal
            Get
                Return _discountAmount
            End Get
            Set(ByVal value As Decimal)
                _discountAmount = value
            End Set
        End Property


        <ColumnInfo("EngineNumber", "'{0}'")> _
        Public Property EngineNumber As String
            Get
                Return _engineNumber
            End Get
            Set(ByVal value As String)
                _engineNumber = value
            End Set
        End Property


        <ColumnInfo("EventData", "'{0}'")> _
        Public Property EventData As String
            Get
                Return _eventData
            End Get
            Set(ByVal value As String)
                _eventData = value
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


        <ColumnInfo("KeyNumber", "'{0}'")> _
        Public Property KeyNumber As String
            Get
                Return _keyNumber
            End Get
            Set(ByVal value As String)
                _keyNumber = value
            End Set
        End Property


        <ColumnInfo("LandedCost", "{0}")> _
        Public Property LandedCost As Decimal
            Get
                Return _landedCost
            End Get
            Set(ByVal value As Decimal)
                _landedCost = value
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


        <ColumnInfo("ProductVolume", "#,##0")> _
        Public Property ProductVolume As Double
            Get
                Return _productVolume
            End Get
            Set(ByVal value As Double)
                _productVolume = value
            End Set
        End Property


        <ColumnInfo("ProductWeight", "#,##0")> _
        Public Property ProductWeight As Double
            Get
                Return _productWeight
            End Get
            Set(ByVal value As Double)
                _productWeight = value
            End Set
        End Property


        <ColumnInfo("PurchaseUnit", "'{0}'")> _
        Public Property PurchaseUnit As String
            Get
                Return _purchaseUnit
            End Get
            Set(ByVal value As String)
                _purchaseUnit = value
            End Set
        End Property


        <ColumnInfo("ReceivedQuantity", "#,##0")> _
        Public Property ReceivedQuantity As Double
            Get
                Return _receivedQuantity
            End Get
            Set(ByVal value As Double)
                _receivedQuantity = value
            End Set
        End Property


        <ColumnInfo("ReferenceNumber", "'{0}'")> _
        Public Property ReferenceNumber As String
            Get
                Return _referenceNumber
            End Get
            Set(ByVal value As String)
                _referenceNumber = value
            End Set
        End Property


        <ColumnInfo("ReturnPRDetail", "'{0}'")> _
        Public Property ReturnPRDetail As String
            Get
                Return _returnPRDetail
            End Get
            Set(ByVal value As String)
                _returnPRDetail = value
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


        <ColumnInfo("StockNumber", "'{0}'")> _
        Public Property StockNumber As String
            Get
                Return _stockNumber
            End Get
            Set(ByVal value As String)
                _stockNumber = value
            End Set
        End Property


        <ColumnInfo("TitleRegistrationFee", "{0}")> _
        Public Property TitleRegistrationFee As Decimal
            Get
                Return _titleRegistrationFee
            End Get
            Set(ByVal value As Decimal)
                _titleRegistrationFee = value
            End Set
        End Property


        <ColumnInfo("TotalAmount", "{0}")> _
        Public Property TotalAmount As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
            End Set
        End Property


        <ColumnInfo("TotalBaseAmount", "{0}")> _
        Public Property TotalBaseAmount As Decimal
            Get
                Return _totalBaseAmount
            End Get
            Set(ByVal value As Decimal)
                _totalBaseAmount = value
            End Set
        End Property


        <ColumnInfo("TotalConsumptionTaxAmount", "{0}")> _
        Public Property TotalConsumptionTaxAmount As Decimal
            Get
                Return _totalConsumptionTaxAmount
            End Get
            Set(ByVal value As Decimal)
                _totalConsumptionTaxAmount = value
            End Set
        End Property


        <ColumnInfo("TotalVolume", "#,##0")> _
        Public Property TotalVolume As Double
            Get
                Return _totalVolume
            End Get
            Set(ByVal value As Double)
                _totalVolume = value
            End Set
        End Property


        <ColumnInfo("TotalWeight", "#,##0")> _
        Public Property TotalWeight As Double
            Get
                Return _totalWeight
            End Get
            Set(ByVal value As Double)
                _totalWeight = value
            End Set
        End Property


        <ColumnInfo("TransactionAmount", "{0}")> _
        Public Property TransactionAmount As Decimal
            Get
                Return _transactionAmount
            End Get
            Set(ByVal value As Decimal)
                _transactionAmount = value
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


        <ColumnInfo("SparePartPRID", "{0}"), _
        RelationInfo("SparePartPRFromVendor", "ID", "SparePartPRDetailFromVendor", "SparePartPRID")> _
        Public Property SparePartPRFromVendor As SparePartPRFromVendor
            Get
                Try
                    If Not isnothing(Me._sparePartPRFromVendor) AndAlso (Not Me._sparePartPRFromVendor.IsLoaded) Then

                        Me._sparePartPRFromVendor = CType(DoLoad(GetType(SparePartPRFromVendor).ToString(), _sparePartPRFromVendor.ID), SparePartPRFromVendor)
                        Me._sparePartPRFromVendor.MarkLoaded()

                    End If

                    Return Me._sparePartPRFromVendor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartPRFromVendor)

                Me._sparePartPRFromVendor = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPRFromVendor.MarkLoaded()
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