
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartBilling Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/29/2016 - 2:32:22 PM
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
    <Serializable(), TableInfo("SparePartBilling")> _
    Public Class SparePartBilling
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
        Private _billingNumber As String = String.Empty
        Private _billingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _totalAmount As Decimal
        Private _tax As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        'Private _dueDate As TOPSPDueDate
        'Private _tOPSPDeposit As TOPSPDeposit
        Private _TermOfPayment As TermOfPayment

        Private _sparePartBillingDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("BillingNumber", "'{0}'")> _
        Public Property BillingNumber As String
            Get
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
            End Set
        End Property


        <ColumnInfo("BillingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BillingDate As DateTime
            Get
                Return _billingDate
            End Get
            Set(ByVal value As DateTime)
                _billingDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("Tax", "{0}")> _
        Public Property Tax As Decimal
            Get
                Return _tax
            End Get
            Set(ByVal value As Decimal)
                _tax = value
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

        '<ColumnInfo("ID", "{0}"), _
        'RelationInfo("SparePartBilling", "ID", "TOPSPDueDate", "SparePartBillingID")> _
        'Public ReadOnly Property TOPSPDueDate As TOPSPDueDate
        '    Get
        '        Try
        '            If Not IsNothing(Me._dueDate) AndAlso (Not Me._dueDate.IsLoaded) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(TOPSPDueDate), "SparePartBilling", Me.ID)


        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(TOPSPDueDate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Dim tempColl As ArrayList = DoLoadArray(GetType(TOPSPDueDate).ToString, criterias)

        '                If tempColl.Count > 0 Then
        '                    Me._dueDate = CType(tempColl(0), TOPSPDueDate)
        '                Else
        '                    Me._dueDate = Nothing
        '                End If

        '            End If

        '            Return Me._dueDate

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        'End Property


        '<ColumnInfo("ID", "{0}"), _
        'RelationInfo("SparePartBilling", "ID", "TOPSPDeposit", "SparePartBillingID")> _
        'Public Property TOPSPDeposit As TOPSPDeposit
        '    Get
        '        Try
        '            If Not IsNothing(Me._tOPSPDeposit) AndAlso (Not Me._tOPSPDeposit.IsLoaded) Then

        '                Me._tOPSPDeposit = CType(DoLoad(GetType(TOPSPDeposit).ToString(), _tOPSPDeposit.ID), TOPSPDeposit)
        '                Me._tOPSPDeposit.MarkLoaded()

        '            End If

        '            Return Me._tOPSPDeposit

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As TOPSPDeposit)

        '        Me._tOPSPDeposit = value
        '        If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._tOPSPDeposit.MarkLoaded()
        '        End If
        '    End Set
        'End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "SparePartBilling", "DealerID")> _
        Public Property Dealer As Dealer
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

        <ColumnInfo("TermOfPaymentID", "{0}"), _
        RelationInfo("TermOfPayment", "ID", "SparePartBilling", "TermOfPaymentID")> _
        Public Property TermOfPayment As TermOfPayment
            Get
                Try
                    If Not IsNothing(Me._TermOfPayment) AndAlso (Not Me._TermOfPayment.IsLoaded) Then

                        Me._TermOfPayment = CType(DoLoad(GetType(TermOfPayment).ToString(), _TermOfPayment.ID), TermOfPayment)
                        Me._TermOfPayment.MarkLoaded()

                    End If

                    Return Me._TermOfPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TermOfPayment)

                Me._TermOfPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._TermOfPayment.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("SparePartBilling", "ID", "SparePartBillingDetail", "SparePartBillingID")> _
        Public ReadOnly Property SparePartBillingDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartBillingDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartBillingDetail), "SparePartBilling", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartBillingDetails = DoLoadArray(GetType(SparePartBillingDetail).ToString, criterias)
                    End If

                    Return Me._sparePartBillingDetails

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

