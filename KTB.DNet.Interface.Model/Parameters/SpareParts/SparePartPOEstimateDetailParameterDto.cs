#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPOEstimateDetailParameterDto  class
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
    public class SparePartPOEstimateDetailParameterDto : DtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public int SparePartPOEstimateID { get; set; }
        [AntiXss]
        public string PartNumber { get; set; }
        [AntiXss]
        public string PartName { get; set; }
        public int OrderQty { get; set; }
        public int AllocQty { get; set; }
        public int AllocationQty { get; set; }
        public int OpenQty { get; set; }
        public Decimal RetailPrice { get; set; }
        [AntiXss]
        public string AltPartNumber { get; set; }
        public Decimal Discount { get; set; }
        [AntiXss]
        public string ItemStatus { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
