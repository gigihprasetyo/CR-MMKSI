
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TransactionControlPKHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 11/27/2015 - 2:03:27 PM
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

    Public Class TransactionControlPKHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTransactionControlPKHistory"
        Private m_UpdateStatement As String = "up_UpdateTransactionControlPKHistory"
        Private m_RetrieveStatement As String = "up_RetrieveTransactionControlPKHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveTransactionControlPKHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTransactionControlPKHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim transactionControlPKHistory As TransactionControlPKHistory = Nothing
            While dr.Read

                transactionControlPKHistory = Me.CreateObject(dr)

            End While

            Return transactionControlPKHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim transactionControlPKHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim transactionControlPKHistory As TransactionControlPKHistory = Me.CreateObject(dr)
                transactionControlPKHistoryList.Add(transactionControlPKHistory)
            End While

            Return transactionControlPKHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transactionControlPKHistory As TransactionControlPKHistory = CType(obj, TransactionControlPKHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@id", DbType.Int32, transactionControlPKHistory.id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transactionControlPKHistory As TransactionControlPKHistory = CType(obj, TransactionControlPKHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@id", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@StatusFrom", DbType.Int32, transactionControlPKHistory.StatusFrom)
            DbCommandWrapper.AddInParameter("@StatusTo", DbType.Int32, transactionControlPKHistory.StatusTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, transactionControlPKHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, transactionControlPKHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@TransactionControlPKID", DbType.Int32, Me.GetRefObject(transactionControlPKHistory.TransactionControlPK))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@id"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "id")

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
            DbCommandWrapper.AddInParameter("@id", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transactionControlPKHistory As TransactionControlPKHistory = CType(obj, TransactionControlPKHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@id", DbType.Int32, transactionControlPKHistory.id)
            DbCommandWrapper.AddInParameter("@StatusFrom", DbType.Int32, transactionControlPKHistory.StatusFrom)
            DbCommandWrapper.AddInParameter("@StatusTo", DbType.Int32, transactionControlPKHistory.StatusTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, transactionControlPKHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, transactionControlPKHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@TransactionControlPKID", DbType.Int32, Me.GetRefObject(transactionControlPKHistory.TransactionControlPK))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TransactionControlPKHistory

            Dim transactionControlPKHistory As TransactionControlPKHistory = New TransactionControlPKHistory

            transactionControlPKHistory.id = CType(dr("id"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusFrom")) Then transactionControlPKHistory.StatusFrom = CType(dr("StatusFrom"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusTo")) Then transactionControlPKHistory.StatusTo = CType(dr("StatusTo"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then transactionControlPKHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then transactionControlPKHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then transactionControlPKHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then transactionControlPKHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then transactionControlPKHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionControlPKID")) Then
                transactionControlPKHistory.TransactionControlPK = New TransactionControlPK(CType(dr("TransactionControlPKID"), Integer))
            End If

            Return transactionControlPKHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(TransactionControlPKHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TransactionControlPKHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TransactionControlPKHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

