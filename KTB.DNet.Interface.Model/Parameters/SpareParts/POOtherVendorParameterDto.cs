#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : POOtherVendorParameterDto  class
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
    public class POOtherVendorParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        [AntiXss]
        public string Owner { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string Address1 { get; set; }
        [AntiXss]
        public string Address2 { get; set; }
        [AntiXss]
        public string Address3 { get; set; }
        [AntiXss]
        public string AllocationPeriod { get; set; }
        public Decimal Balance { get; set; }
        [AntiXss]
        public string City { get; set; }
        [AntiXss]
        public string CloseRespon { get; set; }
        [AntiXss]
        public string Country { get; set; }
        public int DeliveryMethod { get; set; }
        [AntiXss]
        public string Description { get; set; }
        public Decimal DownPayment { get; set; }
        public Decimal DownPaymentAmountPaid { get; set; }
        public Boolean DownPaymentIsPaid { get; set; }
        [AntiXss]
        public string EventDate { get; set; }
        [AntiXss]
        public string ExternalDocNo { get; set; }
        public int FormSource { get; set; }
        public Decimal GrandTotal { get; set; }
        public int PaymentGroup { get; set; }
        [AntiXss]
        public string PersonInCharge { get; set; }
        [AntiXss]
        public string PostalCode { get; set; }
        public int Priority { get; set; }
        [AntiXss]
        public string Province { get; set; }
        [AntiXss]
        public string PRPOType { get; set; }
        [AntiXss]
        public string PurchaseOrderNo { get; set; }
        [AntiXss]
        public string SONo { get; set; }
        [AntiXss]
        public string Site { get; set; }
        public int State { get; set; }
        [AntiXss]
        public string StockReferenceNo { get; set; }
        public int Taxable { get; set; }
        [AntiXss]
        public string TermsOfPayment { get; set; }
        public Decimal TotalAmountBeforeDiscount { get; set; }
        public Decimal TotalBaseAmount { get; set; }
        public Decimal TotalConsumptionTaxAmount { get; set; }
        public Decimal TotalDiscountAmount { get; set; }
        public Decimal TotalTitleRegistrationFee { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        [AntiXss]
        public string VendorDescription { get; set; }
        [AntiXss]
        public string Vendor { get; set; }
        [AntiXss]
        public string Warehouse { get; set; }
        [AntiXss]
        public string WONo { get; set; }

        public List<POOtherVendorDetailParameterDto> POOtherVendorDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (DownPayment < 0)
            {
                results.Add(new ValidationResult(FieldResource.DownPayment + string.Format(MessageResource.ErrorMsgQuantity, DownPayment)));
            }
            if (DownPaymentAmountPaid < 0)
            {
                results.Add(new ValidationResult(FieldResource.DownPaymentAmountPaid + string.Format(MessageResource.ErrorMsgQuantity, DownPaymentAmountPaid)));
            }
            if (GrandTotal < 0)
            {
                results.Add(new ValidationResult(FieldResource.GrandTotal + string.Format(MessageResource.ErrorMsgQuantity, GrandTotal)));
            }
            if (TotalAmountBeforeDiscount < 0)
            {
                results.Add(new ValidationResult(FieldResource.TotalAmountBeforeDiscount + string.Format(MessageResource.ErrorMsgQuantity, TotalAmountBeforeDiscount)));
            }
            if (TotalConsumptionTaxAmount < 0)
            {
                results.Add(new ValidationResult(FieldResource.TotalConsumptionTaxAmount + string.Format(MessageResource.ErrorMsgQuantity, TotalConsumptionTaxAmount)));
            }
            if (TotalDiscountAmount < 0)
            {
                results.Add(new ValidationResult(FieldResource.TotalDiscountAmount + string.Format(MessageResource.ErrorMsgQuantity, TotalDiscountAmount)));
            }
            if (TotalTitleRegistrationFee < 0)
            {
                results.Add(new ValidationResult(FieldResource.TotalTitleRegistrationFee + string.Format(MessageResource.ErrorMsgQuantity, TotalTitleRegistrationFee)));
            }
            return results;
        }
    }
}

