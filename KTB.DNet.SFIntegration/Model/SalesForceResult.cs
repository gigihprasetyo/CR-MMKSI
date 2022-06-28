using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.SFIntegration.Model
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
