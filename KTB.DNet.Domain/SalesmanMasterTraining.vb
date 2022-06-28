#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanMasterTraining Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/1/2007 - 4:35:48 PM
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
    <Serializable(), TableInfo("SalesmanMasterTraining")> _
    Public Class SalesmanMasterTraining
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
        Private _trainingCode As String = String.Empty
        Private _trainingTitle As String = String.Empty
        Private _startingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _endDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _trainer1 As String = String.Empty
        Private _trainer2 As String = String.Empty
        Private _trainer3 As String = String.Empty
        Private _attendanceTarget As Short
        Private _trainingPlace As String = String.Empty
        Private _prerequisite As String = String.Empty
        Private _announcementContent As String = String.Empty
        Private _isReleased As Short
        Private _announcementFileName As String = String.Empty
        Private _materialFileName As String = String.Empty
        Private _registerStartingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _registerEndDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _salesmanTrainingType As SalesmanTrainingType

        Private _salesmanTrainingParticipants As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("TrainingCode", "'{0}'")> _
        Public Property TrainingCode() As String
            Get
                Return _trainingCode
            End Get
            Set(ByVal value As String)
                _trainingCode = value
            End Set
        End Property


        <ColumnInfo("TrainingTitle", "'{0}'")> _
        Public Property TrainingTitle() As String
            Get
                Return _trainingTitle
            End Get
            Set(ByVal value As String)
                _trainingTitle = value
            End Set
        End Property


        <ColumnInfo("StartingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartingDate() As DateTime
            Get
                Return _startingDate
            End Get
            Set(ByVal value As DateTime)
                _startingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("EndDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EndDate() As DateTime
            Get
                Return _endDate
            End Get
            Set(ByVal value As DateTime)
                _endDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Trainer1", "'{0}'")> _
        Public Property Trainer1() As String
            Get
                Return _trainer1
            End Get
            Set(ByVal value As String)
                _trainer1 = value
            End Set
        End Property


        <ColumnInfo("Trainer2", "'{0}'")> _
        Public Property Trainer2() As String
            Get
                Return _trainer2
            End Get
            Set(ByVal value As String)
                _trainer2 = value
            End Set
        End Property


        <ColumnInfo("Trainer3", "'{0}'")> _
        Public Property Trainer3() As String
            Get
                Return _trainer3
            End Get
            Set(ByVal value As String)
                _trainer3 = value
            End Set
        End Property


        <ColumnInfo("AttendanceTarget", "{0}")> _
        Public Property AttendanceTarget() As Short
            Get
                Return _attendanceTarget
            End Get
            Set(ByVal value As Short)
                _attendanceTarget = value
            End Set
        End Property


        <ColumnInfo("TrainingPlace", "'{0}'")> _
        Public Property TrainingPlace() As String
            Get
                Return _trainingPlace
            End Get
            Set(ByVal value As String)
                _trainingPlace = value
            End Set
        End Property


        <ColumnInfo("Prerequisite", "'{0}'")> _
        Public Property Prerequisite() As String
            Get
                Return _prerequisite
            End Get
            Set(ByVal value As String)
                _prerequisite = value
            End Set
        End Property


        <ColumnInfo("AnnouncementContent", "'{0}'")> _
        Public Property AnnouncementContent() As String
            Get
                Return _announcementContent
            End Get
            Set(ByVal value As String)
                _announcementContent = value
            End Set
        End Property


        <ColumnInfo("IsReleased", "{0}")> _
        Public Property IsReleased() As Short
            Get
                Return _isReleased
            End Get
            Set(ByVal value As Short)
                _isReleased = value
            End Set
        End Property


        <ColumnInfo("AnnouncementFileName", "'{0}'")> _
        Public Property AnnouncementFileName() As String
            Get
                Return _announcementFileName
            End Get
            Set(ByVal value As String)
                _announcementFileName = value
            End Set
        End Property


        <ColumnInfo("MaterialFileName", "'{0}'")> _
        Public Property MaterialFileName() As String
            Get
                Return _materialFileName
            End Get
            Set(ByVal value As String)
                _materialFileName = value
            End Set
        End Property

        <ColumnInfo("RegisterStartingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RegisterStartingDate() As DateTime
            Get
                Return _registerStartingDate
            End Get
            Set(ByVal value As DateTime)
                _registerStartingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("RegisterEndDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RegisterEndDate() As DateTime
            Get
                Return _registerEndDate
            End Get
            Set(ByVal value As DateTime)
                _registerEndDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("SalesmanTrainingTypeID", "{0}"), _
        RelationInfo("SalesmanTrainingType", "ID", "SalesmanMasterTraining", "SalesmanTrainingTypeID")> _
        Public Property SalesmanTrainingType() As SalesmanTrainingType
            Get
                Try
                    If Not IsNothing(Me._salesmanTrainingType) AndAlso (Not Me._salesmanTrainingType.IsLoaded) Then

                        Me._salesmanTrainingType = CType(DoLoad(GetType(SalesmanTrainingType).ToString(), _salesmanTrainingType.ID), SalesmanTrainingType)
                        Me._salesmanTrainingType.MarkLoaded()

                    End If

                    Return Me._salesmanTrainingType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanTrainingType)

                Me._salesmanTrainingType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanTrainingType.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("SalesmanMasterTraining", "ID", "SalesmanTrainingParticipant", "SalesmanMasterTrainingID")> _
        Public ReadOnly Property SalesmanTrainingParticipants() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanTrainingParticipants.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanMasterTraining", Me.ID)
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

