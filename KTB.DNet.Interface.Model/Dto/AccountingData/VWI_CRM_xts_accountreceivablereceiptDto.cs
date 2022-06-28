#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountreceivablereceiptDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:52
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_accountreceivablereceiptDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public Guid xts_customerid { get; set; }

		public string ktb_untukpembayaran { get; set; }

		public DateTime modifiedon { get; set; }

		public bool xts_cancelled { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public decimal xts_totalamount { get; set; }

		public string statuscodename { get; set; }

		public decimal xts_totaloutstandingbalance { get; set; }

		public string xts_handling { get; set; }

		public string xts_cancelledname { get; set; }

		public Guid xts_cashandbankdimension3id { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_customeridyominame { get; set; }

		public string xts_cashandbankdimension5idname { get; set; }

		public decimal xts_insuranceamount_base { get; set; }

		public bool xts_type { get; set; }

		public string ktb_colordescription { get; set; }

		public DateTime ktb_giroduedate { get; set; }

		public string ktb_bankgiro { get; set; }

		public string ktb_productionyear { get; set; }

		public Guid xts_cashandbankdimension2id { get; set; }

		public Guid xts_contractid { get; set; }

		public Guid owningbusinessunit { get; set; }

		public Guid xts_receivabledimension2id { get; set; }

		public string ktb_productidname { get; set; }

		public Guid xts_receivabledimension1id { get; set; }

		public string xts_customernumber { get; set; }

		public string xts_cashandbankdimension4idname { get; set; }

		public string xts_customeridname { get; set; }

		public string ktb_referencekwitansi { get; set; }

		public int statuscode { get; set; }

		public int statecode { get; set; }

		public Guid xts_accountreceivableinvoicereferenceid { get; set; }

		public string xts_locking { get; set; }

		public Guid xts_cashandbankdimension5id { get; set; }

		public string xts_receivabledimension2idname { get; set; }

		public string xts_methodofpaymentidname { get; set; }

		public string xts_newvehiclewholesaleorderidname { get; set; }

		public string xts_receivabledimension1idname { get; set; }

		public Guid xts_accountreceivablereceiptreferenceid { get; set; }

		public decimal xts_paymentsettlement_base { get; set; }

		public string statecodename { get; set; }

		public decimal xts_totalremainingbalance_base { get; set; }

		public string xts_receivabledimension5idname { get; set; }

		public decimal exchangerate { get; set; }

		public Guid ownerid { get; set; }

		public decimal xts_totalamount_base { get; set; }

		public string ktb_spkidname { get; set; }

		public Guid xts_newvehiclewholesaleorderid { get; set; }

		public string ktb_description2 { get; set; }

		public string modifiedbyyominame { get; set; }

		public decimal xts_paymentsettlement { get; set; }

		public decimal xts_exchangerateamount { get; set; }

		public decimal xts_totalchangeamount_base { get; set; }

		public string xts_statusname { get; set; }

		public string ktb_description1 { get; set; }

		public string xts_arreceipttype { get; set; }

		public string xts_postinglayername { get; set; }

		public string owneridyominame { get; set; }

		public DateTime xts_endorderdate { get; set; }

		public DateTime createdon { get; set; }

		public string ktb_description4 { get; set; }

		public string xts_receivabledimension6idname { get; set; }

		public string xts_cashandbankdimension1idname { get; set; }

		public Guid modifiedby { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string xts_handlingname { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal xts_totalreceiptamount_base { get; set; }

		public string xts_typename { get; set; }

		public string xts_accountreceivableinvoicereferenceidname { get; set; }

		public decimal xts_totalotherexpenses_base { get; set; }

		public decimal xts_insuranceamount { get; set; }

		public Guid owningteam { get; set; }

		public Guid xts_exchangeratetype { get; set; }

		public Guid xts_accountreceivablereceiptid { get; set; }

		public string xts_postinglayer { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Guid owninguser { get; set; }

		public string xts_idempotentmessage { get; set; }

		public Guid ktb_spkid { get; set; }

		public Guid xts_cashandbankdimension1id { get; set; }

		public Guid xts_receivabledimension6id { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public Guid xts_receivabledimension5id { get; set; }

		public string xts_cashandbankdimension3idname { get; set; }

		public Guid xts_receivabledimension4id { get; set; }

		public string xts_accountreceivablereceiptreferenceidname { get; set; }

		public string owneridname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string ktb_description3 { get; set; }

		public string xts_receiptnumber { get; set; }

		public string xts_receivabledimension4idname { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Guid createdonbehalfby { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public string ktb_chassisnumber { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xts_eventdata { get; set; }

		public string xts_exchangeratetypename { get; set; }

		public string xts_accountreceivablereceiptnumber { get; set; }

		public Guid xts_receivabledimension3id { get; set; }

		public string ktb_say { get; set; }

		public DateTime xts_exchangeratedate { get; set; }

		public string xts_cashandbankaccountidname { get; set; }

		public decimal xts_totaloutstandingbalance_base { get; set; }

		public decimal xts_totalchangeamount { get; set; }

		public decimal xts_totalreceiptamount { get; set; }

		public string xts_contractidname { get; set; }

		public Guid xts_cashandbankdimension4id { get; set; }

		public string xjp_generatedtoken { get; set; }

		public Guid xts_cashandbankid { get; set; }

		public string ktb_girono { get; set; }

		public decimal xts_totalotherexpenses { get; set; }

		public string createdbyname { get; set; }

		public string xts_businessunitidname { get; set; }

		public string xts_sourcetypename { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid createdby { get; set; }

		public DateTime xts_startorderdate { get; set; }

		public int importsequencenumber { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public Guid xts_cashandbankdimension6id { get; set; }

		public Guid xts_cashandbankaccountid { get; set; }

		public string xts_arreceipttypename { get; set; }

		public string xts_cashandbankdimension6idname { get; set; }

		public string xts_cashandbankdimension2idname { get; set; }

		public string xts_cashandbankidname { get; set; }

		public bool xts_bookingfee { get; set; }

		public Guid ktb_productid { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_status { get; set; }

		public string owneridtype { get; set; }

		public string ktb_customerdescription { get; set; }

		public string modifiedbyname { get; set; }

		public Guid xts_methodofpaymentid { get; set; }

		public string createdbyyominame { get; set; }

		public string xts_sourcetype { get; set; }

		public string xts_bookingfeename { get; set; }

		public decimal xts_totalremainingbalance { get; set; }

		public string xts_receivabledimension3idname { get; set; }

		public string ktb_enginenumber { get; set; }

		public string ktb_externalcode { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
