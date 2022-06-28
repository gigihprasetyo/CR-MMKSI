
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : JobPositionCategoryToArea Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 24/05/2019 - 14:16:25
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

    Public Class JobPositionCategoryToAreaMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertJobPositionCategoryToArea"
        Private m_UpdateStatement As String = "up_UpdateJobPositionCategoryToArea"
        Private m_RetrieveStatement As String = "up_RetrieveJobPositionCategoryToArea"
        Private m_RetrieveListStatement As String = "up_RetrieveJobPositionCategoryToAreaList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteJobPositionCategoryToArea"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim JobPositionCategoryToArea As JobPositionCategoryToArea = Nothing
            While dr.Read

                JobPositionCategoryToArea = Me.CreateObject(dr)

            End While

            Return JobPositionCategoryToArea

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim JobPositionCategoryToAreaList As ArrayList = New ArrayList

            While dr.Read
                Dim JobPositionCategoryToArea As JobPositionCategoryToArea = Me.CreateObject(dr)
                JobPositionCategoryToAreaList.Add(JobPositionCategoryToArea)
            End While

            Return JobPositionCategoryToAreaList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim JobPositionCategoryToArea As JobPositionCategoryToArea = CType(obj, JobPositionCategoryToArea)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, JobPositionCategoryToArea.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim JobPositionCategoryToArea As JobPositionCategoryToArea = CType(obj, JobPositionCategoryToArea)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, JobPositionCategoryToArea.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, JobPositionCategoryToArea.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, JobPositionCategoryToArea.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@JobPositionCategoryAreaID", DbType.Int32, JobPositionCategoryToArea.JobPositionCategoryAreaID)
            DbCommandWrapper.AddInParameter("@JobPositionCategoryID", DbType.Int32, JobPositionCategoryToArea.JobPositionCategoryID)

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

            Dim JobPositionCategoryToArea As JobPositionCategoryToArea = CType(obj, JobPositionCategoryToArea)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, JobPositionCategoryToArea.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, JobPositionCategoryToArea.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, JobPositionCategoryToArea.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, JobPositionCategoryToArea.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@JobPositionCategoryAreaID", DbType.Int32, JobPositionCategoryToArea.JobPositionCategoryAreaID)
            DbCommandWrapper.AddInParameter("@JobPositionCategoryID", DbType.Int32, JobPositionCategoryToArea.JobPositionCategoryID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As JobPositionCategoryToArea

            Dim JobPositionCategoryToArea As JobPositionCategoryToArea = New JobPositionCategoryToArea

            JobPositionCategoryToArea.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then JobPositionCategoryToArea.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then JobPositionCategoryToArea.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then JobPositionCategoryToArea.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then JobPositionCategoryToArea.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then JobPositionCategoryToArea.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then JobPositionCategoryToArea.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionCategoryAreaID")) Then
                JobPositionCategoryToArea.JobPositionCategoryAreaID = CType(dr("JobPositionCategoryAreaID"), Integer)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionCategoryID")) Then
                JobPositionCategoryToArea.JobPositionCategoryID = CType(dr("JobPositionCategoryID"), Integer)
            End If

            Return JobPositionCategoryToArea

        End Function

        Private Sub SetTableName()

            If Not (GetType(JobPositionCategoryToArea) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(JobPositionCategoryToArea), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(JobPositionCategoryToArea).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

