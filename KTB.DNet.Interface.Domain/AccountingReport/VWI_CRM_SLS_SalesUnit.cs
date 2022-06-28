#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_SLS_SalesUnit  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 21/01/2020 14:18:44
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_SLS_SalesUnit
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_newvehicledeliveryordernumber { get; set; }

		public string ktb_vehiclecolorname { get; set; }

		public DateTime ktb_tanggalpkt { get; set; }

		public string sitename { get; set; }

		public string xts_productdescription { get; set; }

		public string personinchargename { get; set; }

		public string xts_driver { get; set; }

		public DateTime xts_deliverydate { get; set; }

		public string customername { get; set; }

		public string xts_productexteriorcolor { get; set; }

		public string xts_warehouse { get; set; }

		public string productcategorycode { get; set; }

		public string productcategorydescription { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
