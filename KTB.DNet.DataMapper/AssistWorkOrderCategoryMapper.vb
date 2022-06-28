
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistWorkOrderCategory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/7/2017 - 2:37:08 PM
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

    Public Class AssistWorkOrderCategoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAssistWorkOrderCategory"
        Private m_UpdateStatement As String = "up_UpdateAssistWorkOrderCategory"
        Private m_RetrieveStatement As String = "up_RetrieveAssistWorkOrderCategory"
        Private m_RetrieveListStatement As String = "up_RetrieveAssistWorkOrderCategoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAssistWorkOrderCategory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim assistWorkOrderCategory As AssistWorkOrderCategory = Nothing
            While dr.Read

                assistWorkOrderCategory = Me.CreateObject(dr)

            End While

            Return assistWorkOrderCategory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim assistWorkOrderCategoryList As ArrayList = New ArrayList

            While dr.Read
                Dim assistWorkOrderCategory As AssistWorkOrderCategory = Me.CreateObject(dr)
                assistWorkOrderCategoryList.Add(assistWorkOrderCategory)
            End While

            Return assistWorkOrderCategoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistWorkOrderCategory As AssistWorkOrderCategory = CType(obj, AssistWorkOrderCategory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistWorkOrderCategory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistWorkOrderCategory As AssistWorkOrderCategory = CType(obj, AssistWorkOrderCategory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@WorkOrderCategory", DbType.AnsiString, assistWorkOrderCategory.WorkOrderCategory)
            DbCommandWrapper.AddInParameter("@WorkOrderTypeID", DbType.Int32, Me.GetRefObject(assistWorkOrderCategory.AssistWorkOrderType))
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, assistWorkOrderCategory.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, assistWorkOrderCategory.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistWorkOrderCategory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistWorkOrderCategory.LastUpdateBy)
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

            Dim assistWorkOrderCategory As AssistWorkOrderCategory = CType(obj, AssistWorkOrderCategory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistWorkOrderCategory.ID)
            DbCommandWrapper.AddInParameter("@WorkOrderCategory", DbType.AnsiString, assistWorkOrderCategory.WorkOrderCategory)
            DbCommandWrapper.AddInParameter("@WorkOrderTypeID", DbType.Int32, Me.GetRefObject(assistWorkOrderCategory.AssistWorkOrderType))
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, assistWorkOrderCategory.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, assistWorkOrderCategory.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistWorkOrderCategory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, assistWorkOrderCategory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AssistWorkOrderCategory

            Dim assistWorkOrderCategory As AssistWorkOrderCategory = New AssistWorkOrderCategory

            assistWorkOrderCategory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderCategory")) Then assistWorkOrderCategory.WorkOrderCategory = dr("WorkOrderCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then assistWorkOrderCategory.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then assistWorkOrderCategory.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then assistWorkOrderCategory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then assistWorkOrderCategory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then assistWorkOrderCategory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then assistWorkOrderCategory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then assistWorkOrderCategory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderTypeID")) Then
                assistWorkOrderCategory.AssistWorkOrderType = New AssistWorkOrderType(CType(dr("WorkOrderTypeID"), Integer))
            End If
            Return assistWorkOrderCategory

        End Function

        Private Sub SetTableName()

            If Not (GetType(AssistWorkOrderCategory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AssistWorkOrderCategory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AssistWorkOrderCategory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

