#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xjp_pdidetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 10:19:00
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xjp_pdidetailDto : DtoBase
    {
        
		public bool? ktb_karoseri { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public int? statecode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xjp_accessoriesdescription { get; set; }

		public Guid xjp_accessoriesid { get; set; }

		public decimal? xjp_actualpartfee { get; set; }

		public decimal? xjp_actualservicefee { get; set; }

		public decimal? xjp_actualsubcontractfee { get; set; }

		public Guid xjp_businessunitid { get; set; }

		public decimal? xjp_consumptiontax1amount { get; set; }

		public Guid xjp_consumptiontax1id { get; set; }

		public decimal? xjp_consumptiontax1rate { get; set; }

		public decimal? xjp_consumptiontax2amount { get; set; }

		public Guid xjp_consumptiontax2id { get; set; }

		public decimal? xjp_consumptiontax2rate { get; set; }

		public decimal? xjp_estimatedpartfee { get; set; }

		public decimal? xjp_estimatedservicefee { get; set; }

		public decimal? xjp_estimatedsubcontractfee { get; set; }

		public Guid xjp_installationcategoryatnvsoid { get; set; }

		public Guid xjp_installationcategoryid { get; set; }

		public decimal? xjp_invoicepartfee { get; set; }

		public decimal? xjp_invoiceservicefee { get; set; }

		public decimal? xjp_invoicesubcontractfee { get; set; }

		public Guid xjp_kitid { get; set; }

		public string xjp_locking { get; set; }

		public Guid xjp_parentbusinessunitid { get; set; }

		public Guid xjp_pdidetailid { get; set; }

		public string xjp_pdidetailnumber { get; set; }

		public Guid xjp_predeliveryinspectionid { get; set; }

		public decimal? xjp_quantity { get; set; }

		public Guid xjp_servicecategoryid { get; set; }

    }
}
