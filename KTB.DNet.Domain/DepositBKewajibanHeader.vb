
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBKewajibanHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 3/22/2016 - 1:52:33 PM
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
    <Serializable(), TableInfo("DepositBKewajibanHeader")> _
    Public Class DepositBKewajibanHeader
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
        Private _noRegKewajiban As String = String.Empty
        Private _tipeKewajiban As Short
        Private _periodYear As Short
        Private _noSalesorder As String
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _productCategory As ProductCategory

        Private _depositBPencairanHeaders As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _depositBKewajibanDetails As System.Collections.ArrayList = New System.Collections.ArrayList()
        'Private _depositBKewajibanDealers As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("NoRegKewajiban", "'{0}'")> _
        Public Property NoRegKewajiban As String
            Get
                Return _noRegKewajiban
            End Get
            Set(ByVal value As String)
                _noRegKewajiban = value
            End Set
        End Property


        <ColumnInfo("TipeKewajiban", "{0}")> _
        Public Property TipeKewajiban As Short
            Get
                Return _tipeKewajiban
            End Get
            Set(ByVal value As Short)
                _tipeKewajiban = value
            End Set
        End Property


        <ColumnInfo("PeriodYear", "{0}")> _
        Public Property PeriodYear As Short
            Get
                Return _periodYear
            End Get
            Set(ByVal value As Short)
                _periodYear = value
            End Set
        End Property


        <ColumnInfo("NoSalesorder", "{0}")> _
        Public Property NoSalesorder As String
            Get
                Return _noSalesorder
            End Get
            Set(ByVal value As String)
                _noSalesorder = value
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
        RelationInfo("Dealer", "ID", "DepositBKewajibanHeader", "DealerID")> _
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

        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "DepositBKewajibanHeader", "ProductCategoryID")> _
        Public Property ProductCategory As ProductCategory
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

        <RelationInfo("DepositBKewajibanHeader", "ID", "DepositBPencairanHeader", "KewajibanHeaderID")> _
        Public ReadOnly Property DepositBPencairanHeaders As System.Collections.ArrayList
            Get
                Try
                    If (Me._depositBPencairanHeaders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DepositBPencairanHeader), "DepositBKewajibanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._depositBPencairanHeaders = DoLoadArray(GetType(DepositBPencairanHeader).ToString, criterias)
                    End If

                    Return Me._depositBPencairanHeaders

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("DepositBKewajibanHeader", "ID", "DepositBKewajibanDetail", "DepositBKewajibanHeaderID")> _
        Public ReadOnly Property DepositBKewajibanDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._depositBKewajibanDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DepositBKewajibanDetail), "DepositBKewajibanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DepositBKewajibanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._depositBKewajibanDetails = DoLoadArray(GetType(DepositBKewajibanDetail).ToString, criterias)
                    End If

                    Return Me._depositBKewajibanDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        '<RelationInfo("DepositBKewajibanHeader", "ID", "DepositBKewajibanDealer", "DepositBKewajibanHeaderID")> _
        'Public ReadOnly Property DepositBKewajibanDealers As System.Collections.ArrayList
        '    Get
        '        Try
        '            If (Me._depositBKewajibanDealers.Count < 1) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(DepositBKewajibanDealer), "DepositBKewajibanHeader", Me.ID)
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(DepositBKewajibanDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Me._depositBKewajibanDealers = DoLoadArray(GetType(DepositBKewajibanDealer).ToString, criterias)
        '            End If

        '            Return Me._depositBKewajibanDealers

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing

        '    End Get
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

