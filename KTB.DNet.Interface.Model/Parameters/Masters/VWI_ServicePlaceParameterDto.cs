#region Summary
// ===========================================================================
// AUTHOR        : Mitrais Team
// PURPOSE       : VWI_ServicePlace Dto class.
// SPECIAL NOTES : 
// ---------------------
// Copyright  2018 
// ---------------------
// $History      : $
// Generated on 28/11/2018 - 11:13:18
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.Model
{
    /// <summary>
    /// VWI_ServicePlace Parameter Dto class
    /// </summary>
    public class VWI_ServicePlaceParameterDto : DtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public string ServicePlaceCode { get; set; }
        public string ServicePlaceDescription { get; set; }
        public int Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

