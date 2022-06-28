#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ARReceiptDetailParameterDto  class
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ARReceiptDetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        public int ARReceiptID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Owner")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Owner { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DetailNo")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DetailNo { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ARReceiptNo")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ARReceiptNo { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "BU")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string BU { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "ChangeAmount")]
        public Decimal ChangeAmount { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Customer { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Description { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "DifferenceValue")]
        public double DifferenceValue { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string InvoiceNo { get; set; }

        public DateTime OrderDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "OrderNo")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string OrderNo { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string OrderNoSO { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string OrderNoUVSO { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string OrderNoWO { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "OutstandingBalance")]
        public Decimal OutstandingBalance { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PaidBackToCustomer")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Boolean PaidBackToCustomer { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "ReceiptAmount")]
        public Decimal ReceiptAmount { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "RemainingBalance")]
        public Decimal RemainingBalance { get; set; }

        [DefaultValue(1)]
        [Display(ResourceType = typeof(FieldResource), Name = "SourceType")]
        public int SourceType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string TransactionDocument { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (ID != Constants.NUMBER_DEFAULT_VALUE)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgCreateID, ID)));
            }

            if (SourceType < 1) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataMustBeGreaterThanZero, FieldResource.SourceType))); }
            if (ChangeAmount < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgQuantity, FieldResource.ChangeAmount))); }
            if (DifferenceValue < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgQuantity, FieldResource.DifferenceValue))); }
            if (OutstandingBalance < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgQuantity, FieldResource.OutstandingBalance))); }
            if (ReceiptAmount < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgQuantity, FieldResource.ReceiptAmount))); }
            if (RemainingBalance < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgQuantity, FieldResource.RemainingBalance))); }

            return results;
        }
    }
}

