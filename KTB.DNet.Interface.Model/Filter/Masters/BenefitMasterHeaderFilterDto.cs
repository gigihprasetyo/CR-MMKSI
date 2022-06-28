#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitMasterHeaderFilterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System.Collections.Generic;

namespace KTB.DNet.Interface.Model
{
    public class BenefitMasterHeaderFilterDto : FilterDtoBase
    {
        public List<MatchTypeFilter> MatchTypeFilters { get; set; }

        public List<SortFilter> SortFilters { get; set; }
    }
}