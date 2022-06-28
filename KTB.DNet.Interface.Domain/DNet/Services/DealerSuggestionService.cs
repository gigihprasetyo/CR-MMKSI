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
    public class DealerSuggestionService
    {
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public int Priority { get; set; }
        public string Distance { get; set; }
        public string Category { get; set; }
        public string ListJadwal { get; set; }
    }
}
