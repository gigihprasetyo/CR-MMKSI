
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitReportDocument Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 12/09/2019 - 14:23:04
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

    Public Class BabitReportDocumentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitReportDocument"
        Private m_UpdateStatement As String = "up_UpdateBabitReportDocument"
        Private m_RetrieveStatement As String = "up_RetrieveBabitReportDocument"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitReportDocumentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitReportDocument"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitReportDocument As BabitReportDocument = Nothing
            While dr.Read

                babitReportDocument = Me.CreateObject(dr)

            End While

            Return babitReportDocument

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitReportDocumentList As ArrayList = New ArrayList

            While dr.Read
                Dim babitReportDocument As BabitReportDocument = Me.CreateObject(dr)
                babitReportDocumentList.Add(babitReportDocument)
            End While

            Return babitReportDocumentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitReportDocument As BabitReportDocument = CType(obj, BabitReportDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitReportDocument.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitReportDocument As BabitReportDocument = CType(obj, BabitReportDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, babitReportDocument.FileName)
            DbCommandWrapper.AddInParameter("@FileDescription", DbType.AnsiString, babitReportDocument.FileDescription)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, babitReportDocument.Path)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitReportDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitReportDocument.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitReportHeaderID", DbType.Int32, Me.GetRefObject(babitReportDocument.BabitReportHeader))

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

            Dim babitReportDocument As BabitReportDocument = CType(obj, BabitReportDocument)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitReportDocument.ID)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, babitReportDocument.FileName)
            DbCommandWrapper.AddInParameter("@FileDescription", DbType.AnsiString, babitReportDocument.FileDescription)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, babitReportDocument.Path)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitReportDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitReportDocument.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitReportHeaderID", DbType.Int32, Me.GetRefObject(babitReportDocument.BabitReportHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitReportDocument

            Dim babitReportDocument As BabitReportDocument = New BabitReportDocument

            babitReportDocument.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then babitReportDocument.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileDescription")) Then babitReportDocument.FileDescription = dr("FileDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Path")) Then babitReportDocument.Path = dr("Path").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitReportDocument.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitReportDocument.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitReportDocument.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitReportDocument.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitReportDocument.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitReportHeaderID")) Then
                babitReportDocument.BabitReportHeader = New BabitReportHeader(CType(dr("BabitReportHeaderID"), Integer))
            End If
            Return babitReportDocument

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitReportDocument) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitReportDocument), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitReportDocument).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

