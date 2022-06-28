#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : InventoryTransactionDetailDto  class
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
    public class InventoryTransactionDetailDto : DtoBase
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public Double BaseQuantity { get; set; }
        public string BatchNo { get; set; }
        public string BU { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public string FromBU { get; set; }
        public int InventoryTransactionID { get; set; }
        public string InventoryTransactionNo { get; set; }
        public string InventoryTransferDetail { get; set; }
        public string InventoryUnit { get; set; }
        public string Location { get; set; }
        public string ProductCrossReference { get; set; }
        public string ProductDescription { get; set; }
        public string Product { get; set; }
        public Double Quantity { get; set; }
        public string ReasonCode { get; set; }
        public string ReferenceNo { get; set; }
        public string RegisterSerialNumber { get; set; }
        public int RunningNumber { get; set; }
        public string SerialNo { get; set; }
        public string ServicePartsAndMaterial { get; set; }
        public string Site { get; set; }
        public string SourceData { get; set; }
        public string StockNumber { get; set; }
        public string StockNumberNV { get; set; }
        public Decimal TotalCost { get; set; }
        public string TransactionType { get; set; }
        public string TransactionUnit { get; set; }
        public Decimal UnitCost { get; set; }
        public string VIN { get; set; }
        public string Warehouse { get; set; }
    }
}

