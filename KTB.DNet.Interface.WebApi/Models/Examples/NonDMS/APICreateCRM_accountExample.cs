#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_account class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 17:24:46
 ===========================================================================
*/
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateCRM_accountExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				accountnumber = "accountnumber value",
				address1_line1 = "address1_line1 value",
				address1_line2 = "address1_line2 value",
				address1_line3 = "address1_line3 value",
				address1_postalcode = "address1_postalcode value",
				createdby = "createdby value",
				createdon = "createdon value",
				description = "description value",
				donotbulkemail = "donotbulkemail value",
				donotemail = "donotemail value",
				donotfax = "donotfax value",
				donotphone = "donotphone value",
				donotpostalmail = "donotpostalmail value",
				donotsendmm = "donotsendmm value",
				emailaddress1 = "emailaddress1 value",
				emailaddress2 = "emailaddress2 value",
				emailaddress3 = "emailaddress3 value",
				fax = "fax value",
				ktb_autonumber = "ktb_autonumber value",
				ktb_customercodesap = "ktb_customercodesap value",
				ktb_customerfleetcode = "ktb_customerfleetcode value",
				ktb_customerrequestid = "ktb_customerrequestid value",
				ktb_dealercode = "ktb_dealercode value",
				ktb_groupkategori = "ktb_groupkategori value",
				ktb_interfaceexceptionmessage = "ktb_interfaceexceptionmessage value",
				ktb_interfacehandling = "ktb_interfacehandling value",
				ktb_interfacestatus = "ktb_interfacestatus value",
				ktb_interfacetodnet = "ktb_interfacetodnet value",
				ktb_isinsurance = "ktb_isinsurance value",
				ktb_leveldata = "ktb_leveldata value",
				ktb_ocrid = "ktb_ocrid value",
				ktb_ocrsimid = "ktb_ocrsimid value",
				ktb_overdueonhold = "ktb_overdueonhold value",
				ktb_parentbusinessunitid = "ktb_parentbusinessunitid value",
				ktb_pendidikan = "ktb_pendidikan value",
				ktb_pobox = "ktb_pobox value",
				ktb_prearea = "ktb_prearea value",
				ktb_ribbonocr = "ktb_ribbonocr value",
				ktb_tipepelanggan = "ktb_tipepelanggan value",
				ktb_tipeperusahaan = "ktb_tipeperusahaan value",
				lastusedincampaign = "lastusedincampaign value",
				modifiedby = "modifiedby value",
				modifiedon = "modifiedon value",
				name = "name value",
				originatingleadid = "originatingleadid value",
				ownerid = "ownerid value",
				parentaccountid = "parentaccountid value",
				preferredappointmentdaycode = "preferredappointmentdaycode value",
				preferredappointmenttimecode = "preferredappointmenttimecode value",
				preferredcontactmethodcode = "preferredcontactmethodcode value",
				preferredequipmentid = "preferredequipmentid value",
				preferredsystemuserid = "preferredsystemuserid value",
				primarycontactid = "primarycontactid value",
				revenue = "revenue value",
				statecode = "statecode value",
				telephone1 = "telephone1 value",
				telephone2 = "telephone2 value",
				telephone3 = "telephone3 value",
				transactioncurrencyid = "transactioncurrencyid value",
				websiteurl = "websiteurl value",
				xjp_companycode = "xjp_companycode value",
				xjp_ownershipposition = "xjp_ownershipposition value",
				xjp_prospectdateforincreasevehicle = "xjp_prospectdateforincreasevehicle value",
				xts_address4 = "xts_address4 value",
				xts_aliasname = "xts_aliasname value",
				xts_anniversarydate = "xts_anniversarydate value",
				xts_birthdate = "xts_birthdate value",
				xts_businessunitid = "xts_businessunitid value",
				xts_categoryid = "xts_categoryid value",
				xts_checkcreditlimit = "xts_checkcreditlimit value",
				xts_cityid = "xts_cityid value",
				xts_companyname = "xts_companyname value",
				xts_companysizeid = "xts_companysizeid value",
				xts_countryid = "xts_countryid value",
				xts_creditlimitamount = "xts_creditlimitamount value",
				xts_creditlimitbalance = "xts_creditlimitbalance value",
				xts_customerclassid = "xts_customerclassid value",
				xts_customerclasstype = "xts_customerclasstype value",
				xts_customerid = "xts_customerid value",
				xts_customerrankid = "xts_customerrankid value",
				xts_customertype = "xts_customertype value",
				xts_email4 = "xts_email4 value",
				xts_firstname = "xts_firstname value",
				xts_friday = "xts_friday value",
				xts_gender = "xts_gender value",
				xts_graceperiod = "xts_graceperiod value",
				xts_hobbyid = "xts_hobbyid value",
				xts_homevisit = "xts_homevisit value",
				xts_identificationnumber = "xts_identificationnumber value",
				xts_identificationtype = "xts_identificationtype value",
				xts_ignoredownpayment = "xts_ignoredownpayment value",
				xts_industryid = "xts_industryid value",
				xts_integrationnumber = "xts_integrationnumber value",
				xts_internalnumber = "xts_internalnumber value",
				xts_jobtitleid = "xts_jobtitleid value",
				xts_lastname = "xts_lastname value",
				xts_maritalstatus = "xts_maritalstatus value",
				xts_methodofpaymentid = "xts_methodofpaymentid value",
				xts_monday = "xts_monday value",
				xts_numberofvehicle = "xts_numberofvehicle value",
				xts_originatingcontactid = "xts_originatingcontactid value",
				xts_originatingcustomerpublic = "xts_originatingcustomerpublic value",
				xts_otherhobby = "xts_otherhobby value",
				xts_otherphone = "xts_otherphone value",
				xts_overduebalance = "xts_overduebalance value",
				xts_ownershipid = "xts_ownershipid value",
				xts_parentcustomernumber = "xts_parentcustomernumber value",
				xts_phonepriority = "xts_phonepriority value",
				xts_preferredserviceid = "xts_preferredserviceid value",
				xts_provinceid = "xts_provinceid value",
				xts_religionid = "xts_religionid value",
				xts_residentialtypeid = "xts_residentialtypeid value",
				xts_salespersonid = "xts_salespersonid value",
				xts_salutation = "xts_salutation value",
				xts_saturday = "xts_saturday value",
				xts_sharepersonalinformation = "xts_sharepersonalinformation value",
				xts_shipmenttype = "xts_shipmenttype value",
				xts_shortname = "xts_shortname value",
				xts_sourcecampaignid = "xts_sourcecampaignid value",
				xts_statementdate = "xts_statementdate value",
				xts_sunday = "xts_sunday value",
				xts_taxcode = "xts_taxcode value",
				xts_taxregistrationname = "xts_taxregistrationname value",
				xts_taxregistrationnumber = "xts_taxregistrationnumber value",
				xts_taxzoneid = "xts_taxzoneid value",
				xts_termofpaymentid = "xts_termofpaymentid value",
				xts_thursday = "xts_thursday value",
				xts_tuesday = "xts_tuesday value",
				xts_villageandstreetid = "xts_villageandstreetid value",
				xts_wednesday = "xts_wednesday value",
				UpdatedBy = "UpdatedBy value"
            };

            return obj;
        }
    }
}