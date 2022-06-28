﻿#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : VWI_DepositBHeader_IF class
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
    public class VWI_DepositBHeader_IF
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public int ProductCategoryID { get; set; }
        public DateTime TransactionDate { get; set; }
        public Decimal BeginingBalance { get; set; }
        public Decimal EndBalance { get; set; }
        public Decimal DebetAmount { get; set; }
        public Decimal CreditAmount { get; set; }
        public List<VWI_DepositBDetail_IF> DepositBDetail { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
