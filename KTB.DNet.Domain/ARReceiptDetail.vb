
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : ARReceiptDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 23/03/2018 - 16:15:55
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
    <Serializable(), TableInfo("ARReceiptDetail")> _
    Public Class ARReceiptDetail
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
        Private _detailNo As String = String.Empty
        Private _aRReceiptNo As String = String.Empty
        Private _bU As String = String.Empty
        Private _changeAmount As Decimal
        Private _customer As String = String.Empty
        Private _description As String = String.Empty
        Private _differenceValue As Double
        Private _invoiceNo As String = String.Empty
        Private _orderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _orderNo As String = String.Empty
        Private _orderNoSO As String = String.Empty
        Private _orderNoUVSO As String = String.Empty
        Private _orderNoWO As String = String.Empty
        Private _outstandingBalance As Decimal
        Private _paidBackToCustomer As Boolean
        Private _receiptAmount As Decimal
        Private _remainingBalance As Decimal
        Private _sourceType As Short
        Private _transactionDocument As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _aRReceipt As ARReceipt



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


        <ColumnInfo("DetailNo", "'{0}'")> _
        Public Property DetailNo As String
            Get
                Return _detailNo
            End Get
            Set(ByVal value As String)
                _detailNo = value
            End Set
        End Property


        <ColumnInfo("ARReceiptNo", "'{0}'")> _
        Public Property ARReceiptNo As String
            Get
                Return _aRReceiptNo
            End Get
            Set(ByVal value As String)
                _aRReceiptNo = value
            End Set
        End Property


        <ColumnInfo("BU", "'{0}'")> _
        Public Property BU As String
            Get
                Return _bU
            End Get
            Set(ByVal value As String)
                _bU = value
            End Set
        End Property


        <ColumnInfo("ChangeAmount", "{0}")> _
        Public Property ChangeAmount As Decimal
            Get
                Return _changeAmount
            End Get
            Set(ByVal value As Decimal)
                _changeAmount = value
            End Set
        End Property


        <ColumnInfo("Customer", "'{0}'")> _
        Public Property Customer As String
            Get
                Return _customer
            End Get
            Set(ByVal value As String)
                _customer = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("DifferenceValue", "#,##0")> _
        Public Property DifferenceValue As Double
            Get
                Return _differenceValue
            End Get
            Set(ByVal value As Double)
                _differenceValue = value
            End Set
        End Property


        <ColumnInfo("InvoiceNo", "'{0}'")> _
        Public Property InvoiceNo As String
            Get
                Return _invoiceNo
            End Get
            Set(ByVal value As String)
                _invoiceNo = value
            End Set
        End Property


        <ColumnInfo("OrderDate", "'{0:yyyy/MM/dd}'")> _
        Public Property OrderDate As DateTime
            Get
                Return _orderDate
            End Get
            Set(ByVal value As DateTime)
                _orderDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("OrderNo", "'{0}'")> _
        Public Property OrderNo As String
            Get
                Return _orderNo
            End Get
            Set(ByVal value As String)
                _orderNo = value
            End Set
        End Property


        <ColumnInfo("OrderNoSO", "'{0}'")> _
        Public Property OrderNoSO As String
            Get
                Return _orderNoSO
            End Get
            Set(ByVal value As String)
                _orderNoSO = value
            End Set
        End Property


        <ColumnInfo("OrderNoUVSO", "'{0}'")> _
        Public Property OrderNoUVSO As String
            Get
                Return _orderNoUVSO
            End Get
            Set(ByVal value As String)
                _orderNoUVSO = value
            End Set
        End Property


        <ColumnInfo("OrderNoWO", "'{0}'")> _
        Public Property OrderNoWO As String
            Get
                Return _orderNoWO
            End Get
            Set(ByVal value As String)
                _orderNoWO = value
            End Set
        End Property


        <ColumnInfo("OutstandingBalance", "{0}")> _
        Public Property OutstandingBalance As Decimal
            Get
                Return _outstandingBalance
            End Get
            Set(ByVal value As Decimal)
                _outstandingBalance = value
            End Set
        End Property


        <ColumnInfo("PaidBackToCustomer", "{0}")> _
        Public Property PaidBackToCustomer As Boolean
            Get
                Return _paidBackToCustomer
            End Get
            Set(ByVal value As Boolean)
                _paidBackToCustomer = value
            End Set
        End Property


        <ColumnInfo("ReceiptAmount", "{0}")> _
        Public Property ReceiptAmount As Decimal
            Get
                Return _receiptAmount
            End Get
            Set(ByVal value As Decimal)
                _receiptAmount = value
            End Set
        End Property


        <ColumnInfo("RemainingBalance", "{0}")> _
        Public Property RemainingBalance As Decimal
            Get
                Return _remainingBalance
            End Get
            Set(ByVal value As Decimal)
                _remainingBalance = value
            End Set
        End Property


        <ColumnInfo("SourceType", "{0}")> _
        Public Property SourceType As Short
            Get
                Return _sourceType
            End Get
            Set(ByVal value As Short)
                _sourceType = value
            End Set
        End Property


        <ColumnInfo("TransactionDocument", "'{0}'")> _
        Public Property TransactionDocument As String
            Get
                Return _transactionDocument
            End Get
            Set(ByVal value As String)
                _transactionDocument = value
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


        <ColumnInfo("ARReceiptID", "{0}"), _
        RelationInfo("ARReceipt", "ID", "ARReceiptDetail", "ARReceiptID")> _
        Public Property ARReceipt As ARReceipt
            Get
                Try
                    If Not IsNothing(Me._aRReceipt) AndAlso (Not Me._aRReceipt.IsLoaded) Then

                        Me._aRReceipt = CType(DoLoad(GetType(ARReceipt).ToString(), _aRReceipt.ID), ARReceipt)
                        Me._aRReceipt.MarkLoaded()

                    End If

                    Return Me._aRReceipt

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ARReceipt)

                Me._aRReceipt = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._aRReceipt.MarkLoaded()
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

