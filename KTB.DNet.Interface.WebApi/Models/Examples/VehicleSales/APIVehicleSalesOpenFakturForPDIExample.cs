#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIVehicleSalesOpenFakturExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models
{
    /// <summary>
    /// Open Faktur API examples request body
    /// </summary>
    public class APIVehicleSalesOpenFakturForPDIExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new VWI_OpenFakturForPDIFilterDto
            {
                pages = 1,
                ChassisNumber = "MK2KRWPNUJJ017060"
            };

            return obj;
        }

    }
}