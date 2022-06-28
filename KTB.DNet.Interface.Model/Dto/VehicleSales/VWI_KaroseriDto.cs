#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_KaroseriDto  class
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
    public class VWI_KaroseriDto : ReadDtoBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public int Status { get; set; }
    }
}

