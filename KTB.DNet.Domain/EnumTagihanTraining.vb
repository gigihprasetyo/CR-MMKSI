Imports System.IO
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq

Public Class EnumTagihanTraining
    Public Enum TagihanStatus As Short
        Semua = -1
        Validasi = 1
        Konfirmasi = 2
        Disetujui = 3
        Pembayaran_Transfer = 4
        Pencairan_Deposit_B = 5
        Proses_Transfer = 6
        Selesai = 7
    End Enum

    Public Enum TipePembayaran As Short
        Deposit_B = 1
        Transfer = 2
    End Enum

    Public Enum PembayaranStatus As Short
        Semua = -1
        Baru = 1
        Validasi = 2
        Proses = 3
        Selesai = 4
    End Enum

    Public Enum BuktiPembayaran As Short
        Semua = -1
        Belum_Upload = 1
        Sudah_Upload = 2
    End Enum

    Public Enum PaymentType
        Semua = -1
        Transfer = 1
    End Enum

End Class
