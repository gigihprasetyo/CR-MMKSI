#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistSalesChannelDto  class
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
    public class AssistSalesChannelDto : DtoBase
    {
        public int ID { get; set; }
        public string SalesChannelType { get; set; }
        public string SalesChannelCode { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}

