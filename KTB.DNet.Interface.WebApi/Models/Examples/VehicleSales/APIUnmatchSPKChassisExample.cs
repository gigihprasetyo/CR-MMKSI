using KTB.DNet.Interface.Model;
using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APIUnmatchSPKChassisExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new VWI_UnmatchSPKChassisFilterDto
            {
                pages = 1,
                ChassisNumber = "MK2KRWPNUJJ017060"
            };

            return obj;
        }

    }
}