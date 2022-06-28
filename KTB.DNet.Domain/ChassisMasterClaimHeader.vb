#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMasterClaimHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 9/7/2020 - 9:05:06 AM
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
    <Serializable(), TableInfo("ChassisMasterClaimHeader")> _
    Public Class ChassisMasterClaimHeader
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
        Private _reporterIssue As String = String.Empty
        Private _claimDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerPIC As String = String.Empty
        Private _claimNumber As String = String.Empty
        Private _statusID As Integer
        Private _dateOccur As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _placeOccur As String = String.Empty
        Private _responClaim As Integer
        Private _chassisNumberReplacement As String = String.Empty
        Private _claimPoint As String = String.Empty
        Private _remark As String = String.Empty
        Private _statusStockDMS As Short
        Private _statusProcessRetur As Short
        Private _isTransferSAP As Short
        Private _repairEstimationDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _completionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _nominal As Decimal
        Private _transferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _engineNumberReplacement As String = String.Empty
        Private _soRetur As String = String.Empty
        Private _doRetur As String = String.Empty
        Private _billingRetur As String = String.Empty
        Private _soNormalRetur As String = String.Empty
        Private _doNormalRetur As String = String.Empty
        Private _billingNormalRetur As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateTIme As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _chassisMaster As ChassisMaster
        Private _dealer As Dealer
        Private _pODestination As PODestination
        Private _chassisPODestination As PODestination
        Private _chassisMasterLogisticCompany As ChassisMasterLogisticCompany

        Private _chassisMasterClaimDetails As ArrayList = New ArrayList
        Private _documentUploads As ArrayList = New ArrayList

        Private _isFromUpload As Boolean

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


        <ColumnInfo("ReporterIssue", "'{0}'")> _
        Public Property ReporterIssue As String
            Get
                Return _reporterIssue
            End Get
            Set(ByVal value As String)
                _reporterIssue = value
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


        <ColumnInfo("DealerPIC", "'{0}'")> _
        Public Property DealerPIC As String
            Get
                Return _dealerPIC
            End Get
            Set(ByVal value As String)
                _dealerPIC = value
            End Set
        End Property


        <ColumnInfo("ClaimNumber", "'{0}'")> _
        Public Property ClaimNumber As String
            Get
                Return _claimNumber
            End Get
            Set(ByVal value As String)
                _claimNumber = value
            End Set
        End Property


        <ColumnInfo("StatusID", "{0}")> _
        Public Property StatusID As Integer
            Get
                Return _statusID
            End Get
            Set(ByVal value As Integer)
                _statusID = value
            End Set
        End Property


        <ColumnInfo("DateOccur", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateOccur As DateTime
            Get
                Return _dateOccur
            End Get
            Set(ByVal value As DateTime)
                _dateOccur = value
            End Set
        End Property


        <ColumnInfo("PlaceOccur", "'{0}'")> _
        Public Property PlaceOccur As String
            Get
                Return _placeOccur
            End Get
            Set(ByVal value As String)
                _placeOccur = value
            End Set
        End Property


        <ColumnInfo("ResponClaim", "{0}")> _
        Public Property ResponClaim As Integer
            Get
                Return _responClaim
            End Get
            Set(ByVal value As Integer)
                _responClaim = value
            End Set
        End Property


        <ColumnInfo("ChassisNumberReplacement", "'{0}'")> _
        Public Property ChassisNumberReplacement As String
            Get
                Return _chassisNumberReplacement
            End Get
            Set(ByVal value As String)
                _chassisNumberReplacement = value
            End Set
        End Property


        <ColumnInfo("ClaimPoint", "'{0}'")> _
        Public Property ClaimPoint As String
            Get
                Return _claimPoint
            End Get
            Set(ByVal value As String)
                _claimPoint = value
            End Set
        End Property


        <ColumnInfo("Remark", "'{0}'")> _
        Public Property Remark As String
            Get
                Return _remark
            End Get
            Set(ByVal value As String)
                _remark = value
            End Set
        End Property


        <ColumnInfo("StatusStockDMS", "{0}")> _
        Public Property StatusStockDMS As Short
            Get
                Return _statusStockDMS
            End Get
            Set(ByVal value As Short)
                _statusStockDMS = value
            End Set
        End Property


        <ColumnInfo("StatusProcessRetur", "{0}")> _
        Public Property StatusProcessRetur As Short
            Get
                Return _statusProcessRetur
            End Get
            Set(ByVal value As Short)
                _statusProcessRetur = value
            End Set
        End Property


        <ColumnInfo("IsTransferSAP", "{0}")> _
        Public Property IsTransferSAP As Short
            Get
                Return _isTransferSAP
            End Get
            Set(ByVal value As Short)
                _isTransferSAP = value
            End Set
        End Property


        <ColumnInfo("RepairEstimationDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RepairEstimationDate As DateTime
            Get
                Return _repairEstimationDate
            End Get
            Set(ByVal value As DateTime)
                _repairEstimationDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CompletionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property CompletionDate As DateTime
            Get
                Return _completionDate
            End Get
            Set(ByVal value As DateTime)
                _completionDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Nominal", "{0}")> _
        Public Property Nominal As Decimal
            Get
                Return _nominal
            End Get
            Set(ByVal value As Decimal)
                _nominal = value
            End Set
        End Property


        <ColumnInfo("TransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransferDate As DateTime
            Get
                Return _transferDate
            End Get
            Set(ByVal value As DateTime)
                _transferDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("EngineNumberReplacement", "'{0}'")> _
        Public Property EngineNumberReplacement As String
            Get
                Return _engineNumberReplacement
            End Get
            Set(ByVal value As String)
                _engineNumberReplacement = value
            End Set
        End Property

        <ColumnInfo("SORetur", "'{0}'")> _
        Public Property SORetur As String
            Get
                Return _soRetur
            End Get
            Set(ByVal value As String)
                _soRetur = value
            End Set
        End Property

        <ColumnInfo("DORetur", "'{0}'")> _
        Public Property DORetur As String
            Get
                Return _doRetur
            End Get
            Set(ByVal value As String)
                _doRetur = value
            End Set
        End Property

        <ColumnInfo("BillingRetur", "'{0}'")> _
        Public Property BillingRetur As String
            Get
                Return _billingRetur
            End Get
            Set(ByVal value As String)
                _billingRetur = value
            End Set
        End Property

        <ColumnInfo("SONormalRetur", "'{0}'")> _
        Public Property SONormalRetur As String
            Get
                Return _soNormalRetur
            End Get
            Set(ByVal value As String)
                _soNormalRetur = value
            End Set
        End Property

        <ColumnInfo("DONormalRetur", "'{0}'")> _
        Public Property DONormalRetur As String
            Get
                Return _doNormalRetur
            End Get
            Set(ByVal value As String)
                _doNormalRetur = value
            End Set
        End Property

        <ColumnInfo("BillingNormalRetur", "'{0}'")> _
        Public Property BillingNormalRetur As String
            Get
                Return _billingNormalRetur
            End Get
            Set(ByVal value As String)
                _billingNormalRetur = value
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


        <ColumnInfo("LastUpdateTIme", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTIme As DateTime
            Get
                Return _lastUpdateTIme
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTIme = value
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


        '<ColumnInfo("ChassisMasterID","{0}")> _
        'public property ChassisMasterID as integer

        '	get
        '		return _chassisMasterID}
        '	end get
        '	set(byval value as integer)
        '		_chassisMasterID= value
        '	end set			
        'end property
        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "ChassisMasterClaimHeader", "ChassisMasterID")> _
        Public Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then

                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        'Me._chassisMaster.MarkLoaded()

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

        '    <ColumnInfo("DealerID", "{0}")> _
        '            Public Property DealerID As Short

        '        Get
        'return _dealerID}
        '        End Get
        '        Set(ByVal value As Short)
        '            _dealerID = value
        '        End Set
        '    End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "ChassisMasterClaimHeader", "DealerID")> _
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

        '<ColumnInfo("PODestinationID","{0}")> _
        'public property PODestinationID as integer

        '	get
        '		return _pODestinationID}
        '	end get
        '	set(byval value as integer)
        '		_pODestinationID= value
        '	end set			
        'end property

        <ColumnInfo("PODestinationID", "{0}"), _
        RelationInfo("PODestination", "ID", "ChassisMasterClaimHeader", "PODestinationID")> _
        Public Property PODestination() As PODestination
            Get
                Try
                    If Not IsNothing(Me._pODestination) AndAlso (Not Me._pODestination.IsLoaded) Then

                        Me._pODestination = CType(DoLoad(GetType(PODestination).ToString(), _pODestination.ID), PODestination)
                        Me._pODestination.MarkLoaded()

                    End If

                    Return Me._pODestination

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PODestination)

                Me._pODestination = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pODestination.MarkLoaded()
                End If
            End Set
        End Property


        '<ColumnInfo("ChassisPODestinationID","{0}")> _
        'public property ChassisPODestinationID as integer
        '	get
        '		return _chassisPODestinationID
        '	end get
        '	set(byval value as integer)
        '		_chassisPODestinationID= value
        '	end set
        'end property


        <ColumnInfo("ChassisPODestinationID", "{0}"), _
        RelationInfo("PODestination", "ID", "ChassisMasterClaimHeader", "PODestinationID")> _
        Public Property ChassisPODestination() As PODestination
            Get
                Try
                    If Not IsNothing(Me._chassisPODestination) AndAlso (Not Me._chassisPODestination.IsLoaded) Then

                        Me._chassisPODestination = CType(DoLoad(GetType(PODestination).ToString(), _chassisPODestination.ID), PODestination)
                        Me._chassisPODestination.MarkLoaded()

                    End If

                    Return Me._chassisPODestination

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PODestination)

                Me._chassisPODestination = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisPODestination.MarkLoaded()
                End If
            End Set
        End Property


        '<ColumnInfo("LogisticCompanyID", "'{0}'")> _
        'Public Property LogisticCompanyID As String
        '    Get
        '        Return _logisticCompanyID
        '    End Get
        '    Set(ByVal value As String)
        '        _logisticCompanyID = value
        '    End Set
        'End Property
        <ColumnInfo("LogisticCompanyID", "{0}"), _
        RelationInfo("ChassisMasterLogisticCompany", "ID", "ChassisMasterClaimHeader", "ChassisMasterLogisticCompanyID")> _
        Public Property ChassisMasterLogisticCompany() As ChassisMasterLogisticCompany
            Get
                Try
                    If Not IsNothing(Me._chassisMasterLogisticCompany) AndAlso (Not Me._chassisMasterLogisticCompany.IsLoaded) Then

                        Me._chassisMasterLogisticCompany = CType(DoLoad(GetType(ChassisMasterLogisticCompany).ToString(), _chassisMasterLogisticCompany.ID), ChassisMasterLogisticCompany)
                        Me._chassisMasterLogisticCompany.MarkLoaded()

                    End If

                    Return Me._chassisMasterLogisticCompany

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMasterLogisticCompany)

                Me._chassisMasterLogisticCompany = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMasterLogisticCompany.MarkLoaded()
                End If
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
        <RelationInfo("ChassisMasterClaimHeader", "ID", "ChassisMasterClaimDetail", "ChassisMasterClaimHeaderID")> _
        Public ReadOnly Property ChassisMasterClaimDetails() As System.Collections.ArrayList
            Get
                Try
                    Dim _criteria As Criteria = New Criteria(GetType(ChassisMasterClaimDetail), "ChassisMasterClaimHeader", Me.ID)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                    criterias.opAnd(New Criteria(GetType(ChassisMasterClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Me._chassisMasterClaimDetails = DoLoadArray(GetType(ChassisMasterClaimDetail).ToString, criterias)
                    Return Me._chassisMasterClaimDetails
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
        End Property

        <RelationInfo("DocumentUpload", "DocRegNumber", "ChassisMasterClaimHeader", "ClaimNumber")> _
        Public ReadOnly Property DocumentUploads() As System.Collections.ArrayList
            Get
                Try
                    If (Me.ClaimNumber <> "") Then
                        Dim _criteria As Criteria = New Criteria(GetType(DocumentUpload), "DocRegNumber", Me.ClaimNumber)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DocumentUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Me._documentUploads = DoLoadArray(GetType(DocumentUpload).ToString, criterias)
                    End If
                    Return Me._documentUploads
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property IsFromUpload() As Boolean
            Get
                If Me.CreatedBy.Contains("000002") Then
                    Return True
                End If
                Return False
            End Get
        End Property

        Public ReadOnly Property IsDMSClaim() As Boolean
            Get
                If Me.CreatedBy.Contains("IF") Then
                    Return True
                End If
                Return False
            End Get
        End Property
#End Region

    End Class
End Namespace
