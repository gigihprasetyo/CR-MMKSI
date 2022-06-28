#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ModelStateHelper.cs class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad Ridwan
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion

using System.Linq;
using System.Web.Http.ModelBinding;

namespace KTB.DNet.Interface.WebUI.Helper
{
    /// </summary>
    /// <summary>
    /// ModelStateHelper
    /// </summary>
    public static class ModelStateHelper
    {
        public static object GetParseableModelState(ModelStateDictionary modelState)
        {
            return modelState.Keys.Where(k => modelState[k].Errors.Count > 0)
                        .Select(k => new { propertyName = k, errorMessage = modelState[k].Errors[0].ErrorMessage });
        }

    }
}
