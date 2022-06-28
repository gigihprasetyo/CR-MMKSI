#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_opportunityParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_opportunityParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xjp_benefitforrepairabledamagename { get; set; }

		[AntiXss]
		public string xts_insurancegrade { get; set; }

		[AntiXss]
		public string xts_vehicleincidentalcategoryname { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public bool captureproposalfeedback { get; set; }

		[AntiXss]
		public string ktb_dnetid { get; set; }

		[AntiXss]
		public bool developproposal { get; set; }

		[AntiXss]
		public string contactidname { get; set; }

		[AntiXss]
		public string xts_agencychannelidname { get; set; }

		[AntiXss]
		public string description { get; set; }

		[AntiXss]
		public string salesstagename { get; set; }

		[AntiXss]
		public bool evaluatefit { get; set; }

		[AntiXss]
		public string parentaccountidname { get; set; }

		[AntiXss]
		public string xts_periodofinsurancelabel { get; set; }

		[AntiXss]
		public string xts_vehicledrivinglicensetypename { get; set; }

		[AntiXss]
		public string xts_chasisnumber { get; set; }

		[AntiXss]
		public string xts_lastcontractidname { get; set; }

		[AntiXss]
		public string xts_address3 { get; set; }

		[AntiXss]
		public decimal estimatedvalue_base { get; set; }

		[AntiXss]
		public Guid xts_configurationid { get; set; }

		[AntiXss]
		public string xts_vehiclemodelidname { get; set; }

		[AntiXss]
		public bool xjp_repurchasenewvehicle { get; set; }

		[AntiXss]
		public decimal xts_propertydamageliabilitycompensationamount_base { get; set; }

		[AntiXss]
		public DateTime scheduleproposalmeeting { get; set; }

		[AntiXss]
		public decimal actualvalue { get; set; }

		[AntiXss]
		public string xts_propertydamageliabilityunlimitedcoveragename { get; set; }

		[AntiXss]
		public Guid contactid { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_potentialcustomernumber { get; set; }

		[AntiXss]
		public string xts_countryidname { get; set; }

		[AntiXss]
		public DateTime lastonholdtime { get; set; }

		[AntiXss]
		public decimal totaltax { get; set; }

		[AntiXss]
		public string customeridname { get; set; }

		[AntiXss]
		public Guid opportunityid { get; set; }

		[AntiXss]
		public decimal xjp_firstdeductible { get; set; }

		[AntiXss]
		public string purchasetimeframe { get; set; }

		[AntiXss]
		public string xts_vehicledrivinglicensetypeidname { get; set; }

		[AntiXss]
		public DateTime xts_birthday { get; set; }

		[AntiXss]
		public string ktb_statusinterfacename { get; set; }

		[AntiXss]
		public Guid xts_vehiclebrandid { get; set; }

		[AntiXss]
		public string xts_villageandstreetidname { get; set; }

		[AntiXss]
		public decimal xjp_passengerscoverageamount_base { get; set; }

		[AntiXss]
		public string accountidyominame { get; set; }

		[AntiXss]
		public decimal xts_defaultprice_base { get; set; }

		[AntiXss]
		public Guid parentaccountid { get; set; }

		[AntiXss]
		public string slainvokedidname { get; set; }

		[AntiXss]
		public Guid ktb_vehiclemodelid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string completeinternalreviewname { get; set; }

		[AntiXss]
		public string salesstagecode { get; set; }

		[AntiXss]
		public string prioritycodename { get; set; }

		[AntiXss]
		public Guid xts_categoryid { get; set; }

		[AntiXss]
		public string xts_driverlimitationcategoryname { get; set; }

		[AntiXss]
		public string slaname { get; set; }

		[AntiXss]
		public Guid xts_vehicleincidentcategoryid { get; set; }

		[AntiXss]
		public Guid xts_styleid { get; set; }

		[AntiXss]
		public string xts_insurednameidyominame { get; set; }

		[AntiXss]
		public string xts_styleidname { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationidname { get; set; }

		[AntiXss]
		public string skippricecalculationname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string initialcommunication { get; set; }

		[AntiXss]
		public bool xjp_benefitforrepairabledamage { get; set; }

		[AntiXss]
		public bool xjp_temporaryloanercar { get; set; }

		[AntiXss]
		public Guid xts_exteriorcolorid { get; set; }

		[AntiXss]
		public string customeridtype { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment3 { get; set; }

		[AntiXss]
		public string xjp_bodilyinjurycompensationforonlypassengersname { get; set; }

		[AntiXss]
		public string xts_chasismodel { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string identifypursuitteamname { get; set; }

		[AntiXss]
		public Guid xts_insurednameid { get; set; }

		[AntiXss]
		public string initialcommunicationname { get; set; }

		[AntiXss]
		public string accountidname { get; set; }

		[AntiXss]
		public string originatingleadidname { get; set; }

		[AntiXss]
		public string needname { get; set; }

		[AntiXss]
		public string xts_manufactureridname { get; set; }

		[AntiXss]
		public DateTime xjp_vehicleinspectionexpirationdate { get; set; }

		[AntiXss]
		public string xjp_legalfeename { get; set; }

		[AntiXss]
		public bool xts_whileintheinsuredvehicleonlycoverage { get; set; }

		[AntiXss]
		public Guid xts_cityid { get; set; }

		[AntiXss]
		public decimal totallineitemdiscountamount_base { get; set; }

		[AntiXss]
		public Guid xts_relatedperiodeofinsuranceid { get; set; }

		[AntiXss]
		public decimal xts_propertydamageliabilitycompensationamount { get; set; }

		[AntiXss]
		public string currentsituation { get; set; }

		[AntiXss]
		public string resolvefeedbackname { get; set; }

		[AntiXss]
		public bool identifycustomercontacts { get; set; }

		[AntiXss]
		public string ktb_eventdata { get; set; }

		[AntiXss]
		public decimal totaltax_base { get; set; }

		[AntiXss]
		public string ktb_interfaceexceptionmessage { get; set; }

		[AntiXss]
		public string xts_gender { get; set; }

		[AntiXss]
		public bool presentfinalproposal { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_categoryidname { get; set; }

		[AntiXss]
		public decimal discountamount { get; set; }

		[AntiXss]
		public Guid xts_personinchargeid { get; set; }

		[AntiXss]
		public Guid ktb_spkid { get; set; }

		[AntiXss]
		public string filedebriefname { get; set; }

		[AntiXss]
		public string ktb_tempdocument { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string pricingerrorcodename { get; set; }

		[AntiXss]
		public decimal totaldiscountamount { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public string xts_sourceinformationidname { get; set; }

		[AntiXss]
		public Guid xts_insurancepaymentmethodid { get; set; }

		[AntiXss]
		public Guid xts_vehicleidentificationid { get; set; }

		[AntiXss]
		public string xts_personinchargeidname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xts_salesobjectivename { get; set; }

		[AntiXss]
		public decimal freightamount_base { get; set; }

		[AntiXss]
		public Guid xts_manufacturerid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string xts_vehiclename { get; set; }

		[AntiXss]
		public string customeridyominame { get; set; }

		[AntiXss]
		public string ktb_vehiclemodelidname { get; set; }

		[AntiXss]
		public string xts_vehicleincidentcategoryidname { get; set; }

		[AntiXss]
		public string xts_potentialcontactidname { get; set; }

		[AntiXss]
		public string originatingleadidyominame { get; set; }

		[AntiXss]
		public string campaignidname { get; set; }

		[AntiXss]
		public string decisionmakername { get; set; }

		[AntiXss]
		public string parentcontactidname { get; set; }

		[AntiXss]
		public string xts_driverlimitationcategory { get; set; }

		[AntiXss]
		public string opportunityratingcodename { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public string xts_statusreasonidname { get; set; }

		[AntiXss]
		public DateTime xts_relatedexpirationdate { get; set; }

		[AntiXss]
		public string xts_interiorcoloridname { get; set; }

		[AntiXss]
		public string xts_vehiclebrandidname { get; set; }

		[AntiXss]
		public Guid pricelevelid { get; set; }

		[AntiXss]
		public Guid xts_agencychannelid { get; set; }

		[AntiXss]
		public string xts_policynumber { get; set; }

		[AntiXss]
		public bool presentproposal { get; set; }

		[AntiXss]
		public bool sendthankyounote { get; set; }

		[AntiXss]
		public bool confirminterest { get; set; }

		[AntiXss]
		public decimal xjp_bodilyinjurycompensation_base { get; set; }

		[AntiXss]
		public string purchasetimeframename { get; set; }

		[AntiXss]
		public string skippricecalculation { get; set; }

		[AntiXss]
		public string xts_vehicledrivinglicensetypevalue { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment1idname { get; set; }

		[AntiXss]
		public string pursuitdecisionname { get; set; }

		[AntiXss]
		public decimal xts_bodilyinjuryliabilitycompensationamount { get; set; }

		[AntiXss]
		public string xts_whileintheinsuredvehicleonlycoveragename { get; set; }

		[AntiXss]
		public bool isrevenuesystemcalculated { get; set; }

		[AntiXss]
		public bool pursuitdecision { get; set; }

		[AntiXss]
		public int xto_quantity { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment4 { get; set; }

		[AntiXss]
		public string identifycustomercontactsname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid slaid { get; set; }

		[AntiXss]
		public Guid originatingleadid { get; set; }

		[AntiXss]
		public string xjp_firstregistration { get; set; }

		[AntiXss]
		public string xts_proposeinsurancecompanyname { get; set; }

		[AntiXss]
		public string xts_relatedperiodeofinsuranceidname { get; set; }

		[AntiXss]
		public Guid xts_proposedinsurancecompanyid { get; set; }

		[AntiXss]
		public DateTime schedulefollowup_qualify { get; set; }

		[AntiXss]
		public string presentfinalproposalname { get; set; }

		[AntiXss]
		public decimal totalamount { get; set; }

		[AntiXss]
		public string xts_periodofinsurancevalue { get; set; }

		[AntiXss]
		public bool xjp_disastertotallossspecialagreement { get; set; }

		[AntiXss]
		public string xts_periodofinsurance { get; set; }

		[AntiXss]
		public string quotecomments { get; set; }

		[AntiXss]
		public Guid xts_vehiclemodelid { get; set; }

		[AntiXss]
		public string budgettypename { get; set; }

		[AntiXss]
		public string xts_insuredname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public DateTime schedulefollowup_prospect { get; set; }

		[AntiXss]
		public string budgetstatus { get; set; }

		[AntiXss]
		public bool xjp_bodilyinjurycompensationforonlypassengers { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_periodofinsurancename { get; set; }

		[AntiXss]
		public int closeprobability { get; set; }

		[AntiXss]
		public decimal totalamount_base { get; set; }

		[AntiXss]
		public string ktb_vehicledescription { get; set; }

		[AntiXss]
		public bool xjp_gradeprotection { get; set; }

		[AntiXss]
		public string opportunityratingcode { get; set; }

		[AntiXss]
		public string purchaseprocessname { get; set; }

		[AntiXss]
		public string xts_vehicledrivinglicensetypelabel { get; set; }

		[AntiXss]
		public string participatesinworkflowname { get; set; }

		[AntiXss]
		public string xts_insurancepaymentmethodidname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public string proposedsolution { get; set; }

		[AntiXss]
		public bool resolvefeedback { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_phonenumber { get; set; }

		[AntiXss]
		public string ktb_statusinterface { get; set; }

		[AntiXss]
		public string developproposalname { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public bool decisionmaker { get; set; }

		[AntiXss]
		public string ktb_progressstagename { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment2 { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid slainvokedid { get; set; }

		[AntiXss]
		public string ktb_spkidname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string ktb_occuationname { get; set; }

		[AntiXss]
		public string completefinalproposalname { get; set; }

		[AntiXss]
		public decimal xjp_bodilyinjurycompensation { get; set; }

		[AntiXss]
		public bool participatesinworkflow { get; set; }

		[AntiXss]
		public string xts_exteriorcoloridname { get; set; }

		[AntiXss]
		public DateTime estimatedclosedate { get; set; }

		[AntiXss]
		public string prioritycode { get; set; }

		[AntiXss]
		public string xts_salesobjective { get; set; }

		[AntiXss]
		public bool xts_propertydamageliabilityunlimitedcoverage { get; set; }

		[AntiXss]
		public Guid xts_insurancecompanyid { get; set; }

		[AntiXss]
		public string xts_potentialcontactidyominame { get; set; }

		[AntiXss]
		public decimal xto_discount { get; set; }

		[AntiXss]
		public string purchaseprocess { get; set; }

		[AntiXss]
		public string xts_insurancecarusagepurposename { get; set; }

		[AntiXss]
		public string msdyn_forecastcategory { get; set; }

		[AntiXss]
		public string pricelevelidname { get; set; }

		[AntiXss]
		public string xts_vehicledrivinglicensetype { get; set; }

		[AntiXss]
		public decimal estimatedvalue { get; set; }

		[AntiXss]
		public string xjp_temporaryloanercarname { get; set; }

		[AntiXss]
		public int onholdtime { get; set; }

		[AntiXss]
		public string xts_apppointmentidname { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_provinceidname { get; set; }

		[AntiXss]
		public decimal xts_insuranceannualpremium_base { get; set; }

		[AntiXss]
		public string emailaddress { get; set; }

		[AntiXss]
		public string need { get; set; }

		[AntiXss]
		public string isprivatename { get; set; }

		[AntiXss]
		public string contactidyominame { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string salesstagecodename { get; set; }

		[AntiXss]
		public Guid campaignid { get; set; }

		[AntiXss]
		public Guid parentcontactid { get; set; }

		[AntiXss]
		public decimal totallineitemdiscountamount { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal xjp_firstdeductible_base { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_vehicleincidentalcategory { get; set; }

		[AntiXss]
		public string xts_address4 { get; set; }

		[AntiXss]
		public string xjp_personalliabilityname { get; set; }

		[AntiXss]
		public Guid xts_countryid { get; set; }

		[AntiXss]
		public int xts_agelimiteddriver { get; set; }

		[AntiXss]
		public Guid xts_apppointmentid { get; set; }

		[AntiXss]
		public decimal xts_vehiclevalue_base { get; set; }

		[AntiXss]
		public bool isprivate { get; set; }

		[AntiXss]
		public string xts_address1 { get; set; }

		[AntiXss]
		public string confirminterestname { get; set; }

		[AntiXss]
		public decimal budgetamount_base { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_insurancecompanyname { get; set; }

		[AntiXss]
		public string xts_gendername { get; set; }

		[AntiXss]
		public string xts_cityidname { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string ktb_progressstage { get; set; }

		[AntiXss]
		public Guid xts_statusreasonid { get; set; }

		[AntiXss]
		public bool xjp_personalliability { get; set; }

		[AntiXss]
		public bool completefinalproposal { get; set; }

		[AntiXss]
		public string customerneed { get; set; }

		[AntiXss]
		public string ktb_vehiclecategoryidname { get; set; }

		[AntiXss]
		public string parentcontactidyominame { get; set; }

		[AntiXss]
		public string xjp_repurchasenewvehiclename { get; set; }

		[AntiXss]
		public string ktb_vehiclecolorname { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public string xts_insurancecarusagepurpose { get; set; }

		[AntiXss]
		public string isrevenuesystemcalculatedname { get; set; }

		[AntiXss]
		public string identifycompetitorsname { get; set; }

		[AntiXss]
		public string captureproposalfeedbackname { get; set; }

		[AntiXss]
		public Guid customerid { get; set; }

		[AntiXss]
		public string xjp_disastertotallossspecialagreementname { get; set; }

		[AntiXss]
		public Guid xts_villageandstreetid { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_occuation { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public bool identifycompetitors { get; set; }

		[AntiXss]
		public int teamsfollowed { get; set; }

		[AntiXss]
		public decimal totallineitemamount { get; set; }

		[AntiXss]
		public decimal xts_defaultprice { get; set; }

		[AntiXss]
		public string xjp_gradeprotectionname { get; set; }

		[AntiXss]
		public decimal xts_baseprice_base { get; set; }

		[AntiXss]
		public string xts_postalcode { get; set; }

		[AntiXss]
		public string evaluatefitname { get; set; }

		[AntiXss]
		public string timespentbymeonemailandmeetings { get; set; }

		[AntiXss]
		public decimal totaldiscountamount_base { get; set; }

		[AntiXss]
		public string salesstage { get; set; }

		[AntiXss]
		public string sendthankyounotename { get; set; }

		[AntiXss]
		public string xts_configurationidname { get; set; }

		[AntiXss]
		public string customerpainpoints { get; set; }

		[AntiXss]
		public DateTime finaldecisiondate { get; set; }

		[AntiXss]
		public string xts_insurancecompanyidname { get; set; }

		[AntiXss]
		public decimal xts_baseprice { get; set; }

		[AntiXss]
		public string parentaccountidyominame { get; set; }

		[AntiXss]
		public string timeline { get; set; }

		[AntiXss]
		public decimal xts_insuranceannualpremium { get; set; }

		[AntiXss]
		public string stepname { get; set; }

		[AntiXss]
		public bool xts_bodilyinjuryliabilityunlimitedcoverage { get; set; }

		[AntiXss]
		public decimal xto_discount_base { get; set; }

		[AntiXss]
		public string name { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public decimal totalamountlessfreight { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public string xts_address2 { get; set; }

		[AntiXss]
		public string timelinename { get; set; }

		[AntiXss]
		public Guid ktb_vehiclecategoryid { get; set; }

		[AntiXss]
		public decimal discountamount_base { get; set; }

		[AntiXss]
		public Guid stepid { get; set; }

		[AntiXss]
		public Guid xts_provinceid { get; set; }

		[AntiXss]
		public decimal xjp_seconddeductible_base { get; set; }

		[AntiXss]
		public decimal totalamountlessfreight_base { get; set; }

		[AntiXss]
		public decimal discountpercentage { get; set; }

		[AntiXss]
		public decimal xts_vehiclevalue { get; set; }

		[AntiXss]
		public DateTime xts_leaddate { get; set; }

		[AntiXss]
		public decimal xjp_seconddeductible { get; set; }

		[AntiXss]
		public string pricingerrorcode { get; set; }

		[AntiXss]
		public Guid xts_sourceinformationid { get; set; }

		[AntiXss]
		public DateTime actualclosedate { get; set; }

		[AntiXss]
		public decimal totallineitemamount_base { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public Guid accountid { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public Guid xts_vehicledrivinglicensetypeid { get; set; }

		[AntiXss]
		public string qualificationcomments { get; set; }

		[AntiXss]
		public decimal xjp_passengerscoverageamount { get; set; }

		[AntiXss]
		public bool identifypursuitteam { get; set; }

		[AntiXss]
		public Guid xts_interiorcolorid { get; set; }

		[AntiXss]
		public string xts_proposedinsurancecompanyidname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string presentproposalname { get; set; }

		[AntiXss]
		public string ktb_lostreason { get; set; }

		[AntiXss]
		public decimal actualvalue_base { get; set; }

		[AntiXss]
		public Guid xts_lastcontractid { get; set; }

		[AntiXss]
		public bool filedebrief { get; set; }

		[AntiXss]
		public Guid xts_potentialcontactid { get; set; }

		[AntiXss]
		public Guid xjp_platenumbersegment1id { get; set; }

		[AntiXss]
		public decimal budgetamount { get; set; }

		[AntiXss]
		public string ktb_tempdocumentname { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public decimal xts_bodilyinjuryliabilitycompensationamount_base { get; set; }

		[AntiXss]
		public string msdyn_forecastcategoryname { get; set; }

		[AntiXss]
		public decimal freightamount { get; set; }

		[AntiXss]
		public bool completeinternalreview { get; set; }

		[AntiXss]
		public string xts_bodilyinjuryliabilityunlimitedcoveragename { get; set; }

		[AntiXss]
		public string xts_insurednameidname { get; set; }

		[AntiXss]
		public bool xjp_legalfee { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
