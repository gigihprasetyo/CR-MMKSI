#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartSalesOrderDetailParameterDto  class
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
    public class SparePartSalesOrderDetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        public int SparePartSalesOrderID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Owner")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Owner { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SparePartStatus")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int Status { get; set; }

        public Decimal AmountBeforeDiscount { get; set; }

        public Decimal BaseAmount { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string KodeDealer { get; set; }

        public Decimal ConsumptionTax1Amount { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ConsumptionTax1 { get; set; }

        public Decimal ConsumptionTax2Amount { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ConsumptionTax2 { get; set; }

        public Decimal DiscountAmount { get; set; }

        public Decimal DiscountPercentAge { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ProductCrossReference { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ProductDescription { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Product")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Product { get; set; }

        public DateTime PromiseDate { get; set; }

        public double QtyDelivered { get; set; }

        public double QtyOrder { get; set; }

        public DateTime RequestDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SalesOrderDetailID")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string SalesOrderDetailID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SalesOrderNo")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string SalesOrderNo { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string SalesUnit { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Site { get; set; }

        public Decimal TotalAmount { get; set; }

        public Decimal TotalConsumptionTaxAmount { get; set; }

        public Decimal TransactionAmount { get; set; }

        public Decimal UnitPrice { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Warehouse")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Warehouse { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Status < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.SparePartStatus))); }
            if (QtyDelivered < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.QtyDelivered))); }
            if (QtyOrder < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.QtyOrder))); }

            return results;
        }
    }
}

