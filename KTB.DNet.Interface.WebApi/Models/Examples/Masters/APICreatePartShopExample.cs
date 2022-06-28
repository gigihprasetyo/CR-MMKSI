#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreatePartShopExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Swashbuckle.Examples;
#endregion

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreatePartShopExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new
            {
                Address = "Jl. A Yani No. 7",
                CityCode = "JKJKP",
                DealerCode = "0",
                Email = "test@mail.com",
                Fax = "",
                Name = "MADE Motor",
                Phone = "02112345678",
                UpdatedBy = "test user"
            };
        }
    }
}