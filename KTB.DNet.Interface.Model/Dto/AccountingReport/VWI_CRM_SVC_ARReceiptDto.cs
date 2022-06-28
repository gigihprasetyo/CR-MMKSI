#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_ARReceiptDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:05
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_SVC_ARReceiptDto : DtoBase
    {
        public string businessunitcode { get; set; }

		public string xts_accountreceivableinvoice { get; set; }

		public string xts_type { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public string xts_status { get; set; }

		public string xts_reversing { get; set; }

		public string xts_ordernumber { get; set; }

		public decimal xts_invoiceamount { get; set; }

		public string xts_customernumber { get; set; }

		public decimal xts_balance { get; set; }

		public string address1_line1 { get; set; }

		public decimal xts_totalworkamount { get; set; }

		public decimal xts_totalpartsamount { get; set; }

		public decimal xts_totalmiscchargeamount { get; set; }

		public string xts_platenumber { get; set; }
    }
}
