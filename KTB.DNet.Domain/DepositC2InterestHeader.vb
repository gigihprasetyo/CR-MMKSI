#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositC2InterestHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/17/2020 - 10:54:18 AM
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
    <Serializable(), TableInfo("DepositC2InterestHeader")> _
    Public Class DepositC2InterestHeader
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
        Private _periode As Short
        Private _year As Short
        Private _interestAmount As Decimal
        Private _taxAmount As Decimal
        Private _nettoAmount As Decimal
        Private _status As Short
        Private _filePathKwitansi As String = String.Empty
        Private _filePathLetter As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _productCategory As ProductCategory

        Private _depositC2InterestDetails As System.Collections.ArrayList = New System.Collections.ArrayList()

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


        <ColumnInfo("Periode", "{0}")> _
        Public Property Periode As Short
            Get
                Return _periode
            End Get
            Set(ByVal value As Short)
                _periode = value
            End Set
        End Property


        <ColumnInfo("Year", "{0}")> _
        Public Property Year As Short
            Get
                Return _year
            End Get
            Set(ByVal value As Short)
                _year = value
            End Set
        End Property


        <ColumnInfo("InterestAmount", "{0}")> _
        Public Property InterestAmount As Decimal
            Get
                Return _interestAmount
            End Get
            Set(ByVal value As Decimal)
                _interestAmount = value
            End Set
        End Property


        <ColumnInfo("TaxAmount", "{0}")> _
        Public Property TaxAmount As Decimal
            Get
                Return _taxAmount
            End Get
            Set(ByVal value As Decimal)
                _taxAmount = value
            End Set
        End Property


        <ColumnInfo("NettoAmount", "{0}")> _
        Public Property NettoAmount As Decimal
            Get
                Return _nettoAmount
            End Get
            Set(ByVal value As Decimal)
                _nettoAmount = value
            End Set
        End Property

        <ColumnInfo("FilePathKwitansi", "'{0}'")> _
        Public Property FilePathKwitansi As String
            Get
                Return _filePathKwitansi
            End Get
            Set(ByVal value As String)
                _filePathKwitansi = value
            End Set
        End Property

        <ColumnInfo("FilePathLetter", "'{0}'")> _
        Public Property FilePathLetter As String
            Get
                Return _filePathLetter
            End Get
            Set(ByVal value As String)
                _filePathLetter = value
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

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "DepositC2InterestHeader", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "DepositC2InterestHeader", "ProductCategoryID")> _
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

        <RelationInfo("DepositC2InterestHeader", "ID", "DepositC2InterestDetail", "DepositC2InterestHeaderID")> _
        Public ReadOnly Property DepositC2InterestDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._depositC2InterestDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DepositC2InterestDetail), "DepositC2InterestHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DepositC2InterestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._depositC2InterestDetails = DoLoadArray(GetType(DepositC2InterestDetail).ToString, criterias)
                    End If

                    Return Me._depositC2InterestDetails

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
