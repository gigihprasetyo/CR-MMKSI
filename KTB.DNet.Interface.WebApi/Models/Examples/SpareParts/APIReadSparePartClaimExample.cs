using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIReadSparePartClaimExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new
            {
                pages = 1,
                find = new List<object>{
                new {
                   MatchType = 0,
                   PropertyName = "LastUpdateTime",
                   PropertyValue = DateTime.Now.Date,
                   SqlOperation = 0
                }
               },
                sort = new List<object>{
                new {
                   SortColumn = "ID",
                   SortDirection = 0
                }
               }
            };
        }
    }
}