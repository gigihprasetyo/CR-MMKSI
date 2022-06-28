#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCourseEvaluation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/15/2005 - 8:14:51 AM
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

    Public Class TrCourseEvaluationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrCourseEvaluation"
        Private m_UpdateStatement As String = "up_UpdateTrCourseEvaluation"
        Private m_RetrieveStatement As String = "up_RetrieveTrCourseEvaluation"
        Private m_RetrieveListStatement As String = "up_RetrieveTrCourseEvaluationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrCourseEvaluation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trCourseEvaluation As TrCourseEvaluation = Nothing
            While dr.Read

                trCourseEvaluation = Me.CreateObject(dr)

            End While

            Return trCourseEvaluation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trCourseEvaluationList As ArrayList = New ArrayList

            While dr.Read
                Dim trCourseEvaluation As TrCourseEvaluation = Me.CreateObject(dr)
                trCourseEvaluationList.Add(trCourseEvaluation)
            End While

            Return trCourseEvaluationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCourseEvaluation As TrCourseEvaluation = CType(obj, TrCourseEvaluation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCourseEvaluation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCourseEvaluation As TrCourseEvaluation = CType(obj, TrCourseEvaluation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@EvaluationCode", DbType.AnsiString, trCourseEvaluation.EvaluationCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, trCourseEvaluation.Name)
            DbCommandWrapper.AddInParameter("@Type", DbType.AnsiString, trCourseEvaluation.Type)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trCourseEvaluation.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCourseEvaluation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trCourseEvaluation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CourseID", DbType.Int32, Me.GetRefObject(trCourseEvaluation.TrCourse))

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

            Dim trCourseEvaluation As TrCourseEvaluation = CType(obj, TrCourseEvaluation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCourseEvaluation.ID)
            DbCommandWrapper.AddInParameter("@EvaluationCode", DbType.AnsiString, trCourseEvaluation.EvaluationCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, trCourseEvaluation.Name)
            DbCommandWrapper.AddInParameter("@Type", DbType.AnsiString, trCourseEvaluation.Type)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trCourseEvaluation.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCourseEvaluation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trCourseEvaluation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CourseID", DbType.Int32, Me.GetRefObject(trCourseEvaluation.TrCourse))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrCourseEvaluation

            Dim trCourseEvaluation As TrCourseEvaluation = New TrCourseEvaluation

            trCourseEvaluation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EvaluationCode")) Then trCourseEvaluation.EvaluationCode = dr("EvaluationCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then trCourseEvaluation.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then trCourseEvaluation.Type = dr("Type").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then trCourseEvaluation.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trCourseEvaluation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trCourseEvaluation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trCourseEvaluation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trCourseEvaluation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trCourseEvaluation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CourseID")) Then
                trCourseEvaluation.TrCourse = New TrCourse(CType(dr("CourseID"), Integer))
            End If

            Return trCourseEvaluation

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrCourseEvaluation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrCourseEvaluation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrCourseEvaluation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

