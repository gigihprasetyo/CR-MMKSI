using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class V_WarrantyActivationDto
    {
        public int ID { get; set; }
        public string ChassisNumber { get; set; }
        public short SoldDealerID { get; set; }
        public string CustomerName { get; set; }
        public int PDIID { get; set; }
        public string PDIStatus { get; set; }
        public DateTime PDIDate { get; set; }
        public string PDICertificateFile { get; set; }
        public int WarantyActivationID { get; set; }
        public DateTime WADate { get; set; }
        public string WaCertificateFile { get; set; }
        public string WARequestor { get; set; }
        public string SPKDealerCode { get; set; }
        public string SPKDealerName { get; set; }
        public string DSFilePath { get; set; }
        public DateTime PKTDate { get; set; }
        public int WaStatus { get; set; }
        public string WaStatusDesc { get; set; }
        public int DealerGroupID { get; set; }
        public string SalesmanCode { get; set; }
        public string SalesmanName { get; set; }
        public string PilotingWarranty { get; set; }
        public DateTime LastUpdateTimePKT { get; set; }
        public DateTime LastUpdateTimePDI { get; set; }
    }
}
