#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : SparePartOutstandingOrderDetail class
 SPECIAL NOTES : Generated from database BSIDNET_MMKSI_CR_Sparepart_BO
 GENERATED BY  : Ako
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 13 Jan 2021 10:55:39
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
    public class APIReadSparePartOutstandingOrderDetailExample : IExamplesProvider
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
                        PropertyName = "ID",
                        PropertyValue = "1",
                        SqlOperation = SQLOperation.And
                     }
                },
                sort = new List<SortFilter>
                {
                    new SortFilter
                    {
                        SortColumn = "ID",
                        SortDirection = Sort.SortDirection.ASC
                    }
                }
            };

            return obj;
        }
    }
}