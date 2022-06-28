#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ProfileHeaderDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System.Collections.Generic;

namespace KTB.DNet.Interface.Model
{
    public class ProfileHeaderDto
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public short DataType { get; set; }

        public int DataLength { get; set; }

        public short ControlType { get; set; }

        public short SelectionMode { get; set; }

        public short Mandatory { get; set; }

        public short Status { get; set; }

        public List<ProfileHeaderToGroupDto> ProfileHeaderToGroups { get; set; }

        public List<ProfileDetailDto> ProfileDetails { get; set; }

        public List<ChassisMasterProfileDto> ChassisMasterProfiles { get; set; }

        public List<CustomerProfileDto> CustomerProfiles { get; set; }
    }
}
