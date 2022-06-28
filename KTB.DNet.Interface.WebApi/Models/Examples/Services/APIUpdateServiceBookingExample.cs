
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : API APIUpdateServiceBookingExample class
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
    public class APIUpdateServiceBookingExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 1,
                DealerCode = "100170",
                ChassisNumber = "MK2L0PU39LJ011765",
                VechileTypeCode = "FN00",
                PlateNumber = "B7777UG",
                CustomerName = "Amanda Manopo",
                CustomerPhoneNumber = "234234",
                Odometer = "12000",
                StallMasterCode = "100170-S02",
                WorkingTimeStart = "13032018 07:00:00",
                WorkingTimeEnd = "13032018 07:00:00",
                Notes = "Testing",
                Status = "0",
                PickupType = "1",
                StallServiceType = "1",
                IsMitsubishi = "1",
                PreferredSA = "46127"
            };

            return obj;
        }
    }
}