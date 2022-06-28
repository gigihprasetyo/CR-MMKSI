Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class enumRejectedReason

        Public Enum RejectedReason
            Ganti_Unit = 0
            Ganti_Nama = 1
            Pindah_ke_BrandLain = 2
            Ganti_Model_Brand_Mitsubishi = 3
            Ditolak_leasing = 4
            Batal_Otomatis = 5
            '(status by system setelah spk 6 bulan ≠ selesai) -> hide
            Lainnya = 6
            Menunggu_Unit_Terlalu_Lama = 7
            Bonus_yang_dijanjikan_tidak_sesuai = 8
            Koreksi_Administrasi = 9
            Kebutuhan_Pribadi = 10
        End Enum

        Public Function RetrieveRejectedReason(ByVal strRejectReason As String) As ArrayList
            Dim al As New ArrayList

            Dim sts As RejectedReasonEnum

            If strRejectReason = CType(enumRejectedReason.RejectedReason.Ganti_Unit, String) Then
                sts = New RejectedReasonEnum(0, "Ganti Unit")
                al.Add(sts)
            End If

            sts = New RejectedReasonEnum(1, "Ganti Nama")
            al.Add(sts)
            sts = New RejectedReasonEnum(2, "Pindah ke Brand Lain")
            al.Add(sts)
            sts = New RejectedReasonEnum(3, "Ganti Model Mitsubishi")
            al.Add(sts)
            sts = New RejectedReasonEnum(4, "Ditolak Leasing")
            al.Add(sts)
            sts = New RejectedReasonEnum(5, "Batal Otomatis")
            al.Add(sts)

            If strRejectReason = CType(enumRejectedReason.RejectedReason.Lainnya, String) Then
                sts = New RejectedReasonEnum(6, "Lainnya")
                al.Add(sts)
            End If

            sts = New RejectedReasonEnum(7, "Tidak mau menunggu")
            al.Add(sts)
            sts = New RejectedReasonEnum(8, "Bonus yang dijanjikan tidak sesuai")
            al.Add(sts)
            sts = New RejectedReasonEnum(9, "Koreksi Administrasi")
            al.Add(sts)
            sts = New RejectedReasonEnum(10, "Customer ada kebutuhan lain")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValueStatus(ByVal iStatus As Integer) As String
            Dim str As String = ""
            If iStatus = 0 Then str = "Ganti Unit"
            If iStatus = 1 Then str = "Ganti Nama"
            If iStatus = 2 Then str = "Pindah ke Brand Lain"
            If iStatus = 3 Then str = "Ganti Model Mitsubishi"
            If iStatus = 4 Then str = "Ditolak Leasing"
            If iStatus = 5 Then str = "Batal Otomatis"
            If iStatus = 6 Then str = "Lainnya"
            If iStatus = 7 Then str = "Tidak mau menunggu"
            If iStatus = 8 Then str = "Bonus yang dijanjikan tidak sesuai"
            If iStatus = 9 Then str = "Koreksi Administrasi"
            If iStatus = 10 Then str = "Customer ada kebutuhan lain"
            Return str
        End Function

    End Class

    Public Class RejectedReasonEnum
        Private _code As Integer
        Private _desc As String

        Public Sub New()

        End Sub

        Public Sub New(ByVal code As Integer, ByVal desc As String)
            _code = code
            _desc = desc
        End Sub

        Public Property Code() As Integer
            Get
                Return _code
            End Get
            Set(ByVal Value As Integer)
                _code = Value
            End Set
        End Property

        Public Property Desc() As String
            Get
                Return _desc
            End Get
            Set(ByVal Value As String)
                _desc = Value
            End Set
        End Property
    End Class
End Namespace
