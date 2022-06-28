#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIReadQuickProductExample class
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
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIReadQuickProductExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new
            {
                pages = 1,
                find = new List<object>{
                new {
                   MatchType = 0,
                   PropertyName = "VehicleType",
                   PropertyValue = "AA00",
                   SqlOperation = 0
                }
               },
                sort = new List<object>{
                new {
                   SortColumn = "ID",
                   SortDirection = 0
                }
               }
            };
        }
    }
}