#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Framework.CustomAttribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class DSFPaymentParameterDto : ParameterDtoBaseCustom, IValidatableObject
    {
        public int? ID { get; set; }
        
        [AntiXss]
        public string SlipNumber { get; set; }
              
        public string SONumber { get; set; }

        public decimal Amount { get; set; }

        public short Status { get; set; }

        public short IsCleared { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
  
   
}



