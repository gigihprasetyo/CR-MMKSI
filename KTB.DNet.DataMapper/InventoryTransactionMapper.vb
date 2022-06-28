
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : InventoryTransaction Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 16/03/2018 - 9:12:15
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

    Public Class InventoryTransactionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertInventoryTransaction"
        Private m_UpdateStatement As String = "up_UpdateInventoryTransaction"
        Private m_RetrieveStatement As String = "up_RetrieveInventoryTransaction"
        Private m_RetrieveListStatement As String = "up_RetrieveInventoryTransactionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteInventoryTransaction"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim inventoryTransaction As InventoryTransaction = Nothing
            While dr.Read

                inventoryTransaction = Me.CreateObject(dr)

            End While

            Return inventoryTransaction

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim inventoryTransactionList As ArrayList = New ArrayList

            While dr.Read
                Dim inventoryTransaction As InventoryTransaction = Me.CreateObject(dr)
                inventoryTransactionList.Add(inventoryTransaction)
            End While

            Return inventoryTransactionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim inventoryTransaction As InventoryTransaction = CType(obj, InventoryTransaction)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, inventoryTransaction.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim inventoryTransaction As InventoryTransaction = CType(obj, InventoryTransaction)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, inventoryTransaction.Owner)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, inventoryTransaction.DealerCode)
            DbCommandWrapper.AddInParameter("@InventoryTransactionNo", DbType.AnsiString, inventoryTransaction.InventoryTransactionNo)
            DbCommandWrapper.AddInParameter("@InventoryTransferNo", DbType.AnsiString, inventoryTransaction.InventoryTransferNo)
            DbCommandWrapper.AddInParameter("@PersonInCharge", DbType.AnsiString, inventoryTransaction.PersonInCharge)
            DbCommandWrapper.AddInParameter("@ProcessCode", DbType.AnsiString, inventoryTransaction.ProcessCode)
            DbCommandWrapper.AddInParameter("@SourceData", DbType.AnsiString, inventoryTransaction.SourceData)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, inventoryTransaction.State)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, inventoryTransaction.TransactionDate)
            DbCommandWrapper.AddInParameter("@TransactionTYpe", DbType.Int16, inventoryTransaction.TransactionType)
            DbCommandWrapper.AddInParameter("@WONo", DbType.AnsiString, inventoryTransaction.WONo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, inventoryTransaction.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, inventoryTransaction.LastUpdateBy)
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

            Dim inventoryTransaction As InventoryTransaction = CType(obj, InventoryTransaction)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, inventoryTransaction.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, inventoryTransaction.Owner)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, inventoryTransaction.DealerCode)
            DbCommandWrapper.AddInParameter("@InventoryTransactionNo", DbType.AnsiString, inventoryTransaction.InventoryTransactionNo)
            DbCommandWrapper.AddInParameter("@InventoryTransferNo", DbType.AnsiString, inventoryTransaction.InventoryTransferNo)
            DbCommandWrapper.AddInParameter("@PersonInCharge", DbType.AnsiString, inventoryTransaction.PersonInCharge)
            DbCommandWrapper.AddInParameter("@ProcessCode", DbType.AnsiString, inventoryTransaction.ProcessCode)
            DbCommandWrapper.AddInParameter("@SourceData", DbType.AnsiString, inventoryTransaction.SourceData)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, inventoryTransaction.State)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, inventoryTransaction.TransactionDate)
            DbCommandWrapper.AddInParameter("@TransactionTYpe", DbType.Int16, inventoryTransaction.TransactionType)
            DbCommandWrapper.AddInParameter("@WONo", DbType.AnsiString, inventoryTransaction.WONo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, inventoryTransaction.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, inventoryTransaction.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As InventoryTransaction

            Dim inventoryTransaction As InventoryTransaction = New InventoryTransaction

            inventoryTransaction.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then inventoryTransaction.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then inventoryTransaction.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryTransactionNo")) Then inventoryTransaction.InventoryTransactionNo = dr("InventoryTransactionNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryTransferNo")) Then inventoryTransaction.InventoryTransferNo = dr("InventoryTransferNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PersonInCharge")) Then inventoryTransaction.PersonInCharge = dr("PersonInCharge").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessCode")) Then inventoryTransaction.ProcessCode = dr("ProcessCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SourceData")) Then inventoryTransaction.SourceData = dr("SourceData").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("State")) Then inventoryTransaction.State = CType(dr("State"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then inventoryTransaction.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionTYpe")) Then inventoryTransaction.TransactionType = CType(dr("TransactionTYpe"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("WONo")) Then inventoryTransaction.WONo = dr("WONo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then inventoryTransaction.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then inventoryTransaction.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then inventoryTransaction.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then inventoryTransaction.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then inventoryTransaction.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return inventoryTransaction

        End Function

        Private Sub SetTableName()

            If Not (GetType(InventoryTransaction) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(InventoryTransaction), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(InventoryTransaction).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

