#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIVehiclePurchaseExample class
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
    /// <summary></summary>
    /// <seealso cref="Swashbuckle.Examples.IExamplesProvider" />
    public class APIChassisMasterATAUpdateExample : IExamplesProvider
    {
        /// <summary>Gets the examples.</summary>
        /// <returns></returns>
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ChassisNumber = "MK2L0PU39KJ020741",
                ATA = DateTime.Now,
                RemarkATA ="Boleh diisi apa saja"
            };
            return obj;
        }
    }
}