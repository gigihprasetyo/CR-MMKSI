
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_EmployeeSales Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/04/2018 - 10:03:42
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
    <Serializable(), TableInfo("VWI_EmployeeSales")> _
    Public Class VWI_EmployeeSales
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
        Private _salesmanCode As String = String.Empty
        Private _name As String = String.Empty
        Private _birthCityID As Integer
        Private _placeOfBirth As String = String.Empty
        Private _dateOfBirth As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _gender As Integer
        Private _marriedStatus As Integer
        Private _address As String = String.Empty
        Private _addressCityID As Integer
        Private _city As String = String.Empty
        Private _salesmanAreaID As Integer
        Private _salesmanAreaCode As String = String.Empty
        Private _salesmanAreaDesc As String = String.Empty
        Private _salesmanLevelID As Integer
        Private _salesmanLevelDesc As String = String.Empty
        Private _jobPositionID As Integer
        Private _jobPositionDesc As String = String.Empty
        Private _leaderId As Integer
        Private _leaderSalesmanCode As String = String.Empty
        Private _leaderSalesmanName As String = String.Empty
        Private _hireDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _resignDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _resignReason As String = String.Empty
        Private _dealerId As Short
        Private _dealerCode As String = String.Empty
        Private _dealerBranchID As Short
        Private _dealerBranchCode As String = String.Empty
        Private _status As Integer
        Private _statusDNET As String = String.Empty
        Private _salesmanStatusDNET As Integer

        Private _noKTP As String = String.Empty
        Private _kategori As String = String.Empty
        Private _pendidikan As String = String.Empty
        Private _noHP As String = String.Empty
        Private _email As String = String.Empty

        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")>
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("SalesmanCode", "'{0}'")>
        Public Property SalesmanCode As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property

        <ColumnInfo("Name", "'{0}'")>
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        <ColumnInfo("BirthCityID", "{0}")>
        Public Property BirthCityID As Integer
            Get
                Return _birthCityID
            End Get
            Set(ByVal value As Integer)
                _birthCityID = value
            End Set
        End Property

        <ColumnInfo("PlaceOfBirth", "'{0}'")>
        Public Property PlaceOfBirth As String
            Get
                Return _placeOfBirth
            End Get
            Set(ByVal value As String)
                _placeOfBirth = value
            End Set
        End Property

        <ColumnInfo("DateOfBirth", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property DateOfBirth As DateTime
            Get
                Return _dateOfBirth
            End Get
            Set(ByVal value As DateTime)
                _dateOfBirth = value
            End Set
        End Property

        <ColumnInfo("Gender", "{0}")>
        Public Property Gender As Integer
            Get
                Return _gender
            End Get
            Set(ByVal value As Integer)
                _gender = value
            End Set
        End Property

        <ColumnInfo("MarriedStatus", "{0}")>
        Public Property MarriedStatus As Integer
            Get
                Return _marriedStatus
            End Get
            Set(ByVal value As Integer)
                _marriedStatus = value
            End Set
        End Property

        <ColumnInfo("Address", "'{0}'")>
        Public Property Address As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property

        <ColumnInfo("AddressCityID", "{0}")>
        Public Property AddressCityID As Integer
            Get
                Return _addressCityID
            End Get
            Set(ByVal value As Integer)
                _addressCityID = value
            End Set
        End Property

        <ColumnInfo("City", "'{0}'")>
        Public Property City As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property

        <ColumnInfo("SalesmanAreaID", "{0}")>
        Public Property SalesmanAreaID As Integer
            Get
                Return _salesmanAreaID
            End Get
            Set(ByVal value As Integer)
                _salesmanAreaID = value
            End Set
        End Property

        <ColumnInfo("SalesmanAreaCode", "{0}")>
        Public Property SalesmanAreaCode As String
            Get
                Return _salesmanAreaCode
            End Get
            Set(ByVal value As String)
                _salesmanAreaCode = value
            End Set
        End Property

        <ColumnInfo("SalesmanAreaDesc", "{0}")>
        Public Property SalesmanAreaDesc As String
            Get
                Return _salesmanAreaDesc
            End Get
            Set(ByVal value As String)
                _salesmanAreaDesc = value
            End Set
        End Property

        <ColumnInfo("SalesmanLevelID", "{0}")>
        Public Property SalesmanLevelID As Integer
            Get
                Return _salesmanLevelID
            End Get
            Set(ByVal value As Integer)
                _salesmanLevelID = value
            End Set
        End Property

        <ColumnInfo("SalesmanLevelDesc", "{0}")>
        Public Property SalesmanLevelDesc As String
            Get
                Return _salesmanLevelDesc
            End Get
            Set(ByVal value As String)
                _salesmanLevelDesc = value
            End Set
        End Property

        <ColumnInfo("JobPositionID", "{0}")>
        Public Property JobPositionID As Integer
            Get
                Return _jobPositionID
            End Get
            Set(ByVal value As Integer)
                _jobPositionID = value
            End Set
        End Property

        <ColumnInfo("JobPositionDesc", "{0}")>
        Public Property JobPositionDesc As String
            Get
                Return _jobPositionDesc
            End Get
            Set(ByVal value As String)
                _jobPositionDesc = value
            End Set
        End Property

        <ColumnInfo("LeaderId", "{0}")>
        Public Property LeaderId As Integer
            Get
                Return _leaderId
            End Get
            Set(ByVal value As Integer)
                _leaderId = value
            End Set
        End Property

        <ColumnInfo("LeaderSalesmanCode", "'{0}'")>
        Public Property LeaderSalesmanCode As String
            Get
                Return _leaderSalesmanCode
            End Get
            Set(ByVal value As String)
                _leaderSalesmanCode = value
            End Set
        End Property

        <ColumnInfo("LeaderSalesmanName", "'{0}'")>
        Public Property LeaderSalesmanName As String
            Get
                Return _leaderSalesmanName
            End Get
            Set(ByVal value As String)
                _leaderSalesmanName = value
            End Set
        End Property

        <ColumnInfo("HireDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property HireDate As DateTime
            Get
                Return _hireDate
            End Get
            Set(ByVal value As DateTime)
                _hireDate = value
            End Set
        End Property

        <ColumnInfo("ResignDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ResignDate As DateTime
            Get
                Return _resignDate
            End Get
            Set(ByVal value As DateTime)
                _resignDate = value
            End Set
        End Property

        <ColumnInfo("ResignReason", "'{0}'")>
        Public Property ResignReason As String
            Get
                Return _resignReason
            End Get
            Set(ByVal value As String)
                _resignReason = value
            End Set
        End Property

        <ColumnInfo("DealerId", "{0}")>
        Public Property DealerId As Short
            Get
                Return _dealerId
            End Get
            Set(ByVal value As Short)
                _dealerId = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")>
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("DealerBranchID", "{0}")>
        Public Property DealerBranchID As Short
            Get
                Return _dealerBranchID
            End Get
            Set(ByVal value As Short)
                _dealerBranchID = value
            End Set
        End Property

        <ColumnInfo("DealerBranchCode", "'{0}'")>
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")>
        Public Property Status As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property

        <ColumnInfo("StatusDNET", "{0}")>
        Public Property StatusDNET As String
            Get
                Return _statusDNET
            End Get
            Set(ByVal value As String)
                _statusDNET = value
            End Set
        End Property

        <ColumnInfo("SalesmanStatusDNET", "{0}")>
        Public Property SalesmanStatusDNET As Integer
            Get
                Return _salesmanStatusDNET
            End Get
            Set(ByVal value As Integer)
                _salesmanStatusDNET = value
            End Set
        End Property

        <ColumnInfo("NoKTP", "'{0}'")>
        Public Property NoKTP As String
            Get
                Return _noKTP
            End Get
            Set(ByVal value As String)
                _noKTP = value
            End Set
        End Property

        <ColumnInfo("Kategori", "'{0}'")>
        Public Property Kategori As String
            Get
                Return _kategori
            End Get
            Set(ByVal value As String)
                _kategori = value
            End Set
        End Property

        <ColumnInfo("Pendidikan", "{0}")>
        Public Property Pendidikan As String
            Get
                Return _pendidikan
            End Get
            Set(ByVal value As String)
                _pendidikan = value
            End Set
        End Property

        <ColumnInfo("NoHP", "'{0}'")>
        Public Property NoHP As String
            Get
                Return _noHP
            End Get
            Set(ByVal value As String)
                _noHP = value
            End Set
        End Property

        <ColumnInfo("Email", "'{0}'")>
        Public Property Email As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property

        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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