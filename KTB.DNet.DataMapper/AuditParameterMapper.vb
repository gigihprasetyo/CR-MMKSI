#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AuditParameter Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2007 - 2:15:34 PM
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

    Public Class AuditParameterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAuditParameter"
        Private m_UpdateStatement As String = "up_UpdateAuditParameter"
        Private m_RetrieveStatement As String = "up_RetrieveAuditParameter"
        Private m_RetrieveListStatement As String = "up_RetrieveAuditParameterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAuditParameter"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim auditParameter As AuditParameter = Nothing
            While dr.Read

                auditParameter = Me.CreateObject(dr)

            End While

            Return auditParameter

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim auditParameterList As ArrayList = New ArrayList

            While dr.Read
                Dim auditParameter As AuditParameter = Me.CreateObject(dr)
                auditParameterList.Add(auditParameter)
            End While

            Return auditParameterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim auditParameter As AuditParameter = CType(obj, AuditParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, auditParameter.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim auditParameter As AuditParameter = CType(obj, AuditParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, auditParameter.Code)
            DbCommandWrapper.AddInParameter("@Period", DbType.Int16, auditParameter.Period)
            DbCommandWrapper.AddInParameter("@JukLakFile", DbType.AnsiString, auditParameter.JukLakFile)
            DbCommandWrapper.AddInParameter("@AssessmentItem", DbType.AnsiString, auditParameter.AssessmentItem)
            DbCommandWrapper.AddInParameter("@IsRilis", DbType.Byte, auditParameter.IsRilis)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, auditParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, auditParameter.LastUpdateBy)
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

            Dim auditParameter As AuditParameter = CType(obj, AuditParameter)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, auditParameter.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, auditParameter.Code)
            DbCommandWrapper.AddInParameter("@Period", DbType.Int16, auditParameter.Period)
            DbCommandWrapper.AddInParameter("@JukLakFile", DbType.AnsiString, auditParameter.JukLakFile)
            DbCommandWrapper.AddInParameter("@AssessmentItem", DbType.AnsiString, auditParameter.AssessmentItem)
            DbCommandWrapper.AddInParameter("@IsRilis", DbType.Byte, auditParameter.IsRilis)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, auditParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, auditParameter.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AuditParameter

            Dim auditParameter As AuditParameter = New AuditParameter

            auditParameter.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then auditParameter.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Period")) Then auditParameter.Period = CType(dr("Period"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("JukLakFile")) Then auditParameter.JukLakFile = dr("JukLakFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AssessmentItem")) Then auditParameter.AssessmentItem = dr("AssessmentItem").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsRilis")) Then auditParameter.IsRilis = CType(dr("IsRilis"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then auditParameter.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then auditParameter.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then auditParameter.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then auditParameter.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then auditParameter.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return auditParameter

        End Function

        Private Sub SetTableName()

            If Not (GetType(AuditParameter) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AuditParameter), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AuditParameter).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

