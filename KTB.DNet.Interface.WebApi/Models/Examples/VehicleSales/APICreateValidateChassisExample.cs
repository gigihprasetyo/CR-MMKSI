#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateValidateChassisExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 30/10/2018 7:31
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;
using System.Collections.Generic;

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APICreateValidateChassisExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ChassisNumber = "MK2L0PU39HK013747",
                EngineNumber = "345rtfg",
                VehicleTypeCode = "DK00",
                VehicleColorCode = "OPTR",
                SPKDetailID = 1
            };
            return obj;
        }
    }
}