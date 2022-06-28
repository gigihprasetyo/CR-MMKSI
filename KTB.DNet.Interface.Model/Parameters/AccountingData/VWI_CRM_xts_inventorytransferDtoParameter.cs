#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventorytransferParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 1/9/2020 2:44:55 PM
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
    public class VWI_CRM_xts_inventorytransferParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_transactiontypename { get; set; }

		[AntiXss]
		public string xts_transferstatus { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid xts_tositeid { get; set; }

		[AntiXss]
		public string xts_referencenumber { get; set; }

		[AntiXss]
		public string xts_receiptidname { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid xts_inventorytransferid { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid xts_receiptid { get; set; }

		[AntiXss]
		public string xts_log { get; set; }

		[AntiXss]
		public bool ktb_isclaim { get; set; }

		[AntiXss]
		public Guid xts_personinchargeid { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public bool xts_transferstep { get; set; }

		[AntiXss]
		public string ktb_purchasereceiptidname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_inventorytransfernumber { get; set; }

		[AntiXss]
		public string xts_searchvehicle { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_workorderidname { get; set; }

		[AntiXss]
		public DateTime xts_receiptdate { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string ktb_isclaimname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_tositeidname { get; set; }

		[AntiXss]
		public string xts_transactiontype { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_itemtypefortransfer { get; set; }

		[AntiXss]
		public Guid ktb_parentbusinessunitid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public Guid xts_fromsiteid { get; set; }

		[AntiXss]
		public DateTime ktb_actualreceiptdate { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public DateTime ktb_actualtransferdate { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xts_itemtypefortransfername { get; set; }

		[AntiXss]
		public string xts_personinchargeidname { get; set; }

		[AntiXss]
		public Guid xts_tobusinessunitid { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string xts_fromsiteidname { get; set; }

		[AntiXss]
		public Guid xts_workorderid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid ktb_purchasereceiptid { get; set; }

		[AntiXss]
		public string ktb_ribbondataproductwarehouse { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public string xts_sourcedata { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_transferstepname { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_tobusinessunitidname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string ktb_description { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string xts_transferstatusname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
