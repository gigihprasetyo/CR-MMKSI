﻿#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : VWI_IPAddressRestriction_IF class
 GENERATED BY  : Admin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 11 Okt 2021
 ===========================================================================
*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace KTB.DNet.Interface.Domain
{
    public class VWI_IPAddressRestriction_IF
    {
        public int Id { get; set; }
        public string Begin_IP_Address { get; set; }
        public string End_IP_Address { get; set; }
        public string Country { get; set; }
        public bool IsAllow { get; set; }
        public int RowStatus { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string LastUpdateBy { get; set; }
    }

    public class VWI_UserNameRestriction_IF
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsAllow { get; set; }
        public int RowStatus { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
