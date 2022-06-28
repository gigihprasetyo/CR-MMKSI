using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model.Parameters
{
    public class SparePartClaimParameterDto : ParameterDtoBase, IValidatableObject
    {
        public string UpdatedBy { get; set; }
        public string ClaimDate { get; set; }
        public string DealerCode { get; set; }
        public int ClaimReasonid { get; set; }
        public int StatusID { get; set; }
        public byte Status { get; set; }
        public byte StatusKTB { get; set; }
        public byte ClaimProgressId { get; set; }
        public string SONumber { get; set; }
        public string NoFaktur { get; set; }
        public string Description { get; set; }
        public List<SparePartClaimDetailParameterDto> ClaimDetails { get; set; }
        public List<SparePartClaimDocumentParameterDto> DocumentUpload { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            //// custom validation
            //if (!Utils.IsAlphaNumericPlusUniv(DealerSPKNumber)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.SPKDealerNumber))); }

            //// Return if any errors
            //if (results.Count > 0)
            //{ return results; }

            //// validate SPK Customer
            //Validator.TryValidateObject(SPKCustomer, new ValidationContext(SPKCustomer, null, null), results);

            //// Return if any errors
            //if (results.Count > 0)
            //{ return results; }

            //// validate SPK Details
            //foreach (SPKDetailParameterDto detail in SPKDetails)
            //{
            //    Validator.TryValidateObject(detail, new ValidationContext(detail, null, null), results);
            //}

            return results;
        }
    }


}
