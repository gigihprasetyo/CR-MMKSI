#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ProfileDetailFromHeaderCodeDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Model
{
    public class VWI_ProfileDetailFromHeaderCodeDto
    {
        public int ID { get; set; }
        public int ProfileHeaderID { get; set; }
        public string ProfileHeaderCode { get; set; }
        public string ProfileHeaderDesc { get; set; }
        public string ProfileDetailCode { get; set; }
        public string ProfileDetailDesc { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}


