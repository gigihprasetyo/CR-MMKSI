Namespace KTB.DNet.Domain

    Public Class EnumClaimStatus


        Public Enum ClaimStatus
            Baru = 0
            Dikirim = 1
            Batal = 2
            'Ditolak = 3
            Proses = 4
            Selesai = 5
            Complete_Selesai = 6
        End Enum

        Public Function Baru() As String
            Return ClaimStatus.Baru
        End Function

        Public Function Dikirim() As String
            Return ClaimStatus.Dikirim
        End Function

        Public Function Batal() As String
            Return ClaimStatus.Batal
        End Function

        Public Function Selesai() As String
            Return ClaimStatus.Selesai
        End Function

        Public Function Complete_Selesai() As String
            Return ClaimStatus.Complete_Selesai
        End Function

        'Public Function Ditolak() As String
        '    Return ClaimStatus.Ditolak
        'End Function

        Public Function Proses() As String
            Return ClaimStatus.Proses
        End Function

        Public Function RetrieveStatus() As ArrayList


            Dim al As New ArrayList
            Dim sts As EnumClaimStatusProp
            sts = New EnumClaimStatusProp(0, "Baru")
            al.Add(sts)
            sts = New EnumClaimStatusProp(1, "Dikirim")
            al.Add(sts)
            sts = New EnumClaimStatusProp(2, "Batal")
            al.Add(sts)
            'sts = New EnumClaimStatusProp(3, "Ditolak")
            'al.Add(sts)
            sts = New EnumClaimStatusProp(4, "Proses")
            al.Add(sts)
            sts = New EnumClaimStatusProp(5, "Selesai")
            al.Add(sts)
            sts = New EnumClaimStatusProp(6, "Complete Selesai")
            al.Add(sts)
            Return al

        End Function

        Public Function RetrieveStatusForChangeDealer() As ArrayList


            Dim al As New ArrayList
            Dim sts As EnumClaimStatusProp
            sts = New EnumClaimStatusProp(1, "Dikirim")
            al.Add(sts)
            sts = New EnumClaimStatusProp(2, "Batal")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumClaimStatusProp
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

