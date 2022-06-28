#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_outsourceworkorderreceiptdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 18 Jan 2021 16:16:54
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
    public class CRM_xts_outsourceworkorderreceiptdetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        
		public Guid createdby { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdbyname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdbyyominame { get; set; }

		public DateTime? createdon { get; set; }

		public Guid createdonbehalfby { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string DealerCode { get; set; }

		public decimal? exchangerate { get; set; }

		public int? importsequencenumber { get; set; }

		public Guid modifiedby { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedbyname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedbyyominame { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		public DateTime? overriddencreatedon { get; set; }

		public Guid ownerid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridname { get; set; }

		[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string owneridtype { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
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

		public string statecodename { get; set; }

		public int? statuscode { get; set; }

		public string statuscodename { get; set; }

		public int? timezoneruleversionnumber { get; set; }

		public Guid transactioncurrencyid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		public int? utcconversiontimezonecode { get; set; }

		public long? versionnumber { get; set; }

		public Guid xts_businessunitid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_commentdescription { get; set; }

		public Guid xts_commentid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_commentidname { get; set; }

		public decimal? xts_consumptiontax1amount { get; set; }

		public decimal? xts_consumptiontax1amount_base { get; set; }

		public Guid xts_consumptiontax1id { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		public decimal? xts_consumptiontax2amount { get; set; }

		public decimal? xts_consumptiontax2amount_base { get; set; }

		public Guid xts_consumptiontax2id { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		public decimal? xts_discountamount { get; set; }

		public decimal? xts_discountamount_base { get; set; }

		public decimal? xts_discountbaseamount { get; set; }

		public decimal? xts_discountbaseamount_base { get; set; }

		public decimal? xts_discountrate { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_locking { get; set; }

		public Guid xts_originalworkorderdetailreferenceid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_originalworkorderdetailreferenceidname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_outsourceworkorderdetail { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_outsourceworkorderreceiptdetail { get; set; }

		public Guid xts_outsourceworkorderreceiptdetailid { get; set; }

		public Guid xts_outsourceworkorderreceiptid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_outsourceworkorderreceiptidname { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public string xts_productdescription { get; set; }

		public Guid xts_productid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_productidname { get; set; }

		public Guid xts_productsegment1id { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_productsegment1idname { get; set; }

		public Guid xts_productsegment2id { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_productsegment2idname { get; set; }

		public Guid xts_productsegment3id { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_productsegment3idname { get; set; }

		public Guid xts_productsegment4id { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_productsegment4idname { get; set; }

		//[StringLength(510, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		//[AntiXss]
		public int? xts_producttype { get; set; }

		public string xts_producttypename { get; set; }

		public decimal? xts_quantitymanhour { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_servicecategorydescription { get; set; }

		public Guid xts_servicecategoryid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_servicecategoryidname { get; set; }

		public Guid xts_servicepartsandmaterialsid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_servicepartsandmaterialsidname { get; set; }

		public decimal? xts_totalamountbeforediscount { get; set; }

		public decimal? xts_totalamountbeforediscount_base { get; set; }

		public decimal? xts_totalbaseamount { get; set; }

		public decimal? xts_totalbaseamount_base { get; set; }

		public decimal? xts_totalconsumptiontaxamount { get; set; }

		public decimal? xts_totalconsumptiontaxamount_base { get; set; }

		public decimal? xts_totaltransactionamount { get; set; }

		public decimal? xts_totaltransactionamount_base { get; set; }

		public decimal? xts_transactionamount { get; set; }

		public decimal? xts_transactionamount_base { get; set; }

		public Guid xts_unitid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_unitidname { get; set; }

		public decimal? xts_unitpricelaborrate { get; set; }

		public decimal? xts_unitpricelaborrate_base { get; set; }

		public decimal? xts_withholdingtaxamount { get; set; }

		public decimal? xts_withholdingtaxamount_base { get; set; }

		public Guid xts_withholdingtaxid { get; set; }

		[StringLength(3000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
		[AntiXss]
		public string xts_withholdingtaxidname { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class CRM_xts_outsourceworkorderreceiptdetailDeleteParameterDto
	{
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid xts_outsourceworkorderreceiptdetailid { get; set; }

		#region ModifiedOn ModifiedBy Delete
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public DateTime? modifiedon { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public Guid modifiedby { get; set; }
		#endregion
	}

	public class CRM_xts_outsourceworkorderreceiptdetailUpdateParameterDto : CRM_xts_outsourceworkorderreceiptdetailParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_outsourceworkorderreceiptdetailid { get { return base.xts_outsourceworkorderreceiptdetailid; } set { base.xts_outsourceworkorderreceiptdetailid = value; } }

		#region ModifiedOn ModifiedBy Update
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new DateTime? modifiedon { get { return base.modifiedon; } set { base.modifiedon = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid modifiedby { get { return base.modifiedby; } set { base.modifiedby = value; } }
		#endregion
	}

	public class CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto : CRM_xts_outsourceworkorderreceiptdetailParameterDto
    {
        
		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_businessunitid { get { return base.xts_businessunitid; } set { base.xts_businessunitid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new string xts_outsourceworkorderreceiptdetail { get { return base.xts_outsourceworkorderreceiptdetail; } set { base.xts_outsourceworkorderreceiptdetail = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_outsourceworkorderreceiptid { get { return base.xts_outsourceworkorderreceiptid; } set { base.xts_outsourceworkorderreceiptid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid ownerid { get { return base.ownerid; } set { base.ownerid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new Guid xts_productid { get { return base.xts_productid; } set { base.xts_productid = value; } }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
		public new decimal? xts_quantitymanhour { get { return base.xts_quantitymanhour; } set { base.xts_quantitymanhour = value; } }

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