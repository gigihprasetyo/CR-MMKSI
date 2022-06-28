using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model.Parameters
{
    public class SparePartClaimDetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public string NoBarang { get; set; }
        public int QtyClaim { get; set; }
        public byte StatusDetail { get; set; }
        public byte StatusDetailKTB { get; set; }
        public int ClaimGoodConditionId { get; set; }
        public string Keterangan { get; set; }
        public string UpdatedBy { get; set; }
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
