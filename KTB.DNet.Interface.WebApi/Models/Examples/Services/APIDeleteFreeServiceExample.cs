using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIDeleteFreeServiceExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                DealerCode = "100699",
                DealerBranchCode = "",
                WorkOrderNumber = "TEST123456",
                ChassisNumber = "HanyaMobilMitsubishiAJA",
                UpdatedBy = "DealerUser"
            };

            return obj;
        }
    }
}