#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPOEstimateDetailDto  class
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
    public class SparePartPOEstimateDetailDto : DtoBase
    {
        //public int ID { get; set; }
        //public int SparePartPOEstimateID { get; set; }
        public int DealerCode { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string AltPartNumber { get; set; }
        public string UOM { get; set; }
        public int AllocQty { get; set; }
        public int OrderQty { get; set; }
        //public int AllocationQty { get; set; }
        public Decimal RetailPrice { get; set; }
        //public int OpenQty { get; set; }
        public Decimal Discount { get; set; }
        //public string ItemStatus { get; set; }
        public decimal Tax { get; set; }
    }
}

