#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ARReceiptDto  class
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
    public class ARReceiptDto : DtoBase
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public string GeneratedToken { get; set; }
        public string ARInvoiceReferenceNo { get; set; }
        public string ARReceiptNo { get; set; }
        public string ARReceiptReferenceNo { get; set; }
        public int Type { get; set; }
        public Boolean BookingFee { get; set; }
        public string BU { get; set; }
        public Boolean Cancelled { get; set; }
        public string CashAndBank { get; set; }
        public string Customer { get; set; }
        public string CustomerNo { get; set; }
        public DateTime EndOrderDate { get; set; }
        public string MethodOfPayment { get; set; }
        public Decimal AvailableBalance { get; set; }
        public DateTime StartOrderDate { get; set; }
        public int State { get; set; }
        public Decimal AppliedToDocument { get; set; }
        public Decimal TotalAmountBase { get; set; }
        public Decimal TotalChangeAmount { get; set; }
        public Decimal TotalOutstandingBalanceBase { get; set; }
        public Decimal TotalReceiptAmount { get; set; }
        public Decimal TotalRemainingBalanceBase { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

