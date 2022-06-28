#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_goodsreceiptParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 08:28:00
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
    public class VWI_CRM_xts_goodsreceiptParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public Guid xts_goodsreceiptid { get; set; }
        [AntiXss]
        public Guid xjp_weighttaxid { get; set; }
        [AntiXss]
        public decimal xjp_standardmarketsellingprice { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public Guid xjp_initialassessmentbusinessunitid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public decimal xts_purchasepricebaseamount_base { get; set; }
        [AntiXss]
        public decimal xjp_finaltotaldeposit { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmenttradeinamount { get; set; }
        [AntiXss]
        public decimal xjp_initialthecapitalmanagementcharge_base { get; set; }
        [AntiXss]
        public string xjp_initialairbagequipmentname { get; set; }
        [AntiXss]
        public int xjp_finalassessmentmileage { get; set; }
        [AntiXss]
        public string xjp_finalairbagequipment { get; set; }
        [AntiXss]
        public string xjp_initialassessmentpicidname { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string xjp_useridyominame { get; set; }
        [AntiXss]
        public decimal xts_purchaseprice_base { get; set; }
        [AntiXss]
        public string xjp_taxpaymentcertificatename { get; set; }
        [AntiXss]
        public string xts_vehiclemodelidname { get; set; }
        [AntiXss]
        public string xts_parentbusinessunitidname { get; set; }
        [AntiXss]
        public Guid xts_siteid { get; set; }
        [AntiXss]
        public string xjp_finalrecyclingobjectsname { get; set; }
        [AntiXss]
        public decimal xjp_automobiletaxamount { get; set; }
        [AntiXss]
        public Guid processid { get; set; }
        [AntiXss]
        public string xts_hasapvoucher { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmenttradeinamount_base { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public decimal xts_purchasepricebaseamount { get; set; }
        [AntiXss]
        public string xts_goodsreceiptclassification { get; set; }
        [AntiXss]
        public string xjp_initialfluorocarbonequipmentname { get; set; }
        [AntiXss]
        public decimal xjp_initialshredderdustfee { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmenttradeinamount_base { get; set; }
        [AntiXss]
        public string xjp_initialfluorocarbonclassificationname { get; set; }
        [AntiXss]
        public string xjp_initialairbagequipment { get; set; }
        [AntiXss]
        public Guid xts_consumptiontaxid { get; set; }
        [AntiXss]
        public string xjp_comment { get; set; }
        [AntiXss]
        public bool xjp_transferbook { get; set; }
        [AntiXss]
        public string xjp_recyclingticketname { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmentpricemeter { get; set; }
        [AntiXss]
        public decimal xjp_initialpurchaseprice_base { get; set; }
        [AntiXss]
        public bool xjp_calichangeapprovalbill { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public decimal xjp_finaltotalnondeposit_base { get; set; }
        [AntiXss]
        public string xts_assessmentcompletionstatusname { get; set; }
        [AntiXss]
        public string xjp_theletterofattorneyname { get; set; }
        [AntiXss]
        public DateTime xts_transactiondate { get; set; }
        [AntiXss]
        public decimal xjp_standardmarketsellingprice_base { get; set; }
        [AntiXss]
        public string xts_supplieridyominame { get; set; }
        [AntiXss]
        public string xjp_finalfluorocarbonclassificationname { get; set; }
        [AntiXss]
        public string xjp_finalassessmentbusinessunitidname { get; set; }
        [AntiXss]
        public decimal xjp_initialshredderdustfee_base { get; set; }
        [AntiXss]
        public string xts_usedvehiclesalesorderlookupname { get; set; }
        [AntiXss]
        public string xjp_officiallyregisteredsealname { get; set; }
        [AntiXss]
        public string xts_consumptiontax { get; set; }
        [AntiXss]
        public bool xjp_finalendoflifevehicle { get; set; }
        [AntiXss]
        public decimal xjp_vehicleschedulecost_base { get; set; }
        [AntiXss]
        public string xts_personinchargeidname { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmentmanualmeasure3_base { get; set; }
        [AntiXss]
        public decimal xjp_storecost { get; set; }
        [AntiXss]
        public Guid xjp_antiqueledgerid { get; set; }
        [AntiXss]
        public string xjp_weighttaxidname { get; set; }
        [AntiXss]
        public decimal xjp_initialtotaldeposit { get; set; }
        [AntiXss]
        public string xts_hasapvouchername { get; set; }
        [AntiXss]
        public decimal xjp_initialairbagdepositedamount { get; set; }
        [AntiXss]
        public string xjp_useridname { get; set; }
        [AntiXss]
        public string xjp_finaldepositedpaymentcategoryname { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public decimal xjp_finalfluorocarbondepositedamount { get; set; }
        [AntiXss]
        public Guid xts_vehicletransactionhistoryid { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmentmanualmeasure1_base { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public decimal xjp_vehicleschedulecost { get; set; }
        [AntiXss]
        public DateTime xjp_documentcompleteddate { get; set; }
        [AntiXss]
        public string xts_handlingname { get; set; }
        [AntiXss]
        public bool xjp_initialendoflifevehicle { get; set; }
        [AntiXss]
        public string xjp_antiqueledgeridname { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public Guid xjp_ownerid { get; set; }
        [AntiXss]
        public string xts_goodsreceiptnumber { get; set; }
        [AntiXss]
        public string xjp_initialassessmentgoodsreceiptclassify { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public DateTime xjp_documenteffectivedate { get; set; }
        [AntiXss]
        public string xjp_consumptiontaxcategory { get; set; }
        [AntiXss]
        public string xjp_finalairbagequipmentname { get; set; }
        [AntiXss]
        public string xts_newvehiclesalesorderidname { get; set; }
        [AntiXss]
        public string xjp_selection { get; set; }
        [AntiXss]
        public decimal xts_sellingprice_base { get; set; }
        [AntiXss]
        public bool xjp_theletterofattorney { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmentnewcarmeasure_base { get; set; }
        [AntiXss]
        public decimal xjp_finalpurchaseprice { get; set; }
        [AntiXss]
        public string xjp_initialassessmentgoodsreceiptclassifyname { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public string xjp_carinspectioninstruction { get; set; }
        [AntiXss]
        public string xjp_documentcompletionstatusname { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmenttradeinamount { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_firstyearregistration { get; set; }
        [AntiXss]
        public string xts_stockidname { get; set; }
        [AntiXss]
        public decimal xjp_finalinformationmanagementcharge_base { get; set; }
        [AntiXss]
        public string xjp_consumptiontaxcategoryname { get; set; }
        [AntiXss]
        public string xjp_automobiletaxidname { get; set; }
        [AntiXss]
        public int xts_guaranteemileage { get; set; }
        [AntiXss]
        public string xjp_finalfluorocarbonequipmentname { get; set; }
        [AntiXss]
        public int xjp_initialassessmentmileagebeforeexchanges { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmentnewcarmeasure { get; set; }
        [AntiXss]
        public string xjp_redeemname { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmentmanualmeasure1 { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmentmanualmeasure2 { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmentmanualmeasure3 { get; set; }
        [AntiXss]
        public decimal xts_wholesaleprice_base { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmentnewcarmeasure { get; set; }
        [AntiXss]
        public Guid xts_stockid { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmentnewcarmeasure_base { get; set; }
        [AntiXss]
        public decimal xjp_finalairbagdepositedamount { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmentmanualmeasure1_base { get; set; }
        [AntiXss]
        public string xjp_owneridyominame { get; set; }
        [AntiXss]
        public Guid xts_newvehiclesalesorderid { get; set; }
        [AntiXss]
        public DateTime xts_warrantyperiod { get; set; }
        [AntiXss]
        public string xts_goodsreceiptclassificationname { get; set; }
        [AntiXss]
        public string xjp_idempotentmessage { get; set; }
        [AntiXss]
        public decimal xjp_initialfluorocarbondepositedamount_base { get; set; }
        [AntiXss]
        public string xjp_initialdepositedpaymentcategory { get; set; }
        [AntiXss]
        public DateTime xjp_finalassessmentdate { get; set; }
        [AntiXss]
        public string xjp_finalassessmentgoodsreceiptclassification { get; set; }
        [AntiXss]
        public string xjp_newcarguaranteesuccession { get; set; }
        [AntiXss]
        public decimal xjp_finalshredderdustfee { get; set; }
        [AntiXss]
        public string xjp_firstyearregistrationjapan { get; set; }
        [AntiXss]
        public DateTime xts_carinspectionexpirationdate { get; set; }
        [AntiXss]
        public decimal xjp_storecost_base { get; set; }
        [AntiXss]
        public Guid xjp_automobiletaxid { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public Guid xts_supplierid { get; set; }
        [AntiXss]
        public decimal xts_consumptiontaxamount { get; set; }
        [AntiXss]
        public int xjp_initialassessmentmileage { get; set; }
        [AntiXss]
        public string xjp_initialrecyclingobjectsname { get; set; }
        [AntiXss]
        public Guid xts_locationid { get; set; }
        [AntiXss]
        public string xjp_initialrecyclingobject { get; set; }
        [AntiXss]
        public Guid xts_warrantytypeid { get; set; }
        [AntiXss]
        public Guid xts_initialassessmentid { get; set; }
        [AntiXss]
        public string xjp_finalfluorocarbonclassification { get; set; }
        [AntiXss]
        public Guid xts_salesorderid { get; set; }
        [AntiXss]
        public Guid xts_finalassessmentid { get; set; }
        [AntiXss]
        public string xts_initialassessmentidname { get; set; }
        [AntiXss]
        public string xjp_carinspectionexpirationdatejapan { get; set; }
        [AntiXss]
        public DateTime xts_suppliervendorinvoicedate { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public Guid xts_personinchargeid { get; set; }
        [AntiXss]
        public string xjp_transferbookname { get; set; }
        [AntiXss]
        public Guid xjp_finalassessmentbusinessunitid { get; set; }
        [AntiXss]
        public int xjp_laststatus { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmentmanualmeasure1 { get; set; }
        [AntiXss]
        public Guid xjp_userid { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmentmanualmeasure3 { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmentmanualmeasure2 { get; set; }
        [AntiXss]
        public string xjp_newcarguaranteesuccessionname { get; set; }
        [AntiXss]
        public string xjp_initialdepositedpaymentcategoryname { get; set; }
        [AntiXss]
        public string xjp_specificproductionyearforuvmireport { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmentmanualmeasure2_base { get; set; }
        [AntiXss]
        public string xts_supplierdescription { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmentpricemeter_base { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmentpricemeter { get; set; }
        [AntiXss]
        public string xjp_initialassessmentrunningmeterexchangename { get; set; }
        [AntiXss]
        public decimal xts_purchasepricetotal_base { get; set; }
        [AntiXss]
        public decimal xjp_finaltotalnondeposit { get; set; }
        [AntiXss]
        public int xts_usedvehiclesalesorderlookuptype { get; set; }
        [AntiXss]
        public string xjp_residentscardname { get; set; }
        [AntiXss]
        public decimal xts_sellingprice { get; set; }
        [AntiXss]
        public string xts_consumptiontaxname { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmentmanualmeasure2_base { get; set; }
        [AntiXss]
        public string xjp_grade { get; set; }
        [AntiXss]
        public bool xjp_compulsoryinsurancecertificate { get; set; }
        [AntiXss]
        public Guid xts_vehiclemodelid { get; set; }
        [AntiXss]
        public Guid xts_parentbusinessunitid { get; set; }
        [AntiXss]
        public int xjp_finalassessmentmileagebeforeexchanges { get; set; }
        [AntiXss]
        public string xjp_salepricecompletionstatusname { get; set; }
        [AntiXss]
        public string xts_handling { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public decimal xjp_finalinformationmanagementcharge { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public bool xjp_automobileinspectioncertificate { get; set; }
        [AntiXss]
        public decimal xts_internalsalesprice_base { get; set; }
        [AntiXss]
        public bool xjp_residentscard { get; set; }
        [AntiXss]
        public string xjp_finalassessmentstatusname { get; set; }
        [AntiXss]
        public decimal xjp_finalairbagdepositedamount_base { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public decimal xts_purchasepricetotal { get; set; }
        [AntiXss]
        public decimal xjp_initialinformationmanagementcharge { get; set; }
        [AntiXss]
        public DateTime xjp_initialassessmentdate { get; set; }
        [AntiXss]
        public string xjp_initialassessmentbusinessunitidname { get; set; }
        [AntiXss]
        public decimal xjp_initialassessmentpricemeter_base { get; set; }
        [AntiXss]
        public decimal xjp_finalpurchaseprice_base { get; set; }
        [AntiXss]
        public Guid xts_exteriorcolorid { get; set; }
        [AntiXss]
        public bool xjp_officiallyregisteredseal { get; set; }
        [AntiXss]
        public string xjp_owneridname { get; set; }
        [AntiXss]
        public string xjp_selectionname { get; set; }
        [AntiXss]
        public string traversedpath { get; set; }
        [AntiXss]
        public string xts_supplieridname { get; set; }
        [AntiXss]
        public string xjp_newvehicleguaranteesuccessionname { get; set; }
        [AntiXss]
        public decimal xjp_finaltotaldeposit_base { get; set; }
        [AntiXss]
        public Guid xjp_initialassessmentpicid { get; set; }
        [AntiXss]
        public string xjp_finalrecyclingobjectname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public bool xjp_taxpaymentcertificate { get; set; }
        [AntiXss]
        public Guid xjp_nonsalesdeliveryid { get; set; }
        [AntiXss]
        public decimal xts_consumptiontaxamount_base { get; set; }
        [AntiXss]
        public string xjp_incompletedocument1 { get; set; }
        [AntiXss]
        public string xjp_documentcompletionstatus { get; set; }
        [AntiXss]
        public string xjp_incompletedocument2 { get; set; }
        [AntiXss]
        public string xjp_recognizedmodelforuvmireport { get; set; }
        [AntiXss]
        public Guid xjp_finalassessmentpicid { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public decimal xjp_finalshredderdustfee_base { get; set; }
        [AntiXss]
        public string xts_exteriorcoloridname { get; set; }
        [AntiXss]
        public Guid stageid { get; set; }
        [AntiXss]
        public decimal xjp_finalassessmentmanualmeasure3_base { get; set; }
        [AntiXss]
        public string xjp_finalassessmentrunningmeterexchange { get; set; }
        [AntiXss]
        public string xjp_finaldepositedpaymentcategory { get; set; }
        [AntiXss]
        public string xts_suppliervendorinvoicenumber { get; set; }
        [AntiXss]
        public string xts_warrantytypeidname { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsurancecertificatename { get; set; }
        [AntiXss]
        public decimal xjp_initialtotalnondeposit { get; set; }
        [AntiXss]
        public decimal xjp_automobiletaxamount_base { get; set; }
        [AntiXss]
        public string xjp_initialfluorocarbonequipment { get; set; }
        [AntiXss]
        public Guid xts_warehouseid { get; set; }
        [AntiXss]
        public string xjp_initialassessmentrunningmeterexchange { get; set; }
        [AntiXss]
        public decimal xjp_initialairbagdepositedamount_base { get; set; }
        [AntiXss]
        public string xts_vendoridname { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public decimal xjp_initialinformationmanagementcharge_base { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string xts_siteidname { get; set; }
        [AntiXss]
        public string xts_locationidname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public string xts_platenumber { get; set; }
        [AntiXss]
        public string xjp_calichangeapprovalbillname { get; set; }
        [AntiXss]
        public decimal xjp_finalthecapitalmanagementcharge { get; set; }
        [AntiXss]
        public string xjp_initialfluorocarbonclassification { get; set; }
        [AntiXss]
        public string xts_vehicletransactionhistoryidname { get; set; }
        [AntiXss]
        public string xjp_newvehicleguaranteesuccession { get; set; }
        [AntiXss]
        public string xjp_finalassessmentstatus { get; set; }
        [AntiXss]
        public string xjp_initialrecyclingobjectname { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string xjp_finalrecyclingobject { get; set; }
        [AntiXss]
        public decimal xts_wholesaleprice { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string xjp_carinspectioninstructionname { get; set; }
        [AntiXss]
        public string xjp_nonsalesdeliveryidname { get; set; }
        [AntiXss]
        public Guid xts_vendorid { get; set; }
        [AntiXss]
        public string xjp_finalassessmentpicidname { get; set; }
        [AntiXss]
        public string xts_finalassessmentidname { get; set; }
        [AntiXss]
        public decimal xjp_finalthecapitalmanagementcharge_base { get; set; }
        [AntiXss]
        public decimal xts_minimumsalespriceforretail_base { get; set; }
        [AntiXss]
        public bool xjp_recyclingticket { get; set; }
        [AntiXss]
        public decimal xts_internalsalesprice { get; set; }
        [AntiXss]
        public decimal xjp_weighttaxamount { get; set; }
        [AntiXss]
        public decimal xjp_initialfluorocarbondepositedamount { get; set; }
        [AntiXss]
        public string xjp_finalassessmentgoodsreceiptclassificationname { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public bool xjp_redeem { get; set; }
        [AntiXss]
        public string xts_warehouseidname { get; set; }
        [AntiXss]
        public decimal xjp_initialtotalnondeposit_base { get; set; }
        [AntiXss]
        public string xts_salesorderidname { get; set; }
        [AntiXss]
        public decimal xts_minimumsalespriceforretail { get; set; }
        [AntiXss]
        public string xts_status { get; set; }
        [AntiXss]
        public string xjp_finalfluorocarbonequipment { get; set; }
        [AntiXss]
        public string xjp_initialrecyclingobjects { get; set; }
        [AntiXss]
        public string xts_statusname { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public decimal xjp_initialthecapitalmanagementcharge { get; set; }
        [AntiXss]
        public string xjp_finalrecyclingobjects { get; set; }
        [AntiXss]
        public string xjp_finalendoflifevehiclename { get; set; }
        [AntiXss]
        public string xts_assessmentcompletionstatus { get; set; }
        [AntiXss]
        public decimal xjp_weighttaxamount_base { get; set; }
        [AntiXss]
        public string xjp_automobileinspectioncertificatename { get; set; }
        [AntiXss]
        public string xjp_initialendoflifevehiclename { get; set; }
        [AntiXss]
        public decimal xts_purchaseprice { get; set; }
        [AntiXss]
        public decimal xjp_finalfluorocarbondepositedamount_base { get; set; }
        [AntiXss]
        public string xjp_salepricecompletionstatus { get; set; }
        [AntiXss]
        public decimal xjp_initialtotaldeposit_base { get; set; }
        [AntiXss]
        public string xjp_finalassessmentrunningmeterexchangename { get; set; }
        [AntiXss]
        public string xts_consumptiontaxidname { get; set; }
        [AntiXss]
        public decimal xjp_initialpurchaseprice { get; set; }
        [AntiXss]
        public string xts_vehicleidentificationnumber { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
