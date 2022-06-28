#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimDebitNote Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/6/2020 - 5:28:20 PM
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
    <Serializable(), TableInfo("ClaimDebitNote")> _
    Public Class ClaimDebitNote
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
        Private _debitNoteNumber As String = String.Empty
        Private _debitNoteDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _totalAmount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _claimHeader As ClaimHeader
        'Private _sparePartBilling As SparePartBilling
        Private _claimSPBillingRetur As ClaimSPBillingRetur

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


        <ColumnInfo("DebitNoteNumber", "'{0}'")> _
        Public Property DebitNoteNumber As String
            Get
                Return _debitNoteNumber
            End Get
            Set(ByVal value As String)
                _debitNoteNumber = value
            End Set
        End Property


        <ColumnInfo("DebitNoteDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DebitNoteDate As DateTime
            Get
                Return _debitNoteDate
            End Get
            Set(ByVal value As DateTime)
                _debitNoteDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("ClaimSPBillingReturID", "{0}"), _
        RelationInfo("ClaimSPBillingRetur", "ID", "ClaimDebitNote", "ClaimSPBillingReturID")> _
        Public Property ClaimSPBillingRetur() As ClaimSPBillingRetur
            Get
                Try
                    If Not IsNothing(Me._claimSPBillingRetur) AndAlso (Not Me._claimSPBillingRetur.IsLoaded) Then
                        Me._claimSPBillingRetur = CType(DoLoad(GetType(ClaimSPBillingRetur).ToString(), _claimSPBillingRetur.ID), ClaimSPBillingRetur)
                        Me._claimSPBillingRetur.MarkLoaded()
                    End If
                    Return Me._claimSPBillingRetur
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
            Set(ByVal value As ClaimSPBillingRetur)
                Me._claimSPBillingRetur = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimSPBillingRetur.MarkLoaded()
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
