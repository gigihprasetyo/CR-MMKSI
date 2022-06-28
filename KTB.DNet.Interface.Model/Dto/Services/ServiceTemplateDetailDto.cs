#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceTemplateDetailDto  class
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
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ServiceTemplateDetailDto : DtoBase
    {
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

    }
}

