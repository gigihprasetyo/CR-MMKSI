
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TransferPayment Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 28/07/2016 - 11:05:27
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
    <Serializable(), TableInfo("TransferPayment")> _
    Public Class TransferPayment
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
        Private _dueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _planTransferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _regNumber As String = String.Empty
        Private _isNotOnTime As Short
        Private _status As Short
        Private _validatedBy As String = String.Empty
        Private _validatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _confirmedBy As String = String.Empty
        Private _confirmedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _totalActualAmount As Decimal
        Private _ActualTrfDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _TransferAmount As Decimal
        Private _TransferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _dealerID As Short
        'Private _paymentPurposeID As Byte

        Private _dealer As Dealer
        Private _paymentPurpose As PaymentPurpose

        Private _transferPaymentDetails As System.Collections.ArrayList

        Private _total As Decimal = -1
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


        <ColumnInfo("DueDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DueDate As DateTime
            Get
                Return _dueDate
            End Get
            Set(ByVal value As DateTime)
                _dueDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PlanTransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanTransferDate As DateTime
            Get
                Return _planTransferDate
            End Get
            Set(ByVal value As DateTime)
                _planTransferDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("IsNotOnTime", "{0}")> _
        Public Property IsNotOnTime As Short
            Get
                Return _isNotOnTime
            End Get
            Set(ByVal value As Short)
                _isNotOnTime = value
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


        <ColumnInfo("TotalActualAmount", "{0}")> _
        Public Property TotalActualAmount As Decimal
            Get
                Return _totalActualAmount
            End Get
            Set(ByVal value As Decimal)
                _totalActualAmount = value
            End Set
        End Property


        <ColumnInfo("ActualTrfDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ActualTrfDate As DateTime
            Get
                Return _ActualTrfDate
            End Get
            Set(ByVal value As DateTime)
                _ActualTrfDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("TransferAmount", "{0}")> _
        Public Property TransferAmount As Decimal
            Get
                Return _TransferAmount
            End Get
            Set(value As Decimal)
                _TransferAmount = value
            End Set
        End Property

        <ColumnInfo("TransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransferDate As DateTime
            Get
                Return _TransferDate
            End Get
            Set(value As DateTime)
                _TransferDate = value
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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property



        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "TransferPayment", "DealerID")> _
        Public Property Dealer() As Dealer
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



        <ColumnInfo("PaymentPurposeID", "{0}"), _
        RelationInfo("PaymentPurpose", "ID", "TransferPayment", "PaymentPurposeID")> _
        Public Property PaymentPurpose() As PaymentPurpose
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


        <RelationInfo("TransferPayment", "ID", "TransferPaymentDetail", "TransferPaymentID")> _
        Public ReadOnly Property TransferPaymentDetails() As System.Collections.ArrayList
            Get
                Try
                    If IsNothing(Me._transferPaymentDetails) Then ' (Me._transferPaymentDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TransferPaymentDetail), "TransferPayment", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._transferPaymentDetails = DoLoadArray(GetType(TransferPaymentDetail).ToString, criterias)
                    End If

                    Return Me._transferPaymentDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        Public ReadOnly Property Total() As Decimal
            Get
                If _total = -1 Then
                    _total = 0
                    For i As Integer = 0 To Me.TransferPaymentDetails.Count - 1
                        _total += CType(Me.TransferPaymentDetails(i), TransferPaymentDetail).Amount
                    Next
                End If
                Return _total
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
        Public Enum EnumStatus
            Baru = 0
            Batal = 1
            Konfirmasi = 2
            Batal_Konfirmasi = 3
            Validasi = 4
            BatalValidasi = 5
            Selesai = 6
            '[Enum].Parse(GetType(TransferPayment.EnumStatus), 1)
        End Enum

        Public ReadOnly Property IsAllowToValidate() As Boolean
            Get
                Dim IsAllow As Boolean = False

                If Me.ID > 0 Then
                    If CType(Me.Status, TransferPayment.EnumStatus) = EnumStatus.Baru OrElse CType(Me.Status, TransferPayment.EnumStatus) = EnumStatus.Konfirmasi Then
                        IsAllow = True
                    End If
                End If

                Return IsAllow
            End Get
        End Property

        Public ReadOnly Property IsAllowToAccelerate() As Boolean
            Get
                Dim IsAllow As Boolean = False

                If Me.ID > 0 Then
                    If CType(Me.Status, TransferPayment.EnumStatus) = EnumStatus.Baru Then
                        IsAllow = True
                    End If
                End If

                Return IsAllow
            End Get
        End Property
#End Region

    End Class
End Namespace

