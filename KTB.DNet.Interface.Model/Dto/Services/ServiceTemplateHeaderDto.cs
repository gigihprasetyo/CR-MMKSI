#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceTemplateHeaderDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ServiceTemplateHeaderDto : DtoBase
    {
        public string ServiceTemplate { get; set; }
        public string ServiceTemplateGroup { get; set; }
        public string ServiceTemplateSubGroup { get; set; }
        public string Description { get; set; }
        public string ServiceCategory { get; set; }
        public int IntervalTime { get; set; }
        public string ServiceTemplateVehiclePricePattern { get; set; }
        public string CalculationMethod { get; set; }
        public string KindCode { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public string VechileTypeID { get; set; }
        public List<ServiceTemplateDetailDto> servicetemplatedetails { get; set; }

    }
}

