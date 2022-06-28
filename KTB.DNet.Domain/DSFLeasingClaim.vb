
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DSFLeasingClaim Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 10/12/2019 - 16:40:27
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
    <Serializable(), TableInfo("DSFLeasingClaim")> _
    Public Class DSFLeasingClaim
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
        Private _regNumber As String = String.Empty
        Private _chassisMasterID As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _engineNumber As String = String.Empty
        Private _validatingRemark As String = String.Empty
        Private _validatingResult As ValidateResult = ValidateResult.Valid
        Private _validatingResultCode As ValidateResultCode = ValidateResultCode.OK
        Private _claimDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _assetSeqNo As Integer
        Private _agreementNo As String = String.Empty
        Private _sKDNumber As String = String.Empty
        Private _sKDDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sKDApprovalDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _goLiveDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerName As String = String.Empty
        Private _unit As Integer
        Private _objectLease As String = String.Empty
        Private _aTPMSubsidy As Decimal
        Private _supplierName As String = String.Empty
        Private _programName As String = String.Empty
        Private _collectionPeriodMonth As Byte
        Private _collectionPeriodYear As Short
        Private _totalDP As Decimal
        Private _totalAmountLease As Decimal
        Private _periodLease As Integer
        Private _interestLease As Decimal
        Private _insurance As String = String.Empty
        Private _typeInsurance As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _collectionPeriod As String = String.Empty
        Private _benefitClaimHeader As BenefitClaimHeader
        Private _chassisMaster As ChassisMaster
        Private _dealer As Dealer

        Private _dSFLeasingClaimDocuments As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _remarkByDealer As String = String.Empty
        Private _remarkByDSF As String = String.Empty

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


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "'{0}'")> _
        Public Property ChassisMasterID As String
            Get
                Return _chassisMasterID
            End Get
            Set(ByVal value As String)
                _chassisMasterID = value
            End Set
        End Property


        <ColumnInfo("ClaimDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ClaimDate As DateTime
            Get
                Return _claimDate
            End Get
            Set(ByVal value As DateTime)
                _claimDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("AssetSeqNo", "{0}")> _
        Public Property AssetSeqNo As Integer
            Get
                Return _assetSeqNo
            End Get
            Set(ByVal value As Integer)
                _assetSeqNo = value
            End Set
        End Property


        <ColumnInfo("AgreementNo", "'{0}'")> _
        Public Property AgreementNo As String
            Get
                Return _agreementNo
            End Get
            Set(ByVal value As String)
                _agreementNo = value
            End Set
        End Property


        <ColumnInfo("SKDNumber", "'{0}'")> _
        Public Property SKDNumber As String
            Get
                Return _sKDNumber
            End Get
            Set(ByVal value As String)
                _sKDNumber = value
            End Set
        End Property


        <ColumnInfo("SKDDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SKDDate As DateTime
            Get
                Return _sKDDate
            End Get
            Set(ByVal value As DateTime)
                _sKDDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SKDApprovalDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SKDApprovalDate As DateTime
            Get
                Return _sKDApprovalDate
            End Get
            Set(ByVal value As DateTime)
                _sKDApprovalDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("GoLiveDate", "'{0:yyyy/MM/dd}'")> _
        Public Property GoLiveDate As DateTime
            Get
                Return _goLiveDate
            End Get
            Set(ByVal value As DateTime)
                _goLiveDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("Unit", "{0}")> _
        Public Property Unit As Integer
            Get
                Return _unit
            End Get
            Set(ByVal value As Integer)
                _unit = value
            End Set
        End Property


        <ColumnInfo("ObjectLease", "'{0}'")> _
        Public Property ObjectLease As String
            Get
                Return _objectLease
            End Get
            Set(ByVal value As String)
                _objectLease = value
            End Set
        End Property


        <ColumnInfo("ATPMSubsidy", "{0}")> _
        Public Property ATPMSubsidy As Decimal
            Get
                Return _aTPMSubsidy
            End Get
            Set(ByVal value As Decimal)
                _aTPMSubsidy = value
            End Set
        End Property


        <ColumnInfo("SupplierName", "'{0}'")> _
        Public Property SupplierName As String
            Get
                Return _supplierName
            End Get
            Set(ByVal value As String)
                _supplierName = value
            End Set
        End Property


        <ColumnInfo("ProgramName", "'{0}'")> _
        Public Property ProgramName As String
            Get
                Return _programName
            End Get
            Set(ByVal value As String)
                _programName = value
            End Set
        End Property


        <ColumnInfo("CollectionPeriodMonth", "{0}")> _
        Public Property CollectionPeriodMonth As Byte
            Get
                Return _collectionPeriodMonth
            End Get
            Set(ByVal value As Byte)
                _collectionPeriodMonth = value
            End Set
        End Property


        <ColumnInfo("CollectionPeriodYear", "{0}")> _
        Public Property CollectionPeriodYear As Short
            Get
                Return _collectionPeriodYear
            End Get
            Set(ByVal value As Short)
                _collectionPeriodYear = value
            End Set
        End Property


        <ColumnInfo("TotalDP", "{0}")> _
        Public Property TotalDP As Decimal
            Get
                Return _totalDP
            End Get
            Set(ByVal value As Decimal)
                _totalDP = value
            End Set
        End Property


        <ColumnInfo("TotalAmountLease", "{0}")> _
        Public Property TotalAmountLease As Decimal
            Get
                Return _totalAmountLease
            End Get
            Set(ByVal value As Decimal)
                _totalAmountLease = value
            End Set
        End Property


        <ColumnInfo("PeriodLease", "{0}")> _
        Public Property PeriodLease As Integer
            Get
                Return _periodLease
            End Get
            Set(ByVal value As Integer)
                _periodLease = value
            End Set
        End Property


        <ColumnInfo("InterestLease", "{0}")> _
        Public Property InterestLease As Decimal
            Get
                Return _interestLease
            End Get
            Set(ByVal value As Decimal)
                _interestLease = value
            End Set
        End Property


        <ColumnInfo("Insurance", "'{0}'")> _
        Public Property Insurance As String
            Get
                Return _insurance
            End Get
            Set(ByVal value As String)
                _insurance = value
            End Set
        End Property


        <ColumnInfo("TypeInsurance", "'{0}'")> _
        Public Property TypeInsurance As String
            Get
                Return _typeInsurance
            End Get
            Set(ByVal value As String)
                _typeInsurance = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
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


        <ColumnInfo("BenefitClaimHeaderID", "{0}"), _
        RelationInfo("BenefitClaimHeader", "ID", "DSFLeasingClaim", "BenefitClaimHeaderID")> _
        Public Property BenefitClaimHeader As BenefitClaimHeader
            Get
                Try
                    If Not IsNothing(Me._benefitClaimHeader) AndAlso (Not Me._benefitClaimHeader.IsLoaded) Then

                        Me._benefitClaimHeader = CType(DoLoad(GetType(BenefitClaimHeader).ToString(), _benefitClaimHeader.ID), BenefitClaimHeader)
                        Me._benefitClaimHeader.MarkLoaded()
                    End If
                    Return Me._benefitClaimHeader
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As BenefitClaimHeader)
                Me._benefitClaimHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitClaimHeader.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "DSFLeasingClaim", "ChassisMasterID")> _
        Public Property ChassisMaster As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then

                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then
                            Me._chassisMaster.MarkLoaded()
                        End If
                    End If
                    Return Me._chassisMaster
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)
                Me._chassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMaster.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "DSFLeasingClaim", "DealerID")> _
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


        <RelationInfo("DSFLeasingClaim", "ID", "DSFLeasingClaimDocument", "DSFLeasingClaimID")> _
        Public ReadOnly Property DSFLeasingClaimDocuments As System.Collections.ArrayList
            Get
                Try
                    If (Me._dSFLeasingClaimDocuments.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DSFLeasingClaimDocument), "DSFLeasingClaim", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DSFLeasingClaimDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dSFLeasingClaimDocuments = DoLoadArray(GetType(DSFLeasingClaimDocument).ToString, criterias)
                    End If

                    Return Me._dSFLeasingClaimDocuments

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("RemarkByDealer", "'{0}'")> _
        Public Property RemarkByDealer As String
            Get
                Return _remarkByDealer
            End Get
            Set(ByVal value As String)
                _remarkByDealer = value
            End Set
        End Property

        <ColumnInfo("RemarkByDSF", "'{0}'")> _
        Public Property RemarkByDSF As String
            Get
                Return _remarkByDSF
            End Get
            Set(ByVal value As String)
                _remarkByDSF = value
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

