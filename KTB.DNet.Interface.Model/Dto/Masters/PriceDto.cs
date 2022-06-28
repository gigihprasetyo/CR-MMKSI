#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PriceDto  class
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
using KTB.DNet.Interface.Model.CustomAttribute;
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class PriceDto : DtoBase
    {
        public int ID { get; set; }

        [DateTimeDisplayFormatAttribute]
        public DateTime ValidFrom { get; set; }

        public Decimal BasePrice { get; set; }

        public Decimal OptionPrice { get; set; }

        public Decimal PPN_BM { get; set; }

        public Decimal PPN { get; set; }

        public Decimal PPh22 { get; set; }

        public Decimal Interest { get; set; }

        public Decimal FactoringInt { get; set; }

        public Decimal PPh23 { get; set; }

        public string Status { get; set; }

        public Decimal DiscountReward { get; set; }

        public VehicleColorDto VehicleColor { get; set; }

        public DealerDto Dealer { get; set; }
    }
}
