#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ManualDoc Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/6/2007 - 8:23:37 AM
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

    Public Class ManualDocMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertManualDoc"
        Private m_UpdateStatement As String = "up_UpdateManualDoc"
        Private m_RetrieveStatement As String = "up_RetrieveManualDoc"
        Private m_RetrieveListStatement As String = "up_RetrieveManualDocList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteManualDoc"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim manualDoc As ManualDoc = Nothing
            While dr.Read

                manualDoc = Me.CreateObject(dr)

            End While

            Return manualDoc

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim manualDocList As ArrayList = New ArrayList

            While dr.Read
                Dim manualDoc As ManualDoc = Me.CreateObject(dr)
                manualDocList.Add(manualDoc)
            End While

            Return manualDocList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim manualDoc As ManualDoc = CType(obj, ManualDoc)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, manualDoc.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim manualDoc As ManualDoc = CType(obj, ManualDoc)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, manualDoc.Name)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, manualDoc.Description)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int16, manualDoc.Sequence)
            DbCommandWrapper.AddInParameter("@Type", DbType.Byte, manualDoc.Type)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, manualDoc.FileName)
            DbCommandWrapper.AddInParameter("@DownloadCounter", DbType.Int32, manualDoc.DownloadCounter)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, manualDoc.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, manualDoc.LastUpdateBy)
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

            Dim manualDoc As ManualDoc = CType(obj, ManualDoc)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, manualDoc.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, manualDoc.Name)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, manualDoc.Description)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int16, manualDoc.Sequence)
            DbCommandWrapper.AddInParameter("@Type", DbType.Byte, manualDoc.Type)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, manualDoc.FileName)
            DbCommandWrapper.AddInParameter("@DownloadCounter", DbType.Int32, manualDoc.DownloadCounter)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, manualDoc.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, manualDoc.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ManualDoc

            Dim manualDoc As ManualDoc = New ManualDoc

            manualDoc.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then manualDoc.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then manualDoc.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then manualDoc.Sequence = CType(dr("Sequence"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then manualDoc.Type = CType(dr("Type"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then manualDoc.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadCounter")) Then manualDoc.DownloadCounter = CType(dr("DownloadCounter"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then manualDoc.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then manualDoc.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then manualDoc.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then manualDoc.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then manualDoc.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return manualDoc

        End Function

        Private Sub SetTableName()

            If Not (GetType(ManualDoc) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ManualDoc), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ManualDoc).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

