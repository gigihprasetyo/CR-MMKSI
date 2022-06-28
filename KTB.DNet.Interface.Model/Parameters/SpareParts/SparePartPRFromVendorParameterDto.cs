#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPRFromVendorParameterDto  class
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
    public class SparePartPRFromVendorParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [AntiXss]
        public string PRNumber { get; set; }

        [AntiXss]
        public string PONumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Owner")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Owner { get; set; }

        [AntiXss]
        public string APVoucherNumber { get; set; }

        public Boolean AssignLandedCost { get; set; }

        public Boolean AutoInvoiced { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        public DateTime DeliveryOrderDate { get; set; }

        [AntiXss]
        public string DeliveryOrderNumber { get; set; }

        [AntiXss]
        public string EventData { get; set; }

        [AntiXss]
        public string EventData2 { get; set; }

        [DefaultValue(0)]
        public Decimal GrandTotal { get; set; }

        [DefaultValue(0)]
        public int Handling { get; set; }

        public Boolean LoadData { get; set; }

        public DateTime PackingSlipDate { get; set; }

        [AntiXss]
        public string PackingSlipNumber { get; set; }

        public Boolean PRReferenceRequired { get; set; }

        [AntiXss]
        public string ReturnPRNumber { get; set; }

        public int State { get; set; }

        [DefaultValue(0)]
        public Decimal TotalBaseAmount { get; set; }

        [DefaultValue(0)]
        public Decimal TotalConsumptionTax1Amount { get; set; }

        [DefaultValue(0)]
        public Decimal TotalConsumptionTax2Amount { get; set; }

        [DefaultValue(0)]
        public Decimal TotalConsumptionTaxAmount { get; set; }

        [DefaultValue(0)]
        public Decimal TotalTitleRegistrationFree { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "TransactionDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime TransactionDate { get; set; }

        [AntiXss]
        public string TransferOrderRequestingNumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Type")]
        public int Type { get; set; }

        [AntiXss]
        public string VendorDescription { get; set; }

        [AntiXss]
        public string Vendor { get; set; }

        [AntiXss]
        public string VendorInvoiceNumber { get; set; }

        [AntiXss]
        public string WONumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "UpdateBy")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]


        public List<SparePartPRDetailFromVendorParameterDto> SparePartPRDetailFromVendors { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (TransactionDate == Constants.DATETIME_DEFAULT_VALUE) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.TransactionDate))); }

            // validate enum field            
            if (Type < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.Type))); }

            // Return if any errors
            if (results.Count > 0) return results;

            return results;
        }
    }
}