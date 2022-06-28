#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIReadExample class
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
    public class APIRead_CRM_SVC_DailyReportExample : IExamplesProvider
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
                        PropertyName = "FromDateIn",
                        PropertyValue = "2020-01-01 00:00:00",
                        SqlOperation = SQLOperation.And
                     },
                    new MatchTypeFilter{
                        MatchType = MatchType.Exact,
                        PropertyName = "ToDateIn",
                        PropertyValue = "2020-01-31 00:00:00",
                        SqlOperation = SQLOperation.And
                     }
                }
            };

            return obj;
        }
    }
}