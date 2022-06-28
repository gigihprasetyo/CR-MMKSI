
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceStandardTimeDto class
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

namespace KTB.DNet.Interface.Model
{
    public class ServiceStandardTimeDto : DtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string KindCode { get; set; }
        public string ServiceTemplateGroup { get; set; }
        public string ServiceCategory { get; set; }
        public int VechileTypeID { get; set; }
        public string VechileTypeCode { get; set; }
        public decimal SystemStandardTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }

    }
}

