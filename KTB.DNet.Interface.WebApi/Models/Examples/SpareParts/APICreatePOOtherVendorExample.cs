#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreatePOOtherVendorExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;
using System;
using System.Collections;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreatePOOtherVendorExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var poovdetail = new
            {
                UpdatedBy = "DealerUser",
                DealerCode = "100001",
                BaseQtyOrder = 2,
                BaseQtyReceipt = 2,
                BaseQtyReturn = 0,
                CloseLine = false,
                CloseReason = "",
                Completed = false,
                ConsumptionTax1 = "",
                ConsumptionTax1Amount = 0,
                ConsumptionTax2 = "",
                ConsumptionTax2Amount = 0,
                Department = "DEPT-1",
                Description = "",
                DiscountAmount = 0,
                DiscountPercentage = 0,
                EventData = "",
                FormSource = 1,
                InventoryUnit = "UNIT-1",
                Owner = "Me",
                Product = "My Product",
                ProductCrossReference = "CR-Ref",
                ProductDescription = "",
                ProductSubstitute = "",
                ProductVariant = "V-1",
                ProductVolume = 10,
                ProductWeight = 5,
                PromisedDate = DateTime.Now,
                PurchaseFor = 2,
                PurchaseOrderNo = "PONO-1",
                PurchaseRequisitionDetail = "",
                PurchaseUnit = "UNIT-1",
                QtyOrder = 2,
                QtyReceipt = 2,
                QtyReturn = 0,
                RecallProduct = false,
                ReferenceNo = "",
                RequiredDate = DateTime.Now,
                SalesOrderDetail = "",
                ScheduledShippingDate = DateTime.Now,
                ServicePartsAndMaterial = "Parts-1",
                ShippingDate = DateTime.Now,
                Site = "",
                StockNumber = "SN-1",
                TitleRegistrationFee = 100,
                TotalAmount = 10000,
                TotalAmountBeforeDiscount = 11000,
                TotalBaseAmount = 10000,
                TotalConsumptionTaxAmount = 100,
                TotalVolume = 1000,
                TotalWeight = 10,
                TransactionAmount = 10000,
                UnitCost = 1000,
                Warehouse = "WH-1"
            };
            var poovdetails = new ArrayList();
            poovdetails.Add(poovdetail);

            var obj = new
            {
                UpdatedBy = "DealerUser",
                Address1 = "Jl Meranti Raya No.2",
                Address2 = "Sleman",
                Address3 = "Jogja",
                AllocationPeriod = "",
                Balance = 10000,
                City = "Jogja",
                CloseRespon = "",
                Country = "Indonesia",
                DealerCode = "100001",
                DeliveryMethod = 1,
                Description = "",
                DownPayment = 1000,
                DownPaymentAmountPaid = 900,
                DownPaymentIsPaid = false,
                EventDate = "",
                ExternalDocNo = "",
                FormSource = 1,
                GrandTotal = 10000,
                Owner = "Owner",
                PaymentGroup = 1,
                PersonInCharge = "Me",
                POOtherVendorDetails = poovdetails,
                PostalCode = "10678",
                Priority = 1,
                Province = "DIY",
                PRPOType = "",
                PurchaseOrderDate = DateTime.Now,
                PurchaseOrderNo = "PONO-1",
                Site = "",
                SONo = "SONO-1",
                State = 1,
                StockReferenceNo = "",
                Taxable = 1,
                TermsOfPayment = "",
                TotalAmountBeforeDiscount = 11000,
                TotalBaseAmount = 10000,
                TotalConsumptionTaxAmount = 1000,
                TotalDiscountAmount = 0,
                TotalTitleRegistrationFee = 500,
                Vendor = "You",
                VendorDescription = "",
                Warehouse = "WH-1",
                WONo = "WONO-1"
            };
            return obj;
        }
    }
}