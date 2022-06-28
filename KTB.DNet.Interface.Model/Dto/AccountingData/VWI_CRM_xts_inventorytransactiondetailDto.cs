#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventorytransactiondetailDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 1/9/2020 1:13:13 PM
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_inventorytransactiondetailDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_productidname { get; set; }

		public string ktb_chassisno { get; set; }

		public string xts_departmentidname { get; set; }

		public string xts_warehouseidname { get; set; }

		public string xts_referencenumber { get; set; }

		public decimal xts_unitcost_base { get; set; }

		public Guid xts_inventorytransactiondetailid { get; set; }

		public decimal xts_basequantity { get; set; }

		public string xts_batchidname { get; set; }

		public string createdbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public decimal ktb_latestpurchaseprice_base { get; set; }

		public string owneridtype { get; set; }

		public string xts_locationidname { get; set; }

		public DateTime modifiedon { get; set; }

		public string xts_siteidname { get; set; }

		public Guid owningteam { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string xts_reasonidname { get; set; }

		public string xts_serialidname { get; set; }

		public Guid xts_inventorytransactiondetailrefid { get; set; }

		public decimal ktb_cogstrx_base { get; set; }

		public string xts_productconfigurationidname { get; set; }

		public string modifiedbyname { get; set; }

		public string xts_productdescription { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid xts_transactionunitid { get; set; }

		public Guid ownerid { get; set; }

		public string xts_stocknumbernvidname { get; set; }

		public string xts_productexteriorcoloridname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_productcrossreferenceidname { get; set; }

		public string owneridyominame { get; set; }

		public decimal ktb_latestpurchaseprice { get; set; }

		public Guid xts_inventoryunitid { get; set; }

		public int statuscode { get; set; }

		public Guid xts_siteid { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public string xts_businessunitidname { get; set; }

		public Guid xts_frombusinessunitid { get; set; }

		public string owneridname { get; set; }

		public int importsequencenumber { get; set; }

		public string modifiedbyyominame { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public Guid xts_locationid { get; set; }

		public Guid xts_productcrossreferenceid { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string xts_inventorytransferdetailidname { get; set; }

		public string xts_servicepartsandmaterialidname { get; set; }

		public Guid owninguser { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_inventorytransactionidname { get; set; }

		public string xts_inventorytransactiondetail { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_inventorytransactiondetailrefidname { get; set; }

		public Guid xts_serialid { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string xts_description { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string xts_frombusinessunitidname { get; set; }

		public Guid xts_stockid { get; set; }

		public string xts_vehicleidentificationnumber { get; set; }

		public Guid xts_stocknumbernvid { get; set; }

		public Guid xts_inventorytransferdetailid { get; set; }

		public decimal xts_unitcost { get; set; }

		public string xts_registerserialnumber { get; set; }

		public decimal xts_totalcost_base { get; set; }

		public string xts_transactionunitidname { get; set; }

		public int xts_runningnumber { get; set; }

		public string xts_inventoryunitidname { get; set; }

		public string xts_sourcedata { get; set; }

		public string statuscodename { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string statecodename { get; set; }

		public Guid xts_reasonid { get; set; }

		public string xts_stockidname { get; set; }

		public Guid modifiedby { get; set; }

		public string ktb_updatetosparepartstockname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public decimal exchangerate { get; set; }

		public Guid xts_productid { get; set; }

		public Guid xts_productinteriorcolorid { get; set; }

		public decimal xts_totalcost { get; set; }

		public string createdbyyominame { get; set; }

		public Guid createdby { get; set; }

		public Guid xts_departmentid { get; set; }

		public DateTime createdon { get; set; }

		public decimal xts_quantityreturn { get; set; }

		public Int64 versionnumber { get; set; }

		public decimal ktb_cogstrx { get; set; }

		public Guid xts_productstyleid { get; set; }

		public Guid xts_batchid { get; set; }

		public Guid xts_warehouseid { get; set; }

		public decimal xts_quantity { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_productstyleidname { get; set; }

		public Guid xts_inventorytransactionid { get; set; }

		public string xts_locking { get; set; }

		public int statecode { get; set; }

		public Guid xts_servicepartsandmaterialid { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public string xts_productinteriorcoloridname { get; set; }

		public Guid xts_productconfigurationid { get; set; }

		public string xts_transactiontypename { get; set; }

		public string xts_transactiontype { get; set; }

		public string createdonbehalfbyname { get; set; }

		public bool ktb_updatetosparepartstock { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
