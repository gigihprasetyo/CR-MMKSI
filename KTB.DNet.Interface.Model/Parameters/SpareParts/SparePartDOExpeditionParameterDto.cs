#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDOExpeditionParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SparePartDOExpeditionParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [AntiXss]
        public string ExpeditionNo { get; set; }

        [AntiXss]
        public string ExpeditionName { get; set; }

        public DateTime ETA { get; set; }

        public DateTime ETD { get; set; }

        public DateTime ATD { get; set; }

        public DateTime ATA { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }

    public class SPDOExpeditionUpdateATAParameterDto : ParameterDtoBase, IValidatableObject
    {
        [Display(ResourceType = typeof(FieldResource), Name = "DONumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DONumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ATA")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime ATA { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
