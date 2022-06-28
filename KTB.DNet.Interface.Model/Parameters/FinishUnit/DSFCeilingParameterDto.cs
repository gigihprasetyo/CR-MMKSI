#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Framework.CustomAttribute;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class DSFCeilingParameterDto : ParameterDtoBaseCustom, IValidatableObject
    {
        public int? ID { get; set; }
        
        public string DealerCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string CreditAccount { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        
        [AntiXss]
        public decimal Outstanding { get; set; }

        [AntiXss]
        public int ProductCategoryID { get; set; }

        public decimal FactoringCeiling { get; set; }

        public decimal AvailableCeiling { get; set; }

        public DateTime MaxTOPDate { get; set; }

        public string DealerName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
  
   
}



