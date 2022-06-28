#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Dealer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 8:51:44 AM
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
    <Serializable(), TableInfo("Dealer")> _
    Public Class Dealer
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
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _status As String = String.Empty
        Private _title As String = String.Empty
        Private _searchTerm1 As String = String.Empty
        Private _searchTerm2 As String = String.Empty
        Private _address As String = String.Empty
        Private _zipCode As String = String.Empty
        Private _phone As String = String.Empty
        Private _fax As String = String.Empty
        Private _website As String = String.Empty
        Private _email As String = String.Empty
        Private _salesUnitFlag As String = String.Empty
        Private _serviceFlag As String = String.Empty
        Private _sparepartFlag As String = String.Empty
        Private _sPANumber As String = String.Empty
        Private _sPADate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _freePPh22Indicator As Integer
        Private _freePPh22From As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _freePPh22To As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dueDate As Integer
        Private _agreementNo As String = String.Empty
        Private _agreementDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _creditAccount As String = String.Empty

        Private _rowStatus As Short
        Private _orgBranchType As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _wscno As String = String.Empty
        Private _reconAccount As String = String.Empty
        Private _sortKey As String = String.Empty
        Private _cashManagementGroup As String = String.Empty
        Private _paymentBlock As String = String.Empty
        Private _customerLegal As Integer
        Private _taxCode1 As String = String.Empty

        Private _nicknameDigital As String = String.Empty
        Private _nicknameEcommerce As String = String.Empty
        Private _longitude As String = String.Empty
        Private _latitude As String = String.Empty
        Private _publish As Boolean = False

        Private _legalStatus As String = String.Empty
        Private _mainDealer As Dealer
        Private _parentDealer As Dealer
        Private _parentDealerID As Short
        Private _area1 As Area1
        Private _area2 As Area2
        Private _mainArea As MainArea
        Private _city As City
        Private _dealerGroup As DealerGroup
        Private _province As Province
        Private _dealerAdditionals As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _dealerCategorys As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _buletinOrganizations As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _deposits As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _depositC2s As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _dealerPajaks As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _businessAreas As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _dealerBankAccounts As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _dealerOperationAreaBusiness As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _dealerPaymentMethods As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _dealerFacitys As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _dealerStallEquipments As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("MainDealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "Dealer", "MainDealerID")> _
        Public Property MainDealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._mainDealer) AndAlso (Not Me._mainDealer.IsLoaded) Then

                        Me._mainDealer = CType(DoLoad(GetType(Dealer).ToString(), _mainDealer.ID), Dealer)
                        Me._mainDealer.MarkLoaded()

                    End If

                    Return Me._mainDealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._mainDealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mainDealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ParentDealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "Dealer", "ParentDealerID")> _
        Public Property ParentDealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._parentDealer) AndAlso (Not Me._parentDealer.IsLoaded) Then

                        Me._parentDealer = CType(DoLoad(GetType(Dealer).ToString(), _parentDealer.ID), Dealer)
                        Me._parentDealer.MarkLoaded()

                    End If

                    Return Me._parentDealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._parentDealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._parentDealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("Title", "'{0}'")> _
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property


        <ColumnInfo("SearchTerm1", "'{0}'")> _
        Public Property SearchTerm1() As String
            Get
                Return _searchTerm1
            End Get
            Set(ByVal value As String)
                _searchTerm1 = value
            End Set
        End Property


        <ColumnInfo("SearchTerm2", "'{0}'")> _
        Public Property SearchTerm2() As String
            Get
                Return _searchTerm2
            End Get
            Set(ByVal value As String)
                _searchTerm2 = value
            End Set
        End Property


        <ColumnInfo("Address", "'{0}'")> _
        Public Property Address() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property


        <ColumnInfo("ZipCode", "'{0}'")> _
        Public Property ZipCode() As String
            Get
                Return _zipCode
            End Get
            Set(ByVal value As String)
                _zipCode = value
            End Set
        End Property


        <ColumnInfo("Phone", "'{0}'")> _
        Public Property Phone() As String
            Get
                Return _phone
            End Get
            Set(ByVal value As String)
                _phone = value
            End Set
        End Property


        <ColumnInfo("Fax", "'{0}'")> _
        Public Property Fax() As String
            Get
                Return _fax
            End Get
            Set(ByVal value As String)
                _fax = value
            End Set
        End Property


        <ColumnInfo("Website", "'{0}'")> _
        Public Property Website() As String
            Get
                Return _website
            End Get
            Set(ByVal value As String)
                _website = value
            End Set
        End Property


        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property


        <ColumnInfo("SalesUnitFlag", "'{0}'")> _
        Public Property SalesUnitFlag() As String
            Get
                Return _salesUnitFlag
            End Get
            Set(ByVal value As String)
                _salesUnitFlag = value
            End Set
        End Property


        <ColumnInfo("ServiceFlag", "'{0}'")> _
        Public Property ServiceFlag() As String
            Get
                Return _serviceFlag
            End Get
            Set(ByVal value As String)
                _serviceFlag = value
            End Set
        End Property


        <ColumnInfo("SparepartFlag", "'{0}'")> _
        Public Property SparepartFlag() As String
            Get
                Return _sparepartFlag
            End Get
            Set(ByVal value As String)
                _sparepartFlag = value
            End Set
        End Property


        <ColumnInfo("SPANumber", "'{0}'")> _
        Public Property SPANumber() As String
            Get
                Return _sPANumber
            End Get
            Set(ByVal value As String)
                _sPANumber = value
            End Set
        End Property


        <ColumnInfo("SPADate", "'{0:yyyy/MM/dd}'")> _
        Public Property SPADate() As DateTime
            Get
                Return _sPADate
            End Get
            Set(ByVal value As DateTime)
                _sPADate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("FreePPh22Indicator", "{0}")> _
        Public Property FreePPh22Indicator() As Integer
            Get
                Return _freePPh22Indicator
            End Get
            Set(ByVal value As Integer)
                _freePPh22Indicator = value
            End Set
        End Property


        <ColumnInfo("FreePPh22From", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FreePPh22From() As DateTime
            Get
                Return _freePPh22From
            End Get
            Set(ByVal value As DateTime)
                _freePPh22From = value
            End Set
        End Property


        <ColumnInfo("FreePPh22To", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FreePPh22To() As DateTime
            Get
                Return _freePPh22To
            End Get
            Set(ByVal value As DateTime)
                _freePPh22To = value
            End Set
        End Property

        <ColumnInfo("LegalStatus", "'{0}'")> _
        Public Property LegalStatus() As String
            Get
                Return _legalStatus
            End Get
            Set(ByVal value As String)
                _legalStatus = value
            End Set
        End Property

        <ColumnInfo("WSCNO", "'{0}'")> _
        Public Property WSCNO() As String
            Get
                Return _wscno
            End Get
            Set(ByVal value As String)
                _wscno = value
            End Set
        End Property



        <ColumnInfo("ReconAccount", "'{0}'")> _
        Public Property ReconAccount() As String
            Get
                Return _reconAccount
            End Get
            Set(ByVal value As String)
                _reconAccount = value
            End Set
        End Property



        <ColumnInfo("SortKey", "'{0}'")> _
        Public Property SortKey() As String
            Get
                Return _sortKey
            End Get
            Set(ByVal value As String)
                _sortKey = value
            End Set
        End Property



        <ColumnInfo("CashManagementGroup", "'{0}'")> _
        Public Property CashManagementGroup() As String
            Get
                Return _cashManagementGroup
            End Get
            Set(ByVal value As String)
                _cashManagementGroup = value
            End Set
        End Property



        <ColumnInfo("PaymentBlock", "'{0}'")> _
        Public Property PaymentBlock() As String
            Get
                Return _paymentBlock
            End Get
            Set(ByVal value As String)
                _paymentBlock = value
            End Set
        End Property



        <ColumnInfo("CustomerLegal", "'{0}'")> _
        Public Property CustomerLegal() As Integer
            Get
                Return _customerLegal
            End Get
            Set(ByVal value As Integer)
                _customerLegal = value
            End Set
        End Property



        <ColumnInfo("TaxCode1", "'{0}'")> _
        Public Property TaxCode1() As String
            Get
                Return _taxCode1
            End Get
            Set(ByVal value As String)
                _taxCode1 = value
            End Set
        End Property


        <ColumnInfo("NickNameDigital", "'{0}'")> _
        Public Property NickNameDigital() As String
            Get
                Return _nicknameDigital
            End Get
            Set(ByVal value As String)
                _nicknameDigital = value
            End Set
        End Property


        <ColumnInfo("NickNameEcommerce", "'{0}'")> _
        Public Property NickNameEcommerce() As String
            Get
                Return _nicknameEcommerce
            End Get
            Set(ByVal value As String)
                _nicknameEcommerce = value
            End Set
        End Property


        <ColumnInfo("Longitude", "'{0}'")> _
        Public Property Longitude() As String
            Get
                Return _longitude
            End Get
            Set(ByVal value As String)
                _longitude = value
            End Set
        End Property


        <ColumnInfo("Latitude", "'{0}'")> _
        Public Property Latitude() As String
            Get
                Return _latitude
            End Get
            Set(ByVal value As String)
                _latitude = value
            End Set
        End Property


        <ColumnInfo("Publish", "{0}")> _
        Public Property Publish() As Boolean
            Get
                Return _publish
            End Get
            Set(ByVal value As Boolean)
                _publish = value
            End Set
        End Property


        <ColumnInfo("DueDate", "{0}")> _
        Public Property DueDate() As Integer
            Get
                Return _dueDate
            End Get
            Set(ByVal value As Integer)
                _dueDate = value
            End Set
        End Property

        <ColumnInfo("AgreementNo", "'{0}'")> _
        Public Property AgreementNo() As String
            Get
                Return _agreementNo
            End Get
            Set(ByVal value As String)
                _agreementNo = value
            End Set
        End Property

        <ColumnInfo("AgreementDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property AgreementDate() As DateTime
            Get
                Return _agreementDate
            End Get
            Set(ByVal value As DateTime)
                _agreementDate = value
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

        <ColumnInfo("OrganizationBranchType", "{0}")> _
        Public Property OrganizationBranchType() As Short
            Get
                Return _orgBranchType
            End Get
            Set(ByVal value As Short)
                _orgBranchType = value
            End Set
        End Property

        <ColumnInfo("ParentDealerID", "{0}")> _
        Public Property ParentDealerID() As Short
            Get
                Return _parentDealerID
            End Get
            Set(ByVal value As Short)
                _parentDealerID = value
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


        <ColumnInfo("Area1ID", "{0}"), _
        RelationInfo("Area1", "ID", "Dealer", "Area1ID")> _
        Public Property Area1() As Area1
            Get
                Try
                    If Not IsNothing(Me._area1) AndAlso (Not Me._area1.IsLoaded) Then

                        Me._area1 = CType(DoLoad(GetType(Area1).ToString(), _area1.ID), Area1)
                        Me._area1.MarkLoaded()

                    End If

                    Return Me._area1

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Area1)

                Me._area1 = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._area1.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("Area2ID", "{0}"), _
        RelationInfo("Area2", "ID", "Dealer", "Area2ID")> _
        Public Property Area2() As Area2
            Get
                Try
                    If Not IsNothing(Me._area2) AndAlso (Not Me._area2.IsLoaded) Then

                        Me._area2 = CType(DoLoad(GetType(Area2).ToString(), _area2.ID), Area2)
                        Me._area2.MarkLoaded()

                    End If

                    Return Me._area2

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Area2)

                Me._area2 = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._area2.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("MainAreaID", "{0}"), _
        RelationInfo("MainArea", "ID", "Dealer", "MainAreaID")> _
        Public Property MainArea As MainArea
            Get
                Try
                    If Not IsNothing(Me._mainArea) AndAlso (Not Me._mainArea.IsLoaded) Then

                        Me._mainArea = CType(DoLoad(GetType(MainArea).ToString(), _mainArea.ID), MainArea)
                        Me._mainArea.MarkLoaded()

                    End If

                    Return Me._mainArea

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MainArea)

                Me._mainArea = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mainArea.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "Dealer", "CityID")> _
        Public Property City() As City
            Get
                Try
                    If Not IsNothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

                        Me._city = CType(DoLoad(GetType(City).ToString(), _city.ID), City)
                        Me._city.MarkLoaded()

                    End If

                    Return Me._city

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As City)

                Me._city = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._city.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerGroupID", "{0}"), _
        RelationInfo("DealerGroup", "ID", "Dealer", "DealerGroupID")> _
        Public Property DealerGroup() As DealerGroup
            Get
                Try
                    If Not IsNothing(Me._dealerGroup) AndAlso (Not Me._dealerGroup.IsLoaded) Then

                        Me._dealerGroup = CType(DoLoad(GetType(DealerGroup).ToString(), _dealerGroup.ID), DealerGroup)
                        Me._dealerGroup.MarkLoaded()

                    End If

                    Return Me._dealerGroup

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerGroup)

                Me._dealerGroup = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerGroup.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ProvinceID", "{0}"), _
        RelationInfo("Province", "ID", "Dealer", "ProvinceID")> _
        Public Property Province() As Province
            Get
                Try
                    If Not IsNothing(Me._province) AndAlso (Not Me._province.IsLoaded) Then

                        Me._province = CType(DoLoad(GetType(Province).ToString(), _province.ID), Province)
                        Me._province.MarkLoaded()

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

        <RelationInfo("Dealer", "ID", "DealerPajak", "DealerID")> _
        Public ReadOnly Property DealerPajaks() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealerPajaks.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DealerPajak), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DealerPajak), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealerPajaks = DoLoadArray(GetType(DealerPajak).ToString, criterias)
                    End If

                    Return Me._dealerPajaks

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Dealer", "ID", "DealerOperationAreaBussiness", "DealerID")> _
        Public ReadOnly Property DealerOperationAreaBusiness() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealerOperationAreaBusiness.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DealerOperationAreaBussiness), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DealerOperationAreaBussiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealerOperationAreaBusiness = DoLoadArray(GetType(DealerOperationAreaBussiness).ToString, criterias)
                    End If

                    Return Me._dealerOperationAreaBusiness

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property



        <RelationInfo("Dealer", "ID", "DealerStallEquipment", "DealerID")> _
        Public ReadOnly Property DealerStallEquipments() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealerStallEquipments.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DealerStallEquipment), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DealerStallEquipment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealerStallEquipments = DoLoadArray(GetType(DealerStallEquipment).ToString, criterias)
                    End If

                    Return Me._dealerStallEquipments

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <RelationInfo("Dealer", "ID", "DealerFacility", "DealerID")> _
        Public ReadOnly Property DealerFacilitys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealerFacitys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(Dealerfacility), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(Dealerfacility), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealerFacitys = DoLoadArray(GetType(Dealerfacility).ToString, criterias)
                    End If

                    Return Me._dealerFacitys

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Dealer", "ID", "DealerPaymentMethod", "DealerID")> _
        Public ReadOnly Property DealerPaymentMethods() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealerPaymentMethods.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DealerPaymentMethod), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DealerPaymentMethod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealerPaymentMethods = DoLoadArray(GetType(DealerPaymentMethod).ToString, criterias)
                    End If

                    Return Me._dealerPaymentMethods

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Dealer", "ID", "DealerBankAccount", "DealerID")> _
        Public ReadOnly Property DealerBankAccounts() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealerBankAccounts.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DealerBankAccount), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealerBankAccounts = DoLoadArray(GetType(DealerBankAccount).ToString, criterias)
                    End If

                    Return Me._dealerBankAccounts

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Dealer", "ID", "DealerBankAccount", "DealerID")> _
        Public ReadOnly Property DealerActiveBankAccounts() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealerBankAccounts.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DealerBankAccount), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(DealerBankAccount), "Status", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealerBankAccounts = DoLoadArray(GetType(DealerBankAccount).ToString, criterias)
                    End If

                    Return Me._dealerBankAccounts

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Dealer", "ID", "BusinessArea", "DealerID")> _
        Public ReadOnly Property BusinessAreas() As System.Collections.ArrayList
            Get
                Try
                    If (Me._businessAreas.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BusinessArea), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._businessAreas = DoLoadArray(GetType(BusinessArea).ToString, criterias)
                    End If

                    Return Me._businessAreas

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Dealer", "ID", "BuletinOrganization", "DealerID")> _
        Public ReadOnly Property BuletinOrganizations() As System.Collections.ArrayList
            Get
                Try
                    If (Me._buletinOrganizations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BuletinOrganization), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BuletinOrganization), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._buletinOrganizations = DoLoadArray(GetType(BuletinOrganization).ToString, criterias)
                    End If

                    Return Me._buletinOrganizations

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Dealer", "ID", "Deposit", "DealerID")> _
        Public ReadOnly Property Deposits() As System.Collections.ArrayList
            Get
                Try
                    If (Me._deposits.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(Deposit), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(Deposit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._deposits = DoLoadArray(GetType(Deposit).ToString, criterias)
                    End If

                    Return Me._deposits

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Dealer", "ID", "DepositC2", "DealerID")> _
        Public ReadOnly Property DepositC2s() As System.Collections.ArrayList
            Get
                Try
                    If (Me._depositC2s.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DepositC2), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DepositC2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._depositC2s = DoLoadArray(GetType(DepositC2).ToString, criterias)
                    End If

                    Return Me._depositC2s

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Dealer", "ID", "DealerAdditional", "DealerID")> _
        Public ReadOnly Property DealerAdditionals() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealerAdditionals.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DealerAdditional), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DealerAdditional), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealerAdditionals = DoLoadArray(GetType(DealerAdditional).ToString, criterias)
                    End If

                    Return Me._dealerAdditionals

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <RelationInfo("Dealer", "ID", "DealerCategory", "DealerID")> _
        Public ReadOnly Property DealerCategorys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealerCategorys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DealerCategory), "Dealer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DealerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealerCategorys = DoLoadArray(GetType(DealerCategory).ToString, criterias)
                    End If

                    Return Me._dealerCategorys

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

        Public Function IsHavingActiveAnnualDeposit(Optional ByRef ProductCategoryCode As String = "3") As Boolean
            Dim cADAH As New CriteriaComposite(New Criteria(GetType(AnnualDepositAHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aADAHs As ArrayList

            'cADAH.opAnd(New Criteria(GetType(AnnualDepositAHeader), "Status", MatchType.Exact, 0))
            cADAH.opAnd(New Criteria(GetType(AnnualDepositAHeader), "Dealer.ID", MatchType.Exact, Me.ID))


            Dim strSql As String = ""

            Dim dtStart As DateTime = New DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0).AddYears(-3)
            Dim dtEnd As DateTime = New DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0).AddYears(1).AddDays(-1)
            strSql = EnumDepositA.RetrieveAnnual(dtStart, dtEnd, EnumDepositA.StatusPencairanAnnual.BelumCair)

            cADAH.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "ID", MatchType.InSet, "(" & strSql & ")"))


            aADAHs = DoLoadArray(GetType(AnnualDepositAHeader).ToString(), cADAH)

            If IsNothing(aADAHs) Then
                aADAHs = New ArrayList
            Else
                If aADAHs.Count > 0 Then
                    ProductCategoryCode = CType(aADAHs(0), AnnualDepositAHeader).ProductCategory.ID
                End If

            End If


            Return (aADAHs.Count > 0)

        End Function

        Public ReadOnly Property IsDealerDMS()
            Get
                Dim criterias As New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, Me.ID))
                Dim datas As ArrayList = DoLoadArray(GetType(DealerSystems).ToString, criterias)
                For Each data As DealerSystems In datas
                    If data.GoLiveDate <> CDate("1753-01-01") Then
                        Return True
                    End If
                Next

                Return False
            End Get
        End Property
