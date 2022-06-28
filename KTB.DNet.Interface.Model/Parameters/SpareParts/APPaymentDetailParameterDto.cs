#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APPaymentDetailParameterDto  class
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
    public class APPaymentDetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public int APPaymentID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Owner")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Owner { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string APPaymentDetailNo { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string APPaymentNo { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string BU { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "ChangeAmount")]
        public Decimal ChangeAmount { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Description { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "DifferenceValue")]
        public float DifferenceValue { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ExternalDocumentNo { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "ExternalDocumentType")]
        public int ExternalDocumentType { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "APVoucherNo")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string APVoucherNo { get; set; }

        public DateTime OrderDate { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string OrderNoNVSOReferral { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string OrderNoOutsourceWorkOrder { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string OrderNo { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string OrderNoUVSOReferral { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "OutstandingBalance")]
        public Decimal OutstandingBalance { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "PaymentAmount")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Decimal PaymentAmount { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PaymentSlipNo { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ReceiptFromVendor")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Boolean ReceiptFromVendor { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "RemainingBalance")]
        public Decimal RemainingBalance { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "SourceType")]
        public int SourceType { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string TransactionDocument { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Vendor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (ExternalDocumentType < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.ExternalDocumentType))); }
            if (SourceType < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.SourceType))); }
            if (DifferenceValue < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.DifferenceValue))); }
            if (ChangeAmount < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.ChangeAmount))); }
            if (OutstandingBalance < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.OutstandingBalance))); }
            if (PaymentAmount < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.PaymentAmount))); }
            if (RemainingBalance < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.RemainingBalance))); }

            return results;
        }
    }
}

