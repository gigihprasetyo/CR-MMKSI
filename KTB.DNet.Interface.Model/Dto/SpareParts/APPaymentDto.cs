#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APPaymentDto  class
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
    public class APPaymentDto : DtoBase
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public string APPaymentNo { get; set; }
        public string APReferenceNo { get; set; }
        public string APVoucherReferenceNo { get; set; }
        public Decimal AppliedToDocument { get; set; }
        public string BU { get; set; }
        public Boolean Cancelled { get; set; }
        public string CashAndBank { get; set; }
        public string MethodOfPayment { get; set; }
        public Decimal AvailableBalance { get; set; }
        public int State { get; set; }
        public Decimal TotalChangeAmount { get; set; }
        public Decimal TotalPaymentAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Type { get; set; }
        public string VendorDescription { get; set; }
        public string Vendor { get; set; }
    }
}

