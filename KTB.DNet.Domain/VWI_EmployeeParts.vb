
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_EmployeeParts Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/04/2018 - 14:43:50
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
    <Serializable(), TableInfo("VWI_EmployeeParts")> _
    Public Class VWI_EmployeeParts
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
        Private _placeOfBirth As String = String.Empty
        Private _dateOfBirth As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _gender As Integer
        Private _marriedStatus As Integer
        Private _address As String = String.Empty
        Private _city As String = String.Empty
        Private _hireDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _resignDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _resignReason As String = String.Empty
        Private _dealerId As Short
        Private _dealerCode As String = String.Empty
        Private _dealerBranchId As Integer
        Private _dealerBranchCode As String = String.Empty
        Private _status As Integer
        Private _statusDNET As String = String.Empty
        Private _salesmanStatusDNET As Integer

        Private _salesmanCategoryLevelId As Integer
        Private _positionCode As String
        Private _positionName As String
        Private _parentSalesmanCategoryLevelId As Integer
        Private _parentPositionCode As String
        Private _parentPositionName As String

        Private _noKTP As String = String.Empty
        Private _kategori As String = String.Empty
        Private _pendidikan As String = String.Empty
        Private _noHP As String = String.Empty
        Private _email As String = String.Empty

        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
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

        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
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

        <ColumnInfo("PlaceOfBirth", "'{0}'")> _
        Public Property PlaceOfBirth As String
            Get
                Return _placeOfBirth
            End Get
            Set(ByVal value As String)
                _placeOfBirth = value
            End Set
        End Property

        <ColumnInfo("DateOfBirth", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateOfBirth As DateTime
            Get
                Return _dateOfBirth
            End Get
            Set(ByVal value As DateTime)
                _dateOfBirth = value
            End Set
        End Property

        <ColumnInfo("Gender", "{0}")> _
        Public Property Gender As Integer
            Get
                Return _gender
            End Get
            Set(ByVal value As Integer)
                _gender = value
            End Set
        End Property

        <ColumnInfo("MarriedStatus", "{0}")> _
        Public Property MarriedStatus As Integer
            Get
                Return _marriedStatus
            End Get
            Set(ByVal value As Integer)
                _marriedStatus = value
            End Set
        End Property

        <ColumnInfo("Address", "'{0}'")> _
        Public Property Address As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property

        <ColumnInfo("City", "'{0}'")> _
        Public Property City As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property

        <ColumnInfo("HireDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property HireDate As DateTime
            Get
                Return _hireDate
            End Get
            Set(ByVal value As DateTime)
                _hireDate = value
            End Set
        End Property

        <ColumnInfo("ResignDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ResignDate As DateTime
            Get
                Return _resignDate
            End Get
            Set(ByVal value As DateTime)
                _resignDate = value
            End Set
        End Property

        <ColumnInfo("ResignReason", "'{0}'")> _
        Public Property ResignReason As String
            Get
                Return _resignReason
            End Get
            Set(ByVal value As String)
                _resignReason = value
            End Set
        End Property

        <ColumnInfo("DealerId", "{0}")> _
        Public Property DealerId As Short
            Get
                Return _dealerId
            End Get
            Set(ByVal value As Short)
                _dealerId = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("DealerBranchId", "{0}")> _
        Public Property DealerBranchId As Integer
            Get
                Return _dealerBranchId
            End Get
            Set(ByVal value As Integer)
                _dealerBranchId = value
            End Set
        End Property

        <ColumnInfo("DealerBranchCode", "'{0}'")> _
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

        <ColumnInfo("SalesmanCategoryLevelId", "{0}")> _
        Public Property SalesmanCategoryLevelId As Integer
            Get
                Return _salesmanCategoryLevelId
            End Get
            Set(ByVal value As Integer)
                _salesmanCategoryLevelId = value
            End Set
        End Property

        <ColumnInfo("PositionCode", "'{0}'")>
        Public Property PositionCode As String
            Get
                Return _positionCode
            End Get
            Set(ByVal value As String)
                _positionCode = value
            End Set
        End Property

        <ColumnInfo("PositionName", "{0}")> _
        Public Property PositionName As String
            Get
                Return _positionName
            End Get
            Set(ByVal value As String)
                _positionName = value
            End Set
        End Property

        <ColumnInfo("ParentSalesmanCategoryLevelId", "{0}")> _
        Public Property ParentSalesmanCategoryLevelId As Integer
            Get
                Return _parentSalesmanCategoryLevelId
            End Get
            Set(ByVal value As Integer)
                _parentSalesmanCategoryLevelId = value
            End Set
        End Property

        <ColumnInfo("ParentPositionCode", "'{0}'")>
        Public Property ParentPositionCode As String
            Get
                Return _parentPositionCode
            End Get
            Set(ByVal value As String)
                _parentPositionCode = value
            End Set
        End Property

        <ColumnInfo("ParentPositionName", "{0}")> _
        Public Property ParentPositionName As String
            Get
                Return _parentPositionName
            End Get
            Set(ByVal value As String)
                _parentPositionName = value
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

        <ColumnInfo("Pendidikan", "'{0}'")>
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

        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
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