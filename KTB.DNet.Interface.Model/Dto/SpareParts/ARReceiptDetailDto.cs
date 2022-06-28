#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ARReceiptDetailDto  class
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
    public class ARReceiptDetailDto : DtoBase
    {
        public int ID { get; set; }
        public int ARReceiptID { get; set; }
        public string Owner { get; set; }
        public string DetailNo { get; set; }
        public string ARReceiptNo { get; set; }
        public string BU { get; set; }
        public Decimal ChangeAmount { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public Double DifferenceValue { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNo { get; set; }
        public string OrderNoSO { get; set; }
        public string OrderNoUVSO { get; set; }
        public string OrderNoWO { get; set; }
        public Decimal OutstandingBalance { get; set; }
        public Boolean PaidBackToCustomer { get; set; }
        public Decimal ReceiptAmount { get; set; }
        public Decimal RemainingBalance { get; set; }
        public int SourceType { get; set; }
        public string TransactionDocument { get; set; }
    }
}

