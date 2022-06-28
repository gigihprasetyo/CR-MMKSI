#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPRFromVendorDto  class
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
    public class SparePartPRFromVendorDto : DtoBase
    {
        public int ID { get; set; }
        public string PRNumber { get; set; }
        public string PONumber { get; set; }
        public string Owner { get; set; }
        public string APVoucherNumber { get; set; }
        public Boolean AssignLandedCost { get; set; }
        public Boolean AutoInvoiced { get; set; }
        public string DealerCode { get; set; }
        public DateTime DeliveryOrderDate { get; set; }
        public string DeliveryOrderNumber { get; set; }
        public string EventData { get; set; }
        public string EventData2 { get; set; }
        public Decimal GrandTotal { get; set; }
        public int Handling { get; set; }
        public Boolean LoadData { get; set; }
        public DateTime PackingSlipDate { get; set; }
        public string PackingSlipNumber { get; set; }
        public Boolean PRReferenceRequired { get; set; }
        public string ReturnPRNumber { get; set; }
        public int State { get; set; }
        public Decimal TotalBaseAmount { get; set; }
        public Decimal TotalConsumptionTax1Amount { get; set; }
        public Decimal TotalConsumptionTax2Amount { get; set; }
        public Decimal TotalConsumptionTaxAmount { get; set; }
        public Decimal TotalTitleRegistrationFree { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransferOrderRequestingNumber { get; set; }
        public int Type { get; set; }
        public string VendorDescription { get; set; }
        public string Vendor { get; set; }
        public string VendorInvoiceNumber { get; set; }
        public string WONumber { get; set; }
    }
}