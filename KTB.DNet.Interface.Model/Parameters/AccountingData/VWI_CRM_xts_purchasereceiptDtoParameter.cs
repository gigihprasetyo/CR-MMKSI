#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchasereceiptParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 8:22:50
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
    public class VWI_CRM_xts_purchasereceiptParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_vendordescription { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public bool ktb_updatetosparepartstock { get; set; }

		[AntiXss]
		public Guid ktb_purchaserequisitionid { get; set; }

		[AntiXss]
		public string ktb_isfactoringname { get; set; }

		[AntiXss]
		public Guid xts_provinceid { get; set; }

		[AntiXss]
		public DateTime ktb_packingdate { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public DateTime xts_vendorinvoicedate { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string ktb_interfacehandling { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_vendorinvoicenumber { get; set; }

		[AntiXss]
		public string ktb_identificationtypename { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public Guid xts_termsofpaymentid { get; set; }

		[AntiXss]
		public bool xts_autoinvoiced { get; set; }

		[AntiXss]
		public string xts_loaddataname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxamount { get; set; }

		[AntiXss]
		public string xts_accountpayablevoucheridname { get; set; }

		[AntiXss]
		public string ktb_interfacestatusname { get; set; }

		[AntiXss]
		public bool xts_purchasereceiptreferencerequired { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string xts_provinceidname { get; set; }

		[AntiXss]
		public bool xts_assignlandedcostperdetail { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public string xts_address2 { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public string xts_address4 { get; set; }

		[AntiXss]
		public Guid xts_returnpurchasereceiptid { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public decimal xts_totalwithholdingtaxamount_base { get; set; }

		[AntiXss]
		public string ktb_updatetosparepartstockname { get; set; }

		[AntiXss]
		public string xts_termsofpaymentidname { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xts_accountpayablevoucherid { get; set; }

		[AntiXss]
		public DateTime xts_deliveryorderdate { get; set; }

		[AntiXss]
		public DateTime ktb_paymentdate { get; set; }

		[AntiXss]
		public DateTime ktb_goodissuedate { get; set; }

		[AntiXss]
		public DateTime ktb_ata { get; set; }

		[AntiXss]
		public bool xts_loaddata { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_purchasereceiptnumber { get; set; }

		[AntiXss]
		public string ktb_salesorderno { get; set; }

		[AntiXss]
		public DateTime ktb_pickingdate { get; set; }

		[AntiXss]
		public string xts_assignlandedcostperdetailname { get; set; }

		[AntiXss]
		public decimal xts_totaltitleregistrationfee_base { get; set; }

		[AntiXss]
		public Guid xts_countryid { get; set; }

		[AntiXss]
		public DateTime ktb_duedate { get; set; }

		[AntiXss]
		public string xts_returnpurchasereceiptidname { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public Guid xts_cityid { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_autoinvoicedname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_deliveryordernumber { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public decimal xts_totaltitleregistrationfee { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xts_workorderidname { get; set; }

		[AntiXss]
		public string xts_countryidname { get; set; }

		[AntiXss]
		public Guid xts_transferorderrequestingid { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontax1amount { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public bool ktb_isfactoring { get; set; }

		[AntiXss]
		public string xts_purchaseorderidname { get; set; }

		[AntiXss]
		public Guid xts_workorderid { get; set; }

		[AntiXss]
		public string xts_eventdata2 { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string ktb_interfacestatus { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public DateTime ktb_deliveryfordeliverydate { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_address3 { get; set; }

		[AntiXss]
		public DateTime ktb_invoicedate { get; set; }

		[AntiXss]
		public DateTime ktb_estimationdeliverydate { get; set; }

		[AntiXss]
		public string xts_cityidname { get; set; }

		[AntiXss]
		public Guid xts_purchasereceiptid { get; set; }

		[AntiXss]
		public bool xts_bigdata { get; set; }

		[AntiXss]
		public string ktb_ribbondataproductwarehouse { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public decimal xts_grandtotal { get; set; }

		[AntiXss]
		public decimal xts_grandtotal_base { get; set; }

		[AntiXss]
		public string xts_postalcode { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public DateTime xts_packingslipdate { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_address1 { get; set; }

		[AntiXss]
		public string ktb_ribbondata { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string ktb_identificationtype { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid ktb_prpotypeid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_packingslipnumber { get; set; }

		[AntiXss]
		public Guid xts_purchaseorderid { get; set; }

		[AntiXss]
		public string ktb_purchaserequisitionidname { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontax2amount { get; set; }

		[AntiXss]
		public string ktb_interfacehandlingname { get; set; }

		[AntiXss]
		public Guid xts_vendorid { get; set; }

		[AntiXss]
		public string xts_bigdataname { get; set; }

		[AntiXss]
		public string ktb_expeditionnumber { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal ktb_conversiondays { get; set; }

		[AntiXss]
		public string xts_purchasereceiptreferencerequiredname { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontax2amount_base { get; set; }

		[AntiXss]
		public int ktb_discountpercentage { get; set; }

		[AntiXss]
		public string xts_transferorderrequestingidname { get; set; }

		[AntiXss]
		public string ktb_returnreason { get; set; }

		[AntiXss]
		public string ktb_prpotypeidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontax1amount_base { get; set; }

		[AntiXss]
		public string xts_vendoridname { get; set; }

		[AntiXss]
		public DateTime ktb_eta { get; set; }

		[AntiXss]
		public DateTime ktb_atd { get; set; }

		[AntiXss]
		public string ktb_kondisikendaraan { get; set; }

		[AntiXss]
		public int ktb_endpoint { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
