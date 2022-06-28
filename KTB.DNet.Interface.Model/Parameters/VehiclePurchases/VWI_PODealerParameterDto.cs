#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PODealerParameterDto  class
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
    public class VWI_PODealerParameterDto : IValidatableObject
    {
        public int POHeaderId { get; set; }
        public int DealerId { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string DealerName { get; set; }
        public string DestinationDealerCode { get; set; }
        [AntiXss]
        public string PONumber { get; set; }
        [AntiXss]
        public string POType { get; set; }
        public int NumOfInstallment { get; set; }
        public int AllocQty { get; set; }
        public Decimal Price { get; set; }
        public Decimal Discount { get; set; }
        public Decimal Interest { get; set; }
        [AntiXss]
        public string ContractNumber { get; set; }
        [AntiXss]
        public string PKNumber { get; set; }
        [AntiXss]
        public string DealerPKNumber { get; set; }
        [AntiXss]
        public string DealerPONumber { get; set; }
        [AntiXss]
        public string ProjectName { get; set; }
        public int SalesOrderId { get; set; }
        [AntiXss]
        public string SONumber { get; set; }
        public DateTime SODate { get; set; }
        [AntiXss]
        public string PaymentRef { get; set; }
        [AntiXss]
        public string SOType { get; set; }
        [AntiXss]
        public string TermOfPaymentCode { get; set; }
        [AntiXss]
        public string TOPDescription { get; set; }
        public DateTime DueDate { get; set; }
        [AntiXss]
        public string VehicleColorCode { get; set; }
        [AntiXss]
        public string VehicleTypeCode { get; set; }
        [AntiXss]
        public string MaterialNumber { get; set; }
        [AntiXss]
        public string MaterialDescription { get; set; }
        public Decimal BasePrice { get; set; }
        public Decimal OptionPrice { get; set; }
        public Decimal DiscountBeforeTax { get; set; }
        public Decimal NetPrice { get; set; }
        public Decimal TotalHarga { get; set; }
        public Decimal PPN { get; set; }
        public Decimal TotalHargaPPN { get; set; }
        public Decimal TotalHargaPP { get; set; }
        public Decimal TotalHargaLC { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string SPLNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ETDDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

