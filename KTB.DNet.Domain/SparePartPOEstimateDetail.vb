
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOEstimateDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 10/27/2015 - 3:21:39 PM
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
    <Serializable(), TableInfo("SparePartPOEstimateDetail")> _
    Public Class SparePartPOEstimateDetail
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

        Private _sparePartPOEstimate As SparePartPOEstimate



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


        <ColumnInfo("SparePartPOEstimateID", "{0}"), _
        RelationInfo("SparePartPOEstimate", "ID", "SparePartPOEstimateDetail", "SparePartPOEstimateID")> _
        Public Property SparePartPOEstimate() As SparePartPOEstimate
            Get
                Try
                    If Not isnothing(Me._sparePartPOEstimate) AndAlso (Not Me._sparePartPOEstimate.IsLoaded) Then

                        Me._sparePartPOEstimate = CType(DoLoad(GetType(SparePartPOEstimate).ToString(), _sparePartPOEstimate.ID), SparePartPOEstimate)
                        Me._sparePartPOEstimate.MarkLoaded()

                    End If

                    Return Me._sparePartPOEstimate

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartPOEstimate)

                Me._sparePartPOEstimate = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPOEstimate.MarkLoaded()
                End If
            End Set
        End Property




#End Region

#Region "Custom Properties"
        <ColumnInfo("Amount", "{0}")> _
        Public ReadOnly Property Amount() As Decimal
            Get
                Return _allocQty * _retailPrice
            End Get
        End Property

        <ColumnInfo("TotalAmount", "{0}")> _
        Public ReadOnly Property TotalAmount() As Decimal
            Get
                Return ((_allocQty * _retailPrice) * (100 - _discount)) / 100
            End Get
        End Property



#End Region
#Region "Custom Method"

#End Region

    End Class
End Namespace

