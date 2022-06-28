#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BusinessSectorDetailDto  class
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
    public class BusinessSectorDetailDto : DtoBase
    {
        public int ID { get; set; }
        public int BusinessSectorHeaderID { get; set; }
        public string BusinessDomain { get; set; }
    }
}

