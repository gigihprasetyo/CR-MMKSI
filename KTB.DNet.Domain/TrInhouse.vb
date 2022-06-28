
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrInhouse Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 6/25/2009 - 8:52:30 AM
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
    <Serializable(), TableInfo("TrInhouse")> _
    Public Class TrInhouse
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
        Private _code As String = String.Empty
        Private _organizationID As Integer
        Private _reportDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _approvedBy1 As String = String.Empty
        Private _jobPosition1 As String = String.Empty
        Private _approvedBy2 As String = String.Empty
        Private _jobPosition2 As String = String.Empty
        Private _approvedBy3 As String = String.Empty
        Private _jobPosition3 As String = String.Empty
        Private _uploadedReportFile As String = String.Empty
        Private _uploadedAttendanceFile As String = String.Empty
        Private _uploadedEvaluationFile As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _trInhouseDetails As ArrayList = New ArrayList
        Private _trInhouseInformations As ArrayList = New ArrayList
        Private _trInhouseClasss As ArrayList = New ArrayList
        Private _trInhouseMembers As ArrayList = New ArrayList


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
        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("OrganizationID", "{0}")> _
        Public Property OrganizationID() As Integer
            Get
                Return _organizationID
            End Get
            Set(ByVal value As Integer)
                _organizationID = value
            End Set
        End Property


        <ColumnInfo("ReportDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReportDate() As DateTime
            Get
                Return _reportDate
            End Get
            Set(ByVal value As DateTime)
                _reportDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ApprovedBy1", "'{0}'")> _
        Public Property ApprovedBy1() As String
            Get
                Return _approvedBy1
            End Get
            Set(ByVal value As String)
                _approvedBy1 = value
            End Set
        End Property


        <ColumnInfo("JobPosition1", "'{0}'")> _
        Public Property JobPosition1() As String
            Get
                Return _jobPosition1
            End Get
            Set(ByVal value As String)
                _jobPosition1 = value
            End Set
        End Property


        <ColumnInfo("ApprovedBy2", "'{0}'")> _
        Public Property ApprovedBy2() As String
            Get
                Return _approvedBy2
            End Get
            Set(ByVal value As String)
                _approvedBy2 = value
            End Set
        End Property


        <ColumnInfo("JobPosition2", "'{0}'")> _
        Public Property JobPosition2() As String
            Get
                Return _jobPosition2
            End Get
            Set(ByVal value As String)
                _jobPosition2 = value
            End Set
        End Property


        <ColumnInfo("ApprovedBy3", "'{0}'")> _
        Public Property ApprovedBy3() As String
            Get
                Return _approvedBy3
            End Get
            Set(ByVal value As String)
                _approvedBy3 = value
            End Set
        End Property


        <ColumnInfo("JobPosition3", "'{0}'")> _
        Public Property JobPosition3() As String
            Get
                Return _jobPosition3
            End Get
            Set(ByVal value As String)
                _jobPosition3 = value
            End Set
        End Property

        <ColumnInfo("UploadedReportFile", "'{0}'")> _
                Public Property UploadedReportFile() As String
            Get
                Return _uploadedReportFile
            End Get
            Set(ByVal value As String)
                _uploadedReportFile = value
            End Set
        End Property

        <ColumnInfo("UploadedAttendanceFile", "'{0}'")> _
                Public Property UploadedAttendanceFile() As String
            Get
                Return _uploadedAttendanceFile
            End Get
            Set(ByVal value As String)
                _uploadedAttendanceFile = value
            End Set
        End Property

        <ColumnInfo("UploadedEvaluationFile", "'{0}'")> _
                Public Property UploadedEvaluationFile() As String
            Get
                Return _uploadedEvaluationFile
            End Get
            Set(ByVal value As String)
                _uploadedEvaluationFile = value
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

        <ColumnInfo("OrganizationID", "{0}"), _
                RelationInfo("Dealer", "ID", "TrInhouse", "OrganizationID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), Me._organizationID), Dealer)
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

        <ColumnInfo("ID", "{0}"), _
               RelationInfo("TrInhouseDetail", "InhouseID", "TrInhouse", "ID")> _
       Public ReadOnly Property TrInhouseDetails() As ArrayList
            Get
                Try
                    If Me._trInhouseDetails.Count < 1 Then
                        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrInhouseDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits.opAnd(New Criteria(GetType(TrInhouseDetail), "InhouseID", MatchType.Exact, Me._iD))
                        Me._trInhouseDetails = DoLoadArray(GetType(TrInhouseDetail).ToString, crits)

                    End If

                    Return Me._trInhouseDetails

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
               RelationInfo("TrInhouseInformation", "InhouseID", "TrInhouse", "ID")> _
       Public ReadOnly Property TrInhouseInformations() As ArrayList
            Get
                Try
                    If Me._trInhouseInformations.Count < 1 Then
                        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrInhouseInformation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits.opAnd(New Criteria(GetType(TrInhouseInformation), "InhouseID", MatchType.Exact, Me._iD))
                        Me._trInhouseInformations = DoLoadArray(GetType(TrInhouseInformation).ToString, crits)

                    End If

                    Return Me._trInhouseInformations

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
               RelationInfo("TrInhouseClass", "InhouseID", "TrInhouse", "ID")> _
       Public ReadOnly Property TrInhouseClasss() As ArrayList
            Get
                Try
                    If Me._trInhouseClasss.Count < 1 Then
                        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrInhouseClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits.opAnd(New Criteria(GetType(TrInhouseClass), "InhouseID", MatchType.Exact, Me._iD))
                        Me._trInhouseClasss = DoLoadArray(GetType(TrInhouseClass).ToString, crits)

                    End If

                    Return Me._trInhouseClasss

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
               RelationInfo("TrInhouseMember", "InhouseID", "TrInhouse", "ID")> _
       Public ReadOnly Property TrInhouseMembers() As ArrayList
            Get
                Try
                    If Me._trInhouseMembers.Count < 1 Then
                        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrInhouseMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits.opAnd(New Criteria(GetType(TrInhouseMember), "InhouseID", MatchType.Exact, Me._iD))
                        Me._trInhouseMembers = DoLoadArray(GetType(TrInhouseMember).ToString, crits)

                    End If

                    Return Me._trInhouseMembers

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

#End Region

    End Class
End Namespace

