
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Vw_CcGridContact Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 6/19/2013 - 4:08:29 PM
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
    <Serializable(), TableInfo("V_CcGridContact")> _
    Public Class V_CcGridContact
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
        Private _ccPeriodID As Integer
        Private _ccCustomerCategoryID As Short
        Private _ccVehicleCategoryID As Short
        Private _dealerID As Short
        Private _nonAuthorizedDealerID As Integer
        Private _sex As String = String.Empty
        Private _consumerName As String = String.Empty
        Private _handphoneNo As String = String.Empty
        Private _vehicleType As String = String.Empty
        Private _nameSTNK As String = String.Empty
        Private _addressSTNK As String = String.Empty
        Private _city As String = String.Empty
        Private _chassisNo As String = String.Empty
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerID As Integer
        Private _odometer As String = String.Empty
        Private _homePhoneAreaCode As String = String.Empty
        Private _homePhoneNo As String = String.Empty
        Private _officePhoneAreaCode As String = String.Empty
        Private _officePhoneNo As String = String.Empty
        Private _officePhoneNoExt As String = String.Empty
        Private _serviceType As String = String.Empty
        Private _ccContactStatusID As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerName As String = String.Empty
        Private _dealerCityName As String = String.Empty
        Private _dealerGroupID As Integer
        Private _area1ID As Integer
        Private _customerCategory As String = String.Empty
        Private _customerCategoryCode As String = String.Empty
        Private _yearMonth As String = String.Empty
        Private _vehicleCategoryCode As String = String.Empty
        Private _vehicleCategory As String = String.Empty
        Private _gelar As String = String.Empty
        Private _area As String = String.Empty
        Private _groupName As String = String.Empty
        Private _statusDescription As String = String.Empty
        Private _provinceName As String = String.Empty
        Private _dealerAddress As String = String.Empty
        Private _dealerCode As String = String.Empty




