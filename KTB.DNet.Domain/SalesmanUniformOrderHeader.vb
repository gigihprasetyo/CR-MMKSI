#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanUniformOrderHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/15/2007 - 1:24:38 PM
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
    <Serializable(), TableInfo("SalesmanUniformOrderHeader")> _
    Public Class SalesmanUniformOrderHeader
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
        Private _orderNumber As String = String.Empty
        Private _orderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _description As String = String.Empty
        Private _note As String = String.Empty
        Private _invoiceNo As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _salesmanUnifDistribution As SalesmanUnifDistribution
        Private _dealer As Dealer

        Private _salesmanUniformOrderDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("OrderNumber", "'{0}'")> _
        Public Property OrderNumber() As String
            Get
                Return _orderNumber
            End Get
            Set(ByVal value As String)
                _orderNumber = value
            End Set
        End Property


        <ColumnInfo("OrderDate", "'{0:yyyy/MM/dd}'")> _
        Public Property OrderDate() As DateTime
            Get
                Return _orderDate
            End Get
            Set(ByVal value As DateTime)
                _orderDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("Note", "'{0}'")> _
        Public Property Note() As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _note = value
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


        <ColumnInfo("SalesmanUnifDistributionID", "{0}"), _
        RelationInfo("SalesmanUnifDistribution", "ID", "SalesmanUniformOrderHeader", "SalesmanUnifDistributionID")> _
        Public Property SalesmanUnifDistribution() As SalesmanUnifDistribution
            Get
                Try
                    If Not IsNothing(Me._salesmanUnifDistribution) AndAlso (Not Me._salesmanUnifDistribution.IsLoaded) Then

                        Me._salesmanUnifDistribution = CType(DoLoad(GetType(SalesmanUnifDistribution).ToString(), _salesmanUnifDistribution.ID), SalesmanUnifDistribution)
                        Me._salesmanUnifDistribution.MarkLoaded()

                    End If

                    Return Me._salesmanUnifDistribution

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanUnifDistribution)

                Me._salesmanUnifDistribution = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanUnifDistribution.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "SalesmanUniformOrderHeader", "DealerID")> _
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


        <RelationInfo("SalesmanUniformOrderHeader", "ID", "SalesmanUniformOrderDetail", "SalesmanUniformOrderHeaderID")> _
        Public ReadOnly Property SalesmanUniformOrderDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanUniformOrderDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanUniformOrderDetail), "SalesmanUniformOrderHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanUniformOrderDetails = DoLoadArray(GetType(SalesmanUniformOrderDetail).ToString, criterias)
                    End If

                    Return Me._salesmanUniformOrderDetails

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

#Region "Custom Properties"
        Private _TotalHarga As Decimal

        Public ReadOnly Property TotalHarga() As Decimal
            Get
                'Todo Aggregate
                _TotalHarga = 0
                For Each itemDetail As SalesmanUniformOrderDetail In Me.SalesmanUniformOrderDetails
                    If itemDetail.RowStatus = CType(DBRowStatus.Active, Short) Then
                        _TotalHarga += itemDetail.Qty * itemDetail.SalesmanUniform.DealerPrice
                    End If
                Next

                Return _TotalHarga
            End Get
        End Property
#End Region

    End Class
End Namespace

