#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerProfileHistoryDto  class
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
    public class CustomerProfileHistoryDto : DtoBase
    {
        public int ID { get; set; }

        public CustomerProfileDto CustomerProfile { get; set; }

        public string ProfileValue { get; set; }
    }
}
