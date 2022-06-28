#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPLDetailDto  class
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
    public class SPLDetailDto : DtoBase
    {
        public int ID { get; set; }

        public int PeriodMonth { get; set; }

        public int PeriodYear { get; set; }

        public int Quantity { get; set; }

        public DateTime PriceRefDate { get; set; }

        public int Discount { get; set; }

        public int SurCharge { get; set; }

        public DateTime MaxTopDate { get; set; }

        public int MaxTopDay { get; set; }

        public int MaxTopIndicator { get; set; }

        public int FreeIntIndicator { get; set; }

        public short CreditCeiling { get; set; }

        public DateTime DeliveryDate { get; set; }

        public VehicleTypeDto VehicleType;

        public SPLDto SPL;
    }
}
