#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateStandardCodeCharExample class
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
    public class APIUpdateStandardCodeCharExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 6,
                Category = "SPPOOrderType.EnumOrderType",
                ValueId = "Y",
                ValueCode = "OtherEmergencyX",
                ValueDesc = "OtherEmergencyX",
                Sequence = 6
            };

            return obj;
        }
    }
}