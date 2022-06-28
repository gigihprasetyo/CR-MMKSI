#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKHeaderStatusDto  class
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
    public class SPKHeaderStatusDto
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string DealerCode { get; set; }
        public string SPKNumber { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
