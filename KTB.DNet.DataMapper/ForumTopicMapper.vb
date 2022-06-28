#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ForumTopic Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/28/2007 - 9:37:29 AM
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
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

Namespace KTB.DNet.DataMapper

    Public Class ForumTopicMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertForumTopic"
        Private m_UpdateStatement As String = "up_UpdateForumTopic"
        Private m_RetrieveStatement As String = "up_RetrieveForumTopic"
        Private m_RetrieveListStatement As String = "up_RetrieveForumTopicList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteForumTopic"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim forumTopic As ForumTopic = Nothing
            While dr.Read

                forumTopic = Me.CreateObject(dr)

            End While

            Return forumTopic

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim forumTopicList As ArrayList = New ArrayList

            While dr.Read
                Dim forumTopic As ForumTopic = Me.CreateObject(dr)
                forumTopicList.Add(forumTopic)
            End While

            Return forumTopicList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim forumTopic As ForumTopic = CType(obj, ForumTopic)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, forumTopic.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim forumTopic As ForumTopic = CType(obj, ForumTopic)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, forumTopic.Title)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, forumTopic.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, forumTopic.Attachment)
            DbCommandWrapper.AddInParameter("@LastPostDate", DbType.DateTime, forumTopic.LastPostDate)
            DbCommandWrapper.AddInParameter("@UserEntry", DbType.AnsiString, forumTopic.UserEntry)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, forumTopic.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, forumTopic.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ForumID", DbType.Int32, Me.GetRefObject(forumTopic.Forum))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim forumTopic As ForumTopic = CType(obj, ForumTopic)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, forumTopic.ID)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, forumTopic.Title)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, forumTopic.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, forumTopic.Attachment)
            DbCommandWrapper.AddInParameter("@LastPostDate", DbType.DateTime, forumTopic.LastPostDate)
            DbCommandWrapper.AddInParameter("@UserEntry", DbType.AnsiString, forumTopic.UserEntry)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, forumTopic.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, forumTopic.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ForumID", DbType.Int32, Me.GetRefObject(forumTopic.Forum))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ForumTopic

            Dim forumTopic As ForumTopic = New ForumTopic

            forumTopic.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Title")) Then forumTopic.Title = dr("Title").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then forumTopic.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then forumTopic.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastPostDate")) Then forumTopic.LastPostDate = CType(dr("LastPostDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UserEntry")) Then forumTopic.UserEntry = dr("UserEntry").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then forumTopic.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then forumTopic.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then forumTopic.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then forumTopic.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then forumTopic.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ForumID")) Then
                forumTopic.Forum = New Forum(CType(dr("ForumID"), Integer))
            End If

            Return forumTopic

        End Function

        Private Sub SetTableName()

            If Not (GetType(ForumTopic) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ForumTopic), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ForumTopic).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

