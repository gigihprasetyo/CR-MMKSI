#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/4/2007 - 2:46:38 PM
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
    <Serializable(), TableInfo("SalesmanHeader")> _
    Public Class SalesmanHeader
        Inherits DomainObject

#Region "Public Constants"
        Public Const MAX_PHOTO_SIZE As Integer = 512000
        Public Const VALID_IMAGE_TYPE As String = "IMAGE"
#End Region
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
        Private _image As Byte()
        Private _placeOfBirth As String = String.Empty
        Private _dateOfBirth As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _gender As Byte
        Private _address As String = String.Empty
        Private _city As String = String.Empty
        Private _shopSiteNumber As Integer
        Private _hireDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _jobPositionId_Second As Integer
        Private _jobPositionId_Third As Integer
        Private _leaderId As Integer
        Private _jobPositionId_Leader As Integer
        Private _registerStatus As String = String.Empty
        Private _marriedStatus As String = String.Empty
        Private _resignType As Short
        Private _resignDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _resignReasonType As Short
        Private _resignReason As String = String.Empty
        Private _salesIndicator As Byte
        Private _salesUnitIndicator As Byte
        Private _mechanicIndicator As Byte
        Private _sparePartIndicator As Byte
        Private _sPAdminIndicator As Byte
        Private _sPWareHouseIndicator As Byte
        Private _sPCounterIndicator As Byte
        Private _sPSalesIndicator As Byte
        Private _isRequestID As Byte
        Private _status As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        ' 15 Jul 2021 CR CDP SPK Enhancement
        Private _IsOtherCity As Integer

        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch
        Private _salesmanArea As SalesmanArea
        Private _salesmanLevel As SalesmanLevel
        Private _jobPosition As JobPosition
        Private _salesmanheadertodealer As SalesmanHeaderToDealer
        Private _sAPRegisters As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sAPCustomers As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanTrainings As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanExperiences As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanAreaAssigns As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanTrainingParticipants As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanSalesTargets As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanAccomplishs As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanProfiles As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanUniformOrderDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanUniformAssigneds As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _uniformSalesmans As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _trainingSaless As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _trainingConfirmations As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sAPCustomerProspects As System.Collections.ArrayList = New System.Collections.ArrayList

        ' 8 Jun 2011 ANH salesman part
        Private _salesmanAdditionalInfo As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanPartShop As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanPartTarget As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanPartPerformance As System.Collections.ArrayList = New System.Collections.ArrayList

        ' 24/Jul/2007   Deddy H     Penambahan column temporary
        Private _totalHire As Integer
        Private _totalResign As Integer
        Private _totalBM As Integer
        Private _totalMgr As Integer
        Private _totalAMGR As Integer
        Private _totalspv1 As Integer
        Private _totalspv2 As Integer
        Private _totalspv3 As Integer
        Private _totalsl1 As Integer
        Private _totalsl2 As Integer
        Private _totaltr As Integer
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

        <ColumnInfo("Email", "{0}")> _
        Public ReadOnly Property Email() As String
            Get
                Dim currentEmail As String = String.Empty
                For Each salesmanProfile As SalesmanProfile In Me.SalesmanProfiles
                    If (salesmanProfile.ProfileHeader.ID = 26) Then
                        currentEmail = salesmanProfile.ProfileValue
                    End If
                Next

                Return currentEmail
            End Get
        End Property

        <ColumnInfo("PhoneNumber", "{0}")> _
        Public ReadOnly Property PhoneNumber() As String
            Get
                Dim currentEmail As String = String.Empty
                For Each salesmanProfile As SalesmanProfile In Me.SalesmanProfiles
                    If (salesmanProfile.ProfileHeader.ID = 33) Then
                        currentEmail = salesmanProfile.ProfileValue
                    End If
                Next

                Return currentEmail
            End Get
        End Property

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("SalesmanProfile", "SalesmanHeaderID", "SalesmanHeader", "ID")> _
        Public ReadOnly Property NoKTP() As SalesmanProfile
            Get
                Dim currentEmail As String = String.Empty
                For Each salesmanProfile As SalesmanProfile In _salesmanProfiles
                    If (salesmanProfile.ProfileHeader.ID = 29) Then
                        Return salesmanProfile
                    End If
                Next

                Return Nothing
            End Get
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


        <ColumnInfo("Image", "{0}")> _
        Public Property Image() As Byte()
            Get
                Return _image
            End Get
            Set(ByVal value As Byte())
                _image = value
            End Set
        End Property


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


        <ColumnInfo("ShopSiteNumber", "{0}")> _
        Public Property ShopSiteNumber() As Integer
            Get
                Return _shopSiteNumber
            End Get
            Set(ByVal value As Integer)
                _shopSiteNumber = value
            End Set
        End Property


        <ColumnInfo("HireDate", "'{0:yyyy/MM/dd}'")> _
        Public Property HireDate() As DateTime
            Get
                Return _hireDate
            End Get
            Set(ByVal value As DateTime)
                _hireDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("JobPositionId_Second", "{0}")> _
        Public Property JobPositionId_Second() As Integer
            Get
                Return _jobPositionId_Second
            End Get
            Set(ByVal value As Integer)
                _jobPositionId_Second = value
            End Set
        End Property


        <ColumnInfo("JobPositionId_Third", "{0}")> _
        Public Property JobPositionId_Third() As Integer
            Get
                Return _jobPositionId_Third
            End Get
            Set(ByVal value As Integer)
                _jobPositionId_Third = value
            End Set
        End Property


        <ColumnInfo("LeaderId", "{0}")> _
        Public Property LeaderId() As Integer
            Get
                Return _leaderId
            End Get
            Set(ByVal value As Integer)
                _leaderId = value
            End Set
        End Property


        <ColumnInfo("JobPositionId_Leader", "{0}")> _
        Public Property JobPositionId_Leader() As Integer
            Get
                Return _jobPositionId_Leader
            End Get
            Set(ByVal value As Integer)
                _jobPositionId_Leader = value
            End Set
        End Property


        <ColumnInfo("RegisterStatus", "'{0}'")> _
        Public Property RegisterStatus() As String
            Get
                Return _registerStatus
            End Get
            Set(ByVal value As String)
                _registerStatus = value
            End Set
        End Property


        <ColumnInfo("MarriedStatus", "'{0}'")> _
        Public Property MarriedStatus() As String
            Get
                Return _marriedStatus
            End Get
            Set(ByVal value As String)
                _marriedStatus = value
            End Set
        End Property

        <ColumnInfo("ResignType", "'{0}'")> _
        Public Property ResignType() As Short
            Get
                Return _resignType
            End Get
            Set(ByVal value As Short)
                _resignType = value
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

        <ColumnInfo("ResignReasonType", "'{0}'")> _
        Public Property ResignReasonType() As Short
            Get
                Return _resignReasonType
            End Get
            Set(ByVal value As Short)
                _resignReasonType = value
            End Set
        End Property


        <ColumnInfo("ResignReason", "'{0}'")> _
        Public Property ResignReason() As String
            Get
                Return _resignReason
            End Get
            Set(ByVal value As String)
                _resignReason = value
            End Set
        End Property


        <ColumnInfo("SalesIndicator", "{0}")> _
        Public Property SalesIndicator() As Byte
            Get
                Return _salesIndicator
            End Get
            Set(ByVal value As Byte)
                _salesIndicator = value
            End Set
        End Property


        <ColumnInfo("SalesUnitIndicator", "{0}")> _
        Public Property SalesUnitIndicator() As Byte
            Get
                Return _salesUnitIndicator
            End Get
            Set(ByVal value As Byte)
                _salesUnitIndicator = value
            End Set
        End Property


        <ColumnInfo("MechanicIndicator", "{0}")> _
        Public Property MechanicIndicator() As Byte
            Get
                Return _mechanicIndicator
            End Get
            Set(ByVal value As Byte)
                _mechanicIndicator = value
            End Set
        End Property


        <ColumnInfo("SparePartIndicator", "{0}")> _
        Public Property SparePartIndicator() As Byte
            Get
                Return _sparePartIndicator
            End Get
            Set(ByVal value As Byte)
                _sparePartIndicator = value
            End Set
        End Property


        <ColumnInfo("SPAdminIndicator", "{0}")> _
        Public Property SPAdminIndicator() As Byte
            Get
                Return _sPAdminIndicator
            End Get
            Set(ByVal value As Byte)
                _sPAdminIndicator = value
            End Set
        End Property


        <ColumnInfo("SPWareHouseIndicator", "{0}")> _
        Public Property SPWareHouseIndicator() As Byte
            Get
                Return _sPWareHouseIndicator
            End Get
            Set(ByVal value As Byte)
                _sPWareHouseIndicator = value
            End Set
        End Property


        <ColumnInfo("SPCounterIndicator", "{0}")> _
        Public Property SPCounterIndicator() As Byte
            Get
                Return _sPCounterIndicator
            End Get
            Set(ByVal value As Byte)
                _sPCounterIndicator = value
            End Set
        End Property


        <ColumnInfo("SPSalesIndicator", "{0}")> _
        Public Property SPSalesIndicator() As Byte
            Get
                Return _sPSalesIndicator
            End Get
            Set(ByVal value As Byte)
                _sPSalesIndicator = value
            End Set
        End Property


        <ColumnInfo("IsRequestID", "{0}")> _
        Public Property IsRequestID() As Byte
            Get
                Return _isRequestID
            End Get
            Set(ByVal value As Byte)
                _isRequestID = value
            End Set
        End Property


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


        <ColumnInfo("IsOtherCity", "{0}")>
        Public Property IsOtherCity() As Integer
            Get
                Return _IsOtherCity
            End Get
            Set(ByVal value As Integer)
                _IsOtherCity = value
            End Set
        End Property


        <ColumnInfo("DealerId", "{0}"), _
        RelationInfo("Dealer", "ID", "SalesmanHeader", "DealerId")> _
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

        <ColumnInfo("DealerBranchId", "{0}"), _
        RelationInfo("DealerBranch", "ID", "SalesmanHeader", "DealerBranchId")> _
        Public Property DealerBranch() As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _dealerBranch.ID), DealerBranch)
                        Me._dealerBranch.MarkLoaded()

                    End If

                    Return Me._dealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBranch)

                Me._dealerBranch = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanAreaId", "{0}"), _
        RelationInfo("SalesmanArea", "ID", "SalesmanHeader", "SalesmanAreaId")> _
        Public Property SalesmanArea() As SalesmanArea
            Get
                Try
                    If Not IsNothing(Me._salesmanArea) AndAlso (Not Me._salesmanArea.IsLoaded) Then

                        Me._salesmanArea = CType(DoLoad(GetType(SalesmanArea).ToString(), _salesmanArea.ID), SalesmanArea)
                        Me._salesmanArea.MarkLoaded()

                    End If

                    Return Me._salesmanArea

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanArea)

                Me._salesmanArea = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanArea.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanLevelID", "{0}"), _
        RelationInfo("SalesmanLevel", "ID", "SalesmanHeader", "SalesmanLevelID")> _
        Public Property SalesmanLevel() As SalesmanLevel
            Get
                Try
                    If Not IsNothing(Me._salesmanLevel) AndAlso (Not Me._salesmanLevel.IsLoaded) Then

                        Me._salesmanLevel = CType(DoLoad(GetType(SalesmanLevel).ToString(), _salesmanLevel.ID), SalesmanLevel)
                        Me._salesmanLevel.MarkLoaded()

                    End If

                    Return Me._salesmanLevel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanLevel)

                Me._salesmanLevel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanLevel.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("JobPositionId_Main", "{0}"), _
        RelationInfo("JobPosition", "ID", "SalesmanHeader", "JobPositionId_Main")> _
        Public Property JobPosition() As JobPosition
            Get
                Try
                    If Not IsNothing(Me._jobPosition) AndAlso (Not Me._jobPosition.IsLoaded) Then

                        Me._jobPosition = CType(DoLoad(GetType(JobPosition).ToString(), _jobPosition.ID), JobPosition)
                        Me._jobPosition.MarkLoaded()

                    End If

                    Return Me._jobPosition

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As JobPosition)

                Me._jobPosition = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._jobPosition.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("SalesmanHeaderToDealer", "SalesmanHeaderID", "SalesmanHeader", "ID")> _
        Public Property SalesmanHeaderToDealer() As SalesmanHeaderToDealer
            Get
                Try
                    If Not IsNothing(Me._salesmanheadertodealer) AndAlso (Not Me._salesmanheadertodealer.IsLoaded) Then

                        Me._salesmanheadertodealer = CType(DoLoad(GetType(SalesmanHeaderToDealer).ToString(), _salesmanheadertodealer.ID), SalesmanHeaderToDealer)
                        Me._salesmanheadertodealer.MarkLoaded()

                    End If

                    Return Me._salesmanheadertodealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeaderToDealer)

                Me._salesmanheadertodealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanheadertodealer.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SAPRegister", "SalesmanHeaderID")> _
        Public ReadOnly Property SAPRegisters() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sAPRegisters.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SAPRegister), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sAPRegisters = DoLoadArray(GetType(SAPRegister).ToString, criterias)
                    End If

                    Return Me._sAPRegisters

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SAPCustomer", "SalesmanHeaderID")> _
        Public ReadOnly Property SAPCustomers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sAPCustomers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SAPCustomer), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sAPCustomers = DoLoadArray(GetType(SAPCustomer).ToString, criterias)
                    End If

                    Return Me._sAPCustomers

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SalesmanTraining", "SalesmanId")> _
        Public ReadOnly Property SalesmanTrainings() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanTrainings.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanTraining), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanTraining), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanTrainings = DoLoadArray(GetType(SalesmanTraining).ToString, criterias)
                    End If

                    Return Me._salesmanTrainings

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SalesmanExperience", "SalesmanId")> _
        Public ReadOnly Property SalesmanExperiences() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanExperiences.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanExperience), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanExperience), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanExperiences = DoLoadArray(GetType(SalesmanExperience).ToString, criterias)
                    End If

                    Return Me._salesmanExperiences

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SalesmanAreaAssign", "SalesmanHeaderID")> _
        Public ReadOnly Property SalesmanAreaAssigns() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanAreaAssigns.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanAreaAssign), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanAreaAssigns = DoLoadArray(GetType(SalesmanAreaAssign).ToString, criterias)
                    End If

                    Return Me._salesmanAreaAssigns

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SalesmanTrainingParticipant", "SalesmanHeaderID")> _
        Public ReadOnly Property SalesmanTrainingParticipants() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanTrainingParticipants.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanTrainingParticipants = DoLoadArray(GetType(SalesmanTrainingParticipant).ToString, criterias)
                    End If

                    Return Me._salesmanTrainingParticipants

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SalesmanSalesTarget", "SalesmanHeaderID")> _
        Public ReadOnly Property SalesmanSalesTargets() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanSalesTargets.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanSalesTarget), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanSalesTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanSalesTargets = DoLoadArray(GetType(SalesmanSalesTarget).ToString, criterias)
                    End If

                    Return Me._salesmanSalesTargets

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SalesmanAccomplish", "SalesmanId")> _
        Public ReadOnly Property SalesmanAccomplishs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanAccomplishs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanAccomplish), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanAccomplish), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanAccomplishs = DoLoadArray(GetType(SalesmanAccomplish).ToString, criterias)
                    End If

                    Return Me._salesmanAccomplishs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SalesmanProfile", "SalesmanHeaderID")> _
        Public ReadOnly Property SalesmanProfiles() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanProfiles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanProfile), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanProfiles = DoLoadArray(GetType(SalesmanProfile).ToString, criterias)
                    End If

                    Return Me._salesmanProfiles

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SalesmanUniformOrderDetail", "SalesmanHeaderID")> _
        Public ReadOnly Property SalesmanUniformOrderDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanUniformOrderDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanUniformOrderDetail), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanUniformOrderDetails = DoLoadArray(GetType(SalesmanUniformOrderDetail).ToString, criterias)
                    End If

                    Return Me._salesmanUniformOrderDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SalesmanUniformAssigned", "SalesmanHeaderID")> _
        Public ReadOnly Property SalesmanUniformAssigneds() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanUniformAssigneds.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanUniformAssigneds = DoLoadArray(GetType(SalesmanUniformAssigned).ToString, criterias)
                    End If

                    Return Me._salesmanUniformAssigneds

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        '<RelationInfo("SalesmanHeader", "ID", "UniformSalesman", "SalesmanId")> _
        'Public ReadOnly Property UniformSalesmans() As System.Collections.ArrayList
        '    Get
        '        Try
        '            If (Me._uniformSalesmans.Count < 1) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(UniformSalesman), "SalesmanHeader", Me.ID)
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(UniformSalesman), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Me._uniformSalesmans = DoLoadArray(GetType(UniformSalesman).ToString, criterias)
        '            End If

        '            Return Me._uniformSalesmans

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing

        '    End Get
        'End Property

        <RelationInfo("SalesmanHeader", "ID", "TrainingSales", "SalesmanId")> _
        Public ReadOnly Property TrainingSaless() As System.Collections.ArrayList
            Get
                Try
                    If (Me._trainingSaless.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrainingSales), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrainingSales), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._trainingSaless = DoLoadArray(GetType(TrainingSales).ToString, criterias)
                    End If

                    Return Me._trainingSaless

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "TrainingConfirmation", "SalesmanId")> _
        Public ReadOnly Property TrainingConfirmations() As System.Collections.ArrayList
            Get
                Try
                    If (Me._trainingConfirmations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrainingConfirmation), "SalesmanHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrainingConfirmation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._trainingConfirmations = DoLoadArray(GetType(TrainingConfirmation).ToString, criterias)
                    End If

                    Return Me._trainingConfirmations

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        '<RelationInfo("SalesmanHeader", "ID", "SAPCustomerProspect", "SalesmanHeaderID")> _
        'Public ReadOnly Property SAPCustomerProspects() As System.Collections.ArrayList
        '    Get
        '        Try
        '            If (Me._sAPCustomerProspects.Count < 1) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(SAPCustomerProspect), "SalesmanHeader", Me.ID)
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(SAPCustomerProspect), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Me._sAPCustomerProspects = DoLoadArray(GetType(SAPCustomerProspect).ToString, criterias)
        '            End If

        '            Return Me._sAPCustomerProspects

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing

        '    End Get
        'End Property
        '


        <RelationInfo("SalesmanHeader", "ID", "SalesmanAdditionalInfo", "SalesmanHeaderId")> _
        Public ReadOnly Property SalesmanAdditionalInfo() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanAdditionalInfo.Count < 1) Then
                        'Dim _criteria As Criteria = New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", Me.ID)
                        'Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        'criterias.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Dim _criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", Me.ID))
                        Me._salesmanAdditionalInfo = DoLoadArray(GetType(SalesmanAdditionalInfo).ToString, _criteria)
                    End If

                    Return Me._salesmanAdditionalInfo

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SalesmanHeader", "ID", "SalesmanPartShop", "SalesmanHeaderId")> _
        Public ReadOnly Property SalesmanPartShop() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanPartTarget.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanPartShop), "SalesmanHeader.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanPartShop = DoLoadArray(GetType(SalesmanPartShop).ToString, criterias)
                    End If

                    Return Me._salesmanPartShop

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        '<RelationInfo("SalesmanHeader", "ID", "SalesmanPartTarget", "SalesmanHeaderId")> _
        'Public ReadOnly Property SalesmanPartTarget() As System.Collections.ArrayList
        '    Get
        '        Try
        '            If (Me._salesmanPartTarget.Count < 1) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(SalesmanPartTarget), "SalesmanHeader.ID", Me.ID)
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Me._salesmanPartTarget = DoLoadArray(GetType(SalesmanPartTarget).ToString, criterias)
        '            End If

        '            Return Me._salesmanPartTarget

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing

        '    End Get
        'End Property

        '<RelationInfo("SalesmanHeader", "ID", "SalesmanPartPerformance", "SalesmanHeaderId")> _
        'Public ReadOnly Property SalesmanPartPerformance() As System.Collections.ArrayList
        '    Get
        '        Try
        '            If (Me._salesmanPartPerformance.Count < 1) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(SalesmanPartPerformance), "SalesmanHeader.ID", Me.ID)
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Me._salesmanPartPerformance = DoLoadArray(GetType(SalesmanPartPerformance).ToString, criterias)
        '            End If

        '            Return Me._salesmanPartPerformance

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing

        '    End Get
        'End Property

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

