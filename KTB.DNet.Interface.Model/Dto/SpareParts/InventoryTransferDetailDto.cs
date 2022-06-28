#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : InventoryTransferDetailDto  class
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
    public class InventoryTransferDetailDto : DtoBase
    {
        public int ID { get; set; }
        public int InventoryTransferID { get; set; }
        public string Owner { get; set; }
        public Double BaseQuantity { get; set; }
        public string ConsumptionTaxIn { get; set; }
        public string ConsumptionTaxOut { get; set; }
        public string FromBatchNo { get; set; }
        public string FromDealer { get; set; }
        public string FromConfiguration { get; set; }
        public string FromExteriorColor { get; set; }
        public string FromInteriorColor { get; set; }
        public string FromLocation { get; set; }
        public string FromSerialNo { get; set; }
        public string FromSite { get; set; }
        public string FromStyle { get; set; }
        public string FromWarehouse { get; set; }
        public string InventoryTransferNo { get; set; }
        public string InventoryUnit { get; set; }
        public string ProductDescription { get; set; }
        public string Product { get; set; }
        public Double Quantity { get; set; }
        public string Remarks { get; set; }
        public string ServicePartsandMaterial { get; set; }
        public string SourceData { get; set; }
        public string StockNumber { get; set; }
        public string StockNumberNV { get; set; }
        public string StockNumberLookupName { get; set; }
        public int StockNumberLookupType { get; set; }
        public string ToBatchNo { get; set; }
        public string ToDealer { get; set; }
        public string ToConfiguration { get; set; }
        public string ToExteriorColor { get; set; }
        public string ToInteriorColor { get; set; }
        public string ToLocation { get; set; }
        public string ToSerialNo { get; set; }
        public string ToSite { get; set; }
        public string ToStyle { get; set; }
        public string ToWarehouse { get; set; }
        public string TransactionUnit { get; set; }
        public string VIN { get; set; }
    }
}

