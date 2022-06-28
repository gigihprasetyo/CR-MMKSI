#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateMSPClaimExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 9/11/2018 10:23
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;
using System;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateMSPClaimExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ChassisNumber = "MK2L0PU39HK013747",
                EngineNumber = "4D34-432469",
                DealerCode = "100109",
                DealerBranchCode = "",
                StandKM = 3000,
                ServiceDate = DateTime.Now,
                VisitType = "WI"
            };

            return obj;
        }
    }
}