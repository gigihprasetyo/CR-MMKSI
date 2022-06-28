#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:16:28]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateVWI_CRM_WOInvoiceExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,KD_JENIS_TRANSAKSI = "KD_JENIS_TRANSAKSI Value"
				,FG_PENGGANTI = "FG_PENGGANTI Value"
				,NOMOR_FAKTUR = "NOMOR_FAKTUR Value"
				,MASA_PAJAK = "MASA_PAJAK Value"
				,TAHUN_PAJAK = "TAHUN_PAJAK Value"
				,TANGGAL_FAKTUR = "TANGGAL_FAKTUR Value"
				,NPWP = "NPWP Value"
				,NAMA = "NAMA Value"
				,ALAMAT_LENGKAP = "ALAMAT_LENGKAP Value"
				,JUMLAH_DPP = "JUMLAH_DPP Value"
				,JUMLAH_PPN = "JUMLAH_PPN Value"
				,JUMLAH_PPNBM = "JUMLAH_PPNBM Value"
				,ID_KETERANGAN_TAMBAHAN = "ID_KETERANGAN_TAMBAHAN Value"
				,FG_UANG_MUKA = "FG_UANG_MUKA Value"
				,UANG_MUKA_DPP = "UANG_MUKA_DPP Value"
				,UANG_MUKA_PPN = "UANG_MUKA_PPN Value"
				,UANG_MUKA_PPNBM = "UANG_MUKA_PPNBM Value"
				,REFERENSI = "REFERENSI Value"
				,TRANSACTION_NUMBER = "TRANSACTION_NUMBER Value"
				,RETUR_REFERENSI = "RETUR_REFERENSI Value"
				,ktb_dealercode = "ktb_dealercode Value"
				,msdyn_companycode = "msdyn_companycode Value"
				,xts_workorderid = "xts_workorderid Value"
            };

            return obj;
        }
    }
}