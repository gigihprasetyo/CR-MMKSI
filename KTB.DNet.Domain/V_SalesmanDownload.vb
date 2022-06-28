
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SalesmanDownload Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 1/20/2009 - 4:01:17 PM
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
    <Serializable(), TableInfo("V_SalesmanDownload")> _
    Public Class V_SalesmanDownload
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
        Private _dealerId As Integer
        Private _dealerBranchName As String = String.Empty
        Private _term1 As String = String.Empty
        Private _salesmanCode As String = String.Empty
        Private _name As String = String.Empty
        'Private _image As Byte()
        Private _placeOfBirth As String = String.Empty
        Private _dateOfBirth As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _gender As Byte
        Private _address As String = String.Empty
        Private _city As String = String.Empty
        'Private _shopSiteNumber As Integer
        Private _hireDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        'Private _salesmanAreaId As Integer
        Private _jobPositionId_Main As Integer
        Private _salesmanLevelID As Integer
        'Private _jobPositionId_Second As Integer
        'Private _jobPositionId_Third As Integer
        'Private _leaderId As Integer
        'Private _jobPositionId_Leader As Integer
        'Private _registerStatus As String = String.Empty
        Private _marriedStatus As String = String.Empty
        Private _resignDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        'Private _resignReason As String = String.Empty
        Private _salesIndicator As Byte
        'Private _salesUnitIndicator As Byte
        'Private _mechanicIndicator As Byte
        'Private _sparePartIndicator As Byte
        'Private _sPAdminIndicator As Byte
        'Private _sPWareHouseIndicator As Byte
        'Private _sPCounterIndicator As Byte
        'Private _sPSalesIndicator As Byte
        'Private _isRequestID As Byte
        Private _status As String = String.Empty
        Private _rowStatus As Short
        'Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        'Private _lastUpdateBy As String = String.Empty
        'Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerCode As String = String.Empty
        Private _provinceName As String = String.Empty
        Private _jobDescription As String = String.Empty
        Private _levelDescription As String = String.Empty
        Private _leaderCode As String = String.Empty
        Private _LeaderName As String = String.Empty
        Private _areaDesc As String = String.Empty
        Private _pENDIDIKAN As String = String.Empty
        Private _eMAIL As String = String.Empty
        Private _nO_HP As String = String.Empty
        Private _nOKTP As String = String.Empty
        Private _kATEGORI_TIM As String = String.Empty

        Private _dealerName As String = String.Empty
        Private _dealerAddress As String = String.Empty
        Private _cityName As String = String.Empty
        Private _groupName As String = String.Empty
        Private _area1Description As String = String.Empty
        Private _genderDescription As String = String.Empty
        Private _marriedStatusDesc As String = String.Empty

        Private _YearLastScore As String = String.Empty
        Private _YearLastGrade As String = String.Empty
        Private _LastScore As String = String.Empty
        Private _LastGrade As String = String.Empty

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


        <ColumnInfo("DealerId", "{0}")> _
        Public Property DealerId() As Integer
            Get
                Return _dealerId
            End Get
            Set(ByVal value As Integer)
                _dealerId = value
            End Set
        End Property

        <ColumnInfo("DealerBranchName", "'{0}'")> _
        Public Property DealerBranchName() As String
            Get
                Return _dealerBranchName
            End Get
            Set(ByVal value As String)
                _dealerBranchName = value
            End Set
        End Property

        <ColumnInfo("Term1", "'{0}'")> _
        Public Property Term1() As String
            Get
                Return _term1
            End Get
            Set(ByVal value As String)
                _term1 = value
            End Set
        End Property

        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode() As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        '<ColumnInfo("Image", "{0}")> _
        'Public Property Image() As Byte()
        '    Get
        '        Return _image
        '    End Get
        '    Set(ByVal value As Byte())
        '        _image = value
        '    End Set
        'End Property


        <ColumnInfo("PlaceOfBirth", "'{0}'")> _
        Public Property PlaceOfBirth() As String
            Get
                Return _placeOfBirth
            End Get
            Set(ByVal value As String)
                _placeOfBirth = value
            End Set
        End Property


        <ColumnInfo("DateOfBirth", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateOfBirth() As DateTime
            Get
                Return _dateOfBirth
            End Get
            Set(ByVal value As DateTime)
                _dateOfBirth = value
            End Set
        End Property


        <ColumnInfo("Gender", "{0}")> _
        Public Property Gender() As Byte
            Get
                Return _gender
            End Get
            Set(ByVal value As Byte)
                _gender = value
            End Set
        End Property


        <ColumnInfo("Address", "'{0}'")> _
        Public Property Address() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
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


        '<ColumnInfo("ShopSiteNumber", "{0}")> _
        'Public Property ShopSiteNumber() As Integer
        '    Get
        '        Return _shopSiteNumber
        '    End Get
        '    Set(ByVal value As Integer)
        '        _shopSiteNumber = value
        '    End Set
        'End Property


        <ColumnInfo("HireDate", "'{0:yyyy/MM/dd}'")> _
        Public Property HireDate() As DateTime
            Get
                Return _hireDate
            End Get
            Set(ByVal value As DateTime)
                _hireDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        '<ColumnInfo("SalesmanAreaId", "{0}")> _
        'Public Property SalesmanAreaId() As Integer
        '    Get
        '        Return _salesmanAreaId
        '    End Get
        '    Set(ByVal value As Integer)
        '        _salesmanAreaId = value
        '    End Set
        'End Property


        <ColumnInfo("JobPositionId_Main", "{0}")> _
        Public Property JobPositionId_Main() As Integer
            Get
                Return _jobPositionId_Main
            End Get
            Set(ByVal value As Integer)
                _jobPositionId_Main = value
            End Set
        End Property


        <ColumnInfo("SalesmanLevelID", "{0}")> _
        Public Property SalesmanLevelID() As Integer
            Get
                Return _salesmanLevelID
            End Get
            Set(ByVal value As Integer)
                _salesmanLevelID = value
            End Set
        End Property


        '<ColumnInfo("JobPositionId_Second", "{0}")> _
        'Public Property JobPositionId_Second() As Integer
        '    Get
        '        Return _jobPositionId_Second
        '    End Get
        '    Set(ByVal value As Integer)
        '        _jobPositionId_Second = value
        '    End Set
        'End Property


        '<ColumnInfo("JobPositionId_Third", "{0}")> _
        'Public Property JobPositionId_Third() As Integer
        '    Get
        '        Return _jobPositionId_Third
        '    End Get
        '    Set(ByVal value As Integer)
        '        _jobPositionId_Third = value
        '    End Set
        'End Property


        '<ColumnInfo("LeaderId", "{0}")> _
        'Public Property LeaderId() As Integer
        '    Get
        '        Return _leaderId
        '    End Get
        '    Set(ByVal value As Integer)
        '        _leaderId = value
        '    End Set
        'End Property


        '<ColumnInfo("JobPositionId_Leader", "{0}")> _
        'Public Property JobPositionId_Leader() As Integer
        '    Get
        '        Return _jobPositionId_Leader
        '    End Get
        '    Set(ByVal value As Integer)
        '        _jobPositionId_Leader = value
        '    End Set
        'End Property


        '<ColumnInfo("RegisterStatus", "'{0}'")> _
        'Public Property RegisterStatus() As String
        '    Get
        '        Return _registerStatus
        '    End Get
        '    Set(ByVal value As String)
        '        _registerStatus = value
        '    End Set
        'End Property


        <ColumnInfo("MarriedStatus", "'{0}'")> _
        Public Property MarriedStatus() As String
            Get
                Return _marriedStatus
            End Get
            Set(ByVal value As String)
                _marriedStatus = value
            End Set
        End Property


        <ColumnInfo("ResignDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ResignDate() As DateTime
            Get
                Return _resignDate
            End Get
            Set(ByVal value As DateTime)
                _resignDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        '<ColumnInfo("ResignReason", "'{0}'")> _
        'Public Property ResignReason() As String
        '    Get
        '        Return _resignReason
        '    End Get
        '    Set(ByVal value As String)
        '        _resignReason = value
        '    End Set
        'End Property


        <ColumnInfo("SalesIndicator", "{0}")> _
        Public Property SalesIndicator() As Byte
            Get
                Return _salesIndicator
            End Get
            Set(ByVal value As Byte)
                _salesIndicator = value
            End Set
        End Property


        '<ColumnInfo("SalesUnitIndicator", "{0}")> _
        'Public Property SalesUnitIndicator() As Byte
        '    Get
        '        Return _salesUnitIndicator
        '    End Get
        '    Set(ByVal value As Byte)
        '        _salesUnitIndicator = value
        '    End Set
        'End Property


        '<ColumnInfo("MechanicIndicator", "{0}")> _
        'Public Property MechanicIndicator() As Byte
        '    Get
        '        Return _mechanicIndicator
        '    End Get
        '    Set(ByVal value As Byte)
        '        _mechanicIndicator = value
        '    End Set
        'End Property


        '<ColumnInfo("SparePartIndicator", "{0}")> _
        'Public Property SparePartIndicator() As Byte
        '    Get
        '        Return _sparePartIndicator
        '    End Get
        '    Set(ByVal value As Byte)
        '        _sparePartIndicator = value
        '    End Set
        'End Property


        '<ColumnInfo("SPAdminIndicator", "{0}")> _
        'Public Property SPAdminIndicator() As Byte
        '    Get
        '        Return _sPAdminIndicator
        '    End Get
        '    Set(ByVal value As Byte)
        '        _sPAdminIndicator = value
        '    End Set
        'End Property


        '<ColumnInfo("SPWareHouseIndicator", "{0}")> _
        'Public Property SPWareHouseIndicator() As Byte
        '    Get
        '        Return _sPWareHouseIndicator
        '    End Get
        '    Set(ByVal value As Byte)
        '        _sPWareHouseIndicator = value
        '    End Set
        'End Property


        '<ColumnInfo("SPCounterIndicator", "{0}")> _
        'Public Property SPCounterIndicator() As Byte
        '    Get
        '        Return _sPCounterIndicator
        '    End Get
        '    Set(ByVal value As Byte)
        '        _sPCounterIndicator = value
        '    End Set
        'End Property


        '<ColumnInfo("SPSalesIndicator", "{0}")> _
        'Public Property SPSalesIndicator() As Byte
        '    Get
        '        Return _sPSalesIndicator
        '    End Get
        '    Set(ByVal value As Byte)
        '        _sPSalesIndicator = value
        '    End Set
        'End Property


        '<ColumnInfo("IsRequestID", "{0}")> _
        'Public Property IsRequestID() As Byte
        '    Get
        '        Return _isRequestID
        '    End Get
        '    Set(ByVal value As Byte)
        '        _isRequestID = value
        '    End Set
        'End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
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


        '<ColumnInfo("CreatedBy", "'{0}'")> _
        'Public Property CreatedBy() As String
        '    Get
        '        Return _createdBy
        '    End Get
        '    Set(ByVal value As String)
        '        _createdBy = value
        '    End Set
        'End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        '<ColumnInfo("LastUpdateBy", "'{0}'")> _
        'Public Property LastUpdateBy() As String
        '    Get
        '        Return _lastUpdateBy
        '    End Get
        '    Set(ByVal value As String)
        '        _lastUpdateBy = value
        '    End Set
        'End Property


        '<ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        'Public Property LastUpdateTime() As DateTime
        '    Get
        '        Return _lastUpdateTime
        '    End Get
        '    Set(ByVal value As DateTime)
        '        _lastUpdateTime = value
        '    End Set
        'End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
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


        <ColumnInfo("JobDescription", "'{0}'")> _
        Public Property JobDescription() As String
            Get
                Return _jobDescription
            End Get
            Set(ByVal value As String)
                _jobDescription = value
            End Set
        End Property


        <ColumnInfo("LevelDescription", "'{0}'")> _
        Public Property LevelDescription() As String
            Get
                Return _levelDescription
            End Get
            Set(ByVal value As String)
                _levelDescription = value
            End Set
        End Property


        <ColumnInfo("LeaderCode", "'{0}'")> _
        Public Property LeaderCode() As String
            Get
                Return _leaderCode
            End Get
            Set(ByVal value As String)
                _leaderCode = value
            End Set
        End Property


        <ColumnInfo("LeaderName", "'{0}'")> _
        Public Property LeaderName() As String
            Get
                Return _LeaderName
            End Get
            Set(ByVal value As String)
                _LeaderName = value
            End Set
        End Property


        <ColumnInfo("AreaDesc", "'{0}'")> _
        Public Property AreaDesc() As String
            Get
                Return _areaDesc
            End Get
            Set(ByVal value As String)
                _areaDesc = value
            End Set
        End Property


        <ColumnInfo("PENDIDIKAN", "'{0}'")> _
        Public Property PENDIDIKAN() As String
            Get
                Return _pENDIDIKAN
            End Get
            Set(ByVal value As String)
                _pENDIDIKAN = value
            End Set
        End Property


        <ColumnInfo("EMAIL", "'{0}'")> _
        Public Property EMAIL() As String
            Get
                Return _eMAIL
            End Get
            Set(ByVal value As String)
                _eMAIL = value
            End Set
        End Property


        <ColumnInfo("NO_HP", "'{0}'")> _
        Public Property NO_HP() As String
            Get
                Return _nO_HP
            End Get
            Set(ByVal value As String)
                _nO_HP = value
            End Set
        End Property


        <ColumnInfo("NOKTP", "'{0}'")> _
        Public Property NOKTP() As String
            Get
                Return _nOKTP
            End Get
            Set(ByVal value As String)
                _nOKTP = value
            End Set
        End Property

        <ColumnInfo("KATEGORI_TIM", "'{0}'")> _
        Public Property KATEGORI_TIM() As String
            Get
                Return _kATEGORI_TIM
            End Get
            Set(ByVal value As String)
                _kATEGORI_TIM = value
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

        <ColumnInfo("DealerAddress", "'{0}'")> _
        Public Property DealerAddress() As String
            Get
                Return _dealerAddress
            End Get
            Set(ByVal value As String)
                _dealerAddress = value
            End Set
        End Property

        <ColumnInfo("CityName", "'{0}'")> _
        Public Property CityName() As String
            Get
                Return _cityName
            End Get
            Set(ByVal value As String)
                _cityName = value
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

        <ColumnInfo("Area1Description", "'{0}'")> _
        Public Property Area1Description() As String
            Get
                Return _area1Description
            End Get
            Set(ByVal value As String)
                _area1Description = value
            End Set
        End Property

        <ColumnInfo("GenderDescription", "'{0}'")> _
        Public Property GenderDescription() As String
            Get
                Return _genderDescription
            End Get
            Set(ByVal value As String)
                _genderDescription = value
            End Set
        End Property

        <ColumnInfo("MarriedStatusDesc", "'{0}'")> _
        Public Property MarriedStatusDesc() As String
            Get
                Return _marriedStatusDesc
            End Get
            Set(ByVal value As String)
                _marriedStatusDesc = value
            End Set
        End Property

        <ColumnInfo("LastGrade", "'{0}'")> _
        Public Property LastGrade() As String
            Get
                Return _LastGrade
            End Get
            Set(ByVal value As String)
                _LastGrade = value
            End Set
        End Property

        <ColumnInfo("LastScore", "'{0}'")> _
        Public Property LastScore() As String
            Get
                Return _LastScore
            End Get
            Set(ByVal value As String)
                _LastScore = value
            End Set
        End Property

        <ColumnInfo("YearLastGrade", "'{0}'")> _
        Public Property YearLastGrade() As String
            Get
                Return _YearLastGrade
            End Get
            Set(ByVal value As String)
                _YearLastGrade = value
            End Set
        End Property

        <ColumnInfo("YearLastScore", "'{0}'")> _
        Public Property YearLastScore() As String
            Get
                Return _YearLastScore
            End Get
            Set(ByVal value As String)
                _YearLastScore = value
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
        Private _dealer As dealer

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "POHeader", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property
#End Region

    End Class
End Namespace

