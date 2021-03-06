#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_accountreceivableinvoicedetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 04 Sep 2020 09:14:51
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_accountreceivableinvoicedetailDto : DtoBase
    {
        
		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public int? statecode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string xts_accountreceivableinvoicedetail { get; set; }

		public Guid xts_accountreceivableinvoicedetailid { get; set; }

		public Guid xts_accountreceivableinvoiceid { get; set; }

		public decimal? xts_amount { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_description { get; set; }

		public Guid xts_dimension10id { get; set; }

		public Guid xts_dimension1id { get; set; }

		public Guid xts_dimension2id { get; set; }

		public Guid xts_dimension3id { get; set; }

		public Guid xts_dimension4id { get; set; }

		public Guid xts_dimension5id { get; set; }

		public Guid xts_dimension6id { get; set; }

		public Guid xts_dimension7id { get; set; }

		public Guid xts_dimension8id { get; set; }

		public Guid xts_dimension9id { get; set; }

		public string xts_eventdata { get; set; }

		public Guid xts_reasonid { get; set; }

		public decimal? xts_withholdingtaxamount { get; set; }

		public Guid xts_withholdingtaxid { get; set; }

		public string xts_withholdingtaxpaymentmethod { get; set; }

    }
}
