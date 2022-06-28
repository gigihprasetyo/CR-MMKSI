#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : InventoryTransactionDto  class
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
    public class InventoryTransactionDto : DtoBase
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public string DealerCode { get; set; }
        public string InventoryTransactionNo { get; set; }
        public string InventoryTransferNo { get; set; }
        public string PersonInCharge { get; set; }
        public string ProcessCode { get; set; }
        public string SourceData { get; set; }
        public int State { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionType { get; set; }
        public string WONo { get; set; }
    }
}

