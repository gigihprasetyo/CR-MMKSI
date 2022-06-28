
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MItrais Team
'// PURPOSE       : POOtherVendorDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/03/2018 - 15:50:57
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

    Public Class POOtherVendorDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPOOtherVendorDetail"
        Private m_UpdateStatement As String = "up_UpdatePOOtherVendorDetail"
        Private m_RetrieveStatement As String = "up_RetrievePOOtherVendorDetail"
        Private m_RetrieveListStatement As String = "up_RetrievePOOtherVendorDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePOOtherVendorDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pOOtherVendorDetail As POOtherVendorDetail = Nothing
            While dr.Read

                pOOtherVendorDetail = Me.CreateObject(dr)

            End While

            Return pOOtherVendorDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pOOtherVendorDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim pOOtherVendorDetail As POOtherVendorDetail = Me.CreateObject(dr)
                pOOtherVendorDetailList.Add(pOOtherVendorDetail)
            End While

            Return pOOtherVendorDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pOOtherVendorDetail As POOtherVendorDetail = CType(obj, POOtherVendorDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pOOtherVendorDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pOOtherVendorDetail As POOtherVendorDetail = CType(obj, POOtherVendorDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, pOOtherVendorDetail.Owner)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, pOOtherVendorDetail.DealerCode)
            DbCommandWrapper.AddInParameter("@CloseLine", DbType.Boolean, pOOtherVendorDetail.CloseLine)
            DbCommandWrapper.AddInParameter("@CloseReason", DbType.AnsiString, pOOtherVendorDetail.CloseReason)
            DbCommandWrapper.AddInParameter("@Completed", DbType.Boolean, pOOtherVendorDetail.Completed)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1Amount", DbType.Currency, pOOtherVendorDetail.ConsumptionTax1Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1", DbType.AnsiString, pOOtherVendorDetail.ConsumptionTax1)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2Amount", DbType.Currency, pOOtherVendorDetail.ConsumptionTax2Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2", DbType.AnsiString, pOOtherVendorDetail.ConsumptionTax2)
            DbCommandWrapper.AddInParameter("@Department", DbType.AnsiString, pOOtherVendorDetail.Department)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pOOtherVendorDetail.Description)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, pOOtherVendorDetail.DiscountAmount)
            DbCommandWrapper.AddInParameter("@DiscountPercentage", DbType.Double, pOOtherVendorDetail.DiscountPercentage)
            DbCommandWrapper.AddInParameter("@EventData", DbType.AnsiString, pOOtherVendorDetail.EventData)
            DbCommandWrapper.AddInParameter("@FormSource", DbType.Int16, pOOtherVendorDetail.FormSource)
            DbCommandWrapper.AddInParameter("@BaseQtyOrder", DbType.Double, pOOtherVendorDetail.BaseQtyOrder)
            DbCommandWrapper.AddInParameter("@BaseQtyReceipt", DbType.Double, pOOtherVendorDetail.BaseQtyReceipt)
            DbCommandWrapper.AddInParameter("@BaseQtyReturn", DbType.Double, pOOtherVendorDetail.BaseQtyReturn)
            DbCommandWrapper.AddInParameter("@InventoryUnit", DbType.AnsiString, pOOtherVendorDetail.InventoryUnit)
            DbCommandWrapper.AddInParameter("@ProductCrossReference", DbType.AnsiString, pOOtherVendorDetail.ProductCrossReference)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, pOOtherVendorDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, pOOtherVendorDetail.Product)
            DbCommandWrapper.AddInParameter("@ProductSubstitute", DbType.AnsiString, pOOtherVendorDetail.ProductSubstitute)
            DbCommandWrapper.AddInParameter("@ProductVariant", DbType.AnsiString, pOOtherVendorDetail.ProductVariant)
            DbCommandWrapper.AddInParameter("@ProductVolume", DbType.Double, pOOtherVendorDetail.ProductVolume)
            DbCommandWrapper.AddInParameter("@ProductWeight", DbType.Double, pOOtherVendorDetail.ProductWeight)
            DbCommandWrapper.AddInParameter("@PromisedDate", DbType.DateTime, pOOtherVendorDetail.PromisedDate)
            DbCommandWrapper.AddInParameter("@PurchaseFor", DbType.Int16, pOOtherVendorDetail.PurchaseFor)
            DbCommandWrapper.AddInParameter("@PurchaseOrderNo", DbType.AnsiString, pOOtherVendorDetail.PurchaseOrderNo)
            DbCommandWrapper.AddInParameter("@PurchaseRequisitionDetail", DbType.AnsiString, pOOtherVendorDetail.PurchaseRequisitionDetail)
            DbCommandWrapper.AddInParameter("@PurchaseUnit", DbType.AnsiString, pOOtherVendorDetail.PurchaseUnit)
            DbCommandWrapper.AddInParameter("@QtyOrder", DbType.Double, pOOtherVendorDetail.QtyOrder)
            DbCommandWrapper.AddInParameter("@QtyReceipt", DbType.Double, pOOtherVendorDetail.QtyReceipt)
            DbCommandWrapper.AddInParameter("@QtyReturn", DbType.Double, pOOtherVendorDetail.QtyReturn)
            DbCommandWrapper.AddInParameter("@RecallProduct", DbType.Boolean, pOOtherVendorDetail.RecallProduct)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, pOOtherVendorDetail.ReferenceNo)
            DbCommandWrapper.AddInParameter("@RequiredDate", DbType.DateTime, pOOtherVendorDetail.RequiredDate)
            DbCommandWrapper.AddInParameter("@SalesOrderDetail", DbType.AnsiString, pOOtherVendorDetail.SalesOrderDetail)
            DbCommandWrapper.AddInParameter("@ScheduledShippingDate", DbType.DateTime, pOOtherVendorDetail.ScheduledShippingDate)
            DbCommandWrapper.AddInParameter("@ServicePartsAndMaterial", DbType.AnsiString, pOOtherVendorDetail.ServicePartsAndMaterial)
            DbCommandWrapper.AddInParameter("@ShippingDate", DbType.DateTime, pOOtherVendorDetail.ShippingDate)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, pOOtherVendorDetail.Site)
            DbCommandWrapper.AddInParameter("@StockNumber", DbType.AnsiString, pOOtherVendorDetail.StockNumber)
            DbCommandWrapper.AddInParameter("@TitleRegistrationFee", DbType.Currency, pOOtherVendorDetail.TitleRegistrationFee)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, pOOtherVendorDetail.TotalAmount)
            DbCommandWrapper.AddInParameter("@TotalAmountBeforeDiscount", DbType.Currency, pOOtherVendorDetail.TotalAmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, pOOtherVendorDetail.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, pOOtherVendorDetail.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalVolume", DbType.Double, pOOtherVendorDetail.TotalVolume)
            DbCommandWrapper.AddInParameter("@TotalWeight", DbType.Double, pOOtherVendorDetail.TotalWeight)
            DbCommandWrapper.AddInParameter("@TransactionAmount", DbType.Currency, pOOtherVendorDetail.TransactionAmount)
            DbCommandWrapper.AddInParameter("@UnitCost", DbType.Currency, pOOtherVendorDetail.UnitCost)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, pOOtherVendorDetail.Warehouse)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pOOtherVendorDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pOOtherVendorDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@POOtherVendorID", DbType.Int32, Me.GetRefObject(pOOtherVendorDetail.POOtherVendor))

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

            Dim pOOtherVendorDetail As POOtherVendorDetail = CType(obj, POOtherVendorDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pOOtherVendorDetail.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, pOOtherVendorDetail.Owner)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, pOOtherVendorDetail.DealerCode)
            DbCommandWrapper.AddInParameter("@CloseLine", DbType.Boolean, pOOtherVendorDetail.CloseLine)
            DbCommandWrapper.AddInParameter("@CloseReason", DbType.AnsiString, pOOtherVendorDetail.CloseReason)
            DbCommandWrapper.AddInParameter("@Completed", DbType.Boolean, pOOtherVendorDetail.Completed)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1Amount", DbType.Currency, pOOtherVendorDetail.ConsumptionTax1Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1", DbType.AnsiString, pOOtherVendorDetail.ConsumptionTax1)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2Amount", DbType.Currency, pOOtherVendorDetail.ConsumptionTax2Amount)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2", DbType.AnsiString, pOOtherVendorDetail.ConsumptionTax2)
            DbCommandWrapper.AddInParameter("@Department", DbType.AnsiString, pOOtherVendorDetail.Department)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pOOtherVendorDetail.Description)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, pOOtherVendorDetail.DiscountAmount)
            DbCommandWrapper.AddInParameter("@DiscountPercentage", DbType.Double, pOOtherVendorDetail.DiscountPercentage)
            DbCommandWrapper.AddInParameter("@EventData", DbType.AnsiString, pOOtherVendorDetail.EventData)
            DbCommandWrapper.AddInParameter("@FormSource", DbType.Int16, pOOtherVendorDetail.FormSource)
            DbCommandWrapper.AddInParameter("@BaseQtyOrder", DbType.Double, pOOtherVendorDetail.BaseQtyOrder)
            DbCommandWrapper.AddInParameter("@BaseQtyReceipt", DbType.Double, pOOtherVendorDetail.BaseQtyReceipt)
            DbCommandWrapper.AddInParameter("@BaseQtyReturn", DbType.Double, pOOtherVendorDetail.BaseQtyReturn)
            DbCommandWrapper.AddInParameter("@InventoryUnit", DbType.AnsiString, pOOtherVendorDetail.InventoryUnit)
            DbCommandWrapper.AddInParameter("@ProductCrossReference", DbType.AnsiString, pOOtherVendorDetail.ProductCrossReference)
            DbCommandWrapper.AddInParameter("@ProductDescription", DbType.AnsiString, pOOtherVendorDetail.ProductDescription)
            DbCommandWrapper.AddInParameter("@Product", DbType.AnsiString, pOOtherVendorDetail.Product)
            DbCommandWrapper.AddInParameter("@ProductSubstitute", DbType.AnsiString, pOOtherVendorDetail.ProductSubstitute)
            DbCommandWrapper.AddInParameter("@ProductVariant", DbType.AnsiString, pOOtherVendorDetail.ProductVariant)
            DbCommandWrapper.AddInParameter("@ProductVolume", DbType.Double, pOOtherVendorDetail.ProductVolume)
            DbCommandWrapper.AddInParameter("@ProductWeight", DbType.Double, pOOtherVendorDetail.ProductWeight)
            DbCommandWrapper.AddInParameter("@PromisedDate", DbType.DateTime, pOOtherVendorDetail.PromisedDate)
            DbCommandWrapper.AddInParameter("@PurchaseFor", DbType.Int16, pOOtherVendorDetail.PurchaseFor)
            DbCommandWrapper.AddInParameter("@PurchaseOrderNo", DbType.AnsiString, pOOtherVendorDetail.PurchaseOrderNo)
            DbCommandWrapper.AddInParameter("@PurchaseRequisitionDetail", DbType.AnsiString, pOOtherVendorDetail.PurchaseRequisitionDetail)
            DbCommandWrapper.AddInParameter("@PurchaseUnit", DbType.AnsiString, pOOtherVendorDetail.PurchaseUnit)
            DbCommandWrapper.AddInParameter("@QtyOrder", DbType.Double, pOOtherVendorDetail.QtyOrder)
            DbCommandWrapper.AddInParameter("@QtyReceipt", DbType.Double, pOOtherVendorDetail.QtyReceipt)
            DbCommandWrapper.AddInParameter("@QtyReturn", DbType.Double, pOOtherVendorDetail.QtyReturn)
            DbCommandWrapper.AddInParameter("@RecallProduct", DbType.Boolean, pOOtherVendorDetail.RecallProduct)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, pOOtherVendorDetail.ReferenceNo)
            DbCommandWrapper.AddInParameter("@RequiredDate", DbType.DateTime, pOOtherVendorDetail.RequiredDate)
            DbCommandWrapper.AddInParameter("@SalesOrderDetail", DbType.AnsiString, pOOtherVendorDetail.SalesOrderDetail)
            DbCommandWrapper.AddInParameter("@ScheduledShippingDate", DbType.DateTime, pOOtherVendorDetail.ScheduledShippingDate)
            DbCommandWrapper.AddInParameter("@ServicePartsAndMaterial", DbType.AnsiString, pOOtherVendorDetail.ServicePartsAndMaterial)
            DbCommandWrapper.AddInParameter("@ShippingDate", DbType.DateTime, pOOtherVendorDetail.ShippingDate)
            DbCommandWrapper.AddInParameter("@Site", DbType.AnsiString, pOOtherVendorDetail.Site)
            DbCommandWrapper.AddInParameter("@StockNumber", DbType.AnsiString, pOOtherVendorDetail.StockNumber)
            DbCommandWrapper.AddInParameter("@TitleRegistrationFee", DbType.Currency, pOOtherVendorDetail.TitleRegistrationFee)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, pOOtherVendorDetail.TotalAmount)
            DbCommandWrapper.AddInParameter("@TotalAmountBeforeDiscount", DbType.Currency, pOOtherVendorDetail.TotalAmountBeforeDiscount)
            DbCommandWrapper.AddInParameter("@TotalBaseAmount", DbType.Currency, pOOtherVendorDetail.TotalBaseAmount)
            DbCommandWrapper.AddInParameter("@TotalConsumptionTaxAmount", DbType.Currency, pOOtherVendorDetail.TotalConsumptionTaxAmount)
            DbCommandWrapper.AddInParameter("@TotalVolume", DbType.Double, pOOtherVendorDetail.TotalVolume)
            DbCommandWrapper.AddInParameter("@TotalWeight", DbType.Double, pOOtherVendorDetail.TotalWeight)
            DbCommandWrapper.AddInParameter("@TransactionAmount", DbType.Currency, pOOtherVendorDetail.TransactionAmount)
            DbCommandWrapper.AddInParameter("@UnitCost", DbType.Currency, pOOtherVendorDetail.UnitCost)
            DbCommandWrapper.AddInParameter("@Warehouse", DbType.AnsiString, pOOtherVendorDetail.Warehouse)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pOOtherVendorDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pOOtherVendorDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@POOtherVendorID", DbType.Int32, Me.GetRefObject(pOOtherVendorDetail.POOtherVendor))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As POOtherVendorDetail

            Dim pOOtherVendorDetail As POOtherVendorDetail = New POOtherVendorDetail

            pOOtherVendorDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then pOOtherVendorDetail.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then pOOtherVendorDetail.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CloseLine")) Then pOOtherVendorDetail.CloseLine = CType(dr("CloseLine"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("CloseReason")) Then pOOtherVendorDetail.CloseReason = dr("CloseReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Completed")) Then pOOtherVendorDetail.Completed = CType(dr("Completed"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax1Amount")) Then pOOtherVendorDetail.ConsumptionTax1Amount = CType(dr("ConsumptionTax1Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax1")) Then pOOtherVendorDetail.ConsumptionTax1 = dr("ConsumptionTax1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax2Amount")) Then pOOtherVendorDetail.ConsumptionTax2Amount = CType(dr("ConsumptionTax2Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax2")) Then pOOtherVendorDetail.ConsumptionTax2 = dr("ConsumptionTax2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Department")) Then pOOtherVendorDetail.Department = dr("Department").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then pOOtherVendorDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountAmount")) Then pOOtherVendorDetail.DiscountAmount = CType(dr("DiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountPercentage")) Then pOOtherVendorDetail.DiscountPercentage = CType(dr("DiscountPercentage"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("EventData")) Then pOOtherVendorDetail.EventData = dr("EventData").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FormSource")) Then pOOtherVendorDetail.FormSource = CType(dr("FormSource"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("BaseQtyOrder")) Then pOOtherVendorDetail.BaseQtyOrder = CType(dr("BaseQtyOrder"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("BaseQtyReceipt")) Then pOOtherVendorDetail.BaseQtyReceipt = CType(dr("BaseQtyReceipt"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("BaseQtyReturn")) Then pOOtherVendorDetail.BaseQtyReturn = CType(dr("BaseQtyReturn"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("InventoryUnit")) Then pOOtherVendorDetail.InventoryUnit = dr("InventoryUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCrossReference")) Then pOOtherVendorDetail.ProductCrossReference = dr("ProductCrossReference").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductDescription")) Then pOOtherVendorDetail.ProductDescription = dr("ProductDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Product")) Then pOOtherVendorDetail.Product = dr("Product").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductSubstitute")) Then pOOtherVendorDetail.ProductSubstitute = dr("ProductSubstitute").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductVariant")) Then pOOtherVendorDetail.ProductVariant = dr("ProductVariant").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductVolume")) Then pOOtherVendorDetail.ProductVolume = CType(dr("ProductVolume"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductWeight")) Then pOOtherVendorDetail.ProductWeight = CType(dr("ProductWeight"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("PromisedDate")) Then pOOtherVendorDetail.PromisedDate = CType(dr("PromisedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseFor")) Then pOOtherVendorDetail.PurchaseFor = CType(dr("PurchaseFor"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseOrderNo")) Then pOOtherVendorDetail.PurchaseOrderNo = dr("PurchaseOrderNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseRequisitionDetail")) Then pOOtherVendorDetail.PurchaseRequisitionDetail = dr("PurchaseRequisitionDetail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseUnit")) Then pOOtherVendorDetail.PurchaseUnit = dr("PurchaseUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("QtyOrder")) Then pOOtherVendorDetail.QtyOrder = CType(dr("QtyOrder"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("QtyReceipt")) Then pOOtherVendorDetail.QtyReceipt = CType(dr("QtyReceipt"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("QtyReturn")) Then pOOtherVendorDetail.QtyReturn = CType(dr("QtyReturn"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("RecallProduct")) Then pOOtherVendorDetail.RecallProduct = CType(dr("RecallProduct"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNo")) Then pOOtherVendorDetail.ReferenceNo = dr("ReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RequiredDate")) Then pOOtherVendorDetail.RequiredDate = CType(dr("RequiredDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderDetail")) Then pOOtherVendorDetail.SalesOrderDetail = dr("SalesOrderDetail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ScheduledShippingDate")) Then pOOtherVendorDetail.ScheduledShippingDate = CType(dr("ScheduledShippingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ServicePartsAndMaterial")) Then pOOtherVendorDetail.ServicePartsAndMaterial = dr("ServicePartsAndMaterial").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ShippingDate")) Then pOOtherVendorDetail.ShippingDate = CType(dr("ShippingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Site")) Then pOOtherVendorDetail.Site = dr("Site").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockNumber")) Then pOOtherVendorDetail.StockNumber = dr("StockNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TitleRegistrationFee")) Then pOOtherVendorDetail.TitleRegistrationFee = CType(dr("TitleRegistrationFee"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then pOOtherVendorDetail.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmountBeforeDiscount")) Then pOOtherVendorDetail.TotalAmountBeforeDiscount = CType(dr("TotalAmountBeforeDiscount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalBaseAmount")) Then pOOtherVendorDetail.TotalBaseAmount = CType(dr("TotalBaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTaxAmount")) Then pOOtherVendorDetail.TotalConsumptionTaxAmount = CType(dr("TotalConsumptionTaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalVolume")) Then pOOtherVendorDetail.TotalVolume = CType(dr("TotalVolume"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalWeight")) Then pOOtherVendorDetail.TotalWeight = CType(dr("TotalWeight"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionAmount")) Then pOOtherVendorDetail.TransactionAmount = CType(dr("TransactionAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("UnitCost")) Then pOOtherVendorDetail.UnitCost = CType(dr("UnitCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Warehouse")) Then pOOtherVendorDetail.Warehouse = dr("Warehouse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pOOtherVendorDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pOOtherVendorDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pOOtherVendorDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pOOtherVendorDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pOOtherVendorDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("POOtherVendorID")) Then
                pOOtherVendorDetail.POOtherVendor = New POOtherVendor(CType(dr("POOtherVendorID"), Integer))
            End If

            Return pOOtherVendorDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(POOtherVendorDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(POOtherVendorDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(POOtherVendorDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

