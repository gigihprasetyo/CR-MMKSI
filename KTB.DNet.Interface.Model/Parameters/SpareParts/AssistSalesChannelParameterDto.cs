#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistSalesChannelParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class AssistSalesChannelParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [AntiXss]
        public string SalesChannelType { get; set; }
        [AntiXss]
        public string SalesChannelCode { get; set; }
        [AntiXss]
        public string Description { get; set; }
        public int Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

