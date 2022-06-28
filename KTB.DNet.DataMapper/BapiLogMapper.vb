
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BapiLog Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 22/02/2019 - 11:03:20
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

    Public Class BapiLogMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBapiLog"
        Private m_UpdateStatement As String = "up_UpdateBapiLog"
        Private m_RetrieveStatement As String = "up_RetrieveBapiLog"
        Private m_RetrieveListStatement As String = "up_RetrieveBapiLogList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBapiLog"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim bapiLog As BapiLog = Nothing
            While dr.Read

                bapiLog = Me.CreateObject(dr)

            End While

            Return bapiLog

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim bapiLogList As ArrayList = New ArrayList

            While dr.Read
                Dim bapiLog As BapiLog = Me.CreateObject(dr)
                bapiLogList.Add(bapiLog)
            End While

            Return bapiLogList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bapiLog As BapiLog = CType(obj, BapiLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bapiLog.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bapiLog As BapiLog = CType(obj, BapiLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SubmitDate", DbType.DateTime, bapiLog.SubmitDate)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, bapiLog.UserName)
            DbCommandWrapper.AddInParameter("@KindLog", DbType.Int16, bapiLog.KindLog)
            DbCommandWrapper.AddInParameter("@Body", DbType.AnsiString, bapiLog.Body)
            DbCommandWrapper.AddInParameter("@Status", DbType.Boolean, bapiLog.Status)
            DbCommandWrapper.AddInParameter("@ResponseLog", DbType.AnsiString, bapiLog.ResponseLog)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bapiLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, bapiLog.LastUpdateBy)
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

            Dim bapiLog As BapiLog = CType(obj, BapiLog)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bapiLog.ID)
            DbCommandWrapper.AddInParameter("@SubmitDate", DbType.DateTime, bapiLog.SubmitDate)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, bapiLog.UserName)
            DbCommandWrapper.AddInParameter("@KindLog", DbType.Int16, bapiLog.KindLog)
            DbCommandWrapper.AddInParameter("@Body", DbType.AnsiString, bapiLog.Body)
            DbCommandWrapper.AddInParameter("@Status", DbType.Boolean, bapiLog.Status)
            DbCommandWrapper.AddInParameter("@ResponseLog", DbType.AnsiString, bapiLog.ResponseLog)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bapiLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, bapiLog.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BapiLog

            Dim bapiLog As BapiLog = New BapiLog

            bapiLog.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SubmitDate")) Then bapiLog.SubmitDate = CType(dr("SubmitDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UserName")) Then bapiLog.UserName = dr("UserName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindLog")) Then bapiLog.KindLog = CType(dr("KindLog"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Body")) Then bapiLog.Body = dr("Body").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then bapiLog.Status = CType(dr("Status"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseLog")) Then bapiLog.ResponseLog = dr("ResponseLog").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then bapiLog.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then bapiLog.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then bapiLog.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then bapiLog.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then bapiLog.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return bapiLog

        End Function

        Private Sub SetTableName()

            If Not (GetType(BapiLog) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BapiLog), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BapiLog).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

