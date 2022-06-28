#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APISparePartSalesOrderExample class
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

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APISparePartSalesOrderExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                SalesChannel = 0,
                Owner = "Owner",
                Status = 0,
                DealerCode = "0",
                Customer = "Customer",
                CustomerNo = "CustomerNo",
                DownPaymentAmount = 0,
                DownPaymentAmountReceived = 0,
                DownPaymentIsPaid = true,
                ExternalReferenceNo = "ExternalReferenceNo",
                GrandTotal = 0,
                Handling = 1,
                MethodOfPayment = "MethodOfPayment",
                OrderType = "OrderType",
                SalesOrderNo = "SalesOrderNo",
                SalesPerson = "SalesPerson",
                ShipmentType = "Back Order Allowed",
                State = "Open",
                TermOfPayment = "TermOfPayment",
                TotalAmountBeforeDiscount = 0,
                TotalBaseAmount = 0,
                TotalConsumptionTaxAmount = 0,
                TotalDiscountAmount = 0,
                TotalReceipt = 0,
                TransactionDate = DateTime.Now,
                SparePartSalesOrderDetails = new List<object>{
                    new {
                		  UpdatedBy = "DealerUser",
                          Owner = "Owner",
                          Status = 0,
                          AmountBeforeDiscount = 0,
                          BaseAmount = 0,
                          KodeDealer= "0",
                          ConsumptionTax1Amount = 0,
                          ConsumptionTax1 = "ConsumptionTax1",
                          ConsumptionTax2Amount = 0,
                          ConsumptionTax2 = "ConsumptionTax2",
                          DiscountAmount = 0,
                          DiscountPercentAge = 0,
                          ProductCrossReference = "ProductCrossReference",
                          ProductDescription = "ProductDescription",
                          Product = "Product",
                          PromiseDate = DateTime.Now,
                          QtyDelivered = 100,
                          QtyOrder = 200,
                          RequestDate = DateTime.Now,
                          SalesOrderDetailID = "SalesOrderDetailID",
                          SalesOrderNo = "SalesOrderNo",
                          SalesUnit = "SalesUnit",
                          Site = "Site",
                          TotalAmount = 0,
                          TotalConsumptionTaxAmount = 0,
                          TransactionAmount = 0,
                          UnitPrice = 0,
                          Warehouse = "Warehouse"
                    }
                }
            };
            return obj;
        }
    }
}