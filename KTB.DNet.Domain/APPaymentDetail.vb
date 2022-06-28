
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : APPaymentDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/03/2018 - 10:35:34
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
    <Serializable(), TableInfo("APPaymentDetail")> _
    Public Class APPaymentDetail
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
        Private _aPPaymentDetailNo As String = String.Empty
        Private _aPPaymentNo As String = String.Empty
        Private _bU As String = String.Empty
        Private _changeAmount As Decimal
        Private _description As String = String.Empty
        Private _differenceValue As Double
        Private _externalDocumentNo As String = String.Empty
        Private _externalDocumentType As Short
        Private _aPVoucherNo As String = String.Empty
        Private _orderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _orderNoNVSOReferral As String = String.Empty
        Private _orderNoOutsourceWorkOrder As String = String.Empty
        Private _orderNo As String = String.Empty
        Private _orderNoUVSOReferral As String = String.Empty
        Private _outstandingBalance As Decimal
        Private _paymentAmount As Decimal
        Private _paymentSlipNo As String = String.Empty
        Private _receiptFromVendor As Boolean
        Private _remainingBalance As Decimal
        Private _sourceType As Short
        Private _transactionDocument As String = String.Empty
        Private _vendor As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _aPPayment As APPayment



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


        <ColumnInfo("APPaymentDetailNo", "'{0}'")> _
        Public Property APPaymentDetailNo As String
            Get
                Return _aPPaymentDetailNo
            End Get
            Set(ByVal value As String)
                _aPPaymentDetailNo = value
            End Set
        End Property


        <ColumnInfo("APPaymentNo", "'{0}'")> _
        Public Property APPaymentNo As String
            Get
                Return _aPPaymentNo
            End Get
            Set(ByVal value As String)
                _aPPaymentNo = value
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


        <ColumnInfo("ExternalDocumentNo", "'{0}'")> _
        Public Property ExternalDocumentNo As String
            Get
                Return _externalDocumentNo
            End Get
            Set(ByVal value As String)
                _externalDocumentNo = value
            End Set
        End Property


        <ColumnInfo("ExternalDocumentType", "{0}")> _
        Public Property ExternalDocumentType As Short
            Get
                Return _externalDocumentType
            End Get
            Set(ByVal value As Short)
                _externalDocumentType = value
            End Set
        End Property


        <ColumnInfo("APVoucherNo", "'{0}'")> _
        Public Property APVoucherNo As String
            Get
                Return _aPVoucherNo
            End Get
            Set(ByVal value As String)
                _aPVoucherNo = value
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


        <ColumnInfo("OrderNoNVSOReferral", "'{0}'")> _
        Public Property OrderNoNVSOReferral As String
            Get
                Return _orderNoNVSOReferral
            End Get
            Set(ByVal value As String)
                _orderNoNVSOReferral = value
            End Set
        End Property


        <ColumnInfo("OrderNoOutsourceWorkOrder", "'{0}'")> _
        Public Property OrderNoOutsourceWorkOrder As String
            Get
                Return _orderNoOutsourceWorkOrder
            End Get
            Set(ByVal value As String)
                _orderNoOutsourceWorkOrder = value
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


        <ColumnInfo("OrderNoUVSOReferral", "'{0}'")> _
        Public Property OrderNoUVSOReferral As String
            Get
                Return _orderNoUVSOReferral
            End Get
            Set(ByVal value As String)
                _orderNoUVSOReferral = value
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


        <ColumnInfo("PaymentAmount", "{0}")> _
        Public Property PaymentAmount As Decimal
            Get
                Return _paymentAmount
            End Get
            Set(ByVal value As Decimal)
                _paymentAmount = value
            End Set
        End Property


        <ColumnInfo("PaymentSlipNo", "'{0}'")> _
        Public Property PaymentSlipNo As String
            Get
                Return _paymentSlipNo
            End Get
            Set(ByVal value As String)
                _paymentSlipNo = value
            End Set
        End Property


        <ColumnInfo("ReceiptFromVendor", "{0}")> _
        Public Property ReceiptFromVendor As Boolean
            Get
                Return _receiptFromVendor
            End Get
            Set(ByVal value As Boolean)
                _receiptFromVendor = value
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


        <ColumnInfo("Vendor", "'{0}'")> _
        Public Property Vendor As String
            Get
                Return _vendor
            End Get
            Set(ByVal value As String)
                _vendor = value
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


        <ColumnInfo("APPaymentID", "{0}"), _
        RelationInfo("APPayment", "ID", "APPaymentDetail", "APPaymentID")> _
        Public Property APPayment As APPayment
            Get
                Try
                    If Not IsNothing(Me._aPPayment) AndAlso (Not Me._aPPayment.IsLoaded) Then

                        Me._aPPayment = CType(DoLoad(GetType(APPayment).ToString(), _aPPayment.ID), APPayment)
                        Me._aPPayment.MarkLoaded()

                    End If

                    Return Me._aPPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As APPayment)

                Me._aPPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._aPPayment.MarkLoaded()
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

