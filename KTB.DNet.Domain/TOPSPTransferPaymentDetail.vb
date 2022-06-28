
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPTransferPaymentDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/7/2018 - 11:24:20 AM
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
    <Serializable(), TableInfo("TOPSPTransferPaymentDetail")> _
    Public Class TOPSPTransferPaymentDetail
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
        Private _amount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _tOPSPTransferPayment As TOPSPTransferPayment
        Private _sparePartBilling As SparePartBilling
        Private _tOPSPPenaltyDetails As arraylist

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


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
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


        <ColumnInfo("TOPSPTransferPaymentID", "{0}"), _
        RelationInfo("TOPSPTransferPayment", "ID", "TOPSPTransferPaymentDetail", "TOPSPTransferPaymentID")> _
        Public Property TOPSPTransferPayment As TOPSPTransferPayment
            Get
                Try
                    If Not isnothing(Me._tOPSPTransferPayment) AndAlso (Not Me._tOPSPTransferPayment.IsLoaded) Then

                        Me._tOPSPTransferPayment = CType(DoLoad(GetType(TOPSPTransferPayment).ToString(), _tOPSPTransferPayment.ID), TOPSPTransferPayment)
                        Me._tOPSPTransferPayment.MarkLoaded()

                    End If

                    Return Me._tOPSPTransferPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TOPSPTransferPayment)

                Me._tOPSPTransferPayment = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._tOPSPTransferPayment.MarkLoaded()
                End If
            End Set
        End Property


        Public Property TOPSPPenaltyDetails() As ArrayList
            Get
                Try
                    If IsNothing(Me._tOPSPPenaltyDetails) OrElse (Not IsNothing(_tOPSPPenaltyDetails) AndAlso (Me._tOPSPPenaltyDetails.Count = 0)) Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenaltyDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "SparePartBilling.ID", MatchType.Exact, Me.SparePartBilling.ID))
                        _tOPSPPenaltyDetails = DoLoadArray(GetType(TOPSPPenaltyDetail).ToString, criterias)
                    End If
                    Return Me._tOPSPPenaltyDetails

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(value As ArrayList)
                _tOPSPPenaltyDetails = value
            End Set
        End Property



        <ColumnInfo("SparePartBillingID", "{0}"), _
        RelationInfo("SparePartBilling", "ID", "TOPSPTransferPaymentDetail", "SparePartBillingID")> _
        Public Property SparePartBilling As SparePartBilling
            Get
                Try
                    If Not IsNothing(Me._sparePartBilling) AndAlso (Not Me._sparePartBilling.IsLoaded) Then

                        Me._sparePartBilling = CType(DoLoad(GetType(SparePartBilling).ToString(), _sparePartBilling.ID), SparePartBilling)
                        Me._sparePartBilling.MarkLoaded()

                    End If

                    Return Me._sparePartBilling

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartBilling)

                Me._sparePartBilling = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartBilling.MarkLoaded()
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

