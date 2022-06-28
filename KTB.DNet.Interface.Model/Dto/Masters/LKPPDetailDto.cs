#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : LKPPDetailDto  class
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
    public class LKPPDetailDto : DtoBase
    {
        public int ID { get; set; }

        public string VehicleTypeCode { get; set; }

        public int Unit { get; set; }

        public int UnitRemain { get; set; }

        //public LKPPHeaderDto LKPPHeader { get; set; }

        //public VehicleTypeDto VehicleType { get; set; }
    }
}
