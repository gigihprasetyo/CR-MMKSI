#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : WarrantyActivationParameterDto class
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
    public class WarrantyActivationParameterDto : IValidatableObject
    {
        [Display(ResourceType = typeof(FieldResource), Name = "ChassisNumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ChassisNumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "RequestDate")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //[DisplayFormat(DataFormatString = "dd/MM/yy")]
        public DateTime RequestDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CustomerName")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public string CustomerName { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "NOHP")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public string HandphoneNo { get; set; }

        public string PlateNumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DigitalSignature")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public AttachmentParameterDto DigitalSignature { get; set; }

        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Utils.IsAlphaNumeric(ChassisNumber)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, ChassisNumber))); }

            if (string.IsNullOrEmpty(DigitalSignature.Base64OfStream))
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.DigitalSignature)));
            }

            return results;
        }
    }
}
