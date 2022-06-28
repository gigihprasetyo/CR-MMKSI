#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_WholeSalesPriceDto  class
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
    public class VWI_WholeSalesPriceDto
    {
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public int VehicleColorID { get; set; }
        public string MaterialNumber { get; set; }
        public string MaterialDescription { get; set; }
        public string VehicleColorCode { get; set; }
        public string VehicleColorName { get; set; }
        public string VehicleTypeCode { get; set; }
        public string VehicleTypeDesc { get; set; }
        public DateTime ValidFrom { get; set; }
        public Decimal BasePrice { get; set; }
        public Decimal OptionPrice { get; set; }
        public Decimal PPN_BM { get; set; }
        public Decimal PPN { get; set; }
        public Decimal PPh22 { get; set; }
        public Decimal PPh23 { get; set; }
        public Decimal FactoringInt { get; set; }
        public Decimal DiscountReward { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}

