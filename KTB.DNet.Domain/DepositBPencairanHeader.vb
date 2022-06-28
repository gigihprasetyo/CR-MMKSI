
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBPencairanHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 5/13/2016 - 8:55:13 AM
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
    <Serializable(), TableInfo("DepositBPencairanHeader")> _
    Public Class DepositBPencairanHeader
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
        Private _noReferensi As String = String.Empty
        Private _tipePengajuan As Byte
        Private _dealerAmount As Decimal
        Private _approvalAmount As Decimal
        Private _kTBReason As String = String.Empty
        Private _keterangan As String = String.Empty
        Private _status As Byte
        Private _noReg As String = String.Empty
        Private _flag As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealerBankAccount As DealerBankAccount
        Private _depositBDebitNote As DepositBDebitNote
        Private _depositBInterestHeader As DepositBInterestHeader
        Private _depositBKewajibanHeader As DepositBKewajibanHeader
        Private _indentPartHeader As IndentPartHeader
        Private _productCategory As ProductCategory
        Private _dealer As Dealer

        Private _depositBReceipts As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _depositBPencairanDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("NoReferensi", "'{0}'")> _
        Public Property NoReferensi As String
            Get
                Return _noReferensi
            End Get
            Set(ByVal value As String)
                _noReferensi = value
            End Set
        End Property


        <ColumnInfo("TipePengajuan", "{0}")> _
        Public Property TipePengajuan As Byte
            Get
                Return _tipePengajuan
            End Get
            Set(ByVal value As Byte)
                _tipePengajuan = value
            End Set
        End Property


        <ColumnInfo("DealerAmount", "{0}")> _
        Public Property DealerAmount As Decimal
            Get
                Return _dealerAmount
            End Get
            Set(ByVal value As Decimal)
                _dealerAmount = value
            End Set
        End Property


        <ColumnInfo("ApprovalAmount", "{0}")> _
        Public Property ApprovalAmount As Decimal
            Get
                Return _approvalAmount
            End Get
            Set(ByVal value As Decimal)
                _approvalAmount = value
            End Set
        End Property


        <ColumnInfo("KTBReason", "'{0}'")> _
        Public Property KTBReason As String
            Get
                Return _kTBReason
            End Get
            Set(ByVal value As String)
                _kTBReason = value
            End Set
        End Property


        <ColumnInfo("Keterangan", "'{0}'")> _
        Public Property Keterangan As String
            Get
                Return _keterangan
            End Get
            Set(ByVal value As String)
                _keterangan = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
            End Set
        End Property


        <ColumnInfo("NoReg", "'{0}'")> _
        Public Property NoReg As String
            Get
                Return _noReg
            End Get
            Set(ByVal value As String)
                _noReg = value
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

        <ColumnInfo("Flag", "{0}")> _
        Public Property Flag As Short
            Get
                Return _flag
            End Get
            Set(ByVal value As Short)
                _flag = value
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


        <ColumnInfo("DealerBankAccountID", "{0}"), _
        RelationInfo("DealerBankAccount", "ID", "DepositBPencairanHeader", "DealerBankAccountID")> _
        Public Property DealerBankAccount As DealerBankAccount
            Get
                Try
                    If Not isnothing(Me._dealerBankAccount) AndAlso (Not Me._dealerBankAccount.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBankAccount.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DepositBDebitNoteID", "{0}"), _
        RelationInfo("DepositBDebitNote", "ID", "DepositBPencairanHeader", "DepositBDebitNoteID")> _
        Public Property DepositBDebitNote As DepositBDebitNote
            Get
                Try
                    If Not isnothing(Me._depositBDebitNote) AndAlso (Not Me._depositBDebitNote.IsLoaded) Then

                        Me._depositBDebitNote = CType(DoLoad(GetType(DepositBDebitNote).ToString(), _depositBDebitNote.ID), DepositBDebitNote)
                        Me._depositBDebitNote.MarkLoaded()

                    End If

                    Return Me._depositBDebitNote

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBDebitNote)

                Me._depositBDebitNote = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBDebitNote.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DepositBInterestHID", "{0}"), _
        RelationInfo("DepositBInterestHeader", "ID", "DepositBPencairanHeader", "DepositBInterestHID")> _
        Public Property DepositBInterestHeader As DepositBInterestHeader
            Get
                Try
                    If Not isnothing(Me._depositBInterestHeader) AndAlso (Not Me._depositBInterestHeader.IsLoaded) Then

                        Me._depositBInterestHeader = CType(DoLoad(GetType(DepositBInterestHeader).ToString(), _depositBInterestHeader.ID), DepositBInterestHeader)
                        Me._depositBInterestHeader.MarkLoaded()

                    End If

                    Return Me._depositBInterestHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBInterestHeader)

                Me._depositBInterestHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBInterestHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("KewajibanHeaderID", "{0}"), _
        RelationInfo("DepositBKewajibanHeader", "ID", "DepositBPencairanHeader", "KewajibanHeaderID")> _
        Public Property DepositBKewajibanHeader As DepositBKewajibanHeader
            Get
                Try
                    If Not isnothing(Me._depositBKewajibanHeader) AndAlso (Not Me._depositBKewajibanHeader.IsLoaded) Then

                        Me._depositBKewajibanHeader = CType(DoLoad(GetType(DepositBKewajibanHeader).ToString(), _depositBKewajibanHeader.ID), DepositBKewajibanHeader)
                        Me._depositBKewajibanHeader.MarkLoaded()

                    End If

                    Return Me._depositBKewajibanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBKewajibanHeader)

                Me._depositBKewajibanHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBKewajibanHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("IndentPartEqHeaderID", "{0}"), _
        RelationInfo("IndentPartHeader", "ID", "DepositBPencairanHeader", "IndentPartEqHeaderID")> _
        Public Property IndentPartHeader As IndentPartHeader
            Get
                Try
                    If Not isnothing(Me._indentPartHeader) AndAlso (Not Me._indentPartHeader.IsLoaded) Then

                        Me._indentPartHeader = CType(DoLoad(GetType(IndentPartHeader).ToString(), _indentPartHeader.ID), IndentPartHeader)
                        Me._indentPartHeader.MarkLoaded()

                    End If

                    Return Me._indentPartHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As IndentPartHeader)

                Me._indentPartHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._indentPartHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "DepositBPencairanHeader", "ProductCategoryID")> _
        Public Property ProductCategory As ProductCategory
            Get
                Try
                    If Not isnothing(Me._productCategory) AndAlso (Not Me._productCategory.IsLoaded) Then

                        Me._productCategory = CType(DoLoad(GetType(ProductCategory).ToString(), _productCategory.ID), ProductCategory)
                        Me._productCategory.MarkLoaded()

                    End If

                    Return Me._productCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProductCategory)

                Me._productCategory = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._productCategory.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "DepositBPencairanHeader", "DealerID")> _
        Public Property Dealer As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("DepositBPencairanHeader", "ID", "DepositBReceipt", "DepositBPencairanHeaderID")> _
        Public ReadOnly Property DepositBReceipts As System.Collections.ArrayList
            Get
                Try
                    If (Me._depositBReceipts.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DepositBReceipt), "DepositBPencairanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._depositBReceipts = DoLoadArray(GetType(DepositBReceipt).ToString, criterias)
                    End If

                    Return Me._depositBReceipts

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("DepositBPencairanHeader", "ID", "DepositBPencairanDetail", "HeaderID")> _
        Public ReadOnly Property DepositBPencairanDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._depositBPencairanDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DepositBPencairanDetail), "DepositBPencairanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DepositBPencairanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._depositBPencairanDetails = DoLoadArray(GetType(DepositBPencairanDetail).ToString, criterias)
                    End If

                    Return Me._depositBPencairanDetails

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
        Private _billingID As Integer = 0
        Public Property BillingID As Integer
            Get
                Return _billingID
            End Get
            Set(ByVal value As Integer)
                _billingID = value
            End Set
        End Property
#End Region

    End Class
End Namespace

