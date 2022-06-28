#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_customerclassParameterDto  class
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
    public class VWI_CRM_xts_customerclassParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string xts_accountreceiveabledim5from { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public bool ktb_requiredfleetcode { get; set; }

		[AntiXss]
		public string ktb_requiredfleetcodename { get; set; }

		[AntiXss]
		public Guid xts_dimension8id { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension1idname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension3fromname { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension2id { get; set; }

		[AntiXss]
		public string xts_customerclasstypename { get; set; }

		[AntiXss]
		public Guid xts_dimension5id { get; set; }

		[AntiXss]
		public Guid xts_creditwriteoffaccountid { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension5fromname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension3idname { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim2fromname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension6idname { get; set; }

		[AntiXss]
		public string xts_prepaymentaccountidname { get; set; }

		[AntiXss]
		public Guid xts_dimension2id { get; set; }

		[AntiXss]
		public string xts_dimension7idname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim2from { get; set; }

		[AntiXss]
		public string xts_dimension5idname { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension3id { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim6fromname { get; set; }

		[AntiXss]
		public string xts_writeoffaccountidname { get; set; }

		[AntiXss]
		public Guid xts_dimension4id { get; set; }

		[AntiXss]
		public Guid xts_prepaymentaccountid { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim3fromname { get; set; }

		[AntiXss]
		public string xts_salesaccountidname { get; set; }

		[AntiXss]
		public string xts_company { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim3from { get; set; }

		[AntiXss]
		public string xts_discountaccountidname { get; set; }

		[AntiXss]
		public string xts_dimension10idname { get; set; }

		[AntiXss]
		public Guid xts_discountaccountid { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension1from { get; set; }

		[AntiXss]
		public string xts_subsidizedaccountidname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension4idname { get; set; }

		[AntiXss]
		public string ktb_interfacetodnetname { get; set; }

		[AntiXss]
		public Guid xts_subsidizedaccountid { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension3from { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim1from { get; set; }

		[AntiXss]
		public string xts_customerclasstype { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension2from { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension6fromname { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim4fromname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim4from { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension5from { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension4fromname { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension5id { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension5idname { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public Guid xts_dimension6id { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xts_dimension7id { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_dimension2idname { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim5fromname { get; set; }

		[AntiXss]
		public string xts_creditwriteoffaccountidname { get; set; }

		[AntiXss]
		public string xts_dimension3idname { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension2idname { get; set; }

		[AntiXss]
		public string xts_dimension6idname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension1fromname { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension4id { get; set; }

		[AntiXss]
		public Guid xts_writeoffaccountid { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension6id { get; set; }

		[AntiXss]
		public string xts_dimension8idname { get; set; }

		[AntiXss]
		public bool ktb_interfacetodnet { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid xts_araccountid { get; set; }

		[AntiXss]
		public string xts_dimension1idname { get; set; }

		[AntiXss]
		public Guid xts_dimension10id { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_dimension4idname { get; set; }

		[AntiXss]
		public string xts_dimension9idname { get; set; }

		[AntiXss]
		public Guid xts_dimension3id { get; set; }

		[AntiXss]
		public Guid xts_customerclassid { get; set; }

		[AntiXss]
		public Guid xts_dimension9id { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim6from { get; set; }

		[AntiXss]
		public string xts_araccountidname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension6from { get; set; }

		[AntiXss]
		public string xts_customerclass { get; set; }

		[AntiXss]
		public Guid xts_salesaccountid { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension2fromname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension1id { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension4from { get; set; }

		[AntiXss]
		public Guid xts_dimension1id { get; set; }

		[AntiXss]
		public string xts_accountreceiveabledim1fromname { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
