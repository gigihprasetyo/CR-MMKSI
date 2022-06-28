using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class VWI_ServiceCostEstimationDto
    {
        public decimal Total { get; set; }
        public List<VWI_ServiceCostEstimationSummaryDto> Summaries { get; set; }
    }

    public class VWI_ServiceCostEstimationSummaryDto
    {
        public decimal JasaService { get; set; }
        public string JenisService { get; set; }
        public string JenisKegiatan { get; set; }
        public string KindCode { get; set; }
        public List<VWI_ServiceCostEstimationDetailDto> Details { get; set; }
    }

    public class VWI_ServiceCostEstimationDetailDto
    {
        public string PartName { get; set; }
        public decimal RetalPrice { get; set; }
        public decimal PartQuantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SubTotal { get; set; }
    }
}
