#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_purchasereceipt class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 26 Aug 2020 14:50:37
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_purchasereceiptDto : DtoBase
    {
        public Guid xts_purchasereceiptid { get; set; }
        public DateTime? ktb_ata { get; set; }

		public DateTime? ktb_deliveryfordeliverydate { get; set; }

		public int? ktb_discountpercentage { get; set; }

		public DateTime? ktb_duedate { get; set; }

		public DateTime? ktb_estimationdeliverydate { get; set; }

		public string ktb_expeditionnumber { get; set; }

		public DateTime? ktb_goodissuedate { get; set; }

		public string ktb_identificationtype { get; set; }

		public string ktb_interfacehandling { get; set; }

		public string ktb_interfacestatus { get; set; }

		public DateTime? ktb_invoicedate { get; set; }

		public DateTime? ktb_packingdate { get; set; }

		public DateTime? ktb_paymentdate { get; set; }

		public DateTime? ktb_pickingdate { get; set; }

		public Guid ktb_prpotypeid { get; set; }

		public Guid ktb_purchaserequisitionid { get; set; }

		public string ktb_returnreason { get; set; }

		public string ktb_ribbondata { get; set; }

		public string ktb_ribbondataproductwarehouse { get; set; }

		public string ktb_salesorderno { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public int? statecode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid xts_accountpayablevoucherid { get; set; }

		public string xts_address1 { get; set; }

		public string xts_address2 { get; set; }

		public string xts_address3 { get; set; }

		public string xts_address4 { get; set; }

		public bool? xts_assignlandedcostperdetail { get; set; }

		public Guid xts_businessunitid { get; set; }

		public Guid xts_cityid { get; set; }

		public Guid xts_countryid { get; set; }

		public DateTime? xts_deliveryorderdate { get; set; }

		public string xts_eventdata { get; set; }

		public string xts_eventdata2 { get; set; }

		public decimal? xts_grandtotal { get; set; }

		public bool? xts_loaddata { get; set; }

		public DateTime? xts_packingslipdate { get; set; }

		public string xts_packingslipnumber { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public string xts_postalcode { get; set; }

		public Guid xts_provinceid { get; set; }

		public Guid xts_purchaseorderid { get; set; }

		public string xts_purchasereceiptnumber { get; set; }

		public string xts_purchasereceiptreferencerequiredname { get; set; }

		public Guid xts_returnpurchasereceiptid { get; set; }

		public string xts_status { get; set; }

		public Guid xts_termsofpaymentid { get; set; }

		public decimal? xts_totalbaseamount { get; set; }

		public decimal? xts_totalconsumptiontax1amount { get; set; }

		public decimal? xts_totalconsumptiontax2amount { get; set; }

		public decimal? xts_totalconsumptiontaxamount { get; set; }

		public decimal? xts_totaltitleregistrationfee { get; set; }

		public DateTime? xts_transactiondate { get; set; }

		public Guid xts_transferorderrequestingid { get; set; }

		public string xts_type { get; set; }

		public string xts_vendordescription { get; set; }

		public Guid xts_vendorid { get; set; }

		public DateTime? xts_vendorinvoicedate { get; set; }

		public string xts_vendorinvoicenumber { get; set; }

		public Guid xts_workorderid { get; set; }

        public string xts_deliveryordernumber { get; set; }

    }
}
