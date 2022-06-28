#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_newvehiclesalesorderDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 14/01/2020 9:03:06
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_newvehiclesalesorderDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public decimal xts_totalpayment_base { get; set; }

		public DateTime xjp_insuranceexpireddate { get; set; }

		public decimal xts_compulsoryinsuranceamount { get; set; }

		public Guid xts_financingcompanyid { get; set; }

		public string xts_newvehiclesalescertificatenumber { get; set; }

		public decimal xts_vehiclerelatedproductamount_base { get; set; }

		public int statuscode { get; set; }

		public decimal xts_titleregistrationfee_base { get; set; }

		public string xts_recommendedproductidname { get; set; }

		public decimal xts_tradeindiscount { get; set; }

		public string xts_vehiclemanagementdescription { get; set; }

		public decimal ktb_ageyeardiscount_base { get; set; }

		public string xts_insuranceneededname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string ktb_newbenefitidname { get; set; }

		public string xts_registrationrequiredname { get; set; }

		public string xjp_acquisitiontaxidname { get; set; }

		public decimal xts_remainingfinancingamount { get; set; }

		public string xjp_platenumber { get; set; }

		public string xjp_registrationagencybusinessunitidname { get; set; }

		public int importsequencenumber { get; set; }

		public decimal xts_specialcolorpriceamount_base { get; set; }

		public bool xts_overmaximumdiscount { get; set; }

		public bool ktb_nobookingfeedp { get; set; }

		public int xts_potentiallookuptype { get; set; }

		public string xjp_insuranceapplymethodname { get; set; }

		public Guid xts_withholdingtaxid { get; set; }

		public string xts_address3 { get; set; }

		public Guid xts_stockid { get; set; }

		public string xts_owneridyominame { get; set; }

		public decimal xjp_fundmanagementfee_base { get; set; }

		public decimal xts_consumptiontax2amount { get; set; }

		public string xts_usernumber { get; set; }

		public decimal xts_tradeinnetvaluepayment { get; set; }

		public string xts_newvehiclesalesordernumber { get; set; }

		public decimal xts_taxablemiscellaneouschargeamount { get; set; }

		public decimal xts_vehicletaxamount { get; set; }

		public decimal xjp_methodofpaymentestimatedprofit { get; set; }

		public decimal xts_financingamount_base { get; set; }

		public string xts_userdescription { get; set; }

		public Guid xts_billtoid { get; set; }

		public string xts_siteidname { get; set; }

		public string statecodename { get; set; }

		public decimal xts_calculatedpaymentamountpayment { get; set; }

		public DateTime xts_financingpurchaseorderdate { get; set; }

		public Guid xts_vehiclespecificationid { get; set; }

		public decimal xts_interestamount_base { get; set; }

		public decimal xjp_recyclingamount { get; set; }

		public string xts_miscchargestemplateidname { get; set; }

		public string xts_specialcolorpriceidname { get; set; }

		public string xts_financingcompanyidyominame { get; set; }

		public string xts_addressidname { get; set; }

		public decimal xts_firstpaymentamount_base { get; set; }

		public decimal xts_accessoriestaxamount_base { get; set; }

		public string traversedpath { get; set; }

		public string ktb_typecaroseries { get; set; }

		public decimal ktb_ageyeardiscount { get; set; }

		public DateTime xts_vehicleregistrationreceiptdate { get; set; }

		public decimal xjp_automobiletaxamount { get; set; }

		public decimal xts_withholdingtaxamount { get; set; }

		public string ktb_address4 { get; set; }

		public decimal ktb_estimasiharga { get; set; }

		public Guid xts_vehiclepricelistid { get; set; }

		public string xts_gradenumber { get; set; }

		public string xts_vehiclespecificationidname { get; set; }

		public string xts_tradeintaxcategory { get; set; }

		public decimal ktb_ongkoskirim { get; set; }

		public string xjp_taxationformname { get; set; }

		public decimal xts_firstpaymentamount { get; set; }

		public Guid owningbusinessunit { get; set; }

		public decimal xts_accessoriestaxamount { get; set; }

		public decimal xjp_recyclingamount_base { get; set; }

		public string xts_requestplatenumbername { get; set; }

		public decimal xts_withholdingtaxamount_base { get; set; }

		public string xts_customercontactdescription { get; set; }

		public decimal xjp_additionalfluorocarbonfee { get; set; }

		public string ktb_nobookingfeedpname { get; set; }

		public decimal xjp_additionalfluorocarbonfee_base { get; set; }

		public decimal xts_miscellaneouschargestaxamount_base { get; set; }

		public string ktb_additional { get; set; }

		public bool xts_insuranceneeded { get; set; }

		public string xts_consumptiontax1idname { get; set; }

		public string xts_withholdingtax2idname { get; set; }

		public decimal xts_discountamount { get; set; }

		public string xts_gradeidname { get; set; }

		public decimal xts_referralamount_base { get; set; }

		public Guid xjp_servicebusinessunitid { get; set; }

		public string xts_handling { get; set; }

		public decimal xts_consumptiontax1amount_base { get; set; }

		public string xts_userphone { get; set; }

		public string xts_purchaseorderstatus { get; set; }

		public Guid xjp_registrationdocumentrefnumberid { get; set; }

		public decimal xts_bankchargesfee { get; set; }

		public bool xts_matchingrulepassed { get; set; }

		public DateTime xjp_requestedplatenumberdate { get; set; }

		public Int64 versionnumber { get; set; }

		public decimal xjp_vehicleacquisitiontaxamount_base { get; set; }

		public Guid xts_potentialcustomerid { get; set; }

		public string xjp_registrationprocessbyidname { get; set; }

		public Guid xid_lastdocumentregistrationid { get; set; }

		public decimal xts_subsidizeddiscount { get; set; }

		public DateTime xts_assignvsotosalesdate { get; set; }

		public DateTime xts_cancelleddate { get; set; }

		public decimal xts_calculatedpaymentamountpayment_base { get; set; }

		public string xjp_insuranceapplymethod { get; set; }

		public string xts_modelspecification { get; set; }

		public int ktb_dnetid { get; set; }

		public decimal xts_dealerdiscount { get; set; }

		public decimal xjp_airbagfee_base { get; set; }

		public string createdbyyominame { get; set; }

		public decimal xjp_accessoriesestimatedprofitamount_base { get; set; }

		public string xts_nvsonumberregistrationdetailidname { get; set; }

		public string xjp_vehiclemanagementcategory { get; set; }

		public decimal xts_accessoriesdiscountamount_base { get; set; }

		public string xts_useridyominame { get; set; }

		public bool xts_change { get; set; }

		public DateTime xjp_promisedeliverydate { get; set; }

		public DateTime ktb_tanggalkonfirmasi { get; set; }

		public bool xts_downpaymentispaid { get; set; }

		public decimal xts_vehiclerelatedproducttaxamount { get; set; }

		public decimal xjp_additionalshredderdustfee_base { get; set; }

		public decimal xjp_bonuspaymentamount1 { get; set; }

		public string xts_vehicleorderinvoicingname { get; set; }

		public decimal xts_balance_base { get; set; }

		public decimal xts_vehiclediscountamount { get; set; }

		public string xts_othernumber { get; set; }

		public decimal xts_variousamount_base { get; set; }

		public DateTime xjp_registrationexpecteddate { get; set; }

		public decimal xjp_bonuspaymentamount2_base { get; set; }

		public decimal xts_consumptiontax1amount { get; set; }

		public string ktb_leasingidname { get; set; }

		public string xjp_insurancecategoryidname { get; set; }

		public string xjp_servicebusinessunitidname { get; set; }

		public string xjp_vehiclemanagementidname { get; set; }

		public string xts_customernumber { get; set; }

		public bool xts_registrationfeeispaid { get; set; }

		public decimal xts_subsidizedleasing_base { get; set; }

		public decimal ktb_biayamediator { get; set; }

		public string xjp_taxablebusinesscategory { get; set; }

		public decimal xts_taxablemiscellaneouschargeamount_base { get; set; }

		public string xjp_registrationmethodname { get; set; }

		public decimal xjp_voluntaryinsuranceamount { get; set; }

		public DateTime xts_approvaldate { get; set; }

		public string transactioncurrencyidname { get; set; }

		public Guid ktb_leasingid { get; set; }

		public decimal xts_withholdingtax2amount_base { get; set; }

		public string xts_vehiclemanagementcompleteaddress { get; set; }

		public int ktb_iddnet { get; set; }

		public int xts_numberofcopy { get; set; }

		public Guid xts_addressid { get; set; }

		public decimal xts_outstandingamount_base { get; set; }

		public decimal xts_discountamountbeforetax { get; set; }

		public Guid xjp_automobiletaxid { get; set; }

		public string xjp_registrationaddressidname { get; set; }

		public string xts_termsofpaymentidname { get; set; }

		public string xts_wholesaleorderidname { get; set; }

		public decimal xts_accessoriesdiscountamount { get; set; }

		public string createdonbehalfbyname { get; set; }

		public decimal xjp_registrationpaymentamount { get; set; }

		public string xts_productdescription { get; set; }

		public string xts_postalcode { get; set; }

		public decimal xjp_vehicleestimatedcost { get; set; }

		public decimal xts_bookingfeeamount { get; set; }

		public decimal xts_methodofpaymentestimatedsalesamount { get; set; }

		public decimal xjp_methodofpaymentestimatedcost { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public decimal xjp_vehicleacquisitiontaxamount { get; set; }

		public string xjp_registrationdocumentrefnumberidname { get; set; }

		public string xts_productexteriorcoloridname { get; set; }

		public decimal xjp_fundmanagementfee { get; set; }

		public decimal xts_vehiclediscountpercentage { get; set; }

		public DateTime xts_deliverydate { get; set; }

		public string owneridname { get; set; }

		public decimal xts_tradeinrecyclingamount { get; set; }

		public string ktb_vehiclemodelidname { get; set; }

		public string xts_ownernumber { get; set; }

		public string xts_potentialcontactidname { get; set; }

		public decimal xts_nontaxablemiscellaneouschargeamount_base { get; set; }

		public decimal xts_vehiclerelatedproducttaxamount_base { get; set; }

		public Guid ktb_benefitid { get; set; }

		public DateTime xjp_expectedregistrationdate { get; set; }

		public Guid processid { get; set; }

		public decimal xts_administrationfee_base { get; set; }

		public bool xts_fixedprice { get; set; }

		public Guid xjp_platenumbersegment1id { get; set; }

		public string ktb_ftaccessories { get; set; }

		public decimal ktb_cashbackapm { get; set; }

		public decimal xts_maximumdiscountamount { get; set; }

		public string xjp_platenumbersegment1idname { get; set; }

		public decimal exchangerate { get; set; }

		public decimal xts_balance { get; set; }

		public decimal xts_accessoriesnetsalesprice_base { get; set; }

		public decimal xjp_methodofpaymentestimatedcost_base { get; set; }

		public Guid xjp_registrationagencybusinessunitid { get; set; }

		public Guid xts_miscchargestemplateid { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public string xts_eventdata { get; set; }

		public decimal xjp_fluorocarbonfee_base { get; set; }

		public bool xts_registrationrequired { get; set; }

		public decimal xts_consumptiontax2amount_base { get; set; }

		public string xjp_deliverylocationidname { get; set; }

		public string xts_ownerphone { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public Guid xts_termsofpaymentid { get; set; }

		public string ktb_additionalname { get; set; }

		public decimal xts_tradeintaxamount_base { get; set; }

		public string xts_potentialcustomerdescription { get; set; }

		public int xjp_insuranceperiod { get; set; }

		public Guid xts_warehouseid { get; set; }

		public Guid xts_purchaseorderid { get; set; }

		public DateTime xts_matchdate { get; set; }

		public decimal xjp_shredderdustfee { get; set; }

		public Guid xts_gradeid { get; set; }

		public int xts_totalpaymentmonth { get; set; }

		public decimal ktb_estimasiharga_base { get; set; }

		public decimal xjp_shredderdustfee_base { get; set; }

		public decimal xjp_accessoriesacquisitiontaxamount_base { get; set; }

		public decimal xts_vehicletaxamount_base { get; set; }

		public decimal xts_otherdeductionamountpayment_base { get; set; }

		public Guid ktb_vehiclemodelid { get; set; }

		public string xts_vehiclepricelistidname { get; set; }

		public decimal xts_miscellaneouschargestaxamount { get; set; }

		public string xts_phonenumber { get; set; }

		public string xts_changename { get; set; }

		public DateTime xts_preferredregistrationdate { get; set; }

		public Guid xjp_deliverylocationid { get; set; }

		public string xjp_ownershipcategory { get; set; }

		public decimal xts_downpaymentamount { get; set; }

		public string xts_ordertypeidname { get; set; }

		public DateTime xjp_registrationresultdate { get; set; }

		public Guid xts_siteid { get; set; }

		public decimal xjp_vehicleestimatedprofitamount { get; set; }

		public decimal xts_annualpaymentamount { get; set; }

		public decimal xts_totalpayment { get; set; }

		public string xjp_usagecategoryname { get; set; }

		public string xts_log { get; set; }

		public string xts_gradedescription { get; set; }

		public decimal xts_discountamountbeforetax_base { get; set; }

		public string xts_potentialcustomeridname { get; set; }

		public decimal xjp_fluorocarbonfee { get; set; }

		public decimal xjp_variousestimatedprofitamount { get; set; }

		public string xjp_platenumbersegment4 { get; set; }

		public decimal ktb_cashbackapm_base { get; set; }

		public string xts_fixedpricename { get; set; }

		public DateTime createdon { get; set; }

		public string xts_addressnewidname { get; set; }

		public string xjp_platenumbersegment2 { get; set; }

		public string xts_billtoidyominame { get; set; }

		public string xjp_taxationform { get; set; }

		public decimal xts_tradeinvalue { get; set; }

		public DateTime xjp_desireddeliverydate { get; set; }

		public string xjp_vehiclemanagementcategoryname { get; set; }

		public int xts_financingperiod { get; set; }

		public decimal xjp_variousestimatedprofitamount_base { get; set; }

		public string xts_othercompleteaddress { get; set; }

		public Guid xjp_registrationagencyid { get; set; }

		public string xjp_usagecategory { get; set; }

		public decimal xts_subsidizedmaindealer_base { get; set; }

		public string xjp_locationclasscategory { get; set; }

		public string xts_productinteriorcoloridname { get; set; }

		public decimal xts_totalreceiptamount_base { get; set; }

		public decimal xjp_variouscostamount_base { get; set; }

		public string ktb_mediator { get; set; }

		public decimal xts_maximumdiscountamount_base { get; set; }

		public decimal xts_nontaxablemiscellaneouschargeamount { get; set; }

		public Guid xjp_insurancecategoryid { get; set; }

		public DateTime xts_paymentstartdate { get; set; }

		public string xts_locking { get; set; }

		public decimal xts_consumptiontaxtotalamount_base { get; set; }

		public string ktb_dnetspknumber { get; set; }

		public decimal xts_consumptiontaxtotalamount { get; set; }

		public string ktb_rejectedreason { get; set; }

		public string xts_insurancetypename { get; set; }

		public decimal xts_outstandingamount { get; set; }

		public decimal xts_compulsoryinsuranceamount_base { get; set; }

		public string xts_statusname { get; set; }

		public decimal xts_tradeinvalue_base { get; set; }

		public string xts_financingpurchaseordernumber { get; set; }

		public string xjp_vehiclespecificnumber { get; set; }

		public string xts_stockidname { get; set; }

		public bool ktb_isinterfaced { get; set; }

		public string xts_companyphone { get; set; }

		public string createdbyname { get; set; }

		public string xts_productidname { get; set; }

		public Guid xjp_weighttaxid { get; set; }

		public string xts_originalnewvehiclesalesorderreferenceidname { get; set; }

		public Guid xjp_acquisitiontaxid { get; set; }

		public DateTime xts_purchaseorderdate { get; set; }

		public string xts_potentiallookupname { get; set; }

		public int statecode { get; set; }

		public string xts_usercompleteaddress { get; set; }

		public Guid xts_productconfigurationid { get; set; }

		public string xts_vehiclemanagementphone { get; set; }

		public string ktb_pembayaranname { get; set; }

		public string ktb_vehicledescription { get; set; }

		public decimal xjp_accessoriesacquisitiontaxamount { get; set; }

		public decimal xts_subsidizedsoleagent_base { get; set; }

		public string xts_companynumber { get; set; }

		public DateTime xts_vehicleregistrationsubmitdate { get; set; }

		public string xts_productstyleidname { get; set; }

		public decimal xts_tradeinnetvalue { get; set; }

		public Guid xts_methodofpaymentid { get; set; }

		public decimal xts_tradeinnetvalue_base { get; set; }

		public string xts_priceoptionname { get; set; }

		public decimal xts_tradeinrecyclingamount_base { get; set; }

		public Guid xjp_registrationprocessbyid { get; set; }

		public decimal xts_specialcolorpriceamount { get; set; }

		public decimal xts_subsidizedmaindealer { get; set; }

		public Guid xts_productid { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public bool xts_requestplatenumber { get; set; }

		public Guid ktb_newbenefitid { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public decimal xts_downpaymentamount_base { get; set; }

		public string xts_billtodescription { get; set; }

		public decimal xts_totaldiscountamount { get; set; }

		public decimal xjp_totalsalesprice_base { get; set; }

		public decimal xts_interestamount { get; set; }

		public Guid owninguser { get; set; }

		public decimal xjp_totalacquisitiontaxamount_base { get; set; }

		public decimal ktb_ongkoskirim_base { get; set; }

		public Guid xts_newvehiclesalesquoteid { get; set; }

		public string xts_handlingname { get; set; }

		public decimal xts_miscchargebaseamount_base { get; set; }

		public string xts_companydescription { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public decimal xts_variousamount { get; set; }

		public string xts_otheridname { get; set; }

		public decimal xts_vehiclediscountamount_base { get; set; }

		public string modifiedbyname { get; set; }

		public decimal xjp_totalnetsalesamount { get; set; }

		public string xts_insurancetype { get; set; }

		public Guid xts_consumptiontax1id { get; set; }

		public string owneridtype { get; set; }

		public DateTime xts_canceldeliverydate { get; set; }

		public Guid xts_opportunityid { get; set; }

		public string ktb_vehiclecolorname { get; set; }

		public decimal xjp_additionalshredderdustfee { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xid_lastdocumentregistrationidname { get; set; }

		public string xjp_registrationmethod { get; set; }

		public decimal xts_titleregistrationfee { get; set; }

		public decimal xjp_accessoriesestimatedprofitamount { get; set; }

		public decimal xts_vehiclenetsalesprice_base { get; set; }

		public Guid xts_originalnewvehiclesalesorderreferenceid { get; set; }

		public decimal xts_annualpaymentamount_base { get; set; }

		public string xts_personinchargeidname { get; set; }

		public string xts_downpaymentispaidname { get; set; }

		public DateTime xjp_confirmedregistrationpaymentdate { get; set; }

		public decimal xjp_registrationpaymentamount_base { get; set; }

		public string ktb_lkppnumber { get; set; }

		public string xts_matchingrulepassedname { get; set; }

		public bool xts_insurancefreeofcharge { get; set; }

		public decimal xjp_vehicleestimatedprofitamount_base { get; set; }

		public string xjp_vehiclemanagementidyominame { get; set; }

		public Guid xts_consumptiontax2id { get; set; }

		public Guid xts_insurancecompanyid { get; set; }

		public decimal xts_downpaymentamountpayment { get; set; }

		public decimal xjp_accessoriesestimatedtotalcost { get; set; }

		public Guid ktb_opportunitynoid { get; set; }

		public string xts_otheridyominame { get; set; }

		public decimal xts_priceamountbeforetax { get; set; }

		public decimal xjp_totalacquisitiontaxamount { get; set; }

		public string xts_opportunityidname { get; set; }

		public decimal xts_subsidizedleasing { get; set; }

		public Guid xts_newvehiclesalesorderid { get; set; }

		public DateTime xts_certificateoftitlenumberreceiptdate { get; set; }

		public decimal xjp_othertaxamount { get; set; }

		public string xts_otherphone { get; set; }

		public string xts_potentialcustomeridyominame { get; set; }

		public string xts_companyidname { get; set; }

		public string xts_status { get; set; }

		public decimal xjp_totalestimatedprofitamount_base { get; set; }

		public decimal xts_accessoriesnetsalesprice { get; set; }

		public decimal xts_totalpriceamount_base { get; set; }

		public DateTime xts_salesdate { get; set; }

		public string ktb_benefitidname { get; set; }

		public string xts_ownercompleteaddress { get; set; }

		public decimal xts_netamountaftertax_base { get; set; }

		public int xjp_bonuspayment1applymonth { get; set; }

		public string xts_insurancefreeofchargename { get; set; }

		public decimal xjp_vehicleestimatedcost_base { get; set; }

		public decimal xjp_totalestimatedcostamount { get; set; }

		public string xts_billtoidname { get; set; }

		public decimal xts_netamountaftertax { get; set; }

		public decimal xts_otherdeductionamountpayment { get; set; }

		public decimal xjp_totalestimatedprofitamount { get; set; }

		public Guid xts_specialcolorpriceid { get; set; }

		public decimal xts_subsidizeddiscount_base { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public decimal xts_tradeindiscount_base { get; set; }

		public string xjp_taxablebusinesscategoryname { get; set; }

		public decimal xjp_variouscostamount { get; set; }

		public Guid xts_nvsonumberregistrationdetailid { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public Guid ownerid { get; set; }

		public decimal xjp_methodofpaymentestimatedprofit_base { get; set; }

		public decimal xjp_totalnetsalesamount_base { get; set; }

		public string xts_newvehiclesalesquoteidname { get; set; }

		public Guid xts_productinteriorcolorid { get; set; }

		public decimal xts_bankchargesfee_base { get; set; }

		public string xts_potentialcontactidyominame { get; set; }

		public string xts_vehicleregistrationnumber { get; set; }

		public decimal xts_totalreceiptamount { get; set; }

		public decimal xjp_totalestimatedcostamount_base { get; set; }

		public Guid owningteam { get; set; }

		public decimal xts_totaldiscountamount_base { get; set; }

		public DateTime xts_unmatchdate { get; set; }

		public decimal xjp_voluntaryinsuranceamount_base { get; set; }

		public string xts_productconfigurationidname { get; set; }

		public string xjp_automobiletaxidname { get; set; }

		public decimal xts_vehiclenetsalesprice { get; set; }

		public decimal xts_netamount_base { get; set; }

		public decimal xts_administrationfee { get; set; }

		public Guid ktb_programid { get; set; }

		public string xts_owneridname { get; set; }

		public decimal xjp_accessoriesestimatedtotalcost_base { get; set; }

		public string xts_address1 { get; set; }

		public string xts_requestedplatenumber { get; set; }

		public string xts_ownerdescription { get; set; }

		public string xjp_registrationagencylookupname { get; set; }

		public DateTime xts_newvehiclesalescertificatereceiptdate { get; set; }

		public decimal xts_dealerdiscount_base { get; set; }

		public string owneridyominame { get; set; }

		public Guid ktb_karosericompanyid { get; set; }

		public Guid xjp_vehiclemanagementid { get; set; }

		public decimal xjp_bonuspaymentamount2 { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public decimal xts_methodofpaymentestimatedsalesamount_base { get; set; }

		public Guid stageid { get; set; }

		public string ktb_opportunitynoidname { get; set; }

		public decimal xjp_bonuspaymentamount1_base { get; set; }

		public decimal xjp_weighttaxamount { get; set; }

		public decimal xts_remainingfinancingamount_base { get; set; }

		public decimal xts_financingamount { get; set; }

		public Guid xts_ordertypeid { get; set; }

		public Guid xts_salespersonid { get; set; }

		public string xts_billtonumber { get; set; }

		public string xts_billtocompleteaddress { get; set; }

		public Guid xts_productstyleid { get; set; }

		public string xjp_ownershipcategoryname { get; set; }

		public string ktb_programidname { get; set; }

		public decimal xts_discountamount_base { get; set; }

		public string xjp_idempotentmessage { get; set; }

		public string xts_purchaseorderstatusname { get; set; }

		public string ktb_vehiclecategoryidname { get; set; }

		public string xts_classificationnumber { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public decimal xts_tradeinnetvaluepayment_base { get; set; }

		public Guid xts_addressnewid { get; set; }

		public Guid modifiedby { get; set; }

		public Guid xts_wholesaleorderid { get; set; }

		public string ktb_leasingidyominame { get; set; }

		public decimal xts_subsidizedsoleagent { get; set; }

		public decimal xts_priceamountbeforetax_base { get; set; }

		public bool ktb_konfirmasics { get; set; }

		public Guid ktb_campaignid { get; set; }

		public string xts_registrationagencyinvoicenumber { get; set; }

		public string xjp_remark { get; set; }

		public Guid xts_recommendedproductid { get; set; }

		public string xts_methodofpaymentidname { get; set; }

		public string xts_otherdescription { get; set; }

		public DateTime xts_certificateoftitlenumbersubmitdate { get; set; }

		public string xts_useridname { get; set; }

		public decimal xjp_totalsalesprice { get; set; }

		public Guid xts_personinchargeid { get; set; }

		public string xts_vehicleorderinvoicing { get; set; }

		public string xts_companycompleteaddress { get; set; }

		public string xts_billtophone { get; set; }

		public decimal ktb_biayamediator_base { get; set; }

		public string xts_withholdingtaxidname { get; set; }

		public string xts_companyidyominame { get; set; }

		public decimal xjp_informationmanagementfee_base { get; set; }

		public DateTime xts_invoiceduedate { get; set; }

		public string xts_warehouseidname { get; set; }

		public string xts_salespersonidname { get; set; }

		public string ktb_campaignidname { get; set; }

		public DateTime xts_predeliveryinspectioncancellationdate { get; set; }

		public string xts_insurancecompanyidname { get; set; }

		public string xts_consumptiontax2idname { get; set; }

		public string ktb_karosericompanyidname { get; set; }

		public Guid xts_withholdingtax2id { get; set; }

		public decimal xts_accessoriesamount_base { get; set; }

		public Guid xts_userid { get; set; }

		public string xjp_vehiclemanagementnumber { get; set; }

		public DateTime xts_paymentenddate { get; set; }

		public string ktb_typecaroseriesname { get; set; }

		public string xts_matchingtypename { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_alreadydeliveredname { get; set; }

		public decimal xjp_othertaxamount_base { get; set; }

		public decimal xts_bookingfeeamount_base { get; set; }

		public string xts_address2 { get; set; }

		public decimal xts_withholdingtax2amount { get; set; }

		public Guid ktb_vehiclecategoryid { get; set; }

		public Guid xjp_registrationaddressid { get; set; }

		public decimal xjp_informationmanagementfee { get; set; }

		public string xjp_platenumbersegment3 { get; set; }

		public string ktb_remarks { get; set; }

		public Guid xts_companyid { get; set; }

		public string xts_purchaseorderidname { get; set; }

		public decimal xts_totalpriceamount { get; set; }

		public Guid xts_salesorderid { get; set; }

		public decimal xts_referralamount { get; set; }

		public string ktb_pembayaran { get; set; }

		public DateTime xjp_expectedpaymentdate { get; set; }

		public string xjp_weighttaxidname { get; set; }

		public string xts_overmaximumdiscountname { get; set; }

		public decimal xjp_automobiletaxamount_base { get; set; }

		public DateTime xts_scheduleddeliverydate { get; set; }

		public DateTime xts_newvehiclesalescertificatesubmitdate { get; set; }

		public decimal xjp_weighttaxamount_base { get; set; }

		public DateTime xjp_confirmedpaymentdate { get; set; }

		public string statuscodename { get; set; }

		public string xts_registrationfeeispaidname { get; set; }

		public DateTime xjp_expectedregistrationpaymentdate { get; set; }

		public decimal xts_vehiclerelatedproductamount { get; set; }

		public decimal xts_accessoriesamount { get; set; }

		public decimal xts_miscchargebaseamount { get; set; }

		public string xts_salesorderidname { get; set; }

		public Guid xts_otherid { get; set; }

		public DateTime modifiedon { get; set; }

		public string xts_matchingtype { get; set; }

		public string xjp_locationclasscategoryname { get; set; }

		public string xjp_productsubsidycategory { get; set; }

		public Guid xjp_registrationrequestnumberid { get; set; }

		public string xts_businessunitidname { get; set; }

		public decimal xts_downpaymentamountpayment_base { get; set; }

		public string xts_priceoption { get; set; }

		public int xjp_registrationagencylookuptype { get; set; }

		public string modifiedbyyominame { get; set; }

		public string ktb_konfirmasicsname { get; set; }

		public Guid createdonbehalfby { get; set; }

		public decimal xjp_airbagfee { get; set; }

		public DateTime xjp_desiredregistrationdate { get; set; }

		public decimal xts_interestpercentage { get; set; }

		public bool xts_alreadydelivered { get; set; }

		public decimal xts_vehicleamount_base { get; set; }

		public string xjp_registrationrequestnumberidname { get; set; }

		public Guid xts_potentialcontactid { get; set; }

		public DateTime xts_vehicleregistrationdeliverydate { get; set; }

		public decimal xts_vehicleamount { get; set; }

		public Guid createdby { get; set; }

		public decimal xts_netamount { get; set; }

		public string xts_certificateoftitlenumber { get; set; }

		public string xts_tradeintaxcategoryname { get; set; }

		public decimal xts_tradeintaxamount { get; set; }

		public Guid xts_ownerid { get; set; }

		public int xjp_bonuspayment2applymonth { get; set; }

		public string xts_financingcompanyidname { get; set; }

		public string xjp_registrationagencyidname { get; set; }

		public string ktb_externalcode { get; set; }

		public string ktb_skenariopenjualan { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
