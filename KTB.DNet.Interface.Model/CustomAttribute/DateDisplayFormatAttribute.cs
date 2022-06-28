#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DateDisplayFormatAttribute  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model.CustomAttribute
{
    public class DateDisplayFormatAttribute : DisplayFormatAttribute
    {
        public DateDisplayFormatAttribute()
        {
            ApplyFormatInEditMode = true;
            DataFormatString = "{0:yyyy-MM-dd}";
        }
    }
}
