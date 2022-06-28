#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetailDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion


namespace KTB.DNet.Interface.Model
{
    public class ValidateChassisDto:DtoBase
    {
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string VehicleTypeCode { get; set; }
        public string VehicleColorCode { get; set; } 
    }
}
