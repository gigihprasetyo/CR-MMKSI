#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateCustomerCaseExample class
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
    public class APICreateCustomerCaseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                DealerCode = "100109",
                CaseNumber = "MMKSI_033989",
                ReservationNumber = "RESRVNUM001",
                Description = "Pembeli menanyakan akad kredit",
                Status = 2,
                EvidenceFile = new { FileName = "", Base64OfStream = "" }
            };

            return obj;
        }
    }
}