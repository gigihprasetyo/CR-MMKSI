#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartMaster Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2007 - 8:07:11 AM
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
    <Serializable(), TableInfo("SparePartMaster")> _
    Public Class SparePartMaster
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
        Private _partNumber As String = String.Empty
        Private _partName As String = String.Empty
        Private _partNumberReff As String = String.Empty
        Private _uoM As String = String.Empty
        Private _materialCategoryCode As String = String.Empty
        Private _altPartNumber As String = String.Empty
        Private _altPartName As String = String.Empty
        Private _partCode As String = String.Empty
        Private _modelCode As String = String.Empty
        Private _supplierCode As String = String.Empty
        Private _typeCode As String = String.Empty
        Private _stock As Integer
        Private _retalPrice As Decimal
        Private _partStatus As String = String.Empty
        Private _productType As String = String.Empty
        Private _activeStatus As Short
        Private _accessoriesType As Short
        Private _isWarranty As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _indentPartDetails As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _pQRPartsCode As PQRPartsCode = New PQRPartsCode(0)

        Private _productCategory As ProductCategory

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


        <ColumnInfo("PartNumber", "'{0}'")> _
        Public Property PartNumber() As String
            Get
                Return _partNumber
            End Get
            Set(ByVal value As String)
                _partNumber = value
            End Set
        End Property

        <ColumnInfo("MaterialCategoryCode", "'{0}'")> _
        Public Property MaterialCategoryCode() As String
            Get
                Return _materialCategoryCode
            End Get
            Set(ByVal value As String)
                _materialCategoryCode = value
            End Set
        End Property

        <ColumnInfo("UoM", "'{0}'")> _
        Public Property UoM() As String
            Get
                Return _uoM
            End Get
            Set(ByVal value As String)
                _uoM = value
            End Set
        End Property

        <ColumnInfo("PartNumberReff", "'{0}'")> _
        Public Property PartNumberReff() As String
            Get
                Return _partNumberReff
            End Get
            Set(ByVal value As String)
                _partNumberReff = value
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


        <ColumnInfo("AltPartNumber", "'{0}'")> _
        Public Property AltPartNumber() As String
            Get
                Return _altPartNumber
            End Get
            Set(ByVal value As String)
                _altPartNumber = value
            End Set
        End Property


        <ColumnInfo("AltPartName", "'{0}'")> _
        Public Property AltPartName() As String
            Get
                Return _altPartName
            End Get
            Set(ByVal value As String)
                _altPartName = value
            End Set
        End Property


        <ColumnInfo("PartCode", "'{0}'")> _
        Public Property PartCode() As String
            Get
                Return _partCode
            End Get
            Set(ByVal value As String)
                _partCode = value
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

        'Modified By Ikhsan 20081120
        'Requested by Yurike, as Part OF CR
        'as Part of Additional information in domain control for Columns SupplierCode
        <ColumnInfo("SupplierCode", "'{0}'")> _
        Public Property SupplierCode() As String
            Get
                Return _supplierCode
            End Get
            Set(ByVal value As String)
                _supplierCode = value
            End Set
        End Property

        <ColumnInfo("TypeCode", "'{0}'")> _
        Public Property TypeCode() As String
            Get
                Return _typeCode
            End Get
            Set(ByVal value As String)
                _typeCode = value
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


        <ColumnInfo("RetalPrice", "{0}")> _
        Public Property RetalPrice() As Decimal
            Get
                Return _retalPrice
            End Get
            Set(ByVal value As Decimal)
                _retalPrice = value
            End Set
        End Property


        <ColumnInfo("PartStatus", "'{0}'")> _
        Public Property PartStatus() As String
            Get
                Return _partStatus
            End Get
            Set(ByVal value As String)
                _partStatus = value
            End Set
        End Property

        <ColumnInfo("ProductType", "'{0}'")> _
        Public Property ProductType() As String
            Get
                Return _productType
            End Get
            Set(ByVal value As String)
                _productType = value
            End Set
        End Property

        <ColumnInfo("ActiveStatus", "{0}")> _
        Public Property ActiveStatus() As Short
            Get
                Return _activeStatus
            End Get
            Set(ByVal value As Short)
                _activeStatus = value
            End Set
        End Property

        <ColumnInfo("AccessoriesType", "{0}")> _
        Public Property AccessoriesType() As Short
            Get
                Return _accessoriesType
            End Get
            Set(ByVal value As Short)
                _accessoriesType = value
            End Set
        End Property


        <ColumnInfo("IsWarranty", "{0}")> _
        Public Property IsWarranty() As Short
            Get
                Return _isWarranty
            End Get
            Set(ByVal value As Short)
                _isWarranty = value
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



        <RelationInfo("SparePartMaster", "ID", "IndentPartDetail", "SparePartMasterID")> _
        Public ReadOnly Property IndentPartDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._indentPartDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(IndentPartDetail), "SparePartMaster", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._indentPartDetails = DoLoadArray(GetType(IndentPartDetail).ToString, criterias)
                    End If

                    Return Me._indentPartDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <ColumnInfo("ID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "PQRPartsCode", "SparePartMasterID")> _
        Public ReadOnly Property PQRPartsCode() As PQRPartsCode
            Get
                Try
                    If Not IsNothing(Me._pQRPartsCode) AndAlso (Not Me._pQRPartsCode.IsLoaded) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRPartsCode), "SparePartMaster", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRPartsCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(PQRPartsCode).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._pQRPartsCode = CType(tempColl(0), PQRPartsCode)
                        Else
                            Me._pQRPartsCode = Nothing
                        End If
                    End If

                    Return Me._pQRPartsCode

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
        End Property

        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "SparePartMaster", "ProductCategoryID")> _
        Public Property ProductCategory() As ProductCategory
            Get
                Try
                    If Not IsNothing(Me._productCategory) AndAlso (Not Me._productCategory.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._productCategory.MarkLoaded()
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


#Region "Custom Properties"
        Private _stockSAP As String = String.Empty
        Public Property StockSAP() As String
            Get
                Return _stockSAP
            End Get
            Set(ByVal value As String)
                _stockSAP = value
            End Set
        End Property

        Private _maxStock As Integer
        Public Property MaxStock() As Integer
            Get
                Return _maxStock
            End Get
            Set(ByVal Value As Integer)
                _maxStock = Value
            End Set
        End Property
        Private _pesan As String
        Public Property Pesan() As String
            Get
                Return _pesan
            End Get
            Set(ByVal Value As String)
                _pesan = Value
            End Set
        End Property


#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

