#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchasereceiptdetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 14/01/2020 9:03:10
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
    public class VWI_CRM_xts_purchasereceiptdetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public decimal xts_discountpercentage { get; set; }

		[AntiXss]
		public decimal xts_landedcost { get; set; }

		[AntiXss]
		public decimal bsi_interest_base { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public string xts_returnpurchasereceiptdetailidname { get; set; }

		[AntiXss]
		public Guid xts_purchasereceiptdetailid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public decimal xts_totalamount { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal ktb_consumptiontax1amountinterface_base { get; set; }

		[AntiXss]
		public string xts_processedname { get; set; }

		[AntiXss]
		public decimal ktb_interest_base { get; set; }

		[AntiXss]
		public decimal xts_productweight { get; set; }

		[AntiXss]
		public Guid xts_returnpurchasereceiptdetailid { get; set; }

		[AntiXss]
		public decimal ktb_discountreward { get; set; }

		[AntiXss]
		public decimal ktb_parkingamount { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_chassismodel { get; set; }

		[AntiXss]
		public decimal xts_unitcost_base { get; set; }

		[AntiXss]
		public decimal xts_discountamount_base { get; set; }

		[AntiXss]
		public string ktb_salesorderno { get; set; }

		[AntiXss]
		public decimal ktb_discountdnet { get; set; }

		[AntiXss]
		public string ktb_materialdescription { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_locationid { get; set; }

		[AntiXss]
		public Guid xts_stockid { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount { get; set; }

		[AntiXss]
		public string ktb_vehiclekinddescription { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public decimal ktb_netpricednet_base { get; set; }

		[AntiXss]
		public decimal xts_totalvolume { get; set; }

		[AntiXss]
		public decimal ktb_ppn { get; set; }

		[AntiXss]
		public Guid xts_purchaseorderdetailid { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_batchidname { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_totalamount_base { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public decimal ktb_hargapokok { get; set; }

		[AntiXss]
		public string xts_purchasereceiptidname { get; set; }

		[AntiXss]
		public decimal xts_receivedquantity { get; set; }

		[AntiXss]
		public string xts_stockidname { get; set; }

		[AntiXss]
		public bool ktb_updatetosparepartstock { get; set; }

		[AntiXss]
		public Guid xts_reasoncodeid { get; set; }

		[AntiXss]
		public string xts_purchaseorderdetailidname { get; set; }

		[AntiXss]
		public string xts_servicepartandmaterialidname { get; set; }

		[AntiXss]
		public string xts_withholdingtaxidname { get; set; }

		[AntiXss]
		public decimal xts_landedcost_base { get; set; }

		[AntiXss]
		public string xts_inventoryunitidname { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public decimal bsi_interest { get; set; }

		[AntiXss]
		public decimal xts_productvolume { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_purchaseunitidname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public Guid xts_productinteriorcolorid { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount_base { get; set; }

		[AntiXss]
		public Guid xts_withholdingtaxid { get; set; }

		[AntiXss]
		public string xts_departmentidname { get; set; }

		[AntiXss]
		public decimal xts_unitcost { get; set; }

		[AntiXss]
		public decimal ktb_hargapokok_base { get; set; }

		[AntiXss]
		public decimal xts_qtyreturned { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public decimal xts_discountamount { get; set; }

		[AntiXss]
		public Guid xts_servicepartandmaterialid { get; set; }

		[AntiXss]
		public string ktb_productsap { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid xts_departmentid { get; set; }

		[AntiXss]
		public decimal xts_totalweight { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_productconfigurationidname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_basereceivedquantity { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public Guid xts_purchasereceiptid { get; set; }

		[AntiXss]
		public decimal bsi_discountreward_base { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public decimal ktb_discountdnet_base { get; set; }

		[AntiXss]
		public decimal ktb_discpercentage { get; set; }

		[AntiXss]
		public decimal ktb_ppn_base { get; set; }

		[AntiXss]
		public decimal bsi_discountreward { get; set; }

		[AntiXss]
		public string ktb_vehiclekindcode { get; set; }

		[AntiXss]
		public decimal ktb_parkingamount_base { get; set; }

		[AntiXss]
		public string ktb_materialnumber { get; set; }

		[AntiXss]
		public decimal ktb_discountreward_base { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public bool xts_processed { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee { get; set; }

		[AntiXss]
		public int ktb_endpoint { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_batchid { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string ktb_purchaseorderno { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public string xts_locationidname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public decimal ktb_consumptiontax1amountinterface { get; set; }

		[AntiXss]
		public string ktb_updatetosparepartstockname { get; set; }

		[AntiXss]
		public DateTime ktb_gidate { get; set; }

		[AntiXss]
		public Guid xts_productstyleid { get; set; }

		[AntiXss]
		public decimal ktb_netpricednet { get; set; }

		[AntiXss]
		public Guid xts_inventoryunitid { get; set; }

		[AntiXss]
		public string xts_productinteriorcoloridname { get; set; }

		[AntiXss]
		public string ktb_categoryno { get; set; }

		[AntiXss]
		public string xts_purchasereceiptdetail { get; set; }

		[AntiXss]
		public string xts_processgroup { get; set; }

		[AntiXss]
		public int ktb_discountpercentage { get; set; }

		[AntiXss]
		public decimal ktb_interest { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string ktb_deliveryorderno { get; set; }

		[AntiXss]
		public string xts_referencenumber { get; set; }

		[AntiXss]
		public decimal xts_transactionamount_base { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public decimal xts_baseqtyreturned { get; set; }

		[AntiXss]
		public string xts_productionyear { get; set; }

		[AntiXss]
		public string xts_chassisnumberregister { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public Guid xts_purchaseunitid { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee_base { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public Guid xts_productconfigurationid { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public decimal xts_transactionamount { get; set; }

		[AntiXss]
		public string xts_engineno { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount { get; set; }

		[AntiXss]
		public string xts_keyno { get; set; }

		[AntiXss]
		public string xts_reasoncodeidname { get; set; }

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
