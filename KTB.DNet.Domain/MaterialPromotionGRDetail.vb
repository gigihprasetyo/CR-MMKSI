#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionGRDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/2/2007 - 9:52:54 AM
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
    <Serializable(), TableInfo("MaterialPromotionGRDetail")> _
    Public Class MaterialPromotionGRDetail
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
        Private _qty As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _materialPromotionGRHeader As MaterialPromotionGRHeader
        Private _materialPromotionGIDetail As MaterialPromotionGIDetail



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


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty() As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
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


        <ColumnInfo("MaterialPromotionGRHeaderID", "{0}"), _
        RelationInfo("MaterialPromotionGRHeader", "ID", "MaterialPromotionGRDetail", "MaterialPromotionGRHeaderID")> _
        Public Property MaterialPromotionGRHeader() As MaterialPromotionGRHeader
            Get
                Try
                    If Not isnothing(Me._materialPromotionGRHeader) AndAlso (Not Me._materialPromotionGRHeader.IsLoaded) Then

                        Me._materialPromotionGRHeader = CType(DoLoad(GetType(MaterialPromotionGRHeader).ToString(), _materialPromotionGRHeader.ID), MaterialPromotionGRHeader)
                        Me._materialPromotionGRHeader.MarkLoaded()

                    End If

                    Return Me._materialPromotionGRHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MaterialPromotionGRHeader)

                Me._materialPromotionGRHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._materialPromotionGRHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("MaterialPromotionGIDetailID", "{0}"), _
        RelationInfo("MaterialPromotionGIDetail", "ID", "MaterialPromotionGRDetail", "MaterialPromotionGIDetailID")> _
        Public Property MaterialPromotionGIDetail() As MaterialPromotionGIDetail
            Get
                Try
                    If Not isnothing(Me._materialPromotionGIDetail) AndAlso (Not Me._materialPromotionGIDetail.IsLoaded) Then

                        Me._materialPromotionGIDetail = CType(DoLoad(GetType(MaterialPromotionGIDetail).ToString(), _materialPromotionGIDetail.ID), MaterialPromotionGIDetail)
                        Me._materialPromotionGIDetail.MarkLoaded()

                    End If

                    Return Me._materialPromotionGIDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MaterialPromotionGIDetail)

                Me._materialPromotionGIDetail = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._materialPromotionGIDetail.MarkLoaded()
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

