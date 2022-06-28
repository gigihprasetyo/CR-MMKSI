#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterPKTParameterDto  class
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
    public class ChassisMasterPKTParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ChassisNumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ChassisNumber { get; set; }

        [AntiXss]
        public string DealerCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime PKTDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (PKTDate.Date > DateTime.Now.Date)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDateCompareError, FieldResource.PKT, ValidationResource.GreaterThan, FieldResource.TodayDate)));
            }

            return results;
        }
    }
}
