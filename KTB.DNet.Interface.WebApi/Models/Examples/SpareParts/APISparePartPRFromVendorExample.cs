#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APISparePartPRFromVendorExample class
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
using System.Collections.Generic;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APISparePartPRFromVendorExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                PRNumber = "PR-00001",
                PONumber = "PO-00001",
                Owner = "Test Owner",
                APVoucherNumber = "APV-0001",
                AssignLandedCost = true,
                AutoInvoiced = true,
                DealerCode = "0",
                DeliveryOrderDate = DateTime.Now,
                DeliveryOrderNumber = "WO-1000",
                EventData = "Test Event Data 1",
                EventData2 = "Test Event Data 2",
                GrandTotal = 100.50,
                Handling = 1,
                LoadData = true,
                PackingSlipDate = DateTime.Now,
                PackingSlipNumber = "SLIP-001",
                PRReferenceRequired = true,
                ReturnPRNumber = "RN-0001",
                State = 1,
                TotalBaseAmount = 50.50,
                TotalConsumptionTax1Amount = 5.00,
                TotalConsumptionTax2Amount = 5.00,
                TotalConsumptionTaxAmount = 0,
                TotalTitleRegistrationFree = 0,
                TransactionDate = DateTime.Now,
                TransferOrderRequestingNumber = "OR-0001",
                Type = 1,
                VendorDescription = "Test Vendor Description",
                Vendor = "Test Vendor",
                VendorInvoiceNumber = "Vendor Invoice Number",
                WONumber = "WO-0001",
                SparePartPRDetailFromVendors = new List<object>{
                    new {
                		UpdatedBy = "DealerUser",
                        PRDetailNumber = "PRD-0001",
                        SparePartPRID = 1,
                        PRNumber = "PR-0001",
                        Owner = "Test Owner",
                        BaseReceivedQuantity = 5.00, 
                        BatchNumber = "BACTH-0001",
                        DealerCode = "0",
                        ChassisModel = "Test Chassis Model",
                        ChassisNumberRegister = "CNR-0001",
                        ConsumptionTax1Amount = 0,
                        ConsumptionTax1 = "Tax1",
                        ConsumptionTax2Amount= 0,
                        ConsumptionTax2 = "Tax2",
                        DiscountAmount = 0,
                        EngineNumber = "ER-0001",
                        EventData = "Test Event Data",
                        InventoryUnit = "Test Unit",
                        KeyNumber = "Key-0001",
                        LandedCost = 5.00,
                        Location = "Test Location",
                        ProductDescription = "Test Product Description",
                        Product = "Test Product",
                        ProductVolume = 10.00,
                        ProductWeight = 100.00,
                        PurchaseUnit = "Test Purchase Unit",
                        ReceivedQuantity = 10.00,
                        ReferenceNumber = "Test Reference Number",
                        ReturnPRDetail = "Test Return Detail",
                        ServicePartsAndMaterial = "Test Service Parts",
                        Site = "Test Site",
                        StockNumber = "Test Stock Number",
                        TitleRegistrationFee = 10.00,
                        TotalAmount = 10.00,
                        TotalBaseAmount = 10.00,
                        TotalConsumptionTaxAmount = 10.00,
                        TotalVolume = 10.00,
                        TotalWeight = 10.00,
                        TransactionAmount = 10.00,
                        UnitCost = 10.00,
                        Warehouse = "Test Ware House"
                    }
                }
            };
            return obj;
        }
    }
}