#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_purchasereceiptdetaillandedcost class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 16:19:08
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_purchasereceiptdetaillandedcostDto : DtoBase
    {
        public Guid xts_purchasereceiptdetaillandedcostid { get; set; }
        public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public int? statecode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public decimal? xts_amount { get; set; }

		public Guid xts_apvouchernumberid { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_calculationmethod { get; set; }

		public DateTime? xts_documentdate { get; set; }

		public string xts_invoicenumber { get; set; }

		public Guid xts_landedcostid { get; set; }

		public string xts_landedcostnumber { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Guid xts_purchasereceiptdetailid { get; set; }

		public Guid xts_purchasereceiptid { get; set; }

		public string xts_recognitioncategory { get; set; }

		public decimal? xts_taxamount { get; set; }

		public decimal? xts_totalamount { get; set; }

		public Guid xts_vendorid { get; set; }

    }
}