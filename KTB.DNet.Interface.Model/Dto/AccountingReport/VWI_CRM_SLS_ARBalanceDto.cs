#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_ARBalanceDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/01/2020 14:05:28
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_SLS_ARBalanceDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string InvoiceNumber { get; set; }

		public DateTime TransactionDate { get; set; }

		public DateTime DueDate { get; set; }

		public string DocState { get; set; }

		public string DocType { get; set; }

		public string SourceType { get; set; }

		public string OrderNo { get; set; }

		public string DeliveryOrderNo { get; set; }

		public string CustomerNo { get; set; }

		public string Customer { get; set; }

		public decimal InvoiceAmount { get; set; }

		public decimal Balance { get; set; }

		public string FinancingCompanyNo { get; set; }

		public string FinancingCompany { get; set; }

		public decimal FinancingCompanyInvoiceAmount { get; set; }

		public decimal FinancingCompanyBalance { get; set; }

		public string Reversing { get; set; }

		public string SPKNo { get; set; }

		public string ChassisNo { get; set; }

		public string EngineNumber { get; set; }

		public string ColorDescription { get; set; }

		public string Product { get; set; }

		public string ProductionYear { get; set; }

		public string ProductDescription { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
