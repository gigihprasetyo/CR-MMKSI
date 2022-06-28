
Namespace KTB.DNet.Domain
    Public Class enumStatusForumCategory
        Public Enum StatusForumCategory
            Tidak_Aktif
            Aktif
        End Enum

        Public Shared Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As enumForumCategory
            sts = New EnumForumCategory(0, "Tidak_Aktif")
            al.Add(sts)
            sts = New EnumForumCategory(1, "Aktif")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumForumCategory
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


