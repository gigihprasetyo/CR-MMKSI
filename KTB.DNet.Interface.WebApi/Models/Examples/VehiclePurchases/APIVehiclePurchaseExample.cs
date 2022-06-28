#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIVehiclePurchaseExample class
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

    public class APIVehiclePurchaseCreateExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",                
                BUCode = "BU-001",
                BUName = "Test BU Name",
                DeliveryMethod = "Test Delivery",
                Description = "Test Description",
                PRPOTypeName = "Test Type Name",
                DMSPONo = "Test PO No",
                DMSPOStatus = 0,
                DMSPODate = DateTime.Now.ToString("yyyy-MM-dd"),
                VendorDescription = "Test Vendor Description",
                Vendor = "Test Vendor",
                PurchaseOrderNo = "PO-0001",
                PurchaseReceiptNo = "PR-0001",
                PurchaseReceiptDetailNo = "PRD-001",
                ChassisModel = "Test Chassis",
                ChassisNumberRegister = "Test Chassis Number",
                VehiclePurchaseDetails = new List<object>(){ new
               {
               	   UpdatedBy = "DealerUser",
                   BUCode = "BU-001",
                   BUName = "BU Name Test",
                   CloseLine = true,
                   CloseLineName = "CloseLineName",
                   CloseReason = "CloseReason",
                   Completed = true,
                   CompletedName = "CompletedName",
                   ProductDescription = "ProductDescription",
                   ProductName = "ProductName",
                   ProductVariantName = "ProductVariantName",
                   PODetail = "PODetail",
                   POName = "POName",
                   PRDetailName = "PRDetailName",
                   PurchaseUnitName = "PurchaseUnitName",
                   QtyOrder = 1,
                   QtyReceipt = 1,
                   QtyReturn = 1,
                   RecallProduct = true,
                   RecallProductName = "RecallProductName",
                   SODetailName = "SODetailName",
                   ScheduledShippingDate = DateTime.Now.ToString("yyyy-MM-dd"),
                   ServicePartsAndMaterial = "string",
                   ShippingDate = DateTime.Now.ToString("yyyy-MM-dd"),
                   Site = "Test Site",
                   StockNumberName = "StockNumberName"
                }
              }

            };
            return obj;
        }
    }
}