#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_assigntosalesperson class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 11:46:13
 ===========================================================================
*/
#endregion


#region Namespace Imports
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Model;
using Swashbuckle.Examples;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIReadCRM_xts_assigntosalespersonExample : IExamplesProvider
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
                        PropertyName = "xts_assigntosalespersonid",
                        PropertyValue = "00000000-0000-0000-0000-000000000000",
                        SqlOperation = SQLOperation.And
                     }
                },
                sort = new List<SortFilter>
                {
                    new SortFilter
                    {
                        SortColumn = "xts_assigntosalespersonid",
                        SortDirection = Sort.SortDirection.ASC
                    }
                }
            };

            return obj;
        }
    }
}