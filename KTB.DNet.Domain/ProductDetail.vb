#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : ProductDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/22/2005 - 5:41:02 AM
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
    <Serializable(), TableInfo("ProductDetail")> _
    Public Class ProductDetail
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
        Private _productID As Integer
        Private _description As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _rowStatus As Short
        Private _lockedBy As String = String.Empty

        Private _basicProductID As Integer

        Private _basicProduct As BasicProduct

        Private _product As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("ProductID", "{0}")> _
        Public Property ProductID() As Integer
            Get
                Return _productID
            End Get
            Set(ByVal value As Integer)
                _productID = value
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


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("LockedBy", "'{0}'")> _
        Public Property LockedBy() As String
            Get
                Return _lockedBy
            End Get
            Set(ByVal value As String)
                _lockedBy = value
            End Set
        End Property


        <ColumnInfo("BasicProductID", "{0}")> _
        Public Property BasicProductID() As Integer
            Get
                Return _basicProductID
            End Get
            Set(ByVal value As Integer)
                _basicProductID = value
            End Set
        End Property


        <ColumnInfo("BasicProductID", "{0}"), RelationInfo("BasicProduct", "ID", "ProductDetail", "BasicProductID")> _
  Public Property BasicProduct() As BasicProduct
            Get
                Try
                    If Not IsNothing(Me._basicProduct) And (Not Me._basicProduct.IsLoaded) Then

                        Me._basicProduct = CType(DoLoad(GetType(BasicProduct).ToString(), _basicProduct.ID), BasicProduct)
                        Me._basicProduct.MarkLoaded()

                    End If

                    Return Me._basicProduct

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BasicProduct)

                Me._basicProduct = value
                If (Not IsNothing(value)) And (CType(value, DomainObject).IsLoaded) Then
                    Me._basicProduct.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("ProductDetail", "ID", "Product", "ID")> _
        Public ReadOnly Property Product() As System.Collections.ArrayList
            Get
                Try
                    If (Me._product.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(Product), "ProductDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(Product), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._product = DoLoadArray(GetType(Product).ToString, criterias)
                    End If

                    Return Me._product

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

#Region "Custom Method"

#End Region

    End Class
End Namespace