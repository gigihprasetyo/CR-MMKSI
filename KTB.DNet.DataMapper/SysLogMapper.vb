#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SysLog Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 19/09/2007 - 9:47:41
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

    Public Class SysLogMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSysLog"
        Private m_UpdateStatement As String = "up_UpdateSysLog"
        Private m_RetrieveStatement As String = "up_RetrieveSysLog"
        Private m_RetrieveListStatement As String = "up_RetrieveSysLogList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSysLog"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sysLog As SysLog = Nothing
            While dr.Read

                sysLog = Me.CreateObject(dr)

            End While

            Return sysLog

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sysLogList As ArrayList = New ArrayList

            While dr.Read
                Dim sysLog As SysLog = Me.CreateObject(dr)
                sysLogList.Add(sysLog)
            End While

            Return sysLogList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sysLog As SysLog = CType(obj, SysLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sysLog.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sysLog As SysLog = CType(obj, SysLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, sysLog.Status)
            DbCommandWrapper.AddInParameter("@ModuleName", DbType.AnsiString, sysLog.ModuleName)
            DbCommandWrapper.AddInParameter("@RemoteIPAddress", DbType.AnsiString, sysLog.RemoteIPAddress)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, sysLog.UserName)
            DbCommandWrapper.AddInParameter("@Pages", DbType.AnsiString, sysLog.Pages)
            DbCommandWrapper.AddInParameter("@BlockName", DbType.AnsiString, sysLog.BlockName)
            DbCommandWrapper.AddInParameter("@SubBlockName", DbType.AnsiString, sysLog.SubBlockName)
            DbCommandWrapper.AddInParameter("@Action", DbType.AnsiString, sysLog.Action)
            DbCommandWrapper.AddInParameter("@ResultCode", DbType.AnsiString, sysLog.ResultCode)
            DbCommandWrapper.AddInParameter("@FullMessage", DbType.AnsiString, sysLog.FullMessage)
            DbCommandWrapper.AddInParameter("@LogTime", DbType.DateTime, sysLog.LogTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sysLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sysLog.LastUpdateBy)
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

            Dim sysLog As SysLog = CType(obj, SysLog)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sysLog.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, sysLog.Status)
            DbCommandWrapper.AddInParameter("@ModuleName", DbType.AnsiString, sysLog.ModuleName)
            DbCommandWrapper.AddInParameter("@RemoteIPAddress", DbType.AnsiString, sysLog.RemoteIPAddress)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, sysLog.UserName)
            DbCommandWrapper.AddInParameter("@Pages", DbType.AnsiString, sysLog.Pages)
            DbCommandWrapper.AddInParameter("@BlockName", DbType.AnsiString, sysLog.BlockName)
            DbCommandWrapper.AddInParameter("@SubBlockName", DbType.AnsiString, sysLog.SubBlockName)
            DbCommandWrapper.AddInParameter("@Action", DbType.AnsiString, sysLog.Action)
            DbCommandWrapper.AddInParameter("@ResultCode", DbType.AnsiString, sysLog.ResultCode)
            DbCommandWrapper.AddInParameter("@FullMessage", DbType.AnsiString, sysLog.FullMessage)
            DbCommandWrapper.AddInParameter("@LogTime", DbType.DateTime, sysLog.LogTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sysLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sysLog.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SysLog

            Dim sysLog As SysLog = New SysLog

            sysLog.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sysLog.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ModuleName")) Then sysLog.ModuleName = dr("ModuleName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RemoteIPAddress")) Then sysLog.RemoteIPAddress = dr("RemoteIPAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UserName")) Then sysLog.UserName = dr("UserName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Pages")) Then sysLog.Pages = dr("Pages").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BlockName")) Then sysLog.BlockName = dr("BlockName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubBlockName")) Then sysLog.SubBlockName = dr("SubBlockName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Action")) Then sysLog.Action = dr("Action").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ResultCode")) Then sysLog.ResultCode = dr("ResultCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FullMessage")) Then sysLog.FullMessage = dr("FullMessage").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LogTime")) Then sysLog.LogTime = CType(dr("LogTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sysLog.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sysLog.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sysLog.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sysLog.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sysLog.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sysLog

        End Function

        Private Sub SetTableName()

            If Not (GetType(SysLog) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SysLog), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SysLog).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

