#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterProfileDto  class
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
    public class ChassisMasterProfileDto : DtoBase
    {
        public int ID { get; set; }

        public ProfileHeaderDto ProfileHeader { get; set; }

        public ProfileGroupDto ProfileGroup { get; set; }

        public ChassisMasterDto ChassisMaster { get; set; }

        public string ProfileValue { get; set; }

        //public List<ChassisMasterProfileHistoryDto> ChassisMasterProfileHistorys { get; set; }
    }
}
