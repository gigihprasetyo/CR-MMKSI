using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class SendASBSFIDParameterDto
    { 
        public class CustomerSFIDParameterDto : IValidatableObject
        {
            public Int64 ID { set; get; }
            public int DealerID { set; get; }
            public string SalesmanCode { set; get; }
            public string CustomerType { set; get; }
            public string ClassType { set; get; }
            public string LevelData { set; get; }
            public string CustomerClass { set; get; }
            public Int16 CustomerTypeDNET { set; get; }
            public Int16 CustomerSubClass { set; get; }
            public string CustomerNo { set; get; }
            public string ParentCustomerNo { set; get; }
            public string FirstName { set; get; }
            public string LastName { set; get; }
            public DateTime? BirthDate { set; get; }
            public string CountryCode { set; get; }
            public string HPNo { set; get; }
            public Int16 OtherPhoneType { set; get; }
            public string OtherPhoneNo { set; get; }
            public string Email { set; get; }
            public string Gedung { set; get; }
            public string Alamat { set; get; }
            public string Kelurahan { set; get; }
            public string Kecamatan { set; get; }
            public string PreArea { set; get; }
            public string PostalCode { set; get; }
            public Int16 CityID { set; get; }
            public string POBox { set; get; }
            public int OCRIdentityID { get; set; }
            public Int16 IdentityType { set; get; }
            public string IdentityNumber { set; get; }
            public string IdentityURLPath { set; get; }
            public string NPWPNo { set; get; }
            public string NPWPName { set; get; }
            public Int16 PrintRegion { set; get; }
            public Int16 InterfaceStatus { set; get; }
            public string InterfaceMessage { set; get; }
            public Int16 InterfaceCustSales { set; get; }
            public string GUID { set; get; }
            public string GUIDUpdate { set; get; }
            public string Notes { set; get; }
            public Int16 RowStatus { set; get; }
            public Int16 Gender { set; get; }
            public string CreatedBy { set; get; }
            public DateTime CreatedTime { set; get; }
            public string LastUpdateBy { set; get; }
            public DateTime LastUpdateTime { set; get; }
            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                var results = new List<ValidationResult>();
                return results;
            }
        }

        public class CustomerSend 
        {
            public string dealerCode { get; set; }
            public string context { get; set; }
            public string externalCode { get; set; }
            public string extCodeRunning { get; set; }
            public string guidUpdate { get; set; }
            public string customerType { get; set; }
            public string customerClass { get; set; }
            public int classType { get; set; }
            public string customerNo { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string aliasName { get; set; }
            public string walkin { get; set; }
            public string checkOverDue { get; set; }
            public string checkCreditLimit { get; set; }
            public int levelData { get; set; }
            public int identificationType { get; set; }
            public string identificationNo { get; set; }
            public string birthdate { get; set; }
            public string taxZone { get; set; }
            public string taxRegistrationNo { get; set; }
            public string taxRegistrationName { get; set; }
            public string customerwithNPWP { get; set; }
            public string homePhone { get; set; }
            public string mobilePhone { get; set; }
            public string businessPhone { get; set; }
            public string email { get; set; }
            public string address1 { get; set; }
            public string gedung { get; set; }
            public string kelurahan { get; set; }
            public string kecamatan { get; set; }
            public string postalCode { get; set; }
            public string city { get; set; }
            public string preArea { get; set; }
            public bool cetakProvinsi { get; set; }
            public string pobox { get; set; }
            public DateTime? tanggalAttach { get; set; }
            public string linkKTPDNet { get; set; }
            public string interfaceUploadMessage { get; set; }
            public string originatingLead { get; set; }
            public string originatingContact { get; set; }
            public string originatingCustomerPublic { get; set; }
            public string customerPublicNo { get; set; }
            public string preferredMethod { get; set; }
            public string salesmanCode { get; set; }
            public string parentCustomerNo { get; set; }
            public string subClass { get; set; }
            public string CountryCode { get; set; }
            public string ocrniksim { get; set; }
            public string ocrnik { get; set; }
            public string attachments { get; set; }
            public string gendercode { get; set; }
            public string ID { get; set; }
            public bool InterfaceCustSales { get; set; }

        }
        public class SuspectSFIDParameterDto : IValidatableObject
        {
            public int ID { get; set; }
            public string SalesforceID { get; set; }
            public int DealerID { get; set; }
            public int SalesmanHeaderID { get; set; }
            public Int16 VechileTypeID { get; set; }
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
            public Int16 StatusCode { get; set; }
            public Int16 LeadStatus { get; set; }
            public Int16 StateCode { get; set; }
            public string CampaignName { get; set; }
            public int BusinessSectorDetailID { get; set; }
            public string VehicleModel { get; set; }
            public string Variant { get; set; }
            public int Sequence { get; set; }
            public string Name2 { get; set; }
            public string Telp { get; set; }
            public int? IdentityType { get; set; }
            public string IdentityNumber { get; set; }
            public int JobKind { get; set; }
            public int VechileColorID { get; set; }
            public int DealerVehiclePriceDetailID { get; set; }
            public decimal CusReqPrice { get; set; }
            public decimal CusReqDiscount { get; set; }
            public decimal BookingFee { get; set; }
            public int? BBNType { get; set; }
            public string BlankoSPKNo { get; set; }
            public string BlankoSPKDoc { get; set; }
            public Int16 InterfaceStatus { get; set; }
            public string InterfaceMessage { get; set; }
            public string GUIDUpdate { get; set; }
            public string Topic { get; set; }
            public Int16 VechileModelID { get; set; }
            public string CurrVehicleBrandDesc { get; set; }
            public string VehicleComparison { get; set; }
            public string GUID { get; set; }
            public string CountryCode { get; set; }
            public Int16 RowStatus { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedTime { get; set; }
            public string LastUpdateBy { get; set; }
            public DateTime LastUpdateTime { get; set; }
            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                var results = new List<ValidationResult>();
                return results;
            }
        }
        public class SuspectSend
        {
            public string DealerCode { get; set; }
            public string Context { get; set; }
            public string ExternalCode { get; set; }
            public string ExtCodeRunning { get; set; }
            public string Topic { get; set; }
            public string GUIDUpdate { get; set; }
            public string IdentificationNumber { get; set; }
            public string MobilePhone { get; set; }
            public string GUID { get; set; }
            public string UpdateState { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Alamat { get; set; }
            public string Gedung { get; set; }
            public string Kelurahan { get; set; }
            public string Kecamatan { get; set; }
            public string PostalCode { get; set; }
            public string City { get; set; }
            public string CountryCode { get; set; }
            public string BusinessPhone { get; set; }
            public string HomePhone { get; set; }
            public string Catatan { get; set; }
            public string CustomerClass { get; set; }
            public string PreferredVecModel { get; set; }
            public string CurrentVehicleBrand { get; set; }
            public string CurrentVehicleModel { get; set; }
            public string RegistrationCode { get; set; }
            public int? SubClass { get; set; }
            public int? IdentificationType { get; set; }
            public int? Gender { get; set; }
            public int Quantity { get; set; }
            public int? LeadSourceCode { get; set; }
            public int? CurrentVehicleBrandValue { get; set; }
            public int? AgeSegment { get; set; }
            public int? InformationSource { get; set; }
            public int? ReasonForVisit { get; set; }
            public int? IndustryCode { get; set; }
            public int? TypeOfVisit { get; set; }
            public string BirthDate { get; set; }
            public string LeadDate { get; set; }
            public string EstimatePurchaseDate { get; set; }
            public string Product { get; set; }
            public string ProductColor { get; set; }
            public string EmployeeCode { get; set; }
            public string Contact { get; set; }
            public string Activities { get; set; }
            public string VehicleModel { get; set; }
            public string CampaignID { get; set; }

        }
        public class SuspectContactSFIDParameterDto : IValidatableObject
        {
            public SuspectSFIDParameterDto Lead { get; set; }
            public SuspectContactParameterDto Contact { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                var results = new List<ValidationResult>();
                return results;
            }
        }
        public class SuspectContactParameterDto
        {
            public Int64 ID { set; get; }
            public string SalesmanCode { set; get; }
            public int DealerID { set; get; }
            public string FirstName { set; get; }
            public string LastName { set; get; }
            public int? SPKMasterCountryCodePhoneID { set; get; }
            public string HPNo { set; get; }
            public Int16 PhoneType { set; get; }
            public string Phone { set; get; }
            public string Email { set; get; }
            public Int16 Gender { set; get; }
            public string Address { set; get; }
            public int? CityId { set; get; }
            public Int16 CustomerType { set; get; }
            public Int16 LeadSource { set; get; }
            public string Notes { set; get; }
            public Int16 RowStatus { set; get; }
            public string CreatedBy { set; get; }
            public DateTime? CreatedTime { set; get; }
            public string LastUpdateBy { set; get; }
            public DateTime? LastUpdateTime { set; get; }

        }
        public class SuspectContactSend
        {
            public string DealerCode { set; get; }
            public string Context { set; get; }
            public string ExternalCode { set; get; }
            public string ExtCodeRunning { set; get; }
            public string Topic { set; get; }
            public string GUIDUpdate { set; get; }
            public string IdentificationNumber { set; get; }
            public string MobilePhone { set; get; }
            public ContactSend Contact { set; get; }
            public string ID { set; get; }
            public int UpdateState { set; get; }
            public string Catatan { set; get; }
            public string FirstName { set; get; }
            public string LastName { set; get; }
            public string Email { set; get; }
            public string Alamat { set; get; }
            public string Gedung { set; get; }
            public string Kelurahan { set; get; }
            public string Kecamatan { set; get; }
            public string PostalCode { set; get; }
            public string City { set; get; }
            public string CountryCode { set; get; }
            public string BusinessPhone { set; get; }
            public string HomePhone { set; get; }
            public string CustomerClass { set; get; }
            public string PreferredVecModel { set; get; }
            public string CurrentVehicleBrand { set; get; }
            public string CurrentVehicleModel { set; get; }
            public string RegistrationCode { set; get; }
            public int? SubClass { set; get; }
            public int? IdentificationType { set; get; }
            public int? Gender { set; get; }
            public int Quantity { set; get; }
            public int? LeadSourceCode { set; get; }
            public int? CurrentVehicleBrandValue { set; get; }
            public int? AgeSegment { set; get; }
            public int? InformationSource { set; get; }
            public int? ReasonForVisit { set; get; }
            public int? IndustryCode { set; get; }
            public int? TypeOfVisit { set; get; }
            public string BirthDate { set; get; }
            public string LeadDate { set; get; }
            public string EstimatePurchaseDate { set; get; }
            public string Product { set; get; }
            public string ProductColor { set; get; }
            public string EmployeeCode { set; get; }
        }
        public class ContactSend
        {
            public string IdentificationNumber { set; get; }
            public string MobilePhone { set; get; }
            public string FirstName { set; get; }
            public string LastName { set; get; }
            public string HomePhone { set; get; }
            public string Email { set; get; }
            public int? Gender { set; get; }
            public string BirthDate { set; get; }
            public string Address1 { set; get; }
            public string Address2 { set; get; }
            public string Address3 { set; get; }
            public string Address4 { set; get; }
            public string City { set; get; }
            public int CustomerType { set; get; }
            public int? IdentificationType { set; get; }
            public string ExternalCode { set; get; }
            public string GUIDUpdate { set; get; }

        }
        public class SuspectDisqualifiedSend
        {
            public string DealerCode { set; get; }
            public string Context { set; get; }
            public string ExternalCode { set; get; }
            public string ExtCodeRunning { set; get; }
            public string GUIDUpdate { set; get; }
            public string ID { set; get; }
            public int UpdateState { set; get; }
            public string Product { set; get; }
            public string ProductColor { set; get; }
            public string EmployeeCode { set; get; }

        }
        public class ProspectSend
        {
            public string GUID { set; get; }
            public string GUIDUpdate { set; get; }
            public string ExternalCode { set; get; }
            public string ExtCodeRunning { set; get; }
            public string Topic { set; get; }
            public string ID { set; get; }
            public string UpdateState { set; get; }
            public string LeadId { set; get; }
            public string ContactId { set; get; }
            public string DealerCode { set; get; }
            public string Context { set; get; }
            public string EstimatedCloseDate { set; get; }
            public int? TipeBBN { set; get; }
            public string BookingFee { set; get; }
            public int? Occupation { set; get; }
            public int Rating { set; get; }
            public int Quantity { set; get; }
            public string LeadDate { set; get; }
            public string EmployeeCode { set; get; }
            public string ProductCode { set; get; }
            public string ProductColorCode { set; get; }
            public string CampaignID { set; get; }
            public string Activities { set; get; }
            public string Catatan { set; get; }
            public string VehicleModel { get; set; }
            public string BirthDate { get; set; }
            public string Email { get; set; }

        }

        public class CreateProspectSend
        {
            public string GUID { set; get; }
            public string GUIDUpdate { set; get; }
            public string ExternalCode { set; get; }
            public string ExtCodeRunning { set; get; }
            public string Topic { set; get; }
            public string ID { set; get; }
            public string UpdateState { set; get; }
            public string LeadId { set; get; }
            public string ContactId { set; get; }
            public string DealerCode { set; get; }
            public string Context { set; get; }
            public string EstimatedCloseDate { set; get; }
            public int? TipeBBN { set; get; }
            public string BookingFee { set; get; }
            public int? Occupation { set; get; }
            public int Rating { set; get; }
            public int Quantity { set; get; }
            public string LeadDate { set; get; }
            public string EmployeeCode { set; get; }
            public string ProductCode { set; get; }
            public string ProductColorCode { set; get; }
            public string CampaignID { set; get; }
            public string Catatan { set; get; }
            public string VehicleModel { get; set; }
            public string BirthDate { get; set; }
            public string Email { get; set; }
            public List<Activities> Activities { get; set; }

        }

        public class ProspectWonLostSend
        {
            public string GUID { set; get; }
            public string GUIDUpdate { set; get; }
            public string ExternalCode { set; get; }
            public string ExtCodeRunning { set; get; }
            public string Topic { set; get; }
            public string ID { set; get; }
            public string UpdateState { set; get; }
            public string LeadId { set; get; }
            public string ContactId { set; get; }
            public string DealerCode { set; get; }
            public string Context { set; get; }
            public string EstimatedCloseDate { set; get; }
            public int? TipeBBN { set; get; }
            public string BookingFee { set; get; }
            public int? Occupation { set; get; }
            public int Rating { set; get; }
            public int Quantity { set; get; }
            public string LeadDate { set; get; }
            public string EmployeeCode { set; get; }
            public string ProductCode { set; get; }
            public string ProductColorCode { set; get; }
            public int? AlasanBatal { set; get; }

        }

        public class ActivitySFIDParameterDto : IValidatableObject
        {
            public sapcustomeractivity lead { get; set; }
            public ActivityCreateUpdateSend activity { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                var results = new List<ValidationResult>();
                return results;
            }
        }
        public class sapcustomeractivity
        {
            public int ID { get; set; }
            public string SalesforceID { get; set; }
            public int? DealerID { get; set; }
            public int? SalesmanHeaderID { get; set; }
            public Int16? VechileTypeID { get; set; }
            public string CustomerCode { get; set; }
            public string CustomerName { get; set; }
            public Int16? CustomerType { get; set; }
            public string CustomerAddress { get; set; }
            public Int16? PhoneType { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public Int16? Sex { get; set; }
            public Int16? AgeSegment { get; set; }
            public Int16? CustomerPurpose { get; set; }
            public Int16? InformationType { get; set; }
            public Int16? InformationSource { get; set; }
            public Int16? Status { get; set; }
            public int? Qty { get; set; }
            public DateTime? ProspectDate { get; set; }
            public bool? isSPK { get; set; }
            public string CurrVehicleBrand { get; set; }
            public int? Rating { get; set; }
            public string CurrVehicleType { get; set; }
            public string Note { get; set; }
            public string WebID { get; set; }
            public DateTime? BirthDate { get; set; }
            public string PreferedVehicleModel { get; set; }
            public string Description { get; set; }
            public DateTime? EstimatedCloseDate { get; set; }
            public Guid? OriginatingLeadId { get; set; }
            public Int16? StatusCode { get; set; }
            public Int16? LeadStatus { get; set; }
            public Int16? StateCode { get; set; }
            public string CampaignName { get; set; }
            public int? BusinessSectorDetailID { get; set; }
            public string VehicleModel { get; set; }
            public string Variant { get; set; }
            public int? Sequence { get; set; }
            public string Name2 { get; set; }
            public string Telp { get; set; }
            public int? IdentityType { get; set; }
            public string IdentityNumber { get; set; }
            public int? JobKind { get; set; }
            public int? VechileColorID { get; set; }
            public int? DealerVehiclePriceDetailID { get; set; }
            public decimal? CusReqPrice { get; set; }
            public decimal? CusReqDiscount { get; set; }
            public decimal? BookingFee { get; set; }
            public int? BBNType { get; set; }
            public string BlankoSPKNo { get; set; }
            public string BlankoSPKDoc { get; set; }
            public Int16? InterfaceStatus { get; set; }
            public string InterfaceMessage { get; set; }
            public string GUIDUpdate { get; set; }
            public string Topic { get; set; }
            public Int16? VechileModelID { get; set; }
            public string CurrVehicleBrandDesc { get; set; }
            public string VehicleComparison { get; set; }
            public string GUID { get; set; }
            public string CountryCode { get; set; }
            public Int16? RowStatus { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedTime { get; set; }
            public string LastUpdateBy { get; set; }
            public DateTime? LastUpdateTime { get; set; }
        }
        public class ActivityCreateUpdateSend
        {
            public string SalesmanCode { get; set; }
            public int DealerID { get; set; }
            public int ProfileID { get; set; }
            public int CustomerDataID { get; set; }
            public int GroupID { get; set; }
            public int DataType { get; set; }

            public Activity_JanjiTemu JanjiTemu { set; get; }
            public Activity_Email Email { set; get; }
            public Activity_Telp Telp { set; get; }
            public Activity_PesanTugas PesanTugas { set; get; }
        }
        public class Activity_JanjiTemu
        {
            public int ID { get; set; }
            public string TipeJanjiTemu { get; set; }
            public string Subject { get; set; }
            public string TanggalMulai { get; set; }
            public string TanggalBerakhir { get; set; }
            public string TampilkanWaktu { get; set; }

            public string HasilAktivitas { get; set; }
            public string StatusAktivitas { get; set; }
            public string Rating { get; set; }
            public string Catatan { get; set; }

        }
        public class Activity_Email
        {
            public int ID { get; set; }
            public string TipeEmail { get; set; }
            public string EmailPengirim { get; set; }
            public string TanggalKirim { get; set; }
            public string EmailPenerima { get; set; }
            public string cc { get; set; }
            public string Subject { get; set; }
            public string IsiPesan { get; set; }

            public string HasilAktivitas { get; set; }
            public string StatusAktivitas { get; set; }
            public string Rating { get; set; }
            public string Catatan { get; set; }
        }
        public class Activity_Telp
        {
            public int ID { get; set; }
            public string JenisPanggilan { get; set; }
            public string ArahTelp { get; set; }
            public string Subject { get; set; }
            public string NamaSales { get; set; }
            public string NomorSuspect { get; set; }
            public string TanggalStart { get; set; }
            public string TanggalEnd { get; set; }

            public string HasilAktivitas { get; set; }
            public string StatusAktivitas { get; set; }
            public string Rating { get; set; }
            public string Catatan { get; set; }
        }
        public class Activity_PesanTugas
        {
            public int ID { get; set; }
            public string TipePesan { get; set; }
            public string Deskripsi { get; set; }
            public string Subject { get; set; }
            public string NamaSales { get; set; }
            public string NomorSuspect { get; set; }
            public string TanggalKirim { get; set; }
            public string Reason { get; set; }

            public string HasilAktivitas { get; set; }
            public string StatusAktivitas { get; set; }
            public string Rating { get; set; }
            public string Catatan { get; set; }
        }

        public class ActivitySend
        {
            public string DealerCode { get; set; }
            public string Context { get; set; }
            public string ExternalCode { get; set; }
            public string ExtCodeRunning { get; set; }
            public string Topic { get; set; }
            public string GUIDUpdate { get; set; }
            public string IdentificationNumber { get; set; }
            public string MobilePhone { get; set; }
            public string GUID { get; set; }
            public string UpdateState { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Alamat { get; set; }
            public string Gedung { get; set; }
            public string Kelurahan { get; set; }
            public string Kecamatan { get; set; }
            public string PostalCode { get; set; }
            public string City { get; set; }
            public string CountryCode { get; set; }
            public string BusinessPhone { get; set; }
            public string HomePhone { get; set; }
            public string Catatan { get; set; }
            public string CustomerClass { get; set; }
            public string PreferredVecModel { get; set; }
            public string CurrentVehicleBrand { get; set; }
            public string CurrentVehicleModel { get; set; }
            public string RegistrationCode { get; set; }
            public int? SubClass { get; set; }
            public int? IdentificationType { get; set; }
            public int? Gender { get; set; }
            public int? Quantity { get; set; }
            public int? LeadSourceCode { get; set; }
            public int? CurrentVehicleBrandValue { get; set; }
            public int? AgeSegment { get; set; }
            public int? InformationSource { get; set; }
            public int? ReasonForVisit { get; set; }
            public int? IndustryCode { get; set; }
            public int? TypeOfVisit { get; set; }
            public string BirthDate { get; set; }
            public string LeadDate { get; set; }
            public string EstimatePurchaseDate { get; set; }
            public string Product { get; set; }
            public string ProductColor { get; set; }
            public string EmployeeCode { get; set; }
            public string Contact { get; set; }
            public List<Activities> Activities { get; set; }
            public string VehicleModel { get; set; }
            public string CampaignID { get; set; }

        }
        public class Activities
        {
            public int ActivityType { get; set; }
            public DateTime? ScheduledStart { get; set; }
            public DateTime? ScheduledEnd { get; set; }
            public int ActivityResult { get; set; }
            public int ActivityRating { get; set; }
            public int ActivityStatus { get; set; }
            public string Subject { get; set; }
            public int? StatusCode { get; set; }
            public string Catatan { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string CC { get; set; }
            public int? Direction { get; set; }
            public string ExternalCode { get; set; }

            public int? Type { get; set; }
            public string ShowTimeAs { get; set; }
            public string Email { get; set; }
            public string Sender { get; set; }
            public string Receipt { get; set; }
            public string StatusReason { get; set; }
            public string Description { get; set; }
            public string Due { get; set; }
        }

        public class ActivityContactSFIDParameterDto 
        {
            public string SalesmanCode { get; set; }
            public int DealerID { get; set; }
            public int CustomerDataID { get; set; }
            public int GroupID { get; set; }
            public int DataType { get; set; }

            public int ProfileID { get; set; }
            public string TanggalAktivitas { get; set; }
            public Int16 StatusAktivitas { get; set; }
            public string Catatan { get; set; }
            
        }

        public class ActivitySuspectQualifiedSend
        {
            public string SalesmanCode { get; set; }
            public int DealerID { get; set; }
            public int CustomerDataID { get; set; }
            public int DataType { get; set; }
            public List<JanjiTemu> JanjiTemu { set; get; }
            public List<Email> Email { set; get; }
            public List<Telp> Telp { set; get; }
            public List<PesanTugas> Tugas { set; get; }
        }

        public class JanjiTemu
        {
            public int ProfileID { get; set; }
            public int GroupID { get; set; }

            public string TipeJanjiTemu { get; set; }
            public string Subject { get; set; }
            public string TanggalMulai { get; set; }
            public string TanggalBerakhir { get; set; }
            public string TampilkanWaktu { get; set; }

            public string HasilAktivitas { get; set; }
            public string StatusAktivitas { get; set; }
            public string Rating { get; set; }
            public string Catatan { get; set; }
        }

        public class Email
        {
            public int ProfileID { get; set; }
            public int GroupID { get; set; }

            public string TipeEmail { get; set; }
            public string EmailPengirim { get; set; }
            public string TanggalKirim { get; set; }
            public string EmailPenerima { get; set; }
            public string cc { get; set; }
            public string Subject { get; set; }
            public string IsiPesan { get; set; }

            public string HasilAktivitas { get; set; }
            public string StatusAktivitas { get; set; }
            public string Rating { get; set; }
            public string Catatan { get; set; }
        }

        public class Telp
        {
            public int ProfileID { get; set; }
            public int GroupID { get; set; }

            public string JenisPanggilan { get; set; }
            public string ArahTelp { get; set; }
            public string Subject { get; set; }
            public string NamaSales { get; set; }
            public string NomorSuspect { get; set; }
            public string TanggalStart { get; set; }
            public string TanggalEnd { get; set; }

            public string HasilAktivitas { get; set; }
            public string StatusAktivitas { get; set; }
            public string Rating { get; set; }
            public string Catatan { get; set; }
        }

        public class PesanTugas
        {
            public int ProfileID { get; set; }
            public int GroupID { get; set; }

            public string TipePesan { get; set; }
            public string Deskripsi { get; set; }
            public string Subject { get; set; }
            public string NamaSales { get; set; }
            public string NomorSuspect { get; set; }
            public string TanggalKirim { get; set; }
            public string Reason { get; set; }

            public string HasilAktivitas { get; set; }
            public string StatusAktivitas { get; set; }
            public string Rating { get; set; }
            public string Catatan { get; set; }
        }

        public class CreateProspect
        {
            public SuspectSFIDParameterDto lead { get; set; }
            public ActivitySuspectQualifiedSend activity { get; set; }
        }
    }
}
