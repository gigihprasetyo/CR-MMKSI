#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrainingAnnoucement Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 10:45:01 AM
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
    <Serializable(), TableInfo("TrainingAnnoucement")> _
    Public Class TrainingAnnoucement
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
        Private _announcementNumber As String = String.Empty
        Private _announcementDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _announcementLetter As String = String.Empty
        Private _materialTraining As String = String.Empty
        Private _note As String = String.Empty
        Private _periodStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _trainingCode As TrainingCode



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


        <ColumnInfo("AnnouncementNumber", "'{0}'")> _
        Public Property AnnouncementNumber() As String
            Get
                Return _announcementNumber
            End Get
            Set(ByVal value As String)
                _announcementNumber = value
            End Set
        End Property


        <ColumnInfo("AnnouncementDate", "'{0:yyyy/MM/dd}'")> _
        Public Property AnnouncementDate() As DateTime
            Get
                Return _announcementDate
            End Get
            Set(ByVal value As DateTime)
                _announcementDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("AnnouncementLetter", "'{0}'")> _
        Public Property AnnouncementLetter() As String
            Get
                Return _announcementLetter
            End Get
            Set(ByVal value As String)
                _announcementLetter = value
            End Set
        End Property


        <ColumnInfo("MaterialTraining", "'{0}'")> _
        Public Property MaterialTraining() As String
            Get
                Return _materialTraining
            End Get
            Set(ByVal value As String)
                _materialTraining = value
            End Set
        End Property


        <ColumnInfo("Note", "'{0}'")> _
        Public Property Note() As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _note = value
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


        <ColumnInfo("TrainingCodeId", "{0}"), _
        RelationInfo("TrainingCode", "ID", "TrainingAnnoucement", "TrainingCodeId")> _
        Public Property TrainingCode() As TrainingCode
            Get
                Try
                    If Not isnothing(Me._trainingCode) AndAlso (Not Me._trainingCode.IsLoaded) Then

                        Me._trainingCode = CType(DoLoad(GetType(TrainingCode).ToString(), _trainingCode.ID), TrainingCode)
                        Me._trainingCode.MarkLoaded()

                    End If

                    Return Me._trainingCode

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrainingCode)

                Me._trainingCode = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trainingCode.MarkLoaded()
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

#End Region

    End Class
End Namespace

