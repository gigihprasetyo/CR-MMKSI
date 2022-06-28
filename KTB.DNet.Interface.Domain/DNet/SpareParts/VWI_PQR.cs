#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_PQR  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021/06/29
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Domain
{
    public class VWI_PQR
    {
        public string PQRNo { get; set; }
        public string DealerID { get; set; }
        public string PQRType { get; set; }
        public DateTime DocumentDate { get; set; }
        public string ChassisNumber { get; set; }
        public string Subject { get; set; }
        public List<VWI_PQRSparePartMaster> SparePartMaster { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class VWI_PQRSparePartMaster
    {
        public string PartNumber { get; set; }
    }
}
