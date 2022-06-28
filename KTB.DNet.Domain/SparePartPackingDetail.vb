
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPackingDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 10/5/2016 - 9:27:10 AM
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
    <Serializable(), TableInfo("SparePartPackingDetail")> _
    Public Class SparePartPackingDetail
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
        Private _internalHUItemNo As String = String.Empty
        Private _dOItemNo As Integer
        Private _qty As Decimal
        Private _uoM As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartDO As SparePartDO
        Private _sparePartMaster As SparePartMaster
        Private _sparePartPacking As SparePartPacking



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


        <ColumnInfo("InternalHUItemNo", "'{0}'")> _
        Public Property InternalHUItemNo As String
            Get
                Return _internalHUItemNo
            End Get
            Set(ByVal value As String)
                _internalHUItemNo = value
            End Set
        End Property


        <ColumnInfo("DOItemNo", "{0}")> _
        Public Property DOItemNo As Integer
            Get
                Return _dOItemNo
            End Get
            Set(ByVal value As Integer)
                _dOItemNo = value
            End Set
        End Property


        <ColumnInfo("Qty", "#,##0")> _
        Public Property Qty As Decimal
            Get
                Return _qty
            End Get
            Set(ByVal value As Decimal)
                _qty = value
            End Set
        End Property


        <ColumnInfo("UoM", "'{0}'")> _
        Public Property UoM As String
            Get
                Return _uoM
            End Get
            Set(ByVal value As String)
                _uoM = value
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


        <ColumnInfo("SparePartDOID", "{0}"), _
        RelationInfo("SparePartDO", "ID", "SparePartPackingDetail", "SparePartDOID")> _
        Public Property SparePartDO As SparePartDO
            Get
                Try
                    If Not isnothing(Me._sparePartDO) AndAlso (Not Me._sparePartDO.IsLoaded) Then

                        Me._sparePartDO = CType(DoLoad(GetType(SparePartDO).ToString(), _sparePartDO.ID), SparePartDO)
                        Me._sparePartDO.MarkLoaded()

                    End If

                    Return Me._sparePartDO

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartDO)

                Me._sparePartDO = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartDO.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "SparePartPackingDetail", "SparePartMasterID")> _
        Public Property SparePartMaster As SparePartMaster
            Get
                Try
                    If Not isnothing(Me._sparePartMaster) AndAlso (Not Me._sparePartMaster.IsLoaded) Then

                        Me._sparePartMaster = CType(DoLoad(GetType(SparePartMaster).ToString(), _sparePartMaster.ID), SparePartMaster)
                        Me._sparePartMaster.MarkLoaded()

                    End If

                    Return Me._sparePartMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartMaster)

                Me._sparePartMaster = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartPackingID", "{0}"), _
        RelationInfo("SparePartPacking", "ID", "SparePartPackingDetail", "SparePartPackingID")> _
        Public Property SparePartPacking As SparePartPacking
            Get
                Try
                    If Not isnothing(Me._sparePartPacking) AndAlso (Not Me._sparePartPacking.IsLoaded) Then

                        Me._sparePartPacking = CType(DoLoad(GetType(SparePartPacking).ToString(), _sparePartPacking.ID), SparePartPacking)
                        Me._sparePartPacking.MarkLoaded()

                    End If

                    Return Me._sparePartPacking

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartPacking)

                Me._sparePartPacking = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPacking.MarkLoaded()
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

