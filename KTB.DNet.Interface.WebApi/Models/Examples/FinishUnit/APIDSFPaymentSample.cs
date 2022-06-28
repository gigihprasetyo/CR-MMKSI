using KTB.DNet.Interface.Model;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APIDSFPaymentSample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                CreditAccount = "100000",
                UpdatedBy = "DealerUser"
            };

            return obj;
        }
    }

}