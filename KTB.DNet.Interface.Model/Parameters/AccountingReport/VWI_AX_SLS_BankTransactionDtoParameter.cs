#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_BankTransactionParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_AX_SLS_BankTransactionParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string Company { get; set; }

		[AntiXss]
		public string BankAccount { get; set; }

		[AntiXss]
		public DateTime TransactionDate { get; set; }

		[AntiXss]
		public string VoucherNumber { get; set; }

		[AntiXss]
		public string BankTransactionType { get; set; }

		[AntiXss]
		public string PaymentReference { get; set; }

		[AntiXss]
		public string DepositSlip { get; set; }

		[AntiXss]
		public string CheckNumber { get; set; }

		[AntiXss]
		public string Currency { get; set; }

		[AntiXss]
		public decimal AmountInTransactionCurrency { get; set; }

		[AntiXss]
		public decimal Amount { get; set; }

		[AntiXss]
		public decimal AmountInReportingCurrency { get; set; }

		[AntiXss]
		public string Reconciled { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
