#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SOInvoiceDetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 01/03/2021 0:32:41
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
    public class VWI_CRM_SOInvoiceDetailParameterDto : ParameterDtoBase, IValidatableObject
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

		[AntiXss]
		public decimal xts_rate { get; set; }

		[AntiXss]
		public Guid xts_deliveryorderid { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
