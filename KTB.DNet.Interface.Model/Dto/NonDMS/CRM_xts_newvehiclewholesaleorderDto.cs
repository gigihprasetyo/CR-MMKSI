#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_newvehiclewholesaleorder class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 16:09:34
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_newvehiclewholesaleorderDto : DtoBase
    {
        public Guid xts_newvehiclewholesaleorderid { get; set; }
        public string ktb_additional { get; set; }

		public string ktb_address4 { get; set; }

		public Guid ktb_benefitid { get; set; }

		public Guid ktb_campaignid { get; set; }

		public string ktb_categorycode { get; set; }

		public string ktb_cbu_bodytype1 { get; set; }

		public string ktb_cbu_bodytypelcv1 { get; set; }

		public string ktb_cbu_jeniskend { get; set; }

		public string ktb_cbu_loadprofile1 { get; set; }

		public string ktb_cbu_medanoperasi1 { get; set; }

		public string ktb_cbu_modelkend { get; set; }

		public string ktb_cbu_ownership1 { get; set; }

		public string ktb_cbu_purcstat { get; set; }

		public string ktb_cbu_purcstat2 { get; set; }

		public string ktb_cbu_purpose1 { get; set; }

		public string ktb_cbu_purpose2 { get; set; }

		public string ktb_cbu_userage1 { get; set; }

		public string ktb_cbu_waypaid1 { get; set; }

		public int? ktb_dnetid { get; set; }

		public int? ktb_dnetsapcustomerid { get; set; }

		public int? ktb_dnetspkcustomerid { get; set; }

		public int? ktb_dnetspkdetailid { get; set; }

		public string ktb_dnetspknumber { get; set; }

		public string ktb_interfaceexceptionmessage { get; set; }

		public bool? ktb_isinterfaced { get; set; }

		public Guid ktb_karoseriid { get; set; }

		public Guid ktb_leasingcompanyid { get; set; }

		public string ktb_lkppno { get; set; }

		public Guid ktb_namakonsumenspkid { get; set; }

		public Guid ktb_nvsoregistrationdetailnumberid { get; set; }

		public int? ktb_qty { get; set; }

		public string ktb_rejectedreason { get; set; }

		public string ktb_rejectedreasonoption { get; set; }

		public string ktb_remarks { get; set; }

		public DateTime? ktb_scheduleddeliverydate { get; set; }

		public string ktb_spkdnetreference { get; set; }

		public string ktb_status { get; set; }

		public string ktb_statusinterfacednet { get; set; }

		public string ktb_tempdocument { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public int? statecode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid xjp_billtoid { get; set; }

		public string xjp_billtonumber { get; set; }

		public decimal? xjp_recyclingamount { get; set; }

		public decimal? xjp_tradeinrecyclingamount { get; set; }

		public decimal? xts_accessoriesamount { get; set; }

		public decimal? xts_accessoriesdiscountamount { get; set; }

		public string xts_address1 { get; set; }

		public string xts_address2 { get; set; }

		public string xts_address3 { get; set; }

		public decimal? xts_balance { get; set; }

		public decimal? xts_bookingfeeamount { get; set; }

		public Guid xts_businessunitid { get; set; }

		public decimal? xts_compulsoryinsuranceamount { get; set; }

		public string xts_customernumber { get; set; }

		public decimal? xts_dealerdiscount { get; set; }

		public decimal? xts_downpaymentamount { get; set; }

		public bool? xts_downpaymentamountispaid { get; set; }

		public decimal? xts_downpaymentamountreceived { get; set; }

		public decimal? xts_netamount { get; set; }

		public decimal? xts_netamountaftertax { get; set; }

		public string xts_newvehiclewholesaleordernumber { get; set; }

		public decimal? xts_nontaxablemiscellaneouschargeamount { get; set; }

		public Guid xts_opportunityid { get; set; }

		public decimal? xts_othertaxamount { get; set; }

		public decimal? xts_outstandingamount { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public string xts_phonenumber { get; set; }

		public Guid xts_potentialcontactid { get; set; }

		public string xts_potentialcustomerdescription { get; set; }

		public Guid xts_potentialcustomerid { get; set; }

		public string xts_potentiallookupname { get; set; }

		public int? xts_potentiallookuptype { get; set; }

		public decimal? xts_referralamount { get; set; }

		public Guid xts_salespersonid { get; set; }

		public decimal? xts_specialcolorpriceamount { get; set; }

		public string xts_status { get; set; }

		public decimal? xts_subsidizeddiscount { get; set; }

		public decimal? xts_taxablemiscellaneouschargeamount { get; set; }

		public Guid xts_termofpaymentid { get; set; }

		public decimal? xts_titleregistrationfee { get; set; }

		public decimal? xts_totalpriceamount { get; set; }

		public decimal? xts_totalreceipt { get; set; }

		public decimal? xts_tradeinamount { get; set; }

		public DateTime? xts_transactiondate { get; set; }

		public decimal? xts_vehicleamount { get; set; }

		public decimal? xts_vehiclediscountamount { get; set; }

		public decimal? xts_vehiclediscountpercentage { get; set; }

		public decimal? xts_vehiclerelatedproductamount { get; set; }

		public decimal? xts_voluntaryinsuranceamount { get; set; }

		public string xts_zipcode { get; set; }

		public string ktb_externalcode { get; set; }

	}
}
