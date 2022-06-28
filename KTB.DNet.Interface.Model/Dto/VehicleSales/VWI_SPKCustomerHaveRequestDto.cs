#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SPKCustomerHaveRequestDto  class
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
    public class VWI_SPKCustomerHaveRequestDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string DealerBranchCode { get; set; }
        public string SPKNumber { get; set; }
        public string RequestNo { get; set; }
        public string CustomerCode { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
