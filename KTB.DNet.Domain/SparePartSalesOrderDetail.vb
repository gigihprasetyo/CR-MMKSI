
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartSalesOrderDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 16:19:15
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
    <Serializable(), TableInfo("SparePartSalesOrderDetail")> _
    Public Class SparePartSalesOrderDetail
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
        Private _status As Short
        Private _amountBeforeDiscount As Decimal
        Private _baseAmount As Decimal
        Private _kodeDealer As String = String.Empty
        Private _consumptionTax1Amount As Decimal
        Private _consumptionTax1 As String = String.Empty
        Private _consumptionTax2Amount As Decimal
        Private _consumptionTax2 As String = String.Empty
        Private _discountAmount As Decimal
        Private _discountPercentAge As Decimal
        Private _productCrossReference As String = String.Empty
        Private _productDescription As String = String.Empty
        Private _product As String = String.Empty
        Private _promiseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _qtyDelivered As Double
        Private _qtyOrder As Double
        Private _requestDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _salesOrderDetailID As String = String.Empty
        Private _salesOrderNo As String = String.Empty
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

        Private _sparePartSalesOrder As SparePartSalesOrder



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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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


        <ColumnInfo("KodeDealer", "'{0}'")> _
        Public Property KodeDealer As String
            Get
                Return _kodeDealer
            End Get
            Set(ByVal value As String)
                _kodeDealer = value
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


        <ColumnInfo("DiscountPercentAge", "#,##0")> _
        Public Property DiscountPercentAge As Decimal
            Get
                Return _discountPercentAge
            End Get
            Set(ByVal value As Decimal)
                _discountPercentAge = value
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


        <ColumnInfo("SalesOrderDetailID", "'{0}'")> _
        Public Property SalesOrderDetailID As String
            Get
                Return _salesOrderDetailID
            End Get
            Set(ByVal value As String)
                _salesOrderDetailID = value
            End Set
        End Property


        <ColumnInfo("SalesOrderNo", "'{0}'")> _
        Public Property SalesOrderNo As String
            Get
                Return _salesOrderNo
            End Get
            Set(ByVal value As String)
                _salesOrderNo = value
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


        <ColumnInfo("SparePartSalesOrderID", "{0}"), _
        RelationInfo("SparePartSalesOrder", "ID", "SparePartSalesOrderDetail", "SparePartSalesOrderID")> _
        Public Property SparePartSalesOrder As SparePartSalesOrder
            Get
                Try
                    If Not IsNothing(Me._sparePartSalesOrder) AndAlso (Not Me._sparePartSalesOrder.IsLoaded) Then

                        Me._sparePartSalesOrder = CType(DoLoad(GetType(SparePartSalesOrder).ToString(), _sparePartSalesOrder.ID), SparePartSalesOrder)
                        Me._sparePartSalesOrder.MarkLoaded()

                    End If

                    Return Me._sparePartSalesOrder

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartSalesOrder)

                Me._sparePartSalesOrder = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartSalesOrder.MarkLoaded()
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

