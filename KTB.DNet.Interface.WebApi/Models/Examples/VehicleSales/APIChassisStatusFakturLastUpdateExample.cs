using Swashbuckle.Examples;
using System;

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APIChassisStatusFakturLastUpdateExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                LastUpdateTime = "2019-10-23T15:00:00"
            };

            return obj;
        }
    }
}