using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APISendASBSFIDSuspectContactExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                Lead = new List<object>{
                    new {
                        ID=23,
                        DealerID = 4
                    } 
                },
                Contact = new List<object>{
                    new {
                        ID=23,
                        DealerID = 4
                    }
                }
            };
            
            return obj;
        }
    }
}