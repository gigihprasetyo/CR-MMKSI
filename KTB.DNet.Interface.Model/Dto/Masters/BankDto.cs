﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BankDto  class
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
    public class BankDto : DtoBase
    {
        public int ID { get; set; }

        public string BankCode { get; set; }

        public string BankName { get; set; }
    }
}
