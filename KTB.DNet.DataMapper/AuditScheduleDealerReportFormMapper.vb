#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AuditScheduleDealerReportForm Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 29/09/2007 - 15:53:24
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

    Public Class AuditScheduleDealerReportFormMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAuditScheduleDealerReportForm"
        Private m_UpdateStatement As String = "up_UpdateAuditScheduleDealerReportForm"
        Private m_RetrieveStatement As String = "up_RetrieveAuditScheduleDealerReportForm"
        Private m_RetrieveListStatement As String = "up_RetrieveAuditScheduleDealerReportFormList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAuditScheduleDealerReportForm"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim auditScheduleDealerReportForm As AuditScheduleDealerReportForm = Nothing
            While dr.Read

                auditScheduleDealerReportForm = Me.CreateObject(dr)

            End While

            Return auditScheduleDealerReportForm

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim auditScheduleDealerReportFormList As ArrayList = New ArrayList

            While dr.Read
                Dim auditScheduleDealerReportForm As AuditScheduleDealerReportForm = Me.CreateObject(dr)
                auditScheduleDealerReportFormList.Add(auditScheduleDealerReportForm)
            End While

            Return auditScheduleDealerReportFormList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim auditScheduleDealerReportForm As AuditScheduleDealerReportForm = CType(obj, AuditScheduleDealerReportForm)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, auditScheduleDealerReportForm.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim auditScheduleDealerReportForm As AuditScheduleDealerReportForm = CType(obj, AuditScheduleDealerReportForm)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@FormFile", DbType.AnsiString, auditScheduleDealerReportForm.FormFile)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, auditScheduleDealerReportForm.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, auditScheduleDealerReportForm.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@AuditScheduleDealerID", DbType.Int32, Me.GetRefObject(auditScheduleDealerReportForm.AuditScheduleDealer))

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

            Dim auditScheduleDealerReportForm As AuditScheduleDealerReportForm = CType(obj, AuditScheduleDealerReportForm)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, auditScheduleDealerReportForm.ID)
            DbCommandWrapper.AddInParameter("@FormFile", DbType.AnsiString, auditScheduleDealerReportForm.FormFile)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, auditScheduleDealerReportForm.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, auditScheduleDealerReportForm.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@AuditScheduleDealerID", DbType.Int32, Me.GetRefObject(auditScheduleDealerReportForm.AuditScheduleDealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AuditScheduleDealerReportForm

            Dim auditScheduleDealerReportForm As AuditScheduleDealerReportForm = New AuditScheduleDealerReportForm

            auditScheduleDealerReportForm.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FormFile")) Then auditScheduleDealerReportForm.FormFile = dr("FormFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then auditScheduleDealerReportForm.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then auditScheduleDealerReportForm.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then auditScheduleDealerReportForm.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then auditScheduleDealerReportForm.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then auditScheduleDealerReportForm.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AuditScheduleDealerID")) Then
                auditScheduleDealerReportForm.AuditScheduleDealer = New AuditScheduleDealer(CType(dr("AuditScheduleDealerID"), Integer))
            End If

            Return auditScheduleDealerReportForm

        End Function

        Private Sub SetTableName()

            If Not (GetType(AuditScheduleDealerReportForm) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AuditScheduleDealerReportForm), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AuditScheduleDealerReportForm).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

