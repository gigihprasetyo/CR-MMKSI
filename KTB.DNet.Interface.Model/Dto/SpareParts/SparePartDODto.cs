#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDODto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SparePartDODto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public string DONumber { get; set; }

        public DateTime DoDate { get; set; }

        public DateTime EstmationDeliveryDate { get; set; }

        public DateTime PickingDate { get; set; }

        public DateTime PackingDate { get; set; }

        public DateTime GoodIssueDate { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime ReadyForDeliveryDate { get; set; }

        public DealerDto Dealer { get; set; }

        #endregion

        #region Custom Properties
        #endregion
    }

    public class DeliveryOrderBillingResponseDto : ReadDtoBase
    {
        public string DealerCode { get; set; }
        public DateTime DODate { get; set; }
        public DateTime DueDate { get; set; }
        public string DONumber { get; set; }
        public DateTime BillingDate { get; set; }
        public string BillingNumber { get; set; }
        public string ExpeditionNumber { get; set; }
        public int TermOfPaymentValue { get; set; }
        public string TermOfPaymentCode { get; set; }
        public string TermOfPaymentDesc { get; set; }
        public Decimal AmountC2 { get; set; }
        public List<DeliveryOrderDetailResponseDto> DeliveryOrderDetail { get; set; }
    }
}
