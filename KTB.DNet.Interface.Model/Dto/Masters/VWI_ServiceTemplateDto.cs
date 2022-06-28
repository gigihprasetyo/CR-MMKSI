#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceTemplateDto  class
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
    public class VWI_ServiceTemplateDto : DtoBase
    {
        //public int ID { get; set; }
        public string SVCTMPTParentGroup { get; set; }
        public string SVCTMPTParent { get; set; }
        public string SVCTMPTSubGroup { get; set; }
        public string SvcTemplateCode { get; set; }
        public string Description { get; set; }
        public string DNETKind { get; set; }
        public int IntervalKM { get; set; }
        public string ServiceTemplateActivityDesc { get; set; }
        public Double Duration { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public Double Qty { get; set; }
        public Decimal Price { get; set; }
    }
}

