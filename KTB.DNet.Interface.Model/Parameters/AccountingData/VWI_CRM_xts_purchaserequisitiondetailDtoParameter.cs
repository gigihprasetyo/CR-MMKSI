#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchaserequisitiondetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:01
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
    public class VWI_CRM_xts_purchaserequisitiondetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_productinteriorcoloridname { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_closelinename { get; set; }

		[AntiXss]
		public Guid ktb_purchaserequisitionid { get; set; }

		[AntiXss]
		public Guid xts_purchaserequisitionid { get; set; }

		[AntiXss]
		public Guid xts_productconfigurationid { get; set; }

		[AntiXss]
		public string xts_requestedbyidname { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid xts_productinteriorcolorid { get; set; }

		[AntiXss]
		public decimal xts_quantityonpurchaseorder { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string xts_purchaserequisitiondetail { get; set; }

		[AntiXss]
		public decimal xts_quantityorder { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public int ktb_qty { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_departmentidname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_departmentid { get; set; }

		[AntiXss]
		public DateTime xts_promisedate { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public string ktb_isconversionname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public Guid xts_purchaserequisitiondetailid { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string ktb_productsap { get; set; }

		[AntiXss]
		public Guid ktb_product { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_purchasefor { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_transactionamount_base { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public decimal xts_unitcost { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount_base { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string ktb_purchaserequisitionidname { get; set; }

		[AntiXss]
		public Guid xts_productstyleid { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount { get; set; }

		[AntiXss]
		public string xts_purchaseforname { get; set; }

		[AntiXss]
		public decimal xts_totalamount_base { get; set; }

		[AntiXss]
		public string xts_purchaseorderdetailidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_baseunitidname { get; set; }

		[AntiXss]
		public decimal xts_discountamount { get; set; }

		[AntiXss]
		public decimal xts_unitcost_base { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public bool ktb_isapproved { get; set; }

		[AntiXss]
		public string xts_closereason { get; set; }

		[AntiXss]
		public string xts_purchaseunitidname { get; set; }

		[AntiXss]
		public string xts_purchaserequisitionidname { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public string xts_productconfigurationidname { get; set; }

		[AntiXss]
		public decimal xts_discountamount_base { get; set; }

		[AntiXss]
		public bool xts_closeline { get; set; }

		[AntiXss]
		public decimal xts_discountpercentage { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string ktb_productname { get; set; }

		[AntiXss]
		public Guid xts_purchaseunitid { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public bool ktb_isconversion { get; set; }

		[AntiXss]
		public Guid xts_requestedbyid { get; set; }

		[AntiXss]
		public decimal xts_basequantityorder { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public decimal xts_transactionamount { get; set; }

		[AntiXss]
		public DateTime xts_requireddate { get; set; }

		[AntiXss]
		public decimal xts_totalamount { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public Guid xts_purchaseorderdetailid { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount_base { get; set; }

		[AntiXss]
		public decimal xts_basequantityonpurchaseorder { get; set; }

		[AntiXss]
		public Guid xts_baseunitid { get; set; }

		[AntiXss]
		public string ktb_isapprovedname { get; set; }

		[AntiXss]
		public int ktb_qtyforecast { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

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
