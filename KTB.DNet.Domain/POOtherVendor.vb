
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : POOtherVendor Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 16:11:23
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
    <Serializable(), TableInfo("POOtherVendor")> _
    Public Class POOtherVendor
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
        Private _allocationPeriod As String = String.Empty
        Private _balance As Decimal
        Private _dealerCode As String = String.Empty
        Private _city As String = String.Empty
        Private _closeRespon As String = String.Empty
        Private _country As String = String.Empty
        Private _deliveryMethod As Short
        Private _description As String = String.Empty
        Private _downPayment As Decimal
        Private _downPaymentAmountPaid As Decimal
        Private _downPaymentIsPaid As Boolean
        Private _eventDate As String = String.Empty
        Private _externalDocNo As String = String.Empty
        Private _formSource As Short
        Private _grandTotal As Decimal
        Private _paymentGroup As Short
        Private _personInCharge As String = String.Empty
        Private _postalCode As String = String.Empty
        Private _priority As Short
        Private _province As String = String.Empty
        Private _pRPOType As String = String.Empty
        Private _purchaseOrderNo As String = String.Empty
        Private _sONo As String = String.Empty
        Private _site As String = String.Empty
        Private _state As Short
        Private _stockReferenceNo As String = String.Empty
        Private _taxable As Short
        Private _termsOfPayment As String = String.Empty
        Private _totalAmountBeforeDiscount As Decimal
        Private _totalBaseAmount As Decimal
        Private _totalConsumptionTaxAmount As Decimal
        Private _totalDiscountAmount As Decimal
        Private _totalTitleRegistrationFee As Decimal
        Private _purchaseOrderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _vendorDescription As String = String.Empty
        Private _vendor As String = String.Empty
        Private _warehouse As String = String.Empty
        Private _wONo As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _pOOtherVendorDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("AllocationPeriod", "'{0}'")> _
        Public Property AllocationPeriod As String
            Get
                Return _allocationPeriod
            End Get
            Set(ByVal value As String)
                _allocationPeriod = value
            End Set
        End Property


        <ColumnInfo("Balance", "{0}")> _
        Public Property Balance As Decimal
            Get
                Return _balance
            End Get
            Set(ByVal value As Decimal)
                _balance = value
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


        <ColumnInfo("City", "'{0}'")> _
        Public Property City As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property


        <ColumnInfo("CloseRespon", "'{0}'")> _
        Public Property CloseRespon As String
            Get
                Return _closeRespon
            End Get
            Set(ByVal value As String)
                _closeRespon = value
            End Set
        End Property


        <ColumnInfo("Country", "'{0}'")> _
        Public Property Country As String
            Get
                Return _country
            End Get
            Set(ByVal value As String)
                _country = value
            End Set
        End Property


        <ColumnInfo("DeliveryMethod", "{0}")> _
        Public Property DeliveryMethod As Short
            Get
                Return _deliveryMethod
            End Get
            Set(ByVal value As Short)
                _deliveryMethod = value
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


        <ColumnInfo("DownPayment", "{0}")> _
        Public Property DownPayment As Decimal
            Get
                Return _downPayment
            End Get
            Set(ByVal value As Decimal)
                _downPayment = value
            End Set
        End Property


        <ColumnInfo("DownPaymentAmountPaid", "{0}")> _
        Public Property DownPaymentAmountPaid As Decimal
            Get
                Return _downPaymentAmountPaid
            End Get
            Set(ByVal value As Decimal)
                _downPaymentAmountPaid = value
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


        <ColumnInfo("EventDate", "'{0}'")> _
        Public Property EventDate As String
            Get
                Return _eventDate
            End Get
            Set(ByVal value As String)
                _eventDate = value
            End Set
        End Property


        <ColumnInfo("ExternalDocNo", "'{0}'")> _
        Public Property ExternalDocNo As String
            Get
                Return _externalDocNo
            End Get
            Set(ByVal value As String)
                _externalDocNo = value
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


        <ColumnInfo("GrandTotal", "{0}")> _
        Public Property GrandTotal As Decimal
            Get
                Return _grandTotal
            End Get
            Set(ByVal value As Decimal)
                _grandTotal = value
            End Set
        End Property


        <ColumnInfo("PaymentGroup", "{0}")> _
        Public Property PaymentGroup As Short
            Get
                Return _paymentGroup
            End Get
            Set(ByVal value As Short)
                _paymentGroup = value
            End Set
        End Property


        <ColumnInfo("PersonInCharge", "'{0}'")> _
        Public Property PersonInCharge As String
            Get
                Return _personInCharge
            End Get
            Set(ByVal value As String)
                _personInCharge = value
            End Set
        End Property


        <ColumnInfo("PostalCode", "'{0}'")> _
        Public Property PostalCode As String
            Get
                Return _postalCode
            End Get
            Set(ByVal value As String)
                _postalCode = value
            End Set
        End Property


        <ColumnInfo("Priority", "{0}")> _
        Public Property Priority As Short
            Get
                Return _priority
            End Get
            Set(ByVal value As Short)
                _priority = value
            End Set
        End Property


        <ColumnInfo("Province", "'{0}'")> _
        Public Property Province As String
            Get
                Return _province
            End Get
            Set(ByVal value As String)
                _province = value
            End Set
        End Property


        <ColumnInfo("PRPOType", "'{0}'")> _
        Public Property PRPOType As String
            Get
                Return _pRPOType
            End Get
            Set(ByVal value As String)
                _pRPOType = value
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


        <ColumnInfo("SONo", "'{0}'")> _
        Public Property SONo As String
            Get
                Return _sONo
            End Get
            Set(ByVal value As String)
                _sONo = value
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


        <ColumnInfo("State", "{0}")> _
        Public Property State As Short
            Get
                Return _state
            End Get
            Set(ByVal value As Short)
                _state = value
            End Set
        End Property


        <ColumnInfo("StockReferenceNo", "'{0}'")> _
        Public Property StockReferenceNo As String
            Get
                Return _stockReferenceNo
            End Get
            Set(ByVal value As String)
                _stockReferenceNo = value
            End Set
        End Property


        <ColumnInfo("Taxable", "{0}")> _
        Public Property Taxable As Short
            Get
                Return _taxable
            End Get
            Set(ByVal value As Short)
                _taxable = value
            End Set
        End Property


        <ColumnInfo("TermsOfPayment", "'{0}'")> _
        Public Property TermsOfPayment As String
            Get
                Return _termsOfPayment
            End Get
            Set(ByVal value As String)
                _termsOfPayment = value
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


        <ColumnInfo("TotalTitleRegistrationFee", "{0}")> _
        Public Property TotalTitleRegistrationFee As Decimal
            Get
                Return _totalTitleRegistrationFee
            End Get
            Set(ByVal value As Decimal)
                _totalTitleRegistrationFee = value
            End Set
        End Property


        <ColumnInfo("PurchaseOrderDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PurchaseOrderDate As DateTime
            Get
                Return _purchaseOrderDate
            End Get
            Set(ByVal value As DateTime)
                _purchaseOrderDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("VendorDescription", "'{0}'")> _
        Public Property VendorDescription As String
            Get
                Return _vendorDescription
            End Get
            Set(ByVal value As String)
                _vendorDescription = value
            End Set
        End Property


        <ColumnInfo("Vendor", "'{0}'")> _
        Public Property Vendor As String
            Get
                Return _vendor
            End Get
            Set(ByVal value As String)
                _vendor = value
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


        <ColumnInfo("WONo", "'{0}'")> _
        Public Property WONo As String
            Get
                Return _wONo
            End Get
            Set(ByVal value As String)
                _wONo = value
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



        <RelationInfo("POOtherVendor", "ID", "POOtherVendorDetail", "POOtherVendorID")> _
        Public ReadOnly Property POOtherVendorDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._pOOtherVendorDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(POOtherVendorDetail), "POOtherVendor", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(POOtherVendorDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pOOtherVendorDetails = DoLoadArray(GetType(POOtherVendorDetail).ToString, criterias)
                    End If

                    Return Me._pOOtherVendorDetails

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

