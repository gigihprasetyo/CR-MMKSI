#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceHistoryDetailDto  class
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
using System.Runtime.Serialization;

namespace KTB.DNet.Interface.Model
{
    public class VWI_ServiceHistoryDetailDto : DtoBase
    {
        public string NoWorkOrder { get; set; }
        public string PartNumber { get; set; }
        [IgnoreDataMember]
        public int SparePartMasterID { get; set; }
        public string PartName { get; set; }
        public Double Qty { get; set; }

    }
}
