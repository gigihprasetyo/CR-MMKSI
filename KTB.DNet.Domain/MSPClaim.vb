
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPClaim Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/22/2018 - 1:22:36 PM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework

#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("MSPClaim")> _
    Public Class MSPClaim
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
        Private _claimNumber As String = String.Empty
        Private _claimDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Short

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _objDealer As Dealer
        Private _objPMHeader As PMHeader
        Private _objMSPRegHistory As MSPRegistrationHistory

        ''' <summary>
        ''' MSP Pahse 2
        ''' take out MSP from PM
        ''' </summary>
        ''' <remarks></remarks>
        Private _chassisMaster As ChassisMaster
        Private _standKM As Integer
        Private _pMKindID As Integer
        Private _visitType As String = String.Empty
        Private _serviceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _releaseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _remarks As String = String.Empty

        Private _pmKind As PMKind
        Private _PMKindCode As String = String.Empty
        Private _PMKindDesc As String = String.Empty

#End Region

#Region "Public Properties"

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "MSPClaim", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._objDealer) AndAlso (Not Me._objDealer.IsLoaded) Then
                        Me._objDealer = CType(DoLoad(GetType(Dealer).ToString(), _objDealer.ID), Dealer)
                        Me._objDealer.MarkLoaded()
                    End If

                    Return Me._objDealer
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._objDealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objDealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PMHeaderID", "{0}"), _
        RelationInfo("PMHeader", "ID", "MSPClaim", "PMHeaderID")> _
        Public Property PMHeader() As PMHeader
            Get
                Try
                    If Not IsNothing(Me._objPMHeader) AndAlso (Not Me._objPMHeader.IsLoaded) Then
                        Me._objPMHeader = CType(DoLoad(GetType(PMHeader).ToString(), _objPMHeader.ID), PMHeader)
                        Me._objPMHeader.MarkLoaded()
                    End If

                    Return Me._objPMHeader
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As PMHeader)

                Me._objPMHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objPMHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("MSPRegistrationHistoryID", "{0}"), _
        RelationInfo("MSPRegistrationHistory", "ID", "MSPClaim", "MSPRegistrationHistoryID")> _
        Public Property MSPRegistrationHistory() As MSPRegistrationHistory
            Get
                Try
                    If Not IsNothing(Me._objMSPRegHistory) AndAlso (Not Me._objMSPRegHistory.IsLoaded) Then
                        Me._objMSPRegHistory = CType(DoLoad(GetType(MSPRegistrationHistory).ToString(), _objMSPRegHistory.ID), MSPRegistrationHistory)
                        Me._objMSPRegHistory.MarkLoaded()
                    End If

                    Return Me._objMSPRegHistory
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPRegistrationHistory)

                Me._objMSPRegHistory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objMSPRegHistory.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
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


        <ColumnInfo("ClaimDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ClaimDate As DateTime
            Get
                Return _claimDate
            End Get
            Set(ByVal value As DateTime)
                _claimDate = New DateTime(value.Year, value.Month, value.Day)
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



        <ColumnInfo("ChassisNumberID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "MSPCLaim", "ChassisNumberID")> _
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


        <ColumnInfo("PMKindID", "{0}"), _
     RelationInfo("PMKind", "ID", "MSPClaim", "PMKindID")> _
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

        <ColumnInfo("VisitType", "'{0}'")> _
        Public Property VisitType() As String
            Get
                Return _visitType
            End Get
            Set(ByVal value As String)
                _visitType = value
            End Set
        End Property

        <ColumnInfo("Remarks", "'{0}'")> _
        Public Property Remarks() As String
            Get
                Return _remarks
            End Get
            Set(ByVal value As String)
                _remarks = value
            End Set
        End Property

#End Region

#Region "Custom Property"
        Public ReadOnly Property PMKindCode() As String
            Get
                LoadPMKind()
                Return _PMKindCode
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
        Private Sub LoadPMKind()
            Dim m_PMKind As IMapper = MapperFactory.GetInstance().GetMapper(GetType(PMKind).ToString)

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(PMKind), "KM", MatchType.GreaterOrEqual, StandKM))
            criterias.opAnd(New Criteria(GetType(PMKind), "ID", MatchType.Exact, PMHeader.ID))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(PMKind), "ID", Sort.SortDirection.ASC))

            Dim objPMKindColl As ArrayList = m_PMKind.RetrieveByCriteria(criterias, sortColl)

            If objPMKindColl.Count > 0 Then
                Dim objPMKind As PMKind = objPMKindColl(0)
                _pMKindID = objPMKind.ID
                _PMKindCode = objPMKind.KindCode
                _PMKindDesc = objPMKind.KindDescription
            Else
                _pMKindID = 0
                _PMKindCode = String.Empty
                _PMKindDesc = String.Empty

            End If
        End Sub
#End Region

    End Class
End Namespace

