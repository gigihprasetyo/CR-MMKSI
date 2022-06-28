Namespace KTB.DNet.Domain
    Public Class EnumObligationType
        Public Enum ObligationTypeStatus
            Aktif = 0
            TidakAktif = 1
        End Enum

        Public Function RetrieveObligationTypeStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumObligationTypeStatus
            sts = New EnumObligationTypeStatus(0, "Aktif")
            al.Add(sts)
            sts = New EnumObligationTypeStatus(1, "Tidak Aktif")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumObligationTypeStatus
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
