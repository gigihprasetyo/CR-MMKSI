#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : JobPosition Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/5/2007 - 4:44:23 PM
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
    <Serializable(), TableInfo("JobPosition")> _
    Public Class JobPosition
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
        Private _description As String = String.Empty
        Private _category As Integer = 0
        Private _salesTarget As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Short
        Private _isNeedSuperior As Boolean
        Private _userInfos As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesmanHeaders As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _jobPositionToMenu As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _level As Integer

#End Region

#Region "Public Properties"

        '<ColumnInfo("Status", "{0}")> _
        'Public Property Status() As Short
        '    Get
        '        Return _status
        '    End Get
        '    Set(ByVal value As Short)
        '        _status = value
        '    End Set
        'End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        '<ColumnInfo("IsNeedSuperior", "{0}")> _
        'Public Property IsNeedSuperior() As Boolean
        '    Get
        '        Return _isNeedSuperior
        '    End Get
        '    Set(ByVal value As Boolean)
        '        _isNeedSuperior = value
        '    End Set
        'End Property

        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
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
        <ColumnInfo("Category", "{0}")> _
       Public Property Category() As Integer
            Get
                Return _category
            End Get
            Set(ByVal value As Integer)
                _category = value
            End Set
        End Property

        <ColumnInfo("SalesTarget", "{0}")> _
        Public Property SalesTarget() As Integer
            Get
                Return _salesTarget
            End Get
            Set(ByVal value As Integer)
                _salesTarget = value
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



        <RelationInfo("JobPosition", "ID", "UserInfo", "JobPositionID")> _
        Public ReadOnly Property UserInfos() As System.Collections.ArrayList
            Get
                Try
                    If (Me._userInfos.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(UserInfo), "JobPosition", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._userInfos = DoLoadArray(GetType(UserInfo).ToString, criterias)
                    End If

                    Return Me._userInfos

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("JobPosition", "ID", "SalesmanHeader", "JobPositionId_Main")> _
        Public ReadOnly Property SalesmanHeaders() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanHeaders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanHeader), "JobPosition", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanHeaders = DoLoadArray(GetType(SalesmanHeader).ToString, criterias)
                    End If

                    Return Me._salesmanHeaders

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("JobPosition", "ID", "JobPositionToMenu", "JobPositionID")> _
        Public ReadOnly Property JobPositionToMenu() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanHeaders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(JobPositionToMenu), "JobPositionID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(JobPositionToMenu), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._jobPositionToMenu = DoLoadArray(GetType(JobPositionToMenu).ToString, criterias)
                    End If

                    Return Me._jobPositionToMenu

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        '<ColumnInfo("Level", "{0}")> _
        'Public Property Level() As Integer
        '    Get
        '        Return _level
        '    End Get
        '    Set(ByVal value As Integer)
        '        _level = value
        '    End Set
        'End Property

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

