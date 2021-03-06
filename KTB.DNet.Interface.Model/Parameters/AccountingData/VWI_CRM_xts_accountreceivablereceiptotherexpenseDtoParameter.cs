#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountreceivablereceiptotherexpenseParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:53
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
    public class VWI_CRM_xts_accountreceivablereceiptotherexpenseParameterDto : ParameterDtoBase, IValidatableObject
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
		public string ktb_invoiceidname { get; set; }

		[AntiXss]
		public Guid xts_dimension8id { get; set; }

		[AntiXss]
		public string xts_invoiceidname { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string ktb_type { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_dimension3idname { get; set; }

		[AntiXss]
		public Guid xts_dimension5id { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_dimension4idname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_dimension2id { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_dimension6idname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid ktb_invoiceid { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_reasoncodeidname { get; set; }

		[AntiXss]
		public Guid xts_dimension4id { get; set; }

		[AntiXss]
		public Guid xts_invoiceid { get; set; }

		[AntiXss]
		public DateTime xts_exchangeratedate { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xts_accountreceivablereceiptotherexpenseid { get; set; }

		[AntiXss]
		public decimal xts_exchangerateamount { get; set; }

		[AntiXss]
		public string xts_dimension10idname { get; set; }

		[AntiXss]
		public Guid xts_reasoncodeid { get; set; }

		[AntiXss]
		public string xts_exchangeratetypename { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_accountreceivablereceiptidname { get; set; }

		[AntiXss]
		public Guid xts_dimension1id { get; set; }

		[AntiXss]
		public decimal xts_otherexpenseamount_base { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_dimension5idname { get; set; }

		[AntiXss]
		public decimal xts_otherexpenseamount { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public Guid xts_dimension6id { get; set; }

		[AntiXss]
		public Guid xts_dimension7id { get; set; }

		[AntiXss]
		public string xts_dimension2idname { get; set; }

		[AntiXss]
		public string xts_otherexpenseaccountidname { get; set; }

		[AntiXss]
		public string xts_dimension8idname { get; set; }

		[AntiXss]
		public Guid xts_accountreceivablereceiptid { get; set; }

		[AntiXss]
		public string xts_accountreceivablereceiptotherexpense { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_typename { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xts_otherexpenseaccountid { get; set; }

		[AntiXss]
		public string xts_dimension7idname { get; set; }

		[AntiXss]
		public Guid xts_dimension10id { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_dimension9idname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_dimension1idname { get; set; }

		[AntiXss]
		public Guid xts_dimension9id { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_exchangeratetype { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public Guid xts_dimension3id { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
