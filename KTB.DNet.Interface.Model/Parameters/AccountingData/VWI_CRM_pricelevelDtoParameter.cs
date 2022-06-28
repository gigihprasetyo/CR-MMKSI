#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_equipmentParameterDto  class
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
	public class VWI_CRM_pricelevelParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }
		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }
		[AntiXss]
		public string versiotransactioncurrencyidnamennumber { get; set; }
		[AntiXss]
		public string freighttermscode { get; set; }
		[AntiXss]
		public DateTime enddate { get; set; }
		[AntiXss]
		public string description { get; set; }
		[AntiXss]
		public string statecodename { get; set; }
		[AntiXss]
		public DateTime begindate { get; set; }
		[AntiXss]
		public Guid createdonbehalfby { get; set; }
		[AntiXss]
		public Guid transactioncurrencyid { get; set; }
		[AntiXss]
		public string name { get; set; }
		[AntiXss]
		public Guid pricelevelid { get; set; }
		[AntiXss]
		public Int32 importsequencenumber { get; set; }
		[AntiXss]
		public string organizationidname { get; set; }
		[AntiXss]
		public Int32 statecode { get; set; }
		[AntiXss]
		public string shippingmethodcode { get; set; }
		[AntiXss]
		public string paymentmethodcode { get; set; }
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
		public Guid createdby { get; set; }
		[AntiXss]
		public Int32 timezoneruleversionnumber { get; set; }
		[AntiXss]
		public string shippingmethodcodename { get; set; }
		[AntiXss]
		public string statuscodename { get; set; }
		[AntiXss]
		public string paymentmethodcodename { get; set; }
		[AntiXss]
		public string freighttermscodename { get; set; }
		[AntiXss]
		public DateTime modifiedon { get; set; }
		[AntiXss]
		public decimal exchangerate { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }
		[AntiXss]
		public Int32 statuscode { get; set; }
		[AntiXss]
		public string createdbyname { get; set; }
		[AntiXss]
		public DateTime createdon { get; set; }
		[AntiXss]
		public Guid organizationid { get; set; }
		[AntiXss]
		public string createdonbehalfbyname { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }
		[AntiXss]
		public DateTime overriddencreatedon { get; set; }
		
		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}
