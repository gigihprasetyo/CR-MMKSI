
#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_ServiceStandardTime class
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
    public class VWI_ServiceStandardTime
    {
        public string IDRow { get; set; }
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

