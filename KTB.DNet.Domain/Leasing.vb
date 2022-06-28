
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Leasing Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 3/2/2018 - 1:46:17 PM
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
    <Serializable(), TableInfo("Leasing")> _
    Public Class Leasing
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
        Private _leasingGroupName As String = String.Empty
        Private _leasingCode As String = String.Empty
        Private _leasingName As String = String.Empty
        Private _city As String = String.Empty
        Private _alamat As String = String.Empty
        Private _kelurahan As String = String.Empty
        Private _kecamatan As String = String.Empty
        Private _province As String = String.Empty
        Private _postalCode As String = String.Empty
        Private _phoneNo As String = String.Empty
        Private _fax As String = String.Empty
        Private _webSite As String = String.Empty
        Private _email As String = String.Empty
        Private _contactPerson As String = String.Empty
        Private _hP As String = String.Empty
        Private _status As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
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


        <ColumnInfo("LeasingGroupName", "'{0}'")> _
        Public Property LeasingGroupName As String
            Get
                Return _leasingGroupName
            End Get
            Set(ByVal value As String)
                _leasingGroupName = value
            End Set
        End Property


        <ColumnInfo("LeasingCode", "'{0}'")> _
        Public Property LeasingCode As String
            Get
                Return _leasingCode
            End Get
            Set(ByVal value As String)
                _leasingCode = value
            End Set
        End Property


        <ColumnInfo("LeasingName", "'{0}'")> _
        Public Property LeasingName As String
            Get
                Return _leasingName
            End Get
            Set(ByVal value As String)
                _leasingName = value
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


        <ColumnInfo("Province", "'{0}'")> _
        Public Property Province As String
            Get
                Return _province
            End Get
            Set(ByVal value As String)
                _province = value
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


        <ColumnInfo("PhoneNo", "'{0}'")> _
        Public Property PhoneNo As String
            Get
                Return _phoneNo
            End Get
            Set(ByVal value As String)
                _phoneNo = value
            End Set
        End Property


        <ColumnInfo("Fax", "'{0}'")> _
        Public Property Fax As String
            Get
                Return _fax
            End Get
            Set(ByVal value As String)
                _fax = value
            End Set
        End Property


        <ColumnInfo("WebSite", "'{0}'")> _
        Public Property WebSite As String
            Get
                Return _webSite
            End Get
            Set(ByVal value As String)
                _webSite = value
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


        <ColumnInfo("ContactPerson", "'{0}'")> _
        Public Property ContactPerson As String
            Get
                Return _contactPerson
            End Get
            Set(ByVal value As String)
                _contactPerson = value
            End Set
        End Property


        <ColumnInfo("HP", "'{0}'")> _
        Public Property HP As String
            Get
                Return _hP
            End Get
            Set(ByVal value As String)
                _hP = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
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

