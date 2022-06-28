#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleKindGroupDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System.Collections;

namespace KTB.DNet.Interface.Model
{
    public class VehicleKindGroupDto : DtoBase
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public ArrayList CompetitorTypes { get; set; }

        public ArrayList VehicleTypes { get; set; }
    }
}
