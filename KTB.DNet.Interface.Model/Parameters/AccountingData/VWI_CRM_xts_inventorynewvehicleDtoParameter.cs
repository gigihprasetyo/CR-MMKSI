#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventorynewvehicleParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_inventorynewvehicleParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_lastvehicleorderidname { get; set; }

		[AntiXss]
		public Guid xts_lastvehicleorderid { get; set; }

		[AntiXss]
		public string xjp_nitrogenoxidesunit { get; set; }

		[AntiXss]
		public string xts_vehicleexteriorcoloridname { get; set; }

		[AntiXss]
		public decimal xts_installedaccessories { get; set; }

		[AntiXss]
		public string xjp_lightvehiclecategoryname { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public DateTime xjp_completeinspectionissuedate1 { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public DateTime xts_receivingdate { get; set; }

		[AntiXss]
		public Guid xjp_stocknumberreferenceid { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public decimal xts_width { get; set; }

		[AntiXss]
		public bool xts_receivingprocess { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xjp_lastrecyclingissuecategoryname { get; set; }

		[AntiXss]
		public Guid xjp_bodyshapeid { get; set; }

		[AntiXss]
		public string xjp_shipmentdestination { get; set; }

		[AntiXss]
		public DateTime xjp_scheduledarrivaldate { get; set; }

		[AntiXss]
		public decimal xts_purchasediscount { get; set; }

		[AntiXss]
		public Guid xts_vehicleinteriorcolorid { get; set; }

		[AntiXss]
		public string xts_mainfueltypeidname { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xjp_recyclingticketnumber { get; set; }

		[AntiXss]
		public string xts_chassismodel { get; set; }

		[AntiXss]
		public string xts_departmentidname { get; set; }

		[AntiXss]
		public string ktb_iskaroseriname { get; set; }

		[AntiXss]
		public string xjp_nitrogenoxides { get; set; }

		[AntiXss]
		public decimal xts_totallandedcost { get; set; }

		[AntiXss]
		public bool ktb_beginingbalance { get; set; }

		[AntiXss]
		public decimal xts_totalvalue { get; set; }

		[AntiXss]
		public decimal ktb_caroseriesamount_base { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public DateTime xjp_lastrecyclingdepositdate { get; set; }

		[AntiXss]
		public Guid xts_inventorynewvehicleid { get; set; }

		[AntiXss]
		public Guid xts_locationid { get; set; }

		[AntiXss]
		public string xts_keynumber { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public DateTime xjp_completeinspectionexpirydate2 { get; set; }

		[AntiXss]
		public string ktb_vehiclecategoryidname { get; set; }

		[AntiXss]
		public string xjp_particulatematerialunit { get; set; }

		[AntiXss]
		public bool xjp_suitabilityforabusiness { get; set; }

		[AntiXss]
		public string xts_remarks { get; set; }

		[AntiXss]
		public string xts_configurationidname { get; set; }

		[AntiXss]
		public decimal xts_totallandedcost_base { get; set; }

		[AntiXss]
		public decimal xjp_vehicleconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public Guid xts_vehicleexteriorcolorid { get; set; }

		[AntiXss]
		public int xts_frontfrontaxleweight { get; set; }

		[AntiXss]
		public Guid xts_styleid { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xts_enginevolume { get; set; }

		[AntiXss]
		public string xts_secondfueltypeidname { get; set; }

		[AntiXss]
		public string xjp_registrationdatejapan { get; set; }

		[AntiXss]
		public decimal xts_installedaccessories_base { get; set; }

		[AntiXss]
		public string ktb_beginingbalancename { get; set; }

		[AntiXss]
		public Guid xts_vehicleidentificationid { get; set; }

		[AntiXss]
		public decimal xts_purchasediscount_base { get; set; }

		[AntiXss]
		public Guid xts_reasoncodeid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_classificationnumber { get; set; }

		[AntiXss]
		public DateTime xjp_confirmedarrivalscheduledate { get; set; }

		[AntiXss]
		public string xjp_lastrecyclingissuecategory { get; set; }

		[AntiXss]
		public decimal xjp_grossweight2 { get; set; }

		[AntiXss]
		public Guid ktb_vehiclecategoryid { get; set; }

		[AntiXss]
		public string xjp_completeinspectioncertificatenumber2 { get; set; }

		[AntiXss]
		public DateTime ktb_purchaseorderdate { get; set; }

		[AntiXss]
		public string ktb_dealeralokasiidname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public decimal ktb_caroseriesamount { get; set; }

		[AntiXss]
		public string xjp_registrationmodel { get; set; }

		[AntiXss]
		public string xts_vehiclespecificationidname { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationidname { get; set; }

		[AntiXss]
		public Guid xts_serialid { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal xjp_maximumload1 { get; set; }

		[AntiXss]
		public string ktb_vehiclecolorname { get; set; }

		[AntiXss]
		public decimal xjp_variousconsumptiontaxamount { get; set; }

		[AntiXss]
		public Guid xts_mainfueltypeid { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_madeinid { get; set; }

		[AntiXss]
		public string xjp_suitabilityforbusiness { get; set; }

		[AntiXss]
		public string xjp_purchasecategoryname { get; set; }

		[AntiXss]
		public string ktb_vehicledescription { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xts_totalvalue_base { get; set; }

		[AntiXss]
		public DateTime xts_scheduledshipmentdate { get; set; }

		[AntiXss]
		public decimal xts_serviceandcostinspection_base { get; set; }

		[AntiXss]
		public int xts_rearrearaxleweight { get; set; }

		[AntiXss]
		public Guid ktb_accessoriesid { get; set; }

		[AntiXss]
		public DateTime xjp_actualreceivedate { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid xts_departmentid { get; set; }

		[AntiXss]
		public string xjp_bodyworkclassificationcategoryname { get; set; }

		[AntiXss]
		public Guid xjp_purchaseconsumptiontaxid { get; set; }

		[AntiXss]
		public decimal xts_height { get; set; }

		[AntiXss]
		public string xjp_stockaddress { get; set; }

		[AntiXss]
		public string xts_visibility { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xts_enginenumber { get; set; }

		[AntiXss]
		public DateTime xjp_preliminaryinspectiondate { get; set; }

		[AntiXss]
		public Guid ktb_dealeralokasiid { get; set; }

		[AntiXss]
		public DateTime xjp_pocancellationdatelimit { get; set; }

		[AntiXss]
		public string xts_chassisnumber { get; set; }

		[AntiXss]
		public string xts_productionyear { get; set; }

		[AntiXss]
		public Guid xjp_lastinitialassessmentid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public bool ktb_iskaroseri { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xjp_enginemodel { get; set; }

		[AntiXss]
		public Guid ktb_vehiclemodelid { get; set; }

		[AntiXss]
		public string xts_generation { get; set; }

		[AntiXss]
		public string xjp_lastinitialassessmentidname { get; set; }

		[AntiXss]
		public string xts_stocknumber { get; set; }

		[AntiXss]
		public string xjp_emissionpreventiondevice { get; set; }

		[AntiXss]
		public decimal xts_purchaseprice_base { get; set; }

		[AntiXss]
		public string ktb_vendoridname { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid xts_vehiclespecificationid { get; set; }

		[AntiXss]
		public string xts_productfamily { get; set; }

		[AntiXss]
		public int xjp_capacity2 { get; set; }

		[AntiXss]
		public int xjp_capacity1 { get; set; }

		[AntiXss]
		public DateTime xjp_completeinspectionissuedate2 { get; set; }

		[AntiXss]
		public decimal xts_weight { get; set; }

		[AntiXss]
		public string xjp_idempotentmessage { get; set; }

		[AntiXss]
		public decimal xts_length { get; set; }

		[AntiXss]
		public int xts_frontrearaxleweight { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public decimal xjp_maximumload2 { get; set; }

		[AntiXss]
		public Guid xts_configurationid { get; set; }

		[AntiXss]
		public string xts_dmsreferencenumber { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public string ktb_accessoriesidname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public decimal xts_serviceandcostinspection { get; set; }

		[AntiXss]
		public string xjp_cartype { get; set; }

		[AntiXss]
		public Guid ktb_vendorid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xts_frombusinessunitid { get; set; }

		[AntiXss]
		public string xjp_particulatematerial { get; set; }

		[AntiXss]
		public string xts_ribbondata { get; set; }

		[AntiXss]
		public int xts_rearfrontaxleweight { get; set; }

		[AntiXss]
		public Guid xts_tobusinessunitid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xjp_purchasecategory { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public string xts_locationidname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xts_serialidname { get; set; }

		[AntiXss]
		public int xjp_numberofsideairbag { get; set; }

		[AntiXss]
		public string xjp_bodyworkclassificationcategory { get; set; }

		[AntiXss]
		public string xjp_enginetype { get; set; }

		[AntiXss]
		public string xjp_suitabilityforabusinessname { get; set; }

		[AntiXss]
		public decimal xjp_preliminaryinspectionexpense_base { get; set; }

		[AntiXss]
		public string xts_referencenumber { get; set; }

		[AntiXss]
		public string xjp_bodyshapeidname { get; set; }

		[AntiXss]
		public string xts_vehicleinteriorcoloridname { get; set; }

		[AntiXss]
		public string xjp_newvehiclesalesorderidname { get; set; }

		[AntiXss]
		public string xjp_enginetypename { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public decimal xjp_preliminaryinspectionexpense { get; set; }

		[AntiXss]
		public Guid xts_secondfueltypeid { get; set; }

		[AntiXss]
		public string xts_receivingprocessname { get; set; }

		[AntiXss]
		public Guid xjp_newvehiclesalesorderid { get; set; }

		[AntiXss]
		public string ktb_applicationno { get; set; }

		[AntiXss]
		public string xts_visibilityname { get; set; }

		[AntiXss]
		public string xts_frombusinessunitidname { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_styleidname { get; set; }

		[AntiXss]
		public string xjp_purchaseconsumptiontaxidname { get; set; }

		[AntiXss]
		public DateTime xjp_shipmentdate { get; set; }

		[AntiXss]
		public decimal xjp_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public string xjp_shipmentplace { get; set; }

		[AntiXss]
		public decimal xjp_vehicleconsumptiontaxamount { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string ktb_vehiclemodelidname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string xts_modelspecificationnumber { get; set; }

		[AntiXss]
		public decimal xjp_variousconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public DateTime xts_issueddate { get; set; }

		[AntiXss]
		public decimal xjp_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public decimal xjp_grossweight1 { get; set; }

		[AntiXss]
		public DateTime xjp_completeinspectionexpirydate { get; set; }

		[AntiXss]
		public string xjp_completeinspectioncertificatenumber1 { get; set; }

		[AntiXss]
		public string xts_tobusinessunitidname { get; set; }

		[AntiXss]
		public DateTime xjp_completeinspectiondate { get; set; }

		[AntiXss]
		public string xts_madeinidname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid xjp_usageid { get; set; }

		[AntiXss]
		public bool xjp_lightvehiclecategory { get; set; }

		[AntiXss]
		public string xjp_suitabilityforbusinessname { get; set; }

		[AntiXss]
		public decimal xts_purchaseprice { get; set; }

		[AntiXss]
		public string xjp_stocknumberreferenceidname { get; set; }

		[AntiXss]
		public string xjp_usageidname { get; set; }

		[AntiXss]
		public string xjp_productionplant { get; set; }

		[AntiXss]
		public string xts_reasoncodeidname { get; set; }

		[AntiXss]
		public string ktb_description { get; set; }

		[AntiXss]
		public DateTime ktb_atd { get; set; }

		[AntiXss]
		public DateTime ktb_etd { get; set; }

		[AntiXss]
		public DateTime ktb_eta { get; set; }

		[AntiXss]
		public string ktb_statusinterfacednetname { get; set; }

		[AntiXss]
		public string ktb_statusinterfacednet { get; set; }

		[AntiXss]
		public string ktb_chassisexternalname { get; set; }

		[AntiXss]
		public string ktb_interfaceexceptionmessage { get; set; }

		[AntiXss]
		public bool ktb_chassisexternal { get; set; }

		[AntiXss]
		public DateTime ktb_fakturdate { get; set; }

		[AntiXss]
		public string ktb_revisionstatusname { get; set; }

		[AntiXss]
		public DateTime ktb_validationdate { get; set; }

		[AntiXss]
		public DateTime ktb_printeddate { get; set; }

		[AntiXss]
		public string ktb_revisionstatus { get; set; }

		[AntiXss]
		public string ktb_fakturnumber { get; set; }

		[AntiXss]
		public DateTime ktb_confirmdate { get; set; }

		[AntiXss]
		public string ktb_fakturstatusname { get; set; }

		[AntiXss]
		public string ktb_fakturstatus { get; set; }

		[AntiXss]
		public string ktb_kondisikendaraan { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
