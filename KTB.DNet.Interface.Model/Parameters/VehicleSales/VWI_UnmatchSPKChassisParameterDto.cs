#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_UnmatchSPKChassisParameterDto  class
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
    public class VWI_UnmatchSPKChassisParameterDto : IValidatableObject
    {
        public int ID { get; set; }
        public string RegNumber { get; set; }
        public DateTime RevisionDate { get; set; }
        public int RevisionStatusID { get; set; }
        public string RevisionStatus { get; set; }
        public string RevisionType { get; set; }
        public string ChassisNumber { get; set; }
        public int SPKHeaderID { get; set; }
        public string SPKNumber { get; set; }
        public string DealerCode { get; set; }
        public string DealerSPKNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

