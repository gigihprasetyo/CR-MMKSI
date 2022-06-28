#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_WholeSalesPriceParameterDto  class
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
    public class VWI_WholeSalesPriceParameterDto : DtoBase, IValidatableObject
    {
        public string DealerCode { get; set; }
        public string MaterialNumber { get; set; }
        public string MaterialDescription { get; set; }
        public string VechileTypeCode { get; set; }
        public string VechileTypeDesc { get; set; }
        public string VechileColorCode { get; set; }
        public string VechileColorName { get; set; }
        public DateTime ValidFrom { get; set; }
        public Decimal BasePrice { get; set; }
        public Decimal OptionPrice { get; set; }
        public Decimal PPN_BM { get; set; }
        public Decimal PPN { get; set; }
        public Decimal PPh22 { get; set; }
        public Decimal PPh23 { get; set; }
        public Decimal FactoringInt { get; set; }
        public Decimal DiscountReward { get; set; }
        public int Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

