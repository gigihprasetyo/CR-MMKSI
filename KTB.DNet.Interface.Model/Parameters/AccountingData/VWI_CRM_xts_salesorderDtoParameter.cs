#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_salesorderParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:02
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
    public class VWI_CRM_xts_salesorderParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_methodofpaymentidname { get; set; }

		[AntiXss]
		public string xts_salesordernumber { get; set; }

		[AntiXss]
		public string xjp_idempotentmessage { get; set; }

		[AntiXss]
		public int xts_referencenumberlookuptype { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string ktb_saleschannel { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public string xts_checkstockagainst { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_referencenumberusedvehiclesalesorderid { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_salesorderid { get; set; }

		[AntiXss]
		public Guid xts_newvehiclesalesorderid { get; set; }

		[AntiXss]
		public string xts_behaviorname { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public bool xts_overcreditlimitonhold { get; set; }

		[AntiXss]
		public string xts_billtocustomeridname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string xts_downpaymentispaidname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_billtocustomerid { get; set; }

		[AntiXss]
		public Guid xts_ordertypeid { get; set; }

		[AntiXss]
		public Guid ktb_purchaseorderid { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamount { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public Guid ktb_saleschannelid { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamount_base { get; set; }

		[AntiXss]
		public string ktb_ribbondata { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_referencenumberusedvehiclesalesorderidname { get; set; }

		[AntiXss]
		public decimal xts_balance { get; set; }

		[AntiXss]
		public string xts_billtocustomeridyominame { get; set; }

		[AntiXss]
		public string xts_checkstockagainstname { get; set; }

		[AntiXss]
		public decimal xts_totalreceipt { get; set; }

		[AntiXss]
		public DateTime xts_exchangeratedate { get; set; }

		[AntiXss]
		public decimal xts_totaldiscountamount_base { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xts_termofpaymentid { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxamount_base { get; set; }

		[AntiXss]
		public Guid ktb_purchaserequisitionid { get; set; }

		[AntiXss]
		public string ktb_purchaserequisitionidname { get; set; }

		[AntiXss]
		public decimal xts_exchangerateamount { get; set; }

		[AntiXss]
		public DateTime ktb_latestpurchasepricedate { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public string xts_referencenumberlookupname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public string xts_termofpaymentidname { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public Guid xts_referencenumbernewvehiclesalesorderid { get; set; }

		[AntiXss]
		public decimal xts_totalreceipt_base { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_exchangeratetypeidname { get; set; }

		[AntiXss]
		public string xts_newvehiclesalesorderidname { get; set; }

		[AntiXss]
		public string ktb_saleschannelidname { get; set; }

		[AntiXss]
		public decimal xts_totaldiscountamount { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public Guid xts_referencenumberworkorderid { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string ktb_modelcode { get; set; }

		[AntiXss]
		public Guid ktb_campaignid { get; set; }

		[AntiXss]
		public string xts_shipmenttypename { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public bool xts_downpaymentispaid { get; set; }

		[AntiXss]
		public string xts_referencenumbernewvehiclesalesorderidname { get; set; }

		[AntiXss]
		public string xts_ordertypeidname { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxamount { get; set; }

		[AntiXss]
		public Guid xts_methodofpaymentid { get; set; }

		[AntiXss]
		public decimal xts_exchangerateamount_base { get; set; }

		[AntiXss]
		public Guid xts_exchangeratetypeid { get; set; }

		[AntiXss]
		public string ktb_campaignidname { get; set; }

		[AntiXss]
		public string xts_overcreditlimitonholdname { get; set; }

		[AntiXss]
		public decimal xts_balance_base { get; set; }

		[AntiXss]
		public bool xts_bigdata { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public decimal xts_grandtotal { get; set; }

		[AntiXss]
		public decimal xts_grandtotal_base { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string ktb_customerdescription { get; set; }

		[AntiXss]
		public DateTime xts_cancellationdate { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string ktb_overdueonholdname { get; set; }

		[AntiXss]
		public string xts_shipmenttype { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamountreceived { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_bigdataname { get; set; }

		[AntiXss]
		public string ktb_purchaseorderidname { get; set; }

		[AntiXss]
		public string xts_externalreferencenumber { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_behavior { get; set; }

		[AntiXss]
		public string ktb_say { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount_base { get; set; }

		[AntiXss]
		public bool ktb_overdueonhold { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamountreceived_base { get; set; }

		[AntiXss]
		public string xts_referencenumberworkorderidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string ktb_saleschannelname { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargetaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargebaseamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargeamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargetaxamount { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargeamount { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargebaseamount { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