#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Long
            Get
                Return _iD
            End Get
            Set(ByVal value As Long)
                _iD = value
            End Set
        End Property


        <ColumnInfo("CcPeriodID", "{0}")> _
        Public Property CcPeriodID() As Integer
            Get
                Return _ccPeriodID
            End Get
            Set(ByVal value As Integer)
                _ccPeriodID = value
            End Set
        End Property


        <ColumnInfo("CcCustomerCategoryID", "{0}")> _
        Public Property CcCustomerCategoryID() As Short
            Get
                Return _ccCustomerCategoryID
            End Get
            Set(ByVal value As Short)
                _ccCustomerCategoryID = value
            End Set
        End Property


        <ColumnInfo("CcVehicleCategoryID", "{0}")> _
        Public Property CcVehicleCategoryID() As Short
            Get
                Return _ccVehicleCategoryID
            End Get
            Set(ByVal value As Short)
                _ccVehicleCategoryID = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Short
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("NonAuthorizedDealerID", "{0}")> _
        Public Property NonAuthorizedDealerID() As Integer
            Get
                Return _nonAuthorizedDealerID
            End Get
            Set(ByVal value As Integer)
                _nonAuthorizedDealerID = value
            End Set
        End Property


        <ColumnInfo("Sex", "'{0}'")> _
        Public Property Sex() As String
            Get
                Return _sex
            End Get
            Set(ByVal value As String)
                _sex = value
            End Set
        End Property


        <ColumnInfo("ConsumerName", "'{0}'")> _
        Public Property ConsumerName() As String
            Get
                Return _consumerName
            End Get
            Set(ByVal value As String)
                _consumerName = value
            End Set
        End Property


        <ColumnInfo("HandphoneNo", "'{0}'")> _
        Public Property HandphoneNo() As String
            Get
                Return _handphoneNo
            End Get
            Set(ByVal value As String)
                _handphoneNo = value
            End Set
        End Property


        <ColumnInfo("VehicleType", "'{0}'")> _
        Public Property VehicleType() As String
            Get
                Return _vehicleType
            End Get
            Set(ByVal value As String)
                _vehicleType = value
            End Set
        End Property


        <ColumnInfo("NameSTNK", "'{0}'")> _
        Public Property NameSTNK() As String
            Get
                Return _nameSTNK
            End Get
            Set(ByVal value As String)
                _nameSTNK = value
            End Set
        End Property


        <ColumnInfo("AddressSTNK", "'{0}'")> _
        Public Property AddressSTNK() As String
            Get
                Return _addressSTNK
            End Get
            Set(ByVal value As String)
                _addressSTNK = value
            End Set
        End Property


        <ColumnInfo("City", "'{0}'")> _
        Public Property City() As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property


        <ColumnInfo("ChassisNo", "'{0}'")> _
        Public Property ChassisNo() As String
            Get
                Return _chassisNo
            End Get
            Set(ByVal value As String)
                _chassisNo = value
            End Set
        End Property


        <ColumnInfo("TransactionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransactionDate() As DateTime
            Get
                Return _transactionDate
            End Get
            Set(ByVal value As DateTime)
                _transactionDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerID", "{0}")> _
        Public Property CustomerID() As Integer
            Get
                Return _customerID
            End Get
            Set(ByVal value As Integer)
                _customerID = value
            End Set
        End Property


        <ColumnInfo("Odometer", "'{0}'")> _
        Public Property Odometer() As String
            Get
                Return _odometer
            End Get
            Set(ByVal value As String)
                _odometer = value
            End Set
        End Property


        <ColumnInfo("HomePhoneAreaCode", "'{0}'")> _
        Public Property HomePhoneAreaCode() As String
            Get
                Return _homePhoneAreaCode
            End Get
            Set(ByVal value As String)
                _homePhoneAreaCode = value
            End Set
        End Property


        <ColumnInfo("HomePhoneNo", "'{0}'")> _
        Public Property HomePhoneNo() As String
            Get
                Return _homePhoneNo
            End Get
            Set(ByVal value As String)
                _homePhoneNo = value
            End Set
        End Property


        <ColumnInfo("OfficePhoneAreaCode", "'{0}'")> _
        Public Property OfficePhoneAreaCode() As String
            Get
                Return _officePhoneAreaCode
            End Get
            Set(ByVal value As String)
                _officePhoneAreaCode = value
            End Set
        End Property


        <ColumnInfo("OfficePhoneNo", "'{0}'")> _
        Public Property OfficePhoneNo() As String
            Get
                Return _officePhoneNo
            End Get
            Set(ByVal value As String)
                _officePhoneNo = value
            End Set
        End Property


        <ColumnInfo("OfficePhoneNoExt", "'{0}'")> _
        Public Property OfficePhoneNoExt() As String
            Get
                Return _officePhoneNoExt
            End Get
            Set(ByVal value As String)
                _officePhoneNoExt = value
            End Set
        End Property


        <ColumnInfo("ServiceType", "'{0}'")> _
        Public Property ServiceType() As String
            Get
                Return _serviceType
            End Get
            Set(ByVal value As String)
                _serviceType = value
            End Set
        End Property


        <ColumnInfo("CcContactStatusID", "{0}")> _
        Public Property CcContactStatusID() As Short
            Get
                Return _ccContactStatusID
            End Get
            Set(ByVal value As Short)
                _ccContactStatusID = value
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


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("DealerCityName", "'{0}'")> _
        Public Property DealerCityName() As String
            Get
                Return _dealerCityName
            End Get
            Set(ByVal value As String)
                _dealerCityName = value
            End Set
        End Property


        <ColumnInfo("DealerGroupID", "{0}")> _
        Public Property DealerGroupID() As Integer
            Get
                Return _dealerGroupID
            End Get
            Set(ByVal value As Integer)
                _dealerGroupID = value
            End Set
        End Property


        <ColumnInfo("Area1ID", "{0}")> _
        Public Property Area1ID() As Integer
            Get
                Return _area1ID
            End Get
            Set(ByVal value As Integer)
                _area1ID = value
            End Set
        End Property


        <ColumnInfo("CustomerCategory", "'{0}'")> _
        Public Property CustomerCategory() As String
            Get
                Return _customerCategory
            End Get
            Set(ByVal value As String)
                _customerCategory = value
            End Set
        End Property


        <ColumnInfo("CustomerCategoryCode", "'{0}'")> _
        Public Property CustomerCategoryCode() As String
            Get
                Return _customerCategoryCode
            End Get
            Set(ByVal value As String)
                _customerCategoryCode = value
            End Set
        End Property


        <ColumnInfo("YearMonth", "'{0}'")> _
        Public Property YearMonth() As String
            Get
                Return _yearMonth
            End Get
            Set(ByVal value As String)
                _yearMonth = value
            End Set
        End Property


        <ColumnInfo("VehicleCategoryCode", "'{0}'")> _
        Public Property VehicleCategoryCode() As String
            Get
                Return _vehicleCategoryCode
            End Get
            Set(ByVal value As String)
                _vehicleCategoryCode = value
            End Set
        End Property


        <ColumnInfo("VehicleCategory", "'{0}'")> _
        Public Property VehicleCategory() As String
            Get
                Return _vehicleCategory
            End Get
            Set(ByVal value As String)
                _vehicleCategory = value
            End Set
        End Property


        <ColumnInfo("Gelar", "'{0}'")> _
        Public Property Gelar() As String
            Get
                Return _gelar
            End Get
            Set(ByVal value As String)
                _gelar = value
            End Set
        End Property


        <ColumnInfo("Area", "'{0}'")> _
        Public Property Area() As String
            Get
                Return _area
            End Get
            Set(ByVal value As String)
                _area = value
            End Set
        End Property


        <ColumnInfo("GroupName", "'{0}'")> _
        Public Property GroupName() As String
            Get
                Return _groupName
            End Get
            Set(ByVal value As String)
                _groupName = value
            End Set
        End Property


        <ColumnInfo("StatusDescription", "'{0}'")> _
        Public Property StatusDescription() As String
            Get
                Return _statusDescription
            End Get
            Set(ByVal value As String)
                _statusDescription = value
            End Set
        End Property


        <ColumnInfo("ProvinceName", "'{0}'")> _
        Public Property ProvinceName() As String
            Get
                Return _provinceName
            End Get
            Set(ByVal value As String)
                _provinceName = value
            End Set
        End Property


        <ColumnInfo("DealerAddress", "'{0}'")> _
        Public Property DealerAddress() As String
            Get
                Return _dealerAddress
            End Get
            Set(ByVal value As String)
                _dealerAddress = value
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

