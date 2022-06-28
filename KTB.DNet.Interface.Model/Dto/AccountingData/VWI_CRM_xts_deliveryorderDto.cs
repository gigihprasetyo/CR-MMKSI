#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_deliveryorderDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 10/01/2020 7:00:34
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_deliveryorderDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public decimal xts_totaltaxamount_base { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public string xts_billtocustomeridyominame { get; set; }

		public Guid xts_villageandstreetid { get; set; }

		public string xts_methodofpaymentidname { get; set; }

		public Guid xts_provinceid { get; set; }

		public int xts_referenceid { get; set; }

		public int xts_referencenumberlookuptype { get; set; }

		public string ktb_arinvoicenoname { get; set; }

		public string statuscodename { get; set; }

		public string xts_status { get; set; }

		public Guid xts_customerid { get; set; }

		public string xts_referencenumbersalesorderidname { get; set; }

		public string modifiedbyyominame { get; set; }

		public string owneridtype { get; set; }

		public string xts_log { get; set; }

		public decimal xts_totalbaseamount_base { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string modifiedbyname { get; set; }

		public decimal xts_totalwithholdingtaxamount { get; set; }

		public string xjp_idempotentmessage { get; set; }

		public string xts_deliveryaddressidname { get; set; }

		public string xts_billtocustomeridname { get; set; }

		public string owneridname { get; set; }

		public decimal xts_totalmiscchargebaseamount { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public Guid xts_billtocustomerid { get; set; }

		public Guid xts_deliveryorderid { get; set; }

		public Guid xts_ordertypeid { get; set; }

		public string xts_villageandstreetidname { get; set; }

		public string ktb_statusinterfacedesc { get; set; }

		public string xts_eventdata { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public bool ktb_cogsupdated { get; set; }

		public string xts_address4 { get; set; }

		public Guid owningteam { get; set; }

		public decimal xts_totalbaseamount { get; set; }

		public Guid xts_deliveryaddressid { get; set; }

		public decimal xts_totalreceipt { get; set; }

		public int statecode { get; set; }

		public decimal xts_totaldiscountamount_base { get; set; }

		public Guid xts_salespersonid { get; set; }

		public decimal xts_totalmiscchargebaseamount_base { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public Guid xts_referencenumbersalesorderid { get; set; }

		public string xts_deliverytypename { get; set; }

		public decimal xts_totaltaxamount { get; set; }

		public decimal xts_totalwithholdingtaxamount_base { get; set; }

		public Guid xts_referencenumberdeliveryorderid { get; set; }

		public string ktb_statusinterface { get; set; }

		public string xts_salespersonidname { get; set; }

		public string xts_referencenumberlookupname { get; set; }

		public string xts_locking { get; set; }

		public Guid xts_customercontactid { get; set; }

		public string xts_termofpaymentidname { get; set; }

		public decimal xts_totalamountbeforediscount { get; set; }

		public string xts_customernumber { get; set; }

		public string ktb_handlinginterface { get; set; }

		public Guid xts_countryid { get; set; }

		public string xts_provinceidname { get; set; }

		public decimal xts_totalreceipt_base { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string createdbyyominame { get; set; }

		public string xts_handling { get; set; }

		public Guid xts_cityid { get; set; }

		public string ktb_handlinginterfacename { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_deliveryordernumber { get; set; }

		public string xts_statusname { get; set; }

		public string xts_customeridyominame { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public string xts_countryidname { get; set; }

		public string xts_businessphone { get; set; }

		public string xts_customeridname { get; set; }

		public Guid xts_termofpaymentid { get; set; }

		public int xts_customerlookuptype { get; set; }

		public string xts_address2 { get; set; }

		public string xts_customercontactidyominame { get; set; }

		public decimal xts_totalmiscchargetaxamount { get; set; }

		public string xts_ordertypeidname { get; set; }

		public string createdbyname { get; set; }

		public Guid xts_methodofpaymentid { get; set; }

		public string xts_referencenumberdeliveryorderidname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_deliverytype { get; set; }

		public string xts_address3 { get; set; }

		public decimal xts_totalmiscchargetaxamount_base { get; set; }

		public string xts_cityidname { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public string ktb_say { get; set; }

		public string xts_handlingname { get; set; }

		public decimal xts_grandtotal { get; set; }

		public decimal xts_grandtotal_base { get; set; }

		public string xts_postalcode { get; set; }

		public string xts_customerlookupname { get; set; }

		public string ktb_customerdescription { get; set; }

		public DateTime xts_cancellationdate { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_address1 { get; set; }

		public string ktb_saywithouttax { get; set; }

		public string xts_externalreferencenumber { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid owningbusinessunit { get; set; }

		public decimal xts_totaldiscountamount { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string ktb_modelcode { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Guid ktb_arinvoiceno { get; set; }

		public string ktb_cogsupdatedname { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string xts_eventdatantext { get; set; }

		public decimal xts_totalamountbeforediscount_base { get; set; }

		public string statecodename { get; set; }

		public string xts_businessunitidname { get; set; }

		public string xts_customercontactidname { get; set; }

		public string ktb_ordervehiclename { get; set; }

		public bool ktb_ordervehicle { get; set; }

		public decimal xts_totalmiscchargeamount_base { get; set; }

		public decimal xts_totalmiscchargeamount { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
