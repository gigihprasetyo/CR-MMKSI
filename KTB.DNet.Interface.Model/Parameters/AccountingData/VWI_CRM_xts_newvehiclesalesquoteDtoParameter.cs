#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_newvehiclesalesquoteParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_newvehiclesalesquoteParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Guid xts_gradeid { get; set; }

		[AntiXss]
		public string xjp_deliverylocationidname { get; set; }

		[AntiXss]
		public bool xts_registrationrequired { get; set; }

		[AntiXss]
		public string xts_taxablename { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public string xts_potentialcustomeridname { get; set; }

		[AntiXss]
		public string xjp_taxationformname { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public Guid xts_newvehiclesalesquoteid { get; set; }

		[AntiXss]
		public decimal xts_remainingfinancingamount_base { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public decimal xts_accessoriesamount_base { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xjp_locationclasscategoryname { get; set; }

		[AntiXss]
		public string ktb_productionyear { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xjp_taxablebusinesscategoryname { get; set; }

		[AntiXss]
		public decimal xjp_fluorocarbonfee_base { get; set; }

		[AntiXss]
		public decimal xts_netamountaftertax_base { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public decimal xts_netamount { get; set; }

		[AntiXss]
		public string xts_overmaximumdiscountname { get; set; }

		[AntiXss]
		public decimal xts_totaldiscountamount { get; set; }

		[AntiXss]
		public decimal xjp_informationmanagementfee { get; set; }

		[AntiXss]
		public bool xts_overmaximumdiscount { get; set; }

		[AntiXss]
		public string xts_requestplatenumbername { get; set; }

		[AntiXss]
		public string xts_miscellaneouschargetemplateidname { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string xts_potentialcontactidyominame { get; set; }

		[AntiXss]
		public decimal xjp_additionalfluorocarbonfee_base { get; set; }

		[AntiXss]
		public decimal xjp_variousestimatedprofitamount { get; set; }

		[AntiXss]
		public decimal xts_dealerdiscount { get; set; }

		[AntiXss]
		public Guid xjp_insurancecompanyid { get; set; }

		[AntiXss]
		public decimal xts_tradeindiscount { get; set; }

		[AntiXss]
		public decimal xts_compulsoryinsuranceamount_base { get; set; }

		[AntiXss]
		public string xjp_taxablebusinesscategory { get; set; }

		[AntiXss]
		public decimal xjp_additionalfluorocarbonfee { get; set; }

		[AntiXss]
		public decimal xts_vehicleamount_base { get; set; }

		[AntiXss]
		public string xjp_acquisitiontaxidname { get; set; }

		[AntiXss]
		public decimal xjp_shredderdustfee_base { get; set; }

		[AntiXss]
		public decimal xts_subsidizedmaindealer { get; set; }

		[AntiXss]
		public decimal xjp_informationmanagementfee_base { get; set; }

		[AntiXss]
		public decimal xts_subsidizedsoleagent { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_potentialcontactidname { get; set; }

		[AntiXss]
		public string xts_nvsonumberregistrationdetailidname { get; set; }

		[AntiXss]
		public string xts_potentialcustomerdescription { get; set; }

		[AntiXss]
		public decimal xjp_airbagfee_base { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public decimal xts_referralamount { get; set; }

		[AntiXss]
		public bool xts_taxable { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamount_base { get; set; }

		[AntiXss]
		public Guid xjp_automobiletaxid { get; set; }

		[AntiXss]
		public string xts_productdescription { get; set; }

		[AntiXss]
		public decimal xjp_automobiletaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount_base { get; set; }

		[AntiXss]
		public decimal xjp_vehicleestimatedcost { get; set; }

		[AntiXss]
		public string xjp_insuranceapplymethod { get; set; }

		[AntiXss]
		public Guid xjp_deliverylocationid { get; set; }

		[AntiXss]
		public decimal xts_totaldiscountamount_base { get; set; }

		[AntiXss]
		public string xts_methodofpaymentidname { get; set; }

		[AntiXss]
		public decimal xjp_voluntaryinsuranceamount { get; set; }

		[AntiXss]
		public decimal xjp_fundmanagementfee { get; set; }

		[AntiXss]
		public string xts_registrationprocessbyidname { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public string xts_tradeintaxcategoryname { get; set; }

		[AntiXss]
		public decimal xjp_shredderdustfee { get; set; }

		[AntiXss]
		public decimal xjp_tradeinrecyclingamount_base { get; set; }

		[AntiXss]
		public string xjp_remarks { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xts_taxablemiscellaneouschargeamount_base { get; set; }

		[AntiXss]
		public Guid xts_stockid { get; set; }

		[AntiXss]
		public decimal xts_variousamount_base { get; set; }

		[AntiXss]
		public string xjp_insurancecompanyidname { get; set; }

		[AntiXss]
		public string xts_opportunityidname { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_nontaxablemiscellaneouschargeamount_base { get; set; }

		[AntiXss]
		public decimal xjp_variouscostamount_base { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public decimal xts_subsidizedleasing { get; set; }

		[AntiXss]
		public decimal xts_accessoriesdiscountamount_base { get; set; }

		[AntiXss]
		public decimal xts_taxablemiscellaneouschargeamount { get; set; }

		[AntiXss]
		public decimal xts_tradeinvalue { get; set; }

		[AntiXss]
		public string xjp_registrationmethod { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesestimatedtotalcost_base { get; set; }

		[AntiXss]
		public string xts_stockidname { get; set; }

		[AntiXss]
		public Guid xts_withholdingtax2id { get; set; }

		[AntiXss]
		public Guid xts_specialcolorpriceid { get; set; }

		[AntiXss]
		public Guid xjp_acquisitiontaxid { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_classificationnumber { get; set; }

		[AntiXss]
		public decimal xts_compulsoryinsuranceamount { get; set; }

		[AntiXss]
		public string xts_salesquotenumber { get; set; }

		[AntiXss]
		public bool xts_requestplatenumber { get; set; }

		[AntiXss]
		public Guid xts_vehiclespecificationid { get; set; }

		[AntiXss]
		public string xjp_locationclasscategory { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesestimatedtotalcost { get; set; }

		[AntiXss]
		public decimal xts_referralamount_base { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public Guid xjp_weighttaxid { get; set; }

		[AntiXss]
		public decimal xjp_voluntaryinsuranceamount_base { get; set; }

		[AntiXss]
		public decimal xts_miscellaneouschargestaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_netamount_base { get; set; }

		[AntiXss]
		public string xts_gradeidname { get; set; }

		[AntiXss]
		public decimal xts_vehicleamount { get; set; }

		[AntiXss]
		public string xts_vehiclespecificationidname { get; set; }

		[AntiXss]
		public string xts_recommendedproductidname { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesestimatedprofitamount_base { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public Guid xts_vehiclepricelistid { get; set; }

		[AntiXss]
		public decimal xts_subsidizedmaindealer_base { get; set; }

		[AntiXss]
		public decimal xjp_othertaxamount_base { get; set; }

		[AntiXss]
		public string xts_gradedescription { get; set; }

		[AntiXss]
		public decimal xts_maximumdiscountamount_base { get; set; }

		[AntiXss]
		public decimal xjp_additionalshredderdustfee_base { get; set; }

		[AntiXss]
		public decimal xts_vehiclenetsalesprice_base { get; set; }

		[AntiXss]
		public decimal xts_outstandingamount_base { get; set; }

		[AntiXss]
		public Guid xts_manufacturerid { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public decimal xjp_variouscostamount { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public string xts_productstyleidname { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public Guid xts_productinteriorcolorid { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal xts_tradeinvalue_base { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax1amount_base { get; set; }

		[AntiXss]
		public string xjp_insuranceapplymethodname { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public decimal xts_vehiclenetsalesprice { get; set; }

		[AntiXss]
		public decimal xjp_variousestimatedprofitamount_base { get; set; }

		[AntiXss]
		public decimal xjp_weighttaxamount_base { get; set; }

		[AntiXss]
		public Guid xts_termofpaymentid { get; set; }

		[AntiXss]
		public string xjp_taxationform { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesacquisitiontaxamount_base { get; set; }

		[AntiXss]
		public string xts_siteidname { get; set; }

		[AntiXss]
		public int xts_potentialcustomerlookuptype { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public decimal xts_totalpriceamount { get; set; }

		[AntiXss]
		public decimal xts_subsidizedleasing_base { get; set; }

		[AntiXss]
		public string xts_potentialcustomerlookupname { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public decimal xjp_fluorocarbonfee { get; set; }

		[AntiXss]
		public decimal xts_tradeintaxamount_base { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public decimal xjp_othertaxamount { get; set; }

		[AntiXss]
		public DateTime xjp_estimateregistrationdate { get; set; }

		[AntiXss]
		public string xjp_weighttaxidname { get; set; }

		[AntiXss]
		public Guid xts_nvsonumberregistrationdetailid { get; set; }

		[AntiXss]
		public string xts_productconfigurationidname { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesestimatedprofitamount { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public decimal xts_accessoriesnetsalesprice_base { get; set; }

		[AntiXss]
		public string xts_ordertypeidname { get; set; }

		[AntiXss]
		public decimal xts_subsidizeddiscount { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public decimal xts_withholdingtax2amount { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xts_accessoriesdiscountamount { get; set; }

		[AntiXss]
		public Guid xts_opportunityid { get; set; }

		[AntiXss]
		public decimal xts_variousamount { get; set; }

		[AntiXss]
		public decimal xjp_vehicleacquisitiontaxamount_base { get; set; }

		[AntiXss]
		public string xjp_usagecategoryname { get; set; }

		[AntiXss]
		public decimal xts_nontaxablemiscellaneouschargeamount { get; set; }

		[AntiXss]
		public string xts_financingcompanyidname { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public Guid xts_siteid { get; set; }

		[AntiXss]
		public decimal xts_bookingfeeamount_base { get; set; }

		[AntiXss]
		public decimal xjp_automobiletaxamount { get; set; }

		[AntiXss]
		public decimal xjp_vehicleacquisitiontaxamount { get; set; }

		[AntiXss]
		public decimal xts_tradeintaxamount { get; set; }

		[AntiXss]
		public decimal xjp_recyclingamount { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_priceoptionname { get; set; }

		[AntiXss]
		public decimal xts_tradeindiscount_base { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public decimal xjp_accessoriesacquisitiontaxamount { get; set; }

		[AntiXss]
		public Guid xts_financingcompanyid { get; set; }

		[AntiXss]
		public decimal xjp_fundmanagementfee_base { get; set; }

		[AntiXss]
		public decimal xts_accessoriestaxamount_base { get; set; }

		[AntiXss]
		public decimal xts_dealerdiscount_base { get; set; }

		[AntiXss]
		public decimal xts_withholdingtax2amount_base { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public string xjp_productsubsidycategory { get; set; }

		[AntiXss]
		public Guid xts_registrationprocessbyid { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount { get; set; }

		[AntiXss]
		public decimal xts_subsidizedsoleagent_base { get; set; }

		[AntiXss]
		public decimal xjp_totalacquisitiontaxamount_base { get; set; }

		[AntiXss]
		public string xts_withholdingtaxidname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string xts_modelspecification { get; set; }

		[AntiXss]
		public decimal xts_consumptiontaxtotalamount { get; set; }

		[AntiXss]
		public decimal xts_netamountaftertax { get; set; }

		[AntiXss]
		public decimal xts_bookingfeeamount { get; set; }

		[AntiXss]
		public string xts_gradenumber { get; set; }

		[AntiXss]
		public decimal xts_tradeinnetvalue { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee { get; set; }

		[AntiXss]
		public decimal xts_consumptiontaxtotalamount_base { get; set; }

		[AntiXss]
		public string xts_vehiclepricelistidname { get; set; }

		[AntiXss]
		public decimal xts_tradeinnetvalue_base { get; set; }

		[AntiXss]
		public decimal xjp_additionalshredderdustfee { get; set; }

		[AntiXss]
		public Guid xjp_insurancecategoryid { get; set; }

		[AntiXss]
		public Guid xts_miscellaneouschargetemplateid { get; set; }

		[AntiXss]
		public decimal xts_vehiclediscountamount { get; set; }

		[AntiXss]
		public string xts_manufactureridname { get; set; }

		[AntiXss]
		public Guid xts_withholdingtaxid { get; set; }

		[AntiXss]
		public decimal xts_vehiclerelatedproductamount { get; set; }

		[AntiXss]
		public string xjp_insurancecategoryidname { get; set; }

		[AntiXss]
		public string xts_specialcolorpriceidname { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public decimal xjp_vehicleestimatedprofitamount { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public decimal xts_outstandingamount { get; set; }

		[AntiXss]
		public decimal xjp_vehicleestimatedprofitamount_base { get; set; }

		[AntiXss]
		public string xts_registrationrequiredname { get; set; }

		[AntiXss]
		public string xts_productinteriorcoloridname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string xjp_registrationmethodname { get; set; }

		[AntiXss]
		public string xjp_automobiletaxidname { get; set; }

		[AntiXss]
		public Guid xts_productstyleid { get; set; }

		[AntiXss]
		public decimal xts_specialcolorpriceamount_base { get; set; }

		[AntiXss]
		public decimal xjp_airbagfee { get; set; }

		[AntiXss]
		public decimal xjp_recyclingamount_base { get; set; }

		[AntiXss]
		public Guid xts_ordertypeid { get; set; }

		[AntiXss]
		public decimal xts_maximumdiscountamount { get; set; }

		[AntiXss]
		public decimal xjp_tradeinrecyclingamount { get; set; }

		[AntiXss]
		public string xjp_idempotentmessage { get; set; }

		[AntiXss]
		public DateTime xts_effectiveto { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public decimal xts_subsidizeddiscount_base { get; set; }

		[AntiXss]
		public decimal xjp_vehicleestimatedcost_base { get; set; }

		[AntiXss]
		public string xts_potentialcustomeridyominame { get; set; }

		[AntiXss]
		public string xts_withholdingtax2idname { get; set; }

		[AntiXss]
		public decimal xjp_totalacquisitiontaxamount { get; set; }

		[AntiXss]
		public int xjp_insuranceperiod { get; set; }

		[AntiXss]
		public string xts_financingcompanyidyominame { get; set; }

		[AntiXss]
		public decimal xts_accessoriestaxamount { get; set; }

		[AntiXss]
		public Guid xts_recommendedproductid { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public decimal xjp_weighttaxamount { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public decimal xts_vehiclerelatedproductamount_base { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public decimal xts_accessoriesnetsalesprice { get; set; }

		[AntiXss]
		public decimal xts_consumptiontax2amount_base { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public decimal xts_accessoriesamount { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee_base { get; set; }

		[AntiXss]
		public string xjp_usagecategory { get; set; }

		[AntiXss]
		public decimal xts_miscellaneouschargestaxamount { get; set; }

		[AntiXss]
		public DateTime xts_expecteddeliverydate { get; set; }

		[AntiXss]
		public string xts_priceoption { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public Guid xts_productconfigurationid { get; set; }

		[AntiXss]
		public decimal xts_vehiclediscountamount_base { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_methodofpaymentid { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public decimal xts_specialcolorpriceamount { get; set; }

		[AntiXss]
		public decimal xts_remainingfinancingamount { get; set; }

		[AntiXss]
		public decimal xts_vehiclediscountpercentage { get; set; }

		[AntiXss]
		public Guid xts_potentialcontactid { get; set; }

		[AntiXss]
		public decimal xts_withholdingtaxamount { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamount { get; set; }

		[AntiXss]
		public string xts_termofpaymentidname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xts_potentialcustomerid { get; set; }

		[AntiXss]
		public string xts_tradeintaxcategory { get; set; }

		[AntiXss]
		public decimal xts_totalpriceamount_base { get; set; }

		[AntiXss]
		public string xts_requestedplatenumber { get; set; }

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
