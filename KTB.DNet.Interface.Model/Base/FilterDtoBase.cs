#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FilterDtoBase  class
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
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class FilterDtoBase
    {
        public int pages { get; set; }

        public List<MatchTypeFilter> find { get; set; }

        public List<SortFilter> sort { get; set; }
    }
}
