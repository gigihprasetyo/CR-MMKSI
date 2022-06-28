#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APPaymentParameterDto  class
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
    public class APPaymentParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Owner")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Owner { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string APPaymentNo { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string APReferenceNo { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string APVoucherReferenceNo { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "AppliedToDocument")]
        public Decimal AppliedToDocument { get; set; }

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

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string MethodOfPayment { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "AvailableBalance")]
        public Decimal AvailableBalance { get; set; }

        [DefaultValue(1)]
        [Display(ResourceType = typeof(FieldResource), Name = "State")]
        public int State { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "TotalChangeAmount")]
        public Decimal TotalChangeAmount { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "TotalPaymentAmount")]
        public Decimal TotalPaymentAmount { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "TransactionDate")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime TransactionDate { get; set; }

        [DefaultValue(1)]
        [Display(ResourceType = typeof(FieldResource), Name = "Type")]
        public int Type { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string VendorDescription { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Vendor")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Vendor { get; set; }

        public List<APPaymentDetailParameterDto> APPaymentDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Type < 1) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataMustBeGreaterThanZero, FieldResource.Type))); }
            if (State < 1) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataMustBeGreaterThanZero, FieldResource.State))); }
            if (AvailableBalance < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.AvailableBalance))); }
            if (AppliedToDocument < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.AppliedToDocument))); }
            if (TotalChangeAmount < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.TotalChangeAmount))); }
            if (TotalPaymentAmount < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.TotalPaymentAmount))); }

            return results;
        }
    }
}

