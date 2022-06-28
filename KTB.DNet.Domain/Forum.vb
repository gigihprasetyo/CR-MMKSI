#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Forum Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 10:16:48 AM
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
Imports KTB.DNet.DataMapper.Framework
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("Forum")> _
    Public Class Forum
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
        Private _title As String = String.Empty
        Private _description As String = String.Empty
        Private _type As Byte
        Private _status As Byte
        Private _userEntry As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _forumCategory As ForumCategory

        Private _forumTopics As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _forumMembers As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Title", "'{0}'")> _
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
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


        <ColumnInfo("Type", "{0}")> _
        Public Property Type() As Byte
            Get
                Return _type
            End Get
            Set(ByVal value As Byte)
                _type = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
            End Set
        End Property

        <ColumnInfo("UserEntry", "'{0}'")> _
        Public Property UserEntry() As String
            Get
                Return _userEntry
            End Get
            Set(ByVal value As String)
                _userEntry = value
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


        <ColumnInfo("ForumCategoryID", "{0}"), _
        RelationInfo("ForumCategory", "ID", "Forum", "ForumCategoryID")> _
        Public Property ForumCategory() As ForumCategory
            Get
                Try
                    If Not IsNothing(Me._forumCategory) AndAlso (Not Me._forumCategory.IsLoaded) Then

                        Me._forumCategory = CType(DoLoad(GetType(ForumCategory).ToString(), _forumCategory.ID), ForumCategory)
                        Me._forumCategory.MarkLoaded()

                    End If

                    Return Me._forumCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ForumCategory)

                Me._forumCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._forumCategory.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("Forum", "ID", "ForumTopic", "ForumID")> _
        Public ReadOnly Property ForumTopics() As System.Collections.ArrayList
            Get
                Try
                    If (Me._forumTopics.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ForumTopic), "Forum", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ForumTopic), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._forumTopics = DoLoadArray(GetType(ForumTopic).ToString, criterias)
                    End If

                    Return Me._forumTopics

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Forum", "ID", "ForumMember", "ForumID")> _
        Public ReadOnly Property ForumMembers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._forumMembers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ForumMember), "Forum", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ForumMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._forumMembers = DoLoadArray(GetType(ForumMember).ToString, criterias)
                    End If

                    Return Me._forumMembers

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
        Public ReadOnly Property TotalTopik() As Integer
            Get
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumTopic), "Forum.ID", MatchType.Exact, ID))
                Dim m_ForumTopicMapper As IMapper
                m_ForumTopicMapper = MapperFactory.GetInstance.GetMapper(GetType(ForumTopic).ToString)
                Dim ForumTopicList As ArrayList = m_ForumTopicMapper.RetrieveByCriteria(criterias)
                Return ForumTopicList.Count
            End Get

        End Property

        Public ReadOnly Property TotalPosting() As Integer
            Get
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumTopic), "Forum.ID", MatchType.Exact, ID))
                Dim m_ForumTopicMapper As IMapper
                m_ForumTopicMapper = MapperFactory.GetInstance.GetMapper(GetType(ForumTopic).ToString)
                Dim ForumTopicList As ArrayList = m_ForumTopicMapper.RetrieveByCriteria(criterias)
                If (ForumTopicList.Count > 0) Then
                    For Each item As ForumTopic In ForumTopicList
                        Return item.TotalPosting
                    Next
                End If

                Return 0
            End Get

        End Property
#End Region

    End Class
End Namespace