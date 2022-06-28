#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PurchaseReturnParameterDto  class
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
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_PurchaseReturnParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public string ClaimNo { get; set; }
        public DateTime ClaimDate { get; set; }
        public string DONumberRef { get; set; }
        public string BillingNumber { get; set; }
        public string SONumber { get; set; }
        public string DONumber { get; set; }
        public string FakturRetur { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
        public Decimal NetPrice { get; set; }
        public Decimal TotalPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
                       
            return results;
        }
    }
}

