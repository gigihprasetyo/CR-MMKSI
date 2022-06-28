#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/9/2007 - 1:33:12 PM
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
    <Serializable(), TableInfo("PQRHeader")> _
    Public Class PQRHeader
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
        Private _pQRNo As String = String.Empty
        Private _refPQRNo As String = String.Empty
        Private _year As Integer
        Private _seqNo As Integer
        Private _pqrType As Integer
        Private _documentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _soldDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pQRDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _odoMeter As Integer
        Private _velocity As Integer
        Private _customerName As String = String.Empty
        Private _customerAddress As String = String.Empty
        Private _validationTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _confirmBy As String = String.Empty
        Private _confirmTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _realeseTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _intervalProcess As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _complexity As Short
        Private _subject As String = String.Empty
        Private _symptomps As String = String.Empty
        Private _causes As String = String.Empty
        Private _results As String = String.Empty
        Private _notes As String = String.Empty
        Private _solutions As String = String.Empty
        Private _bobot As Integer
        Private _releaseBy As String = String.Empty
        Private _finishBy As String = String.Empty
        Private _finishDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _codeA As String = String.Empty
        Private _codeB As String = String.Empty
        Private _codeC As String = String.Empty
        Private _installDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _installOdometer As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _workOrderNumber As String = String.Empty

        Private _chassisMaster As ChassisMaster
        Private _category As Category
        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch

        Private _pQRPartsCodes As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pQRAdditionalInfos As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pQRDamageCodes As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pQRDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pQRSolutionReferencess As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pQRAttachments As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pQRChangesHistorys As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pQRProfiles As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pQRQRSs As System.Collections.ArrayList = New System.Collections.ArrayList


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

        <ColumnInfo("PQRType", "{0}")> _
        Public Property PQRType() As Integer
            Get
                Return _pqrType
            End Get
            Set(ByVal value As Integer)
                _pqrType = value
            End Set
        End Property


        <ColumnInfo("PQRNo", "'{0}'")> _
        Public Property PQRNo() As String
            Get
                Return _pQRNo
            End Get
            Set(ByVal value As String)
                _pQRNo = value
            End Set
        End Property


        <ColumnInfo("RefPQRNo", "'{0}'")> _
        Public Property RefPQRNo() As String
            Get
                Return _refPQRNo
            End Get
            Set(ByVal value As String)
                _refPQRNo = value
            End Set
        End Property


        <ColumnInfo("Year", "{0}")> _
        Public Property Year() As Integer
            Get
                Return _year
            End Get
            Set(ByVal value As Integer)
                _year = value
            End Set
        End Property


        <ColumnInfo("SeqNo", "{0}")> _
        Public Property SeqNo() As Integer
            Get
                Return _seqNo
            End Get
            Set(ByVal value As Integer)
                _seqNo = value
            End Set
        End Property


        <ColumnInfo("DocumentDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DocumentDate() As DateTime
            Get
                Return _documentDate
            End Get
            Set(ByVal value As DateTime)
                _documentDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SoldDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SoldDate() As DateTime
            Get
                Return _soldDate
            End Get
            Set(ByVal value As DateTime)
                _soldDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PQRDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PQRDate() As DateTime
            Get
                Return _pQRDate
            End Get
            Set(ByVal value As DateTime)
                _pQRDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("OdoMeter", "{0}")> _
        Public Property OdoMeter() As Integer
            Get
                Return _odoMeter
            End Get
            Set(ByVal value As Integer)
                _odoMeter = value
            End Set
        End Property


        <ColumnInfo("Velocity", "{0}")> _
        Public Property Velocity() As Integer
            Get
                Return _velocity
            End Get
            Set(ByVal value As Integer)
                _velocity = value
            End Set
        End Property


        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName() As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property


        <ColumnInfo("CustomerAddress", "'{0}'")> _
        Public Property CustomerAddress() As String
            Get
                Return _customerAddress
            End Get
            Set(ByVal value As String)
                _customerAddress = value
            End Set
        End Property


        <ColumnInfo("ValidationTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidationTime() As DateTime
            Get
                Return _validationTime
            End Get
            Set(ByVal value As DateTime)
                _validationTime = value
            End Set
        End Property


        <ColumnInfo("ConfirmBy", "'{0}'")> _
        Public Property ConfirmBy() As String
            Get
                Return _confirmBy
            End Get
            Set(ByVal value As String)
                _confirmBy = value
            End Set
        End Property


        <ColumnInfo("ConfirmTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ConfirmTime() As DateTime
            Get
                Return _confirmTime
            End Get
            Set(ByVal value As DateTime)
                _confirmTime = value
            End Set
        End Property


        <ColumnInfo("RealeseTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property RealeseTime() As DateTime
            Get
                Return _realeseTime
            End Get
            Set(ByVal value As DateTime)
                _realeseTime = value
            End Set
        End Property


        <ColumnInfo("IntervalProcess", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property IntervalProcess() As DateTime
            Get
                Return _intervalProcess
            End Get
            Set(ByVal value As DateTime)
                _intervalProcess = value
            End Set
        End Property


        <ColumnInfo("Complexity", "{0}")> _
        Public Property Complexity() As Short
            Get
                Return _complexity
            End Get
            Set(ByVal value As Short)
                _complexity = value
            End Set
        End Property


        <ColumnInfo("Subject", "'{0}'")> _
        Public Property Subject() As String
            Get
                Return _subject
            End Get
            Set(ByVal value As String)
                _subject = value
            End Set
        End Property


        <ColumnInfo("Symptomps", "'{0}'")> _
        Public Property Symptomps() As String
            Get
                Return _symptomps
            End Get
            Set(ByVal value As String)
                _symptomps = value
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


        <ColumnInfo("Solutions", "'{0}'")> _
        Public Property Solutions() As String
            Get
                Return _solutions
            End Get
            Set(ByVal value As String)
                _solutions = value
            End Set
        End Property


        <ColumnInfo("Bobot", "{0}")> _
        Public Property Bobot() As Integer
            Get
                Return _bobot
            End Get
            Set(ByVal value As Integer)
                _bobot = value
            End Set
        End Property


        <ColumnInfo("ReleaseBy", "'{0}'")> _
        Public Property ReleaseBy() As String
            Get
                Return _releaseBy
            End Get
            Set(ByVal value As String)
                _releaseBy = value
            End Set
        End Property


        <ColumnInfo("FinishBy", "'{0}'")> _
        Public Property FinishBy() As String
            Get
                Return _finishBy
            End Get
            Set(ByVal value As String)
                _finishBy = value
            End Set
        End Property


        <ColumnInfo("FinishDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FinishDate() As DateTime
            Get
                Return _finishDate
            End Get
            Set(ByVal value As DateTime)
                _finishDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("WorkOrderNumber", "'{0}'")> _
        Public Property WorkOrderNumber() As String
            Get
                Return _workOrderNumber
            End Get
            Set(ByVal value As String)
                _workOrderNumber = value
            End Set
        End Property

        <ColumnInfo("InstallDate", "'{0:yyyy/MM/dd}'")> _
        Public Property InstallDate() As DateTime
            Get
                Return _installDate
            End Get
            Set(ByVal value As DateTime)
                _installDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("InstallOdometer", "{0}")> _
        Public Property InstallOdometer() As Integer
            Get
                Return _installOdometer
            End Get
            Set(ByVal value As Integer)
                _installOdometer = value
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


        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "PQRHeader", "ChassisMasterID")> _
        Public Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then

                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        Me._chassisMaster.MarkLoaded()

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

        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "PQRHeader", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not IsNothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

                        Me._category = CType(DoLoad(GetType(Category).ToString(), _category.ID), Category)
                        Me._category.MarkLoaded()

                    End If

                    Return Me._category

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Category)

                Me._category = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "PQRHeader", "DealerID")> _
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
        RelationInfo("DealerBranch", "ID", "PQRHeader", "DealerBranchID")> _
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

        <RelationInfo("PQRHeader", "ID", "PQRPartsCode", "PQRHeaderID")> _
        Public ReadOnly Property PQRPartsCodes() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pQRPartsCodes.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRPartsCode), "PQRHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRPartsCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pQRPartsCodes = DoLoadArray(GetType(PQRPartsCode).ToString, criterias)
                    End If

                    Return Me._pQRPartsCodes

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("PQRHeader", "ID", "PQRAdditionalInfo", "PQRHeaderID")> _
        Public ReadOnly Property PQRAdditionalInfos() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pQRAdditionalInfos.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRAdditionalInfo), "PQRHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pQRAdditionalInfos = DoLoadArray(GetType(PQRAdditionalInfo).ToString, criterias)
                    End If

                    Return Me._pQRAdditionalInfos

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("PQRHeader", "ID", "PQRDamageCode", "PQRHeaderID")> _
        Public ReadOnly Property PQRDamageCodes() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pQRDamageCodes.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRDamageCode), "PQRHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRDamageCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pQRDamageCodes = DoLoadArray(GetType(PQRDamageCode).ToString, criterias)
                    End If

                    Return Me._pQRDamageCodes

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("PQRHeader", "ID", "PQRDetail", "PQRHeaderID")> _
        Public ReadOnly Property PQRDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pQRDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRDetail), "PQRHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pQRDetails = DoLoadArray(GetType(PQRDetail).ToString, criterias)
                    End If

                    Return Me._pQRDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("PQRHeader", "ID", "PQRSolutionReferences", "PQRHeaderID")> _
        Public ReadOnly Property PQRSolutionReferencess() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pQRSolutionReferencess.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRSolutionReferences), "PQRHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRSolutionReferences), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pQRSolutionReferencess = DoLoadArray(GetType(PQRSolutionReferences).ToString, criterias)
                    End If

                    Return Me._pQRSolutionReferencess

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("PQRHeader", "ID", "PQRAttachment", "PQRHeaderID")> _
        Public ReadOnly Property PQRAttachments() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pQRAttachments.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRAttachment), "PQRHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRAttachment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pQRAttachments = DoLoadArray(GetType(PQRAttachment).ToString, criterias)
                    End If

                    Return Me._pQRAttachments

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("PQRHeader", "ID", "PQRChangesHistory", "PQRHeaderID")> _
        Public ReadOnly Property PQRChangesHistorys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pQRChangesHistorys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRChangesHistory), "PQRHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRChangesHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pQRChangesHistorys = DoLoadArray(GetType(PQRChangesHistory).ToString, criterias)
                    End If

                    Return Me._pQRChangesHistorys

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("PQRHeader", "ID", "PQRProfile", "PQRHeaderID")> _
        Public ReadOnly Property PQRProfiles() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pQRProfiles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRProfile), "PQRHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pQRProfiles = DoLoadArray(GetType(PQRProfile).ToString, criterias)
                    End If

                    Return Me._pQRProfiles

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("PQRHeader", "ID", "PQRQRS", "PQRHeaderID")> _
        Public ReadOnly Property PQRQRSs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pQRQRSs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRQRS), "PQRHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRQRS), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pQRQRSs = DoLoadArray(GetType(PQRQRS).ToString, criterias)
                    End If

                    Return Me._pQRQRSs

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

