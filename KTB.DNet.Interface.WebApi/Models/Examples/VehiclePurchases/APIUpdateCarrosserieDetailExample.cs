#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateCarrosserieDetailExample class
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

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateCarrosserieDetailExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 1,
                PDIStateCode = 1,
                PDIStateName = "State Name Test 2",
                PDIStatusCode = 1,
                PDIStatusName = "Status Name Test",
                AccessorriesDescription = "Desc Test",
                AccessorriesID = 1,
                AccessorriesName = "Acc Name",
                BUID = 1,
                BUName = "BU Name",
                KITID = 1,
                KITName = "KITName",
                PBUID = 1,
                PBUName = "PBUName",
                PDIDetailID = 1,
                PDIDetailName = "PDIDetailName",
                PDIReceiptDetailID = 1,
                PDIReceiptDetailNO = "PDIReceiptDetailNO",
                PDIReceiptNO = "23232",
                PDIReceiptName = "PDIReceiptName",
                ReceiveQuantity = 1
            };

            return obj;
        }
    }
}