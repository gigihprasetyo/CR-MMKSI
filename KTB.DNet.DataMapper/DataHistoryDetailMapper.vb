
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DataHistoryDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 7/29/2011 - 11:17:14 AM
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

    Public Class DataHistoryDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDataHistoryDetail"
        Private m_UpdateStatement As String = "up_UpdateDataHistoryDetail"
        Private m_RetrieveStatement As String = "up_RetrieveDataHistoryDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveDataHistoryDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDataHistoryDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dataHistoryDetail As DataHistoryDetail = Nothing
            While dr.Read

                dataHistoryDetail = Me.CreateObject(dr)

            End While

            Return dataHistoryDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dataHistoryDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim dataHistoryDetail As DataHistoryDetail = Me.CreateObject(dr)
                dataHistoryDetailList.Add(dataHistoryDetail)
            End While

            Return dataHistoryDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dataHistoryDetail As DataHistoryDetail = CType(obj, DataHistoryDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dataHistoryDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dataHistoryDetail As DataHistoryDetail = CType(obj, DataHistoryDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@DataHistoryID", DbType.Int32, dataHistoryDetail.DataHistoryID)
            DBCommandWrapper.AddInParameter("@FieldName", DbType.AnsiString, dataHistoryDetail.FieldName)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, dataHistoryDetail.Description)
            DbCommandWrapper.AddInParameter("@OldValue", DbType.AnsiString, dataHistoryDetail.OldValue)
            DbCommandWrapper.AddInParameter("@NewValue", DbType.AnsiString, dataHistoryDetail.NewValue)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dataHistoryDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dataHistoryDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@DataHistoryID", DbType.Int32, Me.GetRefObject(dataHistoryDetail.DataHistory))

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

            Dim dataHistoryDetail As DataHistoryDetail = CType(obj, DataHistoryDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dataHistoryDetail.ID)
            'DbCommandWrapper.AddInParameter("@DataHistoryID", DbType.Int32, dataHistoryDetail.DataHistoryID)
            DBCommandWrapper.AddInParameter("@FieldName", DbType.AnsiString, dataHistoryDetail.FieldName)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, dataHistoryDetail.Description)
            DbCommandWrapper.AddInParameter("@OldValue", DbType.AnsiString, dataHistoryDetail.OldValue)
            DbCommandWrapper.AddInParameter("@NewValue", DbType.AnsiString, dataHistoryDetail.NewValue)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dataHistoryDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dataHistoryDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@DataHistoryID", DbType.Int32, Me.GetRefObject(dataHistoryDetail.DataHistory))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DataHistoryDetail

            Dim dataHistoryDetail As DataHistoryDetail = New DataHistoryDetail

            dataHistoryDetail.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("DataHistoryID")) Then dataHistoryDetail.DataHistoryID = CType(dr("DataHistoryID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FieldName")) Then dataHistoryDetail.FieldName = dr("FieldName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then dataHistoryDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OldValue")) Then dataHistoryDetail.OldValue = dr("OldValue").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NewValue")) Then dataHistoryDetail.NewValue = dr("NewValue").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dataHistoryDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dataHistoryDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dataHistoryDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dataHistoryDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dataHistoryDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DataHistoryID")) Then dataHistoryDetail.DataHistory = New DataHistory(CType(dr("DataHistoryID"), Integer))

            Return dataHistoryDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(DataHistoryDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DataHistoryDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DataHistoryDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

