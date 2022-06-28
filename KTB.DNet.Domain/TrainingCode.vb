#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrainingCode Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 10:41:55 AM
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
    <Serializable(), TableInfo("TrainingCode")> _
    Public Class TrainingCode
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
        Private _titleOfTraining As String = String.Empty
        Private _kindOfTraining As String = String.Empty
        Private _schedule As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _trainer1 As String = String.Empty
        Private _trainer2 As String = String.Empty
        Private _trainer3 As String = String.Empty
        Private _targetMember As Integer
        Private _place As String = String.Empty
        Private _prerequisite As String = String.Empty
        Private _periodStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _trainingHistoricals As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _trainingConfirmations As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _trainingSaless As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _trainingAnnoucements As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("TitleOfTraining", "'{0}'")> _
        Public Property TitleOfTraining() As String
            Get
                Return _titleOfTraining
            End Get
            Set(ByVal value As String)
                _titleOfTraining = value
            End Set
        End Property


        <ColumnInfo("KindOfTraining", "'{0}'")> _
        Public Property KindOfTraining() As String
            Get
                Return _kindOfTraining
            End Get
            Set(ByVal value As String)
                _kindOfTraining = value
            End Set
        End Property


        <ColumnInfo("Schedule", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property Schedule() As DateTime
            Get
                Return _schedule
            End Get
            Set(ByVal value As DateTime)
                _schedule = value
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


        <ColumnInfo("TargetMember", "{0}")> _
        Public Property TargetMember() As Integer
            Get
                Return _targetMember
            End Get
            Set(ByVal value As Integer)
                _targetMember = value
            End Set
        End Property


        <ColumnInfo("Place", "'{0}'")> _
        Public Property Place() As String
            Get
                Return _place
            End Get
            Set(ByVal value As String)
                _place = value
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


        <ColumnInfo("PeriodStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodStart() As DateTime
            Get
                Return _periodStart
            End Get
            Set(ByVal value As DateTime)
                _periodStart = value
            End Set
        End Property


        <ColumnInfo("PeriodEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodEnd() As DateTime
            Get
                Return _periodEnd
            End Get
            Set(ByVal value As DateTime)
                _periodEnd = value
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



        <RelationInfo("TrainingCode", "ID", "TrainingHistorical", "TrainingCodeId")> _
        Public ReadOnly Property TrainingHistoricals() As System.Collections.ArrayList
            Get
                Try
                    If (Me._trainingHistoricals.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrainingHistorical), "TrainingCode", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrainingHistorical), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._trainingHistoricals = DoLoadArray(GetType(TrainingHistorical).ToString, criterias)
                    End If

                    Return Me._trainingHistoricals

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("TrainingCode", "ID", "TrainingConfirmation", "TrainingCodeId")> _
        Public ReadOnly Property TrainingConfirmations() As System.Collections.ArrayList
            Get
                Try
                    If (Me._trainingConfirmations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrainingConfirmation), "TrainingCode", Me.ID)
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

        <RelationInfo("TrainingCode", "ID", "TrainingSales", "TrainingCodeId")> _
        Public ReadOnly Property TrainingSaless() As System.Collections.ArrayList
            Get
                Try
                    If (Me._trainingSaless.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrainingSales), "TrainingCode", Me.ID)
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

        <RelationInfo("TrainingCode", "ID", "TrainingAnnoucement", "TrainingCodeId")> _
        Public ReadOnly Property TrainingAnnoucements() As System.Collections.ArrayList
            Get
                Try
                    If (Me._trainingAnnoucements.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrainingAnnoucement), "TrainingCode", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrainingAnnoucement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._trainingAnnoucements = DoLoadArray(GetType(TrainingAnnoucement).ToString, criterias)
                    End If

                    Return Me._trainingAnnoucements

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

