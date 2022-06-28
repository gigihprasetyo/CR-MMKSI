#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PDIParameterDto  class
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
    public class PDIParameterDto : ParameterDtoBase, IValidatableObject
    {
        private const string DATE_FORMAT = "ddMMyyyy";

        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ChassisNumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ChassisNumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Kind")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(1, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Kind { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PDIStatus")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string PDIStatus { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PDIDate")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "ddMMyyyy")]
        public DateTime PDIDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ReleaseBy")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ReleaseBy { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "WorkOrderNumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string WorkOrderNumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ReleaseDate")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "ddMMyyyy")]
        public DateTime ReleaseDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerBranchCode")]
        [AntiXss]
        public string DealerBranchCode { get; set; }

        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Utils.IsAlphaNumeric(ChassisNumber)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, ChassisNumber))); }

            // Validate PDIDate cannot greather than today date            
            if (PDIDate.Date > DateTime.Now.Date)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDateCompareError, FieldResource.PDI, ValidationResource.GreaterThan, FieldResource.TodayDate)));
            }

            return results;
        }
    }
}

