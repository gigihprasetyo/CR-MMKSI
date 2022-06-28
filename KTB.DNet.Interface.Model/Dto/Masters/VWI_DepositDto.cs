#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_DepositDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 14 Sep 2021
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Model
{
    public class VWI_DepositDto : ReadDtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string Period { get; set; }
        public Decimal BegBalance { get; set; }
        public Decimal EndBalance { get; set; }
        public Decimal TotalDebit { get; set; }
        public Decimal TotalCredit { get; set; }
        public Decimal AvailableDeposit { get; set; }
        public Decimal GiroReceive { get; set; }
        public Decimal RO { get; set; }
        public Decimal Service { get; set; }
        public Decimal InClearing { get; set; }
        public List<VWI_DepositLineDto> DepositLine { get; set; }
    }

    public class VWI_DepositLineDto : ReadDtoBase
    {
        public int ID { get; set; }
        public int DepositID { get; set; }
        public string DocumentNo { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime ClearingDate { get; set; }
        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }
        public string ReferenceNo { get; set; }
        public string InvoiceNo { get; set; }
        public string Remark { get; set; }
        public int PaymentType { get; set; }
    }
}
