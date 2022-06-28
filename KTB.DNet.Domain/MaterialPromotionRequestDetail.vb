#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionRequestDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/9/2007 - 12:21:13 PM
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
    <Serializable(), TableInfo("MaterialPromotionRequestDetail")> _
    Public Class MaterialPromotionRequestDetail
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
        Private _requestQty As Integer
        Private _qty As Integer
        Private _description As String = String.Empty
        Private _statusGI As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _materialPromotion As MaterialPromotion
        Private _materialPromotionRequest As MaterialPromotionRequest



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


        <ColumnInfo("RequestQty", "{0}")> _
        Public Property RequestQty() As Integer
            Get
                Return _requestQty
            End Get
            Set(ByVal value As Integer)
                _requestQty = value
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


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("StatusGI", "{0}")> _
        Public Property StatusGI() As Byte
            Get
                Return _statusGI
            End Get
            Set(ByVal value As Byte)
                _statusGI = value
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


        <ColumnInfo("MaterialPromotionID", "{0}"), _
        RelationInfo("MaterialPromotion", "ID", "MaterialPromotionRequestDetail", "MaterialPromotionID")> _
        Public Property MaterialPromotion() As MaterialPromotion
            Get
                Try
                    If Not IsNothing(Me._materialPromotion) AndAlso (Not Me._materialPromotion.IsLoaded) Then

                        Me._materialPromotion = CType(DoLoad(GetType(MaterialPromotion).ToString(), _materialPromotion.ID), MaterialPromotion)
                        Me._materialPromotion.MarkLoaded()

                    End If

                    Return Me._materialPromotion

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MaterialPromotion)

                Me._materialPromotion = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._materialPromotion.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("MaterialPromotionRequestID", "{0}"), _
        RelationInfo("MaterialPromotionRequest", "ID", "MaterialPromotionRequestDetail", "MaterialPromotionRequestID")> _
        Public Property MaterialPromotionRequest() As MaterialPromotionRequest
            Get
                Try
                    If Not IsNothing(Me._materialPromotionRequest) AndAlso (Not Me._materialPromotionRequest.IsLoaded) Then

                        Me._materialPromotionRequest = CType(DoLoad(GetType(MaterialPromotionRequest).ToString(), _materialPromotionRequest.ID), MaterialPromotionRequest)
                        Me._materialPromotionRequest.MarkLoaded()

                    End If

                    Return Me._materialPromotionRequest

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MaterialPromotionRequest)

                Me._materialPromotionRequest = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._materialPromotionRequest.MarkLoaded()
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
        Public ReadOnly Property GIQty() As Integer
            Get
                If Me.ID = 0 Then
                    Return 0
                End If

                Dim criterias As New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "RequestNo", MatchType.Exact, Me.MaterialPromotionRequest.RequestNo))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "MaterialPromotion.ID", MatchType.Exact, Me.MaterialPromotion.ID.ToString))

                Dim agg As Aggregate = New Aggregate(GetType(MaterialPromotionGIGR), "Qty", AggregateType.Sum)

                Dim _GIQty = DoLoadScalar(GetType(MaterialPromotionGIGR).ToString(), agg, criterias)

                Return _GIQty

            End Get
        End Property

#End Region

    End Class
End Namespace

