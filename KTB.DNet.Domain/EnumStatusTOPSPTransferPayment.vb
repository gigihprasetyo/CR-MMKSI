Namespace KTB.DNet.Domain

    Public Class EnumStatusTOPSPTransferPayment

        Public Enum TOPSPStatus
            Baru = 0
            Batal
            Konfirmasi
            Batal_Konfirmasi
            Validasi
            Selesai
        End Enum

        Public Shared Function GetString(ByVal Status As Integer) As String
            Dim str As String = ""
            Select Case Status
                Case 0
                    str = "Baru"
                Case 1
                    str = "Batal"
                Case 2
                    str = "Konfirmasi"
                Case 3
                    str = "Batal Konfirmasi"
                Case 4
                    str = "Validasi"
                Case 5
                    str = "Selesai"
            End Select
            Return str
        End Function

        Public Enum PaymentPurposeCode
            IT = 1
            SP = 9
        End Enum
    End Class

End Namespace