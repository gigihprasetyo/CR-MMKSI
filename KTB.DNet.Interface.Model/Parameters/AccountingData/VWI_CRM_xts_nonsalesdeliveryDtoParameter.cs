#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_nonsalesdeliveryParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:58
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
    public class VWI_CRM_xts_nonsalesdeliveryParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_stockidname { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid xts_productconfigurationid { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid xts_productinteriorcolorid { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid xts_personinchargeid { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

		[AntiXss]
		public string xts_deliverytypeidname { get; set; }

		[AntiXss]
		public string xts_vehiclemodelidname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_vehiclemodelid { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid xjp_antiqueledgerid { get; set; }

		[AntiXss]
		public Guid xts_stockinventorynewvehicleid { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public bool xts_sourcetype { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_destination { get; set; }

		[AntiXss]
		public int xts_mileage { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public Guid xts_productstyleid { get; set; }

		[AntiXss]
		public string xts_transfertousedcarname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public DateTime xts_deliverydate { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xjp_productionyearjapan { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xts_personinchargeidname { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public Guid xts_inventorytransactionid { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public string xts_productconfigurationidname { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public string xts_inventorytransactionidname { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public string xts_usedvehicleexteriorcoloridname { get; set; }

		[AntiXss]
		public bool xts_transfertousedcar { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xts_nonsalesdeliveryid { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationnumber { get; set; }

		[AntiXss]
		public string xts_stockinventorynewvehicleidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid xts_stockid { get; set; }

		[AntiXss]
		public Guid xts_locationid { get; set; }

		[AntiXss]
		public string xts_aliasmodel { get; set; }

		[AntiXss]
		public string xts_nonsalesdeliverynumber { get; set; }

		[AntiXss]
		public Guid xts_usedvehicleexteriorcolorid { get; set; }

		[AntiXss]
		public string xts_productinteriorcoloridname { get; set; }

		[AntiXss]
		public Guid xts_deliverytypeid { get; set; }

		[AntiXss]
		public string xts_sourcetypename { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public string xjp_antiqueledgeridname { get; set; }

		[AntiXss]
		public string xts_locationidname { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_productstyleidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
