using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Domain
{
    public class SAPCustomer
    {
        public int ID { get; set; }
        public string SalesforceID { get; set; }
        public int DealerID { get; set; }
        public int? SalesmanHeaderID { get; set; }
        public Int16? VechileTypeID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public Int16 CustomerType { get; set; }
        public string CustomerAddress { get; set; }
        public Int16 PhoneType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Int16 Sex { get; set; }
        public Int16 AgeSegment { get; set; }
        public Int16 CustomerPurpose { get; set; }
        public Int16 InformationType { get; set; }
        public Int16 InformationSource { get; set; }
        public Int16 Status { get; set; }
        public int Qty { get; set; }
        public DateTime ProspectDate { get; set; }
        public bool isSPK { get; set; }
        public string CurrVehicleBrand { get; set; }
        public int Rating { get; set; }
        public string CurrVehicleType { get; set; }
        public string Note { get; set; }
        public string WebID { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PreferedVehicleModel { get; set; }
        public string Description { get; set; }
        public DateTime? EstimatedCloseDate { get; set; }
        public Guid OriginatingLeadId { get; set; }
        public Int16? StatusCode { get; set; }
        public Int16 LeadStatus { get; set; }
        public Int16? StateCode { get; set; }
        public string CampaignName { get; set; }
        public int? BusinessSectorDetailID { get; set; }
        public string VehicleModel { get; set; }
        public string Variant { get; set; }
        public int Sequence { get; set; }
        public string Name2 { get; set; }
        public string Telp { get; set; }
        public int? IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public int JobKind { get; set; }
        public int? VechileColorID { get; set; }
        public int? DealerVehiclePriceDetailID { get; set; }
        public decimal CusReqPrice { get; set; }
        public decimal CusReqDiscount { get; set; }
        public decimal BookingFee { get; set; }
        public int BBNType { get; set; }
        public string BlankoSPKNo { get; set; }
        public string BlankoSPKDoc { get; set; }
        public Int16 InterfaceStatus { get; set; }
        public string InterfaceMessage { get; set; }
        public string GUIDUpdate { get; set; }
        public string Topic { get; set; }
        public Int16? VechileModelID { get; set; }
        public string CurrVehicleBrandDesc { get; set; }
        public string VehicleComparison { get; set; }
        public string GUID { get; set; }
        public string CountryCode { get; set; }
        public Int16 RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