#End Region

#Region "Non_generated Properties"

        Private _transactionControlStatus As String = String.Empty
        Private _trCLastUpdate As Date = New Date(1900, 1, 1)
        Private _trCLastUpdateBy As String = String.Empty
        Private _transactionControlPK As Domain.TransactionControlPK = Nothing
        Private _isBranch As Boolean = False
        Private _branchCode As String = String.Empty
        Private _equipmentClass As String = String.Empty
        Private _serviceGrade As String = String.Empty
        Private _dealerfacility As String = String.Empty
        Private _dealerStallEquipment As String = String.Empty
        Private _dealerPaymentMethod As String = String.Empty

        Public Property IsBranch() As Boolean
            Get
                Return _isBranch
            End Get
            Set(value As Boolean)
                _isBranch = value
            End Set
        End Property

        Public Property BranchCode() As String
            Get
                Return _branchCode
            End Get
            Set(value As String)
                _branchCode = value
            End Set
        End Property

        Public Property EquipmentClass() As String
            Get
                Return _equipmentClass
            End Get
            Set(value As String)
                _equipmentClass = value
            End Set
        End Property

        Public Property ServiceGrade() As String
            Get
                Return _serviceGrade
            End Get
            Set(value As String)
                _serviceGrade = value
            End Set
        End Property

        Public Property DealerFacility() As String
            Get
                Return _dealerfacility
            End Get
            Set(value As String)
                _dealerfacility = value
            End Set
        End Property

        Public Property DealerStallEquipment() As String
            Get
                Return _dealerStallEquipment
            End Get
            Set(value As String)
                _dealerStallEquipment = value
            End Set
        End Property

        Public Property DealerPaymentMethod() As String
            Get
                Return _dealerPaymentMethod
            End Get
            Set(value As String)
                _dealerPaymentMethod = value
            End Set
        End Property


        Public ReadOnly Property StatusDealer() As String
            Get
                If _status = CType(EnumDealerStatus.DealerStatus.Aktive, String) Then
                    Return "Aktif"
                Else
                    Return "Tidak Aktif"
                End If

            End Get

        End Property

        Public ReadOnly Property TitleDealer() As String
            Get

                If _title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                    Return "DEALER"
                ElseIf _title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                    Return "KTB"
                ElseIf _title = CType(EnumDealerTittle.DealerTittle.LEASING, String) Then
                    Return "LEASING"
                Else
                    Return "LAINNYA"
                End If

            End Get
        End Property

        Public Property TransactionControlStatus() As String
            Get
                Return _transactionControlStatus
            End Get
            Set(ByVal value As String)
                _transactionControlStatus = value
            End Set
        End Property

        Public Property TrCLastUpdate() As Date
            Get
                Return _trCLastUpdate
            End Get
            Set(ByVal value As Date)
                _trCLastUpdate = value
            End Set
        End Property

        Public Property TrCLastUpdateBy() As String
            Get
                Return _trCLastUpdateBy
            End Get
            Set(ByVal value As String)
                _trCLastUpdateBy = value
            End Set
        End Property

        Public ReadOnly Property IsMainDealer() As Boolean
            Get
                If _mainDealer.ID = _iD Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property

        Property TransactionControlPK As TransactionControlPK
            Get
                If (Not IsNothing(_transactionControlPK)) Then
                    _transactionControlPK.MarkLoaded()

                End If
                Return _transactionControlPK
            End Get
            Set(value As TransactionControlPK)
                _transactionControlPK = value
                _transactionControlPK.MarkLoaded()

            End Set
        End Property
        Public Function ShallowCopy() As Dealer
            Return CType(Me.MemberwiseClone, Dealer)
        End Function
#End Region

    End Class
End Namespace

