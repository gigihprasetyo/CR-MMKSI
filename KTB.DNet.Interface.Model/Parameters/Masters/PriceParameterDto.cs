#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PriceParameterDto  class
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
    public class PriceParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public int VechileColorID { get; set; }
        public int DealerID { get; set; }
        public DateTime ValidFrom { get; set; }
        public Decimal BasePrice { get; set; }
        public Decimal OptionPrice { get; set; }
        public Decimal PPN_BM { get; set; }
        public Decimal PPN { get; set; }
        public Decimal PPh22 { get; set; }
        public Decimal Interest { get; set; }
        public Decimal FactoringInt { get; set; }
        public Decimal PPh23 { get; set; }
        [AntiXss]
        public string Status { get; set; }
        public Decimal DiscountReward { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

