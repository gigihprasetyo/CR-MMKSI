#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventorytransferdetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:57
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
    public class VWI_CRM_xts_inventorytransferdetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_fromexteriorcoloridname { get; set; }

		[AntiXss]
		public Guid xts_tobatchid { get; set; }

		[AntiXss]
		public string xts_fromwarehouseidname { get; set; }

		[AntiXss]
		public Guid xts_consumptiontaxinid { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal xts_basequantity { get; set; }

		[AntiXss]
		public Guid xts_toserialid { get; set; }

		[AntiXss]
		public string xts_inventorytransferidname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid xts_inventorytransferid { get; set; }

		[AntiXss]
		public string xts_tointeriorcoloridname { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_fromconfigurationidname { get; set; }

		[AntiXss]
		public string xts_tostyleidname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_fromlocationid { get; set; }

		[AntiXss]
		public Guid xts_inventoryunitid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public Guid xts_inventorytransferdetailid { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_fromserialid { get; set; }

		[AntiXss]
		public Guid xts_tostyleid { get; set; }

		[AntiXss]
		public string xts_fromserialidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid xjp_antiqueledgerid { get; set; }

		[AntiXss]
		public Guid xts_stockinventorynewvehicleid { get; set; }

		[AntiXss]
		public string xts_fromlocationidname { get; set; }

		[AntiXss]
		public string xts_fromsiteidname { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public Guid xts_transactionunitid { get; set; }

		[AntiXss]
		public string xts_servicepartsandmaterialidname { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_tositeidname { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_consumptiontaxinidname { get; set; }

		[AntiXss]
		public Guid xts_fromstyleid { get; set; }

		[AntiXss]
		public Guid xts_towarehouseid { get; set; }

		[AntiXss]
		public string xts_frombusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_toexteriorcolorid { get; set; }

		[AntiXss]
		public string xts_transactionunitidname { get; set; }

		[AntiXss]
		public Guid xts_fromsiteid { get; set; }

		[AntiXss]
		public decimal xts_quantity { get; set; }

		[AntiXss]
		public string xjp_antiqueledgeridname { get; set; }

		[AntiXss]
		public decimal ktb_latestpurchaseprice_base { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_chassisnumber { get; set; }

		[AntiXss]
		public decimal ktb_latestpurchaseprice { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public string xts_fromstyleidname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_tobusinessunitidname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public decimal ktb_cogstrx { get; set; }

		[AntiXss]
		public Guid xts_tositeid { get; set; }

		[AntiXss]
		public string xts_toconfigurationidname { get; set; }

		[AntiXss]
		public Guid xts_tobusinessunitid { get; set; }

		[AntiXss]
		public string xts_toserialidname { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid xts_tointeriorcolorid { get; set; }

		[AntiXss]
		public int xts_stocknumberlookuptype { get; set; }

		[AntiXss]
		public Guid xts_frombusinessunitid { get; set; }

		[AntiXss]
		public string xts_towarehouseidname { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_inventoryunitidname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_tolocationidname { get; set; }

		[AntiXss]
		public Guid xts_tolocationid { get; set; }

		[AntiXss]
		public Guid xts_frombatchid { get; set; }

		[AntiXss]
		public Guid xts_fromexteriorcolorid { get; set; }

		[AntiXss]
		public string xts_inventorytransferdetail { get; set; }

		[AntiXss]
		public string xts_sourcedata { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_toexteriorcoloridname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public decimal ktb_cogstrx_base { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationnumber { get; set; }

		[AntiXss]
		public string xts_stockinventorynewvehicleidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string xts_frominteriorcoloridname { get; set; }

		[AntiXss]
		public string xts_frombatchidname { get; set; }

		[AntiXss]
		public Guid xts_stockid { get; set; }

		[AntiXss]
		public string xts_stockidname { get; set; }

		[AntiXss]
		public string xts_consumptiontaxoutidname { get; set; }

		[AntiXss]
		public string xts_tobatchidname { get; set; }

		[AntiXss]
		public Guid xts_consumptiontaxoutid { get; set; }

		[AntiXss]
		public Guid xts_fromconfigurationid { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_frominteriorcolorid { get; set; }

		[AntiXss]
		public string xts_remarks { get; set; }

		[AntiXss]
		public Guid xts_fromwarehouseid { get; set; }

		[AntiXss]
		public string xts_stocknumberlookupname { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public Guid xts_servicepartsandmaterialid { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public Guid xts_toconfigurationid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public DateTime ktb_ata { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
