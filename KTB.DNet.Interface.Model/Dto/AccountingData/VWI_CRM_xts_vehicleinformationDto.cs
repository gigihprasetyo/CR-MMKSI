#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehicleinformationDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:16:05
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_vehicleinformationDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public Guid xts_vehiclepublicid { get; set; }

		public Guid xts_financingcompanyid { get; set; }

		public Guid xts_vehiclesizeclassid { get; set; }

		public int statuscode { get; set; }

		public string xjp_vehicleowneraliasname { get; set; }

		public Guid xjp_automobileacquisitiontaxid { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string xts_vehicleownerlookupname { get; set; }

		public string xjp_vehicleowneraddress3 { get; set; }

		public string xjp_lastrecyclingissuecategory { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_vehicletype { get; set; }

		public decimal xjp_totalrecyclingpredeposit { get; set; }

		public Guid xjp_vehicleownercityid { get; set; }

		public string ktb_isbackbonename { get; set; }

		public decimal xts_width { get; set; }

		public string xts_receivingstoreidname { get; set; }

		public Guid xts_configurationid { get; set; }

		public string xjp_enginemodel { get; set; }

		public int xts_writeinmileage { get; set; }

		public string xts_vehicleusercontactidname { get; set; }

		public DateTime ktb_openfaktur { get; set; }

		public string xjp_automobileacquisitiontaxidname { get; set; }

		public string xts_vehiclepurchasernumber { get; set; }

		public int xts_vehiclepurchaserlookuptype { get; set; }

		public Guid xjp_automobiletaxid { get; set; }

		public string statecodename { get; set; }

		public string xjp_vehicleuseraddress4 { get; set; }

		public string xjp_vehicleuseraddress2 { get; set; }

		public string xjp_vehicleuseraddress3 { get; set; }

		public Guid xts_vehiclespecificationid { get; set; }

		public string xjp_vehicleuseraddress1 { get; set; }

		public decimal xts_maintenancecost { get; set; }

		public string xts_productexteriorcoloridname { get; set; }

		public DateTime xts_deliverydate { get; set; }

		public Guid xts_productid { get; set; }

		public int xts_lastvehicleordernumberlookuptype { get; set; }

		public string ktb_kodefsextended { get; set; }

		public string xts_customeridyominame { get; set; }

		public Guid xts_vehicleownercustomerid { get; set; }

		public decimal xjp_automobiletaxamount { get; set; }

		public Guid xjp_bodyshapeid { get; set; }

		public string xts_vehiclespecificationidname { get; set; }

		public string xts_enginenumber { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string xts_generation { get; set; }

		public string xts_productsegment3idname { get; set; }

		public decimal xjp_compulsoryinsuranceamount { get; set; }

		public Guid xts_lastvehicleorderid { get; set; }

		public string xjp_vehicleownercityidname { get; set; }

		public Guid xts_styleid { get; set; }

		public string xts_vehiclesizeclassidname { get; set; }

		public string xjp_vehicleowneraddress2 { get; set; }

		public Guid xts_productsegment2id { get; set; }

		public string xjp_vehicleownervillageandstreetidname { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid xts_vehicleinformationid { get; set; }

		public string xts_manufactureridname { get; set; }

		public bool ktb_isbackbone { get; set; }

		public Guid xts_originalstoreid { get; set; }

		public Guid xts_vehicleusercustomerid { get; set; }

		public DateTime xjp_mileageexchangedate { get; set; }

		public Guid xts_productsegment3id { get; set; }

		public string createdbyyominame { get; set; }

		public string ktb_transmissionname { get; set; }

		public Guid xts_vehiclepurchasercontactid { get; set; }

		public string ktb_vehiclebrandname { get; set; }

		public decimal xjp_weighttaxamount_base { get; set; }

		public string xts_productdescription { get; set; }

		public string xts_originatingvehiclepublic { get; set; }

		public string xjp_recognizedmodelidname { get; set; }

		public decimal xjp_grossweight2kg { get; set; }

		public string xts_platenumber { get; set; }

		public decimal xts_maintenancecost_base { get; set; }

		public string ktb_fakturstatusname { get; set; }

		public DateTime xts_servicepackageexpirationdate { get; set; }

		public int xts_deliverymileage { get; set; }

		public string xjp_insurancecategoryidname { get; set; }

		public Guid xjp_compulsoryinsurancevhcclassificationid { get; set; }

		public Guid xts_vehiclepayercustomerid { get; set; }

		public string xts_customernumber { get; set; }

		public Guid xts_receivingstoreid { get; set; }

		public string xjp_usecategory { get; set; }

		public string xjp_vehicleownerprovinceidname { get; set; }

		public string statuscodename { get; set; }

		public string xjp_vehicleuserpostalcode { get; set; }

		public decimal xts_acquisitionprice_base { get; set; }

		public Guid xts_productsegment1id { get; set; }

		public DateTime xjp_lastrecyclingdepositdate { get; set; }

		public string xts_productsegment1idname { get; set; }

		public string xts_issuedestinationidyominame { get; set; }

		public string ktb_drivesystem2 { get; set; }

		public string xjp_vehicleownerfirstname { get; set; }

		public string xts_vehiclepurchasercustomeridyominame { get; set; }

		public decimal xts_weight { get; set; }

		public string ktb_speedtype { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string ktb_dealernamednet { get; set; }

		public string xjp_modifiedvehiclename { get; set; }

		public DateTime ktb_stnkexpireddate { get; set; }

		public string xts_servicepackagecontractavailablename { get; set; }

		public string xts_vehicleownernumber { get; set; }

		public DateTime ktb_warrantystartdate { get; set; }

		public string xts_enginevolumeunitname { get; set; }

		public decimal xts_length { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid xts_vehiclepurchasercustomerid { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public string owneridname { get; set; }

		public int ktb_duration { get; set; }

		public string xts_vehiclepayercustomeridyominame { get; set; }

		public string xjp_vehicleuserfirstname { get; set; }

		public bool xjp_recyclingobject { get; set; }

		public DateTime ktb_pktdate { get; set; }

		public string xts_vehicleuserlookupname { get; set; }

		public string xjp_vehicleuservillageandstreetidname { get; set; }

		public string ktb_statusinterfacednetname { get; set; }

		public Guid xjp_vehicleownercountryid { get; set; }

		public int xts_frontorfrontaxleweight { get; set; }

		public string xjp_automobilecategory { get; set; }

		public string traversedpath { get; set; }

		public string xjp_vehicleinspectionperiod { get; set; }

		public decimal exchangerate { get; set; }

		public string xjp_vehicleuserlastname { get; set; }

		public string xts_productidname { get; set; }

		public string ktb_fakturstatus { get; set; }

		public string xts_vehicleusercustomeridname { get; set; }

		public string ktb_statusinterfacednet { get; set; }

		public decimal xjp_averagemileagemonth { get; set; }

		public string xjp_vehicleownercountryidname { get; set; }

		public Guid xts_warrantytypeid { get; set; }

		public string xts_vehiclepayercontactidname { get; set; }

		public string xjp_vehicleuseraliasname { get; set; }

		public string xts_fuelidname { get; set; }

		public string xts_productsegment2idname { get; set; }

		public string xts_vehiclepayerlookupname { get; set; }

		public string xjp_vehicleownerlastname { get; set; }

		public string xts_lastvehicleorderidname { get; set; }

		public DateTime ktb_fakturdate { get; set; }

		public string xjp_stampauthorityname { get; set; }

		public string ktb_otherbrand { get; set; }

		public string ktb_transmition { get; set; }

		public int xjp_capacity2 { get; set; }

		public Guid xjp_vehiclepurposeid { get; set; }

		public int xjp_capacity1 { get; set; }

		public string xjp_vehicleuserprovinceidname { get; set; }

		public Guid xts_fuelid { get; set; }

		public string xjp_vehicleowneraddress1 { get; set; }

		public Guid xjp_registrationofficeid { get; set; }

		public string xts_vehiclepayernumber { get; set; }

		public string xjp_platenumbersegment1idname { get; set; }

		public string xjp_usagecategoryname { get; set; }

		public string ktb_varianttype { get; set; }

		public string xts_vehicleownercustomeridyominame { get; set; }

		public string xjp_platenumbersegment4 { get; set; }

		public string xjp_bodyshapeidname { get; set; }

		public string xjp_firstregistration { get; set; }

		public DateTime createdon { get; set; }

		public string xjp_platenumbersegment3 { get; set; }

		public string xjp_platenumbersegment2 { get; set; }

		public string ktb_namaassuransi { get; set; }

		public bool xjp_modifiedvehicle { get; set; }

		public DateTime xjp_completeinspectiondate { get; set; }

		public string xjp_deniedvehicletypeemissioncontrol { get; set; }

		public int xts_vehicleuserlookuptype { get; set; }

		public Guid xts_shapeid { get; set; }

		public Guid xjp_vehicleusercountryid { get; set; }

		public string ktb_isinterfacename { get; set; }

		public string xts_vehiclepurchasercontactidyominame { get; set; }

		public string xjp_usagecategory { get; set; }

		public string xts_productinteriorcoloridname { get; set; }

		public string xjp_stampauthoritynumber { get; set; }

		public decimal xjp_automobileacquisitiontaxamount_base { get; set; }

		public decimal xjp_automobileacquisitiontaxamount { get; set; }

		public Guid xts_vehiclemodelid { get; set; }

		public Guid xjp_vehicleownerprovinceid { get; set; }

		public bool ktb_isinterface { get; set; }

		public int xts_currentmileage { get; set; }

		public string xts_locking { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_chassisnumber { get; set; }

		public Guid xjp_usageid { get; set; }

		public string xjp_compulsoryinsurancevhcclassificationidname { get; set; }

		public string ktb_drivesystem2name { get; set; }

		public Guid xts_productsegment4id { get; set; }

		public string ktb_description { get; set; }

		public int xts_historymileage { get; set; }

		public string createdbyname { get; set; }

		public int xts_vehicleownerlookuptype { get; set; }

		public Guid xjp_weighttaxid { get; set; }

		public int xts_regularservice { get; set; }

		public string ktb_speedtypename { get; set; }

		public int statecode { get; set; }

		public int xts_vehiclepayerlookuptype { get; set; }

		public int xts_rearorfrontaxleweight { get; set; }

		public DateTime xts_freeserviceenddate { get; set; }

		public DateTime xts_warrantyexpirationdate { get; set; }

		public DateTime xts_nextestimatedservicedate { get; set; }

		public string xjp_vehicleusercountryidname { get; set; }

		public string ktb_nomorpolis { get; set; }

		public string xts_warrantytypeidname { get; set; }

		public string xjp_vehicleusercityidname { get; set; }

		public decimal xts_averagemileageday { get; set; }

		public DateTime ktb_warrantyexpireddate { get; set; }

		public int ktb_mspkm { get; set; }

		public string xts_vehicleidentificationnumber { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public Guid xjp_vehicleusercityid { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string xts_vehiclepurchasercontactidname { get; set; }

		public string xjp_automobilecategoryname { get; set; }

		public string ktb_vehiclebrand { get; set; }

		public Guid owninguser { get; set; }

		public Guid xjp_insurancecategoryid { get; set; }

		public string ktb_dealercodednet { get; set; }

		public string xts_customeridname { get; set; }

		public DateTime ktb_validuntil { get; set; }

		public string ktb_solddealerid { get; set; }

		public string xts_modelspecificationnumber { get; set; }

		public decimal xts_height { get; set; }

		public Guid xjp_vehicleuservillageandstreetid { get; set; }

		public string modifiedbyname { get; set; }

		public int xts_mileagebeforeexchanged { get; set; }

		public string owneridtype { get; set; }

		public string ktb_typecaroseries { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string ktb_statusintfchassispktcreatednetname { get; set; }

		public string xjp_vehiclepurposeidname { get; set; }

		public string xjp_lastrecyclingissuecategoryname { get; set; }

		public Guid xjp_vehicleownervillageandstreetid { get; set; }

		public DateTime xts_registrationdate { get; set; }

		public decimal xjp_maximumload2 { get; set; }

		public string ktb_transmission { get; set; }

		public DateTime xts_lastmileagedate { get; set; }

		public string ktb_solddealerdesc { get; set; }

		public int xts_rearorrearaxleweight { get; set; }

		public Guid xts_maintenancemodelid { get; set; }

		public string xjp_vehicleuserbusinessphone { get; set; }

		public Guid xts_issuedestinationid { get; set; }

		public string xts_lastusedvehicleorderidname { get; set; }

		public Guid xts_vehicleownercontactid { get; set; }

		public DateTime xjp_compulsoryinsuranceexpirationdate { get; set; }

		public string xts_chassismodel { get; set; }

		public string xjp_usageidname { get; set; }

		public string xjp_usecategoryname { get; set; }

		public string xts_exteriorcolor { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string xts_vehicleusercontactidyominame { get; set; }

		public DateTime xts_lastnextestimatedservicedate { get; set; }

		public DateTime xts_customerreceiptdate { get; set; }

		public string ktb_statusintfchassispktcreatednet { get; set; }

		public Guid xts_lastservicemmsid { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public Guid ownerid { get; set; }

		public string xjp_lightvehiclecategory { get; set; }

		public Guid xts_productinteriorcolorid { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string xjp_runningmeterexchangename { get; set; }

		public string ktb_drivesystem { get; set; }

		public Guid owningteam { get; set; }

		public decimal xjp_grossweight1kg { get; set; }

		public int xts_warrantymileage { get; set; }

		public string xjp_automobiletaxidname { get; set; }

		public string xts_lastvehicleordernumberlookupname { get; set; }

		public Guid xts_vehicleusercontactid { get; set; }

		public string xts_styleidname { get; set; }

		public string xjp_registrationmodel { get; set; }

		public string xts_shapeidname { get; set; }

		public string ktb_leveldataname { get; set; }

		public string xts_vehicleownercontactidyominame { get; set; }

		public Guid xts_customerid { get; set; }

		public Guid xts_lastusedvehicleorderid { get; set; }

		public DateTime xjp_vehicleinspectionexpirationdate { get; set; }

		public string owneridyominame { get; set; }

		public string xjp_infantcategoryname { get; set; }

		public int xts_frontorrearaxleweight { get; set; }

		public decimal xjp_maximumload1 { get; set; }

		public decimal xts_enginevolume { get; set; }

		public string xjp_authorizedvehicletypeemissioncontrol { get; set; }

		public decimal xjp_weighttaxamount { get; set; }

		public string xts_vehiclepayercustomeridname { get; set; }

		public string xts_vehicleusernumber { get; set; }

		public Guid xts_salespersonid { get; set; }

		public string xts_interiorcolor { get; set; }

		public string xts_enginevolumeunit { get; set; }

		public string xts_classificationnumber { get; set; }

		public Guid stageid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_vehiclepurchasercustomeridname { get; set; }

		public string ktb_kodefleet { get; set; }

		public string xjp_compulsoryinsuranceexpirationdatejapan { get; set; }

		public Guid ktb_membershipvincommonid { get; set; }

		public string xts_vehiclepublicidname { get; set; }

		public string xjp_runningmeterexchange { get; set; }

		public Guid xts_vehiclepayercontactid { get; set; }

		public string xts_financingcompanyidyominame { get; set; }

		public bool xts_servicepackagecontractavailable { get; set; }

		public string ktb_mspnumber { get; set; }

		public string xts_configurationidname { get; set; }

		public string xts_keynumber { get; set; }

		public string xts_servicebusinessunitidname { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string xts_vehiclepayercontactidyominame { get; set; }

		public bool xjp_infantcategory { get; set; }

		public string ktb_membershipvincommonidname { get; set; }

		public string ktb_typecaroseriesname { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_vehiclename { get; set; }

		public string xts_salespersonidname { get; set; }

		public string xts_vehicleusercustomeridyominame { get; set; }

		public string xts_vehicletypename { get; set; }

		public string xjp_vehicleownerpostalcode { get; set; }

		public Guid xjp_vehicleuserprovinceid { get; set; }

		public string xjp_weighttaxidname { get; set; }

		public decimal xjp_automobiletaxamount_base { get; set; }

		public string xts_maintenancemodelidname { get; set; }

		public string xts_vehiclemodelidname { get; set; }

		public decimal xts_acquisitionprice { get; set; }

		public Guid xjp_recognizedmodelid { get; set; }

		public string xts_issuedestinationidname { get; set; }

		public string xjp_vehicleinspectionperiodname { get; set; }

		public Guid processid { get; set; }

		public string xjp_vehicleownerbusinessphone { get; set; }

		public Guid xts_servicebusinessunitid { get; set; }

		public string ktb_leveldata { get; set; }

		public string xjp_registrationofficeidname { get; set; }

		public string xts_pkcombinationkey { get; set; }

		public string xts_originalstoreidname { get; set; }

		public string xjp_lightvehiclecategoryname { get; set; }

		public string xts_productsegment4idname { get; set; }

		public string xts_businessunitidname { get; set; }

		public DateTime modifiedon { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xjp_recyclingobjectname { get; set; }

		public string xts_vehiclepurchaserlookupname { get; set; }

		public string xts_vehicleownercontactidname { get; set; }

		public string xts_lastservicemmsidname { get; set; }

		public decimal xjp_compulsoryinsuranceamount_base { get; set; }

		public string xts_vehicleownercustomeridname { get; set; }

		public Guid xjp_platenumbersegment1id { get; set; }

		public Guid createdby { get; set; }

		public string xts_productionyear { get; set; }

		public decimal xjp_totalrecyclingpredeposit_base { get; set; }

		public string xjp_vehicleowneraddress4 { get; set; }

		public string xts_financingcompanyidname { get; set; }

		public string xto_genericnumber { get; set; }

		public string ktb_externalcode { get; set; }

		public string msdyn_companycode { get; set; }
    }
}