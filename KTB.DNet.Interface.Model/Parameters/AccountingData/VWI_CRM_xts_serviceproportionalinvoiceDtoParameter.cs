#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_serviceproportionalinvoiceParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:04
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
    public class VWI_CRM_xts_serviceproportionalinvoiceParameterDto : ParameterDtoBase, IValidatableObject
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
		public string xts_billtocustomernumber { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxallocated { get; set; }

		[AntiXss]
		public Guid xts_withholdingtaxid { get; set; }

		[AntiXss]
		public string xts_billtocustomeridname { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxallocated_base { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal xts_grandtotalbaseallocated { get; set; }

		[AntiXss]
		public decimal xts_grandtotalbaseallocated_base { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public decimal xts_accountreceiptbalance_base { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public decimal xts_grandtotalallocated { get; set; }

		[AntiXss]
		public decimal xts_grandtotaltaxallocated_base { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_billtocustomerid { get; set; }

		[AntiXss]
		public string xts_address2 { get; set; }

		[AntiXss]
		public Guid xts_billtocustomertermofpaymentid { get; set; }

		[AntiXss]
		public decimal xts_grandtotalallocated_base { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_workorderidname { get; set; }

		[AntiXss]
		public decimal xts_caliallocated_base { get; set; }

		[AntiXss]
		public string xts_address4 { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesbaseallocated { get; set; }

		[AntiXss]
		public string xts_billtocustomeridyominame { get; set; }

		[AntiXss]
		public decimal xts_grandtotaltaxallocated { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargeallocated { get; set; }

		[AntiXss]
		public string xts_billtocustomertermofpaymentidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargebaseallocated { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string ktb_say { get; set; }

		[AntiXss]
		public decimal xts_totalworkbaseallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalpartstaxallocated { get; set; }

		[AntiXss]
		public string xts_withholdingtaxidname { get; set; }

		[AntiXss]
		public decimal xts_totalpartsallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalworkbaseallocated { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargebaseallocated_base { get; set; }

		[AntiXss]
		public decimal xts_totalworktaxallocated { get; set; }

		[AntiXss]
		public decimal xts_totalpartsbaseallocated_base { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_serviceproportionalinvoice { get; set; }

		[AntiXss]
		public decimal xts_accountreceiptbalance { get; set; }

		[AntiXss]
		public decimal xts_totalpartstaxallocated_base { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public decimal xts_totalworkallocated { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesallocated { get; set; }

		[AntiXss]
		public decimal xts_totalothersalesbaseallocated_base { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public Guid xts_workorderid { get; set; }

		[AntiXss]
		public decimal xts_totalpartsbaseallocated { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_address3 { get; set; }

		[AntiXss]
		public decimal xts_totalworktaxallocated_base { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public decimal xts_totalothersalestaxallocated_base { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_address1 { get; set; }

		[AntiXss]
		public decimal xts_totalpartsallocated { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargeallocated_base { get; set; }

		[AntiXss]
		public Guid xts_serviceproportionalinvoiceid { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid xts_serviceorderid { get; set; }

		[AntiXss]
		public decimal xts_totalworkallocated_base { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public decimal xjp_caliallocated { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargetaxallocated_base { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string xts_billtocustomerphonenumber { get; set; }

		[AntiXss]
		public string xts_serviceorderidname { get; set; }

		[AntiXss]
		public decimal xjp_caliallocated_base { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_caliallocated { get; set; }

		[AntiXss]
		public int xts_duedaysdate { get; set; }

		[AntiXss]
		public decimal xts_totalothersalestaxallocated { get; set; }

		[AntiXss]
		public decimal xts_totalmiscchargetaxallocated { get; set; }

		[AntiXss]
		public int xts_runningnumber { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public bool ktb_isownrisk { get; set; }

		[AntiXss]
		public string ktb_isownriskname { get; set; }

		[AntiXss]
		public Guid xts_withholdingtax2id { get; set; }

		[AntiXss]
		public string xts_withholdingtax2idname { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtax2allocated { get; set; }

		[AntiXss]
		public Guid xts_withholdingtax2iid { get; set; }

		[AntiXss]
		public string xts_withholdingtax2iidname { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtax2allocated_base { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
