#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SPKCustomerHaveRequestParameterDto  class
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
    public class VWI_SPKCustomerHaveRequestParameterDto : IValidatableObject
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string DealerBranchCode { get; set; }
        public string SPKNumber { get; set; }
        public string RequestNo { get; set; }
        public string CustomerCode { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
