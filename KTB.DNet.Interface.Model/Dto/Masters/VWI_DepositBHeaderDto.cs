#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_DepositBHeaderDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 13 Sep 2021
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Model
{
    public class VWI_DepositBHeaderDto : ReadDtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public int ProductCategoryID { get; set; }
        public DateTime TransactionDate { get; set; }
        public Decimal BeginingBalance { get; set; }
        public Decimal EndBalance { get; set; }
        public Decimal DebetAmount { get; set; }
        public Decimal CreditAmount { get; set; }
        public List<VWI_DepositBDetailDto> DepositBDetail { get; set; }
    }

    public class VWI_DepositBDetailDto : ReadDtoBase
    {
        public int ID { get; set; }
        public int DepositBID { get; set; }
        public string Tipe { get; set; }
        public int StatusDebet { get; set; }
        public Decimal Amount { get; set; }
        public string Description { get; set; }
        public string Reff { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
