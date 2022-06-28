#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionGRHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/2/2007 - 9:53:46 AM
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
    <Serializable(), TableInfo("MaterialPromotionGRHeader")> _
    Public Class MaterialPromotionGRHeader
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
        Private _gRNo As String = String.Empty
        Private _gRDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _materialPromotionGIHeader As MaterialPromotionGIHeader

        Private _materialPromotionGRDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("GRNo", "'{0}'")> _
        Public Property GRNo() As String
            Get
                Return _gRNo
            End Get
            Set(ByVal value As String)
                _gRNo = value
            End Set
        End Property


        <ColumnInfo("GRDate", "'{0:yyyy/MM/dd}'")> _
        Public Property GRDate() As DateTime
            Get
                Return _gRDate
            End Get
            Set(ByVal value As DateTime)
                _gRDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("MaterialPromotionGIHeaderID", "{0}"), _
        RelationInfo("MaterialPromotionGIHeader", "ID", "MaterialPromotionGRHeader", "MaterialPromotionGIHeaderID")> _
        Public Property MaterialPromotionGIHeader() As MaterialPromotionGIHeader
            Get
                Try
                    If Not isnothing(Me._materialPromotionGIHeader) AndAlso (Not Me._materialPromotionGIHeader.IsLoaded) Then

                        Me._materialPromotionGIHeader = CType(DoLoad(GetType(MaterialPromotionGIHeader).ToString(), _materialPromotionGIHeader.ID), MaterialPromotionGIHeader)
                        Me._materialPromotionGIHeader.MarkLoaded()

                    End If

                    Return Me._materialPromotionGIHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MaterialPromotionGIHeader)

                Me._materialPromotionGIHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._materialPromotionGIHeader.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("MaterialPromotionGRHeader", "ID", "MaterialPromotionGRDetail", "MaterialPromotionGRHeaderID")> _
        Public ReadOnly Property MaterialPromotionGRDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._materialPromotionGRDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionGRDetail), "MaterialPromotionGRHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MaterialPromotionGRDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._materialPromotionGRDetails = DoLoadArray(GetType(MaterialPromotionGRDetail).ToString, criterias)
                    End If

                    Return Me._materialPromotionGRDetails

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

#End Region

    End Class
End Namespace

