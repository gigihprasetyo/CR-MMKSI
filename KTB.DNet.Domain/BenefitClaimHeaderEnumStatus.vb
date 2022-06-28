Namespace KTB.DNet.Domain

    Public Class BenefitClaimHeaderEnumStatus


        Public Enum Status
            Baru
            Validasi
            Konfirmasi
            Tolak
            Proses
            Selesai
        End Enum

        Public Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As BenefitClaimHeaderStatus
            sts = New BenefitClaimHeaderStatus(0, "Baru")
            al.Add(sts)
            sts = New BenefitClaimHeaderStatus(1, "Validasi")
            al.Add(sts)
            sts = New BenefitClaimHeaderStatus(2, "Konfirmasi")
            al.Add(sts)
            sts = New BenefitClaimHeaderStatus(3, "Tolak")
            al.Add(sts)
            sts = New BenefitClaimHeaderStatus(4, "Proses")
            al.Add(sts)
            sts = New BenefitClaimHeaderStatus(5, "Selesai")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetString(ByVal iStatus As Integer) As String
            Dim str As String = ""
            Select Case iStatus
                Case 0
                    str = "Baru"
                Case 1
                    str = "Validasi"
                Case 2
                    str = "Konfirmasi"
                Case 3
                    str = "Tolak"
                Case 4
                    str = "Proses"
                Case 5
                    str = "Selesai"
            End Select
            Return str
        End Function

    End Class

    Public Class BenefitClaimHeaderStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValStatus() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameStatus() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace