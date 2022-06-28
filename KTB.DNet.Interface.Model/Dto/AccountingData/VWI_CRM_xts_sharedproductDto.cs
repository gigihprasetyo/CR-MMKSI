#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_sharedproductDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:26:34
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_sharedproductDto : DtoBase
    {
        public string ktb_nongenuinecategory { get; set; }

		public string xts_defaultunitidname { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public string ktb_indicator { get; set; }

		public decimal xts_purchaseprice_base { get; set; }

		public Guid xts_interiorcolorid { get; set; }

		public decimal xts_stockbaseprice_base { get; set; }

		public string statuscodename { get; set; }

		public int xts_productvolume { get; set; }

		public string xts_description { get; set; }

		public string xts_manufacturer { get; set; }

		public string xts_axproducttype { get; set; }

		public string xts_recordtypename { get; set; }

		public string xts_gradenumber { get; set; }

		public decimal xts_servicefee_base { get; set; }

		public string modifiedbyname { get; set; }

		public string xjp_weighttaxreduction { get; set; }

		public decimal xts_servicefee { get; set; }

		public string xts_visibilityname { get; set; }

		public string ktb_fixedpricename { get; set; }

		public string xts_internalnumber { get; set; }

		public string xts_defaultbrand { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public Guid xts_productsegment1id { get; set; }

		public decimal xts_partprice_base { get; set; }

		public string xts_salesunitidname { get; set; }

		public string xts_interiorcoloridname { get; set; }

		public string xts_type { get; set; }

		public Guid xts_gradeid { get; set; }

		public string xts_eventdata { get; set; }

		public string xts_styleidname { get; set; }

		public string xts_product { get; set; }

		public decimal exchangerate { get; set; }

		public decimal xjp_additionalairbagfee { get; set; }

		public string xts_exteriorcoloridname { get; set; }

		public string xts_productsegment3idname { get; set; }

		public string xts_taxcategoryname { get; set; }

		public string ktb_category { get; set; }

		public string xts_generation { get; set; }

		public Guid xts_styleid { get; set; }

		public decimal xjp_airbagrates_base { get; set; }

		public int statecode { get; set; }

		public decimal xjp_additionalairbagfee_base { get; set; }

		public string ktb_categoryname { get; set; }

		public string xts_company { get; set; }

		public string xts_sharedproductreferenceimportstring { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public Guid ktb_nongenuinecategoryid { get; set; }

		public decimal xts_partprice { get; set; }

		public decimal xjp_fluorocarbonfee_base { get; set; }

		public string xts_kittypename { get; set; }

		public string xts_productsegment4idname { get; set; }

		public int xts_productweight { get; set; }

		public decimal xjp_airbagrates { get; set; }

		public string ktb_indicatorname { get; set; }

		public bool xts_visibility { get; set; }

		public decimal xts_purchaseprice { get; set; }

		public string ktb_nongenuinecategoryname { get; set; }

		public string xts_description2 { get; set; }

		public string xjp_acquisitiontaxreduction { get; set; }

		public decimal xjp_additionalfluorocarbonfee { get; set; }

		public Guid xts_sharedproductid { get; set; }

		public string xjp_lightcarflagname { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_chassismodel { get; set; }

		public string xts_sharedproductreferenceidname { get; set; }

		public string xts_productsegment1idname { get; set; }

		public Guid xts_defaultvehiclebrandid { get; set; }

		public string xts_classificationnumber { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public Guid xts_productclassid { get; set; }

		public Guid xts_configurationid { get; set; }

		public string xts_typename { get; set; }

		public decimal xts_additionalvariantprice_base { get; set; }

		public string xts_defaultvehiclebrandidname { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string ktb_modelcode { get; set; }

		public Guid xts_exteriorcolorid { get; set; }

		public string xts_axproducttypename { get; set; }

		public decimal xts_additionalvariantpurchaseprice_base { get; set; }

		public string ktb_typecode { get; set; }

		public Guid createdonbehalfby { get; set; }

		public bool ktb_fixedprice { get; set; }

		public string createdbyname { get; set; }

		public Guid xts_sharedproductreferenceid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public Guid xts_defaultunitid { get; set; }

		public Guid xts_productsegment4id { get; set; }

		public decimal xts_additionalvariantprice { get; set; }

		public decimal xts_stockbaseprice { get; set; }

		public decimal xjp_shredderdustfee_base { get; set; }

		public string xts_gradeidname { get; set; }

		public string xts_productclassidname { get; set; }

		public Guid xts_productsegment2id { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public string xjp_productsubsidycategory { get; set; }

		public Guid xts_purchaseunitid { get; set; }

		public decimal xts_additionalvariantpurchaseprice { get; set; }

		public string xjp_automobiletaxreduction { get; set; }

		public string createdbyyominame { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_entitytag { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public bool xjp_lightcarflag { get; set; }

		public string xts_purchaseunitidname { get; set; }

		public string modifiedbyyominame { get; set; }

		public DateTime xjp_generationdate { get; set; }

		public decimal xjp_shredderdustfee { get; set; }

		public decimal xjp_additionalfluorocarbonfee_base { get; set; }

		public string xts_productfamily { get; set; }

		public Guid xts_productsegment3id { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string xts_configurationidname { get; set; }

		public Guid organizationid { get; set; }

		public string xts_defaultbrandgeneration { get; set; }

		public decimal xjp_fluorocarbonfee { get; set; }

		public string xts_productsegment2idname { get; set; }

		public string organizationidname { get; set; }

		public string ktb_nongenuinecategoryidname { get; set; }

		public string xts_kittype { get; set; }

		public string xts_taxcategory { get; set; }

		public string statecodename { get; set; }

		public Guid xts_salesunitid { get; set; }

		public string xts_recordtype { get; set; }

		public string xts_modelspecification { get; set; }
    }
}
