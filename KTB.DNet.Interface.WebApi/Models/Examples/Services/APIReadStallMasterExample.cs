#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIReadStallMasterExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Model;
using Swashbuckle.Examples;
using System.Collections.Generic;


namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    /// <summary>
    /// API Read Stall Master example
    /// </summary>
    public class APIReadStallMasterExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new FilterDtoBase
            {
                pages = 1,
                find = new List<MatchTypeFilter>
                {
                    new MatchTypeFilter
                    {
                        MatchType = MatchType.Exact,
                        PropertyName = "StallCode",
                        PropertyValue = "100007-S01",
                        SqlOperation = SQLOperation.And
                     }
                },
                sort = new List<SortFilter>
                {
                    new SortFilter
                    {
                        SortColumn = "DealerCode",
                        SortDirection = Sort.SortDirection.ASC
                    }
                }
            };

            return obj;
        }
    }
}