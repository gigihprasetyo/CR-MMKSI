#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : DealerSuggestionService class
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Domain
{
    public class GetServiceType
    {
        public string Kategori { get; set; }
        public string ServiceTypeCode { get; set; }
        public string KindCode { get; set; }
        public string KindDescription { get; set; }
        public decimal MaxDuration { get; set; }
    }
}