#Region "Custom Method"

        Private _Interval As TimeSpan
        Public ReadOnly Property Interval() As TimeSpan
            Get
                If _Interval.Ticks <> TimeSpan.Zero.Ticks Then
                    Return _Interval
                End If
                Dim result As TimeSpan
                Dim endDate As DateTime
                If ValidationTime.Year < 1900 Then
                    Return New TimeSpan(0)
                Else
                    If RealeseTime.Year < 1900 Then
                        endDate = DateTime.Now
                    Else
                        endDate = RealeseTime
                    End If
                End If
                result = endDate.Subtract(ValidationTime)

                'Handle Saturday and Sunday

                'Dim refDate As DateTime = ValidationTime
                'Dim weekendCounter As Integer = 0
                'Do While refDate < endDate
                '    refDate.AddDays(1)
                '    If refDate.DayOfWeek = DayOfWeek.Saturday Or refDate.DayOfWeek = DayOfWeek.Sunday Then
                '        weekendCounter += 1
                '    End If
                'Loop


                'Handle Holiday
                Dim criterias As New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Greater, ValidationTime))
                criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Lesser, endDate))

                Dim agg As Aggregate = New Aggregate(GetType(NationalHoliday), "ID", AggregateType.Count)

                Dim arlHoliday As ArrayList = DoLoadArray(GetType(NationalHoliday).ToString, criterias)
                Dim holidayCounter As Integer = arlHoliday.Count

                'For Each itemHoliday As NationalHoliday In arlHoliday
                '    If itemHoliday.HolidayDateTime.DayOfWeek = DayOfWeek.Saturday Or itemHoliday.HolidayDateTime.DayOfWeek = DayOfWeek.Sunday Then
                '        holidayCounter -= 1
                '    End If
                'Next

                If result.Days - holidayCounter >= 0 Then
                    result = New TimeSpan(result.Days - holidayCounter, result.Hours, result.Minutes, result.Seconds)
                Else
                    result = New TimeSpan(0, result.Hours, result.Minutes, result.Seconds)
                End If

                'Handle 8 Hours work
                If ValidationTime.Hour < 12 And endDate.Hour > 13 Then
                    result = New TimeSpan(result.Days, result.Hours - 1, result.Minutes, result.Seconds)
                End If

                If result.Hours >= 8 Then
                    result = New TimeSpan(result.Days, 8, 0, result.Seconds)
                End If

                _Interval = result
                Return result

            End Get
        End Property

#End Region

    End Class
End Namespace

