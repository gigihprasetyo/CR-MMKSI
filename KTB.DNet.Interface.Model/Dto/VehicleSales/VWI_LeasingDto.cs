#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_LeasingDto  class
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
    public class VWI_LeasingDto : ReadDtoBase
    {
        public string LeasingCode { get; set; }
        public string LeasingName { get; set; }
        public int Status { get; set; }
    }
}

