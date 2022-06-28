#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : ServiceMMS_IF Domain class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021-10-26
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class ServiceMMS_IF
    {
        public int ID { get; set; }
        public int? DealerID { get; set; }
        public int? DealerBranchID { get; set; }
        public string WONumber { get; set; }
        public DateTime? ServiceDate { get; set; }
        public int? ChassisMasterID { get; set; }
        public string PlateNo { get; set; }
        public DateTime? NextEstimatedServiceDate { get; set; }
        public string Notes { get; set; }
        public int? Status { get; set; }
        public int? RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime? LastUpdateTime { get; set; }
    }
}
