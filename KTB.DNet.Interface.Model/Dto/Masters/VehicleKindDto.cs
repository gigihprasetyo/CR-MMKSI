#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleKindDto  class
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
    public class VehicleKindDto : DtoBase
    {
        public int ID { get; set; }

        public int VehicleKindGroupID { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}
