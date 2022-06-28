
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MitraisTeam
'// PURPOSE       : InventoryTransfer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 25/03/2018 - 21:48:19
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

    Public Class InventoryTransferMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertInventoryTransfer"
        Private m_UpdateStatement As String = "up_UpdateInventoryTransfer"
        Private m_RetrieveStatement As String = "up_RetrieveInventoryTransfer"
        Private m_RetrieveListStatement As String = "up_RetrieveInventoryTransferList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteInventoryTransfer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim inventoryTransfer As InventoryTransfer = Nothing
            While dr.Read

                inventoryTransfer = Me.CreateObject(dr)

            End While

            Return inventoryTransfer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim inventoryTransferList As ArrayList = New ArrayList

            While dr.Read
                Dim inventoryTransfer As InventoryTransfer = Me.CreateObject(dr)
                inventoryTransferList.Add(inventoryTransfer)
            End While

            Return inventoryTransferList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim inventoryTransfer As InventoryTransfer = CType(obj, InventoryTransfer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, inventoryTransfer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim inventoryTransfer As InventoryTransfer = CType(obj, InventoryTransfer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, inventoryTransfer.Owner)
            DbCommandWrapper.AddInParameter("@FromDealer", DbType.AnsiString, inventoryTransfer.FromDealer)
            DbCommandWrapper.AddInParameter("@FromSite", DbType.AnsiString, inventoryTransfer.FromSite)
            DbCommandWrapper.AddInParameter("@InventoryTransferNo", DbType.AnsiString, inventoryTransfer.InventoryTransferNo)
            DbCommandWrapper.AddInParameter("@ItemTypeForTransfer", DbType.Int16, inventoryTransfer.ItemTypeForTransfer)
            DbCommandWrapper.AddInParameter("@PersonInCharge", DbType.AnsiString, inventoryTransfer.PersonInCharge)
            DbCommandWrapper.AddInParameter("@ReceiptDate", DbType.DateTime, inventoryTransfer.ReceiptDate)
            DbCommandWrapper.AddInParameter("@ReceiptNo", DbType.AnsiString, inventoryTransfer.ReceiptNo)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, inventoryTransfer.ReferenceNo)
            DbCommandWrapper.AddInParameter("@SearchVehicle", DbType.AnsiString, inventoryTransfer.SearchVehicle)
            DbCommandWrapper.AddInParameter("@SourceData", DbType.AnsiString, inventoryTransfer.SourceData)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, inventoryTransfer.State)
            DbCommandWrapper.AddInParameter("@ToDealer", DbType.AnsiString, inventoryTransfer.ToDealer)
            DbCommandWrapper.AddInParameter("@ToSite", DbType.AnsiString, inventoryTransfer.ToSite)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, inventoryTransfer.TransactionDate)
            DbCommandWrapper.AddInParameter("@TransactionType", DbType.Int16, inventoryTransfer.TransactionType)
            DbCommandWrapper.AddInParameter("@TransferStatus", DbType.Int16, inventoryTransfer.TransferStatus)
            DbCommandWrapper.AddInParameter("@TransferStep", DbType.Boolean, inventoryTransfer.TransferStep)
            DbCommandWrapper.AddInParameter("@WONo", DbType.AnsiString, inventoryTransfer.WONo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, inventoryTransfer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, inventoryTransfer.LastUpdateBy)
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

            Dim inventoryTransfer As InventoryTransfer = CType(obj, InventoryTransfer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, inventoryTransfer.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, inventoryTransfer.Owner)
            DbCommandWrapper.AddInParameter("@FromDealer", DbType.AnsiString, inventoryTransfer.FromDealer)
            DbCommandWrapper.AddInParameter("@FromSite", DbType.AnsiString, inventoryTransfer.FromSite)
            DbCommandWrapper.AddInParameter("@InventoryTransferNo", DbType.AnsiString, inventoryTransfer.InventoryTransferNo)
            DbCommandWrapper.AddInParameter("@ItemTypeForTransfer", DbType.Int16, inventoryTransfer.ItemTypeForTransfer)
            DbCommandWrapper.AddInParameter("@PersonInCharge", DbType.AnsiString, inventoryTransfer.PersonInCharge)
            DbCommandWrapper.AddInParameter("@ReceiptDate", DbType.DateTime, inventoryTransfer.ReceiptDate)
            DbCommandWrapper.AddInParameter("@ReceiptNo", DbType.AnsiString, inventoryTransfer.ReceiptNo)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, inventoryTransfer.ReferenceNo)
            DbCommandWrapper.AddInParameter("@SearchVehicle", DbType.AnsiString, inventoryTransfer.SearchVehicle)
            DbCommandWrapper.AddInParameter("@SourceData", DbType.AnsiString, inventoryTransfer.SourceData)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, inventoryTransfer.State)
            DbCommandWrapper.AddInParameter("@ToDealer", DbType.AnsiString, inventoryTransfer.ToDealer)
            DbCommandWrapper.AddInParameter("@ToSite", DbType.AnsiString, inventoryTransfer.ToSite)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, inventoryTransfer.TransactionDate)
            DbCommandWrapper.AddInParameter("@TransactionType", DbType.Int16, inventoryTransfer.TransactionType)
            DbCommandWrapper.AddInParameter("@TransferStatus", DbType.Int16, inventoryTransfer.TransferStatus)
            DbCommandWrapper.AddInParameter("@TransferStep", DbType.Boolean, inventoryTransfer.TransferStep)
            DbCommandWrapper.AddInParameter("@WONo", DbType.AnsiString, inventoryTransfer.WONo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, inventoryTransfer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, inventoryTransfer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As InventoryTransfer

            Dim inventoryTransfer As InventoryTransfer = New InventoryTransfer

            inventoryTransfer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then inventoryTransfer.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromDealer")) Then inventoryTransfer.FromDealer = dr("FromDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromSite")) Then inventoryTransfer.FromSite = dr("FromSite").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryTransferNo")) Then inventoryTransfer.InventoryTransferNo = dr("InventoryTransferNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ItemTypeForTransfer")) Then inventoryTransfer.ItemTypeForTransfer = CType(dr("ItemTypeForTransfer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PersonInCharge")) Then inventoryTransfer.PersonInCharge = dr("PersonInCharge").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptDate")) Then inventoryTransfer.ReceiptDate = CType(dr("ReceiptDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptNo")) Then inventoryTransfer.ReceiptNo = dr("ReceiptNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNo")) Then inventoryTransfer.ReferenceNo = dr("ReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchVehicle")) Then inventoryTransfer.SearchVehicle = dr("SearchVehicle").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SourceData")) Then inventoryTransfer.SourceData = dr("SourceData").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("State")) Then inventoryTransfer.State = CType(dr("State"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ToDealer")) Then inventoryTransfer.ToDealer = dr("ToDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ToSite")) Then inventoryTransfer.ToSite = dr("ToSite").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then inventoryTransfer.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionType")) Then inventoryTransfer.TransactionType = CType(dr("TransactionType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferStatus")) Then inventoryTransfer.TransferStatus = CType(dr("TransferStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferStep")) Then inventoryTransfer.TransferStep = CType(dr("TransferStep"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("WONo")) Then inventoryTransfer.WONo = dr("WONo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then inventoryTransfer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then inventoryTransfer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then inventoryTransfer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then inventoryTransfer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then inventoryTransfer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return inventoryTransfer

        End Function

        Private Sub SetTableName()

            If Not (GetType(InventoryTransfer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(InventoryTransfer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(InventoryTransfer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

