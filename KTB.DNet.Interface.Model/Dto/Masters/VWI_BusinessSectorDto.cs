#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_BusinessSectorDto  class
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
    public class VWI_BusinessSectorDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string BusinessSectorName { get; set; }
        public string BusinessDomain { get; set; }
        public string BusinessName { get; set; }
        public short Status { get; set; }
    }
}

