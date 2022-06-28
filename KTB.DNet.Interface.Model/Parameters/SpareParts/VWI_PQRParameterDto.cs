#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_PQRParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021/06/29
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion


namespace KTB.DNet.Interface.Model
{
    public class VWI_PQRParameterDto : DtoBase, IValidatableObject
    {
        public string PQRNo { get; set; }
        public string DealerID { get; set; }
        public string PQRType { get; set; }
        public DateTime DocumentDate { get; set; }
        public string ChassisNumber { get; set; }
        public string Subject { get; set; }
        public string PartNumber { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            return results;
        }
    }
}
