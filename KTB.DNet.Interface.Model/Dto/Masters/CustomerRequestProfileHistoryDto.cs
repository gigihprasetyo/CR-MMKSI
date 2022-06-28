#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerRequestProfileHistoryDto  class
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
    public class CustomerRequestProfileHistoryDto : DtoBase
    {
        public int ID { get; set; }
        public int CustomerRequestProfileID { get; set; }
        public string ProvileValue { get; set; }
    }
}
