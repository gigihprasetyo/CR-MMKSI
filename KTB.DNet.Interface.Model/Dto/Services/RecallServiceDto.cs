#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : RecallServiceDto  class
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
    public class RecallServiceDto : DtoBase
    {
        public int ID { get; set; }
        public int ChassisMasterID { get; set; }
        public int MileAge { get; set; }
        public DateTime ServiceDate { get; set; }
        public int ServiceDealerID { get; set; }
        public int DealerBranchID { get; set; }
        public int RecallChassisMasterID { get; set; }
        public string WorkOrderNumber { get; set; }
    }
}

