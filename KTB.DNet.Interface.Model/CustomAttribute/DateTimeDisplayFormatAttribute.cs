#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DateTimeDisplayFormatAttribute  class
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
    public class DateTimeDisplayFormatAttribute : DisplayFormatAttribute
    {
        public DateTimeDisplayFormatAttribute()
        {
            ApplyFormatInEditMode = true;
            DataFormatString = "{0:u}";
        }
    }
}
