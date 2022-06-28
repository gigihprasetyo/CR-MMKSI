#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehicletransactionhistoryParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:06
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
    public class VWI_CRM_xts_vehicletransactionhistoryParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_transactiontypename { get; set; }

		[AntiXss]
		public string xts_stockidname { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_consumptiontaxid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public decimal xjp_airbagfee { get; set; }

		[AntiXss]
		public string xts_referencenumber { get; set; }

		[AntiXss]
		public Guid xts_interiorcolorid { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public string xts_consumptiontaxidname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public bool xts_uvstockbalanceupdate { get; set; }

		[AntiXss]
		public decimal xjp_airbagfee_base { get; set; }

		[AntiXss]
		public string xts_transactionuniquereference { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

		[AntiXss]
		public decimal xjp_fundmanagementfee { get; set; }

		[AntiXss]
		public string xts_vehiclemodelidname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_uvstockbalanceupdatename { get; set; }

		[AntiXss]
		public string xts_interiorcoloridname { get; set; }

		[AntiXss]
		public decimal xjp_compulsoryinsuranceamount { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xjp_shredderdustfee { get; set; }

		[AntiXss]
		public Guid xts_stockinventorynewvehicleid { get; set; }

		[AntiXss]
		public decimal xjp_compulsoryinsuranceamount_base { get; set; }

		[AntiXss]
		public Guid xts_vehicletransactionhistoryid { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public Guid xts_styleid { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public string xts_transactiontype { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal xjp_informationmanagementfee_base { get; set; }

		[AntiXss]
		public decimal xjp_fluorocarbonfee_base { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string xts_manufactureridname { get; set; }

		[AntiXss]
		public decimal xts_quantity { get; set; }

		[AntiXss]
		public decimal xjp_informationmanagementfee { get; set; }

		[AntiXss]
		public Guid xts_vehiclemodelid { get; set; }

		[AntiXss]
		public decimal xjp_fundmanagementfee_base { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public decimal xts_consumptiontaxamount { get; set; }

		[AntiXss]
		public decimal xts_serviceamount_base { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public Guid xts_configurationid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public decimal xts_consumptiontaxamount_base { get; set; }

		[AntiXss]
		public Guid xts_exteriorcolorid { get; set; }

		[AntiXss]
		public bool xts_includeininventhistory { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string xts_stockinventorynewvehicleidname { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_includeininventhistoryname { get; set; }

		[AntiXss]
		public string xts_transactionhistorynumber { get; set; }

		[AntiXss]
		public decimal xts_vehicleamount { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationnumber { get; set; }

		[AntiXss]
		public string xts_referencetype { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_exteriorcoloridname { get; set; }

		[AntiXss]
		public Guid xts_stockid { get; set; }

		[AntiXss]
		public Guid xts_locationid { get; set; }

		[AntiXss]
		public decimal xts_serviceamount { get; set; }

		[AntiXss]
		public decimal xjp_shredderdustfee_base { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_manufacturerid { get; set; }

		[AntiXss]
		public string xts_referencetypename { get; set; }

		[AntiXss]
		public string xts_locationidname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_configurationidname { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public decimal xjp_fluorocarbonfee { get; set; }

		[AntiXss]
		public decimal xts_vehicleamount_base { get; set; }

		[AntiXss]
		public string xts_stocknumberlookupname { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public int xts_stocknumberlookuptype { get; set; }

		[AntiXss]
		public string xts_styleidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
