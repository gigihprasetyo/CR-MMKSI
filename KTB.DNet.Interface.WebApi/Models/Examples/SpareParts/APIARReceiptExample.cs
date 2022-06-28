#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIARReceiptExample class
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
    public class APIARReceiptExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                Owner = "test Owner",
                GeneratedToken = "GeneratedToken",
                ARInvoiceReferenceNo = "ARInvoiceReferenceNo",
                ARReceiptNo = "ARReceiptNo",
                ARReceiptReferenceNo = "ARReceiptReferenceNo",
                Type = 1,
                BookingFee = true,
                BU = "BU",
                Cancelled = true,
                CashAndBank = "CashAndBank",
                Customer = "Customer",
                CustomerNo = "CustomerNo",
                EndOrderDate = DateTime.Now,
                MethodOfPayment = "MethodOfPayment",
                AvailableBalance = 0,
                StartOrderDate = DateTime.Now,
                State = 1,
                AppliedToDocument = 0,
                TotalAmountBase = 0,
                TotalChangeAmount = 0,
                TotalOutstandingBalanceBase = 0,
                TotalReceiptAmount = 0,
                TotalRemainingBalanceBase = 0,
                TransactionDate = DateTime.Now,
                ARReceiptDetails = new List<object>{
                    new {
                	  UpdatedBy = "DealerUser",
                      Owner = "Owner",
                      DetailNo = "DetailNo",
                      ARReceiptNo = "ARReceiptNo",
                      BU = "BU",
                      ChangeAmount = 0,
                      Customer = "Customer",
                      Description = "Description",
                      DifferenceValue = 0,
                      InvoiceNo = "InvoiceNo",
                      OrderDate = DateTime.Now,
                      OrderNo = "OrderNo",
                      OrderNoSO = "OrderNoSO",
                      OrderNoUVSO = "OrderNoUVSO",
                      OrderNoWO = "OrderNoWO",
                      OutstandingBalance = 0,
                      PaidBackToCustomer = true,
                      ReceiptAmount = 0,
                      RemainingBalance = 0,
                      SourceType = 1,
                      TransactionDocument = "TransactionDocument"
                    }
                }
            };
            return obj;
        }
    }
}