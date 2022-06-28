#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AuditScheduleAuditor Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2007 - 12:50:49 PM
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

    Public Class AuditScheduleAuditorMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAuditScheduleAuditor"
        Private m_UpdateStatement As String = "up_UpdateAuditScheduleAuditor"
        Private m_RetrieveStatement As String = "up_RetrieveAuditScheduleAuditor"
        Private m_RetrieveListStatement As String = "up_RetrieveAuditScheduleAuditorList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAuditScheduleAuditor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim auditScheduleAuditor As AuditScheduleAuditor = Nothing
            While dr.Read

                auditScheduleAuditor = Me.CreateObject(dr)

            End While

            Return auditScheduleAuditor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim auditScheduleAuditorList As ArrayList = New ArrayList

            While dr.Read
                Dim auditScheduleAuditor As AuditScheduleAuditor = Me.CreateObject(dr)
                auditScheduleAuditorList.Add(auditScheduleAuditor)
            End While

            Return auditScheduleAuditorList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim auditScheduleAuditor As AuditScheduleAuditor = CType(obj, AuditScheduleAuditor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, auditScheduleAuditor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim auditScheduleAuditor As AuditScheduleAuditor = CType(obj, AuditScheduleAuditor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@AuditorType", DbType.Byte, auditScheduleAuditor.AuditorType)
            DbCommandWrapper.AddInParameter("@Auditor", DbType.AnsiString, auditScheduleAuditor.Auditor)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, auditScheduleAuditor.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, auditScheduleAuditor.EndDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, auditScheduleAuditor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, auditScheduleAuditor.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@AuditScheduleID", DbType.Int32, Me.GetRefObject(auditScheduleAuditor.AuditSchedule))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(auditScheduleAuditor.Dealer))

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

            Dim auditScheduleAuditor As AuditScheduleAuditor = CType(obj, AuditScheduleAuditor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, auditScheduleAuditor.ID)
            DbCommandWrapper.AddInParameter("@AuditorType", DbType.Byte, auditScheduleAuditor.AuditorType)
            DbCommandWrapper.AddInParameter("@Auditor", DbType.AnsiString, auditScheduleAuditor.Auditor)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, auditScheduleAuditor.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, auditScheduleAuditor.EndDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, auditScheduleAuditor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, auditScheduleAuditor.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@AuditScheduleID", DbType.Int32, Me.GetRefObject(auditScheduleAuditor.AuditSchedule))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(auditScheduleAuditor.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AuditScheduleAuditor

            Dim auditScheduleAuditor As AuditScheduleAuditor = New AuditScheduleAuditor

            auditScheduleAuditor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AuditorType")) Then auditScheduleAuditor.AuditorType = CType(dr("AuditorType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Auditor")) Then auditScheduleAuditor.Auditor = dr("Auditor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then auditScheduleAuditor.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndDate")) Then auditScheduleAuditor.EndDate = CType(dr("EndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then auditScheduleAuditor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then auditScheduleAuditor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then auditScheduleAuditor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then auditScheduleAuditor.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then auditScheduleAuditor.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AuditScheduleID")) Then
                auditScheduleAuditor.AuditSchedule = New AuditSchedule(CType(dr("AuditScheduleID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                auditScheduleAuditor.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return auditScheduleAuditor

        End Function

        Private Sub SetTableName()

            If Not (GetType(AuditScheduleAuditor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AuditScheduleAuditor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AuditScheduleAuditor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

