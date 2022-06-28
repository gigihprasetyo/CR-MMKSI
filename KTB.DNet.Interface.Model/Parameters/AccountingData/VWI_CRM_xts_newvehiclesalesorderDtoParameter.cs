#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_newvehiclesalesorderParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_newvehiclesalesorderParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public decimal xts_totalpayment_base { get; set; }

		[AntiXss]
		public DateTime xjp_insuranceexpireddate { get; set; }

		[AntiXss]
		public decimal xts_compulsoryinsuranceamount { get; set; }

		[AntiXss]
		public Guid xts_financingcompanyid { get; set; }

		[AntiXss]
		public string xts_newvehiclesalescertificatenumber { get; set; }

		[AntiXss]
		public decimal xts_vehiclerelatedproductamount_base { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee_base { get; set; }

		[AntiXss]
		public string xts_recommendedproductidname { get; set; }

		[AntiXss]
		public decimal xts_tradeindiscount { get; set; }

		[AntiXss]
		public string xts_vehiclemanagementdescription { get; set; }

		[AntiXss]
		public decimal ktb_ageyeardiscount_base { get; set; }

		[AntiXss]
		public string xts_insuranceneededname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string ktb_newbenefitidname { get; set; }

		[AntiXss]
		public string xts_registrationrequiredname { get; set; }

		[AntiXss]
		public string xjp_acquisitiontaxidname { get; set; }

		[AntiXss]
		public decimal xts_remainingfinancingamount { get; set; }

		[AntiXss]
		public string xjp_platenumber { get; set; }

		[AntiXss]
		public string xjp_registrationagencybusinessunitidname { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public decimal xts_specialcolorpriceamount_base { get; set; }

		[AntiXss]
		public bool xts_overmaximumdiscount { get; set; }

		[AntiXss]
		public bool ktb_nobookingfeedp { get; set; }

		[AntiXss]
		public int xts_potentiallookuptype { get; set; }

		[AntiXss]
		public string xjp_insuranceapplymethodname { get; set; }

		[AntiXss]
		public Guid xts_withholdingtaxid { get; set; }

		[AntiXss]
		public string xts_address3 { get; set; }

		[AntiXss]
		public Guid xts_stockid { get; set; }

		[AntiXss]
		public string xts_owneridyominame { get; set; }

		[AntiXss]
		public decimal xjp_fundmanagementfee_base { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount { get; set; }

		[AntiXss]
		public string xts_usernumber { get; set; }

		[AntiXss]
		public decimal xts_tradeinnetvaluepayment { get; set; }

		[AntiXss]
		public string xts_newvehiclesalesordernumber { get; set; }

		[AntiXss]
		public decimal xts_taxablemiscellaneouschargeamount { get; set; }

		[AntiXss]
		public decimal xts_vehicletaxamount { get; set; }

		[AntiXss]
		public decimal xjp_methodofpaymentestimatedprofit { get; set; }

		[AntiXss]
		public decimal xts_financingamount_base { get; set; }

		[AntiXss]
		public string xts_userdescription { get; set; }

		[AntiXss]
		public Guid xts_billtoid { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xts_calculatedpaymentamountpayment { get; set; }

		[AntiXss]
		public DateTime xts_financingpurchaseorderdate { get; set; }

		[AntiXss]
		public Guid xts_vehiclespecificationid { get; set; }

		[AntiXss]
		public decimal xts_interestamount_base { get; set; }

		[AntiXss]
		public decimal xjp_recyclingamount { get; set; }

		[AntiXss]
		public string xts_miscchargestemplateidname { get; set; }

		[AntiXss]
		public string xts_specialcolorpriceidname { get; set; }

		[AntiXss]
		public string xts_financingcompanyidyominame { get; set; }

		[AntiXss]
		public string xts_addressidname { get; set; }

		[AntiXss]
		public decimal xts_firstpaymentamount_base { get; set; }

		[AntiXss]
		public decimal xts_accessoriestaxamount_base { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string ktb_typecaroseries { get; set; }

		[AntiXss]
		public decimal ktb_ageyeardiscount { get; set; }

		[AntiXss]
		public DateTime xts_vehicleregistrationreceiptdate { get; set; }

		[AntiXss]
		public decimal xjp_automobiletaxamount { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount { get; set; }

		[AntiXss]
		public string ktb_address4 { get; set; }

		[AntiXss]
		public decimal ktb_estimasiharga { get; set; }

		[AntiXss]
		public Guid xts_vehiclepricelistid { get; set; }

		[AntiXss]
		public string xts_gradenumber { get; set; }

		[AntiXss]
		public string xts_vehiclespecificationidname { get; set; }

		[AntiXss]
		public string xts_tradeintaxcategory { get; set; }

		[AntiXss]
		public decimal ktb_ongkoskirim { get; set; }

		[AntiXss]
		public string xjp_taxationformname { get; set; }

		[AntiXss]
		public decimal xts_firstpaymentamount { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public decimal xts_accessoriestaxamount { get; set; }

		[AntiXss]
		public decimal xjp_recyclingamount_base { get; set; }

		[AntiXss]
		public string xts_requestplatenumbername { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount_base { get; set; }

		[AntiXss]
		public string xts_customercontactdescription { get; set; }

		[AntiXss]
		public decimal xjp_additionalfluorocarbonfee { get; set; }

		[AntiXss]
		public string ktb_nobookingfeedpname { get; set; }

		[AntiXss]
		public decimal xjp_additionalfluorocarbonfee_base { get; set; }

		[AntiXss]
		public decimal xts_miscellaneouschargestaxamount_base { get; set; }

		[AntiXss]
		public string ktb_additional { get; set; }

		[AntiXss]
		public bool xts_insuranceneeded { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public string xts_withholdingtax2idname { get; set; }

		[AntiXss]
		public decimal xts_discountamount { get; set; }

		[AntiXss]
		public string xts_gradeidname { get; set; }

		[AntiXss]
		public decimal xts_referralamount_base { get; set; }

		[AntiXss]
		public Guid xjp_servicebusinessunitid { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount_base { get; set; }

		[AntiXss]
		public string xts_userphone { get; set; }

		[AntiXss]
		public string xts_purchaseorderstatus { get; set; }

		[AntiXss]
		public Guid xjp_registrationdocumentrefnumberid { get; set; }

		[AntiXss]
		public decimal xts_bankchargesfee { get; set; }

		[AntiXss]
		public bool xts_matchingrulepassed { get; set; }

		[AntiXss]
		public DateTime xjp_requestedplatenumberdate { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public decimal xjp_vehicleacquisitiontaxamount_base { get; set; }

		[AntiXss]
		public Guid xts_potentialcustomerid { get; set; }

		[AntiXss]
		public string xjp_registrationprocessbyidname { get; set; }

		[AntiXss]
		public Guid xid_lastdocumentregistrationid { get; set; }

		[AntiXss]
		public decimal xts_subsidizeddiscount { get; set; }

		[AntiXss]
		public DateTime xts_assignvsotosalesdate { get; set; }

		[AntiXss]
		public DateTime xts_cancelleddate { get; set; }

		[AntiXss]
		public decimal xts_calculatedpaymentamountpayment_base { get; set; }

		[AntiXss]
		public string xjp_insuranceapplymethod { get; set; }

		[AntiXss]
		public string xts_modelspecification { get; set; }

		[AntiXss]
		public int ktb_dnetid { get; set; }

		[AntiXss]
		public decimal xts_dealerdiscount { get; set; }

		[AntiXss]
		public decimal xjp_airbagfee_base { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesestimatedprofitamount_base { get; set; }

		[AntiXss]
		public string xts_nvsonumberregistrationdetailidname { get; set; }

		[AntiXss]
		public string xjp_vehiclemanagementcategory { get; set; }

		[AntiXss]
		public decimal xts_accessoriesdiscountamount_base { get; set; }

		[AntiXss]
		public string xts_useridyominame { get; set; }

		[AntiXss]
		public bool xts_change { get; set; }

		[AntiXss]
		public DateTime xjp_promisedeliverydate { get; set; }

		[AntiXss]
		public DateTime ktb_tanggalkonfirmasi { get; set; }

		[AntiXss]
		public bool xts_downpaymentispaid { get; set; }

		[AntiXss]
		public decimal xts_vehiclerelatedproducttaxamount { get; set; }

		[AntiXss]
		public decimal xjp_additionalshredderdustfee_base { get; set; }

		[AntiXss]
		public decimal xjp_bonuspaymentamount1 { get; set; }

		[AntiXss]
		public string xts_vehicleorderinvoicingname { get; set; }

		[AntiXss]
		public decimal xts_balance_base { get; set; }

		[AntiXss]
		public decimal xts_vehiclediscountamount { get; set; }

		[AntiXss]
		public string xts_othernumber { get; set; }

		[AntiXss]
		public decimal xts_variousamount_base { get; set; }

		[AntiXss]
		public DateTime xjp_registrationexpecteddate { get; set; }

		[AntiXss]
		public decimal xjp_bonuspaymentamount2_base { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount { get; set; }

		[AntiXss]
		public string ktb_leasingidname { get; set; }

		[AntiXss]
		public string xjp_insurancecategoryidname { get; set; }

		[AntiXss]
		public string xjp_servicebusinessunitidname { get; set; }

		[AntiXss]
		public string xjp_vehiclemanagementidname { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public bool xts_registrationfeeispaid { get; set; }

		[AntiXss]
		public decimal xts_subsidizedleasing_base { get; set; }

		[AntiXss]
		public decimal ktb_biayamediator { get; set; }

		[AntiXss]
		public string xjp_taxablebusinesscategory { get; set; }

		[AntiXss]
		public decimal xts_taxablemiscellaneouschargeamount_base { get; set; }

		[AntiXss]
		public string xjp_registrationmethodname { get; set; }

		[AntiXss]
		public decimal xjp_voluntaryinsuranceamount { get; set; }

		[AntiXss]
		public DateTime xts_approvaldate { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public Guid ktb_leasingid { get; set; }

		[AntiXss]
		public decimal xts_withholdingtax2amount_base { get; set; }

		[AntiXss]
		public string xts_vehiclemanagementcompleteaddress { get; set; }

		[AntiXss]
		public int ktb_iddnet { get; set; }

		[AntiXss]
		public int xts_numberofcopy { get; set; }

		[AntiXss]
		public Guid xts_addressid { get; set; }

		[AntiXss]
		public decimal xts_outstandingamount_base { get; set; }

		[AntiXss]
		public decimal xts_discountamountbeforetax { get; set; }

		[AntiXss]
		public Guid xjp_automobiletaxid { get; set; }

		[AntiXss]
		public string xjp_registrationaddressidname { get; set; }

		[AntiXss]
		public string xts_termsofpaymentidname { get; set; }

		[AntiXss]
		public string xts_wholesaleorderidname { get; set; }

		[AntiXss]
		public decimal xts_accessoriesdiscountamount { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public decimal xjp_registrationpaymentamount { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public string xts_postalcode { get; set; }

		[AntiXss]
		public decimal xjp_vehicleestimatedcost { get; set; }

		[AntiXss]
		public decimal xts_bookingfeeamount { get; set; }

		[AntiXss]
		public decimal xts_methodofpaymentestimatedsalesamount { get; set; }

		[AntiXss]
		public decimal xjp_methodofpaymentestimatedcost { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public decimal xjp_vehicleacquisitiontaxamount { get; set; }

		[AntiXss]
		public string xjp_registrationdocumentrefnumberidname { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public decimal xjp_fundmanagementfee { get; set; }

		[AntiXss]
		public decimal xts_vehiclediscountpercentage { get; set; }

		[AntiXss]
		public DateTime xts_deliverydate { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public decimal xts_tradeinrecyclingamount { get; set; }

		[AntiXss]
		public string ktb_vehiclemodelidname { get; set; }

		[AntiXss]
		public string xts_ownernumber { get; set; }

		[AntiXss]
		public string xts_potentialcontactidname { get; set; }

		[AntiXss]
		public decimal xts_nontaxablemiscellaneouschargeamount_base { get; set; }

		[AntiXss]
		public decimal xts_vehiclerelatedproducttaxamount_base { get; set; }

		[AntiXss]
		public Guid ktb_benefitid { get; set; }

		[AntiXss]
		public DateTime xjp_expectedregistrationdate { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public decimal xts_administrationfee_base { get; set; }

		[AntiXss]
		public bool xts_fixedprice { get; set; }

		[AntiXss]
		public Guid xjp_platenumbersegment1id { get; set; }

		[AntiXss]
		public string ktb_ftaccessories { get; set; }

		[AntiXss]
		public decimal ktb_cashbackapm { get; set; }

		[AntiXss]
		public decimal xts_maximumdiscountamount { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment1idname { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_balance { get; set; }

		[AntiXss]
		public decimal xts_accessoriesnetsalesprice_base { get; set; }

		[AntiXss]
		public decimal xjp_methodofpaymentestimatedcost_base { get; set; }

		[AntiXss]
		public Guid xjp_registrationagencybusinessunitid { get; set; }

		[AntiXss]
		public Guid xts_miscchargestemplateid { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public decimal xjp_fluorocarbonfee_base { get; set; }

		[AntiXss]
		public bool xts_registrationrequired { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public string xjp_deliverylocationidname { get; set; }

		[AntiXss]
		public string xts_ownerphone { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public Guid xts_termsofpaymentid { get; set; }

		[AntiXss]
		public string ktb_additionalname { get; set; }

		[AntiXss]
		public decimal xts_tradeintaxamount_base { get; set; }

		[AntiXss]
		public string xts_potentialcustomerdescription { get; set; }

		[AntiXss]
		public int xjp_insuranceperiod { get; set; }

		[AntiXss]
		public Guid xts_warehouseid { get; set; }

		[AntiXss]
		public Guid xts_purchaseorderid { get; set; }

		[AntiXss]
		public DateTime xts_matchdate { get; set; }

		[AntiXss]
		public decimal xjp_shredderdustfee { get; set; }

		[AntiXss]
		public Guid xts_gradeid { get; set; }

		[AntiXss]
		public int xts_totalpaymentmonth { get; set; }

		[AntiXss]
		public decimal ktb_estimasiharga_base { get; set; }

		[AntiXss]
		public decimal xjp_shredderdustfee_base { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesacquisitiontaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_vehicletaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_otherdeductionamountpayment_base { get; set; }

		[AntiXss]
		public Guid ktb_vehiclemodelid { get; set; }

		[AntiXss]
		public string xts_vehiclepricelistidname { get; set; }

		[AntiXss]
		public decimal xts_miscellaneouschargestaxamount { get; set; }

		[AntiXss]
		public string xts_phonenumber { get; set; }

		[AntiXss]
		public string xts_changename { get; set; }

		[AntiXss]
		public DateTime xts_preferredregistrationdate { get; set; }

		[AntiXss]
		public Guid xjp_deliverylocationid { get; set; }

		[AntiXss]
		public string xjp_ownershipcategory { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamount { get; set; }

		[AntiXss]
		public string xts_ordertypeidname { get; set; }

		[AntiXss]
		public DateTime xjp_registrationresultdate { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public decimal xjp_vehicleestimatedprofitamount { get; set; }

		[AntiXss]
		public decimal xts_annualpaymentamount { get; set; }

		[AntiXss]
		public decimal xts_totalpayment { get; set; }

		[AntiXss]
		public string xjp_usagecategoryname { get; set; }

		[AntiXss]
		public string xts_log { get; set; }

		[AntiXss]
		public string xts_gradedescription { get; set; }

		[AntiXss]
		public decimal xts_discountamountbeforetax_base { get; set; }

		[AntiXss]
		public string xts_potentialcustomeridname { get; set; }

		[AntiXss]
		public decimal xjp_fluorocarbonfee { get; set; }

		[AntiXss]
		public decimal xjp_variousestimatedprofitamount { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment4 { get; set; }

		[AntiXss]
		public decimal ktb_cashbackapm_base { get; set; }

		[AntiXss]
		public string xts_fixedpricename { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_addressnewidname { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment2 { get; set; }

		[AntiXss]
		public string xts_billtoidyominame { get; set; }

		[AntiXss]
		public string xjp_taxationform { get; set; }

		[AntiXss]
		public decimal xts_tradeinvalue { get; set; }

		[AntiXss]
		public DateTime xjp_desireddeliverydate { get; set; }

		[AntiXss]
		public string xjp_vehiclemanagementcategoryname { get; set; }

		[AntiXss]
		public int xts_financingperiod { get; set; }

		[AntiXss]
		public decimal xjp_variousestimatedprofitamount_base { get; set; }

		[AntiXss]
		public string xts_othercompleteaddress { get; set; }

		[AntiXss]
		public Guid xjp_registrationagencyid { get; set; }

		[AntiXss]
		public string xjp_usagecategory { get; set; }

		[AntiXss]
		public decimal xts_subsidizedmaindealer_base { get; set; }

		[AntiXss]
		public string xjp_locationclasscategory { get; set; }

		[AntiXss]
		public string xts_productinteriorcoloridname { get; set; }

		[AntiXss]
		public decimal xts_totalreceiptamount_base { get; set; }

		[AntiXss]
		public decimal xjp_variouscostamount_base { get; set; }

		[AntiXss]
		public string ktb_mediator { get; set; }

		[AntiXss]
		public decimal xts_maximumdiscountamount_base { get; set; }

		[AntiXss]
		public decimal xts_nontaxablemiscellaneouschargeamount { get; set; }

		[AntiXss]
		public Guid xjp_insurancecategoryid { get; set; }

		[AntiXss]
		public DateTime xts_paymentstartdate { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public decimal xts_consumptiontaxtotalamount_base { get; set; }

		[AntiXss]
		public string ktb_dnetspknumber { get; set; }

		[AntiXss]
		public decimal xts_consumptiontaxtotalamount { get; set; }

		[AntiXss]
		public string ktb_rejectedreason { get; set; }

		[AntiXss]
		public string xts_insurancetypename { get; set; }

		[AntiXss]
		public decimal xts_outstandingamount { get; set; }

		[AntiXss]
		public decimal xts_compulsoryinsuranceamount_base { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public decimal xts_tradeinvalue_base { get; set; }

		[AntiXss]
		public string xts_financingpurchaseordernumber { get; set; }

		[AntiXss]
		public string xjp_vehiclespecificnumber { get; set; }

		[AntiXss]
		public string xts_stockidname { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public string xts_companyphone { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public Guid xjp_weighttaxid { get; set; }

		[AntiXss]
		public string xts_originalnewvehiclesalesorderreferenceidname { get; set; }

		[AntiXss]
		public Guid xjp_acquisitiontaxid { get; set; }

		[AntiXss]
		public DateTime xts_purchaseorderdate { get; set; }

		[AntiXss]
		public string xts_potentiallookupname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_usercompleteaddress { get; set; }

		[AntiXss]
		public Guid xts_productconfigurationid { get; set; }

		[AntiXss]
		public string xts_vehiclemanagementphone { get; set; }

		[AntiXss]
		public string ktb_pembayaranname { get; set; }

		[AntiXss]
		public string ktb_vehicledescription { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesacquisitiontaxamount { get; set; }

		[AntiXss]
		public decimal xts_subsidizedsoleagent_base { get; set; }

		[AntiXss]
		public string xts_companynumber { get; set; }

		[AntiXss]
		public DateTime xts_vehicleregistrationsubmitdate { get; set; }

		[AntiXss]
		public string xts_productstyleidname { get; set; }

		[AntiXss]
		public decimal xts_tradeinnetvalue { get; set; }

		[AntiXss]
		public Guid xts_methodofpaymentid { get; set; }

		[AntiXss]
		public decimal xts_tradeinnetvalue_base { get; set; }

		[AntiXss]
		public string xts_priceoptionname { get; set; }

		[AntiXss]
		public decimal xts_tradeinrecyclingamount_base { get; set; }

		[AntiXss]
		public Guid xjp_registrationprocessbyid { get; set; }

		[AntiXss]
		public decimal xts_specialcolorpriceamount { get; set; }

		[AntiXss]
		public decimal xts_subsidizedmaindealer { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public bool xts_requestplatenumber { get; set; }

		[AntiXss]
		public Guid ktb_newbenefitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamount_base { get; set; }

		[AntiXss]
		public string xts_billtodescription { get; set; }

		[AntiXss]
		public decimal xts_totaldiscountamount { get; set; }

		[AntiXss]
		public decimal xjp_totalsalesprice_base { get; set; }

		[AntiXss]
		public decimal xts_interestamount { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public decimal xjp_totalacquisitiontaxamount_base { get; set; }

		[AntiXss]
		public decimal ktb_ongkoskirim_base { get; set; }

		[AntiXss]
		public Guid xts_newvehiclesalesquoteid { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public decimal xts_miscchargebaseamount_base { get; set; }

		[AntiXss]
		public string xts_companydescription { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public decimal xts_variousamount { get; set; }

		[AntiXss]
		public string xts_otheridname { get; set; }

		[AntiXss]
		public decimal xts_vehiclediscountamount_base { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public decimal xjp_totalnetsalesamount { get; set; }

		[AntiXss]
		public string xts_insurancetype { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public DateTime xts_canceldeliverydate { get; set; }

		[AntiXss]
		public Guid xts_opportunityid { get; set; }

		[AntiXss]
		public string ktb_vehiclecolorname { get; set; }

		[AntiXss]
		public decimal xjp_additionalshredderdustfee { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string xid_lastdocumentregistrationidname { get; set; }

		[AntiXss]
		public string xjp_registrationmethod { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesestimatedprofitamount { get; set; }

		[AntiXss]
		public decimal xts_vehiclenetsalesprice_base { get; set; }

		[AntiXss]
		public Guid xts_originalnewvehiclesalesorderreferenceid { get; set; }

		[AntiXss]
		public decimal xts_annualpaymentamount_base { get; set; }

		[AntiXss]
		public string xts_personinchargeidname { get; set; }

		[AntiXss]
		public string xts_downpaymentispaidname { get; set; }

		[AntiXss]
		public DateTime xjp_confirmedregistrationpaymentdate { get; set; }

		[AntiXss]
		public decimal xjp_registrationpaymentamount_base { get; set; }

		[AntiXss]
		public string ktb_lkppnumber { get; set; }

		[AntiXss]
		public string xts_matchingrulepassedname { get; set; }

		[AntiXss]
		public bool xts_insurancefreeofcharge { get; set; }

		[AntiXss]
		public decimal xjp_vehicleestimatedprofitamount_base { get; set; }

		[AntiXss]
		public string xjp_vehiclemanagementidyominame { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public Guid xts_insurancecompanyid { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamountpayment { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesestimatedtotalcost { get; set; }

		[AntiXss]
		public Guid ktb_opportunitynoid { get; set; }

		[AntiXss]
		public string xts_otheridyominame { get; set; }

		[AntiXss]
		public decimal xts_priceamountbeforetax { get; set; }

		[AntiXss]
		public decimal xjp_totalacquisitiontaxamount { get; set; }

		[AntiXss]
		public string xts_opportunityidname { get; set; }

		[AntiXss]
		public decimal xts_subsidizedleasing { get; set; }

		[AntiXss]
		public Guid xts_newvehiclesalesorderid { get; set; }

		[AntiXss]
		public DateTime xts_certificateoftitlenumberreceiptdate { get; set; }

		[AntiXss]
		public decimal xjp_othertaxamount { get; set; }

		[AntiXss]
		public string xts_otherphone { get; set; }

		[AntiXss]
		public string xts_potentialcustomeridyominame { get; set; }

		[AntiXss]
		public string xts_companyidname { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public decimal xjp_totalestimatedprofitamount_base { get; set; }

		[AntiXss]
		public decimal xts_accessoriesnetsalesprice { get; set; }

		[AntiXss]
		public decimal xts_totalpriceamount_base { get; set; }

		[AntiXss]
		public DateTime xts_salesdate { get; set; }

		[AntiXss]
		public string ktb_benefitidname { get; set; }

		[AntiXss]
		public string xts_ownercompleteaddress { get; set; }

		[AntiXss]
		public decimal xts_netamountaftertax_base { get; set; }

		[AntiXss]
		public int xjp_bonuspayment1applymonth { get; set; }

		[AntiXss]
		public string xts_insurancefreeofchargename { get; set; }

		[AntiXss]
		public decimal xjp_vehicleestimatedcost_base { get; set; }

		[AntiXss]
		public decimal xjp_totalestimatedcostamount { get; set; }

		[AntiXss]
		public string xts_billtoidname { get; set; }

		[AntiXss]
		public decimal xts_netamountaftertax { get; set; }

		[AntiXss]
		public decimal xts_otherdeductionamountpayment { get; set; }

		[AntiXss]
		public decimal xjp_totalestimatedprofitamount { get; set; }

		[AntiXss]
		public Guid xts_specialcolorpriceid { get; set; }

		[AntiXss]
		public decimal xts_subsidizeddiscount_base { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_tradeindiscount_base { get; set; }

		[AntiXss]
		public string xjp_taxablebusinesscategoryname { get; set; }

		[AntiXss]
		public decimal xjp_variouscostamount { get; set; }

		[AntiXss]
		public Guid xts_nvsonumberregistrationdetailid { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public decimal xjp_methodofpaymentestimatedprofit_base { get; set; }

		[AntiXss]
		public decimal xjp_totalnetsalesamount_base { get; set; }

		[AntiXss]
		public string xts_newvehiclesalesquoteidname { get; set; }

		[AntiXss]
		public Guid xts_productinteriorcolorid { get; set; }

		[AntiXss]
		public decimal xts_bankchargesfee_base { get; set; }

		[AntiXss]
		public string xts_potentialcontactidyominame { get; set; }

		[AntiXss]
		public string xts_vehicleregistrationnumber { get; set; }

		[AntiXss]
		public decimal xts_totalreceiptamount { get; set; }

		[AntiXss]
		public decimal xjp_totalestimatedcostamount_base { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xts_totaldiscountamount_base { get; set; }

		[AntiXss]
		public DateTime xts_unmatchdate { get; set; }

		[AntiXss]
		public decimal xjp_voluntaryinsuranceamount_base { get; set; }

		[AntiXss]
		public string xts_productconfigurationidname { get; set; }

		[AntiXss]
		public string xjp_automobiletaxidname { get; set; }

		[AntiXss]
		public decimal xts_vehiclenetsalesprice { get; set; }

		[AntiXss]
		public decimal xts_netamount_base { get; set; }

		[AntiXss]
		public decimal xts_administrationfee { get; set; }

		[AntiXss]
		public Guid ktb_programid { get; set; }

		[AntiXss]
		public string xts_owneridname { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesestimatedtotalcost_base { get; set; }

		[AntiXss]
		public string xts_address1 { get; set; }

		[AntiXss]
		public string xts_requestedplatenumber { get; set; }

		[AntiXss]
		public string xts_ownerdescription { get; set; }

		[AntiXss]
		public string xjp_registrationagencylookupname { get; set; }

		[AntiXss]
		public DateTime xts_newvehiclesalescertificatereceiptdate { get; set; }

		[AntiXss]
		public decimal xts_dealerdiscount_base { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public Guid ktb_karosericompanyid { get; set; }

		[AntiXss]
		public Guid xjp_vehiclemanagementid { get; set; }

		[AntiXss]
		public decimal xjp_bonuspaymentamount2 { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public decimal xts_methodofpaymentestimatedsalesamount_base { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string ktb_opportunitynoidname { get; set; }

		[AntiXss]
		public decimal xjp_bonuspaymentamount1_base { get; set; }

		[AntiXss]
		public decimal xjp_weighttaxamount { get; set; }

		[AntiXss]
		public decimal xts_remainingfinancingamount_base { get; set; }

		[AntiXss]
		public decimal xts_financingamount { get; set; }

		[AntiXss]
		public Guid xts_ordertypeid { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public string xts_billtonumber { get; set; }

		[AntiXss]
		public string xts_billtocompleteaddress { get; set; }

		[AntiXss]
		public Guid xts_productstyleid { get; set; }

		[AntiXss]
		public string xjp_ownershipcategoryname { get; set; }

		[AntiXss]
		public string ktb_programidname { get; set; }

		[AntiXss]
		public decimal xts_discountamount_base { get; set; }

		[AntiXss]
		public string xjp_idempotentmessage { get; set; }

		[AntiXss]
		public string xts_purchaseorderstatusname { get; set; }

		[AntiXss]
		public string ktb_vehiclecategoryidname { get; set; }

		[AntiXss]
		public string xts_classificationnumber { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public decimal xts_tradeinnetvaluepayment_base { get; set; }

		[AntiXss]
		public Guid xts_addressnewid { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xts_wholesaleorderid { get; set; }

		[AntiXss]
		public string ktb_leasingidyominame { get; set; }

		[AntiXss]
		public decimal xts_subsidizedsoleagent { get; set; }

		[AntiXss]
		public decimal xts_priceamountbeforetax_base { get; set; }

		[AntiXss]
		public bool ktb_konfirmasics { get; set; }

		[AntiXss]
		public Guid ktb_campaignid { get; set; }

		[AntiXss]
		public string xts_registrationagencyinvoicenumber { get; set; }

		[AntiXss]
		public string xjp_remark { get; set; }

		[AntiXss]
		public Guid xts_recommendedproductid { get; set; }

		[AntiXss]
		public string xts_methodofpaymentidname { get; set; }

		[AntiXss]
		public string xts_otherdescription { get; set; }

		[AntiXss]
		public DateTime xts_certificateoftitlenumbersubmitdate { get; set; }

		[AntiXss]
		public string xts_useridname { get; set; }

		[AntiXss]
		public decimal xjp_totalsalesprice { get; set; }

		[AntiXss]
		public Guid xts_personinchargeid { get; set; }

		[AntiXss]
		public string xts_vehicleorderinvoicing { get; set; }

		[AntiXss]
		public string xts_companycompleteaddress { get; set; }

		[AntiXss]
		public string xts_billtophone { get; set; }

		[AntiXss]
		public decimal ktb_biayamediator_base { get; set; }

		[AntiXss]
		public string xts_withholdingtaxidname { get; set; }

		[AntiXss]
		public string xts_companyidyominame { get; set; }

		[AntiXss]
		public decimal xjp_informationmanagementfee_base { get; set; }

		[AntiXss]
		public DateTime xts_invoiceduedate { get; set; }

		[AntiXss]
		public string xts_warehouseidname { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public string ktb_campaignidname { get; set; }

		[AntiXss]
		public DateTime xts_predeliveryinspectioncancellationdate { get; set; }

		[AntiXss]
		public string xts_insurancecompanyidname { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public string ktb_karosericompanyidname { get; set; }

		[AntiXss]
		public Guid xts_withholdingtax2id { get; set; }

		[AntiXss]
		public decimal xts_accessoriesamount_base { get; set; }

		[AntiXss]
		public Guid xts_userid { get; set; }

		[AntiXss]
		public string xjp_vehiclemanagementnumber { get; set; }

		[AntiXss]
		public DateTime xts_paymentenddate { get; set; }

		[AntiXss]
		public string ktb_typecaroseriesname { get; set; }

		[AntiXss]
		public string xts_matchingtypename { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_alreadydeliveredname { get; set; }

		[AntiXss]
		public decimal xjp_othertaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_bookingfeeamount_base { get; set; }

		[AntiXss]
		public string xts_address2 { get; set; }

		[AntiXss]
		public decimal xts_withholdingtax2amount { get; set; }

		[AntiXss]
		public Guid ktb_vehiclecategoryid { get; set; }

		[AntiXss]
		public Guid xjp_registrationaddressid { get; set; }

		[AntiXss]
		public decimal xjp_informationmanagementfee { get; set; }

		[AntiXss]
		public string xjp_platenumbersegment3 { get; set; }

		[AntiXss]
		public string ktb_remarks { get; set; }

		[AntiXss]
		public Guid xts_companyid { get; set; }

		[AntiXss]
		public string xts_purchaseorderidname { get; set; }

		[AntiXss]
		public decimal xts_totalpriceamount { get; set; }

		[AntiXss]
		public Guid xts_salesorderid { get; set; }

		[AntiXss]
		public decimal xts_referralamount { get; set; }

		[AntiXss]
		public string ktb_pembayaran { get; set; }

		[AntiXss]
		public DateTime xjp_expectedpaymentdate { get; set; }

		[AntiXss]
		public string xjp_weighttaxidname { get; set; }

		[AntiXss]
		public string xts_overmaximumdiscountname { get; set; }

		[AntiXss]
		public decimal xjp_automobiletaxamount_base { get; set; }

		[AntiXss]
		public DateTime xts_scheduleddeliverydate { get; set; }

		[AntiXss]
		public DateTime xts_newvehiclesalescertificatesubmitdate { get; set; }

		[AntiXss]
		public decimal xjp_weighttaxamount_base { get; set; }

		[AntiXss]
		public DateTime xjp_confirmedpaymentdate { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_registrationfeeispaidname { get; set; }

		[AntiXss]
		public DateTime xjp_expectedregistrationpaymentdate { get; set; }

		[AntiXss]
		public decimal xts_vehiclerelatedproductamount { get; set; }

		[AntiXss]
		public decimal xts_accessoriesamount { get; set; }

		[AntiXss]
		public decimal xts_miscchargebaseamount { get; set; }

		[AntiXss]
		public string xts_salesorderidname { get; set; }

		[AntiXss]
		public Guid xts_otherid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string xts_matchingtype { get; set; }

		[AntiXss]
		public string xjp_locationclasscategoryname { get; set; }

		[AntiXss]
		public string xjp_productsubsidycategory { get; set; }

		[AntiXss]
		public Guid xjp_registrationrequestnumberid { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamountpayment_base { get; set; }

		[AntiXss]
		public string xts_priceoption { get; set; }

		[AntiXss]
		public int xjp_registrationagencylookuptype { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string ktb_konfirmasicsname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public decimal xjp_airbagfee { get; set; }

		[AntiXss]
		public DateTime xjp_desiredregistrationdate { get; set; }

		[AntiXss]
		public decimal xts_interestpercentage { get; set; }

		[AntiXss]
		public bool xts_alreadydelivered { get; set; }

		[AntiXss]
		public decimal xts_vehicleamount_base { get; set; }

		[AntiXss]
		public string xjp_registrationrequestnumberidname { get; set; }

		[AntiXss]
		public Guid xts_potentialcontactid { get; set; }

		[AntiXss]
		public DateTime xts_vehicleregistrationdeliverydate { get; set; }

		[AntiXss]
		public decimal xts_vehicleamount { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public decimal xts_netamount { get; set; }

		[AntiXss]
		public string xts_certificateoftitlenumber { get; set; }

		[AntiXss]
		public string xts_tradeintaxcategoryname { get; set; }

		[AntiXss]
		public decimal xts_tradeintaxamount { get; set; }

		[AntiXss]
		public Guid xts_ownerid { get; set; }

		[AntiXss]
		public int xjp_bonuspayment2applymonth { get; set; }

		[AntiXss]
		public string xts_financingcompanyidname { get; set; }

		[AntiXss]
		public string xjp_registrationagencyidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
