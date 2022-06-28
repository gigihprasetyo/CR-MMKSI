#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ProfileHeaderToGroupDto  class
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
    public class ProfileHeaderToGroupDto : DtoBase
    {
        public int ID { get; set; }

        public int Priority { get; set; }

        public string ProfileValue { get; set; }

        public int GroupId { get; set; }

        public int ProfileYbsId { get; set; }

        public int ProfileHeaderId { get; set; }

        public ProfileHeaderDto ProfileHeader { get; set; }

        public ProfileGroupDto ProfileGroup { get; set; }
    }
}
