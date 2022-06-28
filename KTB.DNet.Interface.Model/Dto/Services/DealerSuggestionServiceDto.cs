using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class DealerSuggestionServiceDto
    {
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        [IgnoreDataMember]
        public int Priority { get; set; }
        public string Distance { get; set; }
        public string Category { get; set; }
        public List<DealerSuggestionServiceDetailDto> ListJadwal { get; set; }
    }

    public class DealerSuggestionServiceDetailDto
    {
        public DateTime Tanggal { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
