#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AlertMaster Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/26/2007 - 4:54:01 PM
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
    <Serializable(), TableInfo("AlertMaster")> _
    Public Class AlertMaster
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
        Private _name As String = String.Empty
        Private _alertType As Short
        Private _announcementAlertType As Short
        Private _desc As String = String.Empty
        Private _fontEffect As Short
        Private _dateValidFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dateValidTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _uploadedBy As String = String.Empty
        Private _isIncludeHoliday As Boolean
        Private _timeStartFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _timeStartTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isViaDashboard As Boolean
        Private _viaDashboardFrequency As Integer
        Private _viaDashboardFreqType As String = String.Empty
        Private _isViaAlertBox As Boolean
        Private _viaAlertBoxFrequency As Integer
        Private _viaAlertBoxFreqType As String = String.Empty
        Private _isViaSMS As Boolean
        Private _viaSMSFrequency As Integer
        Private _viaSMSFreqType As String = String.Empty
        Private _isViaEmail As Boolean
        Private _viaEmailFrequency As Integer
        Private _viaEmailFreqType As String = String.Empty
        Private _nextRunForDashboard As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _nextRunForAlertBox As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _nextRunForSMS As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _nextRunForEmail As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _alertModul As AlertModul

        Private _alertSounds As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _alertGroups As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _alertStatuss As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _alertMasterMedias As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _userAlerts As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        <ColumnInfo("AlertType", "{0}")> _
        Public Property AlertType() As Short
            Get
                Return _alertType
            End Get
            Set(ByVal value As Short)
                _alertType = value
            End Set
        End Property


        <ColumnInfo("AnnouncementAlertType", "{0}")> _
        Public Property AnnouncementAlertType() As Short
            Get
                Return _announcementAlertType
            End Get
            Set(ByVal value As Short)
                _announcementAlertType = value
            End Set
        End Property


        <ColumnInfo("Desc", "'{0}'")> _
        Public Property Desc() As String
            Get
                Return _desc
            End Get
            Set(ByVal value As String)
                _desc = value
            End Set
        End Property


        <ColumnInfo("FontEffect", "{0}")> _
        Public Property FontEffect() As Short
            Get
                Return _fontEffect
            End Get
            Set(ByVal value As Short)
                _fontEffect = value
            End Set
        End Property


        <ColumnInfo("DateValidFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateValidFrom() As DateTime
            Get
                Return _dateValidFrom
            End Get
            Set(ByVal value As DateTime)
                _dateValidFrom = value
            End Set
        End Property


        <ColumnInfo("DateValidTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateValidTo() As DateTime
            Get
                Return _dateValidTo
            End Get
            Set(ByVal value As DateTime)
                _dateValidTo = value
            End Set
        End Property


        <ColumnInfo("UploadedBy", "'{0}'")> _
        Public Property UploadedBy() As String
            Get
                Return _uploadedBy
            End Get
            Set(ByVal value As String)
                _uploadedBy = value
            End Set
        End Property


        <ColumnInfo("IsIncludeHoliday", "{0}")> _
        Public Property IsIncludeHoliday() As Boolean
            Get
                Return _isIncludeHoliday
            End Get
            Set(ByVal value As Boolean)
                _isIncludeHoliday = value
            End Set
        End Property


        <ColumnInfo("TimeStartFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TimeStartFrom() As DateTime
            Get
                Return _timeStartFrom
            End Get
            Set(ByVal value As DateTime)
                _timeStartFrom = value
            End Set
        End Property


        <ColumnInfo("TimeStartTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TimeStartTo() As DateTime
            Get
                Return _timeStartTo
            End Get
            Set(ByVal value As DateTime)
                _timeStartTo = value
            End Set
        End Property


        <ColumnInfo("IsViaDashboard", "{0}")> _
        Public Property IsViaDashboard() As Boolean
            Get
                Return _isViaDashboard
            End Get
            Set(ByVal value As Boolean)
                _isViaDashboard = value
            End Set
        End Property


        <ColumnInfo("ViaDashboardFrequency", "{0}")> _
        Public Property ViaDashboardFrequency() As Integer
            Get
                Return _viaDashboardFrequency
            End Get
            Set(ByVal value As Integer)
                _viaDashboardFrequency = value
            End Set
        End Property


        <ColumnInfo("ViaDashboardFreqType", "'{0}'")> _
        Public Property ViaDashboardFreqType() As String
            Get
                Return _viaDashboardFreqType
            End Get
            Set(ByVal value As String)
                _viaDashboardFreqType = value
            End Set
        End Property


        <ColumnInfo("IsViaAlertBox", "{0}")> _
        Public Property IsViaAlertBox() As Boolean
            Get
                Return _isViaAlertBox
            End Get
            Set(ByVal value As Boolean)
                _isViaAlertBox = value
            End Set
        End Property


        <ColumnInfo("ViaAlertBoxFrequency", "{0}")> _
        Public Property ViaAlertBoxFrequency() As Integer
            Get
                Return _viaAlertBoxFrequency
            End Get
            Set(ByVal value As Integer)
                _viaAlertBoxFrequency = value
            End Set
        End Property


        <ColumnInfo("ViaAlertBoxFreqType", "'{0}'")> _
        Public Property ViaAlertBoxFreqType() As String
            Get
                Return _viaAlertBoxFreqType
            End Get
            Set(ByVal value As String)
                _viaAlertBoxFreqType = value
            End Set
        End Property


        <ColumnInfo("IsViaSMS", "{0}")> _
        Public Property IsViaSMS() As Boolean
            Get
                Return _isViaSMS
            End Get
            Set(ByVal value As Boolean)
                _isViaSMS = value
            End Set
        End Property


        <ColumnInfo("ViaSMSFrequency", "{0}")> _
        Public Property ViaSMSFrequency() As Integer
            Get
                Return _viaSMSFrequency
            End Get
            Set(ByVal value As Integer)
                _viaSMSFrequency = value
            End Set
        End Property


        <ColumnInfo("ViaSMSFreqType", "'{0}'")> _
        Public Property ViaSMSFreqType() As String
            Get
                Return _viaSMSFreqType
            End Get
            Set(ByVal value As String)
                _viaSMSFreqType = value
            End Set
        End Property


        <ColumnInfo("IsViaEmail", "{0}")> _
        Public Property IsViaEmail() As Boolean
            Get
                Return _isViaEmail
            End Get
            Set(ByVal value As Boolean)
                _isViaEmail = value
            End Set
        End Property


        <ColumnInfo("ViaEmailFrequency", "{0}")> _
        Public Property ViaEmailFrequency() As Integer
            Get
                Return _viaEmailFrequency
            End Get
            Set(ByVal value As Integer)
                _viaEmailFrequency = value
            End Set
        End Property


        <ColumnInfo("ViaEmailFreqType", "'{0}'")> _
        Public Property ViaEmailFreqType() As String
            Get
                Return _viaEmailFreqType
            End Get
            Set(ByVal value As String)
                _viaEmailFreqType = value
            End Set
        End Property


        <ColumnInfo("NextRunForDashboard", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property NextRunForDashboard() As DateTime
            Get
                Return _nextRunForDashboard
            End Get
            Set(ByVal value As DateTime)
                _nextRunForDashboard = value
            End Set
        End Property


        <ColumnInfo("NextRunForAlertBox", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property NextRunForAlertBox() As DateTime
            Get
                Return _nextRunForAlertBox
            End Get
            Set(ByVal value As DateTime)
                _nextRunForAlertBox = value
            End Set
        End Property


        <ColumnInfo("NextRunForSMS", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property NextRunForSMS() As DateTime
            Get
                Return _nextRunForSMS
            End Get
            Set(ByVal value As DateTime)
                _nextRunForSMS = value
            End Set
        End Property


        <ColumnInfo("NextRunForEmail", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property NextRunForEmail() As DateTime
            Get
                Return _nextRunForEmail
            End Get
            Set(ByVal value As DateTime)
                _nextRunForEmail = value
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


        <ColumnInfo("AlertModulID", "{0}"), _
        RelationInfo("AlertModul", "ID", "AlertMaster", "AlertModulID")> _
        Public Property AlertModul() As AlertModul
            Get
                Try
                    If Not isnothing(Me._alertModul) AndAlso (Not Me._alertModul.IsLoaded) Then

                        Me._alertModul = CType(DoLoad(GetType(AlertModul).ToString(), _alertModul.ID), AlertModul)
                        Me._alertModul.MarkLoaded()

                    End If

                    Return Me._alertModul

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AlertModul)

                Me._alertModul = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._alertModul.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("AlertMaster", "ID", "AlertSound", "AlertMasterID")> _
        Public ReadOnly Property AlertSounds() As System.Collections.ArrayList
            Get
                Try
                    If (Me._alertSounds.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AlertSound), "AlertMaster", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AlertSound), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._alertSounds = DoLoadArray(GetType(AlertSound).ToString, criterias)
                    End If

                    Return Me._alertSounds

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("AlertMaster", "ID", "AlertGroup", "AlertMasterID")> _
        Public ReadOnly Property AlertGroups() As System.Collections.ArrayList
            Get
                Try
                    If (Me._alertGroups.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AlertGroup), "AlertMaster", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AlertGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._alertGroups = DoLoadArray(GetType(AlertGroup).ToString, criterias)
                    End If

                    Return Me._alertGroups

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("AlertMaster", "ID", "AlertStatus", "AlertMasterID")> _
        Public ReadOnly Property AlertStatuss() As System.Collections.ArrayList
            Get
                Try
                    If (Me._alertStatuss.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AlertStatus), "AlertMaster", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AlertStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._alertStatuss = DoLoadArray(GetType(AlertStatus).ToString, criterias)
                    End If

                    Return Me._alertStatuss

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        

        <RelationInfo("AlertMaster", "ID", "UserAlert", "AlertMasterID")> _
        Public ReadOnly Property UserAlerts() As System.Collections.ArrayList
            Get
                Try
                    If (Me._userAlerts.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(UserAlert), "AlertMaster", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(UserAlert), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._userAlerts = DoLoadArray(GetType(UserAlert).ToString, criterias)
                    End If

                    Return Me._userAlerts

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

