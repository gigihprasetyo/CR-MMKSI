#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_OpenFakturDto  class
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
    public class VWI_OpenFakturDto
    {
        public string DealerCode { get; set; }
        public string ChassisNumber { get; set; }
        public string SPKNumber { get; set; }
        public DateTime OpenFakturDate { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

