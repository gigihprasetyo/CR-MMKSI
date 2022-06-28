#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_BusinessUnitDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2020 17:06:22
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_BusinessUnitDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string ftpsiteurl { get; set; }

		public Guid ktb_defaultprpotypeotheremergencyid { get; set; }

		public Guid xts_shiptovillageandstreetid { get; set; }

		public string xts_maincityidname { get; set; }

		public string xts_bankidname { get; set; }

		public string ktb_defaultteamforserviceidname { get; set; }

		public decimal ktb_batchgroupno { get; set; }

		public string ktb_accountnoservice1 { get; set; }

		public string xts_fax { get; set; }

		public string ktb_ownersuspectname { get; set; }

		public string xts_mainaddress3 { get; set; }

		public Guid xts_territory { get; set; }

		public Guid xts_dimension7id { get; set; }

		public Guid ktb_defaultteamforserviceid { get; set; }

		public string address1_stateorprovince { get; set; }

		public string xts_shiptoprovinceidname { get; set; }

		public string ktb_incominginterfaceallowedname { get; set; }

		public Guid ktb_purchaserequisitiondefaultprpotypeid { get; set; }

		public bool isdisabled { get; set; }

		public string xts_shiptoaddress4 { get; set; }

		public string ktb_usedmsname { get; set; }

		public Guid ktb_defaultwarehousepurchasevehicleid { get; set; }

		public Guid createdonbehalfby { get; set; }

		public Guid ktb_bucompanylookup { get; set; }

		public string emailaddress { get; set; }

		public string xts_dimension7idname { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string ktb_groupdealer { get; set; }

		public string xts_billtoprovinceidname { get; set; }

		public Guid parentbusinessunitid { get; set; }

		public string address1_telephone1 { get; set; }

		public string address1_telephone2 { get; set; }

		public string address1_telephone3 { get; set; }

		public string address1_postofficebox { get; set; }

		public Guid organizationid { get; set; }

		public Guid ktb_defaultwarehousepromopurchasepartid { get; set; }

		public string xts_billtocityidname { get; set; }

		public string xts_customeridname { get; set; }

		public Guid usergroupid { get; set; }

		public string fileasname { get; set; }

		public Guid ktb_defaultteamforvehicleid { get; set; }

		public Guid ktb_defaultpaymentbankid { get; set; }

		public string ktb_defaultteamforsparepartidyominame { get; set; }

		public int ktb_dealerid { get; set; }

		public int address2_utcoffset { get; set; }

		public Guid xts_dimension1id { get; set; }

		public string address2_fax { get; set; }

		public Guid xts_mainprovinceid { get; set; }

		public string xts_ledgerchartofaccountidname { get; set; }

		public string xts_dimension5idname { get; set; }

		public string ktb_purchaserequisitiondefaultprpotypeidname { get; set; }

		public decimal address2_longitude { get; set; }

		public string ktb_accountnovehicle1 { get; set; }

		public Guid xts_maincityid { get; set; }

		public string ktb_defaultsiteidname { get; set; }

		public string ktb_allocationdealername { get; set; }

		public string xts_billtocountryidname { get; set; }

		public string isdisabledname { get; set; }

		public string address1_shippingmethodcodename { get; set; }

		public string ktb_defaultwarehousepromopurchasepartidname { get; set; }

		public string modifiedbyyominame { get; set; }

		public bool ktb_usedms { get; set; }

		public string address1_line2 { get; set; }

		public string xts_mainaddress1 { get; set; }

		public Guid createdby { get; set; }

		public string ktb_accountnumber2 { get; set; }

		public string xts_onbehalfteamidname { get; set; }

		public string address2_addresstypecode { get; set; }

		public string xts_shiptocountryidname { get; set; }

		public string xts_billtovillageandstreetidname { get; set; }

		public string xts_organizationtypename { get; set; }

		public string ktb_defaultprpotypeotheremergencyidname { get; set; }

		public string address2_addresstypecodename { get; set; }

		public string xts_shiptocityidname { get; set; }

		public string ktb_defaultprpotypeforvehicleidname { get; set; }

		public string address1_postalcode { get; set; }

		public string tickersymbol { get; set; }

		public string address2_upszone { get; set; }

		public bool xts_newvehicle { get; set; }

		public Guid xts_vendorid { get; set; }

		public string xts_shortname { get; set; }

		public string xjp_accountyypename { get; set; }

		public string stockexchange { get; set; }

		public Guid ktb_defaultvendorpurchasevehicleid { get; set; }

		public Guid ktb_defaultlandedcostid { get; set; }

		public string ktb_defaultteamforvehicleidyominame { get; set; }

		public string ktb_bodypaintname { get; set; }

		public string msdyn_operatingunitid { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_locking { get; set; }

		public bool ktb_allocationdealer { get; set; }

		public string workflowsuspendedname { get; set; }

		public string organizationidname { get; set; }

		public string ktb_userdnet { get; set; }

		public Guid xts_shiptocityid { get; set; }

		public Int64 versionnumber { get; set; }

		public string xts_vendoridname { get; set; }

		public string ktb_defaultprpotypeotherreguleridname { get; set; }

		public string address1_name { get; set; }

		public string address1_fax { get; set; }

		public decimal address1_latitude { get; set; }

		public string xts_defaultsiteidname { get; set; }

		public string address2_shippingmethodcode { get; set; }

		public string ktb_defaultteamforvehiclepurchaseidname { get; set; }

		public string ktb_defaultteamforsparepartidname { get; set; }

		public Guid xts_onbehalfteamid { get; set; }

		public string modifiedbyname { get; set; }

		public string ktb_defaultlandedcostconsumptiontaxidname { get; set; }

		public string createdbyyominame { get; set; }

		public string xts_newvehiclesodefaultmatchingsiteidname { get; set; }

		public string address2_line1 { get; set; }

		public string address1_upszone { get; set; }

		public Guid address2_addressid { get; set; }

		public string ktb_defaultteaminterfacepartidyominame { get; set; }

		public int utcoffset { get; set; }

		public string address1_city { get; set; }

		public string xts_dimension8idname { get; set; }

		public int inheritancemask { get; set; }

		public string address1_line1 { get; set; }

		public string xts_billtoaddress4 { get; set; }

		public string ktb_defaultteamforvehicleidname { get; set; }

		public string xts_territoryname { get; set; }

		public DateTime modifiedon { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string websiteurl { get; set; }

		public string xts_usedvehiclename { get; set; }

		public decimal address1_longitude { get; set; }

		public string address1_addresstypecodename { get; set; }

		public string xts_newvehiclename { get; set; }

		public string address1_addresstypecode { get; set; }

		public bool ktb_incominginterfaceallowed { get; set; }

		public Guid bsi_defaultvendorfactoring { get; set; }

		public string xts_dimension9idname { get; set; }

		public string xts_mainprovinceidname { get; set; }

		public string ktb_defaultteamforvehiclepurchaseidyominame { get; set; }

		public Guid ktb_defaultteamforvehiclepurchaseid { get; set; }

		public Guid xts_dimension4id { get; set; }

		public string ktb_defaultteamforserviceidyominame { get; set; }

		public string address2_county { get; set; }

		public Guid xts_dimension5id { get; set; }

		public Guid ktb_defaultprpotypeotherregulerid { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_customeridyominame { get; set; }

		public string xts_organizationtype { get; set; }

		public string address1_shippingmethodcode { get; set; }

		public Guid ktb_defaultwarehouseregulerpurchasepartid { get; set; }

		public Guid ktb_defaultlandedcostdepositc2id { get; set; }

		public string ktb_defaultlandedcostdepositc2idname { get; set; }

		public string ktb_defaultteaminterfacepartidname { get; set; }

		public string xts_phone { get; set; }

		public string xts_accountname { get; set; }

		public Guid xts_dimension10id { get; set; }

		public bool xts_service { get; set; }

		public Guid ktb_defaultteaminterfacepartid { get; set; }

		public Guid ktb_defaultvendorfactoring { get; set; }

		public string msdyn_companycode { get; set; }

		public string xjp_accountyype { get; set; }

		public string address2_stateorprovince { get; set; }

		public string xts_dimension6idname { get; set; }

		public string ktb_dealerbranch { get; set; }

		public string ktb_dealercode { get; set; }

		public Guid xts_billtovillageandstreetid { get; set; }

		public string address2_country { get; set; }

		public string ktb_tokendnet { get; set; }

		public string address2_line2 { get; set; }

		public string costcenter { get; set; }

		public Guid ktb_defaultteamforsparepartid { get; set; }

		public string xts_mainvillageandstreetidname { get; set; }

		public Guid xts_mainvillageandstreetid { get; set; }

		public Guid xts_dimension2id { get; set; }

		public string address1_line3 { get; set; }

		public string ktb_defaultvendorfactoringname { get; set; }

		public bool workflowsuspended { get; set; }

		public Guid xts_dimension3id { get; set; }

		public Guid xts_billtocityid { get; set; }

		public string ktb_defaultteaminterfacevehicleidyominame { get; set; }

		public string ktb_ownersuspect { get; set; }

		public decimal creditlimit { get; set; }

		public Guid calendarid { get; set; }

		public string xts_mainaddress4 { get; set; }

		public string ktb_allowimportname { get; set; }

		public string ktb_bucompanylookupname { get; set; }

		public string xts_dimension10idname { get; set; }

		public int address1_utcoffset { get; set; }

		public Guid xts_ledgerchartofaccountid { get; set; }

		public string xts_shiptovillageandstreetidname { get; set; }

		public Guid xts_bankid { get; set; }

		public string ktb_defaultpaymentbankidname { get; set; }

		public string ktb_passworddnet { get; set; }

		public string ktb_defaultwarehousepurchasevehicleidname { get; set; }

		public Guid ktb_defaultsiteid { get; set; }

		public Guid xts_newvehiclesodefaultmatchingsiteid { get; set; }

		public decimal exchangerate { get; set; }

		public string xts_dimension4idname { get; set; }

		public Guid ktb_defaultprpotypeforvehicleid { get; set; }

		public Guid xts_dimension6id { get; set; }

		public string address2_city { get; set; }

		public string xts_servicename { get; set; }

		public string xts_taxregistrationnumber { get; set; }

		public string xts_maincountryidname { get; set; }

		public string xts_description2 { get; set; }

		public decimal address2_latitude { get; set; }

		public Guid xts_dimension8id { get; set; }

		public Guid xts_dimension9id { get; set; }

		public string xts_mainaddress2 { get; set; }

		public string divisionname { get; set; }

		public string ktb_defaultdownpaymentbankidname { get; set; }

		public string bsi_defaultvendorfactoringname { get; set; }

		public string xts_dimension2idname { get; set; }

		public string ktb_accountnovehicle2 { get; set; }

		public Guid xts_customerid { get; set; }

		public string ktb_dashboardtitle { get; set; }

		public bool xts_usedvehicle { get; set; }

		public Guid xts_billtoprovinceid { get; set; }

		public bool ktb_bodypaint { get; set; }

		public string address2_postalcode { get; set; }

		public Guid xts_defaultsiteid { get; set; }

		public Guid businessunitid { get; set; }

		public Guid xts_shiptoprovinceid { get; set; }

		public DateTime createdon { get; set; }

		public Guid xts_billtocountryid { get; set; }

		public string address2_shippingmethodcodename { get; set; }

		public string address2_line3 { get; set; }

		public string description { get; set; }

		public Guid modifiedby { get; set; }

		public string address1_county { get; set; }

		public string createdbyname { get; set; }

		public string ktb_defaultteaminterfacevehicleidname { get; set; }

		public string ktb_defaultlandedcostidname { get; set; }

		public string ktb_defaultvendorpurchasevehicleidname { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string xts_dimension3idname { get; set; }

		public string xts_mainpostalcode { get; set; }

		public string xts_onbehalfteamidyominame { get; set; }

		public string disabledreason { get; set; }

		public string address2_postofficebox { get; set; }

		public string address2_telephone1 { get; set; }

		public string address2_telephone2 { get; set; }

		public string address2_telephone3 { get; set; }

		public Guid ktb_defaultteaminterfacevehicleid { get; set; }

		public string xjp_representative { get; set; }

		public Guid address1_addressid { get; set; }

		public string picture { get; set; }

		public string parentbusinessunitidname { get; set; }

		public string ktb_defaultwarehouseregulerpurchasepartidname { get; set; }

		public Guid ktb_defaultlandedcostconsumptiontaxid { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string ktb_bucompany { get; set; }

		public string address2_name { get; set; }

		public string address1_country { get; set; }

		public string name { get; set; }

		public bool ktb_allowimport { get; set; }

		public string xts_accountnumber { get; set; }

		public string xts_dimension1idname { get; set; }

		public Guid xts_maincountryid { get; set; }

		public Guid ktb_defaultdownpaymentbankid { get; set; }

		public Guid xts_shiptocountryid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string ktb_accountnoservice2 { get; set; }

		public Guid xts_defaultsalesordersiteid { get; set; }

		public Guid xts_defaultworkordersiteid { get; set; }

		public Guid xts_defaultpurchasesiteid { get; set; }

		public string xts_defaultsalesordersiteidname { get; set; }

		public string xts_defaultworkordersiteidname { get; set; }

		public string xts_defaultpurchasesiteidname { get; set; }
    }
}
