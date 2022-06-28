#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateDMSWOWarrantyExample class
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
using System;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateDMSWOWarrantyExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                DealerCode = "100675",
                DealerBranchCode = "100675",
                ChassisNumber = "MK2L0PU39HK013747",
                WorkOrderNumber = "RESRVNUM001",
                FailureDate = DateTime.Now,
                ServiceDate = DateTime.Now,
                Owner = "Satria",
                MileAge = 100,
                ServiceBuletin = "",
                Symptoms = "",
                Causes = "",
                Results = "",
                Notes = "",
                isBB = true
            };

            return obj;
        }
    }
}