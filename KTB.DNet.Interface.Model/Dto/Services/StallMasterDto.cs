#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Stall Master  class
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
    public class StallMasterDto : DtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public string StallCode { get; set; }
        public string StallCodeDealer { get; set; }
        public string StallName { get; set; }
        public string StallLocation { get; set; }
        public string StallType { get; set; }
        public string StallCategory { get; set; }
        public string IsBodyPaint { get; set; }
        public string Status { get; set; }
        public string LastUpdatedTime { get; set; }
    }
}
