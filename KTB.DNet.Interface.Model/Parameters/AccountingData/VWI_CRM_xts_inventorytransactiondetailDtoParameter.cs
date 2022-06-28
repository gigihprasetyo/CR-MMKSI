#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventorytransactiondetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 1/9/2020 1:13:13 PM
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
    public class VWI_CRM_xts_inventorytransactiondetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public string ktb_chassisno { get; set; }

		[AntiXss]
		public string xts_departmentidname { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public string xts_referencenumber { get; set; }

		[AntiXss]
		public decimal xts_unitcost_base { get; set; }

		[AntiXss]
		public Guid xts_inventorytransactiondetailid { get; set; }

		[AntiXss]
		public decimal xts_basequantity { get; set; }

		[AntiXss]
		public string xts_batchidname { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal ktb_latestpurchaseprice_base { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_locationidname { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_reasonidname { get; set; }

		[AntiXss]
		public string xts_serialidname { get; set; }

		[AntiXss]
		public Guid xts_inventorytransactiondetailrefid { get; set; }

		[AntiXss]
		public decimal ktb_cogstrx_base { get; set; }

		[AntiXss]
		public string xts_productconfigurationidname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid xts_transactionunitid { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string xts_stocknumbernvidname { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_productcrossreferenceidname { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public decimal ktb_latestpurchaseprice { get; set; }

		[AntiXss]
		public Guid xts_inventoryunitid { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public Guid xts_frombusinessunitid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public Guid xts_locationid { get; set; }

		[AntiXss]
		public Guid xts_productcrossreferenceid { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_inventorytransferdetailidname { get; set; }

		[AntiXss]
		public string xts_servicepartsandmaterialidname { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string xts_inventorytransactionidname { get; set; }

		[AntiXss]
		public string xts_inventorytransactiondetail { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_inventorytransactiondetailrefidname { get; set; }

		[AntiXss]
		public Guid xts_serialid { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_frombusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_stockid { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationnumber { get; set; }

		[AntiXss]
		public Guid xts_stocknumbernvid { get; set; }

		[AntiXss]
		public Guid xts_inventorytransferdetailid { get; set; }

		[AntiXss]
		public decimal xts_unitcost { get; set; }

		[AntiXss]
		public string xts_registerserialnumber { get; set; }

		[AntiXss]
		public decimal xts_totalcost_base { get; set; }

		[AntiXss]
		public string xts_transactionunitidname { get; set; }

		[AntiXss]
		public int xts_runningnumber { get; set; }

		[AntiXss]
		public string xts_inventoryunitidname { get; set; }

		[AntiXss]
		public string xts_sourcedata { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public Guid xts_reasonid { get; set; }

		[AntiXss]
		public string xts_stockidname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string ktb_updatetosparepartstockname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public Guid xts_productinteriorcolorid { get; set; }

		[AntiXss]
		public decimal xts_totalcost { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid xts_departmentid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public decimal xts_quantityreturn { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public decimal ktb_cogstrx { get; set; }

		[AntiXss]
		public Guid xts_productstyleid { get; set; }

		[AntiXss]
		public Guid xts_batchid { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public decimal xts_quantity { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_productstyleidname { get; set; }

		[AntiXss]
		public Guid xts_inventorytransactionid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public Guid xts_servicepartsandmaterialid { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string xts_productinteriorcoloridname { get; set; }

		[AntiXss]
		public Guid xts_productconfigurationid { get; set; }

		[AntiXss]
		public string xts_transactiontypename { get; set; }

		[AntiXss]
		public string xts_transactiontype { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public bool ktb_updatetosparepartstock { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
