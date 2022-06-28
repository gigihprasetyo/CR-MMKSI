
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WsJSONLog Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 10/6/2020 - 4:27:18 PM
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

    Public Class WsJSONLogMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWsJSONLog"
        Private m_UpdateStatement As String = "up_UpdateWsJSONLog"
        Private m_RetrieveStatement As String = "up_RetrieveWsJSONLog"
        Private m_RetrieveListStatement As String = "up_RetrieveWsJSONLogList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWsJSONLog"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim wsJSONLog As WsJSONLog = Nothing
            While dr.Read

                wsJSONLog = Me.CreateObject(dr)

            End While

            Return wsJSONLog

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim wsJSONLogList As ArrayList = New ArrayList

            While dr.Read
                Dim wsJSONLog As WsJSONLog = Me.CreateObject(dr)
                wsJSONLogList.Add(wsJSONLog)
            End While

            Return wsJSONLogList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wsJSONLog As WsJSONLog = CType(obj, WsJSONLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wsJSONLog.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wsJSONLog As WsJSONLog = CType(obj, WsJSONLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Source", DbType.AnsiString, wsJSONLog.Source)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, wsJSONLog.Status)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, wsJSONLog.Message)
            DbCommandWrapper.AddInParameter("@Body", DbType.AnsiString, wsJSONLog.Body)
            DbCommandWrapper.AddInParameter("@KeyName", DbType.AnsiString, wsJSONLog.KeyName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wsJSONLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, wsJSONLog.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, wsJSONLog.LastUpdatedTime)


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

            Dim wsJSONLog As WsJSONLog = CType(obj, WsJSONLog)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wsJSONLog.ID)
            DbCommandWrapper.AddInParameter("@Source", DbType.AnsiString, wsJSONLog.Source)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, wsJSONLog.Status)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, wsJSONLog.Message)
            DbCommandWrapper.AddInParameter("@Body", DbType.AnsiString, wsJSONLog.Body)
            DbCommandWrapper.AddInParameter("@KeyName", DbType.AnsiString, wsJSONLog.KeyName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wsJSONLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, wsJSONLog.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, wsJSONLog.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WsJSONLog

            Dim wsJSONLog As WsJSONLog = New WsJSONLog

            wsJSONLog.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Source")) Then wsJSONLog.Source = dr("Source").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then wsJSONLog.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Message")) Then wsJSONLog.Message = dr("Message").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Body")) Then wsJSONLog.Body = dr("Body").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KeyName")) Then wsJSONLog.KeyName = dr("KeyName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then wsJSONLog.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then wsJSONLog.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then wsJSONLog.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then wsJSONLog.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then wsJSONLog.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            Return wsJSONLog

        End Function

        Private Sub SetTableName()

            If Not (GetType(WsJSONLog) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WsJSONLog), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WsJSONLog).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

