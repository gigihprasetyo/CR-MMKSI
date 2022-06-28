#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PositionWSCDto  class
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
    public class PositionWSCDto : DtoBase
    {
        public int ID { get; set; }

        public string PositionCategory { get; set; }

        public string PositionCode { get; set; }

        public string Description { get; set; }
    }
}

