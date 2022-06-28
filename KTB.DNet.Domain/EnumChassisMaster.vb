Namespace KTB.DNet.Domain

    Public Class EnumChassisMaster
        Public Enum FakturStatus
            Baru = 0
            Validasi = 1
            Konfirmasi = 2
            Proses = 3
            Selesai = 4
        End Enum

        Public Shared Function FakturStatusDesc(ByVal iFakturStatus As String) As String
            If iFakturStatus = "" Then
                Return ""
            Else
                Return CType(iFakturStatus, FakturStatus).ToString()
            End If

        End Function
    End Class


End Namespace
