using KTB.DNet.Interface.Model;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APIDSFCeilingSample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                DealerCode = "100085",
                CreditAccount = "100085",
                DealerName = "SARDANA INDAHBERLIAN MOTOR, PT",
                TotalCeiling = "100000",
                UpdatedBy = "DealerUser"
            };

            return obj;
        }
    }

}