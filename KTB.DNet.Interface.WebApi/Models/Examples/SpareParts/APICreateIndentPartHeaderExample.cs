#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateIndentPartHeaderExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;
using System.Collections.Generic;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateIndentPartHeaderExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                MaterialType = 3,
                DescID = 1,
                ChassisNumber = "JMFLYV97W8J000873",
                DMSPRNo = "tes01",
                DealerCode = "0",
                IndentPartDetail = new List<object>{
                    new {
                		UpdatedBy = "DealerUser",
                        TotalForecast=1,
                        Qty =10,
                        Description = "test detail1 description",
                        AllocationQty = 2,
                        IsCompletedAllocation = 1,
                        PartNumber = "SPA07002B"
                    }, 
                    new {
                		UpdatedBy = "DealerUser",
                        TotalForecast=1,
                        Qty =10,
                        Description = "test detail2 description",
                        AllocationQty = 2,
                        IsCompletedAllocation = 1,
                        PartNumber = "SPA07002F"
                    }
                }
            };

            return obj;
        }
    }
}