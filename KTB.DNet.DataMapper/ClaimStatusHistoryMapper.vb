#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimStatusHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/19/2007 - 11:04:55 AM
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

    Public Class ClaimStatusHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertClaimStatusHistory"
        Private m_UpdateStatement As String = "up_UpdateClaimStatusHistory"
        Private m_RetrieveStatement As String = "up_RetrieveClaimStatusHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveClaimStatusHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteClaimStatusHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim claimStatusHistory As ClaimStatusHistory = Nothing
            While dr.Read

                claimStatusHistory = Me.CreateObject(dr)

            End While

            Return claimStatusHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim claimStatusHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim claimStatusHistory As ClaimStatusHistory = Me.CreateObject(dr)
                claimStatusHistoryList.Add(claimStatusHistory)
            End While

            Return claimStatusHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimStatusHistory As ClaimStatusHistory = CType(obj, ClaimStatusHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimStatusHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimStatusHistory As ClaimStatusHistory = CType(obj, ClaimStatusHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, claimStatusHistory.Status)
            DbCommandWrapper.AddInParameter("@NewStatus", DbType.Byte, claimStatusHistory.NewStatus)
            DbCommandWrapper.AddInParameter("@Progress", DbType.Byte, claimStatusHistory.Progress)
            DbCommandWrapper.AddInParameter("@OldProgress", DbType.Int32, claimStatusHistory.OldProgress)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, claimStatusHistory.Keterangan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimStatusHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, claimStatusHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, Me.GetRefObject(claimStatusHistory.ClaimHeader))

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

            Dim claimStatusHistory As ClaimStatusHistory = CType(obj, ClaimStatusHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimStatusHistory.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, claimStatusHistory.Status)
            DbCommandWrapper.AddInParameter("@NewStatus", DbType.Byte, claimStatusHistory.NewStatus)
            DbCommandWrapper.AddInParameter("@Progress", DbType.Byte, claimStatusHistory.Progress)
            DbCommandWrapper.AddInParameter("@OldProgress", DbType.Int32, claimStatusHistory.OldProgress)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, claimStatusHistory.Keterangan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimStatusHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, claimStatusHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, Me.GetRefObject(claimStatusHistory.ClaimHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ClaimStatusHistory

            Dim claimStatusHistory As ClaimStatusHistory = New ClaimStatusHistory

            claimStatusHistory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then claimStatusHistory.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("NewStatus")) Then claimStatusHistory.NewStatus = CType(dr("NewStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Progress")) Then claimStatusHistory.Progress = CType(dr("Progress"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("OldProgress")) Then claimStatusHistory.OldProgress = CType(dr("OldProgress"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Keterangan")) Then claimStatusHistory.Keterangan = dr("Keterangan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then claimStatusHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then claimStatusHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then claimStatusHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then claimStatusHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then claimStatusHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimHeaderID")) Then
                claimStatusHistory.ClaimHeader = New ClaimHeader(CType(dr("ClaimHeaderID"), Integer))
            End If

            Return claimStatusHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(ClaimStatusHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ClaimStatusHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ClaimStatusHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

