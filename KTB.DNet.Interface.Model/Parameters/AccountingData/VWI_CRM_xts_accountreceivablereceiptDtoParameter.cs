#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountreceivablereceiptParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_accountreceivablereceiptParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public string ktb_untukpembayaran { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public bool xts_cancelled { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public decimal xts_totalamount { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal xts_totaloutstandingbalance { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public string xts_cancelledname { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension3id { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension5idname { get; set; }

		[AntiXss]
		public decimal xts_insuranceamount_base { get; set; }

		[AntiXss]
		public bool xts_type { get; set; }

		[AntiXss]
		public string ktb_colordescription { get; set; }

		[AntiXss]
		public DateTime ktb_giroduedate { get; set; }

		[AntiXss]
		public string ktb_bankgiro { get; set; }

		[AntiXss]
		public string ktb_productionyear { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension2id { get; set; }

		[AntiXss]
		public Guid xts_contractid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension2id { get; set; }

		[AntiXss]
		public string ktb_productidname { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension1id { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension4idname { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public string ktb_referencekwitansi { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public Guid xts_accountreceivableinvoicereferenceid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension5id { get; set; }

		[AntiXss]
		public string xts_receivabledimension2idname { get; set; }

		[AntiXss]
		public string xts_methodofpaymentidname { get; set; }

		[AntiXss]
		public string xts_newvehiclewholesaleorderidname { get; set; }

		[AntiXss]
		public string xts_receivabledimension1idname { get; set; }

		[AntiXss]
		public Guid xts_accountreceivablereceiptreferenceid { get; set; }

		[AntiXss]
		public decimal xts_paymentsettlement_base { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xts_totalremainingbalance_base { get; set; }

		[AntiXss]
		public string xts_receivabledimension5idname { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public decimal xts_totalamount_base { get; set; }

		[AntiXss]
		public string ktb_spkidname { get; set; }

		[AntiXss]
		public Guid xts_newvehiclewholesaleorderid { get; set; }

		[AntiXss]
		public string ktb_description2 { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public decimal xts_paymentsettlement { get; set; }

		[AntiXss]
		public decimal xts_exchangerateamount { get; set; }

		[AntiXss]
		public decimal xts_totalchangeamount_base { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public string ktb_description1 { get; set; }

		[AntiXss]
		public string xts_arreceipttype { get; set; }

		[AntiXss]
		public string xts_postinglayername { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public DateTime xts_endorderdate { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string ktb_description4 { get; set; }

		[AntiXss]
		public string xts_receivabledimension6idname { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension1idname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal xts_totalreceiptamount_base { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public string xts_accountreceivableinvoicereferenceidname { get; set; }

		[AntiXss]
		public decimal xts_totalotherexpenses_base { get; set; }

		[AntiXss]
		public decimal xts_insuranceamount { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public Guid xts_exchangeratetype { get; set; }

		[AntiXss]
		public Guid xts_accountreceivablereceiptid { get; set; }

		[AntiXss]
		public string xts_postinglayer { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string xts_idempotentmessage { get; set; }

		[AntiXss]
		public Guid ktb_spkid { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension1id { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension6id { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension5id { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension3idname { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension4id { get; set; }

		[AntiXss]
		public string xts_accountreceivablereceiptreferenceidname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_description3 { get; set; }

		[AntiXss]
		public string xts_receiptnumber { get; set; }

		[AntiXss]
		public string xts_receivabledimension4idname { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public string ktb_chassisnumber { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public string xts_exchangeratetypename { get; set; }

		[AntiXss]
		public string xts_accountreceivablereceiptnumber { get; set; }

		[AntiXss]
		public Guid xts_receivabledimension3id { get; set; }

		[AntiXss]
		public string ktb_say { get; set; }

		[AntiXss]
		public DateTime xts_exchangeratedate { get; set; }

		[AntiXss]
		public string xts_cashandbankaccountidname { get; set; }

		[AntiXss]
		public decimal xts_totaloutstandingbalance_base { get; set; }

		[AntiXss]
		public decimal xts_totalchangeamount { get; set; }

		[AntiXss]
		public decimal xts_totalreceiptamount { get; set; }

		[AntiXss]
		public string xts_contractidname { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension4id { get; set; }

		[AntiXss]
		public string xjp_generatedtoken { get; set; }

		[AntiXss]
		public Guid xts_cashandbankid { get; set; }

		[AntiXss]
		public string ktb_girono { get; set; }

		[AntiXss]
		public decimal xts_totalotherexpenses { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string xts_sourcetypename { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public DateTime xts_startorderdate { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension6id { get; set; }

		[AntiXss]
		public Guid xts_cashandbankaccountid { get; set; }

		[AntiXss]
		public string xts_arreceipttypename { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension6idname { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension2idname { get; set; }

		[AntiXss]
		public string xts_cashandbankidname { get; set; }

		[AntiXss]
		public bool xts_bookingfee { get; set; }

		[AntiXss]
		public Guid ktb_productid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string ktb_customerdescription { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_methodofpaymentid { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_sourcetype { get; set; }

		[AntiXss]
		public string xts_bookingfeename { get; set; }

		[AntiXss]
		public decimal xts_totalremainingbalance { get; set; }

		[AntiXss]
		public string xts_receivabledimension3idname { get; set; }

		[AntiXss]
		public string ktb_enginenumber { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
