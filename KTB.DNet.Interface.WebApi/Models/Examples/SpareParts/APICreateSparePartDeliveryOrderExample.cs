#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateSparePartDeliveryOrderExample class
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

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateSparePartDeliveryOrderExample : IExamplesProvider
    {
        public object GetExamples()
        {

            var detail = new // SparePartDeliveryOrderDetailParameterDto
            {
                Owner = "string",
                AmountBeforeDiscount = 0,
                BaseAmount = 0,
                BaseQtyDelivered = 0,
                BaseQtyOrder = 0,
                BatchNo = "string",
                BU = "string",
                ConsumptionTax1Amount = 0,
                ConsumptionTax1 = "string",
                ConsumptionTax2Amount = 0,
                ConsumptionTax2 = "string",
                DeliveryOrderDetail = "string",
                DeliveryOrderNo = "string",
                DiscountAmount = 0,
                DiscountBaseAmount = 0,
                DiscountPercentage = 0,
                Location = "string",
                ProductCrossReference = "string",
                ProductDescription = "string",
                Product = "string",
                PromiseDate = DateTime.Now,
                QtyDelivered = 0,
                QtyOrder = 0,
                RequestDate = DateTime.Now,
                RunningNumber = 0,
                SalesOrderDetail = "string",
                SalesUnit = "string",
                Site = "string",
                TotalAmount = 0,
                TotalConsumptionTaxAmount = 0,
                TransactionAmount = 0,
                UnitPrice = 0,
                Warehouse = "string",
                UpdatedBy = "DealerUser"
            };

            object[] detailList = new object[1];
            detailList[0] = detail;

            var obj = new
            {
                UpdatedBy = "DealerUser",
                Owner = "Triyo Ifanto",
                Address1 = "Malang",
                Address2 = "Jawa Timur",
                Address3 = "Address 3",
                Address4 = "Address 4",
                BusinessPhone = "0893489389573",
                BU = "string",
                CancellationDate = DateTime.Now,
                City = "Yogyakarta",
                CustomerContacts = "0893494883333",
                Customer = "Customer 1",
                CustomerNo = "M20383",
                DeliveryAddress = "Sleman, Yogyakarta",
                DeliveryOrderNo = "204",
                DeliveryType = 1,
                ExternalReferenceNo = "597973",
                GrandTotal = 4,
                Status = 1,
                MethodofPayment = "Cash",
                OrderType = "Direct",
                ReferenceNo = "3935424",
                Salesperson = "Salesman2",
                State = 1,
                TermofPayment = "09934",
                TotalAmountBeforeDiscount = 1000000,
                TotalBaseAmount = 203920,
                TotalDiscountAmount = 34230,
                TotalMiscChargeBaseAmount = 230000,
                TotalMiscChargeConsumptionTaxAmount = 2320,
                TotalReceipt = 87840,
                TotalConsumptionTaxAmount = 3930,
                TransactionDate = DateTime.Now,
                SparePartDeliveryOrderDetails = detailList
            };

            return obj;
        }
    }
}