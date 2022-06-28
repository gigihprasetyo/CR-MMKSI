#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_pricelistdetailferenceDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 13:44:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_pricelistdetailDto : DtoBase
    {
        public string company { get; set; }
        public string businessunitcode { get; set; }
        public string ktb_dnetid { get; set; }
        public decimal xts_quantity { get; set; }
        public Guid owninguser { get; set; }
        public decimal xts_partsprice { get; set; }
        public string xts_locking { get; set; }
        public string transactioncurrencyidname { get; set; }
        public string xts_businessunitidname { get; set; }
        public string xts_productidname { get; set; }
        public string statecodename { get; set; }
        public Guid transactioncurrencyid { get; set; }
        public decimal xts_servicefee_base { get; set; }
        public Guid createdonbehalfby { get; set; }
        public DateTime bsi_lastupdatepreviousdate { get; set; }
        public string xts_pkcombinationkey { get; set; }
        public Guid ownerid { get; set; }
        public decimal xts_totalprice_base { get; set; }
        public decimal bsi_previousprice_base { get; set; }
        public string xts_producttypename { get; set; }
        public int importsequencenumber { get; set; }
        public decimal xts_servicefee { get; set; }
        public string xts_productdescription { get; set; }
        public Int64 versionnumber { get; set; }
        public string xts_producttype { get; set; }
        public int statecode { get; set; }
        public int utcconversiontimezonecode { get; set; }
        public string createdbyyominame { get; set; }
        public string xts_pricingmethod { get; set; }
        public Guid owningbusinessunit { get; set; }
        public string modifiedbyname { get; set; }
        public string xts_unitidname { get; set; }
        public Guid modifiedby { get; set; }
        public string modifiedbyyominame { get; set; }
        public Guid createdby { get; set; }
        public int timezoneruleversionnumber { get; set; }
        public Guid xts_pricelistid { get; set; }
        public Guid xts_pricelistdetailid { get; set; }
        public string owneridtype { get; set; }
        public string statuscodename { get; set; }
        public Guid owningteam { get; set; }
        public decimal bsi_previousprice { get; set; }
        public string createdonbehalfbyyominame { get; set; }
        public string owneridyominame { get; set; }
        public DateTime modifiedon { get; set; }
        public string xts_pricingmethodname { get; set; }
        public decimal exchangerate { get; set; }
        public string modifiedonbehalfbyname { get; set; }
        public int statuscode { get; set; }
        public string modifiedonbehalfbyyominame { get; set; }
        public string xts_pricelistdetail { get; set; }
        public string createdbyname { get; set; }
        public DateTime createdon { get; set; }
        public Guid xts_businessunitid { get; set; }
        public string createdonbehalfbyname { get; set; }
        public string xts_pricelistidname { get; set; }
        public decimal xts_totalprice { get; set; }
        public string owneridname { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        public Guid xts_productid { get; set; }
        public decimal xts_partsprice_base { get; set; }
        public Guid xts_unitid { get; set; }
        public DateTime overriddencreatedon { get; set; }
        public string msdyn_companycode { get; set; }
    }
}
