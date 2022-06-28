#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IndentPartDetailParameterDto  class
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class IndentPartDetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "TotalForecast")]
        [DefaultValue(0)]
        public int TotalForecast { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Qty")]
        [DefaultValue(0)]
        public int Qty { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Description { get; set; }

        public int AllocationQty { get; set; }

        public byte IsCompletedAllocation { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Price")]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int IndentPartHeaderID { get; set; }

        public int SparePartMasterID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PartNumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(18, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PartNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }

    public class IndentPartDetailCreateParameterDto : ParameterDtoBase, IValidatableObject
    {
        [DefaultValue(0)]
        public int ID { get; set; }

        [AntiXss]
        public new string UpdatedBy { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "TotalForecast")]
        [DefaultValue(0)]
        public int TotalForecast { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Qty")]
        [DefaultValue(0)]
        public int Qty { get; set; }

        [StringLength(255, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Description { get; set; }

        [DefaultValue(0)]
        public int AllocationQty { get; set; }

        public byte IsCompletedAllocation { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Price")]
        [DefaultValue(0.0)]
        public decimal Price { get; set; }

        public int IndentPartHeaderID { get; set; }

        public int SparePartMasterID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PartNumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(18, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PartNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (ID != 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgCreateID, ID))); }

            if (Qty < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgQuantity, FieldResource.Qty))); }

            if (TotalForecast < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgQuantity, FieldResource.TotalForecast))); }

            return results;
        }
    }
}
