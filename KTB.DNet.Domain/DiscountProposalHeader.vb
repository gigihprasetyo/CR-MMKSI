
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:41:33
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
    <Serializable(), TableInfo("DiscountProposalHeader")> _
    Public Class DiscountProposalHeader
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
        Private _customerType As Short
        Private _proposalRegNo As String = String.Empty
        Private _dealerProposalNo As String = String.Empty
        Private _bBNArea As String = String.Empty
        Private _fleetCategory As Short
        Private _projectName As String = String.Empty
        Private _lastPurchaseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isDealerDirectSales As Short
        Private _contractorName As String = String.Empty
        Private _purchaseMethod As Short
        Private _isAPMSubsidy As Short
        Private _paymentMethod As Short
        Private _purchaseKind As Short
        Private _projectKindMethod As Short
        Private _projectKindMethodOther As String
        Private _deliveryPlanDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerNotes As String = String.Empty
        Private _deliveryRegionCode As String = String.Empty
        Private _submitDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Short
        Private _businessSectorDetailID As Integer
        Private _consideration As String = String.Empty
        Private _mmksiNotes As String = String.Empty
        Private _finalApproval As Short
        Private _rowStatus As Short        
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _fleetCustomerDetail As FleetCustomerDetail
        Private _sPL As SPL
        Private _leasing As LeasingCompany
        Private _province As Province

        Private _discountProposalDetailOwnerships As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _discountProposalDetails As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _discountProposalDetailDocuments As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _discountProposalDetailPrices As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _discountProposalDetailCustomers As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _discountProposalEmailUser As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _discountProposalDetailApproval As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("CustomerType", "{0}")> _
        Public Property CustomerType As Short
            Get
                Return _customerType
            End Get
            Set(ByVal value As Short)
                _customerType = value
            End Set
        End Property


        <ColumnInfo("ProposalRegNo", "'{0}'")> _
        Public Property ProposalRegNo As String
            Get
                Return _proposalRegNo
            End Get
            Set(ByVal value As String)
                _proposalRegNo = value
            End Set
        End Property


        <ColumnInfo("DealerProposalNo", "'{0}'")> _
        Public Property DealerProposalNo As String
            Get
                Return _dealerProposalNo
            End Get
            Set(ByVal value As String)
                _dealerProposalNo = value
            End Set
        End Property


        '<ColumnInfo("BBNArea", "'{0}'")> _
        'Public Property BBNArea As String
        '    Get
        '        Return _bBNArea
        '    End Get
        '    Set(ByVal value As String)
        '        _bBNArea = value
        '    End Set
        'End Property


        <ColumnInfo("FleetCategory", "{0}")> _
        Public Property FleetCategory As Short
            Get
                Return _fleetCategory
            End Get
            Set(ByVal value As Short)
                _fleetCategory = value
            End Set
        End Property


        <ColumnInfo("ProjectName", "'{0}'")> _
        Public Property ProjectName As String
            Get
                Return _projectName
            End Get
            Set(ByVal value As String)
                _projectName = value
            End Set
        End Property


        <ColumnInfo("LastPurchaseDate", "'{0:yyyy/MM/dd}'")> _
        Public Property LastPurchaseDate As DateTime
            Get
                Return _lastPurchaseDate
            End Get
            Set(ByVal value As DateTime)
                _lastPurchaseDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("IsDealerDirectSales", "{0}")> _
        Public Property IsDealerDirectSales As Short
            Get
                Return _isDealerDirectSales
            End Get
            Set(ByVal value As Short)
                _isDealerDirectSales = value
            End Set
        End Property


        <ColumnInfo("ContractorName", "'{0}'")> _
        Public Property ContractorName As String
            Get
                Return _contractorName
            End Get
            Set(ByVal value As String)
                _contractorName = value
            End Set
        End Property


        <ColumnInfo("PurchaseMethod", "{0}")> _
        Public Property PurchaseMethod As Short
            Get
                Return _purchaseMethod
            End Get
            Set(ByVal value As Short)
                _purchaseMethod = value
            End Set
        End Property


        <ColumnInfo("IsAPMSubsidy", "{0}")> _
        Public Property IsAPMSubsidy As Short
            Get
                Return _isAPMSubsidy
            End Get
            Set(ByVal value As Short)
                _isAPMSubsidy = value
            End Set
        End Property

        <ColumnInfo("PaymentMethod", "{0}")> _
        Public Property PaymentMethod As Short
            Get
                Return _paymentMethod
            End Get
            Set(ByVal value As Short)
                _paymentMethod = value
            End Set
        End Property


        <ColumnInfo("PurchaseKind", "{0}")> _
        Public Property PurchaseKind As Short
            Get
                Return _purchaseKind
            End Get
            Set(ByVal value As Short)
                _purchaseKind = value
            End Set
        End Property


        <ColumnInfo("ProjectKindMethod", "{0}")> _
        Public Property ProjectKindMethod As Short
            Get
                Return _projectKindMethod
            End Get
            Set(ByVal value As Short)
                _projectKindMethod = value
            End Set
        End Property


        <ColumnInfo("ProjectKindMethodOther", "{0}")> _
        Public Property ProjectKindMethodOther As String
            Get
                Return _projectKindMethodOther
            End Get
            Set(ByVal value As String)
                _projectKindMethodOther = value
            End Set
        End Property


        <ColumnInfo("DeliveryPlanDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DeliveryPlanDate As DateTime
            Get
                Return _deliveryPlanDate
            End Get
            Set(ByVal value As DateTime)
                _deliveryPlanDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DealerNotes", "'{0}'")> _
        Public Property DealerNotes As String
            Get
                Return _dealerNotes
            End Get
            Set(ByVal value As String)
                _dealerNotes = value
            End Set
        End Property


        <ColumnInfo("DeliveryRegionCode", "'{0}'")> _
        Public Property DeliveryRegionCode As String
            Get
                Return _deliveryRegionCode
            End Get
            Set(ByVal value As String)
                _deliveryRegionCode = value
            End Set
        End Property


        <ColumnInfo("SubmitDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SubmitDate As DateTime
            Get
                Return _submitDate
            End Get
            Set(ByVal value As DateTime)
                _submitDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidFrom As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
            End Set
        End Property


        <ColumnInfo("ValidTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidTo As DateTime
            Get
                Return _validTo
            End Get
            Set(ByVal value As DateTime)
                _validTo = value
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


        <ColumnInfo("BusinessSectorDetailID", "{0}")> _
        Public Property BusinessSectorDetailID As Integer
            Get
                Return _businessSectorDetailID
            End Get
            Set(ByVal value As Integer)
                _businessSectorDetailID = value
            End Set
        End Property


        <ColumnInfo("Consideration", "'{0}'")> _
        Public Property Consideration As String
            Get
                Return _consideration
            End Get
            Set(ByVal value As String)
                _consideration = value
            End Set
        End Property



        <ColumnInfo("MMKSINotes", "'{0}'")> _
        Public Property MMKSINotes As String
            Get
                Return _mmksiNotes
            End Get
            Set(ByVal value As String)
                _mmksiNotes = value
            End Set
        End Property


        <ColumnInfo("FinalApproval", "{0}")> _
        Public Property FinalApproval As Short
            Get
                Return _finalApproval
            End Get
            Set(ByVal value As Short)
                _finalApproval = value
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
        RelationInfo("Dealer", "ID", "DiscountProposalHeader", "DealerID")> _
        Public Property Dealer As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then
                        If _dealer.ID > 0 Then
                            Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                            Me._dealer.MarkLoaded()
                        End If
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

        <ColumnInfo("FleetCustomerDetailID", "{0}"), _
        RelationInfo("FleetCustomerDetail", "ID", "DiscountProposalHeader", "FleetCustomerDetailID")> _
        Public Property FleetCustomerDetail As FleetCustomerDetail
            Get
                Try
                    If Not IsNothing(Me._fleetCustomerDetail) AndAlso (Not Me._fleetCustomerDetail.IsLoaded) Then
                        If _fleetCustomerDetail.ID > 0 Then
                            Me._fleetCustomerDetail = CType(DoLoad(GetType(FleetCustomerDetail).ToString(), _fleetCustomerDetail.ID), FleetCustomerDetail)
                            Me._fleetCustomerDetail.MarkLoaded()
                        End If
                    End If

                    Return Me._fleetCustomerDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As FleetCustomerDetail)

                Me._fleetCustomerDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._fleetCustomerDetail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SPLID", "{0}"), _
        RelationInfo("SPL", "ID", "DiscountProposalHeader", "SPLID")> _
        Public Property SPL As SPL
            Get
                Try
                    If Not isnothing(Me._sPL) AndAlso (Not Me._sPL.IsLoaded) Then
                        If _sPL.ID > 0 Then
                            Me._sPL = CType(DoLoad(GetType(SPL).ToString(), _sPL.ID), SPL)
                            Me._sPL.MarkLoaded()
                        End If
                    End If

                    Return Me._sPL

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPL)

                Me._sPL = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPL.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("LeasingID", "{0}"), _
        RelationInfo("LeasingCompany", "ID", "DiscountProposalHeader", "LeasingID")> _
        Public Property LeasingCompany As LeasingCompany
            Get
                Try
                    If Not IsNothing(Me._leasing) AndAlso (Not Me._leasing.IsLoaded) Then
                        If _leasing.ID > 0 Then
                            Me._leasing = CType(DoLoad(GetType(LeasingCompany).ToString(), _leasing.ID), LeasingCompany)
                            Me._leasing.MarkLoaded()
                        End If
                    End If

                    Return Me._leasing

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As LeasingCompany)

                Me._leasing = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._leasing.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("BBNAreaProvinceID", "{0}"), _
        RelationInfo("Province", "ID", "DiscountProposalHeader", "BBNAreaProvinceID")> _
        Public Property BBNAreaProvince As Province
            Get
                Try
                    If Not IsNothing(Me._province) AndAlso (Not Me._province.IsLoaded) Then
                        If _province.ID > 0 Then
                            Me._province = CType(DoLoad(GetType(Province).ToString(), _province.ID), Province)
                            Me._province.MarkLoaded()
                        End If
                    End If

                    Return Me._province

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Province)

                Me._province = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._province.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalDetailOwnership", "DiscountProposalHeaderID")> _
        Public ReadOnly Property DiscountProposalDetailOwnerships As System.Collections.ArrayList
            Get
                Try
                    If (Me._discountProposalDetailOwnerships.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DiscountProposalDetailOwnership), "DiscountProposalHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DiscountProposalDetailOwnership), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._discountProposalDetailOwnerships = DoLoadArray(GetType(DiscountProposalDetailOwnership).ToString, criterias)
                    End If

                    Return Me._discountProposalDetailOwnerships

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalDetail", "DiscountProposalHeaderID")> _
        Public ReadOnly Property DiscountProposalDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._discountProposalDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DiscountProposalDetail), "DiscountProposalHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DiscountProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._discountProposalDetails = DoLoadArray(GetType(DiscountProposalDetail).ToString, criterias)
                    End If

                    Return Me._discountProposalDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalDetailDocument", "DiscountProposalHeaderID")> _
        Public ReadOnly Property DiscountProposalDetailDocuments As System.Collections.ArrayList
            Get
                Try
                    If (Me._discountProposalDetailDocuments.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._discountProposalDetailDocuments = DoLoadArray(GetType(DiscountProposalDetailDocument).ToString, criterias)
                    End If

                    Return Me._discountProposalDetailDocuments

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalDetailPrice", "DiscountProposalHeaderID")> _
        Public ReadOnly Property DiscountProposalDetailPrices As System.Collections.ArrayList
            Get
                Try
                    If (Me._discountProposalDetailPrices.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DiscountProposalDetailPrice), "DiscountProposalHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DiscountProposalDetailPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._discountProposalDetailPrices = DoLoadArray(GetType(DiscountProposalDetailPrice).ToString, criterias)
                    End If

                    Return Me._discountProposalDetailPrices

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalDetailCustomer", "DiscountProposalHeaderID")> _
        Public ReadOnly Property DiscountProposalDetailCustomers As System.Collections.ArrayList
            Get
                Try
                    If (Me._discountProposalDetailCustomers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DiscountProposalDetailCustomer), "DiscountProposalHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DiscountProposalDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._discountProposalDetailCustomers = DoLoadArray(GetType(DiscountProposalDetailCustomer).ToString, criterias)
                    End If

                    Return Me._discountProposalDetailCustomers

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalEmailUser", "DiscountProposalHeaderID")> _
        Public ReadOnly Property DiscountProposalEmailUsers As System.Collections.ArrayList
            Get
                Try
                    If (Me._discountProposalEmailUser.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DiscountProposalEmailUser), "DiscountProposalHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DiscountProposalEmailUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._discountProposalEmailUser = DoLoadArray(GetType(DiscountProposalEmailUser).ToString, criterias)
                    End If

                    Return Me._discountProposalEmailUser

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalDetailApproval", "DiscountProposalHeaderID")> _
        Public ReadOnly Property DiscountProposalDetailApprovals As System.Collections.ArrayList
            Get
                Try
                    If (Me._discountProposalDetailApproval.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DiscountProposalDetailApproval), "DiscountProposalHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DiscountProposalDetailApproval), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._discountProposalDetailApproval = DoLoadArray(GetType(DiscountProposalDetailApproval).ToString, criterias)
                    End If

                    Return Me._discountProposalDetailApproval

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

