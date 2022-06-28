#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimSPBillingRetur Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/11/2020 - 9:16:14 AM
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
    <Serializable(), TableInfo("ClaimSPBillingRetur")> _
    Public Class ClaimSPBillingRetur
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
        'Private _claimHeaderID As Integer
        'Private _sparePartBillingID As Integer
        Private _billingReturNumber As String = String.Empty
        Private _billingReturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sORetur As String = String.Empty
        Private _sOReturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dORetur As String = String.Empty
        Private _dOReturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _claimHeader As ClaimHeader
        Private _sparePartBilling As SparePartBilling

        Private _isClaimID As Boolean = False

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


        '<ColumnInfo("ClaimHeaderID", "{0}")> _
        'Public Property ClaimHeaderID As Integer
        '    Get
        '        Return _claimHeaderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _claimHeaderID = value
        '    End Set
        'End Property


        '<ColumnInfo("SparePartBillingID", "{0}")> _
        'Public Property SparePartBillingID As Integer
        '    Get
        '        Return _sparePartBillingID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sparePartBillingID = value
        '    End Set
        'End Property


        <ColumnInfo("BillingReturNumber", "'{0}'")> _
        Public Property BillingReturNumber As String
            Get
                Return _billingReturNumber
            End Get
            Set(ByVal value As String)
                _billingReturNumber = value
            End Set
        End Property


        <ColumnInfo("BillingReturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BillingReturDate As DateTime
            Get
                Return _billingReturDate
            End Get
            Set(ByVal value As DateTime)
                _billingReturDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SORetur", "'{0}'")> _
        Public Property SORetur As String
            Get
                Return _sORetur
            End Get
            Set(ByVal value As String)
                _sORetur = value
            End Set
        End Property


        <ColumnInfo("SOReturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SOReturDate As DateTime
            Get
                Return _sOReturDate
            End Get
            Set(ByVal value As DateTime)
                _sOReturDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DORetur", "'{0}'")> _
        Public Property DORetur As String
            Get
                Return _dORetur
            End Get
            Set(ByVal value As String)
                _dORetur = value
            End Set
        End Property


        <ColumnInfo("DOReturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DOReturDate As DateTime
            Get
                Return _dOReturDate
            End Get
            Set(ByVal value As DateTime)
                _dOReturDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("ClaimHeaderID", "{0}"), _
        RelationInfo("ClaimHeader", "ID", "ClaimSPBillingRetur", "ClaimHeaderID")> _
        Public Property ClaimHeader() As ClaimHeader
            Get
                Try
                    If Not IsNothing(Me._claimHeader) AndAlso (Not Me._claimHeader.IsLoaded) Then
                        Me._claimHeader = CType(DoLoad(GetType(ClaimHeader).ToString(), _claimHeader.ID), ClaimHeader)
                        Me._claimHeader.MarkLoaded()
                    End If
                    Return Me._claimHeader
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
            Set(ByVal value As ClaimHeader)
                Me._claimHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SparePartBillingID", "{0}"), _
        RelationInfo("SparePartBilling", "ID", "ClaimSPBillingRetur", "SparePartBillingID")> _
        Public Property SparePartBilling() As SparePartBilling
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


        Public Property IsClaimID() As Boolean
            Get
                Return _isClaimID
            End Get
            Set(ByVal value As Boolean)
                _isClaimID = value
            End Set
        End Property


        Public Property ClaimSPBillingReturDetail As ArrayList = New ArrayList

        Public Property ClaimDebitNote As ClaimDebitNote

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
