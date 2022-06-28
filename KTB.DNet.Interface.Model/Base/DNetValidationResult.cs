#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DNetValidationResult  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 7/11/2018 14:05
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class DNetValidationResult : ValidationResult
    {
        public DNetValidationResult(string errorMessage)
            : base(errorMessage)
        {
            ErrorCode = ErrorCode.DataReferenceNotMatch;
        }

        public DNetValidationResult(ErrorCode errorCode, string errorMessage)
            : base(errorMessage)
        {
            ErrorCode = errorCode;
        }

        public ErrorCode ErrorCode { get; set; }
    }
}
