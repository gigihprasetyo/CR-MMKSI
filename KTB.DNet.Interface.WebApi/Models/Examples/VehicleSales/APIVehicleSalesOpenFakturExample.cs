#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIVehicleSalesOpenFakturExample class
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

namespace KTB.DNet.Interface.WebApi.Models
{
    /// <summary>
    /// Open Faktur API examples request body
    /// </summary>
    public class APIVehicleSalesOpenFakturExample : IExamplesProvider
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
                        PropertyName = "DealerCode",
                        PropertyValue = "100001",
                        SqlOperation = SQLOperation.And
                     }
                },
                sort = new List<SortFilter>
                {
                    new SortFilter
                    {
                        SortColumn = "ChassisNumber",
                        SortDirection = Sort.SortDirection.ASC
                    }
                }
            };

            return obj;
        }

    }
}