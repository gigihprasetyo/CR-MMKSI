#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeeServiceParameterDto  class
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
    public class VWI_EmployeeServiceParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [AntiXss]
        public string Name { get; set; }

        [AntiXss]
        public string DealerCode { get; set; }

        public string DealerBranchCode { get; set; }

        public DateTime BirthDate { get; set; }

        public short Gender { get; set; }

        [StringLength(16, MinimumLength = 16, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgKTPLengthInvalid")]
        [AntiXss]
        public string NoKTP { get; set; }

        public string Email { get; set; }

        public DateTime StartWorkingDate { get; set; }

        [AntiXss]
        public string JobPosition { get; set; }

        [AntiXss]
        public string EducationLevel { get; set; }

        [AntiXss]
        public string ShirtSize { get; set; }

        public int Status { get; set; }

        public AttachmentParameterDto PhotoFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            return results;
        }
    }
}
