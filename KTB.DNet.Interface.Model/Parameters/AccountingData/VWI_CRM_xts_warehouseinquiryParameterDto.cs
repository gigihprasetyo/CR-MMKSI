#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_warehouseinquiryParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2020 11:04:00
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
    public class VWI_CRM_xts_warehouseinquiryParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public decimal xts_quantityordered { get; set; }
        [AntiXss]
        public decimal xts_quantityonsalesorder { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public decimal xts_quantityintransit { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public Guid xts_warehouseid { get; set; }
        [AntiXss]
        public decimal xts_quantityonpurchaseorder { get; set; }
        [AntiXss]
        public string xts_description { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public decimal xts_totalcost_base { get; set; }
        [AntiXss]
        public string xts_warehouseinquiry { get; set; }
        [AntiXss]
        public string xts_exteriorcolordescription { get; set; }
        [AntiXss]
        public decimal xts_quantityonorder { get; set; }
        [AntiXss]
        public string xts_interiorcoloridname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public decimal xts_quantityavailable { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string xts_productidname { get; set; }
        [AntiXss]
        public Guid xts_styleid { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public decimal xts_unitcost { get; set; }
        [AntiXss]
        public string xts_company { get; set; }
        [AntiXss]
        public string xts_configurationdescription { get; set; }
        [AntiXss]
        public decimal xts_quantityonworkorder { get; set; }
        [AntiXss]
        public string xts_warehouseidname { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_interiorcolordescription { get; set; }
        [AntiXss]
        public decimal xts_quantityonhand { get; set; }
        [AntiXss]
        public string xts_producttypename { get; set; }
        [AntiXss]
        public decimal xts_quantityphysicalreserved { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public Guid xts_productclassid { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public Guid xts_configurationid { get; set; }
        [AntiXss]
        public string xts_styledescription { get; set; }
        [AntiXss]
        public decimal xts_unitcost_base { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public Guid xts_exteriorcolorid { get; set; }
        [AntiXss]
        public decimal xts_quantityavailablephysical { get; set; }
        [AntiXss]
        public Guid xts_siteid { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string xts_producttype { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public decimal xts_quantityreserved { get; set; }
        [AntiXss]
        public decimal xts_totalcost { get; set; }
        [AntiXss]
        public Guid xts_productid { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public decimal xts_quantityposted { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public decimal xts_quantityavailableordered { get; set; }
        [AntiXss]
        public decimal xts_totalphysicalcost_base { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string xts_exteriorcoloridname { get; set; }
        [AntiXss]
        public Guid xts_interiorcolorid { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_siteidname { get; set; }
        [AntiXss]
        public Guid organizationid { get; set; }
        [AntiXss]
        public decimal xts_totalphysicalcost { get; set; }
        [AntiXss]
        public decimal xts_quantityorderedreserved { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string organizationidname { get; set; }
        [AntiXss]
        public string xts_configurationidname { get; set; }
        [AntiXss]
        public string xts_styleidname { get; set; }
        [AntiXss]
        public string xts_productclassidname { get; set; }
        [AntiXss]
        public Guid xts_warehouseinquiryid { get; set; }
        
        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
