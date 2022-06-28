#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_consumptiontaxParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:26:07
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
    public class VWI_CRM_xts_consumptiontaxParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string xts_taxdimension8idname { get; set; }

		[AntiXss]
		public Guid xts_taxaccountid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_consumptiontaxid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_taxdimension6fromname { get; set; }

		[AntiXss]
		public string xts_taxdimension9idname { get; set; }

		[AntiXss]
		public string xts_taxdimension2from { get; set; }

		[AntiXss]
		public Guid xts_taxdimension9id { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_roundingmethodname { get; set; }

		[AntiXss]
		public Guid xts_taxdimension3id { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_taxdimension10idname { get; set; }

		[AntiXss]
		public string xts_taxdimension1fromname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_taxdimension5fromname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_taxdimension2id { get; set; }

		[AntiXss]
		public string xts_taxtypename { get; set; }

		[AntiXss]
		public string xts_taxdimension3from { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_taxdimension6idname { get; set; }

		[AntiXss]
		public string xts_taxdimension3idname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_taxtype { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string ktb_bucompanyname { get; set; }

		[AntiXss]
		public Guid xts_taxdimension1id { get; set; }

		[AntiXss]
		public string xts_taxdimension1idname { get; set; }

		[AntiXss]
		public string xts_consumptiontax { get; set; }

		[AntiXss]
		public string xts_taxdimension6from { get; set; }

		[AntiXss]
		public string xts_taxdimension4fromname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_taxdimension3fromname { get; set; }

		[AntiXss]
		public string xts_description2 { get; set; }

		[AntiXss]
		public Guid xts_taxdimension7id { get; set; }

		[AntiXss]
		public string xts_taxdimension2fromname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_taxdimension4idname { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_taxdimension4from { get; set; }

		[AntiXss]
		public Guid xts_taxdimension6id { get; set; }

		[AntiXss]
		public Guid xts_taxdimension4id { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_roundingmethod { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xts_taxdimension5id { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xts_taxdimension8id { get; set; }

		[AntiXss]
		public string xts_basecalculation { get; set; }

		[AntiXss]
		public string xts_taxdimension5from { get; set; }

		[AntiXss]
		public string xts_taxdimension2idname { get; set; }

		[AntiXss]
		public string xts_taxdimension7idname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_company { get; set; }

		[AntiXss]
		public Guid xts_taxdimension10id { get; set; }

		[AntiXss]
		public string xts_basecalculationname { get; set; }

		[AntiXss]
		public string xts_taxdimension1from { get; set; }

		[AntiXss]
		public string xts_taxdimension5idname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public Guid ktb_bucompany { get; set; }

		[AntiXss]
		public string xts_taxaccountidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
