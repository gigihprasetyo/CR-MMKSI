#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehiclePurchaseDetailParameterDto  class
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
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VehiclePurchaseDetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        [AntiXss]
        public string BUCode { get; set; }
        [AntiXss]
        public string BUName { get; set; }
        public bool CloseLine { get; set; }
        [AntiXss]
        public string CloseLineName { get; set; }
        [AntiXss]
        public string CloseReason { get; set; }
        public bool Completed { get; set; }
        [AntiXss]
        public string CompletedName { get; set; }
        [AntiXss]
        public string ProductDescription { get; set; }
        [AntiXss]
        public string ProductName { get; set; }
        [AntiXss]
        public string ProductVariantName { get; set; }
        [AntiXss]
        public string PODetail { get; set; }
        [AntiXss]
        public string POName { get; set; }
        [AntiXss]
        public string PRDetailName { get; set; }
        [AntiXss]
        public string PurchaseUnitName { get; set; }
        [Display(ResourceType = typeof(FieldResource), Name = "QtyOrder")]
        public double QtyOrder { get; set; }
        [Display(ResourceType = typeof(FieldResource), Name = "QtyReceipt")]
        public double QtyReceipt { get; set; }
        [Display(ResourceType = typeof(FieldResource), Name = "QtyReturn")]
        public double QtyReturn { get; set; }
        public bool RecallProduct { get; set; }
        [AntiXss]
        public string RecallProductName { get; set; }
        [AntiXss]
        public string SODetailName { get; set; }
        public DateTime ScheduledShippingDate { get; set; }
        [AntiXss]
        public string ServicePartsAndMaterial { get; set; }
        public DateTime ShippingDate { get; set; }
        [AntiXss]
        public string Site { get; set; }
        [AntiXss]
        public string StockNumberName { get; set; }

        public VehiclePurchaseHeaderParameterDto VehiclePurchaseHeader { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}

