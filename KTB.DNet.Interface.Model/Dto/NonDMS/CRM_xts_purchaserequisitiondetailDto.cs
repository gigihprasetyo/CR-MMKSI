#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_purchaserequisitiondetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Khalil
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 14:10:52
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_purchaserequisitiondetailDto : DtoBase
    {
        public Guid xts_purchaserequisitiondetailid { get; set; }

        public bool? ktb_isapproved { get; set; }

		public string ktb_productsap { get; set; }

		public int? ktb_qtyforecast { get; set; }

		public DateTime? modifiedon { get; set; }

		public int? statecode { get; set; }

		public Guid xts_businessunitid { get; set; }

		public bool? xts_closeline { get; set; }

		public string xts_closereason { get; set; }

		public Guid xts_departmentid { get; set; }

		public string xts_description { get; set; }

		public decimal? xts_discountamount { get; set; }

		public decimal? xts_discountpercentage { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public string xts_productdescription { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public Guid xts_productid { get; set; }

		public Guid xts_productinteriorcolorid { get; set; }

		public Guid xts_productstyleid { get; set; }

		public DateTime? xts_promisedate { get; set; }

		public string xts_purchasefor { get; set; }

		public Guid xts_purchaseorderdetailid { get; set; }

		public string xts_purchaserequisitiondetail { get; set; }

		public Guid xts_purchaserequisitionid { get; set; }

		public decimal? xts_quantityonpurchaseorder { get; set; }

		public decimal? xts_quantityorder { get; set; }

		public Guid xts_requestedbyid { get; set; }

		public DateTime? xts_requireddate { get; set; }

		public Guid xts_siteid { get; set; }

		public decimal? xts_totalamount { get; set; }

		public decimal? xts_totalamountbeforediscount { get; set; }

		public decimal? xts_totalbaseamount { get; set; }

		public decimal? xts_totalconsumptiontaxamount { get; set; }

		public decimal? xts_transactionamount { get; set; }

		public DateTime? xts_transactiondate { get; set; }

		public decimal? xts_unitcost { get; set; }

    }
}
