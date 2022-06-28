Namespace KTB.DNet.Domain

    Public Class EnumAlertManagement
        Public Enum AlertManagementType
            Transactional = 0
            Announcement = 1
        End Enum

        Public Enum AnnouncementAlertType
            OneTimeAlert = 0
            PeriodicAlert = 1
        End Enum

        Public Enum AlertMediaType
            Menit = 0
            Jam = 1
            Hari = 2
            Minggu = 3
            Bulan = 4
            Tahun = 5
        End Enum

        Public Enum TextEffects
            None = 0
            Blinking = 1
            Marching = 2
            LasVegasLights = 3
        End Enum

        Public Function RetrieveTextEffects() As ArrayList
            Dim al As New ArrayList
            Dim sts As ListEnum
            sts = New ListEnum(0, "None")
            al.Add(sts)

            sts = New ListEnum(1, "Blinking")
            al.Add(sts)

            sts = New ListEnum(2, "Marching")
            al.Add(sts)

            sts = New ListEnum(3, "Las Vegas Lights")
            al.Add(sts)

            Return al
        End Function

        Public Function RetrieveAlertManagementType() As ArrayList
            Dim al As New ArrayList
            Dim sts As ListEnum
            sts = New ListEnum(0, "Transactional")
            al.Add(sts)
            sts = New ListEnum(1, "Announcement")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveAnnouncementAlertType() As ArrayList
            Dim al As New ArrayList
            Dim sts As ListEnum
            sts = New ListEnum(0, "One Time Alert")
            al.Add(sts)
            sts = New ListEnum(1, "Periodic Alert")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveAlertMediaType() As ArrayList
            Dim al As New ArrayList
            Dim sts As ListEnum
            sts = New ListEnum(0, "Menit")
            al.Add(sts)
            sts = New ListEnum(1, "Jam")
            al.Add(sts)
            sts = New ListEnum(2, "Hari")
            al.Add(sts)
            sts = New ListEnum(3, "Minggu")
            al.Add(sts)
            sts = New ListEnum(4, "Bulan")
            al.Add(sts)
            sts = New ListEnum(5, "Tahun")
            al.Add(sts)
            Return al
        End Function
    End Class

    Public Class ListEnum
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

