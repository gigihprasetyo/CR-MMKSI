#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreatePDIExample class
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
    public class APIGetPDFPDIExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                ChassisNumber = "MK2L0PU39HK013747",
                IsEncrypted = false
            };

            return obj;
        }
    }
}