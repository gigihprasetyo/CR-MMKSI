#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productclassParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:26:25
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
    public class VWI_CRM_xts_productclassParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public Guid xts_cogsdimension7id { get; set; }

		[AntiXss]
		public Guid xts_goodsintransitdimension2id { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension4id { get; set; }

		[AntiXss]
		public Guid xts_defaultunitid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid xts_inventorydimension4id { get; set; }

		[AntiXss]
		public Guid xts_discountdimension6id { get; set; }

		[AntiXss]
		public Guid xts_salesdimension3id { get; set; }

		[AntiXss]
		public string xts_salesdimension10idname { get; set; }

		[AntiXss]
		public Guid xts_discountdimension10id { get; set; }

		[AntiXss]
		public string xts_goodsintransitdimension7idname { get; set; }

		[AntiXss]
		public bool xts_includetransferorder { get; set; }

		[AntiXss]
		public Guid xts_goodsintransitdimension4id { get; set; }

		[AntiXss]
		public Guid xts_goodsintransitdimension5id { get; set; }

		[AntiXss]
		public string xts_defaultsalesunitidname { get; set; }

		[AntiXss]
		public string xts_discountdimension4idname { get; set; }

		[AntiXss]
		public string xts_cogsdimension6idname { get; set; }

		[AntiXss]
		public string xts_discountdimension7idname { get; set; }

		[AntiXss]
		public string xts_cogsdimension5idname { get; set; }

		[AntiXss]
		public string xts_inventorydimension7idname { get; set; }

		[AntiXss]
		public string xts_salesdimension4idname { get; set; }

		[AntiXss]
		public Guid xts_goodsintransitdimension10id { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public Guid xts_inventorydimension3id { get; set; }

		[AntiXss]
		public string xts_goodsintransitdimension1idname { get; set; }

		[AntiXss]
		public Guid xts_trackingdimensiongroupid { get; set; }

		[AntiXss]
		public string xts_defaultunitidname { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension10id { get; set; }

		[AntiXss]
		public Guid xts_discountdimension1id { get; set; }

		[AntiXss]
		public string xts_discountdimension8idname { get; set; }

		[AntiXss]
		public string xts_inventorydimension6idname { get; set; }

		[AntiXss]
		public Guid xts_inventorydimension9id { get; set; }

		[AntiXss]
		public Guid xts_inventorydimension2id { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public Guid xts_discountdimension8id { get; set; }

		[AntiXss]
		public string xts_salesdimension8idname { get; set; }

		[AntiXss]
		public string xts_salesdimension3idname { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension9id { get; set; }

		[AntiXss]
		public Guid xts_salesdimension4id { get; set; }

		[AntiXss]
		public string xts_goodsintransitdimension6idname { get; set; }

		[AntiXss]
		public Guid xts_discountdimension7id { get; set; }

		[AntiXss]
		public Guid xts_inventorydimension8id { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public Guid xts_defaultsalesunitid { get; set; }

		[AntiXss]
		public string xts_cogsdimension1idname { get; set; }

		[AntiXss]
		public string xts_includepurchaseordername { get; set; }

		[AntiXss]
		public Guid xts_inventorydimension5id { get; set; }

		[AntiXss]
		public Guid xts_salesdimension5id { get; set; }

		[AntiXss]
		public string xts_entitytag { get; set; }

		[AntiXss]
		public string bsi_roundingname { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension6id { get; set; }

		[AntiXss]
		public Guid xts_goodsintransitdimension8id { get; set; }

		[AntiXss]
		public string xts_storagedimensiongroupidname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid xts_goodsintransitdimension9id { get; set; }

		[AntiXss]
		public Guid xts_discountdimension2id { get; set; }

		[AntiXss]
		public string xts_discountdimension6idname { get; set; }

		[AntiXss]
		public string xts_deductworkordername { get; set; }

		[AntiXss]
		public Guid xts_salesdimension10id { get; set; }

		[AntiXss]
		public Guid xts_goodsintransitdimension7id { get; set; }

		[AntiXss]
		public Guid xts_goodsintransitdimension6id { get; set; }

		[AntiXss]
		public string xts_cogsdimension9idname { get; set; }

		[AntiXss]
		public string xts_includetransferordername { get; set; }

		[AntiXss]
		public string xts_deductsalesordername { get; set; }

		[AntiXss]
		public string xts_goodsintransitdimension9idname { get; set; }

		[AntiXss]
		public string xts_goodsintransitdimension2idname { get; set; }

		[AntiXss]
		public bool xts_deductsalesorder { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_discountdimension5idname { get; set; }

		[AntiXss]
		public string xts_goodsintransitdimension4idname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_salesdimension5idname { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_productdimensiongroupidname { get; set; }

		[AntiXss]
		public string xts_trackingdimensiongroupidname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_inventorydimension10idname { get; set; }

		[AntiXss]
		public string xts_salesdimension2idname { get; set; }

		[AntiXss]
		public string xts_cogsdimension7idname { get; set; }

		[AntiXss]
		public string xts_cogsdimension4idname { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension5id { get; set; }

		[AntiXss]
		public string xts_goodsintransitdimension3idname { get; set; }

		[AntiXss]
		public string xts_inventorydimension2idname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xts_discountdimension9id { get; set; }

		[AntiXss]
		public string xts_inventorydimension1idname { get; set; }

		[AntiXss]
		public Guid xts_discountdimension4id { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_cogsdimension2idname { get; set; }

		[AntiXss]
		public string xts_salesdimension6idname { get; set; }

		[AntiXss]
		public Guid xts_inventorydimension1id { get; set; }

		[AntiXss]
		public string xts_inventorydimension5idname { get; set; }

		[AntiXss]
		public string xts_goodsintransitdimension8idname { get; set; }

		[AntiXss]
		public Guid xts_salesdimension1id { get; set; }

		[AntiXss]
		public Guid xts_defaultpurchaseunitid { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_deductreservationname { get; set; }

		[AntiXss]
		public string xts_inventorydimension4idname { get; set; }

		[AntiXss]
		public Guid xts_storagedimensiongroupid { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public string xts_cogsdimension3idname { get; set; }

		[AntiXss]
		public bool bsi_rounding { get; set; }

		[AntiXss]
		public Guid xts_goodsintransitdimension3id { get; set; }

		[AntiXss]
		public string xts_defaultpurchaseunitidname { get; set; }

		[AntiXss]
		public Guid xts_salesdimension6id { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension2id { get; set; }

		[AntiXss]
		public Guid xts_goodsintransitdimension1id { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension3id { get; set; }

		[AntiXss]
		public string xts_salesdimension9idname { get; set; }

		[AntiXss]
		public bool xts_includepurchaseorder { get; set; }

		[AntiXss]
		public string xts_inventorydimension9idname { get; set; }

		[AntiXss]
		public string xts_inventorymodelgroupidname { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension8id { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_salesdimension1idname { get; set; }

		[AntiXss]
		public Guid xts_salesdimension7id { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_cogsdimension8idname { get; set; }

		[AntiXss]
		public string xts_virtualcompany { get; set; }

		[AntiXss]
		public string xts_discountdimension1idname { get; set; }

		[AntiXss]
		public bool xts_deductworkorder { get; set; }

		[AntiXss]
		public Guid xts_inventorymodelgroupid { get; set; }

		[AntiXss]
		public string xts_discountdimension10idname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_discountdimension2idname { get; set; }

		[AntiXss]
		public string xts_postingclassidname { get; set; }

		[AntiXss]
		public Guid xts_inventorydimension7id { get; set; }

		[AntiXss]
		public Guid xts_postingclassid { get; set; }

		[AntiXss]
		public string xts_goodsintransitdimension10idname { get; set; }

		[AntiXss]
		public Guid xts_discountdimension3id { get; set; }

		[AntiXss]
		public Guid xts_productclassid { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension1id { get; set; }

		[AntiXss]
		public Guid xts_discountdimension5id { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public bool xts_deductreservation { get; set; }

		[AntiXss]
		public string xts_productclass { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string xts_salesdimension7idname { get; set; }

		[AntiXss]
		public string xts_discountdimension3idname { get; set; }

		[AntiXss]
		public Guid xts_inventorydimension6id { get; set; }

		[AntiXss]
		public Guid xts_salesdimension8id { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_inventorydimension3idname { get; set; }

		[AntiXss]
		public Guid xts_productdimensiongroupid { get; set; }

		[AntiXss]
		public string xts_inventorydimension8idname { get; set; }

		[AntiXss]
		public string xts_discountdimension9idname { get; set; }

		[AntiXss]
		public Guid xts_salesdimension2id { get; set; }

		[AntiXss]
		public Guid xts_inventorydimension10id { get; set; }

		[AntiXss]
		public string xts_goodsintransitdimension5idname { get; set; }

		[AntiXss]
		public string xts_cogsdimension10idname { get; set; }

		[AntiXss]
		public Guid xts_salesdimension9id { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
