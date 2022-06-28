Namespace KTB.DNet.Domain
    Public Class EnumCategoryStatus
        Public Enum CategoryStatus
            TidakAktif
            Aktif
        End Enum

        Public Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumCategory
            sts = New EnumCategory(0, "Tidak Aktif")
            al.Add(sts)
            sts = New EnumCategory(1, "Aktif")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumCategory
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




