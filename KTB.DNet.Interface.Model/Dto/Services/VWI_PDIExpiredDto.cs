using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class VWI_PDIExpiredDto
    {
        public int ID { get; set; }
        public int PDIID { get; set; }
        public string DealerCode { get; set; }
        public string ChassisNumber { get; set; }
        public string WorkOrderNumber { get; set; }
        public DateTime ExpiredPDIDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}
