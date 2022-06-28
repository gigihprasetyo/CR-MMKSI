#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartBillingDto  class
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
    public class SparePartBillingDto : DtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public string BillingNumber { get; set; }
        public DateTime BillingDate { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal Tax { get; set; }

        //public List<SparePartBillingDetailDto> SparePartBillingDetails { get; set; }
    }
}

