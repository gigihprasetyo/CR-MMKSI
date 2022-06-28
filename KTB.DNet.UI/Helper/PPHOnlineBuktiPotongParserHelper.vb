Imports System
Imports System.IO
Imports System.Text

Namespace KTB.DNet.UI.Helper

    Public Class PPHOnlineBuktiPotongParserHelper
        Public Function Parsere(ByVal arrRawString As ArrayList)

        End Function
    End Class


    Public Class BuktiPotongInfo
        Public H1_Nomor As String
        'A --> Yang dipotong
        Public A1_NPWP As String
        Public A2_NIK As String
        Public A3_Nama As String
        Public A4_Alamat As String
        Public A5_Telepon As String

        Public B1_MasaPajak As String
        Public B2_KodeObyekPajak As String
        Public B3_JumlahPenghasilanBruto As Decimal
        Public B4_TarifLbhTinggi100p As Decimal
        Public B5_Tarifp As Decimal
        Public B6_PPHDipotong As Decimal
        Public B7_NamaDokReferensi As String
        Public B8_NomorDokReferensi As String
        Public B8_TglDokReferensi As DateTime
        'C --> Pemotong
        Public C1_NPWP As String
        Public C2_NamaWajibPajak As String
        Public C3_Tanggal As DateTime
        Public C4_NamaPenandatangan As String
    End Class
End Namespace