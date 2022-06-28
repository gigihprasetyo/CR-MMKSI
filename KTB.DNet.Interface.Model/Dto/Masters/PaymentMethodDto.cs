﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PaymentMethodDto  class
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
    public class PaymentMethodDto : DtoBase
    {
        public byte ID { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
    }
}
