
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDeliveryOrder Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 23/03/2018 - 17:43:23
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
    <Serializable(), TableInfo("SparePartDeliveryOrder")> _
    Public Class SparePartDeliveryOrder
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
        Private _address1 As String = String.Empty
        Private _address2 As String = String.Empty
        Private _address3 As String = String.Empty
        Private _address4 As String = String.Empty
        Private _businessPhone As String = String.Empty
        Private _bU As String = String.Empty
        Private _cancellationDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _city As String = String.Empty
        Private _customerContacts As String = String.Empty
        Private _customer As String = String.Empty
        Private _customerNo As String = String.Empty
        Private _deliveryAddress As String = String.Empty
        Private _deliveryOrderNo As String = String.Empty
        Private _deliveryType As Integer
        Private _externalReferenceNo As String = String.Empty
        Private _grandTotal As Decimal
        Private _status As Short
        Private _methodofPayment As String = String.Empty
        Private _orderType As String = String.Empty
        Private _referenceNo As String = String.Empty
        Private _salesperson As String = String.Empty
        Private _state As Short
        Private _termofPayment As String = String.Empty
        Private _totalAmountBeforeDiscount As Decimal
        Private _totalBaseAmount As Decimal
        Private _totalDiscountAmount As Decimal
        Private _totalMiscChargeBaseAmount As Decimal
        Private _totalMiscChargeConsumptionTaxAmount As Decimal
        Private _totalReceipt As Decimal
        Private _totalConsumptionTaxAmount As Decimal
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

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


        <ColumnInfo("Address1", "'{0}'")> _
        Public Property Address1 As String
            Get
                Return _address1
            End Get
            Set(ByVal value As String)
                _address1 = value
            End Set
        End Property


        <ColumnInfo("Address2", "'{0}'")> _
        Public Property Address2 As String
            Get
                Return _address2
            End Get
            Set(ByVal value As String)
                _address2 = value
            End Set
        End Property


        <ColumnInfo("Address3", "'{0}'")> _
        Public Property Address3 As String
            Get
                Return _address3
            End Get
            Set(ByVal value As String)
                _address3 = value
            End Set
        End Property


        <ColumnInfo("Address4", "'{0}'")> _
        Public Property Address4 As String
            Get
                Return _address4
            End Get
            Set(ByVal value As String)
                _address4 = value
            End Set
        End Property


        <ColumnInfo("BusinessPhone", "'{0}'")> _
        Public Property BusinessPhone As String
            Get
                Return _businessPhone
            End Get
            Set(ByVal value As String)
                _businessPhone = value
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


        <ColumnInfo("CancellationDate", "'{0:yyyy/MM/dd}'")> _
        Public Property CancellationDate As DateTime
            Get
                Return _cancellationDate
            End Get
            Set(ByVal value As DateTime)
                _cancellationDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("City", "'{0}'")> _
        Public Property City As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property


        <ColumnInfo("CustomerContacts", "'{0}'")> _
        Public Property CustomerContacts As String
            Get
                Return _customerContacts
            End Get
            Set(ByVal value As String)
                _customerContacts = value
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


        <ColumnInfo("DeliveryAddress", "'{0}'")> _
        Public Property DeliveryAddress As String
            Get
                Return _deliveryAddress
            End Get
            Set(ByVal value As String)
                _deliveryAddress = value
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


        <ColumnInfo("DeliveryType", "{0}")> _
        Public Property DeliveryType As Integer
            Get
                Return _deliveryType
            End Get
            Set(ByVal value As Integer)
                _deliveryType = value
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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("MethodofPayment", "'{0}'")> _
        Public Property MethodofPayment As String
            Get
                Return _methodofPayment
            End Get
            Set(ByVal value As String)
                _methodofPayment = value
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


        <ColumnInfo("ReferenceNo", "'{0}'")> _
        Public Property ReferenceNo As String
            Get
                Return _referenceNo
            End Get
            Set(ByVal value As String)
                _referenceNo = value
            End Set
        End Property


        <ColumnInfo("Salesperson", "'{0}'")> _
        Public Property Salesperson As String
            Get
                Return _salesperson
            End Get
            Set(ByVal value As String)
                _salesperson = value
            End Set
        End Property


        <ColumnInfo("State", "{0}")> _
        Public Property State As Short
            Get
                Return _state
            End Get
            Set(ByVal value As Short)
                _state = value
            End Set
        End Property


        <ColumnInfo("TermofPayment", "'{0}'")> _
        Public Property TermofPayment As String
            Get
                Return _termofPayment
            End Get
            Set(ByVal value As String)
                _termofPayment = value
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


        <ColumnInfo("TotalDiscountAmount", "{0}")> _
        Public Property TotalDiscountAmount As Decimal
            Get
                Return _totalDiscountAmount
            End Get
            Set(ByVal value As Decimal)
                _totalDiscountAmount = value
            End Set
        End Property


        <ColumnInfo("TotalMiscChargeBaseAmount", "{0}")> _
        Public Property TotalMiscChargeBaseAmount As Decimal
            Get
                Return _totalMiscChargeBaseAmount
            End Get
            Set(ByVal value As Decimal)
                _totalMiscChargeBaseAmount = value
            End Set
        End Property


        <ColumnInfo("TotalMiscChargeConsumptionTaxAmount", "{0}")> _
        Public Property TotalMiscChargeConsumptionTaxAmount As Decimal
            Get
                Return _totalMiscChargeConsumptionTaxAmount
            End Get
            Set(ByVal value As Decimal)
                _totalMiscChargeConsumptionTaxAmount = value
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


        <ColumnInfo("TotalConsumptionTaxAmount", "{0}")> _
        Public Property TotalConsumptionTaxAmount As Decimal
            Get
                Return _totalConsumptionTaxAmount
            End Get
            Set(ByVal value As Decimal)
                _totalConsumptionTaxAmount = value
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

        Private _sparePartDeliveryOrderDetail = New ArrayList

        <RelationInfo("SparePartDeliveryOrder", "ID", "SparePartDeliveryOrderDetail", "SparePartDeliveryOrderID")> _
        Public Property SparePartDeliveryOrderDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartDeliveryOrderDetail.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartDeliveryOrderDetail), "SparePartDeliveryOrder", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartDeliveryOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartDeliveryOrderDetail = DoLoadArray(GetType(SparePartDeliveryOrderDetail).ToString, criterias)
                    End If

                    Return Me._sparePartDeliveryOrderDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get

            Set(ByVal Value As System.Collections.ArrayList)
                Me._sparePartDeliveryOrderDetail = Value
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

