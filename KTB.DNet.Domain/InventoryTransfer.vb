
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : InventoryTransfer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 25/03/2018 - 21:47:18
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
    <Serializable(), TableInfo("InventoryTransfer")> _
    Public Class InventoryTransfer
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
        Private _owner As String = String.Empty
        Private _fromDealer As String = String.Empty
        Private _fromSite As String = String.Empty
        Private _inventoryTransferNo As String = String.Empty
        Private _itemTypeForTransfer As Short
        Private _personInCharge As String = String.Empty
        Private _receiptDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _receiptNo As String = String.Empty
        Private _referenceNo As String = String.Empty
        Private _searchVehicle As String = String.Empty
        Private _sourceData As String = String.Empty
        Private _state As Short
        Private _toDealer As String = String.Empty
        Private _toSite As String = String.Empty
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _transactionType As Short
        Private _transferStatus As Short
        Private _transferStep As Boolean
        Private _wONo As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _inventoryTransferDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("Owner", "'{0}'")> _
        Public Property Owner As String
            Get
                Return _owner
            End Get
            Set(ByVal value As String)
                _owner = value
            End Set
        End Property


        <ColumnInfo("FromDealer", "'{0}'")> _
        Public Property FromDealer As String
            Get
                Return _fromDealer
            End Get
            Set(ByVal value As String)
                _fromDealer = value
            End Set
        End Property


        <ColumnInfo("FromSite", "'{0}'")> _
        Public Property FromSite As String
            Get
                Return _fromSite
            End Get
            Set(ByVal value As String)
                _fromSite = value
            End Set
        End Property


        <ColumnInfo("InventoryTransferNo", "'{0}'")> _
        Public Property InventoryTransferNo As String
            Get
                Return _inventoryTransferNo
            End Get
            Set(ByVal value As String)
                _inventoryTransferNo = value
            End Set
        End Property


        <ColumnInfo("ItemTypeForTransfer", "{0}")> _
        Public Property ItemTypeForTransfer As Short
            Get
                Return _itemTypeForTransfer
            End Get
            Set(ByVal value As Short)
                _itemTypeForTransfer = value
            End Set
        End Property


        <ColumnInfo("PersonInCharge", "'{0}'")> _
        Public Property PersonInCharge As String
            Get
                Return _personInCharge
            End Get
            Set(ByVal value As String)
                _personInCharge = value
            End Set
        End Property


        <ColumnInfo("ReceiptDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReceiptDate As DateTime
            Get
                Return _receiptDate
            End Get
            Set(ByVal value As DateTime)
                _receiptDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ReceiptNo", "'{0}'")> _
        Public Property ReceiptNo As String
            Get
                Return _receiptNo
            End Get
            Set(ByVal value As String)
                _receiptNo = value
            End Set
        End Property


        <ColumnInfo("ReferenceNo", "'{0}'")> _
        Public Property ReferenceNo As String
            Get
                Return _referenceNo
            End Get
            Set(ByVal value As String)
                _referenceNo = value
            End Set
        End Property


        <ColumnInfo("SearchVehicle", "'{0}'")> _
        Public Property SearchVehicle As String
            Get
                Return _searchVehicle
            End Get
            Set(ByVal value As String)
                _searchVehicle = value
            End Set
        End Property


        <ColumnInfo("SourceData", "'{0}'")> _
        Public Property SourceData As String
            Get
                Return _sourceData
            End Get
            Set(ByVal value As String)
                _sourceData = value
            End Set
        End Property


        <ColumnInfo("State", "{0}")> _
        Public Property State As Short
            Get
                Return _state
            End Get
            Set(ByVal value As Short)
                _state = value
            End Set
        End Property


        <ColumnInfo("ToDealer", "'{0}'")> _
        Public Property ToDealer As String
            Get
                Return _toDealer
            End Get
            Set(ByVal value As String)
                _toDealer = value
            End Set
        End Property


        <ColumnInfo("ToSite", "'{0}'")> _
        Public Property ToSite As String
            Get
                Return _toSite
            End Get
            Set(ByVal value As String)
                _toSite = value
            End Set
        End Property


        <ColumnInfo("TransactionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransactionDate As DateTime
            Get
                Return _transactionDate
            End Get
            Set(ByVal value As DateTime)
                _transactionDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("TransactionType", "{0}")> _
        Public Property TransactionType As Short
            Get
                Return _transactionType
            End Get
            Set(ByVal value As Short)
                _transactionType = value
            End Set
        End Property


        <ColumnInfo("TransferStatus", "{0}")> _
        Public Property TransferStatus As Short
            Get
                Return _transferStatus
            End Get
            Set(ByVal value As Short)
                _transferStatus = value
            End Set
        End Property


        <ColumnInfo("TransferStep", "{0}")> _
        Public Property TransferStep As Boolean
            Get
                Return _transferStep
            End Get
            Set(ByVal value As Boolean)
                _transferStep = value
            End Set
        End Property


        <ColumnInfo("WONo", "'{0}'")> _
        Public Property WONo As String
            Get
                Return _wONo
            End Get
            Set(ByVal value As String)
                _wONo = value
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



        <RelationInfo("InventoryTransfer", "ID", "InventoryTransferDetail", "InventoryTransferID")> _
        Public ReadOnly Property InventoryTransferDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._inventoryTransferDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(InventoryTransferDetail), "InventoryTransfer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(InventoryTransferDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._inventoryTransferDetails = DoLoadArray(GetType(InventoryTransferDetail).ToString, criterias)
                    End If

                    Return Me._inventoryTransferDetails

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

