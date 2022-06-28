
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimReceipt Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 10:56:00 AM
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
    <Serializable(), TableInfo("BenefitClaimReceipt")> _
    Public Class BenefitClaimReceipt
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
        Private _receiptNo As String = String.Empty
        Private _receiptDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturPajakDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _receiptAmount As Decimal
        Private _receiptAmountDeducted As Decimal
        Private _vATTotal As Decimal
        Private _pPHTotal As Decimal
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _fakturPajakNo As String = String.Empty
        Private _name As String = String.Empty
        Private _title As String = String.Empty

        Private _benefitClaimHeader As BenefitClaimHeader
        Private _dealerBankAccount As DealerBankAccount
        'Private _dealerBankAccountID As Integer


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


        <ColumnInfo("ReceiptNo", "'{0}'")> _
        Public Property ReceiptNo As String
            Get
                Return _receiptNo
            End Get
            Set(ByVal value As String)
                _receiptNo = value
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

        <ColumnInfo("FakturPajakDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FakturPajakDate As DateTime
            Get
                Return _fakturPajakDate
            End Get
            Set(ByVal value As DateTime)
                _fakturPajakDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("ReceiptAmountDeducted", "{0}")> _
        Public Property ReceiptAmountDeducted As Decimal
            Get
                Return _receiptAmountDeducted
            End Get
            Set(ByVal value As Decimal)
                _receiptAmountDeducted = value
            End Set
        End Property

        <ColumnInfo("VATTotal", "{0}")> _
        Public Property VATTotal As Decimal
            Get
                Return _vATTotal
            End Get
            Set(ByVal value As Decimal)
                _vATTotal = value
            End Set
        End Property


        <ColumnInfo("PPHTotal", "{0}")> _
        Public Property PPHTotal As Decimal
            Get
                Return _pPHTotal
            End Get
            Set(ByVal value As Decimal)
                _pPHTotal = value
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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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


        <ColumnInfo("FakturPajakNo", "'{0}'")> _
        Public Property FakturPajakNo As String
            Get
                Return _fakturPajakNo
            End Get
            Set(ByVal value As String)
                _fakturPajakNo = value
            End Set
        End Property

        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        <ColumnInfo("Title", "'{0}'")> _
        Public Property Title As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        <ColumnInfo("BenefitClaimHeaderID", "{0}"), _
        RelationInfo("BenefitClaimHeader", "ID", "BenefitClaimReceipt", "BenefitClaimHeaderID")> _
        Public Property BenefitClaimHeader As BenefitClaimHeader
            Get
                Try
                    If Not isnothing(Me._benefitClaimHeader) AndAlso (Not Me._benefitClaimHeader.IsLoaded) Then

                        Me._benefitClaimHeader = CType(DoLoad(GetType(BenefitClaimHeader).ToString(), _benefitClaimHeader.ID), BenefitClaimHeader)
                        Me._benefitClaimHeader.MarkLoaded()

                    End If

                    Return Me._benefitClaimHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitClaimHeader)

                Me._benefitClaimHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitClaimHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerBankAccountID", "{0}"), _
        RelationInfo("DealerBankAccount", "ID", "BenefitClaimReceipt", "DealerBankAccountID")> _
        Public Property DealerBankAccount As DealerBankAccount
            Get
                Try
                    If Not IsNothing(Me._dealerBankAccount) AndAlso (Not Me._dealerBankAccount.IsLoaded) Then

                        Me._dealerBankAccount = CType(DoLoad(GetType(DealerBankAccount).ToString(), _dealerBankAccount.ID), DealerBankAccount)
                        Me._dealerBankAccount.MarkLoaded()

                    End If

                    Return Me._dealerBankAccount

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBankAccount)

                Me._dealerBankAccount = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBankAccount.MarkLoaded()
                End If
            End Set
        End Property

        '<ColumnInfo("DealerBankAccountID", "{0}")> _
        'Public Property DealerBankAccountID As Integer
        '    Get
        '        Return _dealerBankAccountID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _dealerBankAccountID = value
        '    End Set
        'End Property

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

