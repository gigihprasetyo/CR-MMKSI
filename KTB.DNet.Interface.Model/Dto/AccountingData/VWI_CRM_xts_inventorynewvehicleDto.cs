#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventorynewvehicleDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:57
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_inventorynewvehicleDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_lastvehicleorderidname { get; set; }

		public Guid xts_lastvehicleorderid { get; set; }

		public string xjp_nitrogenoxidesunit { get; set; }

		public string xts_vehicleexteriorcoloridname { get; set; }

		public decimal xts_installedaccessories { get; set; }

		public string xjp_lightvehiclecategoryname { get; set; }

		public DateTime modifiedon { get; set; }

		public DateTime xjp_completeinspectionissuedate1 { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public DateTime xts_receivingdate { get; set; }

		public Guid xjp_stocknumberreferenceid { get; set; }

		public string statuscodename { get; set; }

		public decimal xts_width { get; set; }

		public bool xts_receivingprocess { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xjp_lastrecyclingissuecategoryname { get; set; }

		public Guid xjp_bodyshapeid { get; set; }

		public string xjp_shipmentdestination { get; set; }

		public DateTime xjp_scheduledarrivaldate { get; set; }

		public decimal xts_purchasediscount { get; set; }

		public Guid xts_vehicleinteriorcolorid { get; set; }

		public string xts_mainfueltypeidname { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xjp_recyclingticketnumber { get; set; }

		public string xts_chassismodel { get; set; }

		public string xts_departmentidname { get; set; }

		public string ktb_iskaroseriname { get; set; }

		public string xjp_nitrogenoxides { get; set; }

		public decimal xts_totallandedcost { get; set; }

		public bool ktb_beginingbalance { get; set; }

		public decimal xts_totalvalue { get; set; }

		public decimal ktb_caroseriesamount_base { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string modifiedbyname { get; set; }

		public DateTime xjp_lastrecyclingdepositdate { get; set; }

		public Guid xts_inventorynewvehicleid { get; set; }

		public Guid xts_locationid { get; set; }

		public string xts_keynumber { get; set; }

		public int statuscode { get; set; }

		public string xts_productdescription { get; set; }

		public string xts_warehouseidname { get; set; }

		public DateTime xjp_completeinspectionexpirydate2 { get; set; }

		public string ktb_vehiclecategoryidname { get; set; }

		public string xjp_particulatematerialunit { get; set; }

		public bool xjp_suitabilityforabusiness { get; set; }

		public string xts_remarks { get; set; }

		public string xts_configurationidname { get; set; }

		public decimal xts_totallandedcost_base { get; set; }

		public decimal xjp_vehicleconsumptiontaxamount_base { get; set; }

		public Guid xts_vehicleexteriorcolorid { get; set; }

		public int xts_frontfrontaxleweight { get; set; }

		public Guid xts_styleid { get; set; }

		public string statecodename { get; set; }

		public decimal xts_enginevolume { get; set; }

		public string xts_secondfueltypeidname { get; set; }

		public string xjp_registrationdatejapan { get; set; }

		public decimal xts_installedaccessories_base { get; set; }

		public string ktb_beginingbalancename { get; set; }

		public Guid xts_vehicleidentificationid { get; set; }

		public decimal xts_purchasediscount_base { get; set; }

		public Guid xts_reasoncodeid { get; set; }

		public DateTime createdon { get; set; }

		public string xts_classificationnumber { get; set; }

		public DateTime xjp_confirmedarrivalscheduledate { get; set; }

		public string xjp_lastrecyclingissuecategory { get; set; }

		public decimal xjp_grossweight2 { get; set; }

		public Guid ktb_vehiclecategoryid { get; set; }

		public string xjp_completeinspectioncertificatenumber2 { get; set; }

		public DateTime ktb_purchaseorderdate { get; set; }

		public string ktb_dealeralokasiidname { get; set; }

		public Guid createdonbehalfby { get; set; }

		public decimal ktb_caroseriesamount { get; set; }

		public string xjp_registrationmodel { get; set; }

		public string xts_vehiclespecificationidname { get; set; }

		public string xts_vehicleidentificationidname { get; set; }

		public Guid xts_serialid { get; set; }

		public Guid xts_productid { get; set; }

		public string owneridyominame { get; set; }

		public Guid modifiedby { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public decimal xjp_maximumload1 { get; set; }

		public string ktb_vehiclecolorname { get; set; }

		public decimal xjp_variousconsumptiontaxamount { get; set; }

		public Guid xts_mainfueltypeid { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public Guid xts_madeinid { get; set; }

		public string xjp_suitabilityforbusiness { get; set; }

		public string xjp_purchasecategoryname { get; set; }

		public string ktb_vehicledescription { get; set; }

		public Guid owningteam { get; set; }

		public decimal xts_totalvalue_base { get; set; }

		public DateTime xts_scheduledshipmentdate { get; set; }

		public decimal xts_serviceandcostinspection_base { get; set; }

		public int xts_rearrearaxleweight { get; set; }

		public Guid ktb_accessoriesid { get; set; }

		public DateTime xjp_actualreceivedate { get; set; }

		public Guid owninguser { get; set; }

		public Guid xts_departmentid { get; set; }

		public string xjp_bodyworkclassificationcategoryname { get; set; }

		public Guid xjp_purchaseconsumptiontaxid { get; set; }

		public decimal xts_height { get; set; }

		public string xjp_stockaddress { get; set; }

		public string xts_visibility { get; set; }

		public string owneridtype { get; set; }

		public string xts_enginenumber { get; set; }

		public DateTime xjp_preliminaryinspectiondate { get; set; }

		public Guid ktb_dealeralokasiid { get; set; }

		public DateTime xjp_pocancellationdatelimit { get; set; }

		public string xts_chassisnumber { get; set; }

		public string xts_productionyear { get; set; }

		public Guid xjp_lastinitialassessmentid { get; set; }

		public string owneridname { get; set; }

		public bool ktb_iskaroseri { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string xjp_enginemodel { get; set; }

		public Guid ktb_vehiclemodelid { get; set; }

		public string xts_generation { get; set; }

		public string xjp_lastinitialassessmentidname { get; set; }

		public string xts_stocknumber { get; set; }

		public string xjp_emissionpreventiondevice { get; set; }

		public decimal xts_purchaseprice_base { get; set; }

		public string ktb_vendoridname { get; set; }

		public Guid xts_siteid { get; set; }

		public decimal exchangerate { get; set; }

		public Guid xts_vehiclespecificationid { get; set; }

		public string xts_productfamily { get; set; }

		public int xjp_capacity2 { get; set; }

		public int xjp_capacity1 { get; set; }

		public DateTime xjp_completeinspectionissuedate2 { get; set; }

		public decimal xts_weight { get; set; }

		public string xjp_idempotentmessage { get; set; }

		public decimal xts_length { get; set; }

		public int xts_frontrearaxleweight { get; set; }

		public string xts_productidname { get; set; }

		public decimal xjp_maximumload2 { get; set; }

		public Guid xts_configurationid { get; set; }

		public string xts_dmsreferencenumber { get; set; }

		public Guid xts_warehouseid { get; set; }

		public string ktb_accessoriesidname { get; set; }

		public Guid ownerid { get; set; }

		public decimal xts_serviceandcostinspection { get; set; }

		public string xjp_cartype { get; set; }

		public Guid ktb_vendorid { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Guid xts_frombusinessunitid { get; set; }

		public string xjp_particulatematerial { get; set; }

		public string xts_ribbondata { get; set; }

		public int xts_rearfrontaxleweight { get; set; }

		public Guid xts_tobusinessunitid { get; set; }

		public string createdbyname { get; set; }

		public string xjp_purchasecategory { get; set; }

		public string xts_businessunitidname { get; set; }

		public int statecode { get; set; }

		public string xts_siteidname { get; set; }

		public string xts_locationidname { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid createdby { get; set; }

		public string xts_serialidname { get; set; }

		public int xjp_numberofsideairbag { get; set; }

		public string xjp_bodyworkclassificationcategory { get; set; }

		public string xjp_enginetype { get; set; }

		public string xjp_suitabilityforabusinessname { get; set; }

		public decimal xjp_preliminaryinspectionexpense_base { get; set; }

		public string xts_referencenumber { get; set; }

		public string xjp_bodyshapeidname { get; set; }

		public string xts_vehicleinteriorcoloridname { get; set; }

		public string xjp_newvehiclesalesorderidname { get; set; }

		public string xjp_enginetypename { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public decimal xjp_preliminaryinspectionexpense { get; set; }

		public Guid xts_secondfueltypeid { get; set; }

		public string xts_receivingprocessname { get; set; }

		public Guid xjp_newvehiclesalesorderid { get; set; }

		public string ktb_applicationno { get; set; }

		public string xts_visibilityname { get; set; }

		public string xts_frombusinessunitidname { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_locking { get; set; }

		public string xts_styleidname { get; set; }

		public string xjp_purchaseconsumptiontaxidname { get; set; }

		public DateTime xjp_shipmentdate { get; set; }

		public decimal xjp_totalconsumptiontaxamount_base { get; set; }

		public string xjp_shipmentplace { get; set; }

		public decimal xjp_vehicleconsumptiontaxamount { get; set; }

		public Int64 versionnumber { get; set; }

		public string ktb_vehiclemodelidname { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_modelspecificationnumber { get; set; }

		public decimal xjp_variousconsumptiontaxamount_base { get; set; }

		public DateTime xts_issueddate { get; set; }

		public decimal xjp_totalconsumptiontaxamount { get; set; }

		public decimal xjp_grossweight1 { get; set; }

		public DateTime xjp_completeinspectionexpirydate { get; set; }

		public string xjp_completeinspectioncertificatenumber1 { get; set; }

		public string xts_tobusinessunitidname { get; set; }

		public DateTime xjp_completeinspectiondate { get; set; }

		public string xts_madeinidname { get; set; }

		public string createdbyyominame { get; set; }

		public Guid xjp_usageid { get; set; }

		public bool xjp_lightvehiclecategory { get; set; }

		public string xjp_suitabilityforbusinessname { get; set; }

		public decimal xts_purchaseprice { get; set; }

		public string xjp_stocknumberreferenceidname { get; set; }

		public string xjp_usageidname { get; set; }

		public string xjp_productionplant { get; set; }

		public string xts_reasoncodeidname { get; set; }

		public string ktb_description { get; set; }

		public DateTime ktb_atd { get; set; }

		public DateTime ktb_etd { get; set; }

		public DateTime ktb_eta { get; set; }

		public string ktb_statusinterfacednetname { get; set; }

		public string ktb_statusinterfacednet { get; set; }

		public string ktb_chassisexternalname { get; set; }

		public string ktb_interfaceexceptionmessage { get; set; }

		public bool ktb_chassisexternal { get; set; }

		public DateTime ktb_fakturdate { get; set; }

		public string ktb_revisionstatusname { get; set; }

		public DateTime ktb_validationdate { get; set; }

		public DateTime ktb_printeddate { get; set; }

		public string ktb_revisionstatus { get; set; }

		public string ktb_fakturnumber { get; set; }

		public DateTime ktb_confirmdate { get; set; }

		public string ktb_fakturstatusname { get; set; }

		public string ktb_fakturstatus { get; set; }

		public string ktb_kondisikendaraan { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
