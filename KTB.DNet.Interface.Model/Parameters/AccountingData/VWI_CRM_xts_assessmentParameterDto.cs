#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_assessmentParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 14:42:00
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
    public class VWI_CRM_xts_assessmentParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public Guid xts_vehiclepublicid { get; set; }
        [AntiXss]
        public Guid xts_consumptiontaxid { get; set; }
        [AntiXss]
        public decimal xts_vehiclemainbodyamounttotal_base { get; set; }
        [AntiXss]
        public string xjp_tonnagetaxrefundobject { get; set; }
        [AntiXss]
        public int xjp_tireaddingpoint { get; set; }
        [AntiXss]
        public Guid xts_receivingwarehouseid { get; set; }
        [AntiXss]
        public int xjp_fixtureaddingpoint { get; set; }
        [AntiXss]
        public string xjp_recyclingairbagequipmentname { get; set; }
        [AntiXss]
        public decimal xjp_fluorocarbondepositedamount_base { get; set; }
        [AntiXss]
        public decimal xts_interiorexteriorcorrectiontotal { get; set; }
        [AntiXss]
        public string xts_automaticcreatedname { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public int xts_transmissionadditionandsubtractionpoint { get; set; }
        [AntiXss]
        public string xjp_userphonenumber { get; set; }
        [AntiXss]
        public Guid xts_fuelcategoryid { get; set; }
        [AntiXss]
        public string xts_transmissionname { get; set; }
        [AntiXss]
        public string xjp_lightvehicleflagname { get; set; }
        [AntiXss]
        public string xts_basicpriceidname { get; set; }
        [AntiXss]
        public string xts_numberofdoors { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public decimal xts_coefficientofvehicleclass { get; set; }
        [AntiXss]
        public decimal xts_subtractioncorrectiontotal { get; set; }
        [AntiXss]
        public string xjp_owneridname { get; set; }
        [AntiXss]
        public string xjp_roofrackmaterialcolumnname { get; set; }
        [AntiXss]
        public string xjp_ownerpostalcode { get; set; }
        [AntiXss]
        public string xts_shiftposition { get; set; }
        [AntiXss]
        public string xts_address3 { get; set; }
        [AntiXss]
        public decimal xts_width { get; set; }
        [AntiXss]
        public string xjp_stereoequipment { get; set; }
        [AntiXss]
        public string xjp_tireshape { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string xts_vehiclemodelidname { get; set; }
        [AntiXss]
        public bool xts_finalassessmentexsistence { get; set; }
        [AntiXss]
        public string xjp_instructionmanualdeduction { get; set; }
        [AntiXss]
        public string xjp_tvequipment { get; set; }
        [AntiXss]
        public decimal xts_runningkmadditionandsubtractionamount_base { get; set; }
        [AntiXss]
        public Guid xts_assessmentid { get; set; }
        [AntiXss]
        public string xjp_address4 { get; set; }
        [AntiXss]
        public int xjp_compulsoryinsuranceremainingmonth { get; set; }
        [AntiXss]
        public decimal xts_transactionamount { get; set; }
        [AntiXss]
        public string xts_endlifevehiclename { get; set; }
        [AntiXss]
        public string xts_firstyearregistration { get; set; }
        [AntiXss]
        public decimal xts_manualmeasure3_base { get; set; }
        [AntiXss]
        public Guid xts_initialassessmentid { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public bool xjp_usersameasclient { get; set; }
        [AntiXss]
        public Guid xts_vehiclespecificationid { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsurancestartdatejapan { get; set; }
        [AntiXss]
        public string xjp_roofrackshapecolumnname { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public Guid xts_productid { get; set; }
        [AntiXss]
        public string xts_villageandstreetidname { get; set; }
        [AntiXss]
        public string xts_customeridyominame { get; set; }
        [AntiXss]
        public Guid xts_usedvehicleexteriorcolorid { get; set; }
        [AntiXss]
        public string xts_handling { get; set; }
        [AntiXss]
        public decimal xts_seatingcapacity1 { get; set; }
        [AntiXss]
        public int xjp_airbagaddingpoint { get; set; }
        [AntiXss]
        public string xjp_ownernumber { get; set; }
        [AntiXss]
        public decimal xts_remainingfinanceamount { get; set; }
        [AntiXss]
        public string xts_enginevolumeunitname { get; set; }
        [AntiXss]
        public decimal xts_basicpriceamount { get; set; }
        [AntiXss]
        public string xjp_navigationsystemequipment { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string xjp_tireshapename { get; set; }
        [AntiXss]
        public int xjp_outerplatevaluededuction { get; set; }
        [AntiXss]
        public string xts_motortype { get; set; }
        [AntiXss]
        public Guid xts_purchaseordertypeid { get; set; }
        [AntiXss]
        public int xjp_cdmdandstaddingpoint { get; set; }
        [AntiXss]
        public string xjp_ownersameasclientname { get; set; }
        [AntiXss]
        public string xjp_ascdname { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsurancecontractnumber { get; set; }
        [AntiXss]
        public string xjp_carinspectionexpirationdatejapan { get; set; }
        [AntiXss]
        public int xjp_commercialvaluedeductionsubtotal { get; set; }
        [AntiXss]
        public string xjp_registrationdatejapan { get; set; }
        [AntiXss]
        public decimal xts_basicpriceamount_base { get; set; }
        [AntiXss]
        public int xjp_navigationsystemcddvdaddingpoint { get; set; }
        [AntiXss]
        public Guid xjp_ownercountryid { get; set; }
        [AntiXss]
        public Guid xjp_caliremainingadditionandsubtractionid { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public Guid xjp_vehicleregistationcategoryid { get; set; }
        [AntiXss]
        public string xjp_powersteeringequipment { get; set; }
        [AntiXss]
        public string xts_manufactureridname { get; set; }
        [AntiXss]
        public string xjp_recyclingobjectname { get; set; }
        [AntiXss]
        public int xts_mileagebeforeitexchanges { get; set; }
        [AntiXss]
        public string xts_runningmeterexchange { get; set; }
        [AntiXss]
        public string xts_customercountryidname { get; set; }
        [AntiXss]
        public int xjp_repairhistorydeductionabc { get; set; }
        [AntiXss]
        public int xjp_sunroofaddingpoint { get; set; }
        [AntiXss]
        public Guid xts_recognizedmodelid { get; set; }
        [AntiXss]
        public string xts_purchaseusagecategoryname { get; set; }
        [AntiXss]
        public int xjp_powerwindowaddingpoint { get; set; }
        [AntiXss]
        public Guid xts_globalcolorid { get; set; }
        [AntiXss]
        public int xjp_absequipmentaddingpoint { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public string xjp_useridname { get; set; }
        [AntiXss]
        public int xts_runningkmadditionandsubtractionpoint { get; set; }
        [AntiXss]
        public string xts_grade { get; set; }
        [AntiXss]
        public string xjp_ownerphonenumber { get; set; }
        [AntiXss]
        public string xts_fuelcategoryidname { get; set; }
        [AntiXss]
        public string xjp_airbagequipmentname { get; set; }
        [AntiXss]
        public string xjp_sunroof { get; set; }
        [AntiXss]
        public Guid xjp_ownerprovinceid { get; set; }
        [AntiXss]
        public string xjp_powerseat { get; set; }
        [AntiXss]
        public Guid xts_vehiclebasicid { get; set; }
        [AntiXss]
        public decimal xts_maximumload2 { get; set; }
        [AntiXss]
        public decimal xts_manualmeasure1 { get; set; }
        [AntiXss]
        public decimal xts_internalpurchasingprice { get; set; }
        [AntiXss]
        public string xjp_writtenguaranteemaintpocketbookdeduction { get; set; }
        [AntiXss]
        public string xts_platenumber { get; set; }
        [AntiXss]
        public string xjp_roofname { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsuranceremainderaddingamount_base { get; set; }
        [AntiXss]
        public decimal xts_subtotalabccoefficientofxvehicleclass { get; set; }
        [AntiXss]
        public string xts_globalcoloridname { get; set; }
        [AntiXss]
        public decimal xts_transactionamount_base { get; set; }
        [AntiXss]
        public string xts_customerpostalcode { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsuranceremainderaddingamount { get; set; }
        [AntiXss]
        public string xts_transmission { get; set; }
        [AntiXss]
        public string xjp_airsuspensionname { get; set; }
        [AntiXss]
        public string xts_automobilelayoutidname { get; set; }
        [AntiXss]
        public string xts_customernumber { get; set; }
        [AntiXss]
        public int xjp_tvdeduction { get; set; }
        [AntiXss]
        public decimal xts_newcarmeasure { get; set; }
        [AntiXss]
        public int xjp_swandawdeduction { get; set; }
        [AntiXss]
        public decimal xts_priceatnewcar { get; set; }
        [AntiXss]
        public Guid xts_parentbusinessunitid { get; set; }
        [AntiXss]
        public string xjp_fluorocarbonclassificationname { get; set; }
        [AntiXss]
        public decimal xjp_coefficientofyearsa { get; set; }
        [AntiXss]
        public Guid xts_newvehiclesalesorderid { get; set; }
        [AntiXss]
        public string xts_leadidyominame { get; set; }
        [AntiXss]
        public decimal xjp_shredderdustcharge { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string xjp_noxobject { get; set; }
        [AntiXss]
        public int xts_mileage { get; set; }
        [AntiXss]
        public string xjp_useraddress2 { get; set; }
        [AntiXss]
        public string xts_laststatus { get; set; }
        [AntiXss]
        public string xjp_absequipmentname { get; set; }
        [AntiXss]
        public Guid xts_personinchargeid { get; set; }
        [AntiXss]
        public string xts_numberofdoorsname { get; set; }
        [AntiXss]
        public decimal xts_weight { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_productdescription { get; set; }
        [AntiXss]
        public int xjp_commercialvalueaddingpointsubtotal { get; set; }
        [AntiXss]
        public decimal xjp_informationmanagementcharge { get; set; }
        [AntiXss]
        public int xts_usedvehiclesalesorderlookuptype { get; set; }
        [AntiXss]
        public Guid xjp_userid { get; set; }
        [AntiXss]
        public string xts_personinchargeidname { get; set; }
        [AntiXss]
        public bool xts_endlifevehicle { get; set; }
        [AntiXss]
        public decimal xts_length { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public decimal xts_engineandelectricitemcorrectiontotal { get; set; }
        [AntiXss]
        public Guid xts_manufacturerid { get; set; }
        [AntiXss]
        public string xjp_depositedpaymentcategory { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public int xjp_coverleatheraddingpoint { get; set; }
        [AntiXss]
        public int xjp_jackandwrenchdeduction { get; set; }
        [AntiXss]
        public int xts_paintcoloradditionandsubtractionpoint { get; set; }
        [AntiXss]
        public string xjp_recyclingobject { get; set; }
        [AntiXss]
        public string xjp_insurancecompanyidname { get; set; }
        [AntiXss]
        public string xjp_roofracklength { get; set; }
        [AntiXss]
        public string xjp_caliremainingadditionandsubtractionidname { get; set; }
        [AntiXss]
        public string xjp_antennadeduction { get; set; }
        [AntiXss]
        public string xts_transactiondetails_json3 { get; set; }
        [AntiXss]
        public string xjp_aeroequipmentname { get; set; }
        [AntiXss]
        public decimal xts_transmissionadditionandsubtractionamount_base { get; set; }
        [AntiXss]
        public string xts_paintcoloradditionandsubtractionidname { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment1idname { get; set; }
        [AntiXss]
        public DateTime xjp_compulsoryinsurancestartdate { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public decimal xjp_shredderdustcharge_base { get; set; }
        [AntiXss]
        public decimal xjp_carinspectionperiod { get; set; }
        [AntiXss]
        public decimal xts_manualmeasure2_base { get; set; }
        [AntiXss]
        public string xjp_recyclingticketname { get; set; }
        [AntiXss]
        public DateTime xts_transactiondate { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public decimal xts_internalpurchasingprice_base { get; set; }
        [AntiXss]
        public int xjp_navigationsystemcddvddeduction { get; set; }
        [AntiXss]
        public string xjp_recyclingticketnumber { get; set; }
        [AntiXss]
        public string xjp_useraddress3 { get; set; }
        [AntiXss]
        public Guid xts_leadid { get; set; }
        [AntiXss]
        public int xjp_swandawaddingpoint { get; set; }
        [AntiXss]
        public string xts_goodsreceiptclassification { get; set; }
        [AntiXss]
        public string xjp_ownercityidname { get; set; }
        [AntiXss]
        public int xjp_otherfixtureaddingpoint { get; set; }
        [AntiXss]
        public string xjp_powerwindow { get; set; }
        [AntiXss]
        public DateTime xts_registrationdate { get; set; }
        [AntiXss]
        public string xts_leadidname { get; set; }
        [AntiXss]
        public string xts_vehicleclassidname { get; set; }
        [AntiXss]
        public int xjp_airconditionerdeduction { get; set; }
        [AntiXss]
        public decimal xjp_subtotadditionalbonuspointitemcorrection { get; set; }
        [AntiXss]
        public int xjp_aeroequipmentaddingpoint { get; set; }
        [AntiXss]
        public string xjp_useraddress1 { get; set; }
        [AntiXss]
        public string xjp_weightcategoryname { get; set; }
        [AntiXss]
        public decimal xjp_informationmanagementcharge_base { get; set; }
        [AntiXss]
        public string xts_customerprovinceidname { get; set; }
        [AntiXss]
        public decimal xjp_thecapitalmgmtchargeconsumingincludingtax { get; set; }
        [AntiXss]
        public Guid xjp_platenumbersegment1id { get; set; }
        [AntiXss]
        public Guid xjp_ownercityid { get; set; }
        [AntiXss]
        public string xjp_useraddress4 { get; set; }
        [AntiXss]
        public string xjp_powerseatname { get; set; }
        [AntiXss]
        public int xjp_fixturedeductionsubtotal { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public Guid xts_receivingsiteid { get; set; }
        [AntiXss]
        public string xts_repairhistory { get; set; }
        [AntiXss]
        public Guid xts_villageandstreetid { get; set; }
        [AntiXss]
        public Guid xts_vehicleclassid { get; set; }
        [AntiXss]
        public decimal xts_manualmeasure1_base { get; set; }
        [AntiXss]
        public string xts_assessmentnumber { get; set; }
        [AntiXss]
        public Guid xts_basicpriceid { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment4 { get; set; }
        [AntiXss]
        public string xts_usedvehiclesalesorderlookupname { get; set; }
        [AntiXss]
        public string xjp_owneraddress4 { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment3 { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment2 { get; set; }
        [AntiXss]
        public string xjp_owneraddress1 { get; set; }
        [AntiXss]
        public string xjp_owneraddress2 { get; set; }
        [AntiXss]
        public string xjp_ownervillageandstreetidname { get; set; }
        [AntiXss]
        public int xjp_fixturededuction { get; set; }
        [AntiXss]
        public string xjp_classificationnumber { get; set; }
        [AntiXss]
        public Guid xts_plannedreceivingstoreid { get; set; }
        [AntiXss]
        public string xts_vehiclebasicidname { get; set; }
        [AntiXss]
        public string xts_motortypename { get; set; }
        [AntiXss]
        public decimal xts_maximumload1 { get; set; }
        [AntiXss]
        public string xts_transactiondetails_json { get; set; }
        [AntiXss]
        public decimal xjp_subtotalengineandelectricitemcorrection { get; set; }
        [AntiXss]
        public string xts_assessmentclassificationname { get; set; }
        [AntiXss]
        public string xts_inventoryusedvehicleidname { get; set; }
        [AntiXss]
        public Guid xts_customercountryid { get; set; }
        [AntiXss]
        public string xjp_airconditionerequipment { get; set; }
        [AntiXss]
        public decimal xts_additionalbonuspointitemcorrectiontotal { get; set; }
        [AntiXss]
        public decimal xts_assessmentpricetotal_base { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public int xjp_cdmdandstdeduction { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_transmissionadditionandsubtractionidname { get; set; }
        [AntiXss]
        public string xts_goodsreceiptclassificationname { get; set; }
        [AntiXss]
        public string xjp_antennadeductionname { get; set; }
        [AntiXss]
        public Guid xjp_ownerid { get; set; }
        [AntiXss]
        public string xts_purchaseordertypeidname { get; set; }
        [AntiXss]
        public string xjp_recyclingticket { get; set; }
        [AntiXss]
        public string xjp_roofrackmaterialcolumn { get; set; }
        [AntiXss]
        public string xjp_writtenguarantmaintpocketbooksetaddpointname { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string xts_productidname { get; set; }
        [AntiXss]
        public string xjp_weightcategory { get; set; }
        [AntiXss]
        public string xts_receivingwarehouseidname { get; set; }
        [AntiXss]
        public bool xjp_lightvehicleflag { get; set; }
        [AntiXss]
        public DateTime xts_expectedreceivingdate { get; set; }
        [AntiXss]
        public string xjp_fluorocarbonequipmentname { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public decimal xts_priceatnewcar_base { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string xjp_airbagequipment { get; set; }
        [AntiXss]
        public Guid xts_inventoryusedvehicleid { get; set; }
        [AntiXss]
        public string xjp_garrageaddress { get; set; }
        [AntiXss]
        public Guid xjp_vhcinspectionremdraddtnandsubtractionid { get; set; }
        [AntiXss]
        public string xjp_recordlist { get; set; }
        [AntiXss]
        public int xjp_carinspectionremainingmonth { get; set; }
        [AntiXss]
        public string xjp_coverleather { get; set; }
        [AntiXss]
        public int xjp_tirededuction { get; set; }
        [AntiXss]
        public decimal xjp_remainingdebtamount { get; set; }
        [AntiXss]
        public int xjp_mileageatcarinspection { get; set; }
        [AntiXss]
        public string xts_vehicleidentificationnumber { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public Guid xjp_ownervillageandstreetid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_phonenumber { get; set; }
        [AntiXss]
        public string xts_vehicleage { get; set; }
        [AntiXss]
        public int xjp_fixtureaddingpointsubtotal { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public decimal xjp_subtotalinteriorexteriorcorrection { get; set; }
        [AntiXss]
        public int xjp_sunroofdeduction { get; set; }
        [AntiXss]
        public Guid xts_paintcoloradditionandsubtractionid { get; set; }
        [AntiXss]
        public int xts_registrationelapsedmonth { get; set; }
        [AntiXss]
        public bool xjp_ascd { get; set; }
        [AntiXss]
        public string xts_customeridname { get; set; }
        [AntiXss]
        public Guid xts_customercityid { get; set; }
        [AntiXss]
        public string xjp_vhcinspectionremdraddtnandsubtractionidname { get; set; }
        [AntiXss]
        public string xts_handlingname { get; set; }
        [AntiXss]
        public string xts_runningmeterexchangename { get; set; }
        [AntiXss]
        public string xts_parentbusinessunitidname { get; set; }
        [AntiXss]
        public string xjp_productionyearjapan { get; set; }
        [AntiXss]
        public int xjp_coverleatherdeduction { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string xts_plannedreceivingstoreidname { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public int xjp_airconditioneraddingpoint { get; set; }
        [AntiXss]
        public int xjp_otherfixturededuction { get; set; }
        [AntiXss]
        public string xts_chassisnumber { get; set; }
        [AntiXss]
        public int xjp_aeroequipmentdeduction { get; set; }
        [AntiXss]
        public string xts_vehiclespecificationidname { get; set; }
        [AntiXss]
        public string xjp_owneraddress3 { get; set; }
        [AntiXss]
        public string xjp_roof { get; set; }
        [AntiXss]
        public decimal xts_rate_base { get; set; }
        [AntiXss]
        public decimal xjp_recyclingchargetotal_base { get; set; }
        [AntiXss]
        public decimal xts_additionalcorrectiontotal { get; set; }
        [AntiXss]
        public string xjp_uservillageandstreetidname { get; set; }
        [AntiXss]
        public string xts_motive { get; set; }
        [AntiXss]
        public Guid xjp_modeldivisionid { get; set; }
        [AntiXss]
        public string xts_shiftpositionname { get; set; }
        [AntiXss]
        public DateTime xjp_carinspectionexpirationdate { get; set; }
        [AntiXss]
        public string xjp_stereoequipmentname { get; set; }
        [AntiXss]
        public string xjp_fluorocarbonequipment { get; set; }
        [AntiXss]
        public string xjp_powerwindowname { get; set; }
        [AntiXss]
        public string xts_bodyshapeidname { get; set; }
        [AntiXss]
        public string xts_customercityidname { get; set; }
        [AntiXss]
        public decimal xts_vehiclemainbodyamounttotal { get; set; }
        [AntiXss]
        public string xts_status { get; set; }
        [AntiXss]
        public decimal xts_transmissionadditionandsubtractionamount { get; set; }
        [AntiXss]
        public decimal xts_runningkmadditionandsubtractionamount { get; set; }
        [AntiXss]
        public string xjp_tvequipmentname { get; set; }
        [AntiXss]
        public string xjp_owneridyominame { get; set; }
        [AntiXss]
        public decimal xjp_thecapitalmgmtchargeconsumingincludingtax_base { get; set; }
        [AntiXss]
        public string xts_chassismodel { get; set; }
        [AntiXss]
        public string xjp_airconditionerequipmentname { get; set; }
        [AntiXss]
        public string xts_usedvehicleexteriorcoloridname { get; set; }
        [AntiXss]
        public decimal xjp_recyclingchargetotal { get; set; }
        [AntiXss]
        public decimal xts_baseamount { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_transactiondetails_json2 { get; set; }
        [AntiXss]
        public string xjp_firstyearregistrationjapan { get; set; }
        [AntiXss]
        public Guid xts_transmissionadditionandsubtractionid { get; set; }
        [AntiXss]
        public string xts_runningkmadditionandsubtractionidname { get; set; }
        [AntiXss]
        public decimal xjp_fluorocarbondepositedamount { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public string xts_receivingsiteidname { get; set; }
        [AntiXss]
        public decimal xts_additionalcorrectiontotal_base { get; set; }
        [AntiXss]
        public string xjp_recyclingairbagequipment { get; set; }
        [AntiXss]
        public int xjp_airbagdeduction { get; set; }
        [AntiXss]
        public string traversedpath { get; set; }
        [AntiXss]
        public decimal xts_consumptiontaxamount_base { get; set; }
        [AntiXss]
        public string xjp_cabsectionname { get; set; }
        [AntiXss]
        public string xjp_instructionmanualdeductionname { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public Guid xts_customerprovinceid { get; set; }
        [AntiXss]
        public Guid xts_automobilelayoutid { get; set; }
        [AntiXss]
        public string xts_recognizedmodelidname { get; set; }
        [AntiXss]
        public string xts_usedvehiclesalesorderidname { get; set; }
        [AntiXss]
        public string xjp_roofracklengthname { get; set; }
        [AntiXss]
        public decimal xts_newcarmeasure_base { get; set; }
        [AntiXss]
        public decimal xjp_remainingdebtamount_base { get; set; }
        [AntiXss]
        public string xjp_absequipment { get; set; }
        [AntiXss]
        public string xts_address1 { get; set; }
        [AntiXss]
        public string xjp_sunroofname { get; set; }
        [AntiXss]
        public string xjp_tonnagetaxrefundobjectname { get; set; }
        [AntiXss]
        public Guid xts_customerid { get; set; }
        [AntiXss]
        public decimal xjp_airbagdepositedamount_base { get; set; }
        [AntiXss]
        public Guid xts_receivingbusinessunitid { get; set; }
        [AntiXss]
        public string xts_finalassessmentexsistencename { get; set; }
        [AntiXss]
        public string xts_motivename { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string xjp_useridyominame { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsuranceenddatejapan { get; set; }
        [AntiXss]
        public string xjp_writtenguarantmaintpocketbooksetaddpoint { get; set; }
        [AntiXss]
        public string xts_vehicleagename { get; set; }
        [AntiXss]
        public string xjp_automaticspeedcontroldeviceexistence { get; set; }
        [AntiXss]
        public Guid xjp_ownervendorid { get; set; }
        [AntiXss]
        public decimal xts_enginevolume { get; set; }
        [AntiXss]
        public decimal xts_paintcoloradditionandsubtractionamount { get; set; }
        [AntiXss]
        public Guid xts_vehiclemodelid { get; set; }
        [AntiXss]
        public string xts_receivingbusinessunitidname { get; set; }
        [AntiXss]
        public bool xts_automaticcreated { get; set; }
        [AntiXss]
        public string xts_comment { get; set; }
        [AntiXss]
        public int xjp_absequipmentdeduction { get; set; }
        [AntiXss]
        public string xjp_otherequipment { get; set; }
        [AntiXss]
        public string xts_enginevolumeunit { get; set; }
        [AntiXss]
        public string xjp_roofrackshapecolumn { get; set; }
        [AntiXss]
        public Guid stageid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public int xjp_sideviewmirrordeduction { get; set; }
        [AntiXss]
        public decimal xts_manualmeasure3 { get; set; }
        [AntiXss]
        public string xts_newvehiclesalesorderidname { get; set; }
        [AntiXss]
        public bool xjp_airsuspension { get; set; }
        [AntiXss]
        public Guid xjp_insurancecompanyid { get; set; }
        [AntiXss]
        public decimal xts_height { get; set; }
        [AntiXss]
        public int xjp_commercialvaluededuction { get; set; }
        [AntiXss]
        public int xjp_actualexpensesdeduction { get; set; }
        [AntiXss]
        public Guid xts_runningkmadditionandsubtractionid { get; set; }
        [AntiXss]
        public string xjp_aeroequipment { get; set; }
        [AntiXss]
        public string xts_vehiclepublicidname { get; set; }
        [AntiXss]
        public string xjp_writtenguaranteemaintpocketbookdeductionname { get; set; }
        [AntiXss]
        public int xjp_powersteeringaddingpoint { get; set; }
        [AntiXss]
        public decimal xjp_airbagdepositedamount { get; set; }
        [AntiXss]
        public decimal xts_paintcoloradditionandsubtractionamount_base { get; set; }
        [AntiXss]
        public decimal xts_consumptiontaxamount { get; set; }
        [AntiXss]
        public string xts_postalcode { get; set; }
        [AntiXss]
        public string xjp_userpostalcode { get; set; }
        [AntiXss]
        public decimal xts_tradeintotal { get; set; }
        [AntiXss]
        public string xts_numberofgears { get; set; }
        [AntiXss]
        public string xjp_coverleathername { get; set; }
        [AntiXss]
        public decimal xts_rate { get; set; }
        [AntiXss]
        public string xjp_ownercountryidname { get; set; }
        [AntiXss]
        public Guid xts_bodyshapeid { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public bool xjp_ownersameasclient { get; set; }
        [AntiXss]
        public string xts_purchaseusagecategory { get; set; }
        [AntiXss]
        public decimal xts_subtractioncorrectiontotal_base { get; set; }
        [AntiXss]
        public string xts_statusname { get; set; }
        [AntiXss]
        public string xts_interiorcolor { get; set; }
        [AntiXss]
        public string xts_repairhistoryname { get; set; }
        [AntiXss]
        public string xjp_usersameasclientname { get; set; }
        [AntiXss]
        public decimal xts_tradeintotal_base { get; set; }
        [AntiXss]
        public decimal xts_seatingcapacity2 { get; set; }
        [AntiXss]
        public string xjp_modelspecification { get; set; }
        [AntiXss]
        public string xts_numberofgearsname { get; set; }
        [AntiXss]
        public decimal xts_remainingfinanceamount_base { get; set; }
        [AntiXss]
        public string xts_address2 { get; set; }
        [AntiXss]
        public string xjp_automaticspeedcontroldeviceexistencename { get; set; }
        [AntiXss]
        public int xjp_accidentrepairexpense { get; set; }
        [AntiXss]
        public string xts_initialassessmentidname { get; set; }
        [AntiXss]
        public string xts_motormodel { get; set; }
        [AntiXss]
        public decimal xjp_vhcinspectionremainderaddingpointamount { get; set; }
        [AntiXss]
        public decimal xts_baseamount_base { get; set; }
        [AntiXss]
        public string xts_assessmentclassification { get; set; }
        [AntiXss]
        public string xjp_ownervendoridname { get; set; }
        [AntiXss]
        public string xjp_ownerprovinceidname { get; set; }
        [AntiXss]
        public int xjp_powersteeringdeduction { get; set; }
        [AntiXss]
        public string xjp_modeldivisionidname { get; set; }
        [AntiXss]
        public string xts_stocknumber { get; set; }
        [AntiXss]
        public string xjp_powersteeringequipmentname { get; set; }
        [AntiXss]
        public string xjp_usernumber { get; set; }
        [AntiXss]
        public decimal xjp_coefficientofyearsc { get; set; }
        [AntiXss]
        public decimal xjp_coefficientofyearsb { get; set; }
        [AntiXss]
        public Guid xjp_uservillageandstreetid { get; set; }
        [AntiXss]
        public string xjp_cabsection { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string xjp_vehicleregistationcategoryidname { get; set; }
        [AntiXss]
        public Guid processid { get; set; }
        [AntiXss]
        public string xts_ribbondata { get; set; }
        [AntiXss]
        public Guid xts_usedvehiclesalesorderid { get; set; }
        [AntiXss]
        public string xjp_aluminiumwheelequipment { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public string xts_consumptiontaxidname { get; set; }
        [AntiXss]
        public string xjp_recordlistname { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public int xjp_commercialvalueaddingpoint { get; set; }
        [AntiXss]
        public decimal xts_manualmeasure2 { get; set; }
        [AntiXss]
        public decimal xts_assessmentpricetotal { get; set; }
        [AntiXss]
        public int xjp_tvaddingpoint { get; set; }
        [AntiXss]
        public string xjp_fluorocarbonclassification { get; set; }
        [AntiXss]
        public string xjp_noxobjectname { get; set; }
        [AntiXss]
        public decimal xjp_vhcinspectionremainderaddingpointamount_base { get; set; }
        [AntiXss]
        public int xjp_powerwindowdeduction { get; set; }
        [AntiXss]
        public string xts_productionyear { get; set; }
        [AntiXss]
        public string xjp_navigationsystemequipmentname { get; set; }
        [AntiXss]
        public DateTime xjp_compulsoryinsuranceenddate { get; set; }
        [AntiXss]
        public string xjp_depositedpaymentcategoryname { get; set; }
        [AntiXss]
        public string xts_laststatusname { get; set; }
        [AntiXss]
        public string xjp_aluminiumwheelequipmentname { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
