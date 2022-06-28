#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MonthlyDocument Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 24/11/2005 - 10:08:22
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
    <Serializable(), TableInfo("MonthlyDocument")> _
    Public Class MonthlyDocument
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Integer)
            _id = id
        End Sub

#End Region

#Region "Private Variables"

        Private _id As Integer
        Private _kind As Integer
        Private _periodeMonth As Short
        Private _periodeYear As Short
        Private _fileName As String = String.Empty
        Private _fileSize As Integer
        Private _lastDownloadBy As String = String.Empty
        Private _lastDownloadDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _nameOfBank As String = String.Empty
        Private _accountNumberBank As String = String.Empty
        Private _amountTransfer As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _dealerID As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _productCategory As ProductCategory

        Private _billingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _billingNo As String = String.Empty
        Private _accountingNo As String = String.Empty
        Private _taxNo As String = String.Empty
        Private _transferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _parkedName As String = String.Empty
        Private _amount As Decimal
        Private _currencies As String = String.Empty
        Private _description As String = String.Empty
        Private _clearingNo As Integer
        Private _settlementDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _actualTransferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

#End Region

#Region "Public Properties"

        <ColumnInfo("BillingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BillingDate() As DateTime
            Get
                Return _billingDate
            End Get
            Set(ByVal value As DateTime)
                _billingDate = value
            End Set
        End Property
        <ColumnInfo("BillingNo", "{0}")> _
        Public Property BillingNo() As String
            Get
                Return _billingNo
            End Get
            Set(ByVal value As String)
                _billingNo = value
            End Set
        End Property
        <ColumnInfo("AccountingNo", "{0}")> _
        Public Property AccountingNo() As String
            Get
                Return _accountingNo
            End Get
            Set(ByVal value As String)
                _accountingNo = value
            End Set
        End Property
        <ColumnInfo("TaxNo", "{0}")> _
        Public Property TaxNo() As String
            Get
                Return _taxNo
            End Get
            Set(ByVal value As String)
                _taxNo = value
            End Set
        End Property
        <ColumnInfo("TransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransferDate() As DateTime
            Get
                Return _transferDate
            End Get
            Set(ByVal value As DateTime)
                _transferDate = value
            End Set
        End Property

        <ColumnInfo("id", "{0}")> _
        Public Property id() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property


        <ColumnInfo("Kind", "{0}")> _
        Public Property Kind() As Integer
            Get
                Return _kind
            End Get
            Set(ByVal value As Integer)
                _kind = value
            End Set
        End Property


        <ColumnInfo("PeriodeMonth", "{0}")> _
        Public Property PeriodeMonth() As Short
            Get
                Return _periodeMonth
            End Get
            Set(ByVal value As Short)
                _periodeMonth = value
            End Set
        End Property


        <ColumnInfo("PeriodeYear", "{0}")> _
        Public Property PeriodeYear() As Short
            Get
                Return _periodeYear
            End Get
            Set(ByVal value As Short)
                _periodeYear = value
            End Set
        End Property


        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property


        <ColumnInfo("FileSize", "{0}")> _
        Public Property FileSize() As Integer
            Get
                Return _fileSize
            End Get
            Set(ByVal value As Integer)
                _fileSize = value
            End Set
        End Property


        <ColumnInfo("LastDownloadBy", "'{0}'")> _
        Public Property LastDownloadBy() As String
            Get
                Return _lastDownloadBy
            End Get
            Set(ByVal value As String)
                _lastDownloadBy = value
            End Set
        End Property


        <ColumnInfo("LastDownloadDate", "'{0:yyyy/MM/dd}'")> _
        Public Property LastDownloadDate() As DateTime
            Get
                Return _lastDownloadDate
            End Get
            Set(ByVal value As DateTime)
                _lastDownloadDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("NameofBank", "'{0}'")> _
        Public Property NameofBank() As String
            Get
                Return _nameOfBank
            End Get
            Set(ByVal value As String)
                _nameOfBank = value
            End Set
        End Property

        <ColumnInfo("AccountNumberBank", "'{0}'")> _
        Public Property AccountNumberBank() As String
            Get
                Return _accountNumberBank
            End Get
            Set(ByVal value As String)
                _accountNumberBank = value
            End Set
        End Property

        <ColumnInfo("AmountTransfer", "{0}")> _
        Public Property AmountTransfer As Decimal
            Get
                Return _amountTransfer
            End Get
            Set(ByVal value As Decimal)
                _amountTransfer = value
            End Set
        End Property

        <ColumnInfo("SettlementDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SettlementDate() As DateTime
            Get
                Return _settlementDate
            End Get
            Set(ByVal value As DateTime)
                _settlementDate = value
            End Set
        End Property

        <ColumnInfo("ParkedName", "{0}")> _
        Public Property ParkedName() As String
            Get
                Return _parkedName
            End Get
            Set(ByVal value As String)
                _parkedName = value
            End Set
        End Property

        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property

        <ColumnInfo("Currencies", "{0}")> _
        Public Property Currencies() As String
            Get
                Return _currencies
            End Get
            Set(ByVal value As String)
                _currencies = value
            End Set
        End Property

        <ColumnInfo("Description", "{0}")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        <ColumnInfo("NoClearing", "{0}")> _
        Public Property NoClearing() As Integer
            Get
                Return _clearingNo
            End Get
            Set(ByVal value As Integer)
                _clearingNo = value
            End Set
        End Property

        <ColumnInfo("ActualTransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ActualTransferDate() As DateTime
            Get
                Return _actualTransferDate
            End Get
            Set(ByVal value As DateTime)
                _actualTransferDate = value
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

        <ColumnInfo("DealerID", "'{0}'")> _
        Public Property DealerID() As String
            Get
                Return _dealerID
            End Get
            Set(ByVal value As String)
                _dealerID = value
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




        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "MonthlyDocument", "DealerID")> _
        Public Property Dealer() As Dealer
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


        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "MonthlyDocument", "ProductCategoryID")> _
        Public Property ProductCategory() As ProductCategory
            Get
                Try
                    If Not IsNothing(Me._productCategory) AndAlso (Not Me._productCategory.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._productCategory.MarkLoaded()
                End If
            End Set
        End Property


#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

