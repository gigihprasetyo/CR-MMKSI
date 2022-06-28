#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_BusinessUnitParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_BusinessUnitParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ftpsiteurl { get; set; }

		[AntiXss]
		public Guid ktb_defaultprpotypeotheremergencyid { get; set; }

		[AntiXss]
		public Guid xts_shiptovillageandstreetid { get; set; }

		[AntiXss]
		public string xts_maincityidname { get; set; }

		[AntiXss]
		public string xts_bankidname { get; set; }

		[AntiXss]
		public string ktb_defaultteamforserviceidname { get; set; }

		[AntiXss]
		public decimal ktb_batchgroupno { get; set; }

		[AntiXss]
		public string ktb_accountnoservice1 { get; set; }

		[AntiXss]
		public string xts_fax { get; set; }

		[AntiXss]
		public string ktb_ownersuspectname { get; set; }

		[AntiXss]
		public string xts_mainaddress3 { get; set; }

		[AntiXss]
		public Guid xts_territory { get; set; }

		[AntiXss]
		public Guid xts_dimension7id { get; set; }

		[AntiXss]
		public Guid ktb_defaultteamforserviceid { get; set; }

		[AntiXss]
		public string address1_stateorprovince { get; set; }

		[AntiXss]
		public string xts_shiptoprovinceidname { get; set; }

		[AntiXss]
		public string ktb_incominginterfaceallowedname { get; set; }

		[AntiXss]
		public Guid ktb_purchaserequisitiondefaultprpotypeid { get; set; }

		[AntiXss]
		public bool isdisabled { get; set; }

		[AntiXss]
		public string xts_shiptoaddress4 { get; set; }

		[AntiXss]
		public string ktb_usedmsname { get; set; }

		[AntiXss]
		public Guid ktb_defaultwarehousepurchasevehicleid { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid ktb_bucompanylookup { get; set; }

		[AntiXss]
		public string emailaddress { get; set; }

		[AntiXss]
		public string xts_dimension7idname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string ktb_groupdealer { get; set; }

		[AntiXss]
		public string xts_billtoprovinceidname { get; set; }

		[AntiXss]
		public Guid parentbusinessunitid { get; set; }

		[AntiXss]
		public string address1_telephone1 { get; set; }

		[AntiXss]
		public string address1_telephone2 { get; set; }

		[AntiXss]
		public string address1_telephone3 { get; set; }

		[AntiXss]
		public string address1_postofficebox { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public Guid ktb_defaultwarehousepromopurchasepartid { get; set; }

		[AntiXss]
		public string xts_billtocityidname { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public Guid usergroupid { get; set; }

		[AntiXss]
		public string fileasname { get; set; }

		[AntiXss]
		public Guid ktb_defaultteamforvehicleid { get; set; }

		[AntiXss]
		public Guid ktb_defaultpaymentbankid { get; set; }

		[AntiXss]
		public string ktb_defaultteamforsparepartidyominame { get; set; }

		[AntiXss]
		public int ktb_dealerid { get; set; }

		[AntiXss]
		public int address2_utcoffset { get; set; }

		[AntiXss]
		public Guid xts_dimension1id { get; set; }

		[AntiXss]
		public string address2_fax { get; set; }

		[AntiXss]
		public Guid xts_mainprovinceid { get; set; }

		[AntiXss]
		public string xts_ledgerchartofaccountidname { get; set; }

		[AntiXss]
		public string xts_dimension5idname { get; set; }

		[AntiXss]
		public string ktb_purchaserequisitiondefaultprpotypeidname { get; set; }

		[AntiXss]
		public decimal address2_longitude { get; set; }

		[AntiXss]
		public string ktb_accountnovehicle1 { get; set; }

		[AntiXss]
		public Guid xts_maincityid { get; set; }

		[AntiXss]
		public string ktb_defaultsiteidname { get; set; }

		[AntiXss]
		public string ktb_allocationdealername { get; set; }

		[AntiXss]
		public string xts_billtocountryidname { get; set; }

		[AntiXss]
		public string isdisabledname { get; set; }

		[AntiXss]
		public string address1_shippingmethodcodename { get; set; }

		[AntiXss]
		public string ktb_defaultwarehousepromopurchasepartidname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public bool ktb_usedms { get; set; }

		[AntiXss]
		public string address1_line2 { get; set; }

		[AntiXss]
		public string xts_mainaddress1 { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string ktb_accountnumber2 { get; set; }

		[AntiXss]
		public string xts_onbehalfteamidname { get; set; }

		[AntiXss]
		public string address2_addresstypecode { get; set; }

		[AntiXss]
		public string xts_shiptocountryidname { get; set; }

		[AntiXss]
		public string xts_billtovillageandstreetidname { get; set; }

		[AntiXss]
		public string xts_organizationtypename { get; set; }

		[AntiXss]
		public string ktb_defaultprpotypeotheremergencyidname { get; set; }

		[AntiXss]
		public string address2_addresstypecodename { get; set; }

		[AntiXss]
		public string xts_shiptocityidname { get; set; }

		[AntiXss]
		public string ktb_defaultprpotypeforvehicleidname { get; set; }

		[AntiXss]
		public string address1_postalcode { get; set; }

		[AntiXss]
		public string tickersymbol { get; set; }

		[AntiXss]
		public string address2_upszone { get; set; }

		[AntiXss]
		public bool xts_newvehicle { get; set; }

		[AntiXss]
		public Guid xts_vendorid { get; set; }

		[AntiXss]
		public string xts_shortname { get; set; }

		[AntiXss]
		public string xjp_accountyypename { get; set; }

		[AntiXss]
		public string stockexchange { get; set; }

		[AntiXss]
		public Guid ktb_defaultvendorpurchasevehicleid { get; set; }

		[AntiXss]
		public Guid ktb_defaultlandedcostid { get; set; }

		[AntiXss]
		public string ktb_defaultteamforvehicleidyominame { get; set; }

		[AntiXss]
		public string ktb_bodypaintname { get; set; }

		[AntiXss]
		public string msdyn_operatingunitid { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public bool ktb_allocationdealer { get; set; }

		[AntiXss]
		public string workflowsuspendedname { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public string ktb_userdnet { get; set; }

		[AntiXss]
		public Guid xts_shiptocityid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string xts_vendoridname { get; set; }

		[AntiXss]
		public string ktb_defaultprpotypeotherreguleridname { get; set; }

		[AntiXss]
		public string address1_name { get; set; }

		[AntiXss]
		public string address1_fax { get; set; }

		[AntiXss]
		public decimal address1_latitude { get; set; }

		[AntiXss]
		public string xts_defaultsiteidname { get; set; }

		[AntiXss]
		public string address2_shippingmethodcode { get; set; }

		[AntiXss]
		public string ktb_defaultteamforvehiclepurchaseidname { get; set; }

		[AntiXss]
		public string ktb_defaultteamforsparepartidname { get; set; }

		[AntiXss]
		public Guid xts_onbehalfteamid { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string ktb_defaultlandedcostconsumptiontaxidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_newvehiclesodefaultmatchingsiteidname { get; set; }

		[AntiXss]
		public string address2_line1 { get; set; }

		[AntiXss]
		public string address1_upszone { get; set; }

		[AntiXss]
		public Guid address2_addressid { get; set; }

		[AntiXss]
		public string ktb_defaultteaminterfacepartidyominame { get; set; }

		[AntiXss]
		public int utcoffset { get; set; }

		[AntiXss]
		public string address1_city { get; set; }

		[AntiXss]
		public string xts_dimension8idname { get; set; }

		[AntiXss]
		public int inheritancemask { get; set; }

		[AntiXss]
		public string address1_line1 { get; set; }

		[AntiXss]
		public string xts_billtoaddress4 { get; set; }

		[AntiXss]
		public string ktb_defaultteamforvehicleidname { get; set; }

		[AntiXss]
		public string xts_territoryname { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string websiteurl { get; set; }

		[AntiXss]
		public string xts_usedvehiclename { get; set; }

		[AntiXss]
		public decimal address1_longitude { get; set; }

		[AntiXss]
		public string address1_addresstypecodename { get; set; }

		[AntiXss]
		public string xts_newvehiclename { get; set; }

		[AntiXss]
		public string address1_addresstypecode { get; set; }

		[AntiXss]
		public bool ktb_incominginterfaceallowed { get; set; }

		[AntiXss]
		public Guid bsi_defaultvendorfactoring { get; set; }

		[AntiXss]
		public string xts_dimension9idname { get; set; }

		[AntiXss]
		public string xts_mainprovinceidname { get; set; }

		[AntiXss]
		public string ktb_defaultteamforvehiclepurchaseidyominame { get; set; }

		[AntiXss]
		public Guid ktb_defaultteamforvehiclepurchaseid { get; set; }

		[AntiXss]
		public Guid xts_dimension4id { get; set; }

		[AntiXss]
		public string ktb_defaultteamforserviceidyominame { get; set; }

		[AntiXss]
		public string address2_county { get; set; }

		[AntiXss]
		public Guid xts_dimension5id { get; set; }

		[AntiXss]
		public Guid ktb_defaultprpotypeotherregulerid { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public string xts_organizationtype { get; set; }

		[AntiXss]
		public string address1_shippingmethodcode { get; set; }

		[AntiXss]
		public Guid ktb_defaultwarehouseregulerpurchasepartid { get; set; }

		[AntiXss]
		public Guid ktb_defaultlandedcostdepositc2id { get; set; }

		[AntiXss]
		public string ktb_defaultlandedcostdepositc2idname { get; set; }

		[AntiXss]
		public string ktb_defaultteaminterfacepartidname { get; set; }

		[AntiXss]
		public string xts_phone { get; set; }

		[AntiXss]
		public string xts_accountname { get; set; }

		[AntiXss]
		public Guid xts_dimension10id { get; set; }

		[AntiXss]
		public bool xts_service { get; set; }

		[AntiXss]
		public Guid ktb_defaultteaminterfacepartid { get; set; }

		[AntiXss]
		public Guid ktb_defaultvendorfactoring { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

		[AntiXss]
		public string xjp_accountyype { get; set; }

		[AntiXss]
		public string address2_stateorprovince { get; set; }

		[AntiXss]
		public string xts_dimension6idname { get; set; }

		[AntiXss]
		public string ktb_dealerbranch { get; set; }

		[AntiXss]
		public string ktb_dealercode { get; set; }

		[AntiXss]
		public Guid xts_billtovillageandstreetid { get; set; }

		[AntiXss]
		public string address2_country { get; set; }

		[AntiXss]
		public string ktb_tokendnet { get; set; }

		[AntiXss]
		public string address2_line2 { get; set; }

		[AntiXss]
		public string costcenter { get; set; }

		[AntiXss]
		public Guid ktb_defaultteamforsparepartid { get; set; }

		[AntiXss]
		public string xts_mainvillageandstreetidname { get; set; }

		[AntiXss]
		public Guid xts_mainvillageandstreetid { get; set; }

		[AntiXss]
		public Guid xts_dimension2id { get; set; }

		[AntiXss]
		public string address1_line3 { get; set; }

		[AntiXss]
		public string ktb_defaultvendorfactoringname { get; set; }

		[AntiXss]
		public bool workflowsuspended { get; set; }

		[AntiXss]
		public Guid xts_dimension3id { get; set; }

		[AntiXss]
		public Guid xts_billtocityid { get; set; }

		[AntiXss]
		public string ktb_defaultteaminterfacevehicleidyominame { get; set; }

		[AntiXss]
		public string ktb_ownersuspect { get; set; }

		[AntiXss]
		public decimal creditlimit { get; set; }

		[AntiXss]
		public Guid calendarid { get; set; }

		[AntiXss]
		public string xts_mainaddress4 { get; set; }

		[AntiXss]
		public string ktb_allowimportname { get; set; }

		[AntiXss]
		public string ktb_bucompanylookupname { get; set; }

		[AntiXss]
		public string xts_dimension10idname { get; set; }

		[AntiXss]
		public int address1_utcoffset { get; set; }

		[AntiXss]
		public Guid xts_ledgerchartofaccountid { get; set; }

		[AntiXss]
		public string xts_shiptovillageandstreetidname { get; set; }

		[AntiXss]
		public Guid xts_bankid { get; set; }

		[AntiXss]
		public string ktb_defaultpaymentbankidname { get; set; }

		[AntiXss]
		public string ktb_passworddnet { get; set; }

		[AntiXss]
		public string ktb_defaultwarehousepurchasevehicleidname { get; set; }

		[AntiXss]
		public Guid ktb_defaultsiteid { get; set; }

		[AntiXss]
		public Guid xts_newvehiclesodefaultmatchingsiteid { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_dimension4idname { get; set; }

		[AntiXss]
		public Guid ktb_defaultprpotypeforvehicleid { get; set; }

		[AntiXss]
		public Guid xts_dimension6id { get; set; }

		[AntiXss]
		public string address2_city { get; set; }

		[AntiXss]
		public string xts_servicename { get; set; }

		[AntiXss]
		public string xts_taxregistrationnumber { get; set; }

		[AntiXss]
		public string xts_maincountryidname { get; set; }

		[AntiXss]
		public string xts_description2 { get; set; }

		[AntiXss]
		public decimal address2_latitude { get; set; }

		[AntiXss]
		public Guid xts_dimension8id { get; set; }

		[AntiXss]
		public Guid xts_dimension9id { get; set; }

		[AntiXss]
		public string xts_mainaddress2 { get; set; }

		[AntiXss]
		public string divisionname { get; set; }

		[AntiXss]
		public string ktb_defaultdownpaymentbankidname { get; set; }

		[AntiXss]
		public string bsi_defaultvendorfactoringname { get; set; }

		[AntiXss]
		public string xts_dimension2idname { get; set; }

		[AntiXss]
		public string ktb_accountnovehicle2 { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public string ktb_dashboardtitle { get; set; }

		[AntiXss]
		public bool xts_usedvehicle { get; set; }

		[AntiXss]
		public Guid xts_billtoprovinceid { get; set; }

		[AntiXss]
		public bool ktb_bodypaint { get; set; }

		[AntiXss]
		public string address2_postalcode { get; set; }

		[AntiXss]
		public Guid xts_defaultsiteid { get; set; }

		[AntiXss]
		public Guid businessunitid { get; set; }

		[AntiXss]
		public Guid xts_shiptoprovinceid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid xts_billtocountryid { get; set; }

		[AntiXss]
		public string address2_shippingmethodcodename { get; set; }

		[AntiXss]
		public string address2_line3 { get; set; }

		[AntiXss]
		public string description { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string address1_county { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string ktb_defaultteaminterfacevehicleidname { get; set; }

		[AntiXss]
		public string ktb_defaultlandedcostidname { get; set; }

		[AntiXss]
		public string ktb_defaultvendorpurchasevehicleidname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_dimension3idname { get; set; }

		[AntiXss]
		public string xts_mainpostalcode { get; set; }

		[AntiXss]
		public string xts_onbehalfteamidyominame { get; set; }

		[AntiXss]
		public string disabledreason { get; set; }

		[AntiXss]
		public string address2_postofficebox { get; set; }

		[AntiXss]
		public string address2_telephone1 { get; set; }

		[AntiXss]
		public string address2_telephone2 { get; set; }

		[AntiXss]
		public string address2_telephone3 { get; set; }

		[AntiXss]
		public Guid ktb_defaultteaminterfacevehicleid { get; set; }

		[AntiXss]
		public string xjp_representative { get; set; }

		[AntiXss]
		public Guid address1_addressid { get; set; }

		[AntiXss]
		public string picture { get; set; }

		[AntiXss]
		public string parentbusinessunitidname { get; set; }

		[AntiXss]
		public string ktb_defaultwarehouseregulerpurchasepartidname { get; set; }

		[AntiXss]
		public Guid ktb_defaultlandedcostconsumptiontaxid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_bucompany { get; set; }

		[AntiXss]
		public string address2_name { get; set; }

		[AntiXss]
		public string address1_country { get; set; }

		[AntiXss]
		public string name { get; set; }

		[AntiXss]
		public bool ktb_allowimport { get; set; }

		[AntiXss]
		public string xts_accountnumber { get; set; }

		[AntiXss]
		public string xts_dimension1idname { get; set; }

		[AntiXss]
		public Guid xts_maincountryid { get; set; }

		[AntiXss]
		public Guid ktb_defaultdownpaymentbankid { get; set; }

		[AntiXss]
		public Guid xts_shiptocountryid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_accountnoservice2 { get; set; }

		[AntiXss]
		public Guid xts_defaultsalesordersiteid { get; set; }

		[AntiXss]
		public Guid xts_defaultworkordersiteid { get; set; }

		[AntiXss]
		public Guid xts_defaultpurchasesiteid { get; set; }

		[AntiXss]
		public string xts_defaultsalesordersiteidname { get; set; }

		[AntiXss]
		public string xts_defaultworkordersiteidname { get; set; }

		[AntiXss]
		public string xts_defaultpurchasesiteidname { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
