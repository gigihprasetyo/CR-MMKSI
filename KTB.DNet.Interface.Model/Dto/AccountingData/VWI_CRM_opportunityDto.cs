#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_opportunityDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:49
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_opportunityDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xjp_benefitforrepairabledamagename { get; set; }

		public string xts_insurancegrade { get; set; }

		public string xts_vehicleincidentalcategoryname { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public int statuscode { get; set; }

		public bool captureproposalfeedback { get; set; }

		public string ktb_dnetid { get; set; }

		public bool developproposal { get; set; }

		public string contactidname { get; set; }

		public string xts_agencychannelidname { get; set; }

		public string description { get; set; }

		public string salesstagename { get; set; }

		public bool evaluatefit { get; set; }

		public string parentaccountidname { get; set; }

		public string xts_periodofinsurancelabel { get; set; }

		public string xts_vehicledrivinglicensetypename { get; set; }

		public string xts_chasisnumber { get; set; }

		public string xts_lastcontractidname { get; set; }

		public string xts_address3 { get; set; }

		public decimal estimatedvalue_base { get; set; }

		public Guid xts_configurationid { get; set; }

		public string xts_vehiclemodelidname { get; set; }

		public bool xjp_repurchasenewvehicle { get; set; }

		public decimal xts_propertydamageliabilitycompensationamount_base { get; set; }

		public DateTime scheduleproposalmeeting { get; set; }

		public decimal actualvalue { get; set; }

		public string xts_propertydamageliabilityunlimitedcoveragename { get; set; }

		public Guid contactid { get; set; }

		public int importsequencenumber { get; set; }

		public string statecodename { get; set; }

		public string xts_potentialcustomernumber { get; set; }

		public string xts_countryidname { get; set; }

		public DateTime lastonholdtime { get; set; }

		public decimal totaltax { get; set; }

		public string customeridname { get; set; }

		public Guid opportunityid { get; set; }

		public decimal xjp_firstdeductible { get; set; }

		public string purchasetimeframe { get; set; }

		public string xts_vehicledrivinglicensetypeidname { get; set; }

		public DateTime xts_birthday { get; set; }

		public string ktb_statusinterfacename { get; set; }

		public Guid xts_vehiclebrandid { get; set; }

		public string xts_villageandstreetidname { get; set; }

		public decimal xjp_passengerscoverageamount_base { get; set; }

		public string accountidyominame { get; set; }

		public decimal xts_defaultprice_base { get; set; }

		public Guid parentaccountid { get; set; }

		public string slainvokedidname { get; set; }

		public Guid ktb_vehiclemodelid { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string completeinternalreviewname { get; set; }

		public string salesstagecode { get; set; }

		public string prioritycodename { get; set; }

		public Guid xts_categoryid { get; set; }

		public string xts_driverlimitationcategoryname { get; set; }

		public string slaname { get; set; }

		public Guid xts_vehicleincidentcategoryid { get; set; }

		public Guid xts_styleid { get; set; }

		public string xts_insurednameidyominame { get; set; }

		public string xts_styleidname { get; set; }

		public string xts_vehicleidentificationidname { get; set; }

		public string skippricecalculationname { get; set; }

		public Int64 versionnumber { get; set; }

		public string initialcommunication { get; set; }

		public bool xjp_benefitforrepairabledamage { get; set; }

		public bool xjp_temporaryloanercar { get; set; }

		public Guid xts_exteriorcolorid { get; set; }

		public string customeridtype { get; set; }

		public string xjp_platenumbersegment3 { get; set; }

		public string xjp_bodilyinjurycompensationforonlypassengersname { get; set; }

		public string xts_chasismodel { get; set; }

		public string createdbyyominame { get; set; }

		public string identifypursuitteamname { get; set; }

		public Guid xts_insurednameid { get; set; }

		public string initialcommunicationname { get; set; }

		public string accountidname { get; set; }

		public string originatingleadidname { get; set; }

		public string needname { get; set; }

		public string xts_manufactureridname { get; set; }

		public DateTime xjp_vehicleinspectionexpirationdate { get; set; }

		public string xjp_legalfeename { get; set; }

		public bool xts_whileintheinsuredvehicleonlycoverage { get; set; }

		public Guid xts_cityid { get; set; }

		public decimal totallineitemdiscountamount_base { get; set; }

		public Guid xts_relatedperiodeofinsuranceid { get; set; }

		public decimal xts_propertydamageliabilitycompensationamount { get; set; }

		public string currentsituation { get; set; }

		public string resolvefeedbackname { get; set; }

		public bool identifycustomercontacts { get; set; }

		public string ktb_eventdata { get; set; }

		public decimal totaltax_base { get; set; }

		public string ktb_interfaceexceptionmessage { get; set; }

		public string xts_gender { get; set; }

		public bool presentfinalproposal { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_categoryidname { get; set; }

		public decimal discountamount { get; set; }

		public Guid xts_personinchargeid { get; set; }

		public Guid ktb_spkid { get; set; }

		public string filedebriefname { get; set; }

		public string ktb_tempdocument { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string pricingerrorcodename { get; set; }

		public decimal totaldiscountamount { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public string xts_sourceinformationidname { get; set; }

		public Guid xts_insurancepaymentmethodid { get; set; }

		public Guid xts_vehicleidentificationid { get; set; }

		public string xts_personinchargeidname { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xts_salesobjectivename { get; set; }

		public decimal freightamount_base { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public string owneridname { get; set; }

		public string xts_vehiclename { get; set; }

		public string customeridyominame { get; set; }

		public string ktb_vehiclemodelidname { get; set; }

		public string xts_vehicleincidentcategoryidname { get; set; }

		public string xts_potentialcontactidname { get; set; }

		public string originatingleadidyominame { get; set; }

		public string campaignidname { get; set; }

		public string decisionmakername { get; set; }

		public string parentcontactidname { get; set; }

		public string xts_driverlimitationcategory { get; set; }

		public string opportunityratingcodename { get; set; }

		public string traversedpath { get; set; }

		public decimal exchangerate { get; set; }

		public bool ktb_isinterfaced { get; set; }

		public string xts_statusreasonidname { get; set; }

		public DateTime xts_relatedexpirationdate { get; set; }

		public string xts_interiorcoloridname { get; set; }

		public string xts_vehiclebrandidname { get; set; }

		public Guid pricelevelid { get; set; }

		public Guid xts_agencychannelid { get; set; }

		public string xts_policynumber { get; set; }

		public bool presentproposal { get; set; }

		public bool sendthankyounote { get; set; }

		public bool confirminterest { get; set; }

		public decimal xjp_bodilyinjurycompensation_base { get; set; }

		public string purchasetimeframename { get; set; }

		public string skippricecalculation { get; set; }

		public string xts_vehicledrivinglicensetypevalue { get; set; }

		public string xjp_platenumbersegment1idname { get; set; }

		public string pursuitdecisionname { get; set; }

		public decimal xts_bodilyinjuryliabilitycompensationamount { get; set; }

		public string xts_whileintheinsuredvehicleonlycoveragename { get; set; }

		public bool isrevenuesystemcalculated { get; set; }

		public bool pursuitdecision { get; set; }

		public int xto_quantity { get; set; }

		public string xjp_platenumbersegment4 { get; set; }

		public string identifycustomercontactsname { get; set; }

		public DateTime createdon { get; set; }

		public Guid slaid { get; set; }

		public Guid originatingleadid { get; set; }

		public string xjp_firstregistration { get; set; }

		public string xts_proposeinsurancecompanyname { get; set; }

		public string xts_relatedperiodeofinsuranceidname { get; set; }

		public Guid xts_proposedinsurancecompanyid { get; set; }

		public DateTime schedulefollowup_qualify { get; set; }

		public string presentfinalproposalname { get; set; }

		public decimal totalamount { get; set; }

		public string xts_periodofinsurancevalue { get; set; }

		public bool xjp_disastertotallossspecialagreement { get; set; }

		public string xts_periodofinsurance { get; set; }

		public string quotecomments { get; set; }

		public Guid xts_vehiclemodelid { get; set; }

		public string budgettypename { get; set; }

		public string xts_insuredname { get; set; }

		public string xts_locking { get; set; }

		public DateTime schedulefollowup_prospect { get; set; }

		public string budgetstatus { get; set; }

		public bool xjp_bodilyinjurycompensationforonlypassengers { get; set; }

		public string createdbyname { get; set; }

		public string xts_productidname { get; set; }

		public int statecode { get; set; }

		public string xts_periodofinsurancename { get; set; }

		public int closeprobability { get; set; }

		public decimal totalamount_base { get; set; }

		public string ktb_vehicledescription { get; set; }

		public bool xjp_gradeprotection { get; set; }

		public string opportunityratingcode { get; set; }

		public string purchaseprocessname { get; set; }

		public string xts_vehicledrivinglicensetypelabel { get; set; }

		public string participatesinworkflowname { get; set; }

		public string xts_insurancepaymentmethodidname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public Guid xts_productid { get; set; }

		public string proposedsolution { get; set; }

		public bool resolvefeedback { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string xts_phonenumber { get; set; }

		public string ktb_statusinterface { get; set; }

		public string developproposalname { get; set; }

		public string statuscodename { get; set; }

		public Guid owninguser { get; set; }

		public bool decisionmaker { get; set; }

		public string ktb_progressstagename { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public string modifiedbyname { get; set; }

		public string xjp_platenumbersegment2 { get; set; }

		public string owneridtype { get; set; }

		public Guid slainvokedid { get; set; }

		public string ktb_spkidname { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string ktb_occuationname { get; set; }

		public string completefinalproposalname { get; set; }

		public decimal xjp_bodilyinjurycompensation { get; set; }

		public bool participatesinworkflow { get; set; }

		public string xts_exteriorcoloridname { get; set; }

		public DateTime estimatedclosedate { get; set; }

		public string prioritycode { get; set; }

		public string xts_salesobjective { get; set; }

		public bool xts_propertydamageliabilityunlimitedcoverage { get; set; }

		public Guid xts_insurancecompanyid { get; set; }

		public string xts_potentialcontactidyominame { get; set; }

		public decimal xto_discount { get; set; }

		public string purchaseprocess { get; set; }

		public string xts_insurancecarusagepurposename { get; set; }

		public string msdyn_forecastcategory { get; set; }

		public string pricelevelidname { get; set; }

		public string xts_vehicledrivinglicensetype { get; set; }

		public decimal estimatedvalue { get; set; }

		public string xjp_temporaryloanercarname { get; set; }

		public int onholdtime { get; set; }

		public string xts_apppointmentidname { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string xts_provinceidname { get; set; }

		public decimal xts_insuranceannualpremium_base { get; set; }

		public string emailaddress { get; set; }

		public string need { get; set; }

		public string isprivatename { get; set; }

		public string contactidyominame { get; set; }

		public Guid ownerid { get; set; }

		public string salesstagecodename { get; set; }

		public Guid campaignid { get; set; }

		public Guid parentcontactid { get; set; }

		public decimal totallineitemdiscountamount { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal xjp_firstdeductible_base { get; set; }

		public Guid owningteam { get; set; }

		public string xts_vehicleincidentalcategory { get; set; }

		public string xts_address4 { get; set; }

		public string xjp_personalliabilityname { get; set; }

		public Guid xts_countryid { get; set; }

		public int xts_agelimiteddriver { get; set; }

		public Guid xts_apppointmentid { get; set; }

		public decimal xts_vehiclevalue_base { get; set; }

		public bool isprivate { get; set; }

		public string xts_address1 { get; set; }

		public string confirminterestname { get; set; }

		public decimal budgetamount_base { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xts_insurancecompanyname { get; set; }

		public string xts_gendername { get; set; }

		public string xts_cityidname { get; set; }

		public string owneridyominame { get; set; }

		public string ktb_progressstage { get; set; }

		public Guid xts_statusreasonid { get; set; }

		public bool xjp_personalliability { get; set; }

		public bool completefinalproposal { get; set; }

		public string customerneed { get; set; }

		public string ktb_vehiclecategoryidname { get; set; }

		public string parentcontactidyominame { get; set; }

		public string xjp_repurchasenewvehiclename { get; set; }

		public string ktb_vehiclecolorname { get; set; }

		public Guid xts_salespersonid { get; set; }

		public string xts_insurancecarusagepurpose { get; set; }

		public string isrevenuesystemcalculatedname { get; set; }

		public string identifycompetitorsname { get; set; }

		public string captureproposalfeedbackname { get; set; }

		public Guid customerid { get; set; }

		public string xjp_disastertotallossspecialagreementname { get; set; }

		public Guid xts_villageandstreetid { get; set; }

		public Guid stageid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string ktb_occuation { get; set; }

		public Guid modifiedby { get; set; }

		public bool identifycompetitors { get; set; }

		public int teamsfollowed { get; set; }

		public decimal totallineitemamount { get; set; }

		public decimal xts_defaultprice { get; set; }

		public string xjp_gradeprotectionname { get; set; }

		public decimal xts_baseprice_base { get; set; }

		public string xts_postalcode { get; set; }

		public string evaluatefitname { get; set; }

		public string timespentbymeonemailandmeetings { get; set; }

		public decimal totaldiscountamount_base { get; set; }

		public string salesstage { get; set; }

		public string sendthankyounotename { get; set; }

		public string xts_configurationidname { get; set; }

		public string customerpainpoints { get; set; }

		public DateTime finaldecisiondate { get; set; }

		public string xts_insurancecompanyidname { get; set; }

		public decimal xts_baseprice { get; set; }

		public string parentaccountidyominame { get; set; }

		public string timeline { get; set; }

		public decimal xts_insuranceannualpremium { get; set; }

		public string stepname { get; set; }

		public bool xts_bodilyinjuryliabilityunlimitedcoverage { get; set; }

		public decimal xto_discount_base { get; set; }

		public string name { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public decimal totalamountlessfreight { get; set; }

		public string xts_salespersonidname { get; set; }

		public string xts_address2 { get; set; }

		public string timelinename { get; set; }

		public Guid ktb_vehiclecategoryid { get; set; }

		public decimal discountamount_base { get; set; }

		public Guid stepid { get; set; }

		public Guid xts_provinceid { get; set; }

		public decimal xjp_seconddeductible_base { get; set; }

		public decimal totalamountlessfreight_base { get; set; }

		public decimal discountpercentage { get; set; }

		public decimal xts_vehiclevalue { get; set; }

		public DateTime xts_leaddate { get; set; }

		public decimal xjp_seconddeductible { get; set; }

		public string pricingerrorcode { get; set; }

		public Guid xts_sourceinformationid { get; set; }

		public DateTime actualclosedate { get; set; }

		public decimal totallineitemamount_base { get; set; }

		public Guid processid { get; set; }

		public DateTime modifiedon { get; set; }

		public Guid accountid { get; set; }

		public string xts_businessunitidname { get; set; }

		public Guid xts_vehicledrivinglicensetypeid { get; set; }

		public string qualificationcomments { get; set; }

		public decimal xjp_passengerscoverageamount { get; set; }

		public bool identifypursuitteam { get; set; }

		public Guid xts_interiorcolorid { get; set; }

		public string xts_proposedinsurancecompanyidname { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string presentproposalname { get; set; }

		public string ktb_lostreason { get; set; }

		public decimal actualvalue_base { get; set; }

		public Guid xts_lastcontractid { get; set; }

		public bool filedebrief { get; set; }

		public Guid xts_potentialcontactid { get; set; }

		public Guid xjp_platenumbersegment1id { get; set; }

		public decimal budgetamount { get; set; }

		public string ktb_tempdocumentname { get; set; }

		public Guid createdby { get; set; }

		public decimal xts_bodilyinjuryliabilitycompensationamount_base { get; set; }

		public string msdyn_forecastcategoryname { get; set; }

		public decimal freightamount { get; set; }

		public bool completeinternalreview { get; set; }

		public string xts_bodilyinjuryliabilityunlimitedcoveragename { get; set; }

		public string xts_insurednameidname { get; set; }

		public bool xjp_legalfee { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
