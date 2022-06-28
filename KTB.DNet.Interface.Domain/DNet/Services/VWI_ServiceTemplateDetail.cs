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
    public class VWI_ServiceTemplateDetail
    {
        public string IDRow { get; set; }
        public string ServiceTemplateHeaderID { get; set; }
        public string VechileTypeID { get; set; }
        public string KindID { get; set; }
        public string ServiceTemplate { get; set; }
        public string ServiceTemplateDetail { get; set; }
        public string ProductType { get; set; }
        public string Product { get; set; }
        public string ProductDescription { get; set; }
        public string PartCode { get; set; }
        public string PartCodeDescription { get; set; }
        public Decimal Quantity { get; set; }
        public string UnitPrice { get; set; }
        public Decimal TotalPrice { get; set; }
        public string ID { get; set; }
    }
}
