#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKChassisDto  class
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
    public class SPKChassisDto : DtoBase
    {
        public int ID { get; set; }
        public int SPKDetailID { get; set; }
        public int ChassisMasterID { get; set; }
        public int MatchingType { get; set; }
        public DateTime MatchingDate { get; set; }
        public string MatchingNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public string KeyNumber { get; set; }
        public string DealerCode { get; set; }
        public string SPKNumber { get; set; }
        public string ChassisNumber { get; set; }
    }
}

