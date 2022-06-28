#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRChangesHistoryBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/4/2007 - 11:05:01 AM
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

    Public Class PQRChangesHistoryBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPQRChangesHistoryBB"
        Private m_UpdateStatement As String = "up_UpdatePQRChangesHistoryBB"
        Private m_RetrieveStatement As String = "up_RetrievePQRChangesHistoryBB"
        Private m_RetrieveListStatement As String = "up_RetrievePQRChangesHistoryBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePQRChangesHistoryBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim PQRChangesHistoryBB As PQRChangesHistoryBB = Nothing
            While dr.Read

                PQRChangesHistoryBB = Me.CreateObject(dr)

            End While

            Return PQRChangesHistoryBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim PQRChangesHistoryBBList As ArrayList = New ArrayList

            While dr.Read
                Dim PQRChangesHistoryBB As PQRChangesHistoryBB = Me.CreateObject(dr)
                PQRChangesHistoryBBList.Add(PQRChangesHistoryBB)
            End While

            Return PQRChangesHistoryBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRChangesHistoryBB As PQRChangesHistoryBB = CType(obj, PQRChangesHistoryBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRChangesHistoryBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRChangesHistoryBB As PQRChangesHistoryBB = CType(obj, PQRChangesHistoryBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, PQRChangesHistoryBB.DocumentType)
            DbCommandWrapper.AddInParameter("@OldStatus", DbType.AnsiString, PQRChangesHistoryBB.OldStatus)
            DbCommandWrapper.AddInParameter("@NewStatus", DbType.AnsiString, PQRChangesHistoryBB.NewStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PQRChangesHistoryBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, PQRChangesHistoryBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRChangesHistoryBB.PQRHeaderBB))

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

            Dim PQRChangesHistoryBB As PQRChangesHistoryBB = CType(obj, PQRChangesHistoryBB)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRChangesHistoryBB.ID)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, PQRChangesHistoryBB.DocumentType)
            DbCommandWrapper.AddInParameter("@OldStatus", DbType.AnsiString, PQRChangesHistoryBB.OldStatus)
            DbCommandWrapper.AddInParameter("@NewStatus", DbType.AnsiString, PQRChangesHistoryBB.NewStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PQRChangesHistoryBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, PQRChangesHistoryBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRChangesHistoryBB.PQRHeaderBB))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PQRChangesHistoryBB

            Dim PQRChangesHistoryBB As PQRChangesHistoryBB = New PQRChangesHistoryBB

            PQRChangesHistoryBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then PQRChangesHistoryBB.DocumentType = dr("DocumentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OldStatus")) Then PQRChangesHistoryBB.OldStatus = dr("OldStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NewStatus")) Then PQRChangesHistoryBB.NewStatus = dr("NewStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then PQRChangesHistoryBB.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then PQRChangesHistoryBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then PQRChangesHistoryBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then PQRChangesHistoryBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then PQRChangesHistoryBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRHeaderBBID")) Then
                PQRChangesHistoryBB.PQRHeaderBB = New PQRHeaderBB(CType(dr("PQRHeaderBBID"), Integer))
            End If

            Return PQRChangesHistoryBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(PQRChangesHistoryBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PQRChangesHistoryBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PQRChangesHistoryBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

