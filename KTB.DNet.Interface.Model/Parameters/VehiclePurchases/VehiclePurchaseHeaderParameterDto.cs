#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehiclePurchaseHeaderParameterDto  class
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
    public class VehiclePurchaseHeaderParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        [AntiXss]
        public string BUCode { get; set; }
        [AntiXss]
        public string BUName { get; set; }
        [AntiXss]
        public string DeliveryMethod { get; set; }
        [AntiXss]
        public string Description { get; set; }
        [AntiXss]
        public string PRPOTypeName { get; set; }
        [AntiXss]
        public string DMSPONo { get; set; }
        public int DMSPOStatus { get; set; }
        public DateTime DMSPODate { get; set; }
        [AntiXss]
        public string VendorDescription { get; set; }
        [AntiXss]
        public string Vendor { get; set; }
        [AntiXss]
        public string PurchaseOrderNo { get; set; }
        [AntiXss]
        public string PurchaseReceiptNo { get; set; }
        [AntiXss]
        public string PurchaseReceiptDetailNo { get; set; }
        [AntiXss]
        public string ChassisModel { get; set; }
        [AntiXss]
        public string ChassisNumberRegister { get; set; }

        public List<VehiclePurchaseDetailParameterDto> VehiclePurchaseDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class VehiclePurchaseHeaderCreateParameterDto : VehiclePurchaseHeaderParameterDto { }
}

