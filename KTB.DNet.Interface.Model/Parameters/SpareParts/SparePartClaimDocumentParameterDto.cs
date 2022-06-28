using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model.Parameters
{
    public class SparePartClaimDocumentParameterDto : ParameterDtoBase, IValidatableObject
    {
        public string UpdatedBy { get; set; }
        public int Type { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
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
