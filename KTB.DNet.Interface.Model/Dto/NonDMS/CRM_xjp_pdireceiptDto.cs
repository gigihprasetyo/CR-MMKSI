#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xjp_pdireceipt class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 10:30:47
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xjp_pdireceiptDto : DtoBase
    {
        
		public Guid ktb_blankospkid { get; set; }

		public string ktb_chassisnumber { get; set; }

		public bool? ktb_iskaroseri { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public int? statecode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid xjp_businessunitid { get; set; }

		public DateTime? xjp_duedate { get; set; }

		public string xjp_handling { get; set; }

		public string xjp_locking { get; set; }

		public Guid xjp_parentbusinessunitid { get; set; }

		public Guid xjp_pdiid { get; set; }

		public Guid xjp_pdireceiptid { get; set; }

		public string xjp_pdireceiptnumber { get; set; }

		public Guid xjp_pdireceiptreferenceid { get; set; }

		public string xjp_pdireceiptstatus { get; set; }

		public Guid xjp_personinchargeid { get; set; }

		public decimal? xjp_totalfee { get; set; }

		public decimal? xjp_totalpartsfee { get; set; }

		public decimal? xjp_totalservicefee { get; set; }

		public decimal? xjp_totalsubcontractfee { get; set; }

		public DateTime? xjp_transactiondate { get; set; }

		public string xjp_transactiontype { get; set; }

		public Guid xjp_vendorid { get; set; }

		public DateTime? xjp_vendorinvoicedate { get; set; }

		public string xjp_vendorinvoicenumber { get; set; }

    }
}
