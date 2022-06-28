#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_newvehicledeliveryorderParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 14/01/2020 9:03:04
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
    public class VWI_CRM_xts_newvehicledeliveryorderParameterDto : ParameterDtoBase, IValidatableObject
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
		public string xts_delivertocustomernumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid xjp_billtocustomerid { get; set; }

		[AntiXss]
		public Guid xts_productconfigurationid { get; set; }

		[AntiXss]
		public string xjp_idempotentmessage { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

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
		public Guid ktb_perlengkapanstandarid { get; set; }

		[AntiXss]
		public string xts_log { get; set; }

		[AntiXss]
		public string xts_cancellednvdoreferenceidname { get; set; }

		[AntiXss]
		public Guid xts_personinchargeid { get; set; }

		[AntiXss]
		public Guid xts_financingcompanyid { get; set; }

		[AntiXss]
		public string xts_notes { get; set; }

		[AntiXss]
		public string xts_financingcompanyidname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

		[AntiXss]
		public string xts_deliveryaddressidname { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment4 { get; set; }

		[AntiXss]
		public string ktb_vehicledescription { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string ktb_vehiclecolorname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment2 { get; set; }

		[AntiXss]
		public string xts_delivertocustomeridname { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public Guid xts_deliveryaddressid { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public Guid xts_newvehiclesalesorderid { get; set; }

		[AntiXss]
		public string xts_ribboneventdata { get; set; }

		[AntiXss]
		public bool ktb_bypassdp { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public Guid xts_newvehicledeliveryorderid { get; set; }

		[AntiXss]
		public string xts_financingpurchaseordernumber { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_driver { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xjp_platenumbersegment1id { get; set; }

		[AntiXss]
		public string xts_delivertocustomeridyominame { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public Guid xts_productstyleid { get; set; }

		[AntiXss]
		public Guid xts_cancellednvdoreferenceid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment3 { get; set; }

		[AntiXss]
		public string xts_financingcompanyidyominame { get; set; }

		[AntiXss]
		public DateTime xts_deliverydate { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xjp_billtocustomernumber { get; set; }

		[AntiXss]
		public string xts_newvehiclesalesorderidname { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public string xjp_billtocustomeridname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string ktb_alamatkirim { get; set; }

		[AntiXss]
		public Guid ktb_vehiclemodelid { get; set; }

		[AntiXss]
		public string xts_personinchargeidname { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid ktb_vehiclecategoryid { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public string xts_productconfigurationidname { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public DateTime ktb_tanggalpkt { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment1idname { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string ktb_perlengkapanstandaridname { get; set; }

		[AntiXss]
		public string ktb_bypassdpname { get; set; }

		[AntiXss]
		public string ktb_vehiclemodelidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_stockid { get; set; }

		[AntiXss]
		public Guid xts_locationid { get; set; }

		[AntiXss]
		public string ktb_vehiclecategoryidname { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string xts_productinteriorcoloridname { get; set; }

		[AntiXss]
		public string ktb_interfaceexceptionmessage { get; set; }

		[AntiXss]
		public string xjp_billtocustomeridyominame { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public string ktb_dealerinternalnumber { get; set; }

		[AntiXss]
		public Guid xts_delivertocustomerid { get; set; }

		[AntiXss]
		public string xts_locationidname { get; set; }

		[AntiXss]
		public string xts_newvehicledeliveryordernumber { get; set; }

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
