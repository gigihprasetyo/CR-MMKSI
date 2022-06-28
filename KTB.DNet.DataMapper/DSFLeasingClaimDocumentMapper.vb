
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DSFLeasingClaimDocument Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 09/12/2019 - 14:48:22
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

    Public Class DSFLeasingClaimDocumentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDSFLeasingClaimDocument"
        Private m_UpdateStatement As String = "up_UpdateDSFLeasingClaimDocument"
        Private m_RetrieveStatement As String = "up_RetrieveDSFLeasingClaimDocument"
        Private m_RetrieveListStatement As String = "up_RetrieveDSFLeasingClaimDocumentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDSFLeasingClaimDocument"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dSFLeasingClaimDocument As DSFLeasingClaimDocument = Nothing
            While dr.Read

                dSFLeasingClaimDocument = Me.CreateObject(dr)

            End While

            Return dSFLeasingClaimDocument

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dSFLeasingClaimDocumentList As ArrayList = New ArrayList

            While dr.Read
                Dim dSFLeasingClaimDocument As DSFLeasingClaimDocument = Me.CreateObject(dr)
                dSFLeasingClaimDocumentList.Add(dSFLeasingClaimDocument)
            End While

            Return dSFLeasingClaimDocumentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dSFLeasingClaimDocument As DSFLeasingClaimDocument = CType(obj, DSFLeasingClaimDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dSFLeasingClaimDocument.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dSFLeasingClaimDocument As DSFLeasingClaimDocument = CType(obj, DSFLeasingClaimDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, dSFLeasingClaimDocument.FileName)
            DbCommandWrapper.AddInParameter("@FileDescription", DbType.AnsiString, dSFLeasingClaimDocument.FileDescription)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, dSFLeasingClaimDocument.Path)
            DbCommandWrapper.AddInParameter("@SourceData", DbType.AnsiString, dSFLeasingClaimDocument.SourceData)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dSFLeasingClaimDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dSFLeasingClaimDocument.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DSFLeasingClaimID", DbType.Int32, Me.GetRefObject(dSFLeasingClaimDocument.DSFLeasingClaim))

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

            Dim dSFLeasingClaimDocument As DSFLeasingClaimDocument = CType(obj, DSFLeasingClaimDocument)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dSFLeasingClaimDocument.ID)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, dSFLeasingClaimDocument.FileName)
            DbCommandWrapper.AddInParameter("@FileDescription", DbType.AnsiString, dSFLeasingClaimDocument.FileDescription)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, dSFLeasingClaimDocument.Path)
            DbCommandWrapper.AddInParameter("@SourceData", DbType.AnsiString, dSFLeasingClaimDocument.SourceData)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dSFLeasingClaimDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dSFLeasingClaimDocument.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DSFLeasingClaimID", DbType.Int32, Me.GetRefObject(dSFLeasingClaimDocument.DSFLeasingClaim))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DSFLeasingClaimDocument

            Dim dSFLeasingClaimDocument As DSFLeasingClaimDocument = New DSFLeasingClaimDocument

            dSFLeasingClaimDocument.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then dSFLeasingClaimDocument.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileDescription")) Then dSFLeasingClaimDocument.FileDescription = dr("FileDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Path")) Then dSFLeasingClaimDocument.Path = dr("Path").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SourceData")) Then dSFLeasingClaimDocument.SourceData = dr("SourceData").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dSFLeasingClaimDocument.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dSFLeasingClaimDocument.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dSFLeasingClaimDocument.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dSFLeasingClaimDocument.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dSFLeasingClaimDocument.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DSFLeasingClaimID")) Then
                dSFLeasingClaimDocument.DSFLeasingClaim = New DSFLeasingClaim(CType(dr("DSFLeasingClaimID"), Integer))
            End If

            Return dSFLeasingClaimDocument

        End Function

        Private Sub SetTableName()

            If Not (GetType(DSFLeasingClaimDocument) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DSFLeasingClaimDocument), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DSFLeasingClaimDocument).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

