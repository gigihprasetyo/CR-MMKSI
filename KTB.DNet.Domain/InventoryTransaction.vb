
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : InventoryTransaction Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 16/03/2018 - 9:03:33
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
    <Serializable(), TableInfo("InventoryTransaction")> _
    Public Class InventoryTransaction
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
        Private _dealerCode As String = String.Empty
        Private _inventoryTransactionNo As String = String.Empty
        Private _inventoryTransferNo As String = String.Empty
        Private _personInCharge As String = String.Empty
        Private _processCode As String = String.Empty
        Private _sourceData As String = String.Empty
        Private _state As Short
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _transactionType As Short
        Private _wONo As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _inventoryTransactionDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("InventoryTransactionNo", "'{0}'")> _
        Public Property InventoryTransactionNo As String
            Get
                Return _inventoryTransactionNo
            End Get
            Set(ByVal value As String)
                _inventoryTransactionNo = value
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


        <ColumnInfo("PersonInCharge", "'{0}'")> _
        Public Property PersonInCharge As String
            Get
                Return _personInCharge
            End Get
            Set(ByVal value As String)
                _personInCharge = value
            End Set
        End Property


        <ColumnInfo("ProcessCode", "'{0}'")> _
        Public Property ProcessCode As String
            Get
                Return _processCode
            End Get
            Set(ByVal value As String)
                _processCode = value
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



        <RelationInfo("InventoryTransaction", "ID", "InventoryTransactionDetail", "InventoryTransactionID")> _
        Public ReadOnly Property InventoryTransactionDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._inventoryTransactionDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(InventoryTransactionDetail), "InventoryTransaction", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(InventoryTransactionDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._inventoryTransactionDetails = DoLoadArray(GetType(InventoryTransactionDetail).ToString, criterias)
                    End If

                    Return Me._inventoryTransactionDetails

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

