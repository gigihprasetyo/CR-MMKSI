#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_VehicleInvoiceDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 02/03/2021 6:32:17
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_VehicleInvoiceDto : DtoBase
    {
        public string KD_JENIS_TRANSAKSI { get; set; }

		public string FG_PENGGANTI { get; set; }

		public string NOMOR_FAKTUR { get; set; }

		public int MASA_PAJAK { get; set; }

		public int TAHUN_PAJAK { get; set; }

		public string TANGGAL_FAKTUR { get; set; }

		public string NPWP { get; set; }

		public string NAMA { get; set; }

		public string ALAMAT_LENGKAP { get; set; }

		public int JUMLAH_DPP { get; set; }

		public int JUMLAH_PPN { get; set; }

		public int JUMLAH_PPNBM { get; set; }

		public string ID_KETERANGAN_TAMBAHAN { get; set; }

		public int FG_UANG_MUKA { get; set; }

		public int UANG_MUKA_DPP { get; set; }

		public int UANG_MUKA_PPN { get; set; }

		public int UANG_MUKA_PPNBM { get; set; }

		public string REFERENSI { get; set; }

		public string TRANSACTION_NUMBER { get; set; }

		public string RETUR_REFERENSI { get; set; }

		public string ktb_dealercode { get; set; }

		public string msdyn_companycode { get; set; }

		//public Guid xts_accountreceivableinvoiceid { get; set; }
		public List<VWI_CRM_VehicleInvoiceDetailDto> VehicleInvoiceDetails { get; set; }
    }
}
