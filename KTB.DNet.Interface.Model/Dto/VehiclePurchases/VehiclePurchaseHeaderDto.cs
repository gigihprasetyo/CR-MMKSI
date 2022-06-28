#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehiclePurchaseHeaderDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class VehiclePurchaseHeaderDto : DtoBase
    {
        public int? ID { get; set; }
        public string BUCode { get; set; }
        public string BUName { get; set; }
        public string DeliveryMethod { get; set; }
        public string Description { get; set; }
        public string PRPOTypeName { get; set; }
        public string DMSPONo { get; set; }
        public int DMSPOStatus { get; set; }
        public DateTime DMSPODate { get; set; }
        public string VendorDescription { get; set; }
        public string Vendor { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string PurchaseReceiptNo { get; set; }
        public string PurchaseReceiptDetailNo { get; set; }
        public string ChassisModel { get; set; }
        public string ChassisNumberRegister { get; set; }
    }
}

