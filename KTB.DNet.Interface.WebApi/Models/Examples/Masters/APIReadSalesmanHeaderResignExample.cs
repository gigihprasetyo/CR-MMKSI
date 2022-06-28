#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIReadSalesmanHeaderResignExample class
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

namespace KTB.DNet.Interface.WebApi.Models
{
    /// <summary></summary>
    /// <seealso cref="Swashbuckle.Examples.IExamplesProvider" />
    public class APIReadSalesmanHeaderResignExample : IExamplesProvider
    {
        /// <summary>Gets the examples.</summary>
        /// <returns></returns>
        public object GetExamples()
        {
            var obj = new
            {
                SalesmanCode = "S-1231231",
                NoKTP = "3173052810910008"
            };

            return obj;
        }
    }
}