#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKChassisParameterDto  class
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
    public class SPKChassisParameterDto : ParameterDtoBase, IValidatableObject
    {
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [Range(1, 3, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgMatchingTypeMustBeBetweenXandY")]
        public int MatchingType { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int SPKDetailID { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime MatchingDate { get; set; }

        [AntiXss]
        public string MatchingNumber { get; set; }

        [AntiXss]
        public string ReferenceNumber { get; set; }

        [AntiXss]
        public string KeyNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ChassisNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string SPKNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Utils.IsAlphaNumeric(ChassisNumber)) { results.Add(new ValidationResult(FieldResource.ChassisNumber + string.Format(MessageResource.ErrorMsgDataInvalid, ChassisNumber))); }

            return results;
        }
    }
}

