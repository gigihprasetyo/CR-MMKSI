#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_xts_salesorder  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:02
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_xts_salesorder
    {
        public Int64 IDRow { get; set; }

		public string company { get; set; }

		public string businessunitcode { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public string xts_methodofpaymentidname { get; set; }

		public string xts_salesordernumber { get; set; }

		public string xjp_idempotentmessage { get; set; }

		public int xts_referencenumberlookuptype { get; set; }

		public string xts_eventdata { get; set; }

		public string statuscodename { get; set; }

		public string xts_status { get; set; }

		public Guid xts_customerid { get; set; }

		public string xts_description { get; set; }

		public string modifiedbyyominame { get; set; }

		public string owneridtype { get; set; }

		public string ktb_saleschannel { get; set; }

		public decimal xts_totalbaseamount_base { get; set; }

		public string xts_checkstockagainst { get; set; }

		public Guid createdonbehalfby { get; set; }

		public Guid xts_referencenumberusedvehiclesalesorderid { get; set; }

		public string modifiedbyname { get; set; }

		public Guid xts_salesorderid { get; set; }

		public Guid xts_newvehiclesalesorderid { get; set; }

		public string xts_behaviorname { get; set; }

		public decimal xts_totalbaseamount { get; set; }

		public bool xts_overcreditlimitonhold { get; set; }

		public string xts_billtocustomeridname { get; set; }

		public string owneridname { get; set; }

		public string xts_downpaymentispaidname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public Guid xts_billtocustomerid { get; set; }

		public Guid xts_ordertypeid { get; set; }

		public Guid ktb_purchaseorderid { get; set; }

		public decimal xts_downpaymentamount { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		public Guid ktb_saleschannelid { get; set; }

		public decimal xts_downpaymentamount_base { get; set; }

		public string ktb_ribbondata { get; set; }

		public Guid owningteam { get; set; }

		public string xts_referencenumberusedvehiclesalesorderidname { get; set; }

		public decimal xts_balance { get; set; }

		public string xts_billtocustomeridyominame { get; set; }

		public string xts_checkstockagainstname { get; set; }

		public decimal xts_totalreceipt { get; set; }

		public DateTime xts_exchangeratedate { get; set; }

		public decimal xts_totaldiscountamount_base { get; set; }

		public int statecode { get; set; }

		public Guid xts_salespersonid { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public Guid xts_termofpaymentid { get; set; }

		public decimal xts_totalwithholdingtaxamount_base { get; set; }

		public Guid ktb_purchaserequisitionid { get; set; }

		public string ktb_purchaserequisitionidname { get; set; }

		public decimal xts_exchangerateamount { get; set; }

		public DateTime ktb_latestpurchasepricedate { get; set; }

		public decimal xts_totalconsumptiontaxamount { get; set; }

		public string xts_referencenumberlookupname { get; set; }

		public string xts_locking { get; set; }

		public string xts_handling { get; set; }

		public string xts_termofpaymentidname { get; set; }

		public decimal xts_totalamountbeforediscount { get; set; }

		public string xts_customernumber { get; set; }

		public Guid xts_referencenumbernewvehiclesalesorderid { get; set; }

		public decimal xts_totalreceipt_base { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_exchangeratetypeidname { get; set; }

		public string xts_newvehiclesalesorderidname { get; set; }

		public string ktb_saleschannelidname { get; set; }

		public decimal xts_totaldiscountamount { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public Guid xts_businessunitid { get; set; }

		public Guid xts_referencenumberworkorderid { get; set; }

		public string xts_statusname { get; set; }

		public string xts_customeridyominame { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public string ktb_modelcode { get; set; }

		public Guid ktb_campaignid { get; set; }

		public string xts_shipmenttypename { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public bool xts_downpaymentispaid { get; set; }

		public string xts_referencenumbernewvehiclesalesorderidname { get; set; }

		public string xts_ordertypeidname { get; set; }

		public string createdbyname { get; set; }

		public decimal xts_totalwithholdingtaxamount { get; set; }

		public Guid xts_methodofpaymentid { get; set; }

		public decimal xts_exchangerateamount_base { get; set; }

		public Guid xts_exchangeratetypeid { get; set; }

		public string ktb_campaignidname { get; set; }

		public string xts_overcreditlimitonholdname { get; set; }

		public decimal xts_balance_base { get; set; }

		public bool xts_bigdata { get; set; }

		public string xts_handlingname { get; set; }

		public decimal xts_grandtotal { get; set; }

		public decimal xts_grandtotal_base { get; set; }

		public string xts_salespersonidname { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string ktb_customerdescription { get; set; }

		public DateTime xts_cancellationdate { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public string ktb_overdueonholdname { get; set; }

		public string xts_shipmenttype { get; set; }

		public string createdbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_customeridname { get; set; }

		public Guid owningbusinessunit { get; set; }

		public decimal xts_downpaymentamountreceived { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_bigdataname { get; set; }

		public string ktb_purchaseorderidname { get; set; }

		public string xts_externalreferencenumber { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string xts_behavior { get; set; }

		public string ktb_say { get; set; }

		public decimal xts_totalamountbeforediscount_base { get; set; }

		public bool ktb_overdueonhold { get; set; }

		public decimal xts_downpaymentamountreceived_base { get; set; }

		public string xts_referencenumberworkorderidname { get; set; }

		public string statecodename { get; set; }

		public string xts_businessunitidname { get; set; }

		public string ktb_saleschannelname { get; set; }

		public string RowStatus { get; set; }

		public decimal xts_totalmiscchargetaxamount_base { get; set; }

		public decimal xts_totalmiscchargebaseamount_base { get; set; }

		public decimal xts_totalmiscchargeamount_base { get; set; }

		public decimal xts_totalmiscchargetaxamount { get; set; }

		public decimal xts_totalmiscchargeamount { get; set; }

		public decimal xts_totalmiscchargebaseamount { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
