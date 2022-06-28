#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PMHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/1/2007 - 4:42:22 PM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("PMHeader")> _
    Public Class PMHeader
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
        Private _standKM As Integer
        Private _serviceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _releaseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pMStatus As String = String.Empty
        Private _entryType As String = String.Empty
        Private _workOrderNumber As String = String.Empty
        Private _bookingNo As String = String.Empty
        Private _visitType As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _chassisMaster As ChassisMaster
        Private _dealer As Dealer
        Private _pMKind As PMKind
	Private _dealerBranch As DealerBranch

        Private _pMDetails As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _PMKindCode As String = String.Empty
        Private _PMKindDesc As String = String.Empty
        Private _PMKindID As Integer = 0

        'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122
        Private _remarks As String = String.Empty
        Private _MSPRegHistoryID As Integer = 0
        Private _isValidMSP As Boolean = True
        'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122

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

        <ColumnInfo("PMKindID", "{0}"), _
       RelationInfo("PMKind", "ID", "PMHeader", "PMKindID")> _
        Public Property PMKind As PMKind
            Get
                Try
                    If Not IsNothing(Me._pmKind) AndAlso (Not Me._pmKind.IsLoaded) Then

                        Me._pmKind = CType(DoLoad(GetType(PMKind).ToString(), _pmKind.ID), PMKind)
                        Me._pmKind.MarkLoaded()

                    End If

                    Return Me._pmKind

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PMKind)

                Me._pmKind = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pmKind.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("StandKM", "{0}")> _
        Public Property StandKM() As Integer
            Get
                Return _standKM
            End Get
            Set(ByVal value As Integer)
                _standKM = value
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


        <ColumnInfo("ReleaseDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReleaseDate() As DateTime
            Get
                Return _releaseDate
            End Get
            Set(ByVal value As DateTime)
                _releaseDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PMStatus", "'{0}'")> _
        Public Property PMStatus() As String
            Get
                Return _pMStatus
            End Get
            Set(ByVal value As String)
                _pMStatus = value
            End Set
        End Property


        <ColumnInfo("EntryType", "'{0}'")> _
        Public Property EntryType() As String
            Get
                Return _entryType
            End Get
            Set(ByVal value As String)
                _entryType = value
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


        <ColumnInfo("BookingNo", "'{0}'")> _
        Public Property BookingNo() As String
            Get
                Return _bookingNo
            End Get
            Set(ByVal value As String)
                _bookingNo = value
            End Set
        End Property


        <ColumnInfo("VisitType", "'{0}'")> _
        Public Property VisitType() As String
            Get
                Return _visitType
            End Get
            Set(ByVal value As String)
                _visitType = value
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


        <ColumnInfo("ChassisNumberID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "PMHeader", "ChassisNumberID")> _
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

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "PMHeader", "DealerID")> _
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
        RelationInfo("DealerBranch", "ID", "PMHeader", "DealerBranchID")> _
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

        <RelationInfo("PMHeader", "ID", "PMDetail", "PMID")> _
        Public ReadOnly Property PMDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pMDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PMDetail), "PMHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PMDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pMDetails = DoLoadArray(GetType(PMDetail).ToString, criterias)
                    End If

                    Return Me._pMDetails

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

#Region "Custom Properties"

        Private _DealerBranchCodeMsg As String = String.Empty

        Public ReadOnly Property PMKindCode() As String
            Get
                LoadPMKind()
                Return _PMKindCode
            End Get
        End Property

        Public ReadOnly Property PMKindDesc() As String
            Get
                LoadPMKind()
                Return _PMKindDesc
            End Get
        End Property

        Public ReadOnly Property PMKindID() As Integer
            Get
                LoadPMKind()
                Return _PMKindID
            End Get
        End Property

        Public Property DealerBranchCodeMsg() As String
            Get
                Return _DealerBranchCodeMsg
            End Get
            Set(ByVal value As String)
                _DealerBranchCodeMsg = value
            End Set
        End Property
#End Region

#Region "Custom Method"
        Private Sub LoadPMKind()
            Dim m_PMKind As IMapper = MapperFactory.GetInstance().GetMapper(GetType(PMKind).ToString)

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PMKind), "ID", MatchType.GreaterOrEqual, PMKind.ID))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(PMKind), "ID", Sort.SortDirection.ASC))

            Dim objPMKindColl As ArrayList = m_PMKind.RetrieveByCriteria(criterias, sortColl)

            If objPMKindColl.Count > 0 Then
                Dim objPMKind As PMKind = objPMKindColl(0)
                _PMKindID = objPMKind.ID
                _PMKindCode = objPMKind.KindCode
                _PMKindDesc = objPMKind.KindDescription
            Else
                _PMKindID = 0
                _PMKindCode = String.Empty
                _PMKindDesc = String.Empty

            End If
        End Sub

        'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122
        <ColumnInfo("Remarks", "'{0}'")> _
        Public Property Remarks() As String
            Get
                Return _remarks
            End Get
            Set(ByVal value As String)
                _remarks = value
            End Set
        End Property

        Public Property MSPRegistrationHistoryID() As Integer
            Get
                Return _MSPRegHistoryID
            End Get
            Set(ByVal value As Integer)
                _MSPRegHistoryID = value
            End Set
        End Property

        Public Property IsValidMSP() As Boolean
            Get
                Return _isValidMSP
            End Get
            Set(ByVal value As Boolean)
                _isValidMSP = value
            End Set
        End Property
        'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122

#End Region

    End Class
End Namespace

