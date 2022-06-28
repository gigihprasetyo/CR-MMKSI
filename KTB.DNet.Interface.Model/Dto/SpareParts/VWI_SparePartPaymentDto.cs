#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SparePartPaymentDto  class
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
    public class VWI_SparePartPaymentDto : ReadDtoBase
    {
        public string ReferenceNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime PostingDate { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string SONumber { get; set; }
        public string DMSPRNo { get; set; }
        public string OrderType { get; set; }
        public Decimal Amount { get; set; }
        public Decimal BillingAmount { get; set; }
        public short IsTOP { get; set; }
        public short IsPenalty { get; set; }
    }
}

