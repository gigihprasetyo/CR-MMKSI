#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EstimationEquipSPPOAlocation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/1/2009 - 11:04:44
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
    <Serializable(), TableInfo("EquipSPPOAlocation")> _
    Public Class EquipSPPOAlocation
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
        Private _alocationQty As Integer
        Private _remailQty As Integer
        Private _note As String = String.Empty
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _sparePartPODetail As SparePartPODetail
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


        <ColumnInfo("AlocationQty", "{0}")> _
        Public Property AlocationQty() As Integer
            Get
                Return _alocationQty
            End Get
            Set(ByVal value As Integer)
                _alocationQty = value
            End Set
        End Property


        <ColumnInfo("RemailQty", "{0}")> _
        Public Property RemailQty() As Integer
            Get
                Return _remailQty
            End Get
            Set(ByVal value As Integer)
                _remailQty = value
            End Set
        End Property


        <ColumnInfo("Note", "'{0}'")> _
        Public Property Note() As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _note = value
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime() As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy() As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("SparePartPODetailID", "{0}"), _
        RelationInfo("SparePartPODetail", "ID", "EquipSPPOAlocation", "SparePartPODetailID")> _
        Public Property SparePartPODetail() As SparePartPODetail
            Get
                Try
                    If Not IsNothing(Me._sparePartPODetail) AndAlso (Not Me._sparePartPODetail.IsLoaded) Then
                        Me._sparePartPODetail = CType(DoLoad(GetType(SparePartPODetail).ToString(), _sparePartPODetail.ID), SparePartPODetail)
                        Me._sparePartPODetail.MarkLoaded()
                    End If
                    Return Me._sparePartPODetail
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
            Set(ByVal Value As SparePartPODetail)
                _sparePartPODetail = Value
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

