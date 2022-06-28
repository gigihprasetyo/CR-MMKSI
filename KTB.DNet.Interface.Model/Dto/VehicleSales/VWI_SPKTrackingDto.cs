using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class VWI_SPKTrackingDto : DtoBase
    {
        [IgnoreDataMember]
        public int ID { get; set; }
        public string SPKNumber { get; set; }
        public string DealerSPKNumber { get; set; }
        public DateTime? DealerSPKDate { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string SalesmanCode { get; set; }
        public string SalesmanName { get; set; }
        public string SPKCustomer { get; set; }
        public string RejectedReason { get; set; }
        public string DealerCity { get; set; }
        public string CustomerType { get; set; }
        public string PilotingSPKMatching { get; set; }
        public new DateTime CreatedTime { get; set; }
        public new DateTime LastUpdateTime { get; set; }
        public List<VWI_SPKTrackingDetailDto> SPKDetail { get; set; }
        [IgnoreDataMember]
        public string BranchName { get; set; }
    }

    public class VWI_SPKTrackingDetailDto
    {
        public string MaterialDescription { get; set; }
        public string Remarks { get; set; }
        public string RejectedReason { get; set; }
        public int Quantity { get; set; }
        public string WebModel { get; set; }
        public string WebVariant { get; set; }
        public string ColorCode { get; set; }
        public string ColorIndName { get; set; }
        public string ColorEngName { get; set; }
        public string ColorWeb { get; set; }
        public List<VWI_SPKTrackingVINDto> VIN { get; set; }
    }

    public class VWI_SPKTrackingVINDto
    {
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public List<VWI_SPKTrackingInfoDto> TrackingStatus { get; set; }
    }

    public class VWI_SPKTrackingInfoDto
    {
        public string ProcessName { get; set; }
        public int ProcessSequence { get; set; }
        public DateTime? ProcessDateTime { get; set; }
        public string StatusDescription { get; set; }
    }
}
