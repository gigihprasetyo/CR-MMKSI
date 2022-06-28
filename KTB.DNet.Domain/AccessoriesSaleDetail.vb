
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AccessoriesSaleDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2012 - 2:44:36 PM
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
    <Serializable(), TableInfo("AccessoriesSaleDetail")> _
    Public Class AccessoriesSaleDetail
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
        Private _accessoriesSaleID As Integer
        Private _spartPartMasterID As Integer
        Private _jumlah As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("AccessoriesSaleID", "{0}")> _
        Public Property AccessoriesSaleID() As Integer
            Get
                Return _accessoriesSaleID
            End Get
            Set(ByVal value As Integer)
                _accessoriesSaleID = value
            End Set
        End Property


        <ColumnInfo("SpartPartMasterID", "{0}")> _
        Public Property SpartPartMasterID() As Integer
            Get
                Return _spartPartMasterID
            End Get
            Set(ByVal value As Integer)
                _spartPartMasterID = value
            End Set
        End Property


        <ColumnInfo("Jumlah", "{0}")> _
        Public Property Jumlah() As Integer
            Get
                Return _jumlah
            End Get
            Set(ByVal value As Integer)
                _jumlah = value
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

        Private _AccessoriesSale As AccessoriesSale
        <ColumnInfo("AccessoriesSaleID", "{0}"), _
        RelationInfo("AccessoriesSale", "ID", "AccessoriesSaleDetail", "AccessoriesSaleID")> _
        Public Property AccessoriesSale() As AccessoriesSale
            Get
                Try
                    If Not IsNothing(Me._AccessoriesSale) AndAlso (Not Me._AccessoriesSale.IsLoaded) Then

                        Me._AccessoriesSale = CType(DoLoad(GetType(AccessoriesSale).ToString(), _AccessoriesSale.ID), AccessoriesSale)
                        Me._AccessoriesSale.MarkLoaded()

                    End If

                    Return Me._AccessoriesSale

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AccessoriesSale)

                Me._AccessoriesSale = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._AccessoriesSale.MarkLoaded()
                End If
            End Set
        End Property

        Private _sparePartMaster As SparePartMaster
        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "AccessoriesSaleDetail", "SparePartMasterID")> _
        Public Property SparePartMaster() As SparePartMaster
            Get
                Try
                    'If IsNothing(Me._sparePartMaster) OrElse Me._sparePartMaster.ID < 1 Then
                    '    Me._sparePartMaster = New SparePartMaster
                    '    Me._sparePartMaster.ID = Me.SpartPartMasterID
                    'End If
                    If Not IsNothing(Me._sparePartMaster) AndAlso (Not Me._sparePartMaster.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartMaster.MarkLoaded()
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

