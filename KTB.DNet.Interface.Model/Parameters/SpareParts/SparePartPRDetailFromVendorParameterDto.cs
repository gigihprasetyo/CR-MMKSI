#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPRDetailFromVendorParameterDto  class
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
    public class SparePartPRDetailFromVendorParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [AntiXss]
        public string PRDetailNumber { get; set; }

        public int SparePartPRID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PRNumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PRNumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Owner")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Owner { get; set; }

        public Double BaseReceivedQuantity { get; set; }

        [AntiXss]
        public string BatchNumber { get; set; }

        [AntiXss]
        public string DealerCode { get; set; }

        [AntiXss]
        public string ChassisModel { get; set; }

        [AntiXss]
        public string ChassisNumberRegister { get; set; }

        public Decimal ConsumptionTax1Amount { get; set; }

        [AntiXss]
        public string ConsumptionTax1 { get; set; }

        public Decimal ConsumptionTax2Amount { get; set; }

        [AntiXss]
        public string ConsumptionTax2 { get; set; }

        public Decimal DiscountAmount { get; set; }

        [AntiXss]
        public string EngineNumber { get; set; }

        [AntiXss]
        public string EventData { get; set; }

        [AntiXss]
        public string InventoryUnit { get; set; }

        [AntiXss]
        public string KeyNumber { get; set; }

        public Decimal LandedCost { get; set; }

        [AntiXss]
        public string Location { get; set; }

        [AntiXss]
        public string ProductDescription { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Product")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Product { get; set; }

        public Double ProductVolume { get; set; }

        public Double ProductWeight { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PurchaseUnit")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PurchaseUnit { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ReceivedQuantity")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Double? ReceivedQuantity { get; set; }

        [AntiXss]
        public string ReferenceNumber { get; set; }

        [AntiXss]
        public string ReturnPRDetail { get; set; }

        [AntiXss]
        public string ServicePartsAndMaterial { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Site { get; set; }

        [AntiXss]
        public string StockNumber { get; set; }

        public Decimal TitleRegistrationFee { get; set; }

        public Decimal TotalAmount { get; set; }

        public Decimal TotalBaseAmount { get; set; }

        public Decimal TotalConsumptionTaxAmount { get; set; }

        public Double TotalVolume { get; set; }

        public Double TotalWeight { get; set; }

        public Decimal TransactionAmount { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "UnitCost")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Decimal? UnitCost { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Warehouse")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Warehouse { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!ReceivedQuantity.HasValue) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ReceivedQuantity))); };
            if (!UnitCost.HasValue) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.UnitCost))); };
            if (!ReceivedQuantity.HasValue) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ReceivedQuantity))); };

            // Return if any errors
            if (results.Count > 0) return results;

            return results;
        }
    }
}
