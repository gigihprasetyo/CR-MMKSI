Namespace KTB.DNet.Domain
    Public Class EnumEventInfo

        Public Enum EventInfoStatus
            Baru
            Validasi
            Konfirmasi
            Disetujui
            Ditolak
        End Enum


        Public Function RetrieveStatusKTB() As ArrayList
            Dim al As New ArrayList
            Dim sts As EventInfoStatusProp
            sts = New EventInfoStatusProp(EventInfoStatus.Validasi, EventInfoStatus.Validasi.ToString)
            al.Add(sts)
            'sts = New EventInfoStatusProp(EventInfoStatus.Konfirmasi, EventInfoStatus.Konfirmasi.ToString) '---disetujui KTB
            'al.Add(sts)
            sts = New EventInfoStatusProp(EventInfoStatus.Disetujui, EventInfoStatus.Disetujui.ToString) '---disetujui KTB
            al.Add(sts)
            sts = New EventInfoStatusProp(EventInfoStatus.Ditolak, EventInfoStatus.Ditolak.ToString) '---disetujui KTB
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveStatusDealer() As ArrayList
            Dim al As New ArrayList
            Dim sts As EventInfoStatusProp
            'sts = New EventInfoStatusProp(EventInfoStatus.Validasi, EventInfoStatus.Validasi.ToString)
            'al.Add(sts)
            sts = New EventInfoStatusProp(EventInfoStatus.Konfirmasi, EventInfoStatus.Konfirmasi.ToString) '---disetujui KTB
            al.Add(sts)
            sts = New EventInfoStatusProp(EventInfoStatus.Disetujui, EventInfoStatus.Disetujui.ToString) '---disetujui KTB
            al.Add(sts)
            sts = New EventInfoStatusProp(EventInfoStatus.Ditolak, EventInfoStatus.Ditolak.ToString) '---disetujui KTB
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EventInfoStatusProp
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


