#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_DepositC2Dto  class
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
    public class VWI_DepositC2Dto : ReadDtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string Period { get; set; }
        public List<VWI_DepositC2LineDto> DepositC2Line { get; set; }
    }
    public class VWI_DepositC2LineDto : ReadDtoBase
    {
        public int ID { get; set; }
        public int DepositC2ID { get; set; }
        public string DocumentNo { get; set; }
        public string BillingNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public Decimal DepositC2Amnt { get; set; }
    }
}
