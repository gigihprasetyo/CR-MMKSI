#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : Service Template Header class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:16:28
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_ServiceTemplateHeader
    {
        public string IDRow { get; set; }
        public string ServiceTemplate { get; set; }
        public string ServiceTemplateGroup { get; set; }
        public string ServiceTemplateSubGroup { get; set; }
        public string Description { get; set; }
        public string ServiceCategory { get; set; }
        public int IntervalTime { get; set; }
        public string ServiceTemplateVehiclePricePattern { get; set; }
        public string CalculationMethod { get; set; }
        public string KindCode { get; set; }
        public string KindID { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public string VechileTypeID { get; set; }
        public string ID { get; set; }
        public List<VWI_ServiceTemplateDetail> servicetemplatedetails { get; set; }
    }
}
