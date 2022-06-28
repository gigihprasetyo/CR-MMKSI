#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateVehiclePurchaseDetailExample class
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

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateVehiclePurchaseDetailExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                BUID = 1,
                BUName = "BU Name Test",
                CloseLine = true,
                CloseLineName = "CloseLineName",
                CloseReason = "CloseReason",
                Completed = true,
                CompletedName = "CompletedName",
                ProductDescription = "ProductDescription",
                ProductID = 1,
                ProductName = "ProductName",
                ProductVariantID = 1,
                ProductVariantName = "ProductVariantName",
                PODetail = "PODetail",
                PODetailID = "PODetailID",
                PONO = 1,
                POName = "POName",
                PRDetailID = 1,
                PRDetailName = "PRDetailName",
                PurchaseUnitID = 1,
                PurchaseUnitName = "PurchaseUnitName",
                QtyOrder = 1,
                QtyReceipt = 1,
                QtyReturn = 1,
                RecallProduct = true,
                RecallProductName = "RecallProductName",
                SODetail = 1,
                SODetailName = "SODetailName",
                ScheduledShippingDate = DateTime.Now,
                ServicePartsAndMaterial = "string",
                ShippingDate = DateTime.Now,
                Site = 1,
                StockNumberID = 1,
                StockNumberName = "StockNumberName"
            };

            return obj;
        }
    }
}