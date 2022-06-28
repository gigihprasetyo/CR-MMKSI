using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIGetServiceTypeExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                ChassisNumber = "MK2NCWPARHJ001626"
            };

            return obj;
        }
    }
}