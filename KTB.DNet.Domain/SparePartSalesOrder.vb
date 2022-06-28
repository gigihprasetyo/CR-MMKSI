
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartSalesOrder Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 16:18:41
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
    <Serializable(), TableInfo("SparePartSalesOrder")> _
    Public Class SparePartSalesOrder
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
        Private _salesChannel As Short
        Private _owner As String = String.Empty
        Private _status As Short
        Private _dealerCode As String = String.Empty
        Private _customer As String = String.Empty
        Private _customerNo As String = String.Empty
        Private _downPaymentAmount As Decimal
        Private _downPaymentAmountReceived As Decimal
        Private _downPaymentIsPaid As Boolean
        Private _externalReferenceNo As String = String.Empty
        Private _grandTotal As Decimal
        Private _handling As Short
        Private _methodOfPayment As String = String.Empty
        Private _orderType As String = String.Empty
        Private _salesOrderNo As String = String.Empty
        Private _salesPerson As String = String.Empty
        Private _shipmentType As String = String.Empty
        Private _state As String = String.Empty
        Private _termOfPayment As String = String.Empty
        Private _totalAmountBeforeDiscount As Decimal
        Private _totalBaseAmount As Decimal
        Private _totalConsumptionTaxAmount As Decimal
        Private _totalDiscountAmount As Decimal
        Private _totalReceipt As Decimal
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _sparePartSalesOrderDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("SalesChannel", "{0}")> _
        Public Property SalesChannel As Short
            Get
                Return _salesChannel
            End Get
            Set(ByVal value As Short)
                _salesChannel = value
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


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("Customer", "'{0}'")> _
        Public Property Customer As String
            Get
                Return _customer
            End Get
            Set(ByVal value As String)
                _customer = value
            End Set
        End Property


        <ColumnInfo("CustomerNo", "'{0}'")> _
        Public Property CustomerNo As String
            Get
                Return _customerNo
            End Get
            Set(ByVal value As String)
                _customerNo = value
            End Set
        End Property


        <ColumnInfo("DownPaymentAmount", "{0}")> _
        Public Property DownPaymentAmount As Decimal
            Get
                Return _downPaymentAmount
            End Get
            Set(ByVal value As Decimal)
                _downPaymentAmount = value
            End Set
        End Property


        <ColumnInfo("DownPaymentAmountReceived", "{0}")> _
        Public Property DownPaymentAmountReceived As Decimal
            Get
                Return _downPaymentAmountReceived
            End Get
            Set(ByVal value As Decimal)
                _downPaymentAmountReceived = value
            End Set
        End Property


        <ColumnInfo("DownPaymentIsPaid", "{0}")> _
        Public Property DownPaymentIsPaid As Boolean
            Get
                Return _downPaymentIsPaid
            End Get
            Set(ByVal value As Boolean)
                _downPaymentIsPaid = value
            End Set
        End Property


        <ColumnInfo("ExternalReferenceNo", "'{0}'")> _
        Public Property ExternalReferenceNo As String
            Get
                Return _externalReferenceNo
            End Get
            Set(ByVal value As String)
                _externalReferenceNo = value
            End Set
        End Property


        <ColumnInfo("GrandTotal", "{0}")> _
        Public Property GrandTotal As Decimal
            Get
                Return _grandTotal
            End Get
            Set(ByVal value As Decimal)
                _grandTotal = value
            End Set
        End Property


        <ColumnInfo("Handling", "{0}")> _
        Public Property Handling As Short
            Get
                Return _handling
            End Get
            Set(ByVal value As Short)
                _handling = value
            End Set
        End Property


        <ColumnInfo("MethodOfPayment", "'{0}'")> _
        Public Property MethodOfPayment As String
            Get
                Return _methodOfPayment
            End Get
            Set(ByVal value As String)
                _methodOfPayment = value
            End Set
        End Property


        <ColumnInfo("OrderType", "'{0}'")> _
        Public Property OrderType As String
            Get
                Return _orderType
            End Get
            Set(ByVal value As String)
                _orderType = value
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


        <ColumnInfo("SalesPerson", "'{0}'")> _
        Public Property SalesPerson As String
            Get
                Return _salesPerson
            End Get
            Set(ByVal value As String)
                _salesPerson = value
            End Set
        End Property


        <ColumnInfo("ShipmentType", "'{0}'")> _
        Public Property ShipmentType As String
            Get
                Return _shipmentType
            End Get
            Set(ByVal value As String)
                _shipmentType = value
            End Set
        End Property


        <ColumnInfo("State", "'{0}'")> _
        Public Property State As String
            Get
                Return _state
            End Get
            Set(ByVal value As String)
                _state = value
            End Set
        End Property


        <ColumnInfo("TermOfPayment", "'{0}'")> _
        Public Property TermOfPayment As String
            Get
                Return _termOfPayment
            End Get
            Set(ByVal value As String)
                _termOfPayment = value
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


        <ColumnInfo("TotalDiscountAmount", "{0}")> _
        Public Property TotalDiscountAmount As Decimal
            Get
                Return _totalDiscountAmount
            End Get
            Set(ByVal value As Decimal)
                _totalDiscountAmount = value
            End Set
        End Property


        <ColumnInfo("TotalReceipt", "{0}")> _
        Public Property TotalReceipt As Decimal
            Get
                Return _totalReceipt
            End Get
            Set(ByVal value As Decimal)
                _totalReceipt = value
            End Set
        End Property


        <ColumnInfo("TransactionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransactionDate As DateTime
            Get
                Return _transactionDate
            End Get
            Set(ByVal value As DateTime)
                _transactionDate = New DateTime(value.Year, value.Month, value.Day)
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



        <RelationInfo("SparePartSalesOrder", "ID", "SparePartSalesOrderDetail", "SparePartSalesOrderID")> _
        Public ReadOnly Property SparePartSalesOrderDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartSalesOrderDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartSalesOrderDetail), "SparePartSalesOrder", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartSalesOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartSalesOrderDetails = DoLoadArray(GetType(SparePartSalesOrderDetail).ToString, criterias)
                    End If

                    Return Me._sparePartSalesOrderDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
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

