#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_inventorytransfer class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 27 Aug 2020 12:00:07
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
    public class APIReadCRM_xts_inventorytransferExample : IExamplesProvider
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
                        PropertyName = "xts_inventorytransferid",
                        PropertyValue = "00000000-0000-0000-0000-000000000000",
                        SqlOperation = SQLOperation.And
                     }
                },
                sort = new List<SortFilter>
                {
                    new SortFilter
                    {
                        SortColumn = "xts_inventorytransferid",
                        SortDirection = Sort.SortDirection.ASC
                    }
                }
            };

            return obj;
        }
    }
}