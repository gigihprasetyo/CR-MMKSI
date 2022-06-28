#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFDCustomer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/31/2021 - 1:38:31 PM
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
    <Serializable(), TableInfo("SFDCustomer")> _
    Public Class SFDCustomer
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Long)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Long
        Private _salesmanCode As String = String.Empty
        Private _customerType As String = String.Empty
        Private _classType As String = String.Empty
        Private _levelData As String = String.Empty
        Private _customerClass As String = String.Empty
        Private _customerTypeDNET As Short
        Private _customerSubClass As Short
        Private _customerNo As String = String.Empty
        Private _parentCustomerNo As String = String.Empty
        Private _firstName As String = String.Empty
        Private _lastName As String = String.Empty
        Private _birthDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _gender As Short
        Private _hPNo As String = String.Empty
        Private _otherPhoneType As Short
        Private _otherPhoneNo As String = String.Empty
        Private _email As String = String.Empty
        Private _gedung As String = String.Empty
        Private _alamat As String = String.Empty
        Private _kelurahan As String = String.Empty
        Private _kecamatan As String = String.Empty
        Private _preArea As String = String.Empty
        Private _postalCode As String = String.Empty
        Private _pOBox As String = String.Empty
        Private _identityType As Short
        Private _identityNumber As String = String.Empty
        Private _identityURLPath As String = String.Empty
        Private _nPWPNo As String = String.Empty
        Private _nPWPName As String = String.Empty
        Private _printRegion As Short
        Private _oCRIdentityID As Integer
        Private _notes As String
        Private _interfaceStatus As Short
        Private _interfaceMessage As String = String.Empty
        Private _interfaceCustSales As Short
        Private _gUID As String = String.Empty
        Private _gUIDUpdate As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _city As City
        Private _dealer As Dealer
        'Private _sPKMasterCountryCodePhone As SPKMasterCountryCodePhone
        Private _sPKMasterCountryCodePhoneID As Integer

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Long
            Get
                Return _iD
            End Get
            Set(ByVal value As Long)
                _iD = value
            End Set
        End Property


        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property


        <ColumnInfo("CustomerType", "'{0}'")> _
        Public Property CustomerType As String
            Get
                Return _customerType
            End Get
            Set(ByVal value As String)
                _customerType = value
            End Set
        End Property


        <ColumnInfo("ClassType", "'{0}'")> _
        Public Property ClassType As String
            Get
                Return _classType
            End Get
            Set(ByVal value As String)
                _classType = value
            End Set
        End Property


        <ColumnInfo("LevelData", "'{0}'")> _
        Public Property LevelData As String
            Get
                Return _levelData
            End Get
            Set(ByVal value As String)
                _levelData = value
            End Set
        End Property


        <ColumnInfo("CustomerClass", "'{0}'")> _
        Public Property CustomerClass As String
            Get
                Return _customerClass
            End Get
            Set(ByVal value As String)
                _customerClass = value
            End Set
        End Property


        <ColumnInfo("CustomerTypeDNET", "{0}")> _
        Public Property CustomerTypeDNET As Short
            Get
                Return _customerTypeDNET
            End Get
            Set(ByVal value As Short)
                _customerTypeDNET = value
            End Set
        End Property


        <ColumnInfo("CustomerSubClass", "{0}")> _
        Public Property CustomerSubClass As Short
            Get
                Return _customerSubClass
            End Get
            Set(ByVal value As Short)
                _customerSubClass = value
            End Set
        End Property


        <ColumnInfo("CustomerNo", "'{0}'")> _
        Public Property CustomerNo As String
            Get
                Return _customerNo
            End Get
            Set(ByVal value As String)
                _customerNo = value
            End Set
        End Property


        <ColumnInfo("ParentCustomerNo", "'{0}'")> _
        Public Property ParentCustomerNo As String
            Get
                Return _parentCustomerNo
            End Get
            Set(ByVal value As String)
                _parentCustomerNo = value
            End Set
        End Property


        <ColumnInfo("FirstName", "'{0}'")> _
        Public Property FirstName As String
            Get
                Return _firstName
            End Get
            Set(ByVal value As String)
                _firstName = value
            End Set
        End Property


        <ColumnInfo("LastName", "'{0}'")> _
        Public Property LastName As String
            Get
                Return _lastName
            End Get
            Set(ByVal value As String)
                _lastName = value
            End Set
        End Property


        <ColumnInfo("BirthDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BirthDate As DateTime
            Get
                Return _birthDate
            End Get
            Set(ByVal value As DateTime)
                _birthDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Gender", "{0}")> _
        Public Property Gender As Short
            Get
                Return _gender
            End Get
            Set(ByVal value As Short)
                _gender = value
            End Set
        End Property



        <ColumnInfo("HPNo", "'{0}'")> _
        Public Property HPNo As String
            Get
                Return _hPNo
            End Get
            Set(ByVal value As String)
                _hPNo = value
            End Set
        End Property


        <ColumnInfo("OtherPhoneType", "{0}")> _
        Public Property OtherPhoneType As Short
            Get
                Return _otherPhoneType
            End Get
            Set(ByVal value As Short)
                _otherPhoneType = value
            End Set
        End Property


        <ColumnInfo("OtherPhoneNo", "'{0}'")> _
        Public Property OtherPhoneNo As String
            Get
                Return _otherPhoneNo
            End Get
            Set(ByVal value As String)
                _otherPhoneNo = value
            End Set
        End Property


        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property


        <ColumnInfo("Gedung", "'{0}'")> _
        Public Property Gedung As String
            Get
                Return _gedung
            End Get
            Set(ByVal value As String)
                _gedung = value
            End Set
        End Property


        <ColumnInfo("Alamat", "'{0}'")> _
        Public Property Alamat As String
            Get
                Return _alamat
            End Get
            Set(ByVal value As String)
                _alamat = value
            End Set
        End Property


        <ColumnInfo("Kelurahan", "'{0}'")> _
        Public Property Kelurahan As String
            Get
                Return _kelurahan
            End Get
            Set(ByVal value As String)
                _kelurahan = value
            End Set
        End Property


        <ColumnInfo("Kecamatan", "'{0}'")> _
        Public Property Kecamatan As String
            Get
                Return _kecamatan
            End Get
            Set(ByVal value As String)
                _kecamatan = value
            End Set
        End Property


        <ColumnInfo("PreArea", "'{0}'")> _
        Public Property PreArea As String
            Get
                Return _preArea
            End Get
            Set(ByVal value As String)
                _preArea = value
            End Set
        End Property


        <ColumnInfo("PostalCode", "'{0}'")> _
        Public Property PostalCode As String
            Get
                Return _postalCode
            End Get
            Set(ByVal value As String)
                _postalCode = value
            End Set
        End Property


        <ColumnInfo("POBox", "'{0}'")> _
        Public Property POBox As String
            Get
                Return _pOBox
            End Get
            Set(ByVal value As String)
                _pOBox = value
            End Set
        End Property


        <ColumnInfo("IdentityType", "{0}")> _
        Public Property IdentityType As Short
            Get
                Return _identityType
            End Get
            Set(ByVal value As Short)
                _identityType = value
            End Set
        End Property


        <ColumnInfo("IdentityNumber", "'{0}'")> _
        Public Property IdentityNumber As String
            Get
                Return _identityNumber
            End Get
            Set(ByVal value As String)
                _identityNumber = value
            End Set
        End Property


        <ColumnInfo("IdentityURLPath", "'{0}'")> _
        Public Property IdentityURLPath As String
            Get
                Return _identityURLPath
            End Get
            Set(ByVal value As String)
                _identityURLPath = value
            End Set
        End Property


        <ColumnInfo("NPWPNo", "'{0}'")> _
        Public Property NPWPNo As String
            Get
                Return _nPWPNo
            End Get
            Set(ByVal value As String)
                _nPWPNo = value
            End Set
        End Property


        <ColumnInfo("NPWPName", "'{0}'")> _
        Public Property NPWPName As String
            Get
                Return _nPWPName
            End Get
            Set(ByVal value As String)
                _nPWPName = value
            End Set
        End Property


        <ColumnInfo("PrintRegion", "{0}")> _
        Public Property PrintRegion As Short
            Get
                Return _printRegion
            End Get
            Set(ByVal value As Short)
                _printRegion = value
            End Set
        End Property

        <ColumnInfo("OCRIdentityID", "{0}")> _
        Public Property OCRIdentityID As Integer
            Get
                Return _oCRIdentityID
            End Get
            Set(ByVal value As Integer)
                _oCRIdentityID = value
            End Set
        End Property


        <ColumnInfo("Notes", "'{0}'")> _
        Public Property Notes As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property


        <ColumnInfo("InterfaceStatus", "{0}")> _
        Public Property InterfaceStatus As Short
            Get
                Return _interfaceStatus
            End Get
            Set(ByVal value As Short)
                _interfaceStatus = value
            End Set
        End Property


        <ColumnInfo("InterfaceMessage", "'{0}'")> _
        Public Property InterfaceMessage As String
            Get
                Return _interfaceMessage
            End Get
            Set(ByVal value As String)
                _interfaceMessage = value
            End Set
        End Property


        <ColumnInfo("InterfaceCustSales", "{0}")> _
        Public Property InterfaceCustSales As Short
            Get
                Return _interfaceCustSales
            End Get
            Set(ByVal value As Short)
                _interfaceCustSales = value
            End Set
        End Property


        <ColumnInfo("GUID", "'{0}'")> _
        Public Property GUID As String
            Get
                Return _gUID
            End Get
            Set(ByVal value As String)
                _gUID = value
            End Set
        End Property


        <ColumnInfo("GUIDUpdate", "'{0}'")> _
        Public Property GUIDUpdate As String
            Get
                Return _gUIDUpdate
            End Get
            Set(ByVal value As String)
                _gUIDUpdate = value
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


        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "SFDCustomer", "CityID")> _
        Public Property City As City
            Get
                Try
                    If Not isnothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._city.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "SFDCustomer", "DealerID")> _
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

        '<ColumnInfo("SPKMasterCountryCodePhoneID", "{0}"), _
        'RelationInfo("SPKMasterCountryCodePhone", "ID", "SFDCustomer", "SPKMasterCountryCodePhoneID")> _
        'Public Property SPKMasterCountryCodePhone As SPKMasterCountryCodePhone
        '    Get
        '        Try
        '            If Not isnothing(Me._sPKMasterCountryCodePhone) AndAlso (Not Me._sPKMasterCountryCodePhone.IsLoaded) Then

        '                Me._sPKMasterCountryCodePhone = CType(DoLoad(GetType(SPKMasterCountryCodePhone).ToString(), _sPKMasterCountryCodePhone.ID), SPKMasterCountryCodePhone)
        '                Me._sPKMasterCountryCodePhone.MarkLoaded()

        '            End If

        '            Return Me._sPKMasterCountryCodePhone

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As SPKMasterCountryCodePhone)

        '        Me._sPKMasterCountryCodePhone = value
        '        If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._sPKMasterCountryCodePhone.MarkLoaded()
        '        End If
        '    End Set
        'End Property


        <ColumnInfo("SPKMasterCountryCodePhoneID", "{0}")> _
        Public Property SPKMasterCountryCodePhoneID As Integer
            Get
                Return _sPKMasterCountryCodePhoneID
            End Get
            Set(ByVal value As Integer)
                _sPKMasterCountryCodePhoneID = value
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

#End Region

    End Class
End Namespace
