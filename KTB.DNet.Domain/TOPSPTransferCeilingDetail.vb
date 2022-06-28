
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPTransferCeilingDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/13/2018 - 2:37:06 PM
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
    <Serializable(), TableInfo("TOPSPTransferCeilingDetail")> _
    Public Class TOPSPTransferCeilingDetail
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
        'Private _tOPSPTransferCeilingID As Integer
        'Private _sparepartBillingID As Integer
        'Private _tOPSPTransferPaymentID As Integer
        Private _amount As Decimal
        Private _isIncome As Short
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _TOPSPTransferCeiling As TOPSPTransferCeiling
        Private _sparepartBilling As SparePartBilling
        Private _TOPSPTransferPayment As TOPSPTransferPayment



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


        <ColumnInfo("TOPSPTransferCeilingID", "{0}"), _
        RelationInfo("TOPSPTransferCeiling", "ID", "TOPSPTransferCeilingDetail", "TOPSPTransferCeilingID")> _
        Public Property TOPSPTransferCeiling As TOPSPTransferCeiling
            Get
                Try
                    If Not IsNothing(Me._TOPSPTransferCeiling) AndAlso (Not Me._TOPSPTransferCeiling.IsLoaded) Then

                        Me._TOPSPTransferCeiling = CType(DoLoad(GetType(TOPSPTransferCeiling).ToString(), _TOPSPTransferCeiling.ID), TOPSPTransferCeiling)
                        Me._TOPSPTransferCeiling.MarkLoaded()

                    End If

                    Return Me._TOPSPTransferCeiling

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TOPSPTransferCeiling)

                Me._TOPSPTransferCeiling = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._TOPSPTransferCeiling.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartBillingID", "{0}"), _
      RelationInfo("SparePartBilling", "ID", "TOPSPTransferCeilingDetail", "SparePartBillingID")> _
        Public Property SparePartBilling As SparePartBilling
            Get
                Try
                    If Not IsNothing(Me._sparepartBilling) AndAlso (Not Me._sparepartBilling.IsLoaded) Then

                        Me._sparepartBilling = CType(DoLoad(GetType(SparePartBilling).ToString(), _sparepartBilling.ID), SparePartBilling)
                        Me._sparepartBilling.MarkLoaded()

                    End If

                    Return Me._sparepartBilling

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartBilling)

                Me._sparepartBilling = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparepartBilling.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TOPSPTransferPaymentID", "{0}"), _
     RelationInfo("TOPSPTransferPayment", "ID", "TOPSPTransferCeilingDetail", "TOPSPTransferPaymentID")> _
        Public Property TOPSPTransferPayment As TOPSPTransferPayment
            Get
                Try
                    If Not IsNothing(Me._TOPSPTransferPayment) AndAlso (Not Me._TOPSPTransferPayment.IsLoaded) Then

                        Me._TOPSPTransferPayment = CType(DoLoad(GetType(TOPSPTransferPayment).ToString(), _TOPSPTransferPayment.ID), TOPSPTransferPayment)
                        Me._TOPSPTransferPayment.MarkLoaded()

                    End If

                    Return Me._TOPSPTransferPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TOPSPTransferPayment)

                Me._TOPSPTransferPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._TOPSPTransferPayment.MarkLoaded()
                End If
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


        <ColumnInfo("IsIncome", "{0}")> _
        Public Property IsIncome As Short
            Get
                Return _isIncome
            End Get
            Set(ByVal value As Short)
                _isIncome = value
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

