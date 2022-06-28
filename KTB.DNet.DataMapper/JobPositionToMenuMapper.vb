
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : JobPositionToMenu Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 7/14/2010 - 8:56:41 AM
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

    Public Class JobPositionToMenuMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertJobPositionToMenu"
        Private m_UpdateStatement As String = "up_UpdateJobPositionToMenu"
        Private m_RetrieveStatement As String = "up_RetrieveJobPositionToMenu"
        Private m_RetrieveListStatement As String = "up_RetrieveJobPositionToMenuList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteJobPositionToMenu"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim jobPositionToMenu As JobPositionToMenu = Nothing
            While dr.Read

                jobPositionToMenu = Me.CreateObject(dr)

            End While

            Return jobPositionToMenu

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim jobPositionToMenuList As ArrayList = New ArrayList

            While dr.Read
                Dim jobPositionToMenu As JobPositionToMenu = Me.CreateObject(dr)
                jobPositionToMenuList.Add(jobPositionToMenu)
            End While

            Return jobPositionToMenuList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim jobPositionToMenu As JobPositionToMenu = CType(obj, JobPositionToMenu)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, jobPositionToMenu.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim jobPositionToMenu As JobPositionToMenu = CType(obj, JobPositionToMenu)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, jobPositionToMenu.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, User)
            'DBCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, jobPositionToMenu.CreatedTime)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, jobPositionToMenu.LastUpdateBy)
            'DBCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, jobPositionToMenu.LastUpdateTime)

            DBCommandWrapper.AddInParameter("@JobPositionID", DbType.Int32, Me.GetRefObject(jobPositionToMenu.JobPosition))
            DBCommandWrapper.AddInParameter("@JobPositionMenuId", DbType.Int32, Me.GetRefObject(jobPositionToMenu.JobPositionMenu))

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

            Dim jobPositionToMenu As JobPositionToMenu = CType(obj, JobPositionToMenu)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, jobPositionToMenu.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, jobPositionToMenu.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, jobPositionToMenu.CreatedBy)
            'DBCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, jobPositionToMenu.CreatedTime)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, jobPositionToMenu.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@JobPositionID", DbType.Int32, Me.GetRefObject(jobPositionToMenu.JobPosition))
            DBCommandWrapper.AddInParameter("@JobPositionMenuId", DbType.Int32, Me.GetRefObject(jobPositionToMenu.JobPositionMenu))

            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As JobPositionToMenu

            Dim jobPositionToMenu As jobPositionToMenu = New jobPositionToMenu

            jobPositionToMenu.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then jobPositionToMenu.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then jobPositionToMenu.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then jobPositionToMenu.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then jobPositionToMenu.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then jobPositionToMenu.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionID")) Then
                jobPositionToMenu.JobPosition = New JobPosition(CType(dr("JobPositionID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionMenuId")) Then
                jobPositionToMenu.JobPositionMenu = New JobPositionMenu(CType(dr("JobPositionMenuId"), Integer))
            End If

            Return jobPositionToMenu

        End Function

        Private Sub SetTableName()

            If Not (GetType(JobPositionToMenu) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(JobPositionToMenu), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(JobPositionToMenu).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

