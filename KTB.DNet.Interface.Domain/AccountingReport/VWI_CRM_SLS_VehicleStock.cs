#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_SLS_VehicleStock  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 21/01/2020 14:18:45
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_SLS_VehicleStock
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_stocknumber { get; set; }

		public string xts_warehouse { get; set; }

		public string xts_productexteriorcolor { get; set; }

		public decimal xts_totalvalue { get; set; }

		public string xts_product { get; set; }

		public string xts_productdescription { get; set; }

		public string ktb_applicationno { get; set; }

		public string xts_keynumber { get; set; }

		public string xts_chassisnumber { get; set; }

		public Guid ktb_vendorid { get; set; }

		public string xts_vendor { get; set; }

		public string iskaroseri { get; set; }

		public decimal xts_purchaseprice { get; set; }

		public decimal ktb_caroseriesamount { get; set; }

		public string description { get; set; }

		public string ktb_bucompany { get; set; }

		public string stockstatus { get; set; }

		public string availabilitystatus { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
