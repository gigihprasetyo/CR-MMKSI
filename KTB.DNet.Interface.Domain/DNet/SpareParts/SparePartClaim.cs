using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Domain
{
    public class SparePartClaim
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string ClaimNumber { get; set; }
        public int StatusID { get; set; }
        public int ApprovedQty { get; set; }
        public string SONumber { get; set; }
        public string SORetur { get; set; }
        public DateTime SOReturDate { get; set; }
        public string FakturRetur { get; set; }
        public DateTime FakturReturDate { get; set; }
    }
}
