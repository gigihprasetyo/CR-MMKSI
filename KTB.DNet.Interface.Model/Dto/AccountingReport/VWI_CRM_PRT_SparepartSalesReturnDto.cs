#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartSalesReturnDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:04
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_PRT_SparepartSalesReturnDto : DtoBase
    {
        public string businessunitcode { get; set; }

		public string xts_deliveryordernumber { get; set; }

		public string xts_product { get; set; }

		public string xts_productdescription { get; set; }

		public decimal xts_unitprice { get; set; }

		public decimal xts_discountamount { get; set; }

		public decimal xts_baseamount { get; set; }

		public decimal xts_totalconsumptiontaxamount { get; set; }

		public decimal xts_totalamount { get; set; }

		public string xts_uom { get; set; }

		public decimal ktb_cogstrx { get; set; }

		public decimal xts_discountpercentage { get; set; }

		public decimal xts_quantityreturned { get; set; }

		public string xts_customernumber { get; set; }

		public string customername { get; set; }

		public Guid xts_customerid { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public Guid xts_referencenumbersalesorderid { get; set; }
    }
}
