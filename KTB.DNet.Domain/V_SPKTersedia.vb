
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPK_Tersedia Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 5/13/2011 - 9:44:53 AM
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
    <Serializable(), TableInfo("V_SPKTersedia")> _
    Public Class V_SPKTersedia
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
        'Private _dealerID As Short
        Private _status As String = String.Empty
        Private _sPKNumber As String = String.Empty
        Private _dealerSPKNumber As String = String.Empty
        Private _planDeliveryMonth As Byte
        Private _planDeliveryYear As Short
        Private _planDeliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _planInvoiceMonth As Byte
        Private _planInvoiceYear As Short
        Private _planInvoiceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerRequestID As Integer
        'Private _sPKCustomerID As Integer
        Private _validateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateBy As String = String.Empty
        Private _rejectedReason As String = String.Empty
        'Private _salesmanHeaderID As Short
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _quantity As Integer
        Private _faktur As Integer

        Private _dealer As Dealer
        Private _category As Category
        Private _salesmanHeader As SalesmanHeader
        Private _sPKCustomer As SPKCustomer

        Private _customerRequest As CustomerRequest

        Private _sPKFakturs As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sPKDetails As System.Collections.ArrayList = New System.Collections.ArrayList



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


        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID() As Short
        '    Get
        '        Return _dealerID
        '    End Get
        '    Set(ByVal value As Short)
        '        _dealerID = value
        '    End Set
        'End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("SPKNumber", "'{0}'")> _
        Public Property SPKNumber() As String
            Get
                Return _sPKNumber
            End Get
            Set(ByVal value As String)
                _sPKNumber = value
            End Set
        End Property


        <ColumnInfo("DealerSPKNumber", "'{0}'")> _
        Public Property DealerSPKNumber() As String
            Get
                Return _dealerSPKNumber
            End Get
            Set(ByVal value As String)
                _dealerSPKNumber = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryMonth", "{0}")> _
        Public Property PlanDeliveryMonth() As Byte
            Get
                Return _planDeliveryMonth
            End Get
            Set(ByVal value As Byte)
                _planDeliveryMonth = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryYear", "{0}")> _
        Public Property PlanDeliveryYear() As Short
            Get
                Return _planDeliveryYear
            End Get
            Set(ByVal value As Short)
                _planDeliveryYear = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanDeliveryDate() As DateTime
            Get
                Return _planDeliveryDate
            End Get
            Set(ByVal value As DateTime)
                _planDeliveryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PlanInvoiceMonth", "{0}")> _
        Public Property PlanInvoiceMonth() As Byte
            Get
                Return _planInvoiceMonth
            End Get
            Set(ByVal value As Byte)
                _planInvoiceMonth = value
            End Set
        End Property


        <ColumnInfo("PlanInvoiceYear", "{0}")> _
        Public Property PlanInvoiceYear() As Short
            Get
                Return _planInvoiceYear
            End Get
            Set(ByVal value As Short)
                _planInvoiceYear = value
            End Set
        End Property


        <ColumnInfo("PlanInvoiceDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanInvoiceDate() As DateTime
            Get
                Return _planInvoiceDate
            End Get
            Set(ByVal value As DateTime)
                _planInvoiceDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerRequestID", "{0}")> _
        Public Property CustomerRequestID() As Integer
            Get
                Return _customerRequestID
            End Get
            Set(ByVal value As Integer)
                _customerRequestID = value
            End Set
        End Property


        '<ColumnInfo("SPKCustomerID", "{0}")> _
        'Public Property SPKCustomerID() As Integer
        '    Get
        '        Return _sPKCustomerID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sPKCustomerID = value
        '    End Set
        'End Property


        <ColumnInfo("ValidateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidateTime() As DateTime
            Get
                Return _validateTime
            End Get
            Set(ByVal value As DateTime)
                _validateTime = value
            End Set
        End Property


        <ColumnInfo("ValidateBy", "'{0}'")> _
        Public Property ValidateBy() As String
            Get
                Return _validateBy
            End Get
            Set(ByVal value As String)
                _validateBy = value
            End Set
        End Property


        <ColumnInfo("RejectedReason", "'{0}'")> _
        Public Property RejectedReason() As String
            Get
                Return _rejectedReason
            End Get
            Set(ByVal value As String)
                _rejectedReason = value
            End Set
        End Property


        '<ColumnInfo("SalesmanHeaderID", "{0}")> _
        'Public Property SalesmanHeaderID() As Short
        '    Get
        '        Return _salesmanHeaderID
        '    End Get
        '    Set(ByVal value As Short)
        '        _salesmanHeaderID = value
        '    End Set
        'End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
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


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity() As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
            End Set
        End Property


        <ColumnInfo("Faktur", "{0}")> _
        Public Property Faktur() As Integer
            Get
                Return _faktur
            End Get
            Set(ByVal value As Integer)
                _faktur = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
                RelationInfo("Dealer", "ID", "V_SPKTersedia", "DealerID")> _
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

        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "V_SPKTersedia", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not isnothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

                        Me._category = CType(DoLoad(GetType(Category).ToString(), _category.ID), Category)
                        Me._category.MarkLoaded()

                    End If

                    Return Me._category

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Category)

                Me._category = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanHeaderID", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "V_SPKTersedia", "SalesmanHeaderID")> _
        Public Property SalesmanHeader() As SalesmanHeader
            Get
                Try
                    If Not IsNothing(Me._salesmanHeader) AndAlso (Not Me._salesmanHeader.IsLoaded) Then

                        Me._salesmanHeader = CType(DoLoad(GetType(SalesmanHeader).ToString(), _salesmanHeader.ID), SalesmanHeader)
                        Me._salesmanHeader.MarkLoaded()

                    End If

                    Return Me._salesmanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeader)

                Me._salesmanHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SPKCustomerID", "{0}"), _
        RelationInfo("SPKCustomer", "ID", "V_SPKTersedia", "SPKCustomerID")> _
        Public Property SPKCustomer() As SPKCustomer
            Get
                Try
                    If Not IsNothing(Me._sPKCustomer) AndAlso (Not Me._sPKCustomer.IsLoaded) Then

                        Me._sPKCustomer = CType(DoLoad(GetType(SPKCustomer).ToString(), _sPKCustomer.ID), SPKCustomer)
                        Me._sPKCustomer.MarkLoaded()

                    End If

                    Return Me._sPKCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKCustomer)

                Me._sPKCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPKCustomer.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("V_SPKTersedia", "SPKHeaderID", "SPKFaktur", "SPKHeaderID")> _
        Public ReadOnly Property SPKFakturs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPKFakturs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPKFaktur), "SPKHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPKFakturs = DoLoadArray(GetType(SPKFaktur).ToString, criterias)
                    End If

                    Return Me._sPKFakturs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("V_SPKTersedia", "SPKHeaderID", "SPKDetail", "SPKHeaderID")> _
        Public ReadOnly Property SPKDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPKDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPKDetail), "SPKHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPKDetails = DoLoadArray(GetType(SPKDetail).ToString, criterias)
                    End If

                    Return Me._sPKDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property



        <ColumnInfo("CustomerRequestID", "{0}"), _
             RelationInfo("CustomerRequest", "ID", "V_SPKTersedia", "CustomerRequestID")> _
        Public ReadOnly Property CustomerRequest() As CustomerRequest
            Get
                Try
                    If IsNothing(Me._customerRequest) Then
                        _customerRequest = New CustomerRequest
                        Me._customerRequest = CType(DoLoad(GetType(CustomerRequest).ToString(), _customerRequestID), CustomerRequest)

                        If IsNothing(_customerRequest) Then
                            _customerRequest = New CustomerRequest
                        End If
                        Me._customerRequest.MarkLoaded()
                    End If

                    Return Me._customerRequest

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

