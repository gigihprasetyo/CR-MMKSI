#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vendorclassParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:26:38
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
    public class VWI_CRM_xts_vendorclassParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_pricevariancedimension6id { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension5idname { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid xts_dimension8id { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension2fromname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension3fromname { get; set; }

		[AntiXss]
		public Guid xts_accountpayableaccruegitaccountid { get; set; }

		[AntiXss]
		public string xts_downpaymentaccountidname { get; set; }

		[AntiXss]
		public Guid xts_pricevariancedimension1id { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension2id { get; set; }

		[AntiXss]
		public Guid xts_dimension5id { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension6from { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension5from { get; set; }

		[AntiXss]
		public Guid xts_advtitleregfeedimension6id { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension5fromname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension3idname { get; set; }

		[AntiXss]
		public Guid xts_pricevariancedimension5id { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension6idname { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension3from { get; set; }

		[AntiXss]
		public Guid xts_dimension7id { get; set; }

		[AntiXss]
		public Guid xts_dimension2id { get; set; }

		[AntiXss]
		public string xts_dimension7idname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_dimension4id { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension1idname { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension3id { get; set; }

		[AntiXss]
		public string xts_accountpayableaccruegitaccountidname { get; set; }

		[AntiXss]
		public Guid xts_downpaymentaccountid { get; set; }

		[AntiXss]
		public Guid xts_pricevariancedimension2id { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_accountpayableaccountidname { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension2from { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension5from { get; set; }

		[AntiXss]
		public string xts_writeoffaccountidname { get; set; }

		[AntiXss]
		public Guid xts_pricevariancedimension4id { get; set; }

		[AntiXss]
		public Guid xts_advtitleregfeedimension4id { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension2from { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension4fromname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension1from { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension5fromname { get; set; }

		[AntiXss]
		public string xts_company { get; set; }

		[AntiXss]
		public Guid xts_advtitleregfeedimension1id { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_vendorclass { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension6idname { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension2fromname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension6from { get; set; }

		[AntiXss]
		public string xts_accountpayableaccrueaccountidname { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension3fromname { get; set; }

		[AntiXss]
		public Guid xts_writeoffaccountid { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension2idname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension1from { get; set; }

		[AntiXss]
		public string xts_advtitleregfeedimension4idname { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension3fromname { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension4from { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension4fromname { get; set; }

		[AntiXss]
		public string xts_vendortypename { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension1fromname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension3from { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension6fromname { get; set; }

		[AntiXss]
		public Guid xts_dimension1id { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension2from { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension5fromname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension1idname { get; set; }

		[AntiXss]
		public string xts_dimension5idname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension5from { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension4idname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension4fromname { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension5id { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension5idname { get; set; }

		[AntiXss]
		public Guid xts_vendorclassid { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public Guid xts_dimension6id { get; set; }

		[AntiXss]
		public Guid xts_advtitleregfeedimension5id { get; set; }

		[AntiXss]
		public string xts_accountpayabledimension1from { get; set; }

		[AntiXss]
		public string xts_dimension2idname { get; set; }

		[AntiXss]
		public string xts_dimension10idname { get; set; }

		[AntiXss]
		public Guid xts_pricevarianceaccountid { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension6fromname { get; set; }

		[AntiXss]
		public string xts_advtitleregfeedimension6idname { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension6from { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_advancetitleregistrationfeeaccountid { get; set; }

		[AntiXss]
		public string xts_pricevarianceaccountidname { get; set; }

		[AntiXss]
		public string xts_dimension6idname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_advtitleregfeedimension2idname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension1fromname { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension4id { get; set; }

		[AntiXss]
		public Guid xts_pricevariancedimension3id { get; set; }

		[AntiXss]
		public Guid xts_advtitleregfeedimension3id { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension6id { get; set; }

		[AntiXss]
		public string xts_dimension8idname { get; set; }

		[AntiXss]
		public Guid xts_advtitleregfeedimension2id { get; set; }

		[AntiXss]
		public Guid xts_accountpayableaccrueaccountid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension3idname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_advtitleregfeedimension1idname { get; set; }

		[AntiXss]
		public Guid xts_dimension10id { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_dimension4idname { get; set; }

		[AntiXss]
		public string xts_dimension3idname { get; set; }

		[AntiXss]
		public string xts_dimension9idname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_dimension3id { get; set; }

		[AntiXss]
		public string xts_vendortype { get; set; }

		[AntiXss]
		public string xts_dimension1idname { get; set; }

		[AntiXss]
		public Guid xts_dimension9id { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension2idname { get; set; }

		[AntiXss]
		public Guid xts_accountpayableaccountid { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension3from { get; set; }

		[AntiXss]
		public string xts_advtitleregfeedimension5idname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension2fromname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension4from { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension1id { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension4from { get; set; }

		[AntiXss]
		public string xts_advtitleregfeedimension3idname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension6fromname { get; set; }

		[AntiXss]
		public string xts_pricevariancedimension1fromname { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension4idname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_advancetitleregistrationfeeaccountidname { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
