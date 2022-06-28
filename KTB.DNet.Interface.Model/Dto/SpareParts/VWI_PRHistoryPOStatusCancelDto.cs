#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PRHistoryPOStatusCancelDto  class
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
    public class VWI_PRHistoryPOStatusCancelDto
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string PONumber { get; set; }
        public string OrderType { get; set; }
        public string DMSPRNO { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}