#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_DMSWorkOrderWSCStatusParameterDto  class
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_DMSWorkOrderWSCStatusParameterDto : DtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string ChassisNumber { get; set; }
        public string WorkOrderNumber { get; set; }
        public string PQRNo { get; set; }
        public int PQRType { get; set; }
        public string PQRTypeText { get; set; }
        public DateTime PQRDate { get; set; }
        public int PQRStatus { get; set; }
        public string PQRStatusText { get; set; }
        public string ClaimType { get; set; }
        public string ClaimNumber { get; set; }
        public string Description { get; set; }
        public string ClaimStatus { get; set; }
        public string WSCStatus { get; set; }
        public string WSCStatusText { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

