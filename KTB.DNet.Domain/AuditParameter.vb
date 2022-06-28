#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AuditParameter Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2007 - 2:10:47 PM
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
    <Serializable(), TableInfo("AuditParameter")> _
    Public Class AuditParameter
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
        Private _period As Short
        Private _jukLakFile As String = String.Empty
        Private _assessmentItem As String = String.Empty
        Private _isRilis As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _auditSchedules As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _auditParameterPhotos As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Period", "{0}")> _
        Public Property Period() As Short
            Get
                Return _period
            End Get
            Set(ByVal value As Short)
                _period = value
            End Set
        End Property


        <ColumnInfo("JukLakFile", "'{0}'")> _
        Public Property JukLakFile() As String
            Get
                Return _jukLakFile
            End Get
            Set(ByVal value As String)
                _jukLakFile = value
            End Set
        End Property


        <ColumnInfo("AssessmentItem", "'{0}'")> _
        Public Property AssessmentItem() As String
            Get
                Return _assessmentItem
            End Get
            Set(ByVal value As String)
                _assessmentItem = value
            End Set
        End Property


        <ColumnInfo("IsRilis", "{0}")> _
        Public Property IsRilis() As Byte
            Get
                Return _isRilis
            End Get
            Set(ByVal value As Byte)
                _isRilis = value
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



        <RelationInfo("AuditParameter", "ID", "AuditSchedule", "AuditParameterID")> _
        Public ReadOnly Property AuditSchedules() As System.Collections.ArrayList
            Get
                Try
                    If (Me._auditSchedules.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AuditSchedule), "AuditParameter", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._auditSchedules = DoLoadArray(GetType(AuditSchedule).ToString, criterias)
                    End If

                    Return Me._auditSchedules

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("AuditParameter", "ID", "AuditParameterPhoto", "AuditParameterID")> _
        Public ReadOnly Property AuditParameterPhotos() As System.Collections.ArrayList
            Get
                Try
                    If (Me._auditParameterPhotos.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AuditParameterPhoto), "AuditParameter", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AuditParameterPhoto), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._auditParameterPhotos = DoLoadArray(GetType(AuditParameterPhoto).ToString, criterias)
                    End If

                    Return Me._auditParameterPhotos

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

