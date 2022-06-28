#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_teamParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:49
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
    public class VWI_CRM_teamParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid queueid { get; set; }

		[AntiXss]
		public Guid administratorid { get; set; }

		[AntiXss]
		public string description { get; set; }

		[AntiXss]
		public string queueidname { get; set; }

		[AntiXss]
		public string regardingobjecttypecode { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid regardingobjectid { get; set; }

		[AntiXss]
		public Guid teamid { get; set; }

		[AntiXss]
		public string name { get; set; }

		[AntiXss]
		public Guid azureactivedirectoryobjectid { get; set; }

		[AntiXss]
		public string teamtypename { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string administratoridname { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public string xts_isdomainname { get; set; }

		[AntiXss]
		public string ktb_teamtype { get; set; }

		[AntiXss]
		public Guid businessunitid { get; set; }

		[AntiXss]
		public bool xts_isdomain { get; set; }

		[AntiXss]
		public string emailaddress { get; set; }

		[AntiXss]
		public bool systemmanaged { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string administratoridyominame { get; set; }

		[AntiXss]
		public string teamtype { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string isdefaultname { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public string yominame { get; set; }

		[AntiXss]
		public Guid teamtemplateid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string businessunitidname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_teamtypename { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public bool new_isdomain { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string systemmanagedname { get; set; }

		[AntiXss]
		public string new_isdomainname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public bool isdefault { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
