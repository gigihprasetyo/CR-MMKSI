#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_purchaserequisitiondetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Khalil
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 14:10:52
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KTB.DNet.Interface.Resources;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_purchaserequisitiondetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        
		public Guid createdby { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdbyname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdbyyominame { get; set; }

		public DateTime? createdon { get; set; }

		public Guid createdonbehalfby { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string DealerCode { get; set; }

		public decimal? exchangerate { get; set; }

		public int? importsequencenumber { get; set; }

		public bool? ktb_isapproved { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_isapprovedname { get; set; }

		public bool? ktb_isconversion { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_isconversionname { get; set; }

		public Guid ktb_product { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_productname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_productsap { get; set; }

		public Guid ktb_purchaserequisitionid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string ktb_purchaserequisitionidname { get; set; }

		public int? ktb_qty { get; set; }

		public int? ktb_qtyforecast { get; set; }

		public Guid modifiedby { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedbyname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedbyyominame { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		public DateTime? overriddencreatedon { get; set; }

		public Guid ownerid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridtype { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridyominame { get; set; }

		public Guid owningbusinessunit { get; set; }

		public Guid owningteam { get; set; }

		public Guid owninguser { get; set; }

		//[StringLength(255, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		//public string RowStatus { get; set; }

		[StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string SourceType { get; set; }

		public int? statecode { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string statecodename { get; set; }

		public int? statuscode { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string statuscodename { get; set; }

		public int? timezoneruleversionnumber { get; set; }

		public Guid transactioncurrencyid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		public int? utcconversiontimezonecode { get; set; }

		public long? versionnumber { get; set; }

		public decimal? xts_basequantityonpurchaseorder { get; set; }

		public decimal? xts_basequantityorder { get; set; }

		public Guid xts_baseunitid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_baseunitidname { get; set; }

		public Guid xts_businessunitid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_businessunitidname { get; set; }

		public bool? xts_closeline { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_closelinename { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_closereason { get; set; }

		public decimal? xts_consumptiontax1amount { get; set; }

		public decimal? xts_consumptiontax1amount_base { get; set; }

		public Guid xts_consumptiontax1id { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		public decimal? xts_consumptiontax2amount { get; set; }

		public decimal? xts_consumptiontax2amount_base { get; set; }

		public Guid xts_consumptiontax2id { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		public Guid xts_departmentid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_departmentidname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_description { get; set; }

		public decimal? xts_discountamount { get; set; }

		public decimal? xts_discountamount_base { get; set; }

		public decimal? xts_discountpercentage { get; set; }

		public string xts_eventdata { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_locking { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		public Guid xts_productconfigurationid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_productconfigurationidname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public string xts_productdescription { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		public Guid xts_productid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_productidname { get; set; }

		public Guid xts_productinteriorcolorid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_productinteriorcoloridname { get; set; }

		public Guid xts_productstyleid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_productstyleidname { get; set; }

		public DateTime? xts_promisedate { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? xts_purchasefor { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_purchaseforname { get; set; }

		public Guid xts_purchaseorderdetailid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_purchaseorderdetailidname { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_purchaserequisitiondetail { get; set; }

		public Guid xts_purchaserequisitiondetailid { get; set; }

		public Guid xts_purchaserequisitionid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_purchaserequisitionidname { get; set; }

		public Guid xts_purchaseunitid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_purchaseunitidname { get; set; }

		public decimal? xts_quantityonpurchaseorder { get; set; }

		public decimal? xts_quantityorder { get; set; }

		public Guid xts_requestedbyid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_requestedbyidname { get; set; }

		public DateTime? xts_requireddate { get; set; }

		public Guid xts_siteid { get; set; }

		[StringLength(320, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_siteidname { get; set; }

		public decimal? xts_totalamount { get; set; }

		public decimal? xts_totalamount_base { get; set; }

		public decimal? xts_totalamountbeforediscount { get; set; }

		public decimal? xts_totalamountbeforediscount_base { get; set; }

		public decimal? xts_totalbaseamount { get; set; }

		public decimal? xts_totalbaseamount_base { get; set; }

		public decimal? xts_totalconsumptiontaxamount { get; set; }

		public decimal? xts_totalconsumptiontaxamount_base { get; set; }

		public decimal? xts_transactionamount { get; set; }

		public decimal? xts_transactionamount_base { get; set; }

		public DateTime? xts_transactiondate { get; set; }

		public decimal? xts_unitcost { get; set; }

		public decimal? xts_unitcost_base { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class CRM_xts_purchaserequisitiondetailDeleteParameterDto
	{
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid xts_purchaserequisitiondetailid { get; set; }

		#region ModifiedOn ModifiedBy Delete
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public DateTime? modifiedon { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid modifiedby { get; set; }
		#endregion
	}

	public class CRM_xts_purchaserequisitiondetailUpdateParameterDto : CRM_xts_purchaserequisitiondetailParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_purchaserequisitiondetailid { get { return base.xts_purchaserequisitiondetailid; } set { base.xts_purchaserequisitiondetailid = value; } }

		#region ModifiedOn ModifiedBy Update
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? modifiedon { get { return base.modifiedon; } set { base.modifiedon = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid modifiedby { get { return base.modifiedby; } set { base.modifiedby = value; } }
		#endregion
	}

	public class CRM_xts_purchaserequisitiondetailCreateParameterDto : CRM_xts_purchaserequisitiondetailParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new int? statecode { get { return base.statecode; } set { base.statecode = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_businessunitid { get { return base.xts_businessunitid; } set { base.xts_businessunitid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_productid { get { return base.xts_productid; } set { base.xts_productid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new int? xts_purchasefor { get { return base.xts_purchasefor; } set { base.xts_purchasefor = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new string xts_purchaserequisitiondetail { get { return base.xts_purchaserequisitiondetail; } set { base.xts_purchaserequisitiondetail = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_purchaserequisitionid { get { return base.xts_purchaserequisitionid; } set { base.xts_purchaserequisitionid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new decimal? xts_quantityorder { get { return base.xts_quantityorder; } set { base.xts_quantityorder = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_siteid { get { return base.xts_siteid; } set { base.xts_siteid = value; } }

		#region Createdon CreatedBy ModifiedOn ModifiedBy
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? createdon { get { return base.createdon; } set { base.createdon = value; } }

		//[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid createdby { get { return base.createdby; } set { base.createdby = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? modifiedon { get { return base.modifiedon; } set { base.modifiedon = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid modifiedby { get { return base.modifiedby; } set { base.modifiedby = value; } }
		#endregion
	}

}
