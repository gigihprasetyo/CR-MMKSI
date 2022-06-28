
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : APPayment Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/03/2018 - 10:35:09
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
    <Serializable(), TableInfo("APPayment")> _
    Public Class APPayment
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
        Private _aPPaymentNo As String = String.Empty
        Private _aPReferenceNo As String = String.Empty
        Private _aPVoucherReferenceNo As String = String.Empty
        Private _appliedToDocument As Decimal
        Private _bU As String = String.Empty
        Private _cancelled As Boolean
        Private _cashAndBank As String = String.Empty
        Private _methodOfPayment As String = String.Empty
        Private _availableBalance As Decimal
        Private _state As Short
        Private _totalChangeAmount As Decimal
        Private _totalPaymentAmount As Decimal
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _type As Short
        Private _vendorDescription As String = String.Empty
        Private _vendor As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _aPPaymentDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("APPaymentNo", "'{0}'")> _
        Public Property APPaymentNo As String
            Get
                Return _aPPaymentNo
            End Get
            Set(ByVal value As String)
                _aPPaymentNo = value
            End Set
        End Property


        <ColumnInfo("APReferenceNo", "'{0}'")> _
        Public Property APReferenceNo As String
            Get
                Return _aPReferenceNo
            End Get
            Set(ByVal value As String)
                _aPReferenceNo = value
            End Set
        End Property


        <ColumnInfo("APVoucherReferenceNo", "'{0}'")> _
        Public Property APVoucherReferenceNo As String
            Get
                Return _aPVoucherReferenceNo
            End Get
            Set(ByVal value As String)
                _aPVoucherReferenceNo = value
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


        <ColumnInfo("State", "{0}")> _
        Public Property State As Short
            Get
                Return _state
            End Get
            Set(ByVal value As Short)
                _state = value
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


        <ColumnInfo("TotalPaymentAmount", "{0}")> _
        Public Property TotalPaymentAmount As Decimal
            Get
                Return _totalPaymentAmount
            End Get
            Set(ByVal value As Decimal)
                _totalPaymentAmount = value
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


        <ColumnInfo("Type", "{0}")> _
        Public Property Type As Short
            Get
                Return _type
            End Get
            Set(ByVal value As Short)
                _type = value
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



        <RelationInfo("APPayment", "ID", "APPaymentDetail", "APPaymentID")> _
        Public ReadOnly Property APPaymentDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._aPPaymentDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(APPaymentDetail), "APPayment", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(APPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._aPPaymentDetails = DoLoadArray(GetType(APPaymentDetail).ToString, criterias)
                    End If

                    Return Me._aPPaymentDetails

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

