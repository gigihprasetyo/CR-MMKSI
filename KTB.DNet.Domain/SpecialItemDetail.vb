#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SpecialItemDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 1/19/2006 - 10:45:40 AM
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
    <Serializable(), TableInfo("SpecialItemDetail")> _
    Public Class SpecialItemDetail
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
        Private _partName As String = String.Empty
        Private _modelCode As String = String.Empty
        Private _itemStatus As Short
        Private _extMaterialGroup As String = String.Empty
        Private _remark As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartMaster As SparePartMaster
        Private _specialItemHeader As SpecialItemHeader

        Private _specialItemPackages As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("PartName", "'{0}'")> _
        Public Property PartName() As String
            Get
                Return _partName
            End Get
            Set(ByVal value As String)
                _partName = value
            End Set
        End Property


        <ColumnInfo("ModelCode", "'{0}'")> _
        Public Property ModelCode() As String
            Get
                Return _modelCode
            End Get
            Set(ByVal value As String)
                _modelCode = value
            End Set
        End Property


        <ColumnInfo("ItemStatus", "{0}")> _
        Public Property ItemStatus() As Short
            Get
                Return _itemStatus
            End Get
            Set(ByVal value As Short)
                _itemStatus = value
            End Set
        End Property


        <ColumnInfo("ExtMaterialGroup", "'{0}'")> _
        Public Property ExtMaterialGroup() As String
            Get
                Return _extMaterialGroup
            End Get
            Set(ByVal value As String)
                _extMaterialGroup = value
            End Set
        End Property


        <ColumnInfo("Remark", "'{0}'")> _
        Public Property Remark() As String
            Get
                Return _remark
            End Get
            Set(ByVal value As String)
                _remark = value
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


        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "SpecialItemDetail", "SparePartMasterID")> _
        Public Property SparePartMaster() As SparePartMaster
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

        <ColumnInfo("SpecialItemHeaderID", "{0}"), _
        RelationInfo("SpecialItemHeader", "ID", "SpecialItemDetail", "SpecialItemHeaderID")> _
        Public Property SpecialItemHeader() As SpecialItemHeader
            Get
                Try
                    If Not isnothing(Me._specialItemHeader) AndAlso (Not Me._specialItemHeader.IsLoaded) Then

                        Me._specialItemHeader = CType(DoLoad(GetType(SpecialItemHeader).ToString(), _specialItemHeader.ID), SpecialItemHeader)
                        Me._specialItemHeader.MarkLoaded()

                    End If

                    Return Me._specialItemHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SpecialItemHeader)

                Me._specialItemHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._specialItemHeader.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("SpecialItemDetail", "ID", "SpecialItemPackage", "SpecialItemDetailID")> _
        Public ReadOnly Property SpecialItemPackages() As System.Collections.ArrayList
            Get
                Try
                    If (Me._specialItemPackages.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SpecialItemPackage), "SpecialItemDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SpecialItemPackage), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._specialItemPackages = DoLoadArray(GetType(SpecialItemPackage).ToString, criterias)
                    End If

                    Return Me._specialItemPackages

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
        Private _specialItemPackage As SpecialItemPackage
        Public Property SpecialItemPackage() As SpecialItemPackage
            Get
                Return _specialItemPackage
            End Get
            Set(ByVal Value As SpecialItemPackage)
                _specialItemPackage = Value
            End Set
        End Property

#End Region

    End Class
End Namespace

