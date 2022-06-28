
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCourseCategory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 20/06/2019 - 8:49:17
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

    Public Class TrCourseCategoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrCourseCategory"
        Private m_UpdateStatement As String = "up_UpdateTrCourseCategory"
        Private m_RetrieveStatement As String = "up_RetrieveTrCourseCategory"
        Private m_RetrieveListStatement As String = "up_RetrieveTrCourseCategoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrCourseCategory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trCourseCategory As TrCourseCategory = Nothing
            While dr.Read

                trCourseCategory = Me.CreateObject(dr)

            End While

            Return trCourseCategory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trCourseCategoryList As ArrayList = New ArrayList

            While dr.Read
                Dim trCourseCategory As TrCourseCategory = Me.CreateObject(dr)
                trCourseCategoryList.Add(trCourseCategory)
            End While

            Return trCourseCategoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCourseCategory As TrCourseCategory = CType(obj, TrCourseCategory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCourseCategory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCourseCategory As TrCourseCategory = CType(obj, TrCourseCategory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, trCourseCategory.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trCourseCategory.Description)
            DbCommandWrapper.AddInParameter("@IsMandatory", DbType.Boolean, trCourseCategory.IsMandatory)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trCourseCategory.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCourseCategory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trCourseCategory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@SalesmanLevelID", DbType.Int32, Me.GetRefObject(trCourseCategory.SalesmanLevel))
            DbCommandWrapper.AddInParameter("@JobPositionCategoryID", DbType.Int32, Me.GetRefObject(trCourseCategory.JobPositionCategory))

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

            Dim trCourseCategory As TrCourseCategory = CType(obj, TrCourseCategory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCourseCategory.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, trCourseCategory.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trCourseCategory.Description)
            DbCommandWrapper.AddInParameter("@IsMandatory", DbType.Boolean, trCourseCategory.IsMandatory)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trCourseCategory.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCourseCategory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trCourseCategory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@SalesmanLevelID", DbType.Int32, Me.GetRefObject(trCourseCategory.SalesmanLevel))
            DbCommandWrapper.AddInParameter("@JobPositionCategoryID", DbType.Int32, Me.GetRefObject(trCourseCategory.JobPositionCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrCourseCategory

            Dim trCourseCategory As TrCourseCategory = New TrCourseCategory

            trCourseCategory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then trCourseCategory.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then trCourseCategory.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsMandatory")) Then trCourseCategory.IsMandatory = CType(dr("IsMandatory"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trCourseCategory.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trCourseCategory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trCourseCategory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trCourseCategory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trCourseCategory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trCourseCategory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanLevelID")) Then
                trCourseCategory.SalesmanLevel = New SalesmanLevel(CType(dr("SalesmanLevelID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionCategoryID")) Then
                trCourseCategory.JobPositionCategory = New JobPositionCategory(CType(dr("JobPositionCategoryID"), Integer))
            End If

            Return trCourseCategory

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrCourseCategory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrCourseCategory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrCourseCategory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

