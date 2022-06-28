using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class PODestinationDto : DtoBase
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string CityCode { get; set; }
        public string DealerCode { get; set; }
    }
}
