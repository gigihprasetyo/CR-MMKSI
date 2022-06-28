#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_newvehiclesalesquoteDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 14/01/2020 9:03:08
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_newvehiclesalesquoteDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public Guid xts_gradeid { get; set; }

		public string xjp_deliverylocationidname { get; set; }

		public bool xts_registrationrequired { get; set; }

		public string xts_taxablename { get; set; }

		public Guid xts_consumptiontax2id { get; set; }

		public string xts_potentialcustomeridname { get; set; }

		public string xjp_taxationformname { get; set; }

		public string traversedpath { get; set; }

		public Guid xts_newvehiclesalesquoteid { get; set; }

		public decimal xts_remainingfinancingamount_base { get; set; }

		public DateTime modifiedon { get; set; }

		public decimal xts_accessoriesamount_base { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string xjp_locationclasscategoryname { get; set; }

		public string ktb_productionyear { get; set; }

		public string statuscodename { get; set; }

		public string xjp_taxablebusinesscategoryname { get; set; }

		public decimal xjp_fluorocarbonfee_base { get; set; }

		public decimal xts_netamountaftertax_base { get; set; }

		public string modifiedbyyominame { get; set; }

		public decimal xts_netamount { get; set; }

		public string xts_overmaximumdiscountname { get; set; }

		public decimal xts_totaldiscountamount { get; set; }

		public decimal xjp_informationmanagementfee { get; set; }

		public bool xts_overmaximumdiscount { get; set; }

		public string xts_requestplatenumbername { get; set; }

		public string xts_miscellaneouschargetemplateidname { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string xts_potentialcontactidyominame { get; set; }

		public decimal xjp_additionalfluorocarbonfee_base { get; set; }

		public decimal xjp_variousestimatedprofitamount { get; set; }

		public decimal xts_dealerdiscount { get; set; }

		public Guid xjp_insurancecompanyid { get; set; }

		public decimal xts_tradeindiscount { get; set; }

		public decimal xts_compulsoryinsuranceamount_base { get; set; }

		public string xjp_taxablebusinesscategory { get; set; }

		public decimal xjp_additionalfluorocarbonfee { get; set; }

		public decimal xts_vehicleamount_base { get; set; }

		public string xjp_acquisitiontaxidname { get; set; }

		public decimal xjp_shredderdustfee_base { get; set; }

		public decimal xts_subsidizedmaindealer { get; set; }

		public decimal xjp_informationmanagementfee_base { get; set; }

		public decimal xts_subsidizedsoleagent { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string xts_potentialcontactidname { get; set; }

		public string xts_nvsonumberregistrationdetailidname { get; set; }

		public string xts_potentialcustomerdescription { get; set; }

		public decimal xjp_airbagfee_base { get; set; }

		public Guid processid { get; set; }

		public decimal xts_referralamount { get; set; }

		public bool xts_taxable { get; set; }

		public string owneridyominame { get; set; }

		public int statuscode { get; set; }

		public decimal xts_consumptiontax1amount { get; set; }

		public decimal xts_downpaymentamount_base { get; set; }

		public Guid xjp_automobiletaxid { get; set; }

		public string xts_productdescription { get; set; }

		public decimal xjp_automobiletaxamount_base { get; set; }

		public decimal xts_withholdingtaxamount_base { get; set; }

		public decimal xjp_vehicleestimatedcost { get; set; }

		public string xjp_insuranceapplymethod { get; set; }

		public Guid xjp_deliverylocationid { get; set; }

		public decimal xts_totaldiscountamount_base { get; set; }

		public string xts_methodofpaymentidname { get; set; }

		public decimal xjp_voluntaryinsuranceamount { get; set; }

		public decimal xjp_fundmanagementfee { get; set; }

		public string xts_registrationprocessbyidname { get; set; }

		public string xts_consumptiontax1idname { get; set; }

		public string xts_tradeintaxcategoryname { get; set; }

		public decimal xjp_shredderdustfee { get; set; }

		public decimal xjp_tradeinrecyclingamount_base { get; set; }

		public string xjp_remarks { get; set; }

		public string statecodename { get; set; }

		public decimal xts_taxablemiscellaneouschargeamount_base { get; set; }

		public Guid xts_stockid { get; set; }

		public decimal xts_variousamount_base { get; set; }

		public string xjp_insurancecompanyidname { get; set; }

		public string xts_opportunityidname { get; set; }

		public decimal exchangerate { get; set; }

		public decimal xts_nontaxablemiscellaneouschargeamount_base { get; set; }

		public decimal xjp_variouscostamount_base { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public decimal xts_subsidizedleasing { get; set; }

		public decimal xts_accessoriesdiscountamount_base { get; set; }

		public decimal xts_taxablemiscellaneouschargeamount { get; set; }

		public decimal xts_tradeinvalue { get; set; }

		public string xjp_registrationmethod { get; set; }

		public decimal xjp_accessoriesestimatedtotalcost_base { get; set; }

		public string xts_stockidname { get; set; }

		public Guid xts_withholdingtax2id { get; set; }

		public Guid xts_specialcolorpriceid { get; set; }

		public Guid xjp_acquisitiontaxid { get; set; }

		public DateTime createdon { get; set; }

		public string xts_classificationnumber { get; set; }

		public decimal xts_compulsoryinsuranceamount { get; set; }

		public string xts_salesquotenumber { get; set; }

		public bool xts_requestplatenumber { get; set; }

		public Guid xts_vehiclespecificationid { get; set; }

		public string xjp_locationclasscategory { get; set; }

		public decimal xjp_accessoriesestimatedtotalcost { get; set; }

		public decimal xts_referralamount_base { get; set; }

		public string xts_statusname { get; set; }

		public Guid xjp_weighttaxid { get; set; }

		public decimal xjp_voluntaryinsuranceamount_base { get; set; }

		public decimal xts_miscellaneouschargestaxamount_base { get; set; }

		public decimal xts_netamount_base { get; set; }

		public string xts_gradeidname { get; set; }

		public decimal xts_vehicleamount { get; set; }

		public string xts_vehiclespecificationidname { get; set; }

		public string xts_recommendedproductidname { get; set; }

		public decimal xjp_accessoriesestimatedprofitamount_base { get; set; }

		public Guid xts_productid { get; set; }

		public Guid xts_vehiclepricelistid { get; set; }

		public decimal xts_subsidizedmaindealer_base { get; set; }

		public decimal xjp_othertaxamount_base { get; set; }

		public string xts_gradedescription { get; set; }

		public decimal xts_maximumdiscountamount_base { get; set; }

		public decimal xjp_additionalshredderdustfee_base { get; set; }

		public decimal xts_vehiclenetsalesprice_base { get; set; }

		public decimal xts_outstandingamount_base { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public decimal xjp_variouscostamount { get; set; }

		public string xts_productexteriorcoloridname { get; set; }

		public string xts_productstyleidname { get; set; }

		public string xts_handlingname { get; set; }

		public Guid xts_productinteriorcolorid { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal xts_tradeinvalue_base { get; set; }

		public decimal xts_consumptiontax1amount_base { get; set; }

		public string xjp_insuranceapplymethodname { get; set; }

		public Guid xts_consumptiontax1id { get; set; }

		public decimal xts_vehiclenetsalesprice { get; set; }

		public decimal xjp_variousestimatedprofitamount_base { get; set; }

		public decimal xjp_weighttaxamount_base { get; set; }

		public Guid xts_termofpaymentid { get; set; }

		public string xjp_taxationform { get; set; }

		public decimal xjp_accessoriesacquisitiontaxamount_base { get; set; }

		public string xts_siteidname { get; set; }

		public int xts_potentialcustomerlookuptype { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Guid owninguser { get; set; }

		public decimal xts_totalpriceamount { get; set; }

		public decimal xts_subsidizedleasing_base { get; set; }

		public string xts_potentialcustomerlookupname { get; set; }

		public Guid xts_salespersonid { get; set; }

		public string owneridtype { get; set; }

		public decimal xjp_fluorocarbonfee { get; set; }

		public decimal xts_tradeintaxamount_base { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public decimal xjp_othertaxamount { get; set; }

		public DateTime xjp_estimateregistrationdate { get; set; }

		public string xjp_weighttaxidname { get; set; }

		public Guid xts_nvsonumberregistrationdetailid { get; set; }

		public string xts_productconfigurationidname { get; set; }

		public decimal xjp_accessoriesestimatedprofitamount { get; set; }

		public string owneridname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public decimal xts_accessoriesnetsalesprice_base { get; set; }

		public string xts_ordertypeidname { get; set; }

		public decimal xts_subsidizeddiscount { get; set; }

		public string xts_salespersonidname { get; set; }

		public decimal xts_withholdingtax2amount { get; set; }

		public Guid owningteam { get; set; }

		public decimal xts_accessoriesdiscountamount { get; set; }

		public Guid xts_opportunityid { get; set; }

		public decimal xts_variousamount { get; set; }

		public decimal xjp_vehicleacquisitiontaxamount_base { get; set; }

		public string xjp_usagecategoryname { get; set; }

		public decimal xts_nontaxablemiscellaneouschargeamount { get; set; }

		public string xts_financingcompanyidname { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public Guid xts_siteid { get; set; }

		public decimal xts_bookingfeeamount_base { get; set; }

		public decimal xjp_automobiletaxamount { get; set; }

		public decimal xjp_vehicleacquisitiontaxamount { get; set; }

		public decimal xts_tradeintaxamount { get; set; }

		public decimal xjp_recyclingamount { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string xts_priceoptionname { get; set; }

		public decimal xts_tradeindiscount_base { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xts_eventdata { get; set; }

		public decimal xjp_accessoriesacquisitiontaxamount { get; set; }

		public Guid xts_financingcompanyid { get; set; }

		public decimal xjp_fundmanagementfee_base { get; set; }

		public decimal xts_accessoriestaxamount_base { get; set; }

		public decimal xts_dealerdiscount_base { get; set; }

		public decimal xts_withholdingtax2amount_base { get; set; }

		public string xts_productidname { get; set; }

		public string xjp_productsubsidycategory { get; set; }

		public Guid xts_registrationprocessbyid { get; set; }

		public decimal xts_consumptiontax2amount { get; set; }

		public decimal xts_subsidizedsoleagent_base { get; set; }

		public decimal xjp_totalacquisitiontaxamount_base { get; set; }

		public string xts_withholdingtaxidname { get; set; }

		public Guid ownerid { get; set; }

		public string xts_modelspecification { get; set; }

		public decimal xts_consumptiontaxtotalamount { get; set; }

		public decimal xts_netamountaftertax { get; set; }

		public decimal xts_bookingfeeamount { get; set; }

		public string xts_gradenumber { get; set; }

		public decimal xts_tradeinnetvalue { get; set; }

		public decimal xts_titleregistrationfee { get; set; }

		public decimal xts_consumptiontaxtotalamount_base { get; set; }

		public string xts_vehiclepricelistidname { get; set; }

		public decimal xts_tradeinnetvalue_base { get; set; }

		public decimal xjp_additionalshredderdustfee { get; set; }

		public Guid xjp_insurancecategoryid { get; set; }

		public Guid xts_miscellaneouschargetemplateid { get; set; }

		public decimal xts_vehiclediscountamount { get; set; }

		public string xts_manufactureridname { get; set; }

		public Guid xts_withholdingtaxid { get; set; }

		public decimal xts_vehiclerelatedproductamount { get; set; }

		public string xjp_insurancecategoryidname { get; set; }

		public string xts_specialcolorpriceidname { get; set; }

		public string createdbyname { get; set; }

		public decimal xjp_vehicleestimatedprofitamount { get; set; }

		public string xts_handling { get; set; }

		public decimal xts_outstandingamount { get; set; }

		public decimal xjp_vehicleestimatedprofitamount_base { get; set; }

		public string xts_registrationrequiredname { get; set; }

		public string xts_productinteriorcoloridname { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid createdby { get; set; }

		public string xjp_registrationmethodname { get; set; }

		public string xjp_automobiletaxidname { get; set; }

		public Guid xts_productstyleid { get; set; }

		public decimal xts_specialcolorpriceamount_base { get; set; }

		public decimal xjp_airbagfee { get; set; }

		public decimal xjp_recyclingamount_base { get; set; }

		public Guid xts_ordertypeid { get; set; }

		public decimal xts_maximumdiscountamount { get; set; }

		public decimal xjp_tradeinrecyclingamount { get; set; }

		public string xjp_idempotentmessage { get; set; }

		public DateTime xts_effectiveto { get; set; }

		public Guid stageid { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public decimal xts_subsidizeddiscount_base { get; set; }

		public decimal xjp_vehicleestimatedcost_base { get; set; }

		public string xts_potentialcustomeridyominame { get; set; }

		public string xts_withholdingtax2idname { get; set; }

		public decimal xjp_totalacquisitiontaxamount { get; set; }

		public int xjp_insuranceperiod { get; set; }

		public string xts_financingcompanyidyominame { get; set; }

		public decimal xts_accessoriestaxamount { get; set; }

		public Guid xts_recommendedproductid { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_locking { get; set; }

		public decimal xjp_weighttaxamount { get; set; }

		public string xts_consumptiontax2idname { get; set; }

		public decimal xts_vehiclerelatedproductamount_base { get; set; }

		public string xts_businessunitidname { get; set; }

		public decimal xts_accessoriesnetsalesprice { get; set; }

		public decimal xts_consumptiontax2amount_base { get; set; }

		public Int64 versionnumber { get; set; }

		public Guid xts_businessunitid { get; set; }

		public decimal xts_accessoriesamount { get; set; }

		public decimal xts_titleregistrationfee_base { get; set; }

		public string xjp_usagecategory { get; set; }

		public decimal xts_miscellaneouschargestaxamount { get; set; }

		public DateTime xts_expecteddeliverydate { get; set; }

		public string xts_priceoption { get; set; }

		public string xts_status { get; set; }

		public Guid xts_productconfigurationid { get; set; }

		public decimal xts_vehiclediscountamount_base { get; set; }

		public string modifiedbyname { get; set; }

		public Guid xts_methodofpaymentid { get; set; }

		public string createdbyyominame { get; set; }

		public decimal xts_specialcolorpriceamount { get; set; }

		public decimal xts_remainingfinancingamount { get; set; }

		public decimal xts_vehiclediscountpercentage { get; set; }

		public Guid xts_potentialcontactid { get; set; }

		public decimal xts_withholdingtaxamount { get; set; }

		public decimal xts_downpaymentamount { get; set; }

		public string xts_termofpaymentidname { get; set; }

		public Guid modifiedby { get; set; }

		public Guid xts_potentialcustomerid { get; set; }

		public string xts_tradeintaxcategory { get; set; }

		public decimal xts_totalpriceamount_base { get; set; }

		public string xts_requestedplatenumber { get; set; }

		public int statecode { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
