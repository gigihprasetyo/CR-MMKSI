#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_POEstimateHaveBillingDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 03/08/2019 - 3:21:39 PM
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
    <Serializable(), TableInfo("VWI_POEstimateHaveBillingDetail")> _
    Public Class VWI_POEstimateHaveBillingDetail
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
        Private _orderQty As Integer
        Private _allocQty As Integer
        Private _allocationQty As Integer
        Private _openQty As Integer
        Private _retailPrice As Decimal
        Private _altPartNumber As String = String.Empty
        Private _discount As Decimal
        Private _itemStatus As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sparePartPOEstimateID As Integer

        Private _dealerCode As String = String.Empty
        Private _uOM As String = String.Empty
        Private _tax As Decimal
        Private _activeStatus As Short
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

        <ColumnInfo("PartName", "'{0}'")> _
        Public Property PartName() As String
            Get
                Return _partName
            End Get
            Set(ByVal value As String)
                _partName = value
            End Set
        End Property

        <ColumnInfo("OrderQty", "{0}")> _
        Public Property OrderQty() As Integer
            Get
                Return _orderQty
            End Get
            Set(ByVal value As Integer)
                _orderQty = value
            End Set
        End Property

        <ColumnInfo("AllocQty", "{0}")> _
        Public Property AllocQty() As Integer
            Get
                Return _allocQty
            End Get
            Set(ByVal value As Integer)
                _allocQty = value
            End Set
        End Property

        <ColumnInfo("AllocationQty", "{0}")> _
        Public Property AllocationQty As Integer
            Get
                Return _allocationQty
            End Get
            Set(ByVal value As Integer)
                _allocationQty = value
            End Set
        End Property

        <ColumnInfo("OpenQty", "{0}")> _
        Public Property OpenQty As Integer
            Get
                Return _openQty
            End Get
            Set(ByVal value As Integer)
                _openQty = value
            End Set
        End Property

        <ColumnInfo("RetailPrice", "{0}")> _
        Public Property RetailPrice() As Decimal
            Get
                Return _retailPrice
            End Get
            Set(ByVal value As Decimal)
                _retailPrice = value
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

        <ColumnInfo("Discount", "{0}")> _
        Public Property Discount() As Decimal
            Get
                Return _discount
            End Get
            Set(ByVal value As Decimal)
                _discount = value
            End Set
        End Property

        <ColumnInfo("ItemStatus", "'{0}'")> _
        Public Property ItemStatus() As String
            Get
                Return _itemStatus
            End Get
            Set(ByVal value As String)
                _itemStatus = value
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

        <ColumnInfo("SparePartPOEstimateID", "{0}")> _
        Public Property SparePartPOEstimateID() As Integer
            Get
                Return _sparePartPOEstimateID
            End Get
            Set(ByVal value As Integer)
                _sparePartPOEstimateID = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("UOM", "'{0}'")> _
        Public Property UOM() As String
            Get
                Return _uOM
            End Get
            Set(ByVal value As String)
                _uOM = value
            End Set
        End Property

        <ColumnInfo("Tax", "{0}")> _
        Public Property Tax() As Decimal
            Get
                Return _tax
            End Get
            Set(ByVal value As Decimal)
                _tax = value
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

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