#Region "Custom Properties"

        Public ReadOnly Property LamaBekerjaTh() As Integer
            Get
                Dim date1 As Date = Me.HireDate
                Dim date2 As Date = Date.Now

                If date2 < date1 Then
                    Return 0
                End If

                Dim monthDiff As Integer = DateDiff(DateInterval.Month, date1, date2)

                If date1.Day > date2.Day Then
                    monthDiff = monthDiff - 1
                End If

                Console.WriteLine(date1.ToString("dd/MM/yyyy"))
                Console.WriteLine(date2.ToString("dd/MM/yyyy"))
                Dim YearDiff As Integer = monthDiff \ 12

                monthDiff = monthDiff Mod 12

                Return YearDiff
            End Get
        End Property

        Public ReadOnly Property LamaBekerjaBulan() As Integer
            Get
                Dim date1 As Date = Me.HireDate
                Dim date2 As Date = Date.Now

                If date2 < date1 Then
                    Return 0
                End If

                Dim monthDiff As Integer = DateDiff(DateInterval.Month, date1, date2)

                If date1.Day > date2.Day Then
                    monthDiff = monthDiff - 1
                End If

                'Console.WriteLine(date1.ToString("dd/MM/yyyy"))
                'Console.WriteLine(date2.ToString("dd/MM/yyyy"))
                'Dim YearDiff As Integer = monthDiff '\ 12

                'monthDiff = monthDiff Mod 12

                Return monthDiff

            End Get
        End Property


