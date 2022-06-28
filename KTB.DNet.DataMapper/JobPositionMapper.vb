#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : JobPosition Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/5/2007 - 4:45:11 PM
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

    Public Class JobPositionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertJobPosition"
        Private m_UpdateStatement As String = "up_UpdateJobPosition"
        Private m_RetrieveStatement As String = "up_RetrieveJobPosition"
        Private m_RetrieveListStatement As String = "up_RetrieveJobPositionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteJobPosition"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim jobPosition As JobPosition = Nothing
            While dr.Read

                jobPosition = Me.CreateObject(dr)

            End While

            Return jobPosition

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim jobPositionList As ArrayList = New ArrayList

            While dr.Read
                Dim jobPosition As JobPosition = Me.CreateObject(dr)
                jobPositionList.Add(jobPosition)
            End While

            Return jobPositionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim jobPosition As JobPosition = CType(obj, JobPosition)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, jobPosition.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim jobPosition As JobPosition = CType(obj, JobPosition)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)
            'DbCommandWrapper.AddInParameter("@IsNeedSuperior", DbType.Boolean, jobPosition.IsNeedSuperior)
            'DbCommandWrapper.AddInParameter("@Status", DbType.Byte, jobPosition.Status)
            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, jobPosition.Code)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, jobPosition.Description)
            DBCommandWrapper.AddInParameter("@Category", DbType.Int32, jobPosition.Category)
            DbCommandWrapper.AddInParameter("@SalesTarget", DbType.Int32, jobPosition.SalesTarget)
            'DbCommandWrapper.AddInParameter("@Level", DbType.Int32, jobPosition.Level)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, jobPosition.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, jobPosition.LastUpdateBy)
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

            Dim jobPosition As JobPosition = CType(obj, JobPosition)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            'DbCommandWrapper.AddInParameter("@Status", DbType.Byte, jobPosition.Status)
            'DbCommandWrapper.AddInParameter("@IsNeedSuperior", DbType.Boolean, jobPosition.IsNeedSuperior)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, jobPosition.ID)
            DBCommandWrapper.AddInParameter("@Code", DbType.AnsiString, jobPosition.Code)
            DBCommandWrapper.AddInParameter("@Category", DbType.Int32, jobPosition.Category)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, jobPosition.Description)
            DbCommandWrapper.AddInParameter("@SalesTarget", DbType.Int32, jobPosition.SalesTarget)
            'DbCommandWrapper.AddInParameter("@Level", DbType.Int32, jobPosition.Level)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, jobPosition.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, jobPosition.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As JobPosition

            Dim jobPosition As JobPosition = New JobPosition

            jobPosition.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then jobPosition.Code = dr("Code").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then jobPosition.Status = CType(dr("Status"), Short)
            'If Not dr.IsDBNull(dr.GetOrdinal("IsNeedSuperior")) Then jobPosition.IsNeedSuperior = CType(dr("IsNeedSuperior"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then jobPosition.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then jobPosition.Category = CType(dr("Category"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesTarget")) Then jobPosition.SalesTarget = CType(dr("SalesTarget"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then jobPosition.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then jobPosition.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then jobPosition.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then jobPosition.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then jobPosition.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("Level")) Then jobPosition.Level = CType(dr("Level"), Integer)

            Return jobPosition

        End Function

        Private Sub SetTableName()

            If Not (GetType(JobPosition) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(JobPosition), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(JobPosition).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

