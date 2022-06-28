using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class ServiceBookingRealtimeParameterDto : IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string ChassisNumber { get; set; }
        [AntiXss]
        public string ServiceBookingCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string VechileModel { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string VariantType { get; set; }
        //[AntiXss]
        //public string VechileTypeCode { get; set; }
        [AntiXss]
        public string PlateNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string CustomerName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string CustomerPhoneNumber { get; set; }
        [AntiXss]
        public int Odometer { get; set; }
        [AntiXss]
        [DisplayFormat(DataFormatString = "ddMMyyyy HH:mm:ss")]
        public DateTime IncomingDateStart { get; set; }
        [AntiXss]
        [DisplayFormat(DataFormatString = "ddMMyyyy HH:mm:ss")]
        public DateTime IncomingDateEnd { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        [DisplayFormat(DataFormatString = "ddMMyyyy HH:mm:ss")]
        public DateTime WorkingTimeStart { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        [DisplayFormat(DataFormatString = "ddMMyyyy HH:mm:ss")]
        public DateTime WorkingTimeEnd { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string PickupType { get; set; }
        [AntiXss]
        public int ServiceAdvisorID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string StallServiceType { get; set; }
        [AntiXss]
        public short IsMitsubishi { get; set; }
        [AntiXss]
        public string Status { get; set; }
        [AntiXss]
        public string GRNote { get; set; }
        [AntiXss]
        public string Complaint { get; set; }
        [AntiXss]
        public string Voucher { get; set; }
        [AntiXss]
        public string UserID { get; set; }

        public List<ServiceBookingActivityRealtimeParameterDto> ServiceBookingActivities { get; set; }

        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class ServiceBookingActivityRealtimeParameterDto : IValidatableObject
    {
        [AntiXss]
        public int ServiceTypeID { get; set; }
        [AntiXss]
        public string ServiceTypeCode { get; set; }
        [AntiXss]
        public string KindCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
