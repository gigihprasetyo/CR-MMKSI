using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples 
{
    public class APISendASBSFIDCustomerExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                ID = 200,
                DealerID = 32,
                SalesmanCode = "",
                CustomerType = "",
                LevelData = "",
                CustomerClass = ""
            };

            return obj;
        }
    }
}