#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PaymentPurposeDto  class
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
    public class PaymentPurposeDto : DtoBase
    {
        public byte ID { get; set; }

        public string PaymentPurposeCode { get; set; }

        public string Description { get; set; }
    }
}
