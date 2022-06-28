#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 11/02/2020 16:48:10]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateVWI_AX_SLS_LedgerTransactionExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,Company = "Company Value"
				,AccountNo = "AccountNo Value"
				,AccountName = "AccountName Value"
				,TransactionDate = "TransactionDate Value"
				,JournalNuber = "JournalNuber Value"
				,Voucher = "Voucher Value"
				,PostingType = "PostingType Value"
				,LedgerAccount = "LedgerAccount Value"
				,Description = "Description Value"
				,Dimension1 = "Dimension1 Value"
				,Dimension2 = "Dimension2 Value"
				,Dimension3 = "Dimension3 Value"
				,Dimension4 = "Dimension4 Value"
				,Dimension5 = "Dimension5 Value"
				,Dimension6 = "Dimension6 Value"
				,Currency = "Currency Value"
				,AmountInTransactionDebit = "AmountInTransactionDebit Value"
				,AmountInTransactionCredit = "AmountInTransactionCredit Value"
				,AmountInAccountingDebit = "AmountInAccountingDebit Value"
				,AmountInAccountingCredit = "AmountInAccountingCredit Value"
				,AmountInReportingDebit = "AmountInReportingDebit Value"
				,AmountInReportingCredit = "AmountInReportingCredit Value"
            };

            return obj;
        }
    }
}