Public Class EnumStatusLKPP
    Public Enum StatusLKPP
        Baru
        Validasi
        Setuju
        Tolak
        Batal_Validasi
        Batal_Setuju
        Batal_Tolak
        Hapus
    End Enum

    Public Shared Function RetrieveStatus() As ArrayList
        Dim al As New ArrayList
        Dim sts As enumStatLKPP

        sts = New enumStatLKPP(0, "Baru")
        al.Add(sts)
        sts = New enumStatLKPP(1, "Validasi")
        al.Add(sts)
        sts = New enumStatLKPP(2, "Setuju")
        al.Add(sts)
        sts = New enumStatLKPP(3, "Tolak")
        al.Add(sts)
        sts = New enumStatLKPP(4, "Batal Validasi")
        al.Add(sts)
        sts = New enumStatLKPP(5, "Batal Setuju")
        al.Add(sts)
        sts = New enumStatLKPP(6, "Batal Tolak")
        al.Add(sts)
        sts = New enumStatLKPP(7, "Hapus")
        al.Add(sts)

        Return al
    End Function

    Public Shared Function RetrieveStatusForFilter(ByVal isDealer As Boolean) As ArrayList
        Dim al As New ArrayList
        Dim sts As enumStatLKPP
        sts = New enumStatLKPP(-1, "Silahkan Pilih")
        al.Add(sts)
        If isDealer = True Then
            sts = New enumStatLKPP(0, "Baru")
            al.Add(sts)
            sts = New enumStatLKPP(1, "Validasi")
            al.Add(sts)
            sts = New enumStatLKPP(2, "Setuju")
            al.Add(sts)
            sts = New enumStatLKPP(3, "Tolak")
            al.Add(sts)
        Else
            sts = New enumStatLKPP(0, "Baru")
            al.Add(sts)
            sts = New enumStatLKPP(1, "Validasi")
            al.Add(sts)
            sts = New enumStatLKPP(2, "Setuju")
            al.Add(sts)
            sts = New enumStatLKPP(3, "Tolak")
            al.Add(sts)
        End If
        Return al
    End Function

    Public Shared Function RetrieveStatusForAction(ByVal isDealer As Boolean) As ArrayList
        Dim al As New ArrayList
        Dim sts As enumStatLKPP
        sts = New enumStatLKPP(-1, "-Silahkan Pilih-")
        al.Add(sts)
        If isDealer = True Then
            sts = New enumStatLKPP(1, "Validasi")
            al.Add(sts)
            sts = New enumStatLKPP(4, "Batal Validasi")
            al.Add(sts)
            sts = New enumStatLKPP(7, "Hapus")
            al.Add(sts)
        Else
            sts = New enumStatLKPP(2, "Setuju")
            al.Add(sts)
            sts = New enumStatLKPP(5, "Batal Setuju")
            al.Add(sts)
            sts = New enumStatLKPP(3, "Tolak")
            al.Add(sts)
            sts = New enumStatLKPP(6, "Batal Tolak")
            al.Add(sts)
        End If
        Return al
    End Function

    Public Shared Function RetrieveStatus(ByVal isIncludeBlank As Boolean) As ArrayList
        Dim al As New ArrayList
        Dim sts As enumStatLKPP

        If (isIncludeBlank) Then
            sts = New enumStatLKPP(-1, "-Silahkan Pilih-")
            al.Add(sts)
        End If

        sts = New enumStatLKPP(0, "Baru")
        al.Add(sts)
        sts = New enumStatLKPP(1, "Validasi")
        al.Add(sts)
        sts = New enumStatLKPP(2, "Setuju")
        al.Add(sts)
        sts = New enumStatLKPP(3, "Tolak")
        al.Add(sts)
        sts = New enumStatLKPP(4, "Batal Validasi")
        al.Add(sts)
        sts = New enumStatLKPP(5, "Batal Setuju")
        al.Add(sts)
        sts = New enumStatLKPP(6, "Batal Tolak")
        al.Add(sts)
        Return al
    End Function

End Class

Public Class enumStatLKPP
    Private _Val As Integer
    Private _Name As String

    Public Sub New(ByVal val As Integer, ByVal name As String)
        _Val = val
        _Name = name
    End Sub

    Public Property ValStatus() As Integer
        Get
            Return _Val
        End Get
        Set(ByVal Value As Integer)
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
