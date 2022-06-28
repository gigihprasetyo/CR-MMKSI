using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIDeleteStallMasterExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                DealerCode = "100699",
                StallCode = "100170-S03",
                StallCodeDealer = "001-SRI",
                StallName = "Test Stall 3"
            };

            return obj;
        }
    }
}