#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : GlobalUpload Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/20/2019 - 1:34:04 PM
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

    Public Class GlobalUploadMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertGlobalUpload"
        Private m_UpdateStatement As String = "up_UpdateGlobalUpload"
        Private m_RetrieveStatement As String = "up_RetrieveGlobalUpload"
        Private m_RetrieveListStatement As String = "up_RetrieveGlobalUploadList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteGlobalUpload"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim globalUpload As GlobalUpload = Nothing
            While dr.Read

                globalUpload = Me.CreateObject(dr)

            End While

            Return globalUpload

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim globalUploadList As ArrayList = New ArrayList

            While dr.Read
                Dim globalUpload As GlobalUpload = Me.CreateObject(dr)
                globalUploadList.Add(globalUpload)
            End While

            Return globalUploadList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim globalUpload As GlobalUpload = CType(obj, GlobalUpload)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, globalUpload.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim globalUpload As GlobalUpload = CType(obj, GlobalUpload)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, globalUpload.Code)
            DbCommandWrapper.AddInParameter("@DisplayName", DbType.AnsiString, globalUpload.DisplayName)
            DbCommandWrapper.AddInParameter("@PrivilegeName", DbType.AnsiString, globalUpload.PrivilegeName)
            DbCommandWrapper.AddInParameter("@ObjectName", DbType.AnsiString, globalUpload.ObjectName)
            DbCommandWrapper.AddInParameter("@FacadeName", DbType.AnsiString, globalUpload.FacadeName)
            DbCommandWrapper.AddInParameter("@ParserName", DbType.AnsiString, globalUpload.ParserName)
            DbCommandWrapper.AddInParameter("@UploadMethodName", DbType.AnsiString, globalUpload.UploadMethodName)
            DbCommandWrapper.AddInParameter("@DownloadMethodName", DbType.AnsiString, globalUpload.DownloadMethodName)
            DbCommandWrapper.AddInParameter("@DownloadFileName", DbType.AnsiString, globalUpload.DownloadFileName)
            DbCommandWrapper.AddInParameter("@MaxFileSize", DbType.Int32, globalUpload.MaxFileSize)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, globalUpload.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, globalUpload.LastUpdatedBy)
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

            Dim globalUpload As GlobalUpload = CType(obj, GlobalUpload)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, globalUpload.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, globalUpload.Code)
            DbCommandWrapper.AddInParameter("@DisplayName", DbType.AnsiString, globalUpload.DisplayName)
            DbCommandWrapper.AddInParameter("@PrivilegeName", DbType.AnsiString, globalUpload.PrivilegeName)
            DbCommandWrapper.AddInParameter("@ObjectName", DbType.AnsiString, globalUpload.ObjectName)
            DbCommandWrapper.AddInParameter("@FacadeName", DbType.AnsiString, globalUpload.FacadeName)
            DbCommandWrapper.AddInParameter("@ParserName", DbType.AnsiString, globalUpload.ParserName)
            DbCommandWrapper.AddInParameter("@UploadMethodName", DbType.AnsiString, globalUpload.UploadMethodName)
            DbCommandWrapper.AddInParameter("@DownloadMethodName", DbType.AnsiString, globalUpload.DownloadMethodName)
            DbCommandWrapper.AddInParameter("@DownloadFileName", DbType.AnsiString, globalUpload.DownloadFileName)
            DbCommandWrapper.AddInParameter("@MaxFileSize", DbType.Int32, globalUpload.MaxFileSize)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, globalUpload.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, globalUpload.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, globalUpload.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As GlobalUpload

            Dim globalUpload As GlobalUpload = New GlobalUpload

            globalUpload.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then globalUpload.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DisplayName")) Then globalUpload.DisplayName = dr("DisplayName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PrivilegeName")) Then globalUpload.PrivilegeName = dr("PrivilegeName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ObjectName")) Then globalUpload.ObjectName = dr("ObjectName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FacadeName")) Then globalUpload.FacadeName = dr("FacadeName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ParserName")) Then globalUpload.ParserName = dr("ParserName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadMethodName")) Then globalUpload.UploadMethodName = dr("UploadMethodName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadMethodName")) Then globalUpload.DownloadMethodName = dr("DownloadMethodName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadFileName")) Then globalUpload.DownloadFileName = dr("DownloadFileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaxFileSize")) Then globalUpload.MaxFileSize = CType(dr("MaxFileSize"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then globalUpload.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then globalUpload.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then globalUpload.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then globalUpload.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then globalUpload.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return globalUpload

        End Function

        Private Sub SetTableName()

            If Not (GetType(GlobalUpload) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(GlobalUpload), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(GlobalUpload).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
