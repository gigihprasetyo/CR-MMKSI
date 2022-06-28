
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartOutstandingOrderDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 10/26/2015 - 4:29:47 PM
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
    <Serializable(), TableInfo("SparePartOutstandingOrderDetail")> _
    Public Class SparePartOutstandingOrderDetail
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
        Private _allocationQty As Integer
        Private _allocationAmount As Decimal
        Private _openQty As Integer
        Private _openAmount As Decimal
        Private _subtitutePartNumber As String = String.Empty
        Private _isTransfer As Short
        Private _estimateFillQty As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _estimateFillDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Short

        Private _sparePartOutstandingOrder As SparePartOutstandingOrder

        Private _sparePart As SparePartMaster


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


        <ColumnInfo("PartNumber", "'{0}'")> _
        Public Property PartNumber As String
            Get
                Return _partNumber
            End Get
            Set(ByVal value As String)
                _partNumber = value
            End Set
        End Property


        <ColumnInfo("PartName", "'{0}'")> _
        Public Property PartName As String
            Get
                Return _partName
            End Get
            Set(ByVal value As String)
                _partName = value
            End Set
        End Property


        <ColumnInfo("OrderQty", "{0}")> _
        Public Property OrderQty As Integer
            Get
                Return _orderQty
            End Get
            Set(ByVal value As Integer)
                _orderQty = value
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


        <ColumnInfo("AllocationAmount", "{0}")> _
        Public Property AllocationAmount As Decimal
            Get
                Return _allocationAmount
            End Get
            Set(ByVal value As Decimal)
                _allocationAmount = value
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


        <ColumnInfo("OpenAmount", "{0}")> _
        Public Property OpenAmount As Decimal
            Get
                Return _openAmount
            End Get
            Set(ByVal value As Decimal)
                _openAmount = value
            End Set
        End Property


        <ColumnInfo("SubtitutePartNumber", "'{0}'")> _
        Public Property SubtitutePartNumber As String
            Get
                Return _subtitutePartNumber
            End Get
            Set(ByVal value As String)
                _subtitutePartNumber = value
            End Set
        End Property


        <ColumnInfo("EstimateFillDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EstimateFillDate As DateTime
            Get
                Return _estimateFillDate
            End Get
            Set(ByVal value As DateTime)
                _estimateFillDate = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("IsTransfer", "{0}")> _
        Public Property IsTransfer As Short
            Get
                Return _isTransfer
            End Get
            Set(ByVal value As Short)
                _isTransfer = value
            End Set
        End Property


        <ColumnInfo("EstimateFillQty", "{0}")> _
        Public Property EstimateFillQty As Integer
            Get
                Return _estimateFillQty
            End Get
            Set(ByVal value As Integer)
                _estimateFillQty = value
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


        <ColumnInfo("SparePartOutstandingOrderID", "{0}"), _
        RelationInfo("SparePartOutstandingOrder", "ID", "SparePartOutstandingOrderDetail", "SparePartOutstandingOrderID")> _
        Public Property SparePartOutstandingOrder As SparePartOutstandingOrder
            Get
                Try
                    If Not IsNothing(Me._sparePartOutstandingOrder) AndAlso (Not Me._sparePartOutstandingOrder.IsLoaded) Then

                        Me._sparePartOutstandingOrder = CType(DoLoad(GetType(SparePartOutstandingOrder).ToString(), _sparePartOutstandingOrder.ID), SparePartOutstandingOrder)
                        Me._sparePartOutstandingOrder.MarkLoaded()

                    End If

                    Return Me._sparePartOutstandingOrder

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartOutstandingOrder)

                Me._sparePartOutstandingOrder = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartOutstandingOrder.MarkLoaded()
                End If
            End Set
        End Property


        Public Property SparePart As SparePartMaster
            Get
                Return _sparePart
            End Get
            Set(value As SparePartMaster)
                _sparePart = value
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

