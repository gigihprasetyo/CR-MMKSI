#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPOParameterDto  class
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
    public class SparePartPOParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "OrderType")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string OrderType { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime PODate { get; set; }
        public DateTime DeliveryDate { get; set; }
        //public string ProcessCode { get; set; }

        public string CancelRequestBy { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DMSPRNo")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DMSPRNo { get; set; }

        [AntiXss]
        public string PQRNo { get; set; }
        public List<SparePartPODetailParameterDto> SparePartPODetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var results = new List<ValidationResult>();
            if (OrderType == "P" && String.IsNullOrEmpty(PQRNo)) { results.Add(new ValidationResult("Wajib mengisikan PQRNo")); }

            return results;
        }
    }

    /// <summary>
    /// Update Spare Part PO Request
    /// </summary>
    public class SparePartPOUpdateParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string PONumber { get; set; }
        [AntiXss]
        public string DMSPRNo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
