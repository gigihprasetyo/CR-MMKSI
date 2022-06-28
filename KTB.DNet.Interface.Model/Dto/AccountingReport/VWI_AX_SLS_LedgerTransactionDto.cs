#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_LedgerTransactionDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 11/02/2020 16:48:11
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_AX_SLS_LedgerTransactionDto : DtoBase
    {
        public Int64 RecordId { get; set; }
        public string Company { get; set; }

		public string AccountNo { get; set; }

		public string AccountName { get; set; }

		public DateTime TransactionDate { get; set; }

		public string JournalNumber { get; set; }

		public string DocumentNo { get; set; }

		public string Voucher { get; set; }

		public string PostingType { get; set; }

		public string LedgerAccount { get; set; }

		public string Description { get; set; }

		public string Dimension1 { get; set; }

		public string Dimension2 { get; set; }

		public string Dimension3 { get; set; }

		public string Dimension4 { get; set; }

		public string Dimension5 { get; set; }

		public string Dimension6 { get; set; }

		public string Currency { get; set; }

		public decimal AmountInTransactionDebit { get; set; }

		public decimal AmountInTransactionCredit { get; set; }

		public decimal AmountInAccountingDebit { get; set; }

		public decimal AmountInAccountingCredit { get; set; }

		public decimal AmountInReportingDebit { get; set; }

		public decimal AmountInReportingCredit { get; set; }
	}
}
