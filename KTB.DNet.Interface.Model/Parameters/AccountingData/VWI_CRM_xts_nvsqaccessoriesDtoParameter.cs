#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_nvsqaccessoriesParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:26:21
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
    public class VWI_CRM_xts_nvsqaccessoriesParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_consumptiontaxid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_pluginflag { get; set; }

		[AntiXss]
		public decimal xts_unitprice { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesestimatedcost { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_nvsqaccessoriesid { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public string xts_mandatoryaccessoriesname { get; set; }

		[AntiXss]
		public decimal xts_transactionamount_base { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public string xts_accessoriesinstallationcategoryidname { get; set; }

		[AntiXss]
		public string xts_pricetype { get; set; }

		[AntiXss]
		public decimal xts_totalprice { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount_base { get; set; }

		[AntiXss]
		public Guid xts_newvehiclesalesquoteid { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal xts_unitprice_base { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public decimal xts_consumptiontaxamount { get; set; }

		[AntiXss]
		public decimal xjp_acquisitiontaxprice { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount { get; set; }

		[AntiXss]
		public decimal xts_quantity { get; set; }

		[AntiXss]
		public Guid xts_accessoriesid { get; set; }

		[AntiXss]
		public Guid xts_accessoriesinstallationcategoryid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_accessoriesidname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public decimal xts_totalprice_base { get; set; }

		[AntiXss]
		public string xts_freetype { get; set; }

		[AntiXss]
		public decimal xts_discountamount { get; set; }

		[AntiXss]
		public Guid xts_xts_nvsqaccessoryid { get; set; }

		[AntiXss]
		public string xts_newvehiclesalesquoteidname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public decimal xts_consumptiontaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesestimatedcost_base { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_pricetypename { get; set; }

		[AntiXss]
		public decimal xts_discountamount_base { get; set; }

		[AntiXss]
		public string xts_consumptiontaxidname { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public bool xts_mandatoryaccessories { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public Guid xts_locationid { get; set; }

		[AntiXss]
		public string xts_xts_nvsqaccessoryidname { get; set; }

		[AntiXss]
		public string xts_nvsqaccessoriesnumber { get; set; }

		[AntiXss]
		public decimal xts_transactionamount { get; set; }

		[AntiXss]
		public Guid xts_vendorid { get; set; }

		[AntiXss]
		public decimal xjp_acquisitiontaxprice_base { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public string xts_freetypename { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xts_locationidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string xts_vendoridname { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
