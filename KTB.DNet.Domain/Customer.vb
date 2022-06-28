#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Customer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/31/2007 - 10:09:08 AM
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
    <Serializable(), TableInfo("Customer")> _
    Public Class Customer
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
        Private _code As String = String.Empty
        Private _reffCode As String = String.Empty
        Private _name1 As String = String.Empty
        Private _name2 As String = String.Empty
        Private _name3 As String = String.Empty
        Private _alamat As String = String.Empty
        Private _kelurahan As String = String.Empty
        Private _kecamatan As String = String.Empty
        Private _postalCode As String = String.Empty
        Private _preArea As String = String.Empty
        Private _printRegion As String = String.Empty
        Private _phoneNo As String = String.Empty
        Private _email As String = String.Empty
        Private _attachment As String = String.Empty
        Private _status As Short
        Private _deletionMark As Short = 0
        Private _completeName As String = String.Empty

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _city As City

        Private _endCustomers As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _customerDealers As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _customerProfiles As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("ReffCode", "'{0}'")> _
        Public Property ReffCode() As String
            Get
                Return _reffCode
            End Get
            Set(ByVal value As String)
                _reffCode = value
            End Set
        End Property


        <ColumnInfo("Name1", "'{0}'")> _
        Public Property Name1() As String
            Get
                Return _name1
            End Get
            Set(ByVal value As String)
                _name1 = value
            End Set
        End Property


        <ColumnInfo("Name2", "'{0}'")> _
        Public Property Name2() As String
            Get
                Return _name2
            End Get
            Set(ByVal value As String)
                _name2 = value
            End Set
        End Property


        <ColumnInfo("Name3", "'{0}'")> _
        Public Property Name3() As String
            Get
                Return _name3
            End Get
            Set(ByVal value As String)
                _name3 = value
            End Set
        End Property


        <ColumnInfo("Alamat", "'{0}'")> _
        Public Property Alamat() As String
            Get
                Return _alamat
            End Get
            Set(ByVal value As String)
                _alamat = value
            End Set
        End Property


        <ColumnInfo("Kelurahan", "'{0}'")> _
        Public Property Kelurahan() As String
            Get
                Return _kelurahan
            End Get
            Set(ByVal value As String)
                _kelurahan = value
            End Set
        End Property


        <ColumnInfo("Kecamatan", "'{0}'")> _
        Public Property Kecamatan() As String
            Get
                Return _kecamatan
            End Get
            Set(ByVal value As String)
                _kecamatan = value
            End Set
        End Property


        <ColumnInfo("PostalCode", "'{0}'")> _
        Public Property PostalCode() As String
            Get
                Return _postalCode
            End Get
            Set(ByVal value As String)
                _postalCode = value
            End Set
        End Property


        <ColumnInfo("PreArea", "'{0}'")> _
        Public Property PreArea() As String
            Get
                Return _preArea
            End Get
            Set(ByVal value As String)
                _preArea = value
            End Set
        End Property


        <ColumnInfo("PrintRegion", "'{0}'")> _
        Public Property PrintRegion() As String
            Get
                Return _printRegion
            End Get
            Set(ByVal value As String)
                _printRegion = value
            End Set
        End Property


        <ColumnInfo("PhoneNo", "'{0}'")> _
        Public Property PhoneNo() As String
            Get
                Return _phoneNo
            End Get
            Set(ByVal value As String)
                _phoneNo = value
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


        <ColumnInfo("Attachment", "'{0}'")> _
        Public Property Attachment() As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property
        <ColumnInfo("CompleteName", "'{0}'")> _
        Public Property CompleteName() As String
            Get
                Return _completeName
            End Get
            Set(ByVal value As String)
                _completeName = value
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


        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "Customer", "CityID")> _
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


        <RelationInfo("Customer", "ID", "EndCustomer", "CustomerID")> _
        Public ReadOnly Property EndCustomers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._endCustomers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EndCustomer), "Customer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._endCustomers = DoLoadArray(GetType(EndCustomer).ToString, criterias)
                    End If

                    Return Me._endCustomers

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Customer", "ID", "CustomerDealer", "NewCustomerID")> _
        Public ReadOnly Property CustomerDealers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._customerDealers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CustomerDealer), "Customer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._customerDealers = DoLoadArray(GetType(CustomerDealer).ToString, criterias)
                    End If

                    Return Me._customerDealers

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Customer", "ID", "CustomerProfile", "CustomerID")> _
        Public ReadOnly Property CustomerProfiles() As System.Collections.ArrayList
            Get
                Try
                    If (Me._customerProfiles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CustomerProfile), "Customer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._customerProfiles = DoLoadArray(GetType(CustomerProfile).ToString, criterias)
                    End If

                    Return Me._customerProfiles

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <ColumnInfo("DeletionMark", "{0}")> _
        Public Property DeletionMark() As Short
            Get
                Return _deletionMark
            End Get
            Set(ByVal value As Short)
                _deletionMark = value
            End Set
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
        Private _customerRequest As CustomerRequest
        <ColumnInfo("Code", "{0}"), _
        RelationInfo("CustomerRequest", "CustomerCode", "Customer", "Code")> _
        Public Property CustomerRequest() As CustomerRequest
            Get
                Try
                    If Not IsNothing(Me._customerRequest) AndAlso (Not Me._customerRequest.IsLoaded) Then

                        Me._customerRequest = CType(DoLoad(GetType(CustomerRequest).ToString(), _customerRequest.ID), CustomerRequest)
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

            Set(ByVal value As CustomerRequest)

                Me._customerRequest = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._customerRequest.MarkLoaded()
                End If
            End Set
        End Property


        Private _myCustomerRequest As CustomerRequest
        <ColumnInfo("Code", "{0}"), _
          RelationInfo("Customer", "CustomerCode", "EndCustomer", "Code")> _
        Public Property MyCustomerRequest() As CustomerRequest
            Get
                If IsNothing(_myCustomerRequest) Then 'logic based on CustomerRequestFacade.RetrieveCodeDesc
                    'Dim i, j As Integer
                    Dim sortColl As SortCollection = New SortCollection
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(CustomerRequest), "CustomerCode", MatchType.Exact, Code))
                    sortColl.Add(New Sort(GetType(CustomerRequest), "ProcessDate", Sort.SortDirection.DESC))
                    Dim CustomerRequestColl As ArrayList = DoLoadArray(GetType(CustomerRequest).ToString, criterias) ' = m_CustomerRequestMapper.RetrieveByCriteria(criterias, sortColl)
                    Dim ProcessDate As Date = DateSerial(1753, 1, 1)
                    For Each oCR As CustomerRequest In CustomerRequestColl
                        If oCR.ProcessDate >= ProcessDate Then
                            _myCustomerRequest = oCR
                            ProcessDate = oCR.ProcessDate
                        End If
                    Next
                End If
                Return _myCustomerRequest
            End Get
            Set(ByVal Value As CustomerRequest)
                _myCustomerRequest = Value
            End Set
        End Property

        Private _IsChangedWSM As Boolean

        Public Property IsChangedWSM() As Boolean
            Get
                Return _IsChangedWSM
            End Get
            Set(ByVal value As Boolean)
                _IsChangedWSM = value
            End Set
        End Property

#End Region

    End Class
End Namespace

