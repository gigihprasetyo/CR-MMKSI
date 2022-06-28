#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PODealerDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class VWI_PODealerDto : DtoBase
    {
        public int POHeaderId { get; set; }
        public int DealerId { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string DestinationDealerCode { get; set; }
        public string PONumber { get; set; }
        public string POType { get; set; }
        public int NumOfInstallment { get; set; }
        public int AllocQty { get; set; }
        public Decimal Price { get; set; }
        public Decimal Discount { get; set; }
        public Decimal Interest { get; set; }
        public string ContractNumber { get; set; }
        public string PKNumber { get; set; }
        public string DealerPKNumber { get; set; }
        public string DealerPONumber { get; set; }
        public string ProjectName { get; set; }
        public int SalesOrderId { get; set; }
        public string SONumber { get; set; }
        public DateTime SODate { get; set; }
        public string PaymentRef { get; set; }
        public string SOType { get; set; }
        public string TermOfPaymentCode { get; set; }
        public string TOPDescription { get; set; }
        public DateTime DueDate { get; set; }
        public string VehicleColorCode { get; set; }
        public string VehicleTypeCode { get; set; }
        public string MaterialNumber { get; set; }
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
        public Decimal TotalDeposit { get; set; }
        public Decimal TotalInterest { get; set; }
        public string SPLNumber { get; set; }

        public DateTime ReleaseDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ETDDate { get; set; }

    }
}

