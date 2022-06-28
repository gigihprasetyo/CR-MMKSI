#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productParameterDto  class
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
    public class VWI_CRM_xts_productParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public Guid xts_gradeid { get; set; }

		[AntiXss]
		public decimal xjp_storecostrate { get; set; }

		[AntiXss]
		public string bsi_productclassroundingname { get; set; }

		[AntiXss]
		public Guid xts_defaultunitid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string xts_manufacturer { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_salesunitid { get; set; }

		[AntiXss]
		public decimal xts_purchaseprice_base { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal ktb_latestpurchaseprice_base { get; set; }

		[AntiXss]
		public decimal xts_servicefee_base { get; set; }

		[AntiXss]
		public string xjp_weighttaxreduction { get; set; }

		[AntiXss]
		public Guid xts_sharedproductid { get; set; }

		[AntiXss]
		public int xts_productweight { get; set; }

		[AntiXss]
		public bool xts_axproducttype { get; set; }

		[AntiXss]
		public decimal xjp_hqexpense_base { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_chassismodel { get; set; }

		[AntiXss]
		public decimal xjp_additionalfluorocarbonfee_base { get; set; }

		[AntiXss]
		public string ktb_indicator { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public decimal xjp_airbagrates { get; set; }

		[AntiXss]
		public string xjp_automobiletaxreduction { get; set; }

		[AntiXss]
		public decimal xjp_additionalfluorocarbonfee { get; set; }

		[AntiXss]
		public string xts_company { get; set; }

		[AntiXss]
		public decimal xjp_shredderdustfee_base { get; set; }

		[AntiXss]
		public string xts_defaultunitidname { get; set; }

		[AntiXss]
		public decimal xjp_additionalairbagfee_base { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public string xts_methodforsalesordername { get; set; }

		[AntiXss]
		public Guid xts_productsegment2id { get; set; }

		[AntiXss]
		public string xts_defaultvehiclebrandidname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public bool xts_stoppedforsales { get; set; }

		[AntiXss]
		public string xts_stoppedforpurchasename { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xjp_acquisitiontaxreduction { get; set; }

		[AntiXss]
		public decimal xts_partprice { get; set; }

		[AntiXss]
		public decimal xts_stockbaseprice { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_internalnumber { get; set; }

		[AntiXss]
		public bool bsi_productclassrounding { get; set; }

		[AntiXss]
		public string ktb_fixedpricename { get; set; }

		[AntiXss]
		public string xts_entitytag { get; set; }

		[AntiXss]
		public string xts_sharedproductidname { get; set; }

		[AntiXss]
		public string xts_description2 { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_classificationnumber { get; set; }

		[AntiXss]
		public string xts_methodforworkordername { get; set; }

		[AntiXss]
		public Guid xts_productsegment4id { get; set; }

		[AntiXss]
		public decimal xjp_airbagrates_base { get; set; }

		[AntiXss]
		public bool xts_stoppedforinventory { get; set; }

		[AntiXss]
		public Guid bsi_producttypeid { get; set; }

		[AntiXss]
		public string xts_methodforworkorder { get; set; }

		[AntiXss]
		public string xts_defaultbrandgeneration { get; set; }

		[AntiXss]
		public string xts_gradeidname { get; set; }

		[AntiXss]
		public string ktb_sourcename { get; set; }

		[AntiXss]
		public DateTime ktb_latestpurchasepricedate { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public Guid xts_productsegment1id { get; set; }

		[AntiXss]
		public string xts_purchaseunitidname { get; set; }

		[AntiXss]
		public decimal xjp_shredderdustfee { get; set; }

		[AntiXss]
		public Guid xts_defaultvehiclebrandid { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public decimal xts_inventoryprice_base { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_kittypename { get; set; }

		[AntiXss]
		public string xts_salesunitidname { get; set; }

		[AntiXss]
		public decimal ktb_latestpurchaseprice { get; set; }

		[AntiXss]
		public int xts_productvolume { get; set; }

		[AntiXss]
		public string ktb_categoryname { get; set; }

		[AntiXss]
		public bool xjp_lightcarflag { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public decimal xts_partprice_base { get; set; }

		[AntiXss]
		public string xts_productsegment2idname { get; set; }

		[AntiXss]
		public decimal xjp_fluorocarbonfee { get; set; }

		[AntiXss]
		public string xts_productclassidname { get; set; }

		[AntiXss]
		public string xts_stoppedforsalesname { get; set; }

		[AntiXss]
		public Guid ktb_vehiclemodelid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_defaultbrand { get; set; }

		[AntiXss]
		public string bsi_producttypeidname { get; set; }

		[AntiXss]
		public Guid xts_productsegment3id { get; set; }

		[AntiXss]
		public string xts_generation { get; set; }

		[AntiXss]
		public string xts_product { get; set; }

		[AntiXss]
		public Guid xts_productclassid { get; set; }

		[AntiXss]
		public decimal xts_servicefee { get; set; }

		[AntiXss]
		public string xts_productsegment4idname { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string ktb_vehicletypecode { get; set; }

		[AntiXss]
		public string xts_recordtype { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public string xts_transactiondetails_json { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public string ktb_category { get; set; }

		[AntiXss]
		public bool ktb_fixedprice { get; set; }

		[AntiXss]
		public string ktb_vehiclemodelidname { get; set; }

		[AntiXss]
		public string ktb_source { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public string ktb_typecode { get; set; }

		[AntiXss]
		public decimal xts_stockbaseprice_base { get; set; }

		[AntiXss]
		public string xts_modelspecification { get; set; }

		[AntiXss]
		public decimal xts_inventoryprice { get; set; }

		[AntiXss]
		public bool xts_stoppedforpurchase { get; set; }

		[AntiXss]
		public string xts_gradenumber { get; set; }

		[AntiXss]
		public decimal xjp_additionalairbagfee { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string ktb_nongenuinecategory { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_fixedpricename { get; set; }

		[AntiXss]
		public string xts_axproducttypename { get; set; }

		[AntiXss]
		public string xts_productsegment1idname { get; set; }

		[AntiXss]
		public string xts_taxcategoryname { get; set; }

		[AntiXss]
		public string xts_stoppedforinventoryname { get; set; }

		[AntiXss]
		public string ktb_indicatorname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xjp_lightcarflagname { get; set; }

		[AntiXss]
		public bool xts_fixedprice { get; set; }

		[AntiXss]
		public string ktb_nongenuinecategoryname { get; set; }

		[AntiXss]
		public string xts_productsegment3idname { get; set; }

		[AntiXss]
		public string new_internalnumber { get; set; }

		[AntiXss]
		public DateTime xjp_generationdate { get; set; }

		[AntiXss]
		public string xts_landedcostgroup { get; set; }

		[AntiXss]
		public string xjp_productsubsidycategory { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_kittype { get; set; }

		[AntiXss]
		public string ktb_nongenuinecategoryidname { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public string ktb_modelcode { get; set; }

		[AntiXss]
		public Guid xts_purchaseunitid { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public Guid ktb_nongenuinecategoryid { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public decimal xjp_hqexpense { get; set; }

		[AntiXss]
		public decimal xts_purchaseprice { get; set; }

		[AntiXss]
		public string xts_taxcategory { get; set; }

		[AntiXss]
		public string xts_methodforsalesorder { get; set; }

		[AntiXss]
		public decimal xjp_fluorocarbonfee_base { get; set; }

		[AntiXss]
		public string xts_recordtypename { get; set; }

		[AntiXss]
		public int statecode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
