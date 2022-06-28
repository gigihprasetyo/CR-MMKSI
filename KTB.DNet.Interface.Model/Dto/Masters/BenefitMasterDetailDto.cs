#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitMasterDetailDto  class
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
    public class BenefitMasterDetailDto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public string FormulaID { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime FakturValidationStart { get; set; }

        public DateTime FakturValidationEnd { get; set; }

        public DateTime FakturOpenStart { get; set; }

        public DateTime FakturOpenEnd { get; set; }

        public short AssyYear { get; set; }

        public short MaxClaim { get; set; }

        public short WSDiscount { get; set; }

        public BenefitMasterHeaderDto BenefitMasterHeader { get; set; }

        public BenefitTypeDto BenefitType { get; set; }

        #endregion

        #region Custom Properties

        #endregion
    }
}
