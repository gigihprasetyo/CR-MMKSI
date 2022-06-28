#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_accountreceivableinvoice class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 16:49:32
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_accountreceivableinvoiceDto : DtoBase
    {
        
		public string ktb_chassisnumber { get; set; }

		public string ktb_customerdescription { get; set; }

		public string ktb_enginenumber { get; set; }

		public Guid ktb_ordertypeid { get; set; }

		public Guid ktb_parentbusinessunitid { get; set; }

		public Guid ktb_productid { get; set; }

		public Guid ktb_spkid { get; set; }

		public string ktb_vehiclecolordescription { get; set; }

		public string ktb_vehicleproductionyear { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public int? statecode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xts_accountreceivableinvoice { get; set; }

		public Guid xts_accountreceivableinvoiceid { get; set; }

		public decimal? xts_balance { get; set; }

		public string xts_billabletype { get; set; }

		public Guid xts_businessunitid { get; set; }

		public Guid xts_customerid { get; set; }

		public string xts_customernumber { get; set; }

		public Guid xts_deliveryorderid { get; set; }

		public string xts_deliveryordernumber { get; set; }

		public DateTime? xts_duedate { get; set; }

		public decimal? xts_financingcompanybalance { get; set; }

		public Guid xts_financingcompanyid { get; set; }

		public decimal? xts_financingcompanyinvoiceamount { get; set; }

		public string xts_financingcompanynumber { get; set; }

		public decimal? xts_financingcompanyreceiptamount { get; set; }

		public decimal? xts_invoiceamount { get; set; }

		public Guid xts_newvehicledeliveryorderid { get; set; }

		public Guid xts_newvehiclesalesorderid { get; set; }

		public string xts_ordernumber { get; set; }

		public string xts_originalreferencenumber { get; set; }

		public Guid xts_originalreferencenumberid { get; set; }

		public bool? xts_reversing { get; set; }

		public Guid xts_salesorderid { get; set; }

		public Guid xts_serviceproportionalinvoiceid { get; set; }

		public string xts_sourcetype { get; set; }

		public string xts_status { get; set; }

		public string xts_taxinvoicenumber { get; set; }

		public string xts_taxregistrationnumber { get; set; }

		public string xts_termofpayment { get; set; }

		public decimal? xts_totalreceiptamount { get; set; }

		public DateTime? xts_transactiondate { get; set; }

		public string xts_type { get; set; }

		public Guid xts_usedvehicledeliveryorderid { get; set; }

		public Guid xts_usedvehiclesalesorderid { get; set; }

		public Guid xts_workorderid { get; set; }

		public Guid xts_writeoffbalanceid { get; set; }

		public bool ktb_isallowcancel { get; set; }

		public string ktb_externalcode { get; set; }


	}
}