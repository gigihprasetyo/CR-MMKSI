#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ARReceiptParameterDto.cs class
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    /// <summary>
    /// ARReceipt Parameter Dto class
    /// </summary>
    public class ARReceiptParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Owner")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Owner { get; set; }

        [StringLength(36, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string GeneratedToken { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ARInvoiceReferenceNo { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ARReceiptNo")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ARReceiptNo { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ARReceiptReferenceNo { get; set; }

        [DefaultValue(1)]
        [Display(ResourceType = typeof(FieldResource), Name = "Type")]
        public int Type { get; set; }

        public Boolean BookingFee { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "BU")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string BU { get; set; }

        public Boolean Cancelled { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CashAndBank")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string CashAndBank { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Customer")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Customer { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string CustomerNo { get; set; }

        public DateTime EndOrderDate { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string MethodOfPayment { get; set; }

        [DefaultValue(0)]
        public Decimal AvailableBalance { get; set; }

        public DateTime StartOrderDate { get; set; }

        [DefaultValue(1)]
        public int State { get; set; }

        [DefaultValue(0)]
        public Decimal AppliedToDocument { get; set; }

        [DefaultValue(0)]
        public Decimal TotalAmountBase { get; set; }

        [DefaultValue(0)]
        public Decimal TotalChangeAmount { get; set; }

        [DefaultValue(0)]
        public Decimal TotalOutstandingBalanceBase { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "TotalReceiptAmount")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Decimal TotalReceiptAmount { get; set; }

        [DefaultValue(0)]
        public Decimal TotalRemainingBalanceBase { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "TransactionDate")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime TransactionDate { get; set; }

        public List<ARReceiptDetailParameterDto> ARReceiptDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Type < 1) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataMustBeGreaterThanZero, FieldResource.Type))); }
            if (State < 1) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataMustBeGreaterThanZero, FieldResource.State))); }
            if (AvailableBalance < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.AvailableBalance))); }
            if (AppliedToDocument < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.AppliedToDocument))); }
            if (TotalAmountBase < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.TotalAmountBase))); }
            if (TotalChangeAmount < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.TotalChangeAmount))); }
            if (TotalOutstandingBalanceBase < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.TotalOutstandingBalanceBase))); }
            if (TotalReceiptAmount < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.TotalReceiptAmount))); }
            if (TotalRemainingBalanceBase < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.TotalRemainingBalanceBase))); }

            return results;
        }
    }
}

