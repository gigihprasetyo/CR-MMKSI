#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartMasterParameterDto  class
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
    public class SparePartMasterParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public int ProductCategoryID { get; set; }
        [AntiXss]
        public string PartNumber { get; set; }
        [AntiXss]
        public string PartName { get; set; }
        [AntiXss]
        public string AltPartNumber { get; set; }
        [AntiXss]
        public string AltPartName { get; set; }
        [AntiXss]
        public string PartCode { get; set; }
        [AntiXss]
        public string ProductType { get; set; }
        [AntiXss]
        public string ModelCode { get; set; }
        [AntiXss]
        public string SupplierCode { get; set; }
        [AntiXss]
        public string TypeCode { get; set; }
        public int Stock { get; set; }
        public Decimal RetalPrice { get; set; }
        [AntiXss]
        public string PartStatus { get; set; }
        public int Status { get; set; }
        public int AccessoriesType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

