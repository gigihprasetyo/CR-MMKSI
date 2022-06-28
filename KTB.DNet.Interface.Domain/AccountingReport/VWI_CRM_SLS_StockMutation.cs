#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_SLS_StockMutation  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:05
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_SLS_StockMutation
    {
        public Int64 IDRow { get; set; }

		public string businessunitcode { get; set; }

		public string xts_transferhistorynumber { get; set; }

		public string transfersourcesitename { get; set; }

		public string transferdestinationsitename { get; set; }

		public DateTime xts_transferdate { get; set; }
    }
}
