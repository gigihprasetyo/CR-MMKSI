#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MessageService Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/21/2020 - 10:26:03 AM
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

    Public Class MessageServiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMessageService"
        Private m_UpdateStatement As String = "up_UpdateMessageService"
        Private m_RetrieveStatement As String = "up_RetrieveMessageService"
        Private m_RetrieveListStatement As String = "up_RetrieveMessageServiceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMessageService"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim messageService As MessageService = Nothing
            While dr.Read

                messageService = Me.CreateObject(dr)

            End While

            Return messageService

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim messageServiceList As ArrayList = New ArrayList

            While dr.Read
                Dim messageService As MessageService = Me.CreateObject(dr)
                messageServiceList.Add(messageService)
            End While

            Return messageServiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim messageService As MessageService = CType(obj, MessageService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, messageService.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim messageService As MessageService = CType(obj, MessageService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RefID", DbType.AnsiString, messageService.RefID)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, messageService.UserName)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, messageService.Type)
            DbCommandWrapper.AddInParameter("@Time", DbType.AnsiString, messageService.Time)
            DbCommandWrapper.AddInParameter("@Sender", DbType.AnsiString, messageService.Sender)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, messageService.Subject)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, messageService.Message)
            DbCommandWrapper.AddInParameter("@BackupEx", DbType.AnsiString, messageService.BackupEx)
            DbCommandWrapper.AddInParameter("@BackupOn", DbType.AnsiString, messageService.BackupOn)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, messageService.Attachment)
            DbCommandWrapper.AddInParameter("@ReffSource", DbType.AnsiString, messageService.ReffSource)
            DbCommandWrapper.AddInParameter("@FID", DbType.Int32, messageService.FID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, messageService.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, messageService.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.StringFixedLength, messageService.LastUpdateBy)


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

            Dim messageService As MessageService = CType(obj, MessageService)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, messageService.ID)
            DbCommandWrapper.AddInParameter("@RefID", DbType.AnsiString, messageService.RefID)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, messageService.UserName)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, messageService.Type)
            DbCommandWrapper.AddInParameter("@Time", DbType.AnsiString, messageService.Time)
            DbCommandWrapper.AddInParameter("@Sender", DbType.AnsiString, messageService.Sender)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, messageService.Subject)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, messageService.Message)
            DbCommandWrapper.AddInParameter("@BackupEx", DbType.AnsiString, messageService.BackupEx)
            DbCommandWrapper.AddInParameter("@BackupOn", DbType.AnsiString, messageService.BackupOn)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, messageService.Attachment)
            DbCommandWrapper.AddInParameter("@ReffSource", DbType.AnsiString, messageService.ReffSource)
            DbCommandWrapper.AddInParameter("@FID", DbType.Int32, messageService.FID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, messageService.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, messageService.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, messageService.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.StringFixedLength, User)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MessageService

            Dim messageService As MessageService = New MessageService

            messageService.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RefID")) Then messageService.RefID = dr("RefID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UserName")) Then messageService.UserName = dr("UserName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then messageService.Type = CType(dr("Type"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Time")) Then messageService.Time = dr("Time").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Sender")) Then messageService.Sender = dr("Sender").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Subject")) Then messageService.Subject = dr("Subject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Message")) Then messageService.Message = dr("Message").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BackupEx")) Then messageService.BackupEx = dr("BackupEx").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BackupOn")) Then messageService.BackupOn = dr("BackupOn").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then messageService.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReffSource")) Then messageService.ReffSource = dr("ReffSource").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FID")) Then messageService.FID = CType(dr("FID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then messageService.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then messageService.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then messageService.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then messageService.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then messageService.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then messageService.LastUpdateBy = dr("LastUpdateBy").ToString

            Return messageService

        End Function

        Private Sub SetTableName()

            If Not (GetType(MessageService) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MessageService), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MessageService).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
