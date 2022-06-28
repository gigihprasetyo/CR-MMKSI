using System;


namespace KTB.DNet.Interface.Model
{
    public class VWI_MSPExMembershipDto : ReadDtoBase
    {
        public int MSPCustomerID { get; set; }
        public int DealerId { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public int ChassisMasterID { get; set; }
        public string MSPCode { get; set; }
        public string ChassisNumber { get; set; }
        public string ColorCode { get; set; }
        public string VehicleTypeCode { get; set; }
        public string VehicleTypeDesc { get; set; }
        public int MSPKm { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public DateTime ValidUntil { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
