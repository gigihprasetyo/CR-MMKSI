#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateIndentPartHeaderExample class
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
    public class APIUpdateIndentPartHeaderExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 20533,
                RequestNo = "IABA018030801",
                MaterialType = 3,
                Status = 1,
                StatusKTB = 2,
                SubmitFile = "",
                PaymentType = 0,
                Price = 0,
                DescID = 1,
                ChassisNumber = "JMFLYV97W8J000873",
                DMSPRNo = "tes01",
                DealerCode = "0",
                IndentPartDetail = new List<object>{
                    new {
                		UpdatedBy = "DealerUser",
                        ID=94305,
                        TotalForecast=1,
                        Qty =10,
                        Description = "test detail1 update description",
                        AllocationQty = 2,
                        IsCompletedAllocation = 1,
                        IndentPartHeaderID = 20533,
                        PartNumber = "SPA07002B"
                    }, 
                    new {
                		UpdatedBy = "DealerUser",
                        ID=94306,
                        TotalForecast=1,
                        Qty =10,
                        Description = "test detail2 upfate description",
                        AllocationQty = 2,
                        IsCompletedAllocation = 1,
                        IndentPartHeaderID = 20533,
                        PartNumber = "SPA07002F"
                    }
                }
            };

            return obj;
        }
    }
}