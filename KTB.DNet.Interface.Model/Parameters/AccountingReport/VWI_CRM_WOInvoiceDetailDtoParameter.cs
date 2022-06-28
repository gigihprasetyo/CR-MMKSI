#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_WOInvoiceDetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:24:00
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
    public class VWI_CRM_WOInvoiceDetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string TRANSACTION_ITEM_NUMBER { get; set; }

		[AntiXss]
		public string OF { get; set; }

		[AntiXss]
		public string KODE_OBJEK { get; set; }

		[AntiXss]
		public string NAMA_OBJEK { get; set; }

		[AntiXss]
		public decimal HARGA_SATUAN { get; set; }

		[AntiXss]
		public decimal JUMLAH_BARANG { get; set; }

		[AntiXss]
		public decimal HARGA_TOTAL { get; set; }

		[AntiXss]
		public decimal DISKON { get; set; }

		[AntiXss]
		public decimal DPP { get; set; }

		[AntiXss]
		public decimal PPN { get; set; }

		[AntiXss]
		public int TARIF_PPNBM { get; set; }

		[AntiXss]
		public int PPNBM { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
