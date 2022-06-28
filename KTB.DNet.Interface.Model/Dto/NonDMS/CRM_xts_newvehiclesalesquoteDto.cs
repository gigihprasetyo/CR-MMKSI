#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_newvehiclesalesquote class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 02 Sep 2020 19:08:00
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_newvehiclesalesquoteDto : DtoBase
    {
        
		public string ktb_productionyear { get; set; }

		public Guid modifiedby { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public int? statecode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public decimal? xjp_accessoriesacquisitiontaxamount { get; set; }

		public decimal? xjp_accessoriesestimatedprofitamount { get; set; }

		public decimal? xjp_accessoriesestimatedtotalcost { get; set; }

		public decimal? xjp_airbagfee { get; set; }

		public decimal? xjp_automobiletaxamount { get; set; }

		public Guid xjp_automobiletaxid { get; set; }

		public Guid xjp_deliverylocationid { get; set; }

		public DateTime? xjp_estimateregistrationdate { get; set; }

		public decimal? xjp_fluorocarbonfee { get; set; }

		public decimal? xjp_fundmanagementfee { get; set; }

		public decimal? xjp_informationmanagementfee { get; set; }

		public string xjp_insuranceapplymethod { get; set; }

		public Guid xjp_insurancecategoryid { get; set; }

		public Guid xjp_insurancecompanyid { get; set; }

		public int? xjp_insuranceperiod { get; set; }

		public string xjp_locationclasscategory { get; set; }

		public decimal? xjp_othertaxamount { get; set; }

		public string xjp_productsubsidycategory { get; set; }

		public decimal? xjp_recyclingamount { get; set; }

		public string xjp_registrationmethod { get; set; }

		public string xjp_remarks { get; set; }

		public decimal? xjp_shredderdustfee { get; set; }

		public string xjp_taxablebusinesscategory { get; set; }

		public string xjp_taxationform { get; set; }

		public decimal? xjp_totalacquisitiontaxamount { get; set; }

		public decimal? xjp_tradeinrecyclingamount { get; set; }

		public string xjp_usagecategory { get; set; }

		public decimal? xjp_variouscostamount { get; set; }

		public decimal? xjp_variousestimatedprofitamount { get; set; }

		public decimal? xjp_vehicleacquisitiontaxamount { get; set; }

		public decimal? xjp_vehicleestimatedcost { get; set; }

		public decimal? xjp_vehicleestimatedprofitamount { get; set; }

		public decimal? xjp_voluntaryinsuranceamount { get; set; }

		public decimal? xjp_weighttaxamount { get; set; }

		public Guid xjp_weighttaxid { get; set; }

		public decimal? xts_accessoriesamount { get; set; }

		public decimal? xts_accessoriesdiscountamount { get; set; }

		public decimal? xts_accessoriesnetsalesprice { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_classificationnumber { get; set; }

		public decimal? xts_compulsoryinsuranceamount { get; set; }

		public Guid xts_consumptiontax1id { get; set; }

		public decimal? xts_dealerdiscount { get; set; }

		public decimal? xts_downpaymentamount { get; set; }

		public DateTime? xts_effectiveto { get; set; }

		public DateTime? xts_expecteddeliverydate { get; set; }

		public Guid xts_financingcompanyid { get; set; }

		public string xts_gradedescription { get; set; }

		public Guid xts_gradeid { get; set; }

		public string xts_gradenumber { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public decimal? xts_maximumdiscountamount { get; set; }

		public Guid xts_methodofpaymentid { get; set; }

		public Guid xts_miscellaneouschargetemplateid { get; set; }

		public string xts_modelspecification { get; set; }

		public decimal? xts_netamount { get; set; }

		public decimal? xts_netamountaftertax { get; set; }

		public Guid xts_newvehiclesalesquoteid { get; set; }

		public decimal? xts_nontaxablemiscellaneouschargeamount { get; set; }

		public Guid xts_opportunityid { get; set; }

		public Guid xts_ordertypeid { get; set; }

		public decimal? xts_outstandingamount { get; set; }

		public bool? xts_overmaximumdiscount { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Guid xts_potentialcontactid { get; set; }

		public string xts_potentialcustomerdescription { get; set; }

		public Guid xts_potentialcustomerid { get; set; }

		public string xts_potentialcustomerlookupname { get; set; }

		public int? xts_potentialcustomerlookuptype { get; set; }

		public string xts_priceoption { get; set; }

		public string xts_productdescription { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public Guid xts_productid { get; set; }

		public Guid xts_productinteriorcolorid { get; set; }

		public Guid xts_productstyleid { get; set; }

		public Guid xts_recommendedproductid { get; set; }

		public decimal? xts_referralamount { get; set; }

		public Guid xts_registrationprocessbyid { get; set; }

		public bool? xts_registrationrequired { get; set; }

		public decimal? xts_remainingfinancingamount { get; set; }

		public string xts_requestedplatenumber { get; set; }

		public bool? xts_requestplatenumber { get; set; }

		public Guid xts_salespersonid { get; set; }

		public string xts_salesquotenumber { get; set; }

		public Guid xts_siteid { get; set; }

		public decimal? xts_specialcolorpriceamount { get; set; }

		public Guid xts_specialcolorpriceid { get; set; }

		public string xts_status { get; set; }

		public decimal? xts_subsidizeddiscount { get; set; }

		public decimal? xts_subsidizedleasing { get; set; }

		public decimal? xts_subsidizedmaindealer { get; set; }

		public decimal? xts_subsidizedsoleagent { get; set; }

		public decimal? xts_taxablemiscellaneouschargeamount { get; set; }

		public Guid xts_termofpaymentid { get; set; }

		public decimal? xts_titleregistrationfee { get; set; }

		public decimal? xts_totaldiscountamount { get; set; }

		public decimal? xts_totalpriceamount { get; set; }

		public decimal? xts_tradeindiscount { get; set; }

		public decimal? xts_tradeinnetvalue { get; set; }

		public decimal? xts_tradeintaxamount { get; set; }

		public string xts_tradeintaxcategory { get; set; }

		public decimal? xts_tradeinvalue { get; set; }

		public DateTime? xts_transactiondate { get; set; }

		public decimal? xts_variousamount { get; set; }

		public decimal? xts_vehicleamount { get; set; }

		public decimal? xts_vehiclediscountamount { get; set; }

		public decimal? xts_vehiclediscountpercentage { get; set; }

		public decimal? xts_vehiclenetsalesprice { get; set; }

		public Guid xts_vehiclepricelistid { get; set; }

		public decimal? xts_vehiclerelatedproductamount { get; set; }

		public Guid xts_vehiclespecificationid { get; set; }

    }
}