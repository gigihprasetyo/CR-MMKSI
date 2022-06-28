

Namespace KTB.DNet.Domain

    Public Class EnumFleetRequest


        Public Enum FleetRequestStatus
            Baru = 0
            Batal = 1
            Validasi = 2
            BatalValidasi = 3
            Konfirmasi = 4
            BatalKonfirmasi = 5
            Ditolak = 6

        End Enum


        Public Function RetrieveFleetRequestStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumFleetRequestStatus
            sts = New EnumFleetRequestStatus(0, "Baru")
            al.Add(sts)
            sts = New EnumFleetRequestStatus(1, "Batal")
            al.Add(sts)
            sts = New EnumFleetRequestStatus(2, "Validasi")
            al.Add(sts)
            sts = New EnumFleetRequestStatus(3, "Batal Validasi")
            al.Add(sts)
            sts = New EnumFleetRequestStatus(4, "Konfirmasi")
            al.Add(sts)
            sts = New EnumFleetRequestStatus(5, "Batal Konfirmasi")
            al.Add(sts)
            sts = New EnumFleetRequestStatus(6, "Ditolak")
            al.Add(sts)
            Return al
        End Function
    End Class

    Public Class EnumFleetRequestStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public ReadOnly Property ValStatus() As Integer
            Get
                Return _val
            End Get
        End Property

        Public ReadOnly Property NameStatus() As String
            Get
                Return _Name
            End Get
        End Property

    End Class

End Namespace
