#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_uvsoreferralinformationParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 08:54:00
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
    public class VWI_CRM_xts_uvsoreferralinformationParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public string xts_uvsoreferralinformation { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public Guid xts_consumptiontaxid { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string xts_methodofpaymentidname { get; set; }
        [AntiXss]
        public string xjp_accounttypename { get; set; }
        [AntiXss]
        public Guid xts_reasonid { get; set; }
        [AntiXss]
        public string xts_uvsqreferralinformationlineidname { get; set; }
        [AntiXss]
        public string xts_consumptiontaxidname { get; set; }
        [AntiXss]
        public decimal xts_totalpaymentamount { get; set; }
        [AntiXss]
        public decimal xts_commission { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string xts_referralidname { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public Guid xts_salesorderid { get; set; }
        [AntiXss]
        public string xts_salesorderidname { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public Guid xts_referralid { get; set; }
        [AntiXss]
        public string xts_referraldescription { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public string xts_apvoucheridname { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public decimal xts_balance { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public Guid xts_uvsqreferralinformationlineid { get; set; }
        [AntiXss]
        public decimal xts_totalpaymentamount_base { get; set; }
        [AntiXss]
        public Guid xts_parentbusinessunitid { get; set; }
        [AntiXss]
        public decimal xts_commission_base { get; set; }
        [AntiXss]
        public decimal xts_consumptiontaxamount { get; set; }
        [AntiXss]
        public decimal xts_totalamount_base { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_referralidyominame { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public decimal xts_consumptiontaxamount_base { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public Guid xts_methodofpaymentid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public decimal xts_balance_base { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public Guid xts_referralvendorid { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public string xts_reasonidname { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string xts_accountnumber { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string xts_parentbusinessunitidname { get; set; }
        [AntiXss]
        public decimal xts_totalamount { get; set; }
        [AntiXss]
        public Guid xts_bankid { get; set; }
        [AntiXss]
        public string xjp_accounttype { get; set; }
        [AntiXss]
        public string xts_referralvendoridname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public Guid xts_apvoucherid { get; set; }
        [AntiXss]
        public Guid xts_uvsoreferralinformationid { get; set; }
        [AntiXss]
        public string xts_bankidname { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
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
