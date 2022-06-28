#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CarrosserieHeaderParameterDto  class
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
    public class CarrosserieHeaderParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public short PDIStateCode { get; set; }
        public short PDIStatusCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "BUCode")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string BUCode { get; set; }

        [AntiXss]
        public string BUName { get; set; }
        [AntiXss]
        public string PDIName { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PDIReceiptNo")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string PDIReceiptNo { get; set; }

        [AntiXss]
        public string PDIReceiptRefName { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PDIReceiptStatus")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public short PDIReceiptStatus { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "TransactionDate")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime TransactionDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "TransactionType")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public short TransactionType { get; set; }

        [AntiXss]
        public string VendorName { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ChassisNumber")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ChassisNumber { get; set; }

        public List<CarrosserieDetailParameterDto> CarrosserieDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    /// <summary>
    /// Carrosserie Header Create Parameter Dto Class
    /// </summary>
    public class CarrosserieHeaderCreateParameterDto : CarrosserieHeaderParameterDto { }
}

