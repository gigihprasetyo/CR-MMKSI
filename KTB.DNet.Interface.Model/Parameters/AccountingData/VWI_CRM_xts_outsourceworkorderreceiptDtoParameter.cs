#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_outsourceworkorderreceiptParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:00
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
    public class VWI_CRM_xts_outsourceworkorderreceiptParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public decimal xts_totaltaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_totaltaxamount { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public DateTime xts_vendorinvoicedate { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

		[AntiXss]
		public string xjp_idempotentmessage { get; set; }

		[AntiXss]
		public decimal xts_totalbillingamountaftertax { get; set; }

		[AntiXss]
		public Guid xts_outsourceworkorderid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public decimal xts_totalbillingamountbeforetax_base { get; set; }

		[AntiXss]
		public decimal xts_totalbillingamountbeforetax { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_originalworkorderreferenceidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_outsourceworkorderidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_outsourceworkorderreceipt { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxamount_base { get; set; }

		[AntiXss]
		public Guid xts_outsourceworkorderreceiptpersoninchargeid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public DateTime xts_outsourceworkorderdate { get; set; }

		[AntiXss]
		public Guid xts_outsourceworkorderreceiptid { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public string xts_destinationworkorderidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public bool xts_automaticallycreateapvoucher { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public Guid xts_vehicleidentificationid { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public string xts_outsourceworkshopidname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public Guid xts_originalworkorderreferenceid { get; set; }

		[AntiXss]
		public string xts_category { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public Guid xts_outsourceworkshopid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxamount { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationidname { get; set; }

		[AntiXss]
		public string xts_automaticallycreateapvouchername { get; set; }

		[AntiXss]
		public string xts_categoryname { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_vehiclemodelname { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public Guid xts_destinationworkorderid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_deliveryinvoicenumber { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xts_totalbillingamountaftertax_base { get; set; }

		[AntiXss]
		public string xts_outsourceworkorderreceiptpersoninchargeidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
