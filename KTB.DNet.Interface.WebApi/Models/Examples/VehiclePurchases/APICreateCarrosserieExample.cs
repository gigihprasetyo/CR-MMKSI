#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateCarrosserieExample class
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
    public class APICreateCarrosserieExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                PDIStateCode = 1,
                PDIStatusCode = 1,
                BUCode = "BUCode",
                BUName = "BU Name Test",
                PDIName = "PDI Name",
                PDIReceiptNo = "PDI Receipt No",
                PDIReceiptRefName = "ReceiptRef Name",
                PDIReceiptStatus = 1,
                TransactionDate = DateTime.Now.ToString("yyyy-MM-dd"),
                TransactionType = 1,
                VendorName = "Vendor Name",
                ChassisNumber = "CN9823923928",
                CarrosserieDetails = new List<object>() { 
                    new {
                		    UpdatedBy = "DealerUser",
                            PDIStateCode = 1,
                            PDIStatusCode = 1,
                            AccessorriesDescription = "desc",
                            AccessorriesName = "name",
                            BUCode = "BUCode",
                            BUName = "test",
                            KITName = "test",
                            PBUCode = "test",
                            PBUName = "test",
                            PDIDetailName = "test",
                            PDIReceiptDetailNo = "test",
                            PDIReceiptName = "test",
                            ReceiveQuantity = 1
                    }
                }
            };
            return obj;
        }
    }
}