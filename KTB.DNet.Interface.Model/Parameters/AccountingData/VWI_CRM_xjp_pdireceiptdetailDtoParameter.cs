#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_pdireceiptdetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:51
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
    public class VWI_CRM_xjp_pdireceiptdetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public decimal xjp_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public decimal xjp_consumptiontax1rate { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xjp_parentbusinessunitidname { get; set; }

		[AntiXss]
		public decimal xjp_consumptiontax1amount { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xjp_accessoriesdescription { get; set; }

		[AntiXss]
		public decimal xjp_consumptiontax2amount { get; set; }

		[AntiXss]
		public decimal xjp_consumptiontax1amount_base { get; set; }

		[AntiXss]
		public Guid xjp_pdireceiptdetailid { get; set; }

		[AntiXss]
		public Guid xjp_parentbusinessunitid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public decimal xjp_totalfee { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xjp_servicefee { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public Guid xjp_consumptiontax2id { get; set; }

		[AntiXss]
		public Guid xjp_pdidetailid { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xjp_kitid { get; set; }

		[AntiXss]
		public string xjp_pdidetailidname { get; set; }

		[AntiXss]
		public Guid xjp_businessunitid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xjp_consumptiontax1id { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public decimal xjp_partfee { get; set; }

		[AntiXss]
		public decimal xjp_totalfee_base { get; set; }

		[AntiXss]
		public Guid xjp_accessoriesid { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xjp_pdireceiptidname { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xjp_accessoriesidname { get; set; }

		[AntiXss]
		public decimal xjp_subcontractfee { get; set; }

		[AntiXss]
		public string xjp_pdireceiptdetailsnumber { get; set; }

		[AntiXss]
		public decimal xjp_receivequantity { get; set; }

		[AntiXss]
		public decimal xjp_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xjp_kitidname { get; set; }

		[AntiXss]
		public decimal xjp_subcontractfee_base { get; set; }

		[AntiXss]
		public decimal xjp_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public decimal xjp_consumptiontax2rate { get; set; }

		[AntiXss]
		public decimal xjp_servicefee_base { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xjp_businessunitidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xjp_partfee_base { get; set; }

		[AntiXss]
		public string xjp_consumptiontax2idname { get; set; }

		[AntiXss]
		public Guid xjp_pdireceiptid { get; set; }

		[AntiXss]
		public string xjp_consumptiontax1idname { get; set; }

		[AntiXss]
		public string xjp_locking { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
