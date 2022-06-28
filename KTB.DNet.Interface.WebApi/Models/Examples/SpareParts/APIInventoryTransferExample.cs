#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIInventoryTransferExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;
using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIInventoryTransferExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                Owner = "test owner",
                FromDealer = "test FromDealer",
                FromSite = "test FromSite",
                InventoryTransferNo = "test InventoryTransferNo",
                ItemTypeForTransfer = 0,
                PersonInCharge = "test PersonInCharge",
                ReceiptDate = DateTime.Now,
                ReceiptNo = "test ReceiptNo",
                ReferenceNo = "test ReferenceNo",
                SearchVehicle = "test SearchVehicle",
                SourceData = "test SourceData",
                State = 1,
                ToDealer = "test ToDealer",
                ToSite = "test ToSite",
                TransactionDate = DateTime.Now,
                TransactionType = 1,
                TransferStatus = 0,
                TransferStep = false,
                WONo = "test WO No",
                InventoryTransferDetails = new List<object>{
                    new {
                		UpdatedBy = "DealerUser",
                        Owner = "test owner",
                        BaseQuantity = 2.5,
                        ConsumptionTaxIn = "test ConsumptionTaxIn",
                        ConsumptionTaxOut = "test ConsumptionTaxOut",
                        FromBatchNo = "test FromBatchNo",
                        FromDealer = "test FromDealer",
                        FromConfiguration = "test FromConfiguration",
                        FromExteriorColor = "test FromExteriorColor",
                        FromInteriorColor = "test FromInteriorColor",
                        FromLocation = "test FromLocation",
                        FromSerialNo = "test FromSerialNo",
                        FromSite = "test FromSite",
                        FromStyle = "test ToDealer",
                        FromWarehouse = "test FromWarehouse",
                        InventoryTransferNo = "test InventoryTransferNo",
                        InventoryUnit = "test InventoryUnit",
                        ProductDescription = "test ProductDescription",
                        Product = "test Product",
                        Quantity =  3.5,
                        Remarks= "test Remarks",
                        ServicePartsandMaterial = "test ServicePartsandMaterial",
                        SourceData = "test SourceData",
                        StockNumber = "test StockNumber",
                        StockNumberNV = "test StockNumberNV",
                        StockNumberLookupName = "test StockNumberLookupName",
                        StockNumberLookupType = 3,
                        ToBatchNo = "test ToBatchNo",
                        ToDealer = "test ToDealer",
                        ToConfiguration = "test ToConfiguration",
                        ToExteriorColor = "test ToExteriorColor",
                        ToInteriorColor = "test ToInteriorColor",
                        ToLocation = "test ToLocation",
                        ToSerialNo = "test ToSerialNo",
                        ToSite = "test ToSite",
                        ToStyle = "test ToStyle",
                        ToWarehouse = "test ToWarehouse",
                        TransactionUnit = "test TransactionUnit",
                        VIN = "test VIN"
                    }
                }
            };
            return obj;
        }
    }
}