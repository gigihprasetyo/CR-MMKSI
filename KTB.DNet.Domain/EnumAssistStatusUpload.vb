Namespace KTB.DNet.Domain

    Public Class EnumAssistStatusUpload
        Public Enum StatusUpload
            GagalValidasySystem = 0
            MenungguValidasi = 1
            TolakValidasi = 2
            MenungguKonfirmasi = 3
            TolakKonfirmasi = 4
            Selesai = 5
        End Enum


        Public Function RetrieveStatusType() As ArrayList
            Dim al As New ArrayList
            Dim sts As enumassistupload
            
            sts = New enumassistupload("5", "Selesai")
            al.Add(sts)
            sts = New enumassistupload("4", "Tolak Konfirmasi")
            al.Add(sts)
            sts = New enumassistupload("3", "Menunggu Konfirmasi")
            al.Add(sts)
            sts = New enumassistupload("2", "Tolak Validasi")
            al.Add(sts)
            sts = New enumassistupload("1", "Menunggu Validasi")
            al.Add(sts)
            sts = New enumassistupload("0", "Gagal Validasi System")
            al.Add(sts)

            Return al
        End Function

    End Class

    Public Class enumassistupload
        Private _Val As String
        Private _Name As String

        Public Function GetName(ByVal month As Integer) As String
            Select Case month
                Case 0
                    Return "Gagal Validasi System"
                Case 1
                    Return "Menunggu Validasi"
                Case 2
                    Return "Tolak Validasi"
                Case 3
                    Return "Menunggu Konfirmasi"
                Case 4
                    Return "Tolak Konfirmasi"
                Case 5
                    Return "Selesai"
            End Select
        End Function

        Public Sub New(ByVal val As String, ByVal name As String)
            _Val = val
            _Name = name
        End Sub

        Public Property ValStatus() As String
            Get
                Return _Val
            End Get
            Set(ByVal Value As String)
                _Val = Value
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

