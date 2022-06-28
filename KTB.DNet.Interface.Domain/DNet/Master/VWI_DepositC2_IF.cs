﻿#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : VWI_DepositC2_IF class
 GENERATED BY  : Admin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 14 Sep 2021
 ===========================================================================
*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace KTB.DNet.Interface.Domain
{
    public class VWI_DepositC2_IF
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string Period { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