#End Region

#Region "Custom Method"
        '24/Jul/2007 Deddy H        Penambahan untuk perhitungan
        Public Property TotalHire() As Integer
            Get
                Return _totalHire

            End Get
            Set(ByVal Value As Integer)
                _totalHire = Value
            End Set
        End Property

        Public Property TotalResign() As Integer
            Get
                Return _totalResign
            End Get
            Set(ByVal Value As Integer)
                _totalResign = Value
            End Set
        End Property

        Public Property TotalBM() As Integer
            Get
                Return _totalBM
            End Get
            Set(ByVal Value As Integer)
                _totalBM = Value
            End Set
        End Property

        Public Property TotalMgr() As Integer
            Get
                Return _totalMgr
            End Get
            Set(ByVal Value As Integer)
                _totalMgr = Value
            End Set
        End Property
        Public Property TotalAMGR() As Integer
            Get
                Return _totalAMGR
            End Get
            Set(ByVal Value As Integer)
                _totalAMGR = Value
            End Set
        End Property
        Public Property Totalspv1() As Integer
            Get
                Return _totalspv1
            End Get
            Set(ByVal Value As Integer)
                _totalspv1 = Value
            End Set
        End Property
        Public Property Totalspv2() As Integer
            Get
                Return _totalspv2
            End Get
            Set(ByVal Value As Integer)
                _totalspv2 = Value
            End Set
        End Property
        Public Property Totalspv3() As Integer
            Get
                Return _totalspv3
            End Get
            Set(ByVal Value As Integer)
                _totalspv3 = Value
            End Set
        End Property
        Public Property Totalsl1() As Integer
            Get
                Return _totalsl1
            End Get
            Set(ByVal Value As Integer)
                _totalsl1 = Value
            End Set
        End Property
        Public Property Totalsl2() As Integer
            Get
                Return _totalsl2
            End Get
            Set(ByVal Value As Integer)
                _totalsl2 = Value
            End Set
        End Property
        Public Property TotalTR() As Integer
            Get
                Return _totaltr
            End Get
            Set(ByVal Value As Integer)
                _totaltr = Value
            End Set
        End Property
#End Region
    End Class
End Namespace

