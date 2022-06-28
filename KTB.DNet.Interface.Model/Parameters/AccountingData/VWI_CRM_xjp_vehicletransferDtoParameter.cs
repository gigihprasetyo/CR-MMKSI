#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_vehicletransferParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 13/01/2020 14:03:26
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
    public class VWI_CRM_xjp_vehicletransferParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xjp_destinationsiteid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xjp_stockidname { get; set; }

		[AntiXss]
		public string xjp_idempotentmessage { get; set; }

		[AntiXss]
		public Guid xjp_productid { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xjp_fromsiteidname { get; set; }

		[AntiXss]
		public string xjp_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xjp_productconfigurationidname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xjp_handlingname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xjp_productidname { get; set; }

		[AntiXss]
		public Guid xjp_parentbusinessunitid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xjp_transactiontype { get; set; }

		[AntiXss]
		public string xjp_destinationlocationidname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid xjp_destinationlocationid { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xjp_destinationwarehouseidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xjp_productconfigurationid { get; set; }

		[AntiXss]
		public string xjp_fromlocationidname { get; set; }

		[AntiXss]
		public string xjp_handling { get; set; }

		[AntiXss]
		public string xjp_statusname { get; set; }

		[AntiXss]
		public string xjp_productexteriorcoloridname { get; set; }

		[AntiXss]
		public Guid xjp_businessunitid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xjp_fromwarehouseidname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xjp_chassisnumber { get; set; }

		[AntiXss]
		public Guid xjp_fromwarehouseid { get; set; }

		[AntiXss]
		public Guid xjp_vehicletransferid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xjp_destinationwarehouseid { get; set; }

		[AntiXss]
		public string xjp_productstyleidname { get; set; }

		[AntiXss]
		public Guid xjp_fromlocationid { get; set; }

		[AntiXss]
		public string xjp_vehicletransfernumber { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xjp_fromsiteid { get; set; }

		[AntiXss]
		public string xjp_businessunitidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string xjp_destinationaddress { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public DateTime xjp_receiptdateandtime { get; set; }

		[AntiXss]
		public string xjp_status { get; set; }

		[AntiXss]
		public Guid xjp_productinteriorcolorid { get; set; }

		[AntiXss]
		public Guid xjp_productstyleid { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xjp_productinteriorcoloridname { get; set; }

		[AntiXss]
		public string xjp_destinationsiteidname { get; set; }

		[AntiXss]
		public Guid xjp_stockid { get; set; }

		[AntiXss]
		public string xjp_transactiontypename { get; set; }

		[AntiXss]
		public Guid xjp_productexteriorcolorid { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xjp_locking { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
