#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SampleIklan Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 9/8/2006 - 1:37:58 PM
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

    Public Class SampleIklanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSampleIklan"
        Private m_UpdateStatement As String = "up_UpdateSampleIklan"
        Private m_RetrieveStatement As String = "up_RetrieveSampleIklan"
        Private m_RetrieveListStatement As String = "up_RetrieveSampleIklanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSampleIklan"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sampleIklan As SampleIklan = Nothing
            While dr.Read

                sampleIklan = Me.CreateObject(dr)

            End While

            Return sampleIklan

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sampleIklanList As ArrayList = New ArrayList

            While dr.Read
                Dim sampleIklan As SampleIklan = Me.CreateObject(dr)
                sampleIklanList.Add(sampleIklan)
            End While

            Return sampleIklanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sampleIklan As SampleIklan = CType(obj, SampleIklan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sampleIklan.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sampleIklan As SampleIklan = CType(obj, SampleIklan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@NoIklan", DbType.AnsiString, sampleIklan.NoIklan)
            DbCommandWrapper.AddInParameter("@UploadedIklan", DbType.AnsiString, sampleIklan.UploadedIklan)
            DbCommandWrapper.AddInParameter("@UplodedDirection", DbType.AnsiString, sampleIklan.UplodedDirection)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, sampleIklan.Note)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sampleIklan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sampleIklan.LastUpdateBy)
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

            Dim sampleIklan As SampleIklan = CType(obj, SampleIklan)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sampleIklan.ID)
            DbCommandWrapper.AddInParameter("@NoIklan", DbType.AnsiString, sampleIklan.NoIklan)
            DbCommandWrapper.AddInParameter("@UploadedIklan", DbType.AnsiString, sampleIklan.UploadedIklan)
            DbCommandWrapper.AddInParameter("@UplodedDirection", DbType.AnsiString, sampleIklan.UplodedDirection)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, sampleIklan.Note)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sampleIklan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sampleIklan.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SampleIklan

            Dim sampleIklan As SampleIklan = New SampleIklan

            sampleIklan.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoIklan")) Then sampleIklan.NoIklan = dr("NoIklan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadedIklan")) Then sampleIklan.UploadedIklan = dr("UploadedIklan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UplodedDirection")) Then sampleIklan.UplodedDirection = dr("UplodedDirection").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then sampleIklan.Note = dr("Note").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sampleIklan.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sampleIklan.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sampleIklan.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sampleIklan.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sampleIklan.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sampleIklan

        End Function

        Private Sub SetTableName()

            If Not (GetType(SampleIklan) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SampleIklan), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SampleIklan).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

