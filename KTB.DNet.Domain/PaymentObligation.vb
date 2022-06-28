#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PaymentObligation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/25/2007 - 3:26:37 PM
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
    <Serializable(), TableInfo("PaymentObligation")> _
    Public Class PaymentObligation
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
        Private _sourceDocument As Integer
        Private _assignment As String = String.Empty
        Private _sequence As Integer
        Private _amount As Decimal
        Private _description As String = String.Empty
        Private _docDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _transactionDueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _paidDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateBy As String = String.Empty
        Private _confirmedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _confirmedBy As String = String.Empty
        Private _isTOP As Short
        Private _status As Integer
        Private _validateMD5Code As String = String.Empty
        Private _validateIPAddress As String = String.Empty
        Private _pinalty As Decimal
        Private _pinaltyReal As Decimal
        Private _paidAmount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _paymentObligationType As PaymentObligationType
        Private _paymentAssignmentType As PaymentAssignmentType
        Private _paymentRegDoc As PaymentRegDoc

        Private _paymentObligationHistorys As System.Collections.ArrayList = New System.Collections.ArrayList


#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("SourceDocument", "{0}")> _
        Public Property SourceDocument() As Integer
            Get
                Return _sourceDocument
            End Get
            Set(ByVal value As Integer)
                _sourceDocument = value
            End Set
        End Property


        <ColumnInfo("Assignment", "'{0}'")> _
        Public Property Assignment() As String
            Get
                Return _assignment
            End Get
            Set(ByVal value As String)
                _assignment = value
            End Set
        End Property


        <ColumnInfo("Sequence", "{0}")> _
        Public Property Sequence() As Integer
            Get
                Return _sequence
            End Get
            Set(ByVal value As Integer)
                _sequence = value
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("DocDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DocDate() As DateTime
            Get
                Return _docDate
            End Get
            Set(ByVal value As DateTime)
                _docDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DueDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DueDate() As DateTime
            Get
                Return _dueDate
            End Get
            Set(ByVal value As DateTime)
                _dueDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("TransactionDueDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransactionDueDate() As DateTime
            Get
                Return _transactionDueDate
            End Get
            Set(ByVal value As DateTime)
                _transactionDueDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PaidDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PaidDate() As DateTime
            Get
                Return _paidDate
            End Get
            Set(ByVal value As DateTime)
                _paidDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ValidateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidateTime() As DateTime
            Get
                Return _validateTime
            End Get
            Set(ByVal value As DateTime)
                _validateTime = value
            End Set
        End Property


        <ColumnInfo("ValidateBy", "'{0}'")> _
        Public Property ValidateBy() As String
            Get
                Return _validateBy
            End Get
            Set(ByVal value As String)
                _validateBy = value
            End Set
        End Property


        <ColumnInfo("ConfirmedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ConfirmedTime() As DateTime
            Get
                Return _confirmedTime
            End Get
            Set(ByVal value As DateTime)
                _confirmedTime = value
            End Set
        End Property


        <ColumnInfo("ConfirmedBy", "'{0}'")> _
        Public Property ConfirmedBy() As String
            Get
                Return _confirmedBy
            End Get
            Set(ByVal value As String)
                _confirmedBy = value
            End Set
        End Property


        <ColumnInfo("IsTOP", "{0}")> _
        Public Property IsTOP() As Short
            Get
                Return _isTOP
            End Get
            Set(ByVal value As Short)
                _isTOP = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("ValidateMD5Code", "'{0}'")> _
        Public Property ValidateMD5Code() As String
            Get
                Return _validateMD5Code
            End Get
            Set(ByVal value As String)
                _validateMD5Code = value
            End Set
        End Property


        <ColumnInfo("ValidateIPAddress", "'{0}'")> _
        Public Property ValidateIPAddress() As String
            Get
                Return _validateIPAddress
            End Get
            Set(ByVal value As String)
                _validateIPAddress = value
            End Set
        End Property


        <ColumnInfo("Pinalty", "{0}")> _
        Public Property Pinalty() As Decimal
            Get
                Return _pinalty
            End Get
            Set(ByVal value As Decimal)
                _pinalty = value
            End Set
        End Property


        <ColumnInfo("PinaltyReal", "{0}")> _
        Public Property PinaltyReal() As Decimal
            Get
                Return _pinaltyReal
            End Get
            Set(ByVal value As Decimal)
                _pinaltyReal = value
            End Set
        End Property


        <ColumnInfo("PaidAmount", "{0}")> _
        Public Property PaidAmount() As Decimal
            Get
                Return _paidAmount
            End Get
            Set(ByVal value As Decimal)
                _paidAmount = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "PaymentObligation", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PaymentObligationTypeID", "{0}"), _
        RelationInfo("PaymentObligationType", "ID", "PaymentObligation", "PaymentObligationTypeID")> _
        Public Property PaymentObligationType() As PaymentObligationType
            Get
                Try
                    If Not isnothing(Me._paymentObligationType) AndAlso (Not Me._paymentObligationType.IsLoaded) Then

                        Me._paymentObligationType = CType(DoLoad(GetType(PaymentObligationType).ToString(), _paymentObligationType.ID), PaymentObligationType)
                        Me._paymentObligationType.MarkLoaded()

                    End If

                    Return Me._paymentObligationType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PaymentObligationType)

                Me._paymentObligationType = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._paymentObligationType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PaymentAssignmentTypeID", "{0}"), _
        RelationInfo("PaymentAssignmentType", "ID", "PaymentObligation", "PaymentAssignmentTypeID")> _
        Public Property PaymentAssignmentType() As PaymentAssignmentType
            Get
                Try
                    If Not isnothing(Me._paymentAssignmentType) AndAlso (Not Me._paymentAssignmentType.IsLoaded) Then

                        Me._paymentAssignmentType = CType(DoLoad(GetType(PaymentAssignmentType).ToString(), _paymentAssignmentType.ID), PaymentAssignmentType)
                        Me._paymentAssignmentType.MarkLoaded()

                    End If

                    Return Me._paymentAssignmentType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PaymentAssignmentType)

                Me._paymentAssignmentType = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._paymentAssignmentType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PaymentRegDocID", "{0}"), _
        RelationInfo("PaymentRegDoc", "ID", "PaymentObligation", "PaymentRegDocID")> _
        Public Property PaymentRegDoc() As PaymentRegDoc
            Get
                Try
                    If Not isnothing(Me._paymentRegDoc) AndAlso (Not Me._paymentRegDoc.IsLoaded) Then

                        Me._paymentRegDoc = CType(DoLoad(GetType(PaymentRegDoc).ToString(), _paymentRegDoc.ID), PaymentRegDoc)
                        Me._paymentRegDoc.MarkLoaded()

                    End If

                    Return Me._paymentRegDoc

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PaymentRegDoc)

                Me._paymentRegDoc = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._paymentRegDoc.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("PaymentObligation", "ID", "PaymentObligationHistory", "PaymentObligationID")> _
        Public ReadOnly Property PaymentObligationHistorys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._paymentObligationHistorys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PaymentObligationHistory), "PaymentObligation", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PaymentObligationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._paymentObligationHistorys = DoLoadArray(GetType(PaymentObligationHistory).ToString, criterias)
                    End If

                    Return Me._paymentObligationHistorys

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
        Public ReadOnly Property TotalAmount() As Decimal
            Get
                If Me.ID = 0 Then
                    Return 0
                End If

                Dim criterias As New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "Assignment", MatchType.Exact, Me.Assignment))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.ID", MatchType.Exact, Me.Dealer.ID))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "Status", MatchType.Exact, CInt(EnumOnlinePayment.StatusOnlinePayment.Baru)))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "SourceDocument", MatchType.Exact, Me.SourceDocument))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "PaymentAssignmentType.ID", MatchType.Exact, Me.PaymentAssignmentType.ID))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "IsTOP", MatchType.Exact, Me.IsTOP))

                Dim agg As Aggregate = New Aggregate(GetType(PaymentObligation), "Amount", AggregateType.Sum)

                TotalAmount = DoLoadScalarDecimal(GetType(PaymentObligation).ToString(), agg, criterias)
                Return TotalAmount
            End Get
        End Property

        Public ReadOnly Property TotalItem() As Integer
            Get
                If Me.ID = 0 Then
                    Return 0
                End If

                Dim criterias As New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "Status", MatchType.Exact, CInt(EnumOnlinePayment.StatusOnlinePayment.Baru)))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "Assignment", MatchType.Exact, Me.Assignment))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.ID", MatchType.Exact, Me.Dealer.ID))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "SourceDocument", MatchType.Exact, Me.SourceDocument))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "PaymentAssignmentType.ID", MatchType.Exact, Me.PaymentAssignmentType.ID))
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "IsTOP", MatchType.Exact, Me.IsTOP))
                'criterias.opAnd(New Criteria(GetType(PaymentObligation), "DocDate", MatchType.GreaterOrEqual, icFromDocDate.Value))
                'criterias.opAnd(New Criteria(GetType(PaymentObligation), "DocDate", MatchType.LesserOrEqual, icToDocDate.Value))


                Dim agg As Aggregate = New Aggregate(GetType(PaymentObligation), "Amount", AggregateType.Count)

                TotalItem = DoLoadScalar(GetType(PaymentObligation).ToString(), agg, criterias)
                Return TotalItem
            End Get
        End Property

        Public ReadOnly Property isTOPDesc() As String
            Get
                If Me.IsTOP = 0 Then
                    Return "COD"
                Else
                    Return "TOP"
                End If

            End Get
        End Property

        Public ReadOnly Property StatusDesc() As String
            Get
                Dim str As String = String.Empty
                str = New EnumOnlinePayment().PaymentObligationStatusDesc(Me.Status)
                Return str
            End Get
        End Property
#End Region

    End Class
End Namespace

