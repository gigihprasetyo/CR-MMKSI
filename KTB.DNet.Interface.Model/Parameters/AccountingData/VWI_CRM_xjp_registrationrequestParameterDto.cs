#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_registrationrequestParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 14:42:00
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
    public class VWI_CRM_xjp_registrationrequestParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public string xjp_requestingbusinessunitidname { get; set; }
        [AntiXss]
        public bool xjp_endoflifevehicle { get; set; }
        [AntiXss]
        public string xjp_parkingspacecertificationcollectedby { get; set; }
        [AntiXss]
        public string xjp_stockstoreidname { get; set; }
        [AntiXss]
        public string xjp_garageaddresssamewithusername { get; set; }
        [AntiXss]
        public string xjp_agencyautomobiletaxpaymentname { get; set; }
        [AntiXss]
        public string xjp_othercarkatakana { get; set; }
        [AntiXss]
        public Guid xjp_garagecityid { get; set; }
        [AntiXss]
        public DateTime xjp_cardeliverydesireddate { get; set; }
        [AntiXss]
        public string xjp_lightcharacterplatetypename { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string xjp_usertype { get; set; }
        [AntiXss]
        public Guid xjp_stockinventorynewvehicleid { get; set; }
        [AntiXss]
        public string xjp_parentbusinessunitdescription { get; set; }
        [AntiXss]
        public Guid xjp_usercityid { get; set; }
        [AntiXss]
        public string xjp_pattern { get; set; }
        [AntiXss]
        public decimal xjp_actualautomobileacquisitiontaxamount_base { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string xjp_owneridname { get; set; }
        [AntiXss]
        public string xjp_productionyear { get; set; }
        [AntiXss]
        public string xjp_registrationtaxcategory { get; set; }
        [AntiXss]
        public string xjp_registrationcategoryname { get; set; }
        [AntiXss]
        public Guid xjp_stockstoreid { get; set; }
        [AntiXss]
        public string xjp_userdescription { get; set; }
        [AntiXss]
        public string xjp_whereaboutsproof { get; set; }
        [AntiXss]
        public string xjp_taxdeclarationbook { get; set; }
        [AntiXss]
        public DateTime xjp_compulsoryinsuranceapplicationdate { get; set; }
        [AntiXss]
        public Guid xjp_receivingbusinessunitid { get; set; }
        [AntiXss]
        public string xjp_vehiclespecificationidname { get; set; }
        [AntiXss]
        public string xjp_garageaddress2 { get; set; }
        [AntiXss]
        public string xjp_leasedocument { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string xjp_othercarnumber { get; set; }
        [AntiXss]
        public string xjp_businessunitidname { get; set; }
        [AntiXss]
        public string xjp_customeridyominame { get; set; }
        [AntiXss]
        public string xjp_registrationpurposename { get; set; }
        [AntiXss]
        public string xjp_completeinspectionexpiredname { get; set; }
        [AntiXss]
        public string xjp_agencyacquisitiontaxpaymentname { get; set; }
        [AntiXss]
        public string xjp_applicationregistrationofficeidname { get; set; }
        [AntiXss]
        public string xjp_endoflifevehiclename { get; set; }
        [AntiXss]
        public string xjp_garageapplicationsamewithusername { get; set; }
        [AntiXss]
        public string xjp_ownerzipcode { get; set; }
        [AntiXss]
        public decimal xjp_taxablemisccharge_base { get; set; }
        [AntiXss]
        public string xjp_ownercityidname { get; set; }
        [AntiXss]
        public string xjp_garageapplicationvillageandstreetidname { get; set; }
        [AntiXss]
        public decimal xjp_automobiletaxamount { get; set; }
        [AntiXss]
        public string xjp_othercarclassification { get; set; }
        [AntiXss]
        public Guid xjp_garageapplicationvillageandstreetid { get; set; }
        [AntiXss]
        public Guid xjp_vehiclespecificationid { get; set; }
        [AntiXss]
        public string xjp_ownernumber { get; set; }
        [AntiXss]
        public string xjp_garageprovinceidname { get; set; }
        [AntiXss]
        public string xjp_vehiclemodelidname { get; set; }
        [AntiXss]
        public string xjp_otherpartyregistrationtypename { get; set; }
        [AntiXss]
        public Guid xjp_registrationresultofficeid { get; set; }
        [AntiXss]
        public string xjp_handlingname { get; set; }
        [AntiXss]
        public string xjp_otherpartyregistrationtype { get; set; }
        [AntiXss]
        public string xjp_patternname { get; set; }
        [AntiXss]
        public string xjp_garageapplicationsamewithuser { get; set; }
        [AntiXss]
        public Guid xjp_vehiclemodelid { get; set; }
        [AntiXss]
        public Guid xjp_creditcompanyid { get; set; }
        [AntiXss]
        public string xjp_currentplatenumber { get; set; }
        [AntiXss]
        public decimal xjp_miscellaneouschargeconsumptiontax_base { get; set; }
        [AntiXss]
        public decimal xjp_vehicleconsumptiontax { get; set; }
        [AntiXss]
        public DateTime xjp_documentcompletiondate { get; set; }
        [AntiXss]
        public string xjp_requiredservice { get; set; }
        [AntiXss]
        public string xjp_remodelingname { get; set; }
        [AntiXss]
        public Guid xjp_customerid { get; set; }
        [AntiXss]
        public bool xjp_peoplewithdisabilities { get; set; }
        [AntiXss]
        public Guid xjp_personinchargeid { get; set; }
        [AntiXss]
        public Guid xjp_paymentmethodid { get; set; }
        [AntiXss]
        public bool xjp_requestplatenumber { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public bool xjp_garageapplicationrequired { get; set; }
        [AntiXss]
        public string xjp_salesordernumber { get; set; }
        [AntiXss]
        public decimal xjp_vehicleconsumptiontax_base { get; set; }
        [AntiXss]
        public string xjp_residentscard { get; set; }
        [AntiXss]
        public Guid xjp_othercarregistrationofficeid { get; set; }
        [AntiXss]
        public string xjp_parentbusinessunitidname { get; set; }
        [AntiXss]
        public decimal xjp_grandtotal { get; set; }
        [AntiXss]
        public string xjp_stocknumberlookupname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string xjp_userrepresentative { get; set; }
        [AntiXss]
        public DateTime xjp_documentexpireddate { get; set; }
        [AntiXss]
        public string xjp_stockinventorynewvehicleidname { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public string xjp_garageaddress1 { get; set; }
        [AntiXss]
        public string xjp_ownerdescription { get; set; }
        [AntiXss]
        public string xjp_insurancecompanyidname { get; set; }
        [AntiXss]
        public Guid xjp_uservillageandaddressid { get; set; }
        [AntiXss]
        public decimal xjp_weighttaxamount_base { get; set; }
        [AntiXss]
        public Guid xjp_ownerprovinceid { get; set; }
        [AntiXss]
        public DateTime xjp_registrationreceiptdate { get; set; }
        [AntiXss]
        public string xjp_insurancecompanycode { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string xjp_garageaddress3 { get; set; }
        [AntiXss]
        public string xjp_userproxydocumentcollectedbyname { get; set; }
        [AntiXss]
        public string xjp_ownershipcategoryname { get; set; }
        [AntiXss]
        public DateTime xjp_vehicleinspectionexpirationdate { get; set; }
        [AntiXss]
        public string xjp_uservillageandaddressidname { get; set; }
        [AntiXss]
        public string xjp_usedvehicletaxreductioncategoryname { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsuranceapplymethod { get; set; }
        [AntiXss]
        public Guid xjp_productid { get; set; }
        [AntiXss]
        public decimal xjp_caliamount_base { get; set; }
        [AntiXss]
        public string xjp_requestedclassification { get; set; }
        [AntiXss]
        public Guid xjp_ownervillageandaddressid { get; set; }
        [AntiXss]
        public decimal xjp_accessoriesacquisitiontaxamount { get; set; }
        [AntiXss]
        public bool xjp_agencyautomobiletaxpayment { get; set; }
        [AntiXss]
        public string xjp_registrationmethodname { get; set; }
        [AntiXss]
        public string xjp_taxpayercategory { get; set; }
        [AntiXss]
        public decimal xjp_actualautomobileacquisitiontaxamount { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string xjp_requestedplatenumber { get; set; }
        [AntiXss]
        public string xjp_useraddress2 { get; set; }
        [AntiXss]
        public DateTime xjp_registrationscheduleddate { get; set; }
        [AntiXss]
        public int xjp_validperiod { get; set; }
        [AntiXss]
        public bool xjp_governmentoffice { get; set; }
        [AntiXss]
        public string xjp_referencenumber { get; set; }
        [AntiXss]
        public string xjp_customeridname { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public bool xjp_parkingplaceownersamewithuser { get; set; }
        [AntiXss]
        public string xjp_vehiclespecificnumber { get; set; }
        [AntiXss]
        public string xjp_garageaddresssamewithuser { get; set; }
        [AntiXss]
        public Guid xjp_userid { get; set; }
        [AntiXss]
        public string xjp_taxpayercategoryname { get; set; }
        [AntiXss]
        public string xjp_agencycalipaymentname { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public string xjp_registrationtype { get; set; }
        [AntiXss]
        public string xjp_agencyhandlingname { get; set; }
        [AntiXss]
        public decimal xjp_nontaxablemiscellaneouscharge { get; set; }
        [AntiXss]
        public string xjp_requestedpersonincharge { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string xjp_requestedbusinessunitidname { get; set; }
        [AntiXss]
        public DateTime xjp_vehicleinspectionexpireddate { get; set; }
        [AntiXss]
        public decimal xjp_miscellaneouschargetotal_base { get; set; }
        [AntiXss]
        public string xjp_parkingplaceownersamewithusername { get; set; }
        [AntiXss]
        public string xjp_height { get; set; }
        [AntiXss]
        public string xjp_companyvehiclename { get; set; }
        [AntiXss]
        public string xjp_userproxydocumentcollectedby { get; set; }
        [AntiXss]
        public bool xjp_companyvehicle { get; set; }
        [AntiXss]
        public string xjp_agencyweighttaxpaymentname { get; set; }
        [AntiXss]
        public decimal xjp_acquisitiontaxtotalamount_base { get; set; }
        [AntiXss]
        public string xjp_osscoloridname { get; set; }
        [AntiXss]
        public string xjp_lightvehiclename { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment1idname { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public string xjp_garageapplicationrequiredname { get; set; }
        [AntiXss]
        public decimal xjp_miscellaneouschargeconsumptiontax { get; set; }
        [AntiXss]
        public decimal xjp_accessoriesconsumptiontax_base { get; set; }
        [AntiXss]
        public Guid xjp_parkingplaceprovinceid { get; set; }
        [AntiXss]
        public DateTime xjp_registrationdesireddate { get; set; }
        [AntiXss]
        public bool xjp_agencyacquisitiontaxpayment { get; set; }
        [AntiXss]
        public string xjp_registrationcategory { get; set; }
        [AntiXss]
        public string xjp_registrationcardeliveryagencyname { get; set; }
        [AntiXss]
        public string xjp_others { get; set; }
        [AntiXss]
        public string xjp_garageaddress { get; set; }
        [AntiXss]
        public decimal xjp_actualweighttaxamount { get; set; }
        [AntiXss]
        public int xjp_insuranceperiod { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsurancecertificatenumber { get; set; }
        [AntiXss]
        public string xjp_physicallyhandicappedpersonpocketbook { get; set; }
        [AntiXss]
        public string xjp_registrationdocumentnumber { get; set; }
        [AntiXss]
        public decimal xjp_accessoriesacquisitiontaxamount_base { get; set; }
        [AntiXss]
        public string xjp_currentplatenumbersegment4 { get; set; }
        [AntiXss]
        public string xjp_userphone { get; set; }
        [AntiXss]
        public string xjp_ownershippatternname { get; set; }
        [AntiXss]
        public decimal xjp_taxablemisccharge { get; set; }
        [AntiXss]
        public string xjp_receivingbusinessunitidname { get; set; }
        [AntiXss]
        public string xjp_ownershipcategory { get; set; }
        [AntiXss]
        public string xjp_locking { get; set; }
        [AntiXss]
        public decimal xjp_nontaxablemiscellaneouscharge_base { get; set; }
        [AntiXss]
        public string xjp_ossregistrationstatus { get; set; }
        [AntiXss]
        public string xjp_useraddress4 { get; set; }
        [AntiXss]
        public decimal xjp_grandtotal_base { get; set; }
        [AntiXss]
        public string xjp_useraddress1 { get; set; }
        [AntiXss]
        public string xjp_parkingspacecertificationcollectedbyname { get; set; }
        [AntiXss]
        public Guid xjp_osscolorid { get; set; }
        [AntiXss]
        public string xjp_parkingplaceprovinceidname { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment4 { get; set; }
        [AntiXss]
        public string xjp_owneraddress4 { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string xjp_parkingplaceaddress { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment2 { get; set; }
        [AntiXss]
        public string xjp_owneraddress1 { get; set; }
        [AntiXss]
        public string xjp_owneraddress2 { get; set; }
        [AntiXss]
        public string xjp_owneraddress3 { get; set; }
        [AntiXss]
        public string xjp_garageaddress4 { get; set; }
        [AntiXss]
        public string xjp_usertypename { get; set; }
        [AntiXss]
        public Guid xjp_businessunitid { get; set; }
        [AntiXss]
        public string xjp_classificationnumber { get; set; }
        [AntiXss]
        public Guid xjp_registrationdocumentrefid { get; set; }
        [AntiXss]
        public string xjp_ownertype { get; set; }
        [AntiXss]
        public string xjp_ownershippattern { get; set; }
        [AntiXss]
        public string xjp_usagecategory { get; set; }
        [AntiXss]
        public string xjp_customernumber { get; set; }
        [AntiXss]
        public int xjp_round { get; set; }
        [AntiXss]
        public Guid xjp_currentplatenumbersegment1id { get; set; }
        [AntiXss]
        public string xjp_registrationresultofficeidname { get; set; }
        [AntiXss]
        public string xjp_compulsoryautomobileliabilityinsurance { get; set; }
        [AntiXss]
        public string xjp_premisesvehiclename { get; set; }
        [AntiXss]
        public string xjp_creditcompanyidyominame { get; set; }
        [AntiXss]
        public string xjp_registrationrequestnumber { get; set; }
        [AntiXss]
        public string xjp_certificationofparkingspace { get; set; }
        [AntiXss]
        public string xjp_width { get; set; }
        [AntiXss]
        public decimal xjp_actualautomobiletaxamount { get; set; }
        [AntiXss]
        public string xjp_governmentofficename { get; set; }
        [AntiXss]
        public Guid xjp_ownerid { get; set; }
        [AntiXss]
        public string xjp_recyclingticket { get; set; }
        [AntiXss]
        public string xjp_registrationtypename { get; set; }
        [AntiXss]
        public decimal xjp_actualautomobiletaxamount_base { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public DateTime xjp_registrationresultsdate { get; set; }
        [AntiXss]
        public string xjp_garageapplicationcityidname { get; set; }
        [AntiXss]
        public Guid xjp_parentbusinessunitid { get; set; }
        [AntiXss]
        public DateTime xjp_scheduleddeliverydate { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public string xjp_productidname { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string xjp_ownerrepresentativedescription { get; set; }
        [AntiXss]
        public DateTime xjp_bringingindate { get; set; }
        [AntiXss]
        public string xjp_vehicleidentificationnumber { get; set; }
        [AntiXss]
        public bool xjp_remodeling { get; set; }
        [AntiXss]
        public decimal xjp_vehicleacquisitiontaxamount { get; set; }
        [AntiXss]
        public Guid xjp_productconfigurationid { get; set; }
        [AntiXss]
        public string xjp_peoplewithdisabilitiesname { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xjp_creditcompanyidname { get; set; }
        [AntiXss]
        public string xjp_writtenreportmodel { get; set; }
        [AntiXss]
        public string xjp_productinteriorcoloridname { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string xjp_requestplatenumbername { get; set; }
        [AntiXss]
        public string xjp_parkingplacecertificatenumber { get; set; }
        [AntiXss]
        public Guid xjp_stockinventoryusedvehicleid { get; set; }
        [AntiXss]
        public decimal xjp_consumptiontaxamount_base { get; set; }
        [AntiXss]
        public string xjp_registrationreferencenumber { get; set; }
        [AntiXss]
        public string xjp_productionyearjapan { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string xjp_requiredservicename { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string xjp_registrationmethod { get; set; }
        [AntiXss]
        public string xjp_garageapplicationprovinceidname { get; set; }
        [AntiXss]
        public DateTime xjp_miscellaneouschargecollectiondate { get; set; }
        [AntiXss]
        public string xjp_spaceforcorrespondence { get; set; }
        [AntiXss]
        public bool xjp_documentcompletion { get; set; }
        [AntiXss]
        public string xjp_officiallyregisteredseal { get; set; }
        [AntiXss]
        public Guid xjp_requestingbusinessunitid { get; set; }
        [AntiXss]
        public DateTime xjp_transactiondate { get; set; }
        [AntiXss]
        public decimal xjp_accessoriesconsumptiontax { get; set; }
        [AntiXss]
        public string xjp_productexteriorcoloridname { get; set; }
        [AntiXss]
        public string xjp_registrationdocumentrefidname { get; set; }
        [AntiXss]
        public string xjp_ownerphone { get; set; }
        [AntiXss]
        public bool xjp_premisesvehicle { get; set; }
        [AntiXss]
        public bool xjp_taxreductioncategory { get; set; }
        [AntiXss]
        public bool xjp_agencyweighttaxpayment { get; set; }
        [AntiXss]
        public Guid xjp_ownercityid { get; set; }
        [AntiXss]
        public bool xjp_completeinspectionexpired { get; set; }
        [AntiXss]
        public string xjp_recognizedmodelidname { get; set; }
        [AntiXss]
        public string xjp_owneridyominame { get; set; }
        [AntiXss]
        public string xjp_personinchargeidname { get; set; }
        [AntiXss]
        public Guid xjp_lastregistrationpaymentid { get; set; }
        [AntiXss]
        public Guid xjp_requestedbusinessunitid { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsuranceholder { get; set; }
        [AntiXss]
        public string xjp_usercityidname { get; set; }
        [AntiXss]
        public string xjp_documentcompletionname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xjp_lastregistrationpaymentidname { get; set; }
        [AntiXss]
        public string xjp_agencyhandling { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public DateTime xjp_scheduleshipmentdate { get; set; }
        [AntiXss]
        public string xjp_currentplatenumbersegment3 { get; set; }
        [AntiXss]
        public string xjp_requestednumber { get; set; }
        [AntiXss]
        public string xjp_agencyregistrationstatusname { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string xjp_productconfigurationidname { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public string xjp_registrationresultplatenumber { get; set; }
        [AntiXss]
        public Guid xjp_applicationregistrationofficeid { get; set; }
        [AntiXss]
        public string xjp_lightcharacter { get; set; }
        [AntiXss]
        public string xjp_useridname { get; set; }
        [AntiXss]
        public string xjp_button { get; set; }
        [AntiXss]
        public string xjp_taxreductioncategoryname { get; set; }
        [AntiXss]
        public Guid xjp_userprovinceid { get; set; }
        [AntiXss]
        public string xjp_garagevillageandstreetidname { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string xjp_useridyominame { get; set; }
        [AntiXss]
        public string xjp_requestedrequestedbusinessunitincharge { get; set; }
        [AntiXss]
        public Guid xjp_garageapplicationcityid { get; set; }
        [AntiXss]
        public string xjp_customerdescription { get; set; }
        [AntiXss]
        public DateTime xjp_registrationsendingdate { get; set; }
        [AntiXss]
        public string xjp_othercarplatenumber { get; set; }
        [AntiXss]
        public decimal xjp_weighttaxamount { get; set; }
        [AntiXss]
        public string xjp_productstyleidname { get; set; }
        [AntiXss]
        public string xjp_parkingplaceownersamewiththeusername { get; set; }
        [AntiXss]
        public string xjp_proxydocumentcollectedby { get; set; }
        [AntiXss]
        public string xjp_currentplatenumbersegment2 { get; set; }
        [AntiXss]
        public string xjp_length { get; set; }
        [AntiXss]
        public decimal xjp_consumptiontaxamount { get; set; }
        [AntiXss]
        public bool xjp_reject { get; set; }
        [AntiXss]
        public DateTime xjp_receivingdate { get; set; }
        [AntiXss]
        public string xjp_userrepresentativedescription { get; set; }
        [AntiXss]
        public string xjp_useraddress3 { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public Guid xjp_insurancecompanyid { get; set; }
        [AntiXss]
        public string xjp_currentplatenumbersegment1idname { get; set; }
        [AntiXss]
        public string xjp_requestedkatakana { get; set; }
        [AntiXss]
        public string xjp_proxydocumentcollectedbyname { get; set; }
        [AntiXss]
        public Guid xjp_garagevillageandstreetid { get; set; }
        [AntiXss]
        public string xjp_tempotherids { get; set; }
        [AntiXss]
        public string xjp_usagecategoryname { get; set; }
        [AntiXss]
        public decimal xjp_acquisitiontaxtotalamount { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsuranceapplymethodname { get; set; }
        [AntiXss]
        public decimal xjp_vehicleacquisitiontaxamount_base { get; set; }
        [AntiXss]
        public string xjp_rentalvehiclename { get; set; }
        [AntiXss]
        public string xjp_userpostalcode { get; set; }
        [AntiXss]
        public string xjp_theletterofattorney { get; set; }
        [AntiXss]
        public Guid xjp_productexteriorcolorid { get; set; }
        [AntiXss]
        public string xjp_agencyregistrationstatus { get; set; }
        [AntiXss]
        public bool xjp_usedvehicletaxreductioncategory { get; set; }
        [AntiXss]
        public bool xjp_ownerhaveothercar { get; set; }
        [AntiXss]
        public DateTime xjp_documentreturndate { get; set; }
        [AntiXss]
        public string xjp_othercarregistrationofficeidname { get; set; }
        [AntiXss]
        public string xjp_ownerrepresentative { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public Guid xjp_garageapplicationprovinceid { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsuranceholdername { get; set; }
        [AntiXss]
        public string xjp_ossregistrationstatusname { get; set; }
        [AntiXss]
        public DateTime xjp_documentreceiptdate { get; set; }
        [AntiXss]
        public Guid xjp_productinteriorcolorid { get; set; }
        [AntiXss]
        public string xjp_modelspecification { get; set; }
        [AntiXss]
        public DateTime xjp_invoiceddate { get; set; }
        [AntiXss]
        public Guid xjp_registrationrequestid { get; set; }
        [AntiXss]
        public decimal xjp_caliamount { get; set; }
        [AntiXss]
        public string xjp_registrationresultperiod { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment3 { get; set; }
        [AntiXss]
        public string xjp_userprovinceidname { get; set; }
        [AntiXss]
        public decimal xjp_miscellaneouschargetotal { get; set; }
        [AntiXss]
        public bool xjp_rentalvehicle { get; set; }
        [AntiXss]
        public string xjp_ownervillageandaddressidname { get; set; }
        [AntiXss]
        public bool xjp_registrationcardeliveryagency { get; set; }
        [AntiXss]
        public string xjp_ownerprovinceidname { get; set; }
        [AntiXss]
        public bool xjp_agencycalipayment { get; set; }
        [AntiXss]
        public decimal xjp_automobiletaxamount_base { get; set; }
        [AntiXss]
        public string xjp_registrationpurpose { get; set; }
        [AntiXss]
        public bool xjp_lightcharacterplatetype { get; set; }
        [AntiXss]
        public string xjp_handling { get; set; }
        [AntiXss]
        public Guid xjp_recognizedmodelid { get; set; }
        [AntiXss]
        public string xjp_ownerhaveothercarname { get; set; }
        [AntiXss]
        public string xjp_rejectname { get; set; }
        [AntiXss]
        public string xjp_statusname { get; set; }
        [AntiXss]
        public bool xjp_lightvehicle { get; set; }
        [AntiXss]
        public string xjp_paymentmethodidname { get; set; }
        [AntiXss]
        public decimal xjp_actualweighttaxamount_base { get; set; }
        [AntiXss]
        public Guid xjp_garageprovinceid { get; set; }
        [AntiXss]
        public int xjp_stocknumberlookuptype { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string xjp_requestedplatenumberproof { get; set; }
        [AntiXss]
        public string xjp_status { get; set; }
        [AntiXss]
        public Guid xjp_platenumbersegment1id { get; set; }
        [AntiXss]
        public string xjp_garagecityidname { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public string xjp_ownertypename { get; set; }
        [AntiXss]
        public string xjp_ribbonsource { get; set; }
        [AntiXss]
        public string xjp_stockinventoryusedvehicleidname { get; set; }
        [AntiXss]
        public string xjp_parkingplaceownersamewiththeuser { get; set; }
        [AntiXss]
        public Guid xjp_productstyleid { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
