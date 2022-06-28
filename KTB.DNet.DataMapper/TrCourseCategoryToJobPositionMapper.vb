
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCourseCategoryToJobPosition Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 20/06/2019 - 8:52:04
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

    Public Class TrCourseCategoryToJobPositionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrCourseCategoryToJobPosition"
        Private m_UpdateStatement As String = "up_UpdateTrCourseCategoryToJobPosition"
        Private m_RetrieveStatement As String = "up_RetrieveTrCourseCategoryToJobPosition"
        Private m_RetrieveListStatement As String = "up_RetrieveTrCourseCategoryToJobPositionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrCourseCategoryToJobPosition"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trCourseCategoryToJobPosition As TrCourseCategoryToJobPosition = Nothing
            While dr.Read

                trCourseCategoryToJobPosition = Me.CreateObject(dr)

            End While

            Return trCourseCategoryToJobPosition

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trCourseCategoryToJobPositionList As ArrayList = New ArrayList

            While dr.Read
                Dim trCourseCategoryToJobPosition As TrCourseCategoryToJobPosition = Me.CreateObject(dr)
                trCourseCategoryToJobPositionList.Add(trCourseCategoryToJobPosition)
            End While

            Return trCourseCategoryToJobPositionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCourseCategoryToJobPosition As TrCourseCategoryToJobPosition = CType(obj, TrCourseCategoryToJobPosition)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCourseCategoryToJobPosition.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCourseCategoryToJobPosition As TrCourseCategoryToJobPosition = CType(obj, TrCourseCategoryToJobPosition)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCourseCategoryToJobPosition.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trCourseCategoryToJobPosition.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@TrCourseCategoryID", DbType.Int32, Me.GetRefObject(trCourseCategoryToJobPosition.TrCourseCategory))
            DbCommandWrapper.AddInParameter("@JobPositionID", DbType.Int32, Me.GetRefObject(trCourseCategoryToJobPosition.JobPosition))

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

            Dim trCourseCategoryToJobPosition As TrCourseCategoryToJobPosition = CType(obj, TrCourseCategoryToJobPosition)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCourseCategoryToJobPosition.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCourseCategoryToJobPosition.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trCourseCategoryToJobPosition.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@TrCourseCategoryID", DbType.Int32, Me.GetRefObject(trCourseCategoryToJobPosition.TrCourseCategory))
            DbCommandWrapper.AddInParameter("@JobPositionID", DbType.Int32, Me.GetRefObject(trCourseCategoryToJobPosition.JobPosition))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrCourseCategoryToJobPosition

            Dim trCourseCategoryToJobPosition As TrCourseCategoryToJobPosition = New TrCourseCategoryToJobPosition

            trCourseCategoryToJobPosition.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trCourseCategoryToJobPosition.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trCourseCategoryToJobPosition.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trCourseCategoryToJobPosition.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trCourseCategoryToJobPosition.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trCourseCategoryToJobPosition.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TrCourseCategoryID")) Then
                trCourseCategoryToJobPosition.TrCourseCategory = New TrCourseCategory(CType(dr("TrCourseCategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionID")) Then
                trCourseCategoryToJobPosition.JobPosition = New JobPosition(CType(dr("JobPositionID"), Integer))
            End If

            Return trCourseCategoryToJobPosition

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrCourseCategoryToJobPosition) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrCourseCategoryToJobPosition), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrCourseCategoryToJobPosition).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