#Region "Custom Property"
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        Public Property EngineNumber As String
            Get
                Return _engineNumber
            End Get
            Set(ByVal value As String)
                _engineNumber = value
            End Set
        End Property

        Public Property ValidatingRemark As String
            Get
                Return _validatingRemark
            End Get
            Set(ByVal value As String)
                _validatingRemark = value
            End Set
        End Property

        Public Property ValidatingResult As ValidateResult
            Get
                Return _validatingResult
            End Get
            Set(ByVal value As ValidateResult)
                _validatingResult = value
            End Set
        End Property

        Public Property ValidatingResultCode As ValidateResultCode
            Get
                Return _validatingResultCode
            End Get
            Set(ByVal value As ValidateResultCode)
                _validatingResultCode = value
            End Set
        End Property

        Public Property CollectionPeriod As String
            Get
                Return _collectionPeriod
            End Get
            Set(ByVal value As String)
                _collectionPeriod = value
            End Set
        End Property

#End Region

        Enum ValidateResult
            Valid = 1
            NotValid = 0
        End Enum

        Enum ValidateResultCode
            OK = 0
            ChassisNotValid = 1
            EngineNotValid = 2
            ChassisAndEngineNotValid = 3
            AlreadyClaimByDealer = 4
            BenefitNotValid = 5
            DataDoubleUpload = 6
            ClaimSudahPernahDiupload = 7
        End Enum

    End Class
End Namespace

