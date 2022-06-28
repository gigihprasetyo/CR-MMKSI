#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MCPDetailDto  class
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
    public class MCPDetailDto : DtoBase
    {
        public int ID { get; set; }

        public int Unit { get; set; }

        public int UnitRemain { get; set; }

        public VehicleTypeDto VehicleType { get; set; }

        public MCPHeaderDto MCPHeader { get; set; }
    }
}
