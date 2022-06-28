
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCourse Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/3/2006 - 11:00:57 AM
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

    Public Class TrCourseMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrCourse"
        Private m_UpdateStatement As String = "up_UpdateTrCourse"
        Private m_RetrieveStatement As String = "up_RetrieveTrCourse"
        Private m_RetrieveListStatement As String = "up_RetrieveTrCourseList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrCourse"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trCourse As TrCourse = Nothing
            While dr.Read

                trCourse = Me.CreateObject(dr)

            End While

            Return trCourse

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trCourseList As ArrayList = New ArrayList

            While dr.Read
                Dim trCourse As TrCourse = Me.CreateObject(dr)
                trCourseList.Add(trCourse)
            End While

            Return trCourseList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCourse As TrCourse = CType(obj, TrCourse)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCourse.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCourse As TrCourse = CType(obj, TrCourse)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CourseCode", DbType.AnsiString, trCourse.CourseCode)
            DbCommandWrapper.AddInParameter("@CourseName", DbType.AnsiString, trCourse.CourseName)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trCourse.Description)
            DbCommandWrapper.AddInParameter("@RequireWorkDate", DbType.Boolean, trCourse.RequireWorkDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trCourse.Status)
            DbCommandWrapper.AddInParameter("@PassingScore", DbType.Decimal, trCourse.PassingScore)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, trCourse.Notes)
            DbCommandWrapper.AddInParameter("@ClassCode", DbType.AnsiString, trCourse.ClassCode)
            DbCommandWrapper.AddInParameter("@WorkDate", DbType.Int32, trCourse.WorkDate)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, trCourse.PaymentType)
            DbCommandWrapper.AddInParameter("@TrTraineeLevelID", DbType.Int32, Me.GetRefObject(trCourse.TrTraineeLevel))
            DbCommandWrapper.AddInParameter("@Category", DbType.Int32, Me.GetRefObject(trCourse.Category))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCourse.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trCourse.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@JobPositionCategoryID", DbType.Int32, Me.GetRefObject(trCourse.JobPositionCategory))


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

            Dim trCourse As TrCourse = CType(obj, TrCourse)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCourse.ID)
            DbCommandWrapper.AddInParameter("@CourseCode", DbType.AnsiString, trCourse.CourseCode)
            DbCommandWrapper.AddInParameter("@CourseName", DbType.AnsiString, trCourse.CourseName)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trCourse.Description)
            DbCommandWrapper.AddInParameter("@RequireWorkDate", DbType.Boolean, trCourse.RequireWorkDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trCourse.Status)
            DbCommandWrapper.AddInParameter("@PassingScore", DbType.Decimal, trCourse.PassingScore)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, trCourse.Notes)
            DbCommandWrapper.AddInParameter("@ClassCode", DbType.AnsiString, trCourse.ClassCode)
            DbCommandWrapper.AddInParameter("@WorkDate", DbType.Int32, trCourse.WorkDate)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, trCourse.PaymentType)
            DbCommandWrapper.AddInParameter("@TrTraineeLevelID", DbType.Int32, Me.GetRefObject(trCourse.TrTraineeLevel))
            DbCommandWrapper.AddInParameter("@Category", DbType.Int32, Me.GetRefObject(trCourse.Category))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCourse.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trCourse.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@JobPositionCategoryID", DbType.Int32, Me.GetRefObject(trCourse.JobPositionCategory))



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrCourse

            Dim trCourse As TrCourse = New TrCourse

            trCourse.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CourseCode")) Then trCourse.CourseCode = dr("CourseCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CourseName")) Then trCourse.CourseName = dr("CourseName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then trCourse.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RequireWorkDate")) Then trCourse.RequireWorkDate = CType(dr("RequireWorkDate"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trCourse.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PassingScore")) Then trCourse.PassingScore = CType(dr("PassingScore"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then trCourse.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClassCode")) Then trCourse.ClassCode = dr("ClassCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkDate")) Then trCourse.WorkDate = CType(dr("WorkDate"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then trCourse.PaymentType = CType(dr("PaymentType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineeLevelID")) Then trCourse.TrTraineeLevel = New TrTraineeLevel(CType(dr("TrTraineeLevelID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then trCourse.Category = New TrCourseCategory(CType(dr("Category"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trCourse.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trCourse.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trCourse.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trCourse.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trCourse.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionCategoryID")) Then
                trCourse.JobPositionCategory = New JobPositionCategory(CType(dr("JobPositionCategoryID"), Integer))
            End If

            Return trCourse

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrCourse) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrCourse), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrCourse).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

