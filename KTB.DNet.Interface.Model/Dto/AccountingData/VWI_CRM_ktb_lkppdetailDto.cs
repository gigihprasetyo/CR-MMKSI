#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_pricelevelDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_ktb_lkppdetailDto : DtoBase
    {
        public String company { get; set; }
        public String businessunitcode { get; set; }
        public Guid ktb_lkppid { get; set; }
        public Guid ktb_lkppdetailid { get; set; }
        public string ktb_nopengadaan { get; set; }
        public Guid ktb_productid { get; set; }
        public int ktb_jumlahunit { get; set; }
        public int ktb_sisaunit { get; set; }
        public DateTime createdon { get; set; }
        public DateTime modifiedon { get; set; }

    }
}
