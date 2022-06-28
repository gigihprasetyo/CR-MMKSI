
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKCustomer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 3/7/2011 - 2:13:45 PM
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
    <Serializable(), TableInfo("SPKCustomer")> _
    Public Class SPKCustomer
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
        Private _tipeCustomer As Short
        Private _tipePerusahaan As Short
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
        Private _officeNo As String = String.Empty
        Private _homeNo As String = String.Empty
        Private _hpNo As String = String.Empty
        Private _email As String = String.Empty
        Private _mCPStatus As Short
        Private _lkppStatus As Short
        Private _status As Integer
        Private _lkppReference As String
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _imagePath As String = String.Empty

        Private _city As City
        Private _sapCustomer As SAPCustomer
        Private _OCRIdentity As OCRIdentity
        Private _businessSectorDetail As BusinessSectorDetail
        'CR SPK
        Private _typePerorangan As Integer
        Private _typeIdentitas As String
        Private _countryCode As String = String.Empty
        '

        Private _sPKCustomerProfiles As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("TipeCustomer", "{0}")> _
        Public Property TipeCustomer() As Short
            Get
                Return _tipeCustomer
            End Get
            Set(ByVal value As Short)
                _tipeCustomer = value
            End Set
        End Property


        <ColumnInfo("TipePerusahaan", "{0}")> _
        Public Property TipePerusahaan() As Short
            Get
                Return _tipePerusahaan
            End Get
            Set(ByVal value As Short)
                _tipePerusahaan = value
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


        <ColumnInfo("OfficeNo", "'{0}'")> _
        Public Property OfficeNo() As String
            Get
                Return _officeNo
            End Get
            Set(ByVal value As String)
                _officeNo = value
            End Set
        End Property


        <ColumnInfo("HomeNo", "'{0}'")> _
        Public Property HomeNo() As String
            Get
                Return _homeNo
            End Get
            Set(ByVal value As String)
                _homeNo = value
            End Set
        End Property


        <ColumnInfo("HpNo", "'{0}'")> _
        Public Property HpNo() As String
            Get
                Return _hpNo
            End Get
            Set(ByVal value As String)
                _hpNo = value
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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("MCPStatus", "{0}")> _
        Public Property MCPStatus() As Short
            Get
                Return _mCPStatus
            End Get
            Set(ByVal value As Short)
                _mCPStatus = value
            End Set
        End Property


        <ColumnInfo("LKPPStatus", "{0}")> _
        Public Property LKPPStatus() As Short
            Get
                Return _lkppStatus
            End Get
            Set(ByVal value As Short)
                _lkppStatus = value
            End Set
        End Property
	
        <ColumnInfo("LKPPReference", "{0}")> _
        Public Property LKPPReference() As String
            Get
                Return _lkppReference
            End Get
            Set(ByVal value As String)
                _lkppReference = value
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
	
        <ColumnInfo("BusinessSectorDetailID", "{0}"), _
        RelationInfo("BusinessSectorDetail", "ID", "SPKCustomer", "BusinessSectorDetailID")> _
        Public Property BusinessSectorDetail() As BusinessSectorDetail
            Get
                Try
                    If Not IsNothing(Me._businessSectorDetail) AndAlso (Not Me._businessSectorDetail.IsLoaded) Then

                        Me._businessSectorDetail = CType(DoLoad(GetType(BusinessSectorDetail).ToString(), _businessSectorDetail.ID), BusinessSectorDetail)
                        Me._businessSectorDetail.MarkLoaded()

                    End If

                    Return Me._businessSectorDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BusinessSectorDetail)

                Me._businessSectorDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._businessSectorDetail.MarkLoaded()
                End If
            End Set
        End Property
	
        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "SPKCustomer", "CityID")> _
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

        <ColumnInfo("SAPCustomerID", "{0}"), _
        RelationInfo("SAPCustomer", "ID", "SPKCustomer", "SAPCustomerID")> _
        Public Property SAPCustomer() As SAPCustomer
            Get
                Try
                    If Not IsNothing(Me._sapCustomer) AndAlso (Not Me._sapCustomer.IsLoaded) Then

                        Me._sapCustomer = CType(DoLoad(GetType(SAPCustomer).ToString(), _sapCustomer.ID), SAPCustomer)
                        Me._sapCustomer.MarkLoaded()

                    End If

                    Return Me._sapCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SAPCustomer)

                Me._sapCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sapCustomer.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("SPKCustomer", "ID", "SPKCustomerProfile", "SPKCustomerID")> _
        Public ReadOnly Property SPKCustomerProfiles() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPKCustomerProfiles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPKCustomerProfile), "SPKCustomer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPKCustomerProfiles = DoLoadArray(GetType(SPKCustomerProfile).ToString, criterias)
                    End If

                    Return Me._sPKCustomerProfiles

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("ID", "{0}"), _
     RelationInfo("SPKCustomer", "ID", "OCRIdentity", "SPKCustomerID")> _
        Public ReadOnly Property OCRIdentity As OCRIdentity
            Get
                Try
                    If IsNothing(_OCRIdentity) Then
                        If Me.ID > 0 Then
                            Dim _criteria As Criteria = New Criteria(GetType(OCRIdentity), "SPKCustomerID", Me.ID)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                            criterias.opAnd(New Criteria(GetType(OCRIdentity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(OCRIdentity), "ImagePath", MatchType.Exact, _imagePath))
                            Dim sortColl As SortCollection = New SortCollection
                            sortColl.Add(New Sort(GetType(OCRIdentity), "ID", Sort.SortDirection.DESC))

                            Dim tempColl As ArrayList = DoLoadArray(GetType(OCRIdentity).ToString, criterias, sortColl)

                            If (tempColl.Count > 0) Then
                                Me._OCRIdentity = CType(tempColl(0), OCRIdentity)
                            Else
                                Me._OCRIdentity = Nothing
                            End If
                        End If
                    End If
                    

                    Return Me._OCRIdentity

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
        End Property

        <ColumnInfo("ImagePath", "'{0}'")> _
        Public Property ImagePath() As String
            Get
                Return _imagePath
            End Get
            Set(ByVal value As String)
                _imagePath = value
            End Set
        End Property
        'CR SPK
        <ColumnInfo("TypeIdentitas", "'{0}'")> _
        Public Property TypeIdentitas() As String
            Get
                Return _typeIdentitas
            End Get
            Set(ByVal value As String)
                _typeIdentitas = value
            End Set
        End Property


        <ColumnInfo("TypePerorangan", "{0}")> _
        Public Property TypePerorangan() As Integer
            Get
                Return _typePerorangan
            End Get
            Set(ByVal value As Integer)
                _typePerorangan = value
            End Set
        End Property

        <ColumnInfo("CountryCode", "'{0}'")> _
        Public Property CountryCode() As String
            Get
                Return _countryCode
            End Get
            Set(ByVal value As String)
                _countryCode = value
            End Set
        End Property
        'end CR SPK
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

