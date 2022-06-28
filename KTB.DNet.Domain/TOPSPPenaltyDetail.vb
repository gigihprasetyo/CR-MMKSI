#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPPenaltyDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/16/2020 - 4:01:51 PM
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
    <Serializable(), TableInfo("TOPSPPenaltyDetail")> _
    Public Class TOPSPPenaltyDetail
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
        Private _accountingDocNo As String = String.Empty
        Private _actualTransferAmount As Decimal
        Private _actualTransferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _penaltyDays As Integer
        Private _amountPenalty As Decimal
        Private _pPh As Decimal
        Private _paymentType As Integer
        Private _billingNumber As String = String.Empty

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _sparePartBillingID As Integer
        'Private _tOPSPPenaltyID As Integer

        Private _sparePartBilling As SparePartBilling
        Private _tOPSPPenalty As TOPSPPenalty

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


        <ColumnInfo("AccountingDocNo", "'{0}'")> _
        Public Property AccountingDocNo As String
            Get
                Return _accountingDocNo
            End Get
            Set(ByVal value As String)
                _accountingDocNo = value
            End Set
        End Property


        <ColumnInfo("ActualTransferAmount", "{0}")> _
        Public Property ActualTransferAmount As Decimal
            Get
                Return _actualTransferAmount
            End Get
            Set(ByVal value As Decimal)
                _actualTransferAmount = value
            End Set
        End Property


        <ColumnInfo("ActualTransferDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ActualTransferDate As DateTime
            Get
                Return _actualTransferDate
            End Get
            Set(ByVal value As DateTime)
                _actualTransferDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PenaltyDays", "{0}")> _
        Public Property PenaltyDays As Integer
            Get
                Return _penaltyDays
            End Get
            Set(ByVal value As Integer)
                _penaltyDays = value
            End Set
        End Property


        <ColumnInfo("AmountPenalty", "{0}")> _
        Public Property AmountPenalty As Decimal
            Get
                Return _amountPenalty
            End Get
            Set(ByVal value As Decimal)
                _amountPenalty = value
            End Set
        End Property


        <ColumnInfo("PPh", "{0}")> _
        Public Property PPh As Decimal
            Get
                Return _pPh
            End Get
            Set(ByVal value As Decimal)
                _pPh = value
            End Set
        End Property


        <ColumnInfo("PaymentType", "{0}")> _
        Public Property PaymentType As Integer
            Get
                Return _paymentType
            End Get
            Set(ByVal value As Integer)
                _paymentType = value
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


        '    <ColumnInfo("SparePartBillingID", "{0}")> _
        '    Public Property SparePartBillingID As Integer

        '        Get
        'return _sparePartBillingID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _sparePartBillingID = value
        '        End Set
        '    End Property
        '    <ColumnInfo("TOPSPPenaltyID", "{0}")> _
        '    Public Property TOPSPPenaltyID As Integer

        '        Get
        'return _tOPSPPenaltyID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _tOPSPPenaltyID = value
        '        End Set
        '    End Property


        <ColumnInfo("TOPSPPenaltyID", "{0}"), _
        RelationInfo("TOPSPPenalty", "ID", "TOPSPPenaltyDetail", "TOPSPPenaltyID")> _
        Public Property TOPSPPenalty As TOPSPPenalty
            Get
                Try
                    If Not IsNothing(Me._TOPSPPenalty) AndAlso (Not Me._TOPSPPenalty.IsLoaded) Then

                        Me._TOPSPPenalty = CType(DoLoad(GetType(TOPSPPenalty).ToString(), _TOPSPPenalty.ID), TOPSPPenalty)
                        Me._TOPSPPenalty.MarkLoaded()
                    End If
                    Return Me._TOPSPPenalty
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As TOPSPPenalty)
                Me._TOPSPPenalty = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._TOPSPPenalty.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartBillingID", "{0}"), _
        RelationInfo("SparePartBilling", "ID", "TOPSPPenaltyDetail", "SparePartBillingID")> _
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
        Public Property BillingNumber As String
            Get
                If Not IsNothing(Me.SparePartBilling) Then
                    Return Me.SparePartBilling.BillingNumber
                End If
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
            End Set
        End Property
#End Region

    End Class
End Namespace
