
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_LeadCustomerSalesForce Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 02/05/2018 - 9:03:57
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
    <Serializable(), TableInfo("VWI_LeadCustomerSalesForce")> _
    Public Class VWI_LeadCustomerSalesForce
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
        Private _dNetID As Integer
        Private _sumberData As String = String.Empty
        Private _createdBy As String = String.Empty
        Private _createDate As String = String.Empty
        Private _createDate_YYYYMMDD As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _customerTypeID As String = String.Empty
        Private _customerType As String = String.Empty
        Private _salesmanCode As String = String.Empty
        Private _name As String = String.Empty
        Private _customerCode As String = String.Empty
        Private _customerName As String = String.Empty
        Private _customerAddress As String = String.Empty
        Private _countryCode As String = String.Empty
        Private _phone As String = String.Empty
        Private _email As String = String.Empty
        Private _sexID As String = String.Empty
        Private _sex As String = String.Empty
        Private _ageSegmentID As String = String.Empty
        Private _ageSegment As String = String.Empty
        Private _customerStatusID As String = String.Empty
        Private _customerStatus As String = String.Empty
        Private _informationTypeID As String = String.Empty
        Private _informationType As String = String.Empty
        Private _informationSourceID As String = String.Empty
        Private _informationSource As String = String.Empty
        Private _customerPurposeID As String = String.Empty
        Private _customerPurpose As String = String.Empty
        Private _prospectDate As String = String.Empty
        Private _prospectDate_YYYYMMDD As String = String.Empty
        Private _vechileTypeCode As String = String.Empty
        Private _description As String = String.Empty
        Private _currVehicleType As String = String.Empty
        Private _currVehicleBrand As String = String.Empty
        Private _note As String = String.Empty
        Private _keterangan As String = String.Empty
        Private _statusResponseID As String = String.Empty
        Private _statusResponse As String = String.Empty
        Private _webID As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)



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


        <ColumnInfo("DNetID", "{0}")> _
        Public Property DNetID As Integer
            Get
                Return _dNetID
            End Get
            Set(ByVal value As Integer)
                _dNetID = value
            End Set
        End Property

        <ColumnInfo("CreatedBy", "'{0}'")>
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property

        <ColumnInfo("SumberData", "'{0}'")> _
        Public Property SumberData As String
            Get
                Return _sumberData
            End Get
            Set(ByVal value As String)
                _sumberData = value
            End Set
        End Property


        <ColumnInfo("CreateDate", "'{0}'")> _
        Public Property CreateDate As String
            Get
                Return _createDate
            End Get
            Set(ByVal value As String)
                _createDate = value
            End Set
        End Property


        <ColumnInfo("CreateDate_YYYYMMDD", "'{0}'")> _
        Public Property CreateDate_YYYYMMDD As String
            Get
                Return _createDate_YYYYMMDD
            End Get
            Set(ByVal value As String)
                _createDate_YYYYMMDD = value
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


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("CustomerTypeID", "'{0}'")> _
        Public Property CustomerTypeID As String
            Get
                Return _customerTypeID
            End Get
            Set(ByVal value As String)
                _customerTypeID = value
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


        <ColumnInfo("CustomerCode", "'{0}'")> _
        Public Property CustomerCode As String
            Get
                Return _customerCode
            End Get
            Set(ByVal value As String)
                _customerCode = value
            End Set
        End Property


        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property


        <ColumnInfo("CustomerAddress", "'{0}'")> _
        Public Property CustomerAddress As String
            Get
                Return _customerAddress
            End Get
            Set(ByVal value As String)
                _customerAddress = value
            End Set
        End Property


        <ColumnInfo("CountryCode", "'{0}'")>
        Public Property CountryCode As String
            Get
                Return _countryCode
            End Get
            Set(ByVal value As String)
                _countryCode = value
            End Set
        End Property

        <ColumnInfo("Phone", "'{0}'")> _
        Public Property Phone As String
            Get
                Return _phone
            End Get
            Set(ByVal value As String)
                _phone = value
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


        <ColumnInfo("SexID", "'{0}'")> _
        Public Property SexID As String
            Get
                Return _sexID
            End Get
            Set(ByVal value As String)
                _sexID = value
            End Set
        End Property


        <ColumnInfo("Sex", "'{0}'")> _
        Public Property Sex As String
            Get
                Return _sex
            End Get
            Set(ByVal value As String)
                _sex = value
            End Set
        End Property


        <ColumnInfo("AgeSegmentID", "'{0}'")> _
        Public Property AgeSegmentID As String
            Get
                Return _ageSegmentID
            End Get
            Set(ByVal value As String)
                _ageSegmentID = value
            End Set
        End Property


        <ColumnInfo("AgeSegment", "'{0}'")> _
        Public Property AgeSegment As String
            Get
                Return _ageSegment
            End Get
            Set(ByVal value As String)
                _ageSegment = value
            End Set
        End Property


        <ColumnInfo("CustomerStatusID", "'{0}'")> _
        Public Property CustomerStatusID As String
            Get
                Return _customerStatusID
            End Get
            Set(ByVal value As String)
                _customerStatusID = value
            End Set
        End Property


        <ColumnInfo("CustomerStatus", "'{0}'")> _
        Public Property CustomerStatus As String
            Get
                Return _customerStatus
            End Get
            Set(ByVal value As String)
                _customerStatus = value
            End Set
        End Property


        <ColumnInfo("InformationTypeID", "'{0}'")> _
        Public Property InformationTypeID As String
            Get
                Return _informationTypeID
            End Get
            Set(ByVal value As String)
                _informationTypeID = value
            End Set
        End Property


        <ColumnInfo("InformationType", "'{0}'")> _
        Public Property InformationType As String
            Get
                Return _informationType
            End Get
            Set(ByVal value As String)
                _informationType = value
            End Set
        End Property


        <ColumnInfo("InformationSourceID", "'{0}'")> _
        Public Property InformationSourceID As String
            Get
                Return _informationSourceID
            End Get
            Set(ByVal value As String)
                _informationSourceID = value
            End Set
        End Property


        <ColumnInfo("InformationSource", "'{0}'")> _
        Public Property InformationSource As String
            Get
                Return _informationSource
            End Get
            Set(ByVal value As String)
                _informationSource = value
            End Set
        End Property


        <ColumnInfo("CustomerPurposeID", "'{0}'")> _
        Public Property CustomerPurposeID As String
            Get
                Return _customerPurposeID
            End Get
            Set(ByVal value As String)
                _customerPurposeID = value
            End Set
        End Property


        <ColumnInfo("CustomerPurpose", "'{0}'")> _
        Public Property CustomerPurpose As String
            Get
                Return _customerPurpose
            End Get
            Set(ByVal value As String)
                _customerPurpose = value
            End Set
        End Property


        <ColumnInfo("ProspectDate", "'{0}'")> _
        Public Property ProspectDate As String
            Get
                Return _prospectDate
            End Get
            Set(ByVal value As String)
                _prospectDate = value
            End Set
        End Property


        <ColumnInfo("ProspectDate_YYYYMMDD", "'{0}'")> _
        Public Property ProspectDate_YYYYMMDD As String
            Get
                Return _prospectDate_YYYYMMDD
            End Get
            Set(ByVal value As String)
                _prospectDate_YYYYMMDD = value
            End Set
        End Property


        <ColumnInfo("VechileTypeCode", "'{0}'")> _
        Public Property VechileTypeCode As String
            Get
                Return _vechileTypeCode
            End Get
            Set(ByVal value As String)
                _vechileTypeCode = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("CurrVehicleType", "'{0}'")> _
        Public Property CurrVehicleType As String
            Get
                Return _currVehicleType
            End Get
            Set(ByVal value As String)
                _currVehicleType = value
            End Set
        End Property


        <ColumnInfo("CurrVehicleBrand", "'{0}'")> _
        Public Property CurrVehicleBrand As String
            Get
                Return _currVehicleBrand
            End Get
            Set(ByVal value As String)
                _currVehicleBrand = value
            End Set
        End Property


        <ColumnInfo("Note", "'{0}'")> _
        Public Property Note As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _note = value
            End Set
        End Property


        <ColumnInfo("Keterangan", "'{0}'")> _
        Public Property Keterangan As String
            Get
                Return _keterangan
            End Get
            Set(ByVal value As String)
                _keterangan = value
            End Set
        End Property


        <ColumnInfo("StatusResponseID", "'{0}'")> _
        Public Property StatusResponseID As String
            Get
                Return _statusResponseID
            End Get
            Set(ByVal value As String)
                _statusResponseID = value
            End Set
        End Property


        <ColumnInfo("StatusResponse", "'{0}'")> _
        Public Property StatusResponse As String
            Get
                Return _statusResponse
            End Get
            Set(ByVal value As String)
                _statusResponse = value
            End Set
        End Property


        <ColumnInfo("WebID", "'{0}'")> _
        Public Property WebID As String
            Get
                Return _webID
            End Get
            Set(ByVal value As String)
                _webID = value
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

