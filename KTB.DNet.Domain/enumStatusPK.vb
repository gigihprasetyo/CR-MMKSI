Namespace KTB.DNet.Domain

    Public Class enumStatusPK
        Public Enum Status
            Baru
            Batal
            Validasi
            Konfirmasi
            Rilis
            Ditolak
            Setuju
            Tidak_Setuju
            DiBlok
            Selesai
            Tunggu_Diskon
        End Enum

        Public Enum StatusSetujuRilis
            Belum
            Disetujui
            Batal
        End Enum

        Public Enum EnumFreezeStatus
            NeverFreeze ' Not Tambahan
            NotFreeze ' Tambahan -> Not in date scope
            Freeze ' Tambahan -> in date scope -> not unlock
            FreezeButUnlock 'Tambahan -> in date scope ->Unlock 
        End Enum

        Public Function Create() As String
            Return Status.Baru
        End Function

        Public Function Validate() As String
            Return Status.Validasi
        End Function

        Public Function Delete() As String
            Return Status.Batal
        End Function

        Public Function UnValidate() As String
            Return Status.Baru
        End Function

        Public Function Confirm() As String
            Return Status.Konfirmasi
        End Function

        Public Function UnConfirm() As String
            Return Status.Validasi
        End Function

        Public Function Release() As String
            Return Status.Rilis
        End Function

        Public Function Reject() As String
            Return Status.Ditolak
        End Function

        Public Function UnReject() As String
            Return Status.Konfirmasi
        End Function

        Public Function Agree() As String
            Return Status.Setuju
        End Function

        Public Function Disagree() As String
            Return Status.Tidak_Setuju
        End Function

        Public Function Block() As String
            Return Status.DiBlok
        End Function

        Public Function UnRelease() As String
            Return Status.Konfirmasi
        End Function

        Public Function UnBlock() As String
            Return Status.Rilis
        End Function

        Public Function SAPUpdate() As String
            Return Status.Selesai
        End Function

        Public Function UnAgree() As String
            Return Status.Konfirmasi
        End Function

        Public Function Tunggu_Diskon() As String
            Return Status.Tunggu_Diskon
        End Function

        Public Function RetrieveList() As ArrayList
            Dim arl As New ArrayList

            arl.Add(New EnumItem(CInt(enumStatus.Status.Baru), enumStatus.Status.Baru.ToString()))
            arl.Add(New EnumItem(CInt(enumStatus.Status.Batal), enumStatus.Status.Batal.ToString()))
            arl.Add(New EnumItem(CInt(enumStatus.Status.DiBlok), enumStatus.Status.DiBlok.ToString()))
            arl.Add(New EnumItem(CInt(enumStatus.Status.Dikirim), enumStatus.Status.Dikirim.ToString()))
            arl.Add(New EnumItem(CInt(enumStatus.Status.Dikunci), enumStatus.Status.Dikunci.ToString()))
            arl.Add(New EnumItem(CInt(enumStatus.Status.Ditolak), enumStatus.Status.Ditolak.ToString()))
            arl.Add(New EnumItem(CInt(enumStatus.Status.Selesai), enumStatus.Status.Selesai.ToString()))
            arl.Add(New EnumItem(CInt(enumStatus.Status.Setuju), enumStatus.Status.Setuju.ToString()))
            arl.Add(New EnumItem(CInt(enumStatus.Status.Tidak_Setuju), enumStatus.Status.Tidak_Setuju.ToString().Replace("_", "")))
            arl.Add(New EnumItem(CInt(enumStatus.Status.Validasi), enumStatus.Status.Validasi.ToString()))

            Return arl
        End Function

        Public Function RetrieveListAlert() As ArrayList
            Dim arl As New ArrayList
            arl.Add(New EnumItem(CInt(enumStatusPK.Status.Baru), enumStatusPK.Status.Baru.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPK.Status.Batal), enumStatusPK.Status.Batal.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPK.Status.Validasi), enumStatusPK.Status.Validasi.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPK.Status.Konfirmasi), enumStatusPK.Status.Konfirmasi.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPK.Status.Rilis), enumStatusPK.Status.Rilis.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPK.Status.Ditolak), enumStatusPK.Status.Ditolak.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPK.Status.Setuju), enumStatusPK.Status.Setuju.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPK.Status.Tidak_Setuju), enumStatusPK.Status.Tidak_Setuju.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPK.Status.DiBlok), enumStatusPK.Status.DiBlok.ToString().Replace("_", "")))
            arl.Add(New EnumItem(CInt(enumStatusPK.Status.Selesai), enumStatusPK.Status.Selesai.ToString()))
            Return arl
        End Function

    End Class
End Namespace