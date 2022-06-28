#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PurchaseReturnDto  class
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
    public class VWI_PurchaseReturnDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string ClaimNo { get; set; }
        public DateTime ClaimDate { get; set; }
        public string DONumberRef { get; set; }
        public string BillingNumber { get; set; }
        public string SONumber { get; set; }
        public string DONumber { get; set; }
        public string FakturRetur { get; set; }
        public string SORetur { get; set; }
        public DateTime SOReturDate { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
        public Decimal RetailPrice { get; set; }
        public Decimal TotalPrice { get; set; }
        public new DateTime LastUpdateTime { get; set; }
    }
}

