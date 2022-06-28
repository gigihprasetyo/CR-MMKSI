#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateInventoryTransactionExample class
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
    public class APICreateInventoryTransactionExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                DealerCode = "0",
                InventoryTransactionNo = "No-1",
                InventoryTransferNo = "TrfNo-1",
                Owner = "Me",
                PersonInCharge = "You",
                ProcessCode = "PC-1",
                SourceData = "Source Info",
                State = 1,
                TransactionDate = DateTime.Today,
                TransactionType = 1,
                WONo = "WO-1",
                InventoryTransactionDetails = new List<object>{
                    new {
                		UpdatedBy = "DealerUser",
                        BaseQuantity = 0,
                        BatchNo = "BATCH-1",
                        BU = "BU-1",
                        Department = "DEPT-1",
                        Description = "More Info",
                        FromBU = "BU Source",
                        InventoryTransactionNo = "No-1",
                        InventoryTransferDetail ="Detail Transfer",
                        InventoryUnit = "Unit-1",
                        Location = "Loc-1",
                        Owner = "Me",
                        Product = "The Product",
                        ProductCrossReference = "Cross Reff",
                        ProductDescription = "My Product Info",
                        Quantity = 1,
                        ReasonCode = "",
                        ReferenceNo = "Reff-1",
                        RegisterSerialNumber = "REG-1",
                        RunningNumber = 1,
                        SerialNo = "00001",
                        ServicePartsAndMaterial = "",
                        Site = "",
                        SourceData = "SRC-1",
                        StockNumber = "STK-1",
                        StockNumberNV = "STK-NV-1",
                        TotalCost = 1,
                        TransactionType = 1,
                        TransactionUnit = "TU",
                        UnitCost = 1000,
                        VIN = "VIN-1",
                        Warehouse = "WH-1"
                    }
                }
            };

            return obj;
        }
    }
}