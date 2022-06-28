#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreatePDIExample class
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
    public class APIServiceBookingEstimationCostExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                DealerCode = "100808",
                ChassisNumber = "MK2NCWPARHJ001626",
                VechileModel = "XPANDER",
                VariantType = "Exceed",
                Parameters = new List<object>
                {
                    new 
                    {
                        ServiceType = "FS",
                        KindCode = "3"
                    }
                }
            };

            return obj;
        }
    }
}