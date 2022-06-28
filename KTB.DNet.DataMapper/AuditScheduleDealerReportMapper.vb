#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AuditScheduleDealerReport Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 29/09/2007 - 8:42:59
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

    Public Class AuditScheduleDealerReportMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAuditScheduleDealerReport"
        Private m_UpdateStatement As String = "up_UpdateAuditScheduleDealerReport"
        Private m_RetrieveStatement As String = "up_RetrieveAuditScheduleDealerReport"
        Private m_RetrieveListStatement As String = "up_RetrieveAuditScheduleDealerReportList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAuditScheduleDealerReport"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim auditScheduleDealerReport As AuditScheduleDealerReport = Nothing
            While dr.Read

                auditScheduleDealerReport = Me.CreateObject(dr)

            End While

            Return auditScheduleDealerReport

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim auditScheduleDealerReportList As ArrayList = New ArrayList

            While dr.Read
                Dim auditScheduleDealerReport As AuditScheduleDealerReport = Me.CreateObject(dr)
                auditScheduleDealerReportList.Add(auditScheduleDealerReport)
            End While

            Return auditScheduleDealerReportList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim auditScheduleDealerReport As AuditScheduleDealerReport = CType(obj, AuditScheduleDealerReport)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, auditScheduleDealerReport.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim auditScheduleDealerReport As AuditScheduleDealerReport = CType(obj, AuditScheduleDealerReport)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ItemDesc", DbType.AnsiString, auditScheduleDealerReport.ItemDesc)
            DbCommandWrapper.AddInParameter("@ItemImage", DbType.Binary, auditScheduleDealerReport.ItemImage)
            DbCommandWrapper.AddInParameter("@ItemImageReparation", DbType.Binary, auditScheduleDealerReport.ItemImageReparation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, auditScheduleDealerReport.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, auditScheduleDealerReport.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@AuditParameterPhotoID", DbType.Int32, auditScheduleDealerReport.AuditParameterPhotoID)
            DbCommandWrapper.AddInParameter("@AuditScheduleDealerID", DbType.Int32, auditScheduleDealerReport.AuditScheduleDealerID)

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

            Dim auditScheduleDealerReport As AuditScheduleDealerReport = CType(obj, AuditScheduleDealerReport)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, auditScheduleDealerReport.ID)
            DbCommandWrapper.AddInParameter("@ItemDesc", DbType.AnsiString, auditScheduleDealerReport.ItemDesc)
            DbCommandWrapper.AddInParameter("@ItemImage", DbType.Binary, auditScheduleDealerReport.ItemImage)
            DbCommandWrapper.AddInParameter("@ItemImageReparation", DbType.Binary, auditScheduleDealerReport.ItemImageReparation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, auditScheduleDealerReport.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, auditScheduleDealerReport.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@AuditParameterPhotoID", DbType.Int32, auditScheduleDealerReport.AuditParameterPhotoID)
            DbCommandWrapper.AddInParameter("@AuditScheduleDealerID", DbType.Int32, auditScheduleDealerReport.AuditScheduleDealerID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AuditScheduleDealerReport

            Dim auditScheduleDealerReport As auditScheduleDealerReport = New auditScheduleDealerReport


            auditScheduleDealerReport.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ItemDesc")) Then auditScheduleDealerReport.ItemDesc = dr("ItemDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ItemImage")) Then auditScheduleDealerReport.ItemImage = CType(dr("ItemImage"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("ItemImageReparation")) Then auditScheduleDealerReport.ItemImageReparation = CType(dr("ItemImageReparation"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then auditScheduleDealerReport.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then auditScheduleDealerReport.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then auditScheduleDealerReport.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then auditScheduleDealerReport.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then auditScheduleDealerReport.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AuditParameterPhotoID")) Then
                auditScheduleDealerReport.AuditParameterPhotoID = CType(dr("AuditParameterPhotoID"), Integer)
                auditScheduleDealerReport.AuditParameterPhoto = New AuditParameterPhoto(auditScheduleDealerReport.AuditParameterPhotoID)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("AuditScheduleDealerID")) Then
                auditScheduleDealerReport.AuditScheduleDealerID = CType(dr("AuditScheduleDealerID"), Integer)
            End If

            Return auditScheduleDealerReport

        End Function

        Private Sub SetTableName()

            If Not (GetType(AuditScheduleDealerReport) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AuditScheduleDealerReport), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AuditScheduleDealerReport).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

