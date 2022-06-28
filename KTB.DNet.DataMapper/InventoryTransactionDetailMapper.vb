
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MItrais Team
'// PURPOSE       : InventoryTransactionDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 26/03/2018 - 13:44:28
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

    Public Class InventoryTransactionDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertInventoryTransactionDetail"
        Private m_UpdateStatement As String = "up_UpdateInventoryTransactionDetail"
        Private m_RetrieveStatement As String = "up_RetrieveInventoryTransactionDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveInventoryTransactionDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteInventoryTransactionDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim inventoryTransactionDetail As InventoryTransactionDetail = Nothing
            While dr.Read

                inventoryTransactionDetail = Me.CreateObject(dr)

            End While

            Return inventoryTransactionDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim inventoryTransactionDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim inventoryTransactionDetail As InventoryTransactionDetail = Me.CreateObject(dr)
                inventoryTransactionDetailList.Add(inventoryTransactionDetail)
            End While

            Return inventoryTransactionDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim inventoryTransactionDetail As InventoryTransactionDetail = CType(obj, InventoryTransactionDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, inventoryTransactionDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim inventoryTransactionDetail As InventoryTransactionDetail = CType(obj, InventoryTransactionDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, inventoryTransactionDetail.Owner)
            DbCommandWrapper.AddInParameter("@BaseQuantity", DbType.Double, inventoryTransactionDetail.BaseQuantity)
            DbCommandWrapper.AddInParameter("@BatchNo", DbType.AnsiString, inventoryTransactionDetail.BatchNo)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, inventoryTransactionDetail.BU)
            DbCommandWrapper.AddInParameter("@Department", DbType.AnsiString, inventoryTransactionDetail.Department)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, inventoryTransactionDetail.Description)
            DbCommandWrapper.AddInParameter("@FromBU", DbType.AnsiString, inventoryTransactionDetail.FromBU)
            DbCommandWrapper.AddInParameter("@InventoryTransactionNo", DbType.AnsiString, inventoryTransactionDetail.InventoryTransactionNo)
            DbCommandWrapper.AddInParameter("@InventoryTransferDetail", DbType.AnsiString, inventoryTransactionDetail.InventoryTransferDetail)
            DbCommandWrapper.AddInParameter("@InventoryUnit", DbType.AnsiString, inventoryTransactionDetail.InventoryUnit)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, inventoryTransactionDetail.Location)
            DbCommandWrapper.AddInParameter("@ProductCrossReference", DbType.AnsiString, inventoryTransactionDetail.ProductCrossReference)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, inventoryTransactionDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, inventoryTransactionDetail.Product)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Double, inventoryTransactionDetail.Quantity)
            DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, inventoryTransactionDetail.ReasonCode)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, inventoryTransactionDetail.ReferenceNo)
            DbCommandWrapper.AddInParameter("@RegisterSerialNumber", DbType.AnsiString, inventoryTransactionDetail.RegisterSerialNumber)
            DbCommandWrapper.AddInParameter("@RunningNumber", DbType.Int32, inventoryTransactionDetail.RunningNumber)
            DbCommandWrapper.AddInParameter("@SerialNo", DbType.AnsiString, inventoryTransactionDetail.SerialNo)
            DbCommandWrapper.AddInParameter("@ServicePartsAndMaterial", DbType.AnsiString, inventoryTransactionDetail.ServicePartsAndMaterial)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, inventoryTransactionDetail.Site)
            DbCommandWrapper.AddInParameter("@SourceData", DbType.AnsiString, inventoryTransactionDetail.SourceData)
            DbCommandWrapper.AddInParameter("@StockNumber", DbType.AnsiString, inventoryTransactionDetail.StockNumber)
            DbCommandWrapper.AddInParameter("@StockNumberNV", DbType.AnsiString, inventoryTransactionDetail.StockNumberNV)
            DbCommandWrapper.AddInParameter("@TotalCost", DbType.Currency, inventoryTransactionDetail.TotalCost)
            DbCommandWrapper.AddInParameter("@TransactionType", DbType.AnsiString, inventoryTransactionDetail.TransactionType)
            DbCommandWrapper.AddInParameter("@TransactionUnit", DbType.AnsiString, inventoryTransactionDetail.TransactionUnit)
            DbCommandWrapper.AddInParameter("@UnitCost", DbType.Currency, inventoryTransactionDetail.UnitCost)
            DbCommandWrapper.AddInParameter("@VIN", DbType.AnsiString, inventoryTransactionDetail.VIN)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, inventoryTransactionDetail.Warehouse)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, inventoryTransactionDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, inventoryTransactionDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@InventoryTransactionID", DbType.Int32, Me.GetRefObject(inventoryTransactionDetail.InventoryTransaction))

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

            Dim inventoryTransactionDetail As InventoryTransactionDetail = CType(obj, InventoryTransactionDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, inventoryTransactionDetail.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, inventoryTransactionDetail.Owner)
            DbCommandWrapper.AddInParameter("@BaseQuantity", DbType.Double, inventoryTransactionDetail.BaseQuantity)
            DbCommandWrapper.AddInParameter("@BatchNo", DbType.AnsiString, inventoryTransactionDetail.BatchNo)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, inventoryTransactionDetail.BU)
            DbCommandWrapper.AddInParameter("@Department", DbType.AnsiString, inventoryTransactionDetail.Department)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, inventoryTransactionDetail.Description)
            DbCommandWrapper.AddInParameter("@FromBU", DbType.AnsiString, inventoryTransactionDetail.FromBU)
            DbCommandWrapper.AddInParameter("@InventoryTransactionNo", DbType.AnsiString, inventoryTransactionDetail.InventoryTransactionNo)
            DbCommandWrapper.AddInParameter("@InventoryTransferDetail", DbType.AnsiString, inventoryTransactionDetail.InventoryTransferDetail)
            DbCommandWrapper.AddInParameter("@InventoryUnit", DbType.AnsiString, inventoryTransactionDetail.InventoryUnit)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, inventoryTransactionDetail.Location)
            DbCommandWrapper.AddInParameter("@ProductCrossReference", DbType.AnsiString, inventoryTransactionDetail.ProductCrossReference)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, inventoryTransactionDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, inventoryTransactionDetail.Product)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Double, inventoryTransactionDetail.Quantity)
            DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, inventoryTransactionDetail.ReasonCode)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, inventoryTransactionDetail.ReferenceNo)
            DbCommandWrapper.AddInParameter("@RegisterSerialNumber", DbType.AnsiString, inventoryTransactionDetail.RegisterSerialNumber)
            DbCommandWrapper.AddInParameter("@RunningNumber", DbType.Int32, inventoryTransactionDetail.RunningNumber)
            DbCommandWrapper.AddInParameter("@SerialNo", DbType.AnsiString, inventoryTransactionDetail.SerialNo)
            DbCommandWrapper.AddInParameter("@ServicePartsAndMaterial", DbType.AnsiString, inventoryTransactionDetail.ServicePartsAndMaterial)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, inventoryTransactionDetail.Site)
            DbCommandWrapper.AddInParameter("@SourceData", DbType.AnsiString, inventoryTransactionDetail.SourceData)
            DbCommandWrapper.AddInParameter("@StockNumber", DbType.AnsiString, inventoryTransactionDetail.StockNumber)
            DbCommandWrapper.AddInParameter("@StockNumberNV", DbType.AnsiString, inventoryTransactionDetail.StockNumberNV)
            DbCommandWrapper.AddInParameter("@TotalCost", DbType.Currency, inventoryTransactionDetail.TotalCost)
            DbCommandWrapper.AddInParameter("@TransactionType", DbType.AnsiString, inventoryTransactionDetail.TransactionType)
            DbCommandWrapper.AddInParameter("@TransactionUnit", DbType.AnsiString, inventoryTransactionDetail.TransactionUnit)
            DbCommandWrapper.AddInParameter("@UnitCost", DbType.Currency, inventoryTransactionDetail.UnitCost)
            DbCommandWrapper.AddInParameter("@VIN", DbType.AnsiString, inventoryTransactionDetail.VIN)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, inventoryTransactionDetail.Warehouse)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, inventoryTransactionDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, inventoryTransactionDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@InventoryTransactionID", DbType.Int32, Me.GetRefObject(inventoryTransactionDetail.InventoryTransaction))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As InventoryTransactionDetail

            Dim inventoryTransactionDetail As InventoryTransactionDetail = New InventoryTransactionDetail

            inventoryTransactionDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then inventoryTransactionDetail.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BaseQuantity")) Then inventoryTransactionDetail.BaseQuantity = CType(dr("BaseQuantity"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("BatchNo")) Then inventoryTransactionDetail.BatchNo = dr("BatchNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BU")) Then inventoryTransactionDetail.BU = dr("BU").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Department")) Then inventoryTransactionDetail.Department = dr("Department").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then inventoryTransactionDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromBU")) Then inventoryTransactionDetail.FromBU = dr("FromBU").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryTransactionNo")) Then inventoryTransactionDetail.InventoryTransactionNo = dr("InventoryTransactionNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryTransferDetail")) Then inventoryTransactionDetail.InventoryTransferDetail = dr("InventoryTransferDetail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryUnit")) Then inventoryTransactionDetail.InventoryUnit = dr("InventoryUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Location")) Then inventoryTransactionDetail.Location = dr("Location").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCrossReference")) Then inventoryTransactionDetail.ProductCrossReference = dr("ProductCrossReference").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductDescription")) Then inventoryTransactionDetail.ProductDescription = dr("ProductDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Product")) Then inventoryTransactionDetail.Product = dr("Product").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then inventoryTransactionDetail.Quantity = CType(dr("Quantity"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("ReasonCode")) Then inventoryTransactionDetail.ReasonCode = dr("ReasonCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNo")) Then inventoryTransactionDetail.ReferenceNo = dr("ReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegisterSerialNumber")) Then inventoryTransactionDetail.RegisterSerialNumber = dr("RegisterSerialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RunningNumber")) Then inventoryTransactionDetail.RunningNumber = CType(dr("RunningNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SerialNo")) Then inventoryTransactionDetail.SerialNo = dr("SerialNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServicePartsAndMaterial")) Then inventoryTransactionDetail.ServicePartsAndMaterial = dr("ServicePartsAndMaterial").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Site")) Then inventoryTransactionDetail.Site = dr("Site").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SourceData")) Then inventoryTransactionDetail.SourceData = dr("SourceData").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockNumber")) Then inventoryTransactionDetail.StockNumber = dr("StockNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockNumberNV")) Then inventoryTransactionDetail.StockNumberNV = dr("StockNumberNV").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalCost")) Then inventoryTransactionDetail.TotalCost = CType(dr("TotalCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionType")) Then inventoryTransactionDetail.TransactionType = dr("TransactionType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionUnit")) Then inventoryTransactionDetail.TransactionUnit = dr("TransactionUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UnitCost")) Then inventoryTransactionDetail.UnitCost = CType(dr("UnitCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("VIN")) Then inventoryTransactionDetail.VIN = dr("VIN").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Warehouse")) Then inventoryTransactionDetail.Warehouse = dr("Warehouse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then inventoryTransactionDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then inventoryTransactionDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then inventoryTransactionDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then inventoryTransactionDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then inventoryTransactionDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryTransactionID")) Then
                inventoryTransactionDetail.InventoryTransaction = New InventoryTransaction(CType(dr("InventoryTransactionID"), Integer))
            End If

            Return inventoryTransactionDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(InventoryTransactionDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(InventoryTransactionDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(InventoryTransactionDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

