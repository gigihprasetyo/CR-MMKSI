#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPRDetailFromVendorDto  class
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
    public class SparePartPRDetailFromVendorDto : DtoBase
    {
        public int ID { get; set; }
        public string PRDetailNumber { get; set; }
        public int SparePartPRID { get; set; }
        public string PRNumber { get; set; }
        public string Owner { get; set; }
        public Double BaseReceivedQuantity { get; set; }
        public string BatchNumber { get; set; }
        public string DealerCode { get; set; }
        public string ChassisModel { get; set; }
        public string ChassisNumberRegister { get; set; }
        public Decimal ConsumptionTax1Amount { get; set; }
        public string ConsumptionTax1 { get; set; }
        public Decimal ConsumptionTax2Amount { get; set; }
        public string ConsumptionTax2 { get; set; }
        public Decimal DiscountAmount { get; set; }
        public string EngineNumber { get; set; }
        public string EventData { get; set; }
        public string InventoryUnit { get; set; }
        public string KeyNumber { get; set; }
        public Decimal LandedCost { get; set; }
        public string Location { get; set; }
        public string ProductDescription { get; set; }
        public string Product { get; set; }
        public Double ProductVolume { get; set; }
        public Double ProductWeight { get; set; }
        public string PurchaseUnit { get; set; }
        public Double ReceivedQuantity { get; set; }
        public string ReferenceNumber { get; set; }
        public string ReturnPRDetail { get; set; }
        public string ServicePartsAndMaterial { get; set; }
        public string Site { get; set; }
        public string StockNumber { get; set; }
        public Decimal TitleRegistrationFee { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal TotalBaseAmount { get; set; }
        public Decimal TotalConsumptionTaxAmount { get; set; }
        public Double TotalVolume { get; set; }
        public Double TotalWeight { get; set; }
        public Decimal TransactionAmount { get; set; }
        public Decimal UnitCost { get; set; }
        public string Warehouse { get; set; }
    }
}