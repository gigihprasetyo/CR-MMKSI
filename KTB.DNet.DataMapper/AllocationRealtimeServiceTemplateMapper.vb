
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SSTTemplate Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 6/21/2013 - 11:17:27 AM
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

    Public Class AllocationRealtimeServiceTemplateMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAllocationRealtimeServiceTemplate"
        Private m_UpdateStatement As String = "up_UpdateAllocationRealtimeServiceTemplate"
        Private m_RetrieveStatement As String = "up_RetrieveAllocationRealtimeServiceTemplate"
        Private m_RetrieveListStatement As String = "up_RetrieveAllocationRealtimeServiceTemplateList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAllocationRealtimeServiceTemplate"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim allocationRealtimeServiceTemplate As AllocationRealtimeServiceTemplate = Nothing
            While dr.Read

                allocationRealtimeServiceTemplate = Me.CreateObject(dr)

            End While

            Return allocationRealtimeServiceTemplate

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim allocationRealtimeServiceTemplateList As ArrayList = New ArrayList

            While dr.Read
                Dim allocationRealtimeServiceTemplate As AllocationRealtimeServiceTemplate = Me.CreateObject(dr)
                allocationRealtimeServiceTemplateList.Add(allocationRealtimeServiceTemplate)
            End While

            Return allocationRealtimeServiceTemplateList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim allocationRealtimeServiceTemplate As AllocationRealtimeServiceTemplate = CType(obj, AllocationRealtimeServiceTemplate)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, allocationRealtimeServiceTemplate.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim allocationRealtimeServiceTemplate As AllocationRealtimeServiceTemplate = CType(obj, AllocationRealtimeServiceTemplate)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@ARSTemplateCategoryID", DbType.Int16, allocationRealtimeServiceTemplate.ARSTemplateCategoryID)
            DbCommandWrapper.AddInParameter("@ColNumber", DbType.Int16, allocationRealtimeServiceTemplate.ColNumber)
            DbCommandWrapper.AddInParameter("@ColTitle", DbType.AnsiString, allocationRealtimeServiceTemplate.ColTitle)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, allocationRealtimeServiceTemplate.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, allocationRealtimeServiceTemplate.LastUpdateBy)
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

            Dim allocationRealtimeServiceTemplate As AllocationRealtimeServiceTemplate = CType(obj, AllocationRealtimeServiceTemplate)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, allocationRealtimeServiceTemplate.ID)
            DbCommandWrapper.AddInParameter("@ARSTemplateCategoryID", DbType.Int16, allocationRealtimeServiceTemplate.ARSTemplateCategoryID)
            DbCommandWrapper.AddInParameter("@ColNumber", DbType.Int16, allocationRealtimeServiceTemplate.ColNumber)
            DbCommandWrapper.AddInParameter("@ColTitle", DbType.AnsiString, allocationRealtimeServiceTemplate.ColTitle)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, allocationRealtimeServiceTemplate.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, allocationRealtimeServiceTemplate.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AllocationRealtimeServiceTemplate

            Dim allocationRealtimeServiceTemplate As AllocationRealtimeServiceTemplate = New AllocationRealtimeServiceTemplate

            allocationRealtimeServiceTemplate.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ARSTemplateCategoryID")) Then allocationRealtimeServiceTemplate.ARSTemplateCategoryID = CType(dr("ARSTemplateCategoryID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ColNumber")) Then allocationRealtimeServiceTemplate.ColNumber = CType(dr("ColNumber"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ColTitle")) Then allocationRealtimeServiceTemplate.ColTitle = dr("ColTitle").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then allocationRealtimeServiceTemplate.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then allocationRealtimeServiceTemplate.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then allocationRealtimeServiceTemplate.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then allocationRealtimeServiceTemplate.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then allocationRealtimeServiceTemplate.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return allocationRealtimeServiceTemplate

        End Function

        Private Sub SetTableName()

            If Not (GetType(AllocationRealtimeServiceTemplate) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AllocationRealtimeServiceTemplate), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AllocationRealtimeServiceTemplate).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

