
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventReportDocument Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 27/05/2019 - 9:57:39
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

    Public Class BabitEventReportDocumentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitEventReportDocument"
        Private m_UpdateStatement As String = "up_UpdateBabitEventReportDocument"
        Private m_RetrieveStatement As String = "up_RetrieveBabitEventReportDocument"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitEventReportDocumentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitEventReportDocument"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitEventReportDocument As BabitEventReportDocument = Nothing
            While dr.Read

                babitEventReportDocument = Me.CreateObject(dr)

            End While

            Return babitEventReportDocument

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitEventReportDocumentList As ArrayList = New ArrayList

            While dr.Read
                Dim babitEventReportDocument As BabitEventReportDocument = Me.CreateObject(dr)
                babitEventReportDocumentList.Add(babitEventReportDocument)
            End While

            Return babitEventReportDocumentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventReportDocument As BabitEventReportDocument = CType(obj, BabitEventReportDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventReportDocument.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventReportDocument As BabitEventReportDocument = CType(obj, BabitEventReportDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, babitEventReportDocument.FileName)
            DbCommandWrapper.AddInParameter("@FileDescription", DbType.AnsiString, babitEventReportDocument.FileDescription)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, babitEventReportDocument.Path)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventReportDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitEventReportDocument.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@EventDealerRequiredDocumentID", DbType.Int32, Me.GetRefObject(babitEventReportDocument.EventDealerRequiredDocument))
            DbCommandWrapper.AddInParameter("@BabitEventReportHeaderID", DbType.Int32, Me.GetRefObject(babitEventReportDocument.BabitEventReportHeader))

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

            Dim babitEventReportDocument As BabitEventReportDocument = CType(obj, BabitEventReportDocument)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventReportDocument.ID)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, babitEventReportDocument.FileName)
            DbCommandWrapper.AddInParameter("@FileDescription", DbType.AnsiString, babitEventReportDocument.FileDescription)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, babitEventReportDocument.Path)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventReportDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitEventReportDocument.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            
            DbCommandWrapper.AddInParameter("@EventDealerRequiredDocumentID", DbType.Int32, Me.GetRefObject(babitEventReportDocument.EventDealerRequiredDocument))
            DbCommandWrapper.AddInParameter("@BabitEventReportHeaderID", DbType.Int32, Me.GetRefObject(babitEventReportDocument.BabitEventReportHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitEventReportDocument

            Dim babitEventReportDocument As BabitEventReportDocument = New BabitEventReportDocument

            babitEventReportDocument.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then babitEventReportDocument.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileDescription")) Then babitEventReportDocument.FileDescription = dr("FileDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Path")) Then babitEventReportDocument.Path = dr("Path").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitEventReportDocument.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitEventReportDocument.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitEventReportDocument.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitEventReportDocument.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitEventReportDocument.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            
            If Not dr.IsDBNull(dr.GetOrdinal("BabitEventReportHeaderID")) Then
                babitEventReportDocument.BabitEventReportHeader = New BabitEventReportHeader(CType(dr("BabitEventReportHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EventDealerRequiredDocumentID")) Then
                babitEventReportDocument.EventDealerRequiredDocument = New EventDealerRequiredDocument(CType(dr("EventDealerRequiredDocumentID"), Integer))
            End If
            Return babitEventReportDocument

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitEventReportDocument) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitEventReportDocument), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitEventReportDocument).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

