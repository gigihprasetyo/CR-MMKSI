#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitTypeDto  class
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
    public class BenefitTypeDto : DtoBase
    {
        #region Public Properties

        public short ID { get; set; }

        public string Name { get; set; }

        public short LeasingBox { get; set; }

        public short AssyYearBox { get; set; }

        public short ReceiptBox { get; set; }

        public short EventValidation { get; set; }

        public short WSDiscount { get; set; }

        public short Status { get; set; }

        #endregion

        #region Custom Properties

        #endregion
    }
}
