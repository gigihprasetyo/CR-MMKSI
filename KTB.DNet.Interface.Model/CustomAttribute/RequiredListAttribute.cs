#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : RequiredListAttribute  class
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
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RequiredListAttribute : ValidationAttribute
    {
        private const string defaultError = "'{0}' must have at least one element.";
        public RequiredListAttribute()
            : base(defaultError) //
        {
        }

        public override bool IsValid(object value)
        {
            IList list = value as IList;
            return (list != null && list.Count > 0);
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(this.ErrorMessageString, name);
        }
    }

}
