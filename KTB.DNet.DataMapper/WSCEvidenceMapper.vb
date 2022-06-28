#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCEvidence Objects Mapper.
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

    Public Class WSCEvidenceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWSCEvidence"
        Private m_UpdateStatement As String = "up_UpdateWSCEvidence"
        Private m_RetrieveStatement As String = "up_RetrieveWSCEvidence"
        Private m_RetrieveListStatement As String = "up_RetrieveWSCEvidenceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWSCEvidence"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim wSCEvidence As WSCEvidence = Nothing
            While dr.Read

                wSCEvidence = Me.CreateObject(dr)

            End While

            Return wSCEvidence

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim wSCEvidenceList As ArrayList = New ArrayList

            While dr.Read
                Dim wSCEvidence As WSCEvidence = Me.CreateObject(dr)
                wSCEvidenceList.Add(wSCEvidence)
            End While

            Return wSCEvidenceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCEvidence As WSCEvidence = CType(obj, WSCEvidence)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wSCEvidence.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCEvidence As WSCEvidence = CType(obj, WSCEvidence)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@EvidenceType", DbType.Int16, wSCEvidence.EvidenceType)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, wSCEvidence.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, wSCEvidence.Status)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.Int16, wSCEvidence.UploadDate)
            DbCommandWrapper.AddInParameter("@UploadMonth", DbType.Int16, wSCEvidence.UploadMonth)
            DbCommandWrapper.AddInParameter("@UploadYear", DbType.Int16, wSCEvidence.UploadYear)
            DbCommandWrapper.AddInParameter("@PathFile", DbType.AnsiString, wSCEvidence.PathFile)
            DbCommandWrapper.AddInParameter("@DownloadDate", DbType.DateTime, wSCEvidence.DownloadDate)
            DbCommandWrapper.AddInParameter("@DownloadBy", DbType.AnsiString, wSCEvidence.DownloadBy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wSCEvidence.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, wSCEvidence.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@WSCHeaderID", DbType.Int32, Me.GetRefObject(wSCEvidence.WSCHeader))

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

            Dim wSCEvidence As WSCEvidence = CType(obj, WSCEvidence)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, wSCEvidence.ID)
            DBCommandWrapper.AddInParameter("@EvidenceType", DbType.Int16, wSCEvidence.EvidenceType)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, wSCEvidence.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, wSCEvidence.Status)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.Int16, wSCEvidence.UploadDate)
            DbCommandWrapper.AddInParameter("@UploadMonth", DbType.Int16, wSCEvidence.UploadMonth)
            DbCommandWrapper.AddInParameter("@UploadYear", DbType.Int16, wSCEvidence.UploadYear)
            DbCommandWrapper.AddInParameter("@PathFile", DbType.AnsiString, wSCEvidence.PathFile)
            DbCommandWrapper.AddInParameter("@DownloadDate", DbType.DateTime, wSCEvidence.DownloadDate)
            DbCommandWrapper.AddInParameter("@DownloadBy", DbType.AnsiString, wSCEvidence.DownloadBy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wSCEvidence.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, wSCEvidence.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@WSCHeaderID", DbType.Int32, Me.GetRefObject(wSCEvidence.WSCHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WSCEvidence

            Dim wSCEvidence As WSCEvidence = New WSCEvidence

            wSCEvidence.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceType")) Then wSCEvidence.EvidenceType = CType(dr("EvidenceType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then wSCEvidence.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then wSCEvidence.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadDate")) Then wSCEvidence.UploadDate = CType(dr("UploadDate"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadMonth")) Then wSCEvidence.UploadMonth = CType(dr("UploadMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadYear")) Then wSCEvidence.UploadYear = CType(dr("UploadYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PathFile")) Then wSCEvidence.PathFile = dr("PathFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadDate")) Then wSCEvidence.DownloadDate = CType(dr("DownloadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadBy")) Then wSCEvidence.DownloadBy = dr("DownloadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then wSCEvidence.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then wSCEvidence.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then wSCEvidence.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then wSCEvidence.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then wSCEvidence.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WSCHeaderID")) Then
                wSCEvidence.WSCHeader = New WSCHeader(CType(dr("WSCHeaderID"), Integer))
            End If

            Return wSCEvidence

        End Function

        Private Sub SetTableName()

            If Not (GetType(WSCEvidence) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WSCEvidence), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WSCEvidence).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

