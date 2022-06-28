#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_StockMutationDto  class
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
    public class VWI_CRM_SLS_StockMutationDto : DtoBase
    {
        public string businessunitcode { get; set; }

		public string xts_transferhistorynumber { get; set; }

		public string transfersourcesitename { get; set; }

		public string transferdestinationsitename { get; set; }

		public DateTime xts_transferdate { get; set; }
    }
}
