
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MitraisTeam
'// PURPOSE       : InventoryTransferDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 25/03/2018 - 21:48:37
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

    Public Class InventoryTransferDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertInventoryTransferDetail"
        Private m_UpdateStatement As String = "up_UpdateInventoryTransferDetail"
        Private m_RetrieveStatement As String = "up_RetrieveInventoryTransferDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveInventoryTransferDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteInventoryTransferDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim inventoryTransferDetail As InventoryTransferDetail = Nothing
            While dr.Read

                inventoryTransferDetail = Me.CreateObject(dr)

            End While

            Return inventoryTransferDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim inventoryTransferDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim inventoryTransferDetail As InventoryTransferDetail = Me.CreateObject(dr)
                inventoryTransferDetailList.Add(inventoryTransferDetail)
            End While

            Return inventoryTransferDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim inventoryTransferDetail As InventoryTransferDetail = CType(obj, InventoryTransferDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, inventoryTransferDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim inventoryTransferDetail As InventoryTransferDetail = CType(obj, InventoryTransferDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, inventoryTransferDetail.Owner)
            DbCommandWrapper.AddInParameter("@BaseQuantity", DbType.Double, inventoryTransferDetail.BaseQuantity)
            DbCommandWrapper.AddInParameter("@ConsumptionTaxIn", DbType.AnsiString, inventoryTransferDetail.ConsumptionTaxIn)
            DbCommandWrapper.AddInParameter("@ConsumptionTaxOut", DbType.AnsiString, inventoryTransferDetail.ConsumptionTaxOut)
            DbCommandWrapper.AddInParameter("@FromBatchNo", DbType.AnsiString, inventoryTransferDetail.FromBatchNo)
            DbCommandWrapper.AddInParameter("@FromDealer", DbType.AnsiString, inventoryTransferDetail.FromDealer)
            DbCommandWrapper.AddInParameter("@FromConfiguration", DbType.AnsiString, inventoryTransferDetail.FromConfiguration)
            DbCommandWrapper.AddInParameter("@FromExteriorColor", DbType.AnsiString, inventoryTransferDetail.FromExteriorColor)
            DbCommandWrapper.AddInParameter("@FromInteriorColor", DbType.AnsiString, inventoryTransferDetail.FromInteriorColor)
            DbCommandWrapper.AddInParameter("@FromLocation", DbType.AnsiString, inventoryTransferDetail.FromLocation)
            DbCommandWrapper.AddInParameter("@FromSerialNo", DbType.AnsiString, inventoryTransferDetail.FromSerialNo)
            DbCommandWrapper.AddInParameter("@FromSite", DbType.AnsiString, inventoryTransferDetail.FromSite)
            DbCommandWrapper.AddInParameter("@FromStyle", DbType.AnsiString, inventoryTransferDetail.FromStyle)
            DbCommandWrapper.AddInParameter("@FromWarehouse", DbType.AnsiString, inventoryTransferDetail.FromWarehouse)
            DbCommandWrapper.AddInParameter("@InventoryTransferNo", DbType.AnsiString, inventoryTransferDetail.InventoryTransferNo)
            DbCommandWrapper.AddInParameter("@InventoryUnit", DbType.AnsiString, inventoryTransferDetail.InventoryUnit)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, inventoryTransferDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, inventoryTransferDetail.Product)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Double, inventoryTransferDetail.Quantity)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, inventoryTransferDetail.Remarks)
            DbCommandWrapper.AddInParameter("@ServicePartsandMaterial", DbType.AnsiString, inventoryTransferDetail.ServicePartsandMaterial)
            DbCommandWrapper.AddInParameter("@SourceData", DbType.AnsiString, inventoryTransferDetail.SourceData)
            DbCommandWrapper.AddInParameter("@StockNumber", DbType.AnsiString, inventoryTransferDetail.StockNumber)
            DbCommandWrapper.AddInParameter("@StockNumberNV", DbType.AnsiString, inventoryTransferDetail.StockNumberNV)
            DbCommandWrapper.AddInParameter("@StockNumberLookupName", DbType.AnsiString, inventoryTransferDetail.StockNumberLookupName)
            DbCommandWrapper.AddInParameter("@StockNumberLookupType", DbType.Int32, inventoryTransferDetail.StockNumberLookupType)
            DbCommandWrapper.AddInParameter("@ToBatchNo", DbType.AnsiString, inventoryTransferDetail.ToBatchNo)
            DbCommandWrapper.AddInParameter("@ToDealer", DbType.AnsiString, inventoryTransferDetail.ToDealer)
            DbCommandWrapper.AddInParameter("@ToConfiguration", DbType.AnsiString, inventoryTransferDetail.ToConfiguration)
            DbCommandWrapper.AddInParameter("@ToExteriorColor", DbType.AnsiString, inventoryTransferDetail.ToExteriorColor)
            DbCommandWrapper.AddInParameter("@ToInteriorColor", DbType.AnsiString, inventoryTransferDetail.ToInteriorColor)
            DbCommandWrapper.AddInParameter("@ToLocation", DbType.AnsiString, inventoryTransferDetail.ToLocation)
            DbCommandWrapper.AddInParameter("@ToSerialNo", DbType.AnsiString, inventoryTransferDetail.ToSerialNo)
            DbCommandWrapper.AddInParameter("@ToSite", DbType.AnsiString, inventoryTransferDetail.ToSite)
            DbCommandWrapper.AddInParameter("@ToStyle", DbType.AnsiString, inventoryTransferDetail.ToStyle)
            DbCommandWrapper.AddInParameter("@ToWarehouse", DbType.AnsiString, inventoryTransferDetail.ToWarehouse)
            DbCommandWrapper.AddInParameter("@TransactionUnit", DbType.AnsiString, inventoryTransferDetail.TransactionUnit)
            DbCommandWrapper.AddInParameter("@VIN", DbType.AnsiString, inventoryTransferDetail.VIN)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, inventoryTransferDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, inventoryTransferDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@InventoryTransferID", DbType.Int32, Me.GetRefObject(inventoryTransferDetail.InventoryTransfer))

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

            Dim inventoryTransferDetail As InventoryTransferDetail = CType(obj, InventoryTransferDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, inventoryTransferDetail.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, inventoryTransferDetail.Owner)
            DbCommandWrapper.AddInParameter("@BaseQuantity", DbType.Double, inventoryTransferDetail.BaseQuantity)
            DbCommandWrapper.AddInParameter("@ConsumptionTaxIn", DbType.AnsiString, inventoryTransferDetail.ConsumptionTaxIn)
            DbCommandWrapper.AddInParameter("@ConsumptionTaxOut", DbType.AnsiString, inventoryTransferDetail.ConsumptionTaxOut)
            DbCommandWrapper.AddInParameter("@FromBatchNo", DbType.AnsiString, inventoryTransferDetail.FromBatchNo)
            DbCommandWrapper.AddInParameter("@FromDealer", DbType.AnsiString, inventoryTransferDetail.FromDealer)
            DbCommandWrapper.AddInParameter("@FromConfiguration", DbType.AnsiString, inventoryTransferDetail.FromConfiguration)
            DbCommandWrapper.AddInParameter("@FromExteriorColor", DbType.AnsiString, inventoryTransferDetail.FromExteriorColor)
            DbCommandWrapper.AddInParameter("@FromInteriorColor", DbType.AnsiString, inventoryTransferDetail.FromInteriorColor)
            DbCommandWrapper.AddInParameter("@FromLocation", DbType.AnsiString, inventoryTransferDetail.FromLocation)
            DbCommandWrapper.AddInParameter("@FromSerialNo", DbType.AnsiString, inventoryTransferDetail.FromSerialNo)
            DbCommandWrapper.AddInParameter("@FromSite", DbType.AnsiString, inventoryTransferDetail.FromSite)
            DbCommandWrapper.AddInParameter("@FromStyle", DbType.AnsiString, inventoryTransferDetail.FromStyle)
            DbCommandWrapper.AddInParameter("@FromWarehouse", DbType.AnsiString, inventoryTransferDetail.FromWarehouse)
            DbCommandWrapper.AddInParameter("@InventoryTransferNo", DbType.AnsiString, inventoryTransferDetail.InventoryTransferNo)
            DbCommandWrapper.AddInParameter("@InventoryUnit", DbType.AnsiString, inventoryTransferDetail.InventoryUnit)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, inventoryTransferDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, inventoryTransferDetail.Product)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Double, inventoryTransferDetail.Quantity)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, inventoryTransferDetail.Remarks)
            DbCommandWrapper.AddInParameter("@ServicePartsandMaterial", DbType.AnsiString, inventoryTransferDetail.ServicePartsandMaterial)
            DbCommandWrapper.AddInParameter("@SourceData", DbType.AnsiString, inventoryTransferDetail.SourceData)
            DbCommandWrapper.AddInParameter("@StockNumber", DbType.AnsiString, inventoryTransferDetail.StockNumber)
            DbCommandWrapper.AddInParameter("@StockNumberNV", DbType.AnsiString, inventoryTransferDetail.StockNumberNV)
            DbCommandWrapper.AddInParameter("@StockNumberLookupName", DbType.AnsiString, inventoryTransferDetail.StockNumberLookupName)
            DbCommandWrapper.AddInParameter("@StockNumberLookupType", DbType.Int32, inventoryTransferDetail.StockNumberLookupType)
            DbCommandWrapper.AddInParameter("@ToBatchNo", DbType.AnsiString, inventoryTransferDetail.ToBatchNo)
            DbCommandWrapper.AddInParameter("@ToDealer", DbType.AnsiString, inventoryTransferDetail.ToDealer)
            DbCommandWrapper.AddInParameter("@ToConfiguration", DbType.AnsiString, inventoryTransferDetail.ToConfiguration)
            DbCommandWrapper.AddInParameter("@ToExteriorColor", DbType.AnsiString, inventoryTransferDetail.ToExteriorColor)
            DbCommandWrapper.AddInParameter("@ToInteriorColor", DbType.AnsiString, inventoryTransferDetail.ToInteriorColor)
            DbCommandWrapper.AddInParameter("@ToLocation", DbType.AnsiString, inventoryTransferDetail.ToLocation)
            DbCommandWrapper.AddInParameter("@ToSerialNo", DbType.AnsiString, inventoryTransferDetail.ToSerialNo)
            DbCommandWrapper.AddInParameter("@ToSite", DbType.AnsiString, inventoryTransferDetail.ToSite)
            DbCommandWrapper.AddInParameter("@ToStyle", DbType.AnsiString, inventoryTransferDetail.ToStyle)
            DbCommandWrapper.AddInParameter("@ToWarehouse", DbType.AnsiString, inventoryTransferDetail.ToWarehouse)
            DbCommandWrapper.AddInParameter("@TransactionUnit", DbType.AnsiString, inventoryTransferDetail.TransactionUnit)
            DbCommandWrapper.AddInParameter("@VIN", DbType.AnsiString, inventoryTransferDetail.VIN)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, inventoryTransferDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, inventoryTransferDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@InventoryTransferID", DbType.Int32, Me.GetRefObject(inventoryTransferDetail.InventoryTransfer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As InventoryTransferDetail

            Dim inventoryTransferDetail As InventoryTransferDetail = New InventoryTransferDetail

            inventoryTransferDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then inventoryTransferDetail.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BaseQuantity")) Then inventoryTransferDetail.BaseQuantity = CType(dr("BaseQuantity"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTaxIn")) Then inventoryTransferDetail.ConsumptionTaxIn = dr("ConsumptionTaxIn").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTaxOut")) Then inventoryTransferDetail.ConsumptionTaxOut = dr("ConsumptionTaxOut").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromBatchNo")) Then inventoryTransferDetail.FromBatchNo = dr("FromBatchNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromDealer")) Then inventoryTransferDetail.FromDealer = dr("FromDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromConfiguration")) Then inventoryTransferDetail.FromConfiguration = dr("FromConfiguration").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromExteriorColor")) Then inventoryTransferDetail.FromExteriorColor = dr("FromExteriorColor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromInteriorColor")) Then inventoryTransferDetail.FromInteriorColor = dr("FromInteriorColor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromLocation")) Then inventoryTransferDetail.FromLocation = dr("FromLocation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromSerialNo")) Then inventoryTransferDetail.FromSerialNo = dr("FromSerialNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromSite")) Then inventoryTransferDetail.FromSite = dr("FromSite").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromStyle")) Then inventoryTransferDetail.FromStyle = dr("FromStyle").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FromWarehouse")) Then inventoryTransferDetail.FromWarehouse = dr("FromWarehouse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryTransferNo")) Then inventoryTransferDetail.InventoryTransferNo = dr("InventoryTransferNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryUnit")) Then inventoryTransferDetail.InventoryUnit = dr("InventoryUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductDescription")) Then inventoryTransferDetail.ProductDescription = dr("ProductDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Product")) Then inventoryTransferDetail.Product = dr("Product").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then inventoryTransferDetail.Quantity = CType(dr("Quantity"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("Remarks")) Then inventoryTransferDetail.Remarks = dr("Remarks").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServicePartsandMaterial")) Then inventoryTransferDetail.ServicePartsandMaterial = dr("ServicePartsandMaterial").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SourceData")) Then inventoryTransferDetail.SourceData = dr("SourceData").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockNumber")) Then inventoryTransferDetail.StockNumber = dr("StockNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockNumberNV")) Then inventoryTransferDetail.StockNumberNV = dr("StockNumberNV").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockNumberLookupName")) Then inventoryTransferDetail.StockNumberLookupName = dr("StockNumberLookupName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockNumberLookupType")) Then inventoryTransferDetail.StockNumberLookupType = CType(dr("StockNumberLookupType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ToBatchNo")) Then inventoryTransferDetail.ToBatchNo = dr("ToBatchNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ToDealer")) Then inventoryTransferDetail.ToDealer = dr("ToDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ToConfiguration")) Then inventoryTransferDetail.ToConfiguration = dr("ToConfiguration").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ToExteriorColor")) Then inventoryTransferDetail.ToExteriorColor = dr("ToExteriorColor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ToInteriorColor")) Then inventoryTransferDetail.ToInteriorColor = dr("ToInteriorColor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ToLocation")) Then inventoryTransferDetail.ToLocation = dr("ToLocation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ToSerialNo")) Then inventoryTransferDetail.ToSerialNo = dr("ToSerialNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ToSite")) Then inventoryTransferDetail.ToSite = dr("ToSite").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ToStyle")) Then inventoryTransferDetail.ToStyle = dr("ToStyle").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ToWarehouse")) Then inventoryTransferDetail.ToWarehouse = dr("ToWarehouse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionUnit")) Then inventoryTransferDetail.TransactionUnit = dr("TransactionUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VIN")) Then inventoryTransferDetail.VIN = dr("VIN").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then inventoryTransferDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then inventoryTransferDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then inventoryTransferDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then inventoryTransferDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then inventoryTransferDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryTransferID")) Then
                inventoryTransferDetail.InventoryTransfer = New InventoryTransfer(CType(dr("InventoryTransferID"), Integer))
            End If

            Return inventoryTransferDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(InventoryTransferDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(InventoryTransferDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(InventoryTransferDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

