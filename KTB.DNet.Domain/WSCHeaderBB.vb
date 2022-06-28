#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCHeaderBB Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/29/2005 - 10:53:03 AM
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
    <Serializable(), TableInfo("WSCHeaderBB")> _
    Public Class WSCHeaderBB
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
        Private _claimType As String = String.Empty
        Private _claimNumber As String = String.Empty
        Private _refClaimNumber As String = String.Empty
        Private _failureDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _serviceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _miliage As Integer
        Private _pQR As String = String.Empty
        Private _pQRStatus As String = String.Empty
        Private _codeA As String = String.Empty
        Private _codeB As String = String.Empty
        Private _codeC As String = String.Empty
        Private _description As String = String.Empty
        Private _evidencePhoto As String = String.Empty
        Private _evidenceInvoice As String = String.Empty
        Private _evidenceDmgPart As String = String.Empty

        Private _evidenceRepair As String = String.Empty
        Private _evidenceWSCLetter As String = String.Empty
        Private _evidenceWSCTechnical As String = String.Empty
        Private _causes As String = String.Empty
        Private _results As String = String.Empty
        Private _notes As String = String.Empty

        Private _reqDmgPart As String = String.Empty
        Private _reqDmgPartBy As String = String.Empty
        Private _reqDmgPartTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _notificationNumber As String = String.Empty
        Private _decideDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _releaseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As String = String.Empty
        Private _claimStatus As String = String.Empty
        Private _laborAmount As Decimal
        Private _partAmount As Decimal
        Private _partReceiveBy As String = String.Empty
        Private _partReceiveTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _downLoadBy As String = String.Empty
        Private _downLoadTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _responseTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _workOrderNumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _reason As Reason
        Private _ChassisMasterBB As ChassisMasterBB
        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch

        Private _wSCDamageRequestPartBBs As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _wSCEvidenceBBs As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _wSCDetailBBs As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("ClaimType", "'{0}'")> _
        Public Property ClaimType() As String
            Get
                Return _claimType
            End Get
            Set(ByVal value As String)
                _claimType = value
            End Set
        End Property


        <ColumnInfo("ClaimNumber", "'{0}'")> _
        Public Property ClaimNumber() As String
            Get
                Return _claimNumber
            End Get
            Set(ByVal value As String)
                _claimNumber = value
            End Set
        End Property


        <ColumnInfo("RefClaimNumber", "'{0}'")> _
        Public Property RefClaimNumber() As String
            Get
                Return _refClaimNumber
            End Get
            Set(ByVal value As String)
                _refClaimNumber = value
            End Set
        End Property


        <ColumnInfo("FailureDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FailureDate() As DateTime
            Get
                Return _failureDate
            End Get
            Set(ByVal value As DateTime)
                _failureDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ServiceDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ServiceDate() As DateTime
            Get
                Return _serviceDate
            End Get
            Set(ByVal value As DateTime)
                _serviceDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Miliage", "{0}")> _
        Public Property Miliage() As Integer
            Get
                Return _miliage
            End Get
            Set(ByVal value As Integer)
                _miliage = value
            End Set
        End Property


        <ColumnInfo("PQR", "'{0}'")> _
        Public Property PQR() As String
            Get
                Return _pQR
            End Get
            Set(ByVal value As String)
                _pQR = value
            End Set
        End Property


        <ColumnInfo("PQRStatus", "'{0}'")> _
        Public Property PQRStatus() As String
            Get
                Return _pQRStatus
            End Get
            Set(ByVal value As String)
                _pQRStatus = value
            End Set
        End Property


        <ColumnInfo("CodeA", "'{0}'")> _
        Public Property CodeA() As String
            Get
                Return _codeA
            End Get
            Set(ByVal value As String)
                _codeA = value
            End Set
        End Property


        <ColumnInfo("CodeB", "'{0}'")> _
        Public Property CodeB() As String
            Get
                Return _codeB
            End Get
            Set(ByVal value As String)
                _codeB = value
            End Set
        End Property


        <ColumnInfo("CodeC", "'{0}'")> _
        Public Property CodeC() As String
            Get
                Return _codeC
            End Get
            Set(ByVal value As String)
                _codeC = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("EvidencePhoto", "'{0}'")> _
        Public Property EvidencePhoto() As String
            Get
                Return _evidencePhoto
            End Get
            Set(ByVal value As String)
                _evidencePhoto = value
            End Set
        End Property


        <ColumnInfo("EvidenceInvoice", "'{0}'")> _
        Public Property EvidenceInvoice() As String
            Get
                Return _evidenceInvoice
            End Get
            Set(ByVal value As String)
                _evidenceInvoice = value
            End Set
        End Property


        <ColumnInfo("EvidenceDmgPart", "'{0}'")> _
        Public Property EvidenceDmgPart() As String
            Get
                Return _evidenceDmgPart
            End Get
            Set(ByVal value As String)
                _evidenceDmgPart = value
            End Set
        End Property


        <ColumnInfo("EvidenceRepair", "'{0}'")> _
        Public Property EvidenceRepair() As String
            Get
                Return _evidenceRepair
            End Get
            Set(ByVal value As String)
                _evidenceRepair = value
            End Set
        End Property


        <ColumnInfo("EvidenceWSCLetter", "'{0}'")> _
        Public Property EvidenceWSCLetter() As String
            Get
                Return _evidenceWSCLetter
            End Get
            Set(ByVal value As String)
                _evidenceWSCLetter = value
            End Set
        End Property


        <ColumnInfo("EvidenceWSCTechnical", "'{0}'")> _
        Public Property EvidenceWSCTechnical() As String
            Get
                Return _evidenceWSCTechnical
            End Get
            Set(ByVal value As String)
                _evidenceWSCTechnical = value
            End Set
        End Property


        <ColumnInfo("ReqDmgPart", "'{0}'")> _
        Public Property ReqDmgPart() As String
            Get
                Return _reqDmgPart
            End Get
            Set(ByVal value As String)
                _reqDmgPart = value
            End Set
        End Property


        <ColumnInfo("Causes", "'{0}'")> _
        Public Property Causes() As String
            Get
                Return _causes
            End Get
            Set(ByVal value As String)
                _causes = value
            End Set
        End Property


        <ColumnInfo("Results", "'{0}'")> _
        Public Property Results() As String
            Get
                Return _results
            End Get
            Set(ByVal value As String)
                _results = value
            End Set
        End Property


        <ColumnInfo("Notes", "'{0}'")> _
        Public Property Notes() As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property


        <ColumnInfo("ReqDmgPartBy", "'{0}'")> _
        Public Property ReqDmgPartBy() As String
            Get
                Return _reqDmgPartBy
            End Get
            Set(ByVal value As String)
                _reqDmgPartBy = value
            End Set
        End Property


        <ColumnInfo("ReqDmgPartTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ReqDmgPartTime() As DateTime
            Get
                Return _reqDmgPartTime
            End Get
            Set(ByVal value As DateTime)
                _reqDmgPartTime = value
            End Set
        End Property


        <ColumnInfo("NotificationNumber", "'{0}'")> _
        Public Property NotificationNumber() As String
            Get
                Return _notificationNumber
            End Get
            Set(ByVal value As String)
                _notificationNumber = value
            End Set
        End Property


        <ColumnInfo("DecideDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DecideDate() As DateTime
            Get
                Return _decideDate
            End Get
            Set(ByVal value As DateTime)
                _decideDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ReleaseDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReleaseDate() As DateTime
            Get
                Return _releaseDate
            End Get
            Set(ByVal value As DateTime)
                _releaseDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("ClaimStatus", "'{0}'")> _
        Public Property ClaimStatus() As String
            Get
                Return _claimStatus
            End Get
            Set(ByVal value As String)
                _claimStatus = value
            End Set
        End Property


        <ColumnInfo("LaborAmount", "{0}")> _
        Public Property LaborAmount() As Decimal
            Get
                Return _laborAmount
            End Get
            Set(ByVal value As Decimal)
                _laborAmount = value
            End Set
        End Property


        <ColumnInfo("PartAmount", "{0}")> _
        Public Property PartAmount() As Decimal
            Get
                Return _partAmount
            End Get
            Set(ByVal value As Decimal)
                _partAmount = value
            End Set
        End Property


        <ColumnInfo("PartReceiveBy", "'{0}'")> _
        Public Property PartReceiveBy() As String
            Get
                Return _partReceiveBy
            End Get
            Set(ByVal value As String)
                _partReceiveBy = value
            End Set
        End Property


        <ColumnInfo("PartReceiveTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PartReceiveTime() As DateTime
            Get
                Return _partReceiveTime
            End Get
            Set(ByVal value As DateTime)
                _partReceiveTime = value
            End Set
        End Property


        <ColumnInfo("DownLoadBy", "'{0}'")> _
        Public Property DownLoadBy() As String
            Get
                Return _downLoadBy
            End Get
            Set(ByVal value As String)
                _downLoadBy = value
            End Set
        End Property


        <ColumnInfo("DownLoadTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DownLoadTime() As DateTime
            Get
                Return _downLoadTime
            End Get
            Set(ByVal value As DateTime)
                _downLoadTime = value
            End Set
        End Property


        <ColumnInfo("ResponseTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ResponseTime() As DateTime
            Get
                Return _responseTime
            End Get
            Set(ByVal value As DateTime)
                _responseTime = value
            End Set
        End Property

        <ColumnInfo("WorkOrderNumber", "'{0}'")> _
        Public Property WorkOrderNumber() As String
            Get
                Return _workOrderNumber
            End Get
            Set(ByVal value As String)
                _workOrderNumber = value
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


        <ColumnInfo("ReasonID", "{0}"), _
        RelationInfo("Reason", "ID", "WSCHeaderBB", "ReasonID")> _
        Public Property Reason() As Reason
            Get
                Try
                    If Not IsNothing(Me._reason) AndAlso (Not Me._reason.IsLoaded) Then

                        Me._reason = CType(DoLoad(GetType(Reason).ToString(), _reason.ID), Reason)
                        Me._reason.MarkLoaded()

                    End If

                    Return Me._reason

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Reason)

                Me._reason = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._reason.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ChassisMasterBBID", "{0}"), _
        RelationInfo("ChassisMasterBB", "ID", "WSCHeaderBB", "ChassisMasterBBID")> _
        Public Property ChassisMasterBB() As ChassisMasterBB
            Get
                Try
                    If Not IsNothing(Me._ChassisMasterBB) AndAlso (Not Me._ChassisMasterBB.IsLoaded) Then

                        Me._ChassisMasterBB = CType(DoLoad(GetType(ChassisMasterBB).ToString(), _ChassisMasterBB.ID), ChassisMasterBB)
                        Me._ChassisMasterBB.MarkLoaded()

                    End If

                    Return Me._ChassisMasterBB

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMasterBB)

                Me._ChassisMasterBB = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ChassisMasterBB.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "WSCHeaderBB", "DealerID")> _
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

        <ColumnInfo("DealerBranchID", "{0}"), _
        RelationInfo("DealerBranch", "ID", "WSCHeaderBB", "DealerBranchID")> _
        Public Property DealerBranch() As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _dealer.ID), DealerBranch)
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


        <RelationInfo("WSCHeader", "ID", "WSCDamageRequestPart", "WSCHeaderID")> _
        Public ReadOnly Property WSCDamageRequestPartBBs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._wSCDamageRequestPartBBs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(wSCDamageRequestPartBB), "WSCHeaderBB", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(wSCDamageRequestPartBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._wSCDamageRequestPartBBs = DoLoadArray(GetType(wSCDamageRequestPartBB).ToString, criterias)
                    End If

                    Return Me._wSCDamageRequestPartBBs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("WSCHeaderBB", "ID", "wSCEvidenceBB", "WSCHeaderBBID")> _
        Public ReadOnly Property wSCEvidenceBBs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._wSCEvidenceBBs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(wSCEvidenceBB), "WSCHeaderBB", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(wSCEvidenceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._wSCEvidenceBBs = DoLoadArray(GetType(wSCEvidenceBB).ToString, criterias)
                    End If

                    Return Me._wSCEvidenceBBs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("WSCHeaderBB", "ID", "wSCDetailBB", "WSCHeaderBBID")> _
        Public ReadOnly Property wSCDetailBBs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._wSCDetailBBs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(wSCDetailBB), "WSCHeaderBB", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(wSCDetailBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._wSCDetailBBs = DoLoadArray(GetType(wSCDetailBB).ToString, criterias)
                    End If

                    Return Me._wSCDetailBBs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
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

#Region "Custom Property"

        Public ReadOnly Property StatusText() As String
            Get
                Return enumStatusWSC.WSCStatusDesc(Status)
            End Get
        End Property

        Public ReadOnly Property SrvcDateText() As String
            Get
                Return IIf(Format(_serviceDate, "dd/MM/yyyy") = "01/01/1753" Or _
                           Format(_serviceDate, "dd/MM/yyyy") = "01/01/1900", _
                           "", Format(_serviceDate, "dd/MM/yyyy"))
            End Get
        End Property

        Public ReadOnly Property DecideDateText() As String
            Get
                Return IIf(Format(_decideDate, "dd/MM/yyyy") = "01/01/1753" Or _
                           Format(_decideDate, "dd/MM/yyyy") = "01/01/1900", _
                           "", Format(_decideDate, "dd/MM/yyyy"))
            End Get
        End Property

        Public ReadOnly Property ReleaseDateText() As String
            Get
                Return IIf(Format(_releaseDate, "dd/MM/yyyy") = "01/01/1753" Or _
                           Format(_releaseDate, "dd/MM/yyyy") = "01/01/1900", _
                           "", Format(_releaseDate, "dd/MM/yyyy"))
            End Get
        End Property

        Public ReadOnly Property CreateDateText() As String
            Get
                Return IIf(Format(_createdTime, "dd/MM/yyyy") = "01/01/1753" Or _
                           Format(_createdTime, "dd/MM/yyyy") = "01/01/1900", _
                           "", Format(_createdTime, "dd/MM/yyyy"))
            End Get
        End Property

        <ColumnInfo("TotalAmount", "{0}")> _
        Public ReadOnly Property TotalAmount() As Decimal
            Get
                Return _partAmount + _laborAmount
            End Get
        End Property

        Private _stringServiceDate As String = String.Empty

        <ColumnInfo("StringServiceDate", "{0}")> _
        Public Property StringServiceDate() As String
            Set(ByVal Value As String)
                _stringServiceDate = Value
            End Set
            Get
                Return _stringServiceDate
            End Get
        End Property

#End Region

    End Class
End Namespace

