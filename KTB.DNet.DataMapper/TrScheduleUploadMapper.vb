
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrScheduleUpload Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/14/2005 - 10:36:36 AM
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

    Public Class TrScheduleUploadMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrScheduleUpload"
        Private m_UpdateStatement As String = "up_UpdateTrScheduleUpload"
        Private m_RetrieveStatement As String = "up_RetrieveTrScheduleUpload"
        Private m_RetrieveListStatement As String = "up_RetrieveTrScheduleUploadList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrScheduleUpload"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trScheduleUpload As TrScheduleUpload = Nothing
            While dr.Read

                trScheduleUpload = Me.CreateObject(dr)

            End While

            Return trScheduleUpload

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trScheduleUploadList As ArrayList = New ArrayList

            While dr.Read
                Dim trScheduleUpload As TrScheduleUpload = Me.CreateObject(dr)
                trScheduleUploadList.Add(trScheduleUpload)
            End While

            Return trScheduleUploadList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trScheduleUpload As TrScheduleUpload = CType(obj, TrScheduleUpload)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trScheduleUpload.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trScheduleUpload As TrScheduleUpload = CType(obj, TrScheduleUpload)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ScheduleYear", DbType.AnsiStringFixedLength, trScheduleUpload.ScheduleYear)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, trScheduleUpload.Name)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trScheduleUpload.Description)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, trScheduleUpload.UploadDate)
            DbCommandWrapper.AddInParameter("@FilePath", DbType.AnsiString, trScheduleUpload.FilePath)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trScheduleUpload.Status)
            DbCommandWrapper.AddInParameter("@JobPositionCategoryID", DbType.Int16, Me.GetRefObject(trScheduleUpload.JobPositionCategory))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trScheduleUpload.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trScheduleUpload.LastUpdateBy)
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

            Dim trScheduleUpload As TrScheduleUpload = CType(obj, TrScheduleUpload)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trScheduleUpload.ID)
            DbCommandWrapper.AddInParameter("@ScheduleYear", DbType.AnsiStringFixedLength, trScheduleUpload.ScheduleYear)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, trScheduleUpload.Name)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trScheduleUpload.Description)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, trScheduleUpload.UploadDate)
            DbCommandWrapper.AddInParameter("@FilePath", DbType.AnsiString, trScheduleUpload.FilePath)
            DbCommandWrapper.AddInParameter("@JobPositionCategoryID", DbType.Int16, Me.GetRefObject(trScheduleUpload.JobPositionCategory))
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trScheduleUpload.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trScheduleUpload.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trScheduleUpload.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrScheduleUpload

            Dim trScheduleUpload As TrScheduleUpload = New TrScheduleUpload

            trScheduleUpload.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ScheduleYear")) Then trScheduleUpload.ScheduleYear = dr("ScheduleYear").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then trScheduleUpload.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then trScheduleUpload.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadDate")) Then trScheduleUpload.UploadDate = CType(dr("UploadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FilePath")) Then trScheduleUpload.FilePath = dr("FilePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trScheduleUpload.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionCategoryID")) Then trScheduleUpload.JobPositionCategory = New JobPositionCategory(CType(dr("JobPositionCategoryID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trScheduleUpload.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trScheduleUpload.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trScheduleUpload.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trScheduleUpload.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trScheduleUpload.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trScheduleUpload

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrScheduleUpload) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrScheduleUpload), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrScheduleUpload).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

