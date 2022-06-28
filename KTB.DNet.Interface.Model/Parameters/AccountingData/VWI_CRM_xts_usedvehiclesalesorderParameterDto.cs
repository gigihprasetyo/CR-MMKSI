#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_equipmentParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
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
    public class VWI_CRM_xts_usedvehiclesalesorderParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public decimal xts_totalpayment_base { get; set; }
        [AntiXss]
        public DateTime xjp_compulsoryinsurancecoveredperiod { get; set; }
        [AntiXss]
        public decimal xjp_bonuspaymentamount1_base { get; set; }
        [AntiXss]
        public Guid xjp_usercityid { get; set; }
        [AntiXss]
        public Guid xts_financingcompanyid { get; set; }
        [AntiXss]
        public string xjp_actualsalesrecognitionname { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductamount_base { get; set; }
        [AntiXss]
        public string xts_transmissionname { get; set; }
        [AntiXss]
        public string xjp_acquisitiontaxreductionidname { get; set; }
        [AntiXss]
        public DateTime xjp_automobiletaxfirstpaymentperiod { get; set; }
        [AntiXss]
        public string xjp_guarantoraddress3 { get; set; }
        [AntiXss]
        public decimal xts_weights { get; set; }
        [AntiXss]
        public decimal xjp_automobiletaxremainingamount_base { get; set; }
        [AntiXss]
        public string xjp_firstyearregistration { get; set; }
        [AntiXss]
        public decimal xts_extrachargesamount { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string xts_salesquoteidname { get; set; }
        [AntiXss]
        public string x01_usercountryidname { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargecost { get; set; }
        [AntiXss]
        public Int32 importsequencenumber { get; set; }
        [AntiXss]
        public string x01_payercountryidname { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargeconsumptiontaxamount_base { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsuranceidname { get; set; }
        [AntiXss]
        public Int32 xts_potentiallookuptype { get; set; }
        [AntiXss]
        public string xjp_usagecategory { get; set; }
        [AntiXss]
        public decimal xts_totaltradeinconsumptiontaxamount_base { get; set; }
        [AntiXss]
        public decimal xts_enginevolume { get; set; }
        [AntiXss]
        public string xjp_actualsalesrecognition { get; set; }
        [AntiXss]
        public Guid xts_stockid { get; set; }
        [AntiXss]
        public bool x01_registrationfeeispaid { get; set; }
        [AntiXss]
        public DateTime xjp_garagecertificateacquisitiondate { get; set; }
        [AntiXss]
        public string xts_virtualaccountname { get; set; }
        [AntiXss]
        public decimal xts_servicemaintenancetaxamount_base { get; set; }
        [AntiXss]
        public Guid xjp_lastregistrationrequestid { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string xts_owneridyominame { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargesalesprice_base { get; set; }
        [AntiXss]
        public string xjp_firstregistrationjapan { get; set; }
        [AntiXss]
        public decimal xts_vehiclecost_base { get; set; }
        [AntiXss]
        public Guid x01_ownercountryid { get; set; }
        [AntiXss]
        public string xjp_insurancecompanycode { get; set; }
        [AntiXss]
        public DateTime xts_birthdate { get; set; }
        [AntiXss]
        public string xts_handling { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargegrossmargin { get; set; }
        [AntiXss]
        public string xts_siteidname { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public decimal xts_serviceaddandmaintenancenetsalesprice_base { get; set; }
        [AntiXss]
        public decimal xts_serviceaddandmaintenancegrossmargin { get; set; }
        [AntiXss]
        public string xjp_registrationpurposename { get; set; }
        [AntiXss]
        public Int32 xjp_effectivebillperioddays { get; set; }
        [AntiXss]
        public decimal xts_ttlvehiclerelatedproducttransactionamount { get; set; }
        [AntiXss]
        public Guid xjp_payerprovinceid { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductcost { get; set; }
        [AntiXss]
        public Guid xts_productid { get; set; }
        [AntiXss]
        public decimal xjp_storecost { get; set; }
        [AntiXss]
        public string xts_intercompanysalesname { get; set; }
        [AntiXss]
        public DateTime xjp_automobiletaxunelapsedtime { get; set; }
        [AntiXss]
        public bool xts_vehicledeliveryplace { get; set; }
        [AntiXss]
        public decimal xjp_automobiletaxamount { get; set; }
        [AntiXss]
        public Guid xts_usedvehicleexteriorcolorid { get; set; }
        [AntiXss]
        public string xjp_owneraddress3 { get; set; }
        [AntiXss]
        public Guid xjp_weightedtaxid { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsvehicleclassificationidname { get; set; }
        [AntiXss]
        public string xts_cardeliveryname { get; set; }
        [AntiXss]
        public decimal xts_totaltradeinamount { get; set; }
        [AntiXss]
        public decimal xjp_serviceacquisitiontaxamount { get; set; }
        [AntiXss]
        public decimal xjp_airbagcharge { get; set; }
        [AntiXss]
        public decimal xts_otherdiscountsubtotal_base { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public decimal xjp_autmbltaxremainingamountconsumptiontax { get; set; }
        [AntiXss]
        public string xjp_weightedtaxreductionidname { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsuranceamount { get; set; }
        [AntiXss]
        public string xts_landtransportationdestinationdistrictidname { get; set; }
        [AntiXss]
        public string x01_ordertypeidname { get; set; }
        [AntiXss]
        public string xjp_payeridname { get; set; }
        [AntiXss]
        public decimal xts_totaltradeinremainderbondamount_base { get; set; }
        [AntiXss]
        public string xjp_vehiclemanagementidname { get; set; }
        [AntiXss]
        public string xjp_guarantoraddress1 { get; set; }
        [AntiXss]
        public Guid xts_miscellaneouschargetemplateid { get; set; }
        [AntiXss]
        public decimal xts_vehiclegrossmarginsubtotal_base { get; set; }
        [AntiXss]
        public string xts_bcmidname { get; set; }
        [AntiXss]
        public string xjp_payeraddress2 { get; set; }
        [AntiXss]
        public string xjp_ownerpostalcode { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public string xts_userphone { get; set; }
        [AntiXss]
        public string xjp_guarantoraddress4 { get; set; }
        [AntiXss]
        public decimal xts_totaltradeinremainderbondamount { get; set; }
        [AntiXss]
        public DateTime xts_paymentendingperiod { get; set; }
        [AntiXss]
        public decimal xjp_automobiletaxremainingamount { get; set; }
        [AntiXss]
        public string xts_owneridname { get; set; }
        [AntiXss]
        public string xts_customeraddress { get; set; }
        [AntiXss]
        public decimal xts_estimatedprofitandlosssalesprice_base { get; set; }
        [AntiXss]
        public Guid xts_potentialcustomerid { get; set; }
        [AntiXss]
        public string xts_manufactureridname { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargegrossmargin_base { get; set; }
        [AntiXss]
        public Guid xjp_userprovinceid { get; set; }
        [AntiXss]
        public Guid xts_landtransportationorigindistrictid { get; set; }
        [AntiXss]
        public decimal xts_vehiclegrossmargin_base { get; set; }
        [AntiXss]
        public decimal xjp_tradeinautomobiletaxcashbackamount_base { get; set; }
        [AntiXss]
        public decimal xts_estimatedprofitandlosscost_base { get; set; }
        [AntiXss]
        public decimal xts_totalreceiptamount_base { get; set; }
        [AntiXss]
        public decimal xjp_shredderresiduecharge_base { get; set; }
        [AntiXss]
        public decimal xts_monthlypayment_base { get; set; }
        [AntiXss]
        public string xts_landtransportationorigindistrictidname { get; set; }
        [AntiXss]
        public string xjp_payeridyominame { get; set; }
        [AntiXss]
        public Guid xts_userid { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public DateTime xts_expectedtradeindate { get; set; }
        [AntiXss]
        public Int32 xts_load { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargediscount_base { get; set; }
        [AntiXss]
        public string xts_useridyominame { get; set; }
        [AntiXss]
        public decimal xts_totalvehicletransactionamount { get; set; }
        [AntiXss]
        public decimal xts_totalmiscellaneoustransactionamount_base { get; set; }
        [AntiXss]
        public decimal xjp_weighttaxamount_base { get; set; }
        [AntiXss]
        public string x01_ownercountryidname { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargebaseamount_base { get; set; }
        [AntiXss]
        public decimal xjp_shredderresiduecharge { get; set; }
        [AntiXss]
        public decimal xjp_elvvariouscosttaxation { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproducttaxamount { get; set; }
        [AntiXss]
        public Guid xjp_guarantorprovinceid { get; set; }
        [AntiXss]
        public decimal xjp_bonuspaymentamount2 { get; set; }
        [AntiXss]
        public decimal xts_salescampaigncost { get; set; }
        [AntiXss]
        public string xjp_recyclingfeedepositclassificationname { get; set; }
        [AntiXss]
        public decimal xts_balance_base { get; set; }
        [AntiXss]
        public DateTime xjp_vehicleinspectionexpirationdate { get; set; }
        [AntiXss]
        public string xts_locationidname { get; set; }
        [AntiXss]
        public decimal xts_serviceaddandmaintenancediscount_base { get; set; }
        [AntiXss]
        public decimal xts_totalpaymentamount_base { get; set; }
        [AntiXss]
        public string xts_consumptiontaxidname { get; set; }
        [AntiXss]
        public bool x01_downpaymentispaid { get; set; }
        [AntiXss]
        public Guid x01_payercountryid { get; set; }
        [AntiXss]
        public decimal xjp_fluorocarboncharge { get; set; }
        [AntiXss]
        public string xts_wholesalesorderidname { get; set; }
        [AntiXss]
        public decimal xjp_bonuspaymentamount2_base { get; set; }
        [AntiXss]
        public string xts_ownername { get; set; }
        [AntiXss]
        public string xjp_payeraliasname { get; set; }
        [AntiXss]
        public string xts_transmission { get; set; }
        [AntiXss]
        public decimal xts_othergrossmarginsubtotal_base { get; set; }
        [AntiXss]
        public decimal xts_consumptiontaxamount { get; set; }
        [AntiXss]
        public string xts_warrantytypeidname { get; set; }
        [AntiXss]
        public string xts_owneraddress { get; set; }
        [AntiXss]
        public decimal xts_totalservicemaintenancetransactionamount { get; set; }
        [AntiXss]
        public string xjp_guarantorvillageandaddressidname { get; set; }
        [AntiXss]
        public DateTime xts_paymentstartingperiod { get; set; }
        [AntiXss]
        public decimal xts_vehiclediscountsubtotal_base { get; set; }
        [AntiXss]
        public decimal xts_vehiclediscount { get; set; }
        [AntiXss]
        public string xts_salespersonname { get; set; }
        [AntiXss]
        public decimal xts_totaltradeinprice_base { get; set; }
        [AntiXss]
        public Int32 xts_numberofpayment { get; set; }
        [AntiXss]
        public string xjp_guarantorprovinceidname { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductbaseamount_base { get; set; }
        [AntiXss]
        public decimal xts_interestrateamount { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string xjp_useraddress2 { get; set; }
        [AntiXss]
        public string xts_laststatus { get; set; }
        [AntiXss]
        public DateTime xts_effectiveto { get; set; }
        [AntiXss]
        public decimal xts_totaladvancepayment_base { get; set; }
        [AntiXss]
        public string xts_status { get; set; }
        [AntiXss]
        public string xjp_vehiclemanagementaddress { get; set; }
        [AntiXss]
        public decimal xts_referralcommissioncost_base { get; set; }
        [AntiXss]
        public string xjp_payerphone { get; set; }
        [AntiXss]
        public Int32 xts_weight { get; set; }
        [AntiXss]
        public string xjp_payercityidname { get; set; }
        [AntiXss]
        public string xts_owneraliasname { get; set; }
        [AntiXss]
        public string xts_productdescription { get; set; }
        [AntiXss]
        public decimal xts_referralgrossmargin_base { get; set; }
        [AntiXss]
        public decimal xjp_informationmanagementcharge { get; set; }
        [AntiXss]
        public decimal xts_variouscostconsumptiontaxamount_base { get; set; }
        [AntiXss]
        public decimal xts_salescampaigncost_base { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public DateTime xjp_automobileweighttaxcarinspectionperiod { get; set; }
        [AntiXss]
        public decimal xts_subtotal { get; set; }
        [AntiXss]
        public Guid xts_manufacturerid { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string xts_potentialcontactidname { get; set; }
        [AntiXss]
        public string xts_vehicledeliveryplacename { get; set; }
        [AntiXss]
        public DateTime xts_actualvehicledeliverydate { get; set; }
        [AntiXss]
        public decimal xts_downpayment_base { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproducttaxamount_base { get; set; }
        [AntiXss]
        public DateTime xts_expectedvehicledeliverydate { get; set; }
        [AntiXss]
        public string xts_ribbonsource { get; set; }
        [AntiXss]
        public Guid processid { get; set; }
        [AntiXss]
        public string xts_virtualaccountidname { get; set; }
        [AntiXss]
        public string xts_salescategory { get; set; }
        [AntiXss]
        public Guid xjp_uservillageandaddressid { get; set; }
        [AntiXss]
        public decimal xts_monthlypayment { get; set; }
        [AntiXss]
        public string traversedpath { get; set; }
        [AntiXss]
        public decimal xts_interestrateamount_base { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public decimal xts_balance { get; set; }
        [AntiXss]
        public decimal xts_estimatedprofitandlossdiscount_base { get; set; }
        [AntiXss]
        public Guid x01_guarantorcountryid { get; set; }
        [AntiXss]
        public Guid xts_locationid { get; set; }
        [AntiXss]
        public string xjp_grade { get; set; }
        [AntiXss]
        public DateTime xts_transactiondate { get; set; }
        [AntiXss]
        public string xjp_recyclingticketnumber { get; set; }
        [AntiXss]
        public string xts_ownerphone { get; set; }
        [AntiXss]
        public Guid xjp_ownervillageandaddressid { get; set; }
        [AntiXss]
        public string xts_currentplatenumber { get; set; }
        [AntiXss]
        public string xjp_uservillageandaddressidname { get; set; }
        [AntiXss]
        public Guid xjp_guarantorcityid { get; set; }
        [AntiXss]
        public Guid xts_warehouseid { get; set; }
        [AntiXss]
        public string xjp_ownercityidname { get; set; }
        [AntiXss]
        public string xjp_guarantoridname { get; set; }
        [AntiXss]
        public Guid xjp_payervillageandaddressid { get; set; }
        [AntiXss]
        public decimal xts_othercostsubtotal_base { get; set; }
        [AntiXss]
        public decimal xts_salescampaignmargin_base { get; set; }
        [AntiXss]
        public Guid xts_salespersonid { get; set; }
        [AntiXss]
        public decimal xjp_capitalmanagementcharge { get; set; }
        [AntiXss]
        public Guid x01_usercountryid { get; set; }
        [AntiXss]
        public string xjp_userpostalcode { get; set; }
        [AntiXss]
        public string xts_username { get; set; }
        [AntiXss]
        public string xjp_payerpostalcode { get; set; }
        [AntiXss]
        public string x01_nvacquisitiontaxidname { get; set; }
        [AntiXss]
        public string xjp_useraddress1 { get; set; }
        [AntiXss]
        public decimal xts_downpayment { get; set; }
        [AntiXss]
        public decimal xjp_informationmanagementcharge_base { get; set; }
        [AntiXss]
        public decimal xts_extrachargesamount_base { get; set; }
        [AntiXss]
        public decimal xjp_tradeinautomobiletaxcashbackamount { get; set; }
        [AntiXss]
        public decimal xjp_subtotaltaxandinsuranceremainingamount_base { get; set; }
        [AntiXss]
        public decimal xts_accessorytaxamount { get; set; }
        [AntiXss]
        public string xjp_useraddress4 { get; set; }
        [AntiXss]
        public decimal xts_othersalessubtotal_base { get; set; }
        [AntiXss]
        public string xjp_consumptiontaxcategoryname { get; set; }
        [AntiXss]
        public string xjp_recyclingfeedepositclassification { get; set; }
        [AntiXss]
        public decimal xts_totalpayment { get; set; }
        [AntiXss]
        public decimal xjp_storecost_base { get; set; }
        [AntiXss]
        public string xjp_usagecategoryname { get; set; }
        [AntiXss]
        public decimal xts_totaltradeinconsumptiontaxamount { get; set; }
        [AntiXss]
        public string xjp_guarantorpostalcode { get; set; }
        [AntiXss]
        public decimal xts_billamount_base { get; set; }
        [AntiXss]
        public string xts_potentialcustomeridname { get; set; }
        [AntiXss]
        public Guid xts_landtransportationdestinationdistrictid { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_useraliasname { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductgrossmargin_base { get; set; }
        [AntiXss]
        public string xjp_owneraddress1 { get; set; }
        [AntiXss]
        public decimal xts_interestratecost { get; set; }
        [AntiXss]
        public string xjp_garagecertificatepolicestationname { get; set; }
        [AntiXss]
        public string xjp_modelspecificationnumber { get; set; }
        [AntiXss]
        public decimal xts_estimatedprofitandlossdiscount { get; set; }
        [AntiXss]
        public Guid xts_salesquoteid { get; set; }
        [AntiXss]
        public decimal xjp_totalrecyclingcharge { get; set; }
        [AntiXss]
        public string xjp_classificationnumber { get; set; }
        [AntiXss]
        public decimal xts_vehicletaxamount_base { get; set; }
        [AntiXss]
        public Guid xts_virtualaccountid { get; set; }
        [AntiXss]
        public Guid xjp_compulsoryinsvehicleclassificationid { get; set; }
        [AntiXss]
        public string xts_customername { get; set; }
        [AntiXss]
        public string xjp_guarantoraliasname { get; set; }
        [AntiXss]
        public string xts_landtransportationdestinationprovinceidname { get; set; }
        [AntiXss]
        public decimal xjp_elfrecyclingdepositedmoney_base { get; set; }
        [AntiXss]
        public Guid xjp_guarantorvillageandaddressid { get; set; }
        [AntiXss]
        public string xjp_useraddress3 { get; set; }
        [AntiXss]
        public string x01_vehiclespecificationidname { get; set; }
        [AntiXss]
        public DateTime xts_registrationdesireddate { get; set; }
        [AntiXss]
        public string xts_claimstatusname { get; set; }
        [AntiXss]
        public decimal xjp_elvtotalamount_base { get; set; }
        [AntiXss]
        public string xjp_consumptiontaxcategory { get; set; }
        [AntiXss]
        public decimal xts_othersalessubtotal { get; set; }
        [AntiXss]
        public Guid xts_vehiclemodelid { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Int32 xts_currentmileage { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public decimal xts_totaltradeinrecyclingdepositamount_base { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsurancerefundamount_base { get; set; }
        [AntiXss]
        public Guid xjp_acquisitiontaxreductionid { get; set; }
        [AntiXss]
        public string xjp_ownervillageandaddressidname { get; set; }
        [AntiXss]
        public decimal xts_interestgrossmargin { get; set; }
        [AntiXss]
        public decimal xts_serviceaddandmaintenancediscount { get; set; }
        [AntiXss]
        public string xts_statusname { get; set; }
        [AntiXss]
        public string xjp_automobiletaxremainingcategoryname { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductcost_base { get; set; }
        [AntiXss]
        public string xts_temporary { get; set; }
        [AntiXss]
        public string xts_stockidname { get; set; }
        [AntiXss]
        public decimal xts_accessorytaxrate { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string xts_productidname { get; set; }
        [AntiXss]
        public string xts_claimstatus { get; set; }
        [AntiXss]
        public Guid xjp_acquisitiontaxid { get; set; }
        [AntiXss]
        public decimal xts_vehiclecostsubtotal { get; set; }
        [AntiXss]
        public string xts_potentiallookupname { get; set; }
        [AntiXss]
        public bool xts_virtualaccountuse { get; set; }
        [AntiXss]
        public Int32 statecode { get; set; }
        [AntiXss]
        public decimal xts_totalremainingamount { get; set; }
        [AntiXss]
        public Int32 statuscode { get; set; }
        [AntiXss]
        public decimal xts_totaltradeinamount_base { get; set; }
        [AntiXss]
        public string x01_registrationfeeispaidname { get; set; }
        [AntiXss]
        public decimal xts_estimatedprofitandlossgrossmargin_base { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargenetsalesprice { get; set; }
        [AntiXss]
        public decimal xts_totalremainingamount_base { get; set; }
        [AntiXss]
        public DateTime xts_warrantyexpirationdate { get; set; }
        [AntiXss]
        public decimal xjp_acquisitiontaxamount_base { get; set; }
        [AntiXss]
        public string xjp_acquisitiontaxidname { get; set; }
        [AntiXss]
        public string xjp_lastregistrationrequestidname { get; set; }
        [AntiXss]
        public decimal xts_vehiclesalessubtotal { get; set; }
        [AntiXss]
        public decimal xjp_remainingvaluetaxamount { get; set; }
        [AntiXss]
        public decimal xjp_elvconsumptiontax_base { get; set; }
        [AntiXss]
        public string xjp_vehicleinspectionexpirationdatejapan { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsuranceremainingamount { get; set; }
        [AntiXss]
        public decimal xjp_voluntaryinsurance_base { get; set; }
        [AntiXss]
        public decimal xts_vehiclegrossmarginsubtotal { get; set; }
        [AntiXss]
        public decimal xts_variouscostconsumptiontaxamount { get; set; }
        [AntiXss]
        public Int32 timezoneruleversionnumber { get; set; }
        [AntiXss]
        public Guid x01_ordertypeid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid xts_potentialcontactid { get; set; }
        [AntiXss]
        public string xjp_guarantoridyominame { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargeconsumptiontaxamount { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public decimal xts_vehicletaxrate { get; set; }
        [AntiXss]
        public string xjp_registrationpurpose { get; set; }
        [AntiXss]
        public decimal xts_vehiclebaseamount_base { get; set; }
        [AntiXss]
        public Guid xts_siteid { get; set; }
        [AntiXss]
        public Guid xjp_ownerprovinceid { get; set; }
        [AntiXss]
        public string xts_usedvehicleexteriorcoloridname { get; set; }
        [AntiXss]
        public string xts_parentbusinessunitidname { get; set; }
        [AntiXss]
        public decimal xts_loads { get; set; }
        [AntiXss]
        public decimal xts_totalmiscellaneoustransactionamount { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string xjp_owneraddress2 { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public Guid xts_landtransportationoriginprovinceid { get; set; }
        [AntiXss]
        public string xts_cardelivery { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public string xjp_guarantorcityidname { get; set; }
        [AntiXss]
        public decimal xts_grandtotal { get; set; }
        [AntiXss]
        public decimal xts_othergrossmarginsubtotal { get; set; }
        [AntiXss]
        public string xjp_owneraddress4 { get; set; }
        [AntiXss]
        public decimal xts_vehiclenetsalesprice_base { get; set; }
        [AntiXss]
        public decimal xts_serviceaddandmaintenancecost { get; set; }
        [AntiXss]
        public decimal xts_discountbaseamount { get; set; }
        [AntiXss]
        public decimal xjp_fluorocarboncharge_base { get; set; }
        [AntiXss]
        public decimal xts_interestratecost_base { get; set; }
        [AntiXss]
        public Guid xts_consumptiontaxid { get; set; }
        [AntiXss]
        public decimal xts_totaltradeinprice { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductdiscountamount_base { get; set; }
        [AntiXss]
        public string xjp_ownershipcategoryname { get; set; }
        [AntiXss]
        public decimal xts_referralgrossmargin { get; set; }
        [AntiXss]
        public string xjp_vehiclemanagementphone { get; set; }
        [AntiXss]
        public string xts_potentialcontactidyominame { get; set; }
        [AntiXss]
        public Guid x01_vehiclespecificationid { get; set; }
        [AntiXss]
        public decimal xts_otherdiscountsubtotal { get; set; }
        [AntiXss]
        public decimal xjp_elvvariouscosttaxation_base { get; set; }
        [AntiXss]
        public decimal xts_billamount { get; set; }
        [AntiXss]
        public Guid xts_bcmid { get; set; }
        [AntiXss]
        public string xts_potentialcustomeridyominame { get; set; }
        [AntiXss]
        public Guid xjp_automobiletaxid { get; set; }
        [AntiXss]
        public decimal xjp_elvtotalamount { get; set; }
        [AntiXss]
        public decimal xts_vehiclegrossmargin { get; set; }
        [AntiXss]
        public decimal xjp_elvvariouscosttaxfree_base { get; set; }
        [AntiXss]
        public decimal xjp_elvvariouscosttaxfree { get; set; }
        [AntiXss]
        public string xjp_recognizedmodelidname { get; set; }
        [AntiXss]
        public decimal xjp_subtotaltaxandinsuranceremainingamount { get; set; }
        [AntiXss]
        public string xjp_payeraddress { get; set; }
        [AntiXss]
        public Guid xjp_vehiclemanagementid { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargesalesprice { get; set; }
        [AntiXss]
        public Guid xts_wholesalesorderid { get; set; }
        [AntiXss]
        public string xjp_usercityidname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public decimal xts_serviceaddandmaintenancenetsalesprice { get; set; }
        [AntiXss]
        public decimal xts_referralcommissioncost { get; set; }
        [AntiXss]
        public decimal xjp_voluntaryinsurance { get; set; }
        [AntiXss]
        public decimal xjp_elfrecyclingdepositedmoney { get; set; }
        [AntiXss]
        public Guid xjp_compulsoryinsuranceid { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public Int32 xjp_bonuspaymentmonth1 { get; set; }
        [AntiXss]
        public DateTime xjp_compulsoryinsuranceunelapsedtime { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargebaseamount { get; set; }
        [AntiXss]
        public bool xts_requestplatenumber { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public decimal xts_totalreceiptamount { get; set; }
        [AntiXss]
        public string xts_requestplatenumbername { get; set; }
        [AntiXss]
        public decimal xts_vehiclebaseamount { get; set; }
        [AntiXss]
        public Int32 xts_warrantymileage { get; set; }
        [AntiXss]
        public string xjp_automobiletaxidname { get; set; }
        [AntiXss]
        public Guid xts_parentbusinessunitid { get; set; }
        [AntiXss]
        public string x01_downpaymentispaidname { get; set; }
        [AntiXss]
        public string xts_enginevolumeunitname { get; set; }
        [AntiXss]
        public string xts_laststatusname { get; set; }
        [AntiXss]
        public decimal xts_vehiclesalessubtotal_base { get; set; }
        [AntiXss]
        public string xts_requestedplatenumber { get; set; }
        [AntiXss]
        public DateTime xts_actualtradeinreceivingdate { get; set; }
        [AntiXss]
        public string xjp_payeraddress4 { get; set; }
        [AntiXss]
        public Guid xts_methodofpaymentid { get; set; }
        [AntiXss]
        public string xjp_guarantoraddress2 { get; set; }
        [AntiXss]
        public string xts_customeraliasname { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsuranceremainingamount_base { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string xjp_payeraddress3 { get; set; }
        [AntiXss]
        public decimal xjp_acquisitiontaxamount { get; set; }
        [AntiXss]
        public string xjp_weightedtaxidname { get; set; }
        [AntiXss]
        public string xts_enginevolumeunit { get; set; }
        [AntiXss]
        public string xjp_guarantorname { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductbaseamount { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductnetsalesprice_base { get; set; }
        [AntiXss]
        public string xjp_automobiletaxreductionidname { get; set; }
        [AntiXss]
        public Guid xjp_payercityid { get; set; }
        [AntiXss]
        public string xts_salescategoryname { get; set; }
        [AntiXss]
        public decimal xjp_weighttaxamount { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsurancerefundamount { get; set; }
        [AntiXss]
        public string xjp_lightcharacterplatetypename { get; set; }
        [AntiXss]
        public string xts_useridname { get; set; }
        [AntiXss]
        public Guid xjp_automobiletaxreductionid { get; set; }
        [AntiXss]
        public string xjp_vehiclemanagementidyominame { get; set; }
        [AntiXss]
        public decimal xts_servicemaintenancetaxamount { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproducttaxrate { get; set; }
        [AntiXss]
        public string x01_guarantorcountryidname { get; set; }
        [AntiXss]
        public decimal xts_serviceaddandmaintenancecost_base { get; set; }
        [AntiXss]
        public string xjp_idempotentmessage { get; set; }
        [AntiXss]
        public decimal xts_vehiclediscountsubtotal { get; set; }
        [AntiXss]
        public decimal xts_totalvehicletransactionamount_base { get; set; }
        [AntiXss]
        public Guid stageid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public decimal xjp_subtotaltaxandinsurance_base { get; set; }
        [AntiXss]
        public string xjp_payervillageandaddressidname { get; set; }
        [AntiXss]
        public decimal xts_grandtotal_base { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductdiscountamount { get; set; }
        [AntiXss]
        public string xjp_payername { get; set; }
        [AntiXss]
        public decimal xts_annualcreditrate { get; set; }
        [AntiXss]
        public Guid x01_nvacquisitiontaxid { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargediscount { get; set; }
        [AntiXss]
        public decimal xjp_remainingvaluetaxamount_base { get; set; }
        [AntiXss]
        public string xjp_payeraddress1 { get; set; }
        [AntiXss]
        public string xts_productionyear { get; set; }
        [AntiXss]
        public DateTime xts_invoiceddate { get; set; }
        [AntiXss]
        public bool xts_intercompanysales { get; set; }
        [AntiXss]
        public Guid xts_landtransportationdestinationprovinceid { get; set; }
        [AntiXss]
        public Int32 xjp_usefullife { get; set; }
        [AntiXss]
        public string xjp_guarantoraddress { get; set; }
        [AntiXss]
        public decimal xts_ttlvehiclerelatedproducttransactionamount_base { get; set; }
        [AntiXss]
        public string xts_financingcompanyidyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string xjp_automobiletaxremainingcategory { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductnetsalesprice { get; set; }
        [AntiXss]
        public decimal xts_consumptiontaxamount_base { get; set; }
        [AntiXss]
        public decimal xjp_capitalmanagementcharge_base { get; set; }
        [AntiXss]
        public string xts_usedvehiclesalesordernumber { get; set; }
        [AntiXss]
        public string xts_warehouseidname { get; set; }
        [AntiXss]
        public string xjp_numberofyearselapsed { get; set; }
        [AntiXss]
        public decimal xts_serviceaddandmaintenanceamount_base { get; set; }
        [AntiXss]
        public decimal xts_totaltradeinrecyclingdepositamount { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public decimal xts_servicemaintenancebaseamount_base { get; set; }
        [AntiXss]
        public decimal xjp_serviceacquisitiontaxamount_base { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargenetsalesprice_base { get; set; }
        [AntiXss]
        public string xjp_vehiclemanagementnumber { get; set; }
        [AntiXss]
        public string xjp_ownershipcategory { get; set; }
        [AntiXss]
        public decimal xjp_totalrecyclingcharge_base { get; set; }
        [AntiXss]
        public decimal xts_servicemaintenancetaxrate { get; set; }
        [AntiXss]
        public decimal xjp_bonuspaymentamount1 { get; set; }
        [AntiXss]
        public Int32 utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string xjp_guarantorphone { get; set; }
        [AntiXss]
        public string xts_salespersonidname { get; set; }
        [AntiXss]
        public Guid xjp_guarantorid { get; set; }
        [AntiXss]
        public decimal xts_vehiclediscount_base { get; set; }
        [AntiXss]
        public decimal xts_discountbaseamount_base { get; set; }
        [AntiXss]
        public decimal xjp_airbagcharge_base { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductgrossmargin { get; set; }
        [AntiXss]
        public string xjp_vehiclemanagementname { get; set; }
        [AntiXss]
        public string xjp_userprovinceidname { get; set; }
        [AntiXss]
        public decimal xts_totalservicemaintenancetransactionamount_base { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsremainamountconsumptiontax_base { get; set; }
        [AntiXss]
        public string xts_methodofpaymentidname { get; set; }
        [AntiXss]
        public decimal xts_serviceaddandmaintenancegrossmargin_base { get; set; }
        [AntiXss]
        public decimal xts_interestgrossmargin_base { get; set; }
        [AntiXss]
        public decimal xts_estimatedprofitandlosssalesprice { get; set; }
        [AntiXss]
        public string xjp_ownerprovinceidname { get; set; }
        [AntiXss]
        public string xjp_payerprovinceidname { get; set; }
        [AntiXss]
        public decimal xjp_automobiletaxamount_base { get; set; }
        [AntiXss]
        public bool xjp_lightcharacterplatetype { get; set; }
        [AntiXss]
        public string xts_vehiclemodelidname { get; set; }
        [AntiXss]
        public Guid xjp_recognizedmodelid { get; set; }
        [AntiXss]
        public decimal xjp_elvconsumptiontax { get; set; }
        [AntiXss]
        public decimal xts_vehiclerelatedproductamount { get; set; }
        [AntiXss]
        public decimal xts_serviceaddandmaintenanceamount { get; set; }
        [AntiXss]
        public decimal xts_totalpaymentamount { get; set; }
        [AntiXss]
        public decimal xjp_autmbltaxremainingamountconsumptiontax_base { get; set; }
        [AntiXss]
        public Guid xjp_ownercityid { get; set; }
        [AntiXss]
        public decimal xts_subtotal_base { get; set; }
        [AntiXss]
        public decimal xts_salescampaignmargin { get; set; }
        [AntiXss]
        public string xts_handlingname { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public Guid xts_usedvehiclesalesorderid { get; set; }
        [AntiXss]
        public decimal xts_othercostsubtotal { get; set; }
        [AntiXss]
        public Guid xts_warrantytypeid { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public decimal xts_totaladvancepayment { get; set; }
        [AntiXss]
        public decimal xts_vehiclenetsalesprice { get; set; }
        [AntiXss]
        public decimal xts_vehiclecostsubtotal_base { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsremainamountconsumptiontax { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public decimal xts_accessorytaxamount_base { get; set; }
        [AntiXss]
        public string xts_landtransportationoriginprovinceidname { get; set; }
        [AntiXss]
        public decimal xts_vehicletaxamount { get; set; }
        [AntiXss]
        public string xts_virtualaccountusename { get; set; }
        [AntiXss]
        public Guid xjp_payerid { get; set; }
        [AntiXss]
        public decimal xts_servicemaintenancebaseamount { get; set; }
        [AntiXss]
        public decimal xts_vehicleamount_base { get; set; }
        [AntiXss]
        public decimal xjp_compulsoryinsuranceamount_base { get; set; }
        [AntiXss]
        public decimal xjp_subtotaltaxandinsurance { get; set; }
        [AntiXss]
        public decimal xts_miscellaneouschargecost_base { get; set; }
        [AntiXss]
        public decimal xts_vehiclecost { get; set; }
        [AntiXss]
        public decimal xts_estimatedprofitandlossgrossmargin { get; set; }
        [AntiXss]
        public decimal xts_vehicleamount { get; set; }
        [AntiXss]
        public string xts_miscellaneouschargetemplateidname { get; set; }
        [AntiXss]
        public decimal xts_estimatedprofitandlosscost { get; set; }
        [AntiXss]
        public Guid xjp_weightedtaxreductionid { get; set; }
        [AntiXss]
        public string xts_useraddress { get; set; }
        [AntiXss]
        public string xjp_contractnumber { get; set; }
        [AntiXss]
        public Guid xts_ownerid { get; set; }
        [AntiXss]
        public string xts_financingcompanyidname { get; set; }
        [AntiXss]
        public Int32 xjp_bonuspaymentmonth2 { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }
        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
