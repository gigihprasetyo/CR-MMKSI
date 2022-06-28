#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_equipment  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_ktb_lkpp
    {
        public String company { get; set; }
        public String businessunitcode { get; set; }
        public Guid ktb_lkppid { get; set; }
        public DateTime ktb_tanggalpengajuan { get; set; }
        public String ktb_nopengadaan { get; set; }
        public String ktb_metodepengadaan { get; set; }
        public String ktb_namacustomer { get; set; }
        public String ktb_deskripsi { get; set; }
        public int ktb_status { get; set; }
        public String ktb_catatan { get; set; }
        public DateTime createdon { get; set; }
        public DateTime modifiedon { get; set; }

    }
}
