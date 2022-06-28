#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_serviceproportionalinvoiceDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_serviceproportionalinvoiceDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public string xts_billtocustomernumber { get; set; }

		public decimal xts_totalwithholdingtaxallocated { get; set; }

		public Guid xts_withholdingtaxid { get; set; }

		public string xts_billtocustomeridname { get; set; }

		public decimal xts_totalwithholdingtaxallocated_base { get; set; }

		public string statuscodename { get; set; }

		public decimal xts_grandtotalbaseallocated { get; set; }

		public decimal xts_grandtotalbaseallocated_base { get; set; }

		public string modifiedbyyominame { get; set; }

		public string owneridtype { get; set; }

		public Guid createdonbehalfby { get; set; }

		public decimal xts_accountreceiptbalance_base { get; set; }

		public string modifiedbyname { get; set; }

		public decimal xts_grandtotalallocated { get; set; }

		public decimal xts_grandtotaltaxallocated_base { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public Guid xts_billtocustomerid { get; set; }

		public string xts_address2 { get; set; }

		public Guid xts_billtocustomertermofpaymentid { get; set; }

		public decimal xts_grandtotalallocated_base { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string transactioncurrencyidname { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public string xts_workorderidname { get; set; }

		public decimal xts_caliallocated_base { get; set; }

		public string xts_address4 { get; set; }

		public Guid owningteam { get; set; }

		public decimal xts_totalothersalesallocated_base { get; set; }

		public decimal xts_totalothersalesbaseallocated { get; set; }

		public string xts_billtocustomeridyominame { get; set; }

		public decimal xts_grandtotaltaxallocated { get; set; }

		public decimal xts_totalmiscchargeallocated { get; set; }

		public string xts_billtocustomertermofpaymentidname { get; set; }

		public int statecode { get; set; }

		public string owneridname { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public decimal xts_totalmiscchargebaseallocated { get; set; }

		public string xts_locking { get; set; }

		public string ktb_say { get; set; }

		public decimal xts_totalworkbaseallocated_base { get; set; }

		public decimal xts_totalpartstaxallocated { get; set; }

		public string xts_withholdingtaxidname { get; set; }

		public decimal xts_totalpartsallocated_base { get; set; }

		public decimal xts_totalworkbaseallocated { get; set; }

		public decimal xts_totalmiscchargebaseallocated_base { get; set; }

		public decimal xts_totalworktaxallocated { get; set; }

		public decimal xts_totalpartsbaseallocated_base { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_serviceproportionalinvoice { get; set; }

		public decimal xts_accountreceiptbalance { get; set; }

		public decimal xts_totalpartstaxallocated_base { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public decimal xts_totalworkallocated { get; set; }

		public decimal xts_totalothersalesallocated { get; set; }

		public decimal xts_totalothersalesbaseallocated_base { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public Guid xts_workorderid { get; set; }

		public decimal xts_totalpartsbaseallocated { get; set; }

		public string createdbyname { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_address3 { get; set; }

		public decimal xts_totalworktaxallocated_base { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public decimal xts_totalothersalestaxallocated_base { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_address1 { get; set; }

		public decimal xts_totalpartsallocated { get; set; }

		public decimal xts_totalmiscchargeallocated_base { get; set; }

		public Guid xts_serviceproportionalinvoiceid { get; set; }

		public string createdbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public Guid xts_serviceorderid { get; set; }

		public decimal xts_totalworkallocated_base { get; set; }

		public Guid owningbusinessunit { get; set; }

		public decimal xjp_caliallocated { get; set; }

		public decimal xts_totalmiscchargetaxallocated_base { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public string xts_billtocustomerphonenumber { get; set; }

		public string xts_serviceorderidname { get; set; }

		public decimal xjp_caliallocated_base { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public decimal xts_caliallocated { get; set; }

		public int xts_duedaysdate { get; set; }

		public decimal xts_totalothersalestaxallocated { get; set; }

		public decimal xts_totalmiscchargetaxallocated { get; set; }

		public int xts_runningnumber { get; set; }

		public string statecodename { get; set; }

		public string xts_businessunitidname { get; set; }

		public bool ktb_isownrisk { get; set; }

		public string ktb_isownriskname { get; set; }

		public Guid xts_withholdingtax2id { get; set; }

		public string xts_withholdingtax2idname { get; set; }

		public decimal xts_totalwithholdingtax2allocated { get; set; }

		public Guid xts_withholdingtax2iid { get; set; }

		public string xts_withholdingtax2iidname { get; set; }

		public decimal xts_totalwithholdingtax2allocated_base { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
