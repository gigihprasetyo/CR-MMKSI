using KTB.DNet.Interface.Model;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APIDSFLeasingClaimCreateSample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new List<DSFLeasingClaimCreateParameter>();
        }
    }

    public class APIDSFLeasingClaimGetFileSample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new APIDSFLeasingClaimGetFileSample();
        }
    }

    public class ResubmitClaimSample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new List<ResubmitClaimParamater>();
        }
    }
}