#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SOInvoiceParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 01/03/2021 0:51:45
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
    public class VWI_CRM_SOInvoiceParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string KD_JENIS_TRANSAKSI { get; set; }

		[AntiXss]
		public string FG_PENGGANTI { get; set; }

		[AntiXss]
		public string NOMOR_FAKTUR { get; set; }

		[AntiXss]
		public int MASA_PAJAK { get; set; }

		[AntiXss]
		public int TAHUN_PAJAK { get; set; }

		[AntiXss]
		public string TANGGAL_FAKTUR { get; set; }

		[AntiXss]
		public string NPWP { get; set; }

		[AntiXss]
		public string NAMA { get; set; }

		[AntiXss]
		public string ALAMAT_LENGKAP { get; set; }

		[AntiXss]
		public int JUMLAH_DPP { get; set; }

		[AntiXss]
		public int JUMLAH_PPN { get; set; }

		[AntiXss]
		public int JUMLAH_PPNBM { get; set; }

		[AntiXss]
		public string ID_KETERANGAN_TAMBAHAN { get; set; }

		[AntiXss]
		public int FG_UANG_MUKA { get; set; }

		[AntiXss]
		public int UANG_MUKA_DPP { get; set; }

		[AntiXss]
		public int UANG_MUKA_PPN { get; set; }

		[AntiXss]
		public int UANG_MUKA_PPNBM { get; set; }

		[AntiXss]
		public string REFERENSI { get; set; }

		[AntiXss]
		public string TRANSACTION_NUMBER { get; set; }

		[AntiXss]
		public string RETUR_REFERENSI { get; set; }

		[AntiXss]
		public string ktb_dealercode { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

		[AntiXss]
		public Guid xts_deliveryorderid { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
