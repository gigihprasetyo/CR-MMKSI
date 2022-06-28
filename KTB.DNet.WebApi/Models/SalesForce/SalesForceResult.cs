using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models.SalesForce
{
    public class SalesForceResult
    {
        public String Status { get; set; }
        public String Message { get; set; }
    }

    public class SalesForceResultArray
    {
        public List<SalesForceResult> sf { get; set; }
    }

}