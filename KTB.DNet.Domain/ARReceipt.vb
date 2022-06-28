
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : ARReceipt Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 23/03/2018 - 16:15:04
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
    <Serializable(), TableInfo("ARReceipt")> _
    Public Class ARReceipt
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
        Private _generatedToken As String = String.Empty
        Private _aRInvoiceReferenceNo As String = String.Empty
        Private _aRReceiptNo As String = String.Empty
        Private _aRReceiptReferenceNo As String = String.Empty
        Private _type As Short
        Private _bookingFee As Boolean
        Private _bU As String = String.Empty
        Private _cancelled As Boolean
        Private _cashAndBank As String = String.Empty
        Private _customer As String = String.Empty
        Private _customerNo As String = String.Empty
        Private _endOrderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _methodOfPayment As String = String.Empty
        Private _availableBalance As Decimal
        Private _startOrderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _state As Short
        Private _appliedToDocument As Decimal
        Private _totalAmountBase As Decimal
        Private _totalChangeAmount As Decimal
        Private _totalOutstandingBalanceBase As Decimal
        Private _totalReceiptAmount As Decimal
        Private _totalRemainingBalanceBase As Decimal
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _aRReceiptDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("GeneratedToken", "'{0}'")> _
        Public Property GeneratedToken As String
            Get
                Return _generatedToken
            End Get
            Set(ByVal value As String)
                _generatedToken = value
            End Set
        End Property


        <ColumnInfo("ARInvoiceReferenceNo", "'{0}'")> _
        Public Property ARInvoiceReferenceNo As String
            Get
                Return _aRInvoiceReferenceNo
            End Get
            Set(ByVal value As String)
                _aRInvoiceReferenceNo = value
            End Set
        End Property


        <ColumnInfo("ARReceiptNo", "'{0}'")> _
        Public Property ARReceiptNo As String
            Get
                Return _aRReceiptNo
            End Get
            Set(ByVal value As String)
                _aRReceiptNo = value
            End Set
        End Property


        <ColumnInfo("ARReceiptReferenceNo", "'{0}'")> _
        Public Property ARReceiptReferenceNo As String
            Get
                Return _aRReceiptReferenceNo
            End Get
            Set(ByVal value As String)
                _aRReceiptReferenceNo = value
            End Set
        End Property


        <ColumnInfo("Type", "{0}")> _
        Public Property Type As Short
            Get
                Return _type
            End Get
            Set(ByVal value As Short)
                _type = value
            End Set
        End Property


        <ColumnInfo("BookingFee", "{0}")> _
        Public Property BookingFee As Boolean
            Get
                Return _bookingFee
            End Get
            Set(ByVal value As Boolean)
                _bookingFee = value
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


        <ColumnInfo("Cancelled", "{0}")> _
        Public Property Cancelled As Boolean
            Get
                Return _cancelled
            End Get
            Set(ByVal value As Boolean)
                _cancelled = value
            End Set
        End Property


        <ColumnInfo("CashAndBank", "'{0}'")> _
        Public Property CashAndBank As String
            Get
                Return _cashAndBank
            End Get
            Set(ByVal value As String)
                _cashAndBank = value
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


        <ColumnInfo("EndOrderDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EndOrderDate As DateTime
            Get
                Return _endOrderDate
            End Get
            Set(ByVal value As DateTime)
                _endOrderDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("AvailableBalance", "{0}")> _
        Public Property AvailableBalance As Decimal
            Get
                Return _availableBalance
            End Get
            Set(ByVal value As Decimal)
                _availableBalance = value
            End Set
        End Property


        <ColumnInfo("StartOrderDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartOrderDate As DateTime
            Get
                Return _startOrderDate
            End Get
            Set(ByVal value As DateTime)
                _startOrderDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("AppliedToDocument", "{0}")> _
        Public Property AppliedToDocument As Decimal
            Get
                Return _appliedToDocument
            End Get
            Set(ByVal value As Decimal)
                _appliedToDocument = value
            End Set
        End Property


        <ColumnInfo("TotalAmountBase", "{0}")> _
        Public Property TotalAmountBase As Decimal
            Get
                Return _totalAmountBase
            End Get
            Set(ByVal value As Decimal)
                _totalAmountBase = value
            End Set
        End Property


        <ColumnInfo("TotalChangeAmount", "{0}")> _
        Public Property TotalChangeAmount As Decimal
            Get
                Return _totalChangeAmount
            End Get
            Set(ByVal value As Decimal)
                _totalChangeAmount = value
            End Set
        End Property


        <ColumnInfo("TotalOutstandingBalanceBase", "{0}")> _
        Public Property TotalOutstandingBalanceBase As Decimal
            Get
                Return _totalOutstandingBalanceBase
            End Get
            Set(ByVal value As Decimal)
                _totalOutstandingBalanceBase = value
            End Set
        End Property


        <ColumnInfo("TotalReceiptAmount", "{0}")> _
        Public Property TotalReceiptAmount As Decimal
            Get
                Return _totalReceiptAmount
            End Get
            Set(ByVal value As Decimal)
                _totalReceiptAmount = value
            End Set
        End Property


        <ColumnInfo("TotalRemainingBalanceBase", "{0}")> _
        Public Property TotalRemainingBalanceBase As Decimal
            Get
                Return _totalRemainingBalanceBase
            End Get
            Set(ByVal value As Decimal)
                _totalRemainingBalanceBase = value
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



        <RelationInfo("ARReceipt", "ID", "ARReceiptDetail", "ARReceiptID")> _
        Public ReadOnly Property ARReceiptDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._aRReceiptDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ARReceiptDetail), "ARReceipt", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ARReceiptDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._aRReceiptDetails = DoLoadArray(GetType(ARReceiptDetail).ToString, criterias)
                    End If

                    Return Me._aRReceiptDetails

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

