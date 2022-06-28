#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AppConfigFilterDto  class
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
    public class AppConfigFilterDto : FilterDtoBase
    {
        public string AppID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public short Status { get; set; }
        public string Value { get; set; }
    }
}
