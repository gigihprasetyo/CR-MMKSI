#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : POOtherVendorDetailParameterDto  class
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
    public class POOtherVendorDetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public int POOtherVendorID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string Owner { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Boolean CloseLine { get; set; }
        [AntiXss]
        public string CloseReason { get; set; }
        public Boolean Completed { get; set; }
        public Decimal ConsumptionTax1Amount { get; set; }
        [AntiXss]
        public string ConsumptionTax1 { get; set; }
        public Decimal ConsumptionTax2Amount { get; set; }
        [AntiXss]
        public string ConsumptionTax2 { get; set; }
        [AntiXss]
        public string Department { get; set; }
        [AntiXss]
        public string Description { get; set; }
        public Decimal DiscountAmount { get; set; }
        public Double DiscountPercentage { get; set; }
        [AntiXss]
        public string EventData { get; set; }
        public int FormSource { get; set; }
        public Double BaseQtyOrder { get; set; }
        public Double BaseQtyReceipt { get; set; }
        public Double BaseQtyReturn { get; set; }
        [AntiXss]
        public string InventoryUnit { get; set; }
        [AntiXss]
        public string ProductCrossReference { get; set; }
        [AntiXss]
        public string ProductDescription { get; set; }
        [AntiXss]
        public string Product { get; set; }
        [AntiXss]
        public string ProductSubstitute { get; set; }
        [AntiXss]
        public string ProductVariant { get; set; }
        public Double ProductVolume { get; set; }
        public Double ProductWeight { get; set; }
        public DateTime PromisedDate { get; set; }
        public int PurchaseFor { get; set; }
        [AntiXss]
        public string PurchaseOrderNo { get; set; }
        [AntiXss]
        public string PurchaseRequisitionDetail { get; set; }
        [AntiXss]
        public string PurchaseUnit { get; set; }
        public Double QtyOrder { get; set; }
        public Double QtyReceipt { get; set; }
        public Double QtyReturn { get; set; }
        public Boolean RecallProduct { get; set; }
        [AntiXss]
        public string ReferenceNo { get; set; }
        public DateTime RequiredDate { get; set; }
        [AntiXss]
        public string SalesOrderDetail { get; set; }
        public DateTime ScheduledShippingDate { get; set; }
        [AntiXss]
        public string ServicePartsAndMaterial { get; set; }
        public DateTime ShippingDate { get; set; }
        [AntiXss]
        public string Site { get; set; }
        [AntiXss]
        public string StockNumber { get; set; }
        public Decimal TitleRegistrationFee { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal TotalAmountBeforeDiscount { get; set; }
        public Decimal TotalBaseAmount { get; set; }
        public Decimal TotalConsumptionTaxAmount { get; set; }
        public Double TotalVolume { get; set; }
        public Double TotalWeight { get; set; }
        public Decimal TransactionAmount { get; set; }
        public Decimal UnitCost { get; set; }
        [AntiXss]
        public string Warehouse { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (ConsumptionTax1Amount < 0)
            {
                results.Add(new ValidationResult(FieldResource.ConsumptionTax1Amount + string.Format(MessageResource.ErrorMsgQuantity, ConsumptionTax1Amount)));
            }
            if (ConsumptionTax2Amount < 0)
            {
                results.Add(new ValidationResult(FieldResource.ConsumptionTax1Amount + string.Format(MessageResource.ErrorMsgQuantity, ConsumptionTax2Amount)));
            }
            if (DiscountPercentage < 0)
            {
                results.Add(new ValidationResult(FieldResource.DiscountPercentage + string.Format(MessageResource.ErrorMsgQuantity, DiscountPercentage)));
            }
            if (BaseQtyOrder < 0)
            {
                results.Add(new ValidationResult(FieldResource.BaseQtyOrder + string.Format(MessageResource.ErrorMsgQuantity, BaseQtyOrder)));
            }
            if (BaseQtyReceipt < 0)
            {
                results.Add(new ValidationResult(FieldResource.BaseQtyReceipt + string.Format(MessageResource.ErrorMsgQuantity, BaseQtyReceipt)));
            }
            if (QtyReturn < 0)
            {
                results.Add(new ValidationResult(FieldResource.QtyReturn + string.Format(MessageResource.ErrorMsgQuantity, QtyReturn)));
            }
            if (TitleRegistrationFee < 0)
            {
                results.Add(new ValidationResult(FieldResource.TitleRegistrationFee + string.Format(MessageResource.ErrorMsgQuantity, TitleRegistrationFee)));
            }
            if (TotalAmount < 0)
            {
                results.Add(new ValidationResult(FieldResource.TotalAmount + string.Format(MessageResource.ErrorMsgQuantity, TotalAmount)));
            }
            if (TotalAmountBeforeDiscount < 0)
            {
                results.Add(new ValidationResult(FieldResource.TotalAmountBeforeDiscount + string.Format(MessageResource.ErrorMsgQuantity, TotalAmountBeforeDiscount)));
            }
            if (TotalBaseAmount < 0)
            {
                results.Add(new ValidationResult(FieldResource.TotalBaseAmount + string.Format(MessageResource.ErrorMsgQuantity, TotalBaseAmount)));
            }
            if (TotalConsumptionTaxAmount < 0)
            {
                results.Add(new ValidationResult(FieldResource.TotalConsumptionTaxAmount + string.Format(MessageResource.ErrorMsgQuantity, TotalConsumptionTaxAmount)));
            }
            if (TotalVolume < 0)
            {
                results.Add(new ValidationResult(FieldResource.TotalVolume + string.Format(MessageResource.ErrorMsgQuantity, TotalVolume)));
            }
            if (TotalWeight < 0)
            {
                results.Add(new ValidationResult(FieldResource.TotalWeight + string.Format(MessageResource.ErrorMsgQuantity, TotalWeight)));
            }
            if (TransactionAmount < 0)
            {
                results.Add(new ValidationResult(FieldResource.TransactionAmount + string.Format(MessageResource.ErrorMsgQuantity, TransactionAmount)));
            }
            if (UnitCost < 0)
            {
                results.Add(new ValidationResult(FieldResource.UnitCost + string.Format(MessageResource.ErrorMsgQuantity, UnitCost)));
            }
            return results;
        }
    }
}

