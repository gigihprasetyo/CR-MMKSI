
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VehiclePurchaseDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 17:22:38
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
    <Serializable(), TableInfo("VehiclePurchaseDetail")> _
    Public Class VehiclePurchaseDetail
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
        Private _bUCode As String = String.Empty
        Private _bUName As String = String.Empty
        Private _closeLine As Boolean
        Private _closeLineName As String = String.Empty
        Private _closeReason As String = String.Empty
        Private _completed As Boolean
        Private _completedName As String = String.Empty
        Private _productDescription As String = String.Empty
        Private _productName As String = String.Empty
        Private _productVariantName As String = String.Empty
        Private _pODetail As String = String.Empty
        Private _pOName As String = String.Empty
        Private _pRDetailName As String = String.Empty
        Private _purchaseUnitName As String = String.Empty
        Private _qtyOrder As Double
        Private _qtyReceipt As Double
        Private _qtyReturn As Double
        Private _recallProduct As Boolean
        Private _recallProductName As String = String.Empty
        Private _sODetailName As String = String.Empty
        Private _scheduledShippingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _servicePartsAndMaterial As String = String.Empty
        Private _shippingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _site As String = String.Empty
        Private _stockNumberName As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _vehiclePurchaseHeader As VehiclePurchaseHeader



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


        <ColumnInfo("BUCode", "'{0}'")> _
        Public Property BUCode As String
            Get
                Return _bUCode
            End Get
            Set(ByVal value As String)
                _bUCode = value
            End Set
        End Property


        <ColumnInfo("BUName", "'{0}'")> _
        Public Property BUName As String
            Get
                Return _bUName
            End Get
            Set(ByVal value As String)
                _bUName = value
            End Set
        End Property


        <ColumnInfo("CloseLine", "{0}")> _
        Public Property CloseLine As Boolean
            Get
                Return _closeLine
            End Get
            Set(ByVal value As Boolean)
                _closeLine = value
            End Set
        End Property


        <ColumnInfo("CloseLineName", "'{0}'")> _
        Public Property CloseLineName As String
            Get
                Return _closeLineName
            End Get
            Set(ByVal value As String)
                _closeLineName = value
            End Set
        End Property


        <ColumnInfo("CloseReason", "'{0}'")> _
        Public Property CloseReason As String
            Get
                Return _closeReason
            End Get
            Set(ByVal value As String)
                _closeReason = value
            End Set
        End Property


        <ColumnInfo("Completed", "{0}")> _
        Public Property Completed As Boolean
            Get
                Return _completed
            End Get
            Set(ByVal value As Boolean)
                _completed = value
            End Set
        End Property


        <ColumnInfo("CompletedName", "'{0}'")> _
        Public Property CompletedName As String
            Get
                Return _completedName
            End Get
            Set(ByVal value As String)
                _completedName = value
            End Set
        End Property


        <ColumnInfo("ProductDescription", "'{0}'")> _
        Public Property ProductDescription As String
            Get
                Return _productDescription
            End Get
            Set(ByVal value As String)
                _productDescription = value
            End Set
        End Property


        <ColumnInfo("ProductName", "'{0}'")> _
        Public Property ProductName As String
            Get
                Return _productName
            End Get
            Set(ByVal value As String)
                _productName = value
            End Set
        End Property


        <ColumnInfo("ProductVariantName", "'{0}'")> _
        Public Property ProductVariantName As String
            Get
                Return _productVariantName
            End Get
            Set(ByVal value As String)
                _productVariantName = value
            End Set
        End Property


        <ColumnInfo("PODetail", "'{0}'")> _
        Public Property PODetail As String
            Get
                Return _pODetail
            End Get
            Set(ByVal value As String)
                _pODetail = value
            End Set
        End Property


        <ColumnInfo("POName", "'{0}'")> _
        Public Property POName As String
            Get
                Return _pOName
            End Get
            Set(ByVal value As String)
                _pOName = value
            End Set
        End Property


        <ColumnInfo("PRDetailName", "'{0}'")> _
        Public Property PRDetailName As String
            Get
                Return _pRDetailName
            End Get
            Set(ByVal value As String)
                _pRDetailName = value
            End Set
        End Property


        <ColumnInfo("PurchaseUnitName", "'{0}'")> _
        Public Property PurchaseUnitName As String
            Get
                Return _purchaseUnitName
            End Get
            Set(ByVal value As String)
                _purchaseUnitName = value
            End Set
        End Property


        <ColumnInfo("QtyOrder", "#,##0")> _
        Public Property QtyOrder As Double
            Get
                Return _qtyOrder
            End Get
            Set(ByVal value As Double)
                _qtyOrder = value
            End Set
        End Property


        <ColumnInfo("QtyReceipt", "#,##0")> _
        Public Property QtyReceipt As Double
            Get
                Return _qtyReceipt
            End Get
            Set(ByVal value As Double)
                _qtyReceipt = value
            End Set
        End Property


        <ColumnInfo("QtyReturn", "#,##0")> _
        Public Property QtyReturn As Double
            Get
                Return _qtyReturn
            End Get
            Set(ByVal value As Double)
                _qtyReturn = value
            End Set
        End Property


        <ColumnInfo("RecallProduct", "{0}")> _
        Public Property RecallProduct As Boolean
            Get
                Return _recallProduct
            End Get
            Set(ByVal value As Boolean)
                _recallProduct = value
            End Set
        End Property


        <ColumnInfo("RecallProductName", "'{0}'")> _
        Public Property RecallProductName As String
            Get
                Return _recallProductName
            End Get
            Set(ByVal value As String)
                _recallProductName = value
            End Set
        End Property


        <ColumnInfo("SODetailName", "'{0}'")> _
        Public Property SODetailName As String
            Get
                Return _sODetailName
            End Get
            Set(ByVal value As String)
                _sODetailName = value
            End Set
        End Property


        <ColumnInfo("ScheduledShippingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ScheduledShippingDate As DateTime
            Get
                Return _scheduledShippingDate
            End Get
            Set(ByVal value As DateTime)
                _scheduledShippingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ServicePartsAndMaterial", "'{0}'")> _
        Public Property ServicePartsAndMaterial As String
            Get
                Return _servicePartsAndMaterial
            End Get
            Set(ByVal value As String)
                _servicePartsAndMaterial = value
            End Set
        End Property


        <ColumnInfo("ShippingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ShippingDate As DateTime
            Get
                Return _shippingDate
            End Get
            Set(ByVal value As DateTime)
                _shippingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Site", "'{0}'")> _
        Public Property Site As String
            Get
                Return _site
            End Get
            Set(ByVal value As String)
                _site = value
            End Set
        End Property


        <ColumnInfo("StockNumberName", "'{0}'")> _
        Public Property StockNumberName As String
            Get
                Return _stockNumberName
            End Get
            Set(ByVal value As String)
                _stockNumberName = value
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


        <ColumnInfo("VehiclePurchaseHeaderID", "{0}"), _
        RelationInfo("VehiclePurchaseHeader", "ID", "VehiclePurchaseDetail", "VehiclePurchaseHeaderID")> _
        Public Property VehiclePurchaseHeader As VehiclePurchaseHeader
            Get
                Try
                    If Not isnothing(Me._vehiclePurchaseHeader) AndAlso (Not Me._vehiclePurchaseHeader.IsLoaded) Then

                        Me._vehiclePurchaseHeader = CType(DoLoad(GetType(VehiclePurchaseHeader).ToString(), _vehiclePurchaseHeader.ID), VehiclePurchaseHeader)
                        Me._vehiclePurchaseHeader.MarkLoaded()

                    End If

                    Return Me._vehiclePurchaseHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VehiclePurchaseHeader)

                Me._vehiclePurchaseHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vehiclePurchaseHeader.MarkLoaded()
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

