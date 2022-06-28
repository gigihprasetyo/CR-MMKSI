#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : StatusChangeHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 16/11/2005 - 16:29:41
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

    Public Class StatusChangeHistorySendEmailFlagMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertStatusChangeHistorySendEmailFlag"
        Private m_UpdateStatement As String = "up_UpdateStatusChangeHistorySendEmailFlag"
        Private m_RetrieveStatement As String = "up_RetrieveStatusChangeHistorySendEmailFlag"
        Private m_RetrieveListStatement As String = "up_RetrieveStatusChangeHistorySendEmailFlagList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteStatusChangeHistorySendEmailFlag"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim StatusChangeHistorySendEmailFlag As StatusChangeHistorySendEmailFlag = Nothing
            While dr.Read

                StatusChangeHistorySendEmailFlag = Me.CreateObject(dr)

            End While

            Return StatusChangeHistorySendEmailFlag

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim StatusChangeHistorySendEmailFlagList As ArrayList = New ArrayList

            While dr.Read
                Dim StatusChangeHistorySendEmailFlag As StatusChangeHistorySendEmailFlag = Me.CreateObject(dr)
                StatusChangeHistorySendEmailFlagList.Add(StatusChangeHistorySendEmailFlag)
            End While

            Return StatusChangeHistorySendEmailFlagList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim StatusChangeHistorySendEmailFlag As StatusChangeHistorySendEmailFlag = CType(obj, StatusChangeHistorySendEmailFlag)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@id", DbType.Int32, StatusChangeHistorySendEmailFlag.id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim StatusChangeHistorySendEmailFlag As StatusChangeHistorySendEmailFlag = CType(obj, StatusChangeHistorySendEmailFlag)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@id", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@IsSendEmail", DbType.Int16, StatusChangeHistorySendEmailFlag.IsSendEmail)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, StatusChangeHistorySendEmailFlag.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, StatusChangeHistorySendEmailFlag.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)

            DbCommandWrapper.AddInParameter("@StatusChangeHistoryID", DbType.Int32, Me.GetRefObject(StatusChangeHistorySendEmailFlag.StatusChangeHistory))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@id"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "id")

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
            DbCommandWrapper.AddInParameter("@id", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim StatusChangeHistorySendEmailFlag As StatusChangeHistorySendEmailFlag = CType(obj, StatusChangeHistorySendEmailFlag)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@id", DbType.Int32, StatusChangeHistorySendEmailFlag.id)
            DbCommandWrapper.AddInParameter("@IsSendEmail", DbType.Int16, StatusChangeHistorySendEmailFlag.IsSendEmail)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, StatusChangeHistorySendEmailFlag.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, StatusChangeHistorySendEmailFlag.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)

            DbCommandWrapper.AddInParameter("@StatusChangeHistoryID", DbType.Int32, Me.GetRefObject(StatusChangeHistorySendEmailFlag.StatusChangeHistory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As StatusChangeHistorySendEmailFlag

            Dim StatusChangeHistorySendEmailFlag As StatusChangeHistorySendEmailFlag = New StatusChangeHistorySendEmailFlag

            StatusChangeHistorySendEmailFlag.id = CType(dr("id"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsSendEmail")) Then StatusChangeHistorySendEmailFlag.IsSendEmail = CType(dr("IsSendEmail"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then StatusChangeHistorySendEmailFlag.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then StatusChangeHistorySendEmailFlag.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then StatusChangeHistorySendEmailFlag.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then StatusChangeHistorySendEmailFlag.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then StatusChangeHistorySendEmailFlag.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("StatusChangeHistoryID")) Then
                StatusChangeHistorySendEmailFlag.StatusChangeHistory = New StatusChangeHistory(CType(dr("StatusChangeHistoryID"), Integer))
            End If
            Return StatusChangeHistorySendEmailFlag

        End Function

        Private Sub SetTableName()

            If Not (GetType(StatusChangeHistorySendEmailFlag) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(StatusChangeHistorySendEmailFlag), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(StatusChangeHistorySendEmailFlag).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

