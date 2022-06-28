#region Summary
// ===========================================================================
// AUTHOR        : Mitrais Team
// PURPOSE       : APPaymentDetail Parameter Dto class.
// SPECIAL NOTES : 
// ---------------------
// Copyright  2018 
// ---------------------
// $History      : $
// Generated on 19/03/2018 - 13:59:44
//
// ===========================================================================	
#endregion

using KTB.DNet.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Embarr.WebAPI.AntiXss;

namespace KTB.DNet.Interface.Model
{
    /// <summary>
    /// APPaymentDetail Parameter Dto class
    /// </summary>
    public class AExperimentModel : DtoBase, IValidatableObject
    {
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgMandatoryField")]
        public int RequiredInteger { get; set; }
        public int OptInteger { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgMandatoryField")]
        [AntiXss]
        public string RequiredString { get; set; }
        [AntiXss]
        public string OptString { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgMandatoryField")]
        public double RequiredDouble { get; set; }
        public double OptDouble { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgMandatoryField")]
        public float RequiredFloat { get; set; }
        public float OptFloat { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgMandatoryField")]
        public bool RequiredBoolean { get; set; }
        public bool OptBoolean { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgMandatoryField")]
        public DateTime RequiredDate { get; set; }
        public DateTime OptDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (RequiredInteger != 0)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgCreateID, RequiredInteger)));
            }

            return results;
        }

    }
}

