#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_UnmatchSPKChassisDto  class
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
    public class VWI_UnmatchSPKChassisDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string RegNumber { get; set; }
        public DateTime RevisionDate { get; set; }
        public int RevisionStatusID { get; set; }
        public string RevisionStatus { get; set; }
        public string RevisionType { get; set; }
        public string ChassisNumber { get; set; }
        public int SPKHeaderID { get; set; }
        public string SPKNumber { get; set; }
        public string DealerCode { get; set; }
        public string DealerSPKNumber { get; set; }
    }
}

