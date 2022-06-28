
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrClassNumEvaluation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 12/21/2009 - 9:38:56 AM
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

    Public Class TrClassNumEvaluationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrClassNumEvaluation"
        Private m_UpdateStatement As String = "up_UpdateTrClassNumEvaluation"
        Private m_RetrieveStatement As String = "up_RetrieveTrClassNumEvaluation"
        Private m_RetrieveListStatement As String = "up_RetrieveTrClassNumEvaluationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrClassNumEvaluation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trClassNumEvaluation As TrClassNumEvaluation = Nothing
            While dr.Read

                trClassNumEvaluation = Me.CreateObject(dr)

            End While

            Return trClassNumEvaluation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trClassNumEvaluationList As ArrayList = New ArrayList

            While dr.Read
                Dim trClassNumEvaluation As TrClassNumEvaluation = Me.CreateObject(dr)
                trClassNumEvaluationList.Add(trClassNumEvaluation)
            End While

            Return trClassNumEvaluationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trClassNumEvaluation As TrClassNumEvaluation = CType(obj, TrClassNumEvaluation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trClassNumEvaluation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trClassNumEvaluation As TrClassNumEvaluation = CType(obj, TrClassNumEvaluation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DBCommandWrapper.AddInParameter("@SpecialName", DbType.AnsiString, trClassNumEvaluation.SpecialName)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trClassNumEvaluation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trClassNumEvaluation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ClassID", DbType.Int32, Me.GetRefObject(trClassNumEvaluation.TrClass))
            DbCommandWrapper.AddInParameter("@CourseEvaluationID", DbType.Int32, Me.GetRefObject(trClassNumEvaluation.TrCourseEvaluation))

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

            Dim trClassNumEvaluation As TrClassNumEvaluation = CType(obj, TrClassNumEvaluation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trClassNumEvaluation.ID)
            DBCommandWrapper.AddInParameter("@SpecialName", DbType.AnsiString, trClassNumEvaluation.SpecialName)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trClassNumEvaluation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trClassNumEvaluation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ClassID", DbType.Int32, Me.GetRefObject(trClassNumEvaluation.TrClass))
            DbCommandWrapper.AddInParameter("@CourseEvaluationID", DbType.Int32, Me.GetRefObject(trClassNumEvaluation.TrCourseEvaluation))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrClassNumEvaluation

            Dim trClassNumEvaluation As TrClassNumEvaluation = New TrClassNumEvaluation

            trClassNumEvaluation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SpecialName")) Then trClassNumEvaluation.SpecialName = dr("SpecialName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trClassNumEvaluation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trClassNumEvaluation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trClassNumEvaluation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trClassNumEvaluation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trClassNumEvaluation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ClassID")) Then
                trClassNumEvaluation.TrClass = New TrClass(CType(dr("ClassID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CourseEvaluationID")) Then
                trClassNumEvaluation.TrCourseEvaluation = New TrCourseEvaluation(CType(dr("CourseEvaluationID"), Integer))
            End If

            Return trClassNumEvaluation

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrClassNumEvaluation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrClassNumEvaluation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrClassNumEvaluation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

