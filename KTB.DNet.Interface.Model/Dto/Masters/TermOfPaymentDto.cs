#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : TermOfPaymentDto  class
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
    public class TermOfPaymentDto : DtoBase
    {
        public int ID { get; set; }

        public string TermOfPaymentCode { get; set; }

        public int TermOfPaymentValue { get; set; }

        public int PaymentType { get; set; }

        public string Description { get; set; }
    }
}

