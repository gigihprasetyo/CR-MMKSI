#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateVehiclePurchaseHeaderExample class
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
    public class APIUpdateVehiclePurchaseHeaderExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 1,
                BUID = 1,
                BUName = "BU Name Test",
                DeliveryMethod = "DeliveryMethod",
                Description = "Description",
                PRPOTypeID = 1,
                PRPOName = "PRPOName",
                DMSPOID = 1,
                DMSPONo = "DMSPONo",
                DMSPOStatus = 1,
                DMSPODate = DateTime.Now,
                VendorDescription = "Vendor",
                Vendor = "Vendor",
                PurchaseOrderNo = "PurchaseOrderNo",
                PurchaseReceiptID = 1,
                PurchaseReceiptNo = "PurchaseReceiptNo",
                PurchaseReceiptDetailNo = "PurchaseReceiptDetailNo",
                PurchaseReceiptDetailID = 1,
                ChassisModel = "ChassisModel",
                ChassisNumberRegister = "ChassisNumberRegister"
            };
            return obj;
        }
    }
}