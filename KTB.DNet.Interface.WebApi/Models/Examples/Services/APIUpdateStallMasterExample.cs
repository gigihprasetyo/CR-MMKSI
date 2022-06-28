#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : API Stall Master class
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
    public class APIUpdateStallMasterExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 34,
                DealerCode = "100170",
                StallCode = "100170-S03",
                StallCodeDealer = "001-SRI",
                StallName = "Test Stall 3",
                StallLocation = "1",
                StallType = "0",
                StallCategory = "2",
                IsBodyPaint = "1",
                Status = "1"
            };

            return obj;
        }
    }
}