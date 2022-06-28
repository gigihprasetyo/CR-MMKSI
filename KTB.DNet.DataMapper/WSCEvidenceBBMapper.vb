#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCEvidenceBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/26/2005 - 2:01:44 PM
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

    Public Class WSCEvidenceBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWSCEvidenceBB"
        Private m_UpdateStatement As String = "up_UpdateWSCEvidenceBB"
        Private m_RetrieveStatement As String = "up_RetrieveWSCEvidenceBB"
        Private m_RetrieveListStatement As String = "up_RetrieveWSCEvidenceBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWSCEvidenceBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim WSCEvidenceBB As WSCEvidenceBB = Nothing
            While dr.Read

                WSCEvidenceBB = Me.CreateObject(dr)

            End While

            Return WSCEvidenceBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim WSCEvidenceBBList As ArrayList = New ArrayList

            While dr.Read
                Dim WSCEvidenceBB As WSCEvidenceBB = Me.CreateObject(dr)
                WSCEvidenceBBList.Add(WSCEvidenceBB)
            End While

            Return WSCEvidenceBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WSCEvidenceBB As WSCEvidenceBB = CType(obj, WSCEvidenceBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, WSCEvidenceBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WSCEvidenceBB As WSCEvidenceBB = CType(obj, WSCEvidenceBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@EvidenceType", DbType.Int16, WSCEvidenceBB.EvidenceType)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, WSCEvidenceBB.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, WSCEvidenceBB.Status)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.Int16, WSCEvidenceBB.UploadDate)
            DbCommandWrapper.AddInParameter("@UploadMonth", DbType.Int16, WSCEvidenceBB.UploadMonth)
            DbCommandWrapper.AddInParameter("@UploadYear", DbType.Int16, WSCEvidenceBB.UploadYear)
            DbCommandWrapper.AddInParameter("@PathFile", DbType.AnsiString, WSCEvidenceBB.PathFile)
            DbCommandWrapper.AddInParameter("@DownloadDate", DbType.DateTime, WSCEvidenceBB.DownloadDate)
            DbCommandWrapper.AddInParameter("@DownloadBy", DbType.AnsiString, WSCEvidenceBB.DownloadBy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, WSCEvidenceBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, WSCEvidenceBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@WSCHeaderBBID", DbType.Int32, Me.GetRefObject(WSCEvidenceBB.WSCHeaderBB))

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WSCEvidenceBB As WSCEvidenceBB = CType(obj, WSCEvidenceBB)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, WSCEvidenceBB.ID)
            DBCommandWrapper.AddInParameter("@EvidenceType", DbType.Int16, WSCEvidenceBB.EvidenceType)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, WSCEvidenceBB.Description)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, WSCEvidenceBB.Status)
            DBCommandWrapper.AddInParameter("@UploadDate", DbType.Int16, WSCEvidenceBB.UploadDate)
            DBCommandWrapper.AddInParameter("@UploadMonth", DbType.Int16, WSCEvidenceBB.UploadMonth)
            DBCommandWrapper.AddInParameter("@UploadYear", DbType.Int16, WSCEvidenceBB.UploadYear)
            DBCommandWrapper.AddInParameter("@PathFile", DbType.AnsiString, WSCEvidenceBB.PathFile)
            DBCommandWrapper.AddInParameter("@DownloadDate", DbType.DateTime, WSCEvidenceBB.DownloadDate)
            DBCommandWrapper.AddInParameter("@DownloadBy", DbType.AnsiString, WSCEvidenceBB.DownloadBy)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, WSCEvidenceBB.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, WSCEvidenceBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@WSCHeaderBBID", DbType.Int32, Me.GetRefObject(WSCEvidenceBB.WSCHeaderBB))

            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WSCEvidenceBB

            Dim WSCEvidenceBB As WSCEvidenceBB = New WSCEvidenceBB

            WSCEvidenceBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceType")) Then WSCEvidenceBB.EvidenceType = CType(dr("EvidenceType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then WSCEvidenceBB.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then WSCEvidenceBB.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadDate")) Then WSCEvidenceBB.UploadDate = CType(dr("UploadDate"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadMonth")) Then WSCEvidenceBB.UploadMonth = CType(dr("UploadMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadYear")) Then WSCEvidenceBB.UploadYear = CType(dr("UploadYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PathFile")) Then WSCEvidenceBB.PathFile = dr("PathFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadDate")) Then WSCEvidenceBB.DownloadDate = CType(dr("DownloadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadBy")) Then WSCEvidenceBB.DownloadBy = dr("DownloadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then WSCEvidenceBB.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then WSCEvidenceBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then WSCEvidenceBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then WSCEvidenceBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then WSCEvidenceBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WSCHeaderBBID")) Then
                WSCEvidenceBB.WSCHeaderBB = New WSCHeaderBB(CType(dr("WSCHeaderBBID"), Integer))
            End If

            Return WSCEvidenceBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(WSCEvidenceBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WSCEvidenceBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WSCEvidenceBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

