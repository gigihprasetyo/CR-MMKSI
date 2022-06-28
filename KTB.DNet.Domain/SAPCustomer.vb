
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SAPCustomer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 7/13/2017 - 9:09:09 AM
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
    <Serializable(), TableInfo("SAPCustomer")> _
    Public Class SAPCustomer
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
        Private _salesforceID As String = String.Empty
        Private _customerCode As String = String.Empty
        Private _customerName As String = String.Empty
        Private _customerType As Short
        Private _customerAddress As String = String.Empty
        Private _phone As String = String.Empty
        Private _email As String = String.Empty
        Private _sex As Byte
        Private _ageSegment As Byte
        Private _customerPurpose As Short
        Private _informationType As Short
        Private _informationSource As Short
        Private _status As Byte
        Private _qty As Integer
        Private _prospectDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isSPK As Boolean
        Private _currVehicleBrand As String = String.Empty
        Private _currVehicleType As String = String.Empty
        Private _vehicleModel As String = String.Empty
        Private _variant As String = String.Empty
        Private _sequence As Integer
        Private _note As String = String.Empty
        Private _webID As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _birthDate As Date = New Date(1900, 1, 1)
        Private _preferedVehicleModel As String = String.Empty
        Private _description As String = String.Empty
        Private _estimatedCloseDate As Date = New Date(1900, 1, 1)
        Private _originatingLeadId As Guid = Guid.Empty
        Private _statusCode As Short
        Private _campaignName As String
        Private _leadStatus As Byte
        Private _stateCode As Byte
        Private _name2 As String = String.Empty
        Private _phoneType As Integer
        Private _telp As String = String.Empty
        Private _identityType As Integer
        Private _identityNumber As String = String.Empty
        Private _jobKind As Integer
        Private _cusReqPrice As Decimal
        Private _cusReqDiscount As Decimal
        Private _bookingFee As Decimal
        Private _bBNType As Integer
        Private _blankoSPKNo As String = String.Empty
        Private _blankoSPKDoc As String = String.Empty
        Private _interfaceStatus As Short
        Private _interfaceMessage As String = String.Empty
        Private _gUIDUpdate As String = String.Empty
        Private _topic As String = String.Empty
        Private _currVehicleBrandDesc As String = String.Empty
        Private _vehicleComparison As String = String.Empty
        Private _gUID As String = String.Empty
        Private _rating As Integer

        Private _vechileModel As VechileModel
        Private _vechileColor As VechileColor
        Private _dealerVehiclePriceDetail As DealerVehiclePriceDetail
        Private _vechileType As VechileType
        Private _businessSectorDetail As BusinessSectorDetail
        Private _salesmanHeader As SalesmanHeader
        Private _dealer As Dealer
        Private _sAPCustomerResponses As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _sPKCustomer As SPKCustomer = New SPKCustomer(0)
        Private _sapCustomerMapping As SAPCustomerMapping = New SAPCustomerMapping(0)
        'CR SPK
        Private _countryCode As String = String.Empty
        '

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


        <ColumnInfo("SalesforceID", "'{0}'")> _
        Public Property SalesforceID As String
            Get
                Return _salesforceID
            End Get
            Set(ByVal value As String)
                _salesforceID = value
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


        <ColumnInfo("CustomerType", "{0}")> _
        Public Property CustomerType As Short
            Get
                Return _customerType
            End Get
            Set(ByVal value As Short)
                _customerType = value
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


        <ColumnInfo("Sex", "{0}")> _
        Public Property Sex As Byte
            Get
                Return _sex
            End Get
            Set(ByVal value As Byte)
                _sex = value
            End Set
        End Property


        <ColumnInfo("AgeSegment", "{0}")> _
        Public Property AgeSegment As Byte
            Get
                Return _ageSegment
            End Get
            Set(ByVal value As Byte)
                _ageSegment = value
            End Set
        End Property


        <ColumnInfo("CustomerPurpose", "{0}")> _
        Public Property CustomerPurpose As Short
            Get
                Return _customerPurpose
            End Get
            Set(ByVal value As Short)
                _customerPurpose = value
            End Set
        End Property


        <ColumnInfo("InformationType", "{0}")> _
        Public Property InformationType As Short
            Get
                Return _informationType
            End Get
            Set(ByVal value As Short)
                _informationType = value
            End Set
        End Property


        <ColumnInfo("InformationSource", "{0}")> _
        Public Property InformationSource As Short
            Get
                Return _informationSource
            End Get
            Set(ByVal value As Short)
                _informationSource = value
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


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
            End Set
        End Property


        <ColumnInfo("ProspectDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ProspectDate As DateTime
            Get
                Return _prospectDate
            End Get
            Set(ByVal value As DateTime)
                _prospectDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("isSPK", "{0}")> _
        Public Property isSPK As Boolean
            Get
                Return _isSPK
            End Get
            Set(ByVal value As Boolean)
                _isSPK = value
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


        <ColumnInfo("CurrVehicleType", "'{0}'")> _
        Public Property CurrVehicleType As String
            Get
                Return _currVehicleType
            End Get
            Set(ByVal value As String)
                _currVehicleType = value
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

        <ColumnInfo("WebID", "'{0}'")> _
        Public Property WebID As String
            Get
                Return _webID
            End Get
            Set(ByVal value As String)
                _webID = value
            End Set
        End Property


        <ColumnInfo("Variant", "'{0}'")> _
        Public Property Variants As String
            Get
                Return _variant
            End Get
            Set(ByVal value As String)
                _variant = value

            End Set
        End Property



        <ColumnInfo("VehicleModel", "'{0}'")> _
        Public Property VehicleModel As String
            Get
                Return _vehicleModel
            End Get
            Set(ByVal value As String)
                _vehicleModel = value
            End Set
        End Property




        <ColumnInfo("Sequence", "{0}")> _
        Public Property Sequence As Integer
            Get
                Return _sequence
            End Get
            Set(ByVal value As Integer)
                _sequence = value
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


        <ColumnInfo("BirthDate", "{0}")> _
        Public Property BirthDate As Date
            Get
                Return _birthDate
            End Get
            Set(ByVal value As Date)
                _birthDate = value
            End Set
        End Property


        <ColumnInfo("PreferedVehicleModel", "'{0}'")> _
        Public Property PreferedVehicleModel As String
            Get
                Return _preferedVehicleModel
            End Get
            Set(ByVal value As String)
                _preferedVehicleModel = value
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


        <ColumnInfo("EstimatedCloseDate", "{0}")> _
        Public Property EstimatedCloseDate As Date
            Get
                Return _estimatedCloseDate
            End Get
            Set(ByVal value As Date)
                _estimatedCloseDate = value
            End Set
        End Property


        <ColumnInfo("OriginatingLeadId", "{0}")> _
        Public Property OriginatingLeadId As Guid
            Get
                Return _originatingLeadId
            End Get
            Set(ByVal value As Guid)
                _originatingLeadId = value
            End Set
        End Property


        <ColumnInfo("StatusCode", "{0}")> _
        Public Property StatusCode As Short
            Get
                Return _statusCode
            End Get
            Set(ByVal value As Short)
                _statusCode = value
            End Set
        End Property


        <ColumnInfo("LeadStatus", "{0}")> _
        Public Property LeadStatus As Byte
            Get
                Return _leadStatus
            End Get
            Set(ByVal value As Byte)
                _leadStatus = value
            End Set
        End Property


        <ColumnInfo("StateCode", "{0}")> _
        Public Property StateCode As Byte
            Get
                Return _stateCode
            End Get
            Set(ByVal value As Byte)
                _stateCode = value
            End Set
        End Property


        <ColumnInfo("CampaignName", "'{0}'")> _
        Public Property CampaignName As String
            Get
                Return _campaignName
            End Get
            Set(ByVal value As String)
                _campaignName = value
            End Set
        End Property

        <ColumnInfo("Name2", "'{0}'")> _
        Public Property Name2 As String
            Get
                Return _name2
            End Get
            Set(ByVal value As String)
                _name2 = value
            End Set
        End Property


        <ColumnInfo("PhoneType", "{0}")> _
        Public Property PhoneType As Integer
            Get
                Return _phoneType
            End Get
            Set(ByVal value As Integer)
                _phoneType = value
            End Set
        End Property


        <ColumnInfo("Telp", "'{0}'")> _
        Public Property Telp As String
            Get
                Return _telp
            End Get
            Set(ByVal value As String)
                _telp = value
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


        <ColumnInfo("JobKind", "{0}")> _
        Public Property JobKind As Integer
            Get
                Return _jobKind
            End Get
            Set(ByVal value As Integer)
                _jobKind = value
            End Set
        End Property


        <ColumnInfo("CusReqPrice", "{0}")> _
        Public Property CusReqPrice As Decimal
            Get
                Return _cusReqPrice
            End Get
            Set(ByVal value As Decimal)
                _cusReqPrice = value
            End Set
        End Property


        <ColumnInfo("CusReqDiscount", "{0}")> _
        Public Property CusReqDiscount As Decimal
            Get
                Return _cusReqDiscount
            End Get
            Set(ByVal value As Decimal)
                _cusReqDiscount = value
            End Set
        End Property


        <ColumnInfo("BookingFee", "{0}")> _
        Public Property BookingFee As Decimal
            Get
                Return _bookingFee
            End Get
            Set(ByVal value As Decimal)
                _bookingFee = value
            End Set
        End Property


        <ColumnInfo("BBNType", "{0}")> _
        Public Property BBNType As Integer
            Get
                Return _bBNType
            End Get
            Set(ByVal value As Integer)
                _bBNType = value
            End Set
        End Property


        <ColumnInfo("BlankoSPKNo", "'{0}'")> _
        Public Property BlankoSPKNo As String
            Get
                Return _blankoSPKNo
            End Get
            Set(ByVal value As String)
                _blankoSPKNo = value
            End Set
        End Property


        <ColumnInfo("BlankoSPKDoc", "'{0}'")> _
        Public Property BlankoSPKDoc As String
            Get
                Return _blankoSPKDoc
            End Get
            Set(ByVal value As String)
                _blankoSPKDoc = value
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


        <ColumnInfo("GUIDUpdate", "'{0}'")> _
        Public Property GUIDUpdate As String
            Get
                Return _gUIDUpdate
            End Get
            Set(ByVal value As String)
                _gUIDUpdate = value
            End Set
        End Property


        <ColumnInfo("Topic", "'{0}'")> _
        Public Property Topic As String
            Get
                Return _topic
            End Get
            Set(ByVal value As String)
                _topic = value
            End Set
        End Property


        <ColumnInfo("CurrVehicleBrandDesc", "'{0}'")> _
        Public Property CurrVehicleBrandDesc As String
            Get
                Return _currVehicleBrandDesc
            End Get
            Set(ByVal value As String)
                _currVehicleBrandDesc = value
            End Set
        End Property


        <ColumnInfo("VehicleComparison", "'{0}'")> _
        Public Property VehicleComparison As String
            Get
                Return _vehicleComparison
            End Get
            Set(ByVal value As String)
                _vehicleComparison = value
            End Set
        End Property


        <ColumnInfo("GUID", "'{0}'")> _
        Public Property SAPCustomerGUID As String
            Get
                Return _gUID
            End Get
            Set(ByVal value As String)
                _gUID = value
            End Set
        End Property


        <ColumnInfo("Rating", "{0}")> _
        Public Property Rating As Integer
            Get
                Return _rating
            End Get
            Set(ByVal value As Integer)
                _rating = value
            End Set
        End Property


        <ColumnInfo("VechileModelID", "{0}"),
        RelationInfo("VechileModel", "ID", "SAPCustomer", "VechileModelID")>
        Public Property VechileModel As VechileModel
            Get
                Try
                    If Not IsNothing(Me._vechileModel) AndAlso (Not Me._vechileModel.IsLoaded) Then

                        Me._vechileModel = CType(DoLoad(GetType(VechileModel).ToString(), _vechileModel.ID), VechileModel)
                        Me._vechileModel.MarkLoaded()

                    End If

                    Return Me._vechileModel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileModel)

                Me._vechileModel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileModel.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("VechileColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "SAPCustomer", "VechileColorID")> _
        Public Property VechileColor As VechileColor
            Get
                Try
                    If Not IsNothing(Me._vechileColor) AndAlso (Not Me._vechileColor.IsLoaded) Then
                        If _vechileColor.ID > 0 Then
                            Me._vechileColor = CType(DoLoad(GetType(VechileColor).ToString(), _vechileColor.ID), VechileColor)
                            Me._vechileColor.MarkLoaded()
                        End If
                    End If

                    Return Me._vechileColor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileColor)

                Me._vechileColor = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColor.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("DealerVehiclePriceDetailID", "{0}"), _
        RelationInfo("DealerVehiclePriceDetail", "ID", "SAPCustomer", "DealerVehiclePriceDetailID")> _
        Public Property DealerVehiclePriceDetail As DealerVehiclePriceDetail
            Get
                Try
                    If Not IsNothing(Me._dealerVehiclePriceDetail) AndAlso (Not Me._dealerVehiclePriceDetail.IsLoaded) Then
                        If _dealerVehiclePriceDetail.ID > 0 Then
                            Me._dealerVehiclePriceDetail = CType(DoLoad(GetType(DealerVehiclePriceDetail).ToString(), _dealerVehiclePriceDetail.ID), DealerVehiclePriceDetail)
                            Me._dealerVehiclePriceDetail.MarkLoaded()
                        End If

                    End If

                    Return Me._dealerVehiclePriceDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerVehiclePriceDetail)

                Me._dealerVehiclePriceDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerVehiclePriceDetail.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("VechileTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "SAPCustomer", "VechileTypeID")> _
        Public Property VechileType As VechileType
            Get
                Try
                    If Not IsNothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then
                        If _vechileType.ID > 0 Then
                            Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), _vechileType.ID), VechileType)
                            Me._vechileType.MarkLoaded()
                        End If
                    End If

                    Return Me._vechileType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileType)

                Me._vechileType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanHeaderID", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "SAPCustomer", "SalesmanHeaderID")> _
        Public Property SalesmanHeader As SalesmanHeader
            Get
                Try
                    If Not IsNothing(Me._salesmanHeader) AndAlso (Not Me._salesmanHeader.IsLoaded) Then

                        Me._salesmanHeader = CType(DoLoad(GetType(SalesmanHeader).ToString(), _salesmanHeader.ID), SalesmanHeader)
                        Me._salesmanHeader.MarkLoaded()

                    End If

                    Return Me._salesmanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeader)

                Me._salesmanHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "SAPCustomer", "DealerID")> _
        Public Property Dealer As Dealer
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

        <ColumnInfo("BusinessSectorDetailID", "{0}"), _
        RelationInfo("BusinessSectorDetail", "ID", "SAPCustomer", "BusinessSectorDetailID")> _
        Public Property BusinessSectorDetail As BusinessSectorDetail
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

        <RelationInfo("SAPCustomer", "ID", "SAPCustomerResponse", "SAPCustomerID")> _
        Public ReadOnly Property SAPCustomerResponses As System.Collections.ArrayList
            Get
                Try
                    If (Me._sAPCustomerResponses.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SAPCustomerResponse), "SAPCustomer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SAPCustomerResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sAPCustomerResponses = DoLoadArray(GetType(SAPCustomerResponse).ToString, criterias)
                    End If

                    Return Me._sAPCustomerResponses

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
        RelationInfo("SAPCustomer", "ID", "SPKCustomer", "SAPCustomerID")> _
        Public ReadOnly Property SPKCustomer As SPKCustomer
            Get
                Try
                    If Not IsNothing(Me._sPKCustomer) AndAlso (Not Me._sPKCustomer.IsLoaded) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPKCustomer), "SAPCustomer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(SPKCustomer).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._sPKCustomer = CType(tempColl(0), SPKCustomer)
                        Else
                            Me._sPKCustomer = Nothing
                        End If
                    End If

                    Return Me._sPKCustomer

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
        RelationInfo("SAPCustomer", "ID", "SAPCustomerMapping", "SAPCustomerID")> _
        Public ReadOnly Property SAPCustomerMapping As SAPCustomerMapping
            Get
                Try
                    If Not IsNothing(Me._sapCustomerMapping) AndAlso (Not Me._sapCustomerMapping.IsLoaded) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SAPCustomerMapping), "SAPCustomer.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SAPCustomerMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(SAPCustomerMapping).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._sapCustomerMapping = CType(tempColl(0), SAPCustomerMapping)
                        Else
                            Me._sapCustomerMapping = New SAPCustomerMapping(0)
                        End If
                    End If

                    Return Me._sapCustomerMapping

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
        End Property

        <ColumnInfo("CountryCode", "'{0}'")> _
        Public Property CountryCode As String
            Get
                Return _countryCode
            End Get
            Set(ByVal value As String)
                _countryCode = value
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


        Private _vechileTypeCode As String
        Private _salesmanHeaderCode As String
        Private _dealerCode As String
        Public Property VehicleTypeCode As String
            Get
                Return _vechileTypeCode
            End Get
            Set(ByVal value As String)
                _vechileTypeCode = value
            End Set
        End Property

        Public Property SalesmanHeaderCode As String
            Get
                Return _salesmanHeaderCode
            End Get
            Set(ByVal value As String)
                _salesmanHeaderCode = value
            End Set
        End Property

        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        Public Property CustomerTypeValue As String
        Public Property AgeSegmentDate As DateTime
        Public Property CustomerPurposeValue As String
        Public Property InformationTypeValue As String
        Public Property SexValue As String
        Public Property InformationSourceValue As String
#End Region

    End Class
End Namespace

