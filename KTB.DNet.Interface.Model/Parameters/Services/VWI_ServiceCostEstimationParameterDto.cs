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
    public class VWI_ServiceCostEstimationParameterDto : IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [AntiXss]
        public string ChassisNumber { get; set; }

        [AntiXss]
        public string VechileModel { get; set; }

        [AntiXss]
        public string VariantType { get; set; }

        //[AntiXss]
        //public string VechileTypeCode { get; set; }

        public List<VWI_ServiceCostEstimationParameterDetailDto> Parameters { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            //if (string.IsNullOrEmpty(ChassisNumber) && string.IsNullOrEmpty(VechileTypeCode))
            //{
            //    results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgServiceBookingEstCost)));
            //}

            return results;
        }
    }

    public class VWI_ServiceCostEstimationParameterDetailDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ServiceTypeCode { get; set; }

        [AntiXss]
        public int ServiceTypeID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string KindCode { get; set; }
    }
}
