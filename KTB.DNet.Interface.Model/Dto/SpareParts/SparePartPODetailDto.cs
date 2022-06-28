#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPODetailDto  class
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
    public class SparePartPODetailDto : DtoBase
    {
        public int ID { get; set; }
        public int SparePartPOID { get; set; }
        public int SparePartMasterID { get; set; }
        public string CheckListStatus { get; set; }
        public int Quantity { get; set; }
        public Decimal RetailPrice { get; set; }
        public string EstimateStatus { get; set; }
        public int StopMark { get; set; }
        public int TotalForecast { get; set; }
    }

    public class SparePartPOOtherDetailDto
    {
        public int Quantity { get; set; }
        public string UoM { get; set; }
        public string PartNumber { get; set; }
        public string DealerCode { get; set; }
        public DateTime PODate { get; set; }
        public decimal RetailPrice { get; set; }
        public int TotalForecast { get; set; }
    }
}

