Namespace KTB.DNet.Domain

    Public Class enumStatusPO

        Public Enum Status
            Baru
            Batal
            Konfirmasi
            Ditolak
            Rilis
            DiBlok
            Setuju
            Tidak_Setuju
            Selesai
        End Enum

        Public Enum PassTOP
            NoPass
            Pass
        End Enum


        Public Function Create() As String
            Return Status.Baru
        End Function

        Public Function Delete() As String
            Return Status.Batal
        End Function

        Public Function Confirm() As String
            Return Status.Konfirmasi
        End Function

        Public Function UnConfirm() As String
            Return Status.Baru
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
            Return Status.Konfirmasi
        End Function

        Public Function SAPUpdate() As String
            Return Status.Selesai
        End Function

        Public Function RetrieveList() As ArrayList
            Dim arl As New ArrayList

            arl.Add(New EnumItem(CInt(enumStatusPO.Status.Baru), enumStatusPO.Status.Baru.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPO.Status.Batal), enumStatusPO.Status.Batal.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPO.Status.DiBlok), enumStatusPO.Status.DiBlok.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPO.Status.Ditolak), enumStatusPO.Status.Ditolak.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPO.Status.Konfirmasi), enumStatusPO.Status.Konfirmasi.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPO.Status.Rilis), enumStatusPO.Status.Rilis.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPO.Status.Selesai), enumStatusPO.Status.Selesai.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPO.Status.Setuju), enumStatusPO.Status.Setuju.ToString()))
            arl.Add(New EnumItem(CInt(enumStatusPO.Status.Tidak_Setuju), enumStatusPO.Status.Tidak_Setuju.ToString().Replace("_", "")))
            Return arl
        End Function

        Public Enum StatusDraftPO
            Baru = 0
            SubmitPO = 1
            Batal = 2
        End Enum
    End Class
End Namespace