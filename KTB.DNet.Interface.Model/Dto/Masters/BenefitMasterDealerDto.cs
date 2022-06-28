#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitMasterDealerDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

namespace KTB.DNet.Interface.Model
{
    public class BenefitMasterDealerDto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public DealerDto Dealer { get; set; }

        public BenefitMasterHeaderDto BenefitMasterHeader { get; set; }

        #endregion

        #region Custom Properties

        #endregion
    }
}
