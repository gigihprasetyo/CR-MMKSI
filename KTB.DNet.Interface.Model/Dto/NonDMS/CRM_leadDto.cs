#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_lead class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 14:17:25
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_leadDto : DtoBase
    {
        public Guid leadid { get; set; }

        public string address1_line1 { get; set; }

		public string address1_line2 { get; set; }

		public string address1_line3 { get; set; }

		public string address1_postalcode { get; set; }

		public decimal? budgetamount { get; set; }

		public Guid campaignid { get; set; }

		public string companyname { get; set; }

		public Guid customerid { get; set; }

		public string description { get; set; }

		public bool? donotbulkemail { get; set; }

		public bool? donotemail { get; set; }

		public bool? donotfax { get; set; }

		public bool? donotphone { get; set; }

		public bool? donotpostalmail { get; set; }

		public bool? donotsendmm { get; set; }

		public string emailaddress1 { get; set; }

		public decimal? estimatedamount { get; set; }

		public DateTime? estimatedclosedate { get; set; }

		public string fax { get; set; }

		public string firstname { get; set; }

		public string fullname { get; set; }

		public string industrycode { get; set; }

		public string initialcommunication { get; set; }

		public int? ktb_dnetid { get; set; }

		public int? ktb_dnetspkcustomerid { get; set; }

		public string ktb_informationsource { get; set; }

		public string ktb_interfaceexceptionmessage { get; set; }

		public string ktb_statusinterfacednet { get; set; }

		public Guid ktb_vehiclecategoryid { get; set; }

		public string ktb_vehiclecolorname { get; set; }

		public string ktb_vehicledescription { get; set; }

		public Guid ktb_vehiclemodelid { get; set; }

		public string ktb_webid { get; set; }

		public string lastname { get; set; }

		public DateTime? lastusedincampaign { get; set; }

		public string leadqualitycode { get; set; }

		public string leadsourcecode { get; set; }

		public string mobilephone { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public string preferredcontactmethodcode { get; set; }

		public decimal? revenue { get; set; }

		public int? statecode { get; set; }

		public int? statuscode { get; set; }

		public string subject { get; set; }

		public string telephone1 { get; set; }

		public string telephone2 { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string websiteurl { get; set; }

		public int? xto_quantity { get; set; }

		public string xto_reasonforvisit { get; set; }

		public string xto_registrationcode { get; set; }

		public string xto_typeofvisit { get; set; }

		public string xts_address4 { get; set; }

		public string xts_agesegment { get; set; }

		public decimal? xts_baseprice { get; set; }

		public DateTime? xts_birthdate { get; set; }

		public Guid xts_businessunitid { get; set; }

		public Guid xts_cityid { get; set; }

		public Guid xts_countryid { get; set; }

		public Guid xts_customerclassid { get; set; }

		public string xts_customernumber { get; set; }

		public DateTime? xts_disorqualifieddate { get; set; }

		public DateTime? xts_estimatedpurchasedate { get; set; }

		public string xts_existingmanufacturer { get; set; }

		public string xts_existingvehiclemodel { get; set; }

		public string xts_gender { get; set; }

		public bool? xts_homevisit { get; set; }

		public string xts_identificationnumber { get; set; }

		public string xts_identificationtype { get; set; }

		public Guid xts_industryid { get; set; }

		public Guid xts_jobtitleid { get; set; }

		public DateTime? xts_leaddate { get; set; }

		public string xts_leadpurpose { get; set; }

		public Guid xts_leadsourceid { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public Guid xts_originatingcampaignresponseid { get; set; }

		public string xts_preferredvehiclemodel { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public Guid xts_productid { get; set; }

		public Guid xts_provinceid { get; set; }

		public Guid xts_religionid { get; set; }

		public Guid xts_residentialtypeid { get; set; }

		public string xts_salesobjective { get; set; }

		public Guid xts_salespersonid { get; set; }

		public string xts_taxregistrationnumber { get; set; }

		public Guid xts_vehiclebrandid { get; set; }

		public Guid xts_vehiclemodelid { get; set; }

		public Guid xts_vendorclassid { get; set; }

		public Guid xts_vendorid { get; set; }

		public string ktb_externalcode { get; set; }

	}
}
