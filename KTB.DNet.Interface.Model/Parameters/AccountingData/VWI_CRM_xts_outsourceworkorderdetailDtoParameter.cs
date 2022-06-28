#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_outsourceworkorderdetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:59
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
    public class VWI_CRM_xts_outsourceworkorderdetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Guid xts_servicecategoryid { get; set; }

		[AntiXss]
		public string xts_producttype { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid xts_withholdingtaxid { get; set; }

		[AntiXss]
		public string xts_commentdescription { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal xts_discountrate { get; set; }

		[AntiXss]
		public decimal xts_discountbaseamount_base { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_unitidname { get; set; }

		[AntiXss]
		public decimal xts_totaltransactionamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_outsourceworkorderid { get; set; }

		[AntiXss]
		public decimal xts_unitpricelaborrate_base { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_productsegment1id { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_transactionamount_base { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_outsourceworkorderdetail { get; set; }

		[AntiXss]
		public decimal xts_quantitymanhour { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public string xts_outsourceworkorderidname { get; set; }

		[AntiXss]
		public string xts_productsegment3idname { get; set; }

		[AntiXss]
		public Guid xts_commentid { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount_base { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public bool xts_proceedtoreceiptstatus { get; set; }

		[AntiXss]
		public string xts_producttypename { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount { get; set; }

		[AntiXss]
		public string xts_commentidname { get; set; }

		[AntiXss]
		public string xts_servicecategorydescription { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public Guid xts_outsourceworkorderdetailid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public decimal xts_discountbaseamount { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public decimal xts_totalwitholdingtaxamount { get; set; }

		[AntiXss]
		public Guid xts_servicepartsandmaterialsid { get; set; }

		[AntiXss]
		public decimal xts_discountamount { get; set; }

		[AntiXss]
		public decimal xts_unitpricelaborrate { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xts_servicepartsandmaterialsidname { get; set; }

		[AntiXss]
		public string xts_partidname { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public Guid xts_partid { get; set; }

		[AntiXss]
		public Guid xts_productsegment2id { get; set; }

		[AntiXss]
		public Guid xts_unitid { get; set; }

		[AntiXss]
		public string xts_partcodedescription { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xts_productsegment4id { get; set; }

		[AntiXss]
		public decimal xts_discountamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalwitholdingtaxamount_base { get; set; }

		[AntiXss]
		public string xts_productsegment2idname { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_productsegment4idname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public decimal xts_transactionamount { get; set; }

		[AntiXss]
		public Guid xts_productsegment3id { get; set; }

		[AntiXss]
		public string xts_proceedtoreceiptstatusname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_productsegment1idname { get; set; }

		[AntiXss]
		public string xts_withholdingtaxidname { get; set; }

		[AntiXss]
		public decimal xts_totalamountbeforediscount_base { get; set; }

		[AntiXss]
		public decimal xts_totaltransactionamount { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string xts_servicecategoryidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
