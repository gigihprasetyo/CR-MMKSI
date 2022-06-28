#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_LedgerTransactionParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_AX_SLS_LedgerTransactionParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string Company { get; set; }

		[AntiXss]
		public string AccountNo { get; set; }

		[AntiXss]
		public string AccountName { get; set; }

		[AntiXss]
		public DateTime TransactionDate { get; set; }

		[AntiXss]
		public string JournalNuber { get; set; }

		[AntiXss]
		public string Voucher { get; set; }

		[AntiXss]
		public string PostingType { get; set; }

		[AntiXss]
		public string LedgerAccount { get; set; }

		[AntiXss]
		public string Description { get; set; }

		[AntiXss]
		public string Dimension1 { get; set; }

		[AntiXss]
		public string Dimension2 { get; set; }

		[AntiXss]
		public string Dimension3 { get; set; }

		[AntiXss]
		public string Dimension4 { get; set; }

		[AntiXss]
		public string Dimension5 { get; set; }

		[AntiXss]
		public string Dimension6 { get; set; }

		[AntiXss]
		public string Currency { get; set; }

		[AntiXss]
		public decimal AmountInTransactionDebit { get; set; }

		[AntiXss]
		public decimal AmountInTransactionCredit { get; set; }

		[AntiXss]
		public decimal AmountInAccountingDebit { get; set; }

		[AntiXss]
		public decimal AmountInAccountingCredit { get; set; }

		[AntiXss]
		public decimal AmountInReportingDebit { get; set; }

		[AntiXss]
		public decimal AmountInReportingCredit { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
