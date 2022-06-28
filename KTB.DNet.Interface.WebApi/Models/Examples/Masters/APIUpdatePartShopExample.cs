#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdatePartShopExample class
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
    public class APIUpdatePartShopExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new
            {
                ID = 0,
                Address = "Jl. Teuku Umar No.7",
                DealerCode = "0",
                Email = "test@mail.com",
                Fax = "",
                Name = "MADE Motor 2",
                Phone = "02112345678",
                UpdatedBy = "test user"
            };
        }
    }
}