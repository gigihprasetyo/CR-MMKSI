#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateCarrosserieHeaderExample class
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
    public class APIUpdateCarrosserieHeaderExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 1,
                PDIStateCode = 1,
                PDIStateName = "State Name Test",
                PDIStatusCode = 1,
                PDIStatusName = "Status Name Test",
                BUID = 1,
                BUName = "BU Name Test",
                PDINO = 1,
                PDIName = "PDI Name",
                PDIReceiptNO = "PDI Receipt No",
                PDIReceiptRef = 1,
                PDIReceiptRefName = "ReceiptRef Name",
                PDIReceiptStatus = 1,
                PDIReceiptStatusName = "Receipt Status Name",
                TransactionDate = DateTime.Now,
                TransactionType = 1,
                VendorID = 1,
                VendorName = "Vendor Name",
                ChassisNumber = "CN9823923928"
            };
            return obj;
        }
    }
}