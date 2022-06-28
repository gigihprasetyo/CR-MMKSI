#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehicleinformationParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_vehicleinformationParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Guid xts_vehiclepublicid { get; set; }

		[AntiXss]
		public Guid xts_financingcompanyid { get; set; }

		[AntiXss]
		public Guid xts_vehiclesizeclassid { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string xjp_vehicleowneraliasname { get; set; }

		[AntiXss]
		public Guid xjp_automobileacquisitiontaxid { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_vehicleownerlookupname { get; set; }

		[AntiXss]
		public string xjp_vehicleowneraddress3 { get; set; }

		[AntiXss]
		public string xjp_lastrecyclingissuecategory { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_vehicletype { get; set; }

		[AntiXss]
		public decimal xjp_totalrecyclingpredeposit { get; set; }

		[AntiXss]
		public Guid xjp_vehicleownercityid { get; set; }

		[AntiXss]
		public string ktb_isbackbonename { get; set; }

		[AntiXss]
		public decimal xts_width { get; set; }

		[AntiXss]
		public string xts_receivingstoreidname { get; set; }

		[AntiXss]
		public Guid xts_configurationid { get; set; }

		[AntiXss]
		public string xjp_enginemodel { get; set; }

		[AntiXss]
		public int xts_writeinmileage { get; set; }

		[AntiXss]
		public string xts_vehicleusercontactidname { get; set; }

		[AntiXss]
		public DateTime ktb_openfaktur { get; set; }

		[AntiXss]
		public string xjp_automobileacquisitiontaxidname { get; set; }

		[AntiXss]
		public string xts_vehiclepurchasernumber { get; set; }

		[AntiXss]
		public int xts_vehiclepurchaserlookuptype { get; set; }

		[AntiXss]
		public Guid xjp_automobiletaxid { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xjp_vehicleuseraddress4 { get; set; }

		[AntiXss]
		public string xjp_vehicleuseraddress2 { get; set; }

		[AntiXss]
		public string xjp_vehicleuseraddress3 { get; set; }

		[AntiXss]
		public Guid xts_vehiclespecificationid { get; set; }

		[AntiXss]
		public string xjp_vehicleuseraddress1 { get; set; }

		[AntiXss]
		public decimal xts_maintenancecost { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public DateTime xts_deliverydate { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public int xts_lastvehicleordernumberlookuptype { get; set; }

		[AntiXss]
		public string ktb_kodefsextended { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public Guid xts_vehicleownercustomerid { get; set; }

		[AntiXss]
		public decimal xjp_automobiletaxamount { get; set; }

		[AntiXss]
		public Guid xjp_bodyshapeid { get; set; }

		[AntiXss]
		public string xts_vehiclespecificationidname { get; set; }

		[AntiXss]
		public string xts_enginenumber { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_generation { get; set; }

		[AntiXss]
		public string xts_productsegment3idname { get; set; }

		[AntiXss]
		public decimal xjp_compulsoryinsuranceamount { get; set; }

		[AntiXss]
		public Guid xts_lastvehicleorderid { get; set; }

		[AntiXss]
		public string xjp_vehicleownercityidname { get; set; }

		[AntiXss]
		public Guid xts_styleid { get; set; }

		[AntiXss]
		public string xts_vehiclesizeclassidname { get; set; }

		[AntiXss]
		public string xjp_vehicleowneraddress2 { get; set; }

		[AntiXss]
		public Guid xts_productsegment2id { get; set; }

		[AntiXss]
		public string xjp_vehicleownervillageandstreetidname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_vehicleinformationid { get; set; }

		[AntiXss]
		public string xts_manufactureridname { get; set; }

		[AntiXss]
		public bool ktb_isbackbone { get; set; }

		[AntiXss]
		public Guid xts_originalstoreid { get; set; }

		[AntiXss]
		public Guid xts_vehicleusercustomerid { get; set; }

		[AntiXss]
		public DateTime xjp_mileageexchangedate { get; set; }

		[AntiXss]
		public Guid xts_productsegment3id { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string ktb_transmissionname { get; set; }

		[AntiXss]
		public Guid xts_vehiclepurchasercontactid { get; set; }

		[AntiXss]
		public string ktb_vehiclebrandname { get; set; }

		[AntiXss]
		public decimal xjp_weighttaxamount_base { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string xts_originatingvehiclepublic { get; set; }

		[AntiXss]
		public string xjp_recognizedmodelidname { get; set; }

		[AntiXss]
		public decimal xjp_grossweight2kg { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

		[AntiXss]
		public decimal xts_maintenancecost_base { get; set; }

		[AntiXss]
		public string ktb_fakturstatusname { get; set; }

		[AntiXss]
		public DateTime xts_servicepackageexpirationdate { get; set; }

		[AntiXss]
		public int xts_deliverymileage { get; set; }

		[AntiXss]
		public string xjp_insurancecategoryidname { get; set; }

		[AntiXss]
		public Guid xjp_compulsoryinsurancevhcclassificationid { get; set; }

		[AntiXss]
		public Guid xts_vehiclepayercustomerid { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public Guid xts_receivingstoreid { get; set; }

		[AntiXss]
		public string xjp_usecategory { get; set; }

		[AntiXss]
		public string xjp_vehicleownerprovinceidname { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xjp_vehicleuserpostalcode { get; set; }

		[AntiXss]
		public decimal xts_acquisitionprice_base { get; set; }

		[AntiXss]
		public Guid xts_productsegment1id { get; set; }

		[AntiXss]
		public DateTime xjp_lastrecyclingdepositdate { get; set; }

		[AntiXss]
		public string xts_productsegment1idname { get; set; }

		[AntiXss]
		public string xts_issuedestinationidyominame { get; set; }

		[AntiXss]
		public string ktb_drivesystem2 { get; set; }

		[AntiXss]
		public string xjp_vehicleownerfirstname { get; set; }

		[AntiXss]
		public string xts_vehiclepurchasercustomeridyominame { get; set; }

		[AntiXss]
		public decimal xts_weight { get; set; }

		[AntiXss]
		public string ktb_speedtype { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_dealernamednet { get; set; }

		[AntiXss]
		public string xjp_modifiedvehiclename { get; set; }

		[AntiXss]
		public DateTime ktb_stnkexpireddate { get; set; }

		[AntiXss]
		public string xts_servicepackagecontractavailablename { get; set; }

		[AntiXss]
		public string xts_vehicleownernumber { get; set; }

		[AntiXss]
		public DateTime ktb_warrantystartdate { get; set; }

		[AntiXss]
		public string xts_enginevolumeunitname { get; set; }

		[AntiXss]
		public decimal xts_length { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid xts_vehiclepurchasercustomerid { get; set; }

		[AntiXss]
		public Guid xts_manufacturerid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public int ktb_duration { get; set; }

		[AntiXss]
		public string xts_vehiclepayercustomeridyominame { get; set; }

		[AntiXss]
		public string xjp_vehicleuserfirstname { get; set; }

		[AntiXss]
		public bool xjp_recyclingobject { get; set; }

		[AntiXss]
		public DateTime ktb_pktdate { get; set; }

		[AntiXss]
		public string xts_vehicleuserlookupname { get; set; }

		[AntiXss]
		public string xjp_vehicleuservillageandstreetidname { get; set; }

		[AntiXss]
		public string ktb_statusinterfacednetname { get; set; }

		[AntiXss]
		public Guid xjp_vehicleownercountryid { get; set; }

		[AntiXss]
		public int xts_frontorfrontaxleweight { get; set; }

		[AntiXss]
		public string xjp_automobilecategory { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string xjp_vehicleinspectionperiod { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xjp_vehicleuserlastname { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public string ktb_fakturstatus { get; set; }

		[AntiXss]
		public string xts_vehicleusercustomeridname { get; set; }

		[AntiXss]
		public string ktb_statusinterfacednet { get; set; }

		[AntiXss]
		public decimal xjp_averagemileagemonth { get; set; }

		[AntiXss]
		public string xjp_vehicleownercountryidname { get; set; }

		[AntiXss]
		public Guid xts_warrantytypeid { get; set; }

		[AntiXss]
		public string xts_vehiclepayercontactidname { get; set; }

		[AntiXss]
		public string xjp_vehicleuseraliasname { get; set; }

		[AntiXss]
		public string xts_fuelidname { get; set; }

		[AntiXss]
		public string xts_productsegment2idname { get; set; }

		[AntiXss]
		public string xts_vehiclepayerlookupname { get; set; }

		[AntiXss]
		public string xjp_vehicleownerlastname { get; set; }

		[AntiXss]
		public string xts_lastvehicleorderidname { get; set; }

		[AntiXss]
		public DateTime ktb_fakturdate { get; set; }

		[AntiXss]
		public string xjp_stampauthorityname { get; set; }

		[AntiXss]
		public string ktb_otherbrand { get; set; }

		[AntiXss]
		public string ktb_transmition { get; set; }

		[AntiXss]
		public int xjp_capacity2 { get; set; }

		[AntiXss]
		public Guid xjp_vehiclepurposeid { get; set; }

		[AntiXss]
		public int xjp_capacity1 { get; set; }

		[AntiXss]
		public string xjp_vehicleuserprovinceidname { get; set; }

		[AntiXss]
		public Guid xts_fuelid { get; set; }

		[AntiXss]
		public string xjp_vehicleowneraddress1 { get; set; }

		[AntiXss]
		public Guid xjp_registrationofficeid { get; set; }

		[AntiXss]
		public string xts_vehiclepayernumber { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment1idname { get; set; }

		[AntiXss]
		public string xjp_usagecategoryname { get; set; }

		[AntiXss]
		public string ktb_varianttype { get; set; }

		[AntiXss]
		public string xts_vehicleownercustomeridyominame { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment4 { get; set; }

		[AntiXss]
		public string xjp_bodyshapeidname { get; set; }

		[AntiXss]
		public string xjp_firstregistration { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment3 { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment2 { get; set; }

		[AntiXss]
		public string ktb_namaassuransi { get; set; }

		[AntiXss]
		public bool xjp_modifiedvehicle { get; set; }

		[AntiXss]
		public DateTime xjp_completeinspectiondate { get; set; }

		[AntiXss]
		public string xjp_deniedvehicletypeemissioncontrol { get; set; }

		[AntiXss]
		public int xts_vehicleuserlookuptype { get; set; }

		[AntiXss]
		public Guid xts_shapeid { get; set; }

		[AntiXss]
		public Guid xjp_vehicleusercountryid { get; set; }

		[AntiXss]
		public string ktb_isinterfacename { get; set; }

		[AntiXss]
		public string xts_vehiclepurchasercontactidyominame { get; set; }

		[AntiXss]
		public string xjp_usagecategory { get; set; }

		[AntiXss]
		public string xts_productinteriorcoloridname { get; set; }

		[AntiXss]
		public string xjp_stampauthoritynumber { get; set; }

		[AntiXss]
		public decimal xjp_automobileacquisitiontaxamount_base { get; set; }

		[AntiXss]
		public decimal xjp_automobileacquisitiontaxamount { get; set; }

		[AntiXss]
		public Guid xts_vehiclemodelid { get; set; }

		[AntiXss]
		public Guid xjp_vehicleownerprovinceid { get; set; }

		[AntiXss]
		public bool ktb_isinterface { get; set; }

		[AntiXss]
		public int xts_currentmileage { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_chassisnumber { get; set; }

		[AntiXss]
		public Guid xjp_usageid { get; set; }

		[AntiXss]
		public string xjp_compulsoryinsurancevhcclassificationidname { get; set; }

		[AntiXss]
		public string ktb_drivesystem2name { get; set; }

		[AntiXss]
		public Guid xts_productsegment4id { get; set; }

		[AntiXss]
		public string ktb_description { get; set; }

		[AntiXss]
		public int xts_historymileage { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public int xts_vehicleownerlookuptype { get; set; }

		[AntiXss]
		public Guid xjp_weighttaxid { get; set; }

		[AntiXss]
		public int xts_regularservice { get; set; }

		[AntiXss]
		public string ktb_speedtypename { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public int xts_vehiclepayerlookuptype { get; set; }

		[AntiXss]
		public int xts_rearorfrontaxleweight { get; set; }

		[AntiXss]
		public DateTime xts_freeserviceenddate { get; set; }

		[AntiXss]
		public DateTime xts_warrantyexpirationdate { get; set; }

		[AntiXss]
		public DateTime xts_nextestimatedservicedate { get; set; }

		[AntiXss]
		public string xjp_vehicleusercountryidname { get; set; }

		[AntiXss]
		public string ktb_nomorpolis { get; set; }

		[AntiXss]
		public string xts_warrantytypeidname { get; set; }

		[AntiXss]
		public string xjp_vehicleusercityidname { get; set; }

		[AntiXss]
		public decimal xts_averagemileageday { get; set; }

		[AntiXss]
		public DateTime ktb_warrantyexpireddate { get; set; }

		[AntiXss]
		public int ktb_mspkm { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationnumber { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xjp_vehicleusercityid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_vehiclepurchasercontactidname { get; set; }

		[AntiXss]
		public string xjp_automobilecategoryname { get; set; }

		[AntiXss]
		public string ktb_vehiclebrand { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid xjp_insurancecategoryid { get; set; }

		[AntiXss]
		public string ktb_dealercodednet { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public DateTime ktb_validuntil { get; set; }

		[AntiXss]
		public string ktb_solddealerid { get; set; }

		[AntiXss]
		public string xts_modelspecificationnumber { get; set; }

		[AntiXss]
		public decimal xts_height { get; set; }

		[AntiXss]
		public Guid xjp_vehicleuservillageandstreetid { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public int xts_mileagebeforeexchanged { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string ktb_typecaroseries { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string ktb_statusintfchassispktcreatednetname { get; set; }

		[AntiXss]
		public string xjp_vehiclepurposeidname { get; set; }

		[AntiXss]
		public string xjp_lastrecyclingissuecategoryname { get; set; }

		[AntiXss]
		public Guid xjp_vehicleownervillageandstreetid { get; set; }

		[AntiXss]
		public DateTime xts_registrationdate { get; set; }

		[AntiXss]
		public decimal xjp_maximumload2 { get; set; }

		[AntiXss]
		public string ktb_transmission { get; set; }

		[AntiXss]
		public DateTime xts_lastmileagedate { get; set; }

		[AntiXss]
		public string ktb_solddealerdesc { get; set; }

		[AntiXss]
		public int xts_rearorrearaxleweight { get; set; }

		[AntiXss]
		public Guid xts_maintenancemodelid { get; set; }

		[AntiXss]
		public string xjp_vehicleuserbusinessphone { get; set; }

		[AntiXss]
		public Guid xts_issuedestinationid { get; set; }

		[AntiXss]
		public string xts_lastusedvehicleorderidname { get; set; }

		[AntiXss]
		public Guid xts_vehicleownercontactid { get; set; }

		[AntiXss]
		public DateTime xjp_compulsoryinsuranceexpirationdate { get; set; }

		[AntiXss]
		public string xts_chassismodel { get; set; }

		[AntiXss]
		public string xjp_usageidname { get; set; }

		[AntiXss]
		public string xjp_usecategoryname { get; set; }

		[AntiXss]
		public string xts_exteriorcolor { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_vehicleusercontactidyominame { get; set; }

		[AntiXss]
		public DateTime xts_lastnextestimatedservicedate { get; set; }

		[AntiXss]
		public DateTime xts_customerreceiptdate { get; set; }

		[AntiXss]
		public string ktb_statusintfchassispktcreatednet { get; set; }

		[AntiXss]
		public Guid xts_lastservicemmsid { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string xjp_lightvehiclecategory { get; set; }

		[AntiXss]
		public Guid xts_productinteriorcolorid { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xjp_runningmeterexchangename { get; set; }

		[AntiXss]
		public string ktb_drivesystem { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xjp_grossweight1kg { get; set; }

		[AntiXss]
		public int xts_warrantymileage { get; set; }

		[AntiXss]
		public string xjp_automobiletaxidname { get; set; }

		[AntiXss]
		public string xts_lastvehicleordernumberlookupname { get; set; }

		[AntiXss]
		public Guid xts_vehicleusercontactid { get; set; }

		[AntiXss]
		public string xts_styleidname { get; set; }

		[AntiXss]
		public string xjp_registrationmodel { get; set; }

		[AntiXss]
		public string xts_shapeidname { get; set; }

		[AntiXss]
		public string ktb_leveldataname { get; set; }

		[AntiXss]
		public string xts_vehicleownercontactidyominame { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public Guid xts_lastusedvehicleorderid { get; set; }

		[AntiXss]
		public DateTime xjp_vehicleinspectionexpirationdate { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xjp_infantcategoryname { get; set; }

		[AntiXss]
		public int xts_frontorrearaxleweight { get; set; }

		[AntiXss]
		public decimal xjp_maximumload1 { get; set; }

		[AntiXss]
		public decimal xts_enginevolume { get; set; }

		[AntiXss]
		public string xjp_authorizedvehicletypeemissioncontrol { get; set; }

		[AntiXss]
		public decimal xjp_weighttaxamount { get; set; }

		[AntiXss]
		public string xts_vehiclepayercustomeridname { get; set; }

		[AntiXss]
		public string xts_vehicleusernumber { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public string xts_interiorcolor { get; set; }

		[AntiXss]
		public string xts_enginevolumeunit { get; set; }

		[AntiXss]
		public string xts_classificationnumber { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_vehiclepurchasercustomeridname { get; set; }

		[AntiXss]
		public string ktb_kodefleet { get; set; }

		[AntiXss]
		public string xjp_compulsoryinsuranceexpirationdatejapan { get; set; }

		[AntiXss]
		public Guid ktb_membershipvincommonid { get; set; }

		[AntiXss]
		public string xts_vehiclepublicidname { get; set; }

		[AntiXss]
		public string xjp_runningmeterexchange { get; set; }

		[AntiXss]
		public Guid xts_vehiclepayercontactid { get; set; }

		[AntiXss]
		public string xts_financingcompanyidyominame { get; set; }

		[AntiXss]
		public bool xts_servicepackagecontractavailable { get; set; }

		[AntiXss]
		public string ktb_mspnumber { get; set; }

		[AntiXss]
		public string xts_configurationidname { get; set; }

		[AntiXss]
		public string xts_keynumber { get; set; }

		[AntiXss]
		public string xts_servicebusinessunitidname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_vehiclepayercontactidyominame { get; set; }

		[AntiXss]
		public bool xjp_infantcategory { get; set; }

		[AntiXss]
		public string ktb_membershipvincommonidname { get; set; }

		[AntiXss]
		public string ktb_typecaroseriesname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_vehiclename { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public string xts_vehicleusercustomeridyominame { get; set; }

		[AntiXss]
		public string xts_vehicletypename { get; set; }

		[AntiXss]
		public string xjp_vehicleownerpostalcode { get; set; }

		[AntiXss]
		public Guid xjp_vehicleuserprovinceid { get; set; }

		[AntiXss]
		public string xjp_weighttaxidname { get; set; }

		[AntiXss]
		public decimal xjp_automobiletaxamount_base { get; set; }

		[AntiXss]
		public string xts_maintenancemodelidname { get; set; }

		[AntiXss]
		public string xts_vehiclemodelidname { get; set; }

		[AntiXss]
		public decimal xts_acquisitionprice { get; set; }

		[AntiXss]
		public Guid xjp_recognizedmodelid { get; set; }

		[AntiXss]
		public string xts_issuedestinationidname { get; set; }

		[AntiXss]
		public string xjp_vehicleinspectionperiodname { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public string xjp_vehicleownerbusinessphone { get; set; }

		[AntiXss]
		public Guid xts_servicebusinessunitid { get; set; }

		[AntiXss]
		public string ktb_leveldata { get; set; }

		[AntiXss]
		public string xjp_registrationofficeidname { get; set; }

		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		[AntiXss]
		public string xts_originalstoreidname { get; set; }

		[AntiXss]
		public string xjp_lightvehiclecategoryname { get; set; }

		[AntiXss]
		public string xts_productsegment4idname { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xjp_recyclingobjectname { get; set; }

		[AntiXss]
		public string xts_vehiclepurchaserlookupname { get; set; }

		[AntiXss]
		public string xts_vehicleownercontactidname { get; set; }

		[AntiXss]
		public string xts_lastservicemmsidname { get; set; }

		[AntiXss]
		public decimal xjp_compulsoryinsuranceamount_base { get; set; }

		[AntiXss]
		public string xts_vehicleownercustomeridname { get; set; }

		[AntiXss]
		public Guid xjp_platenumbersegment1id { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_productionyear { get; set; }

		[AntiXss]
		public decimal xjp_totalrecyclingpredeposit_base { get; set; }

		[AntiXss]
		public string xjp_vehicleowneraddress4 { get; set; }

		[AntiXss]
		public string xts_financingcompanyidname { get; set; }

		[AntiXss]
		public string xto_genericnumber { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
