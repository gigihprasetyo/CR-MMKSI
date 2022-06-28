#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ProfileGroupDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ProfileGroupDto : DtoBase
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public List<ProfileHeaderToGroupDto> ProfileHeaderToGroups { get; set; }
    }
}
