#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FieldFixListDto  class
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
    public class FieldFixListDto
    {
        public int ID { get; set; }
        public string ChassisNo { get; set; }
        public string RecallRegNo { get; set; }
        public string Description { get; set; }
        public string BuletinDescription { get; set; }
        public DateTime ValidStartDate { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}

