
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : POOtherVendorDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 15:07:43
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
    <Serializable(), TableInfo("POOtherVendorDetail")> _
    Public Class POOtherVendorDetail
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
        Private _dealerCode As String = String.Empty
        Private _closeLine As Boolean
        Private _closeReason As String = String.Empty
        Private _completed As Boolean
        Private _consumptionTax1Amount As Decimal
        Private _consumptionTax1 As String = String.Empty
        Private _consumptionTax2Amount As Decimal
        Private _consumptionTax2 As String = String.Empty
        Private _department As String = String.Empty
        Private _description As String = String.Empty
        Private _discountAmount As Decimal
        Private _discountPercentage As Double
        Private _eventData As String = String.Empty
        Private _formSource As Short
        Private _baseQtyOrder As Double
        Private _baseQtyReceipt As Double
        Private _baseQtyReturn As Double
        Private _inventoryUnit As String = String.Empty
        Private _productCrossReference As String = String.Empty
        Private _productDescription As String = String.Empty
        Private _product As String = String.Empty
        Private _productSubstitute As String = String.Empty
        Private _productVariant As String = String.Empty
        Private _productVolume As Double
        Private _productWeight As Double
        Private _promisedDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _purchaseFor As Short
        Private _purchaseOrderNo As String = String.Empty
        Private _purchaseRequisitionDetail As String = String.Empty
        Private _purchaseUnit As String = String.Empty
        Private _qtyOrder As Double
        Private _qtyReceipt As Double
        Private _qtyReturn As Double
        Private _recallProduct As Boolean
        Private _referenceNo As String = String.Empty
        Private _requiredDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _salesOrderDetail As String = String.Empty
        Private _scheduledShippingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _servicePartsAndMaterial As String = String.Empty
        Private _shippingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _site As String = String.Empty
        Private _stockNumber As String = String.Empty
        Private _titleRegistrationFee As Decimal
        Private _totalAmount As Decimal
        Private _totalAmountBeforeDiscount As Decimal
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

        Private _pOOtherVendor As POOtherVendor



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


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("CloseLine", "{0}")> _
        Public Property CloseLine As Boolean
            Get
                Return _closeLine
            End Get
            Set(ByVal value As Boolean)
                _closeLine = value
            End Set
        End Property


        <ColumnInfo("CloseReason", "'{0}'")> _
        Public Property CloseReason As String
            Get
                Return _closeReason
            End Get
            Set(ByVal value As String)
                _closeReason = value
            End Set
        End Property


        <ColumnInfo("Completed", "{0}")> _
        Public Property Completed As Boolean
            Get
                Return _completed
            End Get
            Set(ByVal value As Boolean)
                _completed = value
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


        <ColumnInfo("DiscountAmount", "{0}")> _
        Public Property DiscountAmount As Decimal
            Get
                Return _discountAmount
            End Get
            Set(ByVal value As Decimal)
                _discountAmount = value
            End Set
        End Property


        <ColumnInfo("DiscountPercentage", "#,##0")> _
        Public Property DiscountPercentage As Double
            Get
                Return _discountPercentage
            End Get
            Set(ByVal value As Double)
                _discountPercentage = value
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


        <ColumnInfo("FormSource", "{0}")> _
        Public Property FormSource As Short
            Get
                Return _formSource
            End Get
            Set(ByVal value As Short)
                _formSource = value
            End Set
        End Property


        <ColumnInfo("BaseQtyOrder", "#,##0")> _
        Public Property BaseQtyOrder As Double
            Get
                Return _baseQtyOrder
            End Get
            Set(ByVal value As Double)
                _baseQtyOrder = value
            End Set
        End Property


        <ColumnInfo("BaseQtyReceipt", "#,##0")> _
        Public Property BaseQtyReceipt As Double
            Get
                Return _baseQtyReceipt
            End Get
            Set(ByVal value As Double)
                _baseQtyReceipt = value
            End Set
        End Property


        <ColumnInfo("BaseQtyReturn", "#,##0")> _
        Public Property BaseQtyReturn As Double
            Get
                Return _baseQtyReturn
            End Get
            Set(ByVal value As Double)
                _baseQtyReturn = value
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


        <ColumnInfo("ProductSubstitute", "'{0}'")> _
        Public Property ProductSubstitute As String
            Get
                Return _productSubstitute
            End Get
            Set(ByVal value As String)
                _productSubstitute = value
            End Set
        End Property


        <ColumnInfo("ProductVariant", "'{0}'")> _
        Public Property ProductVariant As String
            Get
                Return _productVariant
            End Get
            Set(ByVal value As String)
                _productVariant = value
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


        <ColumnInfo("PromisedDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PromisedDate As DateTime
            Get
                Return _promisedDate
            End Get
            Set(ByVal value As DateTime)
                _promisedDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PurchaseFor", "{0}")> _
        Public Property PurchaseFor As Short
            Get
                Return _purchaseFor
            End Get
            Set(ByVal value As Short)
                _purchaseFor = value
            End Set
        End Property


        <ColumnInfo("PurchaseOrderNo", "'{0}'")> _
        Public Property PurchaseOrderNo As String
            Get
                Return _purchaseOrderNo
            End Get
            Set(ByVal value As String)
                _purchaseOrderNo = value
            End Set
        End Property


        <ColumnInfo("PurchaseRequisitionDetail", "'{0}'")> _
        Public Property PurchaseRequisitionDetail As String
            Get
                Return _purchaseRequisitionDetail
            End Get
            Set(ByVal value As String)
                _purchaseRequisitionDetail = value
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


        <ColumnInfo("QtyOrder", "#,##0")> _
        Public Property QtyOrder As Double
            Get
                Return _qtyOrder
            End Get
            Set(ByVal value As Double)
                _qtyOrder = value
            End Set
        End Property


        <ColumnInfo("QtyReceipt", "#,##0")> _
        Public Property QtyReceipt As Double
            Get
                Return _qtyReceipt
            End Get
            Set(ByVal value As Double)
                _qtyReceipt = value
            End Set
        End Property


        <ColumnInfo("QtyReturn", "#,##0")> _
        Public Property QtyReturn As Double
            Get
                Return _qtyReturn
            End Get
            Set(ByVal value As Double)
                _qtyReturn = value
            End Set
        End Property


        <ColumnInfo("RecallProduct", "{0}")> _
        Public Property RecallProduct As Boolean
            Get
                Return _recallProduct
            End Get
            Set(ByVal value As Boolean)
                _recallProduct = value
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


        <ColumnInfo("RequiredDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RequiredDate As DateTime
            Get
                Return _requiredDate
            End Get
            Set(ByVal value As DateTime)
                _requiredDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SalesOrderDetail", "'{0}'")> _
        Public Property SalesOrderDetail As String
            Get
                Return _salesOrderDetail
            End Get
            Set(ByVal value As String)
                _salesOrderDetail = value
            End Set
        End Property


        <ColumnInfo("ScheduledShippingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ScheduledShippingDate As DateTime
            Get
                Return _scheduledShippingDate
            End Get
            Set(ByVal value As DateTime)
                _scheduledShippingDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("ShippingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ShippingDate As DateTime
            Get
                Return _shippingDate
            End Get
            Set(ByVal value As DateTime)
                _shippingDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("TotalAmountBeforeDiscount", "{0}")> _
        Public Property TotalAmountBeforeDiscount As Decimal
            Get
                Return _totalAmountBeforeDiscount
            End Get
            Set(ByVal value As Decimal)
                _totalAmountBeforeDiscount = value
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


        <ColumnInfo("POOtherVendorID", "{0}"), _
        RelationInfo("POOtherVendor", "ID", "POOtherVendorDetail", "POOtherVendorID")> _
        Public Property POOtherVendor As POOtherVendor
            Get
                Try
                    If Not isnothing(Me._pOOtherVendor) AndAlso (Not Me._pOOtherVendor.IsLoaded) Then

                        Me._pOOtherVendor = CType(DoLoad(GetType(POOtherVendor).ToString(), _pOOtherVendor.ID), POOtherVendor)
                        Me._pOOtherVendor.MarkLoaded()

                    End If

                    Return Me._pOOtherVendor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As POOtherVendor)

                Me._pOOtherVendor = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pOOtherVendor.MarkLoaded()
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

