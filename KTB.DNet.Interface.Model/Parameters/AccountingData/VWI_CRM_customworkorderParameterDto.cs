#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_customworkorderParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 8:22:47
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
	public class VWI_CRM_customworkorderParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
		public string company { get; set; }
		[AntiXss]
		public string businessunitcode { get; set; }
		[AntiXss]
		public Guid xts_contactpersonid { get; set; }
		[AntiXss]
		public Guid xts_vehiclesizeclassid { get; set; }
		[AntiXss]
		public int ktb_countedprint { get; set; }
		[AntiXss]
		public DateTime xts_actualservicestartdateandtime { get; set; }
		[AntiXss]
		public decimal xts_totalpartsamount { get; set; }
		[AntiXss]
		public decimal xts_esttotaltaxamount_base { get; set; }
		[AntiXss]
		public string ktb_vehiclestatename { get; set; }
		[AntiXss]
		public decimal xts_esttotalothersalestaxamount_base { get; set; }
		[AntiXss]
		public decimal xts_grandtotalamount_base { get; set; }
		[AntiXss]
		public string ktb_say { get; set; }
		[AntiXss]
		public decimal xts_esttotalworktaxamount_base { get; set; }
		[AntiXss]
		public decimal xts_estgrandtotalamount { get; set; }
		[AntiXss]
		public int xts_lastmileage { get; set; }
		[AntiXss]
		public string ktb_svcincominginterfacestatusname { get; set; }
		[AntiXss]
		public string xts_billtocustomeridyominame { get; set; }
		[AntiXss]
		public bool xts_invoiced { get; set; }
		[AntiXss]
		public decimal xts_totalpartstaxamount_base { get; set; }
		[AntiXss]
		public decimal xts_totalcostamount_base { get; set; }
		[AntiXss]
		public string xts_workorder { get; set; }
		[AntiXss]
		public string ktb_workordermemo { get; set; }
		[AntiXss]
		public DateTime xts_scheduledservicestartdateandtime { get; set; }
		[AntiXss]
		public decimal xts_estgrandtotalamount_base { get; set; }
		[AntiXss]
		public string xts_handling { get; set; }
		[AntiXss]
		public int xts_originaloutsorcereferencelookuptype { get; set; }
		[AntiXss]
		public decimal xts_totalothersalesnotallocated_base { get; set; }
		[AntiXss]
		public Int64 versionnumber { get; set; }
		[AntiXss]
		public decimal xts_grandtotalnotallocated { get; set; }
		[AntiXss]
		public string xts_queuestatus { get; set; }
		[AntiXss]
		public Guid xjp_originaloutsorcereferencepdiid { get; set; }
		[AntiXss]
		public string ktb_saleschannelidname { get; set; }
		[AntiXss]
		public decimal xts_esttotalpartstaxamount_base { get; set; }
		[AntiXss]
		public Guid xts_productsegment3id { get; set; }
		[AntiXss]
		public DateTime ktb_wodate { get; set; }
		[AntiXss]
		public Guid xts_billtocustomerid { get; set; }
		[AntiXss]
		public decimal xts_totalpaymentamount_base { get; set; }
		[AntiXss]
		public string xts_billtocustomeridname { get; set; }
		[AntiXss]
		public string xts_customernumber { get; set; }
		[AntiXss]
		public decimal xts_esttotalpartstaxamount { get; set; }
		[AntiXss]
		public Guid xts_productsegment1id { get; set; }
		[AntiXss]
		public string ktb_liststatusinterface { get; set; }
		[AntiXss]
		public string ktb_ordertypetranstype { get; set; }
		[AntiXss]
		public string xts_workorderstatus { get; set; }
		[AntiXss]
		public decimal ktb_totalpartamount { get; set; }
		[AntiXss]
		public DateTime xts_actualservicefinishdateandtime { get; set; }
		[AntiXss]
		public string xts_productdescription { get; set; }
		[AntiXss]
		public Guid transactioncurrencyid { get; set; }
		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }
		[AntiXss]
		public decimal xts_esttotaltaxamount { get; set; }
		[AntiXss]
		public Guid xts_originaloutsorcereferenceoutsourcewoid { get; set; }
		[AntiXss]
		public decimal xts_totalmiscchargebasenotallocated { get; set; }
		[AntiXss]
		public bool xts_isworkorderform { get; set; }
		[AntiXss]
		public decimal xts_esttotalpartsamount { get; set; }
		[AntiXss]
		public decimal xts_esttotalpartsbaseamount_base { get; set; }
		[AntiXss]
		public decimal xts_totalwithholdingtaxnotallocated { get; set; }
		[AntiXss]
		public string xts_dimension10idname { get; set; }
		[AntiXss]
		public decimal xts_totaltaxamount { get; set; }
		[AntiXss]
		public decimal exchangerate { get; set; }
		[AntiXss]
		public decimal xts_balance { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }
		[AntiXss]
		public string xts_pickupaddress1 { get; set; }
		[AntiXss]
		public decimal xts_totalpartsbaseallocated { get; set; }
		[AntiXss]
		public Guid xts_workorderid { get; set; }
		[AntiXss]
		public decimal xts_totalpartstaxnotallocated_base { get; set; }
		[AntiXss]
		public decimal xts_totalmiscchargeallocated_base { get; set; }
		[AntiXss]
		public string xts_claimapprovalnumber { get; set; }
		[AntiXss]
		public decimal xts_totalpartsbaseamount { get; set; }
		[AntiXss]
		public decimal xts_grandtotalnotallocated_base { get; set; }
		[AntiXss]
		public string ktb_paneldescription { get; set; }
		[AntiXss]
		public decimal xts_downpaymentamount { get; set; }
		[AntiXss]
		public Guid xts_serviceadvisorid { get; set; }
		[AntiXss]
		public Guid xts_siteid { get; set; }
		[AntiXss]
		public bool ktb_validatorhandling { get; set; }
		[AntiXss]
		public string xts_contactpersonphone { get; set; }
		[AntiXss]
		public string xts_dimension8idname { get; set; }
		[AntiXss]
		public bool ktb_isinsurance { get; set; }
		[AntiXss]
		public DateTime createdon { get; set; }
		[AntiXss]
		public DateTime xts_invoicepostingdate { get; set; }
		[AntiXss]
		public decimal xts_totalworktaxamount { get; set; }
		[AntiXss]
		public decimal xts_totalothersalesallocated { get; set; }
		[AntiXss]
		public decimal ktb_totalserviceamount { get; set; }
		[AntiXss]
		public string xts_deliveryaddress4 { get; set; }
		[AntiXss]
		public string xts_servicecategoryidname { get; set; }
		[AntiXss]
		public int xts_currentmileage { get; set; }
		[AntiXss]
		public string xts_claimstatus { get; set; }
		[AntiXss]
		public decimal xts_totalpartsbasenotallocated { get; set; }
		[AntiXss]
		public string xts_chassisnumber { get; set; }
		[AntiXss]
		public decimal xts_totalothersalestaxamount { get; set; }
		[AntiXss]
		public decimal xts_totalpartstaxnotallocated { get; set; }
		[AntiXss]
		public bool ktb_isinterfaced { get; set; }
		[AntiXss]
		public int xts_historymileage { get; set; }
		[AntiXss]
		public decimal xts_totalworkbasenotallocated_base { get; set; }
		[AntiXss]
		public decimal xts_grandtotalamount { get; set; }
		[AntiXss]
		public int statecode { get; set; }
		[AntiXss]
		public int statuscode { get; set; }
		[AntiXss]
		public decimal xts_totalothersalesbaseamount { get; set; }
		[AntiXss]
		public decimal xts_totalpartstaxamount { get; set; }
		[AntiXss]
		public decimal xts_totalworkamount { get; set; }
		[AntiXss]
		public DateTime xts_scheduledarrivaldateandtime { get; set; }
		[AntiXss]
		public string ktb_statusinterface { get; set; }
		[AntiXss]
		public DateTime xts_transactiondate { get; set; }
		[AntiXss]
		public Guid owninguser { get; set; }
		[AntiXss]
		public bool ktb_isfreebilltocustomer { get; set; }
		[AntiXss]
		public decimal xts_totalmiscchargetaxnotallocated_base { get; set; }
		[AntiXss]
		public decimal xts_esttotalbaseamount_base { get; set; }
		[AntiXss]
		public decimal xts_totalpartsamount_base { get; set; }
		[AntiXss]
		public string xts_downpaymentispaidname { get; set; }
		[AntiXss]
		public string ktb_contactmobilephonenumber { get; set; }
		[AntiXss]
		public Guid xts_vehicleidentificationid { get; set; }
		[AntiXss]
		public decimal xts_totalpartsnotallocated_base { get; set; }
		[AntiXss]
		public decimal xts_totalpartstaxallocated { get; set; }
		[AntiXss]
		public decimal xts_esttotalbaseamount { get; set; }
		[AntiXss]
		public Guid xts_exchangeratetypeid { get; set; }
		[AntiXss]
		public DateTime xts_scheduledservicefinishdateandtime { get; set; }
		[AntiXss]
		public string ktb_workorderdescription { get; set; }
		[AntiXss]
		public decimal xts_totalpartsbaseamount_base { get; set; }
		[AntiXss]
		public decimal xts_esttotalothersalesbaseamount { get; set; }
		[AntiXss]
		public string xts_manufacturername { get; set; }
		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }
		[AntiXss]
		public string xts_deliveryaddress1 { get; set; }
		[AntiXss]
		public decimal xts_esttotalpartsbaseamount { get; set; }
		[AntiXss]
		public decimal xts_totalothersalesnotallocated { get; set; }
		[AntiXss]
		public decimal xts_totaltaxamount_base { get; set; }
		[AntiXss]
		public decimal xts_totalpartsnotallocated { get; set; }
		[AntiXss]
		public string xts_referralname { get; set; }
		[AntiXss]
		public decimal xts_totalcostamount { get; set; }
		[AntiXss]
		public decimal ktb_totaloilamount { get; set; }
		[AntiXss]
		public int utcconversiontimezonecode { get; set; }
		[AntiXss]
		public decimal xts_totalpartsbasenotallocated_base { get; set; }
		[AntiXss]
		public Guid ktb_contactid { get; set; }
		[AntiXss]
		public DateTime modifiedon { get; set; }
		[AntiXss]
		public DateTime xts_scheduledfinishdateandtime { get; set; }
		[AntiXss]
		public Guid xts_dimension2id { get; set; }
		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }
		[AntiXss]
		public string ktb_dnetidwotype { get; set; }
		[AntiXss]
		public string xts_originaloutsorcereferencesvcinstructionidname { get; set; }
		[AntiXss]
		public decimal xts_esttotalpartsamount_base { get; set; }
		[AntiXss]
		public decimal ktb_grandtotalinformationdetail { get; set; }
		[AntiXss]
		public string msdyn_companycode { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}
