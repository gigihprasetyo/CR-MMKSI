Namespace KTB.DNet.Domain

    Public Class enumStatus
        Public Enum Status
            Baru
            Batal
            Validasi
            Dikunci
            Dikirim
            Ditolak
            Setuju
            Tidak_Setuju
            DiBlok
            Selesai
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
            Return Status.Dikunci
        End Function

        Public Function UnConfirm() As String
            Return Status.Validasi
        End Function

        Public Function Release() As String
            Return Status.Dikirim
        End Function

        Public Function Reject() As String
            Return Status.Ditolak
        End Function

        Public Function UnReject() As String
            Return Status.Dikunci
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
            Return Status.Dikunci
        End Function

        Public Function UnBlock() As String
            Return Status.Dikirim
        End Function

        Public Function SAPUpdate() As String
            Return Status.Selesai
        End Function

    End Class
End Namespace