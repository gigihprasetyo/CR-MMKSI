#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventtransParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 22/03/2022 13:51:22
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
    public class VWI_CRM_xts_inventtransParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string xts_transactiontypename { get; set; }

		[AntiXss]
		public string xts_transactiontype { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public DateTime xts_confirmedshippingdate { get; set; }

		[AntiXss]
		public string xts_locationidname { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public DateTime xts_physicaldate { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_inventorytransactionnumber { get; set; }

		[AntiXss]
		public decimal xts_standardcost_base { get; set; }

		[AntiXss]
		public DateTime xts_expecteddate { get; set; }

		[AntiXss]
		public decimal xts_physicalcost { get; set; }

		[AntiXss]
		public Guid xts_inventoryunitid { get; set; }

		[AntiXss]
		public Guid xts_styleid { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal xts_physicalcost_base { get; set; }

		[AntiXss]
		public string xts_referencecategoryname { get; set; }

		[AntiXss]
		public string xts_interiorcoloridname { get; set; }

		[AntiXss]
		public string xts_issuestatusname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public decimal xts_postedcost_base { get; set; }

		[AntiXss]
		public DateTime xts_financialdate { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public decimal xts_operationscost { get; set; }

		[AntiXss]
		public string xts_inventtranschildnumber { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public decimal xts_operationscost_base { get; set; }

		[AntiXss]
		public string xts_company { get; set; }

		[AntiXss]
		public DateTime xts_requestedshippingdate { get; set; }

		[AntiXss]
		public decimal xts_standardcost { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string xts_inventtranschildtype { get; set; }

		[AntiXss]
		public decimal xts_quantity { get; set; }

		[AntiXss]
		public Guid xts_inventtransid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_invoicenumber { get; set; }

		[AntiXss]
		public decimal xts_settledcost { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_deliveryordernumber { get; set; }

		[AntiXss]
		public string xts_receiptstatusname { get; set; }

		[AntiXss]
		public decimal xts_adjustmentcost { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_referencenumber { get; set; }

		[AntiXss]
		public string xts_referencecategory { get; set; }

		[AntiXss]
		public Guid xts_exteriorcolorid { get; set; }

		[AntiXss]
		public string xts_receiptstatus { get; set; }

		[AntiXss]
		public decimal xts_postedcost { get; set; }

		[AntiXss]
		public decimal xts_adjustmentcost_base { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public string xts_markingreference { get; set; }

		[AntiXss]
		public string xts_inventoryunitidname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_issuestatus { get; set; }

		[AntiXss]
		public decimal xts_settledcost_base { get; set; }

		[AntiXss]
		public DateTime xts_inventorydate { get; set; }

		[AntiXss]
		public string xts_lotnumber { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xts_configurationid { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public string xts_batchnumber { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_inventtranschildtypename { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_serialnumber { get; set; }

		[AntiXss]
		public string xts_exteriorcoloridname { get; set; }

		[AntiXss]
		public Guid xts_interiorcolorid { get; set; }

		[AntiXss]
		public Guid xts_locationid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public string xts_voucher { get; set; }

		[AntiXss]
		public string xts_physicalvouchernumber { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public string xts_configurationidname { get; set; }

		[AntiXss]
		public string xts_styleidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string DealerCode { get; set; }

		[AntiXss]
		public string SourceType { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
