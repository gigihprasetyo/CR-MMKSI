#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceTemplateActivityDto  class
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
    public class VWI_ServiceTemplateActivityDto : DtoBase
    {
        public Int32 ID { get; set; }
        public string ServiceType { get; set; }
        public Int32 ServiceTemplateHeaderID { get; set; }
        public Int32 KindID { get; set; }
        public Int32 VechileTypeID { get; set; }
        public DateTime ValidFrom { get; set; }
        public string KindCode { get; set; }
        public string KindDescription { get; set; }
        public string VehicleTypeCode { get; set; }
        public string ActName { get; set; }
        public string ActSequence { get; set; }
        public Decimal Duration { get; set; }
        public string SVCTemplateActivity { get; set; }

    }
}


