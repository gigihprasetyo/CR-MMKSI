
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : FleetCustomer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 06/06/2018 - 13:36:05
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
    <Serializable(), TableInfo("FleetCustomer")> _
    Public Class FleetCustomer
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
        Private _provinceID As Integer
        Private _preArea As String = String.Empty
        Private _businessSectorDetailId As Integer
        Private _ratioMatrixID As Integer
        Private _categoryIndex As Integer
        Private _typeIndex As Integer
        Private _code As String = String.Empty
        Private _name As String = String.Empty
        Private _gedung As String = String.Empty
        Private _alamat As String = String.Empty
        Private _kecamatan As String = String.Empty
        Private _kelurahan As String = String.Empty
        Private _postalCode As String = String.Empty
        Private _email As String = String.Empty
        Private _phoneNo As String = String.Empty
        Private _tipeCustomer As Integer
        Private _identityType As Integer
        Private _identityNumber As String = String.Empty
        Private _attachment As String = String.Empty
        Private _classificationIndex As Integer
        Private _fleetNickName As String = String.Empty
        Private _fleetNote As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _customerGroupID As Integer
        Private _cityID As Short

        Private _customerGroup As CustomerGroup
        Private _city As City

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


        <ColumnInfo("ProvinceID", "{0}")> _
        Public Property ProvinceID As Integer
            Get
                Return _provinceID
            End Get
            Set(ByVal value As Integer)
                _provinceID = value
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


        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "FleetCustomer", "CityID")> _
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


        <ColumnInfo("BusinessSectorDetailId", "{0}")> _
        Public Property BusinessSectorDetailId As Integer
            Get
                Return _businessSectorDetailId
            End Get
            Set(ByVal value As Integer)
                _businessSectorDetailId = value
            End Set
        End Property


        <ColumnInfo("RatioMatrixID", "{0}")> _
        Public Property RatioMatrixID As Integer
            Get
                Return _ratioMatrixID
            End Get
            Set(ByVal value As Integer)
                _ratioMatrixID = value
            End Set
        End Property


        <ColumnInfo("CategoryIndex", "{0}")> _
        Public Property CategoryIndex As Integer
            Get
                Return _categoryIndex
            End Get
            Set(ByVal value As Integer)
                _categoryIndex = value
            End Set
        End Property


        <ColumnInfo("TypeIndex", "{0}")> _
        Public Property TypeIndex As Integer
            Get
                Return _typeIndex
            End Get
            Set(ByVal value As Integer)
                _typeIndex = value
            End Set
        End Property


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
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


        <ColumnInfo("Kecamatan", "'{0}'")> _
        Public Property Kecamatan As String
            Get
                Return _kecamatan
            End Get
            Set(ByVal value As String)
                _kecamatan = value
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


        <ColumnInfo("PostalCode", "'{0}'")> _
        Public Property PostalCode As String
            Get
                Return _postalCode
            End Get
            Set(ByVal value As String)
                _postalCode = value
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


        <ColumnInfo("PhoneNo", "'{0}'")> _
        Public Property PhoneNo As String
            Get
                Return _phoneNo
            End Get
            Set(ByVal value As String)
                _phoneNo = value
            End Set
        End Property


        <ColumnInfo("TipeCustomer", "{0}")> _
        Public Property TipeCustomer As Integer
            Get
                Return _tipeCustomer
            End Get
            Set(ByVal value As Integer)
                _tipeCustomer = value
            End Set
        End Property


        <ColumnInfo("IdentityType", "{0}")> _
        Public Property IdentityType As Integer
            Get
                Return _identityType
            End Get
            Set(ByVal value As Integer)
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


        <ColumnInfo("Attachment", "'{0}'")> _
        Public Property Attachment As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property


        <ColumnInfo("ClassificationIndex", "{0}")> _
        Public Property ClassificationIndex As Integer
            Get
                Return _classificationIndex
            End Get
            Set(ByVal value As Integer)
                _classificationIndex = value
            End Set
        End Property


        <ColumnInfo("FleetNickName", "'{0}'")> _
        Public Property FleetNickName As String
            Get
                Return _fleetNickName
            End Get
            Set(ByVal value As String)
                _fleetNickName = value
            End Set
        End Property


        <ColumnInfo("FleetNote", "'{0}'")> _
        Public Property FleetNote As String
            Get
                Return _fleetNote
            End Get
            Set(ByVal value As String)
                _fleetNote = value
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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property


        <ColumnInfo("CustomerGroupID", "{0}"), _
        RelationInfo("CustomerGroup", "ID", "FleetCustomer", "CustomerGroupID")> _
        Public Property CustomerGroup() As CustomerGroup
            Get
                Try
                    If Not IsNothing(Me._customerGroup) AndAlso (Not Me._customerGroup.IsLoaded) Then

                        Me._customerGroup = CType(DoLoad(GetType(CustomerGroup).ToString(), _customerGroup.ID), CustomerGroup)
                        Me._customerGroup.MarkLoaded()

                    End If

                    Return Me._customerGroup

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CustomerGroup)

                Me._customerGroup = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._customerGroup.MarkLoaded()
                End If
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

