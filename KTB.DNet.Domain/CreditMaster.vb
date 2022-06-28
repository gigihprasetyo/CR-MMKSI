
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CreditMaster Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2009 - 1:34:41 PM
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
    <Serializable(), TableInfo("CreditMaster")> _
    Public Class CreditMaster
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
        Private _creditAccount As String = String.Empty
        Private _paymentType As Byte
        Private _plafon As Decimal
        Private _outStanding As Decimal
        Private _availablePlafon As Decimal
        Private _maxTOPDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _productCategory As ProductCategory
        Private _v_ProposedPO As v_ProposedPO
        Private _dealers As ArrayList = New ArrayList


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


        <ColumnInfo("CreditAccount", "'{0}'")> _
        Public Property CreditAccount() As String
            Get
                Return _creditAccount
            End Get
            Set(ByVal value As String)
                _creditAccount = value
            End Set
        End Property


        <ColumnInfo("PaymentType", "{0}")> _
        Public Property PaymentType() As Byte
            Get
                Return _paymentType
            End Get
            Set(ByVal value As Byte)
                _paymentType = value
            End Set
        End Property


        <ColumnInfo("Plafon", "{0}")> _
        Public Property Plafon() As Decimal
            Get
                Return _plafon
            End Get
            Set(ByVal value As Decimal)
                _plafon = value
            End Set
        End Property


        <ColumnInfo("OutStanding", "{0}")> _
        Public Property OutStanding() As Decimal
            Get
                Return _outStanding
            End Get
            Set(ByVal value As Decimal)
                _outStanding = value
            End Set
        End Property



        <ColumnInfo("AvailablePlafon", "{0}")> _
        Public Property AvailablePlafon() As Decimal
            Get
                Return _availablePlafon
            End Get
            Set(ByVal value As Decimal)
                _availablePlafon = value
            End Set
        End Property

        <ColumnInfo("MaxTOPDate", "'{0:yyyy/MM/dd}'")> _
          Public Property MaxTOPDate() As DateTime
            Get
                Return _maxTOPDate
            End Get
            Set(ByVal value As DateTime)
                _maxTOPDate = New DateTime(value.Year, value.Month, value.Day)
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



        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "CreditMaster", "ProductCategoryID")> _
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




        <ColumnInfo("CreditAccount", "{0}"), _
                RelationInfo("v_ProposedPO", "CreditAccount", "CreditMaster", "CreditAccount")> _
                Public Property v_ProposedPO() As v_ProposedPO
            Get
                Try
                    If IsNothing(Me._v_ProposedPO) Then ' AndAlso (Not Me._dealer.IsLoaded) Then
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_ProposedPO), "RowStatus", CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(v_ProposedPO), "CreditAccount", Me._creditAccount))
                        Me._v_ProposedPO = CType(DoLoadArray(GetType(v_ProposedPO).ToString, crt)(0), v_ProposedPO)
                        Me._v_ProposedPO.MarkLoaded()

                    End If

                    Return Me._v_ProposedPO

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As v_ProposedPO)

                Me._v_ProposedPO = value
                'If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                '    Me._dealer.MarkLoaded()
                'End If
            End Set
        End Property

        <RelationInfo("CreditMaster", "CreditAccount", "Dealer", "CreditAccount")> _
                Public ReadOnly Property Dealers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(Dealer), "CreditAccount", MatchType.Exact, Me.CreditAccount)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealers = DoLoadArray(GetType(Dealer).ToString, criterias)
                    End If

                    Return Me._dealers

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

