#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimSPBillingReturDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/11/2020 - 9:57:33 AM
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
    <Serializable(), TableInfo("ClaimSPBillingReturDetail")> _
    Public Class ClaimSPBillingReturDetail
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
        'Private _claimSPBillingReturID As Integer
        'Private _sparePartDODetailID As Integer
        Private _qty As Integer
        Private _amount As Decimal
        Private _tax As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _claimSPBillingRetur As ClaimSPBillingRetur
        Private _sparePartDODetail As SparePartDODetail




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


        '<ColumnInfo("ClaimSPBillingReturID", "{0}")> _
        'Public Property ClaimSPBillingReturID As Integer
        '    Get
        '        Return _claimSPBillingReturID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _claimSPBillingReturID = value
        '    End Set
        'End Property


        '<ColumnInfo("SparePartDODetailID", "{0}")> _
        'Public Property SparePartDODetailID As Integer
        '    Get
        '        Return _sparePartDODetailID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sparePartDODetailID = value
        '    End Set
        'End Property


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
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


        <ColumnInfo("ClaimSPBillingReturID", "{0}"), _
        RelationInfo("ClaimSPBillingRetur", "ID", "ClaimSPBillingReturDetail", "ClaimSPBillingReturID")> _
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


        <ColumnInfo("SparePartDODetailID", "{0}"), _
        RelationInfo("SparePartDODetail", "ID", "ClaimSPBillingReturDetail", "SparePartDODetailID")> _
        Public Property SparePartDODetail() As SparePartDODetail
            Get
                Try
                    If Not IsNothing(Me._sparePartDODetail) AndAlso (Not Me._sparePartDODetail.IsLoaded) Then
                        Me._sparePartDODetail = CType(DoLoad(GetType(SparePartDODetail).ToString(), _sparePartDODetail.ID), SparePartDODetail)
                        Me._sparePartDODetail.MarkLoaded()
                    End If
                    Return Me._sparePartDODetail
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
            Set(ByVal value As SparePartDODetail)
                Me._sparePartDODetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartDODetail.MarkLoaded()
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
