#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_SOInvoiceDetail  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 01/03/2021 0:32:40
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_SOInvoiceDetail
    {
        public Int64 IDRow { get; set; }

		public string TRANSACTION_ITEM_NUMBER { get; set; }

		public string OF { get; set; }

		public string KODE_OBJEK { get; set; }

		public string NAMA_OBJEK { get; set; }

		public decimal HARGA_SATUAN { get; set; }

		public decimal JUMLAH_BARANG { get; set; }

		public decimal HARGA_TOTAL { get; set; }

		public decimal DISKON { get; set; }

		public decimal DPP { get; set; }

		public decimal PPN { get; set; }

		public int TARIF_PPNBM { get; set; }

		public int PPNBM { get; set; }

		public decimal xts_rate { get; set; }

		public Guid xts_deliveryorderid { get; set; }
    }
}
