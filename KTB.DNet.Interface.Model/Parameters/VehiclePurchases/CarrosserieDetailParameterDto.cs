#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CarrosserieDetailParameterDto  class
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CarrosserieDetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public short PDIStateCode { get; set; }
        public short PDIStatusCode { get; set; }
        [AntiXss]
        public string AccessorriesDescription { get; set; }
        [AntiXss]
        public string AccessorriesName { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "BUCode")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string BUCode { get; set; }

        [AntiXss]
        public string BUName { get; set; }
        [AntiXss]
        public string KITName { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PBUCode")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string PBUCode { get; set; }

        [AntiXss]
        public string PBUName { get; set; }
        [AntiXss]
        public string PDIDetailName { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PDIReceiptDetailNo")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string PDIReceiptDetailNo { get; set; }

        [AntiXss]
        public string PDIReceiptName { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ReceiveQuantity")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public double ReceiveQuantity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (ReceiveQuantity < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgQuantity, FieldResource.ReceiveQuantity))); }

            return results;
        }
    }
}

