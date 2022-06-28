#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 05/02/2020 9:17:56]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateVWI_AX_SLS_BankTransactionExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,Company = "Company Value"
				,BankAccount = "BankAccount Value"
				,TransactionDate = "TransactionDate Value"
				,VoucherNumber = "VoucherNumber Value"
				,BankTransactionType = "BankTransactionType Value"
				,PaymentReference = "PaymentReference Value"
				,DepositSlip = "DepositSlip Value"
				,CheckNumber = "CheckNumber Value"
				,Currency = "Currency Value"
				,AmountInTransactionCurrency = "AmountInTransactionCurrency Value"
				,Amount = "Amount Value"
				,AmountInReportingCurrency = "AmountInReportingCurrency Value"
				,Reconciled = "Reconciled Value"
            };

            return obj;
        }
    }
}