#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : TrTraineeParameterDto  class
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
    public class TrTraineeParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SalesmanCode")]
        [AntiXss]
        public string SalesmanCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "NamaSiswa")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //[AntiXss]
        public string Name { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerBranchCode")]
        [AntiXss]
        public string DealerBranchCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "BirthDate")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime BirthDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Gender")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public byte Gender { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "NOKTP")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(16, MinimumLength = 16, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgKTPLengthInvalid")]
        [AntiXss]
        public string NoKTP { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "EMAIL")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string Email { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "StartWorkingDate")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime StartWorkingDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "StatusMechanic")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public short Status { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "JobPosition")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string JobPosition { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "EducationLevel")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string EducationLevel { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ShirtSize")]
        [AntiXss]
        public string ShirtSize { get; set; }

        public AttachmentParameterDto PhotoFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Return if any errors
            if (results.Count > 0) return results;

            if (Utils.IsDefaultString(Name)) { results.Add(new ValidationResult(ErrorCode.DataRequiredField + "#" + string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.NamaSiswa))); }
            //if (!Utils.IsAlphanumericIncludeSpace(Name)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.NamaSiswa))); }
            if (!Utils.IsAlphaNumeric(DealerCode)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.DealerCode))); }
            if (!Utils.IsValidJobPosition(JobPosition)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.JobPosition))); }
            if (!Utils.IsAlphanumericIncludeSpace(EducationLevel)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.EducationLevel))); }

            // validate birthdate
            if (!Utils.IsBirthDateValid(BirthDate, true)) { results.Add(new ValidationResult(string.Format(ValidationResource.ValidationBirthDate))); }

            if(!string.IsNullOrEmpty(Email))
            {
                Email = Email.Trim(' ');
                if (!Utils.IsEmailValid(Email)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.EMAIL))); }
            }

            return results;
        }
    }

    // for sweager purpose
    public class TrTraineeUpdateParameterDto : TrTraineeParameterDto
    {
    }
}
