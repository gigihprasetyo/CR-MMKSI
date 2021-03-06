#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xid_documentregistration class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Khalil (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 04 Sep 2020 14:12:48
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xid_documentregistrationDto : DtoBase
    {
        
		public Guid ktb_bpkbnameid { get; set; }

		public Guid ktb_spkdetaildummy { get; set; }

		public Guid ktb_spknameid { get; set; }

		public Guid ktb_validnextdocumentregistrationid { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public Guid xid_businessunitid { get; set; }

		public string xid_cancellationnotes { get; set; }

		public string xid_chasissno { get; set; }

		public string xid_chassismodel { get; set; }

		public string xid_contactperson { get; set; }

		public string xid_contactpersonphone { get; set; }

		public Guid xid_customerid { get; set; }

		public Guid xid_documentregistrationid { get; set; }

		public string xid_documentregistrationnumber { get; set; }

		public string xid_engineno { get; set; }

		public Guid xid_financingcompanyid { get; set; }

		public string xid_financingponumber { get; set; }

		public bool? xid_firststage { get; set; }

		public string xid_invoicenumber { get; set; }

		public Guid xid_newvehiclesalesorderid { get; set; }

		public Guid xid_nvexteriorcolorid { get; set; }

		public Guid xid_parentbusinessunitid { get; set; }

		public Guid xid_personinchargeid { get; set; }

		public string xid_platenumber { get; set; }

		public Guid xid_previousdocregistrationid { get; set; }

		public string xid_productdescription { get; set; }

		public Guid xid_productid { get; set; }

		public Guid xid_progressstageid { get; set; }

		public Guid xid_registrationagencyid { get; set; }

		public Guid xid_registrationcolorid { get; set; }

		public int? xid_stageordernumber { get; set; }

		public string xid_status { get; set; }

		public Guid xid_stocknumberid { get; set; }

		public Guid xid_territoryid { get; set; }

		public DateTime? xid_transactiondate { get; set; }

		public string xid_transactiontype { get; set; }

		public string xid_vehicleownershipcerificatenumber { get; set; }

		public string xid_vehicleregistrationaddress { get; set; }

		public DateTime? xid_vehicleregistrationinvoiceduedate { get; set; }

		public string xid_vehicleregistrationname { get; set; }

		public string xid_vehicleregistrationnumber { get; set; }

		public DateTime? xid_vehicleregistrationvaliddate { get; set; }

    }
}
