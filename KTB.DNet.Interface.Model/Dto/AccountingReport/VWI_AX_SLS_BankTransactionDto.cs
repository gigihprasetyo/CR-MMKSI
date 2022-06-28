#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_BankTransactionDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 05/02/2020 9:17:56
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_AX_SLS_BankTransactionDto : DtoBase
    {
        public string Company { get; set; }

		public string BankAccount { get; set; }

		public DateTime TransactionDate { get; set; }

		public string VoucherNumber { get; set; }

		public string BankTransactionType { get; set; }

		public string PaymentReference { get; set; }

		public string DepositSlip { get; set; }

		public string CheckNumber { get; set; }

		public string Currency { get; set; }

		public decimal AmountInTransactionCurrency { get; set; }

		public decimal Amount { get; set; }

		public decimal AmountInReportingCurrency { get; set; }

		public string Reconciled { get; set; }
    }
}
