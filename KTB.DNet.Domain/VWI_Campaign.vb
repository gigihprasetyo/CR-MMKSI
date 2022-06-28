#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_CampaignReport Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 07/03/2018 - 13:17:15
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
    <Serializable(), TableInfo("VWI_Campaign")>
    Public Class VWI_Campaign
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
        Private _dealerBranchCode As String = String.Empty
        Private _campaignCode As String = String.Empty
        Private _campaignType As Integer = 0
        Private _campaignTypeCode As String = String.Empty
        Private _campaignTypeDesc As String = String.Empty
        Private _campaignName As String = String.Empty
        Private _dealerCampaignName As String = String.Empty
        Private _babitDealerNumber As String = String.Empty
        Private _periodStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _location As String = String.Empty
        Private _campaignDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _locationName As String = String.Empty
        Private _luasArea As Integer = 0
        Private _prospectTarget As Integer = 0
        Private _sPKTarget As Integer = 0
        Private _invitationQty As Integer = 0
        Private _babitCategory As String = String.Empty
        Private _cityCode As String = String.Empty
        Private _cityName As String = String.Empty
        Private _proviceCode As String = String.Empty
        Private _provinceName As String = String.Empty
        Private _status As Short
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _eventType As Integer

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


        <ColumnInfo("DealerCode", "'{0}'")>
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
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


        <ColumnInfo("CampaignCode", "'{0}'")>
        Public Property CampaignCode As String
            Get
                Return _campaignCode
            End Get
            Set(ByVal value As String)
                _campaignCode = value
            End Set
        End Property


        <ColumnInfo("CampaignType", "{0}")>
        Public Property CampaignType As Integer
            Get
                Return _campaignType
            End Get
            Set(ByVal value As Integer)
                _campaignType = value
            End Set
        End Property


        <ColumnInfo("CampaignTypeCode", "'{0}'")>
        Public Property CampaignTypeCode As String
            Get
                Return _campaignTypeCode
            End Get
            Set(ByVal value As String)
                _campaignTypeCode = value
            End Set
        End Property


        <ColumnInfo("CampaignTypeDesc", "'{0}'")>
        Public Property CampaignTypeDesc As String
            Get
                Return _campaignTypeDesc
            End Get
            Set(ByVal value As String)
                _campaignTypeDesc = value
            End Set
        End Property


        <ColumnInfo("CampaignName", "'{0}'")>
        Public Property CampaignName As String
            Get
                Return _campaignName
            End Get
            Set(ByVal value As String)
                _campaignName = value
            End Set
        End Property


        <ColumnInfo("DealerCampaignName", "'{0}'")>
        Public Property DealerCampaignName As String
            Get
                Return _dealerCampaignName
            End Get
            Set(ByVal value As String)
                _dealerCampaignName = value
            End Set
        End Property


        <ColumnInfo("BabitDealerNumber", "'{0}'")>
        Public Property BabitDealerNumber As String
            Get
                Return _babitDealerNumber
            End Get
            Set(ByVal value As String)
                _babitDealerNumber = value
            End Set
        End Property


        <ColumnInfo("PeriodStart", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property PeriodStart As DateTime
            Get
                Return _periodStart
            End Get
            Set(ByVal value As DateTime)
                _periodStart = value
            End Set
        End Property


        <ColumnInfo("PeriodEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property PeriodEnd As DateTime
            Get
                Return _periodEnd
            End Get
            Set(ByVal value As DateTime)
                _periodEnd = value
            End Set
        End Property


        <ColumnInfo("Location", "'{0}'")>
        Public Property Location As String
            Get
                Return _location
            End Get
            Set(ByVal value As String)
                _location = value
            End Set
        End Property


        <ColumnInfo("CampaignDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property CampaignDate As DateTime
            Get
                Return _campaignDate
            End Get
            Set(ByVal value As DateTime)
                _campaignDate = value
            End Set
        End Property


        <ColumnInfo("LocationName", "'{0}'")>
        Public Property LocationName As String
            Get
                Return _locationName
            End Get
            Set(ByVal value As String)
                _locationName = value
            End Set
        End Property


        <ColumnInfo("LuasArea", "{0}")>
        Public Property LuasArea As Integer
            Get
                Return _luasArea
            End Get
            Set(ByVal value As Integer)
                _luasArea = value
            End Set
        End Property


        <ColumnInfo("ProspectTarget", "{0}")>
        Public Property ProspectTarget As Integer
            Get
                Return _prospectTarget
            End Get
            Set(ByVal value As Integer)
                _prospectTarget = value
            End Set
        End Property


        <ColumnInfo("SPKTarget", "{0}")>
        Public Property SPKTarget As Integer
            Get
                Return _sPKTarget
            End Get
            Set(ByVal value As Integer)
                _sPKTarget = value
            End Set
        End Property


        <ColumnInfo("InvitationQty", "{0}")>
        Public Property InvitationQty As Integer
            Get
                Return _invitationQty
            End Get
            Set(ByVal value As Integer)
                _invitationQty = value
            End Set
        End Property


        <ColumnInfo("BabitCategory", "'{0}'")>
        Public Property BabitCategory As String
            Get
                Return _babitCategory
            End Get
            Set(ByVal value As String)
                _babitCategory = value
            End Set
        End Property


        <ColumnInfo("CityCode", "'{0}'")>
        Public Property CityCode As String
            Get
                Return _cityCode
            End Get
            Set(ByVal value As String)
                _cityCode = value
            End Set
        End Property


        <ColumnInfo("CityName", "'{0}'")>
        Public Property CityName As String
            Get
                Return _cityName
            End Get
            Set(ByVal value As String)
                _cityName = value
            End Set
        End Property


        <ColumnInfo("ProvinceCode", "'{0}'")>
        Public Property ProvinceCode As String
            Get
                Return _proviceCode
            End Get
            Set(ByVal value As String)
                _proviceCode = value
            End Set
        End Property


        <ColumnInfo("ProvinceName", "'{0}'")>
        Public Property ProvinceName As String
            Get
                Return _provinceName
            End Get
            Set(ByVal value As String)
                _provinceName = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")>
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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

        <ColumnInfo("EventType", "{0}")>
        Public Property EventType As Integer
            Get
                Return _eventType
            End Get
            Set(ByVal value As Integer)
                _eventType = value
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