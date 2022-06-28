#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productvariantParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
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
	public class VWI_CRM_xts_productvariantParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
		public string xts_sharedproductvariantidname { get; set; }
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }
		[AntiXss]
		public string xts_styleidname { get; set; }
		[AntiXss]
		public string transactioncurrencyidname { get; set; }
		[AntiXss]
		public string xts_productidname { get; set; }
		[AntiXss]
		public Int32 statuscode { get; set; }
		[AntiXss]
		public Guid transactioncurrencyid { get; set; }
		[AntiXss]
		public decimal xts_additionalvariantprice { get; set; }
		[AntiXss]
		public Guid createdonbehalfby { get; set; }
		[AntiXss]
		public DateTime overriddencreatedon { get; set; }
		[AntiXss]
		public Guid xts_productvariantid { get; set; }
		[AntiXss]
		public Guid xts_styleid { get; set; }
		[AntiXss]
		public Guid xts_interiorcolorid { get; set; }
		[AntiXss]
		public Int32 importsequencenumber { get; set; }
		[AntiXss]
		public Guid organizationid { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }
		[AntiXss]
		public string organizationidname { get; set; }
		[AntiXss]
		public Guid xts_configurationid { get; set; }
		[AntiXss]
		public decimal xts_additionalvariantpurchaseprice { get; set; }
		[AntiXss]
		public string xts_configurationidname { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }
		[AntiXss]
		public Int32 utcconversiontimezonecode { get; set; }
		[AntiXss]
		public string createdbyyominame { get; set; }
		[AntiXss]
		public string modifiedbyname { get; set; }
		[AntiXss]
		public Int64 versionnumber { get; set; }
		[AntiXss]
		public Guid modifiedby { get; set; }
		[AntiXss]
		public string modifiedbyyominame { get; set; }
		[AntiXss]
		public string xts_company { get; set; }
		[AntiXss]
		public Int32 timezoneruleversionnumber { get; set; }
		[AntiXss]
		public string xts_productvariant { get; set; }
		[AntiXss]
		public string statuscodename { get; set; }
		[AntiXss]
		public Guid xts_exteriorcolorid { get; set; }
		[AntiXss]
		public Guid xts_sharedproductvariantid { get; set; }
		[AntiXss]
		public DateTime modifiedon { get; set; }
		[AntiXss]
		public decimal exchangerate { get; set; }
		[AntiXss]
		public Int32 statecode { get; set; }
		[AntiXss]
		public string xts_interiorcoloridname { get; set; }
		[AntiXss]
		public string statecodename { get; set; }
		[AntiXss]
		public string createdbyname { get; set; }
		[AntiXss]
		public DateTime createdon { get; set; }
		[AntiXss]
		public string xts_entitytag { get; set; }
		[AntiXss]
		public string xts_exteriorcoloridname { get; set; }
		[AntiXss]
		public Guid createdby { get; set; }
		[AntiXss]
		public string xts_description { get; set; }
		[AntiXss]
		public decimal xts_additionalvariantpurchaseprice_base { get; set; }
		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }
		[AntiXss]
		public Guid xts_productid { get; set; }
		[AntiXss]
		public string createdonbehalfbyname { get; set; }
		[AntiXss]
		public decimal xts_additionalvariantprice_base { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}
