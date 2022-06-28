#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_CustomerVehicleDto  class
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
    public class VWI_CustomerVehicleDto
    {
        public int ID { get; set; }
        public string CustomerCode { get; set; }
        public string DealerCode { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
