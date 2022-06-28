#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_FieldFixServicedDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 7/11/2018 10:44
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class VWI_FieldFixServicedDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime ServiceDate { get; set; }
        public string DealerCode { get; set; }
        public string DealerBranchCode { get; set; }
        public string WorkOrderNumber { get; set; }
        public string RecallRegNo { get; set; }
        public string Description { get; set; }
        public int SystemID { get; set; }

    }
}

