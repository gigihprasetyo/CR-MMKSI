#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotion Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/8/2007 - 12:28:25 PM
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
    <Serializable(), TableInfo("MaterialPromotion")> _
    Public Class MaterialPromotion
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
        Private _goodNo As String = String.Empty
        Private _name As String = String.Empty
        Private _unit As String = String.Empty
        Private _price As Decimal
        Private _stock As Integer
        Private _status As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _productCategory As ProductCategory
        Private _materialPromotionPriceHistorys As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _materialPromotionGIGRs As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _materialPromotionRequestDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _materialPromotionStockAdjustments As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _materialPromotionGIDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _materialPromotionAllocations As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("GoodNo", "'{0}'")> _
        Public Property GoodNo() As String
            Get
                Return _goodNo
            End Get
            Set(ByVal value As String)
                _goodNo = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        <ColumnInfo("Unit", "'{0}'")> _
        Public Property Unit() As String
            Get
                Return _unit
            End Get
            Set(ByVal value As String)
                _unit = value
            End Set
        End Property


        <ColumnInfo("Price", "{0}")> _
        Public Property Price() As Decimal
            Get
                Return _price
            End Get
            Set(ByVal value As Decimal)
                _price = value
            End Set
        End Property


        <ColumnInfo("Stock", "{0}")> _
        Public Property Stock() As Integer
            Get
                Return _stock
            End Get
            Set(ByVal value As Integer)
                _stock = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
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

        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "MaterialPromotion", "ProductCategoryID")> _
              Public Property ProductCategory As ProductCategory
            Get
                Try
                    If Not isnothing(Me._productCategory) AndAlso (Not Me._productCategory.IsLoaded) Then

                        Me._productCategory = CType(DoLoad(GetType(ProductCategory).ToString(), _productCategory.ID), ProductCategory)
                        Me._productCategory.MarkLoaded()

                    End If

                    Return Me._productCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProductCategory)

                Me._productCategory = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._productCategory.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("MaterialPromotion", "ID", "MaterialPromotionPriceHistory", "MaterialPromotionID")> _
        Public ReadOnly Property MaterialPromotionPriceHistorys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._materialPromotionPriceHistorys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionPriceHistory), "MaterialPromotion", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MaterialPromotionPriceHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._materialPromotionPriceHistorys = DoLoadArray(GetType(MaterialPromotionPriceHistory).ToString, criterias)
                    End If

                    Return Me._materialPromotionPriceHistorys

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("MaterialPromotion", "ID", "MaterialPromotionGIGR", "MaterialPromotionID")> _
        Public ReadOnly Property MaterialPromotionGIGRs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._materialPromotionGIGRs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionGIGR), "MaterialPromotion", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._materialPromotionGIGRs = DoLoadArray(GetType(MaterialPromotionGIGR).ToString, criterias)
                    End If

                    Return Me._materialPromotionGIGRs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("MaterialPromotion", "ID", "MaterialPromotionRequestDetail", "MaterialPromotionID")> _
        Public ReadOnly Property MaterialPromotionRequestDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._materialPromotionRequestDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotion", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._materialPromotionRequestDetails = DoLoadArray(GetType(MaterialPromotionRequestDetail).ToString, criterias)
                    End If

                    Return Me._materialPromotionRequestDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("MaterialPromotion", "ID", "MaterialPromotionStockAdjustment", "MaterialPromotionID")> _
        Public ReadOnly Property MaterialPromotionStockAdjustments() As System.Collections.ArrayList
            Get
                Try
                    If (Me._materialPromotionStockAdjustments.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionStockAdjustment), "MaterialPromotion", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MaterialPromotionStockAdjustment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._materialPromotionStockAdjustments = DoLoadArray(GetType(MaterialPromotionStockAdjustment).ToString, criterias)
                    End If

                    Return Me._materialPromotionStockAdjustments

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property



        <RelationInfo("MaterialPromotion", "ID", "MaterialPromotionAllocation", "MaterialPromotionID")> _
        Public ReadOnly Property MaterialPromotionAllocations() As System.Collections.ArrayList
            Get
                Try
                    If (Me._materialPromotionAllocations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._materialPromotionAllocations = DoLoadArray(GetType(MaterialPromotionAllocation).ToString, criterias)
                    End If

                    Return Me._materialPromotionAllocations

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

