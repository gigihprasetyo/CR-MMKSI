#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MessageNotification Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/16/2021 - 11:09:26 AM
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

    Public Class MessageNotificationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMessageNotification"
        Private m_UpdateStatement As String = "up_UpdateMessageNotification"
        Private m_RetrieveStatement As String = "up_RetrieveMessageNotification"
        Private m_RetrieveListStatement As String = "up_RetrieveMessageNotificationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMessageNotification"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim messageNotification As MessageNotification = Nothing
            While dr.Read

                messageNotification = Me.CreateObject(dr)

            End While

            Return messageNotification

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim messageNotificationList As ArrayList = New ArrayList

            While dr.Read
                Dim messageNotification As MessageNotification = Me.CreateObject(dr)
                messageNotificationList.Add(messageNotification)
            End While

            Return messageNotificationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim messageNotification As MessageNotification = CType(obj, MessageNotification)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, messageNotification.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim messageNotification As MessageNotification = CType(obj, MessageNotification)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@MessageTemplateID", DbType.Int32, Me.GetRefObject(messageNotification.MessageTemplate))
            DbCommandWrapper.AddInParameter("@PhoneNumber", DbType.AnsiString, messageNotification.PhoneNumber)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, messageNotification.Message)
            DbCommandWrapper.AddInParameter("@ProcessTime", DbType.DateTime, messageNotification.ProcessTime)
            DbCommandWrapper.AddInParameter("@SendDateTime", DbType.DateTime, messageNotification.SendDateTime)
            DbCommandWrapper.AddInParameter("@TransactionID", DbType.AnsiString, messageNotification.TransactionID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, messageNotification.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, messageNotification.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, messageNotification.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim messageNotification As MessageNotification = CType(obj, MessageNotification)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, messageNotification.ID)
            DbCommandWrapper.AddInParameter("@MessageTemplateID", DbType.Int32, Me.GetRefObject(messageNotification.MessageTemplate))
            DbCommandWrapper.AddInParameter("@PhoneNumber", DbType.AnsiString, messageNotification.PhoneNumber)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, messageNotification.Message)
            DbCommandWrapper.AddInParameter("@ProcessTime", DbType.DateTime, messageNotification.ProcessTime)
            DbCommandWrapper.AddInParameter("@SendDateTime", DbType.DateTime, messageNotification.SendDateTime)
            DbCommandWrapper.AddInParameter("@TransactionID", DbType.AnsiString, messageNotification.TransactionID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, messageNotification.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, messageNotification.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, messageNotification.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MessageNotification

            Dim messageNotification As MessageNotification = New MessageNotification

            messageNotification.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNumber")) Then messageNotification.PhoneNumber = dr("PhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Message")) Then messageNotification.Message = dr("Message").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessTime")) Then messageNotification.ProcessTime = CType(dr("ProcessTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SendDateTime")) Then messageNotification.SendDateTime = CType(dr("SendDateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionID")) Then messageNotification.TransactionID = dr("TransactionID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then messageNotification.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then messageNotification.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then messageNotification.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then messageNotification.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then messageNotification.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then messageNotification.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("MessageTemplateID")) Then
                messageNotification.MessageTemplate = New MessageTemplate(CType(dr("MessageTemplateID"), Integer))
            End If


            Return messageNotification

        End Function

        Private Sub SetTableName()

            If Not (GetType(MessageNotification) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MessageNotification), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MessageNotification).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
