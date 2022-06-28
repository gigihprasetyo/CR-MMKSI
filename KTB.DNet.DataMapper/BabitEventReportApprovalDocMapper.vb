
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventReportApprovalDoc Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 23/09/2019 - 8:36:01
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

    Public Class BabitEventReportApprovalDocMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitEventReportApprovalDoc"
        Private m_UpdateStatement As String = "up_UpdateBabitEventReportApprovalDoc"
        Private m_RetrieveStatement As String = "up_RetrieveBabitEventReportApprovalDoc"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitEventReportApprovalDocList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitEventReportApprovalDoc"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitEventReportApprovalDoc As BabitEventReportApprovalDoc = Nothing
            While dr.Read

                babitEventReportApprovalDoc = Me.CreateObject(dr)

            End While

            Return babitEventReportApprovalDoc

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitEventReportApprovalDocList As ArrayList = New ArrayList

            While dr.Read
                Dim babitEventReportApprovalDoc As BabitEventReportApprovalDoc = Me.CreateObject(dr)
                babitEventReportApprovalDocList.Add(babitEventReportApprovalDoc)
            End While

            Return babitEventReportApprovalDocList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventReportApprovalDoc As BabitEventReportApprovalDoc = CType(obj, BabitEventReportApprovalDoc)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventReportApprovalDoc.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventReportApprovalDoc As BabitEventReportApprovalDoc = CType(obj, BabitEventReportApprovalDoc)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, babitEventReportApprovalDoc.FileName)
            DbCommandWrapper.AddInParameter("@FileDescription", DbType.AnsiString, babitEventReportApprovalDoc.FileDescription)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, babitEventReportApprovalDoc.Path)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventReportApprovalDoc.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitEventReportApprovalDoc.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitEventReportHeaderID", DbType.Int32, Me.GetRefObject(babitEventReportApprovalDoc.BabitEventReportHeader))

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

            Dim babitEventReportApprovalDoc As BabitEventReportApprovalDoc = CType(obj, BabitEventReportApprovalDoc)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventReportApprovalDoc.ID)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, babitEventReportApprovalDoc.FileName)
            DbCommandWrapper.AddInParameter("@FileDescription", DbType.AnsiString, babitEventReportApprovalDoc.FileDescription)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, babitEventReportApprovalDoc.Path)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventReportApprovalDoc.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitEventReportApprovalDoc.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BabitEventReportHeaderID", DbType.Int32, Me.GetRefObject(babitEventReportApprovalDoc.BabitEventReportHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitEventReportApprovalDoc

            Dim babitEventReportApprovalDoc As BabitEventReportApprovalDoc = New BabitEventReportApprovalDoc

            babitEventReportApprovalDoc.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then babitEventReportApprovalDoc.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileDescription")) Then babitEventReportApprovalDoc.FileDescription = dr("FileDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Path")) Then babitEventReportApprovalDoc.Path = dr("Path").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitEventReportApprovalDoc.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitEventReportApprovalDoc.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitEventReportApprovalDoc.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitEventReportApprovalDoc.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitEventReportApprovalDoc.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitEventReportHeaderID")) Then
                babitEventReportApprovalDoc.BabitEventReportHeader = New BabitEventReportHeader(CType(dr("BabitEventReportHeaderID"), Integer))
            End If

            Return babitEventReportApprovalDoc

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitEventReportApprovalDoc) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitEventReportApprovalDoc), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitEventReportApprovalDoc).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

