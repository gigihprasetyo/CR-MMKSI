
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : JobPositionMenu Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 7/14/2010 - 8:54:07 AM
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

    Public Class JobPositionMenuMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertJobPositionMenu"
        Private m_UpdateStatement As String = "up_UpdateJobPositionMenu"
        Private m_RetrieveStatement As String = "up_RetrieveJobPositionMenu"
        Private m_RetrieveListStatement As String = "up_RetrieveJobPositionMenuList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteJobPositionMenu"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim jobPositionMenu As JobPositionMenu = Nothing
            While dr.Read

                jobPositionMenu = Me.CreateObject(dr)

            End While

            Return jobPositionMenu

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim jobPositionMenuList As ArrayList = New ArrayList

            While dr.Read
                Dim jobPositionMenu As JobPositionMenu = Me.CreateObject(dr)
                jobPositionMenuList.Add(jobPositionMenu)
            End While

            Return jobPositionMenuList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim jobPositionMenu As JobPositionMenu = CType(obj, JobPositionMenu)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, jobPositionMenu.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim jobPositionMenu As JobPositionMenu = CType(obj, JobPositionMenu)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Name", DbType.String, jobPositionMenu.Name)
            DbCommandWrapper.AddInParameter("@Category", DbType.Byte, jobPositionMenu.Category)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, jobPositionMenu.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            DBCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, jobPositionMenu.CreatedTime)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, jobPositionMenu.LastUpdateBy)
            DBCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, jobPositionMenu.LastUpdateTime)


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

            Dim jobPositionMenu As JobPositionMenu = CType(obj, JobPositionMenu)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, jobPositionMenu.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.String, jobPositionMenu.Name)
            DbCommandWrapper.AddInParameter("@Category", DbType.Byte, jobPositionMenu.Category)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, jobPositionMenu.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, jobPositionMenu.CreatedBy)
            DBCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, jobPositionMenu.CreatedTime)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, jobPositionMenu.LastUpdateBy)
            DBCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, jobPositionMenu.LastUpdateTime)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As JobPositionMenu

            Dim jobPositionMenu As JobPositionMenu = New JobPositionMenu

            jobPositionMenu.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then jobPositionMenu.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then jobPositionMenu.Category = CType(dr("Category"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then jobPositionMenu.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then jobPositionMenu.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then jobPositionMenu.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then jobPositionMenu.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then jobPositionMenu.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return jobPositionMenu

        End Function

        Private Sub SetTableName()

            If Not (GetType(JobPositionMenu) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(JobPositionMenu), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(JobPositionMenu).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

