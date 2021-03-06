#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_recommendedproductParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 16:37:00
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
    public class VWI_CRM_xts_recommendedproductParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public DateTime xts_startdate { get; set; }
        [AntiXss]
        public decimal xts_titleregistrationfee { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public decimal xts_accessorytotaldiscount_base { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public decimal xts_ontheroadprice { get; set; }
        [AntiXss]
        public Guid xts_productconfigurationid { get; set; }
        [AntiXss]
        public Guid xts_consumptiontax1id { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public Guid xts_productinteriorcolorid { get; set; }
        [AntiXss]
        public string xts_mandatorycolorname { get; set; }
        [AntiXss]
        public decimal xts_vehiclediscount { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string xts_gradenumber { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_productstyleidname { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string xts_defaultbrand { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public decimal xts_maximumdiscountamount_base { get; set; }
        [AntiXss]
        public decimal xts_netsalesprice_base { get; set; }
        [AntiXss]
        public Guid xts_gradeid { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Guid xts_consumptiontax2id { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_mandatoryaccessoryname { get; set; }
        [AntiXss]
        public string xts_productidname { get; set; }
        [AntiXss]
        public decimal xts_accessorysalesprice_base { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public decimal xts_netsalesprice { get; set; }
        [AntiXss]
        public Guid xts_productstyleid { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public decimal xts_totalsalesprice { get; set; }
        [AntiXss]
        public bool xts_mandatorycolor { get; set; }
        [AntiXss]
        public string xts_consumptiontax1idname { get; set; }
        [AntiXss]
        public decimal xts_baseprice_base { get; set; }
        [AntiXss]
        public string xts_consumptiontax2idname { get; set; }
        [AntiXss]
        public Guid xts_productexteriorcolorid { get; set; }
        [AntiXss]
        public string xts_recommendedproduct { get; set; }
        [AntiXss]
        public decimal xts_accessorysalesprice { get; set; }
        [AntiXss]
        public decimal xts_totalsalesprice_base { get; set; }
        [AntiXss]
        public decimal xts_maximumdiscountamount { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public decimal xts_specialcolorprice { get; set; }
        [AntiXss]
        public decimal xts_titleregistrationfee_base { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string xts_pkcombinationkey { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public decimal xts_offtheroadprice { get; set; }
        [AntiXss]
        public string xts_productexteriorcoloridname { get; set; }
        [AntiXss]
        public string xts_productconfigurationidname { get; set; }
        [AntiXss]
        public decimal xts_specialcolorprice_base { get; set; }
        [AntiXss]
        public string xts_gradeidname { get; set; }
        [AntiXss]
        public DateTime xts_enddate { get; set; }
        [AntiXss]
        public Guid xts_productid { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public decimal xts_baseprice { get; set; }
        [AntiXss]
        public bool xts_mandatoryaccessory { get; set; }
        [AntiXss]
        public decimal xts_vehiclediscount_base { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public decimal xts_offtheroadprice_base { get; set; }
        [AntiXss]
        public Guid xts_recommendedproductid { get; set; }
        [AntiXss]
        public decimal xts_ontheroadprice_base { get; set; }
        [AntiXss]
        public string xts_productinteriorcoloridname { get; set; }
        [AntiXss]
        public decimal xts_accessorytotaldiscount { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string xts_defaultbrandgeneration { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
