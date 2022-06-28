#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_userareaassignmentParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:47
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
    public class VWI_CRM_ktb_userareaassignmentParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string ktb_accessservicename { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string ktb_defaulttrans { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string ktb_defaulttransname { get; set; }

		[AntiXss]
		public bool ktb_accesspart { get; set; }

		[AntiXss]
		public bool ktb_accessvhcpurc { get; set; }

		[AntiXss]
		public string ktb_useridyominame { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string ktb_useridname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string ktb_teamidname { get; set; }

		[AntiXss]
		public string ktb_accesspartname { get; set; }

		[AntiXss]
		public string ktb_accessvhcslsname { get; set; }

		[AntiXss]
		public string ktb_userareaassignment { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string ktb_teamidyominame { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid ktb_teamid { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid ktb_userid { get; set; }

		[AntiXss]
		public bool ktb_accessvhcsls { get; set; }

		[AntiXss]
		public string ktb_accessvhcpurcname { get; set; }

		[AntiXss]
		public Guid ktb_businessunitid { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public bool ktb_accessservice { get; set; }

		[AntiXss]
		public Guid ktb_userareaassignmentid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_businessunitidname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
