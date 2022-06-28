#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCertificateLine Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/17/2005 - 8:18:04 AM
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

    Public Class TrCertificateLineMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrCertificateLine"
        Private m_UpdateStatement As String = "up_UpdateTrCertificateLine"
        Private m_RetrieveStatement As String = "up_RetrieveTrCertificateLine"
        Private m_RetrieveListStatement As String = "up_RetrieveTrCertificateLineList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrCertificateLine"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trCertificateLine As TrCertificateLine = Nothing
            While dr.Read

                trCertificateLine = Me.CreateObject(dr)

            End While

            Return trCertificateLine

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trCertificateLineList As ArrayList = New ArrayList

            While dr.Read
                Dim trCertificateLine As TrCertificateLine = Me.CreateObject(dr)
                trCertificateLineList.Add(trCertificateLine)
            End While

            Return trCertificateLineList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCertificateLine As TrCertificateLine = CType(obj, TrCertificateLine)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCertificateLine.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCertificateLine As TrCertificateLine = CType(obj, TrCertificateLine)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@NumTestResult", DbType.Decimal, trCertificateLine.NumTestResult)
            DbCommandWrapper.AddInParameter("@CharTestResult", DbType.AnsiString, trCertificateLine.CharTestResult)
            DBCommandWrapper.AddInParameter("@EntryType", DbType.Int16, trCertificateLine.EntryType)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCertificateLine.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trCertificateLine.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CourseEvaluationID", DbType.Int32, Me.GetRefObject(trCertificateLine.TrCourseEvaluation))
            DbCommandWrapper.AddInParameter("@RegistrationID", DbType.Int32, Me.GetRefObject(trCertificateLine.TrClassRegistration))

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

            Dim trCertificateLine As TrCertificateLine = CType(obj, TrCertificateLine)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCertificateLine.ID)
            DbCommandWrapper.AddInParameter("@NumTestResult", DbType.Decimal, trCertificateLine.NumTestResult)
            DBCommandWrapper.AddInParameter("@CharTestResult", DbType.AnsiString, trCertificateLine.CharTestResult)
            DBCommandWrapper.AddInParameter("@EntryType", DbType.Int16, trCertificateLine.EntryType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCertificateLine.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trCertificateLine.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CourseEvaluationID", DbType.Int32, Me.GetRefObject(trCertificateLine.TrCourseEvaluation))
            DbCommandWrapper.AddInParameter("@RegistrationID", DbType.Int32, Me.GetRefObject(trCertificateLine.TrClassRegistration))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrCertificateLine

            Dim trCertificateLine As TrCertificateLine = New TrCertificateLine

            trCertificateLine.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NumTestResult")) Then trCertificateLine.NumTestResult = CType(dr("NumTestResult"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("CharTestResult")) Then trCertificateLine.CharTestResult = dr("CharTestResult").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EntryType")) Then trCertificateLine.EntryType = CType(dr("EntryType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trCertificateLine.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trCertificateLine.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trCertificateLine.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trCertificateLine.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trCertificateLine.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CourseEvaluationID")) Then
                trCertificateLine.TrCourseEvaluation = New TrCourseEvaluation(CType(dr("CourseEvaluationID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("RegistrationID")) Then
                trCertificateLine.TrClassRegistration = New TrClassRegistration(CType(dr("RegistrationID"), Integer))
            End If

            Return trCertificateLine

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrCertificateLine) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrCertificateLine), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrCertificateLine).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

