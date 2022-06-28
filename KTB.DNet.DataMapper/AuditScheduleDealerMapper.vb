#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AuditScheduleDealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/29/2007 - 9:04:20 AM
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

    Public Class AuditScheduleDealerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAuditScheduleDealer"
        Private m_UpdateStatement As String = "up_UpdateAuditScheduleDealer"
        Private m_RetrieveStatement As String = "up_RetrieveAuditScheduleDealer"
        Private m_RetrieveListStatement As String = "up_RetrieveAuditScheduleDealerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAuditScheduleDealer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim auditScheduleDealer As AuditScheduleDealer = Nothing
            While dr.Read

                auditScheduleDealer = Me.CreateObject(dr)

            End While

            Return auditScheduleDealer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim auditScheduleDealerList As ArrayList = New ArrayList

            While dr.Read
                Dim auditScheduleDealer As AuditScheduleDealer = Me.CreateObject(dr)
                auditScheduleDealerList.Add(auditScheduleDealer)
            End While

            Return auditScheduleDealerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim auditScheduleDealer As AuditScheduleDealer = CType(obj, AuditScheduleDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, auditScheduleDealer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim auditScheduleDealer As AuditScheduleDealer = CType(obj, AuditScheduleDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@IsRilisReport", DbType.Byte, auditScheduleDealer.IsRilisReport)
            DbCommandWrapper.AddInParameter("@AssessmentFile", DbType.AnsiString, auditScheduleDealer.AssessmentFile)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, auditScheduleDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, auditScheduleDealer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@AuditScheduleID", DbType.Int32, Me.GetRefObject(auditScheduleDealer.AuditSchedule))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(auditScheduleDealer.Dealer))
            DbCommandWrapper.AddInParameter("@AuditScheduleAuditorID", DbType.Int32, Me.GetRefObject(auditScheduleDealer.AuditScheduleAuditor))

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

            Dim auditScheduleDealer As AuditScheduleDealer = CType(obj, AuditScheduleDealer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, auditScheduleDealer.ID)
            DbCommandWrapper.AddInParameter("@IsRilisReport", DbType.Byte, auditScheduleDealer.IsRilisReport)
            DbCommandWrapper.AddInParameter("@AssessmentFile", DbType.AnsiString, auditScheduleDealer.AssessmentFile)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, auditScheduleDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, auditScheduleDealer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@AuditScheduleID", DbType.Int32, Me.GetRefObject(auditScheduleDealer.AuditSchedule))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(auditScheduleDealer.Dealer))
            DbCommandWrapper.AddInParameter("@AuditScheduleAuditorID", DbType.Int32, Me.GetRefObject(auditScheduleDealer.AuditScheduleAuditor))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AuditScheduleDealer

            Dim auditScheduleDealer As AuditScheduleDealer = New AuditScheduleDealer

            auditScheduleDealer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsRilisReport")) Then auditScheduleDealer.IsRilisReport = CType(dr("IsRilisReport"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("AssessmentFile")) Then auditScheduleDealer.AssessmentFile = dr("AssessmentFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then auditScheduleDealer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then auditScheduleDealer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then auditScheduleDealer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then auditScheduleDealer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then auditScheduleDealer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AuditScheduleID")) Then
                auditScheduleDealer.AuditSchedule = New AuditSchedule(CType(dr("AuditScheduleID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                auditScheduleDealer.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("AuditScheduleAuditorID")) Then
                auditScheduleDealer.AuditScheduleAuditor = New AuditScheduleAuditor(CType(dr("AuditScheduleAuditorID"), Integer))
            End If

            Return auditScheduleDealer

        End Function

        Private Sub SetTableName()

            If Not (GetType(AuditScheduleDealer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AuditScheduleDealer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AuditScheduleDealer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

