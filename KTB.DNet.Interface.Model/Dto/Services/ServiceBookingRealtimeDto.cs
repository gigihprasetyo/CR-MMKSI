using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class ServiceBookingRealtimeDto
    {
        public string ServiceBookingCode { get; set; }
    }

    public class ServiceBookingRealtimeReadDto
    {
        public int ID { get; set; }
        public string ServiceBookingCode { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string ChassisNumber { get; set; }
        public int StallMasterID { get; set; }
        public string StallCode { get; set; }
        public string VehicleTypeCode { get; set; }
        public string VechileModelDescription { get; set; }
        public string VechileTypeDescription { get; set; }
        public string PlateNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public int Odometer { get; set; }
        public int ServiceAdvisorID { get; set; }
        public string ServiceAdvisorName { get; set; }
        public string IncomingPlan { get; set; }
        public string StallServiceType { get; set; }
        public decimal StandardTime { get; set; }
        public DateTime IncomingDateStart { get; set; }
        public DateTime IncomingDateEnd { get; set; }
        public DateTime WorkingTimeStart { get; set; }
        public DateTime WorkingTimeEnd { get; set; }
        public string IsMitsubishi { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public decimal Total { get; set; }
        public List<ServiceBookingActivityRealtimeReadDto> ServiceBookingActivities { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }

    public class ServiceBookingActivityRealtimeReadDto
    {
        public string ServiceType { get; set; }
        public string KindCode { get; set; }
        public string Description { get; set; }
        public List<ServiceCostEstimationSummaryDto> EstimationCosts { get; set; }
    }

    public class ServiceCostEstimationSummaryDto
    {
        public decimal JasaService { get; set; }
        public List<ServiceCostEstimationDetailDto> Details { get; set; }
    }

    public class ServiceCostEstimationDetailDto
    {
        public string PartName { get; set; }
        public decimal RetalPrice { get; set; }
        public decimal PartQuantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SubTotal { get; set; }
    }
}
