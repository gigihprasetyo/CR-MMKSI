#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : InventoryTransferDto  class
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
    public class InventoryTransferDto : DtoBase
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public string FromDealer { get; set; }
        public string FromSite { get; set; }
        public string InventoryTransferNo { get; set; }
        public int ItemTypeForTransfer { get; set; }
        public string PersonInCharge { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string ReceiptNo { get; set; }
        public string ReferenceNo { get; set; }
        public string SearchVehicle { get; set; }
        public string SourceData { get; set; }
        public int State { get; set; }
        public string ToDealer { get; set; }
        public string ToSite { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionType { get; set; }
        public int TransferStatus { get; set; }
        public Boolean TransferStep { get; set; }
        public string WONo { get; set; }
    }
}

