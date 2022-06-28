#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitEventHeaderDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class BenefitEventHeaderDto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public string EventRegNo { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public short Status { get; set; }

        public DealerDto Dealer { get; set; }

        public BenefitMasterHeaderDto BenefitMasterHeader { get; set; }

        #endregion

        #region Custom Properties

        #endregion
    }
}
