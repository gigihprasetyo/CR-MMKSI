#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_newvehiclewholesaleorderParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 14/01/2020 9:03:09
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
    public class VWI_CRM_xts_newvehiclewholesaleorderParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string ktb_benefitidname { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public decimal xts_vehicleamount_base { get; set; }

		[AntiXss]
		public string ktb_cbu_purcstat { get; set; }

		[AntiXss]
		public string ktb_karoseriidname { get; set; }

		[AntiXss]
		public decimal xts_vehiclerelatedproductamount_base { get; set; }

		[AntiXss]
		public string ktb_rejectedreasonoptionname { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public decimal xts_accessoriesamount_base { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public decimal xjp_tradeinrecyclingamount { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_potentiallookupname { get; set; }

		[AntiXss]
		public string ktb_rejectedreason { get; set; }

		[AntiXss]
		public bool ktb_begbal { get; set; }

		[AntiXss]
		public decimal xts_netamountaftertax_base { get; set; }

		[AntiXss]
		public string ktb_cbu_purcstat2name { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public decimal xts_netamount { get; set; }

		[AntiXss]
		public decimal xts_netamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalreceipt { get; set; }

		[AntiXss]
		public decimal xts_othertaxamount_base { get; set; }

		[AntiXss]
		public string ktb_cbu_userage1name { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string ktb_statusinterfacednetname { get; set; }

		[AntiXss]
		public string ktb_nvsoregistrationdetailnumberidname { get; set; }

		[AntiXss]
		public decimal xts_dealerdiscount { get; set; }

		[AntiXss]
		public decimal xts_voluntaryinsuranceamount_base { get; set; }

		[AntiXss]
		public string xjp_billtoidname { get; set; }

		[AntiXss]
		public decimal xts_balance_base { get; set; }

		[AntiXss]
		public int ktb_qty { get; set; }

		[AntiXss]
		public string ktb_cbu_purpose2name { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string ktb_productidname { get; set; }

		[AntiXss]
		public string ktb_statusinterfacednet { get; set; }

		[AntiXss]
		public string xts_addressidname { get; set; }

		[AntiXss]
		public string xts_potentialcustomerdescription { get; set; }

		[AntiXss]
		public Guid ktb_campaignid { get; set; }

		[AntiXss]
		public decimal xts_referralamount { get; set; }

		[AntiXss]
		public string ktb_categorycode { get; set; }

		[AntiXss]
		public string ktb_interfaceexceptionmessage { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamount_base { get; set; }

		[AntiXss]
		public decimal xts_totalreceipt_base { get; set; }

		[AntiXss]
		public decimal xts_nontaxablemiscellaneouschargeamount_base { get; set; }

		[AntiXss]
		public Guid ktb_namakonsumenspkid { get; set; }

		[AntiXss]
		public string ktb_cbu_modelkend { get; set; }

		[AntiXss]
		public string ktb_cbu_bodytype1name { get; set; }

		[AntiXss]
		public string xts_address3 { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string ktb_namakonsumenspkidyominame { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamountreceived_base { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public decimal xts_taxablemiscellaneouschargeamount_base { get; set; }

		[AntiXss]
		public string ktb_lkppno { get; set; }

		[AntiXss]
		public string ktb_address4 { get; set; }

		[AntiXss]
		public decimal xts_couponamount { get; set; }

		[AntiXss]
		public string ktb_cbu_purpose1name { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_opportunityidname { get; set; }

		[AntiXss]
		public string ktb_cbu_jeniskend { get; set; }

		[AntiXss]
		public string ktb_cbu_medanoperasi1name { get; set; }

		[AntiXss]
		public decimal xts_compulsoryinsuranceamount_base { get; set; }

		[AntiXss]
		public decimal xts_accessoriesdiscountamount_base { get; set; }

		[AntiXss]
		public decimal xts_taxablemiscellaneouschargeamount { get; set; }

		[AntiXss]
		public Guid ktb_productexteriorcolorid { get; set; }

		[AntiXss]
		public Guid xts_newvehiclewholesaleorderid { get; set; }

		[AntiXss]
		public string ktb_begbalname { get; set; }

		[AntiXss]
		public string xjp_billtoidyominame { get; set; }

		[AntiXss]
		public string ktb_cbu_medanoperasi1 { get; set; }

		[AntiXss]
		public string xts_potentialcustomeridname { get; set; }

		[AntiXss]
		public int ktb_dnetid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid xjp_billtoid { get; set; }

		[AntiXss]
		public decimal xts_compulsoryinsuranceamount { get; set; }

		[AntiXss]
		public decimal xts_referralamount_base { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public Guid ktb_nvsoregistrationdetailnumberid { get; set; }

		[AntiXss]
		public string ktb_dnetspknumber { get; set; }

		[AntiXss]
		public string ktb_cbu_bodytypelcv1 { get; set; }

		[AntiXss]
		public string ktb_campaignidname { get; set; }

		[AntiXss]
		public decimal xts_othertaxamount { get; set; }

		[AntiXss]
		public Guid ktb_leasingcompanyid { get; set; }

		[AntiXss]
		public decimal xts_vehicleamount { get; set; }

		[AntiXss]
		public decimal xts_tradeinamount_base { get; set; }

		[AntiXss]
		public string ktb_cbu_purpose1 { get; set; }

		[AntiXss]
		public string ktb_cbu_userage1 { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string ktb_remarks { get; set; }

		[AntiXss]
		public decimal xts_voluntaryinsuranceamount { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string ktb_cbu_purpose2 { get; set; }

		[AntiXss]
		public string xts_zipcode { get; set; }

		[AntiXss]
		public string xts_address1 { get; set; }

		[AntiXss]
		public string ktb_cbu_purcstatname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_potentialcustomerid { get; set; }

		[AntiXss]
		public string xts_newvehiclewholesaleordernumber { get; set; }

		[AntiXss]
		public DateTime ktb_scheduleddeliverydate { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public string ktb_spkdnetreference { get; set; }

		[AntiXss]
		public Guid xts_termofpaymentid { get; set; }

		[AntiXss]
		public decimal xts_outstandingamount_base { get; set; }

		[AntiXss]
		public decimal xjp_tradeinrecyclingamount_base { get; set; }

		[AntiXss]
		public string ktb_cbu_jeniskendname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public decimal xts_totalpriceamount { get; set; }

		[AntiXss]
		public string ktb_rejectedreasonoption { get; set; }

		[AntiXss]
		public decimal xts_nontaxablemiscellaneouschargeamount { get; set; }

		[AntiXss]
		public Guid ktb_karoseriid { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string ktb_additional { get; set; }

		[AntiXss]
		public decimal xts_couponamount_base { get; set; }

		[AntiXss]
		public decimal xts_vehiclediscountamount { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public string ktb_handlingname { get; set; }

		[AntiXss]
		public decimal xts_subsidizeddiscount { get; set; }

		[AntiXss]
		public string ktb_cbu_bodytypelcv1name { get; set; }

		[AntiXss]
		public int xts_potentiallookuptype { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xts_accessoriesdiscountamount { get; set; }

		[AntiXss]
		public Guid xts_opportunityid { get; set; }

		[AntiXss]
		public string xjp_billtonumber { get; set; }

		[AntiXss]
		public int ktb_dnetspkcustomerid { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamountreceived { get; set; }

		[AntiXss]
		public string xts_downpaymentamountispaidname { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public decimal xts_bookingfeeamount_base { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public decimal xts_balance { get; set; }

		[AntiXss]
		public decimal xjp_recyclingamount { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string ktb_cbu_ownership1 { get; set; }

		[AntiXss]
		public string ktb_namakonsumenspkidname { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public string ktb_cbu_purcstat2 { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string ktb_statusname { get; set; }

		[AntiXss]
		public decimal xts_dealerdiscount_base { get; set; }

		[AntiXss]
		public decimal xjp_recyclingamount_base { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public Guid xts_addressid { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public decimal xts_consumptiontaxtotalamount { get; set; }

		[AntiXss]
		public decimal xts_netamountaftertax { get; set; }

		[AntiXss]
		public int ktb_dnetsapcustomerid { get; set; }

		[AntiXss]
		public decimal xts_bookingfeeamount { get; set; }

		[AntiXss]
		public string ktb_cbu_loadprofile1name { get; set; }

		[AntiXss]
		public string ktb_categorycodename { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee { get; set; }

		[AntiXss]
		public decimal xts_consumptiontaxtotalamount_base { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public decimal xts_vehiclerelatedproductamount { get; set; }

		[AntiXss]
		public Guid ktb_benefitid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public int ktb_dnetspkdetailid { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public decimal xts_outstandingamount { get; set; }

		[AntiXss]
		public string xts_phonenumber { get; set; }

		[AntiXss]
		public decimal xts_accessoriesamount { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string ktb_namakonsumenspk { get; set; }

		[AntiXss]
		public string ktb_cbu_loadprofile1 { get; set; }

		[AntiXss]
		public decimal xts_specialcolorpriceamount_base { get; set; }

		[AntiXss]
		public string ktb_cbu_waypaid1 { get; set; }

		[AntiXss]
		public string ktb_cbu_bodytype1 { get; set; }

		[AntiXss]
		public string ktb_leasingcompanyidname { get; set; }

		[AntiXss]
		public string ktb_cbu_modelkendname { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public decimal xts_subsidizeddiscount_base { get; set; }

		[AntiXss]
		public string ktb_handling { get; set; }

		[AntiXss]
		public string xts_potentialcustomeridyominame { get; set; }

		[AntiXss]
		public string ktb_tempdocument { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public Guid ktb_productid { get; set; }

		[AntiXss]
		public string xts_address2 { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string ktb_additionalname { get; set; }

		[AntiXss]
		public string ktb_cbu_waypaid1name { get; set; }

		[AntiXss]
		public decimal xts_titleregistrationfee_base { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public string xts_potentialcontactidname { get; set; }

		[AntiXss]
		public string ktb_status { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public decimal xts_vehiclediscountamount_base { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public decimal xts_specialcolorpriceamount { get; set; }

		[AntiXss]
		public decimal xts_vehiclediscountpercentage { get; set; }

		[AntiXss]
		public Guid xts_potentialcontactid { get; set; }

		[AntiXss]
		public decimal xts_downpaymentamount { get; set; }

		[AntiXss]
		public string xts_termofpaymentidname { get; set; }

		[AntiXss]
		public string ktb_tempdocumentname { get; set; }

		[AntiXss]
		public string xts_potentialcontactidyominame { get; set; }

		[AntiXss]
		public string ktb_productexteriorcoloridname { get; set; }

		[AntiXss]
		public string ktb_cbu_ownership1name { get; set; }

		[AntiXss]
		public bool xts_downpaymentamountispaid { get; set; }

		[AntiXss]
		public decimal xts_tradeinamount { get; set; }

		[AntiXss]
		public decimal xts_totalpriceamount_base { get; set; }

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
