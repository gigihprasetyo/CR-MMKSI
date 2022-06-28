#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_PQRDto  class
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

namespace KTB.DNet.Interface.Model
{
    public class VWI_PQRDto : DtoBase   
    {
        public string PQRNo { get; set; }
        public string DealerID { get; set; }
        public string PQRType { get; set; }
        public DateTime DocumentDate { get; set; }
        public string ChassisNumber { get; set; }
        public string Subject { get; set; }
        public List<VWI_PQRSparePartMasterDto> SparePartMaster { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class VWI_PQRSparePartMasterDto
    {
        public string PartNumber { get; set; }
    }
}
