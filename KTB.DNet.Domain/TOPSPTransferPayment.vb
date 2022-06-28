
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPTransferPayment Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/7/2018 - 10:49:24 AM
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
    <Serializable(), TableInfo("TOPSPTransferPayment")> _
    Public Class TOPSPTransferPayment
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
        Private _regNumber As String = String.Empty
        Private _dueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _transferPlanDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _tOPSPTransferPaymentIDReff As Integer
        Private _isAccelerated As Short
        Private _status As Short
        Private _validatedBy As String = String.Empty
        Private _validatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _confirmedBy As String = String.Empty
        Private _confirmedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _canceledBy As String = String.Empty
        Private _canceledTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _transferAmount As Decimal
        Private _transferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _creditAccount As String = String.Empty
        Private _transferActualDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _paymentPurposeID As Integer
        Private _bankID As Integer
        Private _calPlanTransferDateStart As DateTime
        Private _calPlanTransferDateEnd As DateTime
        Private _noRegDipercepat As String = ""
        Private _noRegPemercepat As String = ""
        Private _noBilling As String = ""
        Private _noKodeBilling As String = ""
        Private _calDueDateStart As DateTime
        Private _calDueDateEnd As DateTime
        Private _ddlStatusIndex As Integer
        Private _kodeDealer As String = ""
        Private _dealer As Dealer
        Private _paymentPurpose As PaymentPurpose
        Private _bank As Bank
        Private _tOPSPTransferPaymentDetails As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _tglBilling As Boolean
        Private _tglTransfer As Boolean
        Private _kodeDealerBilling As String
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

        <ColumnInfo("PaymentPurposeID", "{0}")> _
        Public Property PaymentPurposeID As Integer
            Get
                Return _paymentPurposeID
            End Get
            Set(ByVal value As Integer)
                _paymentPurposeID = value
            End Set
        End Property


        <ColumnInfo("CreditAccount", "'{0}'")> _
        Public Property CreditAccount As String
            Get
                Return _creditAccount
            End Get
            Set(ByVal value As String)
                _creditAccount = value
            End Set
        End Property


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property


        <ColumnInfo("TransferActualDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransferActualDate As DateTime
            Get
                Return _transferActualDate
            End Get
            Set(ByVal value As DateTime)
                _transferActualDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DueDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DueDate As DateTime
            Get
                Return _dueDate
            End Get
            Set(ByVal value As DateTime)
                _dueDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("TransferPlanDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransferPlanDate As DateTime
            Get
                Return _transferPlanDate
            End Get
            Set(ByVal value As DateTime)
                _transferPlanDate = value
            End Set
        End Property

        <ColumnInfo("BankID", "{0}")> _
        Public Property BankID As Integer
            Get
                Return _bankID
            End Get
            Set(ByVal value As Integer)
                _bankID = value
            End Set
        End Property

        <ColumnInfo("TOPSPTransferPaymentIDReff", "{0}")> _
        Public Property TOPSPTransferPaymentIDReff As Integer
            Get
                Return _tOPSPTransferPaymentIDReff
            End Get
            Set(ByVal value As Integer)
                _tOPSPTransferPaymentIDReff = value
            End Set
        End Property


        <ColumnInfo("IsAccelerated", "{0}")> _
        Public Property IsAccelerated As Short
            Get
                Return _isAccelerated
            End Get
            Set(ByVal value As Short)
                _isAccelerated = value
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


        <ColumnInfo("ValidatedBy", "'{0}'")> _
        Public Property ValidatedBy As String
            Get
                Return _validatedBy
            End Get
            Set(ByVal value As String)
                _validatedBy = value
            End Set
        End Property


        <ColumnInfo("ValidatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidatedTime As DateTime
            Get
                Return _validatedTime
            End Get
            Set(ByVal value As DateTime)
                _validatedTime = value
            End Set
        End Property


        <ColumnInfo("ConfirmedBy", "'{0}'")> _
        Public Property ConfirmedBy As String
            Get
                Return _confirmedBy
            End Get
            Set(ByVal value As String)
                _confirmedBy = value
            End Set
        End Property


        <ColumnInfo("ConfirmedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ConfirmedTime As DateTime
            Get
                Return _confirmedTime
            End Get
            Set(ByVal value As DateTime)
                _confirmedTime = value
            End Set
        End Property


        <ColumnInfo("CanceledBy", "'{0}'")> _
        Public Property CanceledBy As String
            Get
                Return _canceledBy
            End Get
            Set(ByVal value As String)
                _canceledBy = value
            End Set
        End Property


        <ColumnInfo("CanceledTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CanceledTime As DateTime
            Get
                Return _canceledTime
            End Get
            Set(ByVal value As DateTime)
                _canceledTime = value
            End Set
        End Property


        <ColumnInfo("TransferAmount", "{0}")> _
        Public Property TransferAmount As Decimal
            Get
                Return _transferAmount
            End Get
            Set(ByVal value As Decimal)
                _transferAmount = value
            End Set
        End Property


        <ColumnInfo("TransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransferDate As DateTime
            Get
                Return _transferDate
            End Get
            Set(ByVal value As DateTime)
                _transferDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "TOPSPTransferPayment", "DealerID")> _
        Public Property Dealer As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BankID", "{0}"), _
        RelationInfo("Bank", "ID", "TOPSPTransferPayment", "BankID")> _
        Public Property Bank As Bank
            Get
                Try
                    If Not IsNothing(Me._bank) AndAlso (Not Me._bank.IsLoaded) Then

                        Me._bank = CType(DoLoad(GetType(Bank).ToString(), _bank.ID), Bank)
                        Me._bank.MarkLoaded()

                    End If

                    Return Me._bank

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Bank)

                Me._bank = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PaymentPurposeID", "{0}"), _
        RelationInfo("PaymentPurpose", "ID", "TOPSPTransferPayment", "PaymentPurposeID")> _
        Public Property PaymentPurpose As PaymentPurpose
            Get
                Try
                    If Not IsNothing(Me._paymentPurpose) AndAlso (Not Me._paymentPurpose.IsLoaded) Then

                        Me._paymentPurpose = CType(DoLoad(GetType(PaymentPurpose).ToString(), _paymentPurpose.ID), PaymentPurpose)
                        Me._paymentPurpose.MarkLoaded()

                    End If

                    Return Me._paymentPurpose

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PaymentPurpose)

                Me._paymentPurpose = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._paymentPurpose.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("TOPSPTransferPayment", "ID", "TOPSPTransferPaymentDetail", "TOPSPTransferPaymentID")> _
        Public ReadOnly Property TOPSPTransferPaymentDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._tOPSPTransferPaymentDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._tOPSPTransferPaymentDetails = DoLoadArray(GetType(TOPSPTransferPaymentDetail).ToString, criterias)
                    End If

                    Return Me._tOPSPTransferPaymentDetails

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

        Public Property calPlanTransferDateStart As DateTime
            Get
                Return _calPlanTransferDateStart
            End Get
            Set(ByVal value As DateTime)
                _calPlanTransferDateStart = value
            End Set
        End Property

        Public Property CalDueDateStart As DateTime
            Get
                Return _calDueDateStart
            End Get
            Set(ByVal value As DateTime)
                _calDueDateStart = value
            End Set
        End Property

        Public Property CalDueDateEnd As DateTime
            Get
                Return _calDueDateEnd
            End Get
            Set(ByVal value As DateTime)
                _calDueDateEnd = value
            End Set
        End Property

        Public Property calPlanTransferDateEnd As DateTime
            Get
                Return _calPlanTransferDateEnd
            End Get
            Set(ByVal value As DateTime)
                _calPlanTransferDateEnd = value
            End Set
        End Property

        Public Property NoRegPemercepat As String
            Get
                Return _noRegPemercepat
            End Get
            Set(ByVal value As String)
                _noRegPemercepat = value
            End Set
        End Property

        Public Property NoRegDipercepat As String
            Get
                Return _noRegDipercepat
            End Get
            Set(ByVal value As String)
                _noRegDipercepat = value
            End Set
        End Property

        Public Property NoBilling As String
            Get
                Return _noBilling
            End Get
            Set(value As String)
                _noBilling = value
            End Set
        End Property

        Public Property NoKodeBilling As String
            Get
                Return _noKodeBilling
            End Get
            Set(value As String)
                _noKodeBilling = value
            End Set
        End Property

        Public Property DdlStatusIndex As Integer
            Get
                Return _ddlStatusIndex
            End Get
            Set(value As Integer)
                _ddlStatusIndex = value
            End Set
        End Property


        Public Property KodeDealer As String
            Get
                Return _kodeDealer
            End Get
            Set(value As String)
                _kodeDealer = value
            End Set
        End Property

        Public Property TglTransfer As Boolean
            Get
                Return _tglTransfer
            End Get
            Set(value As Boolean)
                _tglTransfer = value
            End Set
        End Property

        Public Property TglBilling As Boolean
            Get
                Return _tglBilling
            End Get
            Set(value As Boolean)
                _tglBilling = value
            End Set
        End Property

        Private _billingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Public Property BillingDate As DateTime
            Set(value As DateTime)
                _billingDate = value
            End Set
            Get
                Return _billingDate
            End Get
        End Property

        Private _billingAmount As Decimal
        Public Property BillingAmount As Decimal
            Set(value As Decimal)
                _billingAmount = value
            End Set
            Get
                Return _billingAmount
            End Get
        End Property

        Private _taxAmount As Decimal
        Public Property TaxAmount As Decimal
            Set(value As Decimal)
                _taxAmount = value
            End Set
            Get
                Return _taxAmount
            End Get
        End Property

        Private _c2Amount As Decimal
        Public Property C2Amount As Decimal
            Set(value As Decimal)
                _c2Amount = value
            End Set
            Get
                Return _c2Amount
            End Get
        End Property

        Private _totalAmount As Decimal
        Public Property TotalAmount As Decimal
            Set(value As Decimal)
                _totalAmount = value
            End Set
            Get
                Return _totalAmount
            End Get
        End Property

        Private _docReclass As String
        Public Property DocReclass As String
            Set(value As String)
                _docReclass = value
            End Set
            Get
                Return _docReclass
            End Get
        End Property

        Private _docClearing As String
        Public Property DocClearing As String
            Set(value As String)
                _docClearing = value
            End Set
            Get
                Return _docClearing
            End Get
        End Property

        Private _kliringDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Public Property KliringDate As DateTime
            Set(value As DateTime)
                _kliringDate = value
            End Set
            Get
                Return _kliringDate
            End Get
        End Property

        Private _totalKliring As Decimal
        Public Property TotalKliring As Decimal
            Set(value As Decimal)
                _totalKliring = value
            End Set
            Get
                Return _totalKliring
            End Get
        End Property

        Private _actualDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Public Property ActualDate As DateTime
            Set(value As DateTime)
                _actualDate = value
            End Set
            Get
                Return _actualDate
            End Get
        End Property

        Private _reffBank As String
        Public Property ReffBank As String
            Set(value As String)
                _reffBank = value
            End Set
            Get
                Return _reffBank
            End Get
        End Property

#End Region

    End Class
End Namespace

