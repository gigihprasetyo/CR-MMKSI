#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIAPPaymentExample class
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
    public class APIAPPaymentExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                Owner = "test owner",
                APPaymentNo = "test APPaymentNo",
                APReferenceNo = "test APReferenceNo",
                APVoucherReferenceNo = "test APVoucherReferenceNo",
                AppliedToDocument = 1.5,
                BU = "test BU",
                Cancelled = false,
                CashAndBank = "test CashAndBank",
                MethodOfPayment = "test MethodOfPayment",
                AvailableBalance = 20.5,
                State = 1,
                TotalChangeAmount = 21.5,
                TotalPaymentAmount = 22.5,
                TransactionDate = DateTime.Now,
                Type = 1,
                VendorDescription = "test VendorDescription",
                Vendor = "test Vendor",
                APPaymentDetails = new List<object>{
                    new {
                		UpdatedBy = "DealerUser",
                        Owner = "test owner",
                        APPaymentDetailNo = "test APPaymentDetailNo",
                        APPaymentNo = "test APPaymentNo",
                        BU = "test BU",
                        ChangeAmount = 11.6,
                        Description = "test Description",
                        DifferenceValue = 12.7,
                        ExternalDocumentNo = "test ExternalDocumentNo",
                        ExternalDocumentType = 0,
                        APVoucherNo = "test APVoucherNo",
                        OrderDate = DateTime.Now,
                        OrderNoNVSOReferral = "test OrderNoNVSOReferral",
                        OrderNoOutsourceWorkOrder = "test OrderNoOutsourceWorkOrder",
                        OrderNo = "test OrderNo",
                        OrderNoUVSOReferral = "test OrderNoUVSOReferral",
                        OutstandingBalance = 22.6,
                        PaymentAmount = 23.7,
                        PaymentSlipNo = "test PaymentSlipNo",
                        ReceiptFromVendor =  true,
                        RemainingBalance= 33.3,
                        SourceType = 0,
                        TransactionDocument = "test TransactionDocument",
                        Vendor = "test Vendor"
                    }
                }
            };
            return obj;
        }
    }
}