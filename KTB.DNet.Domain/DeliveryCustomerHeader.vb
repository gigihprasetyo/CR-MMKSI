#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DeliveryCustomerHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/20/2007 - 4:00:00 PM
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
    <Serializable(), TableInfo("DeliveryCustomerHeader")> _
    Public Class DeliveryCustomerHeader
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
        Private _regDONumber As String = String.Empty
        Private _status As Short
        Private _postingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _reffDONumber As String = String.Empty
        Private _salesmanID As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _customer As Customer
        Private _dealer As Dealer
        Private _fromDealer As Integer

        Private _deliveryCustomerDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("RegDONumber", "'{0}'")> _
        Public Property RegDONumber() As String
            Get
                Return _regDONumber
            End Get
            Set(ByVal value As String)
                _regDONumber = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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


        <ColumnInfo("ReffDONumber", "'{0}'")> _
        Public Property ReffDONumber() As String
            Get
                Return _reffDONumber
            End Get
            Set(ByVal value As String)
                _reffDONumber = value
            End Set
        End Property


        <ColumnInfo("SalesmanID", "{0}")> _
        Public Property SalesmanID() As Integer
            Get
                Return _salesmanID
            End Get
            Set(ByVal value As Integer)
                _salesmanID = value
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

        <ColumnInfo("DestinationCustomer", "{0}"), _
        RelationInfo("Customer", "ID", "DeliveryCustomerHeader", "DestinationCustomer")> _
        Public Property Customer() As Customer
            Get
                Try
                    If Not IsNothing(Me._customer) AndAlso (Not Me._customer.IsLoaded) Then

                        Me._customer = CType(DoLoad(GetType(Customer).ToString(), _customer.ID), Customer)
                        Me._customer.MarkLoaded()

                    End If

                    Return Me._customer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Customer)

                Me._customer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._customer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DestinationDealer", "{0}"), _
        RelationInfo("Dealer", "ID", "DeliveryCustomerHeader", "DestinationDealer")> _
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

        <ColumnInfo("FromDealer", "{0}")> _
        Public Property FromDealer() As Integer

            Get
                Return _fromDealer
            End Get
            Set(ByVal value As Integer)
                _fromDealer = value
            End Set
        End Property

        <RelationInfo("DeliveryCustomerHeader", "ID", "DeliveryCustomerDetail", "DeliveryCustomerHeaderID")> _
        Public ReadOnly Property DeliveryCustomerDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._deliveryCustomerDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DeliveryCustomerDetail), "DeliveryCustomerHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DeliveryCustomerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._deliveryCustomerDetails = DoLoadArray(GetType(DeliveryCustomerDetail).ToString, criterias)
                    End If

                    Return Me._deliveryCustomerDetails

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

        Private _sourceDealer As Dealer
        <ColumnInfo("FromDealer", "{0}"), _
               RelationInfo("Dealer", "ID", "DeliveryCustomerHeader", "FromDealer")> _
        Public Property SourceDealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._sourceDealer) AndAlso (Not Me._sourceDealer.IsLoaded) Then

                        Me._sourceDealer = CType(DoLoad(GetType(Dealer).ToString(), _sourceDealer.ID), Dealer)
                        Me._sourceDealer.MarkLoaded()
                    Else
                        _sourceDealer = Nothing
                        Return Me._sourceDealer
                    End If

                    Return Me._sourceDealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._sourceDealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sourceDealer.MarkLoaded()
                End If
            End Set
        End Property

        Public ReadOnly Property DealerDesc() As String
            Get
                Try
                    Return Me._sourceDealer.DealerCode
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property

        Private _DestinatitionDealer As Integer

        <ColumnInfo("DestinationDealer", "{0}"), _
           RelationInfo("Dealer", "ID", "DeliveryCustomerHeader", "DestinationDealer")> _
              Public Property DestinationDealer() As Integer
            Get
                Return _DestinatitionDealer
            End Get
            Set(ByVal Value As Integer)
                _DestinatitionDealer = Value
            End Set
        End Property
#End Region

    End Class
End Namespace

