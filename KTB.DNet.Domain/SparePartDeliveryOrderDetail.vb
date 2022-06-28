
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDeliveryOrderDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/03/2018 - 13:23:55
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
    <Serializable(), TableInfo("SparePartDeliveryOrderDetail")> _
    Public Class SparePartDeliveryOrderDetail
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
        Private _amountBeforeDiscount As Decimal
        Private _baseAmount As Decimal
        Private _baseQtyDelivered As Double
        Private _baseQtyOrder As Double
        Private _batchNo As String = String.Empty
        Private _bU As String = String.Empty
        Private _consumptionTax1Amount As Decimal
        Private _consumptionTax1 As String = String.Empty
        Private _consumptionTax2Amount As Decimal
        Private _consumptionTax2 As String = String.Empty
        Private _deliveryOrderDetail As String = String.Empty
        Private _deliveryOrderNo As String = String.Empty
        Private _discountAmount As Decimal
        Private _discountBaseAmount As Decimal
        Private _discountPercentage As Double
        Private _location As String = String.Empty
        Private _productCrossReference As String = String.Empty
        Private _productDescription As String = String.Empty
        Private _product As String = String.Empty
        Private _promiseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _qtyDelivered As Double
        Private _qtyOrder As Double
        Private _requestDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _runningNumber As Integer
        Private _salesOrderDetail As String = String.Empty
        Private _salesUnit As String = String.Empty
        Private _site As String = String.Empty
        Private _totalAmount As Decimal
        Private _totalConsumptionTaxAmount As Decimal
        Private _transactionAmount As Decimal
        Private _unitPrice As Decimal
        Private _warehouse As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartDeliveryOrder As SparePartDeliveryOrder



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


        <ColumnInfo("AmountBeforeDiscount", "{0}")> _
        Public Property AmountBeforeDiscount As Decimal
            Get
                Return _amountBeforeDiscount
            End Get
            Set(ByVal value As Decimal)
                _amountBeforeDiscount = value
            End Set
        End Property


        <ColumnInfo("BaseAmount", "{0}")> _
        Public Property BaseAmount As Decimal
            Get
                Return _baseAmount
            End Get
            Set(ByVal value As Decimal)
                _baseAmount = value
            End Set
        End Property


        <ColumnInfo("BaseQtyDelivered", "#,##0")> _
        Public Property BaseQtyDelivered As Double
            Get
                Return _baseQtyDelivered
            End Get
            Set(ByVal value As Double)
                _baseQtyDelivered = value
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


        <ColumnInfo("DeliveryOrderDetail", "'{0}'")> _
        Public Property DeliveryOrderDetail As String
            Get
                Return _deliveryOrderDetail
            End Get
            Set(ByVal value As String)
                _deliveryOrderDetail = value
            End Set
        End Property


        <ColumnInfo("DeliveryOrderNo", "'{0}'")> _
        Public Property DeliveryOrderNo As String
            Get
                Return _deliveryOrderNo
            End Get
            Set(ByVal value As String)
                _deliveryOrderNo = value
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


        <ColumnInfo("DiscountBaseAmount", "{0}")> _
        Public Property DiscountBaseAmount As Decimal
            Get
                Return _discountBaseAmount
            End Get
            Set(ByVal value As Decimal)
                _discountBaseAmount = value
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


        <ColumnInfo("PromiseDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PromiseDate As DateTime
            Get
                Return _promiseDate
            End Get
            Set(ByVal value As DateTime)
                _promiseDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("QtyDelivered", "#,##0")> _
        Public Property QtyDelivered As Double
            Get
                Return _qtyDelivered
            End Get
            Set(ByVal value As Double)
                _qtyDelivered = value
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


        <ColumnInfo("RequestDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RequestDate As DateTime
            Get
                Return _requestDate
            End Get
            Set(ByVal value As DateTime)
                _requestDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("SalesOrderDetail", "'{0}'")> _
        Public Property SalesOrderDetail As String
            Get
                Return _salesOrderDetail
            End Get
            Set(ByVal value As String)
                _salesOrderDetail = value
            End Set
        End Property


        <ColumnInfo("SalesUnit", "'{0}'")> _
        Public Property SalesUnit As String
            Get
                Return _salesUnit
            End Get
            Set(ByVal value As String)
                _salesUnit = value
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


        <ColumnInfo("TotalAmount", "{0}")> _
        Public Property TotalAmount As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
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


        <ColumnInfo("TransactionAmount", "{0}")> _
        Public Property TransactionAmount As Decimal
            Get
                Return _transactionAmount
            End Get
            Set(ByVal value As Decimal)
                _transactionAmount = value
            End Set
        End Property


        <ColumnInfo("UnitPrice", "{0}")> _
        Public Property UnitPrice As Decimal
            Get
                Return _unitPrice
            End Get
            Set(ByVal value As Decimal)
                _unitPrice = value
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


        <ColumnInfo("SparePartDeliveryOrderID", "{0}"), _
        RelationInfo("SparePartDeliveryOrder", "ID", "SparePartDeliveryOrderDetail", "SparePartDeliveryOrderID")> _
        Public Property SparePartDeliveryOrder As SparePartDeliveryOrder
            Get
                Try
                    If Not isnothing(Me._sparePartDeliveryOrder) AndAlso (Not Me._sparePartDeliveryOrder.IsLoaded) Then

                        Me._sparePartDeliveryOrder = CType(DoLoad(GetType(SparePartDeliveryOrder).ToString(), _sparePartDeliveryOrder.ID), SparePartDeliveryOrder)
                        Me._sparePartDeliveryOrder.MarkLoaded()

                    End If

                    Return Me._sparePartDeliveryOrder

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartDeliveryOrder)

                Me._sparePartDeliveryOrder = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartDeliveryOrder.MarkLoaded()
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

