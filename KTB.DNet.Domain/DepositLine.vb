#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositLine Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 15/11/2005 - 14:20:35
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
    <Serializable(), TableInfo("DepositLine")> _
    Public Class DepositLine
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
        Private _documentNo As String = String.Empty
        Private _postingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _clearingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _debit As Decimal
        Private _credit As Decimal
        Private _referenceNo As String = String.Empty
        Private _invoiceNo As String = String.Empty
        Private _remark As String = String.Empty
        Private _paymentType As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _deposit As Deposit



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

        <ColumnInfo("DocumentNo", "'{0}'")> _
        Public Property DocumentNo() As String
            Get
                Return _documentNo
            End Get
            Set(ByVal value As String)
                _documentNo = value
            End Set
        End Property

        <ColumnInfo("PostingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PostingDate() As DateTime
            Get
                Return _postingDate
            End Get
            Set(ByVal value As DateTime)
                _postingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ClearingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ClearingDate() As DateTime
            Get
                Return _clearingDate
            End Get
            Set(ByVal value As DateTime)
                _clearingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("Debit", "{0}")> _
        Public Property Debit() As Decimal
            Get
                Return _debit
            End Get
            Set(ByVal value As Decimal)
                _debit = value
            End Set
        End Property

        <ColumnInfo("Credit", "{0}")> _
        Public Property Credit() As Decimal
            Get
                Return _credit
            End Get
            Set(ByVal value As Decimal)
                _credit = value
            End Set
        End Property

        <ColumnInfo("ReferenceNo", "'{0}'")> _
        Public Property ReferenceNo() As String
            Get
                Return _referenceNo
            End Get
            Set(ByVal value As String)
                _referenceNo = value
            End Set
        End Property

        <ColumnInfo("InvoiceNo", "'{0}'")> _
        Public Property InvoiceNo() As String
            Get
                Return _invoiceNo
            End Get
            Set(ByVal value As String)
                _invoiceNo = value
            End Set
        End Property

        <ColumnInfo("Remark", "'{0}'")> _
        Public Property Remark() As String
            Get
                Return _remark
            End Get
            Set(ByVal value As String)
                _remark = value
            End Set
        End Property



        <ColumnInfo("PaymentType", "{0}")> _
        Public Property PaymentType As Short
            Get
                Return _paymentType
            End Get
            Set(ByVal value As Short)
                _paymentType = value
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

        <ColumnInfo("DepositID", "{0}"), _
        RelationInfo("Deposit", "ID", "DepositLine", "DepositID")> _
        Public Property Deposit() As Deposit
            Get
                Try
                    If Not isnothing(Me._deposit) AndAlso (Not Me._deposit.IsLoaded) Then

                        Me._deposit = CType(DoLoad(GetType(Deposit).ToString(), _deposit.ID), Deposit)
                        Me._deposit.MarkLoaded()

                    End If

                    Return Me._deposit

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Deposit)

                Me._deposit = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._deposit.MarkLoaded()
                End If
            End Set
        End Property

#End Region

#Region "Custom Property"

        Public ReadOnly Property PostDateText() As String
            Get
                Return IIf(Format(PostingDate, "dd/MM/yyyy") = "01/01/1753", _
                           "", Format(PostingDate, "dd/MM/yyyy"))
            End Get
        End Property

        Public ReadOnly Property ClearDateText() As String
            Get
                Return IIf(Format(ClearingDate, "dd/MM/yyyy") = "01/01/1753", _
                           "", Format(ClearingDate, "dd/MM/yyyy"))
            End Get
        End Property

#End Region

    End Class
End Namespace
