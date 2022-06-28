using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class GetServiceTypeDto
    {
        public string Kategori { get; set; }
        public string ServiceTypeCode { get; set; }
        public string KindCode { get; set; }
        public string KindDescription { get; set; }
        public decimal MaxDuration { get; set; }
    }
}
