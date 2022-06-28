#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ForumTopic Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/25/2007 - 8:14:05 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("ForumTopic")> _
    Public Class ForumTopic
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
        Private _attachment As String = String.Empty
        Private _lastPostDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _userEntry As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _forum As Forum

        Private _forumPosts As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _forumLastReadPosts As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Attachment", "'{0}'")> _
        Public Property Attachment() As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property


        <ColumnInfo("LastPostDate", "'{0:yyyy/MM/dd}'")> _
        Public Property LastPostDate() As DateTime
            Get
                Return _lastPostDate
            End Get
            Set(ByVal value As DateTime)
                _lastPostDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("ForumID", "{0}"), _
        RelationInfo("Forum", "ID", "ForumTopic", "ForumID")> _
        Public Property Forum() As Forum
            Get
                Try
                    If Not IsNothing(Me._forum) AndAlso (Not Me._forum.IsLoaded) Then

                        Me._forum = CType(DoLoad(GetType(Forum).ToString(), _forum.ID), Forum)
                        Me._forum.MarkLoaded()

                    End If

                    Return Me._forum

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Forum)

                Me._forum = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._forum.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("ForumTopic", "ID", "ForumPost", "ForumTopicID")> _
        Public ReadOnly Property ForumPosts() As System.Collections.ArrayList
            Get
                Try
                    If (Me._forumPosts.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ForumPost), "ForumTopic", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ForumPost), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._forumPosts = DoLoadArray(GetType(ForumPost).ToString, criterias)
                    End If

                    Return Me._forumPosts

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("ForumTopic", "ID", "ForumLastReadPost", "ForumTopicID")> _
        Public ReadOnly Property ForumLastReadPosts() As System.Collections.ArrayList
            Get
                Try
                    If (Me._forumLastReadPosts.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ForumLastReadPost), "ForumTopic", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ForumLastReadPost), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._forumLastReadPosts = DoLoadArray(GetType(ForumLastReadPost).ToString, criterias)
                    End If

                    Return Me._forumLastReadPosts

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
        Public ReadOnly Property TotalPosting() As Integer
            Get
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), "ForumTopic.ID", MatchType.Exact, ID))
                criterias.opAnd(New Criteria(GetType(ForumPost), "isHeader", MatchType.Exact, 0))

                Dim m_ForumPostMapper As IMapper
                m_ForumPostMapper = MapperFactory.GetInstance.GetMapper(GetType(ForumPost).ToString)
                Dim ForumPostList As ArrayList = m_ForumPostMapper.RetrieveByCriteria(criterias)
                Return ForumPostList.Count
            End Get

        End Property

        'Public ReadOnly Property LastPostingBy() As String
        '    Get
        '        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), "ForumTopic.ID", MatchType.Exact, ID))
        '        criterias.opAnd(New Criteria(GetType(ForumPost), "isHeader", MatchType.Exact, 0))
        '        Dim sortColl As SortCollection = New SortCollection
        '        If (Not IsNothing(CreatedTime)) And (Not IsNothing(CreatedTime)) Then
        '            sortColl.Add(New Search.Sort(GetType(ForumPost), "CreatedTime", Sort.SortDirection.ASC))
        '        Else
        '            sortColl = Nothing
        '        End If

        '        Dim m_ForumPostMapper As IMapper
        '        m_ForumPostMapper = MapperFactory.GetInstance.GetMapper(GetType(ForumPost).ToString)
        '        Dim ForumPostList As ArrayList = m_ForumPostMapper.RetrieveByCriteria(criterias, sortColl)
        '        If ForumPostList.Count <> 0 Then
        '            Dim item As ForumPost = ForumPostList(ForumPostList.Count - 1)
        '            Return item.CreatedBy
        '        End If
        '        Return ""
        '    End Get

        'End Property
#End Region

    End Class
End Namespace

