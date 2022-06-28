
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BCPDynamicQuery Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 21/02/2019 - 15:03:20
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

    Public Class BCPDynamicQueryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBCPDynamicQuery"
        Private m_UpdateStatement As String = "up_UpdateBCPDynamicQuery"
        Private m_RetrieveStatement As String = "up_RetrieveBCPDynamicQuery"
        Private m_RetrieveListStatement As String = "up_RetrieveBCPDynamicQueryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBCPDynamicQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim bCPDynamicQuery As BCPDynamicQuery = Nothing
            While dr.Read

                bCPDynamicQuery = Me.CreateObject(dr)

            End While

            Return bCPDynamicQuery

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim bCPDynamicQueryList As ArrayList = New ArrayList

            While dr.Read
                Dim bCPDynamicQuery As BCPDynamicQuery = Me.CreateObject(dr)
                bCPDynamicQueryList.Add(bCPDynamicQuery)
            End While

            Return bCPDynamicQueryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bCPDynamicQuery As BCPDynamicQuery = CType(obj, BCPDynamicQuery)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bCPDynamicQuery.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bCPDynamicQuery As BCPDynamicQuery = CType(obj, BCPDynamicQuery)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@BCPQueryID", DbType.AnsiString, bCPDynamicQuery.BCPQueryID)
            DbCommandWrapper.AddInParameter("@OriginalFieldName", DbType.AnsiString, bCPDynamicQuery.OriginalFieldName)
            DbCommandWrapper.AddInParameter("@ConvertFieldName", DbType.String, bCPDynamicQuery.ConvertFieldName)
            DbCommandWrapper.AddInParameter("@FieldNameInAlias", DbType.AnsiString, bCPDynamicQuery.FieldNameInAlias)
            DbCommandWrapper.AddInParameter("@DefaultWhereClause", DbType.AnsiString, bCPDynamicQuery.DefaultWhereClause)
            DbCommandWrapper.AddInParameter("@AddtionalWhereClause", DbType.AnsiString, bCPDynamicQuery.AddtionalWhereClause)
            DbCommandWrapper.AddInParameter("@Field3", DbType.String, bCPDynamicQuery.Field3)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, bCPDynamicQuery.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bCPDynamicQuery.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, bCPDynamicQuery.LastUpdateBy)
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

            Dim bCPDynamicQuery As BCPDynamicQuery = CType(obj, BCPDynamicQuery)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bCPDynamicQuery.ID)
            DbCommandWrapper.AddInParameter("@BCPQueryID", DbType.AnsiString, bCPDynamicQuery.BCPQueryID)
            DbCommandWrapper.AddInParameter("@OriginalFieldName", DbType.AnsiString, bCPDynamicQuery.OriginalFieldName)
            DbCommandWrapper.AddInParameter("@ConvertFieldName", DbType.String, bCPDynamicQuery.ConvertFieldName)
            DbCommandWrapper.AddInParameter("@FieldNameInAlias", DbType.AnsiString, bCPDynamicQuery.FieldNameInAlias)
            DbCommandWrapper.AddInParameter("@DefaultWhereClause", DbType.AnsiString, bCPDynamicQuery.DefaultWhereClause)
            DbCommandWrapper.AddInParameter("@AddtionalWhereClause", DbType.AnsiString, bCPDynamicQuery.AddtionalWhereClause)
            DbCommandWrapper.AddInParameter("@Field3", DbType.String, bCPDynamicQuery.Field3)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, bCPDynamicQuery.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bCPDynamicQuery.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, bCPDynamicQuery.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BCPDynamicQuery

            Dim bCPDynamicQuery As BCPDynamicQuery = New BCPDynamicQuery

            bCPDynamicQuery.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BCPQueryID")) Then bCPDynamicQuery.BCPQueryID = dr("BCPQueryID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OriginalFieldName")) Then bCPDynamicQuery.OriginalFieldName = dr("OriginalFieldName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConvertFieldName")) Then bCPDynamicQuery.ConvertFieldName = dr("ConvertFieldName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FieldNameInAlias")) Then bCPDynamicQuery.FieldNameInAlias = dr("FieldNameInAlias").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DefaultWhereClause")) Then bCPDynamicQuery.DefaultWhereClause = dr("DefaultWhereClause").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AddtionalWhereClause")) Then bCPDynamicQuery.AddtionalWhereClause = dr("AddtionalWhereClause").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Field3")) Then bCPDynamicQuery.Field3 = dr("Field3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then bCPDynamicQuery.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then bCPDynamicQuery.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then bCPDynamicQuery.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then bCPDynamicQuery.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then bCPDynamicQuery.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then bCPDynamicQuery.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return bCPDynamicQuery

        End Function

        Private Sub SetTableName()

            If Not (GetType(BCPDynamicQuery) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BCPDynamicQuery), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BCPDynamicQuery).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

